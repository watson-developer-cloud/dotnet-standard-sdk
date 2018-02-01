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

using System.Net.Http.Headers;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using System;
using System.Runtime.ExceptionServices;

namespace IBM.WatsonDeveloperCloud.Discovery
{
    public class DiscoveryService : WatsonService, IDiscoveryService
    {
        const string PATH_DISCOVERY = "/v1/environments";
        const string VERSION_DATE_2016_12_01 = "2016-12-01";
        const string SERVICE_NAME = "discovery";
        const string URL = "https://gateway.watsonplatform.net/conversation/api";

        public DiscoveryService()
            : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public DiscoveryService(string userName, string password)
            : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }

        public DiscoveryService(IClient httpClient)
            : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public GetEnvironmentsResponse ListEnvironments()
        {
            GetEnvironmentsResponse result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_DISCOVERY}")
                               .WithArgument("version", VERSION_DATE_2016_12_01)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<GetEnvironmentsResponse>()
                               .Result;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
