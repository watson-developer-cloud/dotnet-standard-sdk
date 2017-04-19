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


namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public class SpeechRecognitionResult
    {
        /// <summary>
        /// Gets or sets if `true`, the result for this utterance is not updated further.
        /// </summary>
        [JsonProperty("final")]
        public bool Final { get; set; }

        /// <summary>
        /// Gets or sets array of alternative transcripts.
        /// </summary>
        [JsonProperty("alternatives")]
        public List<SpeechRecognitionAlternative> Alternatives { get; set; }

        /// <summary>
        /// Gets or sets dictionary (or associative array) whose keys are the strings specified for `keywords` if both that parameter and `keywords_threshold` are specified. A keyword for which no matches are found is omitted from the array. The array is omitted if no keywords are found.
        /// </summary>
        [JsonProperty("keywords_result")]
        public KeywordResults KeywordResults { get; set; }

        /// <summary>
        /// Gets or sets array of word alternative hypotheses found for words of the input audio if a `word_alternatives_threshold` is specified.
        /// </summary>
        [JsonProperty("word_alternatives")]
        public List<WordAlternativeResults> WordAlternativeResults { get; set; }
    }
}