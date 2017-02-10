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

namespace IBM.WatsonDeveloperCloud.Http.Exceptions
{
    [JsonConverter(typeof(ErrorConverter))]
    public class Error
    {
        /// <summary>
        /// HTTP status code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Response message that describes the problem.
        /// </summary>
        public string CodeDescription { get; set; }

        /// <summary>
        /// Description of the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Help message
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// For some session-based responses, indicates whether the session was closed as a result of the error. Set to true if an active session is closed as a result of the problem.
        /// </summary>
        public bool SessionClosed { get; set; }
    }
}