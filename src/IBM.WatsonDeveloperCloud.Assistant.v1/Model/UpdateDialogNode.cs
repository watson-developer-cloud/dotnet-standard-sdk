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

namespace IBM.WatsonDeveloperCloud.Assistant.v1.Model
{
    /// <summary>
    /// UpdateDialogNode.
    /// </summary>
    public class UpdateDialogNode : BaseModel
    {
        /// <summary>
        /// How the dialog node is processed.
        /// </summary>
        /// <value>
        /// How the dialog node is processed.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum NodeTypeEnum
        {
            
            /// <summary>
            /// Enum STANDARD for standard
            /// </summary>
            [EnumMember(Value = "standard")]
            STANDARD,
            
            /// <summary>
            /// Enum EVENT_HANDLER for event_handler
            /// </summary>
            [EnumMember(Value = "event_handler")]
            EVENT_HANDLER,
            
            /// <summary>
            /// Enum FRAME for frame
            /// </summary>
            [EnumMember(Value = "frame")]
            FRAME,
            
            /// <summary>
            /// Enum SLOT for slot
            /// </summary>
            [EnumMember(Value = "slot")]
            SLOT,
            
            /// <summary>
            /// Enum RESPONSE_CONDITION for response_condition
            /// </summary>
            [EnumMember(Value = "response_condition")]
            RESPONSE_CONDITION,
            
            /// <summary>
            /// Enum FOLDER for folder
            /// </summary>
            [EnumMember(Value = "folder")]
            FOLDER
        }

        /// <summary>
        /// How an `event_handler` node is processed.
        /// </summary>
        /// <value>
        /// How an `event_handler` node is processed.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EventNameEnum
        {
            
            /// <summary>
            /// Enum FOCUS for focus
            /// </summary>
            [EnumMember(Value = "focus")]
            FOCUS,
            
            /// <summary>
            /// Enum INPUT for input
            /// </summary>
            [EnumMember(Value = "input")]
            INPUT,
            
            /// <summary>
            /// Enum FILLED for filled
            /// </summary>
            [EnumMember(Value = "filled")]
            FILLED,
            
            /// <summary>
            /// Enum VALIDATE for validate
            /// </summary>
            [EnumMember(Value = "validate")]
            VALIDATE,
            
            /// <summary>
            /// Enum FILLED_MULTIPLE for filled_multiple
            /// </summary>
            [EnumMember(Value = "filled_multiple")]
            FILLED_MULTIPLE,
            
            /// <summary>
            /// Enum GENERIC for generic
            /// </summary>
            [EnumMember(Value = "generic")]
            GENERIC,
            
            /// <summary>
            /// Enum NOMATCH for nomatch
            /// </summary>
            [EnumMember(Value = "nomatch")]
            NOMATCH,
            
            /// <summary>
            /// Enum NOMATCH_RESPONSES_DEPLETED for nomatch_responses_depleted
            /// </summary>
            [EnumMember(Value = "nomatch_responses_depleted")]
            NOMATCH_RESPONSES_DEPLETED,
            
            /// <summary>
            /// Enum DIGRESSION_RETURN_PROMPT for digression_return_prompt
            /// </summary>
            [EnumMember(Value = "digression_return_prompt")]
            DIGRESSION_RETURN_PROMPT
        }

        /// <summary>
        /// Whether this top-level dialog node can be digressed into.
        /// </summary>
        /// <value>
        /// Whether this top-level dialog node can be digressed into.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DigressInEnum
        {
            
            /// <summary>
            /// Enum NOT_AVAILABLE for not_available
            /// </summary>
            [EnumMember(Value = "not_available")]
            NOT_AVAILABLE,
            
            /// <summary>
            /// Enum RETURNS for returns
            /// </summary>
            [EnumMember(Value = "returns")]
            RETURNS,
            
            /// <summary>
            /// Enum DOES_NOT_RETURN for does_not_return
            /// </summary>
            [EnumMember(Value = "does_not_return")]
            DOES_NOT_RETURN
        }

        /// <summary>
        /// Whether this dialog node can be returned to after a digression.
        /// </summary>
        /// <value>
        /// Whether this dialog node can be returned to after a digression.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DigressOutEnum
        {
            
            /// <summary>
            /// Enum ALLOW_RETURNING for allow_returning
            /// </summary>
            [EnumMember(Value = "allow_returning")]
            ALLOW_RETURNING,
            
            /// <summary>
            /// Enum ALLOW_ALL for allow_all
            /// </summary>
            [EnumMember(Value = "allow_all")]
            ALLOW_ALL,
            
            /// <summary>
            /// Enum ALLOW_ALL_NEVER_RETURN for allow_all_never_return
            /// </summary>
            [EnumMember(Value = "allow_all_never_return")]
            ALLOW_ALL_NEVER_RETURN
        }

        /// <summary>
        /// Whether the user can digress to top-level nodes while filling out slots.
        /// </summary>
        /// <value>
        /// Whether the user can digress to top-level nodes while filling out slots.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DigressOutSlotsEnum
        {
            
            /// <summary>
            /// Enum NOT_ALLOWED for not_allowed
            /// </summary>
            [EnumMember(Value = "not_allowed")]
            NOT_ALLOWED,
            
            /// <summary>
            /// Enum ALLOW_RETURNING for allow_returning
            /// </summary>
            [EnumMember(Value = "allow_returning")]
            ALLOW_RETURNING,
            
            /// <summary>
            /// Enum ALLOW_ALL for allow_all
            /// </summary>
            [EnumMember(Value = "allow_all")]
            ALLOW_ALL
        }

        /// <summary>
        /// How the dialog node is processed.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public NodeTypeEnum? NodeType { get; set; }
        /// <summary>
        /// How an `event_handler` node is processed.
        /// </summary>
        [JsonProperty("event_name", NullValueHandling = NullValueHandling.Ignore)]
        public EventNameEnum? EventName { get; set; }
        /// <summary>
        /// Whether this top-level dialog node can be digressed into.
        /// </summary>
        [JsonProperty("digress_in", NullValueHandling = NullValueHandling.Ignore)]
        public DigressInEnum? DigressIn { get; set; }
        /// <summary>
        /// Whether this dialog node can be returned to after a digression.
        /// </summary>
        [JsonProperty("digress_out", NullValueHandling = NullValueHandling.Ignore)]
        public DigressOutEnum? DigressOut { get; set; }
        /// <summary>
        /// Whether the user can digress to top-level nodes while filling out slots.
        /// </summary>
        [JsonProperty("digress_out_slots", NullValueHandling = NullValueHandling.Ignore)]
        public DigressOutSlotsEnum? DigressOutSlots { get; set; }
        /// <summary>
        /// The dialog node ID. This string must conform to the following restrictions:
        /// - It can contain only Unicode alphanumeric, space, underscore, hyphen, and dot characters.
        /// - It must be no longer than 1024 characters.
        /// </summary>
        [JsonProperty("dialog_node", NullValueHandling = NullValueHandling.Ignore)]
        public string DialogNode { get; set; }
        /// <summary>
        /// The description of the dialog node. This string cannot contain carriage return, newline, or tab characters,
        /// and it must be no longer than 128 characters.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The condition that will trigger the dialog node. This string cannot contain carriage return, newline, or tab
        /// characters, and it must be no longer than 2048 characters.
        /// </summary>
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public string Conditions { get; set; }
        /// <summary>
        /// The ID of the parent dialog node.
        /// </summary>
        [JsonProperty("parent", NullValueHandling = NullValueHandling.Ignore)]
        public string Parent { get; set; }
        /// <summary>
        /// The ID of the previous sibling dialog node.
        /// </summary>
        [JsonProperty("previous_sibling", NullValueHandling = NullValueHandling.Ignore)]
        public string PreviousSibling { get; set; }
        /// <summary>
        /// The output of the dialog node. For more information about how to specify dialog node output, see the
        /// [documentation](https://console.bluemix.net/docs/services/conversation/dialog-overview.html#complex).
        /// </summary>
        [JsonProperty("output", NullValueHandling = NullValueHandling.Ignore)]
        public DialogNodeOutput Output { get; set; }
        /// <summary>
        /// The context for the dialog node.
        /// </summary>
        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public object Context { get; set; }
        /// <summary>
        /// The metadata for the dialog node.
        /// </summary>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object Metadata { get; set; }
        /// <summary>
        /// The next step to be executed in dialog processing.
        /// </summary>
        [JsonProperty("next_step", NullValueHandling = NullValueHandling.Ignore)]
        public DialogNodeNextStep NextStep { get; set; }
        /// <summary>
        /// The alias used to identify the dialog node. This string must conform to the following restrictions:
        /// - It can contain only Unicode alphanumeric, space, underscore, hyphen, and dot characters.
        /// - It must be no longer than 64 characters.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        /// <summary>
        /// The location in the dialog context where output is stored.
        /// </summary>
        [JsonProperty("variable", NullValueHandling = NullValueHandling.Ignore)]
        public string Variable { get; set; }
        /// <summary>
        /// An array of objects describing any actions to be invoked by the dialog node.
        /// </summary>
        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNodeAction> Actions { get; set; }
        /// <summary>
        /// A label that can be displayed externally to describe the purpose of the node to users. This string must be
        /// no longer than 512 characters.
        /// </summary>
        [JsonProperty("user_label", NullValueHandling = NullValueHandling.Ignore)]
        public string UserLabel { get; set; }
    }

}
