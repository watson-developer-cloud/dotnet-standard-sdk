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
    /// An object describing the search skill configuration.
    /// </summary>
    public class SearchSettings
    {
        /// <summary>
        /// Configuration settings for the Watson Discovery service instance used by the search integration.
        /// </summary>
        [JsonProperty("discovery", NullValueHandling = NullValueHandling.Ignore)]
        public SearchSettingsDiscovery Discovery { get; set; }
        /// <summary>
        /// The messages included with responses from the search integration.
        /// </summary>
        [JsonProperty("messages", NullValueHandling = NullValueHandling.Ignore)]
        public SearchSettingsMessages Messages { get; set; }
        /// <summary>
        /// The mapping between fields in the Watson Discovery collection and properties in the search response.
        /// </summary>
        [JsonProperty("schema_mapping", NullValueHandling = NullValueHandling.Ignore)]
        public SearchSettingsSchemaMapping SchemaMapping { get; set; }
    }

}
