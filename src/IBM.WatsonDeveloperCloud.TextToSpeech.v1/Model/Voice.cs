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

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    /// <summary>
    /// Voice.
    /// </summary>
    public class Voice
    {
        /// <summary>
        /// The URI of the voice.
        /// </summary>
        /// <value>The URI of the voice.</value>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        /// <summary>
        /// The gender of the voice: `male` or `female`.
        /// </summary>
        /// <value>The gender of the voice: `male` or `female`.</value>
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }
        /// <summary>
        /// The name of the voice. Use this as the voice identifier in all requests.
        /// </summary>
        /// <value>The name of the voice. Use this as the voice identifier in all requests.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The language and region of the voice (for example, `en-US`).
        /// </summary>
        /// <value>The language and region of the voice (for example, `en-US`).</value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// A textual description of the voice.
        /// </summary>
        /// <value>A textual description of the voice.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// If `true`, the voice can be customized; if `false`, the voice cannot be customized. (Same as `custom_pronunciation`; maintained for backward compatibility.).
        /// </summary>
        /// <value>If `true`, the voice can be customized; if `false`, the voice cannot be customized. (Same as `custom_pronunciation`; maintained for backward compatibility.).</value>
        [JsonProperty("customizable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Customizable { get; set; }
        /// <summary>
        /// Describes the additional service features supported with the voice.
        /// </summary>
        /// <value>Describes the additional service features supported with the voice.</value>
        [JsonProperty("supported_features", NullValueHandling = NullValueHandling.Ignore)]
        public SupportedFeatures SupportedFeatures { get; set; }
        /// <summary>
        /// Returns information about a specified custom voice model. **Note:** This field is returned only when you list information about a specific voice and specify the GUID of a custom voice model that is based on that voice.
        /// </summary>
        /// <value>Returns information about a specified custom voice model. **Note:** This field is returned only when you list information about a specific voice and specify the GUID of a custom voice model that is based on that voice.</value>
        [JsonProperty("customization", NullValueHandling = NullValueHandling.Ignore)]
        public VoiceModel Customization { get; set; }
    }

}
