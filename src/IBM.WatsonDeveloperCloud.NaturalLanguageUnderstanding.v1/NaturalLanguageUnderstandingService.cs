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
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using IBM.WatsonDeveloperCloud.Service;
using System;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1
{
    public class NaturalLanguageUnderstandingService : WatsonService, INaturalLanguageUnderstandingService
    {
        const string SERVICE_NAME = "natural_language_understanding";
        const string URL = "https://gateway.watsonplatform.net/natural-language-understanding/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        /** The Constant NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27. */
        public static string NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27 = "2017-02-27";

        public NaturalLanguageUnderstandingService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public NaturalLanguageUnderstandingService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27'");

            VersionDate = versionDate;
        }

        public NaturalLanguageUnderstandingService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public AnalysisResults Analyze(Parameters parameters = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27'");

            AnalysisResults result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/analyze")
                                .WithArgument("version", VersionDate)
                                .WithBody<Parameters>(parameters)
                                .As<AnalysisResults>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public InlineResponse200 DeleteModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27'");

            InlineResponse200 result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/models/{modelId}")
                                .WithArgument("version", VersionDate)
                                .As<InlineResponse200>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ListModelsResults ListModels()
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27'");

            ListModelsResults result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/models")
                                .WithArgument("version", VersionDate)
                                .As<ListModelsResults>()
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
