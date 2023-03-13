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
    /// A restriction that alters the document set that is used by the aggregations that it precedes. Subsequent
    /// aggregations are applied to nested documents from the specified field.
    /// </summary>
    public class QueryAggregationQueryNestedAggregation : QueryAggregation
    {
        /// <summary>
        /// Specifies that the aggregation type is `nested`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public new string Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }
        /// <summary>
        /// The path to the document field to scope subsequent aggregations to.
        /// </summary>
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public new string Path
        {
            get { return base.Path; }
            set { base.Path = value; }
        }
        /// <summary>
        /// Number of nested documents found in the specified field.
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
