/**
* (C) Copyright IBM Corp. 2018, 2022.
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
using System;

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// Workspace.
    /// </summary>
    public class Workspace
    {
        /// <summary>
        /// The current status of the workspace:
        ///  - **Available**: The workspace is available and ready to process messages.
        ///  - **Failed**: An asynchronous operation has failed. See the **status_errors** property for more information
        /// about the cause of the failure. Returned only by the **Export workspace asynchronously** method.
        ///  - **Non Existent**: The workspace does not exist.
        ///  - **Processing**: An asynchronous operation has not yet completed. Returned only by the **Export workspace
        /// asynchronously** method.
        ///  - **Training**: The workspace is training based on new data such as intents or examples.
        /// </summary>
        public class StatusEnumValue
        {
            /// <summary>
            /// Constant AVAILABLE for Available
            /// </summary>
            public const string AVAILABLE = "Available";
            /// <summary>
            /// Constant FAILED for Failed
            /// </summary>
            public const string FAILED = "Failed";
            /// <summary>
            /// Constant NON_EXISTENT for Non Existent
            /// </summary>
            public const string NON_EXISTENT = "Non Existent";
            /// <summary>
            /// Constant PROCESSING for Processing
            /// </summary>
            public const string PROCESSING = "Processing";
            /// <summary>
            /// Constant TRAINING for Training
            /// </summary>
            public const string TRAINING = "Training";
            /// <summary>
            /// Constant UNAVAILABLE for Unavailable
            /// </summary>
            public const string UNAVAILABLE = "Unavailable";
            
        }

        /// <summary>
        /// The current status of the workspace:
        ///  - **Available**: The workspace is available and ready to process messages.
        ///  - **Failed**: An asynchronous operation has failed. See the **status_errors** property for more information
        /// about the cause of the failure. Returned only by the **Export workspace asynchronously** method.
        ///  - **Non Existent**: The workspace does not exist.
        ///  - **Processing**: An asynchronous operation has not yet completed. Returned only by the **Export workspace
        /// asynchronously** method.
        ///  - **Training**: The workspace is training based on new data such as intents or examples.
        /// Constants for possible values can be found using Workspace.StatusEnumValue
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// The name of the workspace. This string cannot contain carriage return, newline, or tab characters.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of the workspace. This string cannot contain carriage return, newline, or tab characters.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The language of the workspace.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// The workspace ID of the workspace.
        /// </summary>
        [JsonProperty("workspace_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string WorkspaceId { get; private set; }
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
        /// An array of messages about errors that caused an asynchronous operation to fail.
        /// </summary>
        [JsonProperty("status_errors", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<StatusError> StatusErrors { get; private set; }
        /// <summary>
        /// Gets or Sets Webhooks
        /// </summary>
        [JsonProperty("webhooks", NullValueHandling = NullValueHandling.Ignore)]
        public List<Webhook> Webhooks { get; set; }
        /// <summary>
        /// An array of intents.
        /// </summary>
        [JsonProperty("intents", NullValueHandling = NullValueHandling.Ignore)]
        public List<Intent> Intents { get; set; }
        /// <summary>
        /// An array of objects describing the entities for the workspace.
        /// </summary>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<Entity> Entities { get; set; }
        /// <summary>
        /// An object containing properties that indicate how many intents, entities, and dialog nodes are defined in
        /// the workspace. This property is included only in responses from the **Export workspace asynchronously**
        /// method, and only when the **verbose** query parameter is set to `true`.
        /// </summary>
        [JsonProperty("counts", NullValueHandling = NullValueHandling.Ignore)]
        public WorkspaceCounts Counts { get; set; }
    }

}
