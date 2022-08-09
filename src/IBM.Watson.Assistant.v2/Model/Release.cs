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

using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// Release.
    /// </summary>
    public class Release
    {
        /// <summary>
        /// The current status of the release:
        ///  - **Available**: The release is available for deployment.
        ///  - **Failed**: An asynchronous publish operation has failed.
        ///  - **Processing**: An asynchronous publish operation has not yet completed.
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
            /// Constant PROCESSING for Processing
            /// </summary>
            public const string PROCESSING = "Processing";
            
        }

        /// <summary>
        /// The current status of the release:
        ///  - **Available**: The release is available for deployment.
        ///  - **Failed**: An asynchronous publish operation has failed.
        ///  - **Processing**: An asynchronous publish operation has not yet completed.
        /// Constants for possible values can be found using Release.StatusEnumValue
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// The name of the release. The name is the version number (an integer), returned as a string.
        /// </summary>
        [JsonProperty("release", NullValueHandling = NullValueHandling.Ignore)]
        public string _Release { get; set; }
        /// <summary>
        /// The description of the release.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// An array of objects describing the environments where this release has been deployed.
        /// </summary>
        [JsonProperty("environment_references", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<EnvironmentReference> EnvironmentReferences { get; private set; }
        /// <summary>
        /// An object describing the versionable content objects (such as skill snapshots) that are included in the
        /// release.
        /// </summary>
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public ReleaseContent Content { get; set; }
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
    }

}
