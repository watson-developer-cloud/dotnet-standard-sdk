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

using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.IntegrationTests
{
    [TestClass]
    public class VisualRecognitionServiceIntegrationTestsRC
    {
        private VisualRecognitionService _service;
        private static string credentials = string.Empty;
        private static string _apikey;
        private static string _endpoint;
        private string _imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Kittyply_edit1.jpg/1200px-Kittyply_edit1.jpg";
        private string _faceUrl = "https://upload.wikimedia.org/wikipedia/commons/a/ab/Ginni_Rometty_at_the_Fortune_MPW_Summit_in_2011.jpg";
        private string _localGiraffeFilePath = @"VisualRecognitionTestData/giraffe_to_classify.jpg";
        private string _localFaceFilePath = @"VisualRecognitionTestData/obama.jpg";
        private string _localGiraffePositiveExamplesFilePath = @"VisualRecognitionTestData/giraffe_positive_examples.zip";
        private string _giraffeClassname = "giraffe";
        private string _localTurtlePositiveExamplesFilePath = @"VisualRecognitionTestData/turtle_positive_examples.zip";
        private string _turtleClassname = "turtle";
        private string _localNegativeExamplesFilePath = @"VisualRecognitionTestData/negative_examples.zip";
        private string _createdClassifierName = "dotnet-standard-test-integration-classifier";
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        private static int _trainRetries = 3;
        private static int _retrainRetries = 3;
        private static int _listClassifiersRetries = 10;

        #region Setup
        [TestInitialize]
        public void Setup()
        {
            Console.WriteLine(string.Format("\nSetting up test"));

            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";
                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                var vcapServices = JObject.Parse(credentials);

                Credential credential = vcapCredentials.GetCredentialByname("visual-recognition-sdk-rc")[0].Credentials;
                _endpoint = credential.Url;
                _apikey = credential.IamApikey;
            }
            #endregion

            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = _apikey,
                ServiceUrl = _endpoint
            };
            _service = new VisualRecognitionService(tokenOptions, "2018-03-19");
            _service.Client.BaseClient.Timeout = TimeSpan.FromMinutes(120);
        }
        #endregion

        #region Teardown
        [TestCleanup]
        public void Teardown()
        {
            var classifiers = _service.ListClassifiers();
            List<string> dotnet_classifiers = new List<string>();

            foreach (Classifier classifier in classifiers._Classifiers)
            {
                if (classifier.Name == _createdClassifierName)
                    dotnet_classifiers.Add(classifier.ClassifierId);
            }

            foreach (string classifierId in dotnet_classifiers)
            {
                try
                {
                    var getClassifierResult = GetClassifier(classifierId);
                    if (getClassifierResult != null)
                        DeleteClassifier(classifierId);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error: {0}", e.Message);
                }

            }
        }
        #endregion

        #region General
        [TestMethod]
        public void Classify_Success_RC()
        {
            using (FileStream fs = File.OpenRead(_localGiraffeFilePath))
            {
                var result = _service.Classify(fs, imagesFileContentType: "image/jpeg");

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Images);
                Assert.IsTrue(result.Images.Count > 0);
            }
        }

        [TestMethod]
        public void ClassifyURL_Success_RC()
        {
            var result = _service.Classify(url: _imageUrl);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Images);
            Assert.IsTrue(result.Images.Count > 0);
        }
        #endregion

        #region Face
        [TestMethod]
        public void DetectFaces_Success_RC()
        {
            using (FileStream fs = File.OpenRead(_localFaceFilePath))
            {
                var result = _service.DetectFaces(fs, null, "image/jpeg");

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Images);
                Assert.IsTrue(result.Images.Count > 0);
            }
        }

        [TestMethod]
        public void DetectFacesURL_Success_RC()
        {
            using (FileStream fs = File.OpenRead(_localFaceFilePath))
            {
                var result = _service.DetectFaces(url: _faceUrl, imagesFile: fs, imagesFileContentType: "image/jpg");
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Images);
                Assert.IsTrue(result.Images.Count > 0);
            }

        }
        #endregion

        [TestMethod]
        public void ListClassifiers_Success_RC()
        {
            Classifiers listClassifiersResult = null;

            try
            {
                listClassifiersResult = _service.ListClassifiers();
            }
            catch
            {
                Assert.Fail("Failed to list classifier - out of retries!");
            }

            Assert.IsNotNull(listClassifiersResult);
        }

        #region Custom
        [TestMethod]
        public void TestClassifiers_Success_RC()
        {
            Classifier createClassifierResult = null;
            try
            {
                createClassifierResult = CreateClassifier();
            }
            catch
            {
                Assert.Fail("Failed to train classifier - out of retries!");
            }

            string createdClassifierId = createClassifierResult.ClassifierId;

            var getClassifierResult = GetClassifier(createdClassifierId);

            try
            {
                IsClassifierReady(createdClassifierId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get classifier...{0}", e.Message);
            }
            autoEvent.WaitOne();

            Classifier updateClassifierResult = null;
            try
            {
                updateClassifierResult = UpdateClassifier(createdClassifierId);
            }
            catch
            {
                Assert.Fail("Failed to retrain classifier - out of retries!");
            }

            try
            {
                IsClassifierReady(createdClassifierId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get classifier...{0}", e.Message);
            }
            autoEvent.WaitOne();

            Task<Stream> getCoreMlModelResult = null;
            try
            {
                getCoreMlModelResult = GetCoreMlModel(createdClassifierId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get classifier...{0}", e.Message);
            }

            try
            {
                IsClassifierReady(createdClassifierId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get classifier...{0}", e.Message);
            }
            autoEvent.WaitOne();

            var deleteClassifierResult = DeleteClassifier(createdClassifierId);

            Assert.IsNotNull(deleteClassifierResult);
            Assert.IsNotNull(getCoreMlModelResult);
            Assert.IsNotNull(updateClassifierResult);
            Assert.IsTrue(updateClassifierResult.ClassifierId == createdClassifierId);
            Assert.IsNotNull(getClassifierResult);
            Assert.IsTrue(getClassifierResult.ClassifierId == createdClassifierId);
            Assert.IsNotNull(createClassifierResult);
            Assert.IsTrue(createClassifierResult.Name == _createdClassifierName);
        }
        #endregion

        #region Create and Update Classifier with retries.
        private Classifier CreateClassifier()
        {
            Classifier classifier = null;

            try
            {
                using (FileStream positiveExamplesStream = File.OpenRead(_localGiraffePositiveExamplesFilePath), negativeExamplesStream = File.OpenRead(_localNegativeExamplesFilePath))
                {
                    Dictionary<string, Stream> positiveExamples = new Dictionary<string, Stream>();
                    positiveExamples.Add(_giraffeClassname, positiveExamplesStream);
                    CreateClassifier createClassifier = new CreateClassifier(_createdClassifierName, positiveExamples, negativeExamplesStream);
                    classifier = _service.CreateClassifier(createClassifier);
                }
            }
            catch (Exception e)
            {
                if (_trainRetries > 0)
                {
                    _trainRetries--;
                    CreateClassifier();
                }
                else
                {
                    throw e;
                }
            }

            return classifier;
        }

        private Classifier UpdateClassifier(string createdClassifierId)
        {
            Classifier updateClassifierResult = null;

            try
            {
                using (FileStream positiveExamplesStream = File.OpenRead(_localTurtlePositiveExamplesFilePath))
                {
                    Dictionary<string, Stream> positiveExamples = new Dictionary<string, Stream>();
                    positiveExamples.Add(_turtleClassname, positiveExamplesStream);
                    UpdateClassifier updateClassifier = new UpdateClassifier(createdClassifierId, positiveExamples);
                    updateClassifierResult = _service.UpdateClassifier(updateClassifier);
                }
            }
            catch (Exception e)
            {
                if (_retrainRetries > 0)
                {
                    _retrainRetries--;
                    UpdateClassifier(createdClassifierId);
                }
                else
                {
                    throw e;
                }
            }

            return updateClassifierResult;
        }

        private Classifiers ListClassifiers(bool? verbose = null)
        {
            Console.WriteLine("\nAttempting to ListClassifiers()");

            Classifiers result = null;
            try
            {
                result = _service.ListClassifiers(verbose: verbose);
            }
            catch (Exception e)
            {
                if (_listClassifiersRetries > 0)
                {
                    _listClassifiersRetries--;
                    ListClassifiers(verbose);
                }
                else
                {
                    throw e;
                }
            }

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

        #region Get Core ML Model
        private Task<Stream> GetCoreMlModel(string createdClassifierId)
        {
            Task<Stream> getCoreMlModelResult = null;

            try
            {
                getCoreMlModelResult = _service.GetCoreMlModel(createdClassifierId);
            }
            catch (Exception e)
            {
                throw e;
            }

            return getCoreMlModelResult;
        }
        #endregion

        #region Utility
        #region IsClassifierReady
        private void IsClassifierReady(string classifierId)
        {
            var getClassifierResponse = _service.GetClassifier(classifierId);

            Console.WriteLine(string.Format("Classifier status is {0}", getClassifierResponse.Status.ToString()));

            if (getClassifierResponse.Status == Classifier.StatusEnum.READY || getClassifierResponse.Status == Classifier.StatusEnum.FAILED)
            {
                autoEvent.Set();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(10000);
                    try
                    {
                        IsClassifierReady(classifierId);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                });
            }
        }
        #endregion
        #endregion

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
