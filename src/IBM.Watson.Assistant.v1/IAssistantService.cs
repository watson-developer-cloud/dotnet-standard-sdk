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
using System.Threading.Tasks;
using IBM.Watson.Assistant.v1.Model;

namespace IBM.Watson.Assistant.v1
{
    public partial interface IAssistantService
    {
        DetailedResponse<MessageResponse> Message(string workspaceId, MessageInput input = null, List<RuntimeIntent> intents = null, List<RuntimeEntity> entities = null, bool? alternateIntents = null, Context context = null, OutputData output = null, bool? nodesVisitedDetails = null);
        DetailedResponse<WorkspaceCollection> ListWorkspaces(long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Workspace> CreateWorkspace(string name = null, string description = null, string language = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, List<Webhook> webhooks = null, bool? includeAudit = null);
        DetailedResponse<Workspace> GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null, string sort = null);
        DetailedResponse<Workspace> UpdateWorkspace(string workspaceId, string name = null, string description = null, string language = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, bool? append = null, List<Webhook> webhooks = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteWorkspace(string workspaceId);
        DetailedResponse<IntentCollection> ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Intent> CreateIntent(string workspaceId, string intent, string description = null, List<Example> examples = null, bool? includeAudit = null);
        DetailedResponse<Intent> GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null);
        DetailedResponse<Intent> UpdateIntent(string workspaceId, string intent, string newIntent = null, string newDescription = null, List<Example> newExamples = null, bool? append = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteIntent(string workspaceId, string intent);
        DetailedResponse<ExampleCollection> ListExamples(string workspaceId, string intent, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Example> CreateExample(string workspaceId, string intent, string text, List<Mention> mentions = null, bool? includeAudit = null);
        DetailedResponse<Example> GetExample(string workspaceId, string intent, string text, bool? includeAudit = null);
        DetailedResponse<Example> UpdateExample(string workspaceId, string intent, string text, string newText = null, List<Mention> newMentions = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteExample(string workspaceId, string intent, string text);
        DetailedResponse<CounterexampleCollection> ListCounterexamples(string workspaceId, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Counterexample> CreateCounterexample(string workspaceId, string text, bool? includeAudit = null);
        DetailedResponse<Counterexample> GetCounterexample(string workspaceId, string text, bool? includeAudit = null);
        DetailedResponse<Counterexample> UpdateCounterexample(string workspaceId, string text, string newText = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteCounterexample(string workspaceId, string text);
        DetailedResponse<EntityCollection> ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Entity> CreateEntity(string workspaceId, string entity, string description = null, Dictionary<string, object> metadata = null, bool? fuzzyMatch = null, List<CreateValue> values = null, bool? includeAudit = null);
        DetailedResponse<Entity> GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null);
        DetailedResponse<Entity> UpdateEntity(string workspaceId, string entity, string newEntity = null, string newDescription = null, Dictionary<string, object> newMetadata = null, bool? newFuzzyMatch = null, List<CreateValue> newValues = null, bool? append = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteEntity(string workspaceId, string entity);
        DetailedResponse<EntityMentionCollection> ListMentions(string workspaceId, string entity, bool? export = null, bool? includeAudit = null);
        DetailedResponse<ValueCollection> ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Value> CreateValue(string workspaceId, string entity, string value, Dictionary<string, object> metadata = null, string type = null, List<string> synonyms = null, List<string> patterns = null, bool? includeAudit = null);
        DetailedResponse<Value> GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null);
        DetailedResponse<Value> UpdateValue(string workspaceId, string entity, string value, string newValue = null, Dictionary<string, object> newMetadata = null, string newType = null, List<string> newSynonyms = null, List<string> newPatterns = null, bool? append = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteValue(string workspaceId, string entity, string value);
        DetailedResponse<SynonymCollection> ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Synonym> CreateSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null);
        DetailedResponse<Synonym> GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null);
        DetailedResponse<Synonym> UpdateSynonym(string workspaceId, string entity, string value, string synonym, string newSynonym = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteSynonym(string workspaceId, string entity, string value, string synonym);
        DetailedResponse<DialogNodeCollection> ListDialogNodes(string workspaceId, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<DialogNode> CreateDialogNode(string workspaceId, string dialogNode, string description = null, string conditions = null, string parent = null, string previousSibling = null, DialogNodeOutput output = null, Dictionary<string, object> context = null, Dictionary<string, object> metadata = null, DialogNodeNextStep nextStep = null, string title = null, string type = null, string eventName = null, string variable = null, List<DialogNodeAction> actions = null, string digressIn = null, string digressOut = null, string digressOutSlots = null, string userLabel = null, bool? disambiguationOptOut = null, bool? includeAudit = null);
        DetailedResponse<DialogNode> GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null);
        DetailedResponse<DialogNode> UpdateDialogNode(string workspaceId, string dialogNode, string newDialogNode = null, string newDescription = null, string newConditions = null, string newParent = null, string newPreviousSibling = null, DialogNodeOutput newOutput = null, Dictionary<string, object> newContext = null, Dictionary<string, object> newMetadata = null, DialogNodeNextStep newNextStep = null, string newTitle = null, string newType = null, string newEventName = null, string newVariable = null, List<DialogNodeAction> newActions = null, string newDigressIn = null, string newDigressOut = null, string newDigressOutSlots = null, string newUserLabel = null, bool? newDisambiguationOptOut = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteDialogNode(string workspaceId, string dialogNode);
        DetailedResponse<LogCollection> ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null);
        DetailedResponse<LogCollection> ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null);
        DetailedResponse<object> DeleteUserData(string customerId);

        /// <summary>
        /// Get response to user input.
        ///
        /// Send user input to a workspace and receive a response.
        ///
        /// **Important:** This method has been superseded by the new v2 runtime API. The v2 API offers significant
        /// advantages, including ease of deployment, automatic state management, versioning, and search capabilities.
        /// For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-api-overview).
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The message to be sent. This includes the user's input, along with optional intents,
        /// entities, and context from the last response. (optional)</param>
        /// <param name="nodesVisitedDetails">Whether to include additional diagnostic information about the dialog
        /// nodes that were visited during processing of the message. (optional, default to false)</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        Task<DetailedResponse<MessageResponse>> MessageAsync(string workspaceId, MessageInput input = null, List<RuntimeIntent> intents = null, List<RuntimeEntity> entities = null, bool? alternateIntents = null, Context context = null, OutputData output = null, bool? nodesVisitedDetails = null);

        /// <summary>
        /// List workspaces.
        ///
        /// List the workspaces associated with a Watson Assistant service instance.
        /// </summary>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="sort">The attribute by which returned workspaces will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="WorkspaceCollection" />WorkspaceCollection</returns>
        Task<DetailedResponse<WorkspaceCollection>> ListWorkspacesAsync(long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);

        /// <summary>
        /// Create workspace.
        ///
        /// Create a workspace based on component objects. You must provide workspace components defining the content of
        /// the new workspace.
        /// </summary>
        /// <param name="body"> (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        Task<DetailedResponse<Workspace>> CreateWorkspaceAsync(string name = null, string description = null, string language = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, List<Webhook> webhooks = null, bool? includeAudit = null);

        /// Get information about a workspace.
        ///
        /// Get information about a workspace, optionally including all workspace content.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <param name="sort">Indicates how the returned workspace data will be sorted. This parameter is valid only if
        /// **export**=`true`. Specify `sort=stable` to sort all workspace objects by unique identifier, in ascending
        /// alphabetical order. (optional)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        Task<DetailedResponse<Workspace>> GetWorkspaceAsync(string workspaceId, bool? export = null, bool? includeAudit = null, string sort = null);

        /// <summary>
        /// Update workspace.
        ///
        /// Update an existing workspace with new or modified data. You must provide component objects defining the
        /// content of the updated workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">Valid data defining the new and updated workspace content.
        ///
        /// The maximum size for this data is 50MB. If you need to import a larger amount of workspace data, consider
        /// importing components such as intents and entities using separate operations. (optional)</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the object. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data for a workspace includes **entities** and
        /// **append**=`false`, all existing entities in the workspace are discarded and replaced with the new entities.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        Task<DetailedResponse<Workspace>> UpdateWorkspaceAsync(string workspaceId, string name = null, string description = null, string language = null, Dictionary<string, object> metadata = null, bool? learningOptOut = null, WorkspaceSystemSettings systemSettings = null, List<CreateIntent> intents = null, List<CreateEntity> entities = null, List<DialogNode> dialogNodes = null, List<Counterexample> counterexamples = null, bool? append = null, List<Webhook> webhooks = null, bool? includeAudit = null);

        /// <summary>
        /// Delete workspace.
        ///
        /// Delete a workspace from the service instance.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteWorkspaceAsync(string workspaceId);

        /// <summary>
        /// List intents.
        ///
        /// List the intents for a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="sort">The attribute by which returned intents will be sorted. To reverse the sort order, prefix
        /// the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="IntentCollection" />IntentCollection</returns>
        Task<DetailedResponse<IntentCollection>> ListIntentsAsync(string workspaceId, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);

        /// <summary>
        /// Create intent.
        ///
        /// Create a new intent.
        ///
        /// If you want to create multiple intents with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The content of the new intent.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Intent" />Intent</returns>
        Task<DetailedResponse<Intent>> CreateIntentAsync(string workspaceId, string intent, string description = null, List<Example> examples = null, bool? includeAudit = null);

        /// <summary>
        /// Get intent.
        ///
        /// Get information about an intent, optionally including all intent content.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Intent" />Intent</returns>
        Task<DetailedResponse<Intent>> GetIntentAsync(string workspaceId, string intent, bool? export = null, bool? includeAudit = null);

        /// <summary>
        /// Update intent.
        ///
        /// Update an existing intent with new or modified data. You must provide component objects defining the content
        /// of the updated intent.
        ///
        /// If you want to update multiple intents with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="body">The updated content of the intent.
        ///
        /// Any elements included in the new data will completely replace the equivalent existing elements, including
        /// all subelements. (Previously existing subelements are not retained unless they are also included in the new
        /// data.) For example, if you update the user input examples for an intent, the previously existing examples
        /// are discarded and replaced with the new examples specified in the update.</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the object. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data for the intent includes **examples** and
        /// **append**=`false`, all existing examples for the intent are discarded and replaced with the new examples.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Intent" />Intent</returns>
        Task<DetailedResponse<Intent>> UpdateIntentAsync(string workspaceId, string intent, string newIntent = null, string newDescription = null, List<Example> newExamples = null, bool? append = null, bool? includeAudit = null);

        /// <summary>
        /// Delete intent.
        ///
        /// Delete an intent from a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteIntentAsync(string workspaceId, string intent);

        /// <summary>
        /// List user input examples.
        ///
        /// List the user input examples for an intent, optionally including contextual entity mentions.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="sort">The attribute by which returned examples will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="ExampleCollection" />ExampleCollection</returns>
        Task<DetailedResponse<ExampleCollection>> ListExamplesAsync(string workspaceId, string intent, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);

        /// <summary>
        /// Create user input example.
        ///
        /// Add a new user input example to an intent.
        ///
        /// If you want to add multiple examples with a single API call, consider using the **[Update
        /// intent](#update-intent)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="body">The content of the new user input example.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Example" />Example</returns>
        Task<DetailedResponse<Example>> CreateExampleAsync(string workspaceId, string intent, string text, List<Mention> mentions = null, bool? includeAudit = null);

        /// <summary>
        /// Get user input example.
        ///
        /// Get information about a user input example.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Example" />Example</returns>
        Task<DetailedResponse<Example>> GetExampleAsync(string workspaceId, string intent, string text, bool? includeAudit = null);

        /// <summary>
        /// Update user input example.
        ///
        /// Update the text of a user input example.
        ///
        /// If you want to update multiple examples with a single API call, consider using the **[Update
        /// intent](#update-intent)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="body">The new text of the user input example.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Example" />Example</returns>
        Task<DetailedResponse<Example>> UpdateExampleAsync(string workspaceId, string intent, string text, string newText = null, List<Mention> newMentions = null, bool? includeAudit = null);

        /// <summary>
        /// Delete user input example.
        ///
        /// Delete a user input example from an intent.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteExampleAsync(string workspaceId, string intent, string text);

        /// <summary>
        /// List counterexamples.
        ///
        /// List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant
        /// input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="sort">The attribute by which returned counterexamples will be sorted. To reverse the sort
        /// order, prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="CounterexampleCollection" />CounterexampleCollection</returns>
        Task<DetailedResponse<CounterexampleCollection>> ListCounterexamplesAsync(string workspaceId, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);

        /// <summary>
        /// Create counterexample.
        ///
        /// Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant
        /// input.
        ///
        /// If you want to add multiple counterexamples with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The content of the new counterexample.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        Task<DetailedResponse<Counterexample>> CreateCounterexampleAsync(string workspaceId, string text, bool? includeAudit = null);

        /// <summary>
        /// Get counterexample.
        ///
        /// Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant
        /// input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        Task<DetailedResponse<Counterexample>> GetCounterexampleAsync(string workspaceId, string text, bool? includeAudit = null);

        /// <summary>
        /// Update counterexample.
        ///
        /// Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="body">The text of the counterexample.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        Task<DetailedResponse<Counterexample>> UpdateCounterexampleAsync(string workspaceId, string text, string newText = null, bool? includeAudit = null);

        /// <summary>
        /// Delete counterexample.
        ///
        /// Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant
        /// input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteCounterexampleAsync(string workspaceId, string text);

        /// <summary>
        /// List entities.
        ///
        /// List the entities for a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="sort">The attribute by which returned entities will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="EntityCollection" />EntityCollection</returns>
        Task<DetailedResponse<EntityCollection>> ListEntitiesAsync(string workspaceId, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);

        /// <summary>
        /// Create entity.
        ///
        /// Create a new entity, or enable a system entity.
        ///
        /// If you want to create multiple entities with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The content of the new entity.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Entity" />Entity</returns>
        Task<DetailedResponse<Entity>> CreateEntityAsync(string workspaceId, string entity, string description = null, Dictionary<string, object> metadata = null, bool? fuzzyMatch = null, List<CreateValue> values = null, bool? includeAudit = null);

        /// <summary>
        /// Get entity.
        ///
        /// Get information about an entity, optionally including all entity content.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Entity" />Entity</returns>
        Task<DetailedResponse<Entity>> GetEntityAsync(string workspaceId, string entity, bool? export = null, bool? includeAudit = null);

        /// <summary>
        /// Update entity.
        ///
        /// Update an existing entity with new or modified data. You must provide component objects defining the content
        /// of the updated entity.
        ///
        /// If you want to update multiple entities with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="body">The updated content of the entity. Any elements included in the new data will completely
        /// replace the equivalent existing elements, including all subelements. (Previously existing subelements are
        /// not retained unless they are also included in the new data.) For example, if you update the values for an
        /// entity, the previously existing values are discarded and replaced with the new values specified in the
        /// update.</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the entity. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data for the entity includes **values** and
        /// **append**=`false`, all existing values for the entity are discarded and replaced with the new values.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Entity" />Entity</returns>
        Task<DetailedResponse<Entity>> UpdateEntityAsync(string workspaceId, string entity, string newEntity = null, string newDescription = null, Dictionary<string, object> newMetadata = null, bool? newFuzzyMatch = null, List<CreateValue> newValues = null, bool? append = null, bool? includeAudit = null);

        /// <summary>
        /// Delete entity.
        ///
        /// Delete an entity from a workspace, or disable a system entity.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteEntityAsync(string workspaceId, string entity);

        /// <summary>
        /// List entity mentions.
        ///
        /// List mentions for a contextual entity. An entity mention is an occurrence of a contextual entity in the
        /// context of an intent user input example.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="EntityMentionCollection" />EntityMentionCollection</returns>
        Task<DetailedResponse<EntityMentionCollection>> ListMentionsAsync(string workspaceId, string entity, bool? export = null, bool? includeAudit = null);

        /// <summary>
        /// List entity values.
        ///
        /// List the values for an entity.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="sort">The attribute by which returned entity values will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="ValueCollection" />ValueCollection</returns>
        Task<DetailedResponse<ValueCollection>> ListValuesAsync(string workspaceId, string entity, bool? export = null, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);

        /// <summary>
        /// Create entity value.
        ///
        /// Create a new value for an entity.
        ///
        /// If you want to create multiple entity values with a single API call, consider using the **[Update
        /// entity](#update-entity)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="body">The new entity value.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Value" />Value</returns>
        Task<DetailedResponse<Value>> CreateValueAsync(string workspaceId, string entity, string value, Dictionary<string, object> metadata = null, string type = null, List<string> synonyms = null, List<string> patterns = null, bool? includeAudit = null);

        /// <summary>
        /// Get entity value.
        ///
        /// Get information about an entity value.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the
        /// returned data includes only information about the element itself. If **export**=`true`, all content,
        /// including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Value" />Value</returns>
        Task<DetailedResponse<Value>> GetValueAsync(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null);

        /// <summary>
        /// Update entity value.
        ///
        /// Update an existing entity value with new or modified data. You must provide component objects defining the
        /// content of the updated entity value.
        ///
        /// If you want to update multiple entity values with a single API call, consider using the **[Update
        /// entity](#update-entity)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">The updated content of the entity value.
        ///
        /// Any elements included in the new data will completely replace the equivalent existing elements, including
        /// all subelements. (Previously existing subelements are not retained unless they are also included in the new
        /// data.) For example, if you update the synonyms for an entity value, the previously existing synonyms are
        /// discarded and replaced with the new synonyms specified in the update.</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the entity value. If
        /// **append**=`false`, elements included in the new data completely replace the corresponding existing
        /// elements, including all subelements. For example, if the new data for the entity value includes **synonyms**
        /// and **append**=`false`, all existing synonyms for the entity value are discarded and replaced with the new
        /// synonyms.
        ///
        /// If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in
        /// the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Value" />Value</returns>
        Task<DetailedResponse<Value>> UpdateValueAsync(string workspaceId, string entity, string value,
            string newValue = null, Dictionary<string, object> newMetadata = null, string newType = null,
            List<string> newSynonyms = null, List<string> newPatterns = null, bool? append = null,
            bool? includeAudit = null);

        /// <summary>
        /// Delete entity value.
        ///
        /// Delete a value from an entity.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteValueAsync(string workspaceId, string entity, string value);

        /// <summary>
        /// List entity value synonyms.
        ///
        /// List the synonyms for an entity value.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="sort">The attribute by which returned entity value synonyms will be sorted. To reverse the sort
        /// order, prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="SynonymCollection" />SynonymCollection</returns>
        Task<DetailedResponse<SynonymCollection>> ListSynonymsAsync(string workspaceId, string entity, string value,
            long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);

        /// <summary>
        /// Create entity value synonym.
        ///
        /// Add a new synonym to an entity value.
        ///
        /// If you want to create multiple synonyms with a single API call, consider using the **[Update
        /// entity](#update-entity)** or **[Update entity value](#update-entity-value)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">The new synonym.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        Task<DetailedResponse<Synonym>> CreateSynonymAsync(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null);

        /// <summary>
        /// Get entity value synonym.
        ///
        /// Get information about a synonym of an entity value.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        Task<DetailedResponse<Synonym>> GetSynonymAsync(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null);

        /// <summary>
        /// Update entity value synonym.
        ///
        /// Update an existing entity value synonym with new text.
        ///
        /// If you want to update multiple synonyms with a single API call, consider using the **[Update
        /// entity](#update-entity)** or **[Update entity value](#update-entity-value)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="body">The updated entity value synonym.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        Task<DetailedResponse<Synonym>> UpdateSynonymAsync(string workspaceId, string entity, string value, string synonym, string newSynonym = null, bool? includeAudit = null);

        /// <summary>
        /// Delete entity value synonym.
        ///
        /// Delete a synonym from an entity value.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteSynonymAsync(string workspaceId, string entity, string value, string synonym);

        /// <summary>
        /// List dialog nodes.
        ///
        /// List the dialog nodes for a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="sort">The attribute by which returned dialog nodes will be sorted. To reverse the sort order,
        /// prefix the value with a minus sign (`-`). (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNodeCollection" />DialogNodeCollection</returns>
        Task<DetailedResponse<DialogNodeCollection>> ListDialogNodesAsync(string workspaceId, long? pageLimit = null, string sort = null, string cursor = null, bool? includeAudit = null);

        /// <summary>
        /// Create dialog node.
        ///
        /// Create a new dialog node.
        ///
        /// If you want to create multiple dialog nodes with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">A CreateDialogNode object defining the content of the new dialog node.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        Task<DetailedResponse<DialogNode>> CreateDialogNodeAsync(string workspaceId, string dialogNode,
            string description = null, string conditions = null, string parent = null, string previousSibling = null,
            DialogNodeOutput output = null, Dictionary<string, object> context = null,
            Dictionary<string, object> metadata = null, DialogNodeNextStep nextStep = null, string title = null,
            string type = null, string eventName = null, string variable = null, List<DialogNodeAction> actions = null,
            string digressIn = null, string digressOut = null, string digressOutSlots = null, string userLabel = null,
            bool? disambiguationOptOut = null, bool? includeAudit = null);

        /// <summary>
        /// Get dialog node.
        ///
        /// Get information about a dialog node.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        Task<DetailedResponse<DialogNode>> GetDialogNodeAsync(string workspaceId, string dialogNode, bool? includeAudit = null);

        /// <summary>
        /// Update dialog node.
        ///
        /// Update an existing dialog node with new or modified data.
        ///
        /// If you want to update multiple dialog nodes with a single API call, consider using the **[Update
        /// workspace](#update-workspace)** method instead.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="body">The updated content of the dialog node.
        ///
        /// Any elements included in the new data will completely replace the equivalent existing elements, including
        /// all subelements. (Previously existing subelements are not retained unless they are also included in the new
        /// data.) For example, if you update the actions for a dialog node, the previously existing actions are
        /// discarded and replaced with the new actions specified in the update.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in
        /// the response. (optional, default to false)</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        Task<DetailedResponse<DialogNode>> UpdateDialogNodeAsync(string workspaceId, string dialogNode,
            string newDialogNode = null, string newDescription = null, string newConditions = null,
            string newParent = null, string newPreviousSibling = null, DialogNodeOutput newOutput = null,
            Dictionary<string, object> newContext = null, Dictionary<string, object> newMetadata = null,
            DialogNodeNextStep newNextStep = null, string newTitle = null, string newType = null,
            string newEventName = null, string newVariable = null, List<DialogNodeAction> newActions = null,
            string newDigressIn = null, string newDigressOut = null, string newDigressOutSlots = null,
            string newUserLabel = null, bool? newDisambiguationOptOut = null, bool? includeAudit = null);

        /// <summary>
        /// Delete dialog node.
        ///
        /// Delete a dialog node from a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteDialogNodeAsync(string workspaceId, string dialogNode);

        /// <summary>
        /// List log events in a workspace.
        ///
        /// List the events from the log of a specific workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="sort">How to sort the returned log events. You can sort by **request_timestamp**. To reverse
        /// the sort order, prefix the parameter value with a minus sign (`-`). (optional)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter.
        /// For more information, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-filter-reference#filter-reference).
        /// (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        Task<DetailedResponse<LogCollection>> ListLogsAsync(string workspaceId, string sort = null, string filter = null,
            long? pageLimit = null, string cursor = null);

        /// <summary>
        /// List log events in all workspaces.
        ///
        /// List the events from the logs of all workspaces in the service instance.
        /// </summary>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter.
        /// You must specify a filter query that includes a value for `language`, as well as a value for
        /// `request.context.system.assistant_id`, `workspace_id`, or `request.context.metadata.deployment`. For more
        /// information, see the
        /// [documentation](https://cloud.ibm.com/docs/assistant?topic=assistant-filter-reference#filter-reference).</param>
        /// <param name="sort">How to sort the returned log events. You can sort by **request_timestamp**. To reverse
        /// the sort order, prefix the parameter value with a minus sign (`-`). (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        Task<DetailedResponse<LogCollection>> ListAllLogsAsync(string filter, string sort = null, long? pageLimit = null, string cursor = null);

        /// <summary>
        /// Delete labeled data.
        ///
        /// Deletes all data associated with a specified customer ID. The method has no effect if no data is associated
        /// with the customer ID.
        ///
        /// You associate a customer ID with data by passing the `X-Watson-Metadata` header with a request that passes
        /// data. For more information about personal data and customer IDs, see [Information
        /// security](https://cloud.ibm.com/docs/assistant?topic=assistant-information-security#information-security).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <returns><see cref="object" />object</returns>
        Task<DetailedResponse<object>> DeleteUserDataAsync(string customerId);
    }
}
