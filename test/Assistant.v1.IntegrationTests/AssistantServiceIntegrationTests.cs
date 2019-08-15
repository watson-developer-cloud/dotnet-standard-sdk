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
using System;
using IBM.Watson.Assistant.v1.Model;
using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Util;

namespace IBM.Watson.Assistant.v1.IntegrationTests
{
    [TestClass]
    public class AssistantServiceIntegrationTests
    {
        private AssistantService service;
        private static string credentials = string.Empty;
        private static string versionDate = "2019-02-28";

        private static string workspaceId;
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
        private static string createdCounterExampleText = "Example text?";
        private static string createdSynonym = "synonym";
        private static string createdExample = "example";
        private static string dialogNodeName = "dialognode";
        private static string dialogNodeDesc = ".NET SDK Integration test dialog node";

        [TestInitialize]
        public void Setup()
        {
            service = new AssistantService();
            service.VersionDate = versionDate;
            var creds = CredentialUtils.GetServiceProperties("assistant");
            creds.TryGetValue("WORKSPACE_ID", out workspaceId);
            service.VersionDate = versionDate;

#if DELETE_DOTNET_WORKSPACES
            service.WithHeader("X-Watson-Test", "1");
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
                    var getWorkspaceResult = GetWorkspace(
                        workspaceId: workspaceId
                        );
                    if (getWorkspaceResult != null)
                        DeleteWorkspace(
                            workspaceId: workspaceId
                            );
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
            service.WithHeader("X-Watson-Test", "1");
            var workspaces = service.ListWorkspaces();
            List<string> dotnet_workpaces = new List<string>();

            foreach (Workspace workspace in workspaces.Result.Workspaces)
            {
                if (workspace.Name == createdWorkspaceName)
                    dotnet_workpaces.Add(workspace.WorkspaceId);
            }

            foreach (string workspaceId in dotnet_workpaces)
            {
                try
                {
                    service.WithHeader("X-Watson-Test", "1");
                    var getWorkspaceResult = service.GetWorkspace(
                        workspaceId: workspaceId
                        );
                    if (getWorkspaceResult != null)
                    {
                        service.WithHeader("X-Watson-Test", "1");
                        service.DeleteWorkspace(
                            workspaceId: workspaceId
                            );
                    }
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
            Context context;

            List<string> intents = new List<string>();
            MessageInput input = new MessageInput()
            {
                Text = inputString
            };

            service.WithHeader("X-Watson-Test", "1");
            var results0 = service.Message(
                workspaceId: workspaceId,
                input: input
                );
            context = results0.Result.Context;
            context.Add("name", "Watson");
            intents.Add(GetIntent(results0.Result));

            input.Text = assistantString0;

            service.WithHeader("X-Watson-Test", "1");
            var results1 = service.Message(
                workspaceId: workspaceId,
                input: input,
                context: context
                );
            context = results1.Result.Context;
            intents.Add(GetIntent(results1.Result));

            input.Text = assistantString1;

            service.WithHeader("X-Watson-Test", "1");
            var results2 = service.Message(
                workspaceId: workspaceId,
                input: input,
                context: context
                );
            context = results2.Result.Context;
            intents.Add(GetIntent(results2.Result));

            input.Text = assistantString2;

            service.WithHeader("X-Watson-Test", "1");
            var results3 = service.Message(
                workspaceId: workspaceId,
                input: input,
                context: context);
            context = results3.Result.Context;
            intents.Add(GetIntent(results3.Result));

            Assert.IsNotNull(results0);
            Assert.IsTrue(results0.Result.Context.Get("name").ToString() == "Watson");
            Assert.IsNotNull(results1);
            Assert.IsTrue(results1.Result.Context.Get("name").ToString() == "Watson");
            Assert.IsNotNull(results2);
            Assert.IsTrue(results2.Result.Context.Get("name").ToString() == "Watson");
            Assert.IsNotNull(results3);
            Assert.IsTrue(results3.Result.Context.Get("name").ToString() == "Watson");
            foreach (string intent in intents)
            {
                Assert.IsTrue(IsUniqueInList(intent, intents));
            }
        }
        #endregion

        private bool IsUniqueInList(string value, List<string> list)
        {
            List<string> duplicates = new List<string>();
            foreach (string item in list)
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
            return messageResponse.Intents[0].Intent;
        }
        #endregion

        #region Counter Examples
        [TestMethod]
        public void TestCounterExamples_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                name: createdWorkspaceName,
                description: createdWorkspaceDescription,
                language: createdWorkspaceLanguage
                );
            var workspaceId = createWorkspaceResult.Result.WorkspaceId;
            service.WithHeader("X-Watson-Test", "1");
            var listCounterExamplesResult = service.ListCounterexamples(
                workspaceId: workspaceId
                );

            service.WithHeader("X-Watson-Test", "1");
            var createCounterexampleResult = service.CreateCounterexample(
                workspaceId: workspaceId,
                text: createdCounterExampleText
                );

            service.WithHeader("X-Watson-Test", "1");
            var getCounterexampleResult = service.GetCounterexample(
                workspaceId: workspaceId,
                text: createdCounterExampleText,
                includeAudit: true
                );

            string updatedCounterExampleText = createdCounterExampleText + "-updated";

            service.WithHeader("X-Watson-Test", "1");
            var updateCounterexampleResult = service.UpdateCounterexample(
                workspaceId: workspaceId,
                text: createdCounterExampleText,
                newText: updatedCounterExampleText
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteCounterexampleResult = service.DeleteCounterexample(
                workspaceId: workspaceId,
                text: updatedCounterExampleText
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId
                );

            Assert.IsNotNull(listCounterExamplesResult);
            Assert.IsNotNull(listCounterExamplesResult.Result.Counterexamples);
            Assert.IsNotNull(createCounterexampleResult);
            Assert.IsFalse(string.IsNullOrEmpty(createCounterexampleResult.Result.Text));
            Assert.IsNotNull(getCounterexampleResult);
            Assert.IsFalse(string.IsNullOrEmpty(getCounterexampleResult.Result.Text));
            Assert.IsNotNull(updateCounterexampleResult);
            Assert.IsTrue(updateCounterexampleResult.Result.Text == updatedCounterExampleText);
            Assert.IsNotNull(deleteCounterexampleResult);
        }
        #endregion

        #region Workspaces
        [TestMethod]
        public void TestWorkspaces_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var ListWorkspacesResult = service.ListWorkspaces();

            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                name: createdWorkspaceName,
                description: createdWorkspaceDescription,
                language: createdWorkspaceLanguage,
                learningOptOut: true
                );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;

            service.WithHeader("X-Watson-Test", "1");
            var getWorkspaceResult = service.GetWorkspace(
                workspaceId: workspaceId,
                export: true,
                includeAudit: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var updateWorkspaceResult = service.UpdateWorkspace(
                workspaceId: workspaceId,
                name: createdWorkspaceName + "-updated",
                description: createdWorkspaceDescription + "-updated",
                language: createdWorkspaceLanguage,
                learningOptOut: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId);

            Assert.IsNotNull(createWorkspaceResult);
            Assert.IsFalse(string.IsNullOrEmpty(workspaceId));
            Assert.IsNotNull(getWorkspaceResult);
            Assert.IsNotNull(updateWorkspaceResult);
            Assert.IsFalse(string.IsNullOrEmpty(workspaceId));
            Assert.IsTrue(updateWorkspaceResult.Result.Name == createdWorkspaceName + "-updated");
            Assert.IsTrue(updateWorkspaceResult.Result.Description == createdWorkspaceDescription + "-updated");
            Assert.IsNotNull(ListWorkspacesResult);
            Assert.IsNotNull(deleteWorkspaceResult);
        }
        #endregion

        #region Entities
        [TestMethod]
        public void TestEntities_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                 name: createdWorkspaceName,
                 description: createdWorkspaceDescription,
                 language: createdWorkspaceLanguage,
                 learningOptOut: true
                 );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;
            service.WithHeader("X-Watson-Test", "1");
            var listEntitiesResult = service.ListEntities(
                workspaceId: workspaceId,
                export: true,
                pageLimit: 1,
                includeCount: true,
                sort: "-updated",
                includeAudit: true);

            service.WithHeader("X-Watson-Test", "1");
            var createEntityResult = service.CreateEntity(
                workspaceId: workspaceId,
                entity: createdEntity,
                description: createdEntityDescription,
                fuzzyMatch: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var getEntityResult = service.GetEntity(
                workspaceId: workspaceId,
                entity: createdEntity,
                export: true,
                includeAudit: true
                );

            string updatedEntity = createdEntity + "-updated";
            string updatedEntityDescription = createdEntityDescription + "-updated";
            service.WithHeader("X-Watson-Test", "1");
            var updateEntityResult = service.UpdateEntity(
                workspaceId: workspaceId,
                entity: createdEntity,
                newEntity: updatedEntity,
                newDescription: updatedEntityDescription,
                newFuzzyMatch: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteEntityResult = service.DeleteEntity(
                workspaceId: workspaceId,
                entity: updatedEntity
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId
                );

            Assert.IsNotNull(listEntitiesResult);
            Assert.IsNotNull(createEntityResult);
            Assert.IsFalse(string.IsNullOrEmpty(createEntityResult.Result._Entity));
            Assert.IsNotNull(getEntityResult);
            Assert.IsTrue(getEntityResult.Result._Entity == createdEntity);
            Assert.IsNotNull(updateEntityResult);
            Assert.IsTrue(updateEntityResult.Result._Entity == updatedEntity);
            Assert.IsTrue(updateEntityResult.Result.Description == updatedEntityDescription);
            Assert.IsNotNull(deleteEntityResult);
        }
        #endregion

        #region Values
        [TestMethod]
        public void TestValues_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                 name: createdWorkspaceName,
                 description: createdWorkspaceDescription,
                 language: createdWorkspaceLanguage,
                 learningOptOut: true
                 );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;

            service.WithHeader("X-Watson-Test", "1");
            var createEntityResult = service.CreateEntity(
                workspaceId: workspaceId,
                entity: createdEntity,
                description: createdEntityDescription,
                fuzzyMatch: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var listValuesResult = service.ListValues(
                workspaceId: workspaceId,
                entity: createdEntity
                );

            service.WithHeader("X-Watson-Test", "1");
            var createValueResult = service.CreateValue(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue
                );

            service.WithHeader("X-Watson-Test", "1");
            var getValueResult = service.GetValue(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue
                );

            string updatedValue = createdValue + "-updated";

            service.WithHeader("X-Watson-Test", "1");
            var updateValueResult = service.UpdateValue(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue,
                newValue: updatedValue
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteValueResult = service.DeleteValue(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: updatedValue
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteEntityResult = service.DeleteEntity(
                workspaceId: workspaceId,
                entity: createdEntity
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId
                );

            Assert.IsNotNull(listValuesResult);
            Assert.IsNotNull(createValueResult);
            Assert.IsFalse(string.IsNullOrEmpty(createValueResult.Result._Value));
            Assert.IsNotNull(getValueResult);
            Assert.IsTrue(getValueResult.Result._Value == createdValue);
            Assert.IsNotNull(updateValueResult);
            Assert.IsTrue(updateValueResult.Result._Value == updatedValue);
            Assert.IsNotNull(deleteValueResult);
        }
        #endregion

        #region Synonyms
        [TestMethod]
        public void TestSynonyms_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                 name: createdWorkspaceName,
                 description: createdWorkspaceDescription,
                 language: createdWorkspaceLanguage,
                 learningOptOut: true
                 );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;

            service.WithHeader("X-Watson-Test", "1");
            var createEntityResult = service.CreateEntity(
                workspaceId: workspaceId,
                entity: createdEntity,
                description: createdEntityDescription,
                fuzzyMatch: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var createValueResult = service.CreateValue(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue
                );

            service.WithHeader("X-Watson-Test", "1");
            var listSynonymsResult = service.ListSynonyms(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue,
                pageLimit: 1,
                includeAudit: true,
                sort: "-updated"
                );

            service.WithHeader("X-Watson-Test", "1");
            var createSynonymResult = service.CreateSynonym(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue,
                synonym: createdSynonym
                );

            service.WithHeader("X-Watson-Test", "1");
            var getSynonymResult = service.GetSynonym(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue,
                synonym: createdSynonym
                );

            string updatedSynonym = createdSynonym + "-updated";
            service.WithHeader("X-Watson-Test", "1");
            var updateSynonymResult = service.UpdateSynonym(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue,
                synonym: createdSynonym,
                newSynonym: updatedSynonym);

            service.WithHeader("X-Watson-Test", "1");
            var deleteSynonymResult = service.DeleteSynonym(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue,
                synonym: updatedSynonym
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteValueResult = service.DeleteValue(
                workspaceId: workspaceId,
                entity: createdEntity,
                value: createdValue
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteEntityResult = service.DeleteEntity(
                workspaceId: workspaceId,
                entity: createdEntity
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId
                );

            Assert.IsNotNull(listSynonymsResult);
            Assert.IsNotNull(createSynonymResult);
            Assert.IsTrue(createSynonymResult.Result._Synonym == createdSynonym);
            Assert.IsNotNull(getSynonymResult);
            Assert.IsTrue(getSynonymResult.Result._Synonym == createdSynonym);
            Assert.IsNotNull(updateSynonymResult);
            Assert.IsTrue(updateSynonymResult.Result._Synonym == updatedSynonym);
            Assert.IsNotNull(deleteSynonymResult);
        }
        #endregion

        #region Intents
        [TestMethod]
        public void TestIntents_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                 name: createdWorkspaceName,
                 description: createdWorkspaceDescription,
                 language: createdWorkspaceLanguage,
                 learningOptOut: true
                 );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;

            service.WithHeader("X-Watson-Test", "1");
            var listIntentsReult = service.ListIntents(
                workspaceId: workspaceId,
                export: true,
                pageLimit: 1,
                includeCount: true,
                sort: "-updated",
                includeAudit: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var createIntentResult = service.CreateIntent(
                workspaceId: workspaceId,
                intent: createdIntent,
                description: createdIntentDescription
                );

            service.WithHeader("X-Watson-Test", "1");
            var getIntentResult = service.GetIntent(
                workspaceId: workspaceId,
                intent: createdIntent
                );

            string updatedIntent = createdIntent + "-updated";
            string updatedIntentDescription = createdIntentDescription + "-updated";

            service.WithHeader("X-Watson-Test", "1");
            var updateIntentResult = service.UpdateIntent(
                workspaceId: workspaceId,
                intent: createdIntent,
                newIntent: updatedIntent,
                newDescription: updatedIntentDescription
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteIntentResult = service.DeleteIntent(
                workspaceId: workspaceId,
                intent: updatedIntent
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(workspaceId);

            Assert.IsNotNull(listIntentsReult);
            Assert.IsNotNull(createIntentResult);
            Assert.IsFalse(string.IsNullOrEmpty(createIntentResult.Result._Intent));
            Assert.IsTrue(createIntentResult.Result._Intent == createdIntent);
            Assert.IsNotNull(getIntentResult);
            Assert.IsFalse(string.IsNullOrEmpty(getIntentResult.Result._Intent));
            Assert.IsNotNull(updateIntentResult);
            Assert.IsTrue(updateIntentResult.Result._Intent == updatedIntent);
            Assert.IsNotNull(deleteIntentResult);
        }
        #endregion

        #region Examples
        [TestMethod]
        public void TestExamples_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                name: createdWorkspaceName,
                description: createdWorkspaceDescription,
                language: createdWorkspaceLanguage,
                learningOptOut: true
                );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;

            service.WithHeader("X-Watson-Test", "1");
            var createIntentResult = service.CreateIntent(
                workspaceId: workspaceId,
                intent: createdIntent,
                description: createdIntentDescription
                );

            service.WithHeader("X-Watson-Test", "1");
            var listExamplesResult = service.ListExamples(
                workspaceId: workspaceId,
                intent: createdIntent,
                pageLimit: 1,
                includeCount: true,
                sort: "-updated",
                includeAudit: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var createExampleResult = service.CreateExample(
                workspaceId: workspaceId,
                intent: createdIntent,
                text: createdExample
                );

            service.WithHeader("X-Watson-Test", "1");
            var getExampleResult = service.GetExample(
                workspaceId: workspaceId,
                intent: createdIntent,
                text: createdExample,
                includeAudit: true
                );

            string updatedExample = createdExample + "-updated";
            service.WithHeader("X-Watson-Test", "1");
            var updateExampleResult = service.UpdateExample(
                workspaceId: workspaceId,
                intent: createdIntent,
                text: createdExample,
                newText: updatedExample
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteExampleResult = service.DeleteExample(
                workspaceId: workspaceId,
                intent: createdIntent,
                text: updatedExample
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteIntentResult = service.DeleteIntent(
                workspaceId: workspaceId,
                intent: createdIntent
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId
                );

            Assert.IsNotNull(listExamplesResult);
            Assert.IsNotNull(createExampleResult);
            Assert.IsTrue(createExampleResult.Result.Text == createdExample);
            Assert.IsNotNull(getExampleResult);
            Assert.IsTrue(getExampleResult.Result.Text == createdExample);
            Assert.IsNotNull(updateExampleResult);
            Assert.IsTrue(updateExampleResult.Result.Text == updatedExample);
            Assert.IsNotNull(deleteExampleResult);
        }
        #endregion

        #region Dialog Nodes
        [TestMethod]
        public void TestDialogNodes_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                name: createdWorkspaceName,
                description: createdWorkspaceDescription,
                language: createdWorkspaceLanguage,
                learningOptOut: true
                );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;

            service.WithHeader("X-Watson-Test", "1");
            var listDialogNodes = service.ListDialogNodes(
                workspaceId: workspaceId
                );

            service.WithHeader("X-Watson-Test", "1");
            var createDialogNodeResult = service.CreateDialogNode(
                workspaceId: workspaceId,
                dialogNode: dialogNodeName,
                description: dialogNodeDesc
                );

            service.WithHeader("X-Watson-Test", "1");
            var getDialogNodeResult = service.GetDialogNode(
                workspaceId: workspaceId,
                dialogNode: dialogNodeName,
                includeAudit: true
                );

            string updatedDialogNodeName = dialogNodeName + "_updated";
            string updatedDialogNodeDescription = dialogNodeDesc + "_updated";
            service.WithHeader("X-Watson-Test", "1");
            var updateDialogNodeResult = service.UpdateDialogNode(
                workspaceId: workspaceId,
                dialogNode: dialogNodeName,
                newDialogNode: updatedDialogNodeName,
                newDescription: updatedDialogNodeDescription
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteDialogNodeResult = service.DeleteDialogNode(
                workspaceId: workspaceId,
                dialogNode: updatedDialogNodeName
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId
                );

            Assert.IsNotNull(listDialogNodes);
            Assert.IsNotNull(createDialogNodeResult);
            Assert.IsTrue(createDialogNodeResult.Result._DialogNode == dialogNodeName);
            Assert.IsTrue(createDialogNodeResult.Result.Description == dialogNodeDesc);
            Assert.IsNotNull(getDialogNodeResult);
            Assert.IsTrue(getDialogNodeResult.Result._DialogNode == dialogNodeName);
            Assert.IsTrue(getDialogNodeResult.Result.Description == dialogNodeDesc);
            Assert.IsNotNull(updateDialogNodeResult);
            Assert.IsTrue(updateDialogNodeResult.Result._DialogNode == updatedDialogNodeName);
            Assert.IsTrue(updateDialogNodeResult.Result.Description == updatedDialogNodeDescription);
            Assert.IsNotNull(deleteDialogNodeResult);
        }
        #endregion

        #region Logs
        [TestMethod]
        public void ListLogs_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                name: createdWorkspaceName,
                description: createdWorkspaceDescription,
                language: createdWorkspaceLanguage,
                learningOptOut: true
                );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;
            service.WithHeader("X-Watson-Test", "1");
            var listLogsResult = service.ListLogs(
                workspaceId: workspaceId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId
                );

            Assert.IsNotNull(listLogsResult.Result);
            Assert.IsNotNull(listLogsResult.Result.Logs);
        }

        [TestMethod]
        public void ListAllLogs_Success()
        {
            var filter = "(language::en,request.context.metadata.deployment::deployment_1)";
            service.WithHeader("X-Watson-Test", "1");
            var listAllLogsResult = service.ListAllLogs(filter);
            Assert.IsNotNull(listAllLogsResult.Result);
            Assert.IsNotNull(listAllLogsResult.Result.Logs);
        }
        #endregion

        #region Mentions
        [TestMethod]
        public void TestMentions_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceResult = service.CreateWorkspace(
                 name: createdWorkspaceName,
                 description: createdWorkspaceDescription,
                 language: createdWorkspaceLanguage,
                 learningOptOut: true
                 );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;

            CreateEntity entity = new CreateEntity()
            {
                Entity = createdEntity,
                Description = createdEntityDescription
            };

            service.WithHeader("X-Watson-Test", "1");
            var createEntityResult = service.CreateEntity(
                workspaceId: workspaceId,
                entity: createdEntity,
                description: createdEntityDescription
                );

            service.WithHeader("X-Watson-Test", "1");
            var ListMentionsResult = service.ListMentions(
                workspaceId: workspaceId,
                entity: createdEntity
                );

            Assert.IsNotNull(createEntityResult);
            Assert.IsFalse(string.IsNullOrEmpty(createEntityResult.Result._Entity));
            Assert.IsNotNull(ListMentionsResult.Result);
            Assert.IsNotNull(ListMentionsResult.Result.Examples);
        }
        #endregion
    }
}