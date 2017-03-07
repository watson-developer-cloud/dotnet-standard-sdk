using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;
using Newtonsoft.Json.Linq;

namespace IBM.WatsonDeveloperCloud.SpeechToText.IntegrationTests
{
    [TestClass]
    public class SpeechToTextServiceIntegrationTest
    {
        private const string SESSION_STATUS_INITIALIZED = "initialized";
        private string _userName;
        private string _password;
        private string _endpoint;

        [TestInitialize]
        public void Setup()
        {
            var environmentVariable =
                Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var isFile =
                Path.IsPathRooted(environmentVariable);

            JObject vcapServices;
            JToken speechToTextCredential;

            if (isFile)
            {
                var fileContent =
                    File.ReadAllText(environmentVariable);

                vcapServices =
                    JObject.Parse(fileContent);
            }
            else
            {
                vcapServices =
                    JObject.Parse(environmentVariable);
            }

            speechToTextCredential =
                    vcapServices["VCAP_SERVICES"]["speech_to_text"][0]["credentials"];

            _endpoint = speechToTextCredential["url"].Value<string>();
            _userName = speechToTextCredential["username"].Value<string>();
            _password = speechToTextCredential["password"].Value<string>();
        }

        [TestMethod]
        public void GetModels_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

            var results = service.GetModels();

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Models);
            Assert.IsTrue(results.Models.Count > 0);
            Assert.IsNotNull(results.Models.First().Name);
            Assert.IsNotNull(results.Models.First().SupportedFeatures);
        }

        [TestMethod]
        public void GetModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

            string modelName = "en-US_NarrowbandModel";

            var results = service.GetModel(modelName);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Name);
            Assert.IsNotNull(results.SupportedFeatures);
        }

        [TestMethod]
        public void CreateSession_Success()
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
        public void GetSessionStatus_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

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
        public void DeleteSession_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

            string modelName = "en-US_NarrowbandModel";

            var model = service.GetModel(modelName);

            var session =
                service.CreateSession(model.Name);

            service.DeleteSession(session);
        }

        [TestMethod]
        public void Recognize_BodyContent_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

            var results =
                service.Recognize(RecognizeOptions.Builder
                                                  .WithBody(audio)
                                                  .Build());

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void Recognize_FormData_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

            var results =
                service.Recognize(RecognizeOptions.Builder
                                                  .WithFormData(new Metadata()
                                                  {
                                                      PartContentType = audio.GetMediaTypeFromFile()
                                                  })
                                                  .Upload(audio)
                                                  .Build());

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void Recognize_WithSession_BodyContent_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

            string modelName = "en-US_BroadbandModel";

            var session =
                service.CreateSession(modelName);

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

            var results =
                service.Recognize(session.SessionId,
                                  RecognizeOptions.Builder
                                                  .WithBody(audio)
                                                  .Build());

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void Recognize_WithSession_FormData_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

            string modelName = "en-US_BroadbandModel";

            var session =
                service.CreateSession(modelName);

            FileStream audio =
                File.OpenRead(@"Assets\test-audio.wav");

            var results =
                service.Recognize(session.SessionId,
                                  RecognizeOptions.Builder
                                                  .WithFormData(new Metadata()
                                                  {
                                                      PartContentType = audio.GetMediaTypeFromFile()
                                                  })
                                                  .Upload(audio)
                                                  .Build());

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
            Assert.IsTrue(results.Results.First().Alternatives.Count > 0);
            Assert.IsNotNull(results.Results.First().Alternatives.First().Transcript);
        }

        [TestMethod]
        public void ObserveResult_Success()
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
                service.Recognize(session.SessionId,
                                  RecognizeOptions.Builder
                                                  .WithBody(audio)
                                                  .Build());

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
        public void CreateCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            string modelName = "en-US_BroadbandModel";

            var customizationId =
                service.CreateCustomModel("model_test", modelName, "Test of Create Custom Model method");

            Assert.IsNotNull(customizationId);
            Assert.IsNotNull(customizationId.CustomizationId);
        }

        [TestMethod]
        public void ListCustomModels_Success()
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
        public void ListCustomModel_Success()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            var result =
                service.ListCustomModel(customization.CustomizationId);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.CustomizationId));
        }

        [TestMethod]
        public void TrainCustomModel()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            service.TrainCustomModel(customization.CustomizationId);
        }

        [TestMethod]
        public void ResetCustomModel()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            service.ResetCustomModel(customization.CustomizationId);
        }

        [TestMethod]
        public void UpgradeCustomModel()
        {
            SpeechToTextService service =
                new SpeechToTextService(_userName, _password);

            service.Endpoint = _endpoint;

            var customizations =
                service.ListCustomModels();

            var customization =
                customizations.Customization.First();

            service.UpgradeCustomModel(customization.CustomizationId);
        }
    }
}