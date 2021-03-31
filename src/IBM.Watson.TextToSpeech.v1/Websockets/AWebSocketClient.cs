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

using IBM.Watson.TextToSpeech.v1.Websockets.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IBM.Watson.TextToSpeech.v1.Websockets
{
    public abstract class AWebSocketClient
    {
        public const string BinaryStreams = "binary_streams";
        public const string ContentType = "content_type";
        public const string Marks = "marks";
        public const string Words = "words";

        private const int ReceiveChunkSize = 1024 * 16 * 4;
        private const int SendChunkSize = 1024;

        protected ClientWebSocket BaseClient { get; set; }
        protected UriBuilder UriBuilder { get; set; }
        protected Dictionary<string, string> QueryString { get; set; }
        protected Dictionary<string, object> WebSocketParameters { get; set; }

        public Action OnOpen = () => { };
        public Action<byte[]> OnMessage = (message) => { };
        public Action<string> OnContentType = (contentType) => { };
        public Action<MarkTiming> OnMarks = (marks) => { };
        public Action<WordTiming> onTimings = (timings) => { };
        public Action<Exception> OnError = (e) => { };
        public Action OnClose = () => { };

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
            this.BaseClient.Options.SetRequestHeader(headerName, headerValue);
            return this;
        }

        public abstract void Send(string request, string openingMessage);

        protected async Task SendText(string message)
        {
            if (BaseClient.State == WebSocketState.Open)
            {
                var messageBuffer = Encoding.UTF8.GetBytes(message);
                var messagesCount = (int)Math.Ceiling((double)messageBuffer.Length / SendChunkSize);

                for (var i = 0; i < messagesCount; i++)
                {
                    var offset = (SendChunkSize * i);
                    var count = SendChunkSize;
                    var lastMessage = ((i + 1) == messagesCount);

                    if ((count * (i + 1)) > messageBuffer.Length)
                    {
                        count = messageBuffer.Length - offset;
                    }
                    await BaseClient.SendAsync(new ArraySegment<byte>(messageBuffer, offset, count), WebSocketMessageType.Text, lastMessage, CancellationToken.None);
                }
            }
        }

        protected async Task HandleResults()
        {
            var buffer = new byte[ReceiveChunkSize];
            var audioStream = new List<byte>();

            while (true)
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

                if (message.Contains(BinaryStreams))
                {
                    var json = JObject.Parse(message);
                    string contentType = json[BinaryStreams][0][ContentType].ToString();
                    OnContentType(contentType);
                }
                else if (message.Contains(Marks))
                {
                    var json = JObject.Parse(message);
                    JToken marks = json[Marks];
                    MarkTiming markTiming = json.ToObject<MarkTiming>();
                    OnMarks(markTiming);
                }
                else if (message.Contains(Words))
                {
                    var json = JObject.Parse(message);
                    JToken words = json[Words];
                    WordTiming wordTimings = json.ToObject<WordTiming>();
                    onTimings(wordTimings);
                }
                else
                {
                    byte[] messageBuffer = new byte[count];
                    Array.Copy(buffer, messageBuffer, count);
                    OnMessage(messageBuffer);
                }
            }
        }
    }
}