﻿using System;
using System.Collections.Generic;
using System.Linq;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.TextToSpeech.v1.Websockets;

namespace IBM.Watson.TextToSpeech.v1
{
    public partial class TextToSpeechService : IBMService, ITextToSpeechService
    {
        public WebSocketClient SynthesizeUsingWebSocket(SynthesizeCallback callback, string text, string voice = null, string customizationId = null, string accept = null, List<string> timings = null)
        {
            if (callback == null) 
            {
                throw new ArgumentNullException("callback cannot be null");
            }
            if (text == null)
            {
                throw new ArgumentNullException("text cannot be null");
            }

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                string url = ($"{this.Endpoint}/v1/synthesize").Replace("https://", "wss://");
                WebSocketClient webSocketClient = new WebSocketClient(url, callback);

                if (!string.IsNullOrEmpty(voice))
                {
                    webSocketClient.AddArgument("voice", voice);
                }
                if (!string.IsNullOrEmpty(customizationId))
                {
                    webSocketClient.AddArgument("customization_id", customizationId);
                }
                if (!string.IsNullOrEmpty(accept))
                {
                    webSocketClient.AddWebSocketParameter("accept", accept);
                }
                if (timings != null)
                {
                    webSocketClient.AddWebSocketParameter("timings", string.Join(",", timings.Select(x => "\"" + x + "\"")));
                }
                var sdkHeaders = Common.GetSdkHeaders("text_to_speech", "v1", "SynthesizeUsingWebSocket");
                foreach (var header in sdkHeaders)
                {
                    webSocketClient.WithHeader(header.Key, header.Value);
                }

                foreach (var header in customRequestHeaders)
                {
                    webSocketClient.WithHeader(header.Key, header.Value);
                }                

                foreach (var header in client.BaseClient.DefaultRequestHeaders)
                {
                    var enumerator = header.Value.GetEnumerator();
                    enumerator.MoveNext();
                    var value = enumerator.Current;
                    webSocketClient = (WebSocketClient) webSocketClient.WithHeader(header.Key, value);
                }
                webSocketClient.Send(text);

                return webSocketClient;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }
    }
}