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
    /// Numeric interval segments to categorize documents by using field values from a single numeric field to describe
    /// the category.
    /// </summary>
    public class QueryAggregationQueryHistogramAggregation : QueryAggregation
    {
        /// <summary>
        /// Specifies that the aggregation type is `histogram`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public new string Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }
        /// <summary>
        /// The numeric field name used to create the histogram.
        /// </summary>
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public new string Field
        {
            get { return base.Field; }
            set { base.Field = value; }
        }
        /// <summary>
        /// The size of the sections that the results are split into.
        /// </summary>
        [JsonProperty("interval", NullValueHandling = NullValueHandling.Ignore)]
        public new long? Interval { get; protected set; }
        /// <summary>
        /// Identifier that can optionally be specified in the query request of this aggregation.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        /// <summary>
        /// Array of numeric intervals.
        /// </summary>
        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public new List<QueryHistogramAggregationResult> Results { get; protected set; }
    }

}
