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
using IBM.WatsonDeveloperCloud.Assistant.v1.Model;

namespace IBM.WatsonDeveloperCloud.Assistant.v1
{
    public partial interface IAssistantService
    {
        /// <summary>
        /// Get response to user input. Get a response to a user's input.    There is no rate limit for this operation.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="request">The message to be sent. This includes the user's input, along with optional intents, entities, and context from the last response. (optional)</param>
        /// <param name="nodesVisitedDetails">Whether to include additional diagnostic information about the dialog nodes that were visited during processing of the message. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        MessageResponse Message(string workspaceId, MessageRequest request = null, bool? nodesVisitedDetails = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Create workspace. Create a workspace based on component objects. You must provide workspace components defining the content of the new workspace.    This operation is limited to 30 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="properties">The content of the new workspace.    The maximum size for this data is 50MB. If you need to import a larger workspace, consider importing the workspace without intents and entities and then adding them separately. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        Workspace CreateWorkspace(CreateWorkspace properties = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete workspace. Delete a workspace from the service instance.    This operation is limited to 30 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteWorkspace(string workspaceId, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get information about a workspace. Get information about a workspace, optionally including all workspace content.    With **export**=`false`, this operation is limited to 6000 requests per 5 minutes. With **export**=`true`, the limit is 20 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the returned data includes only information about the element itself. If **export**=`true`, all content, including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="WorkspaceExport" />WorkspaceExport</returns>
        WorkspaceExport GetWorkspace(string workspaceId, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List workspaces. List the workspaces associated with a Watson Assistant service instance.    This operation is limited to 500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="WorkspaceCollection" />WorkspaceCollection</returns>
        WorkspaceCollection ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update workspace. Update an existing workspace with new or modified data. You must provide component objects defining the content of the updated workspace.    This operation is limited to 30 request per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="properties">Valid data defining the new and updated workspace content.    The maximum size for this data is 50MB. If you need to import a larger amount of workspace data, consider importing components such as intents and entities using separate operations. (optional)</param>
        /// <param name="append">Whether the new data is to be appended to the existing data in the workspace. If **append**=`false`, elements included in the new data completely replace the corresponding existing elements, including all subelements. For example, if the new data includes **entities** and **append**=`false`, all existing entities in the workspace are discarded and replaced with the new entities.    If **append**=`true`, existing elements are preserved, and the new elements are added. If any elements in the new data collide with existing elements, the update request fails. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Workspace" />Workspace</returns>
        Workspace UpdateWorkspace(string workspaceId, UpdateWorkspace properties = null, bool? append = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Create intent. Create a new intent.    This operation is limited to 2000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The content of the new intent.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Intent" />Intent</returns>
        Intent CreateIntent(string workspaceId, CreateIntent body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete intent. Delete an intent from a workspace.    This operation is limited to 2000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteIntent(string workspaceId, string intent, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get intent. Get information about an intent, optionally including all intent content.    With **export**=`false`, this operation is limited to 6000 requests per 5 minutes. With **export**=`true`, the limit is 400 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the returned data includes only information about the element itself. If **export**=`true`, all content, including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="IntentExport" />IntentExport</returns>
        IntentExport GetIntent(string workspaceId, string intent, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List intents. List the intents for a workspace.    With **export**=`false`, this operation is limited to 2000 requests per 30 minutes. With **export**=`true`, the limit is 400 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the returned data includes only information about the element itself. If **export**=`true`, all content, including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="IntentCollection" />IntentCollection</returns>
        IntentCollection ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update intent. Update an existing intent with new or modified data. You must provide component objects defining the content of the updated intent.    This operation is limited to 2000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="body">The updated content of the intent.    Any elements included in the new data will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are also included in the new data.) For example, if you update the user input examples for an intent, the previously existing examples are discarded and replaced with the new examples specified in the update.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Intent" />Intent</returns>
        Intent UpdateIntent(string workspaceId, string intent, UpdateIntent body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Create user input example. Add a new user input example to an intent.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="body">The content of the new user input example.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Example" />Example</returns>
        Example CreateExample(string workspaceId, string intent, CreateExample body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete user input example. Delete a user input example from an intent.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteExample(string workspaceId, string intent, string text, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get user input example. Get information about a user input example.    This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Example" />Example</returns>
        Example GetExample(string workspaceId, string intent, string text, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List user input examples. List the user input examples for an intent.    This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ExampleCollection" />ExampleCollection</returns>
        ExampleCollection ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update user input example. Update the text of a user input example.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="intent">The intent name.</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="body">The new text of the user input example.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Example" />Example</returns>
        Example UpdateExample(string workspaceId, string intent, string text, UpdateExample body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Create counterexample. Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant input.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The content of the new counterexample.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        Counterexample CreateCounterexample(string workspaceId, CreateCounterexample body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete counterexample. Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant input.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteCounterexample(string workspaceId, string text, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get counterexample. Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant input.    This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        Counterexample GetCounterexample(string workspaceId, string text, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List counterexamples. List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant input.    This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="CounterexampleCollection" />CounterexampleCollection</returns>
        CounterexampleCollection ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update counterexample. Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="body">The text of the counterexample.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Counterexample" />Counterexample</returns>
        Counterexample UpdateCounterexample(string workspaceId, string text, UpdateCounterexample body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Create entity. Create a new entity.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="properties">The content of the new entity.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Entity" />Entity</returns>
        Entity CreateEntity(string workspaceId, CreateEntity properties, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete entity. Delete an entity from a workspace.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteEntity(string workspaceId, string entity, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get entity. Get information about an entity, optionally including all entity content.    With **export**=`false`, this operation is limited to 6000 requests per 5 minutes. With **export**=`true`, the limit is 200 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the returned data includes only information about the element itself. If **export**=`true`, all content, including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="EntityExport" />EntityExport</returns>
        EntityExport GetEntity(string workspaceId, string entity, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List entities. List the entities for a workspace.    With **export**=`false`, this operation is limited to 1000 requests per 30 minutes. With **export**=`true`, the limit is 200 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the returned data includes only information about the element itself. If **export**=`true`, all content, including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="EntityCollection" />EntityCollection</returns>
        EntityCollection ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update entity. Update an existing entity with new or modified data. You must provide component objects defining the content of the updated entity.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="properties">The updated content of the entity. Any elements included in the new data will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are also included in the new data.) For example, if you update the values for an entity, the previously existing values are discarded and replaced with the new values specified in the update.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Entity" />Entity</returns>
        Entity UpdateEntity(string workspaceId, string entity, UpdateEntity properties, Dictionary<string, object> customData = null);
        /// <summary>
        /// Add entity value. Create a new value for an entity.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="properties">The new entity value.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Value" />Value</returns>
        Value CreateValue(string workspaceId, string entity, CreateValue properties, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete entity value. Delete a value from an entity.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteValue(string workspaceId, string entity, string value, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get entity value. Get information about an entity value.    This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the returned data includes only information about the element itself. If **export**=`true`, all content, including subelements, is included. (optional, default to false)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ValueExport" />ValueExport</returns>
        ValueExport GetValue(string workspaceId, string entity, string value, bool? export = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List entity values. List the values for an entity.    This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If **export**=`false`, the returned data includes only information about the element itself. If **export**=`true`, all content, including subelements, is included. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ValueCollection" />ValueCollection</returns>
        ValueCollection ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update entity value. Update an existing entity value with new or modified data. You must provide component objects defining the content of the updated entity value.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="properties">The updated content of the entity value.    Any elements included in the new data will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are also included in the new data.) For example, if you update the synonyms for an entity value, the previously existing synonyms are discarded and replaced with the new synonyms specified in the update.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Value" />Value</returns>
        Value UpdateValue(string workspaceId, string entity, string value, UpdateValue properties, Dictionary<string, object> customData = null);
        /// <summary>
        /// Add entity value synonym. Add a new synonym to an entity value.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">The new synonym.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        Synonym CreateSynonym(string workspaceId, string entity, string value, CreateSynonym body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete entity value synonym. Delete a synonym from an entity value.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteSynonym(string workspaceId, string entity, string value, string synonym, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get entity value synonym. Get information about a synonym of an entity value.    This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        Synonym GetSynonym(string workspaceId, string entity, string value, string synonym, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List entity value synonyms. List the synonyms for an entity value.    This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="SynonymCollection" />SynonymCollection</returns>
        SynonymCollection ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update entity value synonym. Update an existing entity value synonym with new text.    This operation is limited to 1000 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="body">The updated entity value synonym.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Synonym" />Synonym</returns>
        Synonym UpdateSynonym(string workspaceId, string entity, string value, string synonym, UpdateSynonym body, Dictionary<string, object> customData = null);
        /// <summary>
        /// Create dialog node. Create a new dialog node.    This operation is limited to 500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="properties">A CreateDialogNode object defining the content of the new dialog node.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        DialogNode CreateDialogNode(string workspaceId, CreateDialogNode properties, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete dialog node. Delete a dialog node from a workspace.    This operation is limited to 500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteDialogNode(string workspaceId, string dialogNode, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get dialog node. Get information about a dialog node.    This operation is limited to 6000 requests per 5 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        DialogNode GetDialogNode(string workspaceId, string dialogNode, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List dialog nodes. List the dialog nodes for a workspace.    This operation is limited to 2500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="includeAudit">Whether to include the audit properties (`created` and `updated` timestamps) in the response. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DialogNodeCollection" />DialogNodeCollection</returns>
        DialogNodeCollection ListDialogNodes(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update dialog node. Update an existing dialog node with new or modified data.    This operation is limited to 500 requests per 30 minutes. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="dialogNode">The dialog node ID (for example, `get_order`).</param>
        /// <param name="properties">The updated content of the dialog node.    Any elements included in the new data will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are also included in the new data.) For example, if you update the actions for a dialog node, the previously existing actions are discarded and replaced with the new actions specified in the update.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DialogNode" />DialogNode</returns>
        DialogNode UpdateDialogNode(string workspaceId, string dialogNode, UpdateDialogNode properties, Dictionary<string, object> customData = null);
        /// <summary>
        /// List log events in all workspaces. List the events from the logs of all workspaces in the service instance.    If **cursor** is not specified, this operation is limited to 40 requests per 30 minutes. If **cursor** is specified, the limit is 120 requests per minute. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter. You must specify a filter query that includes a value for `language`, as well as a value for `workspace_id` or `request.context.metadata.deployment`. For more information, see the [documentation](https://console.bluemix.net/docs/services/conversation/filter-reference.html#filter-query-syntax).</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        LogCollection ListAllLogs(string filter, string sort = null, long? pageLimit = null, string cursor = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List log events in a workspace. List the events from the log of a specific workspace.    If **cursor** is not specified, this operation is limited to 40 requests per 30 minutes. If **cursor** is specified, the limit is 120 requests per minute. For more information, see **Rate limiting**.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="sort">The attribute by which returned results will be sorted. To reverse the sort order, prefix the value with a minus sign (`-`). Supported values are `name`, `updated`, and `workspace_id`. (optional)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter. For more information, see the [documentation](https://console.bluemix.net/docs/services/conversation/filter-reference.html#filter-query-syntax). (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. (optional, default to 100)</param>
        /// <param name="cursor">A token identifying the page of results to retrieve. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="LogCollection" />LogCollection</returns>
        LogCollection ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete labeled data. Deletes all data associated with a specified customer ID. The method has no effect if no data is associated with the customer ID.   You associate a customer ID with data by passing the `X-Watson-Metadata` header with a request that passes data. For more information about personal data and customer IDs, see [Information security](https://console.bluemix.net/docs/services/conversation/information-security.html).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null);
    }
}
