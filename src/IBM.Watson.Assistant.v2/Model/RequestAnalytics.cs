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
    /// An optional object containing analytics data. Currently, this data is used only for events sent to the Segment
    /// extension.
    /// </summary>
    public class RequestAnalytics
    {
        /// <summary>
        /// The browser that was used to send the message that triggered the event.
        /// </summary>
        [JsonProperty("browser", NullValueHandling = NullValueHandling.Ignore)]
        public string Browser { get; set; }
        /// <summary>
        /// The type of device that was used to send the message that triggered the event.
        /// </summary>
        [JsonProperty("device", NullValueHandling = NullValueHandling.Ignore)]
        public string Device { get; set; }
        /// <summary>
        /// The URL of the web page that was used to send the message that triggered the event.
        /// </summary>
        [JsonProperty("pageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string PageUrl { get; set; }
    }

}
