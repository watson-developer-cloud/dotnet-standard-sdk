/**
* (C) Copyright IBM Corp. 2018, 2019.
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
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1
{
    public partial class NaturalLanguageUnderstandingService : IBMService, INaturalLanguageUnderstandingService
    {
        new const string SERVICE_NAME = "natural_language_understanding";
        const string URL = "https://gateway.watsonplatform.net/natural-language-understanding/api";
        public new string DefaultEndpoint = "https://gateway.watsonplatform.net/natural-language-understanding/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public NaturalLanguageUnderstandingService() : base(SERVICE_NAME) { }
        
        [Obsolete("Please use NaturalLanguageUnderstandingService(string versionDate, IAuthenticatorConfig config) instead")]
        public NaturalLanguageUnderstandingService(string userName, string password, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if (string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }
        
        [Obsolete("Please use NaturalLanguageUnderstandingService(string versionDate, IAuthenticatorConfig config) instead")]
        public NaturalLanguageUnderstandingService(TokenOptions options, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if (string.IsNullOrEmpty(versionDate))
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

            IamConfig iamConfig = null;
            if (!string.IsNullOrEmpty(options.IamAccessToken))
            {
                iamConfig = new IamConfig(
                    userManagedAccessToken: options.IamAccessToken
                    );
            }
            else
            {
                iamConfig = new IamConfig(
                    apikey: options.IamApiKey,
                    iamUrl: options.IamUrl
                    );
            }

            SetAuthenticator(iamConfig);
        }

        public NaturalLanguageUnderstandingService(IClient httpClient) : base(SERVICE_NAME, URL)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public NaturalLanguageUnderstandingService(string versionDate, IAuthenticatorConfig config) : base(SERVICE_NAME, config)
        {
            VersionDate = versionDate;
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
        /// - Sentiment
        /// - Syntax (Experimental).
        /// </summary>
        /// <param name="parameters">An object containing request parameters. The `features` object and one of the
        /// `text`, `html`, or `url` attributes are required.</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        public DetailedResponse<AnalysisResults> Analyze(Features features, string text = null, string html = null, string url = null, bool? clean = null, string xpath = null, bool? fallbackToRaw = null, bool? returnAnalyzedText = null, string language = null, long? limitTextCharacters = null)
        {
            if (features == null)
            {
                throw new ArgumentNullException("`features` is required for `Analyze`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<AnalysisResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/analyze");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (features != null)
                {
                    bodyObject["features"] = JToken.FromObject(features);
                }
                if (!string.IsNullOrEmpty(text))
                {
                    bodyObject["text"] = text;
                }
                if (!string.IsNullOrEmpty(html))
                {
                    bodyObject["html"] = html;
                }
                if (!string.IsNullOrEmpty(url))
                {
                    bodyObject["url"] = url;
                }
                if (clean != null)
                {
                    bodyObject["clean"] = JToken.FromObject(clean);
                }
                if (!string.IsNullOrEmpty(xpath))
                {
                    bodyObject["xpath"] = xpath;
                }
                if (fallbackToRaw != null)
                {
                    bodyObject["fallback_to_raw"] = JToken.FromObject(fallbackToRaw);
                }
                if (returnAnalyzedText != null)
                {
                    bodyObject["return_analyzed_text"] = JToken.FromObject(returnAnalyzedText);
                }
                if (!string.IsNullOrEmpty(language))
                {
                    bodyObject["language"] = language;
                }
                if (limitTextCharacters != null)
                {
                    bodyObject["limit_text_characters"] = JToken.FromObject(limitTextCharacters);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "Analyze"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AnalysisResults>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AnalysisResults>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List models.
        ///
        /// Lists Watson Knowledge Studio [custom entities and relations
        /// models](https://cloud.ibm.com/docs/services/natural-language-understanding?topic=natural-language-understanding-customizing)
        /// that are deployed to your Natural Language Understanding service.
        /// </summary>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        public DetailedResponse<ListModelsResults> ListModels()
        {

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<ListModelsResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "ListModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListModelsResults>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListModelsResults>();
                }
            }
            catch (AggregateException ae)
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
        /// <param name="modelId">Model ID of the model to delete.</param>
        /// <returns><see cref="DeleteModelResults" />DeleteModelResults</returns>
        public DetailedResponse<DeleteModelResults> DeleteModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `DeleteModel`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<DeleteModelResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/models/{modelId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "DeleteModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteModelResults>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteModelResults>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
