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
    /// SkillReference.
    /// </summary>
    public class SkillReference
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
        /// Constants for possible values can be found using SkillReference.TypeEnumValue
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// The skill ID of the skill.
        /// </summary>
        [JsonProperty("skill_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SkillId { get; set; }
        /// <summary>
        /// Whether the skill is disabled. A disabled skill in the draft environment does not handle any messages at run
        /// time, and it is not included in saved releases.
        /// </summary>
        [JsonProperty("disabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Disabled { get; set; }
        /// <summary>
        /// The name of the snapshot (skill version) that is saved as part of the release (for example, `draft` or `1`).
        /// </summary>
        [JsonProperty("snapshot", NullValueHandling = NullValueHandling.Ignore)]
        public string Snapshot { get; set; }
        /// <summary>
        /// The type of skill identified by the skill reference. The possible values are `main skill` (for a dialog
        /// skill), `actions skill`, and `search skill`.
        /// </summary>
        [JsonProperty("skill_reference", NullValueHandling = NullValueHandling.Ignore)]
        public string _SkillReference { get; set; }
    }

}
