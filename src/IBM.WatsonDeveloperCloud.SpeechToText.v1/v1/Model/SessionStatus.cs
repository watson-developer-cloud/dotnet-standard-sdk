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
    public class SessionStatus
    {
        /// <summary>
        /// Gets or sets state of the session. The state must be `initialized`
        /// to perform a new recognition request on the session.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets URI for information about the model that is used with
        /// the session.
        /// </summary>
        [JsonProperty("model")]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets URI for REST recognition requests.
        /// </summary>
        [JsonProperty("recognize")]
        public string Recognize { get; set; }

        /// <summary>
        /// Gets or sets URI for REST results observers.
        /// </summary>
        [JsonProperty("observe_result")]
        public string ObserveResult { get; set; }

        /// <summary>
        /// Gets or sets URI for WebSocket recognition requests. Needed only
        /// for working with the WebSocket interface.
        /// </summary>
        [JsonProperty("recognizeWS")]
        public string RecognizeWS { get; set; }
    }
}