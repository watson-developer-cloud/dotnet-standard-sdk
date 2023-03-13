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
    /// A modifier that narrows the document set of the subaggregations it precedes.
    /// </summary>
    public class QueryAggregationQueryFilterAggregation : QueryAggregation
    {
        /// <summary>
        /// Specifies that the aggregation type is `filter`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public new string Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }
        /// <summary>
        /// The filter that is written in Discovery Query Language syntax and is applied to the documents before
        /// subaggregations are run.
        /// </summary>
        [JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
        public new string Match
        {
            get { return base.Match; }
            set { base.Match = value; }
        }
        /// <summary>
        /// Number of documents that match the filter.
        /// </summary>
        [JsonProperty("matching_results", NullValueHandling = NullValueHandling.Ignore)]
        public new long? MatchingResults
        {
            get { return base.MatchingResults; }
            set { base.MatchingResults = value; }
        }
        /// <summary>
        /// An array of subaggregations.
        /// </summary>
        [JsonProperty("aggregations", NullValueHandling = NullValueHandling.Ignore)]
        public new List<Dictionary<string, object>> Aggregations
        {
            get { return base.Aggregations; }
            set { base.Aggregations = value; }
        }
    }

}
