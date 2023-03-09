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

using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// Configuration settings for the Watson Discovery service instance used by the search integration.
    /// </summary>
    public class SearchSettingsDiscovery
    {
        /// <summary>
        /// The ID for the Watson Discovery service instance.
        /// </summary>
        [JsonProperty("instance_id", NullValueHandling = NullValueHandling.Ignore)]
        public string InstanceId { get; set; }
        /// <summary>
        /// The ID for the Watson Discovery project.
        /// </summary>
        [JsonProperty("project_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ProjectId { get; set; }
        /// <summary>
        /// The URL for the Watson Discovery service instance.
        /// </summary>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        /// <summary>
        /// The maximum number of primary results to include in the response.
        /// </summary>
        [JsonProperty("max_primary_results", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxPrimaryResults { get; set; }
        /// <summary>
        /// The maximum total number of primary and additional results to include in the response.
        /// </summary>
        [JsonProperty("max_total_results", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxTotalResults { get; set; }
        /// <summary>
        /// The minimum confidence threshold for included results. Any results with a confidence below this threshold
        /// will be discarded.
        /// </summary>
        [JsonProperty("confidence_threshold", NullValueHandling = NullValueHandling.Ignore)]
        public double? ConfidenceThreshold { get; set; }
        /// <summary>
        /// Whether to include the most relevant passages of text in the **highlight** property of each result.
        /// </summary>
        [JsonProperty("highlight", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Highlight { get; set; }
        /// <summary>
        /// Whether to use the answer finding feature to emphasize answers within highlighted passages. This property is
        /// ignored if **highlight**=`false`.
        ///
        /// **Notes:**
        ///  - Answer finding is available only if the search skill is connected to a Discovery v2 service instance.
        ///  - Answer finding is not supported on IBM Cloud Pak for Data.
        /// </summary>
        [JsonProperty("find_answers", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FindAnswers { get; set; }
        /// <summary>
        /// Authentication information for the Watson Discovery service. For more information, see the [Watson Discovery
        /// documentation](https://cloud.ibm.com/apidocs/discovery-data#authentication).
        ///
        ///  **Note:** You must specify either **basic** or **bearer**, but not both.
        /// </summary>
        [JsonProperty("authentication", NullValueHandling = NullValueHandling.Ignore)]
        public SearchSettingsDiscoveryAuthentication Authentication { get; set; }
    }

}
