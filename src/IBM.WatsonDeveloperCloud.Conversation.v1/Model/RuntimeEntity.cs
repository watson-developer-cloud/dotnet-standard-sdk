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
    /// A term from the request that was identified as an entity.
    /// </summary>
    public class RuntimeEntity
    {
        /// <summary>
        /// The recognized entity from a term in the input.
        /// </summary>
        /// <value>The recognized entity from a term in the input.</value>
        [JsonProperty("entity", NullValueHandling = NullValueHandling.Ignore)]
        public string Entity { get; set; }
        /// <summary>
        /// Zero-based character offsets that indicate where the entity value begins and ends in the input text.
        /// </summary>
        /// <value>Zero-based character offsets that indicate where the entity value begins and ends in the input text.</value>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public List<long?> Location { get; set; }
        /// <summary>
        /// The term in the input text that was recognized.
        /// </summary>
        /// <value>The term in the input text that was recognized.</value>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
        /// <summary>
        /// A decimal percentage that represents Watson's confidence in the entity.
        /// </summary>
        /// <value>A decimal percentage that represents Watson's confidence in the entity.</value>
        [JsonProperty("confidence", NullValueHandling = NullValueHandling.Ignore)]
        public float? Confidence { get; set; }
    }

}
