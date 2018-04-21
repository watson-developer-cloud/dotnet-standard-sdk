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
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud
{
    public class BaseModel
    {
        /// <summary>
        /// Custom data object including custom request headers, response headers and response json.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, object> CustomData {
            get
            {
                return _customData == null ? new Dictionary<string, object>() : _customData;
            }
            set
            {
                _customData = value;
            }
        }
        private Dictionary<string, object> _customData;
        /// <summary>
        /// Gets custom request headers.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, string> CustomRequestHeaders
        {
            get
            {
                return CustomData.ContainsKey(Constants.CUSTOM_REQUEST_HEADERS) ? CustomData[Constants.CUSTOM_REQUEST_HEADERS] as Dictionary<string, string> : null;
            }
        }

        /// <summary>
        /// Gets response headers.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, string> ResponseHeaders
        {
            get
            {
                return CustomData.ContainsKey(Constants.RESPONSE_HEADERS) ? CustomData[Constants.RESPONSE_HEADERS] as Dictionary<string, string> : null;
            }
        }

        /// <summary>
        /// Gets the response json.
        /// </summary>
        [JsonIgnore]
        public string ResponseJson
        {
            get
            {
                return CustomData.ContainsKey(Constants.JSON) ? CustomData[Constants.JSON] as string : null;
            }
        }
    }
}
