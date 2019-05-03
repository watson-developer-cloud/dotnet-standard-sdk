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

using IBM.Watson.TextToSpeech.v1.Model;
using IBM.Cloud.SDK.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using IBM.Cloud.SDK.Core;

namespace IBM.Watson.TextToSpeech.v1.IntegrationTests
{
    [TestClass]
    public class TextToSpeechServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private TextToSpeechService service;
        private static string credentials = string.Empty;
        private const string allisonVoice = "en-US_AllisonVoice";
        private string synthesizeText = "Hello, welcome to the Watson dotnet SDK!";
        private string voiceModelName = "dotnet-sdk-voice-model";
        private string voiceModelUpdatedName = "dotnet-sdk-voice-model-updated";
        private string voiceModelDescription = "Custom voice model for .NET SDK integration tests.";
        private string voiceModelUpdatedDescription = "Custom voice model for .NET SDK integration tests. Updated.";

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.Parent.FullName;
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
                endpoint = credential.Url;
                apikey = credential.IamApikey;
            }
            #endregion

            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = endpoint
            };

            service = new TextToSpeechService(tokenOptions);
            service.SetEndpoint(endpoint);
        }

        #region Voices
        [TestMethod]
        public void Voices_Success()
        {
            var listVoicesResult = service.ListVoices();

            var getVoiceResult = service.GetVoice(
                voice: allisonVoice
                );

            Assert.IsNotNull(getVoiceResult);
            Assert.IsTrue(!string.IsNullOrEmpty(getVoiceResult.Result.Name));
            Assert.IsNotNull(listVoicesResult);
            Assert.IsNotNull(listVoicesResult.Result._Voices);
        }
        #endregion

        #region Synthesize
        [TestMethod]
        public void Synthesize_Success()
        {
            var synthesizeResult = service.Synthesize(
                text: synthesizeText,
                accept: "audio/wav",
                voice: allisonVoice
                );

            //  Save file
            using (FileStream fs = File.Create("synthesize.wav"))
            {
                synthesizeResult.Result.WriteTo(fs);
                fs.Close();
                synthesizeResult.Result.Close();
            }

            Assert.IsNotNull(synthesizeResult.Result);
        }
        #endregion

        #region Pronunciation
        [TestMethod]
        public void Pronunciation_Success()
        {
            var getPronunciationResult = service.GetPronunciation(
                text: "IBM",
                voice: allisonVoice,
                format: "ipa"
                );

            Assert.IsNotNull(getPronunciationResult.Result);
            Assert.IsNotNull(getPronunciationResult.Result._Pronunciation);
        }
        #endregion

        #region Custom Voice Models
        [TestMethod]
        public void CustomVoiceModels_Success()
        {
            var listVoiceModelsResult = service.ListVoiceModels();

            var createVoiceModelResult = service.CreateVoiceModel(
                name: voiceModelName,
                language: "en-US",
                description: voiceModelDescription);
            var customizationId = createVoiceModelResult.Result.CustomizationId;

            var getVoiceModelResult = service.GetVoiceModel(
                customizationId: customizationId
                );

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

            var updateVoiceModelResult = service.UpdateVoiceModel(
                customizationId: customizationId,
                name: voiceModelUpdatedName,
                description: voiceModelUpdatedDescription,
                words: words
                );

            var getVoiceModelResult2 = service.GetVoiceModel(
                customizationId: customizationId
                );

            var deleteVoiceModelResult = service.DeleteVoiceModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(deleteVoiceModelResult.StatusCode == 204);
            Assert.IsNotNull(getVoiceModelResult2.Result);
            Assert.IsTrue(getVoiceModelResult2.Result.Name == voiceModelUpdatedName);
            Assert.IsTrue(getVoiceModelResult2.Result.Description == voiceModelUpdatedDescription);
            Assert.IsTrue(getVoiceModelResult2.Result.Words.Count == 3);
            Assert.IsNotNull(getVoiceModelResult.Result);
            Assert.IsTrue(getVoiceModelResult.Result.Name == voiceModelName);
            Assert.IsTrue(getVoiceModelResult.Result.Description == voiceModelDescription);
            Assert.IsNotNull(createVoiceModelResult.Result);
            Assert.IsNotNull(listVoiceModelsResult.Result);
            Assert.IsNotNull(listVoiceModelsResult.Result.Customizations);
        }
        #endregion

        #region Words
        [TestMethod]
        public void Words_Success()
        {
            var createVoiceModelResult = service.CreateVoiceModel(
                name: voiceModelName, 
                language: "en-US", 
                description: voiceModelDescription
                );
            var customizationId = createVoiceModelResult.Result.CustomizationId;

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

            var addWordsResult = service.AddWords(
                customizationId: customizationId, 
                words: words
                );

            var listWordsResult = service.ListWords(
                customizationId: customizationId
                );

            var getWordResult = service.GetWord(
                customizationId: customizationId, 
                word: "hello"
                );

            var addWordResult = service.AddWord(
                customizationId: customizationId, 
                word: "IBM", 
                translation: "eye bee m", 
                partOfSpeech: "noun"
                );

            var checkAddWordResult = service.ListWords(
                customizationId: customizationId
                );

            var deleteWordResult = service.DeleteWord(
                customizationId: customizationId, 
                word: "hi"
                );

            var checkDeleteWordResult = service.ListWords(
                customizationId: customizationId
                );

            var deleteVoiceModelResult = service.DeleteVoiceModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(checkDeleteWordResult.Result);
            Assert.IsNotNull(checkDeleteWordResult.Result._Words);
            Assert.IsTrue(checkDeleteWordResult.Result._Words.Count == 3);
            Assert.IsNotNull(checkAddWordResult.Result);
            Assert.IsNotNull(checkAddWordResult.Result._Words);
            Assert.IsTrue(checkAddWordResult.Result._Words.Count == 4);
            Assert.IsNotNull(getWordResult.Result);
            Assert.IsTrue(getWordResult.Result._Translation == "hullo");
            Assert.IsNotNull(listWordsResult.Result);
            Assert.IsNotNull(listWordsResult.Result._Words);
            Assert.IsTrue(listWordsResult.Result._Words.Count == 3);
            Assert.IsNotNull(addWordsResult.Result);
        }
        #endregion
    }
}