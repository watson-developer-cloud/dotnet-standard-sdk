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
    /// Analysis features and options.
    /// </summary>
    public class Features : BaseModel
    {
        /// <summary>
        /// Whether or not to return the concepts that are mentioned in the analyzed text.
        /// </summary>
        [JsonProperty("concepts", NullValueHandling = NullValueHandling.Ignore)]
        public ConceptsOptions Concepts { get; set; }
        /// <summary>
        /// Whether or not to extract the emotions implied in the analyzed text.
        /// </summary>
        [JsonProperty("emotion", NullValueHandling = NullValueHandling.Ignore)]
        public EmotionOptions Emotion { get; set; }
        /// <summary>
        /// Whether or not to extract detected entity objects from the analyzed text.
        /// </summary>
        [JsonProperty("entities", NullValueHandling = NullValueHandling.Ignore)]
        public EntitiesOptions Entities { get; set; }
        /// <summary>
        /// Whether or not to return the keywords in the analyzed text.
        /// </summary>
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        public KeywordsOptions Keywords { get; set; }
        /// <summary>
        /// Whether or not the author, publication date, and title of the analyzed text should be returned. This
        /// parameter is only available for URL and HTML input.
        /// </summary>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public MetadataOptions Metadata { get; set; }
        /// <summary>
        /// Whether or not to return the relationships between detected entities in the analyzed text.
        /// </summary>
        [JsonProperty("relations", NullValueHandling = NullValueHandling.Ignore)]
        public RelationsOptions Relations { get; set; }
        /// <summary>
        /// Whether or not to return the subject-action-object relations from the analyzed text.
        /// </summary>
        [JsonProperty("semantic_roles", NullValueHandling = NullValueHandling.Ignore)]
        public SemanticRolesOptions SemanticRoles { get; set; }
        /// <summary>
        /// Whether or not to return the overall sentiment of the analyzed text.
        /// </summary>
        [JsonProperty("sentiment", NullValueHandling = NullValueHandling.Ignore)]
        public SentimentOptions Sentiment { get; set; }
        /// <summary>
        /// Whether or not to return the high level category the content is categorized as (i.e. news, art).
        /// </summary>
        [JsonProperty("categories", NullValueHandling = NullValueHandling.Ignore)]
        public CategoriesOptions Categories { get; set; }
    }

}
