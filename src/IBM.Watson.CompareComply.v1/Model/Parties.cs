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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.Watson.CompareComply.v1.Model
{
    /// <summary>
    /// A party and its corresponding role, including address and contact information if identified.
    /// </summary>
    public class Parties : BaseModel
    {
        /// <summary>
        /// A string that identifies the importance of the party.
        /// </summary>
        /// <value>
        /// A string that identifies the importance of the party.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ImportanceEnum
        {
            
            /// <summary>
            /// Enum PRIMARY for Primary
            /// </summary>
            [EnumMember(Value = "Primary")]
            PRIMARY,
            
            /// <summary>
            /// Enum UNKNOWN for Unknown
            /// </summary>
            [EnumMember(Value = "Unknown")]
            UNKNOWN
        }

        /// <summary>
        /// A string that identifies the importance of the party.
        /// </summary>
        [JsonProperty("importance", NullValueHandling = NullValueHandling.Ignore)]
        public ImportanceEnum? Importance { get; set; }
        /// <summary>
        /// A string identifying the party.
        /// </summary>
        [JsonProperty("party", NullValueHandling = NullValueHandling.Ignore)]
        public string Party { get; set; }
        /// <summary>
        /// A string identifying the party's role.
        /// </summary>
        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }
        /// <summary>
        /// List of the party's address or addresses.
        /// </summary>
        [JsonProperty("addresses", NullValueHandling = NullValueHandling.Ignore)]
        public List<Address> Addresses { get; set; }
        /// <summary>
        /// List of the names and roles of contacts identified in the input document.
        /// </summary>
        [JsonProperty("contacts", NullValueHandling = NullValueHandling.Ignore)]
        public List<Contact> Contacts { get; set; }
    }

}
