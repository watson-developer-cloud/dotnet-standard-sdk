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
    /// Results of the analysis, organized by feature.
    /// </summary>
    public class AnalysisResults
    {
        /// <summary>
        /// Language used to analyze the text.
        /// </summary>
        /// <value>Language used to analyze the text.</value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// Text that was used in the analysis.
        /// </summary>
        /// <value>Text that was used in the analysis.</value>
        [JsonProperty("analyzed_text", NullValueHandling = NullValueHandling.Ignore)]
        public string AnalyzedText { get; set; }
        /// <summary>
        /// URL that was used to retrieve HTML content.
        /// </summary>
        /// <value>URL that was used to retrieve HTML content.</value>
        [JsonProperty("retrieved_url", NullValueHandling = NullValueHandling.Ignore)]
        public string RetrievedUrl { get; set; }
        /// <summary>
        /// API usage information for the request.
        /// </summary>
        /// <value>API usage information for the request.</value>
        [JsonProperty("usage", NullValueHandling = NullValueHandling.Ignore)]
        public Usage Usage { get; set; }
        /// <summary>
        /// The general concepts referenced or alluded to in the specified content.
        /// </summary>
        /// <value>The general concepts referenced or alluded to in the specified content.</value>
        [JsonProperty("concepts", NullValueHandling = NullValueHandling.Ignore)]
        public List<ConceptsResult> Concepts { get; set; }
        /// <summary>
        /// The important entities in the specified content.
        /// </summary>
        /// <value>The important entities in the specified content.</value>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public List<EntitiesResult> Entities { get; set; }
        /// <summary>
        /// The important keywords in content organized by relevance.
        /// </summary>
        /// <value>The important keywords in content organized by relevance.</value>
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        public List<KeywordsResult> Keywords { get; set; }
        /// <summary>
        /// The hierarchical 5-level taxonomy the content is categorized into.
        /// </summary>
        /// <value>The hierarchical 5-level taxonomy the content is categorized into.</value>
        [JsonProperty("categories", NullValueHandling = NullValueHandling.Ignore)]
        public List<CategoriesResult> Categories { get; set; }
        /// <summary>
        /// The anger, disgust, fear, joy, or sadness conveyed by the content.
        /// </summary>
        /// <value>The anger, disgust, fear, joy, or sadness conveyed by the content.</value>
        [JsonProperty("emotion", NullValueHandling = NullValueHandling.Ignore)]
        public EmotionResult Emotion { get; set; }
        /// <summary>
        /// The metadata holds author information, publication date and the title of the text/HTML content.
        /// </summary>
        /// <value>The metadata holds author information, publication date and the title of the text/HTML content.</value>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public MetadataResult Metadata { get; set; }
        /// <summary>
        /// The relationships between entities in the content.
        /// </summary>
        /// <value>The relationships between entities in the content.</value>
        [JsonProperty("relations", NullValueHandling = NullValueHandling.Ignore)]
        public List<RelationsResult> Relations { get; set; }
        /// <summary>
        /// The subjects of actions and the objects the actions act upon.
        /// </summary>
        /// <value>The subjects of actions and the objects the actions act upon.</value>
        [JsonProperty("semantic_roles", NullValueHandling = NullValueHandling.Ignore)]
        public List<SemanticRolesResult> SemanticRoles { get; set; }
        /// <summary>
        /// The sentiment of the content.
        /// </summary>
        /// <value>The sentiment of the content.</value>
        [JsonProperty("sentiment", NullValueHandling = NullValueHandling.Ignore)]
        public SentimentResult Sentiment { get; set; }
    }

}
