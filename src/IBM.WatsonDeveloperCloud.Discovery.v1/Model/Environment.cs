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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Details about an environment.
    /// </summary>
    public class Environment
    {
        /// <summary>
        /// Status of the environment.
        /// </summary>
        /// <value>Status of the environment.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum ACTIVE for active
            /// </summary>
            [EnumMember(Value = "active")]
            ACTIVE,
            
            /// <summary>
            /// Enum PENDING for pending
            /// </summary>
            [EnumMember(Value = "pending")]
            PENDING,
            
            /// <summary>
            /// Enum MAINTENANCE for maintenance
            /// </summary>
            [EnumMember(Value = "maintenance")]
            MAINTENANCE
        }

        /// <summary>
        /// Status of the environment.
        /// </summary>
        /// <value>Status of the environment.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Unique identifier for the environment.
        /// </summary>
        /// <value>Unique identifier for the environment.</value>
        [JsonProperty("environment_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string EnvironmentId { get; private set; }
        /// <summary>
        /// Name that identifies the environment.
        /// </summary>
        /// <value>Name that identifies the environment.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Description of the environment.
        /// </summary>
        /// <value>Description of the environment.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// Creation date of the environment, in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.
        /// </summary>
        /// <value>Creation date of the environment, in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Created { get; private set; }
        /// <summary>
        /// Date of most recent environment update, in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.
        /// </summary>
        /// <value>Date of most recent environment update, in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Updated { get; private set; }
        /// <summary>
        /// If true, then the environment contains read-only collections which are maintained by IBM.
        /// </summary>
        /// <value>If true, then the environment contains read-only collections which are maintained by IBM.</value>
        [JsonProperty("read_only", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? _ReadOnly { get; private set; }
        /// <summary>
        /// **Deprecated**: Size of the environment.
        /// </summary>
        /// <value>**Deprecated**: Size of the environment.</value>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long? Size { get; set; }
        /// <summary>
        /// Details about the resource usage and capacity of the environment.
        /// </summary>
        /// <value>Details about the resource usage and capacity of the environment.</value>
        [JsonProperty("index_capacity", NullValueHandling = NullValueHandling.Ignore)]
        public IndexCapacity IndexCapacity { get; set; }
    }

}
