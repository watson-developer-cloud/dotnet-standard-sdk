/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Summary of the memory usage statistics for this environment.
    /// </summary>
    public class MemoryUsage
    {
        /// <summary>
        /// Number of bytes used in the environment's memory capacity.
        /// </summary>
        /// <value>Number of bytes used in the environment's memory capacity.</value>
        [JsonProperty("used_bytes", NullValueHandling = NullValueHandling.Ignore)]
        public long? UsedBytes { get; private set; }
        /// <summary>
        /// Total number of bytes available in the environment's memory capacity.
        /// </summary>
        /// <value>Total number of bytes available in the environment's memory capacity.</value>
        [JsonProperty("total_bytes", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalBytes { get; private set; }
        /// <summary>
        /// Amount of memory capacity used, in KB or GB format.
        /// </summary>
        /// <value>Amount of memory capacity used, in KB or GB format.</value>
        [JsonProperty("used", NullValueHandling = NullValueHandling.Ignore)]
        public string Used { get; private set; }
        /// <summary>
        /// Total amount of the environment's memory capacity, in KB or GB format.
        /// </summary>
        /// <value>Total amount of the environment's memory capacity, in KB or GB format.</value>
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public string Total { get; private set; }
        /// <summary>
        /// Percentage of the environment's memory capacity that is being used.
        /// </summary>
        /// <value>Percentage of the environment's memory capacity that is being used.</value>
        [JsonProperty("percent_used", NullValueHandling = NullValueHandling.Ignore)]
        public double? PercentUsed { get; private set; }
    }

}
