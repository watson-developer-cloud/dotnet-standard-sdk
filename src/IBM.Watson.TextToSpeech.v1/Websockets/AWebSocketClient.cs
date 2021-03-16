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

namespace IBM.Watson.TextToSpeech.v1.Websockets
{
    public abstract class AWebSocketClient
    {
        public const string BINARY_STREAMS = "binary_streams";
        public const string CONTENT_TYPE = "content_type";
        public const string MARKS = "marks";
        public const string WORDS = "words";

        ArraySegment<byte> stopMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(
           "{\"action\": \"stop\"}"
       ));

        private const int ReceiveChunkSize = 1024;
        private const int SendChunkSize = 1024;

        protected ClientWebSocket BaseClient { get; set; }
        protected UriBuilder UriBuilder { get; set; }
        protected Dictionary<string, string> QueryString { get; set; }

        public Action OnOpen = () => { };
        public Action<byte[]> OnMessage = (message) => { };
        public Action<string> OnContentType = (contentType) => { };
        public Action<MarkTiming[]> OnMarks = (marks) => { };
        public Action<WordTiming[]> onTimings = (timings) => { };
        public Action<Exception> OnError = (ex) => { };
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

        public abstract void Send(FileStream file, string openingMessage);

        public abstract void Send(string request, string accept, string[] timings, string openingMessage);

        protected async Task SendAudio(FileStream file)
        {
            byte[] b = new byte[1024];
            while (file.Read(b, 0, b.Length) > 0)
            {
                await BaseClient.SendAsync(new ArraySegment<byte>(b), WebSocketMessageType.Binary, true, CancellationToken.None);
            }
            await BaseClient.SendAsync(stopMessage, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        protected async Task SendText(string message)
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

        protected async Task HandleResults()
        {
            var buffer = new byte[1024 * 16 * 4];
            var audioStream = new List<byte>();

            while (true)
            {

                var segment = new ArraySegment<byte>(buffer);

                var result = await BaseClient.ReceiveAsync(segment, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    return;
                }
                //58290
                int count = result.Count;
                while (!result.EndOfMessage)
                {
                    if (count >= buffer.Length)
                    {
                        if (BaseClient.State == WebSocketState.Open)
                        {
                            await BaseClient.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
                        }
                        return;
                    }

                    segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    result = await BaseClient.ReceiveAsync(segment, CancellationToken.None);
                    count += result.Count;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, count);

                if (message.Contains(BINARY_STREAMS))
                {
                    var json = JObject.Parse(message);
                    string contentType = json[BINARY_STREAMS][0][CONTENT_TYPE].ToString();
                    OnContentType(contentType);
                }
                else if (message.Contains(MARKS))
                {
                    var json = JObject.Parse(message);
                    JToken marks = json[MARKS];
                    MarkTiming[] markList = new MarkTiming[marks.Count()];
                    for (int i = 0; i < markList.Length; i++)
                    {
                        MarkTiming markTiming = new MarkTiming(marks[i][0].ToString(), Double.Parse(marks[i][1].ToString()));
                        markList[i] = markTiming;
                    }
                    OnMarks(markList);
                }
                else if (message.Contains(WORDS))
                {
                    var json = JObject.Parse(message);
                    JToken words = json[WORDS];
                    WordTiming[] wordList = new WordTiming[words.Count()];
                    for (int i = 0; i < wordList.Length; i++)
                    {
                        WordTiming wordTiming = new WordTiming(words[i][0].ToString(), Double.Parse(words[i][1].ToString()), Double.Parse(words[i][2].ToString()));
                        wordList[i] = wordTiming;
                    }
                    onTimings(wordList);
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