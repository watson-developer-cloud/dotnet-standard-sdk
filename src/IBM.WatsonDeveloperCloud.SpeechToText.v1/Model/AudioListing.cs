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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// AudioListing.
    /// </summary>
    public class AudioListing
    {
        /// <summary>
        /// **For an audio-type resource,** the status of the resource: * `ok` indicates that the service has successfully analyzed the audio data. The data can be used to train the custom model. * `being_processed` indicates that the service is still analyzing the audio data. The service cannot accept requests to add new audio resources or to train the custom model until its analysis is complete. * `invalid` indicates that the audio data is not valid for training the custom model (possibly because it has the wrong format or sampling rate, or because it is corrupted).   Omitted for an archive-type resource.
        /// </summary>
        /// <value>**For an audio-type resource,** the status of the resource: * `ok` indicates that the service has successfully analyzed the audio data. The data can be used to train the custom model. * `being_processed` indicates that the service is still analyzing the audio data. The service cannot accept requests to add new audio resources or to train the custom model until its analysis is complete. * `invalid` indicates that the audio data is not valid for training the custom model (possibly because it has the wrong format or sampling rate, or because it is corrupted).   Omitted for an archive-type resource.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum OK for ok
            /// </summary>
            [EnumMember(Value = "ok")]
            OK,
            
            /// <summary>
            /// Enum BEING_PROCESSED for being_processed
            /// </summary>
            [EnumMember(Value = "being_processed")]
            BEING_PROCESSED,
            
            /// <summary>
            /// Enum INVALID for invalid
            /// </summary>
            [EnumMember(Value = "invalid")]
            INVALID
        }

        /// <summary>
        /// **For an audio-type resource,** the status of the resource: * `ok` indicates that the service has successfully analyzed the audio data. The data can be used to train the custom model. * `being_processed` indicates that the service is still analyzing the audio data. The service cannot accept requests to add new audio resources or to train the custom model until its analysis is complete. * `invalid` indicates that the audio data is not valid for training the custom model (possibly because it has the wrong format or sampling rate, or because it is corrupted).   Omitted for an archive-type resource.
        /// </summary>
        /// <value>**For an audio-type resource,** the status of the resource: * `ok` indicates that the service has successfully analyzed the audio data. The data can be used to train the custom model. * `being_processed` indicates that the service is still analyzing the audio data. The service cannot accept requests to add new audio resources or to train the custom model until its analysis is complete. * `invalid` indicates that the audio data is not valid for training the custom model (possibly because it has the wrong format or sampling rate, or because it is corrupted).   Omitted for an archive-type resource.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// **For an audio-type resource,**  the total seconds of audio in the resource. Omitted for an archive-type resource.
        /// </summary>
        /// <value>**For an audio-type resource,**  the total seconds of audio in the resource. Omitted for an archive-type resource.</value>
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public double? Duration { get; set; }
        /// <summary>
        /// **For an audio-type resource,** the name of the resource. Omitted for an archive-type resource.
        /// </summary>
        /// <value>**For an audio-type resource,** the name of the resource. Omitted for an archive-type resource.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// **For an audio-type resource,** an `AudioDetails` object that provides detailed information about the resource. The object is empty until the service finishes processing the audio. Omitted for an archive-type resource.
        /// </summary>
        /// <value>**For an audio-type resource,** an `AudioDetails` object that provides detailed information about the resource. The object is empty until the service finishes processing the audio. Omitted for an archive-type resource.</value>
        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public AudioDetails Details { get; set; }
        /// <summary>
        /// **For an archive-type resource,** an object of type `AudioResource` that provides information about the resource. Omitted for an audio-type resource.
        /// </summary>
        /// <value>**For an archive-type resource,** an object of type `AudioResource` that provides information about the resource. Omitted for an audio-type resource.</value>
        [JsonProperty("container", NullValueHandling = NullValueHandling.Ignore)]
        public AudioResource Container { get; set; }
        /// <summary>
        /// **For an archive-type resource,** an array of `AudioResource` objects that provides information about the audio-type resources that are contained in the resource. Omitted for an audio-type resource.
        /// </summary>
        /// <value>**For an archive-type resource,** an array of `AudioResource` objects that provides information about the audio-type resources that are contained in the resource. Omitted for an audio-type resource.</value>
        [JsonProperty("audio", NullValueHandling = NullValueHandling.Ignore)]
        public List<AudioResource> Audio { get; set; }
    }

}
