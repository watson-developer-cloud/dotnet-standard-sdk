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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// RuntimeOutput.
    /// </summary>
    public class RuntimeOutput
    {
        /// <summary>
        /// An array of responses to the user.
        /// </summary>
        /// <value>An array of responses to the user.</value>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Text { get; set; }
        /// <summary>
        /// Up to 50 messages logged with the request.
        /// </summary>
        /// <value>Up to 50 messages logged with the request.</value>
        [JsonProperty("log_messages", NullValueHandling = NullValueHandling.Ignore)]
        public List<RuntimeLogMessage> LogMessages { get; set; }
        /// <summary>
        /// An array of the nodes that were triggered to create the response.
        /// </summary>
        /// <value>An array of the nodes that were triggered to create the response.</value>
        [JsonProperty("nodes_visited", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> NodesVisited { get; set; }
    }

}
