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
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.IntegrationTests
{
    [TestClass]
    public class TextToSpeechServiceIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private TextToSpeechService _service;
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
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";
                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                var vcapServices = JObject.Parse(credentials);

                Credential credential = vcapCredentials.GetCredentialByname("text-to-speech-sdk")[0].Credentials;
                _endpoint = credential.Url;
                _username = credential.Username;
                _password = credential.Password;
            }
            #endregion

            _service = new TextToSpeechService(_username, _password);
            _service.SetEndpoint(_endpoint);
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

            var synthesizeResult = Synthesize(synthesizeText, "audio/wav");

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
        //[TestMethod]
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
        private Voice GetVoice(string voice, string customizationId = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetVoice()");
            var result = _service.GetVoice(voice: voice, customizationId: customizationId, customData: customData);

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
        private Voices ListVoices(Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListVoices()");
            var result = _service.ListVoices(customData: customData);

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
        private System.IO.MemoryStream Synthesize(Text text, string accept = null, string voice = null, string customizationId = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Synthesize()");
            var result = _service.Synthesize(text: text, accept: accept, voice: voice, customizationId: customizationId, customData: customData);

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
        private Pronunciation GetPronunciation(string text, string voice = null, string format = null, string customizationId = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetPronunciation()");
            var result = _service.GetPronunciation(text: text, voice: voice, format: format, customizationId: customizationId, customData: customData);

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
        private VoiceModel CreateVoiceModel(CreateVoiceModel createVoiceModel, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateVoiceModel()");
            var result = _service.CreateVoiceModel(createVoiceModel: createVoiceModel, customData: customData);

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
        private BaseModel DeleteVoiceModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteVoiceModel()");
            var result = _service.DeleteVoiceModel(customizationId: customizationId, customData: customData);

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
        private VoiceModel GetVoiceModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetVoiceModel()");
            var result = _service.GetVoiceModel(customizationId: customizationId, customData: customData);

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
        private VoiceModels ListVoiceModels(string language = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListVoiceModels()");
            var result = _service.ListVoiceModels(language: language, customData: customData);

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
        private BaseModel UpdateVoiceModel(string customizationId, UpdateVoiceModel updateVoiceModel, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateVoiceModel()");
            var result = _service.UpdateVoiceModel(customizationId: customizationId, updateVoiceModel: updateVoiceModel, customData: customData);

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
        private BaseModel AddWord(string customizationId, string word, Translation translation, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddWord()");
            var result = _service.AddWord(customizationId: customizationId, word: word, translation: translation, customData: customData);

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
        private BaseModel AddWords(string customizationId, Words customWords, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddWords()");
            var result = _service.AddWords(customizationId: customizationId, customWords: customWords, customData: customData);

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
        private BaseModel DeleteWord(string customizationId, string word, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteWord()");
            var result = _service.DeleteWord(customizationId: customizationId, word: word, customData: customData);

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
        private Translation GetWord(string customizationId, string word, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetWord()");
            var result = _service.GetWord(customizationId: customizationId, word: word, customData: customData);

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
        private Words ListWords(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListWords()");
            var result = _service.ListWords(customizationId: customizationId, customData: customData);

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

        #region DeleteUserData
        private BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteUserData()");
            var result = _service.DeleteUserData(customerId: customerId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteUserData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteUserData()");
            }

            return result;
        }
        #endregion

        #endregion
    }
}