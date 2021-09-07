/**
* (C) Copyright IBM Corp. 2017, 2021.
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

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.Model
{
    /// <summary>
    /// A categorization of the analyzed text.
    /// </summary>
    public class CategoriesResult
    {
        /// <summary>
        /// The path to the category through the multi-level taxonomy hierarchy. For more information about the
        /// categories, see [Categories
        /// hierarchy](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-categories#categories-hierarchy).
        /// </summary>
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
        /// <summary>
        /// Confidence score for the category classification. Higher values indicate greater confidence.
        /// </summary>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public double? Score { get; set; }
        /// <summary>
        /// Information that helps to explain what contributed to the categories result.
        /// </summary>
        [JsonProperty("explanation", NullValueHandling = NullValueHandling.Ignore)]
        public CategoriesResultExplanation Explanation { get; set; }
    }

}
