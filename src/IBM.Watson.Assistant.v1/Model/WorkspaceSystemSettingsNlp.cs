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

using Newtonsoft.Json;

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// Workspace settings related to the version of the training algorithms currently used by the skill.
    /// </summary>
    public class WorkspaceSystemSettingsNlp
    {
        /// <summary>
        /// The policy the skill follows for selecting the algorithm version to use:
        ///
        ///  - `baseline`: the latest mature version
        ///  - `beta`: the latest beta version.
        /// </summary>
        public class ModelEnumValue
        {
            /// <summary>
            /// Constant BASELINE for baseline
            /// </summary>
            public const string BASELINE = "baseline";
            /// <summary>
            /// Constant BETA for beta
            /// </summary>
            public const string BETA = "beta";
            
        }

        /// <summary>
        /// The policy the skill follows for selecting the algorithm version to use:
        ///
        ///  - `baseline`: the latest mature version
        ///  - `beta`: the latest beta version.
        /// Constants for possible values can be found using WorkspaceSystemSettingsNlp.ModelEnumValue
        /// </summary>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    }

}
