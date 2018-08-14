/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// WorkspaceSystemSettingsDisambiguation.
    /// </summary>
    public class WorkspaceSystemSettingsDisambiguation : BaseModel
    {
        /// <summary>
        /// The sensitivity of the disambiguation feature to intent detection conflicts. Set to **high** if you want the
        /// disambiguation feature to be triggered more often. This can be useful for testing or demonstration purposes.
        /// </summary>
        /// <value>
        /// The sensitivity of the disambiguation feature to intent detection conflicts. Set to **high** if you want the
        /// disambiguation feature to be triggered more often. This can be useful for testing or demonstration purposes.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SensitivityEnum
        {
            
            /// <summary>
            /// Enum AUTO for auto
            /// </summary>
            [EnumMember(Value = "auto")]
            AUTO,
            
            /// <summary>
            /// Enum HIGH for high
            /// </summary>
            [EnumMember(Value = "high")]
            HIGH
        }

        /// <summary>
        /// The sensitivity of the disambiguation feature to intent detection conflicts. Set to **high** if you want the
        /// disambiguation feature to be triggered more often. This can be useful for testing or demonstration purposes.
        /// </summary>
        [JsonProperty("sensitivity", NullValueHandling = NullValueHandling.Ignore)]
        public SensitivityEnum? Sensitivity { get; set; }
        /// <summary>
        /// The text of the introductory prompt that accompanies disambiguation options presented to the user.
        /// </summary>
        [JsonProperty("prompt", NullValueHandling = NullValueHandling.Ignore)]
        public string Prompt { get; set; }
        /// <summary>
        /// The user-facing label for the option users can select if none of the suggested options is correct. If no
        /// value is specified for this property, this option does not appear.
        /// </summary>
        [JsonProperty("none_of_the_above_prompt", NullValueHandling = NullValueHandling.Ignore)]
        public string NoneOfTheAbovePrompt { get; set; }
        /// <summary>
        /// Whether the disambiguation feature is enabled for the workspace.
        /// </summary>
        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enabled { get; set; }
    }

}
