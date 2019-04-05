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
using System.Runtime.Serialization;
using IBM.Cloud.SDK.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// UpdateWorkspace.
    /// </summary>
    public class UpdateWorkspace : BaseModel
    {
        /// <summary>
        /// The current status of the workspace.
        /// </summary>
        /// <value>
        /// The current status of the workspace.
        /// </value>
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
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// The name of the workspace. This string cannot contain carriage return, newline, or tab characters, and it
        /// must be no longer than 64 characters.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of the workspace. This string cannot contain carriage return, newline, or tab characters,
        /// and it must be no longer than 128 characters.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The language of the workspace.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// Any metadata related to the workspace.
        /// </summary>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Metadata { get; set; }
        /// <summary>
        /// Whether training data from the workspace (including artifacts such as intents and entities) can be used by
        /// IBM for general service improvements. `true` indicates that workspace training data is not to be used.
        /// </summary>
        [JsonProperty("learning_opt_out", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LearningOptOut { get; set; }
        /// <summary>
        /// Global settings for the workspace.
        /// </summary>
        [JsonProperty("system_settings", NullValueHandling = NullValueHandling.Ignore)]
        public WorkspaceSystemSettings SystemSettings { get; set; }
        /// <summary>
        /// The workspace ID of the workspace.
        /// </summary>
        [JsonProperty("workspace_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string WorkspaceId { get; private set; }
        /// <summary>
        /// The timestamp for creation of the object.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Created { get; private set; }
        /// <summary>
        /// The timestamp for the most recent update to the object.
        /// </summary>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Updated { get; private set; }
        /// <summary>
        /// An array of objects defining the intents for the workspace.
        /// </summary>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<CreateIntent> Intents { get; set; }
        /// <summary>
        /// An array of objects describing the entities for the workspace.
        /// </summary>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<CreateEntity> Entities { get; set; }
        /// <summary>
        /// An array of objects describing the dialog nodes in the workspace.
        /// </summary>
        [JsonProperty("dialog_nodes", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNode> DialogNodes { get; set; }
        /// <summary>
        /// An array of objects defining input examples that have been marked as irrelevant input.
        /// </summary>
        [JsonProperty("counterexamples", NullValueHandling = NullValueHandling.Ignore)]
        public List<Counterexample> Counterexamples { get; set; }
    }

}
