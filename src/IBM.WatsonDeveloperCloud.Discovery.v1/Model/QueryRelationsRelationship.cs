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
    /// QueryRelationsRelationship.
    /// </summary>
    public class QueryRelationsRelationship
    {
        /// <summary>
        /// The identified relationship type.
        /// </summary>
        /// <value>The identified relationship type.</value>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// The number of times the relationship is mentioned.
        /// </summary>
        /// <value>The number of times the relationship is mentioned.</value>
        [JsonProperty("frequency", NullValueHandling = NullValueHandling.Ignore)]
        public long? Frequency { get; set; }
        /// <summary>
        /// Information about the relationship.
        /// </summary>
        /// <value>Information about the relationship.</value>
        [JsonProperty("arguments", NullValueHandling = NullValueHandling.Ignore)]
        public List<QueryRelationsArgument> Arguments { get; set; }
    }

}
