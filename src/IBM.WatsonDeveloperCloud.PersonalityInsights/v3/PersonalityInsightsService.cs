/**
* Copyright 2015 IBM Corp. All Rights Reserved.
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

using System;
using System.Net.Http;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models;
using IBM.WatsonDeveloperCloud.Service;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3
{
    public class PersonalityInsightsService : WatsonService, IPersonalityInsightsService
    {
        const string SERVICE_NAME = "language_translator";

        const string URL = "https://gateway.watsonplatform.net/personality-insights/api";
        const string PATH_GET_PROFILES = "/v3/profile";

        public PersonalityInsightsService()
            : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public PersonalityInsightsService(string userName, string password)
            : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }

        public PersonalityInsightsService(IClient httpClient)
            : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public Profile GetProfile()
        {
            Profile result = null;

            try
            {

               
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}