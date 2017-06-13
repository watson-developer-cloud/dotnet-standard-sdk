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
    public class SpeechModel
    {
        /// <summary>
        /// Gets or sets name of the model for use as an identifier in calls to the service (for example, `en-US_BroadbandModel`).
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets language identifier for the model (for example, `en-US`).
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets sampling rate (minimum acceptable rate for audio) used by the model in Hertz.
        /// </summary>
        [JsonProperty("rate")]
        public int Rate { get; set; }

        /// <summary>
        /// Gets or sets URI for the model.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets describes the additional service features supported with the model.
        /// </summary>
        [JsonProperty("supported_features")]
        public SupportedFeatures SupportedFeatures { get; set; }

        /// <summary>
        /// Gets or sets URI for the model for use with the `POST /v1/sessions` method. (Returned only for requests for a single model with the `GET /v1/models/{model_id}` method.)
        /// </summary>
        [JsonProperty("sessions")]
        public string Sessions { get; set; }

        /// <summary>
        /// Gets or sets brief description of the model.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}