/**
* (C) Copyright IBM Corp. 2023.
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
    /// MessageOutputDebugTurnEventTurnEventStepVisited.
    /// </summary>
    public class MessageOutputDebugTurnEventTurnEventStepVisited : MessageOutputDebugTurnEvent
    {
        /// <summary>
        /// The type of condition (if any) that is defined for the action.
        /// </summary>
        public class ConditionTypeEnumValue
        {
            /// <summary>
            /// Constant USER_DEFINED for user_defined
            /// </summary>
            public const string USER_DEFINED = "user_defined";
            /// <summary>
            /// Constant WELCOME for welcome
            /// </summary>
            public const string WELCOME = "welcome";
            /// <summary>
            /// Constant ANYTHING_ELSE for anything_else
            /// </summary>
            public const string ANYTHING_ELSE = "anything_else";
            
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
        public new TurnEventActionSource Source { get; protected set; }
        /// <summary>
        /// The time when the action started processing the message.
        /// </summary>
        [JsonProperty("action_start_time", NullValueHandling = NullValueHandling.Ignore)]
        public new string ActionStartTime
        {
            get { return base.ActionStartTime; }
            set { base.ActionStartTime = value; }
        }
        /// <summary>
        /// Whether the step collects a customer response.
        /// </summary>
        [JsonProperty("has_question", NullValueHandling = NullValueHandling.Ignore)]
        public new bool? HasQuestion
        {
            get { return base.HasQuestion; }
            set { base.HasQuestion = value; }
        }
    }

}
