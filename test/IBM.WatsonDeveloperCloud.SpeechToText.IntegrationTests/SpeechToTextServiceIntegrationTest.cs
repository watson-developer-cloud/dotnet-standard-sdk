using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;

namespace IBM.WatsonDeveloperCloud.SpeechToText.IntegrationTests
{
    [TestClass]
    public class SpeechToTextServiceIntegrationTest
    {
        const string SESSION_STATUS_INITIALIZED = "initialized";

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
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

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
        public void Recognize_Sucess()
        {
            SpeechToTextService service =
                new SpeechToTextService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net";

            FileStream audio =
                File.OpenRead(@"Assets\let_it_be.ogg");

            var results =
                service.Recognize(audio);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.Results);
            Assert.IsTrue(results.Results.Count > 0);
        }
    }
}