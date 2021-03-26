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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IBM.Watson.SpeechToText.v1.Websockets
{
    public class WebSocketClient : AWebSocketClient
    {
        private WebSocketClient() { }
        public WebSocketClient(string urlService, RecognizeCallback callback)
        {
            BaseClient =
                new ClientWebSocket();

            UriBuilder =
                new UriBuilder(urlService);

            QueryString =
                new Dictionary<string, string>();

            WebsocketsParameters =
                new Dictionary<string, object>();

            OnOpen = callback.OnOpen;
            OnClose = callback.OnClose;
            OnError = callback.OnError;
            OnTranscription = callback.OnTranscription;
        }

        public override void Send(MemoryStream stream, string openingMessage = "start")
        {
            openingMessage =
                $"\"action\": \"{openingMessage}\"";

            foreach (KeyValuePair<string, object> entry in WebsocketsParameters)
            {
                if (IsNumber(entry.Value))
                {
                    openingMessage += $",\"{entry.Key}\": {entry.Value}";
                }
                else
                {
                    openingMessage += $",\"{entry.Key}\": \"{entry.Value}\"";
                }
            }

            openingMessage = $"{{{openingMessage}}}";

            string closingMessage =
                $"\"action\": \"stop\"";

            closingMessage = $"{{{closingMessage}}}";

            // connect the websocket
            Action connectAction = () => BaseClient.ConnectAsync(UriBuilder.Uri, CancellationToken.None).Wait();

            // send opening message and wait for initial delimeter 
            Action<ArraySegment<byte>> openAction = (message) =>
            {
                OnOpen();
                Task.WaitAll(BaseClient.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None), HandleOpenMessage());
            };
            // send all audio and then a closing message; simltaneously print all results until delimeter is recieved
            Action sendAction = () => Task.WaitAll(SendAudio(stream), HandleResults());

            // close down the websocket
            Action closeAction = () =>
            {
                ArraySegment<byte> closeMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(closingMessage));     
                if (BaseClient.State == WebSocketState.Open)
                {
                    Task.WaitAll(BaseClient.SendAsync(closeMessage, WebSocketMessageType.Text, true, CancellationToken.None));
                    BaseClient.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).Wait();
                }
                OnClose();
            };
            ArraySegment<byte> openMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(openingMessage));

            Task.Factory.StartNew(() => connectAction())
                            .ContinueWith((antecedent) =>
                            {
                                if (antecedent.Status == TaskStatus.Faulted)
                                    if (antecedent.Exception != null)
                                        OnError(antecedent.Exception.InnerException);
                            })
                            .ContinueWith((antecedent) => openAction(openMessage), TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith((antecedent) =>
                            {
                                if (antecedent.Status == TaskStatus.Faulted)
                                    if (antecedent.Exception != null)
                                        OnError(antecedent.Exception.InnerException);
                            })
                            .ContinueWith((antecedent) => sendAction(), TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith((antecedent) =>
                            {
                                if (antecedent.Status == TaskStatus.Faulted)
                                    if (antecedent.Exception != null)
                                        OnError(antecedent.Exception.InnerException);
                            })
                            .ContinueWith((antecedent) => closeAction(), TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith((antecedent) =>
                            {
                                if (antecedent.Status == TaskStatus.Faulted)
                                    if (antecedent.Exception != null)
                                        OnError(antecedent.Exception.InnerException);
                            })
                            .Wait();
        }
    }
}