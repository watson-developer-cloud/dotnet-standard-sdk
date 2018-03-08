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
    /// The relations between entities found in the content.
    /// </summary>
    public class RelationsResult
    {
        /// <summary>
        /// Confidence score for the relation. Higher values indicate greater confidence.
        /// </summary>
        /// <value>Confidence score for the relation. Higher values indicate greater confidence.</value>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public float? Score { get; set; }
        /// <summary>
        /// The sentence that contains the relation.
        /// </summary>
        /// <value>The sentence that contains the relation.</value>
        [JsonProperty("sentence", NullValueHandling = NullValueHandling.Ignore)]
        public string Sentence { get; set; }
        /// <summary>
        /// The type of the relation.
        /// </summary>
        /// <value>The type of the relation.</value>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// The extracted relation objects from the text.
        /// </summary>
        /// <value>The extracted relation objects from the text.</value>
        [JsonProperty("arguments", NullValueHandling = NullValueHandling.Ignore)]
        public List<RelationArgument> Arguments { get; set; }
    }

}
