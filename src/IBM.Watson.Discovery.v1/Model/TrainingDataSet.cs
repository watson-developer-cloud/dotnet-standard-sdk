/**
* (C) Copyright IBM Corp. 2017, 2023.
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

namespace IBM.Watson.Discovery.v1.Model
{
    /// <summary>
    /// Training information for a specific collection.
    /// </summary>
    public class TrainingDataSet
    {
        /// <summary>
        /// The environment id associated with this training data set.
        /// </summary>
        [JsonProperty("environment_id", NullValueHandling = NullValueHandling.Ignore)]
        public string EnvironmentId { get; set; }
        /// <summary>
        /// The collection id associated with this training data set.
        /// </summary>
        [JsonProperty("collection_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CollectionId { get; set; }
        /// <summary>
        /// Array of training queries. At least 50 queries are required for training to begin. A maximum of 10,000
        /// queries are returned.
        /// </summary>
        [JsonProperty("queries", NullValueHandling = NullValueHandling.Ignore)]
        public List<TrainingQuery> Queries { get; set; }
    }

}
