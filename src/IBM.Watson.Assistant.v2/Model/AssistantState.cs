/**
* (C) Copyright IBM Corp. 2023.
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
    /// Status information about the skills for the assistant. Included in responses only if **status**=`Available`.
    /// </summary>
    public class AssistantState
    {
        /// <summary>
        /// Whether the action skill is disabled in the draft environment.
        /// </summary>
        [JsonProperty("action_disabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ActionDisabled { get; set; }
        /// <summary>
        /// Whether the dialog skill is disabled in the draft environment.
        /// </summary>
        [JsonProperty("dialog_disabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DialogDisabled { get; set; }
    }

}
