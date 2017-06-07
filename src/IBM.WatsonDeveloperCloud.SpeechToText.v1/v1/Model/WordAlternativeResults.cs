﻿/**
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
    public class WordAlternativeResults
    {
        /// <summary>
        /// Gets or sets start time in seconds of the word from the input audio that corresponds to the word alternatives.
        /// </summary>
        [JsonProperty("start_time")]
        public double StartTime { get; set; }

        /// <summary>
        /// Gets or sets end time in seconds of the word from the input audio that corresponds to the word alternatives.
        /// </summary>
        [JsonProperty("end_time")]
        public double Endtime { get; set; }

        /// <summary>
        /// Gets or sets array of word alternative hypotheses for a word from the input audio.
        /// </summary>
        [JsonProperty("alternatives")]
        public List<WordAlternativeResult> Alternatives { get; set; }
    }
}