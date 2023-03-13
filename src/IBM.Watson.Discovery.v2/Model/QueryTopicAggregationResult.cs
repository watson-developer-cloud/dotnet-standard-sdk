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
    /// Result for the `topic` aggregation.
    /// </summary>
    public class QueryTopicAggregationResult
    {
        /// <summary>
        /// Array of subaggregations  of type `term` or `group_by` and `timeslice`. Each element of the matrix that is
        /// returned contains a **topic_indicator** that is calculated from the combination of each aggregation value
        /// and segment of time.
        /// </summary>
        [JsonProperty("aggregations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Dictionary<string, object>> Aggregations { get; set; }
    }

}
