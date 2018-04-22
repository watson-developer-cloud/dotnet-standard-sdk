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

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model
{
    /// <summary>
    /// ToneChatScore.
    /// </summary>
    public class ToneChatScore : BaseModel
    {
        /// <summary>
        /// The score for the tone in the range of 0.5 to 1. A score greater than 0.75 indicates a high likelihood that the tone is perceived in the utterance.
        /// </summary>
        /// <value>The score for the tone in the range of 0.5 to 1. A score greater than 0.75 indicates a high likelihood that the tone is perceived in the utterance.</value>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public double? Score { get; set; }
        /// <summary>
        /// The unique, non-localized identifier of the tone for the results. The service can return results for the following tone IDs: `sad`, `frustrated`, `satisfied`, `excited`, `polite`, `impolite`, and `sympathetic`. The service returns results only for tones whose scores meet a minimum threshold of 0.5.
        /// </summary>
        /// <value>The unique, non-localized identifier of the tone for the results. The service can return results for the following tone IDs: `sad`, `frustrated`, `satisfied`, `excited`, `polite`, `impolite`, and `sympathetic`. The service returns results only for tones whose scores meet a minimum threshold of 0.5.</value>
        [JsonProperty("tone_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ToneId { get; set; }
        /// <summary>
        /// The user-visible, localized name of the tone.
        /// </summary>
        /// <value>The user-visible, localized name of the tone.</value>
        [JsonProperty("tone_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ToneName { get; set; }
    }

}
