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
