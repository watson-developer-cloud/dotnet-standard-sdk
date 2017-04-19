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
    public class SpeechRecognitionAlternative
    {
        /// <summary>
        /// Gets or sets transcription of the audio.
        /// </summary>
        [JsonProperty("transcript")]
        public string Transcript { get; set; }

        /// <summary>
        /// Gets or sets confidence score of the transcript in the range of 0 to 1. Available only for the best alternative and only in results marked as final.
        /// </summary>
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// Gets or sets time alignments for each word from transcript as a list of lists. Each inner list consists of three elements: the word followed by its start and end time in seconds. Example: `[["hello",0.0,1.2],["world",1.2,2.5]]`. Available only for the best alternative.
        /// </summary>
        [JsonProperty("timestamps")]
        public List<string> Timestamps { get; set; }

        /// <summary>
        /// Gets or sets confidence score for each word of the transcript as a list of lists. Each inner list consists of two elements: the word and its confidence score in the range of 0 to 1. Example: `[["hello",0.95],["world",0.866]]`. Available only for the best alternative and only in results marked as final.
        /// </summary>
        [JsonProperty("word_confidence")]
        public List<string> WordConfidence { get; set; }
    }
}