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

using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;
using IBM.WatsonDeveloperCloud.Service;
using System;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3
{
    public class PersonalityInsightsService : WatsonService, IPersonalityInsightsService
    {
        const string SERVICE_NAME = "personality_insights";
        const string URL = "https://gateway.watsonplatform.net/personality-insights/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public PersonalityInsightsService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public PersonalityInsightsService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public PersonalityInsightsService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public Profile Profile(string contentType, string accept, ContentListContainer body, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null)
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (string.IsNullOrEmpty(accept))
                throw new ArgumentNullException(nameof(accept));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Profile result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v3/profile")
                                .WithArgument("version", VersionDate)
                                .WithHeader("Content-Type", contentType)
                                .WithHeader("Content-Language", contentLanguage)
                                .WithHeader("Accept", accept)
                                .WithHeader("Accept-Language", acceptLanguage)
                                .WithArgument("raw_scores", rawScores)
                                .WithArgument("csv_headers", csvHeaders)
                                .WithArgument("consumption_preferences", consumptionPreferences)
                                .WithBody<ContentListContainer>(body)
                                .As<Profile>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
