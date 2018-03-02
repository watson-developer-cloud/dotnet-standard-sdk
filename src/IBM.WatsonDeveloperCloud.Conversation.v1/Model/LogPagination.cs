/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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
    /// The pagination data for the returned objects.
    /// </summary>
    public class LogPagination
    {
        /// <summary>
        /// The URL that will return the next page of results.
        /// </summary>
        /// <value>The URL that will return the next page of results.</value>
        [JsonProperty("next_url", NullValueHandling = NullValueHandling.Ignore)]
        public string NextUrl { get; set; }
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        /// <value>Reserved for future use.</value>
        [JsonProperty("matched", NullValueHandling = NullValueHandling.Ignore)]
        public long? Matched { get; set; }
    }

}
