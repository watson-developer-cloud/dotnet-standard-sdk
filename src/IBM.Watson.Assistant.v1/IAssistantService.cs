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

using System.Collections.Generic;
using IBM.Watson.Assistant.v1.Model;

namespace IBM.Watson.Assistant.v1
{
    public partial interface IAssistantService
    {
        MessageResponse Message(string workspaceId, MessageRequest request = null, bool? nodesVisitedDetails = null, Dictionary<string, object> customData = null);
        Workspace CreateWorkspace(CreateWorkspace properties = null, Dictionary<string, object> customData = null);
        BaseModel DeleteWorkspace(string workspaceId, Dictionary<string, object> customData = null);
        WorkspaceExport GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null, string sort = null, Dictionary<string, object> customData = null);
        WorkspaceCollection ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        Workspace UpdateWorkspace(string workspaceId, UpdateWorkspace properties = null, bool? append = null, Dictionary<string, object> customData = null);
        Intent CreateIntent(string workspaceId, CreateIntent body, Dictionary<string, object> customData = null);
        BaseModel DeleteIntent(string workspaceId, string intent, Dictionary<string, object> customData = null);
        IntentExport GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        IntentCollection ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        Intent UpdateIntent(string workspaceId, string intent, UpdateIntent body, Dictionary<string, object> customData = null);
        Example CreateExample(string workspaceId, string intent, CreateExample body, Dictionary<string, object> customData = null);
        BaseModel DeleteExample(string workspaceId, string intent, string text, Dictionary<string, object> customData = null);
        Example GetExample(string workspaceId, string intent, string text, bool? includeAudit = null, Dictionary<string, object> customData = null);
        ExampleCollection ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        Example UpdateExample(string workspaceId, string intent, string text, UpdateExample body, Dictionary<string, object> customData = null);
        Counterexample CreateCounterexample(string workspaceId, CreateCounterexample body, Dictionary<string, object> customData = null);
        BaseModel DeleteCounterexample(string workspaceId, string text, Dictionary<string, object> customData = null);
        Counterexample GetCounterexample(string workspaceId, string text, bool? includeAudit = null, Dictionary<string, object> customData = null);
        CounterexampleCollection ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        Counterexample UpdateCounterexample(string workspaceId, string text, UpdateCounterexample body, Dictionary<string, object> customData = null);
        Entity CreateEntity(string workspaceId, CreateEntity properties, Dictionary<string, object> customData = null);
        BaseModel DeleteEntity(string workspaceId, string entity, Dictionary<string, object> customData = null);
        EntityExport GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        EntityCollection ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        Entity UpdateEntity(string workspaceId, string entity, UpdateEntity properties, Dictionary<string, object> customData = null);
        EntityMentionCollection ListMentions(string workspaceId, string entity, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        Value CreateValue(string workspaceId, string entity, CreateValue properties, Dictionary<string, object> customData = null);
        BaseModel DeleteValue(string workspaceId, string entity, string value, Dictionary<string, object> customData = null);
        ValueExport GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        ValueCollection ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        Value UpdateValue(string workspaceId, string entity, string value, UpdateValue properties, Dictionary<string, object> customData = null);
        Synonym CreateSynonym(string workspaceId, string entity, string value, CreateSynonym body, Dictionary<string, object> customData = null);
        BaseModel DeleteSynonym(string workspaceId, string entity, string value, string synonym, Dictionary<string, object> customData = null);
        Synonym GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null, Dictionary<string, object> customData = null);
        SynonymCollection ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        Synonym UpdateSynonym(string workspaceId, string entity, string value, string synonym, UpdateSynonym body, Dictionary<string, object> customData = null);
        DialogNode CreateDialogNode(string workspaceId, CreateDialogNode properties, Dictionary<string, object> customData = null);
        BaseModel DeleteDialogNode(string workspaceId, string dialogNode, Dictionary<string, object> customData = null);
        DialogNode GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null, Dictionary<string, object> customData = null);
        DialogNodeCollection ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        DialogNode UpdateDialogNode(string workspaceId, string dialogNode, UpdateDialogNode properties, Dictionary<string, object> customData = null);
        LogCollection ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null, Dictionary<string, object> customData = null);
        LogCollection ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null, Dictionary<string, object> customData = null);
        BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null);
    }
}
