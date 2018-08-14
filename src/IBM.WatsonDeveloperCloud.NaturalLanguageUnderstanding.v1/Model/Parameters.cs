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
    /// An object containing request parameters.
    /// </summary>
    public class Parameters : BaseModel
    {
        /// <summary>
        /// The plain text to analyze.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// The HTML file to analyze.
        /// </summary>
        [JsonProperty("html", NullValueHandling = NullValueHandling.Ignore)]
        public string Html { get; set; }
        /// <summary>
        /// The web page to analyze.
        /// </summary>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        /// <summary>
        /// Specific features to analyze the document for.
        /// </summary>
        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public Features Features { get; set; }
        /// <summary>
        /// Remove website elements, such as links, ads, etc.
        /// </summary>
        [JsonProperty("clean", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Clean { get; set; }
        /// <summary>
        /// XPath query for targeting nodes in HTML.
        /// </summary>
        [JsonProperty("xpath", NullValueHandling = NullValueHandling.Ignore)]
        public string Xpath { get; set; }
        /// <summary>
        /// Whether to use raw HTML content if text cleaning fails.
        /// </summary>
        [JsonProperty("fallback_to_raw", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FallbackToRaw { get; set; }
        /// <summary>
        /// Whether or not to return the analyzed text.
        /// </summary>
        [JsonProperty("return_analyzed_text", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReturnAnalyzedText { get; set; }
        /// <summary>
        /// ISO 639-1 code indicating the language to use in the analysis.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// Sets the maximum number of characters that are processed by the service.
        /// </summary>
        [JsonProperty("limit_text_characters", NullValueHandling = NullValueHandling.Ignore)]
        public long? LimitTextCharacters { get; set; }
    }

}
