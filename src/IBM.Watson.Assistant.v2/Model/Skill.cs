/**
* (C) Copyright IBM Corp. 2023.
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

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// Skill.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// The current status of the skill:
        ///  - **Available**: The skill is available and ready to process messages.
        ///  - **Failed**: An asynchronous operation has failed. See the **status_errors** property for more information
        /// about the cause of the failure.
        ///  - **Non Existent**: The skill does not exist.
        ///  - **Processing**: An asynchronous operation has not yet completed.
        ///  - **Training**: The skill is training based on new data.
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
        /// The type of skill.
        /// </summary>
        public class TypeEnumValue
        {
            /// <summary>
            /// Constant ACTION for action
            /// </summary>
            public const string ACTION = "action";
            /// <summary>
            /// Constant DIALOG for dialog
            /// </summary>
            public const string DIALOG = "dialog";
            /// <summary>
            /// Constant SEARCH for search
            /// </summary>
            public const string SEARCH = "search";
            
        }

        /// <summary>
        /// The current status of the skill:
        ///  - **Available**: The skill is available and ready to process messages.
        ///  - **Failed**: An asynchronous operation has failed. See the **status_errors** property for more information
        /// about the cause of the failure.
        ///  - **Non Existent**: The skill does not exist.
        ///  - **Processing**: An asynchronous operation has not yet completed.
        ///  - **Training**: The skill is training based on new data.
        /// Constants for possible values can be found using Skill.StatusEnumValue
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// The type of skill.
        /// Constants for possible values can be found using Skill.TypeEnumValue
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// The name of the skill. This string cannot contain carriage return, newline, or tab characters.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of the skill. This string cannot contain carriage return, newline, or tab characters.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// An object containing the conversational content of an action or dialog skill.
        /// </summary>
        [JsonProperty("workspace", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Workspace { get; set; }
        /// <summary>
        /// The skill ID of the skill.
        /// </summary>
        [JsonProperty("skill_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string SkillId { get; private set; }
        /// <summary>
        /// An array of messages about errors that caused an asynchronous operation to fail. Included only if
        /// **status**=`Failed`.
        /// </summary>
        [JsonProperty("status_errors", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<StatusError> StatusErrors { get; private set; }
        /// <summary>
        /// The description of the failed asynchronous operation. Included only if **status**=`Failed`.
        /// </summary>
        [JsonProperty("status_description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string StatusDescription { get; private set; }
        /// <summary>
        /// For internal use only.
        /// </summary>
        [JsonProperty("dialog_settings", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> DialogSettings { get; set; }
        /// <summary>
        /// The unique identifier of the assistant the skill is associated with.
        /// </summary>
        [JsonProperty("assistant_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string AssistantId { get; private set; }
        /// <summary>
        /// The unique identifier of the workspace that contains the skill content. Included only for action and dialog
        /// skills.
        /// </summary>
        [JsonProperty("workspace_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string WorkspaceId { get; private set; }
        /// <summary>
        /// The unique identifier of the environment where the skill is defined. For action and dialog skills, this is
        /// always the draft environment.
        /// </summary>
        [JsonProperty("environment_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string EnvironmentId { get; private set; }
        /// <summary>
        /// Whether the skill is structurally valid.
        /// </summary>
        [JsonProperty("valid", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? Valid { get; private set; }
        /// <summary>
        /// The name that will be given to the next snapshot that is created for the skill. A snapshot of each
        /// versionable skill is saved for each new release of an assistant.
        /// </summary>
        [JsonProperty("next_snapshot_version", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string NextSnapshotVersion { get; private set; }
        /// <summary>
        /// An object describing the search skill configuration.
        /// </summary>
        [JsonProperty("search_settings", NullValueHandling = NullValueHandling.Ignore)]
        public SearchSettings SearchSettings { get; set; }
        /// <summary>
        /// An array of warnings describing errors with the search skill configuration. Included only for search skills.
        /// </summary>
        [JsonProperty("warnings", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<SearchSkillWarning> Warnings { get; private set; }
        /// <summary>
        /// The language of the skill.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
    }

}
