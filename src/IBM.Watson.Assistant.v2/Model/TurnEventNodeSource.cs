/**
* (C) Copyright IBM Corp. 2022.
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

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// TurnEventNodeSource.
    /// </summary>
    public class TurnEventNodeSource
    {
        /// <summary>
        /// The type of turn event.
        /// </summary>
        public class TypeEnumValue
        {
            /// <summary>
            /// Constant DIALOG_NODE for dialog_node
            /// </summary>
            public const string DIALOG_NODE = "dialog_node";
            
        }

        /// <summary>
        /// The type of turn event.
        /// Constants for possible values can be found using TurnEventNodeSource.TypeEnumValue
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// A dialog node that was visited during processing of the input message.
        /// </summary>
        [JsonProperty("dialog_node", NullValueHandling = NullValueHandling.Ignore)]
        public string DialogNode { get; set; }
        /// <summary>
        /// The title of the dialog node.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        /// <summary>
        /// The condition that triggered the dialog node.
        /// </summary>
        [JsonProperty("condition", NullValueHandling = NullValueHandling.Ignore)]
        public string Condition { get; set; }
    }

}
