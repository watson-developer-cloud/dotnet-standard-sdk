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

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// Workspace settings related to the version of the training algorithms currently used by the skill.
    /// </summary>
    public class WorkspaceSystemSettingsNlp
    {
        /// <summary>
        /// The policy the skill follows for selecting the algorithm version to use. For more information, see the
        /// [documentation](/docs/watson-assistant?topic=watson-assistant-algorithm-version).
        ///
        ///  On IBM Cloud, you can specify `latest`, `previous`, or `beta`.
        ///
        ///  On IBM Cloud Pak for Data, you can specify either `beta` or the date of the version you want to use, in
        /// `YYYY-MM-DD` format.
        /// </summary>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    }

}
