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
    /// MessageOutputDebugTurnEventTurnEventActionFinished.
    /// </summary>
    public class MessageOutputDebugTurnEventTurnEventActionFinished : MessageOutputDebugTurnEvent
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
        /// The reason the action finished processing.
        /// </summary>
        public class ReasonEnumValue
        {
            /// <summary>
            /// Constant ALL_STEPS_DONE for all_steps_done
            /// </summary>
            public const string ALL_STEPS_DONE = "all_steps_done";
            /// <summary>
            /// Constant NO_STEPS_VISITED for no_steps_visited
            /// </summary>
            public const string NO_STEPS_VISITED = "no_steps_visited";
            /// <summary>
            /// Constant ENDED_BY_STEP for ended_by_step
            /// </summary>
            public const string ENDED_BY_STEP = "ended_by_step";
            /// <summary>
            /// Constant CONNECT_TO_AGENT for connect_to_agent
            /// </summary>
            public const string CONNECT_TO_AGENT = "connect_to_agent";
            /// <summary>
            /// Constant MAX_RETRIES_REACHED for max_retries_reached
            /// </summary>
            public const string MAX_RETRIES_REACHED = "max_retries_reached";
            /// <summary>
            /// Constant FALLBACK for fallback
            /// </summary>
            public const string FALLBACK = "fallback";
            
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
        public new TurnEventActionSource Source
        {
            get { return base.Source; }
            set { base.Source = value; }
        }
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
        /// The state of all action variables at the time the action finished.
        /// </summary>
        [JsonProperty("action_variables", NullValueHandling = NullValueHandling.Ignore)]
        public new Dictionary<string, object> ActionVariables
        {
            get { return base.ActionVariables; }
            set { base.ActionVariables = value; }
        }
    }

}
