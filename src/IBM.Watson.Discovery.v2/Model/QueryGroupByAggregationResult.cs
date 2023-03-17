/**
* (C) Copyright IBM Corp. 2020, 2023.
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
    /// Result group for the `group_by` aggregation.
    /// </summary>
    public class QueryGroupByAggregationResult
    {
        /// <summary>
        /// The condition that is met by the documents in this group. For example, `YEARTXT<2000`.
        /// </summary>
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }
        /// <summary>
        /// Number of documents that meet the query and condition.
        /// </summary>
        [JsonProperty("matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public long? MatchingResults { get; set; }
        /// <summary>
        /// The relevancy for this group. Returned only if `relevancy:true` is specified in the request.
        /// </summary>
        [JsonProperty("relevancy", NullValueHandling = NullValueHandling.Ignore)]
        public double? Relevancy { get; set; }
        /// <summary>
        /// Number of documents that meet the condition in the whole set of documents in this collection. Returned only
        /// when `relevancy:true` is specified in the request.
        /// </summary>
        [JsonProperty("total_matching_documents", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalMatchingDocuments { get; set; }
        /// <summary>
        /// The number of documents that are estimated to match the query and condition. Returned only when
        /// `relevancy:true` is specified in the request.
        /// </summary>
        [JsonProperty("estimated_matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public double? EstimatedMatchingResults { get; set; }
        /// <summary>
        /// An array of subaggregations. Returned only when this aggregation is returned as a subaggregation.
        /// </summary>
        [JsonProperty("aggregations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, object>> Aggregations { get; set; }
    }

}
