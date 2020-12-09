/**
* (C) Copyright IBM Corp. 2019, 2020.
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

using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v1.Model;
using System;
using System.Collections.Generic;

namespace IBM.Watson.Assistant.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
        string versionDate = "{versionDate}";
        string workspaceId = "{workspaceId}";
        string intent;
        string example;
        string counterexample;
        string entity;
        string value;
        string synonym;
        string dialogNode;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();
            example.Message();

            example.ListWorkspaces();
            example.CreateWorkspace();
            example.GetWorkspace();
            example.UpdateWorkspace();

            example.ListIntents();
            example.CreateIntent();
            example.GetIntent();
            example.UpdateIntent();

            example.ListExamples();
            example.CreateExample();
            example.GetExample();
            example.UpdateExample();

            example.ListCounterexamples();
            example.CreateCounterexample();
            example.GetCounterexample();
            example.UpdateCounterexample();

            example.ListEntities();
            example.CreateEntity();
            example.GetEntity();
            example.UpdateEntity();

            example.ListEntityMentions();

            example.ListValues();
            example.CreateValue();
            example.GetValue();
            example.UpdateValue();

            example.ListSynonyms();
            example.CreateSynonym();
            example.GetSynonym();
            example.UpdateSynonym();

            example.ListDialogNodes();
            example.CreateDialogNode();
            example.GetDialogNode();
            example.UpdateDialogNode();

            example.ListLogs();
            example.ListAllLogs();

            example.DeleteUserData();

            example.DeleteDialogNode();
            example.DeleteSynonyms();
            example.DeleteValue();
            example.DeleteEntity();
            example.DeleteCounterexample();
            example.DeleteExample();
            example.DeleteIntent();
            example.DeleteWorkspace();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Message
        public void Message()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Message(
                workspaceId: "{workspaceId}",
                input: new MessageInput()
                {
                    Text = "Hello"
                }
                );

            Console.WriteLine(result.Response);

            workspaceId = null;
        }
        #endregion

        #region Workspaces
        public void ListWorkspaces()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListWorkspaces();
            Console.WriteLine(result.Response);
        }

        public void CreateWorkspace()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateWorkspace(
                name: "API test",
                description: "Example workspace created via API"
                );

            Console.WriteLine(result.Response);

            workspaceId = result.Result.WorkspaceId;
        }

        public void GetWorkspace()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetWorkspace(
                workspaceId: "{workspaceId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateWorkspace()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateWorkspace(
                workspaceId: "{workspaceId}",
                name: "Updated workspace",
                description: "Example workspace updated via API"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteWorkspace()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteWorkspace(
                workspaceId: "{workspaceId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Intents
        public void ListIntents()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListIntents(
                workspaceId: "{workspaceId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateIntent()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateIntent(
                workspaceId: "{workspaceId}",
                intent: "hello",
                examples: new List<Example>()
                {
        new Example()
        {
            Text = "Good morning"
        },
        new Example()
        {
            Text = "Hi there"
        }
                }
                );

            Console.WriteLine(result.Response);

            intent = result.Result._Intent;
        }

        public void GetIntent()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetIntent(
                workspaceId: "{workspaceId}",
                intent: "hello"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateIntent()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateIntent(
                workspaceId: "{workspaceId}",
                intent: "hello",
                newExamples: new List<Example>()
                {
        new Example()
        {
            Text = "Good afternoon"
        }
                },
                newDescription: "Updated intent"
                );

            Console.WriteLine(result.Response);

            intent = result.Result._Intent;
        }

        public void DeleteIntent()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteIntent(
                workspaceId: "{workspaceId}",
                intent: "hello"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Examples
        public void ListExamples()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListExamples(
                workspaceId: "{workspaceId}",
                intent: "hello"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateExample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateExample(
                workspaceId: "{workspaceId}",
                intent: "hello",
                text: "Howdy!"
                );

            Console.WriteLine(result.Response);

            example = result.Result.Text;
        }

        public void GetExample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetExample(
                workspaceId: "{workspaceId}",
                intent: "hello",
                text: "Good afternoon"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateExample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateExample(
                workspaceId: "{workspaceId}",
                intent: "hello",
                text: "Hi there",
                newText: "Hello there!"
                );

            Console.WriteLine(result.Response);

            example = result.Result.Text;
        }

        public void DeleteExample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteExample(
                workspaceId: "{workspaceId}",
                intent: "hello",
                text: "Good afternoon"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Counterexamples
        public void ListCounterexamples()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListCounterexamples(
                workspaceId: "{workspaceId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateCounterexample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateCounterexample(
                workspaceId: "{workspaceId}",
                text: "Make me a sandwich"
                );

            Console.WriteLine(result.Response);

            counterexample = result.Result.Text;
        }

        public void GetCounterexample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetCounterexample(
                workspaceId: "{workspaceId}",
                text: "What are you wearing?"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateCounterexample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateCounterexample(
                workspaceId: "{workspaceId}",
                text: "{counterexample}",
                newText: "Make me a cheeseburger"
                );

            Console.WriteLine(result.Response);

            counterexample = result.Result.Text;
        }

        public void DeleteCounterexample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteCounterexample(
                workspaceId: "{workspaceId}",
                text: "Make me a sandwich"
            );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Entities
        public void ListEntities()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListEntities(
                workspaceId: "{workspaceId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateEntity()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateEntity(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                values: new List<CreateValue>()
                {
                    new CreateValue()
                    {
                        Value = "water"
                    },
                    new CreateValue()
                    {
                        Value = "orange juice"
                    },
                    new CreateValue()
                    {
                        Value = "soda"
                    }
                }
                );

            Console.WriteLine(result.Response);

            entity = result.Result._Entity;
        }

        public void GetEntity()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetEntity(
                workspaceId: "{workspaceId}",
                entity: "beverage"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateEntity()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateEntity(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                newDescription: "Liquid refreshment"
                );

            Console.WriteLine(result.Response);

            entity = result.Result._Entity;
        }

        public void DeleteEntity()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteEntity(
                workspaceId: "{workspaceId}",
                entity: "beverage"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Mentions
        public void ListEntityMentions()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListMentions(
                workspaceId: "{workspaceId}",
                entity: "beverage"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Values
        public void ListValues()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListValues(
                workspaceId: "{workspaceId}",
                entity: "beverage"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateValue()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateValue(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "beer"
                );

            Console.WriteLine(result.Response);

            value = result.Result._Value;
        }

        public void GetValue()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetValue(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "orange juice"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateValue()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateValue(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "soda",
                newSynonyms: new List<string>()
                {
"pop",
"soft drink"
                }
                );

            Console.WriteLine(result.Response);

            value = result.Result._Value;
        }

        public void DeleteValue()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteValue(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "beer"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Synonyms
        public void ListSynonyms()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListSynonyms(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "soda"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateSynonym()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateSynonym(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "orange juice",
                synonym: "OJ"
                );

            Console.WriteLine(result.Response);

            synonym = result.Result._Synonym;
        }

        public void GetSynonym()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetSynonym(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "orange juice",
                synonym: "OJ"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateSynonym()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateSynonym(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "orange juice",
                synonym: "OJ",
                newSynonym: "O.J."
                );

            Console.WriteLine(result.Response);

            synonym = result.Result._Synonym;
        }

        public void DeleteSynonyms()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteSynonym(
                workspaceId: "{workspaceId}",
                entity: "beverage",
                value: "orange juice",
                synonym: "OJ"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Dialog Nodes
        public void ListDialogNodes()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListDialogNodes(
                workspaceId: "{workspaceId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateDialogNode()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateDialogNode(
                workspaceId: "{workspaceId}",
                dialogNode: "example_dialog_node"
                );

            Console.WriteLine(result.Response);

            dialogNode = result.Result._DialogNode;
        }

        public void GetDialogNode()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetDialogNode(
                workspaceId: "{workspaceId}",
                dialogNode: "greeting"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateDialogNode()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            DialogNodeOutputGenericDialogNodeOutputResponseTypeText typeText =
                new DialogNodeOutputGenericDialogNodeOutputResponseTypeText();
            typeText.ResponseType = "text";
            typeText.Values = new List<DialogNodeOutputTextValuesElement>()
            {
                new DialogNodeOutputTextValuesElement()
                {
                    Text = "Hello! What can I do for you?"
                }
            };



            var result = service.UpdateDialogNode(
                workspaceId: "{workspaceId}",
                dialogNode: "greeting",
                newOutput: new DialogNodeOutput()
                {
                    Generic = new List<DialogNodeOutputGeneric>()
                    {
                        typeText
                    }
                }
                );

            Console.WriteLine(result.Response);

            dialogNode = result.Result._DialogNode;
        }

        public void DeleteDialogNode()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteDialogNode(
                workspaceId: "{workspaceId}",
                dialogNode: "greeting"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Logs
        public void ListLogs()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListLogs(
                workspaceId: "{workspaceId}"
                );

            Console.WriteLine(result.Response);
        }

        public void ListAllLogs()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListAllLogs(
                filter: "language::en,request.context.metadata.deployment::testDeployment"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2019-02-28", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteUserData(
                customerId: "{id}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
