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
    /// Whether or not to return important people, places, geopolitical, and other entities detected in the analyzed content.
    /// </summary>
    public class EntitiesOptions
    {
        /// <summary>
        /// Maximum number of entities to return.
        /// </summary>
        /// <value>Maximum number of entities to return.</value>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public long? Limit { get; set; }
        /// <summary>
        /// Set this to true to return locations of entity mentions.
        /// </summary>
        /// <value>Set this to true to return locations of entity mentions.</value>
        [JsonProperty("mentions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Mentions { get; set; }
        /// <summary>
        /// Enter a custom model ID to override the standard entity detection model.
        /// </summary>
        /// <value>Enter a custom model ID to override the standard entity detection model.</value>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
        /// <summary>
        /// Set this to true to return sentiment information for detected entities.
        /// </summary>
        /// <value>Set this to true to return sentiment information for detected entities.</value>
        [JsonProperty("sentiment", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Sentiment { get; set; }
        /// <summary>
        /// Set this to true to analyze emotion for detected keywords.
        /// </summary>
        /// <value>Set this to true to analyze emotion for detected keywords.</value>
        [JsonProperty("emotion", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Emotion { get; set; }
    }

}
