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

using IBM.Cloud.SDK.Core;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.SpeechToText.v1;
using IBM.Watson.SpeechToText.v1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IBM.Watson.SpeechToText.v1.IntegrationTests
{
    [TestClass]
    public class SpeechToTextServiceIntegrationTest
    {
        //private const string SESSION_STATUS_INITIALIZED = "initialized";
        private static string apikey;
        private static string endpoint;
        private AutoResetEvent autoEvent = new AutoResetEvent(false);
        private static string credentials = string.Empty;
        private const string EN_US = "en-US_BroadbandModel";
        private string customModelName = "dotnet-integration-test-custom-model";
        private string customModelDescription = "A custom model to test .NET SDK Speech to Text customization.";
        private string corpusName = "The Jabberwocky";
        private string corpusPath = @"SpeechToTextTestData/theJabberwocky-utf8.txt";
        private string acousticModelName = "dotnet-integration-test-custom-acoustic-model";
        private string acousticModelDescription = "A custom model to teset .NET SDK Speech to Text acoustic customization.";
        private string acousticResourceUrl = "https://archive.org/download/Greatest_Speeches_of_the_20th_Century/KeynoteAddressforDemocraticConvention_64kb.mp3";
        private string acousticResourceName = "firstOrbit";
        private string acousticResourceMimeType = "audio/mpeg";
        private string testAudioPath = @"SpeechToTextTestData/test-audio.wav";
        private string grammarName = "dotnet-sdk-grammars";
        private string grammarPath = @"SpeechToTextTestData/confirm.abnf";
        private string grammarsContentType = "application/srgs";
        private SpeechToTextService service;

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

                Credential credential = vcapCredentials.GetCredentialByname("speech-to-text-sdk")[0].Credentials;
                endpoint = credential.Url;
                apikey = credential.IamApikey;
            }
            #endregion

            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = endpoint
            };

            service = new SpeechToTextService(tokenOptions);
            service.SetEndpoint(endpoint);
        }

        #region Models
        [TestMethod]
        public void TestModels_Success()
        {
            var listModelsResult = ListModels();

            var getModelResult = GetModel(EN_US);

            Assert.IsNotNull(getModelResult);
            Assert.IsTrue(getModelResult.Name == EN_US);
            Assert.IsNotNull(listModelsResult);
            Assert.IsNotNull(listModelsResult.Models);
        }
        #endregion

        #region Custom Language Models
        [TestMethod]
        public void TestCustomLanguageModels_Success()
        {
            var listLanguageModelsResult = ListLanguageModels();

            CreateLanguageModel createLanguageModel = new Model.CreateLanguageModel
            {
                Name = customModelName,
                BaseModelName = Model.CreateLanguageModel.BaseModelNameEnum.EN_US_BROADBANDMODEL,
                Description = customModelDescription
            };

            var createLanguageModelResult = CreateLanguageModel(createLanguageModel);
            string customizationId = createLanguageModelResult.CustomizationId;

            var getLanguageModelResult = GetLanguageModel(customizationId);

            var listCorporaResults = ListCorpora(customizationId);

            object addCorpusResults = null;
            using (FileStream corpusStream = File.OpenRead(corpusPath))
            {
                addCorpusResults = AddCorpus(customizationId, corpusName, corpusStream);
            }

            var getCorpusResults = GetCorpus(customizationId, corpusName);

            CheckCorpusStatus(customizationId, corpusName);
            autoEvent.WaitOne();

            var trainLanguageModelResult = TrainLanguageModel(customizationId);
            CheckCustomizationStatus(customizationId);
            autoEvent.WaitOne();
            Assert.IsNotNull(trainLanguageModelResult);
            trainLanguageModelResult = null;

            var listCustomWordsResult = ListWords(customizationId);

            var customWords = new CustomWords()
            {
                Words = new List<CustomWord>()
                            {
                                new CustomWord()
                                {
                                    DisplayAs = "Watson",
                                    SoundsLike = new List<string>()
                                    {
                                        "wat son"
                                    },
                                    Word = "watson"
                                },
                                new CustomWord()
                                {
                                    DisplayAs = "C#",
                                    SoundsLike = new List<string>()
                                    {
                                        "si sharp"
                                    },
                                    Word = "csharp"
                                },
                                new CustomWord()
                                {
                                    DisplayAs = "SDK",
                                    SoundsLike = new List<string>()
                                    {
                                        "S.D.K."
                                    },
                                    Word = "sdk"
                                }
                            }
            };

            var addCustomWordsResult = AddWords(customizationId, customWords);

            CheckCustomizationStatus(customizationId);
            autoEvent.WaitOne();

            trainLanguageModelResult = TrainLanguageModel(customizationId);
            Assert.IsNotNull(trainLanguageModelResult);
            trainLanguageModelResult = null;

            CheckCustomizationStatus(customizationId);
            autoEvent.WaitOne();

            var customWord = new CustomWord()
            {
                DisplayAs = ".NET",
                SoundsLike = new List<string>()
                {
                    "dotnet"
                },
                Word = "dotnet"
            };

            var addCustomWordResult = AddWord(customizationId, "dotnet", customWord);

            var getCustomWordResult = GetWord(customizationId, "dotnet");

            trainLanguageModelResult = TrainLanguageModel(customizationId);
            CheckCustomizationStatus(customizationId);
            autoEvent.WaitOne();
            Assert.IsNotNull(trainLanguageModelResult);
            trainLanguageModelResult = null;

            CheckCorpusStatus(customizationId, corpusName);
            autoEvent.WaitOne();

            //var upgradeLanguageModelResult = UpgradeLanguageModel(customizationId);
            //  Assert.IsNotNull(upgradeLanguageModelResult);

            var listGrammarsResult = service.ListGrammars(customizationId);
            var addGrammarResult = service.AddGrammar(customizationId, grammarName, File.ReadAllText(grammarPath), grammarsContentType);
            var getGrammarResult = service.GetGrammar(customizationId, grammarName);

            CheckCustomizationStatus(customizationId);
            autoEvent.WaitOne();

            var deleteGrammarResult = service.DeleteGrammar(customizationId, grammarName);

            var deleteCustomWordResults = DeleteWord(customizationId, "csharp");

            var deleteCorpusResults = DeleteCorpus(customizationId, corpusName);

            var resetLanguageModelResult = ResetLanguageModel(customizationId);
            Assert.IsNotNull(resetLanguageModelResult);

            var deleteLanguageModelResults = DeleteLanguageModel(customizationId);

            Assert.IsNotNull(deleteGrammarResult);
            Assert.IsNotNull(getGrammarResult);
            Assert.IsTrue(getGrammarResult.Name == grammarName);
            Assert.IsNotNull(addGrammarResult);
            Assert.IsNotNull(listGrammarsResult);
            Assert.IsNotNull(deleteCustomWordResults);
            Assert.IsNotNull(deleteCorpusResults);
            Assert.IsNotNull(deleteLanguageModelResults);
            Assert.IsNotNull(getCustomWordResult);
            Assert.IsTrue(getCustomWordResult._Word == "dotnet");
            Assert.IsNotNull(addCustomWordResult);
            Assert.IsNotNull(addCustomWordsResult);
            Assert.IsNotNull(listCustomWordsResult);
            Assert.IsNotNull(listCustomWordsResult._Words);
            Assert.IsNotNull(getCorpusResults);
            Assert.IsTrue(getCorpusResults.Name == corpusName);
            Assert.IsNotNull(addCorpusResults);
            Assert.IsNotNull(listCorporaResults);
            Assert.IsNotNull(listCorporaResults._Corpora);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.CustomizationId == customizationId);
            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsNotNull(listLanguageModelsResult);
            Assert.IsNotNull(listLanguageModelsResult.Customizations);
        }
        #endregion

        #region German Model
        [TestMethod]
        public void TestGermanLanguageModel_Success()
        {
            CreateLanguageModel createLanguageModel = new Model.CreateLanguageModel
            {
                Name = customModelName,
                BaseModelName = Model.CreateLanguageModel.BaseModelNameEnum.DE_DE_BROADBANDMODEL,
                Description = customModelDescription
            };

            var createLanguageModelResult = CreateLanguageModel(createLanguageModel);
            string customizationId = createLanguageModelResult.CustomizationId;

            var getLanguageModelResult = GetLanguageModel(customizationId);

            var deleteLanguageModelResult = DeleteLanguageModel(customizationId);

            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsNotNull(deleteLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.BaseModelName == "de-DE_BroadbandModel");
        }
        #endregion

        #region Brazilian Broadband Model
        [TestMethod]
        public void TestBrazilianBroadbandLanguageModel_Success()
        {
            CreateLanguageModel createLanguageModel = new Model.CreateLanguageModel
            {
                Name = customModelName,
                BaseModelName = Model.CreateLanguageModel.BaseModelNameEnum.PT_BR_BROADBANDMODEL,
                Description = customModelDescription
            };

            var createLanguageModelResult = CreateLanguageModel(createLanguageModel);
            string customizationId = createLanguageModelResult.CustomizationId;

            var getLanguageModelResult = GetLanguageModel(customizationId);

            var deleteLanguageModelResult = DeleteLanguageModel(customizationId);

            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsNotNull(deleteLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.BaseModelName == "pt-BR_BroadbandModel");
        }
        #endregion

        #region Brazilian Narrowband Model
        [TestMethod]
        public void TestBrazilianNarrowbandLanguageModel_Success()
        {
            CreateLanguageModel createLanguageModel = new Model.CreateLanguageModel
            {
                Name = customModelName,
                BaseModelName = Model.CreateLanguageModel.BaseModelNameEnum.PT_BR_NARROWBANDMODEL,
                Description = customModelDescription
            };

            var createLanguageModelResult = CreateLanguageModel(createLanguageModel);
            string customizationId = createLanguageModelResult.CustomizationId;

            var getLanguageModelResult = GetLanguageModel(customizationId);

            var deleteLanguageModelResult = DeleteLanguageModel(customizationId);

            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsNotNull(deleteLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.BaseModelName == "pt-BR_NarrowbandModel");
        }
        #endregion

        #region German Acoustic Customization
        [TestMethod]
        public void TestGermanAcousticCustomization()
        {
            byte[] acousticResourceData = null;

            try
            {
                acousticResourceData = DownloadAcousticResource(acousticResourceUrl).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Failed to get credentials: {0}", e.Message));
            }

            Task.WaitAll();

            var listAcousticModelsResult = ListAcousticModels();

            var acousticModel = new CreateAcousticModel
            {
                Name = acousticModelName,
                BaseModelName = Model.CreateAcousticModel.BaseModelNameEnum.DE_DE_BROADBANDMODEL,
                Description = acousticModelDescription
            };

            var createAcousticModelResult = CreateAcousticModel(acousticModel);
            var acousticCustomizationId = createAcousticModelResult.CustomizationId;
            var getAcousticModelResult = GetAcousticModel(acousticCustomizationId);
            var deleteAcousticModelResult = DeleteAcousticModel(acousticCustomizationId);

            Assert.IsNotNull(createAcousticModelResult);
            Assert.IsNotNull(getAcousticModelResult);
            Assert.IsNotNull(deleteAcousticModelResult);
            Assert.IsTrue(getAcousticModelResult.BaseModelName == "de-DE_BroadbandModel");
        }
        #endregion

        #region Acoustic Customizations
        [TestMethod]
        public void TestAcousticCustomizations()
        {
            byte[] acousticResourceData = null;

            try
            {
                acousticResourceData = DownloadAcousticResource(acousticResourceUrl).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Failed to get credentials: {0}", e.Message));
            }

            Task.WaitAll();

            var listAcousticModelsResult = ListAcousticModels();

            var acousticModel = new CreateAcousticModel
            {
                Name = acousticModelName,
                BaseModelName = Model.CreateAcousticModel.BaseModelNameEnum.EN_US_BROADBANDMODEL,
                Description = acousticModelDescription
            };

            var createAcousticModelResult = CreateAcousticModel(acousticModel);
            var acousticCustomizationId = createAcousticModelResult.CustomizationId;

            var getAcousticModelResult = GetAcousticModel(acousticCustomizationId);


            var listAudioResult = ListAudio(acousticCustomizationId);

            object addAudioResult = null;

            addAudioResult = AddAudio(acousticCustomizationId, acousticResourceName, acousticResourceData, acousticResourceMimeType, allowOverwrite: true);

            var getAudioResult = GetAudio(acousticCustomizationId, acousticResourceName);

            CheckAudioStatus(acousticCustomizationId, acousticResourceName);
            autoEvent.WaitOne();

            CheckAcousticCustomizationStatus(acousticCustomizationId);
            autoEvent.WaitOne();

            var trainAcousticModelResult = TrainAcousticModel(acousticCustomizationId);

            CheckAcousticCustomizationStatus(acousticCustomizationId);
            autoEvent.WaitOne();

            //var upgradeAcousticModel = UpgradeAcousticModel(acousticCustomizationId);

            //CheckAcousticCustomizationStatus(acousticCustomizationId);
            //autoEvent.WaitOne();

            var deleteAudioResult = DeleteAudio(acousticCustomizationId, acousticResourceName);

            var resetAcousticModelResult = ResetAcousticModel(acousticCustomizationId);

            var deleteAcousticModelResult = DeleteAcousticModel(acousticCustomizationId);

            Assert.IsNotNull(deleteAudioResult);
            Assert.IsNotNull(deleteAcousticModelResult);

            Assert.IsNotNull(getAudioResult);
            Assert.IsTrue(getAudioResult.Name == acousticResourceName);
            Assert.IsNotNull(addAudioResult);
            Assert.IsNotNull(listAudioResult);
            Assert.IsNotNull(listAudioResult.Audio);
            Assert.IsNotNull(getAcousticModelResult);
            Assert.IsTrue(getAcousticModelResult.Name == acousticModelName);
            Assert.IsTrue(getAcousticModelResult.Description == acousticModelDescription);
            Assert.IsNotNull(createAcousticModelResult);
            Assert.IsNotNull(listAcousticModelsResult);
        }
        #endregion

        #region Recognize
        [TestMethod]
        public void TestRecognize_Success()
        {
            var testAudio = File.ReadAllBytes(testAudioPath);
            var recognizeResult = service.RecognizeSessionless(testAudio, "audio/wav");
            Assert.IsNotNull(recognizeResult);
        }
        #endregion

        #region Jobs
        [TestMethod]
        public void TestJobs_Success()
        {
            var testAudio = File.ReadAllBytes(testAudioPath);
            var createJobResult = service.CreateJob(testAudio, "audio/mp3");
            var jobId = createJobResult.Id;

            var checkJobsResult = service.CheckJobs();
            var checkJobResult = service.CheckJob(jobId);

            var deleteJobResult = service.DeleteJob(jobId);
            Assert.IsNotNull(checkJobsResult);
            Assert.IsNotNull(checkJobResult);
            Assert.IsNotNull(createJobResult);
            Assert.IsTrue(!string.IsNullOrEmpty(createJobResult.Id));
            Assert.IsNotNull(deleteJobResult);
        }
        #endregion

        private void CheckCustomizationStatus(string classifierId)
        {
            var getLangaugeModelResult = service.GetLanguageModel(classifierId);

            Console.WriteLine(string.Format("Classifier status is {0}", getLangaugeModelResult.Status));

            if (getLangaugeModelResult.Status == LanguageModel.StatusEnum.READY || getLangaugeModelResult.Status == LanguageModel.StatusEnum.AVAILABLE)
                autoEvent.Set();
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    CheckCustomizationStatus(classifierId);
                });
            }
        }

        private void CheckCorpusStatus(string classifierId, string corpusName)
        {
            var getCorpusResult = service.GetCorpus(classifierId, corpusName);

            Console.WriteLine(string.Format("Corpus status is {0}", getCorpusResult.Status));

            if (getCorpusResult.Status == Corpus.StatusEnum.BEING_PROCESSED)
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    CheckCorpusStatus(classifierId, corpusName);
                });
            }
            else if (getCorpusResult.Status == Corpus.StatusEnum.UNDETERMINED)
            {
                throw new Exception("Corpus status is undetermined.");
            }
            else
            {
                autoEvent.Set();
            }
        }

        private void CheckAcousticCustomizationStatus(string classifierId)
        {
            var getAcousticModelResult = service.GetAcousticModel(classifierId);

            Console.WriteLine(string.Format("Classifier status is {0}", getAcousticModelResult.Status));

            if (getAcousticModelResult.Status == AcousticModel.StatusEnum.AVAILABLE || getAcousticModelResult.Status == AcousticModel.StatusEnum.READY)
                autoEvent.Set();
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    CheckAcousticCustomizationStatus(classifierId);
                });
            }
        }

        private void CheckAudioStatus(string classifierId, string audioname)
        {
            var getAudioResult = service.GetAudio(classifierId, audioname);

            Console.WriteLine(string.Format("Classifier status is {0}", getAudioResult.Status));

            if (getAudioResult.Status == AudioListing.StatusEnum.OK)
            {
                autoEvent.Set();
            }
            else if (getAudioResult.Status == AudioListing.StatusEnum.INVALID)
            {
                throw new Exception("Adding audio failed");
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    CheckAcousticCustomizationStatus(classifierId);
                });
            }
        }

        public async Task<byte[]> DownloadAcousticResource(string acousticResourceUrl)
        {
            var client = new HttpClient();
            var task = client.GetByteArrayAsync(acousticResourceUrl);
            var msg = await task;

            return msg;
        }

        #region Generated
        #region GetModel
        private SpeechModel GetModel(string modelId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetModel()");
            var result = service.GetModel(modelId: modelId, customData: customData);

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
        private SpeechModels ListModels(Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListModels()");
            var result = service.ListModels(customData: customData);

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

        #region RecognizeSessionless
        private SpeechRecognitionResults RecognizeSessionless(byte[] audio, string contentType, string model = null, string customizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to RecognizeSessionless()");
            var result = service.RecognizeSessionless(audio: audio, contentType: contentType, model: model, customizationId: customizationId, acousticCustomizationId: acousticCustomizationId, baseModelVersion: baseModelVersion, customizationWeight: customizationWeight, inactivityTimeout: inactivityTimeout, keywords: keywords, keywordsThreshold: keywordsThreshold, maxAlternatives: maxAlternatives, wordAlternativesThreshold: wordAlternativesThreshold, wordConfidence: wordConfidence, timestamps: timestamps, profanityFilter: profanityFilter, smartFormatting: smartFormatting, speakerLabels: speakerLabels, customData: customData);

            if (result != null)
            {
                Console.WriteLine("RecognizeSessionless() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to RecognizeSessionless()");
            }

            return result;
        }
        #endregion

        #region CheckJob
        private RecognitionJob CheckJob(string id, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CheckJob()");
            var result = service.CheckJob(id: id, customData: customData);

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
        private RecognitionJobs CheckJobs(Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CheckJobs()");
            var result = service.CheckJobs(customData: customData);

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
        private RecognitionJob CreateJob(byte[] audio, string contentType, string model = null, string callbackUrl = null, string events = null, string userToken = null, long? resultsTtl = null, string customizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateJob()");
            var result = service.CreateJob(audio: audio, contentType: contentType, model: model, callbackUrl: callbackUrl, events: events, userToken: userToken, resultsTtl: resultsTtl, customizationId: customizationId, acousticCustomizationId: acousticCustomizationId, baseModelVersion: baseModelVersion, customizationWeight: customizationWeight, inactivityTimeout: inactivityTimeout, keywords: keywords, keywordsThreshold: keywordsThreshold, maxAlternatives: maxAlternatives, wordAlternativesThreshold: wordAlternativesThreshold, wordConfidence: wordConfidence, timestamps: timestamps, profanityFilter: profanityFilter, smartFormatting: smartFormatting, speakerLabels: speakerLabels, customData: customData);

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
        private BaseModel DeleteJob(string id, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteJob()");
            var result = service.DeleteJob(id: id, customData: customData);

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
        private RegisterStatus RegisterCallback(string callbackUrl, string userSecret = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to RegisterCallback()");
            var result = service.RegisterCallback(callbackUrl: callbackUrl, userSecret: userSecret, customData: customData);

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
        private BaseModel UnregisterCallback(string callbackUrl, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UnregisterCallback()");
            var result = service.UnregisterCallback(callbackUrl: callbackUrl, customData: customData);

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
        private LanguageModel CreateLanguageModel(CreateLanguageModel createLanguageModel, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateLanguageModel()");
            var result = service.CreateLanguageModel(createLanguageModel: createLanguageModel, customData: customData);

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
        private BaseModel DeleteLanguageModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteLanguageModel()");
            var result = service.DeleteLanguageModel(customizationId: customizationId, customData: customData);

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
        private LanguageModel GetLanguageModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetLanguageModel()");
            var result = service.GetLanguageModel(customizationId: customizationId, customData: customData);

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
        private LanguageModels ListLanguageModels(string language = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListLanguageModels()");
            var result = service.ListLanguageModels(language: language, customData: customData);

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
        private BaseModel ResetLanguageModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ResetLanguageModel()");
            var result = service.ResetLanguageModel(customizationId: customizationId, customData: customData);

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
        private BaseModel TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to TrainLanguageModel()");
            var result = service.TrainLanguageModel(customizationId: customizationId, wordTypeToAdd: wordTypeToAdd, customizationWeight: customizationWeight, customData: customData);

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
        private BaseModel UpgradeLanguageModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpgradeLanguageModel()");
            var result = service.UpgradeLanguageModel(customizationId: customizationId, customData: customData);

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
        private BaseModel AddCorpus(string customizationId, string corpusName, System.IO.FileStream corpusFile, bool? allowOverwrite = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddCorpus()");
            var result = service.AddCorpus(customizationId: customizationId, corpusName: corpusName, corpusFile: corpusFile, allowOverwrite: allowOverwrite, customData: customData);

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
        private BaseModel DeleteCorpus(string customizationId, string corpusName, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteCorpus()");
            var result = service.DeleteCorpus(customizationId: customizationId, corpusName: corpusName, customData: customData);

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
        private Corpus GetCorpus(string customizationId, string corpusName, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetCorpus()");
            var result = service.GetCorpus(customizationId: customizationId, corpusName: corpusName, customData: customData);

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
        private Corpora ListCorpora(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListCorpora()");
            var result = service.ListCorpora(customizationId: customizationId, customData: customData);

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
        private BaseModel AddWord(string customizationId, string wordName, CustomWord customWord, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddWord()");
            var result = service.AddWord(customizationId: customizationId, wordName: wordName, customWord: customWord, customData: customData);

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
        private BaseModel AddWords(string customizationId, CustomWords customWords, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddWords()");
            var result = service.AddWords(customizationId: customizationId, customWords: customWords, customData: customData);

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
        private BaseModel DeleteWord(string customizationId, string wordName, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteWord()");
            var result = service.DeleteWord(customizationId: customizationId, wordName: wordName, customData: customData);

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
        private Word GetWord(string customizationId, string wordName, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetWord()");
            var result = service.GetWord(customizationId: customizationId, wordName: wordName, customData: customData);

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
        private Words ListWords(string customizationId, string wordType = null, string sort = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListWords()");
            var result = service.ListWords(customizationId: customizationId, wordType: wordType, sort: sort, customData: customData);

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
        private AcousticModel CreateAcousticModel(CreateAcousticModel createAcousticModel, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateAcousticModel()");
            var result = service.CreateAcousticModel(createAcousticModel: createAcousticModel, customData: customData);

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
        private BaseModel DeleteAcousticModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteAcousticModel()");
            var result = service.DeleteAcousticModel(customizationId: customizationId, customData: customData);

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
        private AcousticModel GetAcousticModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetAcousticModel()");
            var result = service.GetAcousticModel(customizationId: customizationId, customData: customData);

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
        private AcousticModels ListAcousticModels(string language = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListAcousticModels()");
            var result = service.ListAcousticModels(language: language, customData: customData);

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
        private BaseModel ResetAcousticModel(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ResetAcousticModel()");
            var result = service.ResetAcousticModel(customizationId: customizationId, customData: customData);

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
        private BaseModel TrainAcousticModel(string customizationId, string customLanguageModelId = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to TrainAcousticModel()");
            var result = service.TrainAcousticModel(customizationId: customizationId, customLanguageModelId: customLanguageModelId, customData: customData);

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
        private BaseModel UpgradeAcousticModel(string customizationId, string customLanguageModelId = null, Dictionary<string, object> customData = null, bool? force = null)
        {
            Console.WriteLine("\nAttempting to UpgradeAcousticModel()");
            var result = service.UpgradeAcousticModel(customizationId: customizationId, customLanguageModelId: customLanguageModelId, customData: customData, force: force);

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
        private BaseModel AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddAudio()");
            var result = service.AddAudio(customizationId: customizationId, audioName: audioName, audioResource: audioResource, contentType: contentType, containedContentType: containedContentType, allowOverwrite: allowOverwrite, customData: customData);

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
        private BaseModel DeleteAudio(string customizationId, string audioName, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteAudio()");
            var result = service.DeleteAudio(customizationId: customizationId, audioName: audioName, customData: customData);

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
        private AudioListing GetAudio(string customizationId, string audioName, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetAudio()");
            var result = service.GetAudio(customizationId: customizationId, audioName: audioName, customData: customData);

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
        private AudioResources ListAudio(string customizationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListAudio()");
            var result = service.ListAudio(customizationId: customizationId, customData: customData);

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

        #region DeleteUserData
        private BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteUserData()");
            var result = service.DeleteUserData(customerId: customerId, customData: customData);

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
