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
    public class Session
    {
        /// <summary>
        /// Gets or sets identifier for the new session.
        /// </summary>
        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets URI for the new session.
        /// </summary>
        [JsonProperty("new_session_uri")]
        public string NewSessionUri { get; set; }

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