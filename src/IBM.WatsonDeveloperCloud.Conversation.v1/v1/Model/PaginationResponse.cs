﻿/**
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
    /// PaginationResponse object
    /// </summary>
    public class PaginationResponse
    {
        /// <summary>
        /// The URL that will return the same page of results.
        /// </summary>
        [JsonProperty("refresh_url")]
        public string RefreshUrl { get; set; }

        /// <summary>
        /// The URL that will return the next page of results.
        /// </summary>
        [JsonProperty("next_url")]
        public string NextUrl { get; set; }

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [JsonProperty("matched")]
        public int Matched { get; set; }
    }
}
