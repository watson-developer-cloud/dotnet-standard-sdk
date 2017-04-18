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

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public class SpeakerLabelsResult
    {
        /// <summary>
        /// Gets or sets the start time of a word from the transcript. The
        /// value matches the start time of a word from the `timestamps`
        /// array.
        /// </summary>
        [JsonProperty("from")]
        public double From { get; set; }

        /// <summary>
        /// Gets or sets the end time of a word from the transcript. The value
        /// matches the end time of a word from the `timestamps` array.
        /// </summary>
        [JsonProperty("to")]
        public double To { get; set; }

        /// <summary>
        /// Gets or sets the numeric identifier that the service assigns to a
        /// speaker from the audio. Speaker IDs begin at `0` initially but
        /// can evolve and change across interim results and between interim
        /// and final results as the service processes the audio. They are
        /// not guaranteed to be sequential, contiguous, or ordered.
        /// </summary>
        [JsonProperty("speaker")]
        public int Speaker { get; set; }

        /// <summary>
        /// Gets or sets a score that indicates how confident the service is
        /// in its identification of the speaker in the range of 0 to 1.
        /// </summary>
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// Gets or sets an indication of whether the service might further
        /// change word and speaker-label results. A value of `true` means
        /// that the service guarantees not to send any further updates for
        /// the current or any preceding results; `false` means that the
        /// service might send further updates to the results.
        /// </summary>
        [JsonProperty("final")]
        public bool Final { get; set; }
    }
}