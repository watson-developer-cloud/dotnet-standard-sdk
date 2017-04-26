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
    /// UpdateWorkspace object
    /// </summary>
    public class UpdateWorkspace
    {
        /// <summary>
        /// The name of the workspace.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The description of the workspace.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The language of the workspace.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Any metadata that is required by the workspace.
        /// </summary>
        [JsonProperty("metadata")]
        public object Metadata { get; set; }

        /// <summary>
        /// The counter examples of the workspace.
        /// </summary>
        [JsonProperty("counterexamples")]
        public List<CreateExample> CounterExamples { get; set; }

        /// <summary>
        /// The dialog nodes of the workspace.
        /// </summary>
        [JsonProperty("dialog_nodes")]
        public List<CreateDialogNode> DialogNodes { get; set; }

        /// <summary>
        /// The entities of the workspace.
        /// </summary>
        [JsonProperty("entities")]
        public List<CreateEntity> Entities { get; set; }

        /// <summary>
        /// The intents of the workspace.
        /// </summary>
        [JsonProperty("intents")]
        public List<CreateIntent> Intents { get; set; }
    }
}
