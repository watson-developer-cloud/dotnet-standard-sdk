/**
* (C) Copyright IBM Corp. 2017, 2021.
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

/**
* IBM OpenAPI SDK Code Generator Version: 99-SNAPSHOT-07189efd-20210907-091205
*/
 
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1
{
    public partial class NaturalLanguageUnderstandingService : IBMService, INaturalLanguageUnderstandingService
    {
        const string defaultServiceName = "natural_language_understanding";
        private const string defaultServiceUrl = "https://api.us-south.natural-language-understanding.watson.cloud.ibm.com";
        public string Version { get; set; }

        public NaturalLanguageUnderstandingService(string version) : this(version, defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public NaturalLanguageUnderstandingService(string version, IAuthenticator authenticator) : this(version, defaultServiceName, authenticator) {}
        public NaturalLanguageUnderstandingService(string version, string serviceName) : this(version, serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public NaturalLanguageUnderstandingService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public NaturalLanguageUnderstandingService(string version, string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentNullException("`version` is required");
            }
            Version = version;

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
        }

        /// <summary>
        /// Analyze text.
        ///
        /// Analyzes text, HTML, or a public webpage for the following features:
        /// - Categories
        /// - Classifications
        /// - Concepts
        /// - Emotion
        /// - Entities
        /// - Keywords
        /// - Metadata
        /// - Relations
        /// - Semantic roles
        /// - Sentiment
        /// - Syntax
        /// - Summarization (Experimental)
        ///
        /// If a language for the input text is not specified with the `language` parameter, the service [automatically
        /// detects the
        /// language](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-detectable-languages).
        /// </summary>
        /// <param name="features">Specific features to analyze the document for.</param>
        /// <param name="text">The plain text to analyze. One of the `text`, `html`, or `url` parameters is required.
        /// (optional)</param>
        /// <param name="html">The HTML file to analyze. One of the `text`, `html`, or `url` parameters is required.
        /// (optional)</param>
        /// <param name="url">The webpage to analyze. One of the `text`, `html`, or `url` parameters is required.
        /// (optional)</param>
        /// <param name="clean">Set this to `false` to disable webpage cleaning. For more information about webpage
        /// cleaning, see [Analyzing
        /// webpages](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-analyzing-webpages).
        /// (optional, default to true)</param>
        /// <param name="xpath">An [XPath
        /// query](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-analyzing-webpages#xpath)
        /// to perform on `html` or `url` input. Results of the query will be appended to the cleaned webpage text
        /// before it is analyzed. To analyze only the results of the XPath query, set the `clean` parameter to `false`.
        /// (optional)</param>
        /// <param name="fallbackToRaw">Whether to use raw HTML content if text cleaning fails. (optional, default to
        /// true)</param>
        /// <param name="returnAnalyzedText">Whether or not to return the analyzed text. (optional, default to
        /// false)</param>
        /// <param name="language">ISO 639-1 code that specifies the language of your text. This overrides automatic
        /// language detection. Language support differs depending on the features you include in your analysis. For
        /// more information, see [Language
        /// support](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-language-support).
        /// (optional)</param>
        /// <param name="limitTextCharacters">Sets the maximum number of characters that are processed by the service.
        /// (optional)</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        public DetailedResponse<AnalysisResults> Analyze(Features features, string text = null, string html = null, string url = null, bool? clean = null, string xpath = null, bool? fallbackToRaw = null, bool? returnAnalyzedText = null, string language = null, long? limitTextCharacters = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (features == null)
            {
                throw new ArgumentNullException("`features` is required for `Analyze`");
            }
            DetailedResponse<AnalysisResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/analyze");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
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
        /// models](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-customizing)
        /// that are deployed to your Natural Language Understanding service.
        /// </summary>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        public DetailedResponse<ListModelsResults> ListModels()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ListModelsResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `DeleteModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<DeleteModelResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/models/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

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
        /// <summary>
        /// Create sentiment model.
        ///
        /// (Beta) Creates a custom sentiment model by uploading training data and associated metadata. The model begins
        /// the training and deploying process and is ready to use when the `status` is `available`.
        /// </summary>
        /// <param name="language">The 2-letter language code of this model.</param>
        /// <param name="trainingData">Training data in CSV format. For more information, see [Sentiment training data
        /// requirements](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-custom-sentiment#sentiment-training-data-requirements).</param>
        /// <param name="name">An optional name for the model. (optional)</param>
        /// <param name="description">An optional description of the model. (optional)</param>
        /// <param name="modelVersion">An optional version string. (optional)</param>
        /// <param name="workspaceId">ID of the Watson Knowledge Studio workspace that deployed this model to Natural
        /// Language Understanding. (optional)</param>
        /// <param name="versionDescription">The description of the version. (optional)</param>
        /// <returns><see cref="SentimentModel" />SentimentModel</returns>
        public DetailedResponse<SentimentModel> CreateSentimentModel(string language, System.IO.MemoryStream trainingData, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentNullException("`language` is required for `CreateSentimentModel`");
            }
            if (trainingData == null)
            {
                throw new ArgumentNullException("`trainingData` is required for `CreateSentimentModel`");
            }
            DetailedResponse<SentimentModel> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (language != null)
                {
                    var languageContent = new StringContent(language, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    languageContent.Headers.ContentType = null;
                    formData.Add(languageContent, "language");
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/csv", out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                if (name != null)
                {
                    var nameContent = new StringContent(name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    nameContent.Headers.ContentType = null;
                    formData.Add(nameContent, "name");
                }

                if (description != null)
                {
                    var descriptionContent = new StringContent(description, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    descriptionContent.Headers.ContentType = null;
                    formData.Add(descriptionContent, "description");
                }

                if (modelVersion != null)
                {
                    var modelVersionContent = new StringContent(modelVersion, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    modelVersionContent.Headers.ContentType = null;
                    formData.Add(modelVersionContent, "model_version");
                }

                if (workspaceId != null)
                {
                    var workspaceIdContent = new StringContent(workspaceId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    workspaceIdContent.Headers.ContentType = null;
                    formData.Add(workspaceIdContent, "workspace_id");
                }

                if (versionDescription != null)
                {
                    var versionDescriptionContent = new StringContent(versionDescription, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    versionDescriptionContent.Headers.ContentType = null;
                    formData.Add(versionDescriptionContent, "version_description");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/models/sentiment");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "CreateSentimentModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SentimentModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SentimentModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List sentiment models.
        ///
        /// (Beta) Returns all custom sentiment models associated with this service instance.
        /// </summary>
        /// <returns><see cref="ListSentimentModelsResponse" />ListSentimentModelsResponse</returns>
        public DetailedResponse<ListSentimentModelsResponse> ListSentimentModels()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ListSentimentModelsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models/sentiment");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "ListSentimentModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListSentimentModelsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListSentimentModelsResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get sentiment model details.
        ///
        /// (Beta) Returns the status of the sentiment model with the given model ID.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <returns><see cref="SentimentModel" />SentimentModel</returns>
        public DetailedResponse<SentimentModel> GetSentimentModel(string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `GetSentimentModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<SentimentModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models/sentiment/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "GetSentimentModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SentimentModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SentimentModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update sentiment model.
        ///
        /// (Beta) Overwrites the training data associated with this custom sentiment model and retrains the model. The
        /// new model replaces the current deployment.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <param name="language">The 2-letter language code of this model.</param>
        /// <param name="trainingData">Training data in CSV format. For more information, see [Sentiment training data
        /// requirements](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-custom-sentiment#sentiment-training-data-requirements).</param>
        /// <param name="name">An optional name for the model. (optional)</param>
        /// <param name="description">An optional description of the model. (optional)</param>
        /// <param name="modelVersion">An optional version string. (optional)</param>
        /// <param name="workspaceId">ID of the Watson Knowledge Studio workspace that deployed this model to Natural
        /// Language Understanding. (optional)</param>
        /// <param name="versionDescription">The description of the version. (optional)</param>
        /// <returns><see cref="SentimentModel" />SentimentModel</returns>
        public DetailedResponse<SentimentModel> UpdateSentimentModel(string modelId, string language, System.IO.MemoryStream trainingData, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `UpdateSentimentModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentNullException("`language` is required for `UpdateSentimentModel`");
            }
            if (trainingData == null)
            {
                throw new ArgumentNullException("`trainingData` is required for `UpdateSentimentModel`");
            }
            DetailedResponse<SentimentModel> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (language != null)
                {
                    var languageContent = new StringContent(language, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    languageContent.Headers.ContentType = null;
                    formData.Add(languageContent, "language");
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/csv", out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                if (name != null)
                {
                    var nameContent = new StringContent(name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    nameContent.Headers.ContentType = null;
                    formData.Add(nameContent, "name");
                }

                if (description != null)
                {
                    var descriptionContent = new StringContent(description, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    descriptionContent.Headers.ContentType = null;
                    formData.Add(descriptionContent, "description");
                }

                if (modelVersion != null)
                {
                    var modelVersionContent = new StringContent(modelVersion, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    modelVersionContent.Headers.ContentType = null;
                    formData.Add(modelVersionContent, "model_version");
                }

                if (workspaceId != null)
                {
                    var workspaceIdContent = new StringContent(workspaceId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    workspaceIdContent.Headers.ContentType = null;
                    formData.Add(workspaceIdContent, "workspace_id");
                }

                if (versionDescription != null)
                {
                    var versionDescriptionContent = new StringContent(versionDescription, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    versionDescriptionContent.Headers.ContentType = null;
                    formData.Add(versionDescriptionContent, "version_description");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/models/sentiment/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "UpdateSentimentModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SentimentModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SentimentModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete sentiment model.
        ///
        /// (Beta) Un-deploys the custom sentiment model with the given model ID and deletes all associated customer
        /// data, including any training data or binary artifacts.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <returns><see cref="DeleteModelResults" />DeleteModelResults</returns>
        public DetailedResponse<DeleteModelResults> DeleteSentimentModel(string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `DeleteSentimentModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<DeleteModelResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/models/sentiment/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "DeleteSentimentModel"));
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
        /// <summary>
        /// Create categories model.
        ///
        /// (Beta) Creates a custom categories model by uploading training data and associated metadata. The model
        /// begins the training and deploying process and is ready to use when the `status` is `available`.
        /// </summary>
        /// <param name="language">The 2-letter language code of this model.</param>
        /// <param name="trainingData">Training data in JSON format. For more information, see [Categories training data
        /// requirements](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-categories##categories-training-data-requirements).</param>
        /// <param name="trainingDataContentType">The content type of trainingData. (optional)</param>
        /// <param name="name">An optional name for the model. (optional)</param>
        /// <param name="description">An optional description of the model. (optional)</param>
        /// <param name="modelVersion">An optional version string. (optional)</param>
        /// <param name="workspaceId">ID of the Watson Knowledge Studio workspace that deployed this model to Natural
        /// Language Understanding. (optional)</param>
        /// <param name="versionDescription">The description of the version. (optional)</param>
        /// <returns><see cref="CategoriesModel" />CategoriesModel</returns>
        public DetailedResponse<CategoriesModel> CreateCategoriesModel(string language, System.IO.MemoryStream trainingData, string trainingDataContentType = null, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentNullException("`language` is required for `CreateCategoriesModel`");
            }
            if (trainingData == null)
            {
                throw new ArgumentNullException("`trainingData` is required for `CreateCategoriesModel`");
            }
            DetailedResponse<CategoriesModel> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (language != null)
                {
                    var languageContent = new StringContent(language, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    languageContent.Headers.ContentType = null;
                    formData.Add(languageContent, "language");
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(trainingDataContentType, out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                if (name != null)
                {
                    var nameContent = new StringContent(name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    nameContent.Headers.ContentType = null;
                    formData.Add(nameContent, "name");
                }

                if (description != null)
                {
                    var descriptionContent = new StringContent(description, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    descriptionContent.Headers.ContentType = null;
                    formData.Add(descriptionContent, "description");
                }

                if (modelVersion != null)
                {
                    var modelVersionContent = new StringContent(modelVersion, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    modelVersionContent.Headers.ContentType = null;
                    formData.Add(modelVersionContent, "model_version");
                }

                if (workspaceId != null)
                {
                    var workspaceIdContent = new StringContent(workspaceId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    workspaceIdContent.Headers.ContentType = null;
                    formData.Add(workspaceIdContent, "workspace_id");
                }

                if (versionDescription != null)
                {
                    var versionDescriptionContent = new StringContent(versionDescription, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    versionDescriptionContent.Headers.ContentType = null;
                    formData.Add(versionDescriptionContent, "version_description");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/models/categories");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "CreateCategoriesModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CategoriesModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CategoriesModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for CreateCategoriesModel.
        /// </summary>
        public class CreateCategoriesModelEnums
        {
            /// <summary>
            /// The content type of trainingData.
            /// </summary>
            public class TrainingDataContentTypeValue
            {
                /// <summary>
                /// Constant JSON for json
                /// </summary>
                public const string JSON = "json";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                
            }
        }

        /// <summary>
        /// List categories models.
        ///
        /// (Beta) Returns all custom categories models associated with this service instance.
        /// </summary>
        /// <returns><see cref="CategoriesModelList" />CategoriesModelList</returns>
        public DetailedResponse<CategoriesModelList> ListCategoriesModels()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<CategoriesModelList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models/categories");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "ListCategoriesModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CategoriesModelList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CategoriesModelList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get categories model details.
        ///
        /// (Beta) Returns the status of the categories model with the given model ID.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <returns><see cref="CategoriesModel" />CategoriesModel</returns>
        public DetailedResponse<CategoriesModel> GetCategoriesModel(string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `GetCategoriesModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<CategoriesModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models/categories/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "GetCategoriesModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CategoriesModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CategoriesModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update categories model.
        ///
        /// (Beta) Overwrites the training data associated with this custom categories model and retrains the model. The
        /// new model replaces the current deployment.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <param name="language">The 2-letter language code of this model.</param>
        /// <param name="trainingData">Training data in JSON format. For more information, see [Categories training data
        /// requirements](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-categories##categories-training-data-requirements).</param>
        /// <param name="trainingDataContentType">The content type of trainingData. (optional)</param>
        /// <param name="name">An optional name for the model. (optional)</param>
        /// <param name="description">An optional description of the model. (optional)</param>
        /// <param name="modelVersion">An optional version string. (optional)</param>
        /// <param name="workspaceId">ID of the Watson Knowledge Studio workspace that deployed this model to Natural
        /// Language Understanding. (optional)</param>
        /// <param name="versionDescription">The description of the version. (optional)</param>
        /// <returns><see cref="CategoriesModel" />CategoriesModel</returns>
        public DetailedResponse<CategoriesModel> UpdateCategoriesModel(string modelId, string language, System.IO.MemoryStream trainingData, string trainingDataContentType = null, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `UpdateCategoriesModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentNullException("`language` is required for `UpdateCategoriesModel`");
            }
            if (trainingData == null)
            {
                throw new ArgumentNullException("`trainingData` is required for `UpdateCategoriesModel`");
            }
            DetailedResponse<CategoriesModel> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (language != null)
                {
                    var languageContent = new StringContent(language, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    languageContent.Headers.ContentType = null;
                    formData.Add(languageContent, "language");
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(trainingDataContentType, out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                if (name != null)
                {
                    var nameContent = new StringContent(name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    nameContent.Headers.ContentType = null;
                    formData.Add(nameContent, "name");
                }

                if (description != null)
                {
                    var descriptionContent = new StringContent(description, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    descriptionContent.Headers.ContentType = null;
                    formData.Add(descriptionContent, "description");
                }

                if (modelVersion != null)
                {
                    var modelVersionContent = new StringContent(modelVersion, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    modelVersionContent.Headers.ContentType = null;
                    formData.Add(modelVersionContent, "model_version");
                }

                if (workspaceId != null)
                {
                    var workspaceIdContent = new StringContent(workspaceId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    workspaceIdContent.Headers.ContentType = null;
                    formData.Add(workspaceIdContent, "workspace_id");
                }

                if (versionDescription != null)
                {
                    var versionDescriptionContent = new StringContent(versionDescription, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    versionDescriptionContent.Headers.ContentType = null;
                    formData.Add(versionDescriptionContent, "version_description");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/models/categories/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "UpdateCategoriesModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CategoriesModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CategoriesModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for UpdateCategoriesModel.
        /// </summary>
        public class UpdateCategoriesModelEnums
        {
            /// <summary>
            /// The content type of trainingData.
            /// </summary>
            public class TrainingDataContentTypeValue
            {
                /// <summary>
                /// Constant JSON for json
                /// </summary>
                public const string JSON = "json";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                
            }
        }

        /// <summary>
        /// Delete categories model.
        ///
        /// (Beta) Un-deploys the custom categories model with the given model ID and deletes all associated customer
        /// data, including any training data or binary artifacts.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <returns><see cref="DeleteModelResults" />DeleteModelResults</returns>
        public DetailedResponse<DeleteModelResults> DeleteCategoriesModel(string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `DeleteCategoriesModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<DeleteModelResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/models/categories/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "DeleteCategoriesModel"));
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
        /// <summary>
        /// Create classifications model.
        ///
        /// Creates a custom classifications model by uploading training data and associated metadata. The model begins
        /// the training and deploying process and is ready to use when the `status` is `available`.
        /// </summary>
        /// <param name="language">The 2-letter language code of this model.</param>
        /// <param name="trainingData">Training data in JSON format. For more information, see [Classifications training
        /// data
        /// requirements](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-classifications#classification-training-data-requirements).</param>
        /// <param name="trainingDataContentType">The content type of trainingData. (optional)</param>
        /// <param name="name">An optional name for the model. (optional)</param>
        /// <param name="description">An optional description of the model. (optional)</param>
        /// <param name="modelVersion">An optional version string. (optional)</param>
        /// <param name="workspaceId">ID of the Watson Knowledge Studio workspace that deployed this model to Natural
        /// Language Understanding. (optional)</param>
        /// <param name="versionDescription">The description of the version. (optional)</param>
        /// <returns><see cref="ClassificationsModel" />ClassificationsModel</returns>
        public DetailedResponse<ClassificationsModel> CreateClassificationsModel(string language, System.IO.MemoryStream trainingData, string trainingDataContentType = null, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentNullException("`language` is required for `CreateClassificationsModel`");
            }
            if (trainingData == null)
            {
                throw new ArgumentNullException("`trainingData` is required for `CreateClassificationsModel`");
            }
            DetailedResponse<ClassificationsModel> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (language != null)
                {
                    var languageContent = new StringContent(language, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    languageContent.Headers.ContentType = null;
                    formData.Add(languageContent, "language");
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(trainingDataContentType, out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                if (name != null)
                {
                    var nameContent = new StringContent(name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    nameContent.Headers.ContentType = null;
                    formData.Add(nameContent, "name");
                }

                if (description != null)
                {
                    var descriptionContent = new StringContent(description, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    descriptionContent.Headers.ContentType = null;
                    formData.Add(descriptionContent, "description");
                }

                if (modelVersion != null)
                {
                    var modelVersionContent = new StringContent(modelVersion, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    modelVersionContent.Headers.ContentType = null;
                    formData.Add(modelVersionContent, "model_version");
                }

                if (workspaceId != null)
                {
                    var workspaceIdContent = new StringContent(workspaceId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    workspaceIdContent.Headers.ContentType = null;
                    formData.Add(workspaceIdContent, "workspace_id");
                }

                if (versionDescription != null)
                {
                    var versionDescriptionContent = new StringContent(versionDescription, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    versionDescriptionContent.Headers.ContentType = null;
                    formData.Add(versionDescriptionContent, "version_description");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/models/classifications");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "CreateClassificationsModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ClassificationsModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ClassificationsModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for CreateClassificationsModel.
        /// </summary>
        public class CreateClassificationsModelEnums
        {
            /// <summary>
            /// The content type of trainingData.
            /// </summary>
            public class TrainingDataContentTypeValue
            {
                /// <summary>
                /// Constant JSON for json
                /// </summary>
                public const string JSON = "json";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                
            }
        }

        /// <summary>
        /// List classifications models.
        ///
        /// Returns all custom classifications models associated with this service instance.
        /// </summary>
        /// <returns><see cref="ClassificationsModelList" />ClassificationsModelList</returns>
        public DetailedResponse<ClassificationsModelList> ListClassificationsModels()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ClassificationsModelList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models/classifications");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "ListClassificationsModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ClassificationsModelList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ClassificationsModelList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get classifications model details.
        ///
        /// Returns the status of the classifications model with the given model ID.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <returns><see cref="ClassificationsModel" />ClassificationsModel</returns>
        public DetailedResponse<ClassificationsModel> GetClassificationsModel(string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `GetClassificationsModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<ClassificationsModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models/classifications/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "GetClassificationsModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ClassificationsModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ClassificationsModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update classifications model.
        ///
        /// Overwrites the training data associated with this custom classifications model and retrains the model. The
        /// new model replaces the current deployment.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <param name="language">The 2-letter language code of this model.</param>
        /// <param name="trainingData">Training data in JSON format. For more information, see [Classifications training
        /// data
        /// requirements](https://cloud.ibm.com/docs/natural-language-understanding?topic=natural-language-understanding-classifications#classification-training-data-requirements).</param>
        /// <param name="trainingDataContentType">The content type of trainingData. (optional)</param>
        /// <param name="name">An optional name for the model. (optional)</param>
        /// <param name="description">An optional description of the model. (optional)</param>
        /// <param name="modelVersion">An optional version string. (optional)</param>
        /// <param name="workspaceId">ID of the Watson Knowledge Studio workspace that deployed this model to Natural
        /// Language Understanding. (optional)</param>
        /// <param name="versionDescription">The description of the version. (optional)</param>
        /// <returns><see cref="ClassificationsModel" />ClassificationsModel</returns>
        public DetailedResponse<ClassificationsModel> UpdateClassificationsModel(string modelId, string language, System.IO.MemoryStream trainingData, string trainingDataContentType = null, string name = null, string description = null, string modelVersion = null, string workspaceId = null, string versionDescription = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `UpdateClassificationsModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentNullException("`language` is required for `UpdateClassificationsModel`");
            }
            if (trainingData == null)
            {
                throw new ArgumentNullException("`trainingData` is required for `UpdateClassificationsModel`");
            }
            DetailedResponse<ClassificationsModel> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (language != null)
                {
                    var languageContent = new StringContent(language, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    languageContent.Headers.ContentType = null;
                    formData.Add(languageContent, "language");
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(trainingDataContentType, out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                if (name != null)
                {
                    var nameContent = new StringContent(name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    nameContent.Headers.ContentType = null;
                    formData.Add(nameContent, "name");
                }

                if (description != null)
                {
                    var descriptionContent = new StringContent(description, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    descriptionContent.Headers.ContentType = null;
                    formData.Add(descriptionContent, "description");
                }

                if (modelVersion != null)
                {
                    var modelVersionContent = new StringContent(modelVersion, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    modelVersionContent.Headers.ContentType = null;
                    formData.Add(modelVersionContent, "model_version");
                }

                if (workspaceId != null)
                {
                    var workspaceIdContent = new StringContent(workspaceId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    workspaceIdContent.Headers.ContentType = null;
                    formData.Add(workspaceIdContent, "workspace_id");
                }

                if (versionDescription != null)
                {
                    var versionDescriptionContent = new StringContent(versionDescription, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    versionDescriptionContent.Headers.ContentType = null;
                    formData.Add(versionDescriptionContent, "version_description");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/models/classifications/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "UpdateClassificationsModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ClassificationsModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ClassificationsModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for UpdateClassificationsModel.
        /// </summary>
        public class UpdateClassificationsModelEnums
        {
            /// <summary>
            /// The content type of trainingData.
            /// </summary>
            public class TrainingDataContentTypeValue
            {
                /// <summary>
                /// Constant JSON for json
                /// </summary>
                public const string JSON = "json";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                
            }
        }

        /// <summary>
        /// Delete classifications model.
        ///
        /// Un-deploys the custom classifications model with the given model ID and deletes all associated customer
        /// data, including any training data or binary artifacts.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <returns><see cref="DeleteModelResults" />DeleteModelResults</returns>
        public DetailedResponse<DeleteModelResults> DeleteClassificationsModel(string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `DeleteClassificationsModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<DeleteModelResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/models/classifications/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("natural-language-understanding", "v1", "DeleteClassificationsModel"));
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
