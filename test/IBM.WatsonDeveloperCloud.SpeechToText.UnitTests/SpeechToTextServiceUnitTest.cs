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
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using System.Net.Http;
using System.Net;

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

            SpeechModelSet response = new SpeechModelSet()
            {
                Models = new List<SpeechModel>()
                {
                    new SpeechModel()
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

            request.As<SpeechModelSet>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModels();

            Assert.IsNotNull(models);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(models.Models.Count > 0);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetModels_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechModelSet response = new SpeechModelSet()
            {
                Models = new List<SpeechModel>()
                {
                    new SpeechModel()
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
                  .Returns(x => { throw new ServiceResponseException(Substitute.For<IResponse>(),
                                                                     Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                     string.Empty); });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModels();
        }

        [TestMethod]
        public void GetModel_Sucess()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechModelSet response = new SpeechModelSet()
            {
                Models = new List<SpeechModel>()
                {
                    new SpeechModel()
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

            request.As<SpeechModelSet>()
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

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetModel_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechModelSet response = new SpeechModelSet()
            {
                Models = new List<SpeechModel>()
                {
                    new SpeechModel()
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
                 .Returns(x => {
                     throw new ServiceResponseException(Substitute.For<IResponse>(),
                                                         Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                         string.Empty);
                 });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModels();
        }

        [TestMethod]
        public void CreateSession_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            Session response = new Session()
            {
                SessionId = "session_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<Session>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.CreateSession("model_name");

            Assert.IsNotNull(models);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(models.SessionId));
        }

        [TestMethod]
        public void CreateSession_Default_Name_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            Session response = new Session()
            {
                SessionId = "session_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<Session>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.CreateSession(string.Empty);

            Assert.IsNotNull(models);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(models.SessionId));
        }

        [TestMethod]
        public void CreateSession_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            Session response = new Session()
            {
                SessionId = "session_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x => {
                      throw new ServiceResponseException(Substitute.For<IResponse>(),
                                                          Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                          string.Empty);
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.CreateSession(string.Empty);
        }

        [TestMethod]
        public void GetSessionStatus_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<RecognizeStatus>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var sessionStatus = service.GetSessionStatus(new Session() { SessionId = "session_id" });

            Assert.IsNotNull(sessionStatus);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.AreEqual(sessionStatus.Session.State, "initialized");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSessionStatus_SessionId_Empty()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<RecognizeStatus>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var sessionStatus = service.GetSessionStatus(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetSessionStatus_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => {
                       throw new ServiceResponseException(Substitute.For<IResponse>(),
                                                           Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                           string.Empty);
                   });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var sessionStatus = service.GetSessionStatus("session_id");
        }

        [TestMethod]
        public void DeleteSession_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<RecognizeStatus>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteSession(new Session() { SessionId = "session_id" });

            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSession_SessionId_Empty()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<RecognizeStatus>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteSession(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void DeleteSession_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                   .Returns(x => {
                       throw new ServiceResponseException(Substitute.For<IResponse>(),
                                                           Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                           string.Empty);
                   });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteSession("session_id");
        }
    }
}
