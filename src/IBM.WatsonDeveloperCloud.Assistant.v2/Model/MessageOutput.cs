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

namespace IBM.WatsonDeveloperCloud.Assistant.v2.Model
{
    /// <summary>
    /// Assistant output to be rendered or processed by the client.
    /// </summary>
    public class MessageOutput : BaseModel
    {
        /// <summary>
        /// Output intended for any channel. It is the responsibility of the client application to implement the
        /// supported response types.
        /// </summary>
        [JsonProperty("generic", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogRuntimeResponseGeneric> Generic { get; set; }
        /// <summary>
        /// An array of intents recognized in the user input, sorted in descending order of confidence.
        /// </summary>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeIntent> Intents { get; set; }
        /// <summary>
        /// An array of entities identified in the user input.
        /// </summary>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeEntity> Entities { get; set; }
        /// <summary>
        /// An array of objects describing any actions requested by the dialog node.
        /// </summary>
        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNodeAction> Actions { get; set; }
        /// <summary>
        /// Additional detailed information about a message response and how it was generated.
        /// </summary>
        [JsonProperty("debug", NullValueHandling = NullValueHandling.Ignore)]
        public MessageOutputDebug Debug { get; set; }
    }

}
