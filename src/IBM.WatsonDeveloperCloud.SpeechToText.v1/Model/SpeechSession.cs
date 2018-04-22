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
    /// SpeechSession.
    /// </summary>
    public class SpeechSession : BaseModel
    {
        /// <summary>
        /// URI for HTTP REST recognition requests.
        /// </summary>
        /// <value>URI for HTTP REST recognition requests.</value>
        [JsonProperty("recognize", NullValueHandling = NullValueHandling.Ignore)]
        public string Recognize { get; set; }
        /// <summary>
        /// URI for WebSocket recognition requests. **Note:** This field is needed only for working with the WebSocket interface.
        /// </summary>
        /// <value>URI for WebSocket recognition requests. **Note:** This field is needed only for working with the WebSocket interface.</value>
        [JsonProperty("recognizeWS", NullValueHandling = NullValueHandling.Ignore)]
        public string RecognizeWS { get; set; }
        /// <summary>
        /// URI for HTTP REST results observers.
        /// </summary>
        /// <value>URI for HTTP REST results observers.</value>
        [JsonProperty("observe_result", NullValueHandling = NullValueHandling.Ignore)]
        public string ObserveResult { get; set; }
        /// <summary>
        /// Identifier for the new session. **Note:** This field is returned only when you create a new session.
        /// </summary>
        /// <value>Identifier for the new session. **Note:** This field is returned only when you create a new session.</value>
        [JsonProperty("session_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionId { get; set; }
        /// <summary>
        /// URI for the new session. **Note:** This field is returned only when you create a new session.
        /// </summary>
        /// <value>URI for the new session. **Note:** This field is returned only when you create a new session.</value>
        [JsonProperty("new_session_uri", NullValueHandling = NullValueHandling.Ignore)]
        public string NewSessionUri { get; set; }
        /// <summary>
        /// State of the session. The state must be `initialized` for the session to accept another recognition request. Other internal states are possible, but they have no meaning for the user. **Note:** This field is returned only when you request the status of an existing session.
        /// </summary>
        /// <value>State of the session. The state must be `initialized` for the session to accept another recognition request. Other internal states are possible, but they have no meaning for the user. **Note:** This field is returned only when you request the status of an existing session.</value>
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
        /// <summary>
        /// URI for information about the model that is used with the session. **Note:** This field is returned only when you request the status of an existing session.
        /// </summary>
        /// <value>URI for information about the model that is used with the session. **Note:** This field is returned only when you request the status of an existing session.</value>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    }

}
