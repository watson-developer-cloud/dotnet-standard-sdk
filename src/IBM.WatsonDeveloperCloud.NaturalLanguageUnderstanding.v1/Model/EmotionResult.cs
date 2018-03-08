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
    /// The detected anger, disgust, fear, joy, or sadness that is conveyed by the content. Emotion information can be returned for detected entities, keywords, or user-specified target phrases found in the text.
    /// </summary>
    public class EmotionResult
    {
        /// <summary>
        /// The returned emotion results across the document.
        /// </summary>
        /// <value>The returned emotion results across the document.</value>
        [JsonProperty("document", NullValueHandling = NullValueHandling.Ignore)]
        public DocumentEmotionResults Document { get; set; }
        /// <summary>
        /// The returned emotion results per specified target.
        /// </summary>
        /// <value>The returned emotion results per specified target.</value>
        [JsonProperty("targets", NullValueHandling = NullValueHandling.Ignore)]
        public List<TargetedEmotionResults> Targets { get; set; }
    }

}
