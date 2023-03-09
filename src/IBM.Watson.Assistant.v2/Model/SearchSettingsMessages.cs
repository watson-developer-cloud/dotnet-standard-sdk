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
    /// The messages included with responses from the search integration.
    /// </summary>
    public class SearchSettingsMessages
    {
        /// <summary>
        /// The message to include in the response to a successful query.
        /// </summary>
        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public string Success { get; set; }
        /// <summary>
        /// The message to include in the response when the query encounters an error.
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
        /// <summary>
        /// The message to include in the response when there is no result from the query.
        /// </summary>
        [JsonProperty("no_result", NullValueHandling = NullValueHandling.Ignore)]
        public string NoResult { get; set; }
    }

}
