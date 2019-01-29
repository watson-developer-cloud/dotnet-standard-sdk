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
using System.IO;
using System.Net.Http;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1.Model;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using System;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1
{
    public partial class NaturalLanguageClassifierService : WatsonService, INaturalLanguageClassifierService
    {
        const string SERVICE_NAME = "natural_language_classifier";
        const string URL = "https://gateway.watsonplatform.net/natural-language-classifier/api";
        public NaturalLanguageClassifierService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public NaturalLanguageClassifierService(string userName, string password) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }

        public NaturalLanguageClassifierService(TokenOptions options) : this()
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

        public NaturalLanguageClassifierService(IClient httpClient) : this()
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
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classification" />Classification</returns>
        public Classification Classify(string classifierId, ClassifyInput body, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            Classification result = null;

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
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers/{classifierId}/classify");

                restRequest.WithBody<ClassifyInput>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
        
                restRequest.WithHeader("X-IBMCloud-SDK-Analytics", "service_name=natural_language_classifier;service_version=v1;operation_id=Classify");
                result = restRequest.As<Classification>().Result;
                if(result == null)
                    result = new Classification();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ClassificationCollection" />ClassificationCollection</returns>
        public ClassificationCollection ClassifyCollection(string classifierId, ClassifyCollectionInput body, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            ClassificationCollection result = null;

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
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers/{classifierId}/classify_collection");

                restRequest.WithBody<ClassifyCollectionInput>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
        
                restRequest.WithHeader("X-IBMCloud-SDK-Analytics", "service_name=natural_language_classifier;service_version=v1;operation_id=ClassifyCollection");
                result = restRequest.As<ClassificationCollection>().Result;
                if(result == null)
                    result = new ClassificationCollection();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public Classifier CreateClassifier(System.IO.FileStream metadata, System.IO.FileStream trainingData, Dictionary<string, object> customData = null)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (trainingData == null)
                throw new ArgumentNullException(nameof(trainingData));
            Classifier result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (metadata != null)
                {
                    var metadataContent = new ByteArrayContent((metadata as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/json", out contentType);
                    metadataContent.Headers.ContentType = contentType;
                    formData.Add(metadataContent, "training_metadata", metadata.Name);
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent((trainingData as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/csv", out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", trainingData.Name);
                }

                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers");

                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
        
                restRequest.WithHeader("X-IBMCloud-SDK-Analytics", "service_name=natural_language_classifier;service_version=v1;operation_id=CreateClassifier");
                result = restRequest.As<Classifier>().Result;
                if(result == null)
                    result = new Classifier();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete classifier.
        /// </summary>
        /// <param name="classifierId">Classifier ID to delete.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));
            BaseModel result = null;

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
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/classifiers/{classifierId}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
        
                restRequest.WithHeader("X-IBMCloud-SDK-Analytics", "service_name=natural_language_classifier;service_version=v1;operation_id=DeleteClassifier");
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public Classifier GetClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));
            Classifier result = null;

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
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/classifiers/{classifierId}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
        
                restRequest.WithHeader("X-IBMCloud-SDK-Analytics", "service_name=natural_language_classifier;service_version=v1;operation_id=GetClassifier");
                result = restRequest.As<Classifier>().Result;
                if(result == null)
                    result = new Classifier();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ClassifierList" />ClassifierList</returns>
        public ClassifierList ListClassifiers(Dictionary<string, object> customData = null)
        {
            ClassifierList result = null;

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
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/classifiers");

                if (customData != null)
                    restRequest.WithCustomData(customData);
        
                restRequest.WithHeader("X-IBMCloud-SDK-Analytics", "service_name=natural_language_classifier;service_version=v1;operation_id=ListClassifiers");
                result = restRequest.As<ClassifierList>().Result;
                if(result == null)
                    result = new ClassifierList();
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
