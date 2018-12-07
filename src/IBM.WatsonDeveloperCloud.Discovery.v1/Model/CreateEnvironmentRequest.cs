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

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// CreateEnvironmentRequest.
    /// </summary>
    public class CreateEnvironmentRequest : BaseModel
    {
        /// <summary>
        /// Size of the environment. In the Lite plan the default and only accepted value is `LT`, in all other plans
        /// the default is `S`.
        /// </summary>
        /// <value>
        /// Size of the environment. In the Lite plan the default and only accepted value is `LT`, in all other plans
        /// the default is `S`.
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
        /// Size of the environment. In the Lite plan the default and only accepted value is `LT`, in all other plans
        /// the default is `S`.
        /// </summary>
        [Obsolete("Integer size is deprecated. Please use StringSize.")]
        public long? Size
        {
            get
            {
                int size;
                int.TryParse(_convertedSize, out size);
                return size;
            }
            set { _convertedSize = value.ToString(); }
        }
        /// <summary>
        /// Size of the environment.
        /// </summary>
        public SizeEnum? StringSize
        {
            get
            {
                SizeEnum size;
                Enum.TryParse(_convertedSize, out size);
                return size;
            }
            set { _convertedSize = value.ToString(); }
        }
        /// <summary>
        /// Size of the environment
        /// </summary>
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public string _convertedSize { get; set; }
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
