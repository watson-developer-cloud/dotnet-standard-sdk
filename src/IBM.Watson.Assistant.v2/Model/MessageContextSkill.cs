/**
* (C) Copyright IBM Corp. 2018, 2022.
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
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// Contains information specific to a particular skill used by the assistant. The property name must be the same as
    /// the name of the skill.
    ///
    /// **Note:** The default skill names are `main skill` for the dialog skill (if enabled), and `actions skill` for
    /// the actions skill.
    /// </summary>
    public class MessageContextSkill
    {
        /// <summary>
        /// Arbitrary variables that can be read and written by a particular skill.
        /// </summary>
        [JsonProperty("user_defined", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> UserDefined { get; set; }
        /// <summary>
        /// System context data used by the skill.
        /// </summary>
        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public MessageContextSkillSystem System { get; set; }
    }

}
