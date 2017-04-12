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

using IBM.WatsonDeveloperCloud.Conversation.v1;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.WatsonDeveloperCloud.Conversation.IntegrationTests
{
    [TestClass]
    public class ConversationServiceIntegrationTest
    {   
        private string _userName;
        private string _password;
        private string _endpoint;
        private string _workspace;
        private string _tempWorkspace;

        [TestInitialize]
        public void Setup()
        {
            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
            File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            var conversationCredential =
            vcapServices["conversation"][0]["credentials"];

            _endpoint = conversationCredential["url"].Value<string>();
            _userName = conversationCredential["username"].Value<string>();
            _password = conversationCredential["password"].Value<string>();
            _workspace = conversationCredential["workspace"].Value<string>();

        }

        [TestMethod]
        public void ListWorkspaces()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.ListWorkspaces();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Workspaces.Count >= 1);
        }

        [TestMethod]
        public void GetWorkspace()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.GetWorkspace(_workspace);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void GetWorkspace_With_Export()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.GetWorkspace(_workspace, true);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void CreateWorkspace()
        {
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

            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.CreateWorkspace(workspace);
            _tempWorkspace = results.WorkspaceId;

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void UpdateWorkspace()
        {
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

            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.UpdateWorkspace(_tempWorkspace, workspace);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void DeleteWorkspace()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.DeleteWorkspace(_tempWorkspace);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void Message()
        {
            #region messageRequest

            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new { Text = "Turn on the lights" }
            };

            #endregion messageRequest

            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.Message(_workspace, messageRequest);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Intents.Count >= 1);
        }

        [TestMethod]
        public void ListIntents()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.ListIntents(_workspace);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Intents.Count >= 1);
        }

        [TestMethod]
        public void ListIntents_With_Export()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.ListIntents(_workspace, true);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Intents.Count >= 1);
        }

        [TestMethod]
        public void CreateIntent()
        {
            #region CreateIntent

            CreateIntent request = new CreateIntent()
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

            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.CreateIntent(_workspace, request);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void UpdateIntent()
        {
            #region UpdateIntent

            UpdateIntent request = new UpdateIntent()
            {
                Intent = "Test_SDK_NET",
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

            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.UpdateIntent(_workspace, "Test_SDK_NET", request);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void GetIntent()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.GetIntent(_workspace, "Test_SDK_NET");

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void GetIntent_With_Export()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.GetIntent(_workspace, "Test_SDK_NET", true);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void DeleteIntent()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.DeleteIntent(_workspace, "Test_SDK_NET");

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void CreateExample()
        {
            #region CreateExample

            CreateExample request = new CreateExample()
            {
                Text = "Test_SDK_NET"
            };

            #endregion

            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.CreateExample(_workspace, "greetings", request);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void ListExamples()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.ListExamples(_workspace, "greetings");

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void UpdateExample()
        {
            #region UpdateExample

            UpdateExample request = new UpdateExample()
            {
                Text = "Update_SDK_NET"
            };

            #endregion

            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.UpdateExample(_workspace, "greetings", "Test_SDK_NET", request);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void GetExample()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.GetExample(_workspace, "greetings", "Update_SDK_NET");

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void DeleteExample()
        {
            ConversationService service = new ConversationService(_userName, _password);
            service.Endpoint = _endpoint;

            var results = service.DeleteExample(_workspace, "greetings", "Update_SDK_NET");

            Assert.IsNotNull(results);
        }
    }

}
