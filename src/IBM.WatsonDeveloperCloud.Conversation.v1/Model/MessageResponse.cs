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

using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// Returns the last user input, the recognized intents and entities, and the updated context and system output. 
    /// The response can include properties that are added by dialog node output or by the client app.
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// The user input from the request.
        /// </summary>
        [JsonProperty("input")]
        public dynamic Input { get; set; }

        /// <summary>
        /// Whether to return more than one intent. Included in the response only.
        /// </summary>
        [JsonProperty("alternate_intents")]
        public bool AlternateIntents { get; set; }

        /// <summary>
        /// A context object that includes state information for the conversation.
        /// </summary>
        [JsonProperty("context")]
        public dynamic Context { get; set; }

        /// <summary>
        /// An entities object that includes terms from the request that are identified as entities. Returns an empty array 
        /// if no entities are returned.
        /// </summary>
        [JsonProperty("entities")]
        public List<EntityResponse> Entities { get; set; }

        /// <summary>
        /// An array of intent name-confidence pairs for the user input. The list is sorted in descending order of confidence. 
        /// If there are 10 or fewer intents, the sum of the confidence values is 100%. Returns an empty array if no intents are returned. 
        /// </summary>
        [JsonProperty("intents")]
        public List<Intent> Intents { get; set; }

        /// <summary>
        /// An output object that includes the response to the user, the nodes that were hit, and messages from the log.
        /// </summary>
        [JsonProperty("output")]
        public dynamic Output { get; set; }
    }
}
