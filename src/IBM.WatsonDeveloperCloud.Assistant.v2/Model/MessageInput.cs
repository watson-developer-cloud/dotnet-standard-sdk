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

namespace IBM.WatsonDeveloperCloud.Assistant.v2.Model
{
    /// <summary>
    /// The user input.
    /// </summary>
    public class MessageInput : BaseModel
    {
        /// <summary>
        /// The type of user input. Currently, only text input is supported.
        /// </summary>
        /// <value>
        /// The type of user input. Currently, only text input is supported.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum MessageTypeEnum
        {
            
            /// <summary>
            /// Enum TEXT for text
            /// </summary>
            [EnumMember(Value = "text")]
            TEXT
        }

        /// <summary>
        /// The type of user input. Currently, only text input is supported.
        /// </summary>
        [JsonProperty("message_type", NullValueHandling = NullValueHandling.Ignore)]
        public MessageTypeEnum? MessageType { get; set; }
        /// <summary>
        /// The text of the user input. This string cannot contain carriage return, newline, or tab characters, and it
        /// must be no longer than 2048 characters.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// Properties that control how the assistant responds.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public MessageInputOptions Options { get; set; }
        /// <summary>
        /// Intents to use when evaluating the user input. Include intents from the previous response to continue using
        /// those intents rather than trying to recognize intents in the new input.
        /// </summary>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeIntent> Intents { get; set; }
        /// <summary>
        /// Entities to use when evaluating the message. Include entities from the previous response to continue using
        /// those entities rather than detecting entities in the new input.
        /// </summary>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeEntity> Entities { get; set; }
        /// <summary>
        /// For internal use only.
        /// </summary>
        [JsonProperty("suggestion_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SuggestionId { get; set; }
    }

}
