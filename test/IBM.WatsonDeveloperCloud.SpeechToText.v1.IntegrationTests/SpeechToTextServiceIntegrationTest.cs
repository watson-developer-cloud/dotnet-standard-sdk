/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.IntegrationTests
{
    [TestClass]
    public class SpeechToTextServiceIntegrationTest
    {
        private const string SESSION_STATUS_INITIALIZED = "initialized";
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private static string _createdCustomizationID;
        private AutoResetEvent autoEvent = new AutoResetEvent(false);
        private static string credentials = string.Empty;
        private string _enUsBroadbandModel = "en-US_BroadbandModel";
        private string _customModelName = "dotnet-integration-test-custom-model";
        private SpeechToTextService service;

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
                _endpoint = vcapServices["speech_to_text"]["url"].Value<string>();
                _username = vcapServices["speech_to_text"]["username"].Value<string>();
                _password = vcapServices["speech_to_text"]["password"].Value<string>();
            }

            service = new SpeechToTextService(_username, _password);
            service.Endpoint = _endpoint;
        }

        #region Models
        [TestMethod]
        public void TestModels_Success()
        {
            var listModelsResult = ListModels();

            var getModelResult = GetModel(_enUsBroadbandModel);

            Assert.IsNotNull(getModelResult);
            Assert.IsTrue(getModelResult.Name == _enUsBroadbandModel);
            Assert.IsNotNull(listModelsResult);
            Assert.IsNotNull(listModelsResult.Models);
        }
        #endregion

        #region Sessions
        [Ignore]
        [TestMethod]
        public void TestSessions_Success()
        {
            var createSessionResult = CreateSession();
            string sessionId = createSessionResult.SessionId;

            var deleteSessionResult = DeleteSession(sessionId);

            Assert.IsNotNull(deleteSessionResult);
            Assert.IsNotNull(createSessionResult);
            Assert.IsNotNull(createSessionResult.SessionId);
        }
        #endregion

        #region Custom Language Models
        [TestMethod]
        public void TestCustomLanguageModels_Success()
        {
            var listLanguageModelsResult = ListLanguageModels();

            CreateLanguageModel createLanguageModel = new Model.CreateLanguageModel
            {
                Name = _customModelName,
                BaseModelName = _enUsBroadbandModel
            };

            var createLanguageModelResult = CreateLanguageModel("application/json", createLanguageModel);
            string customizationId = createLanguageModelResult.CustomizationId;

            var getLanguageModelResult = GetLanguageModel(customizationId);

            var deleteLanguageModelResult = DeleteLanguageModel(customizationId);

            Assert.IsNotNull(deleteLanguageModelResult);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.CustomizationId == customizationId);
            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsTrue(createLanguageModelResult.Name == _customModelName);
            Assert.IsTrue(createLanguageModelResult.BaseModelName == _enUsBroadbandModel);
            Assert.IsNotNull(listLanguageModelsResult);
            Assert.IsNotNull(listLanguageModelsResult.Customizations);
        }
        #endregion

        #region GetModel
        private SpeechModel GetModel(string modelId)
        {
            Console.WriteLine("\nAttempting to GetModel()");
            var result = service.GetModel(modelId: modelId);

            if (result != null)
            {
                Console.WriteLine("GetModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetModel()");
            }

            return result;
        }
        #endregion

        #region ListModels
        private SpeechModels ListModels()
        {
            Console.WriteLine("\nAttempting to ListModels()");
            var result = service.ListModels();

            if (result != null)
            {
                Console.WriteLine("ListModels() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListModels()");
            }

            return result;
        }
        #endregion

        #region CreateSession
        private SpeechSession CreateSession(string model = null, string customizationId = null, string acousticCustomizationId = null, double? customizationWeight = null, string version = null)
        {
            Console.WriteLine("\nAttempting to CreateSession()");
            var result = service.CreateSession(model: model, customizationId: customizationId, acousticCustomizationId: acousticCustomizationId, customizationWeight: customizationWeight, version: version);

            if (result != null)
            {
                Console.WriteLine("CreateSession() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateSession()");
            }

            return result;
        }
        #endregion

        #region DeleteSession
        private object DeleteSession(string sessionId)
        {
            Console.WriteLine("\nAttempting to DeleteSession()");
            var result = service.DeleteSession(sessionId: sessionId);

            if (result != null)
            {
                Console.WriteLine("DeleteSession() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteSession()");
            }

            return result;
        }
        #endregion

        #region GetSessionStatus
        private SessionStatus GetSessionStatus(string sessionId)
        {
            Console.WriteLine("\nAttempting to GetSessionStatus()");
            var result = service.GetSessionStatus(sessionId: sessionId);

            if (result != null)
            {
                Console.WriteLine("GetSessionStatus() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetSessionStatus()");
            }

            return result;
        }
        #endregion

        #region CheckJob
        private RecognitionJob CheckJob(string id)
        {
            Console.WriteLine("\nAttempting to CheckJob()");
            var result = service.CheckJob(id: id);

            if (result != null)
            {
                Console.WriteLine("CheckJob() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CheckJob()");
            }

            return result;
        }
        #endregion

        #region CheckJobs
        private RecognitionJobs CheckJobs()
        {
            Console.WriteLine("\nAttempting to CheckJobs()");
            var result = service.CheckJobs();

            if (result != null)
            {
                Console.WriteLine("CheckJobs() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CheckJobs()");
            }

            return result;
        }
        #endregion

        #region CreateJob
        private RecognitionJob CreateJob(byte[] audio, string contentType, string model = null, string callbackUrl = null, string events = null, string userToken = null, long? resultsTtl = null, string transferEncoding = null, string customizationId = null, string acousticCustomizationId = null, double? customizationWeight = null, string version = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null)
        {
            Console.WriteLine("\nAttempting to CreateJob()");
            var result = service.CreateJob(audio: audio, contentType: contentType, model: model, callbackUrl: callbackUrl, events: events, userToken: userToken, resultsTtl: resultsTtl, transferEncoding: transferEncoding, customizationId: customizationId, acousticCustomizationId: acousticCustomizationId, customizationWeight: customizationWeight, version: version, inactivityTimeout: inactivityTimeout, keywords: keywords, keywordsThreshold: keywordsThreshold, maxAlternatives: maxAlternatives, wordAlternativesThreshold: wordAlternativesThreshold, wordConfidence: wordConfidence, timestamps: timestamps, profanityFilter: profanityFilter, smartFormatting: smartFormatting, speakerLabels: speakerLabels);

            if (result != null)
            {
                Console.WriteLine("CreateJob() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateJob()");
            }

            return result;
        }
        #endregion

        #region DeleteJob
        private object DeleteJob(string id)
        {
            Console.WriteLine("\nAttempting to DeleteJob()");
            var result = service.DeleteJob(id: id);

            if (result != null)
            {
                Console.WriteLine("DeleteJob() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteJob()");
            }

            return result;
        }
        #endregion

        #region RegisterCallback
        private RegisterStatus RegisterCallback(string callbackUrl, string userSecret = null)
        {
            Console.WriteLine("\nAttempting to RegisterCallback()");
            var result = service.RegisterCallback(callbackUrl: callbackUrl, userSecret: userSecret);

            if (result != null)
            {
                Console.WriteLine("RegisterCallback() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to RegisterCallback()");
            }

            return result;
        }
        #endregion

        #region UnregisterCallback
        private object UnregisterCallback(string callbackUrl)
        {
            Console.WriteLine("\nAttempting to UnregisterCallback()");
            var result = service.UnregisterCallback(callbackUrl: callbackUrl);

            if (result != null)
            {
                Console.WriteLine("UnregisterCallback() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UnregisterCallback()");
            }

            return result;
        }
        #endregion

        #region CreateLanguageModel
        private LanguageModel CreateLanguageModel(string contentType, CreateLanguageModel createLanguageModel)
        {
            Console.WriteLine("\nAttempting to CreateLanguageModel()");
            var result = service.CreateLanguageModel(contentType: contentType, createLanguageModel: createLanguageModel);

            if (result != null)
            {
                Console.WriteLine("CreateLanguageModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateLanguageModel()");
            }

            return result;
        }
        #endregion

        #region DeleteLanguageModel
        private object DeleteLanguageModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to DeleteLanguageModel()");
            var result = service.DeleteLanguageModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("DeleteLanguageModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteLanguageModel()");
            }

            return result;
        }
        #endregion

        #region GetLanguageModel
        private LanguageModel GetLanguageModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to GetLanguageModel()");
            var result = service.GetLanguageModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("GetLanguageModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetLanguageModel()");
            }

            return result;
        }
        #endregion

        #region ListLanguageModels
        private LanguageModels ListLanguageModels(string language = null)
        {
            Console.WriteLine("\nAttempting to ListLanguageModels()");
            var result = service.ListLanguageModels(language: language);

            if (result != null)
            {
                Console.WriteLine("ListLanguageModels() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListLanguageModels()");
            }

            return result;
        }
        #endregion

        #region ResetLanguageModel
        private object ResetLanguageModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to ResetLanguageModel()");
            var result = service.ResetLanguageModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("ResetLanguageModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ResetLanguageModel()");
            }

            return result;
        }
        #endregion

        #region TrainLanguageModel
        private object TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null)
        {
            Console.WriteLine("\nAttempting to TrainLanguageModel()");
            var result = service.TrainLanguageModel(customizationId: customizationId, wordTypeToAdd: wordTypeToAdd, customizationWeight: customizationWeight);

            if (result != null)
            {
                Console.WriteLine("TrainLanguageModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to TrainLanguageModel()");
            }

            return result;
        }
        #endregion

        #region UpgradeLanguageModel
        private object UpgradeLanguageModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to UpgradeLanguageModel()");
            var result = service.UpgradeLanguageModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("UpgradeLanguageModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpgradeLanguageModel()");
            }

            return result;
        }
        #endregion

        #region AddCorpus
        private object AddCorpus(string customizationId, string corpusName, System.IO.Stream corpusFile, bool? allowOverwrite = null, string corpusFileContentType = null)
        {
            Console.WriteLine("\nAttempting to AddCorpus()");
            var result = service.AddCorpus(customizationId: customizationId, corpusName: corpusName, corpusFile: corpusFile, allowOverwrite: allowOverwrite, corpusFileContentType: corpusFileContentType);

            if (result != null)
            {
                Console.WriteLine("AddCorpus() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to AddCorpus()");
            }

            return result;
        }
        #endregion

        #region DeleteCorpus
        private object DeleteCorpus(string customizationId, string corpusName)
        {
            Console.WriteLine("\nAttempting to DeleteCorpus()");
            var result = service.DeleteCorpus(customizationId: customizationId, corpusName: corpusName);

            if (result != null)
            {
                Console.WriteLine("DeleteCorpus() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteCorpus()");
            }

            return result;
        }
        #endregion

        #region GetCorpus
        private Corpus GetCorpus(string customizationId, string corpusName)
        {
            Console.WriteLine("\nAttempting to GetCorpus()");
            var result = service.GetCorpus(customizationId: customizationId, corpusName: corpusName);

            if (result != null)
            {
                Console.WriteLine("GetCorpus() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetCorpus()");
            }

            return result;
        }
        #endregion

        #region ListCorpora
        private Corpora ListCorpora(string customizationId)
        {
            Console.WriteLine("\nAttempting to ListCorpora()");
            var result = service.ListCorpora(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("ListCorpora() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListCorpora()");
            }

            return result;
        }
        #endregion

        #region AddWord
        private object AddWord(string customizationId, string wordName, string contentType, CustomWord customWord)
        {
            Console.WriteLine("\nAttempting to AddWord()");
            var result = service.AddWord(customizationId: customizationId, wordName: wordName, contentType: contentType, customWord: customWord);

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
        private object AddWords(string customizationId, string contentType, CustomWords customWords)
        {
            Console.WriteLine("\nAttempting to AddWords()");
            var result = service.AddWords(customizationId: customizationId, contentType: contentType, customWords: customWords);

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
        private object DeleteWord(string customizationId, string wordName)
        {
            Console.WriteLine("\nAttempting to DeleteWord()");
            var result = service.DeleteWord(customizationId: customizationId, wordName: wordName);

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
        private Word GetWord(string customizationId, string wordName)
        {
            Console.WriteLine("\nAttempting to GetWord()");
            var result = service.GetWord(customizationId: customizationId, wordName: wordName);

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
        private Words ListWords(string customizationId, string wordType = null, string sort = null)
        {
            Console.WriteLine("\nAttempting to ListWords()");
            var result = service.ListWords(customizationId: customizationId, wordType: wordType, sort: sort);

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

        #region CreateAcousticModel
        private AcousticModel CreateAcousticModel(string contentType, CreateAcousticModel createAcousticModel)
        {
            Console.WriteLine("\nAttempting to CreateAcousticModel()");
            var result = service.CreateAcousticModel(contentType: contentType, createAcousticModel: createAcousticModel);

            if (result != null)
            {
                Console.WriteLine("CreateAcousticModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateAcousticModel()");
            }

            return result;
        }
        #endregion

        #region DeleteAcousticModel
        private object DeleteAcousticModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to DeleteAcousticModel()");
            var result = service.DeleteAcousticModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("DeleteAcousticModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteAcousticModel()");
            }

            return result;
        }
        #endregion

        #region GetAcousticModel
        private AcousticModel GetAcousticModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to GetAcousticModel()");
            var result = service.GetAcousticModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("GetAcousticModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetAcousticModel()");
            }

            return result;
        }
        #endregion

        #region ListAcousticModels
        private AcousticModels ListAcousticModels(string language = null)
        {
            Console.WriteLine("\nAttempting to ListAcousticModels()");
            var result = service.ListAcousticModels(language: language);

            if (result != null)
            {
                Console.WriteLine("ListAcousticModels() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListAcousticModels()");
            }

            return result;
        }
        #endregion

        #region ResetAcousticModel
        private object ResetAcousticModel(string customizationId)
        {
            Console.WriteLine("\nAttempting to ResetAcousticModel()");
            var result = service.ResetAcousticModel(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("ResetAcousticModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ResetAcousticModel()");
            }

            return result;
        }
        #endregion

        #region TrainAcousticModel
        private object TrainAcousticModel(string customizationId, string customLanguageModelId = null)
        {
            Console.WriteLine("\nAttempting to TrainAcousticModel()");
            var result = service.TrainAcousticModel(customizationId: customizationId, customLanguageModelId: customLanguageModelId);

            if (result != null)
            {
                Console.WriteLine("TrainAcousticModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to TrainAcousticModel()");
            }

            return result;
        }
        #endregion

        #region UpgradeAcousticModel
        private object UpgradeAcousticModel(string customizationId, string customLanguageModelId = null)
        {
            Console.WriteLine("\nAttempting to UpgradeAcousticModel()");
            var result = service.UpgradeAcousticModel(customizationId: customizationId, customLanguageModelId: customLanguageModelId);

            if (result != null)
            {
                Console.WriteLine("UpgradeAcousticModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpgradeAcousticModel()");
            }

            return result;
        }
        #endregion

        #region AddAudio
        private object AddAudio(string customizationId, string audioName, List<byte[]> audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null)
        {
            Console.WriteLine("\nAttempting to AddAudio()");
            var result = service.AddAudio(customizationId: customizationId, audioName: audioName, audioResource: audioResource, contentType: contentType, containedContentType: containedContentType, allowOverwrite: allowOverwrite);

            if (result != null)
            {
                Console.WriteLine("AddAudio() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to AddAudio()");
            }

            return result;
        }
        #endregion

        #region DeleteAudio
        private object DeleteAudio(string customizationId, string audioName)
        {
            Console.WriteLine("\nAttempting to DeleteAudio()");
            var result = service.DeleteAudio(customizationId: customizationId, audioName: audioName);

            if (result != null)
            {
                Console.WriteLine("DeleteAudio() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteAudio()");
            }

            return result;
        }
        #endregion

        #region GetAudio
        private AudioListing GetAudio(string customizationId, string audioName)
        {
            Console.WriteLine("\nAttempting to GetAudio()");
            var result = service.GetAudio(customizationId: customizationId, audioName: audioName);

            if (result != null)
            {
                Console.WriteLine("GetAudio() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetAudio()");
            }

            return result;
        }
        #endregion

        #region ListAudio
        private AudioResources ListAudio(string customizationId)
        {
            Console.WriteLine("\nAttempting to ListAudio()");
            var result = service.ListAudio(customizationId: customizationId);

            if (result != null)
            {
                Console.WriteLine("ListAudio() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListAudio()");
            }

            return result;
        }
        #endregion

        //[TestMethod]
        //public void t00_GetModels_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    var results = _speechToText.GetModels();

        //    Assert.IsNotNull(results);
        //    Assert.IsNotNull(results.Models);
        //    Assert.IsTrue(results.Models.Count > 0);
        //    Assert.IsNotNull(results.Models.First().Name);
        //    Assert.IsNotNull(results.Models.First().SupportedFeatures);
        //}

        //[TestMethod]
        //public void t01_GetModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    string modelName = "en-US_NarrowbandModel";

        //    var results = _speechToText.GetModel(modelName);

        //    Assert.IsNotNull(results);
        //    Assert.IsNotNull(results.Name);
        //    Assert.IsNotNull(results.SupportedFeatures);
        //}

        //[TestMethod]
        //public void t02_CreateSession_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    string modelName = "en-US_NarrowbandModel";

        //    var model = _speechToText.GetModel(modelName);

        //    var result =
        //        _speechToText.CreateSession(model.Name);

        //    Assert.IsNotNull(result);
        //    Assert.IsNotNull(result.SessionId);
        //}

        //[TestMethod]
        //public void t03_GetSessionStatus_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    string modelName = "en-US_NarrowbandModel";

        //    var model = _speechToText.GetModel(modelName);

        //    var session =
        //        _speechToText.CreateSession(model.Name);

        //    var result =
        //        _speechToText.GetSessionStatus(session);

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(SESSION_STATUS_INITIALIZED, result.Session.State);
        //}

        //[TestMethod]
        //public void t04_Recognize_BodyContent_Sucess()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    FileStream audio =
        //        File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

        //    var results =
        //        _speechToText.Recognize(audio.GetMediaTypeFromFile(), audio, "");

        //    Assert.IsNotNull(results);
        //    Assert.IsNotNull(results.Results);
        //    Assert.IsTrue(results.Results.Count > 0);
        //    Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
        //    Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        //}

        //[TestMethod]
        //public void t05_Recognize_FormData_Sucess()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    FileStream audio =
        //        File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

        //    var results =
        //        _speechToText.Recognize(audio.GetMediaTypeFromFile(),
        //                          new Metadata()
        //                          {
        //                              PartContentType = audio.GetMediaTypeFromFile()
        //                          },
        //                          audio);

        //    Assert.IsNotNull(results);
        //    Assert.IsNotNull(results.Results);
        //    Assert.IsTrue(results.Results.Count > 0);
        //    Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
        //    Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        //}

        //[TestMethod]
        //public void t06_Recognize_WithSession_BodyContent_Sucess()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    string modelName = "en-US_BroadbandModel";

        //    var session =
        //        _speechToText.CreateSession(modelName);

        //    FileStream audio =
        //        File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

        //    var results =
        //        _speechToText.RecognizeWithSession(session.SessionId, audio.GetMediaTypeFromFile(), audio);

        //    Assert.IsNotNull(results);
        //    Assert.IsNotNull(results.Results);
        //    Assert.IsTrue(results.Results.Count > 0);
        //    Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
        //    Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        //}

        //[TestMethod]
        //public void t07_Recognize_WithSession_FormData_Sucess()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    string modelName = "en-US_BroadbandModel";

        //    var session =
        //        _speechToText.CreateSession(modelName);

        //    FileStream audio =
        //        File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

        //    var results =
        //        _speechToText.RecognizeWithSession(session.SessionId,
        //                                     audio.GetMediaTypeFromFile(),
        //                                     new Metadata()
        //                                     {
        //                                         PartContentType = audio.GetMediaTypeFromFile()
        //                                     },
        //                                     audio);

        //    Assert.IsNotNull(results);
        //    Assert.IsNotNull(results.Results);
        //    Assert.IsTrue(results.Results.Count > 0);
        //    Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
        //    Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        //}

        //[TestMethod]
        //public void t08_ObserveResult_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    string modelName = "en-US_BroadbandModel";

        //    var session =
        //        _speechToText.CreateSession(modelName);

        //    FileStream audio =
        //        File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

        //    Metadata metadata = new Metadata()
        //    {
        //        PartContentType = audio.GetMediaTypeFromFile()
        //    };

        //    var taskObserveResult = Task.Factory.StartNew<List<SpeechRecognitionEvent>>(() =>
        //    {
        //        return _speechToText.ObserveResult(session.SessionId, interimResults: true);
        //    });

        //    taskObserveResult.ContinueWith((antecedent) =>
        //    {
        //        var results = antecedent.Result;

        //        Assert.IsNotNull(results);
        //        Assert.IsTrue(results.Count > 0);
        //        Assert.IsNotNull(results.First().Results);
        //        Assert.IsTrue(results.First().Results.Count > 0);
        //        Assert.IsTrue(results.First().Results.First().Alternatives.Count > 0);
        //        Assert.IsNotNull(results.First().Results.First().Alternatives.First().Transcript);
        //    });

        //    var taskRecognizeWithSession = Task.Factory.StartNew(() =>
        //    {
        //        _speechToText.RecognizeWithSession(session.SessionId, audio.GetMediaTypeFromFile(), metadata, audio, "chunked", modelName);
        //    });

        //    taskObserveResult.Wait();
        //}

        //[TestMethod]
        //public void t09_CreateCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    string modelName = "en-US_BroadbandModel";

        //    var customizationId =
        //        _speechToText.CreateCustomModel("model_test", modelName, "Test of Create Custom Model method");

        //    _createdCustomizationID = customizationId.CustomizationId;

        //    Assert.IsNotNull(customizationId);
        //    Assert.IsNotNull(customizationId.CustomizationId);
        //}

        //[TestMethod]
        //public void t10_AddCorpus_Success()
        //{
        //    _speechToText =
        //       new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var body =
        //        File.OpenRead(@"SpeechToTextTestData\test-stt-corpus.txt");

        //    object result = _speechToText.AddCorpus(customization,
        //                      "stt_integration",
        //                      false,
        //                      body);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t11_TrainCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var result = _speechToText.TrainCustomModel(customization);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t12_WaitForTraining()
        //{
        //    IsTrainingComplete();
        //    autoEvent.WaitOne();
        //    Assert.IsTrue(true);
        //}

        //[TestMethod]
        //public void t13_AddCustomWords_Success()
        //{
        //    _speechToText =
        //      new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    object result = _speechToText.AddCustomWords(_createdCustomizationID,
        //                          new Words()
        //                          {
        //                              WordsProperty = new List<Word>()
        //                              {
        //                                  new Word()
        //                                  {
        //                                     DisplayAs = "Watson",
        //                                     SoundsLike = new List<string>()
        //                                     {
        //                                         "wat son"
        //                                     },
        //                                     WordProperty = "watson"
        //                                  },
        //                                  new Word()
        //                                  {
        //                                     DisplayAs = "C#",
        //                                     SoundsLike = new List<string>()
        //                                     {
        //                                         "si sharp"
        //                                     },
        //                                     WordProperty = "csharp"
        //                                  },
        //                                   new Word()
        //                                  {
        //                                     DisplayAs = "SDK",
        //                                     SoundsLike = new List<string>()
        //                                     {
        //                                         "S.D.K."
        //                                     },
        //                                     WordProperty = "sdk"
        //                                  }
        //                              }
        //                          });

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t14_TrainCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var result = _speechToText.TrainCustomModel(customization);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t15_WaitForTraining()
        //{
        //    IsTrainingComplete();
        //    autoEvent.WaitOne();
        //    Assert.IsTrue(true);
        //}

        //[TestMethod]
        //public void t16_AddCustomWord_Success()
        //{
        //    _speechToText =
        //      new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    object result = _speechToText.AddCustomWord(customization,
        //                          "social",
        //                          new WordDefinition()
        //                          {
        //                              DisplayAs = "Social",
        //                              SoundsLike = new List<string>()
        //                                     {
        //                                         "so cial"
        //                                     }
        //                          });

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t17_TrainCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var result = _speechToText.TrainCustomModel(customization);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t18_WaitForTraining()
        //{
        //    IsTrainingComplete();
        //    autoEvent.WaitOne();
        //    Assert.IsTrue(true);
        //}

        //[TestMethod]
        //public void t19_ListCustomModels_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customizations =
        //        _speechToText.ListCustomModels();

        //    Assert.IsNotNull(customizations);
        //    Assert.IsNotNull(customizations.Customization);
        //    Assert.IsTrue(customizations.Customization.Count() > 0);
        //}

        //[TestMethod]
        //public void t20_ListCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var result =
        //        _speechToText.ListCustomModel(_createdCustomizationID);

        //    Assert.IsNotNull(result);
        //    Assert.IsFalse(string.IsNullOrEmpty(result.CustomizationId));
        //}

        //[TestMethod]
        //public void t21_ListCorpora_Success()
        //{
        //    _speechToText =
        //       new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var corpora =
        //        _speechToText.ListCorpora(customization);

        //    Assert.IsNotNull(corpora);
        //    Assert.IsNotNull(corpora.CorporaProperty);
        //    Assert.IsTrue(corpora.CorporaProperty.Count() > 0);
        //}

        //[TestMethod]
        //public void t22_GetCorpus_Success()
        //{
        //    _speechToText =
        //      new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var corpora =
        //        _speechToText.ListCorpora(customization);

        //    var corpus =
        //        _speechToText.GetCorpus(customization, corpora.CorporaProperty.First().Name);

        //    Assert.IsNotNull(corpus);
        //    Assert.IsNotNull(corpus.Name);
        //    Assert.AreEqual(corpora.CorporaProperty.First().Name, corpus.Name);
        //}

        //[TestMethod]
        //public void t23_ListCustomWords_Success()
        //{
        //    _speechToText =
        //      new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var words =
        //        _speechToText.ListCustomWords(_createdCustomizationID, null, null);

        //    Assert.IsNotNull(words);
        //}

        //[TestMethod]
        //public void t24_ListCustomWord_Success()
        //{
        //    _speechToText =
        //      new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var words =
        //        _speechToText.ListCustomWords(customization, WordType.All, null);

        //    var word =
        //        _speechToText.ListCustomWord(customization, words.Words.First().Word);

        //    Assert.IsNotNull(word);
        //}
        //[TestMethod]
        //public void t25_DeleteCustomWord_Success()
        //{
        //    _speechToText =
        //      new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var words =
        //        _speechToText.ListCustomWords(customization, WordType.All, null);

        //    object result = _speechToText.DeleteCustomWord(customization, words.Words.First().Word);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t26_TrainCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var result = _speechToText.TrainCustomModel(_createdCustomizationID);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t27_WaitForTraining()
        //{
        //    IsTrainingComplete();
        //    autoEvent.WaitOne();
        //    Assert.IsTrue(true);
        //}


        //[TestMethod]
        //public void t28_DeleteCorpus_Success()
        //{
        //    _speechToText =
        //      new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var corpora =
        //        _speechToText.ListCorpora(customization);

        //    var result = _speechToText.DeleteCorpus(customization, corpora.CorporaProperty.First().Name);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t29_ResetCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var result = _speechToText.ResetCustomModel(customization);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t30_UpgradeCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    var result = _speechToText.UpgradeCustomModel(customization);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void t31_DeleteCustomModel_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    _speechToText.Endpoint = _endpoint;

        //    var customization =
        //        _createdCustomizationID;

        //    object result = _speechToText.DeleteCustomModel(customization);

        //    Assert.IsNotNull(result);
        //}

        ////[TestMethod]
        //public void t32_DeleteSession_Success()
        //{
        //    _speechToText =
        //        new SpeechToTextService(_username, _password);

        //    string modelName = "en-US_NarrowbandModel";

        //    var model = _speechToText.GetModel(modelName);

        //    var session =
        //        _speechToText.CreateSession(model.Name);

        //    object result = _speechToText.DeleteSession(session);

        //    Assert.IsNotNull(result);
        //}

        //private bool IsTrainingComplete()
        //{
        //    _speechToText = new SpeechToTextService(_username, _password);
        //    _speechToText.Endpoint = _endpoint;

        //    var result = _speechToText.ListCustomModel(_createdCustomizationID);

        //    string status = result.Status.ToLower();
        //    Console.WriteLine(string.Format("Classifier status is {0}", status));

        //    if (status == "ready" || status == "available")
        //        autoEvent.Set();
        //    else
        //    {
        //        Task.Factory.StartNew(() =>
        //        {
        //            System.Threading.Thread.Sleep(5000);
        //            IsTrainingComplete();
        //        });
        //    }

        //    return result.Status.ToLower() == "ready";
        //}
    }
}