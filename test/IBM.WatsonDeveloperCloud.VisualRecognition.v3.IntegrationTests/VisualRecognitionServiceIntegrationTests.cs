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

//  Comment to enable testing of collections, collection images and similarity search.
//#define ENABLE_COLLECTIONS

using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.IntegrationTests
{
    [TestClass]
    public class VisualRecognitionServiceIntegrationTests
    {
        private string apikey;
        private string _imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Kittyply_edit1.jpg/1200px-Kittyply_edit1.jpg";
        private string _faceUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/President_Barack_Obama.jpg/220px-President_Barack_Obama.jpg";
        private string _localGiraffeFilePath = @"VisualRecognitionTestData\giraffe_to_classify.jpg";
        private string _localFaceFilePath = @"VisualRecognitionTestData\obama.jpg";
        private string _localGiraffePositiveExamplesFilePath = @"VisualRecognitionTestData\giraffe_positive_examples.zip";
        private string _giraffeClassname = "giraffe";
        private string _localTurtlePositiveExamplesFilePath = @"VisualRecognitionTestData\turtle_positive_examples.zip";
        private string _turtleClassname = "turtle";
        private string _localNegativeExamplesFilePath = @"VisualRecognitionTestData\negative_examples.zip";
        private string _createdClassifierName = "dotnet-standard-test-classifier";
        private static string _createdClassifierId = "";
#if ENABLE_COLLECTIONS
        private string _localImageMetadataPath = @"VisualRecognitionTestData\imageMetadata.json";
        private string _localTurtleFilePath = @"VisualRecognitionTestData\turtle_to_classify.jpg";
        private string _collectionNameToCreate = "dotnet-standard-test-collection";
        private static string _createdCollectionId = "";
        private static string _addedImageId = "";
#endif
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        [TestInitialize]
        public void Setup()
        {
            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
            File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            apikey = vcapServices["visual_recognition"][0]["credentials"]["apikey"].ToString();
        }

        [TestMethod]
        public void t00_ClassifyGet_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.Classify(_imageUrl);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Images);
            Assert.IsTrue(result.Images.Count > 0);
        }

        [TestMethod]
        public void t01_ClassifyPost_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            using (FileStream fs = File.OpenRead(_localGiraffeFilePath))
            {
                var result = service.Classify((fs as Stream).ReadAllBytes(), Path.GetFileName(_localGiraffeFilePath), "image/jpeg");

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Images);
                Assert.IsTrue(result.Images.Count > 0);
            }
        }

        [TestMethod]
        public void t02_DetectFacesGet_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.DetectFaces(_faceUrl);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Images);
            Assert.IsTrue(result.Images.Count > 0);
        }

        [TestMethod]
        public void t03_DetectFacesPost_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            using (FileStream fs = File.OpenRead(_localFaceFilePath))
            {
                var result = service.DetectFaces((fs as Stream).ReadAllBytes(), Path.GetFileName(_localFaceFilePath), "image/jpeg");

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Images);
                Assert.IsTrue(result.Images.Count > 0);
            }
        }

        [TestMethod]
        public void t04_GetClassifiersBrief_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);
            var results = service.GetClassifiersBrief();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void t05_GetClassifiersVerbose_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);
            var results = service.GetClassifiersVerbose();

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void t06_CreateClassifier_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            using (FileStream positiveExamplesStream = File.OpenRead(_localGiraffePositiveExamplesFilePath), negativeExamplesStream = File.OpenRead(_localNegativeExamplesFilePath))
            {
                Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
                positiveExamples.Add(_giraffeClassname, positiveExamplesStream.ReadAllBytes());

                var result = service.CreateClassifier(_createdClassifierName, positiveExamples, negativeExamplesStream.ReadAllBytes());

                _createdClassifierId = result.ClassifierId;
                Console.WriteLine(string.Format("Created classifier {0}", _createdClassifierId));

                Assert.IsNotNull(result);
                Assert.IsTrue(!string.IsNullOrEmpty(_createdClassifierId));
            }
        }

        [TestMethod]
        public void t07_WaitForClassifier()
        {
            IsClassifierReady(_createdClassifierId);
            autoEvent.WaitOne();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void t08_UpdateClassifier_Success()
        {
            if (string.IsNullOrEmpty(_createdClassifierId))
                Assert.Fail("Created classsifier ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            using (FileStream positiveExamplesStream = File.OpenRead(_localTurtlePositiveExamplesFilePath))
            {
                Dictionary<string, byte[]> positiveExamples = new Dictionary<string, byte[]>();
                positiveExamples.Add(_turtleClassname, positiveExamplesStream.ReadAllBytes());

                var result = service.UpdateClassifier(_createdClassifierId, positiveExamples);

                Assert.IsNotNull(result);
            }
        }

#if ENABLE_COLLECTIONS
        [TestMethod]
        public void t09_GetCollections_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.GetCollections();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t10_CreateCollection_Success()
        {
            DeleteDotnetCollections();

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.CreateCollection(_collectionNameToCreate);

            _createdCollectionId = result.CollectionId;
            Console.WriteLine(string.Format("Created collection {0}", _createdCollectionId));

            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(_createdCollectionId));
        }

        [TestMethod]
        public void t11_GetCollection_Success()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("Created collection ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.GetCollection(_createdCollectionId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.CollectionId == _createdCollectionId);
        }

        [TestMethod]
        public void t12_GetCollectionImages_Success()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("Created collection ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.GetCollectionImages(_createdCollectionId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t13_AddCollectionImages_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            using (FileStream imageStream = File.OpenRead(_localGiraffeFilePath), metadataStream = File.OpenRead(_localImageMetadataPath))
            {
                var result = service.AddImage(_createdCollectionId, imageStream.ReadAllBytes(), Path.GetFileName(_localGiraffeFilePath), metadataStream.ReadAllBytes());

                _addedImageId = result.Images[0].ImageId;
                Console.WriteLine(string.Format("Added image {0}", _addedImageId));

                Assert.IsNotNull(result);
                Assert.IsTrue(result.ImagesProcessed > 0);
                Assert.IsTrue(!string.IsNullOrEmpty(_addedImageId));
            }
        }

        [TestMethod]
        public void t14_GetCollectionImage_Success()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("Created collection ID is null or empty.");

            if (string.IsNullOrEmpty(_addedImageId))
                Assert.Fail("Added image ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.GetImage(_createdCollectionId, _addedImageId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ImageId == _addedImageId);
        }

        [TestMethod]
        public void t15_GetCollectionImageMetadata_Success()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("Created collection ID is null or empty.");

            if (string.IsNullOrEmpty(_addedImageId))
                Assert.Fail("Added image ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.GetMetadata(_createdCollectionId, _addedImageId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t16_AddCollectionImageMetadata_Success()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("Created collection ID is null or empty.");

            if (string.IsNullOrEmpty(_addedImageId))
                Assert.Fail("Added image ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            using (FileStream metadataStream = File.OpenRead(_localImageMetadataPath))
            {
                var result = service.AddImageMetadata(_createdCollectionId, _addedImageId, metadataStream.ReadAllBytes());

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count > 0);
            }
        }

        [TestMethod]
        public void t17_DeleteCollectionImageMetadata_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.DeleteImageMetadata(_createdCollectionId, _addedImageId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t18_FindSimilar_Success()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            using (FileStream imageStream = File.OpenRead(_localTurtleFilePath))
            {
                var result = service.FindSimilar(_createdCollectionId, imageStream.ReadAllBytes(), Path.GetFileName(_localGiraffeFilePath));

                Assert.IsNotNull(result);
                Assert.IsTrue(result.ImagesProcessed > 0);
            }
        }

        [TestMethod]
        public void t19_DeleteCollectionImage_Success()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("Created collection ID is null or empty.");

            if (string.IsNullOrEmpty(_addedImageId))
                Assert.Fail("Added image ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.DeleteImage(_createdCollectionId, _addedImageId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void t20_DeleteCollection_Success()
        {
            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("Created collection ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.DeleteCollection(_createdCollectionId);

            Assert.IsNotNull(result);
        }
#endif

        [TestMethod]
        public void t21_DeleteClassifier_Success()
        {
            if (string.IsNullOrEmpty(_createdClassifierId))
                Assert.Fail("Created classsifier ID is null or empty.");

            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.DeleteClassifier(_createdClassifierId);

            Assert.IsNotNull(result);
        }

        private bool IsClassifierReady(string classifierId)
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            var result = service.GetClassifier(classifierId);

            string status = result.Status.ToLower();
            Console.WriteLine(string.Format("Classifier status is {0}", status));

            if (status == "ready")
                autoEvent.Set();
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    IsClassifierReady(classifierId);
                });
            }

            return result.Status.ToLower() == "ready";
        }

#if ENABLE_COLLECTIONS
        private void DeleteDotnetCollections()
        {
            VisualRecognitionService service = new VisualRecognitionService();
            service.SetCredential(apikey);

            List<string> collectionIdsToDelete = new List<string>();

            var collections = service.GetCollections();

            foreach (CreateCollection collection in collections.Collections)
            {
                string name = collection.Name;
                string id = collection.CollectionId;

                if (name == _collectionNameToCreate)
                    collectionIdsToDelete.Add(id);
            }

            if (collectionIdsToDelete.Count > 0)
            {
                foreach (string collectionIdToDelete in collectionIdsToDelete)
                    service.DeleteCollection(collectionIdToDelete);
            }
        }
#endif

        private bool ContainsClass(GetClassifiersPerClassifierVerbose result, string classname)
        {
            bool containsClass = false;

            foreach (ModelClass _class in result.Classes)
            {
                if (_class._Class == classname)
                    containsClass = true;
            }

            return containsClass;
        }
    }
}
