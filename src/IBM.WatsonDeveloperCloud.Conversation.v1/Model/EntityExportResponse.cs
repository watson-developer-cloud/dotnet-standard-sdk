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
using System;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// EntityExportResponse.
    /// </summary>
    public class EntityExportResponse
    {
        /// <summary>
        /// The name of the entity.
        /// </summary>
        /// <value>The name of the entity.</value>
        [JsonProperty("entity", NullValueHandling = NullValueHandling.Ignore)]
        public string Entity { get; set; }
        /// <summary>
        /// The timestamp for creation of the entity.
        /// </summary>
        /// <value>The timestamp for creation of the entity.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }
        /// <summary>
        /// The timestamp for the last update to the entity.
        /// </summary>
        /// <value>The timestamp for the last update to the entity.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Updated { get; set; }
        /// <summary>
        /// The description of the entity.
        /// </summary>
        /// <value>The description of the entity.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// Any metadata related to the entity.
        /// </summary>
        /// <value>Any metadata related to the entity.</value>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public object Metadata { get; set; }
        /// <summary>
        /// Whether fuzzy matching is used for the entity.
        /// </summary>
        /// <value>Whether fuzzy matching is used for the entity.</value>
        [JsonProperty("fuzzy_match", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FuzzyMatch { get; set; }
        /// <summary>
        /// An array of entity values.
        /// </summary>
        /// <value>An array of entity values.</value>
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<ValueExportResponse> Values { get; set; }
    }

}
