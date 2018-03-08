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

using System.IO;
using System.Net.Http;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using System;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public partial class VisualRecognitionService : WatsonService, IVisualRecognitionService
    {
        const string SERVICE_NAME = "visual_recognition";
        const string URL = "https://gateway-a.watsonplatform.net/visual-recognition/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public VisualRecognitionService() : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }


        public VisualRecognitionService(string apikey, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(apikey))
                throw new ArgumentNullException(nameof(apikey));

            this.SetCredential(apikey);

            if (string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public VisualRecognitionService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public ClassifiedImages Classify(System.IO.Stream imagesFile = null, string acceptLanguage = null, string url = null, float? threshold = null, List<string> owners = null, List<string> classifierIds = null, string imagesFileContentType = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ClassifiedImages result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (imagesFile != null)
                {
                    var imagesFileContent = new ByteArrayContent((imagesFile as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(imagesFileContentType, out contentType);
                    imagesFileContent.Headers.ContentType = contentType;
                    formData.Add(imagesFileContent, "images_file", "filename");
                }

                if (url != null)
                {
                    var urlContent = new StringContent(url, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    urlContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    formData.Add(urlContent, "url");
                }

                if (threshold != null)
                {
                    var thresholdContent = new StringContent(threshold.ToString(), Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(thresholdContent, "threshold");
                }

                if (owners != null)
                {
                    var ownersContent = new StringContent(string.Join(",", owners.ToArray()), Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(ownersContent, "owners");
                }

                if (classifierIds != null)
                {
                    var classifierIdsContent = new StringContent(string.Join(",", classifierIds.ToArray()), Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(classifierIdsContent, "classifier_ids");
                }

                var request = this.Client.PostAsync($"{this.Endpoint}/v3/classify");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                request.WithHeader("Accept-Language", acceptLanguage);
                request.WithBodyContent(formData);
                result = request.As<ClassifiedImages>().Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public DetectedFaces DetectFaces(System.IO.Stream imagesFile = null, string url = null, string imagesFileContentType = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetectedFaces result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (imagesFile != null)
                {
                    var imagesFileContent = new ByteArrayContent((imagesFile as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(imagesFileContentType, out contentType);
                    imagesFileContent.Headers.ContentType = contentType;
                    formData.Add(imagesFileContent, "images_file", "filename");
                }

                if (url != null)
                {
                    var urlContent = new StringContent(url, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    urlContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain");
                    formData.Add(urlContent, "url");
                }

                var request = this.Client.PostAsync($"{this.Endpoint}/v3/detect_faces");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                request.WithBodyContent(formData);
                result = request.As<DetectedFaces>().Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteClassifier(string classifierId)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.DeleteAsync($"{this.Endpoint}/v3/classifiers/{classifierId}");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Classifier GetClassifier(string classifierId)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifier result = null;

            try
            {
                var request = this.Client.GetAsync($"{this.Endpoint}/v3/classifiers/{classifierId}");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                result = request.As<Classifier>().Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Classifiers ListClassifiers(bool? verbose = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifiers result = null;

            try
            {
                var request = this.Client.GetAsync($"{this.Endpoint}/v3/classifiers");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                if (verbose != null)
                    request.WithArgument("verbose", verbose);
                result = request.As<Classifiers>().Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
