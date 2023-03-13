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
    /// Returns a scalar calculation across all documents for the field specified. Possible calculations include min,
    /// max, sum, average, and unique_count.
    /// </summary>
    public class QueryAggregationQueryCalculationAggregation : QueryAggregation
    {
        /// <summary>
        /// Specifies the calculation type, such as 'average`, `max`, `min`, `sum`, or `unique_count`.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public new string Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }
        /// <summary>
        /// The field to perform the calculation on.
        /// </summary>
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public new string Field
        {
            get { return base.Field; }
            set { base.Field = value; }
        }
        /// <summary>
        /// The value of the calculation.
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public new double? Value
        {
            get { return base.Value; }
            set { base.Value = value; }
        }
    }

}
