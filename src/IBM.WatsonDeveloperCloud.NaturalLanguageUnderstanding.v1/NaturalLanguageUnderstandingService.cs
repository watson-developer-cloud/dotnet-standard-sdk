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
        private readonly TokenManager _tokenManager;
        private string _versionDate;
        public string VersionDate
        {
            get => _versionDate;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value), "VersionDate must not be null nor empty");
                _versionDate = value;
            }
        }

        public NaturalLanguageUnderstandingService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(Endpoint))
                Endpoint = URL;
        }

        public NaturalLanguageUnderstandingService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName), "UserName must not be null nor empty");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password must not be null nor empty");

            SetCredential(userName, password);

            VersionDate = versionDate;
        }

        public NaturalLanguageUnderstandingService(TokenOptions options, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey), "Either IamAccessToken or IamApiKey must not be null nor empty");

            VersionDate = versionDate;

            _tokenManager = new TokenManager(options);
        }

        public NaturalLanguageUnderstandingService(IClient httpClient) : this()
        {
            Client = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "HttpClient must not be null");
        }

        /// <summary>
        /// Analyze text, HTML, or a public webpage. Analyzes text, HTML, or a public webpage with one or more text analysis features.  ### Concepts Identify general concepts that are referenced or alluded to in your content. Concepts that are detected typically have an associated link to a DBpedia resource.  ### Emotion Detect anger, disgust, fear, joy, or sadness that is conveyed by your content. Emotion information can be returned for detected entities, keywords, or user-specified target phrases found in the text.  ### Entities Detect important people, places, geopolitical entities and other types of entities in your content. Entity detection recognizes consecutive coreferences of each entity. For example, analysis of the following text would count "Barack Obama" and "He" as the same entity:  "Barack Obama was the 44th President of the United States. He took office in January 2009."  ### Keywords Determine the most important keywords in your content. Keyword phrases are organized by relevance in the results.  ### Metadata Get author information, publication date, and the title of your text/HTML content.  ### Relations Recognize when two entities are related, and identify the type of relation.  For example, you can identify an "awardedTo" relation between an award and its recipient.  ### Semantic Roles Parse sentences into subject-action-object form, and identify entities and keywords that are subjects or objects of an action.  ### Sentiment Determine whether your content conveys postive or negative sentiment. Sentiment information can be returned for detected entities, keywords, or user-specified target phrases found in the text.   ### Categories Categorize your content into a hierarchical 5-level taxonomy. For example, "Leonardo DiCaprio won an Oscar" returns "/art and entertainment/movies and tv/movies" as the most confident classification.
        /// </summary>
        /// <param name="parameters">An object containing request parameters. The `features` object and one of the `text`, `html`, or `url` attributes are required.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        public AnalysisResults Analyze(Parameters parameters, Dictionary<string, object> customData = null)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            AnalysisResults result;

            try
            {
                var client = _tokenManager == null ? Client.WithAuthentication(UserName, Password) : Client.WithAuthentication(_tokenManager.GetToken());

                var restRequest = client.PostAsync($"{Endpoint}/v1/analyze");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody(parameters);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<AnalysisResults>().Result ?? new AnalysisResults();
                result.CustomData = restRequest.CustomData;
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
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="InlineResponse200" />InlineResponse200</returns>
        public InlineResponse200 DeleteModel(string modelId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));

            InlineResponse200 result;

            try
            {
                var client = _tokenManager == null ? Client.WithAuthentication(UserName, Password) : Client.WithAuthentication(_tokenManager.GetToken());

                var restRequest = client.DeleteAsync($"{Endpoint}/v1/models/{modelId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<InlineResponse200>().Result ?? new InlineResponse200();
                result.CustomData = restRequest.CustomData;
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
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        public ListModelsResults ListModels(Dictionary<string, object> customData = null)
        {

            ListModelsResults result;

            try
            {
                var client = _tokenManager == null ? Client.WithAuthentication(UserName, Password) : Client.WithAuthentication(_tokenManager.GetToken());

                var restRequest = client.GetAsync($"{Endpoint}/v1/models");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<ListModelsResults>().Result ?? new ListModelsResults();
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
