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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// DocumentSnapshot.
    /// </summary>
    public class DocumentSnapshot
    {
        /// <summary>
        /// Gets or Sets Step
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StepEnum
        {
            
            /// <summary>
            /// Enum HTML_INPUT for html_input
            /// </summary>
            [EnumMember(Value = "html_input")]
            HTML_INPUT,
            
            /// <summary>
            /// Enum HTML_OUTPUT for html_output
            /// </summary>
            [EnumMember(Value = "html_output")]
            HTML_OUTPUT,
            
            /// <summary>
            /// Enum JSON_OUTPUT for json_output
            /// </summary>
            [EnumMember(Value = "json_output")]
            JSON_OUTPUT,
            
            /// <summary>
            /// Enum JSON_NORMALIZATIONS_OUTPUT for json_normalizations_output
            /// </summary>
            [EnumMember(Value = "json_normalizations_output")]
            JSON_NORMALIZATIONS_OUTPUT,
            
            /// <summary>
            /// Enum ENRICHMENTS_OUTPUT for enrichments_output
            /// </summary>
            [EnumMember(Value = "enrichments_output")]
            ENRICHMENTS_OUTPUT,
            
            /// <summary>
            /// Enum NORMALIZATIONS_OUTPUT for normalizations_output
            /// </summary>
            [EnumMember(Value = "normalizations_output")]
            NORMALIZATIONS_OUTPUT
        }

        /// <summary>
        /// Gets or Sets Step
        /// </summary>
        [JsonProperty("step", NullValueHandling = NullValueHandling.Ignore)]
        public StepEnum? Step { get; set; }
        /// <summary>
        /// Gets or Sets Snapshot
        /// </summary>
        [JsonProperty("snapshot", NullValueHandling = NullValueHandling.Ignore)]
        public object Snapshot { get; set; }
    }

}
