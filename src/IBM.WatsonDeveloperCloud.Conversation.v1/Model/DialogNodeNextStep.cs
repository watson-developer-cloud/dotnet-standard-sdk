/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// The next step to execute following this dialog node.
    /// </summary>
    public class DialogNodeNextStep
    {
        /// <summary>
        /// How the `next_step` reference is processed.
        /// </summary>
        /// <value>How the `next_step` reference is processed.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum BehaviorEnum
        {
            
            /// <summary>
            /// Enum JUMP_TO for jump_to
            /// </summary>
            [EnumMember(Value = "jump_to")]
            JUMP_TO
        }

        /// <summary>
        /// Which part of the dialog node to process next.
        /// </summary>
        /// <value>Which part of the dialog node to process next.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SelectorEnum
        {
            
            /// <summary>
            /// Enum CONDITION for condition
            /// </summary>
            [EnumMember(Value = "condition")]
            CONDITION,
            
            /// <summary>
            /// Enum CLIENT for client
            /// </summary>
            [EnumMember(Value = "client")]
            CLIENT,
            
            /// <summary>
            /// Enum USER_INPUT for user_input
            /// </summary>
            [EnumMember(Value = "user_input")]
            USER_INPUT,
            
            /// <summary>
            /// Enum BODY for body
            /// </summary>
            [EnumMember(Value = "body")]
            BODY
        }

        /// <summary>
        /// How the `next_step` reference is processed.
        /// </summary>
        /// <value>How the `next_step` reference is processed.</value>
        [JsonProperty("behavior", NullValueHandling = NullValueHandling.Ignore)]
        public BehaviorEnum? Behavior { get; set; }
        /// <summary>
        /// Which part of the dialog node to process next.
        /// </summary>
        /// <value>Which part of the dialog node to process next.</value>
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public SelectorEnum? Selector { get; set; }
        /// <summary>
        /// The ID of the dialog node to process next.
        /// </summary>
        /// <value>The ID of the dialog node to process next.</value>
        [JsonProperty("dialog_node", NullValueHandling = NullValueHandling.Ignore)]
        public string DialogNode { get; set; }
    }

}
