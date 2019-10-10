/**
* (C) Copyright IBM Corp. 2018, 2019.
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


using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using IBM.Watson.VisualRecognition.v4.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Cloud.SDK.Core.Model;
using System.Net;

namespace IBM.Watson.VisualRecognition.v4.UnitTests
{
    [TestClass]
    public class VisualRecognitionServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            VisualRecognitionService service =
            new VisualRecognitionService(httpClient: null);
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
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("versionDate");
        }

        [TestMethod]
        public void Constructor()
        {
            VisualRecognitionService service = new VisualRecognitionService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            VisualRecognitionService service = new VisualRecognitionService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            VisualRecognitionService service = new VisualRecognitionService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var url = Environment.GetEnvironmentVariable("VISUAL_RECOGNITION_SERVICE_URL");
            Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_SERVICE_URL", null);
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/visual-recognition/api");
            Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_SERVICE_URL", url);
        }
        #endregion

        [TestMethod]
        public void Analyze_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.Analyze(
                collectionIds: new List<string>() { "collectionIds0", "collectionIds1" },
                features: new List<string>() { "features0", "features1" },
                imagesFile: new List<FileWithMetadata>() { new FileWithMetadata() },
                imageUrl: new List<string>() { "imageUrl0", "imageUrl1" },
                threshold: default(float?)
                );


            request.Received().WithArgument("version", "versionDate");
        }

        [TestMethod]
        public void CreateCollection_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateCollection(
                name: "name",
                description: "description"
                );


            request.Received().WithArgument("version", "versionDate");
        }

        [TestMethod]
        public void ListCollections_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.ListCollections(
                );


            request.Received().WithArgument("version", "versionDate");
        }

        [TestMethod]
        public void GetCollection_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.GetCollection(
                collectionId: "collectionId"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().GetAsync($"{service.ServiceUrl}/v4/collections/{collectionId}");
        }

        [TestMethod]
        public void UpdateCollection_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateCollection(
                collectionId: "collectionId",
                name: "name",
                description: "description"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().PostAsync($"{service.ServiceUrl}/v4/collections/{collectionId}");
        }

        [TestMethod]
        public void DeleteCollection_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteCollection(
                collectionId: "collectionId"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().DeleteAsync($"{service.ServiceUrl}/v4/collections/{collectionId}");
        }

        [TestMethod]
        public void AddImages_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.AddImages(
                collectionId: "collectionId",
                imagesFile: new List<FileWithMetadata>() { new FileWithMetadata() },
                imageUrl: new List<string>() { "imageUrl0", "imageUrl1" },
                trainingData: "trainingData"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().PostAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images");
        }

        [TestMethod]
        public void ListImages_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.ListImages(
                collectionId: "collectionId"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().GetAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images");
        }

        [TestMethod]
        public void GetImageDetails_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.GetImageDetails(
                collectionId: "collectionId",
                imageId: "imageId"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().GetAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}");
        }

        [TestMethod]
        public void DeleteImage_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteImage(
                collectionId: "collectionId",
                imageId: "imageId"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().DeleteAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}");
        }

        [TestMethod]
        public void GetJpegImage_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.GetJpegImage(
                collectionId: "collectionId",
                imageId: "imageId",
                size: "size"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().GetAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}/jpeg");
        }

        [TestMethod]
        public void Train_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.Train(
                collectionId: "collectionId"
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().PostAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/train");
        }

        [TestMethod]
        public void AddImageTrainingData_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.AddImageTrainingData(
                collectionId: "collectionId",
                imageId: "imageId",
                objects: new List<TrainingDataObject>() { new TrainingDataObject() }
                );


            request.Received().WithArgument("version", "versionDate");
            client.Received().PostAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}/training_data");
        }

        [TestMethod]
        public void DeleteUserData_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteUserData(
                customerId: "customerId"
                );


            request.Received().WithArgument("version", "versionDate");
        }

    }
}
