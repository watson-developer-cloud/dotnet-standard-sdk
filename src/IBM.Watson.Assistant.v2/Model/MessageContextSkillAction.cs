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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// Context variables that are used by the action skill.
    /// </summary>
    public class MessageContextSkillAction
    {
        /// <summary>
        /// An object containing any arbitrary variables that can be read and written by a particular skill.
        /// </summary>
        [JsonProperty("user_defined", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> UserDefined { get; set; }
        /// <summary>
        /// System context data used by the skill.
        /// </summary>
        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public MessageContextSkillSystem System { get; set; }
        /// <summary>
        /// An object containing action variables. Action variables can be accessed only by steps in the same action,
        /// and do not persist after the action ends.
        /// </summary>
        [JsonProperty("action_variables", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> ActionVariables { get; set; }
        /// <summary>
        /// An object containing skill variables. (In the Watson Assistant user interface, skill variables are called
        /// _session variables_.) Skill variables can be accessed by any action and persist for the duration of the
        /// session.
        /// </summary>
        [JsonProperty("skill_variables", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> SkillVariables { get; set; }
    }

}
