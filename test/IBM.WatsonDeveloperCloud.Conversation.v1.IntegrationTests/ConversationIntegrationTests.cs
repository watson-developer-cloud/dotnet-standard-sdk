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
using System.IO;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.IntegratiationTests
{
    [TestClass]
    public class ConversationIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private ConversationService conversation;
        private static string credentials = string.Empty;

        private static string _workspaceID;
        private string _inputString = "Turn on the winshield wipers";

        private static string _createdWorkspaceName = "dotnet-sdk-example-workspace-delete";
        private static string _createdWorkspaceDescription = "A Workspace created by the .NET SDK Conversation example script.";
        private static string _createdWorkspaceLanguage = "en";
        private static string _createdWorkspaceId;
        private static string _createdEntity = "entity";
        private static string _createdEntityDescription = "Entity created by the .NET SDK Conversation example script.";
        private static string _createdValue = "value";
        private static string _createdIntent = "intent";
        private static string _createdIntentDescription = "Intent created by the .NET SDK Conversation example script.";
        private static string _createdCounterExampleText = "Example text";
        private static string _createdSynonym = "synonym";
        private static string _createdExample = "example";

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

            conversation = new ConversationService(_username, _password, ConversationService.CONVERSATION_VERSION_DATE_2017_05_26);
            conversation.Endpoint = _endpoint;
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

            Console.WriteLine(string.Format("\nCalling Message(\"{0}\")...", _inputString));
            var result = conversation.Message(_workspaceID, messageRequest);
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

            Assert.IsNotNull(result);
        }
        #endregion

        #region Workspaces
        [TestMethod]
        public void ListWorkspaces_Success()
        {
            Console.WriteLine("\nCalling ListWorkspaces()...");
            var result = conversation.ListWorkspaces();

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Workspaces != null && result.Workspaces.Count > 0)
                {
                    foreach (Workspace workspace in result.Workspaces)
                        Console.WriteLine(string.Format("Workspace name: {0} | WorkspaceID: {1}", workspace.Name, workspace.WorkspaceId));
                }
                else
                {
                    Console.WriteLine("There are no workspaces.");
                }
            }
            else
            {
                Console.WriteLine("Workspaces are null.");
            }
        }

        [TestMethod]
        public void CreateWorkspace_Success()
        {
            Console.WriteLine("\nCalling CreateWorkspace()...");
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };

            var result = conversation.CreateWorkspace(workspace);


            if (result != null)
            {
                Console.WriteLine(string.Format("Workspace Name: {0}, id: {1}, description: {2}", result.Name, result.WorkspaceId, result.Description));
                if (!string.IsNullOrEmpty(result.WorkspaceId))
                    _createdWorkspaceId = result.WorkspaceId;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetWorkspace_Success()
        {
            Console.WriteLine(string.Format("\nCalling GetWorkspace({0})...", _createdWorkspaceId));

            var result = conversation.GetWorkspace(_createdWorkspaceId);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("Workspace name: {0} | id: {1} | description: {2}", result.Name, result.WorkspaceId, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void UpdateWorkspace_Success()
        {
            Console.WriteLine(string.Format("\nCalling UpdateWorkspace({0})...", _createdWorkspaceId));

            UpdateWorkspace workspace = new UpdateWorkspace()
            {
                Name = _createdWorkspaceName + "-updated",
                Description = _createdWorkspaceDescription + "-updated",
                Language = _createdWorkspaceLanguage
            };

            var result = conversation.UpdateWorkspace(_createdWorkspaceId, workspace);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("Updated Workspace name: {0} | id: {1} | description: {2}", result.Name, result.WorkspaceId, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Counter Examples
        [TestMethod]
        public void ListCounterExamples_Success()
        {
            Console.WriteLine(string.Format("\nCalling ListCounterExamples({0})...", _createdWorkspaceId));

            var result = conversation.ListCounterexamples(_createdWorkspaceId);

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Counterexamples.Count > 0)
                {
                    foreach (Counterexample counterExample in result.Counterexamples)
                        Console.WriteLine(string.Format("CounterExample name: {0} | Created: {1}", counterExample.Text, counterExample.Created));
                }
                else
                {
                    Console.WriteLine("There are no counter examples.");
                }
            }
            else
            {
                Console.WriteLine("CounterExamples are null.");
            }
        }

        [TestMethod]
        public void CreateCounterExample_Success()
        {
            Console.WriteLine("\nCalling CreateCounterExample()...");

            CreateCounterexample example = new CreateCounterexample()
            {
                Text = _createdCounterExampleText
            };

            var result = conversation.CreateCounterexample(_createdWorkspaceId, example);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("CounterExample name: {0}, created: {1}", result.Text, result.Created));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void GetCounterExample_Success()
        {
            Console.WriteLine(string.Format("\nCalling GetCounterExample({0}, {1})...", _createdWorkspaceId, _createdCounterExampleText));
            var result = conversation.GetCounterexample(_createdWorkspaceId, _createdCounterExampleText);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("CounterExample name: {0}, created: {1}", result.Text, result.Created));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void UpdateCounterExample_Success()
        {
            string updatedCounterExampleText = _createdCounterExampleText + "-updated";
            Console.WriteLine(string.Format("\nCalling UpdateCounterExample({0}, {1})...", _createdWorkspaceId, updatedCounterExampleText));
            UpdateCounterexample example = new UpdateCounterexample()
            {
                Text = updatedCounterExampleText
            };

            var result = conversation.UpdateCounterexample(_createdWorkspaceId, _createdCounterExampleText, example);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("CounterExample name: {0}, created: {1}", result.Text, result.Created));
                _createdCounterExampleText = updatedCounterExampleText;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Entities
        [TestMethod]
        public void ListEntities_Success()
        {
            Console.WriteLine(string.Format("\nCalling ListEntities({0})...", _createdWorkspaceId));
            var result = conversation.ListEntities(_createdWorkspaceId);

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Entities != null && result.Entities.Count > 0)
                {
                    foreach (EntityExport entity in result.Entities)
                        Console.WriteLine(string.Format("Entity: {0} | Created: {1}", entity.EntityName, entity.Description));
                }
                else
                {
                    Console.WriteLine("There are no entities.");
                }
            }
            else
            {
                Console.WriteLine("Entities are null.");
            }
        }

        [TestMethod]
        public void CreateEntity_Success()
        {
            Console.WriteLine(string.Format("\nCalling CreateEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var result = conversation.CreateEntity(_createdWorkspaceId, entity);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("entity: {0} | description: {1}", result.EntityName, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void GetEntity_Success()
        {
            Console.WriteLine(string.Format("\nCalling GetEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
            var result = conversation.GetEntity(_createdWorkspaceId, _createdEntity);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("entity: {0} | description: {1}", result.EntityName, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void UpdateEntity_Success()
        {
            string updatedEntity = _createdEntity + "-updated";
            string updatedEntityDescription = _createdEntityDescription + "-updated";

            Console.WriteLine(string.Format("\nCalling UpdateEntity({0}, {1})...", _createdWorkspaceId, _createdEntity, updatedEntity));
            UpdateEntity entity = new UpdateEntity()
            {
                Entity = updatedEntity,
                Description = updatedEntityDescription
            };

            var result = conversation.UpdateEntity(_createdWorkspaceId, _createdEntity, entity);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("entity: {0} | description: {1}", result.EntityName, result.Description));
                _createdEntity = updatedEntity;
                _createdEntityDescription = updatedEntityDescription;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Values
        [TestMethod]
        public void ListValues_Success()
        {
            Console.WriteLine(string.Format("\nCalling ListValues({0}, {1})...", _createdWorkspaceId, _createdEntity));
            var result = conversation.ListValues(_createdWorkspaceId, _createdEntity);

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Values != null && result.Values.Count > 0)
                {
                    foreach (ValueExport value in result.Values)
                        Console.WriteLine(string.Format("value: {0} | Created: {1}", value.ValueText, value.Created));
                }
                else
                {
                    Console.WriteLine("There are no values.");
                }
            }
            else
            {
                Console.WriteLine("Values are null.");
            }
        }

        [TestMethod]
        public void CreateValue_Success()
        {
            Console.WriteLine(string.Format("\nCalling CreateValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var result = conversation.CreateValue(_createdWorkspaceId, _createdEntity, value);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("value: {0}", result.ValueText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void GetValue_Success()
        {
            Console.WriteLine(string.Format("\nCalling GetValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
            var result = conversation.GetValue(_createdWorkspaceId, _createdEntity, _createdValue);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("value: {0}", result.ValueText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void UpdateValue_Success()
        {
            string updatedValue = _createdValue + "-updated";

            Console.WriteLine(string.Format("\nCalling UpdateValue({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, updatedValue));

            UpdateValue value = new UpdateValue()
            {
                Value = updatedValue
            };

            var result = conversation.UpdateValue(_createdWorkspaceId, _createdEntity, _createdValue, value);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("value: {0}", result.ValueText));
                _createdValue = updatedValue;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Synonyms
        [TestMethod]
        public void ListSynonyms_Success()
        {
            Console.WriteLine(string.Format("\nCalling ListSynonyms({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
            var result = conversation.ListSynonyms(_createdWorkspaceId, _createdEntity, _createdValue);

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Synonyms != null && result.Synonyms.Count > 0)
                {
                    foreach (Synonym synonym in result.Synonyms)
                        Console.WriteLine(string.Format("Synonym: {0} | Created: {1}", synonym.SynonymText, synonym.Created));
                }
                else
                {
                    Console.WriteLine("There are no synonyms.");
                }
            }
            else
            {
                Console.WriteLine("Synonyms are null.");
            }
        }

        [TestMethod]
        public void CreateSynonym_Success()
        {
            Console.WriteLine(string.Format("\nCalling CreateSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = _createdSynonym
            };

            var result = conversation.CreateSynonym(_createdWorkspaceId, _createdEntity, _createdValue, synonym);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("synonym: {0}", result.SynonymText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void GetSynonym_Success()
        {
            Console.WriteLine(string.Format("\nCalling GetSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
            var result = conversation.GetSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("synonym: {0}", result.SynonymText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void UpdateSynonym_Success()
        {
            string updatedSynonym = _createdSynonym + "-updated";
            Console.WriteLine(string.Format("\nCalling UpdateSynonym({0}, {1}, {2}, {3}, {4})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym, updatedSynonym));

            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = updatedSynonym
            };

            var result = conversation.UpdateSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym, synonym);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("synonym: {0}", result.SynonymText));
                _createdSynonym = updatedSynonym;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Intents
        [TestMethod]
        public void ListIntents_Success()
        {
            Console.WriteLine(string.Format("\nCalling ListIntents({0})...", _createdWorkspaceId));
            var result = conversation.ListIntents(_createdWorkspaceId);

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Intents != null && result.Intents.Count > 0)
                {
                    foreach (IntentExport intent in result.Intents)
                        Console.WriteLine(string.Format("Intent: {0} | Created: {1}", intent.IntentName, intent.Created));
                }
                else
                {
                    Console.WriteLine("There are no intents.");
                }
            }
            else
            {
                Console.WriteLine("Intents are null.");
            }
        }

        [TestMethod]
        public void CreateIntent_Success()
        {
            Console.WriteLine(string.Format("\nCalling CreateIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
            CreateIntent intent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var result = conversation.CreateIntent(_createdWorkspaceId, intent);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine("intent: {0} | description: {1}", result.IntentName, result.Description);
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void GetIntent_Success()
        {
            Console.WriteLine(string.Format("\nCalling GetIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
            var result = conversation.GetIntent(_createdWorkspaceId, _createdIntent);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine("intent: {0} | description: {1}", result.IntentName, result.Description);
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void UpdateIntent_Success()
        {
            string updatedIntent = _createdIntent + "-updated";
            string updatedIntentDescription = _createdIntentDescription + "-updated";
            Console.WriteLine(string.Format("\nCalling UpdateIntent({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, updatedIntent));

            UpdateIntent intent = new UpdateIntent()
            {
                Intent = updatedIntent,
                Description = updatedIntentDescription
            };

            var result = conversation.UpdateIntent(_createdWorkspaceId, _createdIntent, intent);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine("intent: {0} | description: {1}", result.IntentName, result.Description);
                _createdIntent = updatedIntent;
                _createdIntentDescription = updatedIntentDescription;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Examples
        [TestMethod]
        public void ListExamples_Success()
        {
            Console.WriteLine(string.Format("\nCalling ListExamples({0}, {1})...", _createdWorkspaceId, _createdIntent));
            var result = conversation.ListExamples(_createdWorkspaceId, _createdIntent);

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Examples != null && result.Examples.Count > 0)
                {
                    foreach (Example example in result.Examples)
                        Console.WriteLine(string.Format("Example: {0} | Created: {1}", example.ExampleText, example.Created));
                }
                else
                {
                    Console.WriteLine("There are no examples.");
                }
            }
            else
            {
                Console.WriteLine("Examples are null.");
            }
        }

        [TestMethod]
        public void CreateExample_Success()
        {
            Console.WriteLine(string.Format("\nCalling CreateExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));

            CreateExample example = new CreateExample()
            {
                Text = _createdExample
            };

            var result = conversation.CreateExample(_createdWorkspaceId, _createdIntent, example);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("example: {0}", result.ExampleText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void GetExample_Success()
        {
            Console.WriteLine(string.Format("\nCalling GetExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));
            var result = conversation.GetExample(_createdWorkspaceId, _createdIntent, _createdExample);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("example: {0}", result.ExampleText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void UpdateExample_Success()
        {
            string updatedExample = _createdExample + "-updated";
            Console.WriteLine(string.Format("\nCalling UpdateExample({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdIntent, _createdExample, updatedExample));

            UpdateExample example = new UpdateExample()
            {
                Text = updatedExample
            };

            var result = conversation.UpdateExample(_createdWorkspaceId, _createdIntent, _createdExample, example);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("example: {0}", result.ExampleText));
                _createdExample = updatedExample;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void DeleteExample_Success()
        {
            Console.WriteLine(string.Format("\nCalling DeleteExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));
            var result = conversation.DeleteExample(_createdWorkspaceId, _createdIntent, _createdExample);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted example {0}.", _createdExample));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        [TestMethod]
        public void ListLogEventsSuccess()
        {
            Console.WriteLine("Running Test ListLogEventsSuccess.");
            var result = ListLogEvents();
            Assert.IsNotNull(result);
        }

        #region Delete
        [TestMethod]
        public void DeleteIntent_Success()
        {
            Console.WriteLine(string.Format("\nCalling DeleteIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
            var result = conversation.DeleteIntent(_createdWorkspaceId, _createdIntent);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted intent {0}", _createdIntent));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void DeleteSynonym_Success()
        {
            Console.WriteLine(string.Format("\nCalling DeleteSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
            var result = conversation.DeleteSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted synonym {0}", _createdSynonym));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void DeleteValue_Success()
        {
            Console.WriteLine(string.Format("\nCalling DeleteValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
            var result = conversation.DeleteValue(_createdWorkspaceId, _createdEntity, _createdValue);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine("Deleted value {0}", _createdValue);
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void DeleteEntity_Success()
        {
            Console.WriteLine(string.Format("\nCalling DeleteEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
            var result = conversation.DeleteEntity(_createdWorkspaceId, _createdEntity);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted entity {0}.", _createdEntity));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void DeleteCounterExample_Success()
        {
            Console.WriteLine(string.Format("\nCalling DeleteCounterExample({0}, {1})...", _createdWorkspaceId, _createdCounterExampleText));
            var result = conversation.DeleteCounterexample(_createdWorkspaceId, _createdCounterExampleText);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted counterExample {0}.", _createdCounterExampleText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        [TestMethod]
        public void DeleteWorkspace_Success()
        {
            Console.WriteLine(string.Format("\nCalling DeleteWorkspace({0})...", _createdWorkspaceId));
            var result = conversation.DeleteWorkspace(_createdWorkspaceId);

            Assert.IsNotNull(result);

            if (result != null)
            {
                Console.WriteLine(string.Format("Workspace {0} deleted.", _createdWorkspaceId));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region CreateWorkspace
        private Workspace CreateWorkspace()
        {
            Console.WriteLine("Attempting to CreateWorkspace()");
            var result = ConversationService.CreateWorkspace(();

            if (result != null)
            {
                Console.WriteLine("CreateWorkspace() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to CreateWorkspace()");
            }

            return result;
        }
        #endregion

        #region DeleteWorkspace
        private object DeleteWorkspace()
        {
            Console.WriteLine("Attempting to DeleteWorkspace()");
            var result = ConversationService.DeleteWorkspace((workspaceId:);

            if (result != null)
            {
                Console.WriteLine("DeleteWorkspace() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to DeleteWorkspace()");
            }

            return result;
        }
        #endregion

        #region GetWorkspace
        private WorkspaceExport GetWorkspace()
        {
            Console.WriteLine("Attempting to GetWorkspace()");
            var result = ConversationService.GetWorkspace((workspaceId:, );

            if (result != null)
            {
                Console.WriteLine("GetWorkspace() succeeded");
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
            Console.WriteLine("Attempting to ListWorkspaces()");
            var result = ConversationService.ListWorkspaces(();

            if (result != null)
            {
                Console.WriteLine("ListWorkspaces() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListWorkspaces()");
            }

            return result;
        }
        #endregion

        #region UpdateWorkspace
        private Workspace UpdateWorkspace()
        {
            Console.WriteLine("Attempting to UpdateWorkspace()");
            var result = ConversationService.UpdateWorkspace((workspaceId:, );

            if (result != null)
            {
                Console.WriteLine("UpdateWorkspace() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to UpdateWorkspace()");
            }

            return result;
        }
        #endregion

        #region Message
        private MessageResponse Message()
        {
            Console.WriteLine("Attempting to Message()");
            var result = ConversationService.Message((workspaceId:, );

            if (result != null)
            {
                Console.WriteLine("Message() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to Message()");
            }

            return result;
        }
        #endregion

        #region CreateIntent
        private Intent CreateIntent()
        {
            Console.WriteLine("Attempting to CreateIntent()");
            var result = ConversationService.CreateIntent((workspaceId:, body:);

            if (result != null)
            {
                Console.WriteLine("CreateIntent() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to CreateIntent()");
            }

            return result;
        }
        #endregion

        #region DeleteIntent
        private object DeleteIntent()
        {
            Console.WriteLine("Attempting to DeleteIntent()");
            var result = ConversationService.DeleteIntent((workspaceId:, intent:);

            if (result != null)
            {
                Console.WriteLine("DeleteIntent() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to DeleteIntent()");
            }

            return result;
        }
        #endregion

        #region GetIntent
        private IntentExport GetIntent()
        {
            Console.WriteLine("Attempting to GetIntent()");
            var result = ConversationService.GetIntent((workspaceId:, intent:, );

            if (result != null)
            {
                Console.WriteLine("GetIntent() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to GetIntent()");
            }

            return result;
        }
        #endregion

        #region ListIntents
        private IntentCollection ListIntents()
        {
            Console.WriteLine("Attempting to ListIntents()");
            var result = ConversationService.ListIntents((workspaceId:, );

            if (result != null)
            {
                Console.WriteLine("ListIntents() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListIntents()");
            }

            return result;
        }
        #endregion

        #region UpdateIntent
        private Intent UpdateIntent()
        {
            Console.WriteLine("Attempting to UpdateIntent()");
            var result = ConversationService.UpdateIntent((workspaceId:, intent:, body:);

            if (result != null)
            {
                Console.WriteLine("UpdateIntent() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to UpdateIntent()");
            }

            return result;
        }
        #endregion

        #region CreateExample
        private Example CreateExample()
        {
            Console.WriteLine("Attempting to CreateExample()");
            var result = ConversationService.CreateExample((workspaceId:, intent:, body:);

            if (result != null)
            {
                Console.WriteLine("CreateExample() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to CreateExample()");
            }

            return result;
        }
        #endregion

        #region DeleteExample
        private object DeleteExample()
        {
            Console.WriteLine("Attempting to DeleteExample()");
            var result = ConversationService.DeleteExample((workspaceId:, intent:, text:);

            if (result != null)
            {
                Console.WriteLine("DeleteExample() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to DeleteExample()");
            }

            return result;
        }
        #endregion

        #region GetExample
        private Example GetExample()
        {
            Console.WriteLine("Attempting to GetExample()");
            var result = ConversationService.GetExample((workspaceId:, intent:, text:);

            if (result != null)
            {
                Console.WriteLine("GetExample() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to GetExample()");
            }

            return result;
        }
        #endregion

        #region ListExamples
        private ExampleCollection ListExamples()
        {
            Console.WriteLine("Attempting to ListExamples()");
            var result = ConversationService.ListExamples((workspaceId:, intent:, );

            if (result != null)
            {
                Console.WriteLine("ListExamples() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListExamples()");
            }

            return result;
        }
        #endregion

        #region UpdateExample
        private Example UpdateExample()
        {
            Console.WriteLine("Attempting to UpdateExample()");
            var result = ConversationService.UpdateExample((workspaceId:, intent:, text:, body:);

            if (result != null)
            {
                Console.WriteLine("UpdateExample() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to UpdateExample()");
            }

            return result;
        }
        #endregion

        #region CreateEntity
        private Entity CreateEntity()
        {
            Console.WriteLine("Attempting to CreateEntity()");
            var result = ConversationService.CreateEntity((workspaceId:, properties:);

            if (result != null)
            {
                Console.WriteLine("CreateEntity() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to CreateEntity()");
            }

            return result;
        }
        #endregion

        #region DeleteEntity
        private object DeleteEntity()
        {
            Console.WriteLine("Attempting to DeleteEntity()");
            var result = ConversationService.DeleteEntity((workspaceId:, entity:);

            if (result != null)
            {
                Console.WriteLine("DeleteEntity() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to DeleteEntity()");
            }

            return result;
        }
        #endregion

        #region GetEntity
        private EntityExport GetEntity()
        {
            Console.WriteLine("Attempting to GetEntity()");
            var result = ConversationService.GetEntity((workspaceId:, entity:, );

            if (result != null)
            {
                Console.WriteLine("GetEntity() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to GetEntity()");
            }

            return result;
        }
        #endregion

        #region ListEntities
        private EntityCollection ListEntities()
        {
            Console.WriteLine("Attempting to ListEntities()");
            var result = ConversationService.ListEntities((workspaceId:, );

            if (result != null)
            {
                Console.WriteLine("ListEntities() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListEntities()");
            }

            return result;
        }
        #endregion

        #region UpdateEntity
        private Entity UpdateEntity()
        {
            Console.WriteLine("Attempting to UpdateEntity()");
            var result = ConversationService.UpdateEntity((workspaceId:, entity:, properties:);

            if (result != null)
            {
                Console.WriteLine("UpdateEntity() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to UpdateEntity()");
            }

            return result;
        }
        #endregion

        #region CreateValue
        private Value CreateValue()
        {
            Console.WriteLine("Attempting to CreateValue()");
            var result = ConversationService.CreateValue((workspaceId:, entity:, body:);

            if (result != null)
            {
                Console.WriteLine("CreateValue() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to CreateValue()");
            }

            return result;
        }
        #endregion

        #region DeleteValue
        private object DeleteValue()
        {
            Console.WriteLine("Attempting to DeleteValue()");
            var result = ConversationService.DeleteValue((workspaceId:, entity:, value:);

            if (result != null)
            {
                Console.WriteLine("DeleteValue() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to DeleteValue()");
            }

            return result;
        }
        #endregion

        #region GetValue
        private ValueExport GetValue()
        {
            Console.WriteLine("Attempting to GetValue()");
            var result = ConversationService.GetValue((workspaceId:, entity:, value:, );

            if (result != null)
            {
                Console.WriteLine("GetValue() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to GetValue()");
            }

            return result;
        }
        #endregion

        #region ListValues
        private ValueCollection ListValues()
        {
            Console.WriteLine("Attempting to ListValues()");
            var result = ConversationService.ListValues((workspaceId:, entity:, );

            if (result != null)
            {
                Console.WriteLine("ListValues() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListValues()");
            }

            return result;
        }
        #endregion

        #region UpdateValue
        private Value UpdateValue()
        {
            Console.WriteLine("Attempting to UpdateValue()");
            var result = ConversationService.UpdateValue((workspaceId:, entity:, value:, body:);

            if (result != null)
            {
                Console.WriteLine("UpdateValue() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to UpdateValue()");
            }

            return result;
        }
        #endregion

        #region CreateSynonym
        private Synonym CreateSynonym()
        {
            Console.WriteLine("Attempting to CreateSynonym()");
            var result = ConversationService.CreateSynonym((workspaceId:, entity:, value:, body:);

            if (result != null)
            {
                Console.WriteLine("CreateSynonym() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to CreateSynonym()");
            }

            return result;
        }
        #endregion

        #region DeleteSynonym
        private object DeleteSynonym()
        {
            Console.WriteLine("Attempting to DeleteSynonym()");
            var result = ConversationService.DeleteSynonym((workspaceId:, entity:, value:, synonym:);

            if (result != null)
            {
                Console.WriteLine("DeleteSynonym() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to DeleteSynonym()");
            }

            return result;
        }
        #endregion

        #region GetSynonym
        private Synonym GetSynonym()
        {
            Console.WriteLine("Attempting to GetSynonym()");
            var result = ConversationService.GetSynonym((workspaceId:, entity:, value:, synonym:);

            if (result != null)
            {
                Console.WriteLine("GetSynonym() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to GetSynonym()");
            }

            return result;
        }
        #endregion

        #region ListSynonyms
        private SynonymCollection ListSynonyms()
        {
            Console.WriteLine("Attempting to ListSynonyms()");
            var result = ConversationService.ListSynonyms((workspaceId:, entity:, value:, );

            if (result != null)
            {
                Console.WriteLine("ListSynonyms() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListSynonyms()");
            }

            return result;
        }
        #endregion

        #region UpdateSynonym
        private Synonym UpdateSynonym()
        {
            Console.WriteLine("Attempting to UpdateSynonym()");
            var result = ConversationService.UpdateSynonym((workspaceId:, entity:, value:, synonym:, body:);

            if (result != null)
            {
                Console.WriteLine("UpdateSynonym() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to UpdateSynonym()");
            }

            return result;
        }
        #endregion

        #region CreateDialogNode
        private DialogNode CreateDialogNode()
        {
            Console.WriteLine("Attempting to CreateDialogNode()");
            var result = ConversationService.CreateDialogNode((workspaceId:, properties:);

            if (result != null)
            {
                Console.WriteLine("CreateDialogNode() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to CreateDialogNode()");
            }

            return result;
        }
        #endregion

        #region DeleteDialogNode
        private object DeleteDialogNode()
        {
            Console.WriteLine("Attempting to DeleteDialogNode()");
            var result = ConversationService.DeleteDialogNode((workspaceId:, dialogNode:);

            if (result != null)
            {
                Console.WriteLine("DeleteDialogNode() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to DeleteDialogNode()");
            }

            return result;
        }
        #endregion

        #region GetDialogNode
        private DialogNode GetDialogNode()
        {
            Console.WriteLine("Attempting to GetDialogNode()");
            var result = ConversationService.GetDialogNode((workspaceId:, dialogNode:);

            if (result != null)
            {
                Console.WriteLine("GetDialogNode() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to GetDialogNode()");
            }

            return result;
        }
        #endregion

        #region ListDialogNodes
        private DialogNodeCollection ListDialogNodes()
        {
            Console.WriteLine("Attempting to ListDialogNodes()");
            var result = ConversationService.ListDialogNodes((workspaceId:, );

            if (result != null)
            {
                Console.WriteLine("ListDialogNodes() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListDialogNodes()");
            }

            return result;
        }
        #endregion

        #region UpdateDialogNode
        private DialogNode UpdateDialogNode()
        {
            Console.WriteLine("Attempting to UpdateDialogNode()");
            var result = ConversationService.UpdateDialogNode((workspaceId:, dialogNode:, properties:);

            if (result != null)
            {
                Console.WriteLine("UpdateDialogNode() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to UpdateDialogNode()");
            }

            return result;
        }
        #endregion

        #region ListAllLogs
        private LogCollection ListAllLogs()
        {
            Console.WriteLine("Attempting to ListAllLogs()");
            var result = ConversationService.ListAllLogs((filter:, );

            if (result != null)
            {
                Console.WriteLine("ListAllLogs() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListAllLogs()");
            }

            return result;
        }
        #endregion

        #region ListLogs
        private LogCollection ListLogs()
        {
            Console.WriteLine("Attempting to ListLogs()");
            var result = ConversationService.ListLogs((workspaceId:, );

            if (result != null)
            {
                Console.WriteLine("ListLogs() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListLogs()");
            }

            return result;
        }
        #endregion

        #region CreateCounterexample
        private Counterexample CreateCounterexample()
        {
            Console.WriteLine("Attempting to CreateCounterexample()");
            var result = ConversationService.CreateCounterexample((workspaceId:, body:);

            if (result != null)
            {
                Console.WriteLine("CreateCounterexample() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to CreateCounterexample()");
            }

            return result;
        }
        #endregion

        #region DeleteCounterexample
        private object DeleteCounterexample()
        {
            Console.WriteLine("Attempting to DeleteCounterexample()");
            var result = ConversationService.DeleteCounterexample((workspaceId:, text:);

            if (result != null)
            {
                Console.WriteLine("DeleteCounterexample() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to DeleteCounterexample()");
            }

            return result;
        }
        #endregion

        #region GetCounterexample
        private Counterexample GetCounterexample()
        {
            Console.WriteLine("Attempting to GetCounterexample()");
            var result = ConversationService.GetCounterexample((workspaceId:, text:);

            if (result != null)
            {
                Console.WriteLine("GetCounterexample() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to GetCounterexample()");
            }

            return result;
        }
        #endregion

        #region ListCounterexamples
        private CounterexampleCollection ListCounterexamples()
        {
            Console.WriteLine("Attempting to ListCounterexamples()");
            var result = ConversationService.ListCounterexamples((workspaceId:, );

            if (result != null)
            {
                Console.WriteLine("ListCounterexamples() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to ListCounterexamples()");
            }

            return result;
        }
        #endregion

        #region UpdateCounterexample
        private Counterexample UpdateCounterexample()
        {
            Console.WriteLine("Attempting to UpdateCounterexample()");
            var result = ConversationService.UpdateCounterexample((workspaceId:, text:, body:);

            if (result != null)
            {
                Console.WriteLine("UpdateCounterexample() succeeded");
            }
            else
            {
                Console.WriteLine("Failed to UpdateCounterexample()");
            }

            return result;
        }
        #endregion
    }
}
