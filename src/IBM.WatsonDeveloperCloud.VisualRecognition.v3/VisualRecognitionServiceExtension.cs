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

using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3
{
    public partial class VisualRecognitionService : WatsonService, IVisualRecognitionService
    {
        /// <summary>
        /// Create a classifier. Train a new multi-faceted classifier on the uploaded image data. Create your custom classifier with positive or negative examples. Include at least two sets of examples, either two positive example files or one positive and one negative file. You can upload a maximum of 256 MB per call.  Encode all names in UTF-8 if they contain non-ASCII characters (.zip and image file names, and classifier and class names). The service assumes UTF-8 encoding if it encounters non-ASCII characters.
        /// </summary>
        /// <param name="createClassifier">Object used to define options for creating a classifier.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public Classifier CreateClassifier(CreateClassifier createClassifier)
        {
            if (createClassifier == null)
                throw new ArgumentNullException(nameof(createClassifier));

            if(!createClassifier.IsValid())
                throw new ArgumentException("At least one positive example and one negative example is required to train a classifier.");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifier result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (createClassifier.Name != null)
                {
                    var nameContent = new StringContent(createClassifier.Name, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    nameContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    formData.Add(nameContent, "name");
                }

                if (createClassifier.PositiveExamples != null && createClassifier.PositiveExamples.Count > 0)
                {
                    foreach(KeyValuePair<string, Stream> kvp in createClassifier.PositiveExamples)
                    {
                        var classnamePositiveExamplesContent = new ByteArrayContent((kvp.Value as Stream).ReadAllBytes());
                        MediaTypeHeaderValue contentType;
                        MediaTypeHeaderValue.TryParse("application/zip", out contentType);
                        classnamePositiveExamplesContent.Headers.ContentType = contentType;
                        formData.Add(classnamePositiveExamplesContent, string.Format("{0}_positive_examples", kvp.Key), string.Format("{0}_positive_examples.zip", kvp.Key));
                    }
                }

                if (createClassifier.NegativeExamples != null)
                {
                    var negativeExamplesContent = new ByteArrayContent((createClassifier.NegativeExamples as Stream).ReadAllBytes());
                    MediaTypeHeaderValue contentType;
                    MediaTypeHeaderValue.TryParse("application/zip", out contentType);
                    negativeExamplesContent.Headers.ContentType = contentType;
                    formData.Add(negativeExamplesContent, "negative_examples", "negative_examples.zip");
                }

                result = this.Client.PostAsync($"{this.Endpoint}/v3/classifiers")
                                .WithArgument("api_key", ApiKey)
                                .WithArgument("version", VersionDate)
                                .WithBodyContent(formData)
                                .WithFormatter(new MediaTypeHeaderValue("application/octet-stream"))
                                .As<Classifier>()
                                .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a classifier. Update a custom classifier by adding new positive or negative classes (examples) or by adding new images to existing classes. You must supply at least one set of positive or negative examples. For details, see [Updating custom classifiers](https://console.bluemix.net/docs/services/visual-recognition/customizing.html#updating-custom-classifiers).  Encode all names in UTF-8 if they contain non-ASCII characters (.zip and image file names, and classifier and class names). The service assumes UTF-8 encoding if it encounters non-ASCII characters.  **Important:** You can't update a custom classifier with an API key for a Lite plan. To update a custom classifer on a Lite plan, create another service instance on a Standard plan and re-create your custom classifier.  **Tip:** Don't make retraining calls on a classifier until the status is ready. When you submit retraining requests in parallel, the last request overwrites the previous requests. The retrained property shows the last time the classifier retraining finished.
        /// </summary>
        /// <param name="updateClassifier">Object used to define options for updating a classifier.</param>
        /// <returns><see cref="Classifier" />Classifier</returns>
        public Classifier UpdateClassifier(UpdateClassifier updateClassifier)
        {
            if (updateClassifier == null)
                throw new ArgumentNullException(nameof(updateClassifier));

            if (string.IsNullOrEmpty(updateClassifier.ClassifierId))
                throw new ArgumentNullException(nameof(updateClassifier.ClassifierId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Classifier result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (updateClassifier.PositiveExamples != null && updateClassifier.PositiveExamples.Count > 0)
                {
                    foreach (KeyValuePair<string, Stream> kvp in updateClassifier.PositiveExamples)
                    {
                        var classnamePositiveExamplesContent = new ByteArrayContent((kvp.Value as Stream).ReadAllBytes());
                        MediaTypeHeaderValue contentType;
                        MediaTypeHeaderValue.TryParse("application/zip", out contentType);
                        classnamePositiveExamplesContent.Headers.ContentType = contentType;
                        formData.Add(classnamePositiveExamplesContent, string.Format("{0}_positive_examples", kvp.Key), string.Format("{0}_positive_examples.zip", kvp.Key));
                    }
                }

                if (updateClassifier.NegativeExamples != null)
                {
                    var negativeExamplesContent = new ByteArrayContent((updateClassifier.NegativeExamples as Stream).ReadAllBytes());
                    MediaTypeHeaderValue contentType;
                    MediaTypeHeaderValue.TryParse("application/zip", out contentType);
                    negativeExamplesContent.Headers.ContentType = contentType;
                    formData.Add(negativeExamplesContent, "negative_examples", "negative_examples.zip");
                }

                result = this.Client.PostAsync($"{this.Endpoint}/v3/classifiers/{updateClassifier.ClassifierId}")
                                .WithArgument("api_key", ApiKey)
                                .WithArgument("version", VersionDate)
                                .WithBodyContent(formData)
                                .WithFormatter(new MediaTypeHeaderValue("application/octet-stream"))
                                .As<Classifier>()
                                .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Gets the stream of a Core ML model file (.mlmodel) of a custom classifier that returns "core_ml_enabled": true in the classifier details.
        /// </summary>
        /// <param name="classifierId"></param>
        /// <returns>The Core ML model of the requested classifier.</returns>
        public Task<Stream> GetCoreMlModel(string classifierId)
        {
            if (string.IsNullOrEmpty(classifierId))
                throw new ArgumentNullException(nameof(classifierId));

            System.Threading.Tasks.Task<Stream> result = null;
            
            try
            {
                var request = this.Client.GetAsync($"{this.Endpoint}/v3/classifiers/{classifierId}/core_ml_model");
                request.WithArgument("api_key", ApiKey);
                request.WithArgument("version", VersionDate);
                request.WithArgument("classifier_id", classifierId);
                request.WithFormatter(MediaTypeHeaderValue.Parse("application/octet-stream"));
                result = request.AsStream();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Downloads a Core ML model file (.mlmodel) of a custom classifier that returns "core_ml_enabled": true in the classifier details.
        /// The name of the retreived Core ML model will be [classifierId].mlmodel.
        /// </summary>
        /// <param name="classifierId">The requested Core ML model's classifier ID</param>
        /// <param name="filePath">The file path (without the filename) to download the Core ML model.</param>
        public void DownloadCoreMlModel(string classifierId, string filePath)
        {
            var data = GetCoreMlModel(classifierId);

            using (Stream file = File.Create(string.Format("{0}\\{1}.mlmodel", filePath, classifierId)))
            {
                Utility.CopyStream(data.Result, file);
            }
        }
    }
    
    /// <summary>
    /// Object to create a classifier
    /// </summary>
    public class CreateClassifier
    {
        /// <summary>
        /// The name of the new classifier. Encode special characters in UTF-8.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name;

        /// <summary>
        /// A Dictionary of positive example classname and .zip file of images that depict the visual subject of a class in the new classifier. You can include more than one positive example file in a call. Append `_positive_examples` to the form name. The prefix is used as the class name. For example, `goldenretriever_positive_examples` creates the class **goldenretriever**.  Include at least 10 images in .jpg or .png format. The minimum recommended image resolution is 32X32 pixels. The maximum number of images is 10,000 images or 100 MB per .zip file.  Encode special characters in the file name in UTF-8.  The API explorer limits you to training only one class. To train more classes, use the API functionality.
        /// </summary>
        public Dictionary<string, System.IO.Stream> PositiveExamples
        {
            get { return _positiveExamples; }
            set { _positiveExamples = value; }
        }
        private Dictionary<string, System.IO.Stream> _positiveExamples;

        /// <summary>
        /// A compressed (.zip) file of images that do not depict the visual subject of any of the classes of the new classifier. Must contain a minimum of 10 images.  Encode special characters in the file name in UTF-8. (optional).
        /// </summary>
        public System.IO.Stream NegativeExamples
        {
            get { return _negativeExamples; }
            set { _negativeExamples = value; }
        }
        private System.IO.Stream _negativeExamples;

        public CreateClassifier(string name, Dictionary<string, System.IO.Stream> positiveExamples, System.IO.Stream negativeExamples = null)
        {
            Name = name;
            PositiveExamples = positiveExamples;
            NegativeExamples = negativeExamples;

            if (!IsValid())
                throw new ArgumentException("At least one positive example and one negative example is required to train a classifier.");
        }

        /// <summary>
        /// Check to see if more than two positive examples or one positive example and one negative example are contained in the object.
        /// </summary>
        /// <returns>True if there are more than two positive examples or there is one positive example and one negative example.</returns>
        public bool IsValid()
        {
            return PositiveExamples.Count >= 2 || (PositiveExamples.Count == 1 && NegativeExamples != null);
        }
    }

    /// <summary>
    /// Object to update a classifier
    /// </summary>
    public class UpdateClassifier
    {
        /// <summary>
        /// The ID of the classifier.
        /// </summary>
        public string ClassifierId
        {
            get { return _classifierId; }
            set { _classifierId = value; }
        }
        private string _classifierId;

        /// <summary>
        /// A Dictionary of positive example classname and .zip file of images that depict the visual subject of a class in the new classifier. You can include more than one positive example file in a call. Append `_positive_examples` to the form name. The prefix is used as the class name. For example, `goldenretriever_positive_examples` creates the class **goldenretriever**.  Include at least 10 images in .jpg or .png format. The minimum recommended image resolution is 32X32 pixels. The maximum number of images is 10,000 images or 100 MB per .zip file.  Encode special characters in the file name in UTF-8.  The API explorer limits you to training only one class. To train more classes, use the API functionality.
        /// </summary>
        public Dictionary<string, System.IO.Stream> PositiveExamples
        {
            get { return _positiveExamples; }
            set { _positiveExamples = value; }
        }
        private Dictionary<string, System.IO.Stream> _positiveExamples;

        /// <summary>
        /// A compressed (.zip) file of images that do not depict the visual subject of any of the classes of the new classifier. Must contain a minimum of 10 images.  Encode special characters in the file name in UTF-8. (optional).
        /// </summary>
        public System.IO.Stream NegativeExamples
        {
            get { return _negativeExamples; }
            set { _negativeExamples = value; }
        }
        private System.IO.Stream _negativeExamples;

        public UpdateClassifier(string classifierId, Dictionary<string, System.IO.Stream> positiveExamples = null, System.IO.Stream negativeExamples = null)
        {
            ClassifierId = classifierId;
            PositiveExamples = positiveExamples;
            NegativeExamples = negativeExamples;
        }
    }
}