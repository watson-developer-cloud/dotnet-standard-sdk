

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

    public partial class QueryResult
    {
        /// <summary>
        /// Initializes a new instance of the QueryResult class.
        /// </summary>
        public QueryResult() { }

        /// <summary>
        /// Initializes a new instance of the QueryResult class.
        /// </summary>
        public QueryResult(string id = default(string), double? score = default(double?))
        {
            Id = id;
            Score = score;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public double? Score { get; set; }

    }
}
