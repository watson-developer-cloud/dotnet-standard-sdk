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
    /// WorkspaceExportResponse object
    /// </summary>
    public class WorkspaceExportResponse
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
        /// The timestamp for creation of the workspace.
        /// </summary>
        [JsonProperty("created")]
        public string Created { get; set; }

        /// <summary>
        /// The timestamp for the last update to the workspace.
        /// </summary>
        [JsonProperty("updated")]
        public string Updated { get; set; }

        /// <summary>
        /// The workspace ID.
        /// </summary>
        [JsonProperty("workspace_id")]
        public string WorkspaceId { get; set; }

        /// <summary>
        /// The current status of the workspace (Non Existent, Training, Failed, Available, or Unavailable).
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// An array of Intent collection.
        /// </summary>
        [JsonProperty("intents")]
        public List<IntentExportResponse> Intents { get; set; }

        /// <summary>
        /// An array of Entity collections
        /// </summary>
        [JsonProperty("entities")]
        public List<EntityExportResponse> Entities { get; set; }

        /// <summary>
        /// An array of CounterExample collection.
        /// </summary>
        [JsonProperty("counterexamples")]
        public List<ExampleResponse> CounterExamples { get; set; }

        /// <summary>
        /// An array of dialog nodes.
        /// </summary>
        [JsonProperty("dialog_nodes")]
        public List<DialogNodeResponse> DialogNodes { get; set; }
    }
}
