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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// CaptureGroup.
    /// </summary>
    public class CaptureGroup
    {
        /// <summary>
        /// A recognized capture group for the entity.
        /// </summary>
        /// <value>A recognized capture group for the entity.</value>
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }
        /// <summary>
        /// Zero-based character offsets that indicate where the entity value begins and ends in the input text.
        /// </summary>
        /// <value>Zero-based character offsets that indicate where the entity value begins and ends in the input text.</value>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public List<long?> Location { get; set; }
    }

}
