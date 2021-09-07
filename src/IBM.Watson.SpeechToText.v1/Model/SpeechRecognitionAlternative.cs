/**
* (C) Copyright IBM Corp. 2021.
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

namespace IBM.Watson.SpeechToText.v1.Model
{
    /// <summary>
    /// An alternative transcript from speech recognition results.
    /// </summary>
    public class SpeechRecognitionAlternative
    {
        /// <summary>
        /// A transcription of the audio.
        /// </summary>
        [JsonProperty("transcript", NullValueHandling = NullValueHandling.Ignore)]
        public string Transcript { get; set; }
        /// <summary>
        /// A score that indicates the service's confidence in the transcript in the range of 0.0 to 1.0. The service
        /// returns a confidence score only for the best alternative and only with results marked as final.
        /// </summary>
        [JsonProperty("confidence", NullValueHandling = NullValueHandling.Ignore)]
        public double? Confidence { get; set; }
        /// <summary>
        /// Time alignments for each word from the transcript as a list of lists. Each inner list consists of three
        /// elements: the word followed by its start and end time in seconds, for example:
        /// `[["hello",0.0,1.2],["world",1.2,2.5]]`. Timestamps are returned only for the best alternative.
        /// </summary>
        [JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
        public List<List<string>> Timestamps { get; set; }
        /// <summary>
        /// A confidence score for each word of the transcript as a list of lists. Each inner list consists of two
        /// elements: the word and its confidence score in the range of 0.0 to 1.0, for example:
        /// `[["hello",0.95],["world",0.866]]`. Confidence scores are returned only for the best alternative and only
        /// with results marked as final.
        /// </summary>
        [JsonProperty("word_confidence", NullValueHandling = NullValueHandling.Ignore)]
        public List<List<string>> WordConfidence { get; set; }
    }

}
