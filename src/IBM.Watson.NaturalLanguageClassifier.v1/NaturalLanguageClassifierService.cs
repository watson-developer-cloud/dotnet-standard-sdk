/**
* (C) Copyright IBM Corp. 2018, 2020.
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
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.NaturalLanguageClassifier.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.NaturalLanguageClassifier.v1
{
    public partial class NaturalLanguageClassifierService : IBMService, INaturalLanguageClassifierService
    {
        const string defaultServiceName = "natural_language_classifier";
        private const string defaultServiceUrl = "https://gateway.watsonplatform.net/natural-language-classifier/api";

        public NaturalLanguageClassifierService() : this(ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public NaturalLanguageClassifierService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public NaturalLanguageClassifierService(IAuthenticator authenticator) : base(defaultServiceName, authenticator)
        {

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
        }

        /// <summary>
        /// Classify a phrase.
        ///
        /// Returns label information for the input. The status must be `Available` before you can use the classifier to
        /// classify text.
        /// </summary>
        /// <param name="classifierId">Classifier ID to use.</param>
        /// <param name="text">The submitted phrase. The maximum length is 2048 characters.</param>
        /// <returns><see cref="Classification" />Classification</returns>
        public DetailedResponse<Classification> Classify(string classifierId, string text)
        {
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `Classify`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `Classify`");
            }
            DetailedResponse<Classification> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers/{classifierId}/classify");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(text))
                {
                    bodyObject["text"] = text;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural_language_classifier", "v1", "Classify"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Classification>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Classification>();
                }
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
        /// <param name="collection">The submitted phrases.</param>
        /// <returns><see cref="ClassificationCollection" />ClassificationCollection</returns>
        public DetailedResponse<ClassificationCollection> ClassifyCollection(string classifierId, List<ClassifyInput> collection)
        {
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `ClassifyCollection`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            if (collection == null)
            {
                throw new ArgumentNullException("`collection` is required for `ClassifyCollection`");
            }
            DetailedResponse<ClassificationCollection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers/{classifierId}/classify_collection");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (collection != null && collection.Count > 0)
                {
                    bodyObject["collection"] = JToken.FromObject(collection);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural_language_classifier", "v1", "ClassifyCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ClassificationCollection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ClassificationCollection>();
                }
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
        /// <param name="trainingMetadata">Metadata in JSON format. The metadata identifies the language of the data,
        /// and an optional name to identify the classifier. Specify the language with the 2-letter primary language
        /// code as assigned in ISO standard 639.
        ///
        /// Supported languages are English (`en`), Arabic (`ar`), French (`fr`), German, (`de`), Italian (`it`),
        /// Japanese (`ja`), Korean (`ko`), Brazilian Portuguese (`pt`), and Spanish (`es`).</param>
        /// <param name="trainingData">Training data in CSV format. Each text value must have at least one class. The
        /// data can include up to 3,000 classes and 20,000 records. For details, see [Data
        /// preparation](https://cloud.ibm.com/docs/services/natural-language-classifier?topic=natural-language-classifier-using-your-data).</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public DetailedResponse<Classifier> CreateClassifier(System.IO.MemoryStream trainingMetadata, System.IO.MemoryStream trainingData)
        {
            if (trainingMetadata == null)
            {
                throw new ArgumentNullException("`trainingMetadata` is required for `CreateClassifier`");
            }
            if (trainingData == null)
            {
                throw new ArgumentNullException("`trainingData` is required for `CreateClassifier`");
            }
            DetailedResponse<Classifier> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (trainingMetadata != null)
                {
                    var trainingMetadataContent = new ByteArrayContent(trainingMetadata.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/json", out contentType);
                    trainingMetadataContent.Headers.ContentType = contentType;
                    formData.Add(trainingMetadataContent, "training_metadata", "filename");
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
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/classifiers");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("natural_language_classifier", "v1", "CreateClassifier"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Classifier>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Classifier>();
                }
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
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/classifiers");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("natural_language_classifier", "v1", "ListClassifiers"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ClassifierList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ClassifierList>();
                }
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
            {
                throw new ArgumentNullException("`classifierId` is required for `GetClassifier`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            DetailedResponse<Classifier> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/classifiers/{classifierId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("natural_language_classifier", "v1", "GetClassifier"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Classifier>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Classifier>();
                }
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
            {
                throw new ArgumentNullException("`classifierId` is required for `DeleteClassifier`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/classifiers/{classifierId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("natural_language_classifier", "v1", "DeleteClassifier"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
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
