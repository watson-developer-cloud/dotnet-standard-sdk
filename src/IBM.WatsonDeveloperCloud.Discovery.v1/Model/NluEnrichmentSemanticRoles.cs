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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// An object specifiying the semantic roles enrichment and related parameters.
    /// </summary>
    public class NluEnrichmentSemanticRoles
    {
        /// <summary>
        /// When `true` entities are extracted from the identified sentence parts.
        /// </summary>
        /// <value>When `true` entities are extracted from the identified sentence parts.</value>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Entities { get; set; }
        /// <summary>
        /// When `true`, keywords are extracted from the identified sentence parts.
        /// </summary>
        /// <value>When `true`, keywords are extracted from the identified sentence parts.</value>
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Keywords { get; set; }
        /// <summary>
        /// The maximum number of semantic roles enrichments to extact from each instance of the specified field.
        /// </summary>
        /// <value>The maximum number of semantic roles enrichments to extact from each instance of the specified field.</value>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public long? Limit { get; set; }
    }

}
