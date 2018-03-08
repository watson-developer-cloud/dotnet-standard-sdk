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

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model
{
    /// <summary>
    /// Disambiguation information for the entity.
    /// </summary>
    public class DisambiguationResult
    {
        /// <summary>
        /// Common entity name.
        /// </summary>
        /// <value>Common entity name.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Link to the corresponding DBpedia resource.
        /// </summary>
        /// <value>Link to the corresponding DBpedia resource.</value>
        [JsonProperty("dbpedia_resource", NullValueHandling = NullValueHandling.Ignore)]
        public string DbpediaResource { get; set; }
        /// <summary>
        /// Entity subtype information.
        /// </summary>
        /// <value>Entity subtype information.</value>
        [JsonProperty("subtype", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Subtype { get; set; }
    }

}
