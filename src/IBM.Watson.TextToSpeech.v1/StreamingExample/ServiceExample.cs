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
using IBM.Watson.TextToSpeech.v1.Websockets.Util;
using System;
using System.IO;
//using System.Media;
using System.Threading;
using System.Threading.Tasks;
using static IBM.Watson.TextToSpeech.v1.TextToSpeechService;

namespace IBM.Watson.TextToSpeech.v1.StreamingExample
{
    public class StreamingExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";

        string testText = "Jabberwocky is a nonsense poem written by Lewis Carroll about the killing of a creature named the Jabberwock. It was included in his 1871 novel Through the Looking-Glass, and What Alice Found There, the sequel to Alice's Adventures in Wonderland (1865). The book tells of Alice's adventures within the back-to-front world of Looking-Glass Land. In an early scene in which she first encounters the chess piece characters White King and White Queen, Alice finds a book written in a seemingly unintelligible language. Realizing that she is travelling through an inverted world, she recognises that the verses on the pages are written in mirror-writing. She holds a mirror to one of the poems and reads the reflected verse of Jabberwocky. She finds the nonsense verse as puzzling as the odd land she has passed into, later revealed as a dreamscape.";
        
        static void Main(string[] args)
        {
            StreamingExample example = new StreamingExample();
            example.SynthesizeUsingWebsockets();
        }

        // Show how to use traditional functions for callbacks rather than anonymous
        public void onOpen()
        {
            Console.WriteLine("OnOpen()");
        }

        public void onClose()
        {
            Console.WriteLine("OnClose()");
        }

        #region SynthesizeUsingWebsockets
        public void SynthesizeUsingWebsockets()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: apikey);

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl(url);

            SynthesizeCallback callback = new SynthesizeCallback();

            MemoryStream soundStream = new MemoryStream();
            //SoundPlayer player = new SoundPlayer(soundStream);

            callback.OnOpen = () => onOpen();

            callback.OnClose = () => onClose();

            callback.OnMessage = (bytes) =>
            {
                //  SoundPlayer needs .net core 3
                //player.Stream.Position = player.Stream.Length;
                //player.Stream.WriteAsync(bytes, 0, bytes.Length);
                //WaveUtils.ReWriteWaveHeader((MemoryStream)player.Stream);
                //Console.WriteLine(string.Format("OnMessage({0})", player.Stream.Length));

                soundStream.Position = soundStream.Length;
                soundStream.WriteAsync(bytes, 0, bytes.Length);
                WaveUtils.ReWriteWaveHeader(soundStream);
                Console.WriteLine(string.Format("OnMessage({0})", soundStream.Length));
            };

            var synthesizeResult = Task.Run(() => service.SynthesizeUsingWebsockets(
                voice: SynthesizeEnums.VoiceValue.EN_US_ALLISONVOICE,
                callback: callback,
                accept: SynthesizeEnums.AcceptValue.AUDIO_WAV,
                text: testText));

            //  buffer
            while(soundStream.Length < 65536 * 8)
            {
                Thread.Sleep(1000);
            }

            //Task.Run(() => player.Play());

            Console.ReadKey();
        }
        #endregion
    }
}
