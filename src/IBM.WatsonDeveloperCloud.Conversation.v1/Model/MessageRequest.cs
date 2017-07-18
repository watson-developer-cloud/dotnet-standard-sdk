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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// A request formatted for the Conversation service.
    /// </summary>
    public class MessageRequest
    {
        /// <summary>
        /// An input object that includes the input text.
        /// </summary>
        /// <value>An input object that includes the input text.</value>
        [JsonProperty("input", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Input { get; set; }
        /// <summary>
        /// Whether to return more than one intent. Set to `true` to return all matching intents.
        /// </summary>
        /// <value>Whether to return more than one intent. Set to `true` to return all matching intents.</value>
        [JsonProperty("alternate_intents", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AlternateIntents { get; set; }
        /// <summary>
        /// State information for the conversation. Continue a conversation by including the context object from the previous response.
        /// </summary>
        /// <value>State information for the conversation. Continue a conversation by including the context object from the previous response.</value>
        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Context { get; set; }
        /// <summary>
        /// Include the entities from the previous response when they do not need to change and to prevent Watson from trying to identify them.
        /// </summary>
        /// <value>Include the entities from the previous response when they do not need to change and to prevent Watson from trying to identify them.</value>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeEntity> Entities { get; set; }
        /// <summary>
        /// An array of name-confidence pairs for the user input. Include the intents from the previous response when they do not need to change and to prevent Watson from trying to identify them.
        /// </summary>
        /// <value>An array of name-confidence pairs for the user input. Include the intents from the previous response when they do not need to change and to prevent Watson from trying to identify them.</value>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeIntent> Intents { get; set; }
        /// <summary>
        /// System output. Include the output from the request when you have several requests within the same Dialog turn to pass back in the intermediate information.
        /// </summary>
        /// <value>System output. Include the output from the request when you have several requests within the same Dialog turn to pass back in the intermediate information.</value>
        [JsonProperty("output", NullValueHandling = NullValueHandling.Ignore)]
        public OutputData Output { get; set; }
    }

}
