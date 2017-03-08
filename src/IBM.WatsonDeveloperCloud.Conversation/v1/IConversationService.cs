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
    }
}