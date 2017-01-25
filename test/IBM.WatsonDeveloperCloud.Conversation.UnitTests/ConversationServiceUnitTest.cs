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

using IBM.WatsonDeveloperCloud.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Conversation.v1;
using System.Net.Http.Headers;
using System;

namespace IBM.WatsonDeveloperCloud.Conversation.UnitTests
{
    [TestClass]
    public class ConversationServiceUnitTest
    {
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            return client;
        }

        [TestMethod]
        public void Message_Success_With_WorkspaceId()
        {

            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region messageRequest

            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<string>(new string[] { "dialog_id_1", "dialog_id_2", "dialog_id_3" })
                    }
                },
                Entities = new List<EntityResponse>()
                {
                    new EntityResponse()
                    {
                        Entity = "appliance",
                        Value = "light",
                        Location = new List<int>(new int[] { 12, 18 })
                    }
                },
                Intents = new List<Intent>()
                {
                    new Intent()
                    {
                        Confidence = 0.9743563172970104,
                        IntentDescription = "turn_on"
                    }
                },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                    {
                        new LogMessageResponse()
                        {
                            Level = "warn",
                            Msg = "No dialog node matched for the input at a root level!"
                        }
                    },
                    NodesVisited = new List<string>(new string[] { "node_1_1467232431348", "node_2_1467232480480", "node_4_1467232602708" }),
                    Text = new List<string>(new string[] { "Ok. Turning on the light", "Ok. Turning off the light" })
                }

            };

            #endregion messageRequest

            #region response

            MessageResponse response = new MessageResponse()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<string>(new string[] { "dialog_id_1", "dialog_id_2", "dialog_id_3" })
                    }
                },
                Entities = new List<EntityResponse>()
                            {
                                new EntityResponse()
                                {
                                    Entity = "appliance",
                                    Value = "light",
                                    Location = new List<int>(new int[] { 12, 18 })
                                }
                            },
                Intents = new List<Intent>()
                            {
                                new Intent()
                                {
                                    Confidence = 0.9743563172970104,
                                    IntentDescription = "turn_on"
                                }
                            },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = "warn",
                                        Msg = "No dialog node matched for the input at a root level!"
                                    }
                                },
                    NodesVisited = new List<string>(new string[] { "node_1_1467232431348", "node_2_1467232480480", "node_4_1467232602708" }),
                    Text = new List<string>(new string[] { "Ok. Turning on the light", "Ok. Turning off the light" })
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<MessageRequest>(Arg.Any<MessageRequest>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<MessageResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var message = service.Message("workspace_Id", null);

            Assert.IsNotNull(message);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(message.Intents.Count >= 1);

        }

        [TestMethod]
        public void Message_Success_With_WorkspaceId_MessageRequest()
        {

            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region messageRequest

            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<string>(new string[] { "dialog_id_1", "dialog_id_2", "dialog_id_3" })
                    }
                },
                Entities = new List<EntityResponse>()
                {
                    new EntityResponse()
                    {
                        Entity = "appliance",
                        Value = "light",
                        Location = new List<int>(new int[] { 12, 18 })
                    }
                },
                Intents = new List<Intent>()
                {
                    new Intent()
                    {
                        Confidence = 0.9743563172970104,
                        IntentDescription = "turn_on"
                    }
                },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                    {
                        new LogMessageResponse()
                        {
                            Level = "warn",
                            Msg = "No dialog node matched for the input at a root level!"
                        }
                    },
                    NodesVisited = new List<string>(new string[] { "node_1_1467232431348", "node_2_1467232480480", "node_4_1467232602708" }),
                    Text = new List<string>(new string[] { "Ok. Turning on the light", "Ok. Turning off the light" })
                }

            };

            #endregion messageRequest

            #region response

            MessageResponse response = new MessageResponse()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<string>(new string[] { "dialog_id_1", "dialog_id_2", "dialog_id_3" })
                    }
                },
                Entities = new List<EntityResponse>()
                            {
                                new EntityResponse()
                                {
                                    Entity = "appliance",
                                    Value = "light",
                                    Location = new List<int>(new int[] { 12, 18 })
                                }
                            },
                Intents = new List<Intent>()
                            {
                                new Intent()
                                {
                                    Confidence = 0.9743563172970104,
                                    IntentDescription = "turn_on"
                                }
                            },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = "warn",
                                        Msg = "No dialog node matched for the input at a root level!"
                                    }
                                },
                    NodesVisited = new List<string>(new string[] { "node_1_1467232431348", "node_2_1467232480480", "node_4_1467232602708" }),
                    Text = new List<string>(new string[] { "Ok. Turning on the light", "Ok. Turning off the light" })
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<MessageRequest>(Arg.Any<MessageRequest>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<MessageResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var message = service.Message("workspace_Id", messageRequest);

            Assert.IsNotNull(message);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(message.Intents.Count >= 1);

        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Message_WorkspaceId_Null()
        {

            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region messageRequest

            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<string>(new string[] { "dialog_id_1", "dialog_id_2", "dialog_id_3" })
                    }
                },
                Entities = new List<EntityResponse>()
                {
                    new EntityResponse()
                    {
                        Entity = "appliance",
                        Value = "light",
                        Location = new List<int>(new int[] { 12, 18 })
                    }
                },
                Intents = new List<Intent>()
                {
                    new Intent()
                    {
                        Confidence = 0.9743563172970104,
                        IntentDescription = "turn_on"
                    }
                },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                    {
                        new LogMessageResponse()
                        {
                            Level = "warn",
                            Msg = "No dialog node matched for the input at a root level!"
                        }
                    },
                    NodesVisited = new List<string>(new string[] { "node_1_1467232431348", "node_2_1467232480480", "node_4_1467232602708" }),
                    Text = new List<string>(new string[] { "Ok. Turning on the light", "Ok. Turning off the light" })
                }

            };

            #endregion messageRequest

            #region response

            MessageResponse response = new MessageResponse()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<string>(new string[] { "dialog_id_1", "dialog_id_2", "dialog_id_3" })
                    }
                },
                Entities = new List<EntityResponse>()
                            {
                                new EntityResponse()
                                {
                                    Entity = "appliance",
                                    Value = "light",
                                    Location = new List<int>(new int[] { 12, 18 })
                                }
                            },
                Intents = new List<Intent>()
                            {
                                new Intent()
                                {
                                    Confidence = 0.9743563172970104,
                                    IntentDescription = "turn_on"
                                }
                            },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = "warn",
                                        Msg = "No dialog node matched for the input at a root level!"
                                    }
                                },
                    NodesVisited = new List<string>(new string[] { "node_1_1467232431348", "node_2_1467232480480", "node_4_1467232602708" }),
                    Text = new List<string>(new string[] { "Ok. Turning on the light", "Ok. Turning off the light" })
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<MessageRequest>(Arg.Any<MessageRequest>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<MessageResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var message = service.Message(null, messageRequest);

        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void Message_WorkspaceId_MessageRequest_Cath_Exception()
        {

            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region messageRequest

            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<string>(new string[] { "dialog_id_1", "dialog_id_2", "dialog_id_3" })
                    }
                },
                Entities = new List<EntityResponse>()
                {
                    new EntityResponse()
                    {
                        Entity = "appliance",
                        Value = "light",
                        Location = new List<int>(new int[] { 12, 18 })
                    }
                },
                Intents = new List<Intent>()
                {
                    new Intent()
                    {
                        Confidence = 0.9743563172970104,
                        IntentDescription = "turn_on"
                    }
                },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                    {
                        new LogMessageResponse()
                        {
                            Level = "warn",
                            Msg = "No dialog node matched for the input at a root level!"
                        }
                    },
                    NodesVisited = new List<string>(new string[] { "node_1_1467232431348", "node_2_1467232480480", "node_4_1467232602708" }),
                    Text = new List<string>(new string[] { "Ok. Turning on the light", "Ok. Turning off the light" })
                }

            };

            #endregion messageRequest

            #region response

            MessageResponse response = new MessageResponse()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<string>(new string[] { "dialog_id_1", "dialog_id_2", "dialog_id_3" })
                    }
                },
                Entities = new List<EntityResponse>()
                            {
                                new EntityResponse()
                                {
                                    Entity = "appliance",
                                    Value = "light",
                                    Location = new List<int>(new int[] { 12, 18 })
                                }
                            },
                Intents = new List<Intent>()
                            {
                                new Intent()
                                {
                                    Confidence = 0.9743563172970104,
                                    IntentDescription = "turn_on"
                                }
                            },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessageResponse>()
                                {
                                    new LogMessageResponse()
                                    {
                                        Level = "warn",
                                        Msg = "No dialog node matched for the input at a root level!"
                                    }
                                },
                    NodesVisited = new List<string>(new string[] { "node_1_1467232431348", "node_2_1467232480480", "node_4_1467232602708" }),
                    Text = new List<string>(new string[] { "Ok. Turning on the light", "Ok. Turning off the light" })
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<MessageRequest>(Arg.Any<MessageRequest>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new Exception(); });

            request.As<MessageResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var message = service.Message("workspace_Id", messageRequest);

        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            ConversationService service =
                new ConversationService(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            ConversationService service =
                new ConversationService(null, "pass");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_PassWord_Null()
        {
            ConversationService service =
                new ConversationService("username", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            ConversationService service =
                new ConversationService("username", "password");
        }

    }
}



