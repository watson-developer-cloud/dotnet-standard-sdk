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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// WorkspaceExportResponse.
    /// </summary>
    public class WorkspaceExportResponse
    {
        /// <summary>
        /// The current status of the workspace.
        /// </summary>
        /// <value>The current status of the workspace.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum NON_EXISTENT for Non Existent
            /// </summary>
            [EnumMember(Value = "Non Existent")]
            NON_EXISTENT,
            
            /// <summary>
            /// Enum TRAINING for Training
            /// </summary>
            [EnumMember(Value = "Training")]
            TRAINING,
            
            /// <summary>
            /// Enum FAILED for Failed
            /// </summary>
            [EnumMember(Value = "Failed")]
            FAILED,
            
            /// <summary>
            /// Enum AVAILABLE for Available
            /// </summary>
            [EnumMember(Value = "Available")]
            AVAILABLE,
            
            /// <summary>
            /// Enum UNAVAILABLE for Unavailable
            /// </summary>
            [EnumMember(Value = "Unavailable")]
            UNAVAILABLE
        }

        /// <summary>
        /// The current status of the workspace.
        /// </summary>
        /// <value>The current status of the workspace.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// The name of the workspace.
        /// </summary>
        /// <value>The name of the workspace.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of the workspace.
        /// </summary>
        /// <value>The description of the workspace.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The language of the workspace.
        /// </summary>
        /// <value>The language of the workspace.</value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// Any metadata that is required by the workspace.
        /// </summary>
        /// <value>Any metadata that is required by the workspace.</value>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object Metadata { get; set; }
        /// <summary>
        /// The timestamp for creation of the workspace.
        /// </summary>
        /// <value>The timestamp for creation of the workspace.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }
        /// <summary>
        /// The timestamp for the last update to the workspace.
        /// </summary>
        /// <value>The timestamp for the last update to the workspace.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Updated { get; set; }
        /// <summary>
        /// The workspace ID.
        /// </summary>
        /// <value>The workspace ID.</value>
        [JsonProperty("workspace_id", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkspaceId { get; set; }
        /// <summary>
        /// An array of intents.
        /// </summary>
        /// <value>An array of intents.</value>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<IntentExportResponse> Intents { get; set; }
        /// <summary>
        /// An array of entities.
        /// </summary>
        /// <value>An array of entities.</value>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<EntityExportResponse> Entities { get; set; }
        /// <summary>
        /// An array of counterexamples.
        /// </summary>
        /// <value>An array of counterexamples.</value>
        [JsonProperty("counterexamples", NullValueHandling = NullValueHandling.Ignore)]
        public List<ExampleResponse> Counterexamples { get; set; }
        /// <summary>
        /// An array of objects describing the dialog nodes in the workspace.
        /// </summary>
        /// <value>An array of objects describing the dialog nodes in the workspace.</value>
        [JsonProperty("dialog_nodes", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNodeResponse> DialogNodes { get; set; }
    }

}
