/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// LogExportResponse.
    /// </summary>
    public class LogExportResponse
    {
        /// <summary>
        /// Gets or Sets Request
        /// </summary>
        [JsonProperty("request", NullValueHandling = NullValueHandling.Ignore)]
        public MessageRequest Request { get; set; }
        /// <summary>
        /// Gets or Sets Response
        /// </summary>
        [JsonProperty("response", NullValueHandling = NullValueHandling.Ignore)]
        public MessageResponse Response { get; set; }
        /// <summary>
        /// A unique identifier for the logged message.
        /// </summary>
        /// <value>A unique identifier for the logged message.</value>
        [JsonProperty("log_id", NullValueHandling = NullValueHandling.Ignore)]
        public string LogId { get; set; }
        /// <summary>
        /// The timestamp for receipt of the message.
        /// </summary>
        /// <value>The timestamp for receipt of the message.</value>
        [JsonProperty("request_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestTimestamp { get; set; }
        /// <summary>
        /// The timestamp for the system response to the message.
        /// </summary>
        /// <value>The timestamp for the system response to the message.</value>
        [JsonProperty("response_timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string ResponseTimestamp { get; set; }
    }

}
