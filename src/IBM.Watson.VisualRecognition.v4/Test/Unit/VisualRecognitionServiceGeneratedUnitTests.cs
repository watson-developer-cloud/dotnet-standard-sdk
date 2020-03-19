/**
* (C) Copyright IBM Corp. 2020.
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
using NSubstitute;
using System;
using Newtonsoft.Json;
using IBM.Cloud.SDK.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;
using IBM.Watson.VisualRecognition.v4.Model;

namespace IBM.Watson.VisualRecognition.v4.UnitTests
{
    [TestClass]
    public class VisualRecognitionServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            VisualRecognitionService service = new VisualRecognitionService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            VisualRecognitionService service = new VisualRecognitionService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("VISUAL_RECOGNITION_URL");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_URL", "http://www.url.com");
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("testString");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_URL", url);
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            VisualRecognitionService service = new VisualRecognitionService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("testString", "test_service");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/visual-recognition/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestLocationModel()
        {

            Location testRequestModel = new Location()
            {
                Top = 38,
                Left = 38,
                Width = 38,
                Height = 38
            };

            Assert.IsTrue(testRequestModel.Top == 38);
            Assert.IsTrue(testRequestModel.Left == 38);
            Assert.IsTrue(testRequestModel.Width == 38);
            Assert.IsTrue(testRequestModel.Height == 38);
        }

        [TestMethod]
        public void TestObjectTrainingStatusModel()
        {

            ObjectTrainingStatus testRequestModel = new ObjectTrainingStatus()
            {
                Ready = true,
                InProgress = true,
                DataChanged = true,
                LatestFailed = true,
                Description = "testString"
            };

            Assert.IsTrue(testRequestModel.Ready == true);
            Assert.IsTrue(testRequestModel.InProgress == true);
            Assert.IsTrue(testRequestModel.DataChanged == true);
            Assert.IsTrue(testRequestModel.LatestFailed == true);
            Assert.IsTrue(testRequestModel.Description == "testString");
        }

        [TestMethod]
        public void TestTrainingDataObjectModel()
        {

            TrainingDataObject testRequestModel = new TrainingDataObject()
            {
                _Object = "testString",
                Location = LocationModel
            };

            Assert.IsTrue(testRequestModel._Object == "testString");
            Assert.IsTrue(testRequestModel.Location == LocationModel);
        }

        [TestMethod]
        public void TestTrainingStatusModel()
        {

            TrainingStatus testRequestModel = new TrainingStatus()
            {
                Objects = ObjectTrainingStatusModel
            };

            Assert.IsTrue(testRequestModel.Objects == ObjectTrainingStatusModel);
        }

        [TestMethod]
        public void TestUpdateObjectMetadataModel()
        {

            UpdateObjectMetadata testRequestModel = new UpdateObjectMetadata()
            {
                _Object = "testString",
            };

            Assert.IsTrue(testRequestModel._Object == "testString");
        }

        [TestMethod]
        public void TestTestAnalyzeAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'images': [{'source': {'type': 'file', 'filename': 'Filename', 'archive_filename': 'ArchiveFilename', 'source_url': 'SourceUrl', 'resolved_url': 'ResolvedUrl'}, 'dimensions': {'height': 6, 'width': 5}, 'objects': {'collections': [{'collection_id': 'CollectionId', 'objects': [{'object': '_Object', 'location': {'top': 3, 'left': 4, 'width': 5, 'height': 6}, 'score': 5}]}]}, 'errors': [{'code': 'invalid_field', 'message': 'Message', 'more_info': 'MoreInfo', 'target': {'type': 'field', 'name': 'Name'}}]}], 'warnings': [{'code': 'invalid_field', 'message': 'Message', 'more_info': 'MoreInfo'}], 'trace': 'Trace'}";
            var response = new DetailedResponse<AnalyzeResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<AnalyzeResponse>(responseJson),
                StatusCode = 200
            };

            List<string> collectionIds = new List<string> { "testString" };
            List<string> features = new List<string> { "objects" };
            List<FileWithMetadata> imagesFile = new List<FileWithMetadata> { new FileWithMetadata() { Data = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file.")), ContentType = "contentType", Filename = "filename" } };
            List<string> imageUrl = new List<string> { "testString" };
            float? threshold = 36.0f;

            request.As<AnalyzeResponse>().Returns(Task.FromResult(response));

            var result = service.Analyze(collectionIds: collectionIds, features: features, imagesFile: imagesFile, imageUrl: imageUrl, threshold: threshold);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/analyze";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'image_count': 10, 'training_status': {'objects': {'ready': false, 'in_progress': true, 'data_changed': false, 'latest_failed': true, 'description': 'Description'}}}";
            var response = new DetailedResponse<Collection>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Collection>(responseJson),
                StatusCode = 200
            };

            ObjectTrainingStatus ObjectTrainingStatusModel = new ObjectTrainingStatus()
            {
                Ready = true,
                InProgress = true,
                DataChanged = true,
                LatestFailed = true,
                Description = "testString"
            };

            TrainingStatus TrainingStatusModel = new TrainingStatus()
            {
                Objects = ObjectTrainingStatusModel
            };
            string name = "testString";
            string description = "testString";
            TrainingStatus trainingStatus = TrainingStatusModel;

            request.As<Collection>().Returns(Task.FromResult(response));

            var result = service.CreateCollection(name: name, description: description, trainingStatus: trainingStatus);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListCollectionsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'collections': [{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'image_count': 10, 'training_status': {'objects': {'ready': false, 'in_progress': true, 'data_changed': false, 'latest_failed': true, 'description': 'Description'}}}]}";
            var response = new DetailedResponse<CollectionsList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<CollectionsList>(responseJson),
                StatusCode = 200
            };


            request.As<CollectionsList>().Returns(Task.FromResult(response));

            var result = service.ListCollections();

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'image_count': 10, 'training_status': {'objects': {'ready': false, 'in_progress': true, 'data_changed': false, 'latest_failed': true, 'description': 'Description'}}}";
            var response = new DetailedResponse<Collection>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Collection>(responseJson),
                StatusCode = 200
            };

            string collectionId = "testString";

            request.As<Collection>().Returns(Task.FromResult(response));

            var result = service.GetCollection(collectionId: collectionId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'image_count': 10, 'training_status': {'objects': {'ready': false, 'in_progress': true, 'data_changed': false, 'latest_failed': true, 'description': 'Description'}}}";
            var response = new DetailedResponse<Collection>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Collection>(responseJson),
                StatusCode = 200
            };

            ObjectTrainingStatus ObjectTrainingStatusModel = new ObjectTrainingStatus()
            {
                Ready = true,
                InProgress = true,
                DataChanged = true,
                LatestFailed = true,
                Description = "testString"
            };

            TrainingStatus TrainingStatusModel = new TrainingStatus()
            {
                Objects = ObjectTrainingStatusModel
            };
            string collectionId = "testString";
            string name = "testString";
            string description = "testString";
            TrainingStatus trainingStatus = TrainingStatusModel;

            request.As<Collection>().Returns(Task.FromResult(response));

            var result = service.UpdateCollection(collectionId: collectionId, name: name, description: description, trainingStatus: trainingStatus);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteCollectionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string collectionId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteCollection(collectionId: collectionId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddImagesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'images': [{'image_id': 'ImageId', 'updated': '2019-01-01T12:00:00', 'created': '2019-01-01T12:00:00', 'source': {'type': 'file', 'filename': 'Filename', 'archive_filename': 'ArchiveFilename', 'source_url': 'SourceUrl', 'resolved_url': 'ResolvedUrl'}, 'dimensions': {'height': 6, 'width': 5}, 'errors': [{'code': 'invalid_field', 'message': 'Message', 'more_info': 'MoreInfo', 'target': {'type': 'field', 'name': 'Name'}}], 'training_data': {'objects': [{'object': '_Object', 'location': {'top': 3, 'left': 4, 'width': 5, 'height': 6}}]}}], 'warnings': [{'code': 'invalid_field', 'message': 'Message', 'more_info': 'MoreInfo'}], 'trace': 'Trace'}";
            var response = new DetailedResponse<ImageDetailsList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ImageDetailsList>(responseJson),
                StatusCode = 200
            };

            string collectionId = "testString";
            List<FileWithMetadata> imagesFile = new List<FileWithMetadata> { new FileWithMetadata() { Data = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file.")), ContentType = "contentType", Filename = "filename" } };
            List<string> imageUrl = new List<string> { "testString" };
            string trainingData = "testString";

            request.As<ImageDetailsList>().Returns(Task.FromResult(response));

            var result = service.AddImages(collectionId: collectionId, imagesFile: imagesFile, imageUrl: imageUrl, trainingData: trainingData);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/images";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListImagesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'images': [{'image_id': 'ImageId', 'updated': '2019-01-01T12:00:00'}]}";
            var response = new DetailedResponse<ImageSummaryList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ImageSummaryList>(responseJson),
                StatusCode = 200
            };

            string collectionId = "testString";

            request.As<ImageSummaryList>().Returns(Task.FromResult(response));

            var result = service.ListImages(collectionId: collectionId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/images";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetImageDetailsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'image_id': 'ImageId', 'updated': '2019-01-01T12:00:00', 'created': '2019-01-01T12:00:00', 'source': {'type': 'file', 'filename': 'Filename', 'archive_filename': 'ArchiveFilename', 'source_url': 'SourceUrl', 'resolved_url': 'ResolvedUrl'}, 'dimensions': {'height': 6, 'width': 5}, 'errors': [{'code': 'invalid_field', 'message': 'Message', 'more_info': 'MoreInfo', 'target': {'type': 'field', 'name': 'Name'}}], 'training_data': {'objects': [{'object': '_Object', 'location': {'top': 3, 'left': 4, 'width': 5, 'height': 6}}]}}";
            var response = new DetailedResponse<ImageDetails>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ImageDetails>(responseJson),
                StatusCode = 200
            };

            string collectionId = "testString";
            string imageId = "testString";

            request.As<ImageDetails>().Returns(Task.FromResult(response));

            var result = service.GetImageDetails(collectionId: collectionId, imageId: imageId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteImageAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string collectionId = "testString";
            string imageId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteImage(collectionId: collectionId, imageId: imageId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetJpegImageAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<byte[]>()
            {
                Result = new byte[4],
                StatusCode = 200
            };
            string collectionId = "testString";
            string imageId = "testString";
            string size = "full";

            request.As<byte[]>().Returns(Task.FromResult(response));

            var result = service.GetJpegImage(collectionId: collectionId, imageId: imageId, size: size);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}/jpeg";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListObjectMetadataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'object_count': 11, 'objects': [{'object': '_Object', 'count': 5}]}";
            var response = new DetailedResponse<ObjectMetadataList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ObjectMetadataList>(responseJson),
                StatusCode = 200
            };

            string collectionId = "testString";

            request.As<ObjectMetadataList>().Returns(Task.FromResult(response));

            var result = service.ListObjectMetadata(collectionId: collectionId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/objects";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateObjectMetadataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'object': '_Object', 'count': 5}";
            var response = new DetailedResponse<UpdateObjectMetadata>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<UpdateObjectMetadata>(responseJson),
                StatusCode = 200
            };

            string collectionId = "testString";
            string _object = "testString";
            string newObject = "testString";

            request.As<UpdateObjectMetadata>().Returns(Task.FromResult(response));

            var result = service.UpdateObjectMetadata(collectionId: collectionId, _object: _object, newObject: newObject);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/objects/{_object}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetObjectMetadataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'object': '_Object', 'count': 5}";
            var response = new DetailedResponse<ObjectMetadata>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ObjectMetadata>(responseJson),
                StatusCode = 200
            };

            string collectionId = "testString";
            string _object = "testString";

            request.As<ObjectMetadata>().Returns(Task.FromResult(response));

            var result = service.GetObjectMetadata(collectionId: collectionId, _object: _object);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/objects/{_object}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteObjectAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string collectionId = "testString";
            string _object = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteObject(collectionId: collectionId, _object: _object);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/objects/{_object}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestTrainAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'collection_id': 'CollectionId', 'name': 'Name', 'description': 'Description', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'image_count': 10, 'training_status': {'objects': {'ready': false, 'in_progress': true, 'data_changed': false, 'latest_failed': true, 'description': 'Description'}}}";
            var response = new DetailedResponse<Collection>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Collection>(responseJson),
                StatusCode = 202
            };

            string collectionId = "testString";

            request.As<Collection>().Returns(Task.FromResult(response));

            var result = service.Train(collectionId: collectionId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/train";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddImageTrainingDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'objects': [{'object': '_Object', 'location': {'top': 3, 'left': 4, 'width': 5, 'height': 6}}]}";
            var response = new DetailedResponse<TrainingDataObjects>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingDataObjects>(responseJson),
                StatusCode = 200
            };

            Location LocationModel = new Location()
            {
                Top = 38,
                Left = 38,
                Width = 38,
                Height = 38
            };

            TrainingDataObject TrainingDataObjectModel = new TrainingDataObject()
            {
                _Object = "testString",
                Location = LocationModel
            };
            string collectionId = "testString";
            string imageId = "testString";
            List<TrainingDataObject> objects = new List<TrainingDataObject> { TrainingDataObjectModel };

            request.As<TrainingDataObjects>().Returns(Task.FromResult(response));

            var result = service.AddImageTrainingData(collectionId: collectionId, imageId: imageId, objects: objects);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}/training_data";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetTrainingUsageAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'start_time': '2019-01-01T12:00:00', 'end_time': '2019-01-01T12:00:00', 'completed_events': 15, 'trained_images': 13, 'events': [{'type': 'objects', 'collection_id': 'CollectionId', 'completion_time': '2019-01-01T12:00:00', 'status': 'failed', 'image_count': 10}]}";
            var response = new DetailedResponse<TrainingEvents>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingEvents>(responseJson),
                StatusCode = 200
            };

            string startTime = "testString";
            string endTime = "testString";

            request.As<TrainingEvents>().Returns(Task.FromResult(response));

            var result = service.GetTrainingUsage(startTime: startTime, endTime: endTime);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/training_usage";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteUserDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 202
            };

            string customerId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteUserData(customerId: customerId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v4/user_data";
            client.Received().DeleteAsync(messageUrl);
        }

    }
}
