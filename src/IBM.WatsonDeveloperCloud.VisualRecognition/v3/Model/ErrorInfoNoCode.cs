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
namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model
{
    /// <summary>
    /// Information about something that went wrong. Omitted if no error.
    /// </summary>
    public class ErrorInfoNoCode
    {
        /// <summary>
        /// Codified error string, like 'input_error'.
        /// </summary>
        /// <value>Codified error string, like 'input_error'.</value>
        [JsonProperty("error_id")]
        public string ErrorId { get; set; }
        /// <summary>
        /// Human-readable error string, like 'Ignoring image with no valid data.'.
        /// </summary>
        /// <value>Human-readable error string, like 'Ignoring image with no valid data.'.</value>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
