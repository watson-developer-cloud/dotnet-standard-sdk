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
using System.Net;

namespace IBM.Watson.VisualRecognition.v4.UnitTests
{
    [TestClass]
    public class VisualRecognitionUnitTests
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
            VisualRecognitionService service =
                new VisualRecognitionService(new IBMHttpClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            VisualRecognitionService service = Substitute.For<VisualRecognitionService>("verisionDate");
        }

        [TestMethod]
        public void Constructor()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(new IBMHttpClient());

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

        #region Analysis
        [TestMethod]
        public void Analysis()
        {
            IClient client = CreateClient();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region response
            var analyzeResponse = new AnalyzeResponse()
            {
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Source = new ImageSource()
                        {
                            Type = "type",
                            Filename = "filename",
                            ArchiveFilename = "archiveFilename",
                            SourceUrl = "sourceUrl",
                            ResolvedUrl = "resolvedUrl"
                        },
                        Dimensions = new ImageDimensions()
                        {
                            Width = 100,
                            Height = 200
                        },
                        Objects = new DetectedObjects()
                        {
                            Collections = new List<CollectionObjects>()
                            {
                                new CollectionObjects()
                                {
                                    CollectionId = "collectionId",
                                    Objects = new List<ObjectDetail>()
                                    {
                                        new ObjectDetail()
                                        {
                                            _Object = "object",
                                            Location = new Location()
                                            {
                                                Top = 0,
                                                Left = 1,
                                                Width = 2,
                                                Height = 3
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var response = new DetailedResponse<AnalyzeResponse>();
            response.Result = analyzeResponse;
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new MultipartFormDataContent())
                .Returns(request);
            request.As<AnalyzeResponse>()
                .Returns(Task.FromResult(response));

            NoAuthAuthenticator authenticator = new NoAuthAuthenticator();
            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";
            service.Analyze(
                collectionIds: new List<string> { "colletionIds" },
                features: new List<string> { "features" });

            Assert.IsTrue(response.Result.Images[0].Source.Type == "type");
            Assert.IsTrue(response.Result.Images[0].Source.Filename == "filename");
            Assert.IsTrue(response.Result.Images[0].Source.ArchiveFilename == "archiveFilename");
            Assert.IsTrue(response.Result.Images[0].Source.SourceUrl == "sourceUrl");
            Assert.IsTrue(response.Result.Images[0].Source.ResolvedUrl == "resolvedUrl");
            Assert.IsTrue(response.Result.Images[0].Dimensions.Width == 100);
            Assert.IsTrue(response.Result.Images[0].Dimensions.Height == 200);
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].CollectionId == "collectionId");
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0]._Object == "object");
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0].Location.Top == 0);
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0].Location.Left == 1);
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0].Location.Width == 2);
            Assert.IsTrue(response.Result.Images[0].Objects.Collections[0].Objects[0].Location.Height == 3);

        }
        #endregion

        #region CreateCollection
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCollectionNoVersionDate()
        {
            VisualRecognitionService service = new VisualRecognitionService(
                versionDate: "versionDate",
                authenticator: new NoAuthAuthenticator()
                );
            service.VersionDate = null;

            var result = service.CreateCollection(
                name: "collectionName",
                description: "collectionDescription"
                );
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateCollectionCatchException()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateCollection(
                name: "collectionName",
                description: "collectionDescription"
                );
        }

        [TestMethod]
        public void CreateCollection()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";
            var result = service.CreateCollection(
                name: "collectionName",
                description: "collectionDescription"
                );

            JObject bodyObject = new JObject();
            bodyObject["name"] = "collectionName";
            bodyObject["description"] = "collectionDescription";
            var json = JsonConvert.SerializeObject(bodyObject);

            request.Received().WithArgument("version", "versionDate");
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));

        }
        #endregion

        #region ListCollections
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCollectionsNoVersionDate()
        {
            VisualRecognitionService service = new VisualRecognitionService(
                versionDate: "versionDate",
                authenticator: new NoAuthAuthenticator()
                );
            service.VersionDate = null;

            var result = service.ListCollections();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListCollectionCatchException()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.ListCollections();
        }

        [TestMethod]
        public void ListCollections()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";
            var result = service.ListCollections();

            request.Received().WithArgument("version", "versionDate");
        }
        #endregion

        #region GetCollection
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollectionsNoVersionDate()
        {
            VisualRecognitionService service = new VisualRecognitionService(
                versionDate: "versionDate",
                authenticator: new NoAuthAuthenticator()
                );
            service.VersionDate = null;

            var result = service.GetCollection(
                collectionId: "collectionId"
                );
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollectionsNoCollectionId()
        {
            VisualRecognitionService service = new VisualRecognitionService(
                versionDate: "versionDate",
                authenticator: new NoAuthAuthenticator()
                );

            var result = service.GetCollection(
                collectionId: null
                );
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetCollectionCatchException()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.GetCollection(
                collectionId: "collectionId"
                );
        }

        [TestMethod]
        public void GetCollection()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";
            var result = service.GetCollection(
                collectionId: "collectionId"
                );

            request.Received().WithArgument("version", "versionDate");
            client.Received().GetAsync($"{service.ServiceUrl}/v4/collections/{"collectionId"}");
        }
        #endregion

        #region UpdateCollection
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCollectionsNoVersionDate()
        {
            VisualRecognitionService service = new VisualRecognitionService(
                versionDate: "versionDate",
                authenticator: new NoAuthAuthenticator()
                );
            service.VersionDate = null;

            var result = service.UpdateCollection(
                collectionId: "collectionId"
                );
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCollectionsNoCollectionId()
        {
            VisualRecognitionService service = new VisualRecognitionService(
                versionDate: "versionDate",
                authenticator: new NoAuthAuthenticator()
                );

            var result = service.UpdateCollection(
                collectionId: null
                );
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateCollectionCatchException()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateCollection(
                collectionId: "collectionId",
                name: "collectionName",
                description: "collectionDescription"
                );
        }

        [TestMethod]
        public void UpdateCollections()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";
            var result = service.UpdateCollection(
                collectionId: "collectionId",
                name: "collectionName",
                description: "collectionDescription"
                );

            JObject bodyObject = new JObject();
            bodyObject["name"] = "collectionName";
            bodyObject["description"] = "collectionDescription";
            var json = JsonConvert.SerializeObject(bodyObject);

            request.Received().WithArgument("version", "versionDate");
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v4/collections/{"collectionId"}");
        }
        #endregion

        #region DeleteCollection
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCollectionsNoVersionDate()
        {
            VisualRecognitionService service = new VisualRecognitionService(
                versionDate: "versionDate",
                authenticator: new NoAuthAuthenticator()
                );
            service.VersionDate = null;

            var result = service.DeleteCollection(
                collectionId: "collectionId"
                );
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCollectionsNoCollectionId()
        {
            VisualRecognitionService service = new VisualRecognitionService(
                versionDate: "versionDate",
                authenticator: new NoAuthAuthenticator()
                );

            var result = service.DeleteCollection(
                collectionId: null
                );
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteCollectionCatchException()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteCollection(
                collectionId: "collectionId"
                );
        }

        [TestMethod]
        public void DeleteCollection()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            VisualRecognitionService service = new VisualRecognitionService(client);
            service.VersionDate = "versionDate";
            var result = service.DeleteCollection(
                collectionId: "collectionId"
                );

            request.Received().WithArgument("version", "versionDate");
            client.Received().DeleteAsync($"{service.ServiceUrl}/v4/collections/{"collectionId"}");
        }
        #endregion

        #region AddImages
        #endregion

        #region ListImages
        #endregion

        #region GetImageDetails
        #endregion

        #region DeleteImage
        #endregion

        #region GetJpgImage
        #endregion

        #region Train
        #endregion

        #region AddImageTrainingData
        #endregion

        #region DeleteUserData
        #endregion

        #region Create Client
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>())
                .Returns(client);

            return client;
        }
        #endregion

    }
}
