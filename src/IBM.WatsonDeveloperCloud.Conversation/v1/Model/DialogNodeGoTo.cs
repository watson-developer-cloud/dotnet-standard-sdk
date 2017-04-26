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

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// DialogNodeGoTo object
    /// </summary>
    public class DialogNodeGoTo
    {
        /// <summary>
        /// The ID of the dialog node to go to.
        /// </summary>
        [JsonProperty("dialog_node")]
        public string DialogNode { get; set; }

        /// <summary>
        /// Where in the target node focus is passed to.
        /// </summary>
        [JsonProperty("selector")]
        public string Selector { get; set; }

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [JsonProperty("return")]
        public bool Return { get; set; }
    }
}
