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

namespace IBM.WatsonDeveloperCloud.Conversation.v1.UnitTests
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
                Input = new { Text = "Turn on the lights" },
                AlternateIntents = true,
                Context = new
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<DialogStack>()
                                    {
                                        new DialogStack()
                                        {
                                            DialogNode = "root"
                                        }
                                    }
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
                Input = new { Text = "Turn on the lights" },
                AlternateIntents = true,
                Context = new
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<DialogStack>()
                                    {
                                        new DialogStack()
                                        {
                                            DialogNode = "root"
                                        }
                                    }
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
                Input = new { Text = "Turn on the lights" },
                AlternateIntents = true,
                Context = new
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<DialogStack>()
                                    {
                                        new DialogStack()
                                        {
                                            DialogNode = "root"
                                        }
                                    }
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
                Input = new { Text = "Turn on the lights" },
                AlternateIntents = true,
                Context = new
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<DialogStack>()
                                    {
                                        new DialogStack()
                                        {
                                            DialogNode = "root"
                                        }
                                    }
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
                Input = new { Text = "Turn on the lights" },
                AlternateIntents = true,
                Context = new
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<DialogStack>()
                                    {
                                        new DialogStack()
                                        {
                                            DialogNode = "root"
                                        }
                                    }
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
                Input = new { Text = "Turn on the lights" },
                AlternateIntents = true,
                Context = new
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<DialogStack>()
                                    {
                                        new DialogStack()
                                        {
                                            DialogNode = "root"
                                        }
                                    }
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

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Message_WorkspaceId_MessageRequest_Cath_Exception()
        {

            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region messageRequest

            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new { Text = "Turn on the lights" },
                AlternateIntents = true,
                Context = new
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<DialogStack>()
                                    {
                                        new DialogStack()
                                        {
                                            DialogNode = "root"
                                        }
                                    }
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
                Input = new { Text = "Turn on the lights" },
                AlternateIntents = true,
                Context = new
                {
                    ConversationId = "1b7b67c0-90ed-45dc-8508-9488bc483d5b",
                    System = new SystemResponse()
                    {
                        DialogRequestCounter = 2,
                        DialogTurnCounter = 2,
                        DialogStack = new List<DialogStack>()
                                    {
                                        new DialogStack()
                                        {
                                            DialogNode = "root"
                                        }
                                    }
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
                   .Returns(x => { throw new AggregateException(new Exception()); });

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

        [TestMethod]
        public void ListWorskpaces_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            WorkspaceCollectionResponse response = new WorkspaceCollectionResponse()
            {
                Workspaces = new List<WorkspaceResponse>()
                {
                    new WorkspaceResponse()
                    {
                        Name = "Car_Dashboard",
                        Created = "2016-07-13T12:26:55.781Z",
                        Updated = "2016-11-29T21:46:38.969Z",
                        Language = "en",
                        Metadata = new object() { },
                        Description = "Cognitive Car workspace which allows multi-turn conversations to perform tasks in the car.",
                        WorkspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc"
                    },
                    new WorkspaceResponse()
                    {
                        Name = "workspace-example",
                        Created = "2016-07-11T17:06:57.089Z",
                        Updated = "2016-11-29T21:46:38.969Z",
                        Language = "en",
                        Metadata = new object() { },
                        Description = "Example workspace to try out the service",
                        WorkspaceId = "293b58fc-3c5b-4ac5-a8f4-8d52c393d875"
                    }
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WorkspaceCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListWorkspaces();

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Workspaces.Count >=1);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListWorskpaces_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            WorkspaceCollectionResponse response = new WorkspaceCollectionResponse()
            {
                Workspaces = new List<WorkspaceResponse>()
                {
                    new WorkspaceResponse()
                    {
                        Name = "Car_Dashboard",
                        Created = "2016-07-13T12:26:55.781Z",
                        Updated = "2016-11-29T21:46:38.969Z",
                        Language = "en",
                        Metadata = new object() { },
                        Description = "Cognitive Car workspace which allows multi-turn conversations to perform tasks in the car.",
                        WorkspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc"
                    },
                    new WorkspaceResponse()
                    {
                        Name = "workspace-example",
                        Created = "2016-07-11T17:06:57.089Z",
                        Updated = "2016-11-29T21:46:38.969Z",
                        Language = "en",
                        Metadata = new object() { },
                        Description = "Example workspace to try out the service",
                        WorkspaceId = "293b58fc-3c5b-4ac5-a8f4-8d52c393d875"
                    }
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WorkspaceCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var workspace = service.ListWorkspaces();
        }

        [TestMethod]
        public void CreateWorskpace_Success_With_CreateWorkspace()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region worskpace

            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = "Test SDK",
                Description = "Test Conversation SDK",
                Language = "en",
                Metadata = new object() { },
                CounterExamples = new List<CreateExample>()
               {
                   new CreateExample()
                   {
                       Text = "How to increase revenue"
                   }
               },
                DialogNodes = new List<CreateDialogNode>()
               {
                   new CreateDialogNode()
                   {
                       DialogNode = "node_2",
                       Description = null,
                       Conditions = null,
                       Parent = "node_1",
                       PreviousSibling = null,
                       Output = new DialogNodeOutput()
                       {
                           Text = "But now... Back to business"
                       },
                       Context = null,
                       Metadata = new object() { },
                       GoTo = null
                   },
                   new CreateDialogNode()
                   {
                       DialogNode = "node_1",
                       Description = null,
                       Conditions = "revenue",
                       Parent = null,
                       PreviousSibling = null,
                       Output = new DialogNodeOutput()
                       {
                           Text = "ok.. you have been putting a few pounds lately, so I recommend you go to the Green Bowl for a salad - it has 5 star rating on yelp and I recommend the cesar salad for \\$9.75. :-)"
                       },
                       Context = null,
                       Metadata = new object() { },
                       GoTo = new DialogNodeGoTo()
                       {
                           Return = true,
                           Selector = "body",
                           DialogNode = "node_2"
                       }
                   }
               },
                Entities = new List<CreateEntity>()
               {
                   new CreateEntity()
                   {
                       Entity = "Metrics",
                       Description = null,
                       Source = null,
                       OpenList = false,
                       Type = null,
                       Values = new List<CreateValue>()
                       {
                           new CreateValue()
                           {
                               Value = "Market Share",
                               Metadata = new object() { },
                               Synonyms = new List<string>() {"closing the gap", "ground", "share"}
                           },
                           new CreateValue()
                           {
                               Value = "Profit",
                               Metadata = new object() { },
                               Synonyms = new List<string>() {"bottomline", "earnings", "gain", "margin", "margins", "net", "profit", "profits", "return", "yield"}
                           },
                           new CreateValue()
                           {
                               Value = "Revenue",
                               Metadata = new object() { },
                               Synonyms = new List<string>() {"income", "sales", "topline"}
                           }
                       }
                   }
               },
                Intents = new List<CreateIntent>()
              {
                  new CreateIntent()
                  {
                      Intent = "not_happy",
                      Description = null,
                      Examples = new List<CreateExample>()
                      {
                          new CreateExample()
                          {
                              Text = "I'm not happy with my company results"
                          }
                      }
                  }
              }
            };

            #endregion

            #region response

            WorkspaceResponse response = new WorkspaceResponse()
            {
                Name = "Pizza app",
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:53:59.153Z",
                Language = "en",
                Metadata = new object() { },
                Description = "Pizza app",
                WorkspaceId = "pizza_app-e0f3"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateWorkspace>(Arg.Any<CreateWorkspace>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<WorkspaceResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateWorkspace(workspace);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateWorskpace_CreateWorkspace_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            WorkspaceResponse response = new WorkspaceResponse()
            {
                Name = "Pizza app",
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:53:59.153Z",
                Language = "en",
                Metadata = new object() { },
                Description = "Pizza app",
                WorkspaceId = "pizza_app-e0f3"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateWorkspace>(Arg.Any<CreateWorkspace>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<WorkspaceResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateWorskpace_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region worskpace

            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = "Test SDK",
                Description = "Test Conversation SDK",
                Language = "en",
                Metadata = new object() { },
                CounterExamples = new List<CreateExample>()
               {
                   new CreateExample()
                   {
                       Text = "How to increase revenue"
                   }
               },
                DialogNodes = new List<CreateDialogNode>()
               {
                   new CreateDialogNode()
                   {
                       DialogNode = "node_2",
                       Description = null,
                       Conditions = null,
                       Parent = "node_1",
                       PreviousSibling = null,
                       Output = new DialogNodeOutput()
                       {
                           Text = "But now... Back to business"
                       },
                       Context = null,
                       Metadata = new object() { },
                       GoTo = null
                   },
                   new CreateDialogNode()
                   {
                       DialogNode = "node_1",
                       Description = null,
                       Conditions = "revenue",
                       Parent = null,
                       PreviousSibling = null,
                       Output = new DialogNodeOutput()
                       {
                           Text = "ok.. you have been putting a few pounds lately, so I recommend you go to the Green Bowl for a salad - it has 5 star rating on yelp and I recommend the cesar salad for \\$9.75. :-)"
                       },
                       Context = null,
                       Metadata = new object() { },
                       GoTo = new DialogNodeGoTo()
                       {
                           Return = true,
                           Selector = "body",
                           DialogNode = "node_2"
                       }
                   }
               },
                Entities = new List<CreateEntity>()
               {
                   new CreateEntity()
                   {
                       Entity = "Metrics",
                       Description = null,
                       Source = null,
                       OpenList = false,
                       Type = null,
                       Values = new List<CreateValue>()
                       {
                           new CreateValue()
                           {
                               Value = "Market Share",
                               Metadata = new object() { },
                               Synonyms = new List<string>() {"closing the gap", "ground", "share"}
                           },
                           new CreateValue()
                           {
                               Value = "Profit",
                               Metadata = new object() { },
                               Synonyms = new List<string>() {"bottomline", "earnings", "gain", "margin", "margins", "net", "profit", "profits", "return", "yield"}
                           },
                           new CreateValue()
                           {
                               Value = "Revenue",
                               Metadata = new object() { },
                               Synonyms = new List<string>() {"income", "sales", "topline"}
                           }
                       }
                   }
               },
                Intents = new List<CreateIntent>()
              {
                  new CreateIntent()
                  {
                      Intent = "not_happy",
                      Description = null,
                      Examples = new List<CreateExample>()
                      {
                          new CreateExample()
                          {
                              Text = "I'm not happy with my company results"
                          }
                      }
                  }
              }
            };

            #endregion

            #region response

            WorkspaceResponse response = new WorkspaceResponse()
            {
                Name = "Pizza app",
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:53:59.153Z",
                Language = "en",
                Metadata = new object() { },
                Description = "Pizza app",
                WorkspaceId = "pizza_app-e0f3"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateWorkspace>(Arg.Any<CreateWorkspace>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<WorkspaceResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateWorkspace(workspace);
        }

        [TestMethod]
        public void DeleteWorkspace_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteWorkspace(workspaceId);

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWorkspace_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteWorkspace_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteWorkspace(workspaceId);
        }

        [TestMethod]
        public void GetWorkspace_Success_With_WorkspaceId()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            WorkspaceExportResponse response = new WorkspaceExportResponse()
            {
                Name = "Pizza app",
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:53:59.153Z",
                Language = "en",
                Metadata = new object() { },
                Description = "Pizza app",
                WorkspaceId = "pizza_app-e0f3",
                CounterExamples = null,
                DialogNodes = null,
                Entities = null,
                Intents = null,
                Status = "Available"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WorkspaceExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetWorkspace(workspaceId);

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetWorkspace_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            WorkspaceExportResponse response = new WorkspaceExportResponse()
            {
                Name = "Pizza app",
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:53:59.153Z",
                Language = "en",
                Metadata = new object() { },
                Description = "Pizza app",
                WorkspaceId = "pizza_app-e0f3",
                CounterExamples = null,
                DialogNodes = null,
                Entities = null,
                Intents = null,
                Status = "Available"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WorkspaceExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetWorkspace_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            WorkspaceExportResponse response = new WorkspaceExportResponse()
            {
                Name = "Pizza app",
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:53:59.153Z",
                Language = "en",
                Metadata = new object() { },
                Description = "Pizza app",
                WorkspaceId = "pizza_app-e0f3",
                CounterExamples = null,
                DialogNodes = null,
                Entities = null,
                Intents = null,
                Status = "Available"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<WorkspaceExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetWorkspace(workspaceId);
        }

        [TestMethod]
        public void GetWorkspace_Success_With_WorkspaceId_Export()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            WorkspaceExportResponse response = new WorkspaceExportResponse()
            {
                Name = "Car_Dashboard",
                Created = "2016-07-13T12:26:55.781Z",
                Updated = "2016-11-29T21:46:38.969Z",
                Language = "en",
                Metadata = new object() { },
                Description = "Cognitive Car workspace which allows multi-turn conversations to perform tasks in the car.",
                WorkspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc",
                CounterExamples = null,
                DialogNodes = new List<DialogNodeResponse>()
                {
                    new DialogNodeResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Conditions = "$reprompt",
                        Description = null,
                        Context = null,
                        DialogNode = "node_4",
                        GoTo = new DialogNodeGoTo()
                        {
                            DialogNode = "node_3",
                            Return = true,
                            Selector = "user_input"
                        },
                        Metadata = new object() { },
                        Output = new DialogNodeOutput()
                        {
                            Text = "Which option would you like? You can say first, third, nearest and so on.."
                        },
                        Parent = "node_1",
                        PreviousSibling = "2"
                    }
                },
                Entities = new List<EntityExportResponse>()
                {
                    new EntityExportResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Description = null,
                        Entity = "amenity",
                        OpenList = false,
                        Source = null,
                        Type = null,
                        Updated = "2016-07-13T12:26:55.781Z",
                        Values = new List<ValueExportResponse>()
                        {
                            new ValueExportResponse()
                            {
                                Created = "2016-07-13T12:26:55.781Z",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"fuel", "fuel station", "fuel stations", "gas", "gas station", "gas stations", "pump", "pumps" },
                                Updated = "2016-07-13T12:26:55.781Z",
                                Value = "gas"
                            }
                        }
                    }
                },
                Intents = new List<IntentExportResponse>()
                {
                    new IntentExportResponse()
                    {
                        Intent = "capabilities",
                        Created = "2016-07-13T12:26:55.781Z",
                        Examples = new List<ExampleResponse>()
                        {
                            new ExampleResponse()
                            {
                                Text = "can you turn on the radio",
                                Created = "2016-07-13T12:26:55.781Z",
                                Updated = "2016-11-29T18:07:41.876Z"
                            }
                        },
                        Description = null,
                        Updated = "2016-07-13T12:26:55.781Z"
                    }
                },
                Status = "Available"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WorkspaceExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetWorkspace(workspaceId, true);

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void UpdateWorkspace_Sucess_With_WorkspaceId_UpdateWorkspace()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region workspace

            UpdateWorkspace workspace = new UpdateWorkspace()
            {
                Name = "",
                Metadata = new object() { },
                Language = "en",
                Description = null,
                CounterExamples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "How to increase revenue"
                    }
                },
                DialogNodes = new List<CreateDialogNode>()
                {
                    new CreateDialogNode()
                    {
                        DialogNode = "node_2",
                        Description = null,
                        Conditions = null,
                        Parent = "node_1",
                        PreviousSibling = null,
                        Output = new DialogNodeOutput()
                        {
                            Text = "But now... Back to business"
                        },
                        Context = null,
                        Metadata = new object() { },
                        GoTo = null
                    },
                    new CreateDialogNode()
                    {
                        DialogNode = "node_1",
                        Description = null,
                        Conditions = "revenue",
                        Parent = null,
                        PreviousSibling = null,
                        Output = new DialogNodeOutput()
                        {
                            Text = "ok.. you have been putting a few pounds lately, so I recommend you go to the Green Bowl for a salad - it has 5 star rating on yelp and I recommend the cesar salad for \\$9.75. :-)"
                        },
                        Context = null,
                        Metadata = new object() { },
                        GoTo = new DialogNodeGoTo()
                        {
                            Return = true,
                            Selector = "body",
                            DialogNode = "node_2"
                        }
                    }
                },
                Entities = new List<CreateEntity>()
                {
                    new CreateEntity()
                    {
                        Entity = "Metrics",
                        Description = null,
                        Source = null,
                        OpenList = false,
                        Type = null,
                        Values = new List<CreateValue>()
                        {
                            new CreateValue()
                            {
                                Value = "Market Share",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"closing the gap", "ground", "share"}
                            },
                            new CreateValue()
                            {
                                Value = "Profit",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"bottomline", "earnings", "gain", "margin", "margins", "net", "profit", "profits", "return", "yield"}
                            },
                            new CreateValue()
                            {
                                Value = "Revenue",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"income", "sales", "topline"}
                            }
                        }
                    }
                },
                Intents = new List<CreateIntent>()
                {
                    new CreateIntent()
                    {
                        Intent = "not_happy",
                        Description = null,
                        Examples = new List<CreateExample>()
                        {
                            new CreateExample()
                            {
                                Text = "I'm not happy with my company results"
                            }
                        }
                    }
                }
            };

            #endregion

            #region response

            WorkspaceResponse response = new WorkspaceResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:54:59.153Z",
                Name = "Pizza app",
                Description = "Pizza app",
                Language = "en",
                Metadata = new object() { },
                WorkspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateWorkspace>(Arg.Any<UpdateWorkspace>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<WorkspaceResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateWorkspace(workspaceId, workspace);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWorkspace_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region workspace

            UpdateWorkspace workspace = new UpdateWorkspace()
            {
                Name = "",
                Metadata = new object() { },
                Language = "en",
                Description = null,
                CounterExamples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "How to increase revenue"
                    }
                },
                DialogNodes = new List<CreateDialogNode>()
                {
                    new CreateDialogNode()
                    {
                        DialogNode = "node_2",
                        Description = null,
                        Conditions = null,
                        Parent = "node_1",
                        PreviousSibling = null,
                        Output = new DialogNodeOutput()
                        {
                            Text = "But now... Back to business"
                        },
                        Context = null,
                        Metadata = new object() { },
                        GoTo = null
                    },
                    new CreateDialogNode()
                    {
                        DialogNode = "node_1",
                        Description = null,
                        Conditions = "revenue",
                        Parent = null,
                        PreviousSibling = null,
                        Output = new DialogNodeOutput()
                        {
                            Text = "ok.. you have been putting a few pounds lately, so I recommend you go to the Green Bowl for a salad - it has 5 star rating on yelp and I recommend the cesar salad for \\$9.75. :-)"
                        },
                        Context = null,
                        Metadata = new object() { },
                        GoTo = new DialogNodeGoTo()
                        {
                            Return = true,
                            Selector = "body",
                            DialogNode = "node_2"
                        }
                    }
                },
                Entities = new List<CreateEntity>()
                {
                    new CreateEntity()
                    {
                        Entity = "Metrics",
                        Description = null,
                        Source = null,
                        OpenList = false,
                        Type = null,
                        Values = new List<CreateValue>()
                        {
                            new CreateValue()
                            {
                                Value = "Market Share",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"closing the gap", "ground", "share"}
                            },
                            new CreateValue()
                            {
                                Value = "Profit",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"bottomline", "earnings", "gain", "margin", "margins", "net", "profit", "profits", "return", "yield"}
                            },
                            new CreateValue()
                            {
                                Value = "Revenue",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"income", "sales", "topline"}
                            }
                        }
                    }
                },
                Intents = new List<CreateIntent>()
                {
                    new CreateIntent()
                    {
                        Intent = "not_happy",
                        Description = null,
                        Examples = new List<CreateExample>()
                        {
                            new CreateExample()
                            {
                                Text = "I'm not happy with my company results"
                            }
                        }
                    }
                }
            };

            #endregion

            #region response

            WorkspaceResponse response = new WorkspaceResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:54:59.153Z",
                Name = "Pizza app",
                Description = "Pizza app",
                Language = "en",
                Metadata = new object() { },
                WorkspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateWorkspace>(Arg.Any<UpdateWorkspace>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<WorkspaceResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateWorkspace(null, workspace);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWorkspace_UpdateWorkspace_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            WorkspaceResponse response = new WorkspaceResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:54:59.153Z",
                Name = "Pizza app",
                Description = "Pizza app",
                Language = "en",
                Metadata = new object() { },
                WorkspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateWorkspace>(Arg.Any<UpdateWorkspace>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<WorkspaceResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateWorkspace(workspaceId, null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateWorkspace_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region workspace

            UpdateWorkspace workspace = new UpdateWorkspace()
            {
                Name = "",
                Metadata = new object() { },
                Language = "en",
                Description = null,
                CounterExamples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "How to increase revenue"
                    }
                },
                DialogNodes = new List<CreateDialogNode>()
                {
                    new CreateDialogNode()
                    {
                        DialogNode = "node_2",
                        Description = null,
                        Conditions = null,
                        Parent = "node_1",
                        PreviousSibling = null,
                        Output = new DialogNodeOutput()
                        {
                            Text = "But now... Back to business"
                        },
                        Context = null,
                        Metadata = new object() { },
                        GoTo = null
                    },
                    new CreateDialogNode()
                    {
                        DialogNode = "node_1",
                        Description = null,
                        Conditions = "revenue",
                        Parent = null,
                        PreviousSibling = null,
                        Output = new DialogNodeOutput()
                        {
                            Text = "ok.. you have been putting a few pounds lately, so I recommend you go to the Green Bowl for a salad - it has 5 star rating on yelp and I recommend the cesar salad for \\$9.75. :-)"
                        },
                        Context = null,
                        Metadata = new object() { },
                        GoTo = new DialogNodeGoTo()
                        {
                            Return = true,
                            Selector = "body",
                            DialogNode = "node_2"
                        }
                    }
                },
                Entities = new List<CreateEntity>()
                {
                    new CreateEntity()
                    {
                        Entity = "Metrics",
                        Description = null,
                        Source = null,
                        OpenList = false,
                        Type = null,
                        Values = new List<CreateValue>()
                        {
                            new CreateValue()
                            {
                                Value = "Market Share",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"closing the gap", "ground", "share"}
                            },
                            new CreateValue()
                            {
                                Value = "Profit",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"bottomline", "earnings", "gain", "margin", "margins", "net", "profit", "profits", "return", "yield"}
                            },
                            new CreateValue()
                            {
                                Value = "Revenue",
                                Metadata = new object() { },
                                Synonyms = new List<string>() {"income", "sales", "topline"}
                            }
                        }
                    }
                },
                Intents = new List<CreateIntent>()
                {
                    new CreateIntent()
                    {
                        Intent = "not_happy",
                        Description = null,
                        Examples = new List<CreateExample>()
                        {
                            new CreateExample()
                            {
                                Text = "I'm not happy with my company results"
                            }
                        }
                    }
                }
            };

            #endregion

            #region response

            WorkspaceResponse response = new WorkspaceResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-06T23:54:59.153Z",
                Name = "Pizza app",
                Description = "Pizza app",
                Language = "en",
                Metadata = new object() { },
                WorkspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateWorkspace>(Arg.Any<UpdateWorkspace>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<WorkspaceResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateWorkspace(workspaceId, workspace);
        }

        [TestMethod]
        public void ListIntents_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentCollectionResponse response = new IntentCollectionResponse()
            {
                Intents = new List<IntentExportResponse>()
                {
                    new IntentExportResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Description = null,
                        Intent = "pizza_order",
                        Updated = "2016-07-13T12:26:55.781Z",
                        Examples = new List<ExampleResponse>()
                        {
                            new ExampleResponse()
                            {
                                Created = "2016-07-13T12:26:55.781Z",
                                Text = "then bye",
                                Updated = "2016-11-29T18:07:41.876Z"
                            }
                        }
                    }
                },
                Pagination = new PaginationResponse()
                {
                    Matched = 0,
                    NextUrl = "/v1/workspaces/pizza_app-e0f3/intents?cursor=base64=&version=2017-12-18&page_limit=1",
                    RefreshUrl = "/v1/workspaces/pizza_app-e0f3/intents?version=2017-12-18&page_limit=1",
                    Total = 0
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<IntentCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListIntents(workspaceId);

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Intents.Count >= 1);
        }

        [TestMethod]
        public void ListIntents_Success_With_Export()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentCollectionResponse response = new IntentCollectionResponse()
            {
                Intents = new List<IntentExportResponse>()
                {
                    new IntentExportResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Description = null,
                        Intent = "pizza_order",
                        Updated = "2016-07-13T12:26:55.781Z",
                        Examples = new List<ExampleResponse>()
                        {
                            new ExampleResponse()
                            {
                                Created = "2016-07-13T12:26:55.781Z",
                                Text = "then bye",
                                Updated = "2016-11-29T18:07:41.876Z"
                            }
                        }
                    }
                },
                Pagination = new PaginationResponse()
                {
                    Matched = 0,
                    NextUrl = "/v1/workspaces/pizza_app-e0f3/intents?cursor=base64=&version=2017-12-18&page_limit=1",
                    RefreshUrl = "/v1/workspaces/pizza_app-e0f3/intents?version=2017-12-18&page_limit=1",
                    Total = 0
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<IntentCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListIntents(workspaceId, true);

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Intents.Count >= 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListIntents_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            IntentCollectionResponse response = new IntentCollectionResponse()
            {
                Intents = new List<IntentExportResponse>()
                {
                    new IntentExportResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Description = null,
                        Intent = "pizza_order",
                        Updated = "2016-07-13T12:26:55.781Z",
                        Examples = new List<ExampleResponse>()
                        {
                            new ExampleResponse()
                            {
                                Created = "2016-07-13T12:26:55.781Z",
                                Text = "then bye",
                                Updated = "2016-11-29T18:07:41.876Z"
                            }
                        }
                    }
                },
                Pagination = new PaginationResponse()
                {
                    Matched = 0,
                    NextUrl = "/v1/workspaces/pizza_app-e0f3/intents?cursor=base64=&version=2017-12-18&page_limit=1",
                    RefreshUrl = "/v1/workspaces/pizza_app-e0f3/intents?version=2017-12-18&page_limit=1",
                    Total = 0
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<IntentCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListIntents(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListIntents_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentCollectionResponse response = new IntentCollectionResponse()
            {
                Intents = new List<IntentExportResponse>()
                {
                    new IntentExportResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Description = null,
                        Intent = "pizza_order",
                        Updated = "2016-07-13T12:26:55.781Z",
                        Examples = new List<ExampleResponse>()
                        {
                            new ExampleResponse()
                            {
                                Created = "2016-07-13T12:26:55.781Z",
                                Text = "then bye",
                                Updated = "2016-11-29T18:07:41.876Z"
                            }
                        }
                    }
                },
                Pagination = new PaginationResponse()
                {
                    Matched = 0,
                    NextUrl = "/v1/workspaces/pizza_app-e0f3/intents?cursor=base64=&version=2017-12-18&page_limit=1",
                    RefreshUrl = "/v1/workspaces/pizza_app-e0f3/intents?version=2017-12-18&page_limit=1",
                    Total = 0
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });


            request.As<IntentCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListIntents(workspaceId);
        }

        [TestMethod]
        public void CreateIntent_Success_With_WorkspaceId_CreateIntent()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Intent = "pizza_order"
            };

            #endregion

            #region createIntent

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = "Test_SDK_NET",
                Description = null,
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "Test Create Example"
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateIntent>(Arg.Any<CreateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<IntentResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateIntent(workspaceId, createIntent);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateIntent_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Intent = "pizza_order"
            };

            #endregion

            #region createIntent

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = "Test_SDK_NET",
                Description = null,
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "Test Create Example"
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateIntent>(Arg.Any<CreateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<IntentResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateIntent(null, createIntent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateIntent_CreateIntent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Intent = "pizza_order"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateIntent>(Arg.Any<CreateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<IntentResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateIntent(workspaceId, null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateIntent_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Intent = "pizza_order"
            };

            #endregion

            #region createIntent

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = "Test_SDK_NET",
                Description = null,
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "Test Create Example"
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateIntent>(Arg.Any<CreateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<IntentResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateIntent(workspaceId, createIntent);
        }

        [TestMethod]
        public void DeleteIntent_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteIntent(workspaceId, "pizza_order");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteIntent_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteIntent(null, "pizza_order");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteIntent_Intent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteIntent(workspaceId, null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteIntent_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteIntent(workspaceId, "pizza_order");
        }

        [TestMethod]
        public void GetIntent_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentExportResponse response = new IntentExportResponse()
            {
                Intent = "pizza_order",
                Created = "2015-12-06T23:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Updated = "2015-12-07T18:53:59.153Z",
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Text = "then bye",
                        Updated = "2016-11-29T18:07:41.876Z"
                    }
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<IntentExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetIntent(workspaceId, "pizza_order");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void GetIntent_Success_With_Export()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentExportResponse response = new IntentExportResponse()
            {
                Intent = "pizza_order",
                Created = "2015-12-06T23:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Updated = "2015-12-07T18:53:59.153Z",
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Text = "then bye",
                        Updated = "2016-11-29T18:07:41.876Z"
                    }
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<IntentExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetIntent(workspaceId, "pizza_order", true);

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetIntent_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            IntentExportResponse response = new IntentExportResponse()
            {
                Intent = "pizza_order",
                Created = "2015-12-06T23:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Updated = "2015-12-07T18:53:59.153Z",
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Text = "then bye",
                        Updated = "2016-11-29T18:07:41.876Z"
                    }
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<IntentExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetIntent(null, "pizza_order");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetIntent_Intent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentExportResponse response = new IntentExportResponse()
            {
                Intent = "pizza_order",
                Created = "2015-12-06T23:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Updated = "2015-12-07T18:53:59.153Z",
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Text = "then bye",
                        Updated = "2016-11-29T18:07:41.876Z"
                    }
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<IntentExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetIntent(workspaceId, null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetIntent_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentExportResponse response = new IntentExportResponse()
            {
                Intent = "pizza_order",
                Created = "2015-12-06T23:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Updated = "2015-12-07T18:53:59.153Z",
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-13T12:26:55.781Z",
                        Text = "then bye",
                        Updated = "2016-11-29T18:07:41.876Z"
                    }
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<IntentExportResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetIntent(workspaceId, "pizza_order");
        }

        [TestMethod]
        public void UpdateIntent_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "Test weather",
                Intent = "weather"
            };

            #endregion

            #region updateIntent

            UpdateIntent updateIntent = new UpdateIntent()
            {
                Intent = "weather",
                Description = "Test Description",
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "Test Create Example"
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateIntent>(Arg.Any<UpdateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<IntentResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateIntent(workspaceId, "weather", updateIntent);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Intent = "pizza_order"
            };

            #endregion

            #region updateIntent

            UpdateIntent updateIntent = new UpdateIntent()
            {
                Intent = "pizza_order",
                Description = null,
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "Update Intent"
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateIntent>(Arg.Any<UpdateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<IntentResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateIntent(null, "pizza_order", updateIntent);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_Intent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Intent = "pizza_order"
            };

            #endregion

            #region updateIntent

            UpdateIntent updateIntent = new UpdateIntent()
            {
                Intent = "pizza_order",
                Description = null,
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "Update Intent"
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateIntent>(Arg.Any<UpdateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<IntentResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateIntent(workspaceId, null, updateIntent);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_UpdateIntent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Intent = "pizza_order"
            };

            #endregion

            #region updateIntent

            UpdateIntent updateIntent = new UpdateIntent()
            {
                Intent = "pizza_order",
                Description = null,
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "Update Intent"
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateIntent>(Arg.Any<UpdateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<IntentResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateIntent(workspaceId, "pizza_order", null);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateIntent_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            IntentResponse response = new IntentResponse()
            {
                Created = "2015-12-06T23:53:59.153Z",
                Updated = "2015-12-07T18:53:59.153Z",
                Description = "User wants to start a new pizza order",
                Intent = "pizza_order"
            };

            #endregion

            #region updateIntent

            UpdateIntent updateIntent = new UpdateIntent()
            {
                Intent = "pizza_order",
                Description = null,
                Examples = new List<CreateExample>()
                {
                    new CreateExample()
                    {
                        Text = "Update Intent"
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateIntent>(Arg.Any<UpdateIntent>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<IntentResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateIntent(workspaceId, "pizza_order", updateIntent);
        }

        [TestMethod]
        public void ListExamples_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleCollectionResponse response = new ExampleCollectionResponse()
            {
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-11T16:39:01.774Z?",
                        Text = "Can I order a pizza?",
                        Updated = "2015-12-07T18:53:59.153Z"
                    }
                },
                Pagination = new PaginationResponse()
                {
                    Matched = 0,
                    NextUrl = "/v1/workspaces/pizza_app-e0f3/intents/order/examples?cursor=base64=&version=2017-12-18&page_limit=2",
                    RefreshUrl = "/v1/workspaces/pizza_app-e0f3/intents/order/examples?version=2017-12-18&page_limit=2",
                    Total = 0
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<ExampleCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListExamples(workspaceId, "pizza_order");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Examples.Count >= 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListExamples_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            ExampleCollectionResponse response = new ExampleCollectionResponse()
            {
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-11T16:39:01.774Z?",
                        Text = "Can I order a pizza?",
                        Updated = "2015-12-07T18:53:59.153Z"
                    }
                },
                Pagination = new PaginationResponse()
                {
                    Matched = 0,
                    NextUrl = "/v1/workspaces/pizza_app-e0f3/intents/order/examples?cursor=base64=&version=2017-12-18&page_limit=2",
                    RefreshUrl = "/v1/workspaces/pizza_app-e0f3/intents/order/examples?version=2017-12-18&page_limit=2",
                    Total = 0
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<ExampleCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListExamples(null, "pizza_order");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListExamples_Intent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleCollectionResponse response = new ExampleCollectionResponse()
            {
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-11T16:39:01.774Z?",
                        Text = "Can I order a pizza?",
                        Updated = "2015-12-07T18:53:59.153Z"
                    }
                },
                Pagination = new PaginationResponse()
                {
                    Matched = 0,
                    NextUrl = "/v1/workspaces/pizza_app-e0f3/intents/order/examples?cursor=base64=&version=2017-12-18&page_limit=2",
                    RefreshUrl = "/v1/workspaces/pizza_app-e0f3/intents/order/examples?version=2017-12-18&page_limit=2",
                    Total = 0
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<ExampleCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListExamples(workspaceId, null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListExamples_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleCollectionResponse response = new ExampleCollectionResponse()
            {
                Examples = new List<ExampleResponse>()
                {
                    new ExampleResponse()
                    {
                        Created = "2016-07-11T16:39:01.774Z?",
                        Text = "Can I order a pizza?",
                        Updated = "2015-12-07T18:53:59.153Z"
                    }
                },
                Pagination = new PaginationResponse()
                {
                    Matched = 0,
                    NextUrl = "/v1/workspaces/pizza_app-e0f3/intents/order/examples?cursor=base64=&version=2017-12-18&page_limit=2",
                    RefreshUrl = "/v1/workspaces/pizza_app-e0f3/intents/order/examples?version=2017-12-18&page_limit=2",
                    Total = 0
                }
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<ExampleCollectionResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.ListExamples(workspaceId, "pizza_order");
        }

        [TestMethod]
        public void CreateExample_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region createIntent

            CreateExample createExample = new CreateExample()
            {
                Text = "Bye."
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateExample>(Arg.Any<CreateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateExample(workspaceId, "pizza_order", createExample);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region createIntent

            CreateExample createExample = new CreateExample()
            {
                Text = "Bye."
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateExample>(Arg.Any<CreateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateExample(null, "pizza_order", createExample);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_Intent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region createIntent

            CreateExample createExample = new CreateExample()
            {
                Text = "Bye."
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateExample>(Arg.Any<CreateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateExample(workspaceId, null, createExample);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_CreateExample_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region createIntent

            CreateExample createExample = new CreateExample()
            {
                Text = "Bye."
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateExample>(Arg.Any<CreateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateExample(workspaceId, "pizza_order", null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateExample_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region createIntent

            CreateExample createExample = new CreateExample()
            {
                Text = "Bye."
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBody<CreateExample>(Arg.Any<CreateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.CreateExample(workspaceId, "pizza_order", createExample);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void DeleteExample_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteExample(workspaceId, "pizza_order", "Unit Test");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteExample(null, "pizza_order", "Unit Test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_Intent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteExample(workspaceId, null, "Unit Test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_Text_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteExample(workspaceId, "pizza_order", null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteExample_Catch_Expcetion()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";
            object response = new object() { };

            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<object>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.DeleteExample(workspaceId, "pizza_order", "Unit Test");
        }

        [TestMethod]
        public void GetExample_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetExample(workspaceId, "pizza_order", "Unit Test");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetExample(null, "pizza_order", "Unit Test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_Intent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetExample(workspaceId, null, "Unit Test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_Text_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetExample(workspaceId, "pizza_order", null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetExample_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<ExampleResponse>()
                   .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.GetExample(workspaceId, "pizza_order", "Unit Test");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void UpdateExample_Success()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region updateExample

            UpdateExample updateExample = new UpdateExample()
            {
                Text = "Update Test"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateExample>(Arg.Any<UpdateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateExample(workspaceId, "pizza_order", "Unit Test", updateExample);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_WorkspaceId_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region updateExample

            UpdateExample updateExample = new UpdateExample()
            {
                Text = "Update Test"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateExample>(Arg.Any<UpdateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateExample(null, "pizza_order", "Unit Test", updateExample);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_Intent_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region updateExample

            UpdateExample updateExample = new UpdateExample()
            {
                Text = "Update Test"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateExample>(Arg.Any<UpdateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateExample(workspaceId, null, "Unit Test", updateExample);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_Text_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region updateExample

            UpdateExample updateExample = new UpdateExample()
            {
                Text = "Update Test"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateExample>(Arg.Any<UpdateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateExample(workspaceId, "pizza_order", null, updateExample);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_UpdateExample_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region updateExample

            UpdateExample updateExample = new UpdateExample()
            {
                Text = "Update Test"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateExample>(Arg.Any<UpdateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ExampleResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateExample(workspaceId, "pizza_order", "Unit Test", null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateExample_Catch_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();
            string workspaceId = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

            #region response

            ExampleResponse response = new ExampleResponse()
            {
                Created = "2016-07-13T12:26:55.781Z",
                Text = "then bye",
                Updated = "2016-11-29T18:07:41.876Z"
            };

            #endregion

            #region updateExample

            UpdateExample updateExample = new UpdateExample()
            {
                Text = "Update Test"
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                       .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                       .Returns(request);

            request.WithBody<UpdateExample>(Arg.Any<UpdateExample>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<ExampleResponse>()
                       .Returns(Task.FromResult(response));

            ConversationService service = new ConversationService(client);
            var result = service.UpdateExample(workspaceId, "pizza_order", "Unit Test", updateExample);
        }
    }
}



