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
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public VisualRecognitionService(string apikey, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(apikey))
                throw new ArgumentNullException(nameof(apikey));

            this.SetCredential(apikey);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public VisualRecognitionService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Classify images. Classify images with built-in or custom classifiers.
        /// </summary>
        /// <param name="imagesFile">An image file (.jpg, .png) or .zip file with images. Maximum image size is 10 MB. Include no more than 20 images and limit the .zip file to 100 MB. Encode the image and .zip file names in UTF-8 if they contain non-ASCII characters. The service assumes UTF-8 encoding if it encounters non-ASCII characters.  You can also include an image with the **url** parameter. (optional)</param>
        /// <param name="acceptLanguage">The language of the output class names. The full set of languages is supported only for the built-in `default` classifier ID. The class names of custom classifiers are not translated.  The response might not be in the specified language when the requested language is not supported or when there is no translation for the class name. (optional, default to en)</param>
        /// <param name="url">The URL of an image to analyze. Must be in .jpg, or .png format. The minimum recommended pixel density is 32X32 pixels per inch, and the maximum image size is 10 MB.  You can also include images with the **images_file** parameter. (optional)</param>
        /// <param name="threshold">The minimum score a class must have to be displayed in the response. Set the threshold to `0.0` to ignore the classification score and return all values. (optional, default to 0.5)</param>
        /// <param name="owners">The categories of classifiers to apply. Use `IBM` to classify against the `default` general classifier, and use `me` to classify against your custom classifiers. To analyze the image against both classifier categories, set the value to both `IBM` and `me`.   The built-in `default` classifier is used if both **classifier_ids** and **owners** parameters are empty.  The **classifier_ids** parameter overrides **owners**, so make sure that **classifier_ids** is empty. (optional)</param>
        /// <param name="classifierIds">Which classifiers to apply. Overrides the **owners** parameter. You can specify both custom and built-in classifier IDs. The built-in `default` classifier is used if both **classifier_ids** and **owners** parameters are empty.  The following built-in classifier IDs require no training: - `default`: Returns classes from thousands of general tags. - `food`: (Beta) Enhances specificity and accuracy for images of food items. - `explicit`: (Beta) Evaluates whether the image might be pornographic. (optional)</param>
        /// <param name="imagesFileContentType">The content type of imagesFile. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ClassifiedImages" />ClassifiedImages</returns>
        public ClassifiedImages Classify(System.IO.Stream imagesFile = null, string acceptLanguage = null, string url = null, float? threshold = null, List<string> owners = null, List<string> classifierIds = null, string imagesFileContentType = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
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
                    var ownersContent = new StringContent(string.Join(", ", owners.ToArray()), Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(ownersContent, "owners");
                }

                if (classifierIds != null)
                {
                    var classifierIdsContent = new StringContent(string.Join(", ", classifierIds.ToArray()), Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(classifierIdsContent, "classifier_ids");
                }

                var request = this.Client.PostAsync($"{this.Endpoint}/v3/classify");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                request.WithHeader("Accept-Language", acceptLanguage);
                request.WithBodyContent(formData);
                if (customData != null)
                    request.WithCustomData(customData);
                result = request.As<ClassifiedImages>().Result;
                if(result == null)
                    result = new ClassifiedImages();
                result.CustomData = request.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Detect faces in images. **Important:** On April 2, 2018, the identity information in the response to calls to the Face model was removed. The identity information refers to the `name` of the person, `score`, and `type_hierarchy` knowledge graph. For details about the enhanced Face model, see the [Release notes](https://console.bluemix.net/docs/services/visual-recognition/release-notes.html#2april2018).  Analyze and get data about faces in images. Responses can include estimated age and gender. This feature uses a built-in model, so no training is necessary. The Detect faces method does not support general biometric facial recognition.  Supported image formats include .gif, .jpg, .png, and .tif. The maximum image size is 10 MB. The minimum recommended pixel density is 32X32 pixels per inch.
        /// </summary>
        /// <param name="imagesFile">An image file (gif, .jpg, .png, .tif.) or .zip file with images. Limit the .zip file to 100 MB. You can include a maximum of 15 images in a request.  Encode the image and .zip file names in UTF-8 if they contain non-ASCII characters. The service assumes UTF-8 encoding if it encounters non-ASCII characters.  You can also include an image with the **url** parameter. (optional)</param>
        /// <param name="url">The URL of an image to analyze. Must be in .gif, .jpg, .png, or .tif format. The minimum recommended pixel density is 32X32 pixels per inch, and the maximum image size is 10 MB. Redirects are followed, so you can use a shortened URL.  You can also include images with the **images_file** parameter. (optional)</param>
        /// <param name="imagesFileContentType">The content type of imagesFile. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DetectedFaces" />DetectedFaces</returns>
        public DetectedFaces DetectFaces(System.IO.Stream imagesFile = null, string url = null, string imagesFileContentType = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
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
                    urlContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    formData.Add(urlContent, "url");
                }

                var request = this.Client.PostAsync($"{this.Endpoint}/v3/detect_faces");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                request.WithBodyContent(formData);
                if (customData != null)
                    request.WithCustomData(customData);
                result = request.As<DetectedFaces>().Result;
                if(result == null)
                    result = new DetectedFaces();
                result.CustomData = request.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        
        /// <summary>
        /// Delete a classifier. 
        /// </summary>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BaseModel result = null;

            try
            {
                var request = this.Client.DeleteAsync($"{this.Endpoint}/v3/classifiers/{classifierId}");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                if (customData != null)
                    request.WithCustomData(customData);
                result = request.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = request.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Retrieve classifier details. Retrieve information about a custom classifier.
        /// </summary>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public Classifier GetClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifier result = null;

            try
            {
                var request = this.Client.GetAsync($"{this.Endpoint}/v3/classifiers/{classifierId}");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                if (customData != null)
                    request.WithCustomData(customData);
                result = request.As<Classifier>().Result;
                if(result == null)
                    result = new Classifier();
                result.CustomData = request.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Retrieve a list of classifiers. 
        /// </summary>
        /// <param name="verbose">Specify `true` to return details about the classifiers. Omit this parameter to return a brief list of classifiers. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifiers" />Classifiers</returns>
        public Classifiers ListClassifiers(bool? verbose = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifiers result = null;

            try
            {
                var request = this.Client.GetAsync($"{this.Endpoint}/v3/classifiers");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                if (verbose != null)
                    request.WithArgument("verbose", verbose);
                if (customData != null)
                    request.WithCustomData(customData);
                result = request.As<Classifiers>().Result;
                if(result == null)
                    result = new Classifiers();
                result.CustomData = request.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
