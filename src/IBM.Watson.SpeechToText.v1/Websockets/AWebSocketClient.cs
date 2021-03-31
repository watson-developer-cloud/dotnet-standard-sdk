/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.Watson.SpeechToText.v1.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IBM.Watson.SpeechToText.v1.Websockets
{
    public abstract class AWebSocketClient
    {
        public const string Results = "results";
        public const string Error = "error";
        public const string State = "state";

        ArraySegment<byte> stopMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(
           "{\"action\": \"stop\"}"
        ));

        private const int ReceiveChunkSize = 1024 * 16 * 4;

        protected ClientWebSocket BaseClient { get; set; }
        protected UriBuilder UriBuilder { get; set; }
        protected Dictionary<string, string> QueryString { get; set; }
        protected Dictionary<string, object> WebSocketParameters { get; set; }

        public Action OnOpen = () => { };
        public Action<SpeechRecognitionResults> OnMessage = (speechResults) => { };
        public Action<Exception> OnError = (ex) => { };
        public Action OnClose = () => { };

        public bool IsNumber(object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        public AWebSocketClient AddArgument(string argumentName, string argumentValue)
        {
            if (QueryString.ContainsKey(argumentName))
                QueryString[argumentName] = argumentValue;
            else
                QueryString.Add(argumentName, argumentValue);

            UriBuilder.Query =
                string.Join("&", QueryString.Keys.Where(key => !string.IsNullOrWhiteSpace(QueryString[key])).Select(key => string.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(QueryString[key]))));

            return this;
        }
        public AWebSocketClient AddWebSocketParameter(string key, object value)
        {
            if (WebSocketParameters.ContainsKey(key))
                WebSocketParameters[key] = value;
            else
                WebSocketParameters.Add(key, value);

            return this;
        }
        public AWebSocketClient WithAuthentication(string userName, string password)
        {
            BaseClient.Options.SetRequestHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + password)));
            return this;
        }
        public AWebSocketClient WithHeader(string headerName, string headerValue)
        {
            BaseClient.Options.SetRequestHeader(headerName, headerValue);
            return this;
        }

        public abstract void Send(MemoryStream file, string openingMessage);

        protected async Task SendAudio(MemoryStream stream)
        {
            byte[] b = new byte[1024];

            while (stream.Read(b, 0, b.Length) > 0 && BaseClient.State == WebSocketState.Open)
            {
                await BaseClient.SendAsync(new ArraySegment<byte>(b), WebSocketMessageType.Binary, true, CancellationToken.None);
            }
            if (BaseClient.State == WebSocketState.Open)
            {
                await BaseClient.SendAsync(stopMessage, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        protected async Task HandleResults()
        {
            var buffer = new byte[ReceiveChunkSize];
            while (BaseClient.State == WebSocketState.Open)
            {
                var segment = new ArraySegment<byte>(buffer);

                var result = await BaseClient.ReceiveAsync(segment, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    return;
                }

                int count = result.Count;

                while (!result.EndOfMessage)
                {
                    segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    result = await BaseClient.ReceiveAsync(segment, CancellationToken.None);
                    count += result.Count;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, count);
                var json = JObject.Parse(message);

                if (json.ContainsKey(Error))
                {
                    OnError(new Exception(json[Error].ToString()));
                }
                else if (json.ContainsKey(State))
                {
                    return;
                }
                else
                {
                    SpeechRecognitionResults results = json.ToObject<SpeechRecognitionResults>();
                    OnMessage(results);
                }
            }
        }

        protected async Task HandleOpenMessage()
        {
            var buffer = new byte[ReceiveChunkSize];

            var segment = new ArraySegment<byte>(buffer);

            var result = await BaseClient.ReceiveAsync(segment, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                return;
            }

            int count = result.Count;

            while (!result.EndOfMessage)
            {
                segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                result = await BaseClient.ReceiveAsync(segment, CancellationToken.None);
                count += result.Count;
            }
            var message = Encoding.UTF8.GetString(buffer, 0, count);
            var json = JObject.Parse(message);

            if (json.ContainsKey(Error))
            {
                OnError(new Exception(json[Error].ToString()));
                BaseClient.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).Wait();
            }
        }
    }
}