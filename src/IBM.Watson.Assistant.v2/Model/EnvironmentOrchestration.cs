/**
* (C) Copyright IBM Corp. 2022.
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

using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// The search skill orchestration settings for the environment.
    /// </summary>
    public class EnvironmentOrchestration
    {
        /// <summary>
        /// Whether assistants deployed to the environment fall back to a search skill when responding to messages that
        /// do not match any intent. If no search skill is configured for the assistant, this property is ignored.
        /// </summary>
        [JsonProperty("search_skill_fallback", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SearchSkillFallback { get; set; }
    }

}
