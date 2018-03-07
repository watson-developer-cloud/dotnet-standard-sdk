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
    /// The most important keywords in the content, organized by relevance.
    /// </summary>
    public class KeywordsResult
    {
        /// <summary>
        /// Relevance score from 0 to 1. Higher values indicate greater relevance.
        /// </summary>
        /// <value>Relevance score from 0 to 1. Higher values indicate greater relevance.</value>
        [JsonProperty("relevance", NullValueHandling = NullValueHandling.Ignore)]
        public float? Relevance { get; set; }
        /// <summary>
        /// The keyword text.
        /// </summary>
        /// <value>The keyword text.</value>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// Emotion analysis results for the keyword, enabled with the "emotion" option.
        /// </summary>
        /// <value>Emotion analysis results for the keyword, enabled with the "emotion" option.</value>
        [JsonProperty("emotion", NullValueHandling = NullValueHandling.Ignore)]
        public EmotionScores Emotion { get; set; }
        /// <summary>
        /// Sentiment analysis results for the keyword, enabled with the "sentiment" option.
        /// </summary>
        /// <value>Sentiment analysis results for the keyword, enabled with the "sentiment" option.</value>
        [JsonProperty("sentiment", NullValueHandling = NullValueHandling.Ignore)]
        public FeatureSentimentResults Sentiment { get; set; }
    }

}
