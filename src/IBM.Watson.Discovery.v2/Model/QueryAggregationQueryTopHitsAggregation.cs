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

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// Returns the top documents ranked by the score of the query.
    /// </summary>
    public class QueryAggregationQueryTopHitsAggregation : QueryAggregation
    {
        /// <summary>
        /// Specifies that the aggregation type is `top_hits`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public new string Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }
        /// <summary>
        /// The number of documents to return.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public new long? Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }
        /// <summary>
        /// Identifier specified in the query request of this aggregation.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        /// <summary>
        /// A query response that contains the matching documents for the preceding aggregations.
        /// </summary>
        [JsonProperty("hits", NullValueHandling = NullValueHandling.Ignore)]
        public new QueryTopHitsAggregationResult Hits
        {
            get { return base.Hits; }
            set { base.Hits = value; }
        }
    }

}
