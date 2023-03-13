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
    /// Returns results from the field that is specified.
    /// </summary>
    public class QueryAggregationQueryTermAggregation : QueryAggregation
    {
        /// <summary>
        /// Specifies that the aggregation type is `term`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public new string Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }
        /// <summary>
        /// The field in the document where the values come from.
        /// </summary>
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public new string Field
        {
            get { return base.Field; }
            set { base.Field = value; }
        }
        /// <summary>
        /// The number of results returned. Not returned if `relevancy:true` is specified in the request.
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public new long? Count
        {
            get { return base.Count; }
            set { base.Count = value; }
        }
        /// <summary>
        /// Identifier specified in the query request of this aggregation. Not returned if `relevancy:true` is specified
        /// in the request.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        /// <summary>
        /// An array of results.
        /// </summary>
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public new List<QueryTermAggregationResult> Results { get; protected set; }
    }

}
