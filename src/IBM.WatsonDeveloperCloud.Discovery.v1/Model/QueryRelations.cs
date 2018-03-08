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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// A respresentation of a relationship query.
    /// </summary>
    public class QueryRelations
    {
        /// <summary>
        /// The sorting method for the relationships, can be `score` or `frequency`. `frequency` is the number of unique times each entity is identified. The default is `score`.
        /// </summary>
        /// <value>The sorting method for the relationships, can be `score` or `frequency`. `frequency` is the number of unique times each entity is identified. The default is `score`.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SortEnum
        {
            
            /// <summary>
            /// Enum SCORE for score
            /// </summary>
            [EnumMember(Value = "score")]
            SCORE,
            
            /// <summary>
            /// Enum FREQUENCY for frequency
            /// </summary>
            [EnumMember(Value = "frequency")]
            FREQUENCY
        }

        /// <summary>
        /// The sorting method for the relationships, can be `score` or `frequency`. `frequency` is the number of unique times each entity is identified. The default is `score`.
        /// </summary>
        /// <value>The sorting method for the relationships, can be `score` or `frequency`. `frequency` is the number of unique times each entity is identified. The default is `score`.</value>
        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public SortEnum? Sort { get; set; }
        /// <summary>
        /// An array of entities to find relationships for.
        /// </summary>
        /// <value>An array of entities to find relationships for.</value>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<QueryRelationsEntity> Entities { get; set; }
        /// <summary>
        /// Entity text to provide context for the queried entity and rank based on that association. For example, if you wanted to query the city of London in England your query would look for `London` with the context of `England`.
        /// </summary>
        /// <value>Entity text to provide context for the queried entity and rank based on that association. For example, if you wanted to query the city of London in England your query would look for `London` with the context of `England`.</value>
        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public QueryEntitiesContext Context { get; set; }
        /// <summary>
        /// Filters to apply to the relationship query.
        /// </summary>
        /// <value>Filters to apply to the relationship query.</value>
        [JsonProperty("filter", NullValueHandling = NullValueHandling.Ignore)]
        public QueryRelationsFilter Filter { get; set; }
        /// <summary>
        /// The number of results to return. The default is `10`. The maximum is `1000`.
        /// </summary>
        /// <value>The number of results to return. The default is `10`. The maximum is `1000`.</value>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }
    }

}
