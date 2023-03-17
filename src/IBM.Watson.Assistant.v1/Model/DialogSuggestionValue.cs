/**
* (C) Copyright IBM Corp. 2018, 2023.
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

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// An object defining the message input, intents, and entities to be sent to the Watson Assistant service if the
    /// user selects the corresponding disambiguation option.
    ///
    ///  **Note:** These properties must be included in the request body of the next message sent to the assistant. Do
    /// not modify or remove any of the included properties.
    /// </summary>
    public class DialogSuggestionValue
    {
        /// <summary>
        /// An input object that includes the input text.
        /// </summary>
        [JsonProperty("input", NullValueHandling = NullValueHandling.Ignore)]
        public MessageInput Input { get; set; }
        /// <summary>
        /// An array of intents to be sent along with the user input.
        /// </summary>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeIntent> Intents { get; set; }
        /// <summary>
        /// An array of entities to be sent along with the user input.
        /// </summary>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeEntity> Entities { get; set; }
    }

}
