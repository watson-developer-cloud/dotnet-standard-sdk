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

using System.Text;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public NaturalLanguageUnderstandingService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Analyze text, HTML, or a public webpage. Analyzes text, HTML, or a public webpage with one or more text analysis features.
        /// </summary>
        /// <param name="parameters">An object containing request parameters. The `features` object and one of the `text`, `html`, or `url` attributes are required.</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        public AnalysisResults Analyze(Parameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            AnalysisResults result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/analyze");
                request.WithArgument("version", VersionDate);
                request.WithBody<Parameters>(parameters);
                result = request.As<AnalysisResults>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Delete model. Deletes a custom model.
        /// </summary>
        /// <param name="modelId">model_id of the model to delete.</param>
        /// <returns><see cref="InlineResponse200" />InlineResponse200</returns>
        public InlineResponse200 DeleteModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            InlineResponse200 result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/models/{modelId}");
                request.WithArgument("version", VersionDate);
                result = request.As<InlineResponse200>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List models. Lists available models for Relations and Entities features, including Watson Knowledge Studio custom models that you have created and linked to your Natural Language Understanding service.
        /// </summary>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        public ListModelsResults ListModels()
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ListModelsResults result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/models");
                request.WithArgument("version", VersionDate);
                result = request.As<ListModelsResults>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
