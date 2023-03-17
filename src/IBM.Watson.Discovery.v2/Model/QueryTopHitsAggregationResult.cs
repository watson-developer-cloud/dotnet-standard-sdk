/**
* (C) Copyright IBM Corp. 2019, 2023.
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
    /// A query response that contains the matching documents for the preceding aggregations.
    /// </summary>
    public class QueryTopHitsAggregationResult
    {
        /// <summary>
        /// Number of matching results.
        /// </summary>
        [JsonProperty("matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public long? MatchingResults { get; set; }
        /// <summary>
        /// An array of the document results in an ordered list.
        /// </summary>
        [JsonProperty("hits", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, object>> Hits { get; set; }
    }

}
