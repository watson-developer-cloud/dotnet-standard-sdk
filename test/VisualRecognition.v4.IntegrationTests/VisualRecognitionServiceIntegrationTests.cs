
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
using IBM.Cloud.SDK.Core.Model;
using IBM.Cloud.SDK.Core.Util;
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
        private string localTurtleFilePath = @"VisualRecognitionTestData/turtle_to_classify.jpg";
        private string giraffeClassname = "giraffe";
        private string turtleClassname = "turtle";
        private string versionDate = "2019-02-11";
        private string collectionId;
        private string dogImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/American_Eskimo_Dog.jpg/1280px-American_Eskimo_Dog.jpg";
        private string catImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/Felis_silvestris_catus_lying_on_rice_straw.jpg/1280px-Felis_silvestris_catus_lying_on_rice_straw.jpg";

        #region Setup
        [TestInitialize]
        public void Setup()
        {
            service = new VisualRecognitionService(versionDate);
            var creds = CredentialUtils.GetServiceProperties("visual_recognition");
            creds.TryGetValue("COLLECTION_ID", out collectionId);
            service.Client.BaseClient.Timeout = TimeSpan.FromMinutes(120);
        }
        #endregion

        #region Analysis
        [TestMethod]
        public void Analyze_Success()
        {
            DetailedResponse<AnalyzeResponse> analyzeResult = null;
            List<FileWithMetadata> imagesFile = new List<FileWithMetadata>();

            using (FileStream fs = File.OpenRead(localGiraffeFilePath), fs2 = File.OpenRead(localTurtleFilePath))
            {
                using (MemoryStream ms = new MemoryStream(), ms2 = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    fs2.CopyTo(ms2);
                    FileWithMetadata fileWithMetadata = new FileWithMetadata()
                    {
                        Data = ms,
                        ContentType = "image/jpeg",
                        Filename = Path.GetFileName(localGiraffeFilePath)
                    };
                    imagesFile.Add(fileWithMetadata);

                    FileWithMetadata fileWithMetadata2 = new FileWithMetadata()
                    {
                        Data = ms2,
                        ContentType = "image/jpeg",
                        Filename = Path.GetFileName(localTurtleFilePath)
                    };
                    imagesFile.Add(fileWithMetadata2);

                    service.WithHeader("X-Watson-Test", "1");
                    analyzeResult = service.Analyze(
                        collectionIds: new List<string>() { collectionId },
                        features: new List<string>() { "objects" },
                        imagesFile: imagesFile);
                }
            }

            Assert.IsNotNull(analyzeResult.Result);
            Assert.IsTrue(analyzeResult.Result.Images[0].Objects.Collections[0].Objects[0]._Object == giraffeClassname);
            Assert.IsTrue(analyzeResult.Result.Images.Count == 2);
            Assert.IsTrue(analyzeResult.Result.Images[0].Source.Filename == Path.GetFileName(localGiraffeFilePath));
            Assert.IsTrue(analyzeResult.Result.Images[1].Source.Filename == Path.GetFileName(localTurtleFilePath));
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
            var listCollectionResult = service.ListCollections();

            var createCollectionResult = service.CreateCollection(
                name: Utility.ConvertToUtf8(testCollectionName),
                description: Utility.ConvertToUtf8(testCollectionDescription));

            var collectionId = createCollectionResult.Result.CollectionId;

            var getCollectionResult = service.GetCollection(
                collectionId: collectionId);

            var updateCollectionResult = service.UpdateCollection(
                collectionId: collectionId,
                name: Utility.ConvertToUtf8(updatedTestCollectionName),
                description: Utility.ConvertToUtf8(updatedTestCollectionDescription));

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
                name: Utility.ConvertToUtf8(testCollectionName),
                description: Utility.ConvertToUtf8(testCollectionDescription));

            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<ImageDetailsList> addImagesResult = null;
            List<FileWithMetadata> imagesFile = new List<FileWithMetadata>();

            using (FileStream fs = File.OpenRead(localGiraffePositiveExamplesFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);

                    FileWithMetadata fileWithMetadata = new FileWithMetadata()
                    {
                        Data = ms,
                        ContentType = "application/zip",
                        Filename = Path.GetFileName(localGiraffePositiveExamplesFilePath)
                    };
                    imagesFile.Add(fileWithMetadata);

                    addImagesResult = service.AddImages(
                        collectionId: collectionId,
                        imagesFile: imagesFile);
                }
            }
            var imageId = addImagesResult.Result.Images[0].ImageId;

            var addImageViaUrlResult = service.AddImages(
                collectionId: collectionId,
                imageUrl: new List<string>() { dogImageUrl, catImageUrl });

            var listImageResult = service.ListImages(
                collectionId: collectionId);

            var getImageResult = service.GetImageDetails(
                collectionId: collectionId,
                imageId: imageId);

            var getJpgImageResult = service.GetJpegImage(
                collectionId: collectionId,
                imageId: imageId, 
                size: "thumbnail"
                );

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
                name: Utility.ConvertToUtf8(testCollectionName),
                description: Utility.ConvertToUtf8(testCollectionDescription));

            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<ImageDetailsList> addImagesResult = null;
            List<FileWithMetadata> imagesFile = new List<FileWithMetadata>();

            using (FileStream fs = File.OpenRead(localGiraffePositiveExamplesFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);

                    FileWithMetadata fileWithMetadata = new FileWithMetadata()
                    {
                        Data = ms,
                        ContentType = "application/zip",
                        Filename = Path.GetFileName(localGiraffePositiveExamplesFilePath)
                    };
                    imagesFile.Add(fileWithMetadata);

                    addImagesResult = service.AddImages(
                        collectionId: collectionId,
                        imagesFile: imagesFile);
                }
            }

            var imageId = addImagesResult.Result.Images[0].ImageId;

            var objectName = giraffeClassname;
            List<TrainingDataObject> objects = new List<TrainingDataObject>()
            {
                new TrainingDataObject()
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

        #region Training Usage
        [TestMethod]
        public void GetTrainingUsage()
        {
            service.WithHeader("X-Watson-Test", "1");
            var startTime = "2019-11-18";
            var endTime = "2019-11-20";
            var getTrainingUsageResult = service.GetTrainingUsage(
                startTime: startTime, 
                endTime: endTime
                );

            Assert.IsNotNull(getTrainingUsageResult.Result);
            Assert.IsTrue(getTrainingUsageResult.Result.StartTime.Value.Year == 2019);
            Assert.IsTrue(getTrainingUsageResult.Result.StartTime.Value.Month == 11);
            Assert.IsTrue(getTrainingUsageResult.Result.StartTime.Value.Day == 18);
            Assert.IsTrue(getTrainingUsageResult.Result.EndTime.Value.Year == 2019);
            Assert.IsTrue(getTrainingUsageResult.Result.EndTime.Value.Month == 11);
            Assert.IsTrue(getTrainingUsageResult.Result.EndTime.Value.Day == 20);
            Assert.IsTrue(getTrainingUsageResult.Result.TrainedImages > 0);
            Assert.IsTrue(getTrainingUsageResult.Result.Events.Count > 0);
            Assert.IsTrue(getTrainingUsageResult.Result.Events[0].CollectionId == collectionId);
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
    }
}
