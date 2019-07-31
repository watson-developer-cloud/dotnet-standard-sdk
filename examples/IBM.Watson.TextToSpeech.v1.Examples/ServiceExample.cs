/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.TextToSpeech.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
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
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.ListVoices();

            Console.WriteLine(result.Result);
        }

        public void GetVoice()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.GetVoice("en-US_AllisonVoice");

            Console.WriteLine(result.Result);
        }
        #endregion

        #region Synthesis
        public void Synthesize()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

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
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

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
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.ListVoiceModels();

            Console.WriteLine(result.Result);
        }

        public void CreateVoiceModel()
        {
            IamConfig config = new IamConfig();
            {
                apikey = "{apikey}";
            }

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.CreateVoiceModel(
                name: "First Model",
                language: "en-US",
                description: "First custom voice model"
                );

            Console.WriteLine(result.Result);

            customizationId = result.Result.CustomizationId;
        }

        public void GetVoiceModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.GetVoiceModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.Result);
        }

        public void UpdateVoiceModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

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

            var result = service.UpdateVoiceModel(
                customizationId: "{customizationId}",
                name: "First Model Update",
                description: "First custom voice model update",
                words: words
                );

            Console.WriteLine(result.Result);
        }

        public void DeleteVoiceModel()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteVoiceModel(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Custom Words
        private void AddWords()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

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
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.ListWords(
                customizationId: "{customizationId}"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void AddWord()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.AddWord(
                customizationId: "{customizationId}",
                word: "ACLs",
                translation: "ackles"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void GetWord()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.GetWord(
                customizationId: "{customizationId}",
                word: "ACLs"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void DeleteWord()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

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
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint("{url}");

            var result = service.DeleteUserData(
                customerId: "customer_ID"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion
    }
}
