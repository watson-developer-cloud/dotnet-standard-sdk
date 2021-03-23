/**
* (C) Copyright IBM Corp. 2021.
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

using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.TextToSpeech.v1.Websockets;
using System;
using System.IO;
using static IBM.Watson.TextToSpeech.v1.TextToSpeechService;

namespace IBM.Watson.TextToSpeech.v1.Examples.Streaming
{
    public class StreamingExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
        string customizationId;

        static void Main(string[] args)
        {
            StreamingExample example = new StreamingExample();
            example.SynthesizeUsingWebsockets();
        }

        #region SynthesizeUsingWebsockets
        public void SynthesizeUsingWebsockets()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            SynthesizeCallback callback = new SynthesizeCallback();

            MemoryStream soundStream = new MemoryStream();

            SoundPlayer player = new SoundPlayer(soundStream);

            callback.OnOpen = () =>
            {
                Console.WriteLine("open");
            };

            callback.OnClose = () =>
            {
                Console.WriteLine("close");
            };
            callback.OnMessage = (bytes) =>
            {
                player.Stream.Position = player.Stream.Length;
                player.Stream.WriteAsync(bytes, 0, bytes.Length);
                WaveUtils.ReWriteWaveHeader((MemoryStream)player.Stream);
                Console.WriteLine("new message call");
            };

            var synthesizeResult = service.SynthesizeUsingWebsockets(
                voice: SynthesizeEnums.VoiceValue.EN_US_ALLISONVOICE,
                callback: callback,
                accept: SynthesizeEnums.AcceptValue.AUDIO_WAV,
                timings: new string[] { "words" },
                text: "Lorem Ipsum is simply dummy text of the printing and typesetting industry.");
            player.PlaySync();
        }
        #endregion
    }
}
