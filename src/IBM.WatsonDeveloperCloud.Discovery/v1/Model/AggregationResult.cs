

/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 the "License";
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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    using System.Linq;

    public partial class AggregationResult
    {
        /// <summary>
        /// Initializes a new instance of the AggregationResult class.
        /// </summary>
        public AggregationResult() { }

        /// <summary>
        /// Initializes a new instance of the AggregationResult class.
        /// </summary>
        public AggregationResult(string key = default(string), double? matchingResults = default(double?))
        {
            Key = key;
            MatchingResults = matchingResults;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "matching_results")]
        public double? MatchingResults { get; set; }

    }
}
