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
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using System;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public partial class VisualRecognitionService : WatsonService, IVisualRecognitionService
    {
        const string SERVICE_NAME = "visual_recognition";
        const string URL = "https://gateway.watsonplatform.net/visual-recognition/api";
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

        public VisualRecognitionService(, string versionDate) : this()
        {
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public VisualRecognitionService(TokenOptions options, string versionDate) : this()
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

        public VisualRecognitionService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Classify images.
        ///
        /// Classify images with built-in or custom classifiers.
        /// </summary>
        /// <param name="imagesFile">An image file (.jpg, .png) or .zip file with images. Maximum image size is 10 MB.
        /// Include no more than 20 images and limit the .zip file to 100 MB. Encode the image and .zip file names in
        /// UTF-8 if they contain non-ASCII characters. The service assumes UTF-8 encoding if it encounters non-ASCII
        /// characters.
        ///
        /// You can also include an image with the **url** parameter. (optional)</param>
        /// <param name="acceptLanguage">The language of the output class names. The full set of languages is supported
        /// for the built-in classifier IDs: `default`, `food`, and `explicit`. The class names of custom classifiers
        /// are not translated.
        ///
        /// The response might not be in the specified language when the requested language is not supported or when
        /// there is no translation for the class name. (optional, default to en)</param>
        /// <param name="url">The URL of an image to analyze. Must be in .jpg, or .png format. The minimum recommended
        /// pixel density is 32X32 pixels per inch, and the maximum image size is 10 MB.
        ///
        /// You can also include images with the **images_file** parameter. (optional)</param>
        /// <param name="threshold">The minimum score a class must have to be displayed in the response. Set the
        /// threshold to `0.0` to ignore the classification score and return all values. (optional, default to
        /// 0.5)</param>
        /// <param name="owners">The categories of classifiers to apply. Use `IBM` to classify against the `default`
        /// general classifier, and use `me` to classify against your custom classifiers. To analyze the image against
        /// both classifier categories, set the value to both `IBM` and `me`.
        ///
        /// The built-in `default` classifier is used if both **classifier_ids** and **owners** parameters are empty.
        ///
        /// The **classifier_ids** parameter overrides **owners**, so make sure that **classifier_ids** is empty.
        /// (optional)</param>
        /// <param name="classifierIds">Which classifiers to apply. Overrides the **owners** parameter. You can specify
        /// both custom and built-in classifier IDs. The built-in `default` classifier is used if both
        /// **classifier_ids** and **owners** parameters are empty.
        ///
        /// The following built-in classifier IDs require no training:
        /// - `default`: Returns classes from thousands of general tags.
        /// - `food`: Enhances specificity and accuracy for images of food items.
        /// - `explicit`: Evaluates whether the image might be pornographic. (optional)</param>
        /// <param name="imagesFileContentType">The content type of imagesFile. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ClassifiedImages" />ClassifiedImages</returns>
        public ClassifiedImages Classify(System.IO.FileStream imagesFile = null, string acceptLanguage = null, string url = null, float? threshold = null, List<string> owners = null, List<string> classifierIds = null, string imagesFileContentType = null, Dictionary<string, object> customData = null)
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
                    formData.Add(imagesFileContent, "images_file", imagesFile.Name);
                }

                if (url != null)
                {
                    var urlContent = new StringContent(url, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    urlContent.Headers.ContentType = null;
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

                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/classify");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(acceptLanguage))
                    restRequest.WithHeader("Accept-Language", acceptLanguage);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<ClassifiedImages>().Result;
                if(result == null)
                    result = new ClassifiedImages();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Detect faces in images.
        ///
        /// **Important:** On April 2, 2018, the identity information in the response to calls to the Face model was
        /// removed. The identity information refers to the `name` of the person, `score`, and `type_hierarchy`
        /// knowledge graph. For details about the enhanced Face model, see the [Release
        /// notes](https://cloud.ibm.com/docs/services/visual-recognition/release-notes.html#2april2018).
        ///
        /// Analyze and get data about faces in images. Responses can include estimated age and gender. This feature
        /// uses a built-in model, so no training is necessary. The Detect faces method does not support general
        /// biometric facial recognition.
        ///
        /// Supported image formats include .gif, .jpg, .png, and .tif. The maximum image size is 10 MB. The minimum
        /// recommended pixel density is 32X32 pixels per inch.
        /// </summary>
        /// <param name="imagesFile">An image file (gif, .jpg, .png, .tif.) or .zip file with images. Limit the .zip
        /// file to 100 MB. You can include a maximum of 15 images in a request.
        ///
        /// Encode the image and .zip file names in UTF-8 if they contain non-ASCII characters. The service assumes
        /// UTF-8 encoding if it encounters non-ASCII characters.
        ///
        /// You can also include an image with the **url** parameter. (optional)</param>
        /// <param name="url">The URL of an image to analyze. Must be in .gif, .jpg, .png, or .tif format. The minimum
        /// recommended pixel density is 32X32 pixels per inch, and the maximum image size is 10 MB. Redirects are
        /// followed, so you can use a shortened URL.
        ///
        /// You can also include images with the **images_file** parameter. (optional)</param>
        /// <param name="acceptLanguage">The language used for the value of `gender_label` in the response. (optional,
        /// default to en)</param>
        /// <param name="imagesFileContentType">The content type of imagesFile. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DetectedFaces" />DetectedFaces</returns>
        public DetectedFaces DetectFaces(System.IO.FileStream imagesFile = null, string url = null, string acceptLanguage = null, string imagesFileContentType = null, Dictionary<string, object> customData = null)
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
                    formData.Add(imagesFileContent, "images_file", imagesFile.Name);
                }

                if (url != null)
                {
                    var urlContent = new StringContent(url, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    urlContent.Headers.ContentType = null;
                    formData.Add(urlContent, "url");
                }

                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/detect_faces");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(acceptLanguage))
                    restRequest.WithHeader("Accept-Language", acceptLanguage);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DetectedFaces>().Result;
                if(result == null)
                    result = new DetectedFaces();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create a classifier.
        ///
        /// Train a new multi-faceted classifier on the uploaded image data. Create your custom classifier with positive
        /// or negative examples. Include at least two sets of examples, either two positive example files or one
        /// positive and one negative file. You can upload a maximum of 256 MB per call.
        ///
        /// Encode all names in UTF-8 if they contain non-ASCII characters (.zip and image file names, and classifier
        /// and class names). The service assumes UTF-8 encoding if it encounters non-ASCII characters.
        /// </summary>
        /// <param name="name">The name of the new classifier. Encode special characters in UTF-8.</param>
        /// <param name="positiveExamples">A .zip file of images that depict the visual subject of a class in the new
        /// classifier. You can include more than one positive example file in a call.
        ///
        /// Specify the parameter name by appending `_positive_examples` to the class name. For example,
        /// `goldenretriever_positive_examples` creates the class **goldenretriever**.
        ///
        /// Include at least 10 images in .jpg or .png format. The minimum recommended image resolution is 32X32 pixels.
        /// The maximum number of images is 10,000 images or 100 MB per .zip file.
        ///
        /// Encode special characters in the file name in UTF-8.</param>
        /// <param name="negativeExamples">A .zip file of images that do not depict the visual subject of any of the
        /// classes of the new classifier. Must contain a minimum of 10 images.
        ///
        /// Encode special characters in the file name in UTF-8. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public Classifier CreateClassifier(string name, Dictionary<string, System.IO.FileStream> positiveExamples, System.IO.FileStream negativeExamples = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (positiveExamples == null)
                throw new ArgumentNullException(nameof(positiveExamples));
            if (positiveExamples.Count == 0)
                throw new ArgumentException("positiveExamples must contain at least one dictionary entry");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifier result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (name != null)
                {
                    var nameContent = new StringContent(name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    nameContent.Headers.ContentType = null;
                    formData.Add(nameContent, "name");
                }

                if (positiveExamples != null && positiveExamples.Count > 0)
                {
                    foreach(KeyValuePair<string, System.IO.FileStream> entry in positiveExamples)
                    {
                        var partName = string.Format("{0}_positive_examples", entry.Key);
                        var partContent = new ByteArrayContent((entry.Value as Stream).ReadAllBytes());
                        System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                        System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                        partContent.Headers.ContentType = contentType;
                        formData.Add(partContent, partName, entry.Value.Name);
                    }
                }

                if (negativeExamples != null)
                {
                    var negativeExamplesContent = new ByteArrayContent((negativeExamples as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    negativeExamplesContent.Headers.ContentType = contentType;
                    formData.Add(negativeExamplesContent, "negative_examples", negativeExamples.Name);
                }

                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/classifiers");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
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
        /// Delete a classifier.
        /// </summary>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v3/classifiers/{classifierId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
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
        /// Retrieve classifier details.
        ///
        /// Retrieve information about a custom classifier.
        /// </summary>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public Classifier GetClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifier result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v3/classifiers/{classifierId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
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
        /// Retrieve a list of classifiers.
        /// </summary>
        /// <param name="verbose">Specify `true` to return details about the classifiers. Omit this parameter to return
        /// a brief list of classifiers. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifiers" />Classifiers</returns>
        public Classifiers ListClassifiers(bool? verbose = null, Dictionary<string, object> customData = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifiers result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v3/classifiers");

                restRequest.WithArgument("version", VersionDate);
                if (verbose != null)
                    restRequest.WithArgument("verbose", verbose);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Classifiers>().Result;
                if(result == null)
                    result = new Classifiers();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a classifier.
        ///
        /// Update a custom classifier by adding new positive or negative classes (examples) or by adding new images to
        /// existing classes. You must supply at least one set of positive or negative examples. For details, see
        /// [Updating custom
        /// classifiers](https://cloud.ibm.com/docs/services/visual-recognition/customizing.html#updating-custom-classifiers).
        ///
        /// Encode all names in UTF-8 if they contain non-ASCII characters (.zip and image file names, and classifier
        /// and class names). The service assumes UTF-8 encoding if it encounters non-ASCII characters.
        ///
        /// **Tip:** Don't make retraining calls on a classifier until the status is ready. When you submit retraining
        /// requests in parallel, the last request overwrites the previous requests. The retrained property shows the
        /// last time the classifier retraining finished.
        /// </summary>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="positiveExamples">A .zip file of images that depict the visual subject of a class in the
        /// classifier. The positive examples create or update classes in the classifier. You can include more than one
        /// positive example file in a call.
        ///
        /// Specify the parameter name by appending `_positive_examples` to the class name. For example,
        /// `goldenretriever_positive_examples` creates the class `goldenretriever`.
        ///
        /// Include at least 10 images in .jpg or .png format. The minimum recommended image resolution is 32X32 pixels.
        /// The maximum number of images is 10,000 images or 100 MB per .zip file.
        ///
        /// Encode special characters in the file name in UTF-8. (optional)</param>
        /// <param name="negativeExamples">A .zip file of images that do not depict the visual subject of any of the
        /// classes of the new classifier. Must contain a minimum of 10 images.
        ///
        /// Encode special characters in the file name in UTF-8. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public Classifier UpdateClassifier(string classifierId, Dictionary<string, System.IO.FileStream> positiveExamples = null, System.IO.FileStream negativeExamples = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifier result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (positiveExamples != null && positiveExamples.Count > 0)
                {
                    foreach(KeyValuePair<string, System.IO.FileStream> entry in positiveExamples)
                    {
                        var partName = string.Format("{0}_positive_examples", entry.Key);
                        var partContent = new ByteArrayContent((entry.Value as Stream).ReadAllBytes());
                        System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                        System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                        partContent.Headers.ContentType = contentType;
                        formData.Add(partContent, partName, entry.Value.Name);
                    }
                }

                if (negativeExamples != null)
                {
                    var negativeExamplesContent = new ByteArrayContent((negativeExamples as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    negativeExamplesContent.Headers.ContentType = contentType;
                    formData.Add(negativeExamplesContent, "negative_examples", negativeExamples.Name);
                }

                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/classifiers/{classifierId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
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
        /// Retrieve a Core ML model of a classifier.
        ///
        /// Download a Core ML model file (.mlmodel) of a custom classifier that returns <tt>"core_ml_enabled":
        /// true</tt> in the classifier details.
        /// </summary>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="byte[]" />byte[]</returns>
        public byte[] GetCoreMlModel(string classifierId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            byte[] result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v3/classifiers/{classifierId}/core_ml_model");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<byte[]>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Delete labeled data.
        ///
        /// Deletes all data associated with a specified customer ID. The method has no effect if no data is associated
        /// with the customer ID.
        ///
        /// You associate a customer ID with data by passing the `X-Watson-Metadata` header with a request that passes
        /// data. For more information about personal data and customer IDs, see [Information
        /// security](https://cloud.ibm.com/docs/services/visual-recognition/information-security.html).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customerId))
                throw new ArgumentNullException(nameof(customerId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v3/user_data");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(customerId))
                    restRequest.WithArgument("customer_id", customerId);
                if (customData != null)
                    restRequest.WithCustomData(customData);
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
    }
}
