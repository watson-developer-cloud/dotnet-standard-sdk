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
using IBM.Cloud.SDK.Core;
using Newtonsoft.Json;
using System;

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// UpdateEntity.
    /// </summary>
    public class UpdateEntity : BaseModel
    {
        /// <summary>
        /// The name of the entity. This string must conform to the following restrictions:
        /// - It can contain only Unicode alphanumeric, underscore, and hyphen characters.
        /// - It cannot begin with the reserved prefix `sys-`.
        /// - It must be no longer than 64 characters.
        /// </summary>
        [JsonProperty("entity", NullValueHandling = NullValueHandling.Ignore)]
        public string Entity { get; set; }
        /// <summary>
        /// The description of the entity. This string cannot contain carriage return, newline, or tab characters, and
        /// it must be no longer than 128 characters.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// Any metadata related to the entity.
        /// </summary>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Metadata { get; set; }
        /// <summary>
        /// Whether to use fuzzy matching for the entity.
        /// </summary>
        [JsonProperty("fuzzy_match", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FuzzyMatch { get; set; }
        /// <summary>
        /// The timestamp for creation of the object.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Created { get; private set; }
        /// <summary>
        /// The timestamp for the most recent update to the object.
        /// </summary>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Updated { get; private set; }
        /// <summary>
        /// An array of objects describing the entity values.
        /// </summary>
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<CreateValue> Values { get; set; }
    }

}
