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

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public partial class VoiceCustomization
    {
        /// <summary>
        /// Gets or sets URI of the voice.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets gender of the voice: 'male' or 'female'.
        /// </summary>
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets name of the voice. Use this as the voice identifier
        /// in all requests.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets language and region of the voice (for example,
        /// 'en-US').
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets textual description of the voice.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets if `true`, the voice can be customized; if `false`,
        /// the voice cannot be customized. (Same as `custom_pronunciation`;
        /// maintained for backward compatibility.)
        /// </summary>
        [JsonProperty(PropertyName = "customizable")]
        public bool Customizable { get; set; }

        /// <summary>
        /// Gets or sets describes the additional service features supported
        /// with the voice.
        /// </summary>
        [JsonProperty(PropertyName = "supported_features")]
        public SupportedFeatures SupportedFeatures { get; set; }

        /// <summary>
        /// Gets or sets information about a specific custom voice model.
        /// Returned only when the `customization_id` parameter is specified
        /// with the method.
        /// </summary>
        [JsonProperty(PropertyName = "customization")]
        public Customization Customization { get; set; }
    }
}
