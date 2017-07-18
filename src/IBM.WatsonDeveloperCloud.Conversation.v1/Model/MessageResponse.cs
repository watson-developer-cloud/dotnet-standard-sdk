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
    /// A response from the Conversation service.
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// The user input from the request.
        /// </summary>
        /// <value>The user input from the request.</value>
        [JsonProperty("input", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Input { get; set; }
        /// <summary>
        /// An array of intents recognized in the user input, sorted in descending order of confidence.
        /// </summary>
        /// <value>An array of intents recognized in the user input, sorted in descending order of confidence.</value>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeIntent> Intents { get; set; }
        /// <summary>
        /// An array of entities identified in the user input.
        /// </summary>
        /// <value>An array of entities identified in the user input.</value>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeEntity> Entities { get; set; }
        /// <summary>
        /// Whether to return more than one intent. `true` indicates that all matching intents are returned.
        /// </summary>
        /// <value>Whether to return more than one intent. `true` indicates that all matching intents are returned.</value>
        [JsonProperty("alternate_intents", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AlternateIntents { get; set; }
        /// <summary>
        /// State information for the conversation.
        /// </summary>
        /// <value>State information for the conversation.</value>
        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Context { get; set; }
        /// <summary>
        /// Output from the dialog, including the response to the user, the nodes that were triggered, and log messages.
        /// </summary>
        /// <value>Output from the dialog, including the response to the user, the nodes that were triggered, and log messages.</value>
        [JsonProperty("output", NullValueHandling = NullValueHandling.Ignore)]
        public RuntimeOutput Output { get; set; }
    }

}
