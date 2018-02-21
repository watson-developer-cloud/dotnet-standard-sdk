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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// DeleteConfigurationResponse.
    /// </summary>
    public class DeleteConfigurationResponse
    {
        /// <summary>
        /// Status of the configuration. A deleted configuration has the status deleted.
        /// </summary>
        /// <value>Status of the configuration. A deleted configuration has the status deleted.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum DELETED for deleted
            /// </summary>
            [EnumMember(Value = "deleted")]
            DELETED
        }

        /// <summary>
        /// Status of the configuration. A deleted configuration has the status deleted.
        /// </summary>
        /// <value>Status of the configuration. A deleted configuration has the status deleted.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// The unique identifier for the configuration.
        /// </summary>
        /// <value>The unique identifier for the configuration.</value>
        [JsonProperty("configuration_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfigurationId { get; set; }
        /// <summary>
        /// An array of notice messages, if any.
        /// </summary>
        /// <value>An array of notice messages, if any.</value>
        [JsonProperty("notices", NullValueHandling = NullValueHandling.Ignore)]
        public List<Notice> Notices { get; set; }
    }

}
