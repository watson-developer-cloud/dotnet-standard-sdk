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

namespace IBM.WatsonDeveloperCloud.Sockets
{
    public class WebSocketClient : AWebSocketClient
    {
        private WebSocketClient() { }
        public WebSocketClient(string urlService)
        {
            BaseClient =
                new ClientWebSocket();

            UriBuilder =
                new UriBuilder(urlService);

            QueryString =
                new Dictionary<string, string>();
        }

        public override void Send(FileStream file, string openingMessage)
        {
            // connect the websocket
            Action connectAction = () => BaseClient.ConnectAsync(UriBuilder.Uri, CancellationToken.None).Wait();

            // send opening message and wait for initial delimeter 
            Action<ArraySegment<byte>> openAction = (message) => Task.WaitAll(BaseClient.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None), HandleResults());

            // send all audio and then a closing message; simltaneously print all results until delimeter is recieved
            Action sendAction = () => Task.WaitAll(SendAudio(file), HandleResults());

            // close down the websocket
            Action closeAction = () => BaseClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None).Wait();

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

        public override void Send(string request, string openingMessage)
        {
            // connect the websocket
            Action connectAction = () => BaseClient.ConnectAsync(UriBuilder.Uri, CancellationToken.None).Wait();

            // send opening message and wait for initial delimeter 
            Action<ArraySegment<byte>> openAction = (message) => Task.WaitAll(BaseClient.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None), HandleResults());

            // send all audio and then a closing message; simltaneously print all results until delimeter is recieved
            Action sendAction = () => Task.WaitAll(SendText(request), HandleResults());

            // close down the websocket
            Action closeAction = () => BaseClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None).Wait();

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