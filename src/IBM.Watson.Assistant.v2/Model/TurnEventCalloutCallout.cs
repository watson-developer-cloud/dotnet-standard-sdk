/**
* (C) Copyright IBM Corp. 2022.
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
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// TurnEventCalloutCallout.
    /// </summary>
    public class TurnEventCalloutCallout
    {
        /// <summary>
        /// callout type.
        /// </summary>
        public class TypeEnumValue
        {
            /// <summary>
            /// Constant INTEGRATION_INTERACTION for integration_interaction
            /// </summary>
            public const string INTEGRATION_INTERACTION = "integration_interaction";
            
        }

        /// <summary>
        /// callout type.
        /// Constants for possible values can be found using TurnEventCalloutCallout.TypeEnumValue
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// For internal use only.
        /// </summary>
        [JsonProperty("internal", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> _Internal { get; set; }
    }

}
