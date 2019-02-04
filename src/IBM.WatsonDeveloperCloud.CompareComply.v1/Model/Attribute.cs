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

namespace IBM.WatsonDeveloperCloud.CompareComply.v1.Model
{
    /// <summary>
    /// List of document attributes.
    /// </summary>
    public class Attribute : BaseModel
    {
        /// <summary>
        /// The type of attribute.
        /// </summary>
        /// <value>
        /// The type of attribute.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            
            /// <summary>
            /// Enum ADDRESS for Address
            /// </summary>
            [EnumMember(Value = "Address")]
            ADDRESS,
            
            /// <summary>
            /// Enum CURRENCY for Currency
            /// </summary>
            [EnumMember(Value = "Currency")]
            CURRENCY,
            
            /// <summary>
            /// Enum DATETIME for DateTime
            /// </summary>
            [EnumMember(Value = "DateTime")]
            DATETIME,
            
            /// <summary>
            /// Enum LOCATION for Location
            /// </summary>
            [EnumMember(Value = "Location")]
            LOCATION,
            
            /// <summary>
            /// Enum ORGANIZATION for Organization
            /// </summary>
            [EnumMember(Value = "Organization")]
            ORGANIZATION,
            
            /// <summary>
            /// Enum PERSON for Person
            /// </summary>
            [EnumMember(Value = "Person")]
            PERSON
        }

        /// <summary>
        /// The type of attribute.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// The text associated with the attribute.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// The numeric location of the identified element in the document, represented with two integers labeled
        /// `begin` and `end`.
        /// </summary>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public Location Location { get; set; }
    }

}
