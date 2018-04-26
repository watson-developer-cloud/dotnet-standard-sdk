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

using IBM.WatsonDeveloperCloud.Assistant.v1;
using IBM.WatsonDeveloperCloud.Discovery.v1;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.Example
{
    class IamTokenExample
    {
        private TokenManager _iamAssistantTokenManager;
        private AssistantService _assistant;
        private string _assistantVersionDate = "2018-02-16";
        private DiscoveryService _discovery;
        private string _discoveryVersionDate = "2018-03-05";
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

            //  Get token
            string getTokenResult = null;
            try
            {
                getTokenResult = _iamAssistantTokenManager.GetToken();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting token: {0}", e.Message);
            }

            Console.WriteLine("GetToken() response: \n{0}", JsonConvert.SerializeObject(getTokenResult, Formatting.Indented));

            //  Call Assistant
            _assistant = new AssistantService(iamAssistantTokenOptions, _assistantVersionDate);
            _assistant.Endpoint = _assistantUrl;

            var _assistantListWorkspacesResponse = _assistant.ListWorkspaces();

            Console.WriteLine("ListWorkspaces() response: \n{0}", JsonConvert.SerializeObject(_assistantListWorkspacesResponse, Formatting.Indented));

            //  Call Discovery
            TokenOptions iamDiscoveryTokenOptions = new TokenOptions()
            {
                IamApiKey = _discoveryApikey,
                IamUrl = _iamUrl
            };

            _discovery = new DiscoveryService(iamDiscoveryTokenOptions, _discoveryVersionDate);
            _discovery.Endpoint = _discoveryUrl;

            var _discoveryListEnvironmentsResponse = _discovery.ListEnvironments();

            Console.WriteLine("ListEnvironments() response: \n{0}", JsonConvert.SerializeObject(_discoveryListEnvironmentsResponse, Formatting.Indented));
        }
    }
}
