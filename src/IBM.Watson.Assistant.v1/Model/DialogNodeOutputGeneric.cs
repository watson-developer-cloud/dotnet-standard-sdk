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
using IBM.Cloud.SDK.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// DialogNodeOutputGeneric.
    /// </summary>
    public class DialogNodeOutputGeneric : BaseModel
    {
        /// <summary>
        /// The type of response returned by the dialog node. The specified response type must be supported by the
        /// client application or channel.
        /// </summary>
        /// <value>
        /// The type of response returned by the dialog node. The specified response type must be supported by the
        /// client application or channel.
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
            CONNECT_TO_AGENT
        }

        /// <summary>
        /// How a response is selected from the list, if more than one response is specified. Valid only when
        /// **response_type**=`text`.
        /// </summary>
        /// <value>
        /// How a response is selected from the list, if more than one response is specified. Valid only when
        /// **response_type**=`text`.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SelectionPolicyEnum
        {
            
            /// <summary>
            /// Enum SEQUENTIAL for sequential
            /// </summary>
            [EnumMember(Value = "sequential")]
            SEQUENTIAL,
            
            /// <summary>
            /// Enum RANDOM for random
            /// </summary>
            [EnumMember(Value = "random")]
            RANDOM,
            
            /// <summary>
            /// Enum MULTILINE for multiline
            /// </summary>
            [EnumMember(Value = "multiline")]
            MULTILINE
        }

        /// <summary>
        /// The preferred type of control to display, if supported by the channel. Valid only when
        /// **response_type**=`option`.
        /// </summary>
        /// <value>
        /// The preferred type of control to display, if supported by the channel. Valid only when
        /// **response_type**=`option`.
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
        /// </summary>
        [JsonProperty("response_type", NullValueHandling = NullValueHandling.Ignore)]
        public ResponseTypeEnum? ResponseType { get; set; }
        /// <summary>
        /// How a response is selected from the list, if more than one response is specified. Valid only when
        /// **response_type**=`text`.
        /// </summary>
        [JsonProperty("selection_policy", NullValueHandling = NullValueHandling.Ignore)]
        public SelectionPolicyEnum? SelectionPolicy { get; set; }
        /// <summary>
        /// The preferred type of control to display, if supported by the channel. Valid only when
        /// **response_type**=`option`.
        /// </summary>
        [JsonProperty("preference", NullValueHandling = NullValueHandling.Ignore)]
        public PreferenceEnum? Preference { get; set; }
        /// <summary>
        /// A list of one or more objects defining text responses. Required when **response_type**=`text`.
        /// </summary>
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNodeOutputTextValuesElement> Values { get; set; }
        /// <summary>
        /// The delimiter to use as a separator between responses when `selection_policy`=`multiline`.
        /// </summary>
        [JsonProperty("delimiter", NullValueHandling = NullValueHandling.Ignore)]
        public string Delimiter { get; set; }
        /// <summary>
        /// How long to pause, in milliseconds. The valid values are from 0 to 10000. Valid only when
        /// **response_type**=`pause`.
        /// </summary>
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public long? Time { get; set; }
        /// <summary>
        /// Whether to send a \"user is typing\" event during the pause. Ignored if the channel does not support this
        /// event. Valid only when **response_type**=`pause`.
        /// </summary>
        [JsonProperty("typing", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Typing { get; set; }
        /// <summary>
        /// The URL of the image. Required when **response_type**=`image`.
        /// </summary>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        /// <summary>
        /// An optional title to show before the response. Valid only when **response_type**=`image` or `option`. This
        /// string must be no longer than 512 characters.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        /// <summary>
        /// An optional description to show with the response. Valid only when **response_type**=`image` or `option`.
        /// This string must be no longer than 256 characters.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// An array of objects describing the options from which the user can choose. You can include up to 20 options.
        /// Required when **response_type**=`option`.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNodeOutputOptionsElement> Options { get; set; }
        /// <summary>
        /// An optional message to be sent to the human agent who will be taking over the conversation. Valid only when
        /// **reponse_type**=`connect_to_agent`. This string must be no longer than 256 characters.
        /// </summary>
        [JsonProperty("message_to_human_agent", NullValueHandling = NullValueHandling.Ignore)]
        public string MessageToHumanAgent { get; set; }
    }

}
