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
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Model
{
    /// <summary>
    /// EntityExportResponse object
    /// </summary>
    public class EntityExportResponse
    {
        /// <summary>
        /// The name of the entity.
        /// </summary>
        [JsonProperty("entity")]
        public string Entity { get; set; }

        /// <summary>
        /// The description of the entity.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The source of the entity. For system entities, system.entities is returned.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [JsonProperty("open_list")]
        public bool OpenList { get; set; }

        /// <summary>
        /// The timestamp for creation of the entity.
        /// </summary>
        [JsonProperty("created")]
        public string Created { get; set; }

        /// <summary>
        /// The timestamp for the last update to the entity.
        /// </summary>
        [JsonProperty("updated")]
        public string Updated { get; set; }

        /// <summary>
        /// An array of entity values.
        /// </summary>
        [JsonProperty("values")]
        public List<ValueExportResponse> Values { get; set; }
    }
}
