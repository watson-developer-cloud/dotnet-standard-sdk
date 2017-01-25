/**
* Copyright 2015 IBM Corp. All Rights Reserved.
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
    /// The user's input, with optional intents, entities, and other properties from the response.
    /// </summary>
    public class MessageRequest
    {
        /// <summary>
        /// An input object that includes the input text.
        /// </summary>
        [JsonProperty("input", NullValueHandling = NullValueHandling.Ignore)]
        public InputData Input { get; set; }

        /// <summary>
        ///Whether to return more than one intent. Default is false. 
        ///Set to true to return all matching intents.For example, return all intents when the confidence is not high 
        ///to allow users to choose their intent.
        /// </summary>
        [JsonProperty("alternate_intents", NullValueHandling = NullValueHandling.Ignore)]
        public bool AlternateIntents { get; set; }

        /// <summary>
        /// State information for the conversation. When you send multiple requests for the same conversation, 
        /// include the context object from the response. 
        /// </summary>
        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public Context Context { get; set; }

        /// <summary>
        /// The portion of the user's input that you can use to provide a different response or action to an intent. 
        /// Include entities from the request when they do not need to change so that Watson does not try to identify them. 
        /// </summary>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<EntityResponse> Entities { get; set; }

        /// <summary>
        /// An array of name-confidence pairs for the user input. 
        /// Include the intents from the request when they do not need to change so that Watson does not try to identify them. 
        /// </summary>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<Intent> Intents { get; set; }

        /// <summary>
        /// System output. Include the output from the request when you have 
        /// several requests within the same Dialog turn to pass back in the intermediate information.
        /// </summary>
        [JsonProperty("output", NullValueHandling = NullValueHandling.Ignore)]
        public OutputData Output { get; set; }
    }
}
