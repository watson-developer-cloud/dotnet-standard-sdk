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

using IBM.Cloud.SDK.Core.Util;
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

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Voices
        public void ListVoices()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.ListVoices();

            Console.WriteLine(result.Result);
        }

        public void GetVoice()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.GetVoice("en-US_AllisonVoice");

            Console.WriteLine(result.Result);
        }
        #endregion

        #region Synthesis
        public void Synthesize()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

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
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

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
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.ListVoiceModels();

            Console.WriteLine(result.Result);
        }

        public void CreateVoiceModels()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.CreateVoiceModel(
                name: "dotnet-sdk-voice-model",
                language: "en-US",
                description: "Custom voice model for .NET SDK examples.");
            customizationId = result.Result.CustomizationId;

            Console.WriteLine(result.Result);
        }

        public void GetVoiceModel()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.GetVoiceModel(
                customizationId: customizationId
                );

            Console.WriteLine(result.Result);
        }

        public void UpdateVoiceModel()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

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
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.DeleteVoiceModel(
                customizationId: customizationId
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion

        #region Custom Words
        private void AddWords()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

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
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.ListWords(
                customizationId: customizationId
                );

            Console.WriteLine(result.StatusCode);
        }

        private void AddWord()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

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
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.GetWord(
                customizationId: customizationId,
                word: "hello"
                );

            Console.WriteLine(result.StatusCode);
        }

        private void DeleteWord()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

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
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            TextToSpeechService service = new TextToSpeechService(tokenOptions);

            var result = service.DeleteUserData(
                customerId: "customerId"
                );

            Console.WriteLine(result.StatusCode);
        }
        #endregion
    }
}
