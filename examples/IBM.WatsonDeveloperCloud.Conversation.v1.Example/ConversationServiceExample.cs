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

using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using System;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Example
{
    public class ConversationServiceExample
    {
        private ConversationService _conversation;

        //  The car example workspace
        private string _workspaceID;
        private string _inputString = "Turn on the winshield wipers";

        private string _createdWorkspaceName = "dotnet-sdk-example-workspace-delete";
        private string _createdWorkspaceDescription = "A Workspace created by the .NET SDK Conversation example script.";
        private string _createdWorkspaceLanguage = "en";
        private string _createdWorkspaceId;
        private string _createdEntity = "entity";
        private string _createdEntityDescription = "Entity created by the .NET SDK Conversation example script.";
        private string _createdValue = "value";
        private string _createdIntent = "intent";
        private string _createdIntentDescription = "Intent created by the .NET SDK Conversation example script.";
        private string _createdCounterExampleText = "Example text";
        private string _createdSynonym = "synonym";
        private string _createdExample = "example";

        #region Constructor
        public ConversationServiceExample(string username, string password, string workspaceID)
        {
            _conversation = new ConversationService(username, password, ConversationService.CONVERSATION_VERSION_DATE_2017_05_26);
            _workspaceID = workspaceID;
            //_conversation.Endpoint = "http://localhost:1234";

            ListWorkspaces();
            CreateWorkspace();
            GetWorkspace();
            UpdateWorkspace();

            Message();

            ListCounterExamples();
            CreateCounterExample();
            GetCounterExample();
            UpdateCounterExample();

            ListEntities();
            CreateEntity();
            GetEntity();
            UpdateEntity();

            ListValues();
            CreateValue();
            GetValue();
            UpdateValue();

            ListSynonyms();
            CreateSynonym();
            GetSynonym();
            UpdateSynonym();

            ListIntents();
            CreateIntent();
            GetIntent();
            UpdateIntent();

            ListExamples();
            CreateExample();
            GetExample();
            UpdateExample();

            ListLogEvents();

            DeleteExample();
            DeleteIntent();
            DeleteSynonym();
            DeleteValue();
            DeleteEntity();
            DeleteCounterExample();
            DeleteWorkspace();


            Console.WriteLine("\n~ Conversation examples complete.");
        }
        #endregion

        #region Message
        private void Message()
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
            var result = _conversation.Message(_workspaceID, messageRequest);

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
        private void ListWorkspaces()
        {
            Console.WriteLine("\nCalling ListWorkspaces()...");
            var result = _conversation.ListWorkspaces();

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

        private void CreateWorkspace()
        {
            Console.WriteLine("\nCalling CreateWorkspace()...");
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };

            var result = _conversation.CreateWorkspace(workspace);

            if(result != null)
            {
                Console.WriteLine(string.Format("Workspace Name: {0}, id: {1}, description: {2}", result.Name, result.WorkspaceId, result.Description));
                if (!string.IsNullOrEmpty(result.WorkspaceId))
                    _createdWorkspaceId = result.WorkspaceId;
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void GetWorkspace()
        {
            Console.WriteLine(string.Format("\nCalling GetWorkspace({0})...", _createdWorkspaceId));

            var result = _conversation.GetWorkspace(_createdWorkspaceId);

            if (result != null)
            {
                Console.WriteLine(string.Format("Workspace name: {0} | id: {1} | description: {2}", result.Name, result.WorkspaceId, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void UpdateWorkspace()
        {
            Console.WriteLine(string.Format("\nCalling UpdateWorkspace({0})...", _createdWorkspaceId));

            UpdateWorkspace workspace = new UpdateWorkspace()
            {
                Name = _createdWorkspaceName + "-updated",
                Description = _createdWorkspaceDescription + "-updated",
                Language = _createdWorkspaceLanguage
            };

            var result = _conversation.UpdateWorkspace(_createdWorkspaceId, workspace);

            if (result != null)
            {
                Console.WriteLine(string.Format("Updated Workspace name: {0} | id: {1} | description: {2}", result.Name, result.WorkspaceId, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void DeleteWorkspace()
        {
            Console.WriteLine(string.Format("\nCalling DeleteWorkspace({0})...", _createdWorkspaceId));
            var result = _conversation.DeleteWorkspace(_createdWorkspaceId);

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

        #region Counter Examples
        private void ListCounterExamples()
        {
            Console.WriteLine(string.Format("\nCalling ListCounterExamples({0})...", _createdWorkspaceId));
            
            var result = _conversation.ListCounterexamples(_createdWorkspaceId);

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

        private void CreateCounterExample()
        {
            Console.WriteLine("\nCalling CreateCounterExample()...");

            CreateCounterexample example = new CreateCounterexample()
            {
                Text = _createdCounterExampleText
            };

            var result = _conversation.CreateCounterexample(_createdWorkspaceId, example);

            if (result != null)
            {
                Console.WriteLine(string.Format("CounterExample name: {0}, created: {1}", result.Text, result.Created));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void GetCounterExample()
        {
            Console.WriteLine(string.Format("\nCalling GetCounterExample({0}, {1})...", _createdWorkspaceId, _createdCounterExampleText));
            var result = _conversation.GetCounterexample(_createdWorkspaceId, _createdCounterExampleText);

            if (result != null)
            {
                Console.WriteLine(string.Format("CounterExample name: {0}, created: {1}", result.Text, result.Created));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void UpdateCounterExample()
        {
            string updatedCounterExampleText = _createdCounterExampleText + "-updated";
            Console.WriteLine(string.Format("\nCalling UpdateCounterExample({0}, {1})...", _createdWorkspaceId, updatedCounterExampleText));
            UpdateCounterexample example = new UpdateCounterexample()
            {
                Text = updatedCounterExampleText
            };

            var result = _conversation.UpdateCounterexample(_createdWorkspaceId, _createdCounterExampleText, example);

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

        private void DeleteCounterExample()
        {
            Console.WriteLine(string.Format("\nCalling DeleteCounterExample({0}, {1})...", _createdWorkspaceId, _createdCounterExampleText));
            var result = _conversation.DeleteCounterexample(_createdWorkspaceId, _createdCounterExampleText);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted counterExample {0}.", _createdCounterExampleText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Entities
        private void ListEntities()
        {
            Console.WriteLine(string.Format("\nCalling ListEntities({0})...", _createdWorkspaceId));
            var result = _conversation.ListEntities(_createdWorkspaceId);

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

        private void CreateEntity()
        {
            Console.WriteLine(string.Format("\nCalling CreateEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
            CreateEntity entity = new CreateEntity()
            {
                Entity = _createdEntity,
                Description = _createdEntityDescription
            };

            var result = _conversation.CreateEntity(_createdWorkspaceId, entity);

            if (result != null)
            {
                Console.WriteLine(string.Format("entity: {0} | description: {1}", result.EntityName, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void GetEntity()
        {
            Console.WriteLine(string.Format("\nCalling GetEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
            var result = _conversation.GetEntity(_createdWorkspaceId, _createdEntity);

            if (result != null)
            {
                Console.WriteLine(string.Format("entity: {0} | description: {1}", result.EntityName, result.Description));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void UpdateEntity()
        {
            string updatedEntity = _createdEntity + "-updated";
            string updatedEntityDescription = _createdEntityDescription + "-updated";

            Console.WriteLine(string.Format("\nCalling UpdateEntity({0}, {1})...", _createdWorkspaceId, _createdEntity, updatedEntity));
            UpdateEntity entity = new UpdateEntity()
            {
                Entity = updatedEntity,
                Description = updatedEntityDescription
            };

            var result = _conversation.UpdateEntity(_createdWorkspaceId, _createdEntity, entity);

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

        private void DeleteEntity()
        {
            Console.WriteLine(string.Format("\nCalling DeleteEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
            var result = _conversation.DeleteEntity(_createdWorkspaceId, _createdEntity);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted entity {0}.", _createdEntity));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Values
        private void ListValues()
        {
            Console.WriteLine(string.Format("\nCalling ListValues({0}, {1})...", _createdWorkspaceId, _createdEntity));
            var result = _conversation.ListValues(_createdWorkspaceId, _createdEntity);

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

        private void CreateValue()
        {
            Console.WriteLine(string.Format("\nCalling CreateValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
            CreateValue value = new CreateValue()
            {
                Value = _createdValue
            };

            var result = _conversation.CreateValue(_createdWorkspaceId, _createdEntity, value);

            if (result != null)
            {
                Console.WriteLine(string.Format("value: {0}", result.ValueText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void GetValue()
        {
            Console.WriteLine(string.Format("\nCalling GetValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
            var result = _conversation.GetValue(_createdWorkspaceId, _createdEntity, _createdValue);

            if (result != null)
            {
                Console.WriteLine(string.Format("value: {0}", result.ValueText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void UpdateValue()
        {
            string updatedValue = _createdValue + "-updated";

            Console.WriteLine(string.Format("\nCalling UpdateValue({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, updatedValue));

            UpdateValue value = new UpdateValue()
            {
                Value = updatedValue
            };

            var result = _conversation.UpdateValue(_createdWorkspaceId, _createdEntity, _createdValue, value);

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

        private void DeleteValue()
        {
            Console.WriteLine(string.Format("\nCalling DeleteValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
            var result = _conversation.DeleteValue(_createdWorkspaceId, _createdEntity, _createdValue);

            if (result != null)
            {
                Console.WriteLine("Deleted value {0}", _createdValue);
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Synonyms
        private void ListSynonyms()
        {
            Console.WriteLine(string.Format("\nCalling ListSynonyms({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
            var result = _conversation.ListSynonyms(_createdWorkspaceId, _createdEntity, _createdValue);

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

        private void CreateSynonym()
        {
            Console.WriteLine(string.Format("\nCalling CreateSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
            CreateSynonym synonym = new CreateSynonym()
            {
                Synonym = _createdSynonym
            };

            var result = _conversation.CreateSynonym(_createdWorkspaceId, _createdEntity, _createdValue, synonym);

            if (result != null)
            {
                Console.WriteLine(string.Format("synonym: {0}", result.SynonymText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void GetSynonym()
        {
            Console.WriteLine(string.Format("\nCalling GetSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
            var result = _conversation.GetSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym);

            if (result != null)
            {
                Console.WriteLine(string.Format("synonym: {0}", result.SynonymText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void UpdateSynonym()
        {
            string updatedSynonym = _createdSynonym + "-updated";
            Console.WriteLine(string.Format("\nCalling UpdateSynonym({0}, {1}, {2}, {3}, {4})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym, updatedSynonym));

            UpdateSynonym synonym = new UpdateSynonym()
            {
                Synonym = updatedSynonym
            };

            var result = _conversation.UpdateSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym, synonym);

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

        private void DeleteSynonym()
        {
            Console.WriteLine(string.Format("\nCalling DeleteSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
            var result = _conversation.DeleteSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted synonym {0}", _createdSynonym));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Intents
        private void ListIntents()
        {
            Console.WriteLine(string.Format("\nCalling ListIntents({0})...", _createdWorkspaceId));
            var result = _conversation.ListIntents(_createdWorkspaceId);

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
        
        private void CreateIntent()
        {
            Console.WriteLine(string.Format("\nCalling CreateIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
            CreateIntent intent = new CreateIntent()
            {
                Intent = _createdIntent,
                Description = _createdIntentDescription
            };

            var result = _conversation.CreateIntent(_createdWorkspaceId, intent);

            if (result != null)
            {
                Console.WriteLine("intent: {0} | description: {1}", result.IntentName, result.Description);
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void GetIntent()
        {
            Console.WriteLine(string.Format("\nCalling GetIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
            var result = _conversation.GetIntent(_createdWorkspaceId, _createdIntent);

            if (result != null)
            {
                Console.WriteLine("intent: {0} | description: {1}", result.IntentName, result.Description);
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void UpdateIntent()
        {
            string updatedIntent = _createdIntent + "-updated";
            string updatedIntentDescription = _createdIntentDescription + "-updated";
            Console.WriteLine(string.Format("\nCalling UpdateIntent({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, updatedIntent));

            UpdateIntent intent = new UpdateIntent()
            {
                Intent = updatedIntent,
                Description = updatedIntentDescription
            };

            var result = _conversation.UpdateIntent(_createdWorkspaceId, _createdIntent, intent);

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

        private void DeleteIntent()
        {
            Console.WriteLine(string.Format("\nCalling DeleteIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
            var result = _conversation.DeleteIntent(_createdWorkspaceId, _createdIntent);

            if (result != null)
            {
                Console.WriteLine(string.Format("Deleted intent {0}", _createdIntent));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Examples
        private void ListExamples()
        {
            Console.WriteLine(string.Format("\nCalling ListExamples({0}, {1})...", _createdWorkspaceId, _createdIntent));
            var result = _conversation.ListExamples(_createdWorkspaceId, _createdIntent);

            if (result != null)
            {
                if (result.Examples != null && result.Examples.Count > 0)
                {
                    foreach (Model.Example example in result.Examples)
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

        private void CreateExample()
        {
            Console.WriteLine(string.Format("\nCalling CreateExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));

            CreateExample example = new CreateExample()
            {
                Text = _createdExample
            };

            var result = _conversation.CreateExample(_createdWorkspaceId, _createdIntent, example);

            if (result != null)
            {
                Console.WriteLine(string.Format("example: {0}", result.ExampleText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void GetExample()
        {
            Console.WriteLine(string.Format("\nCalling GetExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));
            var result = _conversation.GetExample(_createdWorkspaceId, _createdIntent, _createdExample);

            if (result != null)
            {
                Console.WriteLine(string.Format("example: {0}", result.ExampleText));
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }

        private void UpdateExample()
        {
            string updatedExample = _createdExample + "-updated";
            Console.WriteLine(string.Format("\nCalling UpdateExample({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdIntent, _createdExample, updatedExample));

            UpdateExample example = new UpdateExample()
            {
                Text = updatedExample
            };

            var result = _conversation.UpdateExample(_createdWorkspaceId, _createdIntent, _createdExample, example);

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

        private void DeleteExample()
        {
            Console.WriteLine(string.Format("\nCalling DeleteExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));
            var result = _conversation.DeleteExample(_createdWorkspaceId, _createdIntent, _createdExample);

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
        private void ListLogEvents()
        {
            Console.WriteLine(string.Format("\nCalling ListLogEvents({0})...", _createdWorkspaceId));
            var result = _conversation.ListLogs(_createdWorkspaceId);

            if (result != null)
            {
                if (result.Logs != null && result.Logs.Count > 0)
                {
                    foreach (LogExport log in result.Logs)
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
    }
}
