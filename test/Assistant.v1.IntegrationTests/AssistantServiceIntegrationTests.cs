/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

//  Uncomment to delete dotnet workspaces before running tests.
//#define DELETE_DOTNET_WORKSPACES

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using IBM.Watson.Assistant.v1.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using IBM.Watson.Util;

namespace IBM.Watson.Assistant.v1.IntegrationTests
{
    [TestClass]
    public class AssistantServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private AssistantService service;
        private static string credentials = string.Empty;

        private static string workspaceID;
        private string inputString = "Hello";
        private string assistantString0 = "Are you open on christmas?";
        private string assistantString1 = "Can you connect me to a real person?";
        private string assistantString2 = "goodbye";

        private static string createdWorkspaceName = "dotnet-sdk-example-workspace-delete";
        private static string createdWorkspaceDescription = "A Workspace created by the .NET SDK Conversation example script. Please delete this.";
        private static string createdWorkspaceLanguage = "en";
        private static string createdEntity = "entity";
        private static string createdEntityDescription = "Entity created by the .NET SDK Conversation example script.";
        private static string createdValue = "value";
        private static string createdIntent = "intent";
        private static string createdIntentDescription = "Intent created by the .NET SDK Conversation example script.";
        private static string createdCounterExampleText = "Example text";
        private static string createdSynonym = "synonym";
        private static string createdExample = "example";
        private static string dialogNodeName = "dialognode";
        private static string dialogNodeDesc = ".NET SDK Integration test dialog node";

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";
                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                var vcapServices = JObject.Parse(credentials);

                Credential credential = vcapCredentials.GetCredentialByname("assistant-sdk")[0].Credentials;
                endpoint = credential.Url;
                apikey = credential.IamApikey;
                workspaceID = credential.WorkspaceId;
            }
            #endregion

            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = endpoint
            };
            service = new AssistantService(tokenOptions, "2018-02-16");
            service.SetEndpoint(endpoint);

#if DELETE_DOTNET_WORKSPACES
            var workspaces = _service.ListWorkspaces();
            List<string> dotnet_workpaces = new List<string>();

            foreach (Workspace workspace in workspaces.Workspaces)
            {
                if (workspace.Name == _createdWorkspaceName)
                    dotnet_workpaces.Add(workspace.WorkspaceId);
            }

            foreach (string workspaceId in dotnet_workpaces)
            {
                try
                {
                    var getWorkspaceResult = GetWorkspace(workspaceId);
                    if (getWorkspaceResult != null)
                        DeleteWorkspace(workspaceId);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: {0}", e.Message);
                }
            }
#endif
        }

        [TestCleanup]
        public void Teardown()
        {
            var workspaces = service.ListWorkspaces();
            List<string> dotnet_workpaces = new List<string>();

            foreach (Workspace workspace in workspaces.Workspaces)
            {
                if (workspace.Name == createdWorkspaceName)
                    dotnet_workpaces.Add(workspace.WorkspaceId);
            }

            foreach (string workspaceId in dotnet_workpaces)
            {
                try
                {
                    var getWorkspaceResult = GetWorkspace(workspaceId);
                    if (getWorkspaceResult != null)
                        DeleteWorkspace(workspaceId);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: {0}", e.Message);
                }

            }
        }

        #region Message
        [TestMethod]
        public void Message_Success()
        {
            dynamic context;

            List<string> intents = new List<string>();
            MessageRequest messageRequest0 = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = inputString
                }
            };

            var results0 = Message(workspaceID, messageRequest0);
            context = results0.Context;
            intents.Add(GetIntent(results0));

            MessageRequest messageRequest1 = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = assistantString0
                },
                Context = context
            };

            var results1 = Message(workspaceID, messageRequest1);
            context = results1.Context;
            intents.Add(GetIntent(results1));

            MessageRequest messageRequest2 = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = assistantString1
                },
                Context = context
            };

            var results2 = Message(workspaceID, messageRequest2);
            context = results2.Context;
            intents.Add(GetIntent(results2));

            MessageRequest messageRequest3 = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = assistantString2
                },
                Context = context
            };

            var results3 = Message(workspaceID, messageRequest3);
            context = results3.Context;
            intents.Add(GetIntent(results3));

            Assert.IsNotNull(results0);
            Assert.IsNotNull(results1);
            Assert.IsNotNull(results2);
            Assert.IsNotNull(results3);
            foreach(string intent in intents)
            {
                Assert.IsTrue(IsUniqueInList(intent, intents));
            }
        }
        #endregion

        private bool IsUniqueInList(string value, List<string> list)
        {
            List<string> duplicates = new List<string>();
            foreach(string item in list)
            {
                duplicates = list.FindAll(x => x == item);
            }

            if (duplicates.Count > 1)
                return false;
            else
                return true;
        }
        
        #region Get Intent
        private string GetIntent(MessageResponse messageResponse)
        {
            return messageResponse.Intents[0]["intent"].ToString(); ;
        }
        #endregion

        #region Counter Examples
        [TestMethod]
        public void TestCounterExamples_Success()
        {
            CreateWorkspace createWorkspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage
            };

            var createWorkspaceResult = CreateWorkspace(createWorkspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var listCounterExamplesResult = ListCounterexamples(workspaceId);

            CreateCounterexample example = new CreateCounterexample()
            {
                Text = createdCounterExampleText
            };

            var createCounterexampleResult = CreateCounterexample(workspaceId, example);

            var getCounterexampleResult = GetCounterexample(workspaceId, example.Text);

            string updatedCounterExampleText = createdCounterExampleText + "-updated";
            UpdateCounterexample updateCounterExample = new UpdateCounterexample()
            {
                Text = updatedCounterExampleText
            };

            var updateCounterexampleResult = UpdateCounterexample(workspaceId, example.Text, updateCounterExample);

            var deleteCounterexampleResult = DeleteCounterexample(workspaceId, updateCounterExample.Text);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listCounterExamplesResult);
            Assert.IsNotNull(listCounterExamplesResult.Counterexamples);
            Assert.IsNotNull(createCounterexampleResult);
            Assert.IsFalse(string.IsNullOrEmpty(createCounterexampleResult.Text));
            Assert.IsNotNull(getCounterexampleResult);
            Assert.IsFalse(string.IsNullOrEmpty(getCounterexampleResult.Text));
            Assert.IsNotNull(updateCounterexampleResult);
            Assert.IsTrue(updateCounterexampleResult.Text == updateCounterExample.Text);
            Assert.IsNotNull(deleteCounterexampleResult);
        }
        #endregion

        #region Custom Headers
        [TestMethod]
        public void SendCustomHeaders_Success()
        {
            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = inputString
                },
                AlternateIntents = true
            };

            Dictionary<string, object> customData = new Dictionary<string, object>();
            Dictionary<string, string> customHeaders = new Dictionary<string, string>();
            customHeaders.Add("X-Watson-Metadata", "customer_id=some-assistant-customer-id");
            customData.Add(Constants.CUSTOM_REQUEST_HEADERS, customHeaders);

            var results = Message(workspaceID, messageRequest, customData: customData);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.CustomData);
            Assert.IsNotNull(results.CustomRequestHeaders);
            Assert.IsNotNull(results.ResponseHeaders);
            Assert.IsNotNull(results.ResponseJson);
        }
        #endregion

        #region Workspaces
        [TestMethod]
        public void TestWorkspaces_Success()
        {
            var ListWorkspacesResult = ListWorkspaces();

            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            var getWorkspaceResult = GetWorkspace(workspaceId);

            UpdateWorkspace updateWorkspace = new UpdateWorkspace()
            {
                Name = createdWorkspaceName + "-updated",
                Description = createdWorkspaceDescription + "-updated",
                Language = createdWorkspaceLanguage
            };

            var updateWorkspaceResult = UpdateWorkspace(workspaceId, updateWorkspace);

            var deleteWorkspaceResult = DeleteWorkspace(createWorkspaceResult.WorkspaceId);

            Assert.IsNotNull(createWorkspaceResult);
            Assert.IsFalse(string.IsNullOrEmpty(workspaceId));
            Assert.IsNotNull(getWorkspaceResult);
            Assert.IsFalse(string.IsNullOrEmpty(getWorkspaceResult.WorkspaceId));
            Assert.IsNotNull(updateWorkspaceResult);
            Assert.IsFalse(string.IsNullOrEmpty(updateWorkspaceResult.WorkspaceId));
            Assert.IsNotNull(ListWorkspacesResult);
            Assert.IsNotNull(deleteWorkspaceResult);
        }
        #endregion

        #region Entities
        [TestMethod]
        public void TestEntities_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var listEntitiesResult = ListEntities(workspaceId);

            CreateEntity entity = new CreateEntity()
            {
                Entity = createdEntity,
                Description = createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            var getEntityResult = GetEntity(workspaceId, entity.Entity);

            string updatedEntity = createdEntity + "-updated";
            string updatedEntityDescription = createdEntityDescription + "-updated";
            UpdateEntity updateEntity = new UpdateEntity()
            {
                Entity = updatedEntity,
                Description = updatedEntityDescription
            };

            var updateEntityResult = UpdateEntity(workspaceId, entity.Entity, updateEntity);

            var deleteEntityResult = DeleteEntity(workspaceId, updateEntity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listEntitiesResult);
            Assert.IsNotNull(createEntityResult);
            Assert.IsFalse(string.IsNullOrEmpty(createEntityResult.EntityName));
            Assert.IsNotNull(getEntityResult);
            Assert.IsTrue(getEntityResult.EntityName == entity.Entity);
            Assert.IsNotNull(updateEntityResult);
            Assert.IsTrue(updateEntityResult.EntityName == updateEntity.Entity);
            Assert.IsTrue(updateEntityResult.Description == updateEntity.Description);
            Assert.IsNotNull(deleteEntityResult);
        }
        #endregion

        #region Values
        [TestMethod]
        public void TestValues_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = createdEntity,
                Description = createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            var listValuesResult = ListValues(workspaceId, entity.Entity);

            CreateValue value = new CreateValue()
            {
                Value = createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);
            var getValueResult = GetValue(workspaceId, entity.Entity, value.Value);

            string updatedValue = createdValue + "-updated";
            UpdateValue updateValue = new UpdateValue()
            {
                Value = updatedValue
            };

            var updateValueResult = UpdateValue(workspaceId, entity.Entity, value.Value, updateValue);

            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, updateValue.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listValuesResult);
            Assert.IsNotNull(createValueResult);
            Assert.IsFalse(string.IsNullOrEmpty(createValueResult.ValueText));
            Assert.IsNotNull(getValueResult);
            Assert.IsTrue(getValueResult.ValueText == value.Value);
            Assert.IsNotNull(updateValueResult);
            Assert.IsTrue(updateValueResult.ValueText == updateValue.Value);
            Assert.IsNotNull(deleteValueResult);
        }
        #endregion

        #region Synonyms
        [TestMethod]
        public void TestSynonyms_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = createdEntity,
                Description = createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);
            var listSynonymsResult = ListSynonyms(workspaceId, entity.Entity, value.Value);

            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = createdSynonym
            };
            var createSynonymResult = CreateSynonym(workspaceId, entity.Entity, value.Value, synonym);
            var getSynonymResult = GetSynonym(workspaceId, entity.Entity, value.Value, synonym.Synonym);

            string updatedSynonym = createdSynonym + "-updated";
            UpdateSynonym updateSynonym = new UpdateSynonym()
            {
                Synonym = updatedSynonym
            };
            var updateSynonymResult = UpdateSynonym(workspaceId, entity.Entity, value.Value, synonym.Synonym, updateSynonym);

            var deleteSynonymResult = DeleteSynonym(workspaceId, entity.Entity, value.Value, updateSynonym.Synonym);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listSynonymsResult);
            Assert.IsNotNull(createSynonymResult);
            Assert.IsTrue(createSynonymResult.SynonymText == synonym.Synonym);
            Assert.IsNotNull(getSynonymResult);
            Assert.IsTrue(getSynonymResult.SynonymText == synonym.Synonym);
            Assert.IsNotNull(updateSynonymResult);
            Assert.IsTrue(updateSynonymResult.SynonymText == updateSynonym.Synonym);
            Assert.IsNotNull(deleteSynonymResult);
        }
        #endregion

        #region Intents
        [TestMethod]
        public void TestIntents_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var listIntentsReult = ListIntents(workspaceId);

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = createdIntent,
                Description = createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            var getIntentResult = GetIntent(workspaceId, createIntent.Intent);

            string updatedIntent = createdIntent + "-updated";
            string updatedIntentDescription = createdIntentDescription + "-updated";
            UpdateIntent updateIntent = new UpdateIntent()
            {
                Intent = updatedIntent,
                Description = updatedIntentDescription
            };

            var updateIntentResult = UpdateIntent(workspaceId, createIntent.Intent, updateIntent);
            var deleteIntentResult = DeleteIntent(workspaceId, updateIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listIntentsReult);
            Assert.IsNotNull(createIntentResult);
            Assert.IsFalse(string.IsNullOrEmpty(createIntentResult.IntentName));
            Assert.IsNotNull(getIntentResult);
            Assert.IsFalse(string.IsNullOrEmpty(getIntentResult.IntentName));
            Assert.IsNotNull(updateIntentResult);
            Assert.IsTrue(updateIntentResult.IntentName == updateIntent.Intent);
            Assert.IsNotNull(deleteIntentResult);
        }
        #endregion

        #region Examples
        [TestMethod]
        public void TestExamples_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = createdIntent,
                Description = createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            var listExamplesResult = ListExamples(workspaceId, createIntent.Intent);

            CreateExample createExample = new CreateExample()
            {
                Text = createdExample
            };
            var createExampleResult = CreateExample(workspaceId, createIntent.Intent, createExample);
            var getExampleResult = GetExample(workspaceId, createIntent.Intent, createExample.Text);

            string updatedExample = createdExample + "-updated";
            UpdateExample updateExample = new UpdateExample()
            {
                Text = updatedExample
            };
            var updateExampleResult = UpdateExample(workspaceId, createIntent.Intent, createExample.Text, updateExample);

            var deleteExampleResult = DeleteExample(workspaceId, createIntent.Intent, updateExample.Text);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listExamplesResult);
            Assert.IsNotNull(createExampleResult);
            Assert.IsTrue(createExampleResult.ExampleText == createExample.Text);
            Assert.IsNotNull(getExampleResult);
            Assert.IsTrue(getExampleResult.ExampleText == createExample.Text);
            Assert.IsNotNull(updateExampleResult);
            Assert.IsTrue(updateExampleResult.ExampleText == updateExample.Text);
            Assert.IsNotNull(deleteExampleResult);
        }
        #endregion

        #region Dialog Nodes
        [TestMethod]
        public void TestDialogNodes_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var listDialogNodes = ListDialogNodes(workspaceId);

            CreateDialogNode createDialogNode = new CreateDialogNode()
            {
                DialogNode = dialogNodeName,
                Description = dialogNodeDesc
            };
            var createDialogNodeResult = CreateDialogNode(workspaceId, createDialogNode);
            var getDialogNodeResult = GetDialogNode(workspaceId, createDialogNode.DialogNode);

            string updatedDialogNodeName = dialogNodeName + "_updated";
            string updatedDialogNodeDescription = dialogNodeDesc + "_updated";
            UpdateDialogNode updateDialogNode = new UpdateDialogNode()
            {
                DialogNode = updatedDialogNodeName,
                Description = updatedDialogNodeDescription
            };

            var updateDialogNodeResult = UpdateDialogNode(workspaceId, createDialogNode.DialogNode, updateDialogNode);
            var deleteDialogNodeResult = DeleteDialogNode(workspaceId, updateDialogNode.DialogNode);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listDialogNodes);
            Assert.IsNotNull(createDialogNodeResult);
            Assert.IsTrue(createDialogNodeResult.DialogNodeId == createDialogNode.DialogNode);
            Assert.IsTrue(createDialogNodeResult.Description == createDialogNode.Description);
            Assert.IsNotNull(getDialogNodeResult);
            Assert.IsTrue(getDialogNodeResult.DialogNodeId == createDialogNode.DialogNode);
            Assert.IsTrue(getDialogNodeResult.Description == createDialogNode.Description);
            Assert.IsNotNull(updateDialogNodeResult);
            Assert.IsTrue(updateDialogNodeResult.DialogNodeId == updateDialogNode.DialogNode);
            Assert.IsTrue(updateDialogNodeResult.Description == updateDialogNode.Description);
            Assert.IsNotNull(deleteDialogNodeResult);
        }
        #endregion

        #region Logs
        [TestMethod]
        public void ListLogs_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var listLogsResult = ListLogs(workspaceId);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listLogsResult);
        }

        [TestMethod]
        public void ListAllLogs_Success()
        {
            var filter = "(language::en,request.context.metadata.deployment::deployment_1)";
            var listAllLogsResult = ListAllLogs(filter);
            Assert.IsNotNull(listAllLogsResult);
        }
        #endregion

        #region Mentions
        [TestMethod]
        public void TestMentions_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = createdWorkspaceName,
                Description = createdWorkspaceDescription,
                Language = createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = createdEntity,
                Description = createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);

            var ListMentionsResult = ListMentions(workspaceId, createdEntity);

            Assert.IsNotNull(createEntityResult);
            Assert.IsFalse(string.IsNullOrEmpty(createEntityResult.EntityName));
            Assert.IsNotNull(ListMentionsResult);
        }
        #endregion

        #region Delay
        private void Delay(int delayTimeInMilliseconds)
        {
            System.Threading.Thread.Sleep(delayTimeInMilliseconds);
        }
        #endregion

        #region Generated
        #region Message
        private MessageResponse Message(string workspaceId, MessageRequest request = null, bool? nodesVisitedDetails = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Message()");
            var result = service.Message(workspaceId: workspaceId, request: request, nodesVisitedDetails: nodesVisitedDetails, customData: customData);

            if (result != null)
            {
                Console.WriteLine("Message() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Message()");
            }

            return result;
        }
        #endregion

        #region CreateWorkspace
        private Workspace CreateWorkspace(CreateWorkspace properties = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateWorkspace()");
            var result = service.CreateWorkspace(properties: properties, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateWorkspace() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateWorkspace()");
            }

            return result;
        }
        #endregion

        #region DeleteWorkspace
        private BaseModel DeleteWorkspace(string workspaceId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteWorkspace()");
            var result = service.DeleteWorkspace(workspaceId: workspaceId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteWorkspace() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteWorkspace()");
            }

            return result;
        }
        #endregion

        #region GetWorkspace
        private WorkspaceExport GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetWorkspace()");
            var result = service.GetWorkspace(workspaceId: workspaceId, export: export, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetWorkspace() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetWorkspace()");
            }

            return result;
        }
        #endregion

        #region ListWorkspaces
        private WorkspaceCollection ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListWorkspaces()");
            var result = service.ListWorkspaces(pageLimit: pageLimit, includeCount: includeCount, sort: sort, cursor: cursor, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListWorkspaces() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListWorkspaces()");
            }

            return result;
        }
        #endregion

        #region UpdateWorkspace
        private Workspace UpdateWorkspace(string workspaceId, UpdateWorkspace properties = null, bool? append = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateWorkspace()");
            var result = service.UpdateWorkspace(workspaceId: workspaceId, properties: properties, append: append, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateWorkspace() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateWorkspace()");
            }

            return result;
        }
        #endregion

        #region CreateIntent
        private Intent CreateIntent(string workspaceId, CreateIntent body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateIntent()");
            var result = service.CreateIntent(workspaceId: workspaceId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateIntent() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateIntent()");
            }

            return result;
        }
        #endregion

        #region DeleteIntent
        private BaseModel DeleteIntent(string workspaceId, string intent, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteIntent()");
            var result = service.DeleteIntent(workspaceId: workspaceId, intent: intent, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteIntent() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteIntent()");
            }

            return result;
        }
        #endregion

        #region GetIntent
        private IntentExport GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetIntent()");
            var result = service.GetIntent(workspaceId: workspaceId, intent: intent, export: export, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetIntent() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetIntent()");
            }

            return result;
        }
        #endregion

        #region ListIntents
        private IntentCollection ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListIntents()");
            var result = service.ListIntents(workspaceId: workspaceId, export: export, pageLimit: pageLimit, includeCount: includeCount, sort: sort, cursor: cursor, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListIntents() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListIntents()");
            }

            return result;
        }
        #endregion

        #region UpdateIntent
        private Intent UpdateIntent(string workspaceId, string intent, UpdateIntent body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateIntent()");
            var result = service.UpdateIntent(workspaceId: workspaceId, intent: intent, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateIntent() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateIntent()");
            }

            return result;
        }
        #endregion

        #region CreateExample
        private Example CreateExample(string workspaceId, string intent, CreateExample body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateExample()");
            var result = service.CreateExample(workspaceId: workspaceId, intent: intent, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateExample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateExample()");
            }

            return result;
        }
        #endregion

        #region DeleteExample
        private BaseModel DeleteExample(string workspaceId, string intent, string text, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteExample()");
            var result = service.DeleteExample(workspaceId: workspaceId, intent: intent, text: text, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteExample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteExample()");
            }

            return result;
        }
        #endregion

        #region GetExample
        private Example GetExample(string workspaceId, string intent, string text, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetExample()");
            var result = service.GetExample(workspaceId: workspaceId, intent: intent, text: text, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetExample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetExample()");
            }

            return result;
        }
        #endregion

        #region ListExamples
        private ExampleCollection ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListExamples()");
            var result = service.ListExamples(workspaceId: workspaceId, intent: intent, pageLimit: pageLimit, includeCount: includeCount, sort: sort, cursor: cursor, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListExamples() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListExamples()");
            }

            return result;
        }
        #endregion

        #region UpdateExample
        private Example UpdateExample(string workspaceId, string intent, string text, UpdateExample body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateExample()");
            var result = service.UpdateExample(workspaceId: workspaceId, intent: intent, text: text, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateExample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateExample()");
            }

            return result;
        }
        #endregion

        #region CreateCounterexample
        private Counterexample CreateCounterexample(string workspaceId, CreateCounterexample body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateCounterexample()");
            var result = service.CreateCounterexample(workspaceId: workspaceId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateCounterexample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateCounterexample()");
            }

            return result;
        }
        #endregion

        #region DeleteCounterexample
        private BaseModel DeleteCounterexample(string workspaceId, string text, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteCounterexample()");
            var result = service.DeleteCounterexample(workspaceId: workspaceId, text: text, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteCounterexample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteCounterexample()");
            }

            return result;
        }
        #endregion

        #region GetCounterexample
        private Counterexample GetCounterexample(string workspaceId, string text, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetCounterexample()");
            var result = service.GetCounterexample(workspaceId: workspaceId, text: text, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetCounterexample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetCounterexample()");
            }

            return result;
        }
        #endregion

        #region ListCounterexamples
        private CounterexampleCollection ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListCounterexamples()");
            var result = service.ListCounterexamples(workspaceId: workspaceId, pageLimit: pageLimit, includeCount: includeCount, sort: sort, cursor: cursor, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListCounterexamples() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListCounterexamples()");
            }

            return result;
        }
        #endregion

        #region UpdateCounterexample
        private Counterexample UpdateCounterexample(string workspaceId, string text, UpdateCounterexample body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateCounterexample()");
            var result = service.UpdateCounterexample(workspaceId: workspaceId, text: text, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateCounterexample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateCounterexample()");
            }

            return result;
        }
        #endregion

        #region CreateEntity
        private Entity CreateEntity(string workspaceId, CreateEntity properties, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateEntity()");
            var result = service.CreateEntity(workspaceId: workspaceId, properties: properties, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateEntity() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateEntity()");
            }

            return result;
        }
        #endregion

        #region DeleteEntity
        private BaseModel DeleteEntity(string workspaceId, string entity, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteEntity()");
            var result = service.DeleteEntity(workspaceId: workspaceId, entity: entity, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteEntity() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteEntity()");
            }

            return result;
        }
        #endregion

        #region GetEntity
        private EntityExport GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetEntity()");
            var result = service.GetEntity(workspaceId: workspaceId, entity: entity, export: export, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetEntity() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetEntity()");
            }

            return result;
        }
        #endregion

        #region ListEntities
        private EntityCollection ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListEntities()");
            var result = service.ListEntities(workspaceId: workspaceId, export: export, pageLimit: pageLimit, includeCount: includeCount, sort: sort, cursor: cursor, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListEntities() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListEntities()");
            }

            return result;
        }
        #endregion

        #region UpdateEntity
        private Entity UpdateEntity(string workspaceId, string entity, UpdateEntity properties, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateEntity()");
            var result = service.UpdateEntity(workspaceId: workspaceId, entity: entity, properties: properties, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateEntity() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateEntity()");
            }

            return result;
        }
        #endregion

        #region ListMentions
        private EntityMentionCollection ListMentions(string workspaceId, string entity, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListMentions()");
            var result = service.ListMentions(workspaceId: workspaceId, entity: entity, export: export, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListMentions() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListMentions()");
            }

            return result;
        }
        #endregion

        #region CreateValue
        private Value CreateValue(string workspaceId, string entity, CreateValue properties, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateValue()");
            var result = service.CreateValue(workspaceId: workspaceId, entity: entity, properties: properties, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateValue() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateValue()");
            }

            return result;
        }
        #endregion

        #region DeleteValue
        private BaseModel DeleteValue(string workspaceId, string entity, string value, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteValue()");
            var result = service.DeleteValue(workspaceId: workspaceId, entity: entity, value: value, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteValue() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteValue()");
            }

            return result;
        }
        #endregion

        #region GetValue
        private ValueExport GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetValue()");
            var result = service.GetValue(workspaceId: workspaceId, entity: entity, value: value, export: export, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetValue() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetValue()");
            }

            return result;
        }
        #endregion

        #region ListValues
        private ValueCollection ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListValues()");
            var result = service.ListValues(workspaceId: workspaceId, entity: entity, export: export, pageLimit: pageLimit, includeCount: includeCount, sort: sort, cursor: cursor, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListValues() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListValues()");
            }

            return result;
        }
        #endregion

        #region UpdateValue
        private Value UpdateValue(string workspaceId, string entity, string value, UpdateValue properties, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateValue()");
            var result = service.UpdateValue(workspaceId: workspaceId, entity: entity, value: value, properties: properties, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateValue() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateValue()");
            }

            return result;
        }
        #endregion

        #region CreateSynonym
        private Synonym CreateSynonym(string workspaceId, string entity, string value, CreateSynonym body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateSynonym()");
            var result = service.CreateSynonym(workspaceId: workspaceId, entity: entity, value: value, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateSynonym() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateSynonym()");
            }

            return result;
        }
        #endregion

        #region DeleteSynonym
        private BaseModel DeleteSynonym(string workspaceId, string entity, string value, string synonym, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteSynonym()");
            var result = service.DeleteSynonym(workspaceId: workspaceId, entity: entity, value: value, synonym: synonym, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteSynonym() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteSynonym()");
            }

            return result;
        }
        #endregion

        #region GetSynonym
        private Synonym GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetSynonym()");
            var result = service.GetSynonym(workspaceId: workspaceId, entity: entity, value: value, synonym: synonym, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetSynonym() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetSynonym()");
            }

            return result;
        }
        #endregion

        #region ListSynonyms
        private SynonymCollection ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListSynonyms()");
            var result = service.ListSynonyms(workspaceId: workspaceId, entity: entity, value: value, pageLimit: pageLimit, includeCount: includeCount, sort: sort, cursor: cursor, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListSynonyms() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListSynonyms()");
            }

            return result;
        }
        #endregion

        #region UpdateSynonym
        private Synonym UpdateSynonym(string workspaceId, string entity, string value, string synonym, UpdateSynonym body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateSynonym()");
            var result = service.UpdateSynonym(workspaceId: workspaceId, entity: entity, value: value, synonym: synonym, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateSynonym() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateSynonym()");
            }

            return result;
        }
        #endregion

        #region CreateDialogNode
        private DialogNode CreateDialogNode(string workspaceId, CreateDialogNode properties, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateDialogNode()");
            var result = service.CreateDialogNode(workspaceId: workspaceId, properties: properties, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateDialogNode() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateDialogNode()");
            }

            return result;
        }
        #endregion

        #region DeleteDialogNode
        private BaseModel DeleteDialogNode(string workspaceId, string dialogNode, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteDialogNode()");
            var result = service.DeleteDialogNode(workspaceId: workspaceId, dialogNode: dialogNode, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteDialogNode() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteDialogNode()");
            }

            return result;
        }
        #endregion

        #region GetDialogNode
        private DialogNode GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetDialogNode()");
            var result = service.GetDialogNode(workspaceId: workspaceId, dialogNode: dialogNode, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetDialogNode() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetDialogNode()");
            }

            return result;
        }
        #endregion

        #region ListDialogNodes
        private DialogNodeCollection ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListDialogNodes()");
            var result = service.ListDialogNodes(workspaceId: workspaceId, pageLimit: pageLimit, includeCount: includeCount, sort: sort, cursor: cursor, includeAudit: includeAudit, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListDialogNodes() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListDialogNodes()");
            }

            return result;
        }
        #endregion

        #region UpdateDialogNode
        private DialogNode UpdateDialogNode(string workspaceId, string dialogNode, UpdateDialogNode properties, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateDialogNode()");
            var result = service.UpdateDialogNode(workspaceId: workspaceId, dialogNode: dialogNode, properties: properties, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateDialogNode() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateDialogNode()");
            }

            return result;
        }
        #endregion

        #region ListAllLogs
        private LogCollection ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListAllLogs()");
            var result = service.ListAllLogs(filter: filter, sort: sort, pageLimit: pageLimit, cursor: cursor, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListAllLogs() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListAllLogs()");
            }

            return result;
        }
        #endregion

        #region ListLogs
        private LogCollection ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListLogs()");
            var result = service.ListLogs(workspaceId: workspaceId, sort: sort, filter: filter, pageLimit: pageLimit, cursor: cursor, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListLogs() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListLogs()");
            }

            return result;
        }
        #endregion

        #region DeleteUserData
        private BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteUserData()");
            var result = service.DeleteUserData(customerId: customerId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteUserData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteUserData()");
            }

            return result;
        }
        #endregion

        #endregion
    }
}