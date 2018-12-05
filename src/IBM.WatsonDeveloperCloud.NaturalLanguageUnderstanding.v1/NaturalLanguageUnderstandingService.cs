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

using System.Collections.Generic;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using System;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1
{
    public partial class NaturalLanguageUnderstandingService : WatsonService, INaturalLanguageUnderstandingService
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

        public NaturalLanguageUnderstandingService(TokenOptions options, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;

            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                this.Endpoint = options.ServiceUrl;
            }
            else
            {
                options.ServiceUrl = this.Endpoint;
            }

            _tokenManager = new TokenManager(options);
        }

        public NaturalLanguageUnderstandingService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Analyze text.
        ///
        /// Analyzes text, HTML, or a public webpage for the following features:
        /// - Categories
        /// - Concepts
        /// - Emotion
        /// - Entities
        /// - Keywords
        /// - Metadata
        /// - Relations
        /// - Semantic roles
        /// - Sentiment.
        /// </summary>
        /// <param name="parameters">An object containing request parameters. The `features` object and one of the
        /// `text`, `html`, or `url` attributes are required.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        public AnalysisResults Analyze(Parameters parameters, Dictionary<string, object> customData = null)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            AnalysisResults result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/analyze");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<Parameters>(parameters);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<AnalysisResults>().Result;
                if(result == null)
                    result = new AnalysisResults();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Delete model.
        ///
        /// Deletes a custom model.
        /// </summary>
        /// <param name="modelId">model_id of the model to delete.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="InlineResponse200" />InlineResponse200</returns>
        public InlineResponse200 DeleteModel(string modelId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            InlineResponse200 result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/models/{modelId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<InlineResponse200>().Result;
                if(result == null)
                    result = new InlineResponse200();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List models.
        ///
        /// Lists Watson Knowledge Studio [custom
        /// models](/docs/services/natural-language-understanding/customizing.html) that are deployed to your Natural
        /// Language Understanding service.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        public ListModelsResults ListModels(Dictionary<string, object> customData = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ListModelsResults result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<ListModelsResults>().Result;
                if(result == null)
                    result = new ListModelsResults();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
