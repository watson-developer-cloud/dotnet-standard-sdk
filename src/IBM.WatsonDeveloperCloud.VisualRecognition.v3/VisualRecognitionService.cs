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

using System.IO;
using System.Net.Http;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using System;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public class VisualRecognitionService : WatsonService, IVisualRecognitionService
    {
        const string SERVICE_NAME = "visual_recognition";
        const string URL = "https://gateway-a.watsonplatform.net/visual-recognition/api";

        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        /** The Constant VISUAL_RECOGNITION_VERSION_DATE_2016_05_20. */
        public static string VISUAL_RECOGNITION_VERSION_DATE_2016_05_20 = "2016-05-20";

        public VisualRecognitionService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public VisualRecognitionService(string apikey, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(apikey))
                throw new ArgumentNullException(nameof(apikey));

            this.SetCredential(apikey);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'VISUAL_RECOGNITION_VERSION_DATE_2016_05_20'");

            VersionDate = versionDate;
        }

        public VisualRecognitionService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public ClassifiedImages Classify(System.IO.Stream imagesFile = null, string parameters = null, string acceptLanguage = null, string imagesFileContentType = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'VISUAL_RECOGNITION_VERSION_DATE_2016_05_20'");

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
                    formData.Add(imagesFileContent, "images_file", "fileName");
                }

                if (parameters != null)
                {
                    var parametersContent = new StringContent(parameters, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    parametersContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    formData.Add(parametersContent, "parameters");
                }

                result = this.Client.PostAsync($"{this.Endpoint}/v3/classify")
                                .WithArgument("version", VersionDate)
                                .WithArgument("api_key", ApiKey)
                                .WithHeader("Accept-Language", acceptLanguage)
                                .WithBodyContent(formData)
                                .As<ClassifiedImages>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public DetectedFaces DetectFaces(System.IO.Stream imagesFile = null, string parameters = null, string imagesFileContentType = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'VISUAL_RECOGNITION_VERSION_DATE_2016_05_20'");

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

                if (parameters != null)
                {
                    var parametersContent = new StringContent(parameters, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(parametersContent, "parameters");
                }

                result = this.Client.PostAsync($"{this.Endpoint}/v3/detect_faces")
                                .WithArgument("version", VersionDate)
                                .WithArgument("api_key", ApiKey)
                                .WithBodyContent(formData)
                                .As<DetectedFaces>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public Classifier CreateClassifier(string name, System.IO.Stream classnamePositiveExamples, System.IO.Stream negativeExamples = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (classnamePositiveExamples == null)
                throw new ArgumentNullException(nameof(classnamePositiveExamples));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'VISUAL_RECOGNITION_VERSION_DATE_2016_05_20'");

            Classifier result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (name != null)
                {
                    var nameContent = new StringContent(name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(nameContent, "name");
                }

                if (classnamePositiveExamples != null)
                {
                    var classnamePositiveExamplesContent = new ByteArrayContent((classnamePositiveExamples as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    classnamePositiveExamplesContent.Headers.ContentType = contentType;
                    formData.Add(classnamePositiveExamplesContent, "classname_positive_examples", "filename");
                }

                if (negativeExamples != null)
                {
                    var negativeExamplesContent = new ByteArrayContent((negativeExamples as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    negativeExamplesContent.Headers.ContentType = contentType;
                    formData.Add(negativeExamplesContent, "negative_examples", "filename");
                }

                result = this.Client.PostAsync($"{this.Endpoint}/v3/classifiers")
                                .WithArgument("version", VersionDate)
                                .WithArgument("api_key", ApiKey)
                                .WithBodyContent(formData)
                                .As<Classifier>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteClassifier(string classifierId)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'VISUAL_RECOGNITION_VERSION_DATE_2016_05_20'");

            object result = null;

            try
            {
                result = this.Client.DeleteAsync($"{this.Endpoint}/v3/classifiers/{classifierId}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("api_key", ApiKey)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Classifier GetClassifier(string classifierId)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'VISUAL_RECOGNITION_VERSION_DATE_2016_05_20'");

            Classifier result = null;

            try
            {
                result = this.Client.GetAsync($"{this.Endpoint}/v3/classifiers/{classifierId}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("api_key", ApiKey)
                                .As<Classifier>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Classifiers ListClassifiers(bool? verbose = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'VISUAL_RECOGNITION_VERSION_DATE_2016_05_20'");

            Classifiers result = null;

            try
            {
                result = this.Client.GetAsync($"{this.Endpoint}/v3/classifiers")
                                .WithArgument("version", VersionDate)
                                .WithArgument("verbose", verbose)
                                .WithArgument("api_key", ApiKey)
                                .As<Classifiers>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Classifier UpdateClassifier(string classifierId, System.IO.Stream classnamePositiveExamples = null, System.IO.Stream negativeExamples = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'VISUAL_RECOGNITION_VERSION_DATE_2016_05_20'");

            Classifier result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (classnamePositiveExamples != null)
                {
                    var classnamePositiveExamplesContent = new ByteArrayContent((classnamePositiveExamples as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    classnamePositiveExamplesContent.Headers.ContentType = contentType;
                    formData.Add(classnamePositiveExamplesContent, "classname_positive_examples", "filename");
                }

                if (negativeExamples != null)
                {
                    var negativeExamplesContent = new ByteArrayContent((negativeExamples as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    negativeExamplesContent.Headers.ContentType = contentType;
                    formData.Add(negativeExamplesContent, "negative_examples", "filename");
                }

                result = this.Client.PostAsync($"{this.Endpoint}/v3/classifiers/{classifierId}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("api_key", ApiKey)
                                .WithBodyContent(formData)
                                .As<Classifier>()
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
