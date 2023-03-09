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
    /// AssistantData.
    /// </summary>
    public class AssistantData
    {
        /// <summary>
        /// The unique identifier of the assistant.
        /// </summary>
        [JsonProperty("assistant_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string AssistantId { get; private set; }
        /// <summary>
        /// The name of the assistant. This string cannot contain carriage return, newline, or tab characters.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of the assistant. This string cannot contain carriage return, newline, or tab characters.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The language of the assistant.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// An array of skill references identifying the skills associated with the assistant.
        /// </summary>
        [JsonProperty("assistant_skills", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<AssistantSkill> AssistantSkills { get; private set; }
        /// <summary>
        /// An array of objects describing the environments defined for the assistant.
        /// </summary>
        [JsonProperty("assistant_environments", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<EnvironmentReference> AssistantEnvironments { get; private set; }
    }

}
