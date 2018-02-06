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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// RegisterStatus.
    /// </summary>
    public class RegisterStatus
    {
        /// <summary>
        /// The current status of the job: * `created` if the callback URL was successfully white-listed as a result of the call. * `already created` if the URL was already white-listed.
        /// </summary>
        /// <value>The current status of the job: * `created` if the callback URL was successfully white-listed as a result of the call. * `already created` if the URL was already white-listed.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum CREATED for created
            /// </summary>
            [EnumMember(Value = "created")]
            CREATED,
            
            /// <summary>
            /// Enum ALREADY_CREATED for already created
            /// </summary>
            [EnumMember(Value = "already created")]
            ALREADY_CREATED
        }

        /// <summary>
        /// The current status of the job: * `created` if the callback URL was successfully white-listed as a result of the call. * `already created` if the URL was already white-listed.
        /// </summary>
        /// <value>The current status of the job: * `created` if the callback URL was successfully white-listed as a result of the call. * `already created` if the URL was already white-listed.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// The callback URL that is successfully registered.
        /// </summary>
        /// <value>The callback URL that is successfully registered.</value>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }

}
