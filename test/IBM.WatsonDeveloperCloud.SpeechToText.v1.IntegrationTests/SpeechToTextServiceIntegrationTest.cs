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
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private string _userName;
        private string _password;
        private string _endpoint;
        private static string _createdCustomizationID;
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        [TestInitialize]
        public void Setup()
        {

            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
            File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            _endpoint = vcapServices["speech_to_text"][0]["credentials"]["url"].Value<string>();
            _userName = vcapServices["speech_to_text"][0]["credentials"]["username"].Value<string>();
            _password = vcapServices["speech_to_text"][0]["credentials"]["password"].Value<string>();
        }

        [TestMethod]
        public void t00_GetModels_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            var results = service.GetModels();

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Models);
            Assert.IsTrue(results.Models.Count > 0);
            Assert.IsNotNull(results.Models.First().Name);
            Assert.IsNotNull(results.Models.First().SupportedFeatures);
        }

        [TestMethod]
        public void t01_GetModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            string modelName = "en-US_NarrowbandModel";

            var results = service.GetModel(modelName);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Name);
            Assert.IsNotNull(results.SupportedFeatures);
        }

        [TestMethod]
        public void t02_CreateSession_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            string modelName = "en-US_NarrowbandModel";

            var model = service.GetModel(modelName);

            var result =
                service.CreateSession(model.Name);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.SessionId);
        }

        [TestMethod]
        public void t03_GetSessionStatus_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            string modelName = "en-US_NarrowbandModel";

            var model = service.GetModel(modelName);

            var session =
                service.CreateSession(model.Name);

            var result =
                service.GetSessionStatus(session);

            Assert.IsNotNull(result);
            Assert.AreEqual(SESSION_STATUS_INITIALIZED, result.Session.State);
        }

        [TestMethod]
        public void t04_Recognize_BodyContent_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            FileStream audio =
                File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

            var results =
                service.Recognize(audio.GetMediaTypeFromFile(), audio, "");

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void t05_Recognize_FormData_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            FileStream audio =
                File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

            var results =
                service.Recognize(audio.GetMediaTypeFromFile(),
                                  new Metadata()
                                  {
                                      PartContentType = audio.GetMediaTypeFromFile()
                                  },
                                  audio);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void t06_Recognize_WithSession_BodyContent_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            string modelName = "en-US_BroadbandModel";

            var session =
                service.CreateSession(modelName);

            FileStream audio =
                File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

            var results =
                service.RecognizeWithSession(session.SessionId, audio.GetMediaTypeFromFile(), audio);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void t07_Recognize_WithSession_FormData_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            string modelName = "en-US_BroadbandModel";

            var session =
                service.CreateSession(modelName);

            FileStream audio =
                File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

            var results =
                service.RecognizeWithSession(session.SessionId,
                                             audio.GetMediaTypeFromFile(),
                                             new Metadata()
                                             {
                                                 PartContentType = audio.GetMediaTypeFromFile()
                                             },
                                             audio);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void t08_ObserveResult_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            string modelName = "en-US_BroadbandModel";

            var session =
                service.CreateSession(modelName);

            FileStream audio =
                File.OpenRead(@"SpeechToTextTestData\test-audio.wav");

            Metadata metadata = new Metadata()
            {
                PartContentType = audio.GetMediaTypeFromFile()
            };

            var taskObserveResult = Task.Factory.StartNew<List<SpeechRecognitionEvent>>(() =>
            {
                return service.ObserveResult(session.SessionId, interimResults: true);
            });

            taskObserveResult.ContinueWith((antecedent) =>
            {
                var results = antecedent.Result;

                Assert.IsNotNull(results);
                Assert.IsTrue(results.Count > 0);
                Assert.IsNotNull(results.First().Results);
                Assert.IsTrue(results.First().Results.Count > 0);
                Assert.IsTrue(results.First().Results.First().Alternatives.Count > 0);
                Assert.IsNotNull(results.First().Results.First().Alternatives.First().Transcript);
            });

            var taskRecognizeWithSession = Task.Factory.StartNew(() =>
            {
                service.RecognizeWithSession(session.SessionId, audio.GetMediaTypeFromFile(), metadata, audio, "chunked", modelName);
            });

            taskObserveResult.Wait();
        }
        
        [TestMethod]
        public void t09_CreateCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            string modelName = "en-US_BroadbandModel";

            var customizationId =
                service.CreateCustomModel("model_test", modelName, "Test of Create Custom Model method");

            _createdCustomizationID = customizationId.CustomizationId;

            Assert.IsNotNull(customizationId);
            Assert.IsNotNull(customizationId.CustomizationId);
        }

        [TestMethod]
        public void t10_AddCorpus_Success()
        {
            SpeechToTextService service =
               new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var body =
                File.OpenRead(@"SpeechToTextTestData\test-stt-corpus.txt");

            object result = service.AddCorpus(customization,
                              "stt_integration",
                              false,
                              body);

            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void t11_TrainCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var result = service.TrainCustomModel(customization);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t12_WaitForTraining()
        {
            IsTrainingComplete();
            autoEvent.WaitOne();
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void t13_AddCustomWords_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            object result = service.AddCustomWords(_createdCustomizationID,
                                  new Words()
                                  {
                                      WordsProperty = new List<Word>()
                                      {
                                          new Word()
                                          {
                                             DisplayAs = "Watson",
                                             SoundsLike = new List<string>()
                                             {
                                                 "wat son"
                                             },
                                             WordProperty = "watson"
                                          },
                                          new Word()
                                          {
                                             DisplayAs = "C#",
                                             SoundsLike = new List<string>()
                                             {
                                                 "si sharp"
                                             },
                                             WordProperty = "csharp"
                                          },
                                           new Word()
                                          {
                                             DisplayAs = "SDK",
                                             SoundsLike = new List<string>()
                                             {
                                                 "S.D.K."
                                             },
                                             WordProperty = "sdk"
                                          }
                                      }
                                  });

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t14_TrainCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var result = service.TrainCustomModel(customization);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t15_WaitForTraining()
        {
            IsTrainingComplete();
            autoEvent.WaitOne();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void t16_AddCustomWord_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            object result = service.AddCustomWord(customization,
                                  "social",
                                  new WordDefinition()
                                  {
                                      DisplayAs = "Social",
                                      SoundsLike = new List<string>()
                                             {
                                                 "so cial"
                                             }
                                  });

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t17_TrainCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var result = service.TrainCustomModel(customization);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t18_WaitForTraining()
        {
            IsTrainingComplete();
            autoEvent.WaitOne();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void t19_ListCustomModels_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            Assert.IsNotNull(customizations);
            Assert.IsNotNull(customizations.Customization);
            Assert.IsTrue(customizations.Customization.Count() > 0);
        }
        
        [TestMethod]
        public void t20_ListCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var result =
                service.ListCustomModel(_createdCustomizationID);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.CustomizationId));
        }
        
        [TestMethod]
        public void t21_ListCorpora_Success()
        {
            SpeechToTextService service =
               new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var corpora =
                service.ListCorpora(customization);

            Assert.IsNotNull(corpora);
            Assert.IsNotNull(corpora.CorporaProperty);
            Assert.IsTrue(corpora.CorporaProperty.Count() > 0);
        }

        [TestMethod]
        public void t22_GetCorpus_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var corpora =
                service.ListCorpora(customization);

            var corpus =
                service.GetCorpus(customization, corpora.CorporaProperty.First().Name);

            Assert.IsNotNull(corpus);
            Assert.IsNotNull(corpus.Name);
            Assert.AreEqual(corpora.CorporaProperty.First().Name, corpus.Name);
        }

        [TestMethod]
        public void t23_ListCustomWords_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var words =
                service.ListCustomWords(_createdCustomizationID, null, null);

            Assert.IsNotNull(words);
        }

        [TestMethod]
        public void t24_ListCustomWord_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var words =
                service.ListCustomWords(customization, WordType.All, null);

            var word =
                service.ListCustomWord(customization, words.Words.First().Word);

            Assert.IsNotNull(word);
        }
        [TestMethod]
        public void t25_DeleteCustomWord_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var words =
                service.ListCustomWords(customization, WordType.All, null);

            object result = service.DeleteCustomWord(customization, words.Words.First().Word);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t26_TrainCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var result = service.TrainCustomModel(_createdCustomizationID);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t27_WaitForTraining()
        {
            IsTrainingComplete();
            autoEvent.WaitOne();
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void t28_DeleteCorpus_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var corpora =
                service.ListCorpora(customization);

            var result = service.DeleteCorpus(customization, corpora.CorporaProperty.First().Name);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t29_ResetCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var result = service.ResetCustomModel(customization);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t30_UpgradeCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            var result = service.UpgradeCustomModel(customization);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t31_DeleteCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customization =
                _createdCustomizationID;

            object result = service.DeleteCustomModel(customization);

            Assert.IsNotNull(result);
        }

        //[TestMethod]
        public void t32_DeleteSession_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            string modelName = "en-US_NarrowbandModel";

            var model = service.GetModel(modelName);

            var session =
                service.CreateSession(model.Name);

            object result = service.DeleteSession(session);

            Assert.IsNotNull(result);
        }

        private bool IsTrainingComplete()
        {
            SpeechToTextService service = new SpeechToTextService(_userName, _password);
            service.Endpoint = _endpoint;

            var result = service.ListCustomModel(_createdCustomizationID);

            string status = result.Status.ToLower();
            Console.WriteLine(string.Format("Classifier status is {0}", status));

            if (status == "ready" || status == "available")
                autoEvent.Set();
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    IsTrainingComplete();
                });
            }

            return result.Status.ToLower() == "ready";
        }
    }
}