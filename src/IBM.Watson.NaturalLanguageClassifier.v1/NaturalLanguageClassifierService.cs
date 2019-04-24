/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.NaturalLanguageClassifier.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.NaturalLanguageClassifier.v1
{
    public partial class NaturalLanguageClassifierService : IBMService, INaturalLanguageClassifierService
    {
        new const string SERVICE_NAME = "natural_language_classifier";
        const string URL = "https://gateway.watsonplatform.net/natural-language-classifier/api";
        public NaturalLanguageClassifierService() : base(SERVICE_NAME) { }
        
        public NaturalLanguageClassifierService(string userName, string password) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }
        
        public NaturalLanguageClassifierService(TokenOptions options) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
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

        public NaturalLanguageClassifierService(IClient httpClient) : base(SERVICE_NAME, URL)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Classify a phrase.
        ///
        /// Returns label information for the input. The status must be `Available` before you can use the classifier to
        /// classify text.
        /// </summary>
        /// <param name="classifierId">Classifier ID to use.</param>
        /// <param name="body">Phrase to classify.</param>
        /// <returns><see cref="Classification" />Classification</returns>
        public DetailedResponse<Classification> Classify(string classifierId, string text)
        {
        if (string.IsNullOrEmpty(classifierId))
            throw new ArgumentNullException("`classifierId` is required for `Classify`");
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("`text` is required for `Classify`");
            DetailedResponse<Classification> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers/{classifierId}/classify");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(text))
                    bodyObject["text"] = text;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("natural_language_classifier", "v1", "Classify"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Classification>().Result;
                if (result == null)
                    result = new DetailedResponse<Classification>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Classify multiple phrases.
        ///
        /// Returns label information for multiple phrases. The status must be `Available` before you can use the
        /// classifier to classify text.
        ///
        /// Note that classifying Japanese texts is a beta feature.
        /// </summary>
        /// <param name="classifierId">Classifier ID to use.</param>
        /// <param name="body">Phrase to classify. You can submit up to 30 text phrases in a request.</param>
        /// <returns><see cref="ClassificationCollection" />ClassificationCollection</returns>
        public DetailedResponse<ClassificationCollection> ClassifyCollection(string classifierId, List<ClassifyInput> collection)
        {
        if (string.IsNullOrEmpty(classifierId))
            throw new ArgumentNullException("`classifierId` is required for `ClassifyCollection`");
        if (collection == null)
            throw new ArgumentNullException("`collection` is required for `ClassifyCollection`");
            DetailedResponse<ClassificationCollection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers/{classifierId}/classify_collection");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (collection != null && collection.Count > 0)
                    bodyObject["collection"] = JToken.FromObject(collection);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("natural_language_classifier", "v1", "ClassifyCollection"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ClassificationCollection>().Result;
                if (result == null)
                    result = new DetailedResponse<ClassificationCollection>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create classifier.
        ///
        /// Sends data to create and train a classifier and returns information about the new classifier.
        /// </summary>
        /// <param name="metadata">Metadata in JSON format. The metadata identifies the language of the data, and an
        /// optional name to identify the classifier. Specify the language with the 2-letter primary language code as
        /// assigned in ISO standard 639.
        ///
        /// Supported languages are English (`en`), Arabic (`ar`), French (`fr`), German, (`de`), Italian (`it`),
        /// Japanese (`ja`), Korean (`ko`), Brazilian Portuguese (`pt`), and Spanish (`es`).</param>
        /// <param name="trainingData">Training data in CSV format. Each text value must have at least one class. The
        /// data can include up to 3,000 classes and 20,000 records. For details, see [Data
        /// preparation](https://cloud.ibm.com/docs/services/natural-language-classifier/using-your-data.html).</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public DetailedResponse<Classifier> CreateClassifier(System.IO.MemoryStream metadata, System.IO.MemoryStream trainingData)
        {
        if (metadata == null)
            throw new ArgumentNullException("`metadata` is required for `CreateClassifier`");
        if (trainingData == null)
            throw new ArgumentNullException("`trainingData` is required for `CreateClassifier`");
            DetailedResponse<Classifier> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (metadata != null)
                {
                    var metadataContent = new ByteArrayContent(metadata.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/json", out contentType);
                    metadataContent.Headers.ContentType = contentType;
                    formData.Add(metadataContent, "training_metadata", "filename");
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/csv", out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithBodyContent(formData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("natural_language_classifier", "v1", "CreateClassifier"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Classifier>().Result;
                if (result == null)
                    result = new DetailedResponse<Classifier>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List classifiers.
        ///
        /// Returns an empty array if no classifiers are available.
        /// </summary>
        /// <returns><see cref="ClassifierList" />ClassifierList</returns>
        public DetailedResponse<ClassifierList> ListClassifiers()
        {
            DetailedResponse<ClassifierList> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/classifiers");

                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("natural_language_classifier", "v1", "ListClassifiers"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ClassifierList>().Result;
                if (result == null)
                    result = new DetailedResponse<ClassifierList>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get information about a classifier.
        ///
        /// Returns status and other information about a classifier.
        /// </summary>
        /// <param name="classifierId">Classifier ID to query.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public DetailedResponse<Classifier> GetClassifier(string classifierId)
        {
        if (string.IsNullOrEmpty(classifierId))
            throw new ArgumentNullException("`classifierId` is required for `GetClassifier`");
            DetailedResponse<Classifier> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/classifiers/{classifierId}");

                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("natural_language_classifier", "v1", "GetClassifier"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Classifier>().Result;
                if (result == null)
                    result = new DetailedResponse<Classifier>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete classifier.
        /// </summary>
        /// <param name="classifierId">Classifier ID to delete.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteClassifier(string classifierId)
        {
        if (string.IsNullOrEmpty(classifierId))
            throw new ArgumentNullException("`classifierId` is required for `DeleteClassifier`");
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/classifiers/{classifierId}");

                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("natural_language_classifier", "v1", "DeleteClassifier"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
