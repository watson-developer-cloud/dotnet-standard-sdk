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
    /// The mapping between fields in the Watson Discovery collection and properties in the search response.
    /// </summary>
    public class SearchSettingsSchemaMapping
    {
        /// <summary>
        /// The field in the collection to map to the **url** property of the response.
        /// </summary>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        /// <summary>
        /// The field in the collection to map to the **body** property in the response.
        /// </summary>
        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }
        /// <summary>
        /// The field in the collection to map to the **title** property for the schema.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
    }

}
