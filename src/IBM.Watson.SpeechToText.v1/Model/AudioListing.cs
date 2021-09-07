/**
* (C) Copyright IBM Corp. 2018, 2021.
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
    /// Information about an audio resource from a custom acoustic model.
    /// </summary>
    public class AudioListing
    {
        /// <summary>
        /// _For an audio-type resource_, the status of the resource:
        /// * `ok`: The service successfully analyzed the audio data. The data can be used to train the custom model.
        /// * `being_processed`: The service is still analyzing the audio data. The service cannot accept requests to
        /// add new audio resources or to train the custom model until its analysis is complete.
        /// * `invalid`: The audio data is not valid for training the custom model (possibly because it has the wrong
        /// format or sampling rate, or because it is corrupted).
        ///
        /// Omitted for an archive-type resource.
        /// </summary>
        public class StatusEnumValue
        {
            /// <summary>
            /// Constant OK for ok
            /// </summary>
            public const string OK = "ok";
            /// <summary>
            /// Constant BEING_PROCESSED for being_processed
            /// </summary>
            public const string BEING_PROCESSED = "being_processed";
            /// <summary>
            /// Constant INVALID for invalid
            /// </summary>
            public const string INVALID = "invalid";
            
        }

        /// <summary>
        /// _For an audio-type resource_, the status of the resource:
        /// * `ok`: The service successfully analyzed the audio data. The data can be used to train the custom model.
        /// * `being_processed`: The service is still analyzing the audio data. The service cannot accept requests to
        /// add new audio resources or to train the custom model until its analysis is complete.
        /// * `invalid`: The audio data is not valid for training the custom model (possibly because it has the wrong
        /// format or sampling rate, or because it is corrupted).
        ///
        /// Omitted for an archive-type resource.
        /// Constants for possible values can be found using AudioListing.StatusEnumValue
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// _For an audio-type resource_, the total seconds of audio in the resource. Omitted for an archive-type
        /// resource.
        /// </summary>
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? Duration { get; set; }
        /// <summary>
        /// _For an audio-type resource_, the user-specified name of the resource. Omitted for an archive-type resource.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// _For an audio-type resource_, an `AudioDetails` object that provides detailed information about the
        /// resource. The object is empty until the service finishes processing the audio. Omitted for an archive-type
        /// resource.
        /// </summary>
        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public AudioDetails Details { get; set; }
        /// <summary>
        /// _For an archive-type resource_, an object of type `AudioResource` that provides information about the
        /// resource. Omitted for an audio-type resource.
        /// </summary>
        [JsonProperty("container", NullValueHandling = NullValueHandling.Ignore)]
        public AudioResource Container { get; set; }
        /// <summary>
        /// _For an archive-type resource_, an array of `AudioResource` objects that provides information about the
        /// audio-type resources that are contained in the resource. Omitted for an audio-type resource.
        /// </summary>
        [JsonProperty("audio", NullValueHandling = NullValueHandling.Ignore)]
        public List<AudioResource> Audio { get; set; }
    }

}
