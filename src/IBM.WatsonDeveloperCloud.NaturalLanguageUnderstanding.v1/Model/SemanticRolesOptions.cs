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

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model
{
    /// <summary>
    /// An option specifying whether or not to identify the subjects, actions, and verbs in the analyzed content.
    /// </summary>
    public class SemanticRolesOptions
    {
        /// <summary>
        /// Maximum number of semantic_roles results to return.
        /// </summary>
        /// <value>Maximum number of semantic_roles results to return.</value>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public long? Limit { get; set; }
        /// <summary>
        /// Set this to true to return keyword information for subjects and objects.
        /// </summary>
        /// <value>Set this to true to return keyword information for subjects and objects.</value>
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Keywords { get; set; }
        /// <summary>
        /// Set this to true to return entity information for subjects and objects.
        /// </summary>
        /// <value>Set this to true to return entity information for subjects and objects.</value>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Entities { get; set; }
    }

}
