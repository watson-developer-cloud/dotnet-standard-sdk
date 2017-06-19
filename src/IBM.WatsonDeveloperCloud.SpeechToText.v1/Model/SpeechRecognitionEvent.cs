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
    public class SpeechRecognitionEvent
    {
        /// <summary>
        /// Gets or sets an array that can include interim and final results.
        /// Final results are guaranteed not to change; interim results might
        /// be replaced by further interim results and final results. The
        /// service periodically sends updates to the results list; the
        /// `result_index` is set to the lowest index in the array that has
        /// changed; it is incremented for new results.
        /// </summary>
        [JsonProperty("results")]
        public List<SpeechRecognitionResult> Results { get; set; }

        /// <summary>
        /// Gets or sets an array that identifies which words were spoken by
        /// which speakers in a multi-person exchange. Returned in the
        /// response only if `speaker_labels` is `true`.
        /// </summary>
        [JsonProperty("speaker_labels")]
        public List<SpeakerLabelsResult> SpeakerLabels { get; set; }

        /// <summary>
        /// Gets or sets an index that indicates a change point in the
        /// `results` array.
        /// </summary>
        [JsonProperty("result_index")]
        public int ResultIndex { get; set; }

        /// <summary>
        /// Gets or sets an array of warning messages about invalid query
        /// parameters or JSON fields included with the request. Each warning
        /// includes a descriptive message and a list of invalid argument
        /// strings, for example, `"Unknown arguments:"` or `"Unknown url
        /// query arguments:"` followed by a list of the form
        /// "invalid_arg_1, invalid_arg_2." The request succeeds despite
        /// the warnings.
        /// </summary>
        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }
    }
}