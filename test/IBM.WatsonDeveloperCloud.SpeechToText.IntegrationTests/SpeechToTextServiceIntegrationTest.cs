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

namespace IBM.WatsonDeveloperCloud.SpeechToText.IntegrationTests
{
    [TestClass]
    public class SpeechToTextServiceIntegrationTest
    {
        private const string SESSION_STATUS_INITIALIZED = "initialized";
        private string _userName;
        private string _password;
        private string _endpoint;
        private static string _createdCustomizationID;

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
        public void t03_CreateSession_Success()
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
        public void t04_GetSessionStatus_Success()
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
        public void t05_Recognize_BodyContent_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

            var results =
                service.Recognize(audio.GetMediaTypeFromFile(), audio);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void t06_Recognize_FormData_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

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
        public void t07_Recognize_WithSession_BodyContent_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            string modelName = "en-US_BroadbandModel";

            var session =
                service.CreateSession(modelName);

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

            var results =
                service.RecognizeWithSession(session.SessionId, audio.GetMediaTypeFromFile(), audio);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void t08_Recognize_WithSession_FormData_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            string modelName = "en-US_BroadbandModel";

            var session =
                service.CreateSession(modelName);

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

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
        public void t09_ObserveResult_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            string modelName = "en-US_BroadbandModel";

            var session =
                service.CreateSession(modelName);

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

            var recognize =
                service.RecognizeWithSession(session.SessionId, audio.GetMediaTypeFromFile(), audio);

            var results =
                service.ObserveResult(session.SessionId);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
            Assert.IsNotNull(results.First().Results);
            Assert.IsTrue(results.First().Results.Count > 0);
            Assert.IsTrue(results.First().Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.First().Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void t10_CreateCustomModel_Success()
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
        public void t11_ListCustomModels_Success()
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
        public void t12_ListCustomModel_Success()
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
        public void t13_TrainCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var result = service.TrainCustomModel(customization.CustomizationId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t14_UpgradeCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var result = service.UpgradeCustomModel(customization.CustomizationId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t15_ResetCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var result = service.ResetCustomModel(customization.CustomizationId);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void t16_AddCorpus_Success()
        {
            SpeechToTextService service =
               new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var body =
                File.OpenRead(@"Assets\test-stt-corpus.txt");

            object result = service.AddCorpus(customization.CustomizationId,
                              "stt_integration",
                              false,
                              body);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t17_ListCorpora_Success()
        {
            SpeechToTextService service =
               new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var corpora =
                service.ListCorpora(customization.CustomizationId);

            Assert.IsNotNull(corpora);
            Assert.IsNotNull(corpora.CorporaProperty);
            Assert.IsTrue(corpora.CorporaProperty.Count() > 0);
        }

        [TestMethod]
        public void t18_GetCorpus_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var corpora =
                service.ListCorpora(customization.CustomizationId);

            var corpus =
                service.GetCorpus(customization.CustomizationId, corpora.CorporaProperty.First().Name);

            Assert.IsNotNull(corpus);
            Assert.IsNotNull(corpus.Name);
            Assert.AreEqual(corpora.CorporaProperty.First().Name, corpus.Name);
        }

        [TestMethod]
        public void t19_AddCustomWords_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            object result = service.AddCustomWords(customization.CustomizationId,
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
        public void t20_AddCustomWord_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            object result = service.AddCustomWord(customization.CustomizationId,
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
        public void t21_ListCustomWords_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var words =
                service.ListCustomWords(customization.CustomizationId, null, null);

            Assert.IsNull(words);
        }

        [TestMethod]
        public void t22_ListCustomWord_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var words =
                service.ListCustomWords(customization.CustomizationId, WordType.All, null);

            var word =
                service.ListCustomWord(customization.CustomizationId, words.Words.First().Word);

            Assert.IsNotNull(word);
        }
        
        [TestMethod]
        public void t23_DeleteCustomWord_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var words =
                service.ListCustomWords(customization.CustomizationId, WordType.All, null);

            object result = service.DeleteCustomWord(customization.CustomizationId, words.Words.First().Word);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t24_DeleteCorpus_Success()
        {
            SpeechToTextService service =
              new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var corpora =
                service.ListCorpora(customization.CustomizationId);

            var result = service.DeleteCorpus(customization.CustomizationId, corpora.CorporaProperty.First().Name);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t25_DeleteCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            object result = service.DeleteCustomModel(customization.CustomizationId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t26_DeleteSession_Success()
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
    }
}