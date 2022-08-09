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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// Finds results from documents that are similar to documents of interest. Use this parameter to add a *More like
    /// these* function to your search. You can include this parameter with or without a **query**, **filter** or
    /// **natural_language_query** parameter.
    /// </summary>
    public class QueryLargeSimilar
    {
        /// <summary>
        /// When `true`, includes documents in the query results that are similar to documents you specify.
        /// </summary>
        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enabled { get; set; }
        /// <summary>
        /// The list of documents of interest. Required if **enabled** is `true`.
        /// </summary>
        [JsonProperty("document_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> DocumentIds { get; set; }
        /// <summary>
        /// Looks for similarities in the specified subset of fields in the documents. If not specified, all of the
        /// document fields are used.
        /// </summary>
        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Fields { get; set; }
    }

}
