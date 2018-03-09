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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model
{
    /// <summary>
    /// Profile.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// The language model that was used to process the input.
        /// </summary>
        /// <value>The language model that was used to process the input.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ProcessedLanguageEnum
        {
            
            /// <summary>
            /// Enum AR for ar
            /// </summary>
            [EnumMember(Value = "ar")]
            AR,
            
            /// <summary>
            /// Enum EN for en
            /// </summary>
            [EnumMember(Value = "en")]
            EN,
            
            /// <summary>
            /// Enum ES for es
            /// </summary>
            [EnumMember(Value = "es")]
            ES,
            
            /// <summary>
            /// Enum JA for ja
            /// </summary>
            [EnumMember(Value = "ja")]
            JA,
            
            /// <summary>
            /// Enum KO for ko
            /// </summary>
            [EnumMember(Value = "ko")]
            KO
        }

        /// <summary>
        /// The language model that was used to process the input.
        /// </summary>
        /// <value>The language model that was used to process the input.</value>
        [JsonProperty("processed_language", NullValueHandling = NullValueHandling.Ignore)]
        public ProcessedLanguageEnum? ProcessedLanguage { get; set; }
        /// <summary>
        /// The number of words that were found in the input.
        /// </summary>
        /// <value>The number of words that were found in the input.</value>
        [JsonProperty("word_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? WordCount { get; set; }
        /// <summary>
        /// When guidance is appropriate, a string that provides a message that indicates the number of words found and where that value falls in the range of required or suggested number of words.
        /// </summary>
        /// <value>When guidance is appropriate, a string that provides a message that indicates the number of words found and where that value falls in the range of required or suggested number of words.</value>
        [JsonProperty("word_count_message", NullValueHandling = NullValueHandling.Ignore)]
        public string WordCountMessage { get; set; }
        /// <summary>
        /// Detailed results for the Big Five personality characteristics (dimensions and facets) inferred from the input text.
        /// </summary>
        /// <value>Detailed results for the Big Five personality characteristics (dimensions and facets) inferred from the input text.</value>
        [JsonProperty("personality", NullValueHandling = NullValueHandling.Ignore)]
        public List<Trait> Personality { get; set; }
        /// <summary>
        /// Detailed results for the Needs characteristics inferred from the input text.
        /// </summary>
        /// <value>Detailed results for the Needs characteristics inferred from the input text.</value>
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<Trait> Values { get; set; }
        /// <summary>
        /// Detailed results for the Values characteristics inferred from the input text.
        /// </summary>
        /// <value>Detailed results for the Values characteristics inferred from the input text.</value>
        [JsonProperty("needs", NullValueHandling = NullValueHandling.Ignore)]
        public List<Trait> Needs { get; set; }
        /// <summary>
        /// For JSON content that is timestamped, detailed results about the social behavior disclosed by the input in terms of temporal characteristics. The results include information about the distribution of the content over the days of the week and the hours of the day.
        /// </summary>
        /// <value>For JSON content that is timestamped, detailed results about the social behavior disclosed by the input in terms of temporal characteristics. The results include information about the distribution of the content over the days of the week and the hours of the day.</value>
        [JsonProperty("behavior", NullValueHandling = NullValueHandling.Ignore)]
        public List<Behavior> Behavior { get; set; }
        /// <summary>
        /// If the `consumption_preferences` query parameter is `true`, detailed results for each category of consumption preferences. Each element of the array provides information inferred from the input text for the individual preferences of that category.
        /// </summary>
        /// <value>If the `consumption_preferences` query parameter is `true`, detailed results for each category of consumption preferences. Each element of the array provides information inferred from the input text for the individual preferences of that category.</value>
        [JsonProperty("consumption_preferences", NullValueHandling = NullValueHandling.Ignore)]
        public List<ConsumptionPreferencesCategory> ConsumptionPreferences { get; set; }
        /// <summary>
        /// Warning messages associated with the input text submitted with the request. The array is empty if the input generated no warnings.
        /// </summary>
        /// <value>Warning messages associated with the input text submitted with the request. The array is empty if the input generated no warnings.</value>
        [JsonProperty("warnings", NullValueHandling = NullValueHandling.Ignore)]
        public List<Warning> Warnings { get; set; }
    }

}
