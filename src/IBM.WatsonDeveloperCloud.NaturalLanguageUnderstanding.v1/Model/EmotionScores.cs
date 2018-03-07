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
    /// EmotionScores.
    /// </summary>
    public class EmotionScores
    {
        /// <summary>
        /// Anger score from 0 to 1. A higher score means that the text is more likely to convey anger.
        /// </summary>
        /// <value>Anger score from 0 to 1. A higher score means that the text is more likely to convey anger.</value>
        [JsonProperty("anger", NullValueHandling = NullValueHandling.Ignore)]
        public float? Anger { get; set; }
        /// <summary>
        /// Disgust score from 0 to 1. A higher score means that the text is more likely to convey disgust.
        /// </summary>
        /// <value>Disgust score from 0 to 1. A higher score means that the text is more likely to convey disgust.</value>
        [JsonProperty("disgust", NullValueHandling = NullValueHandling.Ignore)]
        public float? Disgust { get; set; }
        /// <summary>
        /// Fear score from 0 to 1. A higher score means that the text is more likely to convey fear.
        /// </summary>
        /// <value>Fear score from 0 to 1. A higher score means that the text is more likely to convey fear.</value>
        [JsonProperty("fear", NullValueHandling = NullValueHandling.Ignore)]
        public float? Fear { get; set; }
        /// <summary>
        /// Joy score from 0 to 1. A higher score means that the text is more likely to convey joy.
        /// </summary>
        /// <value>Joy score from 0 to 1. A higher score means that the text is more likely to convey joy.</value>
        [JsonProperty("joy", NullValueHandling = NullValueHandling.Ignore)]
        public float? Joy { get; set; }
        /// <summary>
        /// Sadness score from 0 to 1. A higher score means that the text is more likely to convey sadness.
        /// </summary>
        /// <value>Sadness score from 0 to 1. A higher score means that the text is more likely to convey sadness.</value>
        [JsonProperty("sadness", NullValueHandling = NullValueHandling.Ignore)]
        public float? Sadness { get; set; }
    }

}
