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
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.ListVoices();

            Console.WriteLine(result.Result);
        }

        public void GetVoice()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.GetVoice("en-US_AllisonVoice");

            Console.WriteLine(result.Result);
        }
        #endregion

        #region Synthesis
        public void Synthesize()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.Synthesize(
                text: "Hello, welcome to the Watson dotnet SDK!",
                accept: "audio/wav",
                voice: "en-US_AllisonVoice"
                );

            //  Save file
            using (FileStream fs = File.Create("synthesize.wav"))
            {
                result.Result.WriteTo(fs);
                fs.Close();
                result.Result.Close();
            }

            Console.WriteLine(result.Result);
        }
        #endregion

        #region Pronunciation
        public void GetPronunciation()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.GetPronunciation(
                text: "IBM",
                voice: "en-US_AllisonVoice",
                format: "ipa"
                );

            Console.WriteLine(result.Result);
        }
        #endregion

        #region Custom Models
        public void ListVoiceModels()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.ListVoiceModels();

            Console.WriteLine(result.Result);
        }

        public void CreateVoiceModel()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.CreateVoiceModel(
                name: "dotnet-sdk-voice-model",
                language: "en-US",
                description: "Custom voice model for .NET SDK examples.");
            customizationId = result.Result.CustomizationId;

            Console.WriteLine(result.Result);
        }

        public void GetVoiceModel()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.GetVoiceModel(
                customizationId: customizationId
                );

            Console.WriteLine(result.Result);
        }

        public void UpdateVoiceModel()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var words = new List<Word>()
            {
                new Word()
                {
                    _Word = "hello",
                    Translation = "hullo"
                },
                new Word()
                {
                    _Word = "goodbye",
                    Translation = "gbye"
                },
                new Word()
                {
                    _Word = "hi",
                    Translation = "ohioooo"
                }
            };

            var result = service.UpdateVoiceModel(
                customizationId: customizationId,
                name: "dotnet-sdk-voice-model-updated",
                description: "Custom voice model for .NET SDK integration tests. Updated.",
                words: words
                );

            Console.WriteLine(result.Result);
        }

        public void DeleteVoiceModel()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.DeleteVoiceModel(
                customizationId: customizationId
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Custom Words
        private void AddWords()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var words = new List<Word>()
            {
                new Word()
                {
                    _Word = "hello",
                    Translation = "hullo"
                },
                new Word()
                {
                    _Word = "goodbye",
                    Translation = "gbye"
                },
                new Word()
                {
                    _Word = "hi",
                    Translation = "ohioooo"
                }
            };

            var result = service.AddWords(
                customizationId: customizationId,
                words: words
                );

            Console.WriteLine(result.StatusCode);
        }

        private void ListWords()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.ListWords(
                customizationId: customizationId
                );

            Console.WriteLine(result.StatusCode);
        }

        private void AddWord()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.AddWord(
                customizationId: customizationId,
                word: "IBM",
                translation: "eye bee m",
                partOfSpeech: "noun"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void GetWord()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.GetWord(
                customizationId: customizationId,
                word: "hello"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void DeleteWord()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.DeleteWord(
                customizationId: customizationId,
                word: "hello"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            TextToSpeechService service = new TextToSpeechService(config);
            service.SetEndpoint(url);

            var result = service.DeleteUserData(
                customerId: "customerId"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion
    }
}
