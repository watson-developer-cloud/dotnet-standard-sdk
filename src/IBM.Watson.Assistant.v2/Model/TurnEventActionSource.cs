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

using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// TurnEventActionSource.
    /// </summary>
    public class TurnEventActionSource
    {
        /// <summary>
        /// The type of turn event.
        /// </summary>
        public class TypeEnumValue
        {
            /// <summary>
            /// Constant ACTION for action
            /// </summary>
            public const string ACTION = "action";
            
        }

        /// <summary>
        /// The type of turn event.
        /// Constants for possible values can be found using TurnEventActionSource.TypeEnumValue
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// An action that was visited during processing of the message.
        /// </summary>
        [JsonProperty("action", NullValueHandling = NullValueHandling.Ignore)]
        public string Action { get; set; }
        /// <summary>
        /// The title of the action.
        /// </summary>
        [JsonProperty("action_title", NullValueHandling = NullValueHandling.Ignore)]
        public string ActionTitle { get; set; }
        /// <summary>
        /// The condition that triggered the dialog node.
        /// </summary>
        [JsonProperty("condition", NullValueHandling = NullValueHandling.Ignore)]
        public string Condition { get; set; }
    }

}
