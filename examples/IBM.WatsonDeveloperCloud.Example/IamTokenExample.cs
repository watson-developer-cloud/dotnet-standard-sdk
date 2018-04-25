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

using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.Example
{
    class IamTokenExample
    {
        private TokenManager _iamAssistantTokenManager;
        private string _assistantApikey;
        private string _assistantUrl;
        private string _discoveryApikey;
        private string _discoveryUrl;
        private string _iamApikey;
        private string _iamUrl;

        public IamTokenExample()
        {
            //  Get token
            TokenOptions iamAssistantTokenOptions = new TokenOptions()
            {
                IamApiKey = _assistantApikey,
                IamUrl = _iamUrl
            };
            _iamAssistantTokenManager = new TokenManager(iamAssistantTokenOptions);

            var getTokenResult = _iamAssistantTokenManager.GetToken();

            Console.WriteLine(JsonConvert.SerializeObject(getTokenResult, Formatting.Indented));
        }
    }
}
