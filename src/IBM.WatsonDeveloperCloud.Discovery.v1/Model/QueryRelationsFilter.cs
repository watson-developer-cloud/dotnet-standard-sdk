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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// QueryRelationsFilter.
    /// </summary>
    public class QueryRelationsFilter
    {
        /// <summary>
        /// A list of relation types to include or exclude from the query.
        /// </summary>
        /// <value>A list of relation types to include or exclude from the query.</value>
        [JsonProperty("relation_types", NullValueHandling = NullValueHandling.Ignore)]
        public QueryFilterType RelationTypes { get; set; }
        /// <summary>
        /// A list of entity types to include or exclude from the query.
        /// </summary>
        /// <value>A list of entity types to include or exclude from the query.</value>
        [JsonProperty("entity_types", NullValueHandling = NullValueHandling.Ignore)]
        public QueryFilterType EntityTypes { get; set; }
        /// <summary>
        /// A comma-separated list of document IDs to include in the query.
        /// </summary>
        /// <value>A comma-separated list of document IDs to include in the query.</value>
        [JsonProperty("document_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> DocumentIds { get; set; }
    }

}
