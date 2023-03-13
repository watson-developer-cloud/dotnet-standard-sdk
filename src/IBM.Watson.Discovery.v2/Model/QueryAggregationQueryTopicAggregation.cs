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
    /// Detects how much the frequency of a given facet value deviates from the expected average for the given time
    /// period. This aggregation type does not use data from previous time periods. It calculates an index by using the
    /// averages of frequency counts of other facet values for the given time period.
    /// </summary>
    public class QueryAggregationQueryTopicAggregation : QueryAggregation
    {
        /// <summary>
        /// Specifies that the aggregation type is `topic`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public new string Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }
        /// <summary>
        /// Specifies the `term` or `group_by` aggregation for the facet that you want to analyze.
        /// </summary>
        [JsonProperty("facet", NullValueHandling = NullValueHandling.Ignore)]
        public new string Facet
        {
            get { return base.Facet; }
            set { base.Facet = value; }
        }
        /// <summary>
        /// Specifies the `timeslice` aggregation that defines the time segments.
        /// </summary>
        [JsonProperty("time_segments", NullValueHandling = NullValueHandling.Ignore)]
        public new string TimeSegments
        {
            get { return base.TimeSegments; }
            set { base.TimeSegments = value; }
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
        public new List<QueryTopicAggregationResult> Results { get; protected set; }
    }

}
