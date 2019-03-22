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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.Watson.Discovery.v1.Model
{
    /// <summary>
    /// Object containing source crawl status information.
    /// </summary>
    public class SourceStatus : BaseModel
    {
        /// <summary>
        /// The current status of the source crawl for this collection. This field returns `not_configured` if the
        /// default configuration for this source does not have a **source** object defined.
        ///
        /// -  `running` indicates that a crawl to fetch more documents is in progress.
        /// -  `complete` indicates that the crawl has completed with no errors.
        /// -  `complete_with_notices` indicates that some notices were generated during the crawl. Notices can be
        /// checked by using the **notices** query method.
        /// -  `stopped` indicates that the crawl has stopped but is not complete.
        /// </summary>
        /// <value>
        /// The current status of the source crawl for this collection. This field returns `not_configured` if the
        /// default configuration for this source does not have a **source** object defined.
        ///
        /// -  `running` indicates that a crawl to fetch more documents is in progress.
        /// -  `complete` indicates that the crawl has completed with no errors.
        /// -  `complete_with_notices` indicates that some notices were generated during the crawl. Notices can be
        /// checked by using the **notices** query method.
        /// -  `stopped` indicates that the crawl has stopped but is not complete.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum RUNNING for running
            /// </summary>
            [EnumMember(Value = "running")]
            RUNNING,
            
            /// <summary>
            /// Enum COMPLETE for complete
            /// </summary>
            [EnumMember(Value = "complete")]
            COMPLETE,
            
            /// <summary>
            /// Enum COMPLETE_WITH_NOTICES for complete_with_notices
            /// </summary>
            [EnumMember(Value = "complete_with_notices")]
            COMPLETE_WITH_NOTICES,
            
            /// <summary>
            /// Enum STOPPED for stopped
            /// </summary>
            [EnumMember(Value = "stopped")]
            STOPPED,
            
            /// <summary>
            /// Enum NOT_CONFIGURED for not_configured
            /// </summary>
            [EnumMember(Value = "not_configured")]
            NOT_CONFIGURED
        }

        /// <summary>
        /// The current status of the source crawl for this collection. This field returns `not_configured` if the
        /// default configuration for this source does not have a **source** object defined.
        ///
        /// -  `running` indicates that a crawl to fetch more documents is in progress.
        /// -  `complete` indicates that the crawl has completed with no errors.
        /// -  `complete_with_notices` indicates that some notices were generated during the crawl. Notices can be
        /// checked by using the **notices** query method.
        /// -  `stopped` indicates that the crawl has stopped but is not complete.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// Date in UTC format indicating when the last crawl was attempted. If `null`, no crawl was completed.
        /// </summary>
        [JsonProperty("last_updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdated { get; set; }
    }

}
