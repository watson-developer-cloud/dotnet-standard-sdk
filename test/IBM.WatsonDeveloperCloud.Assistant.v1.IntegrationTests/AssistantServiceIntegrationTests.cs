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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using IBM.WatsonDeveloperCloud.Assistant.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Assistant.v1.IntegrationTests
{
    [TestClass]
    public class AssistantServiceIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private AssistantService service;
        private static string credentials = string.Empty;

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
                _endpoint = vcapServices["assistant"]["url"].Value<string>();
                _username = vcapServices["assistant"]["username"].Value<string>();
                _password = vcapServices["assistant"]["password"].Value<string>();
            }

            service = new AssistantService(_username, _password, AssistantService.ASSISTANT_);
            service.Endpoint = _endpoint;
        }

        #region Message
        private MessageResponse Message(string workspaceId, MessageRequest request = null, bool? nodesVisitedDetails = null)
        {
            Console.WriteLine("\nAttempting to Message()");
            var result = service.Message(workspaceId:workspaceId, request:request, nodesVisitedDetails:nodesVisitedDetails);

            if(result != null)
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
        private Workspace CreateWorkspace(CreateWorkspace properties = null)
        {
            Console.WriteLine("\nAttempting to CreateWorkspace()");
            var result = service.CreateWorkspace(properties:properties);

            if(result != null)
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
            var result = service.DeleteWorkspace(workspaceId:workspaceId);

            if(result != null)
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
        private WorkspaceExport GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to GetWorkspace()");
            var result = service.GetWorkspace(workspaceId:workspaceId, export:export, includeAudit:includeAudit);

            if(result != null)
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
        private WorkspaceCollection ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to ListWorkspaces()");
            var result = service.ListWorkspaces(pageLimit:pageLimit, includeCount:includeCount, sort:sort, cursor:cursor, includeAudit:includeAudit);

            if(result != null)
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
        private Workspace UpdateWorkspace(string workspaceId, UpdateWorkspace properties = null, bool? append = null)
        {
            Console.WriteLine("\nAttempting to UpdateWorkspace()");
            var result = service.UpdateWorkspace(workspaceId:workspaceId, properties:properties, append:append);

            if(result != null)
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
        private Intent CreateIntent(string workspaceId, CreateIntent body)
        {
            Console.WriteLine("\nAttempting to CreateIntent()");
            var result = service.CreateIntent(workspaceId:workspaceId, body:body);

            if(result != null)
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
            var result = service.DeleteIntent(workspaceId:workspaceId, intent:intent);

            if(result != null)
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
        private IntentExport GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to GetIntent()");
            var result = service.GetIntent(workspaceId:workspaceId, intent:intent, export:export, includeAudit:includeAudit);

            if(result != null)
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
        private IntentCollection ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to ListIntents()");
            var result = service.ListIntents(workspaceId:workspaceId, export:export, pageLimit:pageLimit, includeCount:includeCount, sort:sort, cursor:cursor, includeAudit:includeAudit);

            if(result != null)
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
            var result = service.UpdateIntent(workspaceId:workspaceId, intent:intent, body:body);

            if(result != null)
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
            var result = service.CreateExample(workspaceId:workspaceId, intent:intent, body:body);

            if(result != null)
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
            var result = service.DeleteExample(workspaceId:workspaceId, intent:intent, text:text);

            if(result != null)
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
        private Example GetExample(string workspaceId, string intent, string text, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to GetExample()");
            var result = service.GetExample(workspaceId:workspaceId, intent:intent, text:text, includeAudit:includeAudit);

            if(result != null)
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
        private ExampleCollection ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to ListExamples()");
            var result = service.ListExamples(workspaceId:workspaceId, intent:intent, pageLimit:pageLimit, includeCount:includeCount, sort:sort, cursor:cursor, includeAudit:includeAudit);

            if(result != null)
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
            var result = service.UpdateExample(workspaceId:workspaceId, intent:intent, text:text, body:body);

            if(result != null)
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
        private Counterexample CreateCounterexample(string workspaceId, CreateCounterexample body)
        {
            Console.WriteLine("\nAttempting to CreateCounterexample()");
            var result = service.CreateCounterexample(workspaceId:workspaceId, body:body);

            if(result != null)
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
            var result = service.DeleteCounterexample(workspaceId:workspaceId, text:text);

            if(result != null)
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
        private Counterexample GetCounterexample(string workspaceId, string text, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to GetCounterexample()");
            var result = service.GetCounterexample(workspaceId:workspaceId, text:text, includeAudit:includeAudit);

            if(result != null)
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
        private CounterexampleCollection ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to ListCounterexamples()");
            var result = service.ListCounterexamples(workspaceId:workspaceId, pageLimit:pageLimit, includeCount:includeCount, sort:sort, cursor:cursor, includeAudit:includeAudit);

            if(result != null)
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
            var result = service.UpdateCounterexample(workspaceId:workspaceId, text:text, body:body);

            if(result != null)
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
        private Entity CreateEntity(string workspaceId, CreateEntity properties)
        {
            Console.WriteLine("\nAttempting to CreateEntity()");
            var result = service.CreateEntity(workspaceId:workspaceId, properties:properties);

            if(result != null)
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
            var result = service.DeleteEntity(workspaceId:workspaceId, entity:entity);

            if(result != null)
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
        private EntityExport GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to GetEntity()");
            var result = service.GetEntity(workspaceId:workspaceId, entity:entity, export:export, includeAudit:includeAudit);

            if(result != null)
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
        private EntityCollection ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to ListEntities()");
            var result = service.ListEntities(workspaceId:workspaceId, export:export, pageLimit:pageLimit, includeCount:includeCount, sort:sort, cursor:cursor, includeAudit:includeAudit);

            if(result != null)
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
            var result = service.UpdateEntity(workspaceId:workspaceId, entity:entity, properties:properties);

            if(result != null)
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
        private Value CreateValue(string workspaceId, string entity, CreateValue properties)
        {
            Console.WriteLine("\nAttempting to CreateValue()");
            var result = service.CreateValue(workspaceId:workspaceId, entity:entity, properties:properties);

            if(result != null)
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
            var result = service.DeleteValue(workspaceId:workspaceId, entity:entity, value:value);

            if(result != null)
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
        private ValueExport GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to GetValue()");
            var result = service.GetValue(workspaceId:workspaceId, entity:entity, value:value, export:export, includeAudit:includeAudit);

            if(result != null)
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
        private ValueCollection ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to ListValues()");
            var result = service.ListValues(workspaceId:workspaceId, entity:entity, export:export, pageLimit:pageLimit, includeCount:includeCount, sort:sort, cursor:cursor, includeAudit:includeAudit);

            if(result != null)
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
        private Value UpdateValue(string workspaceId, string entity, string value, UpdateValue properties)
        {
            Console.WriteLine("\nAttempting to UpdateValue()");
            var result = service.UpdateValue(workspaceId:workspaceId, entity:entity, value:value, properties:properties);

            if(result != null)
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
            var result = service.CreateSynonym(workspaceId:workspaceId, entity:entity, value:value, body:body);

            if(result != null)
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
            var result = service.DeleteSynonym(workspaceId:workspaceId, entity:entity, value:value, synonym:synonym);

            if(result != null)
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
        private Synonym GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to GetSynonym()");
            var result = service.GetSynonym(workspaceId:workspaceId, entity:entity, value:value, synonym:synonym, includeAudit:includeAudit);

            if(result != null)
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
        private SynonymCollection ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to ListSynonyms()");
            var result = service.ListSynonyms(workspaceId:workspaceId, entity:entity, value:value, pageLimit:pageLimit, includeCount:includeCount, sort:sort, cursor:cursor, includeAudit:includeAudit);

            if(result != null)
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
            var result = service.UpdateSynonym(workspaceId:workspaceId, entity:entity, value:value, synonym:synonym, body:body);

            if(result != null)
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
            var result = service.CreateDialogNode(workspaceId:workspaceId, properties:properties);

            if(result != null)
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
            var result = service.DeleteDialogNode(workspaceId:workspaceId, dialogNode:dialogNode);

            if(result != null)
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
        private DialogNode GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to GetDialogNode()");
            var result = service.GetDialogNode(workspaceId:workspaceId, dialogNode:dialogNode, includeAudit:includeAudit);

            if(result != null)
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
        private DialogNodeCollection ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null)
        {
            Console.WriteLine("\nAttempting to ListDialogNodes()");
            var result = service.ListDialogNodes(workspaceId:workspaceId, pageLimit:pageLimit, includeCount:includeCount, sort:sort, cursor:cursor, includeAudit:includeAudit);

            if(result != null)
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
            var result = service.UpdateDialogNode(workspaceId:workspaceId, dialogNode:dialogNode, properties:properties);

            if(result != null)
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
        private LogCollection ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null)
        {
            Console.WriteLine("\nAttempting to ListAllLogs()");
            var result = service.ListAllLogs(filter:filter, sort:sort, pageLimit:pageLimit, cursor:cursor);

            if(result != null)
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
        private LogCollection ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null)
        {
            Console.WriteLine("\nAttempting to ListLogs()");
            var result = service.ListLogs(workspaceId:workspaceId, sort:sort, filter:filter, pageLimit:pageLimit, cursor:cursor);

            if(result != null)
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

    }
}