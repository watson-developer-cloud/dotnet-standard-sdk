/**
* (C) Copyright IBM Corp. 2022, 2023.
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
using JsonSubTypes;
using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// MessageOutputDebugTurnEvent.
    /// Classes which extend this class:
    /// - MessageOutputDebugTurnEventTurnEventActionVisited
    /// - MessageOutputDebugTurnEventTurnEventActionFinished
    /// - MessageOutputDebugTurnEventTurnEventStepVisited
    /// - MessageOutputDebugTurnEventTurnEventStepAnswered
    /// - MessageOutputDebugTurnEventTurnEventHandlerVisited
    /// - MessageOutputDebugTurnEventTurnEventCallout
    /// - MessageOutputDebugTurnEventTurnEventSearch
    /// - MessageOutputDebugTurnEventTurnEventNodeVisited
    /// </summary>
    [JsonConverter(typeof(JsonSubtypes), "event")]
    [JsonSubtypes.KnownSubType(typeof(MessageOutputDebugTurnEventTurnEventActionVisited), "action_visited")]
    [JsonSubtypes.KnownSubType(typeof(MessageOutputDebugTurnEventTurnEventActionFinished), "action_finished")]
    [JsonSubtypes.KnownSubType(typeof(MessageOutputDebugTurnEventTurnEventStepVisited), "step_visited")]
    [JsonSubtypes.KnownSubType(typeof(MessageOutputDebugTurnEventTurnEventStepAnswered), "step_answered")]
    [JsonSubtypes.KnownSubType(typeof(MessageOutputDebugTurnEventTurnEventHandlerVisited), "handler_visited")]
    [JsonSubtypes.KnownSubType(typeof(MessageOutputDebugTurnEventTurnEventCallout), "callout")]
    [JsonSubtypes.KnownSubType(typeof(MessageOutputDebugTurnEventTurnEventSearch), "search")]
    [JsonSubtypes.KnownSubType(typeof(MessageOutputDebugTurnEventTurnEventNodeVisited), "node_visited")]
    public class MessageOutputDebugTurnEvent
    {
        /// This ctor is protected to prevent instantiation of this base class.
        /// Instead, users should instantiate one of the subclasses listed below:
        /// - MessageOutputDebugTurnEventTurnEventActionVisited
        /// - MessageOutputDebugTurnEventTurnEventActionFinished
        /// - MessageOutputDebugTurnEventTurnEventStepVisited
        /// - MessageOutputDebugTurnEventTurnEventStepAnswered
        /// - MessageOutputDebugTurnEventTurnEventHandlerVisited
        /// - MessageOutputDebugTurnEventTurnEventCallout
        /// - MessageOutputDebugTurnEventTurnEventSearch
        /// - MessageOutputDebugTurnEventTurnEventNodeVisited
        protected MessageOutputDebugTurnEvent()
        {
        }

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
        /// The reason the action was visited.
        /// </summary>
        public class ReasonEnumValue
        {
            /// <summary>
            /// Constant INTENT for intent
            /// </summary>
            public const string INTENT = "intent";
            /// <summary>
            /// Constant INVOKE_SUBACTION for invoke_subaction
            /// </summary>
            public const string INVOKE_SUBACTION = "invoke_subaction";
            /// <summary>
            /// Constant SUBACTION_RETURN for subaction_return
            /// </summary>
            public const string SUBACTION_RETURN = "subaction_return";
            /// <summary>
            /// Constant INVOKE_EXTERNAL for invoke_external
            /// </summary>
            public const string INVOKE_EXTERNAL = "invoke_external";
            /// <summary>
            /// Constant TOPIC_SWITCH for topic_switch
            /// </summary>
            public const string TOPIC_SWITCH = "topic_switch";
            /// <summary>
            /// Constant TOPIC_RETURN for topic_return
            /// </summary>
            public const string TOPIC_RETURN = "topic_return";
            /// <summary>
            /// Constant AGENT_REQUESTED for agent_requested
            /// </summary>
            public const string AGENT_REQUESTED = "agent_requested";
            /// <summary>
            /// Constant STEP_VALIDATION_FAILED for step_validation_failed
            /// </summary>
            public const string STEP_VALIDATION_FAILED = "step_validation_failed";
            /// <summary>
            /// Constant NO_ACTION_MATCHES for no_action_matches
            /// </summary>
            public const string NO_ACTION_MATCHES = "no_action_matches";
            
        }

        /// <summary>
        /// The type of condition (if any) that is defined for the action.
        /// Constants for possible values can be found using MessageOutputDebugTurnEvent.ConditionTypeEnumValue
        /// </summary>
        [JsonProperty("condition_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ConditionType { get; set; }
        /// <summary>
        /// The reason the action was visited.
        /// Constants for possible values can be found using MessageOutputDebugTurnEvent.ReasonEnumValue
        /// </summary>
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }
        /// <summary>
        /// The type of turn event.
        /// </summary>
        [JsonProperty("event", NullValueHandling = NullValueHandling.Ignore)]
        public string _Event { get; protected set; }
        /// <summary>
        /// The time when the action started processing the message.
        /// </summary>
        [JsonProperty("action_start_time", NullValueHandling = NullValueHandling.Ignore)]
        public string ActionStartTime { get; protected set; }
        /// <summary>
        /// The variable where the result of the call to the action is stored. Included only if
        /// **reason**=`subaction_return`.
        /// </summary>
        [JsonProperty("result_variable", NullValueHandling = NullValueHandling.Ignore)]
        public string ResultVariable { get; protected set; }
        /// <summary>
        /// The state of all action variables at the time the action finished.
        /// </summary>
        [JsonProperty("action_variables", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> ActionVariables { get; protected set; }
        /// <summary>
        /// Whether the step collects a customer response.
        /// </summary>
        [JsonProperty("has_question", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasQuestion { get; protected set; }
        /// <summary>
        /// Whether the step was answered in response to a prompt from the assistant. If this property is `false`, the
        /// user provided the answer without visiting the step.
        /// </summary>
        [JsonProperty("prompted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Prompted { get; protected set; }
        /// <summary>
        /// Gets or Sets Callout
        /// </summary>
        [JsonProperty("callout", NullValueHandling = NullValueHandling.Ignore)]
        public TurnEventCalloutCallout Callout { get; protected set; }
    }

}
