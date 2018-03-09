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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// SpeechRecognitionAlternative.
    /// </summary>
    public class SpeechRecognitionAlternative
    {
        /// <summary>
        /// A transcription of the audio.
        /// </summary>
        /// <value>A transcription of the audio.</value>
        [JsonProperty("transcript", NullValueHandling = NullValueHandling.Ignore)]
        public string Transcript { get; set; }
        /// <summary>
        /// A score that indicates the service's confidence in the transcript in the range of 0 to 1. Available only for the best alternative and only in results marked as final.
        /// </summary>
        /// <value>A score that indicates the service's confidence in the transcript in the range of 0 to 1. Available only for the best alternative and only in results marked as final.</value>
        [JsonProperty("confidence", NullValueHandling = NullValueHandling.Ignore)]
        public double? Confidence { get; set; }
        /// <summary>
        /// Time alignments for each word from the transcript as a list of lists. Each inner list consists of three elements: the word followed by its start and end time in seconds. Example: `[["hello",0.0,1.2],["world",1.2,2.5]]`. Available only for the best alternative.
        /// </summary>
        /// <value>Time alignments for each word from the transcript as a list of lists. Each inner list consists of three elements: the word followed by its start and end time in seconds. Example: `[["hello",0.0,1.2],["world",1.2,2.5]]`. Available only for the best alternative.</value>
        [JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Timestamps { get; set; }
        /// <summary>
        /// A confidence score for each word of the transcript as a list of lists. Each inner list consists of two elements: the word and its confidence score in the range of 0 to 1. Example: `[["hello",0.95],["world",0.866]]`. Available only for the best alternative and only in results marked as final.
        /// </summary>
        /// <value>A confidence score for each word of the transcript as a list of lists. Each inner list consists of two elements: the word and its confidence score in the range of 0 to 1. Example: `[["hello",0.95],["world",0.866]]`. Available only for the best alternative and only in results marked as final.</value>
        [JsonProperty("word_confidence", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> WordConfidence { get; set; }
    }

}
