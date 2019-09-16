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

namespace IBM.Watson.VisualRecognition.v4.UnitTests
{
    [TestClass]
    public class DiscoveryUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(httpClient: null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            VisualRecognitionService service =
                new VisualRecognitionService(new IBMHttpClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor_Authenticator()
        {
            VisualRecognitionService service = new VisualRecognitionService("versionDate", new NoAuthAuthenticator());

            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NoVersion()
        {
            VisualRecognitionService service = new VisualRecognitionService(null, new NoAuthAuthenticator());
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
            DetailedResponse<AnalyzeResponse> response = new DetailedResponse<AnalyzeResponse>()
            {
                Result = new AnalyzeResponse()
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
                                                    Width
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            #endregion

            NoAuthAuthenticator authenticator = new NoAuthAuthenticator();
            VisualRecognitionService service = new VisualRecognitionService("versionDate", authenticator);
            service.Analyze(
                collectionIds: "colletionIds",
                features: "features");

            //Assert.
        }
        #endregion

        #region Create Client
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                .Returns(client);

            return client;
        }
        #endregion

        #region Environments
        //#region List Environments
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void ListEnvironments_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;
        //    service.ListEnvironments();
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void ListEnvironments_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.GetAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";
        //    service.ListEnvironments();
        //}

        //[TestMethod]
        //public void ListEnvironments_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.GetAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response


        //    var diskUsage = Substitute.For<DiskUsage>();
        //    diskUsage.UsedBytes.Returns(0);
        //    diskUsage.MaximumAllowedBytes.Returns(0);

        //    DetailedResponse<ListEnvironmentsResponse> response = new DetailedResponse<ListEnvironmentsResponse>()
        //    {
        //        Result = new ListEnvironmentsResponse()
        //        {
        //            Environments = new List<Environment>()
        //            {
        //               new Environment()
        //               {
        //                   Status = Environment.StatusEnumValue.PENDING,
        //                   Name = "name",
        //                   Description = "description",
        //                   Size = Environment.SizeEnumValue.XS
        //               }
        //            }
        //        }
        //    };
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.As<ListEnvironmentsResponse>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.ListEnvironments();

        //    Assert.IsNotNull(result);
        //    client.Received().GetAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.Environments != null);
        //    Assert.IsTrue(result.Result.Environments.Count > 0);
        //    Assert.IsTrue(result.Result.Environments[0].Status == Environment.StatusEnumValue.PENDING);
        //    Assert.IsTrue(result.Result.Environments[0].Name == "name");
        //    Assert.IsTrue(result.Result.Environments[0].Description == "description");
        //    Assert.IsTrue(result.Result.Environments[0].Size == Environment.SizeEnumValue.XS);
        //}
        //#endregion

        //#region Create Environment
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void CreateEnvironment_No_Environment()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.CreateEnvironment(null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void CreateEnvironment_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;

        //    service.CreateEnvironment("environment");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void CreateEnvironment_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";

        //    service.CreateEnvironment("environment");
        //}

        //[TestMethod]
        //public void CreateEnvironment_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    DetailedResponse<Environment> response = new DetailedResponse<Environment>()
        //    {
        //        Result = new Environment()
        //        {
        //            Status = Environment.StatusEnumValue.PENDING,
        //            Name = "name",
        //            Description = "description",
        //            Size = Environment.SizeEnumValue.XS,
        //            IndexCapacity = new IndexCapacity()
        //            {
        //                DiskUsage = new DiskUsage() { }
        //            }
        //        }
        //    };
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithBodyContent(new StringContent("environment"))
        //        .Returns(request);
        //    request.As<Environment>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.CreateEnvironment("environment");

        //    Assert.IsNotNull(result);
        //    client.Received().PostAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.Name == "name");
        //    Assert.IsTrue(result.Result.Description == "description");
        //    Assert.IsTrue(result.Result.Size == Environment.SizeEnumValue.XS);
        //}
        //#endregion

        //#region Delete Environment
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void DeleteEnvironment_No_EnvironmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.DeleteEnvironment(null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void DeleteEnvironment_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;
        //    service.DeleteEnvironment("environmentId");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void DeleteEnvironment_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.DeleteAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";

        //    service.DeleteEnvironment("environmentId");
        //}

        //[TestMethod]
        //public void DeleteEnvironment_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.DeleteAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    DetailedResponse<DeleteEnvironmentResponse> response = new DetailedResponse<DeleteEnvironmentResponse>()
        //    {
        //        Result = new DeleteEnvironmentResponse()
        //        {
        //            EnvironmentId = "environmentId",
        //            Status = DeleteEnvironmentResponse.StatusEnumValue.DELETED
        //        }
        //    };
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.As<DeleteEnvironmentResponse>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.DeleteEnvironment("environmentId");

        //    Assert.IsNotNull(result);
        //    client.Received().DeleteAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.EnvironmentId == "environmentId");
        //    Assert.IsTrue(result.Result.Status == DeleteEnvironmentResponse.StatusEnumValue.DELETED);
        //}
        //#endregion

        //#region Get Envronment
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void GetEnvironment_No_EnvironmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.GetEnvironment(null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void GetEnvironment_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;
        //    service.GetEnvironment("environmentId");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void GetEnvironment_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.GetAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";
        //    service.GetEnvironment("EnvironmentId");
        //}

        //[TestMethod]
        //public void GetEnvironment_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.GetAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    DetailedResponse<Environment> response = new DetailedResponse<Environment>()
        //    {
        //        Result = new Environment()
        //        {
        //            Status = Environment.StatusEnumValue.PENDING,
        //            Name = "name",
        //            Description = "description",
        //            Size = Environment.SizeEnumValue.XS
        //        }
        //    };
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.As<Environment>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.GetEnvironment("environmentId");

        //    Assert.IsNotNull(result);
        //    client.Received().GetAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.Status == Environment.StatusEnumValue.PENDING);
        //    Assert.IsTrue(result.Result.Name == "name");
        //    Assert.IsTrue(result.Result.Description == "description");
        //    Assert.IsTrue(result.Result.Size == Environment.SizeEnumValue.XS);
        //}
        //#endregion

        //#region Update Environment
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void UpdateEnvironment_No_EnvironmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);

        //    service.UpdateEnvironment(null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void UpdateEnvironment_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;

        //    service.UpdateEnvironment("environmentId");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void UpdateEnvironment_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PutAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";

        //    service.UpdateEnvironment("environmentId");
        //}

        //[TestMethod]
        //public void UpdateEnvironment_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PutAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    DetailedResponse<Environment> response = new DetailedResponse<Environment>()
        //    {
        //        Result = new Environment()
        //        {
        //            Status = Environment.StatusEnumValue.PENDING,
        //            Name = "name",
        //            Description = "description",
        //            Size = Environment.SizeEnumValue.XS
        //        }
        //    };
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithBodyContent(new StringContent("environment"))
        //        .Returns(request);
        //    request.As<Environment>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.UpdateEnvironment("environmentId");

        //    Assert.IsNotNull(result);
        //    client.Received().PutAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.Status == Environment.StatusEnumValue.PENDING);
        //    Assert.IsTrue(result.Result.Name == "name");
        //    Assert.IsTrue(result.Result.Description == "description");
        //    Assert.IsTrue(result.Result.Size == Environment.SizeEnumValue.XS);
        //}
        //#endregion
        #endregion

        #region Preview Environment
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void TestConfigurationInEnvronment_No_EnvironmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.TestConfigurationInEnvironment(null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void TestConfigurationInEnvronment_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;

        //    service.TestConfigurationInEnvironment("envronmentId");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void TestConfigurationInEnvronment_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";

        //    service.TestConfigurationInEnvironment("environmentId");
        //}

        //[TestMethod]
        //public void TestConfigurationInEnvronment_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    var notice = Substitute.For<Notice>();
        //    notice.NoticeId.Returns("noticeId");
        //    notice.Created.Returns(DateTime.MinValue);
        //    notice.DocumentId.Returns("documentId");
        //    notice.Step.Returns("step");
        //    notice.Description.Returns("description");
        //    notice.QueryId.Returns("queryId");
        //    notice.Severity = Notice.SeverityEnumValue.ERROR;

        //    var response = Substitute.For<DetailedResponse<TestDocument>>();
        //    response.Result = Substitute.For<TestDocument>();
        //    response.Result.ConfigurationId.Returns("configurationId");
        //    response.Result.Status.Returns("status");
        //    response.Result.EnrichedFieldUnits.Returns(1);
        //    response.Result.OriginalMediaType.Returns("originalMediaType");

        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
        //        .Returns(request);
        //    request.As<TestDocument>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.TestConfigurationInEnvironment("environmentId");

        //    Assert.IsNotNull(result);
        //    client.Received().PostAsync(Arg.Any<string>());

        //    Assert.IsNotNull(result.Result.ConfigurationId);
        //    Assert.IsNotNull(result.Result.Status);
        //    Assert.IsNotNull(result.Result.EnrichedFieldUnits);
        //    Assert.IsNotNull(result.Result.OriginalMediaType);
        //}

        //[TestMethod]
        //public void TestConfigurationInEnvronment_Success_WithConfiguration()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    var notice = Substitute.For<Notice>();
        //    notice.NoticeId.Returns("noticeId");
        //    notice.Created.Returns(DateTime.MinValue);
        //    notice.DocumentId.Returns("documentId");
        //    notice.Step.Returns("step");
        //    notice.Description.Returns("description");
        //    notice.QueryId.Returns("queryId");
        //    notice.Severity = Notice.SeverityEnumValue.ERROR;

        //    DetailedResponse<TestDocument> response = Substitute.For<DetailedResponse<TestDocument>>();
        //    response.Result = Substitute.For<TestDocument>();
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
        //        .Returns(request);
        //    request.As<TestDocument>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.TestConfigurationInEnvironment("environmentId", configuration: "configuration");

        //    Assert.IsNotNull(result.Result);
        //    client.Received().PostAsync(Arg.Any<string>());
        //}

        //[TestMethod]
        //public void TestConfigurationInEnvronment_Success_WithFile()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    var notice = Substitute.For<Notice>();
        //    notice.NoticeId.Returns("noticeId");
        //    notice.Created.Returns(DateTime.MinValue);
        //    notice.DocumentId.Returns("documentId");
        //    notice.Step.Returns("step");
        //    notice.Description.Returns("description");
        //    notice.QueryId.Returns("queryId");
        //    notice.Severity = Notice.SeverityEnumValue.ERROR;

        //    DetailedResponse<TestDocument> response = Substitute.For<DetailedResponse<TestDocument>>();
        //    response.Result = Substitute.For<TestDocument>();
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
        //        .Returns(request);
        //    request.As<TestDocument>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";
        //    var result = service.TestConfigurationInEnvironment("environmentId", file: Substitute.For<MemoryStream>(), filename: "filename");
        //    Assert.IsNotNull(result.Result);
        //    client.Received().PostAsync(Arg.Any<string>());
        //}

        //[TestMethod]
        //public void TestConfigurationInEnvronment_Success_WithMetadata()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    var notice = Substitute.For<Notice>();
        //    notice.NoticeId.Returns("noticeId");
        //    notice.Created.Returns(DateTime.MinValue);
        //    notice.DocumentId.Returns("documentId");
        //    notice.Step.Returns("step");
        //    notice.Description.Returns("description");
        //    notice.QueryId.Returns("queryId");
        //    notice.Severity = Notice.SeverityEnumValue.ERROR;

        //    DetailedResponse<TestDocument> response = Substitute.For<DetailedResponse<TestDocument>>();
        //    response.Result = Substitute.For<TestDocument>();
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
        //        .Returns(request);
        //    request.As<TestDocument>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.TestConfigurationInEnvironment("environmentId", metadata: "metadata");
        //    Assert.IsNotNull(result.Result);
        //    client.Received().PostAsync(Arg.Any<string>());
        //}
        #endregion

        #region Configrations
        //#region List Configurations
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void ListConfigurations_No_EnvironmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.ListConfigurations(null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void ListConfigurations_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;
        //    service.ListConfigurations("environmentId");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void ListConfigurations_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.GetAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";
        //    service.ListConfigurations("environmentId");
        //}

        //[TestMethod]
        //public void ListConfigurations_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.GetAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    var configuration = Substitute.For<Configuration>();
        //    configuration.ConfigurationId.Returns("configurationId");
        //    configuration.Created.Returns(DateTime.MinValue);
        //    configuration.Updated.Returns(DateTime.MinValue);
        //    configuration.Name = "name";
        //    configuration.Description = "description";
        //    configuration.Conversions = new Conversions()
        //    {
        //        Pdf = new PdfSettings()
        //        {
        //            Heading = new PdfHeadingDetection()
        //            {
        //                Fonts = new List<FontSetting>()
        //                {
        //                    new FontSetting()
        //                    {
        //                        Level = 1,
        //                        Bold = false,
        //                        Italic = false,
        //                        Name = "name"
        //                    }
        //                }
        //            }
        //        },
        //        Word = new WordSettings()
        //        {
        //            Heading = new WordHeadingDetection()
        //            {
        //                Fonts = new List<FontSetting>()
        //                {
        //                    new FontSetting()
        //                    {
        //                        Level = 1,
        //                        Bold = false,
        //                        Italic = false,
        //                        Name = "name"
        //                    }
        //                },
        //                Styles = new List<WordStyle>()
        //                {
        //                    new WordStyle()
        //                    {
        //                        Level = 1,
        //                        Names = new List<string>
        //                        {
        //                            "name"
        //                        }
        //                    }
        //                }
        //            }
        //        },
        //        Html = new HtmlSettings()
        //        {
        //            ExcludeTagsCompletely = new List<string>()
        //            {
        //                "exclude"
        //            },
        //            ExcludeTagsKeepContent = new List<string>()
        //            {
        //                "exclude but keep content"
        //            },
        //            KeepContent = new XPathPatterns()
        //            {
        //                Xpaths = new List<string>()
        //                {
        //                    "keepContent"
        //                }
        //            },
        //            ExcludeContent = new XPathPatterns()
        //            {
        //                Xpaths = new List<string>()
        //                {
        //                    "excludeContent"
        //                }
        //            },
        //            KeepTagAttributes = new List<string>()
        //            {
        //                "keepTagAttributes"
        //            },
        //            ExcludeTagAttributes = new List<string>()
        //            {
        //                "excludeTagAttributes"
        //            }
        //        },
        //        JsonNormalizations = new List<NormalizationOperation>()
        //        {
        //            new NormalizationOperation()
        //            {

        //            }
        //        },

        //    };
        //    configuration.Enrichments = new List<Enrichment>()
        //    {
        //        new Enrichment()
        //        {
        //            Description = "description",
        //            DestinationField = "destinationField",
        //            SourceField = "sourceField",
        //            Overwrite = false,
        //            _Enrichment = "enrichmentName",
        //            IgnoreDownstreamErrors = false,
        //            Options = new EnrichmentOptions()
        //            {
        //                Features = new NluEnrichmentFeatures()
        //                {
        //                }
        //            }
        //        }
        //    };
        //    configuration.Normalizations = new List<NormalizationOperation>()
        //    {
        //        new NormalizationOperation()
        //        {
        //            Operation = NormalizationOperation.OperationEnumValue.MERGE,
        //            SourceField = "sourceField",
        //            DestinationField = "destinationField"
        //        }
        //    };

        //    DetailedResponse<ListConfigurationsResponse> response = Substitute.For<DetailedResponse<ListConfigurationsResponse>>();
        //    response.Result = Substitute.For<ListConfigurationsResponse>();
        //    response.Result.Configurations = Substitute.For<List<Configuration>>();
        //    response.Result.Configurations.Add(configuration);
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.As<ListConfigurationsResponse>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.ListConfigurations("environmentId");

        //    Assert.IsNotNull(result);
        //    client.Received().GetAsync(Arg.Any<string>());
        //    Assert.IsNotNull(result.Result.Configurations);
        //    Assert.IsTrue(result.Result.Configurations.Count > 0);
        //    Assert.IsTrue(result.Result.Configurations[0].Name == "name");
        //    Assert.IsTrue(result.Result.Configurations[0].Description == "description");
        //    Assert.IsNotNull(result.Result.Configurations[0].ConfigurationId);
        //    Assert.IsNotNull(result.Result.Configurations[0].Created);
        //    Assert.IsNotNull(result.Result.Configurations[0].Updated);
        //    Assert.IsNotNull(result.Result.Configurations[0].Enrichments);
        //    Assert.IsTrue(result.Result.Configurations[0].Enrichments.Count > 0);
        //    Assert.IsTrue(result.Result.Configurations[0].Enrichments[0].Description == "description");
        //    Assert.IsTrue(result.Result.Configurations[0].Enrichments[0].SourceField == "sourceField");
        //    Assert.IsTrue(result.Result.Configurations[0].Enrichments[0].Overwrite == false);
        //    Assert.IsTrue(result.Result.Configurations[0].Enrichments[0]._Enrichment == "enrichmentName");
        //    Assert.IsTrue(result.Result.Configurations[0].Enrichments[0].IgnoreDownstreamErrors == false);
        //    Assert.IsNotNull(result.Result.Configurations[0].Enrichments[0].Options);
        //}
        //#endregion

        //#region Create Configuration
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void CreateConfiguration_No_EnvironmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.CreateConfiguration(null, "configuration");
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void CreateConfiguration_No_Configuration()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.CreateConfiguration("environmentId", null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void CreateConfiguration_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;

        //    service.CreateConfiguration("environmentId", "configuration");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void CreateConfiguration_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";

        //    service.CreateConfiguration("environmentId", "configuration");
        //}

        //[TestMethod]
        //public void CreateConfiguration_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PostAsync(Arg.Any<string>())
        //        .Returns(request);

        //    DetailedResponse<Configuration> Configuration = new DetailedResponse<Configuration>()
        //    {
        //        Result = new Configuration()
        //        {
        //            Name = "name",
        //            Description = "description",
        //            Conversions = new Conversions()
        //            {
        //                Pdf = new PdfSettings()
        //                {
        //                    Heading = new PdfHeadingDetection()
        //                    {
        //                        Fonts = new List<FontSetting>()
        //                           {
        //                               new FontSetting()
        //                               {
        //                                   Level = 1,
        //                                   Bold = false,
        //                                   Italic = false,
        //                                   Name = "name"
        //                               }
        //                           }
        //                    }
        //                }
        //            },
        //            Enrichments = new List<Enrichment>()
        //        {
        //            new Enrichment()
        //            {
        //                Description = "description",
        //                DestinationField = "destinationField",
        //                SourceField = "sourceField",
        //                Overwrite = false,
        //                _Enrichment = "enrichmentName",
        //                IgnoreDownstreamErrors = false,
        //                Options = new EnrichmentOptions()
        //                {
        //                    Features = new NluEnrichmentFeatures() { }
        //                }
        //            }
        //        },
        //            Normalizations = new List<NormalizationOperation>()
        //        {
        //            new NormalizationOperation()
        //            {
        //                Operation = NormalizationOperation.OperationEnumValue.MERGE,
        //                SourceField = "sourceField",
        //                DestinationField = "destinationField"
        //            }
        //        }
        //        }
        //    };

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithBody<Configuration>(Arg.Any<Configuration>())
        //        .Returns(request);
        //    request.As<Configuration>()
        //        .Returns(Task.FromResult(Configuration));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.CreateConfiguration("environmentId", "name", "description");

        //    Assert.IsNotNull(result);
        //    client.Received().PostAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.Name == "name");
        //    Assert.IsTrue(result.Result.Description == "description");
        //}
        //#endregion

        //#region Delete Configuration
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void DeleteConfiguration_No_environmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.DeleteConfiguration(null, "configurationId");
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void DeleteConfiguration_No_ConfigurationId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.DeleteConfiguration("environmentId", null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void DeleteConfiguration_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;
        //    service.DeleteConfiguration("environmentId", "ConfigurationId");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void DeleteConfiguration_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.DeleteAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";

        //    service.DeleteConfiguration("environmentId", "configurationId");
        //}

        //[TestMethod]
        //public void DeleteConfiguration_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.DeleteAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    var notice = Substitute.For<Notice>();
        //    notice.NoticeId.Returns("noticeId");
        //    notice.Created.Returns(DateTime.MinValue);
        //    notice.DocumentId.Returns("documentId");
        //    notice.Step.Returns("step");
        //    notice.Description.Returns("description");
        //    notice.QueryId.Returns("queryId");
        //    notice.Severity = Notice.SeverityEnumValue.ERROR;

        //    DetailedResponse<DeleteConfigurationResponse> response = new DetailedResponse<DeleteConfigurationResponse>()
        //    {
        //        Result = new DeleteConfigurationResponse()
        //        {
        //            ConfigurationId = "ConfigurationId",
        //            Status = DeleteConfigurationResponse.StatusEnumValue.DELETED,
        //            Notices = new List<Notice>()
        //            {
        //                notice
        //            }
        //        }
        //    };
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.As<DeleteConfigurationResponse>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.DeleteConfiguration("environmentId", "ConfigurationId");

        //    Assert.IsNotNull(result);
        //    client.Received().DeleteAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.ConfigurationId == "ConfigurationId");
        //    Assert.IsTrue(result.Result.Status == DeleteConfigurationResponse.StatusEnumValue.DELETED);
        //    Assert.IsNotNull(result.Result.Notices);
        //    Assert.IsTrue(result.Result.Notices.Count > 0);
        //    Assert.IsTrue(result.Result.Notices[0].Severity == Notice.SeverityEnumValue.ERROR);
        //    Assert.IsNotNull(result.Result.Notices[0].NoticeId);
        //    Assert.IsNotNull(result.Result.Notices[0].Created);
        //    Assert.IsNotNull(result.Result.Notices[0].DocumentId);
        //    Assert.IsNotNull(result.Result.Notices[0].Step);
        //    Assert.IsNotNull(result.Result.Notices[0].Description);
        //}
        //#endregion

        //#region Get Configuration
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void GetConfiguration_No_EnvironmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.GetConfiguration(null, "configurationId");
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void GetConfiguration_No_ConfigurationId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.GetConfiguration("environmentId", null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void GetConfiguration_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;
        //    service.GetConfiguration("environmentId", "ConfigurationId");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void GetConfiguration_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.GetAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";
        //    service.GetConfiguration("environmentId", "ConfigurationId");
        //}

        //[TestMethod]
        //public void GetConfiguration_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.GetAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region Response
        //    DetailedResponse<Configuration> response = new DetailedResponse<Configuration>()
        //    {
        //        Result = new Configuration()
        //        {
        //            Name = "name",
        //            Description = "description",
        //            Conversions = new Conversions()
        //            {
        //                Pdf = new PdfSettings()
        //                {
        //                    Heading = new PdfHeadingDetection()
        //                    {
        //                        Fonts = new List<FontSetting>()
        //                           {
        //                               new FontSetting()
        //                               {
        //                                   Level = 1,
        //                                   Bold = false,
        //                                   Italic = false,
        //                                   Name = "name"
        //                               }
        //                           }
        //                    }
        //                }
        //            },
        //            Enrichments = new List<Enrichment>()
        //            {
        //                new Enrichment()
        //                {
        //                    Description = "description",
        //                    DestinationField = "destinationField",
        //                    SourceField = "sourceField",
        //                    Overwrite = false,
        //                    _Enrichment = "enrichmentName",
        //                    IgnoreDownstreamErrors = false,
        //                    Options = new EnrichmentOptions()
        //                    {
        //                        Features = new NluEnrichmentFeatures() { }
        //                    }
        //                }
        //            },
        //            Normalizations = new List<NormalizationOperation>()
        //            {
        //                new NormalizationOperation()
        //                {
        //                    Operation = NormalizationOperation.OperationEnumValue.MERGE,
        //                    SourceField = "sourceField",
        //                    DestinationField = "destinationField"
        //                }
        //            }
        //        }
        //    };
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.As<Configuration>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.GetConfiguration("environmentId", "ConfigurationId");

        //    Assert.IsNotNull(result);
        //    client.Received().GetAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.Name == "name");
        //    Assert.IsTrue(result.Result.Description == "description");
        //}
        //#endregion

        //#region Update Configuration
        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void UpdateConfiguration_No_EnvironmentId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.UpdateConfiguration(null, "configurationId", "configuration");
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void UpdateConfiguration_No_ConfigurationId()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.UpdateConfiguration("environmentId", null, "configuration");
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void UpdateConfiguration_No_Configuration()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.UpdateConfiguration("environmentId", "configurationId", null);
        //}

        //[TestMethod, ExpectedException(typeof(ArgumentNullException))]
        //public void UpdateConfiguration_No_VersionDate()
        //{
        //    BasicAuthenticator authenticator = new BasicAuthenticator("username", "password");
        //    DiscoveryService service = new DiscoveryService("versionDate", authenticator);
        //    service.VersionDate = null;

        //    service.UpdateConfiguration("environmentId", "ConfigurationId", "configuration");
        //}

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        //public void UpdateConfiguration_Catch_Exception()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PutAsync(Arg.Any<string>())
        //         .Returns(x =>
        //         {
        //             throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
        //                                                                       Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
        //                                                                       string.Empty));
        //         });

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "2017-11-07";

        //    service.UpdateConfiguration("environmentId", "ConfigurationId", "configuration");
        //}

        //[TestMethod]
        //public void UpdateConfiguration_Success()
        //{
        //    IClient client = CreateClient();

        //    IRequest request = Substitute.For<IRequest>();
        //    client.PutAsync(Arg.Any<string>())
        //        .Returns(request);

        //    #region response
        //    DetailedResponse<Configuration> response = new DetailedResponse<Configuration>()
        //    {
        //        Result = new Configuration()
        //        {
        //            Name = "name",
        //            Description = "description",
        //            Conversions = new Conversions()
        //            {
        //                Pdf = new PdfSettings()
        //                {
        //                    Heading = new PdfHeadingDetection()
        //                    {
        //                        Fonts = new List<FontSetting>()
        //                           {
        //                               new FontSetting()
        //                               {
        //                                   Level = 1,
        //                                   Bold = false,
        //                                   Italic = false,
        //                                   Name = "name"
        //                               }
        //                           }
        //                    }
        //                }
        //            },
        //            Enrichments = new List<Enrichment>()
        //            {
        //            new Enrichment()
        //                {
        //                    Description = "description",
        //                    DestinationField = "destinationField",
        //                    SourceField = "sourceField",
        //                    Overwrite = false,
        //                    _Enrichment = "enrichmentName",
        //                    IgnoreDownstreamErrors = false,
        //                    Options = new EnrichmentOptions()
        //                    {
        //                        Features = new NluEnrichmentFeatures() { }
        //                    }
        //                }
        //            },
        //            Normalizations = new List<NormalizationOperation>()
        //            {
        //                new NormalizationOperation()
        //                {
        //                    Operation = NormalizationOperation.OperationEnumValue.MERGE,
        //                    SourceField = "sourceField",
        //                    DestinationField = "destinationField"
        //                }
        //            }
        //        }
        //    };
        //    #endregion

        //    request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
        //        .Returns(request);
        //    request.WithBody<Configuration>(Arg.Any<Configuration>())
        //        .Returns(request);
        //    request.As<Configuration>()
        //        .Returns(Task.FromResult(response));

        //    DiscoveryService service = new DiscoveryService(client);
        //    service.VersionDate = "versionDate";

        //    var result = service.UpdateConfiguration("environmentId", "ConfigurationId", "configuration");

        //    Assert.IsNotNull(result);
        //    client.Received().PutAsync(Arg.Any<string>());
        //    Assert.IsTrue(result.Result.Name == "name");
        //    Assert.IsTrue(result.Result.Description == "description");
        //}
        //#endregion
        #endregion

        
    }
}
