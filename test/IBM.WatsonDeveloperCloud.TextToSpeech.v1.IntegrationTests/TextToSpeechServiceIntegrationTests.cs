/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.IntegrationTests
{
    [TestClass]
    public class TextToSpeechServiceIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private TextToSpeechService service;
        private static string credentials = string.Empty;
        private const string _allisonVoice = "en-US_AllisonVoice";
        private string _synthesizeText = "Hello, welcome to the Watson dotnet SDK!";
        private string _voiceModelName = "dotnet-sdk-voice-model";
        private string _voiceModelUpdatedName = "dotnet-sdk-voice-model-updated";
        private string _voiceModelDescription = "Custom voice model for .NET SDK integration tests.";
        private string _voiceModelUpdatedDescription = "Custom voice model for .NET SDK integration tests. Updated.";

        [TestInitialize]
        public void Setup()
        {
            if (string.IsNullOrEmpty(credentials))
            {
                try
                {
                    credentials = Utility.SimpleGet(
                        Environment.GetEnvironmentVariable("VCAP_URL"),
                        Environment.GetEnvironmentVariable("VCAP_USERNAME"),
                        Environment.GetEnvironmentVariable("VCAP_PASSWORD")).Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Failed to get credentials: {0}", e.Message));
                }

                Task.WaitAll();

                var vcapServices = JObject.Parse(credentials);
                _endpoint = vcapServices["text_to_speech"]["url"].Value<string>();
                _username = vcapServices["text_to_speech"]["username"].Value<string>();
                _password = vcapServices["text_to_speech"]["password"].Value<string>();
            }

            service = new TextToSpeechService(_username, _password);
            service.Endpoint = _endpoint;
        }

        #region Voices
        [TestMethod]
        public void Voices_Success()
        {
            var listVoicesResult = ListVoices();

            var getVoiceResult = GetVoice(_allisonVoice);


            Assert.IsNotNull(getVoiceResult);
            Assert.IsTrue(!string.IsNullOrEmpty(getVoiceResult.Name));
            Assert.IsNotNull(listVoicesResult);
            Assert.IsNotNull(listVoicesResult._Voices);
        }
        #endregion

        #region Synthesize
        [TestMethod]
        public void Synthesize_Success()
        {
            Text synthesizeText = new Text
            {
                _Text = _synthesizeText
            };

            var synthesizeResult = Synthesize(synthesizeText, "audio/wave");

            Assert.IsNotNull(synthesizeResult);
        }
        #endregion

        #region Pronunciation
        [TestMethod]
        public void Pronunciation_Success()
        {
            var getPronunciationResult = GetPronunciation("IBM");

            Assert.IsNotNull(getPronunciationResult);
        }
        #endregion

        #region Custom Voice Models
        [TestMethod]
        public void CustomVoiceModels_Success()
        {
            var listVoiceModelsResult = ListVoiceModels();

            var createVoiceModel = new CreateVoiceModel
            {
                Name = _voiceModelName,
                Description = _voiceModelDescription,
                Language = Model.CreateVoiceModel.LanguageEnum.EN_US
            };

            var createVoiceModelResult = CreateVoiceModel(createVoiceModel);
            var customizationId = createVoiceModelResult.CustomizationId;

            var getVoiceModelResult = GetVoiceModel(customizationId);

            var updateVoiceModel = new UpdateVoiceModel
            {
                Name = _voiceModelUpdatedName,
                Description = _voiceModelUpdatedDescription,
                Words = new System.Collections.Generic.List<Word>()
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
                }
            };

            var updateVoiceModelResult = UpdateVoiceModel(customizationId, updateVoiceModel);

            var deleteVoiceModelResult = DeleteVoiceModel(customizationId);

            Assert.IsNotNull(updateVoiceModel);
            Assert.IsTrue(updateVoiceModel.Name == _voiceModelUpdatedName);
            Assert.IsTrue(updateVoiceModel.Description == _voiceModelUpdatedDescription);
            Assert.IsTrue(updateVoiceModel.Words.Count == 3);
            Assert.IsNotNull(getVoiceModelResult);
            Assert.IsTrue(getVoiceModelResult.Name == _voiceModelName);
            Assert.IsTrue(getVoiceModelResult.Description == _voiceModelDescription);
            Assert.IsNotNull(createVoiceModelResult);
            Assert.IsNotNull(listVoiceModelsResult);
            Assert.IsNotNull(listVoiceModelsResult.Customizations);
        }
        #endregion

        #region Words
        [TestMethod]
        private void Words_Success()
        {
            var createVoiceModel = new CreateVoiceModel
            {
                Name = _voiceModelName,
                Description = _voiceModelDescription,
                Language = Model.CreateVoiceModel.LanguageEnum.EN_US
            };

            var createVoiceModelResult = CreateVoiceModel(createVoiceModel);
            var customizationId = createVoiceModelResult.CustomizationId;

            var customWords = new Words
            {
                _Words = new System.Collections.Generic.List<Word>()
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
            }
            };

            var addWordsResult = AddWords(customizationId, customWords);

            var listWordsResult = ListWords(customizationId);

            var getWordResult = GetWord(customizationId, "hello");

            var wordTranslation = new Translation
            {
                _Translation = "eye bee m"
            };

            var addWordResult = AddWord(customizationId, "IBM", wordTranslation);

            var checkAddWordResult = ListWords(customizationId);

            var deleteWordResult = DeleteWord(customizationId, "hi");

            var checkDeleteWordResult = ListWords(customizationId);

            var deleteVoiceModelResult = DeleteVoiceModel(customizationId);

            Assert.IsNotNull(checkDeleteWordResult);
            Assert.IsNotNull(checkDeleteWordResult._Words);
            Assert.IsTrue(checkDeleteWordResult._Words.Count == 3);
            Assert.IsNotNull(checkAddWordResult);
            Assert.IsNotNull(checkAddWordResult._Words);
            Assert.IsTrue(checkAddWordResult._Words.Count == 4);
            Assert.IsNotNull(getWordResult);
            Assert.IsTrue(getWordResult._Translation == "hullo");
            Assert.IsNotNull(listWordsResult);
            Assert.IsNotNull(listWordsResult._Words);
            Assert.IsTrue(listWordsResult._Words.Count == 3);
            Assert.IsNotNull(addWordsResult);
        }
        #endregion

        #region Generated
        #region GetVoice
        private Voice GetVoice(string voice, string customizationId = null)
        {
            Console.WriteLine("\nAttempting to GetVoice()");
            var result = service.GetVoice(voice: voice, customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("GetVoice() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetVoice()");
            }

            return result;
        }
        #endregion

        #region ListVoices
        private Voices ListVoices()
        {
            Console.WriteLine("\nAttempting to ListVoices()");
            var result = service.ListVoices();

            if (result != null)
            {
                Console.WriteLine("ListVoices() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListVoices()");
            }

            return result;
        }
        #endregion

        #region Synthesize
        private System.IO.Stream Synthesize(Text text, string accept, string voice = null, string customizationId = null)
        {
            Console.WriteLine("\nAttempting to Synthesize()");
            var result = service.Synthesize(text: text, accept: accept, voice: voice, customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("Synthesize() succeeded!");
            }
            else
            {
                Console.WriteLine("Failed to Synthesize()");
            }

            return result;
        }
        #endregion

        #region GetPronunciation
        private Pronunciation GetPronunciation(string text, string voice = null, string format = null, string customizationId = null)
        {
            Console.WriteLine("\nAttempting to GetPronunciation()");
            var result = service.GetPronunciation(text: text, voice: voice, format: format, customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("GetPronunciation() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetPronunciation()");
            }

            return result;
        }
        #endregion

        #region CreateVoiceModel
        private VoiceModel CreateVoiceModel(CreateVoiceModel createVoiceModel)
        {
            Console.WriteLine("\nAttempting to CreateVoiceModel()");
            var result = service.CreateVoiceModel(createVoiceModel: createVoiceModel);

            if (result != null)
            {
                Console.WriteLine("CreateVoiceModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateVoiceModel()");
            }

            return result;
        }
        #endregion

        #region DeleteVoiceModel
        private object DeleteVoiceModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to DeleteVoiceModel()");
            var result = service.DeleteVoiceModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("DeleteVoiceModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteVoiceModel()");
            }

            return result;
        }
        #endregion

        #region GetVoiceModel
        private VoiceModel GetVoiceModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to GetVoiceModel()");
            var result = service.GetVoiceModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("GetVoiceModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetVoiceModel()");
            }

            return result;
        }
        #endregion

        #region ListVoiceModels
        private VoiceModels ListVoiceModels(string language = null)
        {
            Console.WriteLine("\nAttempting to ListVoiceModels()");
            var result = service.ListVoiceModels(language: language);

            if (result != null)
            {
                Console.WriteLine("ListVoiceModels() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListVoiceModels()");
            }

            return result;
        }
        #endregion

        #region UpdateVoiceModel
        private object UpdateVoiceModel(string customizationId, UpdateVoiceModel updateVoiceModel)
        {
            Console.WriteLine("\nAttempting to UpdateVoiceModel()");
            var result = service.UpdateVoiceModel(customizationId: customizationId, updateVoiceModel: updateVoiceModel);

            if (result != null)
            {
                Console.WriteLine("UpdateVoiceModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateVoiceModel()");
            }

            return result;
        }
        #endregion

        #region AddWord
        private object AddWord(string customizationId, string word, Translation translation)
        {
            Console.WriteLine("\nAttempting to AddWord()");
            var result = service.AddWord(customizationId: customizationId, word: word, translation: translation);

            if (result != null)
            {
                Console.WriteLine("AddWord() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to AddWord()");
            }

            return result;
        }
        #endregion

        #region AddWords
        private object AddWords(string customizationId, Words customWords)
        {
            Console.WriteLine("\nAttempting to AddWords()");
            var result = service.AddWords(customizationId: customizationId, customWords: customWords);

            if (result != null)
            {
                Console.WriteLine("AddWords() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to AddWords()");
            }

            return result;
        }
        #endregion

        #region DeleteWord
        private object DeleteWord(string customizationId, string word)
        {
            Console.WriteLine("\nAttempting to DeleteWord()");
            var result = service.DeleteWord(customizationId: customizationId, word: word);

            if (result != null)
            {
                Console.WriteLine("DeleteWord() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteWord()");
            }

            return result;
        }
        #endregion

        #region GetWord
        private Translation GetWord(string customizationId, string word)
        {
            Console.WriteLine("\nAttempting to GetWord()");
            var result = service.GetWord(customizationId: customizationId, word: word);

            if (result != null)
            {
                Console.WriteLine("GetWord() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetWord()");
            }

            return result;
        }
        #endregion

        #region ListWords
        private Words ListWords(string customizationId)
        {
            Console.WriteLine("\nAttempting to ListWords()");
            var result = service.ListWords(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("ListWords() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListWords()");
            }

            return result;
        }
        #endregion
        #endregion
    }
}