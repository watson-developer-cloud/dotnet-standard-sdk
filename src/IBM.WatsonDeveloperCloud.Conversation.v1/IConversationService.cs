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
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace IBM.WatsonDeveloperCloud.Conversation.v1
{
    public interface IConversationService
    {
        Workspace CreateWorkspace(CreateWorkspace properties = null);

        object DeleteWorkspace(string workspaceId);

        WorkspaceExport GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null);

        WorkspaceCollection ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);

        Workspace UpdateWorkspace(string workspaceId, UpdateWorkspace properties = null, bool? append = null);
        MessageResponse Message(string workspaceId, MessageRequest request = null, bool? nodesVisitedDetails = null);
        Intent CreateIntent(string workspaceId, CreateIntent body);

        object DeleteIntent(string workspaceId, string intent);

        IntentExport GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null);

        IntentCollection ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);

        Intent UpdateIntent(string workspaceId, string intent, UpdateIntent body);
        Example CreateExample(string workspaceId, string intent, CreateExample body);

        object DeleteExample(string workspaceId, string intent, string text);

        Example GetExample(string workspaceId, string intent, string text, bool? includeAudit = null);

        ExampleCollection ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);

        Example UpdateExample(string workspaceId, string intent, string text, UpdateExample body);
        Entity CreateEntity(string workspaceId, CreateEntity properties);

        object DeleteEntity(string workspaceId, string entity);

        EntityExport GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null);

        EntityCollection ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);

        Entity UpdateEntity(string workspaceId, string entity, UpdateEntity properties);
        Value CreateValue(string workspaceId, string entity, CreateValue properties);

        object DeleteValue(string workspaceId, string entity, string value);

        ValueExport GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null);

        ValueCollection ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);

        Value UpdateValue(string workspaceId, string entity, string value, UpdateValue properties);
        Synonym CreateSynonym(string workspaceId, string entity, string value, CreateSynonym body);

        object DeleteSynonym(string workspaceId, string entity, string value, string synonym);

        Synonym GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null);

        SynonymCollection ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);

        Synonym UpdateSynonym(string workspaceId, string entity, string value, string synonym, UpdateSynonym body);
        DialogNode CreateDialogNode(string workspaceId, CreateDialogNode properties);

        object DeleteDialogNode(string workspaceId, string dialogNode);

        DialogNode GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null);

        DialogNodeCollection ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);

        DialogNode UpdateDialogNode(string workspaceId, string dialogNode, UpdateDialogNode properties);
        LogCollection ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null);

        LogCollection ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null);
        Counterexample CreateCounterexample(string workspaceId, CreateCounterexample body);

        object DeleteCounterexample(string workspaceId, string text);

        Counterexample GetCounterexample(string workspaceId, string text, bool? includeAudit = null);

        CounterexampleCollection ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);

        Counterexample UpdateCounterexample(string workspaceId, string text, UpdateCounterexample body);
    }
}
