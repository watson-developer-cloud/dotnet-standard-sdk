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
        /* Old tests
        //#region Workspaces
        //[TestMethod]
        //public void ListWorkspaces_Success()
        //{
        //    Console.WriteLine("\nCalling ListWorkspaces()...");
        //    var result = service.ListWorkspaces();

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        if (result.Workspaces != null && result.Workspaces.Count > 0)
        //        {
        //            foreach (Workspace workspace in result.Workspaces)
        //                Console.WriteLine(string.Format("Workspace name: {0} | WorkspaceID: {1}", workspace.Name, workspace.WorkspaceId));
        //        }
        //        else
        //        {
        //            Console.WriteLine("There are no workspaces.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Workspaces are null.");
        //    }
        //}

        //[TestMethod]
        //public void CreateWorkspace_Success()
        //{
        //    Console.WriteLine("\nCalling CreateWorkspace()...");
        //    CreateWorkspace workspace = new CreateWorkspace()
        //    {
        //        Name = _createdWorkspaceName,
        //        Description = _createdWorkspaceDescription,
        //        Language = _createdWorkspaceLanguage
        //    };

        //    var result = service.CreateWorkspace(workspace);


        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Workspace Name: {0}, id: {1}, description: {2}", result.Name, result.WorkspaceId, result.Description));
        //        if (!string.IsNullOrEmpty(result.WorkspaceId))
        //            _createdWorkspaceId = result.WorkspaceId;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetWorkspace_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling GetWorkspace({0})...", _createdWorkspaceId));

        //    var result = service.GetWorkspace(_createdWorkspaceId);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Workspace name: {0} | id: {1} | description: {2}", result.Name, result.WorkspaceId, result.Description));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void UpdateWorkspace_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling UpdateWorkspace({0})...", _createdWorkspaceId));

        //    UpdateWorkspace workspace = new UpdateWorkspace()
        //    {
        //        Name = _createdWorkspaceName + "-updated",
        //        Description = _createdWorkspaceDescription + "-updated",
        //        Language = _createdWorkspaceLanguage
        //    };

        //    var result = service.UpdateWorkspace(_createdWorkspaceId, workspace);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Updated Workspace name: {0} | id: {1} | description: {2}", result.Name, result.WorkspaceId, result.Description));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}
        //#endregion

        //#region Counter Examples
        //[TestMethod]
        //public void ListCounterExamples_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling ListCounterExamples({0})...", _createdWorkspaceId));

        //    var result = service.ListCounterexamples(_createdWorkspaceId);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        if (result.Counterexamples.Count > 0)
        //        {
        //            foreach (Counterexample counterExample in result.Counterexamples)
        //                Console.WriteLine(string.Format("CounterExample name: {0} | Created: {1}", counterExample.Text, counterExample.Created));
        //        }
        //        else
        //        {
        //            Console.WriteLine("There are no counter examples.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("CounterExamples are null.");
        //    }
        //}

        //[TestMethod]
        //public void CreateCounterExample_Success()
        //{
        //    Console.WriteLine("\nCalling CreateCounterExample()...");

        //    CreateCounterexample example = new CreateCounterexample()
        //    {
        //        Text = _createdCounterExampleText
        //    };

        //    var result = service.CreateCounterexample(_createdWorkspaceId, example);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("CounterExample name: {0}, created: {1}", result.Text, result.Created));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void GetCounterExample_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling GetCounterExample({0}, {1})...", _createdWorkspaceId, _createdCounterExampleText));
        //    var result = service.GetCounterexample(_createdWorkspaceId, _createdCounterExampleText);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("CounterExample name: {0}, created: {1}", result.Text, result.Created));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void UpdateCounterExample_Success()
        //{
        //    string updatedCounterExampleText = _createdCounterExampleText + "-updated";
        //    Console.WriteLine(string.Format("\nCalling UpdateCounterExample({0}, {1})...", _createdWorkspaceId, updatedCounterExampleText));
        //    UpdateCounterexample example = new UpdateCounterexample()
        //    {
        //        Text = updatedCounterExampleText
        //    };

        //    var result = service.UpdateCounterexample(_createdWorkspaceId, _createdCounterExampleText, example);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("CounterExample name: {0}, created: {1}", result.Text, result.Created));
        //        _createdCounterExampleText = updatedCounterExampleText;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}
        //#endregion

        //#region Entities
        //[TestMethod]
        //public void ListEntities_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling ListEntities({0})...", _createdWorkspaceId));
        //    var result = service.ListEntities(_createdWorkspaceId);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        if (result.Entities != null && result.Entities.Count > 0)
        //        {
        //            foreach (EntityExport entity in result.Entities)
        //                Console.WriteLine(string.Format("Entity: {0} | Created: {1}", entity.EntityName, entity.Description));
        //        }
        //        else
        //        {
        //            Console.WriteLine("There are no entities.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Entities are null.");
        //    }
        //}

        //[TestMethod]
        //public void CreateEntity_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling CreateEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
        //    CreateEntity entity = new CreateEntity()
        //    {
        //        Entity = _createdEntity,
        //        Description = _createdEntityDescription
        //    };

        //    var result = service.CreateEntity(_createdWorkspaceId, entity);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("entity: {0} | description: {1}", result.EntityName, result.Description));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void GetEntity_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling GetEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
        //    var result = service.GetEntity(_createdWorkspaceId, _createdEntity);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("entity: {0} | description: {1}", result.EntityName, result.Description));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void UpdateEntity_Success()
        //{
        //    string updatedEntity = _createdEntity + "-updated";
        //    string updatedEntityDescription = _createdEntityDescription + "-updated";

        //    Console.WriteLine(string.Format("\nCalling UpdateEntity({0}, {1})...", _createdWorkspaceId, _createdEntity, updatedEntity));
        //    UpdateEntity entity = new UpdateEntity()
        //    {
        //        Entity = updatedEntity,
        //        Description = updatedEntityDescription
        //    };

        //    var result = service.UpdateEntity(_createdWorkspaceId, _createdEntity, entity);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("entity: {0} | description: {1}", result.EntityName, result.Description));
        //        _createdEntity = updatedEntity;
        //        _createdEntityDescription = updatedEntityDescription;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}
        //#endregion

        //#region Values
        //[TestMethod]
        //public void ListValues_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling ListValues({0}, {1})...", _createdWorkspaceId, _createdEntity));
        //    var result = service.ListValues(_createdWorkspaceId, _createdEntity);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        if (result.Values != null && result.Values.Count > 0)
        //        {
        //            foreach (ValueExport value in result.Values)
        //                Console.WriteLine(string.Format("value: {0} | Created: {1}", value.ValueText, value.Created));
        //        }
        //        else
        //        {
        //            Console.WriteLine("There are no values.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Values are null.");
        //    }
        //}

        //[TestMethod]
        //public void CreateValue_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling CreateValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
        //    CreateValue value = new CreateValue()
        //    {
        //        Value = _createdValue
        //    };

        //    var result = service.CreateValue(_createdWorkspaceId, _createdEntity, value);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("value: {0}", result.ValueText));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void GetValue_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling GetValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
        //    var result = service.GetValue(_createdWorkspaceId, _createdEntity, _createdValue);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("value: {0}", result.ValueText));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void UpdateValue_Success()
        //{
        //    string updatedValue = _createdValue + "-updated";

        //    Console.WriteLine(string.Format("\nCalling UpdateValue({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, updatedValue));

        //    UpdateValue value = new UpdateValue()
        //    {
        //        Value = updatedValue
        //    };

        //    var result = service.UpdateValue(_createdWorkspaceId, _createdEntity, _createdValue, value);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("value: {0}", result.ValueText));
        //        _createdValue = updatedValue;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}
        //#endregion

        //#region Synonyms
        //[TestMethod]
        //public void ListSynonyms_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling ListSynonyms({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
        //    var result = service.ListSynonyms(_createdWorkspaceId, _createdEntity, _createdValue);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        if (result.Synonyms != null && result.Synonyms.Count > 0)
        //        {
        //            foreach (Synonym synonym in result.Synonyms)
        //                Console.WriteLine(string.Format("Synonym: {0} | Created: {1}", synonym.SynonymText, synonym.Created));
        //        }
        //        else
        //        {
        //            Console.WriteLine("There are no synonyms.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Synonyms are null.");
        //    }
        //}

        //[TestMethod]
        //public void CreateSynonym_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling CreateSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
        //    CreateSynonym synonym = new CreateSynonym()
        //    {
        //        Synonym = _createdSynonym
        //    };

        //    var result = service.CreateSynonym(_createdWorkspaceId, _createdEntity, _createdValue, synonym);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("synonym: {0}", result.SynonymText));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void GetSynonym_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling GetSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
        //    var result = service.GetSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("synonym: {0}", result.SynonymText));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void UpdateSynonym_Success()
        //{
        //    string updatedSynonym = _createdSynonym + "-updated";
        //    Console.WriteLine(string.Format("\nCalling UpdateSynonym({0}, {1}, {2}, {3}, {4})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym, updatedSynonym));

        //    UpdateSynonym synonym = new UpdateSynonym()
        //    {
        //        Synonym = updatedSynonym
        //    };

        //    var result = service.UpdateSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym, synonym);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("synonym: {0}", result.SynonymText));
        //        _createdSynonym = updatedSynonym;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}
        //#endregion

        //#region Intents
        //[TestMethod]
        //public void ListIntents_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling ListIntents({0})...", _createdWorkspaceId));
        //    var result = service.ListIntents(_createdWorkspaceId);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        if (result.Intents != null && result.Intents.Count > 0)
        //        {
        //            foreach (IntentExport intent in result.Intents)
        //                Console.WriteLine(string.Format("Intent: {0} | Created: {1}", intent.IntentName, intent.Created));
        //        }
        //        else
        //        {
        //            Console.WriteLine("There are no intents.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Intents are null.");
        //    }
        //}

        //[TestMethod]
        //public void CreateIntent_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling CreateIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
        //    CreateIntent intent = new CreateIntent()
        //    {
        //        Intent = _createdIntent,
        //        Description = _createdIntentDescription
        //    };

        //    var result = service.CreateIntent(_createdWorkspaceId, intent);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine("intent: {0} | description: {1}", result.IntentName, result.Description);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void GetIntent_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling GetIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
        //    var result = service.GetIntent(_createdWorkspaceId, _createdIntent);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine("intent: {0} | description: {1}", result.IntentName, result.Description);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void UpdateIntent_Success()
        //{
        //    string updatedIntent = _createdIntent + "-updated";
        //    string updatedIntentDescription = _createdIntentDescription + "-updated";
        //    Console.WriteLine(string.Format("\nCalling UpdateIntent({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, updatedIntent));

        //    UpdateIntent intent = new UpdateIntent()
        //    {
        //        Intent = updatedIntent,
        //        Description = updatedIntentDescription
        //    };

        //    var result = service.UpdateIntent(_createdWorkspaceId, _createdIntent, intent);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine("intent: {0} | description: {1}", result.IntentName, result.Description);
        //        _createdIntent = updatedIntent;
        //        _createdIntentDescription = updatedIntentDescription;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}
        //#endregion

        //#region Examples
        //[TestMethod]
        //public void ListExamples_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling ListExamples({0}, {1})...", _createdWorkspaceId, _createdIntent));
        //    var result = service.ListExamples(_createdWorkspaceId, _createdIntent);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        if (result.Examples != null && result.Examples.Count > 0)
        //        {
        //            foreach (Example example in result.Examples)
        //                Console.WriteLine(string.Format("Example: {0} | Created: {1}", example.ExampleText, example.Created));
        //        }
        //        else
        //        {
        //            Console.WriteLine("There are no examples.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Examples are null.");
        //    }
        //}

        //[TestMethod]
        //public void CreateExample_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling CreateExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));

        //    CreateExample example = new CreateExample()
        //    {
        //        Text = _createdExample
        //    };

        //    var result = service.CreateExample(_createdWorkspaceId, _createdIntent, example);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("example: {0}", result.ExampleText));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void GetExample_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling GetExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));
        //    var result = service.GetExample(_createdWorkspaceId, _createdIntent, _createdExample);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("example: {0}", result.ExampleText));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void UpdateExample_Success()
        //{
        //    string updatedExample = _createdExample + "-updated";
        //    Console.WriteLine(string.Format("\nCalling UpdateExample({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdIntent, _createdExample, updatedExample));

        //    UpdateExample example = new UpdateExample()
        //    {
        //        Text = updatedExample
        //    };

        //    var result = service.UpdateExample(_createdWorkspaceId, _createdIntent, _createdExample, example);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("example: {0}", result.ExampleText));
        //        _createdExample = updatedExample;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void DeleteExample_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling DeleteExample({0}, {1}, {2})...", _createdWorkspaceId, _createdIntent, _createdExample));
        //    var result = service.DeleteExample(_createdWorkspaceId, _createdIntent, _createdExample);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Deleted example {0}.", _createdExample));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}
        //#endregion

        //[TestMethod]
        //public void ListLogEventsSuccess()
        //{
        //    Console.WriteLine("Running Test ListLogEventsSuccess.");
        //    var result = listlo();
        //    Assert.IsNotNull(result);
        //}

        //#region Delete
        //[TestMethod]
        //public void DeleteIntent_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling DeleteIntent({0}, {1})...", _createdWorkspaceId, _createdIntent));
        //    var result = service.DeleteIntent(_createdWorkspaceId, _createdIntent);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Deleted intent {0}", _createdIntent));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void DeleteSynonym_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling DeleteSynonym({0}, {1}, {2}, {3})...", _createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym));
        //    var result = service.DeleteSynonym(_createdWorkspaceId, _createdEntity, _createdValue, _createdSynonym);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Deleted synonym {0}", _createdSynonym));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void DeleteValue_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling DeleteValue({0}, {1}, {2})...", _createdWorkspaceId, _createdEntity, _createdValue));
        //    var result = service.DeleteValue(_createdWorkspaceId, _createdEntity, _createdValue);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine("Deleted value {0}", _createdValue);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void DeleteEntity_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling DeleteEntity({0}, {1})...", _createdWorkspaceId, _createdEntity));
        //    var result = service.DeleteEntity(_createdWorkspaceId, _createdEntity);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Deleted entity {0}.", _createdEntity));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void DeleteCounterExample_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling DeleteCounterExample({0}, {1})...", _createdWorkspaceId, _createdCounterExampleText));
        //    var result = service.DeleteCounterexample(_createdWorkspaceId, _createdCounterExampleText);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Deleted counterExample {0}.", _createdCounterExampleText));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}

        //[TestMethod]
        //public void DeleteWorkspace_Success()
        //{
        //    Console.WriteLine(string.Format("\nCalling DeleteWorkspace({0})...", _createdWorkspaceId));
        //    var result = service.DeleteWorkspace(_createdWorkspaceId);

        //    Assert.IsNotNull(result);

        //    if (result != null)
        //    {
        //        Console.WriteLine(string.Format("Workspace {0} deleted.", _createdWorkspaceId));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Result is null.");
        //    }
        //}
        //#endregion
        */

        #region CreateWorkspace
        private Workspace CreateWorkspace()
        {
            Console.WriteLine("\nAttempting to CreateWorkspace()");
            CreateWorkspace workspace = new CreateWorkspace()
            {
                Name = _createdWorkspaceName,
                Description = _createdWorkspaceDescription,
                Language = _createdWorkspaceLanguage
            };
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
            Console.WriteLine("\nAttempting to UpdateWorkspace()");
            var result = service.UpdateWorkspace(workspaceId: workspaceId, properties:properties);

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
            Console.WriteLine("\nAttempting to Message()");
            var result = service.Message(workspaceId: workspaceId, request:request);

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
    }
}
