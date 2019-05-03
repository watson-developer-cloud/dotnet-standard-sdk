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

using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Util;
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
            var listModelsResult = service.ListModels();

            var getModelResult = service.GetModel(
                modelId: EN_US
                );

            Assert.IsNotNull(getModelResult);
            Assert.IsTrue(getModelResult.Result.Name == EN_US);
            Assert.IsNotNull(listModelsResult);
            Assert.IsNotNull(listModelsResult.Result.Models);
        }
        #endregion

        #region Custom Language Models
        [TestMethod]
        public void TestCustomLanguageModels_Success()
        {
            var listLanguageModelsResult = service.ListLanguageModels();

            var createLanguageModelResult = service.CreateLanguageModel(
                name: customModelName,
                baseModelName: EN_US,
                dialect: "en-US",
                description: customModelDescription
                );
            string customizationId = createLanguageModelResult.Result.CustomizationId;

            var getLanguageModelResult = service.GetLanguageModel(
                customizationId: customizationId
                );

            var listCorporaResults = service.ListCorpora(
                customizationId: customizationId
                );

            DetailedResponse<object> addCorpusResults = null;
            using (FileStream fs = File.OpenRead(corpusPath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    addCorpusResults = service.AddCorpus(
                        customizationId: customizationId,
                        corpusName: corpusName,
                        corpusFile: ms
                        );
                }
            }

            var getCorpusResults = service.GetCorpus(
                customizationId: customizationId,
                corpusName: corpusName
                );

            CheckCorpusStatus(
                customizationId: customizationId,
                corpusName: corpusName
                );
            autoEvent.WaitOne();

            var trainLanguageModelResult = service.TrainLanguageModel(
                customizationId: customizationId
                );
            CheckCustomizationStatus(
                customizationId: customizationId
                );
            autoEvent.WaitOne();

            Assert.IsNotNull(trainLanguageModelResult);
            trainLanguageModelResult = null;

            var listCustomWordsResult = service.ListWords(
                customizationId: customizationId,
                wordType: "all",
                sort: "-alphabetical"
                );

            var words = new List<CustomWord>()
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
            };

            var addCustomWordsResult = service.AddWords(
                customizationId: customizationId,
                words: words
                );

            CheckCustomizationStatus(
                customizationId: customizationId
                );
            autoEvent.WaitOne();

            trainLanguageModelResult = service.TrainLanguageModel(
                customizationId: customizationId
                );
            Assert.IsNotNull(trainLanguageModelResult);
            trainLanguageModelResult = null;

            CheckCustomizationStatus(
                customizationId: customizationId
                );
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

            var addCustomWordResult = service.AddWord(
                customizationId: customizationId,
                wordName: "dotnet",
                word: "dotnet",
                soundsLike: new List<string>() { "dotnet" },
                displayAs: ".NET"
                );

            var getCustomWordResult = service.GetWord(
                customizationId: customizationId,
                wordName: "dotnet"
                );

            trainLanguageModelResult = service.TrainLanguageModel(
                customizationId: customizationId
                );
            CheckCustomizationStatus(
                customizationId: customizationId
                );
            autoEvent.WaitOne();
            Assert.IsNotNull(trainLanguageModelResult);
            trainLanguageModelResult = null;

            CheckCorpusStatus(
                customizationId: customizationId,
                corpusName: corpusName
                );
            autoEvent.WaitOne();

            //var upgradeLanguageModelResult = service.UpgradeLanguageModel(
            //    customizationId: customizationId
            //    );
            //Assert.IsNotNull(upgradeLanguageModelResult);

            var listGrammarsResult = service.ListGrammars(customizationId: customizationId);
            var addGrammarResult = service.AddGrammar(
                customizationId: customizationId,
                grammarName: grammarName,
                grammarFile: File.ReadAllText(grammarPath),
                contentType: grammarsContentType
                );
            var getGrammarResult = service.GetGrammar(
                customizationId: customizationId,
                grammarName: grammarName
                );

            CheckCustomizationStatus(
                customizationId: customizationId
                );
            autoEvent.WaitOne();

            var deleteGrammarResult = service.DeleteGrammar(
                customizationId: customizationId,
                grammarName: grammarName
                );

            var deleteCustomWordResults = service.DeleteWord(
                customizationId: customizationId,
                wordName: "csharp"
                );

            var deleteCorpusResults = service.DeleteCorpus(
                customizationId: customizationId,
                corpusName: corpusName
                );

            var resetLanguageModelResult = service.ResetLanguageModel(
                customizationId: customizationId
                );
            Assert.IsNotNull(resetLanguageModelResult);

            var deleteLanguageModelResults = service.DeleteLanguageModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(deleteGrammarResult);
            Assert.IsNotNull(getGrammarResult);
            Assert.IsTrue(getGrammarResult.Result.Name == grammarName);
            Assert.IsNotNull(addGrammarResult);
            Assert.IsNotNull(listGrammarsResult);
            Assert.IsNotNull(deleteCustomWordResults);
            Assert.IsNotNull(deleteCorpusResults);
            Assert.IsNotNull(deleteLanguageModelResults);
            Assert.IsNotNull(getCustomWordResult);
            Assert.IsTrue(getCustomWordResult.Result._Word == "dotnet");
            Assert.IsNotNull(addCustomWordResult);
            Assert.IsNotNull(addCustomWordsResult);
            Assert.IsNotNull(listCustomWordsResult);
            Assert.IsNotNull(listCustomWordsResult.Result._Words);
            Assert.IsNotNull(getCorpusResults);
            Assert.IsTrue(getCorpusResults.Result.Name == corpusName);
            Assert.IsNotNull(addCorpusResults);
            Assert.IsNotNull(listCorporaResults);
            Assert.IsNotNull(listCorporaResults.Result._Corpora);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.Result.CustomizationId == customizationId);
            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsNotNull(listLanguageModelsResult);
            Assert.IsNotNull(listLanguageModelsResult.Result.Customizations);
        }
        #endregion

        #region German Model
        [TestMethod]
        public void TestGermanLanguageModel_Success()
        {
            var createLanguageModelResult = service.CreateLanguageModel(
                name: customModelName,
                baseModelName: "de-DE_BroadbandModel",
                dialect: "de-DE",
                description: customModelDescription
                );
            string customizationId = createLanguageModelResult.Result.CustomizationId;

            var getLanguageModelResult = service.GetLanguageModel(
                customizationId: customizationId
                );

            var deleteLanguageModelResult = service.DeleteLanguageModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsNotNull(deleteLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.Result.BaseModelName == "de-DE_BroadbandModel");
        }
        #endregion

        #region Brazilian Broadband Model
        [TestMethod]
        public void TestBrazilianBroadbandLanguageModel_Success()
        {
            var createLanguageModelResult = service.CreateLanguageModel(
                name: customModelName,
                baseModelName: "pt-BR_BroadbandModel",
                dialect: "pt-BR",
                description: customModelDescription
                );
            string customizationId = createLanguageModelResult.Result.CustomizationId;

            var getLanguageModelResult = service.GetLanguageModel(
                customizationId: customizationId
                );

            var deleteLanguageModelResult = service.DeleteLanguageModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsNotNull(deleteLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.Result.BaseModelName == "pt-BR_BroadbandModel");
        }
        #endregion

        #region Brazilian Narrowband Model
        [TestMethod]
        public void TestBrazilianNarrowbandLanguageModel_Success()
        {
            var createLanguageModelResult = service.CreateLanguageModel(
                name: customModelName,
                baseModelName: "pt-BR_NarrowbandModel",
                dialect: "pt-BR",
                description: customModelDescription
                );
            string customizationId = createLanguageModelResult.Result.CustomizationId;

            var getLanguageModelResult = service.GetLanguageModel(
                customizationId: customizationId
                );

            var deleteLanguageModelResult = service.DeleteLanguageModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(createLanguageModelResult);
            Assert.IsNotNull(getLanguageModelResult);
            Assert.IsNotNull(deleteLanguageModelResult);
            Assert.IsTrue(getLanguageModelResult.Result.BaseModelName == "pt-BR_NarrowbandModel");
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

            var listAcousticModelsResult = service.ListAcousticModels();

            var createAcousticModelResult = service.CreateAcousticModel(name: acousticModelName, baseModelName: "de-DE_BroadbandModel", description: acousticModelDescription);
            var acousticCustomizationId = createAcousticModelResult.Result.CustomizationId;
            var getAcousticModelResult = service.GetAcousticModel(
                customizationId: acousticCustomizationId
                );
            var deleteAcousticModelResult = service.DeleteAcousticModel(
                customizationId: acousticCustomizationId
                );

            Assert.IsNotNull(createAcousticModelResult);
            Assert.IsNotNull(getAcousticModelResult);
            Assert.IsNotNull(deleteAcousticModelResult);
            Assert.IsTrue(getAcousticModelResult.Result.BaseModelName == "de-DE_BroadbandModel");
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

            var listAcousticModelsResult = service.ListAcousticModels();

            var createAcousticModelResult = service.CreateAcousticModel(
                name: acousticModelName,
                baseModelName: EN_US,
                description: acousticModelDescription
                );
            var acousticCustomizationId = createAcousticModelResult.Result.CustomizationId;

            var getAcousticModelResult = service.GetAcousticModel(
                customizationId: acousticCustomizationId
                );

            var listAudioResult = service.ListAudio(
                customizationId: acousticCustomizationId
                );

            DetailedResponse<object> addAudioResult = null;

            addAudioResult = service.AddAudio(
                customizationId: acousticCustomizationId,
                audioName: acousticResourceName,
                audioResource: acousticResourceData,
                contentType: acousticResourceMimeType,
                allowOverwrite: true
                );

            var getAudioResult = service.GetAudio(
                customizationId: acousticCustomizationId,
                audioName: acousticResourceName
                );

            CheckAudioStatus(
                customizationId: acousticCustomizationId,
                audioName: acousticResourceName
                );
            autoEvent.WaitOne();

            CheckAcousticCustomizationStatus(
                customizationId: acousticCustomizationId
                );
            autoEvent.WaitOne();

            var trainAcousticModelResult = service.TrainAcousticModel(
                customizationId: acousticCustomizationId
                );

            CheckAcousticCustomizationStatus(
                customizationId: acousticCustomizationId
                );
            autoEvent.WaitOne();

            //var upgradeAcousticModel = UpgradeAcousticModel(acousticCustomizationId);

            //CheckAcousticCustomizationStatus(acousticCustomizationId);
            //autoEvent.WaitOne();

            var deleteAudioResult = service.DeleteAudio(
                customizationId: acousticCustomizationId,
                audioName: acousticResourceName
                );

            var resetAcousticModelResult = service.ResetAcousticModel(
                customizationId: acousticCustomizationId
                );

            var deleteAcousticModelResult = service.DeleteAcousticModel(
                customizationId: acousticCustomizationId
                );

            Assert.IsNotNull(deleteAudioResult);
            Assert.IsNotNull(deleteAcousticModelResult);

            Assert.IsNotNull(getAudioResult);
            Assert.IsTrue(getAudioResult.Result.Name == acousticResourceName);
            Assert.IsNotNull(addAudioResult);
            Assert.IsNotNull(listAudioResult);
            Assert.IsNotNull(listAudioResult.Result.Audio);
            Assert.IsNotNull(getAcousticModelResult);
            Assert.IsTrue(getAcousticModelResult.Result.Name == acousticModelName);
            Assert.IsTrue(getAcousticModelResult.Result.Description == acousticModelDescription);
            Assert.IsNotNull(createAcousticModelResult);
            Assert.IsNotNull(listAcousticModelsResult);
        }
        #endregion

        #region Recognize
        [TestMethod]
        public void TestRecognize_Success()
        {
            var testAudio = File.ReadAllBytes(testAudioPath);
            var recognizeResult = service.Recognize(
                audio: testAudio,
                contentType: "audio/wav"
                );
            Assert.IsNotNull(recognizeResult.Result);
            Assert.IsNotNull(recognizeResult.Result.Results);
            Assert.IsTrue(recognizeResult.Result.Results.Count > 0);
        }
        #endregion

        #region Jobs
        [TestMethod]
        public void TestJobs_Success()
        {
            var testAudio = File.ReadAllBytes(testAudioPath);
            var createJobResult = service.CreateJob(
                audio: testAudio,
                contentType: "audio/mp3"
                );
            var jobId = createJobResult.Result.Id;

            var checkJobsResult = service.CheckJobs();
            var checkJobResult = service.CheckJob(
                id: jobId
                );

            var deleteJobResult = service.DeleteJob(
                id: jobId
                );
            Assert.IsNotNull(checkJobsResult);
            Assert.IsNotNull(checkJobResult);
            Assert.IsNotNull(createJobResult);
            Assert.IsTrue(!string.IsNullOrEmpty(createJobResult.Result.Id));
            Assert.IsNotNull(deleteJobResult);
        }
        #endregion

        private void CheckCustomizationStatus(string customizationId)
        {
            var getLangaugeModelResult = service.GetLanguageModel(customizationId);

            Console.WriteLine(string.Format("Classifier status is {0}", getLangaugeModelResult.Result.Status));

            if (getLangaugeModelResult.Result.Status == LanguageModel.StatusEnumValue.READY || getLangaugeModelResult.Result.Status == LanguageModel.StatusEnumValue.AVAILABLE)
                autoEvent.Set();
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    CheckCustomizationStatus(customizationId);
                });
            }
        }

        private void CheckCorpusStatus(string customizationId, string corpusName)
        {
            var getCorpusResult = service.GetCorpus(customizationId, corpusName);

            Console.WriteLine(string.Format("Corpus status is {0}", getCorpusResult.Result.Status));

            if (getCorpusResult.Result.Status == Corpus.StatusEnumValue.BEING_PROCESSED)
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    CheckCorpusStatus(customizationId, corpusName);
                });
            }
            else if (getCorpusResult.Result.Status == Corpus.StatusEnumValue.UNDETERMINED)
            {
                throw new Exception("Corpus status is undetermined.");
            }
            else
            {
                autoEvent.Set();
            }
        }

        private void CheckAcousticCustomizationStatus(string customizationId)
        {
            var getAcousticModelResult = service.GetAcousticModel(customizationId);

            Console.WriteLine(string.Format("Classifier status is {0}", getAcousticModelResult.Result.Status));

            if (getAcousticModelResult.Result.Status == AcousticModel.StatusEnumValue.AVAILABLE || getAcousticModelResult.Result.Status == AcousticModel.StatusEnumValue.READY)
                autoEvent.Set();
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    CheckAcousticCustomizationStatus(customizationId);
                });
            }
        }

        private void CheckAudioStatus(string customizationId, string audioName)
        {
            var getAudioResult = service.GetAudio(customizationId, audioName);

            Console.WriteLine(string.Format("Classifier status is {0}", getAudioResult.Result.Status));

            if (getAudioResult.Result.Status == AudioListing.StatusEnumValue.OK)
            {
                autoEvent.Set();
            }
            else if (getAudioResult.Result.Status == AudioListing.StatusEnumValue.INVALID)
            {
                throw new Exception("Adding audio failed");
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    CheckAcousticCustomizationStatus(customizationId);
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
    }
}
