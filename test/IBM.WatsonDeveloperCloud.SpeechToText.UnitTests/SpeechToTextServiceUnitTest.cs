using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace IBM.WatsonDeveloperCloud.SpeechToText.UnitTest
{
    [TestClass]
    public class SpeechToTextServiceUnitTest
    {
        [TestMethod]
        public void GetModels_Sucess()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            ModelSet response = new ModelSet()
            {
                Models = new List<Model>()
                {
                    new Model()
                    {
                        Description = "TEST",
                        Language = "pt-br",
                        Name = "UNIT_TEST",
                        Rate = 1,
                        Sessions = Guid.NewGuid().ToString(),
                        Url = "http://"
                    }
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<ModelSet>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModels();

            Assert.IsNotNull(models);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(models.Models.Count > 0);
        }

        [TestMethod]
        public void GetModel_Sucess()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            ModelSet response = new ModelSet()
            {
                Models = new List<Model>()
                {
                    new Model()
                    {
                        Description = "TEST",
                        Language = "pt-br",
                        Name = "UNIT_TEST",
                        Rate = 1,
                        Sessions = Guid.NewGuid().ToString(),
                        Url = "http://"
                    }
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<ModelSet>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModels();

            Assert.IsNotNull(models);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(models.Models.Count > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetModel_Without_ModelName()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModel(string.Empty);
        }
    }
}
