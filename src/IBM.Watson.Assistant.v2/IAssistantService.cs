/**
* (C) Copyright IBM Corp. 2019, 2023.
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
using Environment = IBM.Watson.Assistant.v2.Model.Environment;

namespace IBM.Watson.Assistant.v2
{
    public partial interface IAssistantService
    {
        DetailedResponse<AssistantData> CreateAssistant(string language = null, string name = null, string description = null);
        DetailedResponse<AssistantCollection> ListAssistants(long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<object> DeleteAssistant(string assistantId);
        DetailedResponse<SessionResponse> CreateSession(string assistantId, RequestAnalytics analytics = null);
        DetailedResponse<object> DeleteSession(string assistantId, string sessionId);
        DetailedResponse<MessageResponse> Message(string assistantId, string sessionId, MessageInput input = null, MessageContext context = null, string userId = null);
        DetailedResponse<MessageResponseStateless> MessageStateless(string assistantId, MessageInputStateless input = null, MessageContextStateless context = null, string userId = null);
        DetailedResponse<BulkClassifyResponse> BulkClassify(string skillId, List<BulkClassifyUtterance> input);
        DetailedResponse<LogCollection> ListLogs(string assistantId, string sort = null, string filter = null, long? pageLimit = null, string cursor = null);
        DetailedResponse<object> DeleteUserData(string customerId);
        DetailedResponse<EnvironmentCollection> ListEnvironments(string assistantId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Environment> GetEnvironment(string assistantId, string environmentId, bool? includeAudit = null);
        DetailedResponse<Environment> UpdateEnvironment(string assistantId, string environmentId, string name = null, string description = null, long? sessionTimeout = null, List<EnvironmentSkill> skillReferences = null);
        DetailedResponse<Release> CreateRelease(string assistantId, string description = null);
        DetailedResponse<ReleaseCollection> ListReleases(string assistantId, long? pageLimit = null, bool? includeCount = null, string sort = null, string cursor = null, bool? includeAudit = null);
        DetailedResponse<Release> GetRelease(string assistantId, string release, bool? includeAudit = null);
        DetailedResponse<object> DeleteRelease(string assistantId, string release);
        DetailedResponse<Environment> DeployRelease(string assistantId, string release, string environmentId, bool? includeAudit = null);
        DetailedResponse<Skill> GetSkill(string assistantId, string skillId);
        DetailedResponse<Skill> UpdateSkill(string assistantId, string skillId, string name = null, string description = null, Dictionary<string, object> workspace = null, Dictionary<string, object> dialogSettings = null, SearchSettings searchSettings = null);
        DetailedResponse<SkillsExport> ExportSkills(string assistantId, bool? includeAudit = null);
        DetailedResponse<SkillsAsyncRequestStatus> ImportSkills(string assistantId, List<SkillImport> assistantSkills, AssistantState assistantState, bool? includeAudit = null);
        DetailedResponse<SkillsAsyncRequestStatus> ImportSkillsStatus(string assistantId);
    }
}
