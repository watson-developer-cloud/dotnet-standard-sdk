/**
* (C) Copyright IBM Corp. 2017, 2020.
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

using IBM.Watson.VisualRecognition.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using IBM.Cloud.SDK.Core.Util;
using Newtonsoft.Json;
using System.Globalization;
using IBM.Cloud.SDK.Core.Http;

namespace IBM.Watson.VisualRecognition.v3.IntegrationTests
{
    [TestClass]
    public class VisualRecognitionServiceIntegrationTests
    {
        private VisualRecognitionService service;
        private static string credentials = string.Empty;
        private static string apikey;
        private static string endpoint;
        private string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Kittyply_edit1.jpg/1200px-Kittyply_edit1.jpg";
        private string faceUrl = "https://upload.wikimedia.org/wikipedia/commons/a/ab/Ginni_Rometty_at_the_Fortune_MPW_Summit_in_2011.jpg";
        private string localGiraffeFilePath = @"VisualRecognitionTestData/giraffe_to_classify.jpg";
        private string localFaceFilePath = @"VisualRecognitionTestData/obama.jpg";
        private string localGiraffePositiveExamplesFilePath = @"VisualRecognitionTestData/giraffe_positive_examples.zip";
        private string giraffeClassname = "giraffe";
        private string localTurtlePositiveExamplesFilePath = @"VisualRecognitionTestData/turtle_positive_examples.zip";
        private string turtleClassname = "turtle";
        private string localNegativeExamplesFilePath = @"VisualRecognitionTestData/negative_examples.zip";
        private string createdClassifierName = "dotnet-standard-test-integration-classifier";
        private string versionDate = "2018-03-19";
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        private static int _trainRetries = 3;
        private static int _retrainRetries = 3;
        private static int _listClassifiersRetries = 10;

        #region Setup
        [TestInitialize]
        public void Setup()
        {
            service = new VisualRecognitionService(versionDate);
            service.Client.BaseClient.Timeout = TimeSpan.FromMinutes(120);
        }
        #endregion

        #region Teardown
        [TestCleanup]
        public void Teardown()
        {
            service.WithHeader("X-Watson-Test", "1");
            var classifiers = service.ListClassifiers();
            List<string> dotnet_classifiers = new List<string>();

            foreach (Classifier classifier in classifiers.Result._Classifiers)
            {
                if (classifier.Name == createdClassifierName)
                    dotnet_classifiers.Add(classifier.ClassifierId);
            }

            foreach (string classifierId in dotnet_classifiers)
            {
                try
                {
                    service.WithHeader("X-Watson-Test", "1");
                    var getClassifierResult = service.GetClassifier(
                        classifierId: classifierId
                        );
                    if (getClassifierResult != null)
                        service.WithHeader("X-Watson-Test", "1");
                    service.DeleteClassifier(
                        classifierId: classifierId
                        );
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
        public void Classify_Success()
        {
            using (FileStream fs = File.OpenRead(localGiraffeFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    var result = service.Classify(
                        imagesFile: ms,
                        imagesFilename: Path.GetFileName(localGiraffeFilePath),
                        imagesFileContentType: "image/jpeg",
                        threshold: 0.5f,
                        acceptLanguage: "en-US"
                        );

                    Assert.IsNotNull(result.Result);
                    Assert.IsNotNull(result.Result.Images);
                    Assert.IsTrue(result.Result.Images.Count > 0);
                }
            }
        }

        [TestMethod]
        public void Classify_With_Threshold_Success()
        {
            using (FileStream fs = File.OpenRead(localGiraffeFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    var previousCulture = CultureInfo.CurrentCulture;

                    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
                    float value;
                    bool b = float.TryParse("0,1", NumberStyles.Any, new CultureInfo("pt-BR"), out value);

                    CultureInfo.CurrentCulture = previousCulture;

                    service.WithHeader("X-Watson-Test", "1");
                    var result = service.Classify(
                        imagesFile: ms,
                        imagesFilename: Path.GetFileName(localGiraffeFilePath),
                        imagesFileContentType: "image/jpeg",
                        threshold: value
                        );

                    Assert.IsNotNull(result.Result);
                    Assert.IsNotNull(result.Result.Images);
                    Assert.IsTrue(result.Result.Images.Count > 0);

                }
            }
        }

        [TestMethod]
        public void ClassifyURL_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.Classify(
                url: imageUrl,
                threshold: 0.5f,
                acceptLanguage: "en"
                );

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Images);
            Assert.IsTrue(result.Result.Images.Count > 0);
        }
        #endregion

        [TestMethod]
        public void ListClassifiers_Success()
        {
            DetailedResponse<Classifiers> listClassifiersResult = null;

            try
            {
                service.WithHeader("X-Watson-Test", "1");
                listClassifiersResult = service.ListClassifiers();
            }
            catch
            {
                Assert.Fail("Failed to list classifier - out of retries!");
            }

            Assert.IsNotNull(listClassifiersResult.Result);
            Assert.IsNotNull(listClassifiersResult.Result._Classifiers);
            Assert.IsTrue(listClassifiersResult.Result._Classifiers.Count > 0);
        }

        #region Custom
        //[TestMethod]
        public void TestClassifiers_Success()
        {
            DetailedResponse<Classifier> createClassifierResult = null;
            string createdClassifierId;
            using (FileStream positiveExamplesFileStream = File.OpenRead(localGiraffePositiveExamplesFilePath), negativeExamplesFileStream = File.OpenRead(localNegativeExamplesFilePath))
            {
                using (MemoryStream positiveExamplesMemoryStream = new MemoryStream(), negativeExamplesMemoryStream = new MemoryStream())
                {
                    positiveExamplesFileStream.CopyTo(positiveExamplesMemoryStream);
                    negativeExamplesFileStream.CopyTo(negativeExamplesMemoryStream);
                    Dictionary<string, MemoryStream> positiveExamples = new Dictionary<string, MemoryStream>();
                    positiveExamples.Add(giraffeClassname, positiveExamplesMemoryStream);
                    service.WithHeader("X-Watson-Test", "1");
                    createClassifierResult = service.CreateClassifier(
                        name: createdClassifierName,
                        positiveExamples: positiveExamples,
                        negativeExamples: negativeExamplesMemoryStream,
                        negativeExamplesFilename: Path.GetFileName(localNegativeExamplesFilePath)
                        );
                    createdClassifierId = createClassifierResult.Result.ClassifierId;
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var getClassifierResult = service.GetClassifier(
                    classifierId: createdClassifierId
                    );

            try
            {
                IsClassifierReady(
                    classifierId: createdClassifierId
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get classifier...{0}", e.Message);
            }
            autoEvent.WaitOne();

            DetailedResponse<Classifier> updateClassifierResult = null;
            using (FileStream positiveExamplesStream = File.OpenRead(localTurtlePositiveExamplesFilePath))
            {
                using (MemoryStream positiveExamplesMemoryStream = new MemoryStream())
                {
                    Dictionary<string, MemoryStream> positiveExamples = new Dictionary<string, MemoryStream>();
                    positiveExamples.Add(turtleClassname, positiveExamplesMemoryStream);
                    service.WithHeader("X-Watson-Test", "1");
                    updateClassifierResult = service.UpdateClassifier(
                        classifierId: createdClassifierId,
                        positiveExamples: positiveExamples
                        );
                }
            }

            try
            {
                IsClassifierReady(
                    classifierId: createdClassifierId
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get classifier...{0}", e.Message);
            }
            autoEvent.WaitOne();

            DetailedResponse<MemoryStream> getCoreMlModelResult = null;
            try
            {
                service.WithHeader("X-Watson-Test", "1");
                getCoreMlModelResult = service.GetCoreMlModel(
                    classifierId: createdClassifierId
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get classifier...{0}", e.Message);
            }

            try
            {
                IsClassifierReady(
                    classifierId: createdClassifierId
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get classifier...{0}", e.Message);
            }
            autoEvent.WaitOne();

            service.WithHeader("X-Watson-Test", "1");
            var deleteClassifierResult = service.DeleteClassifier(
                classifierId: createdClassifierId
                );

            Assert.IsNotNull(deleteClassifierResult.Result);
            Assert.IsNotNull(getCoreMlModelResult.Result);
            Assert.IsNotNull(updateClassifierResult.Result);
            Assert.IsTrue(updateClassifierResult.Result.ClassifierId == createdClassifierId);
            Assert.IsNotNull(getClassifierResult.Result);
            Assert.IsTrue(getClassifierResult.Result.ClassifierId == createdClassifierId);
            Assert.IsNotNull(createClassifierResult.Result);
            Assert.IsTrue(createClassifierResult.Result.Name == createdClassifierName);
        }
        #endregion

        #region Utility
        #region IsClassifierReady
        private void IsClassifierReady(string classifierId)
        {
            service.WithHeader("X-Watson-Test", "1");
            var getClassifierResponse = service.GetClassifier(classifierId);

            Console.WriteLine(string.Format("Classifier status is {0}", getClassifierResponse.Result.Status.ToString()));

            if (getClassifierResponse.Result.Status == Classifier.StatusEnumValue.READY || getClassifierResponse.Result.Status == Classifier.StatusEnumValue.FAILED)
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
    }
}
