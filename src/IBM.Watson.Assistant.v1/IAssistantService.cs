/**
* (C) Copyright IBM Corp. 2018, 2020.
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

using IBM.Cloud.SDK.Core.Http;
using System.Collections.Generic;
using IBM.Watson.Assistant.v1.Model;

namespace IBM.Watson.Assistant.v1
{
    public partial interface IAssistantService
    {
        DetailedResponse<MessageResponse> Message(string workspaceId, MessageInput input = null, List<RuntimeIntent> intents = null, List<RuntimeEntity> entities = null, bool? alternateIntents = null, Context context = null, OutputData output = null, bool? nodesVisitedDetails = null);
        DetailedResponse<WorkspaceCollection> ListWorkspaces(long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Workspace> CreateWorkspace(string name = null, string description = null, string language = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, List<Webhook> webhooks = null);
        DetailedResponse<Workspace> GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null, string sort = null);
        DetailedResponse<Workspace> UpdateWorkspace(string workspaceId, string name = null, string description = null, string language = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, bool? append = null, List<Webhook> webhooks = null);
        DetailedResponse<object> DeleteWorkspace(string workspaceId);
        DetailedResponse<IntentCollection> ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Intent> CreateIntent(string workspaceId, string intent, string description = null, List<Example> examples = null);
        DetailedResponse<Intent> GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null);
        DetailedResponse<Intent> UpdateIntent(string workspaceId, string intent, string newIntent = null, string newDescription = null, List<Example> newExamples = null);
        DetailedResponse<object> DeleteIntent(string workspaceId, string intent);
        DetailedResponse<ExampleCollection> ListExamples(string workspaceId, string intent, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Example> CreateExample(string workspaceId, string intent, string text, List<Mention> mentions = null);
        DetailedResponse<Example> GetExample(string workspaceId, string intent, string text, bool? includeAudit = null);
        DetailedResponse<Example> UpdateExample(string workspaceId, string intent, string text, string newText = null, List<Mention> newMentions = null);
        DetailedResponse<object> DeleteExample(string workspaceId, string intent, string text);
        DetailedResponse<CounterexampleCollection> ListCounterexamples(string workspaceId, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Counterexample> CreateCounterexample(string workspaceId, string text);
        DetailedResponse<Counterexample> GetCounterexample(string workspaceId, string text, bool? includeAudit = null);
        DetailedResponse<Counterexample> UpdateCounterexample(string workspaceId, string text, string newText = null);
        DetailedResponse<object> DeleteCounterexample(string workspaceId, string text);
        DetailedResponse<EntityCollection> ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Entity> CreateEntity(string workspaceId, string entity, string description = null, Dictionary<string, object> metadata = null, bool? fuzzyMatch = null, List<CreateValue> values = null);
        DetailedResponse<Entity> GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null);
        DetailedResponse<Entity> UpdateEntity(string workspaceId, string entity, string newEntity = null, string newDescription = null, Dictionary<string, object> newMetadata = null, bool? newFuzzyMatch = null, List<CreateValue> newValues = null);
        DetailedResponse<object> DeleteEntity(string workspaceId, string entity);
        DetailedResponse<EntityMentionCollection> ListMentions(string workspaceId, string entity, bool? export = null, bool? includeAudit = null);
        DetailedResponse<ValueCollection> ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Value> CreateValue(string workspaceId, string entity, string value, Dictionary<string, object> metadata = null, string type = null, List<string> synonyms = null, List<string> patterns = null);
        DetailedResponse<Value> GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null);
        DetailedResponse<Value> UpdateValue(string workspaceId, string entity, string value, string newValue = null, Dictionary<string, object> newMetadata = null, string newType = null, List<string> newSynonyms = null, List<string> newPatterns = null);
        DetailedResponse<object> DeleteValue(string workspaceId, string entity, string value);
        DetailedResponse<SynonymCollection> ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Synonym> CreateSynonym(string workspaceId, string entity, string value, string synonym);
        DetailedResponse<Synonym> GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null);
        DetailedResponse<Synonym> UpdateSynonym(string workspaceId, string entity, string value, string synonym, string newSynonym = null);
        DetailedResponse<object> DeleteSynonym(string workspaceId, string entity, string value, string synonym);
        DetailedResponse<DialogNodeCollection> ListDialogNodes(string workspaceId, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<DialogNode> CreateDialogNode(string workspaceId, string dialogNode, string description = null, string conditions = null, string parent = null, string previousSibling = null, DialogNodeOutput output = null, Dictionary<string, object> context = null, Dictionary<string, object> metadata = null, DialogNodeNextStep nextStep = null, string title = null, string type = null, string eventName = null, string variable = null, List<DialogNodeAction> actions = null, string digressIn = null, string digressOut = null, string digressOutSlots = null, string userLabel = null, bool? disambiguationOptOut = null);
        DetailedResponse<DialogNode> GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null);
        DetailedResponse<DialogNode> UpdateDialogNode(string workspaceId, string dialogNode, string newDialogNode = null, string newDescription = null, string newConditions = null, string newParent = null, string newPreviousSibling = null, DialogNodeOutput newOutput = null, Dictionary<string, object> newContext = null, Dictionary<string, object> newMetadata = null, DialogNodeNextStep newNextStep = null, string newTitle = null, string newType = null, string newEventName = null, string newVariable = null, List<DialogNodeAction> newActions = null, string newDigressIn = null, string newDigressOut = null, string newDigressOutSlots = null, string newUserLabel = null, bool? newDisambiguationOptOut = null);
        DetailedResponse<object> DeleteDialogNode(string workspaceId, string dialogNode);
        DetailedResponse<LogCollection> ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null);
        DetailedResponse<LogCollection> ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null);
        DetailedResponse<object> DeleteUserData(string customerId);
    }
}
