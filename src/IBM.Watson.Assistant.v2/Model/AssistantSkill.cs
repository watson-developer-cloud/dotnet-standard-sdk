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
    /// AssistantSkill.
    /// </summary>
    public class AssistantSkill
    {
        /// <summary>
        /// The type of the skill.
        /// </summary>
        public class TypeEnumValue
        {
            /// <summary>
            /// Constant DIALOG for dialog
            /// </summary>
            public const string DIALOG = "dialog";
            /// <summary>
            /// Constant ACTION for action
            /// </summary>
            public const string ACTION = "action";
            /// <summary>
            /// Constant SEARCH for search
            /// </summary>
            public const string SEARCH = "search";
            
        }

        /// <summary>
        /// The type of the skill.
        /// Constants for possible values can be found using AssistantSkill.TypeEnumValue
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// The skill ID of the skill.
        /// </summary>
        [JsonProperty("skill_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SkillId { get; set; }
    }

}
