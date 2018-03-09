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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// SpeechModel.
    /// </summary>
    public class SpeechModel
    {
        /// <summary>
        /// The name of the model for use as an identifier in calls to the service (for example, `en-US_BroadbandModel`).
        /// </summary>
        /// <value>The name of the model for use as an identifier in calls to the service (for example, `en-US_BroadbandModel`).</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The language identifier for the model (for example, `en-US`).
        /// </summary>
        /// <value>The language identifier for the model (for example, `en-US`).</value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// The sampling rate (minimum acceptable rate for audio) used by the model in Hertz.
        /// </summary>
        /// <value>The sampling rate (minimum acceptable rate for audio) used by the model in Hertz.</value>
        [JsonProperty("rate", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rate { get; set; }
        /// <summary>
        /// The URI for the model.
        /// </summary>
        /// <value>The URI for the model.</value>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        /// <summary>
        /// Describes the additional service features supported with the model.
        /// </summary>
        /// <value>Describes the additional service features supported with the model.</value>
        [JsonProperty("supported_features", NullValueHandling = NullValueHandling.Ignore)]
        public SupportedFeatures SupportedFeatures { get; set; }
        /// <summary>
        /// Brief description of the model.
        /// </summary>
        /// <value>Brief description of the model.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The URI for the model for use with the `POST /v1/sessions` method. (Returned only for requests for a single model with the `GET /v1/models/{model_id}` method.).
        /// </summary>
        /// <value>The URI for the model for use with the `POST /v1/sessions` method. (Returned only for requests for a single model with the `GET /v1/models/{model_id}` method.).</value>
        [JsonProperty("sessions", NullValueHandling = NullValueHandling.Ignore)]
        public string Sessions { get; set; }
    }

}
