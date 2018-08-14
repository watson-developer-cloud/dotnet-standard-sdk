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

using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.VisRec.v3.Example
{
    class VisualRecognitionServiceExample
    {
        private string _apikey;
        private string _versionDate;
        private VisualRecognitionService _service;
        private string _localGiraffeFilePath = @"VisualRecognitionTestData/giraffe_to_classify.jpg";
        private string _imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Kittyply_edit1.jpg/1200px-Kittyply_edit1.jpg";

        #region Constructor
        public VisualRecognitionServiceExample(string apikey, string versionDate)
        {

            _service = new VisualRecognitionService(apikey, versionDate);
            _service.Client.BaseClient.Timeout = TimeSpan.FromMinutes(120);

            ClassifyImage();
            ClassifyUrl();
            //ListAllClassifiers();

            Console.WriteLine("\n~ VisualRecognition examples complete.");
        }
        #endregion

        private void ClassifyImage()
        {
            using (FileStream fs = File.OpenRead(_localGiraffeFilePath))
            {
                var result = Classify(fs, imagesFileContentType: "image/jpeg");
            }
        }

        private void ClassifyUrl()
        {
            var result = Classify(url: _imageUrl);
        }

        private void ListAllClassifiers()
        {
            var listClassifiersResult = ListClassifiers();
        }

        #region Generated
        #region Classify
        private ClassifiedImages Classify(System.IO.FileStream imagesFile = null, string acceptLanguage = null, string url = null, float? threshold = null, List<string> owners = null, List<string> classifierIds = null, string imagesFileContentType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Classify()");
            var result = _service.Classify(imagesFile: imagesFile, acceptLanguage: acceptLanguage, url: url, threshold: threshold, owners: owners, classifierIds: classifierIds, imagesFileContentType: imagesFileContentType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("Classify() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Classify()");
            }

            return result;
        }
        #endregion

        #region DetectFaces
        private DetectedFaces DetectFaces(System.IO.FileStream imagesFile = null, string url = null, string imagesFileContentType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DetectFaces()");
            var result = _service.DetectFaces(imagesFile: imagesFile, url: url, imagesFileContentType: imagesFileContentType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DetectFaces() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DetectFaces()");
            }

            return result;
        }
        #endregion

        #region CreateClassifier
        private Classifier CreateClassifier(CreateClassifier createClassifier, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateClassifier()");
            var result = _service.CreateClassifier(createClassifier: createClassifier, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateClassifier() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateClassifier()");
            }

            return result;
        }
        #endregion

        #region DeleteClassifier
        private BaseModel DeleteClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteClassifier()");
            var result = _service.DeleteClassifier(classifierId: classifierId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteClassifier() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteClassifier()");
            }

            return result;
        }
        #endregion

        #region GetClassifier
        private Classifier GetClassifier(string classifierId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetClassifier()");
            var result = _service.GetClassifier(classifierId: classifierId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetClassifier() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetClassifier()");
            }

            return result;
        }
        #endregion

        #region ListClassifiers
        private Classifiers ListClassifiers(bool? verbose = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListClassifiers()");
            var result = _service.ListClassifiers(verbose: verbose, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListClassifiers() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListClassifiers()");
            }

            return result;
        }
        #endregion

        #region UpdateClassifier
        private Classifier UpdateClassifier(UpdateClassifier updateClassifier, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateClassifier()");
            var result = _service.UpdateClassifier(updateClassifier: updateClassifier, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateClassifier() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateClassifier()");
            }

            return result;
        }
        #endregion

        #region GetCoreMlModel
        private Task<Stream> GetCoreMlModel(string classifierId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetCoreMlModel()");
            var result = _service.GetCoreMlModel(classifierId: classifierId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetCoreMlModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetCoreMlModel()");
            }

            return result;
        }
        #endregion

        #region DeleteUserData
        private BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteUserData()");
            var result = _service.DeleteUserData(customerId: customerId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteUserData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteUserData()");
            }

            return result;
        }
        #endregion

        #endregion
    }
}
