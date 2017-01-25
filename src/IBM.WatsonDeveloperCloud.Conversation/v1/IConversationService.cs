/**
* Copyright 2015 IBM Corp. All Rights Reserved.
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
        /// Get a response to a user's input.
        /// </summary>
        /// <param name="workspaceId">Unique identifier of the workspace</param>
        /// <param name="request">A MessageRequest object that provides the input text and optional context</param>
        /// <returns></returns>
        MessageResponse Message(string workspaceId, MessageRequest request);
    }
}