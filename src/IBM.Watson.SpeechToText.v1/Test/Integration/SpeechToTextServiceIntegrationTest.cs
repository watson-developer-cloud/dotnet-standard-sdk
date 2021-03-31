/**
* (C) Copyright IBM Corp. 2017, 2020.
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
using IBM.Watson.SpeechToText.v1.Model;
using IBM.Watson.SpeechToText.v1.Websockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static IBM.Watson.SpeechToText.v1.SpeechToTextService;

namespace IBM.Watson.SpeechToText.v1.IntegrationTests
{
    [TestClass]
    public class SpeechToTextServiceIntegrationTest
    {
        //private const string SESSION_STATUS_INITIALIZED = "initialized";
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
            service = new SpeechToTextService();
        }

        #region Models
        [TestMethod]
        public void TestModels_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listModelsResult = service.ListModels();

            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
            var listLanguageModelsResult = service.ListLanguageModels();

            service.WithHeader("X-Watson-Test", "1");
            var createLanguageModelResult = service.CreateLanguageModel(
                name: customModelName,
                baseModelName: EN_US,
                dialect: "en-US",
                description: customModelDescription
                );
            string customizationId = createLanguageModelResult.Result.CustomizationId;

            service.WithHeader("X-Watson-Test", "1");
            var getLanguageModelResult = service.GetLanguageModel(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var listCorporaResults = service.ListCorpora(
                customizationId: customizationId
                );

            DetailedResponse<object> addCorpusResults = null;
            using (FileStream fs = File.OpenRead(corpusPath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    addCorpusResults = service.AddCorpus(
                        customizationId: customizationId,
                        corpusName: corpusName,
                        corpusFile: ms
                        );
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var getCorpusResults = service.GetCorpus(
                customizationId: customizationId,
                corpusName: corpusName
                );

            CheckCorpusStatus(
                customizationId: customizationId,
                corpusName: corpusName
                );
            autoEvent.WaitOne();

            service.WithHeader("X-Watson-Test", "1");
            var trainLanguageModelResult = service.TrainLanguageModel(
                customizationId: customizationId
                );
            CheckCustomizationStatus(
                customizationId: customizationId
                );
            autoEvent.WaitOne();

            Assert.IsNotNull(trainLanguageModelResult);
            trainLanguageModelResult = null;

            service.WithHeader("X-Watson-Test", "1");
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

            service.WithHeader("X-Watson-Test", "1");
            var addCustomWordsResult = service.AddWords(
                customizationId: customizationId,
                words: words
                );

            CheckCustomizationStatus(
                customizationId: customizationId
                );
            autoEvent.WaitOne();

            service.WithHeader("X-Watson-Test", "1");
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

            service.WithHeader("X-Watson-Test", "1");
            var addCustomWordResult = service.AddWord(
                customizationId: customizationId,
                wordName: "dotnet",
                word: "dotnet",
                soundsLike: new List<string>() { "dotnet" },
                displayAs: ".NET"
                );

            service.WithHeader("X-Watson-Test", "1");
            var getCustomWordResult = service.GetWord(
                customizationId: customizationId,
                wordName: "dotnet"
                );

            service.WithHeader("X-Watson-Test", "1");
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

            //service.WithHeader("X-Watson-Test", "1");
            //var upgradeLanguageModelResult = service.UpgradeLanguageModel(
            //    customizationId: customizationId
            //    );
            //Assert.IsNotNull(upgradeLanguageModelResult);

            service.WithHeader("X-Watson-Test", "1");
            var listGrammarsResult = service.ListGrammars(customizationId: customizationId);
            service.WithHeader("X-Watson-Test", "1");
            var addGrammarResult = service.AddGrammar(
                customizationId: customizationId,
                grammarName: grammarName,
                grammarFile: new MemoryStream(File.ReadAllBytes(grammarPath)),
                contentType: grammarsContentType
                );
            service.WithHeader("X-Watson-Test", "1");
            var getGrammarResult = service.GetGrammar(
                customizationId: customizationId,
                grammarName: grammarName
                );

            CheckCustomizationStatus(
                customizationId: customizationId
                );
            autoEvent.WaitOne();

            service.WithHeader("X-Watson-Test", "1");
            var deleteGrammarResult = service.DeleteGrammar(
                customizationId: customizationId,
                grammarName: grammarName
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteCustomWordResults = service.DeleteWord(
                customizationId: customizationId,
                wordName: "csharp"
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteCorpusResults = service.DeleteCorpus(
                customizationId: customizationId,
                corpusName: corpusName
                );

            service.WithHeader("X-Watson-Test", "1");
            var resetLanguageModelResult = service.ResetLanguageModel(
                customizationId: customizationId
                );
            Assert.IsNotNull(resetLanguageModelResult);

            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
            var createLanguageModelResult = service.CreateLanguageModel(
                name: customModelName,
                baseModelName: "de-DE_BroadbandModel",
                dialect: "de-DE",
                description: customModelDescription
                );
            string customizationId = createLanguageModelResult.Result.CustomizationId;

            service.WithHeader("X-Watson-Test", "1");
            var getLanguageModelResult = service.GetLanguageModel(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
            var createLanguageModelResult = service.CreateLanguageModel(
                name: customModelName,
                baseModelName: "pt-BR_BroadbandModel",
                dialect: "pt-BR",
                description: customModelDescription
                );
            string customizationId = createLanguageModelResult.Result.CustomizationId;

            service.WithHeader("X-Watson-Test", "1");
            var getLanguageModelResult = service.GetLanguageModel(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
            var createLanguageModelResult = service.CreateLanguageModel(
                name: customModelName,
                baseModelName: "pt-BR_NarrowbandModel",
                dialect: "pt-BR",
                description: customModelDescription
                );
            string customizationId = createLanguageModelResult.Result.CustomizationId;

            service.WithHeader("X-Watson-Test", "1");
            var getLanguageModelResult = service.GetLanguageModel(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
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
                Console.WriteLine(string.Format("Failed to get acoustic resource data: {0}", e.Message));
            }

            Task.WaitAll();

            service.WithHeader("X-Watson-Test", "1");
            var listAcousticModelsResult = service.ListAcousticModels();

            service.WithHeader("X-Watson-Test", "1");
            var createAcousticModelResult = service.CreateAcousticModel(
                name: acousticModelName,
                baseModelName: "de-DE_BroadbandModel",
                description: acousticModelDescription
                );
            var acousticCustomizationId = createAcousticModelResult.Result.CustomizationId;
            service.WithHeader("X-Watson-Test", "1");
            var getAcousticModelResult = service.GetAcousticModel(
                customizationId: acousticCustomizationId
                );
            service.WithHeader("X-Watson-Test", "1");
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

            service.WithHeader("X-Watson-Test", "1");
            var listAcousticModelsResult = service.ListAcousticModels();

            service.WithHeader("X-Watson-Test", "1");
            var createAcousticModelResult = service.CreateAcousticModel(
                name: acousticModelName,
                baseModelName: EN_US,
                description: acousticModelDescription
                );
            var acousticCustomizationId = createAcousticModelResult.Result.CustomizationId;

            service.WithHeader("X-Watson-Test", "1");
            var getAcousticModelResult = service.GetAcousticModel(
                customizationId: acousticCustomizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var listAudioResult = service.ListAudio(
                customizationId: acousticCustomizationId
                );

            DetailedResponse<object> addAudioResult = null;

            service.WithHeader("X-Watson-Test", "1");
            addAudioResult = service.AddAudio(
                customizationId: acousticCustomizationId,
                audioName: acousticResourceName,
                audioResource: new MemoryStream(acousticResourceData),
                contentType: acousticResourceMimeType,
                allowOverwrite: true
                );

            service.WithHeader("X-Watson-Test", "1");
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

            service.WithHeader("X-Watson-Test", "1");
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

            service.WithHeader("X-Watson-Test", "1");
            var deleteAudioResult = service.DeleteAudio(
                customizationId: acousticCustomizationId,
                audioName: acousticResourceName
                );

            service.WithHeader("X-Watson-Test", "1");
            var resetAcousticModelResult = service.ResetAcousticModel(
                customizationId: acousticCustomizationId
                );

            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
            var recognizeResult = service.Recognize(
                audio: new MemoryStream(testAudio),
                contentType: "audio/wav",
                endOfPhraseSilenceTime: 0.4,
                splitTranscriptAtPhraseEnd: true,
                speechDetectorSensitivity: 0.5f,
                backgroundAudioSuppression: 0.5f
                );
            Assert.IsNotNull(recognizeResult.Result);
            Assert.IsNotNull(recognizeResult.Result.Results);
            Assert.IsTrue(recognizeResult.Result.Results.Count > 1);
        }

        [TestMethod]
        public void TestRecognizeUsingWebsockets()
        {
            service.WithHeader("X-Watson-Test", "1");
            RecognizeCallback callback = new RecognizeCallback();

            string Name = @"SpeechToTextTestData/sample1.wav";

            try
            {
                byte[] filebytes = File.ReadAllBytes(Name);
                MemoryStream stream = new MemoryStream(filebytes);

                callback.OnOpen = () =>
                {
                    System.Diagnostics.Debug.WriteLine("On Open");
                };
                callback.OnClose = () =>
                {
                    System.Diagnostics.Debug.WriteLine("On Close");
                };
                callback.OnMessage = (speechResults) =>
                {
                    System.Diagnostics.Debug.WriteLine("On Message");
                    Assert.IsNotNull(speechResults);
                };
                callback.OnError = (err) =>
                {
                    System.Diagnostics.Debug.WriteLine("On error");
                    System.Diagnostics.Debug.WriteLine(err);
                };
                service.RecognizeUsingWebSocket(
                    callback: callback,
                    audio: stream,
                    contentType: RecognizeEnums.ContentTypeValue.AUDIO_WAV,
                    interimResults: true,
                    model: RecognizeEnums.ModelValue.EN_US_BROADBANDMODEL,
                    inactivityTimeout: 55,
                    keywords: new List<string>{"could"},
                    keywordsThreshold: 0.8f,
                    timestamps: true,
                    profanityFilter: true,
                    smartFormatting: true,
                    speakerLabels: true,
                    processingMetrics: true,
                    processingMetricsInterval: 0.9f,
                    audioMetrics: true,
                    endOfPhraseSilenceTime: 0,
                    splitTranscriptAtPhraseEnd: true,
                    speechDetectorSensitivity: 1.0f,
                    backgroundAudioSuppression: 0.5f
                    );
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
        #endregion

        #region Jobs
        [TestMethod]
        public void TestJobs_Success()
        {
            var testAudio = File.ReadAllBytes(testAudioPath);
            service.WithHeader("X-Watson-Test", "1");
            var createJobResult = service.CreateJob(
                audio: new MemoryStream(testAudio),
                contentType: "audio/mp3",
                endOfPhraseSilenceTime: 2,
                splitTranscriptAtPhraseEnd: true,
                speechDetectorSensitivity: 0.5f,
                backgroundAudioSuppression: 0.5f
                );
            var jobId = createJobResult.Result.Id;

            service.WithHeader("X-Watson-Test", "1");
            var checkJobsResult = service.CheckJobs();
            service.WithHeader("X-Watson-Test", "1");
            var checkJobResult = service.CheckJob(
                id: jobId
                );

            service.WithHeader("X-Watson-Test", "1");
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

        #region Callbacks
        [TestMethod]
        public void TestCallbacks_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var registerCallbackResult = service.RegisterCallback(
                callbackUrl: "https://watson-test-resources.mybluemix.net/speech-to-text-async/secure/callback",
                userSecret: "ThisIsMySecret"
                );

            service.WithHeader("X-Watson-Test", "1");
            var unregisterCallbackResult = service.UnregisterCallback(
                callbackUrl: "https://watson-test-resources.mybluemix.net/speech-to-text-async/secure/callback"
                );

            Assert.IsNotNull(registerCallbackResult);
            Assert.IsNotNull(registerCallbackResult.Result);
            Assert.IsNotNull(registerCallbackResult.Result.Status);
            Assert.IsTrue(registerCallbackResult.Result.Url == "https://watson-test-resources.mybluemix.net/speech-to-text-async/secure/callback");
            Assert.IsNotNull(unregisterCallbackResult.StatusCode == 200);
        }
        #endregion

        private void CheckCustomizationStatus(string customizationId)
        {
            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
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
