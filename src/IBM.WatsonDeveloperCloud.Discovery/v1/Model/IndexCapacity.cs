

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
    /// Details about the disk and memory usage of this environment.
    /// </summary>
    public partial class IndexCapacity
    {
        /// <summary>
        /// Initializes a new instance of the IndexCapacity class.
        /// </summary>
        public IndexCapacity() { }

        /// <summary>
        /// Initializes a new instance of the IndexCapacity class.
        /// </summary>
        /// <param name="diskUsage">Summary of the disk usage of the
        /// environment</param>
        /// <param name="memoryUsage">Summary of the memory usage of the
        /// environment</param>
        public IndexCapacity(DiskUsage diskUsage = default(DiskUsage), MemoryUsage memoryUsage = default(MemoryUsage))
        {
            DiskUsage = diskUsage;
            MemoryUsage = memoryUsage;
        }

        /// <summary>
        /// Gets or sets summary of the disk usage of the environment
        /// </summary>
        [JsonProperty(PropertyName = "disk_usage")]
        public DiskUsage DiskUsage { get; set; }

        /// <summary>
        /// Gets or sets summary of the memory usage of the environment
        /// </summary>
        [JsonProperty(PropertyName = "memory_usage")]
        public MemoryUsage MemoryUsage { get; set; }

    }
}
