/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

using IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace IBM.WatsonDeveloperCloud.Conversation.v1
{
    public interface IConversationService
    {
        /// <summary>
        /// Create counterexample. Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateExample object defining the content of the new user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        ExampleResponse CreateCounterexample(string workspaceId, CreateExample body);

        /// <summary>
        /// Delete counterexample. Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteCounterexample(string workspaceId, string text);

        /// <summary>
        /// Get counterexample. Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        ExampleResponse GetCounterexample(string workspaceId, string text);

        /// <summary>
        /// List counterexamples. List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="CounterexampleCollectionResponse" />CounterexampleCollectionResponse</returns>
        CounterexampleCollectionResponse ListCounterexamples(string workspaceId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null);

        /// <summary>
        /// Update counterexample. Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="text">The text of a user input counterexample (for example, `What are you wearing?`).</param>
        /// <param name="body">An UpdateExample object defining the new text for the counterexample.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        ExampleResponse UpdateCounterexample(string workspaceId, string text, UpdateExample body);
        /// <summary>
        /// Create entity. Create a new entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateEntity object defining the content of the new entity.</param>
        /// <returns><see cref="EntityResponse" />EntityResponse</returns>
        EntityResponse CreateEntity(string workspaceId, CreateEntity body);

        /// <summary>
        /// Delete entity. Delete an entity from a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteEntity(string workspaceId, string entity);

        /// <summary>
        /// Get entity. Get information about an entity, optionally including all entity content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="EntityExportResponse" />EntityExportResponse</returns>
        EntityExportResponse GetEntity(string workspaceId, string entity, bool? export = null);

        /// <summary>
        /// List entities. List the entities for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="EntityCollectionResponse" />EntityCollectionResponse</returns>
        EntityCollectionResponse ListEntities(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null);

        /// <summary>
        /// Update entity. Update an existing entity with new or modified data.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="body">An UpdateEntity object defining the updated content of the entity.</param>
        /// <returns><see cref="EntityResponse" />EntityResponse</returns>
        EntityResponse UpdateEntity(string workspaceId, string entity, UpdateEntity body);
        /// <summary>
        /// Create user input example. Add a new user input example to an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="body">A CreateExample object defining the content of the new user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        ExampleResponse CreateExample(string workspaceId, string intent, CreateExample body);

        /// <summary>
        /// Delete user input example. Delete a user input example from an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteExample(string workspaceId, string intent, string text);

        /// <summary>
        /// Get user input example. Get information about a user input example.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        ExampleResponse GetExample(string workspaceId, string intent, string text);

        /// <summary>
        /// List user input examples. List the user input examples for an intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="ExampleCollectionResponse" />ExampleCollectionResponse</returns>
        ExampleCollectionResponse ListExamples(string workspaceId, string intent, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null);

        /// <summary>
        /// Update user input example. Update the text of a user input example.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="text">The text of the user input example.</param>
        /// <param name="body">An UpdateExample object defining the new text for the user input example.</param>
        /// <returns><see cref="ExampleResponse" />ExampleResponse</returns>
        ExampleResponse UpdateExample(string workspaceId, string intent, string text, UpdateExample body);
        /// <summary>
        /// Create intent. Create a new intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">A CreateIntent object defining the content of the new intent.</param>
        /// <returns><see cref="IntentResponse" />IntentResponse</returns>
        IntentResponse CreateIntent(string workspaceId, CreateIntent body);

        /// <summary>
        /// Delete intent. Delete an intent from a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteIntent(string workspaceId, string intent);

        /// <summary>
        /// Get intent. Get information about an intent, optionally including all intent content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="IntentExportResponse" />IntentExportResponse</returns>
        IntentExportResponse GetIntent(string workspaceId, string intent, bool? export = null);

        /// <summary>
        /// List intents. List the intents for a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="IntentCollectionResponse" />IntentCollectionResponse</returns>
        IntentCollectionResponse ListIntents(string workspaceId, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null);

        /// <summary>
        /// Update intent. Update an existing intent with new or modified data. You must provide data defining the content of the updated intent.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="intent">The intent name (for example, `pizza_order`).</param>
        /// <param name="body">An UpdateIntent object defining the updated content of the intent.</param>
        /// <returns><see cref="IntentResponse" />IntentResponse</returns>
        IntentResponse UpdateIntent(string workspaceId, string intent, UpdateIntent body);
        /// <summary>
        /// List log events. 
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="filter">A cacheable parameter that limits the results to those matching the specified filter. (optional)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="LogCollectionResponse" />LogCollectionResponse</returns>
        LogCollectionResponse ListLogs(string workspaceId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null);
        /// <summary>
        /// Get a response to a user's input. 
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace.</param>
        /// <param name="body">The user's input, with optional intents, entities, and other properties from the response. (optional)</param>
        /// <returns><see cref="MessageResponse" />MessageResponse</returns>
        MessageResponse Message(string workspaceId, MessageRequest body = null);
        /// <summary>
        /// Add entity value synonym. Add a new synonym to an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">A CreateSynonym object defining the new synonym for the entity value.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        SynonymResponse CreateSynonym(string workspaceId, string entity, string value, CreateSynonym body);

        /// <summary>
        /// Delete entity value synonym. Delete a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteSynonym(string workspaceId, string entity, string value, string synonym);

        /// <summary>
        /// Get entity value synonym. Get information about a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        SynonymResponse GetSynonym(string workspaceId, string entity, string value, string synonym);

        /// <summary>
        /// List entity value synonyms. List the synonyms for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="SynonymCollectionResponse" />SynonymCollectionResponse</returns>
        SynonymCollectionResponse ListSynonyms(string workspaceId, string entity, string value, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null);

        /// <summary>
        /// Update entity value synonym. Update the information about a synonym for an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="synonym">The text of the synonym.</param>
        /// <param name="body">An UpdateSynonym object defining the new information for an entity value synonym.</param>
        /// <returns><see cref="SynonymResponse" />SynonymResponse</returns>
        SynonymResponse UpdateSynonym(string workspaceId, string entity, string value, string synonym, UpdateSynonym body);
        /// <summary>
        /// Add entity value. Create a new value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="body">A CreateValue object defining the content of the new value for the entity.</param>
        /// <returns><see cref="ValueResponse" />ValueResponse</returns>
        ValueResponse CreateValue(string workspaceId, string entity, CreateValue body);

        /// <summary>
        /// Delete entity value. Delete a value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteValue(string workspaceId, string entity, string value);

        /// <summary>
        /// Get entity value. Get information about an entity value.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="ValueExportResponse" />ValueExportResponse</returns>
        ValueExportResponse GetValue(string workspaceId, string entity, string value, bool? export = null);

        /// <summary>
        /// List entity values. List the values for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="ValueCollectionResponse" />ValueCollectionResponse</returns>
        ValueCollectionResponse ListValues(string workspaceId, string entity, bool? export = null, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null);

        /// <summary>
        /// Update entity value. Update the content of a value for an entity.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="value">The text of the entity value.</param>
        /// <param name="body">An UpdateValue object defining the new content for value for the entity.</param>
        /// <returns><see cref="ValueResponse" />ValueResponse</returns>
        ValueResponse UpdateValue(string workspaceId, string entity, string value, UpdateValue body);
        /// <summary>
        /// Create workspace. Create a workspace based on component objects. You must provide workspace components defining the content of the new workspace.
        /// </summary>
        /// <param name="body">Valid data defining the content of the new workspace. (optional)</param>
        /// <returns><see cref="WorkspaceResponse" />WorkspaceResponse</returns>
        WorkspaceResponse CreateWorkspace(CreateWorkspace body = null);

        /// <summary>
        /// Delete workspace. Delete a workspace from the service instance.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteWorkspace(string workspaceId);

        /// <summary>
        /// Get information about a workspace. Get information about a workspace, optionally including all workspace content.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional, default to false)</param>
        /// <returns><see cref="WorkspaceExportResponse" />WorkspaceExportResponse</returns>
        WorkspaceExportResponse GetWorkspace(string workspaceId, bool? export = null);

        /// <summary>
        /// List workspaces. List the workspaces associated with a Conversation service instance.
        /// </summary>
        /// <param name="pageLimit">The number of records to return in each page of results. The default page limit is 100. (optional)</param>
        /// <param name="includeCount">Whether to include information about the number of records returned. (optional, default to false)</param>
        /// <param name="sort">Sorts the response according to the value of the specified property, in ascending or descending order. (optional)</param>
        /// <param name="cursor">A token identifying the last value from the previous page of results. (optional)</param>
        /// <returns><see cref="WorkspaceCollectionResponse" />WorkspaceCollectionResponse</returns>
        WorkspaceCollectionResponse ListWorkspaces(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null);

        /// <summary>
        /// Update workspace. Update an existing workspace with new or modified data. You must provide component objects defining the content of the updated workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <param name="body">Valid data defining the new workspace content. Any elements included in the new data will completely replace the existing elements, including all subelements. Previously existing subelements are not retained unless they are included in the new data. (optional)</param>
        /// <returns><see cref="WorkspaceResponse" />WorkspaceResponse</returns>
        WorkspaceResponse UpdateWorkspace(string workspaceId, UpdateWorkspace body = null);
    }
}
