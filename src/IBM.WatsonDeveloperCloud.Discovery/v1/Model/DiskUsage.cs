

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

    /// <summary>
    /// Summary of the disk usage statistics for this environment
    /// </summary>
    public partial class DiskUsage
    {
        /// <summary>
        /// Initializes a new instance of the DiskUsage class.
        /// </summary>
        public DiskUsage() { }

        /// <summary>
        /// Initializes a new instance of the DiskUsage class.
        /// </summary>
        /// <param name="usedBytes">Number of bytes used on the environment's
        /// disk capacity</param>
        /// <param name="totalBytes">Total number of bytes available in the
        /// environment's disk capacity</param>
        /// <param name="used">Amount of disk capacity used, in KB or GB
        /// format</param>
        /// <param name="total">Total amount of the environment's disk
        /// capacity, in KB or GB format</param>
        /// <param name="percentUsed">Percentage of the environment's disk
        /// capacity that is being used</param>
        public DiskUsage(int? usedBytes = default(int?), int? totalBytes = default(int?), string used = default(string), string total = default(string), double? percentUsed = default(double?))
        {
            UsedBytes = usedBytes;
            TotalBytes = totalBytes;
            Used = used;
            Total = total;
            PercentUsed = percentUsed;
        }

        /// <summary>
        /// Gets number of bytes used on the environment's disk capacity
        /// </summary>
        [JsonProperty(PropertyName = "used_bytes")]
        public int? UsedBytes { get; private set; }

        /// <summary>
        /// Gets total number of bytes available in the environment's disk
        /// capacity
        /// </summary>
        [JsonProperty(PropertyName = "total_bytes")]
        public int? TotalBytes { get; private set; }

        /// <summary>
        /// Gets amount of disk capacity used, in KB or GB format
        /// </summary>
        [JsonProperty(PropertyName = "used")]
        public string Used { get; private set; }

        /// <summary>
        /// Gets total amount of the environment's disk capacity, in KB or GB
        /// format
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public string Total { get; private set; }

        /// <summary>
        /// Gets percentage of the environment's disk capacity that is being
        /// used
        /// </summary>
        [JsonProperty(PropertyName = "percent_used")]
        public double? PercentUsed { get; private set; }

    }
}
