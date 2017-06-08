/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model
{
    /// <summary>
    /// SentenceAnalysis.
    /// </summary>
    public class SentenceAnalysis
    {
        /// <summary>
        /// The unique identifier of a sentence of the input content. The first sentence has ID 0, and the ID of each subsequent sentence is incremented by one.
        /// </summary>
        /// <value>The unique identifier of a sentence of the input content. The first sentence has ID 0, and the ID of each subsequent sentence is incremented by one.</value>
        [JsonProperty("sentence_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SentenceId { get; set; }
        /// <summary>
        /// The text of the input sentence.
        /// </summary>
        /// <value>The text of the input sentence.</value>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// The offset of the first character of the sentence in the overall input content.
        /// </summary>
        /// <value>The offset of the first character of the sentence in the overall input content.</value>
        [JsonProperty("input_from", NullValueHandling = NullValueHandling.Ignore)]
        public int? InputFrom { get; set; }
        /// <summary>
        /// The offset of the last character of the sentence in the overall input content.
        /// </summary>
        /// <value>The offset of the last character of the sentence in the overall input content.</value>
        [JsonProperty("input_to", NullValueHandling = NullValueHandling.Ignore)]
        public int? InputTo { get; set; }
        /// <summary>
        /// An array of `ToneCategory` objects that provides the results for the tone analysis of the sentence. The service returns results only for the tones specified with the `tones` parameter of the request.
        /// </summary>
        /// <value>An array of `ToneCategory` objects that provides the results for the tone analysis of the sentence. The service returns results only for the tones specified with the `tones` parameter of the request.</value>
        [JsonProperty("tone_categories", NullValueHandling = NullValueHandling.Ignore)]
        public List<ToneCategory> ToneCategories { get; set; }
    }

}
