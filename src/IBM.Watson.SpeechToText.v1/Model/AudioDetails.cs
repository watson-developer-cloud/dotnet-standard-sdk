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

using IBM.Cloud.SDK.Core;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.Watson.SpeechToText.v1.Model
{
    /// <summary>
    /// AudioDetails.
    /// </summary>
    public class AudioDetails : BaseModel
    {
        /// <summary>
        /// The type of the audio resource:
        /// * `audio` for an individual audio file
        /// * `archive` for an archive (**.zip** or **.tar.gz**) file that contains audio files
        /// * `undetermined` for a resource that the service cannot validate (for example, if the user mistakenly passes
        /// a file that does not contain audio, such as a JPEG file).
        /// </summary>
        /// <value>
        /// The type of the audio resource:
        /// * `audio` for an individual audio file
        /// * `archive` for an archive (**.zip** or **.tar.gz**) file that contains audio files
        /// * `undetermined` for a resource that the service cannot validate (for example, if the user mistakenly passes
        /// a file that does not contain audio, such as a JPEG file).
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
        {
            
            /// <summary>
            /// Enum AUDIO for audio
            /// </summary>
            [EnumMember(Value = "audio")]
            AUDIO,
            
            /// <summary>
            /// Enum ARCHIVE for archive
            /// </summary>
            [EnumMember(Value = "archive")]
            ARCHIVE,
            
            /// <summary>
            /// Enum UNDETERMINED for undetermined
            /// </summary>
            [EnumMember(Value = "undetermined")]
            UNDETERMINED
        }

        /// <summary>
        /// **For an archive-type resource,** the format of the compressed archive:
        /// * `zip` for a **.zip** file
        /// * `gzip` for a **.tar.gz** file
        ///
        /// Omitted for an audio-type resource.
        /// </summary>
        /// <value>
        /// **For an archive-type resource,** the format of the compressed archive:
        /// * `zip` for a **.zip** file
        /// * `gzip` for a **.tar.gz** file
        ///
        /// Omitted for an audio-type resource.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CompressionEnum
        {
            
            /// <summary>
            /// Enum ZIP for zip
            /// </summary>
            [EnumMember(Value = "zip")]
            ZIP,
            
            /// <summary>
            /// Enum GZIP for gzip
            /// </summary>
            [EnumMember(Value = "gzip")]
            GZIP
        }

        /// <summary>
        /// The type of the audio resource:
        /// * `audio` for an individual audio file
        /// * `archive` for an archive (**.zip** or **.tar.gz**) file that contains audio files
        /// * `undetermined` for a resource that the service cannot validate (for example, if the user mistakenly passes
        /// a file that does not contain audio, such as a JPEG file).
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// **For an archive-type resource,** the format of the compressed archive:
        /// * `zip` for a **.zip** file
        /// * `gzip` for a **.tar.gz** file
        ///
        /// Omitted for an audio-type resource.
        /// </summary>
        [JsonProperty("compression", NullValueHandling = NullValueHandling.Ignore)]
        public CompressionEnum? Compression { get; set; }
        /// <summary>
        /// **For an audio-type resource,** the codec in which the audio is encoded. Omitted for an archive-type
        /// resource.
        /// </summary>
        [JsonProperty("codec", NullValueHandling = NullValueHandling.Ignore)]
        public string Codec { get; set; }
        /// <summary>
        /// **For an audio-type resource,** the sampling rate of the audio in Hertz (samples per second). Omitted for an
        /// archive-type resource.
        /// </summary>
        [JsonProperty("frequency", NullValueHandling = NullValueHandling.Ignore)]
        public long? Frequency { get; set; }
    }

}
