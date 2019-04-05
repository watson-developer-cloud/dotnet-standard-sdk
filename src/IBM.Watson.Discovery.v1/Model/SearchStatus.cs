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
using IBM.Cloud.SDK.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.Watson.Discovery.v1.Model
{
    /// <summary>
    /// Information about the Continuous Relevancy Training for this environment.
    /// </summary>
    public class SearchStatus : BaseModel
    {
        /// <summary>
        /// The current status of Continuous Relevancy Training for this environment.
        /// </summary>
        /// <value>
        /// The current status of Continuous Relevancy Training for this environment.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum NO_DATA for NO_DATA
            /// </summary>
            [EnumMember(Value = "NO_DATA")]
            NO_DATA,
            
            /// <summary>
            /// Enum INSUFFICENT_DATA for INSUFFICENT_DATA
            /// </summary>
            [EnumMember(Value = "INSUFFICENT_DATA")]
            INSUFFICENT_DATA,
            
            /// <summary>
            /// Enum TRAINING for TRAINING
            /// </summary>
            [EnumMember(Value = "TRAINING")]
            TRAINING,
            
            /// <summary>
            /// Enum TRAINED for TRAINED
            /// </summary>
            [EnumMember(Value = "TRAINED")]
            TRAINED,
            
            /// <summary>
            /// Enum NOT_APPLICABLE for NOT_APPLICABLE
            /// </summary>
            [EnumMember(Value = "NOT_APPLICABLE")]
            NOT_APPLICABLE
        }

        /// <summary>
        /// The current status of Continuous Relevancy Training for this environment.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Current scope of the training. Always returned as `environment`.
        /// </summary>
        [JsonProperty("scope", NullValueHandling = NullValueHandling.Ignore)]
        public string Scope { get; set; }
        /// <summary>
        /// Long description of the current Continuous Relevancy Training status.
        /// </summary>
        [JsonProperty("status_description", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusDescription { get; set; }
        /// <summary>
        /// The date stamp of the most recent completed training for this environment.
        /// </summary>
        [JsonProperty("last_trained", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastTrained { get; set; }
    }

}
