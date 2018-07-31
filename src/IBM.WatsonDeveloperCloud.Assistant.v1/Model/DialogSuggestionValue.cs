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
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Assistant.v1.Model
{
    /// <summary>
    /// An object defining the message input, intents, and entities to be sent to the Watson Assistant service if the
    /// user selects the corresponding disambiguation option.
    /// </summary>
    public class DialogSuggestionValue : BaseModel
    {
        /// <summary>
        /// The user input.
        /// </summary>
        /// <value>
        /// The user input.
        /// </value>
        [JsonProperty("input", NullValueHandling = NullValueHandling.Ignore)]
        public InputData Input { get; set; }
        /// <summary>
        /// An array of intents to be sent along with the user input.
        /// </summary>
        /// <value>
        /// An array of intents to be sent along with the user input.
        /// </value>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeIntent> Intents { get; set; }
        /// <summary>
        /// An array of entities to be sent along with the user input.
        /// </summary>
        /// <value>
        /// An array of entities to be sent along with the user input.
        /// </value>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeEntity> Entities { get; set; }
    }

}
