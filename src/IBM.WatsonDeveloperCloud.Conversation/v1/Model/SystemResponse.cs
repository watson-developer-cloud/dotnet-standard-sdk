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
    /// Information about the dialog.
    /// </summary>
    public class SystemResponse
    {
        /// <summary>
        /// An array of dialog node IDs that are in focus in the conversation.
        /// </summary>
        [JsonProperty("dialog_stack")]
        public List<DialogStack> DialogStack { get; set; }

        /// <summary>
        /// The number of cycles of user input and response in this conversation.
        /// </summary>
        [JsonProperty("dialog_turn_counter")]
        public int DialogTurnCounter { get; set; }

        /// <summary>
        /// The number of inputs in this conversation. This counter might be higher than the dialog_turn_counter counter 
        /// when multiple inputs are needed before a response can be returned.
        /// </summary>
        [JsonProperty("dialog_request_counter")]
        public int DialogRequestCounter { get; set; }
    }
}
