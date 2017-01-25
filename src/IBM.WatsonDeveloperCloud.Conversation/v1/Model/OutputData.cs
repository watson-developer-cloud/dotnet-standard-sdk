/**
* Copyright 2015 IBM Corp. All Rights Reserved.
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
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// OutputData object
    /// </summary>
    public class OutputData
    {
        /// <summary>
        /// Up to 50 messages logged with the request.
        /// </summary>
        [JsonProperty("log_messages")]
        public List<LogMessageResponse> LogMessages { get; set; }

        /// <summary>
        /// Responses to the user.
        /// </summary>
        [JsonProperty("text")]
        public List<string> Text { get; set; }

        /// <summary>
        /// The nodes that were executed to create the response.
        /// </summary>
        [JsonProperty("nodes_visited")]
        public List<string> NodesVisited { get; set; }
    }
}
