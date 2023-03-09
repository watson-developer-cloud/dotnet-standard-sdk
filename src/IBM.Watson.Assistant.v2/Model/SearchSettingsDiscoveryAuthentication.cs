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
    /// Authentication information for the Watson Discovery service. For more information, see the [Watson Discovery
    /// documentation](https://cloud.ibm.com/apidocs/discovery-data#authentication).
    ///
    ///  **Note:** You must specify either **basic** or **bearer**, but not both.
    /// </summary>
    public class SearchSettingsDiscoveryAuthentication
    {
        /// <summary>
        /// The HTTP basic authentication credentials for Watson Discovery. Specify your Watson Discovery API key in the
        /// format `apikey:{apikey}`.
        /// </summary>
        [JsonProperty("basic", NullValueHandling = NullValueHandling.Ignore)]
        public string Basic { get; set; }
        /// <summary>
        /// The authentication bearer token for Watson Discovery.
        /// </summary>
        [JsonProperty("bearer", NullValueHandling = NullValueHandling.Ignore)]
        public string Bearer { get; set; }
    }

}
