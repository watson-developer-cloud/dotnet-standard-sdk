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
    /// DialogRuntimeResponseGeneric.
    /// </summary>
    public class DialogRuntimeResponseGeneric : BaseModel
    {
        /// <summary>
        /// The type of response returned by the dialog node. The specified response type must be supported by the
        /// client application or channel.
        ///
        /// **Note:** The **suggestion** response type is part of the disambiguation feature, which is only available
        /// for Premium users.
        /// </summary>
        /// <value>
        /// The type of response returned by the dialog node. The specified response type must be supported by the
        /// client application or channel.
        ///
        /// **Note:** The **suggestion** response type is part of the disambiguation feature, which is only available
        /// for Premium users.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ResponseTypeEnum
        {
            
            /// <summary>
            /// Enum TEXT for text
            /// </summary>
            [EnumMember(Value = "text")]
            TEXT,
            
            /// <summary>
            /// Enum PAUSE for pause
            /// </summary>
            [EnumMember(Value = "pause")]
            PAUSE,
            
            /// <summary>
            /// Enum IMAGE for image
            /// </summary>
            [EnumMember(Value = "image")]
            IMAGE,
            
            /// <summary>
            /// Enum OPTION for option
            /// </summary>
            [EnumMember(Value = "option")]
            OPTION,
            
            /// <summary>
            /// Enum CONNECT_TO_AGENT for connect_to_agent
            /// </summary>
            [EnumMember(Value = "connect_to_agent")]
            CONNECT_TO_AGENT,
            
            /// <summary>
            /// Enum SUGGESTION for suggestion
            /// </summary>
            [EnumMember(Value = "suggestion")]
            SUGGESTION
        }

        /// <summary>
        /// The preferred type of control to display.
        /// </summary>
        /// <value>
        /// The preferred type of control to display.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PreferenceEnum
        {
            
            /// <summary>
            /// Enum DROPDOWN for dropdown
            /// </summary>
            [EnumMember(Value = "dropdown")]
            DROPDOWN,
            
            /// <summary>
            /// Enum BUTTON for button
            /// </summary>
            [EnumMember(Value = "button")]
            BUTTON
        }

        /// <summary>
        /// The type of response returned by the dialog node. The specified response type must be supported by the
        /// client application or channel.
        ///
        /// **Note:** The **suggestion** response type is part of the disambiguation feature, which is only available
        /// for Premium users.
        /// </summary>
        [JsonProperty("response_type", NullValueHandling = NullValueHandling.Ignore)]
        public ResponseTypeEnum? ResponseType { get; set; }
        /// <summary>
        /// The preferred type of control to display.
        /// </summary>
        [JsonProperty("preference", NullValueHandling = NullValueHandling.Ignore)]
        public PreferenceEnum? Preference { get; set; }
        /// <summary>
        /// The text of the response.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// How long to pause, in milliseconds.
        /// </summary>
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public long? Time { get; set; }
        /// <summary>
        /// Whether to send a \"user is typing\" event during the pause.
        /// </summary>
        [JsonProperty("typing", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Typing { get; set; }
        /// <summary>
        /// The URL of the image.
        /// </summary>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        /// <summary>
        /// The title or introductory text to show before the response.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        /// <summary>
        /// The description to show with the the response.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// An array of objects describing the options from which the user can choose.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNodeOutputOptionsElement> Options { get; set; }
        /// <summary>
        /// A message to be sent to the human agent who will be taking over the conversation.
        /// </summary>
        [JsonProperty("message_to_human_agent", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageToHumanAgent { get; set; }
        /// <summary>
        /// A label identifying the topic of the conversation, derived from the **user_label** property of the relevant
        /// node.
        /// </summary>
        [JsonProperty("topic", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Topic { get; private set; }
        /// <summary>
        /// An array of objects describing the possible matching dialog nodes from which the user can choose.
        ///
        /// **Note:** The **suggestions** property is part of the disambiguation feature, which is only available for
        /// Premium users.
        /// </summary>
        [JsonProperty("suggestions", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogSuggestion> Suggestions { get; set; }
    }

}
