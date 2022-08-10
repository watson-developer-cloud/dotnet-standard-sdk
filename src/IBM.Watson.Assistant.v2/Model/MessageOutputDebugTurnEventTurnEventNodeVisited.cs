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
    /// MessageOutputDebugTurnEventTurnEventNodeVisited.
    /// </summary>
    public class MessageOutputDebugTurnEventTurnEventNodeVisited : MessageOutputDebugTurnEvent
    {
        /// <summary>
        /// The reason the dialog node was visited.
        /// </summary>
        public class ReasonEnumValue
        {
            /// <summary>
            /// Constant WELCOME for welcome
            /// </summary>
            public const string WELCOME = "welcome";
            /// <summary>
            /// Constant BRANCH_START for branch_start
            /// </summary>
            public const string BRANCH_START = "branch_start";
            /// <summary>
            /// Constant TOPIC_SWITCH for topic_switch
            /// </summary>
            public const string TOPIC_SWITCH = "topic_switch";
            /// <summary>
            /// Constant TOPIC_RETURN for topic_return
            /// </summary>
            public const string TOPIC_RETURN = "topic_return";
            /// <summary>
            /// Constant TOPIC_SWITCH_WITHOUT_RETURN for topic_switch_without_return
            /// </summary>
            public const string TOPIC_SWITCH_WITHOUT_RETURN = "topic_switch_without_return";
            /// <summary>
            /// Constant JUMP for jump
            /// </summary>
            public const string JUMP = "jump";
            
        }

        /// <summary>
        /// The type of turn event.
        /// </summary>
        [JsonProperty("event", NullValueHandling = NullValueHandling.Ignore)]
        public new string _Event
        {
            get { return base._Event; }
            set { base._Event = value; }
        }
        /// <summary>
        /// Gets or Sets Source
        /// </summary>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public new TurnEventNodeSource Source { get; protected set; }
    }

}
