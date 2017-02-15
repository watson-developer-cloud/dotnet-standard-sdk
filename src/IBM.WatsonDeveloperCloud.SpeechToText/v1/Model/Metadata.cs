

using Newtonsoft.Json;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public class Metadata
    {
        /// <summary>
        /// The MIME type of the data in the following parts. All data parts must have the same MIME type. = ['audio/flac', 'audio/l16', 'audio/wav', 'audio/mulaw', 'audio/basic', 'audio/ogg;codecs=opus']
        /// </summary>
        [JsonProperty("part_content_type")]
        public string PartContentType { get; set; }

        /// <summary>
        /// The number of audio data parts (audio files) sent with the request. Server-side end-of-stream detection is applied to the last (and possibly the only) data part. If omitted, the number of parts is determined from the request itself. 
        /// </summary>
        [JsonProperty("data_parts_count", NullValueHandling = NullValueHandling.Ignore)]
        public int? DataPartsCount { get; set; }

        /// <summary>
        /// The sequence ID for all data parts of this recognition task in the form of a user-specified integer. If omitted, no sequence ID is associated with the recognition task. Used only for session-based requests. 
        /// </summary>
        [JsonProperty("sequence_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequenceId { get; set; }

        /// <summary>
        /// If true, multiple final results that represent consecutive phrases separated by pauses are returned. If false (the default), recognition ends after the first "end of speech" incident is detected. 
        /// </summary>
        [JsonProperty("continuous", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Continuous { get; set; }

        /// <summary>
        /// The time in seconds after which, if only silence (no speech) is detected in submitted audio, the connection is closed with a 400 error and, for session-based methods, with session_closed set to true. Useful for stopping audio submission from a live microphone when a user simply walks away. Use -1 for infinity. See also the continuous parameter.
        /// </summary>
        [JsonProperty("inactivity_timeout", NullValueHandling = NullValueHandling.Ignore)]
        public double? InactivityTimeout { get; set; }

        /// <summary>
        /// Array of keyword strings to spot in the audio. Each keyword string can include one or more tokens. Keywords are spotted only in the final hypothesis, not in interim results. Omit the parameter or specify an empty array if you do not need to spot keywords. 
        /// </summary>
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Keywords { get; set; }

        /// <summary>
        /// Confidence value that is the lower bound for spotting a keyword. A word is considered to match a keyword if its confidence is greater than or equal to the threshold. Specify a probability between 0 and 1 inclusive. No keyword spotting is performed if you omit the parameter. If you specify a threshold, you must also specify one or more keywords. 
        /// </summary>
        [JsonProperty("keywords_threshold", NullValueHandling = NullValueHandling.Ignore)]
        public double? KeywordsThreshold { get; set; }

        /// <summary>
        /// Maximum number of alternative transcripts to be returned. By default, a single transcription is returned.
        /// </summary>
        [JsonProperty("max_alternatives", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxAlternatives { get; set; }

        /// <summary>
        /// Confidence value that is the lower bound for identifying a hypothesis as a possible word alternative (also known as "Confusion Networks"). An alternative word is considered if its confidence is greater than or equal to the threshold. Specify a probability between 0 and 1 inclusive. No alternative words are computed if you omit the parameter. 
        /// </summary>
        [JsonProperty("word_alternatives_threshold", NullValueHandling = NullValueHandling.Ignore)]
        public double? WordAlternativesThreshold { get; set; }

        /// <summary>
        /// If true, a confidence measure in the range 0 to 1 is returned for each word. 
        /// </summary>
        [JsonProperty("word_confidence", NullValueHandling = NullValueHandling.Ignore)]
        public bool? WordConfidence { get; set; }

        /// <summary>
        /// If true, time alignment for each word is returned.
        /// </summary>
        [JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Timestamps { get; set; }

        /// <summary>
        /// If true (the default), filters profanity from all output except for keyword results by replacing inappropriate words with a series of asterisks. Set the parameter to false to return results with no censoring. Applies to US English transcription only.
        /// </summary>
        [JsonProperty("profanity_filter", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ProfanityFilter { get; set; }

        /// <summary>
        /// If true, converts dates, times, series of digits and numbers, phone numbers, currency values, and Internet addresses into more readable, conventional representations in the final transcript of a recognition request. If false (the default), no formatting is performed. Applies to US English transcription only.
        /// </summary>
        [JsonProperty("smart_formatting", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SmartFormatting { get; set; }

        /// <summary>
        /// Indicates whether labels that identify which words were spoken by which participants in a multi-person exchange are to be included in the response. If true, speaker labels are returned; if false (the default), they are not. Speaker labels can be returned only for the following language models: en-US_NarrowbandModel, es-ES_NarrowbandModel, and ja-JP_NarrowbandModel. Setting speaker_labels to true forces the continuous and timestamps parameters to be true, as well, regardless of whether the user specifies false for the parameters.
        /// </summary>
        [JsonProperty("speaker_labels", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SpeakerLabels { get; set; }
    }
}