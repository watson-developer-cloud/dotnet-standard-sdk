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

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// EnvironmentReference.
    /// </summary>
    public class EnvironmentReference
    {
        /// <summary>
        /// The type of the deployed environment. All environments other than the draft and live environments have the
        /// type `staging`.
        /// </summary>
        public class EnvironmentEnumValue
        {
            /// <summary>
            /// Constant DRAFT for draft
            /// </summary>
            public const string DRAFT = "draft";
            /// <summary>
            /// Constant LIVE for live
            /// </summary>
            public const string LIVE = "live";
            /// <summary>
            /// Constant STAGING for staging
            /// </summary>
            public const string STAGING = "staging";
            
        }

        /// <summary>
        /// The type of the deployed environment. All environments other than the draft and live environments have the
        /// type `staging`.
        /// Constants for possible values can be found using EnvironmentReference.EnvironmentEnumValue
        /// </summary>
        [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
        public string Environment { get; set; }
        /// <summary>
        /// The name of the deployed environment.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The environment ID of the deployed environment.
        /// </summary>
        [JsonProperty("environment_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string EnvironmentId { get; private set; }
    }

}
