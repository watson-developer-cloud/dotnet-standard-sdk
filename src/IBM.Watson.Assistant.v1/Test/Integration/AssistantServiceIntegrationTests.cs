/**
* (C) Copyright IBM Corp. 2018, 2022.
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
using IBM.Cloud.SDK.Core.Authentication.Iam;
using System.Threading;
using IBM.Cloud.SDK.Core.Http;

namespace IBM.Watson.Assistant.v1.IntegrationTests
{
    [TestClass]
    public class AssistantServiceIntegrationTests
    {
        private AssistantService service;
        private static string credentials = string.Empty;
        private static string versionDate = "2019-02-28";

        private static string workspaceId;
        private static string apikey;

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
            var creds = CredentialUtils.GetServiceProperties("assistant");
            creds.TryGetValue("WORKSPACE_ID", out workspaceId);
            service = new AssistantService(versionDate);


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
            Assert.IsTrue(results1.Result.Entities[1].Entity == "sys-date");
            Assert.IsTrue(results1.Result.Entities[1].Interpretation.Timezone == "GMT");
            Assert.IsTrue(results1.Result.Entities[1].Interpretation.Granularity == RuntimeEntityInterpretation.GranularityEnumValue.DAY);
            Assert.IsTrue(results1.Result.Entities[1].Interpretation.Festival == "christmas");
            Assert.IsTrue(results1.Result.Entities[1].Interpretation.CalendarType == "GREGORIAN");
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
                text: createdCounterExampleText,
                includeAudit: true
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
                newText: updatedCounterExampleText,
                includeAudit: true
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
            Assert.IsNotNull(createCounterexampleResult.Result.Created);
            Assert.IsNotNull(createCounterexampleResult.Result.Updated);
            Assert.IsFalse(string.IsNullOrEmpty(createCounterexampleResult.Result.Text));
            Assert.IsNotNull(getCounterexampleResult);
            Assert.IsFalse(string.IsNullOrEmpty(getCounterexampleResult.Result.Text));
            Assert.IsNotNull(updateCounterexampleResult);
            Assert.IsNotNull(updateCounterexampleResult.Result.Created);
            Assert.IsNotNull(updateCounterexampleResult.Result.Updated);
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
            var metadata = new Dictionary<string, object>();
            metadata.Add("name", "Watson");
            var systemSettings = new WorkspaceSystemSettings()
            {
                Tooling = new WorkspaceSystemSettingsTooling()
                {
                    StoreGenericResponses = false
                },
                Disambiguation = new WorkspaceSystemSettingsDisambiguation()
                {
                    Sensitivity = WorkspaceSystemSettingsDisambiguation.SensitivityEnumValue.AUTO,
                    Prompt = "Hello, welcome to watson",
                    NoneOfTheAbovePrompt = "None of the above",
                    Enabled = true,
                    Randomize = true,
                    MaxSuggestions = 5
                },
                OffTopic = new WorkspaceSystemSettingsOffTopic()
                {
                    Enabled = true
                },
                SystemEntities = new WorkspaceSystemSettingsSystemEntities()
                {
                    Enabled = true
                }
            };
            var webhooks = new List<Webhook>()
            {
                new Webhook()
                {
                    Url = "http://www.cloud.ibm.com",
                    Name = "IBM Cloud",
                    Headers = new List<WebhookHeader>()
                    {
                        new WebhookHeader(){
                            Name = "testWebhookHeaderName",
                            Value = "testWebhookHeaderValue"
                        }
                    }
                }
            };
            var createWorkspaceResult = service.CreateWorkspace(
                name: createdWorkspaceName,
                description: createdWorkspaceDescription,
                language: createdWorkspaceLanguage, 
                metadata: metadata, 
                learningOptOut: false, 
                systemSettings: systemSettings, 
                webhooks: webhooks,
                includeAudit: true
                );

            var workspaceId = createWorkspaceResult.Result.WorkspaceId;

            service.WithHeader("X-Watson-Test", "1");
            var getWorkspaceResult = service.GetWorkspace(
                workspaceId: workspaceId,
                export: true,
                includeAudit: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var updatedMetadata = new Dictionary<string, object>();
            updatedMetadata.Add("name", "Watson-updated");
            var updatedSystemSettings = new WorkspaceSystemSettings()
            {
                Tooling = new WorkspaceSystemSettingsTooling()
                {
                    StoreGenericResponses = true
                },
                Disambiguation = new WorkspaceSystemSettingsDisambiguation()
                {
                    Sensitivity = WorkspaceSystemSettingsDisambiguation.SensitivityEnumValue.AUTO,
                    Prompt = "Hello, welcome to Watson",
                    NoneOfTheAbovePrompt = "I didn't understand",
                    Enabled = true,
                    Randomize = true,
                    MaxSuggestions = 3
                },
                OffTopic = new WorkspaceSystemSettingsOffTopic()
                {
                    Enabled = false
                }
            };
            var updatedWebhooks = new List<Webhook>()
            {
                new Webhook()
                {
                    Url = "http://www.cloud.ibm.com/apidocs",
                    Name = "IBM Cloud Docs",
                    Headers = new List<WebhookHeader>()
                    {
                        new WebhookHeader(){
                            Name = "testWebhookHeaderName-updated",
                            Value = "testWebhookHeaderValue-updated"
                        }
                    }
                }
            };
            var updateWorkspaceResult = service.UpdateWorkspace(
                workspaceId: workspaceId,
                name: createdWorkspaceName + "-updated",
                description: createdWorkspaceDescription + "-updated",
                language: createdWorkspaceLanguage,
                learningOptOut: true,
                metadata: updatedMetadata,
                systemSettings: updatedSystemSettings,
                webhooks: updatedWebhooks,
                includeAudit: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteWorkspaceResult = service.DeleteWorkspace(
                workspaceId: workspaceId);

            Assert.IsNotNull(createWorkspaceResult);
            Assert.IsNotNull(workspaceId);
            Assert.IsNotNull(createWorkspaceResult.Result.Metadata);
            Assert.IsNotNull(createWorkspaceResult.Result.Metadata["name"].ToString() == "Watson");
            Assert.IsNotNull(createWorkspaceResult.Result.Created);
            Assert.IsNotNull(createWorkspaceResult.Result.Updated);
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.SystemEntities.Enabled == true);
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.Tooling.StoreGenericResponses == false);
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.Disambiguation.Sensitivity == WorkspaceSystemSettingsDisambiguation.SensitivityEnumValue.AUTO);
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.Disambiguation.Prompt == "Hello, welcome to watson");
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.Disambiguation.NoneOfTheAbovePrompt == "None of the above");
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.Disambiguation.Enabled == true);
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.Disambiguation.Randomize == true);
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.Disambiguation.MaxSuggestions == 5);
            Assert.IsNotNull(createWorkspaceResult.Result.SystemSettings.OffTopic.Enabled == true);
            Assert.IsTrue(createWorkspaceResult.Result.Webhooks[0].Name == "IBM Cloud");
            Assert.IsNotNull(createWorkspaceResult.Result.Webhooks[0].Headers);
            Assert.IsTrue(createWorkspaceResult.Result.Webhooks[0].Headers[0].Name == "testWebhookHeaderName");
            Assert.IsTrue(createWorkspaceResult.Result.Webhooks[0].Headers[0].Value == "testWebhookHeaderValue");
            Assert.IsNotNull(getWorkspaceResult);
            Assert.IsNotNull(updateWorkspaceResult);
            Assert.IsNotNull(workspaceId);
            Assert.IsTrue(updateWorkspaceResult.Result.Name == createdWorkspaceName + "-updated");
            Assert.IsTrue(updateWorkspaceResult.Result.Description == createdWorkspaceDescription + "-updated");
            Assert.IsNotNull(updateWorkspaceResult.Result.Metadata);
            Assert.IsNotNull(updateWorkspaceResult.Result.Metadata["name"].ToString() == "Watson-updated");
            Assert.IsNotNull(updateWorkspaceResult.Result.SystemSettings.Tooling.StoreGenericResponses == true);
            Assert.IsNotNull(updateWorkspaceResult.Result.SystemSettings.Disambiguation.Sensitivity == WorkspaceSystemSettingsDisambiguation.SensitivityEnumValue.AUTO);
            Assert.IsNotNull(updateWorkspaceResult.Result.SystemSettings.Disambiguation.Prompt == "Hello, welcome to Watson");
            Assert.IsNotNull(updateWorkspaceResult.Result.SystemSettings.Disambiguation.NoneOfTheAbovePrompt == "I didn't understand");
            Assert.IsNotNull(updateWorkspaceResult.Result.SystemSettings.Disambiguation.Enabled == true);
            Assert.IsNotNull(updateWorkspaceResult.Result.SystemSettings.Disambiguation.Randomize == true);
            Assert.IsNotNull(updateWorkspaceResult.Result.SystemSettings.Disambiguation.MaxSuggestions == 3);
            Assert.IsNotNull(updateWorkspaceResult.Result.SystemSettings.OffTopic.Enabled == false);
            Assert.IsTrue(updateWorkspaceResult.Result.Webhooks[0].Name == "IBM Cloud Docs");
            Assert.IsNotNull(updateWorkspaceResult.Result.Created);
            Assert.IsNotNull(updateWorkspaceResult.Result.Updated);
            Assert.IsNotNull(updateWorkspaceResult.Result.Webhooks[0].Headers);
            Assert.IsTrue(updateWorkspaceResult.Result.Webhooks[0].Headers[0].Name == "testWebhookHeaderName-updated");
            Assert.IsTrue(updateWorkspaceResult.Result.Webhooks[0].Headers[0].Value == "testWebhookHeaderValue-updated");
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
                sort: "-updated",
                includeAudit: true);

            service.WithHeader("X-Watson-Test", "1");
            var createEntityResult = service.CreateEntity(
                workspaceId: workspaceId,
                entity: createdEntity,
                description: createdEntityDescription,
                fuzzyMatch: true,
                includeAudit: true
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
                newFuzzyMatch: true,
                append: true,
                includeAudit: true
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
            Assert.IsNotNull(createEntityResult.Result.Created);
            Assert.IsNotNull(createEntityResult.Result.Updated);
            Assert.IsFalse(string.IsNullOrEmpty(createEntityResult.Result._Entity));
            Assert.IsNotNull(getEntityResult);
            Assert.IsTrue(getEntityResult.Result._Entity == createdEntity);
            Assert.IsNotNull(updateEntityResult);
            Assert.IsNotNull(updateEntityResult.Result.Created);
            Assert.IsNotNull(updateEntityResult.Result.Updated);
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
                value: createdValue,
                includeAudit: true
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
                newValue: updatedValue,
                append: true,
                includeAudit: true
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
            Assert.IsNotNull(createValueResult.Result.Created);
            Assert.IsNotNull(createValueResult.Result.Updated);
            Assert.IsFalse(string.IsNullOrEmpty(createValueResult.Result._Value));
            Assert.IsNotNull(getValueResult);
            Assert.IsTrue(getValueResult.Result._Value == createdValue);
            Assert.IsNotNull(updateValueResult);
            Assert.IsNotNull(updateValueResult.Result.Created);
            Assert.IsNotNull(updateValueResult.Result.Updated);
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
                synonym: createdSynonym,
                includeAudit: true
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
                newSynonym: updatedSynonym,
                includeAudit: true
                );

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
            Assert.IsNotNull(createSynonymResult.Result.Created);
            Assert.IsNotNull(createSynonymResult.Result.Updated);
            Assert.IsTrue(createSynonymResult.Result._Synonym == createdSynonym);
            Assert.IsNotNull(getSynonymResult);
            Assert.IsTrue(getSynonymResult.Result._Synonym == createdSynonym);
            Assert.IsNotNull(updateSynonymResult);
            Assert.IsNotNull(updateSynonymResult.Result.Created);
            Assert.IsNotNull(updateSynonymResult.Result.Updated);
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
                sort: "-updated",
                includeAudit: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var createIntentResult = service.CreateIntent(
                workspaceId: workspaceId,
                intent: createdIntent,
                description: createdIntentDescription,
                includeAudit: true
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
                newDescription: updatedIntentDescription,
                append:true,
                includeAudit: true
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
            Assert.IsNotNull(createIntentResult.Result.Created);
            Assert.IsNotNull(createIntentResult.Result.Updated);
            Assert.IsFalse(string.IsNullOrEmpty(createIntentResult.Result._Intent));
            Assert.IsTrue(createIntentResult.Result._Intent == createdIntent);
            Assert.IsNotNull(getIntentResult);
            Assert.IsFalse(string.IsNullOrEmpty(getIntentResult.Result._Intent));
            Assert.IsNotNull(updateIntentResult);
            Assert.IsNotNull(updateIntentResult.Result);
            Assert.IsNotNull(updateIntentResult.Result.Created);
            Assert.IsNotNull(updateIntentResult.Result.Updated);
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
                sort: "-updated",
                includeAudit: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var createExampleResult = service.CreateExample(
                workspaceId: workspaceId,
                intent: createdIntent,
                text: createdExample,
                includeAudit: true
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
                newText: updatedExample,
                includeAudit: true
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
            Assert.IsNotNull(createExampleResult.Result.Created);
            Assert.IsNotNull(createExampleResult.Result.Updated);
            Assert.IsTrue(createExampleResult.Result.Text == createdExample);
            Assert.IsNotNull(getExampleResult);
            Assert.IsTrue(getExampleResult.Result.Text == createdExample);
            Assert.IsNotNull(updateExampleResult);
            Assert.IsNotNull(updateExampleResult.Result.Created);
            Assert.IsNotNull(updateExampleResult.Result.Updated);
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
                description: dialogNodeDesc,
                disambiguationOptOut: true,
                includeAudit: true
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
                newDescription: updatedDialogNodeDescription,
                newDisambiguationOptOut: true,
                includeAudit: true
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
            Assert.IsNotNull(createDialogNodeResult.Result.Created);
            Assert.IsNotNull(createDialogNodeResult.Result.Updated);
            Assert.IsTrue(createDialogNodeResult.Result._DialogNode == dialogNodeName);
            Assert.IsTrue(createDialogNodeResult.Result.Description == dialogNodeDesc);
            Assert.IsTrue(createDialogNodeResult.Result.DisambiguationOptOut == true);
            Assert.IsNotNull(getDialogNodeResult);
            Assert.IsTrue(getDialogNodeResult.Result._DialogNode == dialogNodeName);
            Assert.IsTrue(getDialogNodeResult.Result.Description == dialogNodeDesc);
            Assert.IsNotNull(updateDialogNodeResult);
            Assert.IsNotNull(updateDialogNodeResult.Result.Created);
            Assert.IsNotNull(updateDialogNodeResult.Result.Updated);
            Assert.IsTrue(updateDialogNodeResult.Result._DialogNode == updatedDialogNodeName);
            Assert.IsTrue(updateDialogNodeResult.Result.Description == updatedDialogNodeDescription);
            Assert.IsTrue(updateDialogNodeResult.Result.DisambiguationOptOut == true);
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

        #region Bulk Classify
        //[TestMethod]
        public void TestBulk_Classify()
        {
            service.WithHeader("X-Watson-Test", "1");
            List<BulkClassifyUtterance> bulkClassifyUtterances = new List<BulkClassifyUtterance>();
            BulkClassifyUtterance bulkClassifyUtterance = new BulkClassifyUtterance();
            bulkClassifyUtterance.Text = "help I need help";
            bulkClassifyUtterances.Add(bulkClassifyUtterance);
            var bulkClassifyResponse = service.BulkClassify(workspaceId, bulkClassifyUtterances);

            Assert.IsNotNull(bulkClassifyResponse);
        }
        #endregion

        #region Miscellaneous
        [TestMethod]
        public void TestRuntimeResponseGenericRuntimeResponseTypeChannelTransfer()
        {
            service.WithHeader("X-Watson-Test", "1");

            MessageInput input = new MessageInput();
            input.Text = "test sdk";

            var response = service.Message(
                workspaceId: workspaceId,
                input: input
                );
            Assert.IsNotNull(response);

            RuntimeResponseGenericRuntimeResponseTypeChannelTransfer
                runtimeResponseGenericRuntimeResponseTypeChannelTransfer =
                    (RuntimeResponseGenericRuntimeResponseTypeChannelTransfer)response.Result.Output.Generic[0];
            ChannelTransferInfo channelTransferInfo =
                runtimeResponseGenericRuntimeResponseTypeChannelTransfer.TransferInfo;
            Assert.IsNotNull(channelTransferInfo);
        }

        [TestMethod]
        public void TestRuntimeResponseGeneric()
        {
            service.WithHeader("X-Watson-Test", "1");

            string[] inputStrings = { "audio", "iframe", "video" };

            foreach (string inputMessage in inputStrings)
            {
                MessageInput input = new MessageInput();
                input.Text = inputMessage;

                var response = service.Message(
                workspaceId: workspaceId,
                input: input
                );

                Assert.IsNotNull(response);
                Assert.IsTrue(response.Result.Output.Generic[0].ResponseType.Contains(inputMessage));
            }
        }

        [TestMethod]
        public void TestWorkspaceAsync()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createWorkspaceAsyncResult = service.CreateWorkspaceAsync(
                name: createdWorkspaceName,
                description: createdWorkspaceDescription,
                language: createdWorkspaceLanguage
                );
            var workspaceId = createWorkspaceAsyncResult.Result.WorkspaceId;

            DetailedResponse<Workspace> getWorkspaceResult = null;
            var workspaceStatus = createWorkspaceAsyncResult.Result.Status;
            while (workspaceStatus == Workspace.StatusEnumValue.PROCESSING)
            {
                Thread.Sleep(10000);
                getWorkspaceResult = service.GetWorkspace(
                        workspaceId: workspaceId
                        );
                workspaceStatus = getWorkspaceResult.Result.Status;
            }

            Assert.IsTrue(workspaceStatus == Workspace.StatusEnumValue.AVAILABLE);

            var updateWorkspaceAsyncResult = service.UpdateWorkspaceAsync(
                workspaceId: workspaceId,
                name: createdWorkspaceName,
                description: createdWorkspaceDescription + "-updated",
                language: createdWorkspaceLanguage,
                learningOptOut: true
                );

            workspaceStatus = Workspace.StatusEnumValue.PROCESSING;
            while (workspaceStatus == Workspace.StatusEnumValue.PROCESSING)
            {
                Thread.Sleep(10000);
                getWorkspaceResult = service.GetWorkspace(
                        workspaceId: workspaceId
                        );
                workspaceStatus = getWorkspaceResult.Result.Status;
            }
            Assert.IsTrue(workspaceStatus == Workspace.StatusEnumValue.AVAILABLE);
            Assert.IsTrue(getWorkspaceResult.Result.Description == createdWorkspaceDescription + "-updated");

            var exportWorkspaceAsyncResult = service.ExportWorkspaceAsync(
                workspaceId: workspaceId
                );
            workspaceStatus = Workspace.StatusEnumValue.PROCESSING;

            while(workspaceStatus == Workspace.StatusEnumValue.PROCESSING)
            {
                Thread.Sleep(10000);
                exportWorkspaceAsyncResult = service.ExportWorkspaceAsync(
                    workspaceId: workspaceId
                    );
                workspaceStatus = getWorkspaceResult.Result.Status;
            }

            Assert.IsTrue(workspaceStatus == Workspace.StatusEnumValue.AVAILABLE);
            Assert.IsTrue(exportWorkspaceAsyncResult.Result.Description == createdWorkspaceDescription + "-updated");
        }
        #endregion
    }
}
