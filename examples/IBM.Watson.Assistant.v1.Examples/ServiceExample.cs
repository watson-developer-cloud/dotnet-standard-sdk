/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

namespace IBM.Watson.Assistant.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
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
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            MessageInput input = new MessageInput()
            {
                Text = "hello"
            };

            var result = service.Message(
                workspaceId: workspaceId,
                input: input
                );

            Console.WriteLine(result.Response);
            
            workspaceId = null;
        }
        #endregion

        #region Workspaces
        public void ListWorkspaces()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListWorkspaces();
            Console.WriteLine(result.Response);
        }

        public void CreateWorkspace()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateWorkspace(
                name: "Workspace name",
                description: "Workspace description"
                );

            Console.WriteLine(result.Response);

            workspaceId = result.Result.WorkspaceId;
        }

        public void GetWorkspace()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetWorkspace(
                workspaceId: workspaceId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateWorkspace()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateWorkspace(
                workspaceId: workspaceId,
                name: "Updated workspace name",
                description: "Updated workspace description"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteWorkspace()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteWorkspace(
                workspaceId: workspaceId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Intents
        public void ListIntents()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListIntents(
                workspaceId: workspaceId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateIntent()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateIntent(
                workspaceId: workspaceId,
                intent: "example_intent"
                );

            Console.WriteLine(result.Response);

            intent = result.Result._Intent;
        }

        public void GetIntent()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetIntent(
                workspaceId: workspaceId,
                intent: intent
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateIntent()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateIntent(
                workspaceId: workspaceId,
                intent: intent,
                newIntent: "updated_intent"
                );

            Console.WriteLine(result.Response);

            intent = result.Result._Intent;
        }

        public void DeleteIntent()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteIntent(
                workspaceId: workspaceId,
                intent: intent
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Examples
        public void ListExamples()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListExamples(
                workspaceId: workspaceId,
                intent: intent
                );

            Console.WriteLine(result.Response);
        }

        public void CreateExample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateExample(
                workspaceId: workspaceId,
                intent: intent,
                text: "Example"
                );

            Console.WriteLine(result.Response);

            example = result.Result.Text;
        }

        public void GetExample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetExample(
                workspaceId: workspaceId,
                intent: intent,
                text: example
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateExample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateExample(
                workspaceId: workspaceId,
                intent: intent,
                text: example,
                newText: "Updated example"
                );

            Console.WriteLine(result.Response);

            example = result.Result.Text;
        }

        public void DeleteExample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteExample(
                workspaceId: workspaceId,
                intent: intent,
                text: example
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Counterexamples
        public void ListCounterexamples()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListCounterexamples(
                workspaceId: workspaceId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateCounterexample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateCounterexample(
                workspaceId: workspaceId,
                text: "Counterexample"
                );

            Console.WriteLine(result.Response);

            counterexample = result.Result.Text;
        }

        public void GetCounterexample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetCounterexample(
                workspaceId: workspaceId,
                text: counterexample
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateCounterexample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateCounterexample(
                workspaceId: workspaceId,
                text: counterexample,
                newText: "Updated counterexample"
                );

            Console.WriteLine(result.Response);

            counterexample = result.Result.Text;
        }

        public void DeleteCounterexample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteCounterexample(
                workspaceId: workspaceId,
                text: counterexample
                );

            Console.WriteLine(result.Response);
        }
#endregion

        #region Entities
        public void ListEntities()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListEntities(
                workspaceId: workspaceId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateEntity()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateEntity(
                workspaceId: workspaceId,
                entity: "example_entity"
                );

            Console.WriteLine(result.Response);

            entity = result.Result._Entity;
        }

        public void GetEntity()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetEntity(
                workspaceId: workspaceId,
                entity: entity
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateEntity()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateEntity(
                workspaceId: workspaceId,
                entity: entity,
                newEntity: "updated_entity"
                );

            Console.WriteLine(result.Response);

            entity = result.Result._Entity;
        }

        public void DeleteEntity()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteEntity(
                workspaceId: workspaceId,
                entity: entity
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Mentions
        public void ListEntityMentions()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListMentions(
                workspaceId: workspaceId,
                entity: entity
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Values
        public void ListValues()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListValues(
                workspaceId: workspaceId,
                entity: entity
                );

            Console.WriteLine(result.Response);
        }

        public void CreateValue()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateValue(
                workspaceId: workspaceId,
                entity: entity,
                value: "Value"
                );

            Console.WriteLine(result.Response);

            value = result.Result._Value;
        }

        public void GetValue()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetValue(
                workspaceId: workspaceId,
                entity: entity,
                value: value
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateValue()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateValue(
                workspaceId: workspaceId,
                entity: entity,
                value: value,
                newValue: "Updated value"
                );

            Console.WriteLine(result.Response);

            value = result.Result._Value;
        }

        public void DeleteValue()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteValue(
                workspaceId: workspaceId,
                entity: entity,
                value: value
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Synonyms
        public void ListSynonyms()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListSynonyms(
                workspaceId: workspaceId,
                entity: entity,
                value: value
                );

            Console.WriteLine(result.Response);
        }

        public void CreateSynonym()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateSynonym(
                workspaceId: workspaceId,
                entity: entity,
                value: value,
                synonym: "Synonym"
                );

            Console.WriteLine(result.Response);

            synonym = result.Result._Synonym;
        }

        public void GetSynonym()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetSynonym(
                workspaceId: workspaceId,
                entity: entity,
                value: value,
                synonym: synonym
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateSynonym()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateSynonym(
                workspaceId: workspaceId,
                entity: entity,
                value: value,
                synonym: synonym,
                newSynonym: "Updated synonym"
                );

            Console.WriteLine(result.Response);

            synonym = result.Result._Synonym;
        }

        public void DeleteSynonyms()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteSynonym(
                workspaceId: workspaceId,
                entity: entity,
                value: value,
                synonym: synonym
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Dialog Nodes
        public void ListDialogNodes()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListDialogNodes(
                workspaceId: workspaceId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateDialogNode()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateDialogNode(
                workspaceId: workspaceId,
                dialogNode: "example_dialog_node"
                );

            Console.WriteLine(result.Response);

            dialogNode = result.Result._DialogNode;
        }

        public void GetDialogNode()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetDialogNode(
                workspaceId: workspaceId,
                dialogNode: dialogNode
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateDialogNode()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateDialogNode(
                workspaceId: workspaceId,
                dialogNode: dialogNode,
                newDialogNode: "updated_dialog_node"
                );

            Console.WriteLine(result.Response);

            dialogNode = result.Result._DialogNode;
        }

        public void DeleteDialogNode()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteDialogNode(
                workspaceId: workspaceId,
                dialogNode: dialogNode
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Logs
        public void ListLogs()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListLogs(
                workspaceId: workspaceId
                );

            Console.WriteLine(result.Response);
        }

        public void ListAllLogs()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListAllLogs(
                filter: "(language::en,request.context.metadata.deployment::deployment_1)"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region User Data
        public void DeleteUserData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteUserData(
                customerId: "test-customer-id"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
