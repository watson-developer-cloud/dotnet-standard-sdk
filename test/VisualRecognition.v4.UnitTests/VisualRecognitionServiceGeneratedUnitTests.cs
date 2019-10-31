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

using NSubstitute;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using IBM.Watson.VisualRecognition.v4.Model;
using IBM.Cloud.SDK.Core.Model;

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
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY", "apikey");
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("versionDate");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY", apikey);
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
            var apikey = System.Environment.GetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY", "apikey");
            var url = System.Environment.GetEnvironmentVariable("VISUAL_RECOGNITION_URL");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_URL", null);
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/visual-recognition/api");
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_URL", url);
            System.Environment.SetEnvironmentVariable("VISUAL_RECOGNITION_APIKEY", apikey);
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
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionIds = new List<string>() { "collectionIds0", "collectionIds1" };
            var features = new List<string>() { "features0", "features1" };
            var imageUrl = new List<string>() { "imageUrl0", "imageUrl1" };
            float? threshold = 0.5f;

            var result = service.Analyze(collectionIds: collectionIds, features: features, imagesFile: imagesFile, imageUrl: imageUrl, threshold: threshold);

            request.Received().WithArgument("version", versionDate);

        }
        [TestMethod]
        public void CreateCollection_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var name = "name";
            var description = "description";
            var result = service.CreateCollection(
                name: name,
                description: description
                );

            var result = service.CreateCollection(name: name, description: description);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(name))
            {
                bodyObject["name"] = JToken.FromObject(name);
            }
            if (!string.IsNullOrEmpty(description))
            {
                bodyObject["description"] = JToken.FromObject(description);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
        [TestMethod]
        public void ListCollections_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;


            var result = service.ListCollections();

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void GetCollection_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";

            var result = service.GetCollection(collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);
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
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";

            var result = service.UpdateCollection(collectionId: collectionId, name: name, description: description);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(name))
            {
                bodyObject["name"] = JToken.FromObject(name);
            }
            if (!string.IsNullOrEmpty(description))
            {
                bodyObject["description"] = JToken.FromObject(description);
            }
            var json = JsonConvert.SerializeObject(bodyObject);

            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v4/collections/{collectionId}");
        }
        [TestMethod]
        public void DeleteCollection_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";

            var result = service.DeleteCollection(collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);
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
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";
            var imagesFile = new FileWithMetadata();
                var imagesFile = new List<FileWithMetadata>();
            var imageUrl = new List<string>() { "imageUrl0", "imageUrl1" };
                var imageUrl = new List<string>();
            var trainingData = "trainingData";
                var trainingData = new string();

            var result = service.AddImages(collectionId: collectionId, imagesFile: imagesFile, imageUrl: imageUrl, trainingData: trainingData);

            request.Received().WithArgument("version", versionDate);
            client.Received().PostAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images");

        }
        [TestMethod]
        public void ListImages_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";

            var result = service.ListImages(collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images");
        }
        [TestMethod]
        public void GetImageDetails_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";
            var imageId = "imageId";

            var result = service.GetImageDetails(collectionId: collectionId, imageId: imageId);

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}");
        }
        [TestMethod]
        public void DeleteImage_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";
            var imageId = "imageId";

            var result = service.DeleteImage(collectionId: collectionId, imageId: imageId);

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}");
        }
        [TestMethod]
        public void GetJpegImage_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";
            var imageId = "imageId";
            var size = "size";

            var result = service.GetJpegImage(collectionId: collectionId, imageId: imageId, size: size);

            request.Received().WithArgument("version", versionDate);
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
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";

            var result = service.Train(collectionId: collectionId);

            request.Received().WithArgument("version", versionDate);
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
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var collectionId = "collectionId";
            var imageId = "imageId";
            var objects = new List<TrainingDataObject>();
            var result = service.AddImageTrainingData(
                collectionId: collectionId,
                imageId: imageId,
                objects: objects
                );

            var result = service.AddImageTrainingData(collectionId: collectionId, imageId: imageId, objects: objects);

            JObject bodyObject = new JObject();
            if (objects != null && objects.Count > 0)
            {
                bodyObject["objects"] = JToken.FromObject(objects);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v4/collections/{collectionId}/images/{imageId}/training_data");
        }
        [TestMethod]
        public void GetTrainingUsage_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var startTime = "startTime";
            var endTime = "endTime";

            var result = service.GetTrainingUsage(startTime: startTime, endTime: endTime);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void DeleteUserData_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var customerId = "customerId";

            var result = service.DeleteUserData(customerId: customerId);

            request.Received().WithArgument("version", versionDate);
        }
    }
}
