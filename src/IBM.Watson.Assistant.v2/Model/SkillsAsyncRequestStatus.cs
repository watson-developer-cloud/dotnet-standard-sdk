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
    /// SkillsAsyncRequestStatus.
    /// </summary>
    public class SkillsAsyncRequestStatus
    {
        /// <summary>
        /// The current status of the asynchronous operation:
        ///  - `Available`: An asynchronous export is available.
        ///  - `Completed`: An asynchronous import operation has completed successfully.
        ///  - `Failed`: An asynchronous operation has failed. See the **status_errors** property for more information
        /// about the cause of the failure.
        ///  - `Processing`: An asynchronous operation has not yet completed.
        /// </summary>
        public class StatusEnumValue
        {
            /// <summary>
            /// Constant AVAILABLE for Available
            /// </summary>
            public const string AVAILABLE = "Available";
            /// <summary>
            /// Constant COMPLETED for Completed
            /// </summary>
            public const string COMPLETED = "Completed";
            /// <summary>
            /// Constant FAILED for Failed
            /// </summary>
            public const string FAILED = "Failed";
            /// <summary>
            /// Constant PROCESSING for Processing
            /// </summary>
            public const string PROCESSING = "Processing";
            
        }

        /// <summary>
        /// The current status of the asynchronous operation:
        ///  - `Available`: An asynchronous export is available.
        ///  - `Completed`: An asynchronous import operation has completed successfully.
        ///  - `Failed`: An asynchronous operation has failed. See the **status_errors** property for more information
        /// about the cause of the failure.
        ///  - `Processing`: An asynchronous operation has not yet completed.
        /// Constants for possible values can be found using SkillsAsyncRequestStatus.StatusEnumValue
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// The assistant ID of the assistant.
        /// </summary>
        [JsonProperty("assistant_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string AssistantId { get; private set; }
        /// <summary>
        /// The description of the failed asynchronous operation. Included only if **status**=`Failed`.
        /// </summary>
        [JsonProperty("status_description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string StatusDescription { get; private set; }
        /// <summary>
        /// An array of messages about errors that caused an asynchronous operation to fail. Included only if
        /// **status**=`Failed`.
        /// </summary>
        [JsonProperty("status_errors", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<StatusError> StatusErrors { get; private set; }
    }

}
