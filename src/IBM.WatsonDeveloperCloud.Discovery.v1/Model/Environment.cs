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
    public class Environment : BaseModel
    {
        /// <summary>
        /// Current status of the environment. `resizing` is displayed when a request to increase the environment size
        /// has been made, but is still in the process of being completed.
        /// </summary>
        /// <value>
        /// Current status of the environment. `resizing` is displayed when a request to increase the environment size
        /// has been made, but is still in the process of being completed.
        /// </value>
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
            MAINTENANCE,
            
            /// <summary>
            /// Enum RESIZING for resizing
            /// </summary>
            [EnumMember(Value = "resizing")]
            RESIZING
        }

        /// <summary>
        /// Current size of the environment.
        /// </summary>
        /// <value>
        /// Current size of the environment.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SizeEnum
        {
            
            /// <summary>
            /// Enum LT for LT
            /// </summary>
            [EnumMember(Value = "LT")]
            LT,
            
            /// <summary>
            /// Enum XS for XS
            /// </summary>
            [EnumMember(Value = "XS")]
            XS,
            
            /// <summary>
            /// Enum S for S
            /// </summary>
            [EnumMember(Value = "S")]
            S,
            
            /// <summary>
            /// Enum MS for MS
            /// </summary>
            [EnumMember(Value = "MS")]
            MS,
            
            /// <summary>
            /// Enum M for M
            /// </summary>
            [EnumMember(Value = "M")]
            M,
            
            /// <summary>
            /// Enum ML for ML
            /// </summary>
            [EnumMember(Value = "ML")]
            ML,
            
            /// <summary>
            /// Enum L for L
            /// </summary>
            [EnumMember(Value = "L")]
            L,
            
            /// <summary>
            /// Enum XL for XL
            /// </summary>
            [EnumMember(Value = "XL")]
            XL,
            
            /// <summary>
            /// Enum XXL for XXL
            /// </summary>
            [EnumMember(Value = "XXL")]
            XXL,
            
            /// <summary>
            /// Enum XXXL for XXXL
            /// </summary>
            [EnumMember(Value = "XXXL")]
            XXXL
        }

        /// <summary>
        /// Current status of the environment. `resizing` is displayed when a request to increase the environment size
        /// has been made, but is still in the process of being completed.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Current size of the environment.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public SizeEnum? Size { get; set; }
        /// <summary>
        /// Unique identifier for the environment.
        /// </summary>
        [JsonProperty("environment_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string EnvironmentId { get; private set; }
        /// <summary>
        /// Name that identifies the environment.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Description of the environment.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// Creation date of the environment, in the format `yyyy-MM-dd'T'HH:mm:ss.SSS'Z'`.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Created { get; private set; }
        /// <summary>
        /// Date of most recent environment update, in the format `yyyy-MM-dd'T'HH:mm:ss.SSS'Z'`.
        /// </summary>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Updated { get; private set; }
        /// <summary>
        /// If `true`, the environment contains read-only collections that are maintained by IBM.
        /// </summary>
        [JsonProperty("read_only", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? _ReadOnly { get; private set; }
        /// <summary>
        /// The new size requested for this environment. Only returned when the environment *status* is `resizing`.
        ///
        /// *Note:* Querying and indexing can still be performed during an environment upsize.
        /// </summary>
        [JsonProperty("requested_size", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestedSize { get; set; }
        /// <summary>
        /// Details about the resource usage and capacity of the environment.
        /// </summary>
        [JsonProperty("index_capacity", NullValueHandling = NullValueHandling.Ignore)]
        public IndexCapacity IndexCapacity { get; set; }
        /// <summary>
        /// Information about Continuous Relevancy Training for this environment.
        /// </summary>
        [JsonProperty("search_status", NullValueHandling = NullValueHandling.Ignore)]
        public SearchStatus SearchStatus { get; set; }
    }

}
