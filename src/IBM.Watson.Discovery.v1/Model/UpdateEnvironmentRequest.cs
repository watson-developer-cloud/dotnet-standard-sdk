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

namespace IBM.Watson.Discovery.v1.Model
{
    /// <summary>
    /// UpdateEnvironmentRequest.
    /// </summary>
    public class UpdateEnvironmentRequest : BaseModel
    {
        /// <summary>
        /// Size that the environment should be increased to. Environment size cannot be modified when using a Lite
        /// plan. Environment size can only increased and not decreased.
        /// </summary>
        /// <value>
        /// Size that the environment should be increased to. Environment size cannot be modified when using a Lite
        /// plan. Environment size can only increased and not decreased.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SizeEnum
        {
            
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
        /// Size that the environment should be increased to. Environment size cannot be modified when using a Lite
        /// plan. Environment size can only increased and not decreased.
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public SizeEnum? Size { get; set; }
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
    }

}
