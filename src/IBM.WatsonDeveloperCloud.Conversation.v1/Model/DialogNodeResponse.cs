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
    /// DialogNodeResponse object
    /// </summary>
    public class DialogNodeResponse
    {
        /// <summary>
        /// The dialog node ID.
        /// </summary>
        [JsonProperty("dialog_node")]
        public string DialogNode { get; set; }

        /// <summary>
        /// The description of the dialog node.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The condition that will trigger the dialog node.
        /// </summary>
        [JsonProperty("conditions")]
        public string Conditions { get; set; }

        /// <summary>
        /// The parent dialog node.
        /// </summary>
        [JsonProperty("parent")]
        public string Parent { get; set; }

        /// <summary>
        /// The previous dialog node.
        /// </summary>
        [JsonProperty("previous_sibling")]
        public string PreviousSibling { get; set; }

        /// <summary>
        /// The output dialog node.
        /// </summary>
        [JsonProperty("output")]
        public DialogNodeOutput Output { get; set; }

        /// <summary>
        /// The context for the dialog node.
        /// </summary>
        [JsonProperty("context")]
        public object Context { get; set; }

        /// <summary>
        /// The metadata for the dialog node.
        /// </summary>
        [JsonProperty("metadata")]
        public object Metadata { get; set; }

        /// <summary>
        /// The go to for the dialog node.
        /// </summary>
        [JsonProperty("go_to")]
        public DialogNodeGoTo GoTo { get; set; }

        /// <summary>
        /// The timestamp for creation of the dialog node.
        /// </summary>
        [JsonProperty("created")]
        public string Created { get; set; }
    }
}
