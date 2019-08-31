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

using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.VisualRecognition.v4.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace IBM.Watson.VisualRecognition.v4.IntegrationTests
{
    [TestClass]
    public class VisualRecognitionServiceIntegrationTests
    {
        private VisualRecognitionService service;
        private static string credentials = string.Empty;
        private string localGiraffeFilePath = @"VisualRecognitionTestData/giraffe_to_classify.jpg";
        private string localGiraffePositiveExamplesFilePath = @"VisualRecognitionTestData/giraffe_positive_examples.zip";
        private string giraffeClassname = "giraffe";
        private string versionDate = "2019-02-11";
        private string dotnetCollectionId = "d31d6534-3458-40c4-b6de-2185a5f3cbe4";

        #region Setup
        [TestInitialize]
        public void Setup()
        {
            service = new VisualRecognitionService(versionDate);
            service.Client.BaseClient.Timeout = TimeSpan.FromMinutes(120);
        }
        #endregion

        #region Analysis
        [TestMethod]
        public void Analyze_Success()
        {
            DetailedResponse<AnalyzeResponse> analyzeResult = null;
            using (FileStream fs = File.OpenRead(localGiraffeFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);

                    service.WithHeader("X-Watson-Test", "1");
                    analyzeResult = service.Analyze(
                        collectionIds: new List<string>() { dotnetCollectionId },
                        features: new List<string>() { "objects" },
                        imagesFile: ms);
                }
            }

            Assert.IsNotNull(analyzeResult.Result);
            Assert.IsTrue(analyzeResult.Result.Images[0].Objects.Collections[0].Objects[0]._Object == giraffeClassname);
        }
        #endregion

        #region Collections
        [TestMethod]
        public void Collections_Success()
        {

            string testCollectionName = "testCollection";
            string updatedTestCollectionName = "newTestCollection";
            string testCollectionDescription = ".NET test collection";
            string updatedTestCollectionDescription = "udpdated .NET test collection";
            var createCollectionResult = service.CreateCollection(
                name: ConvertToUtf8(testCollectionName),
                description: ConvertToUtf8(testCollectionDescription));

            var collectionId = createCollectionResult.Result.CollectionId;

            var listCollectionResult = service.ListCollections();

            var getCollectionResult = service.GetCollection(
                collectionId: collectionId);

            var updateCollectionResult = service.UpdateCollection(
                collectionId: collectionId,
                name: ConvertToUtf8(updatedTestCollectionName),
                description: ConvertToUtf8(updatedTestCollectionDescription));

            var deleteCollectionResult = service.DeleteCollection(
                collectionId: collectionId);

            Assert.IsTrue(deleteCollectionResult.StatusCode == 200);
            Assert.IsNotNull(updateCollectionResult.Result);
            Assert.IsTrue(updateCollectionResult.Result.CollectionId == collectionId);
            Assert.IsTrue(updateCollectionResult.Result.Name == updatedTestCollectionName);
            Assert.IsTrue(updateCollectionResult.Result.Description == updatedTestCollectionDescription);
            Assert.IsNotNull(getCollectionResult.Result);
            Assert.IsTrue(getCollectionResult.Result.CollectionId == collectionId);
            Assert.IsTrue(getCollectionResult.Result.Name == testCollectionName);
            Assert.IsTrue(getCollectionResult.Result.Description == testCollectionDescription);
            Assert.IsNotNull(listCollectionResult.Result);
            Assert.IsNotNull(listCollectionResult.Result.Collections);
            Assert.IsTrue(listCollectionResult.Result.Collections.Count > 0);
            Assert.IsNotNull(createCollectionResult.Result);
            Assert.IsTrue(!string.IsNullOrEmpty(collectionId));
            Assert.IsTrue(createCollectionResult.Result.Name == testCollectionName);
        }
        #endregion

        #region Images
        [TestMethod]
        public void Images_Success()
        {
            string testCollectionName = "testCollection";
            string testCollectionDescription = ".NET test collection";

            var createCollectionResult = service.CreateCollection(
                name: ConvertToUtf8(testCollectionName),
                description: ConvertToUtf8(testCollectionDescription));

            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<ImageDetailsList> addImagesResult = null;
            using (FileStream fs = File.OpenRead(localGiraffePositiveExamplesFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    addImagesResult = service.AddImages(
                    collectionId: collectionId,
                    imagesFile: ms);
                }
            }
            var imageId = addImagesResult.Result.Images[0].ImageId;

            var listImageResult = service.ListImages(
                collectionId: collectionId);

            var getImageResult = service.GetImageDetails(
                collectionId: collectionId,
                imageId: imageId);

            var getJpgImageResult = service.GetJpegImage(
                collectionId: collectionId,
                imageId: imageId);

            //  Save file
            using (FileStream fs = File.Create("giraffe.jpg"))
            {
                getJpgImageResult.Result.WriteTo(fs);
                fs.Close();
                getJpgImageResult.Result.Close();
            }

            var deleteImageResult = service.DeleteImage(
                collectionId: collectionId,
                imageId: imageId);

            var deleteCollectionResult = service.DeleteCollection(
                collectionId: collectionId);

            Assert.IsTrue(deleteImageResult.StatusCode == 200);
            Assert.IsNotNull(getJpgImageResult.Result);
            Assert.IsNotNull(getImageResult.Result);
            Assert.IsTrue(getImageResult.Result.ImageId == imageId);
            Assert.IsNotNull(listImageResult.Result);
            Assert.IsNotNull(listImageResult.Result.Images);
            Assert.IsTrue(listImageResult.Result.Images.Count > 0);
            Assert.IsNotNull(addImagesResult.Result);
            Assert.IsNotNull(addImagesResult.Result.Images);
            Assert.IsTrue(addImagesResult.Result.Images.Count > 0);
        }
        #endregion

        #region Training
        [TestMethod]
        public void Training_Success()
        {
            string testCollectionName = "testCollection";
            string testCollectionDescription = ".NET test collection";

            var createCollectionResult = service.CreateCollection(
                name: ConvertToUtf8(testCollectionName),
                description: ConvertToUtf8(testCollectionDescription));

            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<ImageDetailsList> addImagesResult = null;
            using (FileStream fs = File.OpenRead(localGiraffePositiveExamplesFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    addImagesResult = service.AddImages(
                    collectionId: collectionId,
                    imagesFile: ms);
                }
            }

            var imageId = addImagesResult.Result.Images[0].ImageId;

            var objectName = giraffeClassname;
            List<BaseObject> objects = new List<BaseObject>()
            {
                new BaseObject()
                {
                    _Object = objectName,
                    Location = new Location()
                    {
                        Left = 270,
                        Top = 64,
                        Width = 755,
                        Height = 784
                    }

                }
            };

            var addTrainingDataResult = service.AddImageTrainingData(
                collectionId: collectionId,
                imageId: imageId,
                objects: objects);

            var trainCollectionResult = service.Train(
                collectionId: collectionId);

            var deleteCollectionResult = service.DeleteCollection(
                collectionId: collectionId);

            Assert.IsTrue(trainCollectionResult.StatusCode == 202);
            Assert.IsNotNull(trainCollectionResult.Result);
            Assert.IsTrue(trainCollectionResult.Result.ImageCount > 0);
            Assert.IsTrue(trainCollectionResult.Result.TrainingStatus.Objects.InProgress == true);
            Assert.IsNotNull(addTrainingDataResult.Result);
            Assert.IsNotNull(addTrainingDataResult.Result.Objects);
            Assert.IsTrue(addTrainingDataResult.Result.Objects.Count > 0);
            Assert.IsTrue(addTrainingDataResult.Result.Objects[0]._Object == objectName);
        }
        #endregion
        
        #region Delete labeled data
        [TestMethod]
        public void DeleteUserData()
        {
            var deleteLabeledDataResult = service.DeleteUserData(
                customerId: "my_customer_ID");

            Assert.IsTrue(deleteLabeledDataResult.StatusCode == 202);
        }
        #endregion

        //  TODO move this to Utils
        #region ConvertToUtf8
        private static string ConvertToUtf8(string input)
        {
            byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(input);
            return System.Text.Encoding.UTF8.GetString(utf8Bytes);
        }
        #endregion
    }
}
