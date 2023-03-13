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
    /// Calculates relevancy values using combinations of document sets from results of the specified pair of
    /// aggregations.
    /// </summary>
    public class QueryAggregationQueryPairAggregation : QueryAggregation
    {
        /// <summary>
        /// Specifies that the aggregation type is `pair`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public new string Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }
        /// <summary>
        /// Specifies the first aggregation in the pair. The aggregation must be a `term`, `group_by`, `histogram`, or
        /// `timeslice` aggregation type.
        /// </summary>
        [JsonProperty("first", NullValueHandling = NullValueHandling.Ignore)]
        public new string First
        {
            get { return base.First; }
            set { base.First = value; }
        }
        /// <summary>
        /// Specifies the second aggregation in the pair. The aggregation must be a `term`, `group_by`, `histogram`, or
        /// `timeslice` aggregation type.
        /// </summary>
        [JsonProperty("second", NullValueHandling = NullValueHandling.Ignore)]
        public new string Second
        {
            get { return base.Second; }
            set { base.Second = value; }
        }
        /// <summary>
        /// Indicates whether to include estimated matching result information.
        /// </summary>
        [JsonProperty("show_estimated_matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public new bool? ShowEstimatedMatchingResults
        {
            get { return base.ShowEstimatedMatchingResults; }
            set { base.ShowEstimatedMatchingResults = value; }
        }
        /// <summary>
        /// Indicates whether to include total matching documents information.
        /// </summary>
        [JsonProperty("show_total_matching_documents", NullValueHandling = NullValueHandling.Ignore)]
        public new bool? ShowTotalMatchingDocuments
        {
            get { return base.ShowTotalMatchingDocuments; }
            set { base.ShowTotalMatchingDocuments = value; }
        }
        /// <summary>
        /// An array of aggregations.
        /// </summary>
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public new List<QueryPairAggregationResult> Results { get; protected set; }
    }

}
