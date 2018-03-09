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

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model
{
    /// <summary>
    /// The hierarchical 5-level taxonomy the content is categorized into.
    /// </summary>
    public class CategoriesResult
    {
        /// <summary>
        /// The path to the category through the taxonomy hierarchy.
        /// </summary>
        /// <value>The path to the category through the taxonomy hierarchy.</value>
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
        /// <summary>
        /// Confidence score for the category classification. Higher values indicate greater confidence.
        /// </summary>
        /// <value>Confidence score for the category classification. Higher values indicate greater confidence.</value>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public float? Score { get; set; }
    }

}
