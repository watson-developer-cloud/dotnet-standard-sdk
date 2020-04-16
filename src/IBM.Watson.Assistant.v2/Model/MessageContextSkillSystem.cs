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

using IBM.Cloud.SDK.Core.Model;
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// System context data used by the skill.
    /// </summary>
    public class MessageContextSkillSystem : DynamicModel<object>
    {
        /// <summary>
        /// An encoded string representing the current conversation state. By saving this value and then sending it in
        /// the context of a subsequent message request, you can restore the conversation to the same state. This can be
        /// useful if you need to return to an earlier point in the conversation or resume a paused conversation after
        /// the session has expired.
        /// </summary>
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
    }

}
