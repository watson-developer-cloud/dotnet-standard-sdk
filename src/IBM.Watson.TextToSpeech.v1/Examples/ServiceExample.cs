/**
* (C) Copyright IBM Corp. 2019, 2020.
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
using IBM.Watson.TextToSpeech.v1.Model;
using IBM.Watson.TextToSpeech.v1.Websockets;
using System;
using System.Collections.Generic;
using System.IO;
using static IBM.Watson.TextToSpeech.v1.TextToSpeechService;

namespace IBM.Watson.TextToSpeech.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
        string customizationId;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.ListVoices();
            example.GetVoice();

            example.Synthesize();

            example.GetPronunciation();

            example.CreateVoiceModel();
            example.ListVoiceModels();
            example.UpdateVoiceModel();
            example.GetVoiceModel();

            example.AddWords();
            example.ListWords();
            example.AddWord();
            example.GetWord();

            example.DeleteWord();
            example.DeleteVoiceModel();

            example.DeleteUserData();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Voices
        public void ListVoices()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListVoices();

            Console.WriteLine(result.Result);
        }

        public void GetVoice()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetVoice("en-US_AllisonVoice");

            Console.WriteLine(result.Result);
        }
        #endregion

        #region Synthesis
        public void Synthesize()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Synthesize(
                text: "Hello world",
                accept: "audio/wav",
                voice: "en-US_AllisonVoice"
                );

            using (FileStream fs = File.Create("hello_world.wav"))
            {
                result.Result.WriteTo(fs);
                fs.Close();
                result.Result.Close();
            }
        }
        #endregion

        #region Pronunciation
        public void GetPronunciation()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetPronunciation(
                text: "IEEE",
                format: "ibm",
                voice: "en-US_AllisonVoice"
                );

            Console.WriteLine(result.Result);
        }
        #endregion

        #region Custom Models
        public void ListVoiceModels()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListCustomModels();

            Console.WriteLine(result.Result);
        }

        public void CreateVoiceModel()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateCustomModel(
                name: "First Model",
                language: "en-US",
                description: "First custom voice model"
                );

            Console.WriteLine(result.Result);

            customizationId = result.Result.CustomizationId;
        }

        public void GetVoiceModel()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetCustomModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Result);
        }

        public void UpdateVoiceModel()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var words = new List<Word>()
            {
                new Word()
                {
                    _Word = "NCAA",
                    Translation = "N C double A"
                },
                new Word()
                {
                    _Word = "iPhone",
                    Translation = "I phone"
                }
            };

            var result = service.UpdateCustomModel(
                customizationId: "{customizationId}",
                name: "First Model Update",
                description: "First custom voice model update",
                words: words
                );

            Console.WriteLine(result.Result);
        }

        public void DeleteVoiceModel()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteCustomModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Custom Words
        private void AddWords()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var words = new List<Word>()
            {
                new Word()
                {
                    _Word = "EEE",
                    Translation = "<phoneme alphabet=\"ibm\" ph=\"tr1Ipxl.1i\"></phoneme>"
                },
                new Word()
                {
                    _Word = "IEEE",
                    Translation = "<phoneme alphabet=\"ibm\" ph=\"1Y.tr1Ipxl.1i\"></phoneme>"
                }
            };

            var result = service.AddWords(
                customizationId: "{customizationId}",
                words: words
                );

            Console.WriteLine(result.StatusCode);
        }

        private void ListWords()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListWords(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void AddWord()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.AddWord(
                customizationId: "{customizationId}",
                word: "ACLs",
                translation: "ackles"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void GetWord()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetWord(
                customizationId: "{customizationId}",
                word: "ACLs"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void DeleteWord()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteWord(
                customizationId: "{customizationId}",
                word: "ACLs"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            TextToSpeechService service = new TextToSpeechService(authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteUserData(
                customerId: "customer_ID"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Websockets
        public void SynthesizeUsingWebsockets()
        {
            IamAuthenticator authenticator = new IamAuthenticator("{apikey}");
            TextToSpeechService service = new TextToSpeechService(authenticator);

            SynthesizeCallback callback = new SynthesizeCallback();

            MemoryStream soundStream = new MemoryStream();
            // Example requires SoundPlayer
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
