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

using IBM.Watson.Assistant.v2.Model;
using IBM.Watson.Http;
using IBM.Watson.Http.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.Watson.Assistant.v2.UnitTests
{
    [TestClass]
    public class AssistantV2UnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            AssistantService service =
                new AssistantService(null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            AssistantService service =
                new AssistantService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            AssistantService service =
                new AssistantService(null, "password", "2018-02-16");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Password_Null()
        {
            AssistantService service =
                new AssistantService("username", null, "2018-02-16");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Version_Null()
        {
            AssistantService service =
                new AssistantService("username", "password", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            AssistantService service =
                new AssistantService("username", "password", "2018-02-16");

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            AssistantService service =
                new AssistantService(new WatsonHttpClient());

            Assert.IsNotNull(service);
        }
        #endregion

        #region Create Client
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                .Returns(client);

            return client;
        }
        #endregion

        #region Sessions
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSession_No_VersionDate()
        {
            AssistantService service = new AssistantService("username", "password", "versionDate");
            service.VersionDate = null;

            service.CreateSession("assistantId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSession_No_AssistantId()
        {
            AssistantService service = new AssistantService("username", "password", "versionDate");
            service.CreateSession(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateSession_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(x =>
                {
                    throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                });

            AssistantService service = new AssistantService(client);
            service.VersionDate = "versionDate";
            service.CreateSession("assistantId");
        }

        [TestMethod]
        public void CreateSession_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<SessionResponse>();
            response.SessionId = "sessionId";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<SessionResponse>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateSession("assistantId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.SessionId);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSession_No_VersionDate()
        {
            AssistantService service = new AssistantService("username", "password", "versionDate");
            service.VersionDate = null;

            service.DeleteSession("assistantId", "sessionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSession_No_AssistantId()
        {
            AssistantService service = new AssistantService("username", "password", "versionDate");
            service.DeleteSession(null, "sessionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSession_No_SessionId()
        {
            AssistantService service = new AssistantService("username", "password", "versionDate");
            service.DeleteSession("assistantId", null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteSession_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(x =>
                {
                    throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                });

            AssistantService service = new AssistantService(client);
            service.VersionDate = "versionDate";
            service.DeleteSession("assistantId", "sessionId");
        }

        [TestMethod]
        public void DeleteSession_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<BaseModel>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<BaseModel>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteSession("assistantId", "sessionId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Message
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Message_No_VersionDate()
        {
            AssistantService service = new AssistantService("username", "password", "versionDate");
            service.VersionDate = null;

            service.Message("assistantId", "sessionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Message_No_AssistantId()
        {
            AssistantService service = new AssistantService("username", "password", "versionDate");
            service.Message(null, "sessionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Message_No_SessionId()
        {
            AssistantService service = new AssistantService("username", "password", "versionDate");
            service.Message("assistantId", null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Message_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(x =>
                {
                    throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                });

            AssistantService service = new AssistantService(client);
            service.VersionDate = "versionDate";
            service.Message("assistantId", "sessionId");
        }

        [TestMethod]
        public void Message_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<MessageResponse>();
            response.Output = new MessageOutput()
            {
                Generic = new List<DialogRuntimeResponseGeneric>()
                {
                    new DialogRuntimeResponseGeneric()
                    {
                        ResponseType = DialogRuntimeResponseGeneric.ResponseTypeEnum.TEXT,
                        Preference = DialogRuntimeResponseGeneric.PreferenceEnum.BUTTON,
                        Text = "text",
                        Time = 1,
                        Typing = true,
                        Source = "source",
                        Title = "title",
                        Description = "description",
                        Options = new List<DialogNodeOutputOptionsElement>()
                        {
                            new DialogNodeOutputOptionsElement()
                            {
                                Label = "label",
                                Value = new DialogNodeOutputOptionsElementValue()
                                {
                                    Input = new MessageInput()
                                    {
                                        MessageType = MessageInput.MessageTypeEnum.TEXT,
                                        Text = "text",
                                        Options = new MessageInputOptions()
                                        {
                                            Debug = false,
                                            Restart = false,
                                            AlternateIntents = false,
                                            ReturnContext = false
                                        },
                                        Intents = new List<RuntimeIntent>()
                                        {
                                            new RuntimeIntent()
                                            {
                                                Intent = "intent",
                                                Confidence = 0.5
                                            }
                                        },
                                        Entities = new List<RuntimeEntity>()
                                        {
                                            new RuntimeEntity()
                                            {
                                                Entity = "entity",
                                                Location = new List<long?>()
                                                {
                                                    1
                                                },
                                                Value = "value",
                                                Confidence = 0.5f,
                                                Groups = new List<CaptureGroup>()
                                                {
                                                    new CaptureGroup()
                                                    {
                                                        Group = "group",
                                                        Location = new List<long?>()
                                                        {
                                                            1
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        SuggestionId = "suggestionid"
                                    }
                                }
                            }
                        }
                    }
                },
                Intents = new List<RuntimeIntent>()
                {
                    new RuntimeIntent()
                    {
                        Intent = "intent",
                        Confidence = 0.5
                    }
                },
                Entities = new List<RuntimeEntity>()
                {
                    new RuntimeEntity()
                    {
                        Entity = "entity",
                        Location = new List<long?>()
                        {
                            1
                        },
                        Value = "value",
                        Confidence = 0.5f,
                        Groups = new List<CaptureGroup>()
                        {
                            new CaptureGroup()
                            {
                                Group = "group",
                                Location = new List<long?>()
                                {
                                    1
                                }
                            }
                        }
                    }
                },
                Actions = new List<DialogNodeAction>()
                {
                    new DialogNodeAction()
                },
                Debug = new MessageOutputDebug()
                {
                    BranchExitedReason = MessageOutputDebug.BranchExitedReasonEnum.COMPLETED,
                    NodesVisited = new List<DialogNodesVisited>()
                    {
                        new DialogNodesVisited()
                    },
                    LogMessages = new List<DialogLogMessage>()
                    {
                        new DialogLogMessage()
                    },
                    BranchExited = true
                }
            };
            response.Context = new MessageContext()
            {
                Global = new MessageContextGlobal()
                {
                    System = new MessageContextGlobalSystem()
                    {
                        Timezone = "CST",
                        UserId = "watson",
                        TurnCount = 2
                    }
                },
                Skills = new MessageContextSkills()
                {
                    
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<MessageResponse>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.VersionDate = "versionDate";

            var result = service.Message("assistantId", "sessionId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());

        }
        #endregion
    }
}
