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

namespace IBM.WatsonDeveloperCloud.Conversation.v1.IntegratiationTests
{
    [TestClass]
    public class ConversationIntegrationTests
    {
        public string _username;
        public string _password;
        public string _endpoint;
        public ConversationService conversation;

        public string _workspaceID;
        public string _inputString = "Turn on the winshield wipers";

        public static string _createdWorkspaceName = "dotnet-sdk-example-workspace-delete";
        public static string _createdWorkspaceDescription = "A Workspace created by the .NET SDK Conversation example script.";
        public static string _createdWorkspaceLanguage = "en";
        public static string _createdWorkspaceId;
        public static string _createdEntity = "entity";
        public static string _createdEntityDescription = "Entity created by the .NET SDK Conversation example script.";
        public static string _createdValue = "value";
        public static string _createdIntent = "intent";
        public static string _createdIntentDescription = "Intent created by the .NET SDK Conversation example script.";
        public static string _createdCounterExampleText = "Example text";
        public static string _createdSynonym = "synonym";
        public static string _createdExample = "example";

        [TestInitialize]
        public void Setup()
        {
            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
                File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            _endpoint = vcapServices["conversation"][0]["credentials"]["url"].Value<string>();
            _username = vcapServices["conversation"][0]["credentials"]["username"].Value<string>();
            _password = vcapServices["conversation"][0]["credentials"]["password"].Value<string>();
            _workspaceID = vcapServices["conversation"][0]["credentials"]["workspaceId"].Value<string>();

            conversation = new ConversationService(_username, _password, ConversationService.CONVERSATION_VERSION_DATE_2017_05_26);
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

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Intents != null)
                {
                    foreach (RuntimeIntent intent in result.Intents)
                    {
                        Console.WriteLine(string.Format("intent: {0} | confidence: {1}", intent.Intent, intent.Confidence));
                    }
                }
                else
                {
                    Console.WriteLine("Intents is null.");
                }

                if (result.Output != null)
                {
                    if (result.Output.Text != null && result.Output.Text.Count > 0)
                    {
                        foreach (string output in result.Output.Text)
                            Console.WriteLine(string.Format("Output: \"{0}\"", output));
                    }
                    else
                    {
                        Console.WriteLine("There is no output.");
                    }
                }
                else
                {
                    Console.WriteLine("Output is null.");
                }
            }
            else
            {
                Console.WriteLine("Failed to message.");
            }
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
                    foreach (WorkspaceResponse workspace in result.Workspaces)
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
                    foreach (ExampleResponse counterExample in result.Counterexamples)
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

            CreateExample example = new CreateExample()
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
            UpdateExample example = new UpdateExample()
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
                    foreach (EntityExportResponse entity in result.Entities)
                        Console.WriteLine(string.Format("Entity: {0} | Created: {1}", entity.Entity, entity.Description));
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
                Console.WriteLine(string.Format("entity: {0} | description: {1}", result.Entity, result.Description));
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
                Console.WriteLine(string.Format("entity: {0} | description: {1}", result.Entity, result.Description));
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
                Console.WriteLine(string.Format("entity: {0} | description: {1}", result.Entity, result.Description));
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
                    foreach (ValueExportResponse value in result.Values)
                        Console.WriteLine(string.Format("value: {0} | Created: {1}", value.Value, value.Created));
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
                Console.WriteLine(string.Format("value: {0}", result.Value));
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
                Console.WriteLine(string.Format("value: {0}", result.Value));
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
                Console.WriteLine(string.Format("value: {0}", result.Value));
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
                    foreach (SynonymResponse synonym in result.Synonyms)
                        Console.WriteLine(string.Format("Synonym: {0} | Created: {1}", synonym.Synonym, synonym.Created));
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
                Console.WriteLine(string.Format("synonym: {0}", result.Synonym));
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
                Console.WriteLine(string.Format("synonym: {0}", result.Synonym));
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
                Console.WriteLine(string.Format("synonym: {0}", result.Synonym));
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
                    foreach (IntentExportResponse intent in result.Intents)
                        Console.WriteLine(string.Format("Intent: {0} | Created: {1}", intent.Intent, intent.Created));
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
                Console.WriteLine("intent: {0} | description: {1}", result.Intent, result.Description);
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
                Console.WriteLine("intent: {0} | description: {1}", result.Intent, result.Description);
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
                Console.WriteLine("intent: {0} | description: {1}", result.Intent, result.Description);
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
                    foreach (ExampleResponse example in result.Examples)
                        Console.WriteLine(string.Format("Example: {0} | Created: {1}", example.Text, example.Created));
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
                Console.WriteLine(string.Format("example: {0}", result.Text));
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
                Console.WriteLine(string.Format("example: {0}", result.Text));
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
                Console.WriteLine(string.Format("example: {0}", result.Text));
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

        #region Logs
        [TestMethod]
        public void ListLogEvents_Success()
        {
            Console.WriteLine(string.Format("\nCalling ListLogEvents({0})...", _createdWorkspaceId));
            var result = conversation.ListLogs(_createdWorkspaceId);

            Assert.IsNotNull(result);

            if (result != null)
            {
                if (result.Logs != null && result.Logs.Count > 0)
                {
                    foreach (LogExportResponse log in result.Logs)
                        Console.WriteLine(string.Format("Log: {0} | Request timestamp: {1}", log.LogId, log.RequestTimestamp));
                }
                else
                {
                    Console.WriteLine("There are no logs.");
                }
            }
            else
            {
                Console.WriteLine("Logs are null.");
            }
        }
        #endregion

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
    }
}
