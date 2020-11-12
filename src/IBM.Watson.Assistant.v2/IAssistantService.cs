/**
* (C) Copyright IBM Corp. 2019, 2020.
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
using IBM.Watson.Assistant.v2.Model;

namespace IBM.Watson.Assistant.v2
{
    public partial interface IAssistantService
    {
        DetailedResponse<SessionResponse> CreateSession(string assistantId);
        DetailedResponse<object> DeleteSession(string assistantId, string sessionId);
        DetailedResponse<MessageResponse> Message(string assistantId, string sessionId, MessageInput input = null, MessageContext context = null);
        DetailedResponse<MessageResponseStateless> MessageStateless(string assistantId, MessageInputStateless input = null, MessageContextStateless context = null);
        DetailedResponse<LogCollection> ListLogs(string assistantId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null);
        DetailedResponse<object> DeleteUserData(string customerId);
        DetailedResponse<BulkClassifyResponse> BulkClassify(string skillId, List<BulkClassifyUtterance> input = null);
    }
}
