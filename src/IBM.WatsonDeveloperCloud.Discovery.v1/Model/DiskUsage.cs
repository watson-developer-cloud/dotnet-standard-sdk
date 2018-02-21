/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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
    /// Summary of the disk usage statistics for the environment.
    /// </summary>
    public class DiskUsage
    {
        /// <summary>
        /// Number of bytes used on the environment's disk capacity.
        /// </summary>
        /// <value>Number of bytes used on the environment's disk capacity.</value>
        [JsonProperty("used_bytes", NullValueHandling = NullValueHandling.Ignore)]
        public virtual long? UsedBytes { get; private set; }
        /// <summary>
        /// Total number of bytes available in the environment's disk capacity.
        /// </summary>
        /// <value>Total number of bytes available in the environment's disk capacity.</value>
        [JsonProperty("maximum_allowed_bytes", NullValueHandling = NullValueHandling.Ignore)]
        public virtual long? MaximumAllowedBytes { get; private set; }
        /// <summary>
        /// **Deprecated**: Total number of bytes available in the environment's disk capacity.
        /// </summary>
        /// <value>**Deprecated**: Total number of bytes available in the environment's disk capacity.</value>
        [JsonProperty("total_bytes", NullValueHandling = NullValueHandling.Ignore)]
        public virtual long? TotalBytes { get; private set; }
        /// <summary>
        /// **Deprecated**: Amount of disk capacity used, in KB or GB format.
        /// </summary>
        /// <value>**Deprecated**: Amount of disk capacity used, in KB or GB format.</value>
        [JsonProperty("used", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Used { get; private set; }
        /// <summary>
        /// **Deprecated**: Total amount of the environment's disk capacity, in KB or GB format.
        /// </summary>
        /// <value>**Deprecated**: Total amount of the environment's disk capacity, in KB or GB format.</value>
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Total { get; private set; }
        /// <summary>
        /// **Deprecated**: Percentage of the environment's disk capacity that is being used.
        /// </summary>
        /// <value>**Deprecated**: Percentage of the environment's disk capacity that is being used.</value>
        [JsonProperty("percent_used", NullValueHandling = NullValueHandling.Ignore)]
        public virtual double? PercentUsed { get; private set; }
    }

}
