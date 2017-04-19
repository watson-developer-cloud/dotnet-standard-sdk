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
        #region Workspaces

        /// <summary>
        /// List the workspaces associated with a Conversation service instance.
        /// </summary>
        /// <returns></returns>
        WorkspaceCollectionResponse ListWorkspaces();

        /// <summary>
        /// Create a workspace based on JSON input. You must provide JSON data defining the content of the new workspace.
        /// </summary>
        /// <param name="request">A Workspace object defining the content of the new workspace.</param>
        /// <returns></returns>
        WorkspaceResponse CreateWorkspace(CreateWorkspace request);

        /// <summary>
        /// Delete a workspace from the service instance.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <returns></returns>
        object DeleteWorkspace(string workspaceId);

        /// <summary>
        /// Get information about a workspace, optionally including all workspace content.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <returns></returns>
        WorkspaceExportResponse GetWorkspace(string workspaceId);

        /// <summary>
        /// Get information about a workspace, optionally including all workspace content.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="export">Whether to include all workspace content (such as intents, entities, and dialog nodes) in the exported data. If export=false, the exported data includes only information about the workspace itself (such as the workspace name, description, language, and workspace ID). The default value is falset</param>
        /// <returns></returns>
        WorkspaceExportResponse GetWorkspace(string workspaceId, bool export = false);

        /// <summary>
        ///  Update an existing workspace with new or modified data. You must provide JSON data defining the content of the new workspace.
        ///  Any elements included in the new JSON will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are included in the new JSON.) For example, if you update an entity, the previous version of the entity(including all of its values and synonyms) is discarded and replaced with the new version specified in the JSON input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="request">The JSON data defining the new and updated workspace content</param>
        /// <returns></returns>
        WorkspaceResponse UpdateWorkspace(string workspaceId, UpdateWorkspace request);

        #endregion

        #region Message

        /// <summary>
        /// Get a response to a user's input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="request">A MessageRequest object that provides the input text and optional context</param>
        /// <returns></returns>
        MessageResponse Message(string workspaceId, MessageRequest request);

        #endregion

        #region Intents

        /// <summary>
        /// List the intents for a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <returns></returns>
        IntentCollectionResponse ListIntents(string workspaceId);

        /// <summary>
        /// List the intents for a workspace.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=false, the returned data includes only information about the element itself. If export=true, all content, including subelements, is included. The default value is false. </param>
        /// <returns></returns>
        IntentCollectionResponse ListIntents(string workspaceId, bool export = false);

        /// <summary>
        /// Create a new intent.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="request">A CreateIntent object defining the content of the new intent. </param>
        /// <returns></returns>
        IntentResponse CreateIntent(string workspaceId, CreateIntent request);

        /// <summary>
        /// Delete an intent from a workspace. 
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The intent name (for example, pizza_order). </param>
        /// <returns></returns>
        object DeleteIntent(string workspaceId, string intent);

        /// <summary>
        /// Get information about an intent, optionally including all intent content. 
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The intent name (for example, pizza_order).</param>
        /// <returns></returns>
        IntentExportResponse GetIntent(string workspaceId, string intent);

        /// <summary>
        /// Get information about an intent, optionally including all intent content. 
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The intent name (for example, pizza_order).</param>
        /// <param name="export">Whether to include all element content in the returned data. If export=false, the returned data includes only information about the element itself. If export=true, all content, including subelements, is included. The default value is false. </param>
        /// <returns></returns>
        IntentExportResponse GetIntent(string workspaceId, string intent, bool export = false);

        /// <summary>
        /// Update an existing intent with new or modified data. You must provide JSON data defining the content of the updated intent. Any elements included in the new JSON will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are included in the new JSON.) For example, if you update the user input examples for an intent, the previously existing examples are discarded and replaced with the new examples specified in the JSON input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The name of the intent to update.</param>
        /// <param name="request">An UpdateIntent object defining the updated content of the intent.</param>
        /// <returns></returns>
        IntentResponse UpdateIntent(string workspaceId, string intent, UpdateIntent request);

        #endregion

        #region Examples

        /// <summary>
        /// List the user input examples for an intent. 
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The intent name (for example, pizza_order).</param>
        /// <returns></returns>
        ExampleCollectionResponse ListExamples(string workspaceId, string intent);

        /// <summary>
        /// Add a new user input example to an intent.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The intent name (for example, pizza_order).</param>
        /// <param name="request">A CreateExample object defining the content of the new user input example..</param>
        /// <returns></returns>
        ExampleResponse CreateExample(string workspaceId, string intent, CreateExample request);

        /// <summary>
        /// Delete a user input example from an intent.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The intent name (for example, pizza_order).</param>
        /// <param name="text">The text of the user input example, with spaces and special characters in URL encoding.</param>
        /// <returns></returns>
        object DeleteExample(string workspaceId, string intent, string text);

        /// <summary>
        /// Get information about a user input example.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The intent name (for example, pizza_order).</param>
        /// <param name="text">The text of the user input example, with spaces and special characters in URL encoding.</param>
        /// <returns></returns>
        ExampleResponse GetExample(string workspaceId, string intent, string text);

        /// <summary>
        /// Update the text of a user input example.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="intent">The intent name (for example, pizza_order).</param>
        /// <param name="text">The text of the user input example, with spaces and special characters in URL encoding.</param>
        /// <param name="request">An UpdateExample object defining the new text for the user input example.</param>
        /// <returns></returns>
        ExampleResponse UpdateExample(string workspaceId, string intent, string text, UpdateExample request);       

        #endregion
    }
}