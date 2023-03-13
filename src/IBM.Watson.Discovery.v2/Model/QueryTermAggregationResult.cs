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

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// Top value result for the `term` aggregation.
    /// </summary>
    public class QueryTermAggregationResult
    {
        /// <summary>
        /// Value of the field with a nonzero frequency in the document set.
        /// </summary>
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }
        /// <summary>
        /// Number of documents that contain the 'key'.
        /// </summary>
        [JsonProperty("matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public long? MatchingResults { get; set; }
        /// <summary>
        /// The relevancy score for this result. Returned only if `relevancy:true` is specified in the request.
        /// </summary>
        [JsonProperty("relevancy", NullValueHandling = NullValueHandling.Ignore)]
        public double? Relevancy { get; set; }
        /// <summary>
        /// Number of documents in the collection that contain the term in the specified field. Returned only when
        /// `relevancy:true` is specified in the request.
        /// </summary>
        [JsonProperty("total_matching_documents", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalMatchingDocuments { get; set; }
        /// <summary>
        /// Number of documents that are estimated to match the query and also meet the condition. Returned only when
        /// `relevancy:true` is specified in the request.
        /// </summary>
        [JsonProperty("estimated_matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public double? EstimatedMatchingResults { get; set; }
        /// <summary>
        /// An array of subaggregations. Returned only when this aggregation is combined with other aggregations in the
        /// request or is returned as a subaggregation.
        /// </summary>
        [JsonProperty("aggregations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, object>> Aggregations { get; set; }
    }

}
