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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.IntegratiationTests
{
    [TestClass]
    public class ConversationIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private ConversationService service;
        private static string credentials = string.Empty;

        private static string _workspaceID;
        private string _inputString = "Turn on the winshield wipers";

        private static string _createdWorkspaceName = "dotnet-sdk-example-workspace-delete";
        private static string _createdWorkspaceDescription = "A Workspace created by the .NET SDK Conversation example script. Please delete this.";
        private static string _createdWorkspaceLanguage = "en";
        private static string _createdEntity = "entity";
        private static string _createdEntityDescription = "Entity created by the .NET SDK Conversation example script.";
        private static string _createdValue = "value";
        private static string _createdIntent = "intent";
        private static string _createdIntentDescription = "Intent created by the .NET SDK Conversation example script.";
        private static string _createdCounterExampleText = "Example text";
        private static string _createdSynonym = "synonym";
        private static string _createdExample = "example";
        private static string _dialogNodeName = "test-dialog-node";
        private static string _dialogNodeDesc = ".NET SDK Integration test dialog node";
        private int _delayTimeInMilliseconds = 500;

        [TestInitialize]
        public void Setup()
        {
            if (string.IsNullOrEmpty(credentials))
            {
                try
                {
                    credentials = Utility.SimpleGet(
                        Environment.GetEnvironmentVariable("VCAP_URL"),
                        Environment.GetEnvironmentVariable("VCAP_USERNAME"),
                        Environment.GetEnvironmentVariable("VCAP_PASSWORD")).Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Failed to get credentials: {0}", e.Message));
                }

                Task.WaitAll();
                var vcapServices = JObject.Parse(credentials);

                _endpoint = vcapServices["conversation"]["url"].Value<string>();
                _username = vcapServices["conversation"]["username"].Value<string>();
                _password = vcapServices["conversation"]["password"].Value<string>();
                _workspaceID = "506e4a2e-3d5d-4dca-b374-38edbb4139ab";
            }

            service = new ConversationService(_username, _password, ConversationService.CONVERSATION_VERSION_DATE_2017_05_26)
            {
                Endpoint = _endpoint
            };

            var workspaces = service.ListWorkspaces();
            List<string> dotnet_workpaces = new List<string>();

            foreach (Workspace workspace in workspaces.Workspaces)
            {
                if (workspace.Name == _createdWorkspaceName)
                    dotnet_workpaces.Add(workspace.WorkspaceId);
            }

            foreach (string workspaceId in dotnet_workpaces)
            {
                DeleteWorkspace(workspaceId);
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            var workspaces = service.ListWorkspaces();
            List<string> dotnet_workpaces = new List<string>();

            foreach (Workspace workspace in workspaces.Workspaces)
            {
                if (workspace.Name == _createdWorkspaceName)
                    dotnet_workpaces.Add(workspace.WorkspaceId);
            }

            foreach (string workspaceId in dotnet_workpaces)
            {
                DeleteWorkspace(workspaceId);
            }
        }

        #region Message
        [TestMethod]
        public void Message_Success()
        {
            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = _inputString
                },
                AlternateIntents = true
            };

            var results = Message(_workspaceID, messageRequest);
            Assert.IsNotNull(results);
        }
        #endregion


        #region Workspaces
        [TestMethod]
        public void ListWorkspaces_Success()
        {
            var result = ListWorkspaces();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateWorkspace_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var deleteWorkspaceResult = DeleteWorkspace(createWorkspaceResult.WorkspaceId);

            Assert.IsNotNull(createWorkspaceResult);
            Assert.IsFalse(string.IsNullOrEmpty(workspaceId));
        }

        [TestMethod]
        public void GetWorkspace_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            var getWorkspaceResult = GetWorkspace(workspaceId);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(getWorkspaceResult);
            Assert.IsFalse(string.IsNullOrEmpty(getWorkspaceResult.WorkspaceId));
        }

        [TestMethod]
        public void UpdateWorkspace_Success()
        {
            CreateWorkspace createWorkspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };

            var createWorkspaceResult = CreateWorkspace(createWorkspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            UpdateWorkspace updateWorkspace = new UpdateWorkspace()
            {
                Name = _createdWorkspaceName + "-updated",
                Description = _createdWorkspaceDescription + "-updated",
                Language = _createdWorkspaceLanguage
            };

            var updateWorkspaceResult = UpdateWorkspace(workspaceId, updateWorkspace);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(updateWorkspaceResult);
            Assert.IsFalse(string.IsNullOrEmpty(updateWorkspaceResult.WorkspaceId));
        }



        [TestMethod]
        public void DeleteWorkspace_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var deleteWorkspaceResult = DeleteWorkspace(createWorkspaceResult.WorkspaceId);

            Assert.IsNotNull(deleteWorkspaceResult);
        }
        #endregion

        #region Counter Examples
        [TestMethod]
        public void ListCounterExamples_Success()
        {
            CreateWorkspace createWorkspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };

            var createWorkspaceResult = CreateWorkspace(createWorkspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            var listCounterExamplesResult = ListCounterexamples(workspaceId);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listCounterExamplesResult);
            Assert.IsNotNull(listCounterExamplesResult.Counterexamples);
        }

        [TestMethod]
        public void CreateCounterExample_Success()
        {
            CreateWorkspace createWorkspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };

            var createWorkspaceResult = CreateWorkspace(createWorkspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateCounterexample example = new CreateCounterexample()
            {
                Text = _createdCounterExampleText
            };

            var createCounterexampleResult = CreateCounterexample(workspaceId, example);
            var deleteCounterexampleResult = DeleteCounterexample(workspaceId, example.Text);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(createCounterexampleResult);
            Assert.IsFalse(string.IsNullOrEmpty(createCounterexampleResult.Text));
        }

        [TestMethod]
        public void GetCounterExample_Success()
        {
            CreateWorkspace createWorkspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };

            var createWorkspaceResult = CreateWorkspace(createWorkspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateCounterexample example = new CreateCounterexample()
            {
                Text = _createdCounterExampleText
            };

            var createCounterexampleResult = CreateCounterexample(workspaceId, example);

            var getCounterexampleResult = GetCounterexample(workspaceId, example.Text);
            var deleteCounterexampleResult = DeleteCounterexample(workspaceId, example.Text);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(getCounterexampleResult);
            Assert.IsFalse(string.IsNullOrEmpty(getCounterexampleResult.Text));
        }

        [TestMethod]
        public void UpdateCounterExample_Success()
        {
            CreateWorkspace createWorkspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };

            var createWorkspaceResult = CreateWorkspace(createWorkspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateCounterexample example = new CreateCounterexample()
            {
                Text = _createdCounterExampleText
            };

            var createCounterexampleResult = CreateCounterexample(workspaceId, example);

            string updatedCounterExampleText = _createdCounterExampleText + "-updated";
            UpdateCounterexample updateCounterExample = new UpdateCounterexample()
            {
                Text = updatedCounterExampleText
            };

            var updateCounterexampleResult = UpdateCounterexample(workspaceId, example.Text, updateCounterExample);
            var deleteCounterexampleResult = DeleteCounterexample(workspaceId, updateCounterExample.Text);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(updateCounterexampleResult);
            Assert.IsTrue(updateCounterexampleResult.Text == updateCounterExample.Text);
        }

        [TestMethod]
        public void DeleteCounterExample_Success()
        {
            CreateWorkspace createWorkspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };

            var createWorkspaceResult = CreateWorkspace(createWorkspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateCounterexample example = new CreateCounterexample()
            {
                Text = _createdCounterExampleText
            };

            var createCounterexampleResult = CreateCounterexample(workspaceId, example);
            var deleteCounterexampleResult = DeleteCounterexample(workspaceId, example.Text);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(deleteCounterexampleResult);
        }
        #endregion

        #region Entities
        [TestMethod]
        public void ListEntities_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var listEntitiesResult = ListEntities(workspaceId);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listEntitiesResult);
        }

        [TestMethod]
        public void CreateEntity_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(createEntityResult);
            Assert.IsFalse(string.IsNullOrEmpty(createEntityResult.EntityName));
        }

        [TestMethod]
        public void GetEntity_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            var getEntityResult = GetEntity(workspaceId, entity.Entity);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(getEntityResult);
            Assert.IsTrue(getEntityResult.EntityName == entity.Entity);
        }

        [TestMethod]
        public void UpdateEntity_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);

            string updatedEntity = _createdEntity + "-updated";
            string updatedEntityDescription = _createdEntityDescription + "-updated";
            UpdateEntity updateEntity = new UpdateEntity()
            {
                Entity = updatedEntity,
                Description = updatedEntityDescription
            };

            var updateEntityResult = UpdateEntity(workspaceId, entity.Entity, updateEntity);
            var deleteEntityResult = DeleteEntity(workspaceId, updateEntity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(updateEntityResult);
            Assert.IsTrue(updateEntityResult.EntityName == updateEntity.Entity);
            Assert.IsTrue(updateEntityResult.Description == updateEntity.Description);
        }

        [TestMethod]
        public void DeleteEntity_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(deleteEntityResult);
        }
        #endregion

        #region Values
        [TestMethod]
        public void ListValues_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            var listValuesResult = ListValues(workspaceId, entity.Entity);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listValuesResult);
        }

        [TestMethod]
        public void CreateValue_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(createValueResult);
            Assert.IsFalse(string.IsNullOrEmpty(createValueResult.ValueText));
        }

        [TestMethod]
        public void GetValue_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);
            var getValueResult = GetValue(workspaceId, entity.Entity, value.Value);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(getValueResult);
            Assert.IsTrue(getValueResult.ValueText == value.Value);
        }

        [TestMethod]
        public void UpdateValue_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);

            string updatedValue = _createdValue + "-updated";
            UpdateValue updateValue = new UpdateValue()
            {
                Value = updatedValue
            };

            var updateValueResult = UpdateValue(workspaceId, entity.Entity, value.Value, updateValue);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(updateValueResult);
            Assert.IsTrue(updateValueResult.ValueText == updateValue.Value);
        }

        [TestMethod]
        public void DeleteValue_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(deleteValueResult);
        }
        #endregion

        #region Synonyms
        [TestMethod]
        public void ListSynonyms_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);
            var listSynonymsResult = ListSynonyms(workspaceId, entity.Entity, value.Value);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listSynonymsResult);
        }

        [TestMethod]
        public void CreateSynonym_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);

            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = _createdSynonym
            };
            var createSynonymResult = CreateSynonym(workspaceId, entity.Entity, value.Value, synonym);
            var deleteSynonymResult = DeleteSynonym(workspaceId, entity.Entity, value.Value, createSynonymResult.SynonymText);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(createSynonymResult);
            Assert.IsTrue(createSynonymResult.SynonymText == synonym.Synonym);
        }

        [TestMethod]
        public void GetSynonym_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);

            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = _createdSynonym
            };
            var createSynonymResult = CreateSynonym(workspaceId, entity.Entity, value.Value, synonym);
            var getSynonymResult = GetSynonym(workspaceId, entity.Entity, value.Value, synonym.Synonym);
            var deleteSynonymResult = DeleteSynonym(workspaceId, entity.Entity, value.Value, createSynonymResult.SynonymText);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(getSynonymResult);
            Assert.IsTrue(getSynonymResult.SynonymText == synonym.Synonym);
        }

        [TestMethod]
        public void UpdateSynonym_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);

            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = _createdSynonym
            };
            var createSynonymResult = CreateSynonym(workspaceId, entity.Entity, value.Value, synonym);
            string updatedSynonym = _createdSynonym + "-updated";
            UpdateSynonym updateSynonym = new UpdateSynonym()
            {
                Synonym = updatedSynonym
            };
            var updateSynonymResult = UpdateSynonym(workspaceId, entity.Entity, value.Value, synonym.Synonym, updateSynonym);
            var deleteSynonymResult = DeleteSynonym(workspaceId, entity.Entity, value.Value, updateSynonym.Synonym);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(updateSynonymResult);
            Assert.IsTrue(updateSynonymResult.SynonymText == updateSynonym.Synonym);
        }

        [TestMethod]
        public void DeleteSynonym_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var createEntityResult = CreateEntity(workspaceId, entity);
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var createValueResult = CreateValue(workspaceId, entity.Entity, value);

            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = _createdSynonym
            };
            var createSynonymResult = CreateSynonym(workspaceId, entity.Entity, value.Value, synonym);
            var deleteSynonymResult = DeleteSynonym(workspaceId, entity.Entity, value.Value, createSynonymResult.SynonymText);
            var deleteValueResult = DeleteValue(workspaceId, entity.Entity, value.Value);
            var deleteEntityResult = DeleteEntity(workspaceId, entity.Entity);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(deleteSynonymResult);
        }
        #endregion

        #region Intents
        [TestMethod]
        public void ListIntents_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var listIntentsReult = ListIntents(workspaceId);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listIntentsReult);
        }

        [TestMethod]
        public void CreateIntent_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(createIntentResult);
            Assert.IsFalse(string.IsNullOrEmpty(createIntentResult.IntentName));
        }

        [TestMethod]
        public void GetIntent_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            var getIntentResult = GetIntent(workspaceId, createIntent.Intent);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(getIntentResult);
            Assert.IsFalse(string.IsNullOrEmpty(getIntentResult.IntentName));
        }

        [TestMethod]
        public void UpdateIntent_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            string updatedIntent = _createdIntent + "-updated";
            string updatedIntentDescription = _createdIntentDescription + "-updated";
            UpdateIntent updateIntent = new UpdateIntent()
            {
                Intent = updatedIntent,
                Description = updatedIntentDescription
            };
            var updateIntentResult = UpdateIntent(workspaceId, createIntent.Intent, updateIntent);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(updateIntentResult);
            Assert.IsTrue(updateIntentResult.IntentName == updateIntent.Intent);
        }

        [TestMethod]
        public void DeleteIntent_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(deleteIntentResult);
        }
        #endregion

        #region Examples
        [TestMethod]
        public void ListExamples_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            var listExamplesResult = ListExamples(workspaceId, createIntent.Intent);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listExamplesResult);
        }

        [TestMethod]
        public void CreateExample_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            CreateExample createExample = new CreateExample()
            {
                Text = _createdExample
            };
            var createExampleResult = CreateExample(workspaceId, createIntent.Intent, createExample);
            var deleteExampleResult = DeleteExample(workspaceId, createIntent.Intent, createExample.Text);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(createExampleResult);
            Assert.IsTrue(createExampleResult.ExampleText == createExample.Text);
        }

        [TestMethod]
        public void GetExample_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            CreateExample createExample = new CreateExample()
            {
                Text = _createdExample
            };
            var createExampleResult = CreateExample(workspaceId, createIntent.Intent, createExample);
            var getExampleResult = GetExample(workspaceId, createIntent.Intent, createExample.Text);
            var deleteExampleResult = DeleteExample(workspaceId, createIntent.Intent, createExample.Text);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(getExampleResult);
            Assert.IsTrue(getExampleResult.ExampleText == createExample.Text);
        }

        [TestMethod]
        public void UpdateExample_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            CreateExample createExample = new CreateExample()
            {
                Text = _createdExample
            };
            var createExampleResult = CreateExample(workspaceId, createIntent.Intent, createExample);
            string updatedExample = _createdExample + "-updated";
            UpdateExample updateExample = new UpdateExample()
            {
                Text = updatedExample
            };
            var updateExampleResult = UpdateExample(workspaceId, createIntent.Intent, createExample.Text, updateExample);
            var deleteExampleResult = DeleteExample(workspaceId, createIntent.Intent, updateExample.Text);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(updateExampleResult);
            Assert.IsTrue(updateExampleResult.ExampleText == updateExample.Text);
        }

        [TestMethod]
        public void DeleteExample_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;

            CreateIntent createIntent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var createIntentResult = CreateIntent(workspaceId, createIntent);
            CreateExample createExample = new CreateExample()
            {
                Text = _createdExample
            };
            var createExampleResult = CreateExample(workspaceId, createIntent.Intent, createExample);
            var deleteExampleResult = DeleteExample(workspaceId, createIntent.Intent, createExample.Text);
            var deleteIntentResult = DeleteIntent(workspaceId, createIntent.Intent);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(deleteExampleResult);
        }
        #endregion

        #region Dialog Nodes
        [TestMethod]
        public void ListDialogNodes_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            var listDialogNodes = ListDialogNodes(workspaceId);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listDialogNodes);
        }

        [TestMethod]
        public void CreateDialogNode_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            CreateDialogNode createDialogNode = new CreateDialogNode()
            {
                DialogNode = _dialogNodeName,
                Description = _dialogNodeDesc
            };
            var createDialogNodeResult = CreateDialogNode(workspaceId, createDialogNode);
            var deleteDialogNodeResult = DeleteDialogNode(workspaceId, createDialogNode.DialogNode);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(createDialogNodeResult);
            Assert.IsTrue(createDialogNodeResult.DialogNodeId == createDialogNode.DialogNode);
            Assert.IsTrue(createDialogNodeResult.Description == createDialogNode.Description);
        }

        [TestMethod]
        public void GetDialogNode_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            CreateDialogNode createDialogNode = new CreateDialogNode()
            {
                DialogNode = _dialogNodeName,
                Description = _dialogNodeDesc
            };
            var createDialogNodeResult = CreateDialogNode(workspaceId, createDialogNode);
            var getDialogNodeResult = GetDialogNode(workspaceId, createDialogNode.DialogNode);
            var deleteDialogNodeResult = DeleteDialogNode(workspaceId, createDialogNode.DialogNode);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(getDialogNodeResult);
            Assert.IsTrue(getDialogNodeResult.DialogNodeId == createDialogNode.DialogNode);
            Assert.IsTrue(getDialogNodeResult.Description == createDialogNode.Description);
        }

        [TestMethod]
        public void UpdateDialogNode_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            CreateDialogNode createDialogNode = new CreateDialogNode()
            {
                DialogNode = _dialogNodeName,
                Description = _dialogNodeDesc
            };
            var createDialogNodeResult = CreateDialogNode(workspaceId, createDialogNode);
            string updatedDialogNodeName = _dialogNodeName + "_updated";
            string updatedDialogNodeDescription = _dialogNodeDesc + "_updated";
            UpdateDialogNode updateDialogNode = new UpdateDialogNode()
            {
                DialogNode = updatedDialogNodeName,
                Description = updatedDialogNodeDescription
            };
            var updateDialogNodeResult = UpdateDialogNode(workspaceId, createDialogNode.DialogNode, updateDialogNode);
            var deleteDialogNodeResult = DeleteDialogNode(workspaceId, updateDialogNode.DialogNode);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(updateDialogNodeResult);
            Assert.IsTrue(updateDialogNodeResult.DialogNodeId == updateDialogNode.DialogNode);
            Assert.IsTrue(updateDialogNodeResult.Description == updateDialogNode.Description);
        }

        [TestMethod]
        public void DeleteDialogNode_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
                LearningOptOut = true
            };

            var createWorkspaceResult = CreateWorkspace(workspace);
            var workspaceId = createWorkspaceResult.WorkspaceId;
            CreateDialogNode createDialogNode = new CreateDialogNode()
            {
                DialogNode = _dialogNodeName,
                Description = _dialogNodeDesc
            };
            var createDialogNodeResult = CreateDialogNode(workspaceId, createDialogNode);
            var deleteDialogNodeResult = DeleteDialogNode(workspaceId, createDialogNode.DialogNode);
            var deleteWorkspaceResult = DeleteWorkspace(workspaceId);

            Assert.IsNotNull(deleteDialogNodeResult);
        }
        #endregion

        #region Logs
        [TestMethod]
        public void ListLogs_Success()
        {
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage,
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


        #region CreateWorkspace
        private Workspace CreateWorkspace(CreateWorkspace workspace)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to CreateWorkspace()");
            var result = service.CreateWorkspace(workspace);

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
        private object DeleteWorkspace(string workspaceId)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to DeleteWorkspace()");
            var result = service.DeleteWorkspace(workspaceId: workspaceId);

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
        private WorkspaceExport GetWorkspace(string workspaceId)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to GetWorkspace()");
            var result = service.GetWorkspace(workspaceId: workspaceId);

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
        private WorkspaceCollection ListWorkspaces()
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListWorkspaces()");
            var result = service.ListWorkspaces();

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
        private Workspace UpdateWorkspace(string workspaceId, UpdateWorkspace properties)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to UpdateWorkspace()");
            var result = service.UpdateWorkspace(workspaceId: workspaceId, properties: properties);

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

        #region Message
        private MessageResponse Message(string workspaceId, MessageRequest request)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to Message()");
            var result = service.Message(workspaceId: workspaceId, request: request);

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

        #region CreateIntent
        private Intent CreateIntent(string workspaceId, CreateIntent body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to CreateIntent()");
            var result = service.CreateIntent(workspaceId: workspaceId, body: body);

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
        private object DeleteIntent(string workspaceId, string intent)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to DeleteIntent()");
            var result = service.DeleteIntent(workspaceId: workspaceId, intent: intent);

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
        private IntentExport GetIntent(string workspaceId, string intent)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to GetIntent()");
            var result = service.GetIntent(workspaceId: workspaceId, intent: intent);

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
        private IntentCollection ListIntents(string workspaceId)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListIntents()");
            var result = service.ListIntents(workspaceId: workspaceId);

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
        private Intent UpdateIntent(string workspaceId, string intent, UpdateIntent body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to UpdateIntent()");
            var result = service.UpdateIntent(workspaceId: workspaceId, intent: intent, body: body);

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
        private Example CreateExample(string workspaceId, string intent, CreateExample body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to CreateExample()");
            var result = service.CreateExample(workspaceId: workspaceId, intent: intent, body: body);

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
        private object DeleteExample(string workspaceId, string intent, string text)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to DeleteExample()");
            var result = service.DeleteExample(workspaceId: workspaceId, intent: intent, text: text);

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
        private Example GetExample(string workspaceId, string intent, string text)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to GetExample()");
            var result = service.GetExample(workspaceId: workspaceId, intent: intent, text: text);

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
        private ExampleCollection ListExamples(string workspaceId, string intent)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListExamples()");
            var result = service.ListExamples(workspaceId: workspaceId, intent: intent);

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
        private Example UpdateExample(string workspaceId, string intent, string text, UpdateExample body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to UpdateExample()");
            var result = service.UpdateExample(workspaceId: workspaceId, intent: intent, text: text, body: body);

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

        #region CreateEntity
        private Entity CreateEntity(string workspaceId, CreateEntity properties)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to CreateEntity()");
            var result = service.CreateEntity(workspaceId: workspaceId, properties: properties);

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
        private object DeleteEntity(string workspaceId, string entity)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to DeleteEntity()");
            var result = service.DeleteEntity(workspaceId: workspaceId, entity: entity);

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
        private EntityExport GetEntity(string workspaceId, string entity)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to GetEntity()");
            var result = service.GetEntity(workspaceId: workspaceId, entity: entity);

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
        private EntityCollection ListEntities(string workspaceId)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListEntities()");
            var result = service.ListEntities(workspaceId: workspaceId);

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
        private Entity UpdateEntity(string workspaceId, string entity, UpdateEntity properties)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to UpdateEntity()");
            var result = service.UpdateEntity(workspaceId: workspaceId, entity: entity, properties: properties);

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

        #region CreateValue
        private Value CreateValue(string workspaceId, string entity, CreateValue body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to CreateValue()");
            var result = service.CreateValue(workspaceId: workspaceId, entity: entity, body: body);

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
        private object DeleteValue(string workspaceId, string entity, string value)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to DeleteValue()");
            var result = service.DeleteValue(workspaceId: workspaceId, entity: entity, value: value);

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
        private ValueExport GetValue(string workspaceId, string entity, string value)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to GetValue()");
            var result = service.GetValue(workspaceId: workspaceId, entity: entity, value: value);

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
        private ValueCollection ListValues(string workspaceId, string entity)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListValues()");
            var result = service.ListValues(workspaceId: workspaceId, entity: entity);

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
        private Value UpdateValue(string workspaceId, string entity, string value, UpdateValue body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to UpdateValue()");
            var result = service.UpdateValue(workspaceId: workspaceId, entity: entity, value: value, body: body);

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
        private Synonym CreateSynonym(string workspaceId, string entity, string value, CreateSynonym body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to CreateSynonym()");
            var result = service.CreateSynonym(workspaceId: workspaceId, entity: entity, value: value, body: body);

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
        private object DeleteSynonym(string workspaceId, string entity, string value, string synonym)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to DeleteSynonym()");
            var result = service.DeleteSynonym(workspaceId: workspaceId, entity: entity, value: value, synonym: synonym);

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
        private Synonym GetSynonym(string workspaceId, string entity, string value, string synonym)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to GetSynonym()");
            var result = service.GetSynonym(workspaceId: workspaceId, entity: entity, value: value, synonym: synonym);

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
        private SynonymCollection ListSynonyms(string workspaceId, string entity, string value)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListSynonyms()");
            var result = service.ListSynonyms(workspaceId: workspaceId, entity: entity, value: value);

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
        private Synonym UpdateSynonym(string workspaceId, string entity, string value, string synonym, UpdateSynonym body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to UpdateSynonym()");
            var result = service.UpdateSynonym(workspaceId: workspaceId, entity: entity, value: value, synonym: synonym, body: body);

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
        private DialogNode CreateDialogNode(string workspaceId, CreateDialogNode properties)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to CreateDialogNode()");
            var result = service.CreateDialogNode(workspaceId: workspaceId, properties: properties);

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
        private object DeleteDialogNode(string workspaceId, string dialogNode)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to DeleteDialogNode()");
            var result = service.DeleteDialogNode(workspaceId: workspaceId, dialogNode: dialogNode);

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
        private DialogNode GetDialogNode(string workspaceId, string dialogNode)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to GetDialogNode()");
            var result = service.GetDialogNode(workspaceId: workspaceId, dialogNode: dialogNode);

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
        private DialogNodeCollection ListDialogNodes(string workspaceId)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListDialogNodes()");
            var result = service.ListDialogNodes(workspaceId: workspaceId);

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
        private DialogNode UpdateDialogNode(string workspaceId, string dialogNode, UpdateDialogNode properties)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to UpdateDialogNode()");
            var result = service.UpdateDialogNode(workspaceId: workspaceId, dialogNode: dialogNode, properties: properties);

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
        private LogCollection ListAllLogs(string filter)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListAllLogs()");
            var result = service.ListAllLogs(filter: filter);

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
        private LogCollection ListLogs(string workspaceId)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListLogs()");
            var result = service.ListLogs(workspaceId: workspaceId);

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

        #region CreateCounterexample
        private Counterexample CreateCounterexample(string workspaceId, CreateCounterexample body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to CreateCounterexample()");
            var result = service.CreateCounterexample(workspaceId: workspaceId, body: body);

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
        private object DeleteCounterexample(string workspaceId, string text)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to DeleteCounterexample()");
            var result = service.DeleteCounterexample(workspaceId: workspaceId, text: text);

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
        private Counterexample GetCounterexample(string workspaceId, string text)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to GetCounterexample()");
            var result = service.GetCounterexample(workspaceId: workspaceId, text: text);

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
        private CounterexampleCollection ListCounterexamples(string workspaceId)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to ListCounterexamples()");
            var result = service.ListCounterexamples(workspaceId: workspaceId);

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
        private Counterexample UpdateCounterexample(string workspaceId, string text, UpdateCounterexample body)
        {
            Delay(_delayTimeInMilliseconds);
            Console.WriteLine("\nAttempting to UpdateCounterexample()");
            var result = service.UpdateCounterexample(workspaceId: workspaceId, text: text, body: body);

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

        #region Delay
        private void Delay(int delayTimeInMilliseconds)
        {
            System.Threading.Thread.Sleep(delayTimeInMilliseconds);
        }
        #endregion
    }
}
