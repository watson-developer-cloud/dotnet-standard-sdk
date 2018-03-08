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
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using System.Net.Http;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.UnitTests
{
    [TestClass]
    public class DiscoveryUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            DiscoveryService service =
                new DiscoveryService(null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            DiscoveryService service =
                new DiscoveryService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            DiscoveryService service =
                new DiscoveryService(null, "password", "2017-11-07");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Password_Null()
        {
            DiscoveryService service =
                new DiscoveryService("username", null, "2017-11-07");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Version_Null()
        {
            DiscoveryService service =
                new DiscoveryService("username", "password", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            DiscoveryService service =
                new DiscoveryService("username", "password", "2017-11-07");

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            DiscoveryService service =
                new DiscoveryService();

            Assert.IsNotNull(service);
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
        #region List Environments
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListEnvironments_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListEnvironments();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListEnvironments_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.ListEnvironments();
        }

        [TestMethod]
        public void ListEnvironments_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var memoryUsage = Substitute.For<MemoryUsage>();
            memoryUsage.PercentUsed.Returns(100);
            memoryUsage.Total.Returns("total");
            memoryUsage.TotalBytes.Returns(0);
            memoryUsage.Used.Returns("used");
            memoryUsage.UsedBytes.Returns(100);

            var diskUsage = Substitute.For<DiskUsage>();
            diskUsage.UsedBytes.Returns(0);
            diskUsage.MaximumAllowedBytes.Returns(0);
            diskUsage.TotalBytes.Returns(0);
            diskUsage.Used.Returns("used");
            diskUsage.Total.Returns("total");
            diskUsage.PercentUsed.Returns(0);

            ListEnvironmentsResponse response = new ListEnvironmentsResponse()
            {
                Environments = new List<Model.Environment>()
               {
                   new Model.Environment()
                   {
                       Status = Model.Environment.StatusEnum.PENDING,
                       Name = "name",
                       Description = "description",
                       Size = 1,
                       IndexCapacity = new IndexCapacity()
                       {
                           DiskUsage = diskUsage,
                           MemoryUsage = memoryUsage
                       }
                   }
               }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ListEnvironmentsResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.ListEnvironments();

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Environments != null);
            Assert.IsTrue(result.Environments.Count > 0);
            Assert.IsTrue(result.Environments[0].Status == Model.Environment.StatusEnum.PENDING);
            Assert.IsTrue(result.Environments[0].Name == "name");
            Assert.IsTrue(result.Environments[0].Description == "description");
            Assert.IsTrue(result.Environments[0].Size == 1);
            Assert.IsTrue(result.Environments[0].IndexCapacity.DiskUsage.PercentUsed == 0);
            Assert.IsTrue(result.Environments[0].IndexCapacity.DiskUsage.Total == "total");
            Assert.IsTrue(result.Environments[0].IndexCapacity.DiskUsage.Used == "used");
            Assert.IsTrue(result.Environments[0].IndexCapacity.DiskUsage.TotalBytes == 0);
            Assert.IsTrue(result.Environments[0].IndexCapacity.DiskUsage.MaximumAllowedBytes == 0);
            Assert.IsTrue(result.Environments[0].IndexCapacity.DiskUsage.UsedBytes == 0);

            Assert.IsTrue(result.Environments[0].IndexCapacity.MemoryUsage.PercentUsed == 100);
            Assert.IsTrue(result.Environments[0].IndexCapacity.MemoryUsage.Total == "total");
            Assert.IsTrue(result.Environments[0].IndexCapacity.MemoryUsage.Used == "used");
            Assert.IsTrue(result.Environments[0].IndexCapacity.MemoryUsage.TotalBytes == 0);
            Assert.IsTrue(result.Environments[0].IndexCapacity.MemoryUsage.UsedBytes == 100);
        }
        #endregion

        #region Create Environment
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateEnvironment_No_Environment()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateEnvironment(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateEnvironment_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            CreateEnvironmentRequest environment = new CreateEnvironmentRequest()
            {
                Name = "name",
                Description = "description",
                Size = 1
            };

            service.CreateEnvironment(environment);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateEnvironment_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            CreateEnvironmentRequest environment = new CreateEnvironmentRequest()
            {
                Name = "name",
                Description = "description",
                Size = 1
            };
            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.CreateEnvironment(environment);
        }

        [TestMethod]
        public void CreateEnvironment_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            Model.Environment response = new Model.Environment()
            {
                Status = Model.Environment.StatusEnum.PENDING,
                Name = "name",
                Description = "description",
                Size = 1,
                IndexCapacity = new IndexCapacity()
                {
                    DiskUsage = new DiskUsage() { },
                    MemoryUsage = new MemoryUsage() { }
                }
            };
            #endregion

            CreateEnvironmentRequest environment = new CreateEnvironmentRequest()
            {
                Name = "name",
                Description = "description",
                Size = 1
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateEnvironmentRequest>(environment)
                .Returns(request);
            request.As<Model.Environment>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateEnvironment(environment);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.Size == 1);
        }
        #endregion

        #region Delete Environment
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteEnvironment_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteEnvironment(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteEnvironment_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteEnvironment("environmentId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteEnvironment_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.DeleteEnvironment("environmentId");
        }

        [TestMethod]
        public void DeleteEnvironment_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DeleteEnvironmentResponse response = new DeleteEnvironmentResponse()
            {
                EnvironmentId = "environmentId",
                Status = DeleteEnvironmentResponse.StatusEnum.DELETED
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<DeleteEnvironmentResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteEnvironment("environmentId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
            Assert.IsTrue(result.EnvironmentId == "environmentId");
            Assert.IsTrue(result.Status == DeleteEnvironmentResponse.StatusEnum.DELETED);
        }
        #endregion

        #region Get Envronment
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEnvironment_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetEnvironment(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEnvironment_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetEnvironment("environmentId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetEnvironment_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.GetEnvironment("EnvironmentId");
        }

        [TestMethod]
        public void GetEnvironment_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            Model.Environment response = new Model.Environment()
            {
                Status = Model.Environment.StatusEnum.PENDING,
                Name = "name",
                Description = "description",
                Size = 1,
                IndexCapacity = new IndexCapacity()
                {
                    DiskUsage = new DiskUsage() { },
                    MemoryUsage = new MemoryUsage() { }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<Model.Environment>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.GetEnvironment("environmentId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == Model.Environment.StatusEnum.PENDING);
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.Size == 1);
        }
        #endregion

        #region Update Environment
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEnvironment_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            UpdateEnvironmentRequest environment = new UpdateEnvironmentRequest()
            {
                Name = "name",
                Description = "description"
            };

            service.UpdateEnvironment(null, environment);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEnvironment_No_Body()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateEnvironment("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEnvironment_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            UpdateEnvironmentRequest environment = new UpdateEnvironmentRequest()
            {
                Name = "name",
                Description = "description"
            };

            service.UpdateEnvironment("environmentId", environment);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateEnvironment_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            UpdateEnvironmentRequest environment = new UpdateEnvironmentRequest()
            {
                Name = "name",
                Description = "description"
            };
            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.UpdateEnvironment("environmentId", environment);
        }

        [TestMethod]
        public void UpdateEnvironment_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            Model.Environment response = new Model.Environment()
            {
                Status = Model.Environment.StatusEnum.PENDING,
                Name = "name",
                Description = "description",
                Size = 1,
                IndexCapacity = new IndexCapacity()
                {
                    DiskUsage = new DiskUsage() { },
                    MemoryUsage = new MemoryUsage() { }
                }
            };
            #endregion

            UpdateEnvironmentRequest environment = new UpdateEnvironmentRequest()
            {
                Name = "name",
                Description = "description"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateEnvironmentRequest>(environment)
                .Returns(request);
            request.As<Model.Environment>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateEnvironment("environmentId", environment);

            Assert.IsNotNull(result);
            client.Received().PutAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == Model.Environment.StatusEnum.PENDING);
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.Size == 1);
        }
        #endregion
        #endregion

        #region Preview Environment
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConfigurationInEnvronment_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.TestConfigurationInEnvironment(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConfigurationInEnvronment_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.TestConfigurationInEnvironment("envronmentId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void TestConfigurationInEnvronment_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.TestConfigurationInEnvironment("environmentId");
        }

        [TestMethod]
        public void TestConfigurationInEnvronment_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            var response = Substitute.For<TestDocument>();
            response.ConfigurationId.Returns("configurationId");
            response.Status.Returns("status");
            response.EnrichedFieldUnits.Returns(1);
            response.OriginalMediaType.Returns("originalMediaType");
            response.Snapshots = new List<DocumentSnapshot>()
            {
                new DocumentSnapshot()
                {
                    Step = DocumentSnapshot.StepEnum.HTML_INPUT,
                    Snapshot = new object() { }
                }
            };
            response.Notices = new List<Notice>()
            {
                notice
            };

            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<TestDocument>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.TestConfigurationInEnvironment("environmentId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Snapshots);
            Assert.IsTrue(result.Snapshots.Count > 0);
            Assert.IsTrue(result.Snapshots[0].Step == DocumentSnapshot.StepEnum.HTML_INPUT);
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
            Assert.IsNotNull(result.Notices[0].QueryId);
            Assert.IsNotNull(result.ConfigurationId);
            Assert.IsNotNull(result.Status);
            Assert.IsNotNull(result.EnrichedFieldUnits);
            Assert.IsNotNull(result.OriginalMediaType);
        }

        [TestMethod]
        public void TestConfigurationInEnvronment_Success_WithConfiguration()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            TestDocument response = new TestDocument()
            {
                Snapshots = new List<DocumentSnapshot>()
                {
                    new DocumentSnapshot()
                    {
                        Step = DocumentSnapshot.StepEnum.HTML_INPUT,
                        Snapshot = new object() { }
                    }
                },
                Notices = new List<Notice>()
                {
                    notice
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<TestDocument>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.TestConfigurationInEnvironment("environmentId", configuration: "configuration");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Snapshots);
            Assert.IsTrue(result.Snapshots.Count > 0);
            Assert.IsTrue(result.Snapshots[0].Step == DocumentSnapshot.StepEnum.HTML_INPUT);
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
        }

        [TestMethod]
        public void TestConfigurationInEnvronment_Success_WithFile()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            TestDocument response = new TestDocument()
            {
                Snapshots = new List<DocumentSnapshot>()
                {
                    new DocumentSnapshot()
                    {
                        Step = DocumentSnapshot.StepEnum.HTML_INPUT,
                        Snapshot = new object() { }
                    }
                },
                Notices = new List<Notice>()
                {
                    notice
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<TestDocument>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";
            var result = service.TestConfigurationInEnvironment("environmentId", file: Substitute.For<FileStream>("any_file", FileMode.Create));
            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Snapshots);
            Assert.IsTrue(result.Snapshots.Count > 0);
            Assert.IsTrue(result.Snapshots[0].Step == DocumentSnapshot.StepEnum.HTML_INPUT);
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
        }

        [TestMethod]
        public void TestConfigurationInEnvronment_Success_WithMetadata()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            TestDocument response = new TestDocument()
            {
                Snapshots = new List<DocumentSnapshot>()
                {
                    new DocumentSnapshot()
                    {
                        Step = DocumentSnapshot.StepEnum.HTML_INPUT,
                        Snapshot = new object() { }
                    }
                },
                Notices = new List<Notice>()
                {
                    notice
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<TestDocument>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.TestConfigurationInEnvironment("environmentId", metadata: "metadata");
            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Snapshots);
            Assert.IsTrue(result.Snapshots.Count > 0);
            Assert.IsTrue(result.Snapshots[0].Step == DocumentSnapshot.StepEnum.HTML_INPUT);
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
        }
        #endregion

        #region Configrations
        #region List Configurations
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListConfigurations_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.ListConfigurations(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListConfigurations_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListConfigurations("environmentId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListConfigurations_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.ListConfigurations("environmentId");
        }

        [TestMethod]
        public void ListConfigurations_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var configuration = Substitute.For<Configuration>();
            configuration.ConfigurationId.Returns("configurationId");
            configuration.Created.Returns(DateTime.MinValue);
            configuration.Updated.Returns(DateTime.MinValue);
            configuration.Name = "name";
            configuration.Description = "description";
            configuration.Conversions = new Conversions()
            {
                Pdf = new PdfSettings()
                {
                    Heading = new PdfHeadingDetection()
                    {
                        Fonts = new List<FontSetting>()
                        {
                            new FontSetting()
                            {
                                Level = 1,
                                MinSize = 1,
                                MaxSize = 1,
                                Bold = false,
                                Italic = false,
                                Name = "name"
                            }
                        }
                    }
                },
                Word = new WordSettings()
                {
                    Heading = new WordHeadingDetection()
                    {
                        Fonts = new List<FontSetting>()
                        {
                            new FontSetting()
                            {
                                Level = 1,
                                MinSize = 1,
                                MaxSize = 1,
                                Bold = false,
                                Italic = false,
                                Name = "name"
                            }
                        },
                        Styles = new List<WordStyle>()
                        {
                            new WordStyle()
                            {
                                Level = 1,
                                Names = new List<string>
                                {
                                    "name"
                                }
                            }
                        }
                    }
                },
                Html = new HtmlSettings()
                {
                    ExcludeTagsCompletely = new List<string>()
                    {
                        "exclude"
                    },
                    ExcludeTagsKeepContent = new List<string>()
                    {
                        "exclude but keep content"
                    },
                    KeepContent = new XPathPatterns()
                    {
                        Xpaths = new List<string>()
                        {
                            "keepContent"
                        }
                    },
                    ExcludeContent = new XPathPatterns()
                    {
                        Xpaths = new List<string>()
                        {
                            "excludeContent"
                        }
                    },
                    KeepTagAttributes = new List<string>()
                    {
                        "keepTagAttributes"
                    },
                    ExcludeTagAttributes = new List<string>()
                    {
                        "excludeTagAttributes"
                    }
                },
                JsonNormalizations = new List<NormalizationOperation>()
                {
                    new NormalizationOperation()
                    {

                    }
                },

            };
            configuration.Enrichments = new List<Enrichment>()
            {
                new Enrichment()
                {
                    Description = "description",
                    DestinationField = "destinationField",
                    SourceField = "sourceField",
                    Overwrite = false,
                    EnrichmentName = "enrichmentName",
                    IgnoreDownstreamErrors = false,
                    Options = new EnrichmentOptions()
                    {
                        Features = new NluEnrichmentFeatures()
                        {
                        }
                    }
                }
            };
            configuration.Normalizations = new List<NormalizationOperation>()
            {
                new NormalizationOperation()
                {
                    Operation = NormalizationOperation.OperationEnum.MERGE,
                    SourceField = "sourceField",
                    DestinationField = "destinationField"
                }
            };

            ListConfigurationsResponse response = new ListConfigurationsResponse()
            {
                Configurations = new List<Configuration>()
               {
                   configuration
               }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ListConfigurationsResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.ListConfigurations("environmentId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Configurations);
            Assert.IsTrue(result.Configurations.Count > 0);
            Assert.IsTrue(result.Configurations[0].Name == "name");
            Assert.IsTrue(result.Configurations[0].Description == "description");
            Assert.IsNotNull(result.Configurations[0].ConfigurationId);
            Assert.IsNotNull(result.Configurations[0].Created);
            Assert.IsNotNull(result.Configurations[0].Updated);
            Assert.IsNotNull(result.Configurations[0].Enrichments);
            Assert.IsTrue(result.Configurations[0].Enrichments.Count > 0);
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Description == "description");
            Assert.IsTrue(result.Configurations[0].Enrichments[0].SourceField == "sourceField");
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Overwrite == false);
            Assert.IsTrue(result.Configurations[0].Enrichments[0].EnrichmentName == "enrichmentName");
            Assert.IsTrue(result.Configurations[0].Enrichments[0].IgnoreDownstreamErrors == false);
            Assert.IsNotNull(result.Configurations[0].Enrichments[0].Options);
        }
        #endregion

        #region Create Configuration
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateConfiguration_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateConfiguration(null, new Configuration());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateConfiguration_No_Configuration()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateConfiguration("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateConfiguration_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.CreateConfiguration("environmentId", new Configuration());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateConfiguration_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.CreateConfiguration("environmentId", new Configuration());
        }

        [TestMethod]
        public void CreateConfiguration_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            Configuration Configuration = new Configuration()
            {
                Name = "name",
                Description = "description",
                Conversions = new Conversions()
                {
                    Pdf = new PdfSettings()
                    {
                        Heading = new PdfHeadingDetection()
                        {
                            Fonts = new List<FontSetting>()
                                   {
                                       new FontSetting()
                                       {
                                           Level = 1,
                                           MinSize = 1,
                                           MaxSize = 1,
                                           Bold = false,
                                           Italic = false,
                                           Name = "name"
                                       }
                                   }
                        }
                    }
                },
                Enrichments = new List<Enrichment>()
                {
                    new Enrichment()
                    {
                        Description = "description",
                        DestinationField = "destinationField",
                        SourceField = "sourceField",
                        Overwrite = false,
                        EnrichmentName = "enrichmentName",
                        IgnoreDownstreamErrors = false,
                        Options = new EnrichmentOptions()
                        {
                            Features = new NluEnrichmentFeatures() { }
                        }
                    }
                },
                Normalizations = new List<NormalizationOperation>()
                {
                    new NormalizationOperation()
                    {
                        Operation = NormalizationOperation.OperationEnum.MERGE,
                        SourceField = "sourceField",
                        DestinationField = "destinationField"
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<Configuration>(Arg.Any<Configuration>())
                .Returns(request);
            request.As<Configuration>()
                .Returns(Task.FromResult(Configuration));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateConfiguration("environmentId", Configuration);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
        }
        #endregion

        #region Delete Configuration
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteConfiguration_No_environmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteConfiguration(null, "configurationId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteConfiguration_No_ConfigurationId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteConfiguration("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteConfiguration_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteConfiguration("environmentId", "ConfigurationId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteConfiguration_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.DeleteConfiguration("environmentId", "configurationId");
        }

        [TestMethod]
        public void DeleteConfiguration_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            DeleteConfigurationResponse response = new DeleteConfigurationResponse()
            {
                ConfigurationId = "ConfigurationId",
                Status = DeleteConfigurationResponse.StatusEnum.DELETED,
                Notices = new List<Notice>()
                {
                    notice
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<DeleteConfigurationResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteConfiguration("environmentId", "ConfigurationId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
            Assert.IsTrue(result.ConfigurationId == "ConfigurationId");
            Assert.IsTrue(result.Status == DeleteConfigurationResponse.StatusEnum.DELETED);
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
        }
        #endregion

        #region Get Configuration
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetConfiguration_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetConfiguration(null, "configurationId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetConfiguration_No_ConfigurationId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetConfiguration("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetConfiguration_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetConfiguration("environmentId", "ConfigurationId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetConfiguration_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.GetConfiguration("environmentId", "ConfigurationId");
        }

        [TestMethod]
        public void GetConfiguration_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            Configuration response = new Configuration()
            {
                Name = "name",
                Description = "description",
                Conversions = new Conversions()
                {
                    Pdf = new PdfSettings()
                    {
                        Heading = new PdfHeadingDetection()
                        {
                            Fonts = new List<FontSetting>()
                                   {
                                       new FontSetting()
                                       {
                                           Level = 1,
                                           MinSize = 1,
                                           MaxSize = 1,
                                           Bold = false,
                                           Italic = false,
                                           Name = "name"
                                       }
                                   }
                        }
                    }
                },
                Enrichments = new List<Enrichment>()
                {
                    new Enrichment()
                    {
                        Description = "description",
                        DestinationField = "destinationField",
                        SourceField = "sourceField",
                        Overwrite = false,
                        EnrichmentName = "enrichmentName",
                        IgnoreDownstreamErrors = false,
                        Options = new EnrichmentOptions()
                        {
                            Features = new NluEnrichmentFeatures() { }
                        }
                    }
                },
                Normalizations = new List<NormalizationOperation>()
                {
                    new NormalizationOperation()
                    {
                        Operation = NormalizationOperation.OperationEnum.MERGE,
                        SourceField = "sourceField",
                        DestinationField = "destinationField"
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<Configuration>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.GetConfiguration("environmentId", "ConfigurationId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
        }
        #endregion

        #region Update Configuration
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateConfiguration_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateConfiguration(null, "configurationId", new Configuration());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateConfiguration_No_ConfigurationId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateConfiguration("environmentId", null, new Configuration());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateConfiguration_No_Configuration()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateConfiguration("environmentId", "configurationId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateConfiguration_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.UpdateConfiguration("environmentId", "ConfigurationId", new Configuration());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateConfiguration_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.UpdateConfiguration("environmentId", "ConfigurationId", new Configuration());
        }

        [TestMethod]
        public void UpdateConfiguration_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            #region response
            Configuration response = new Configuration()
            {
                Name = "name",
                Description = "description",
                Conversions = new Conversions()
                {
                    Pdf = new PdfSettings()
                    {
                        Heading = new PdfHeadingDetection()
                        {
                            Fonts = new List<FontSetting>()
                                   {
                                       new FontSetting()
                                       {
                                           Level = 1,
                                           MinSize = 1,
                                           MaxSize = 1,
                                           Bold = false,
                                           Italic = false,
                                           Name = "name"
                                       }
                                   }
                        }
                    }
                },
                Enrichments = new List<Enrichment>()
                {
                    new Enrichment()
                    {
                        Description = "description",
                        DestinationField = "destinationField",
                        SourceField = "sourceField",
                        Overwrite = false,
                        EnrichmentName = "enrichmentName",
                        IgnoreDownstreamErrors = false,
                        Options = new EnrichmentOptions()
                        {
                            Features = new NluEnrichmentFeatures() { }
                        }
                    }
                },
                Normalizations = new List<NormalizationOperation>()
                {
                    new NormalizationOperation()
                    {
                        Operation = NormalizationOperation.OperationEnum.MERGE,
                        SourceField = "sourceField",
                        DestinationField = "destinationField"
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<Configuration>(Arg.Any<Configuration>())
                .Returns(request);
            request.As<Configuration>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateConfiguration("environmentId", "ConfigurationId", new Configuration());

            Assert.IsNotNull(result);
            client.Received().PutAsync(Arg.Any<string>());
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
        }
        #endregion
        #endregion

        #region Collections
        #region List Collections
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCollections_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.ListCollections(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCollections_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListCollections("environmentId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListCollections_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.ListCollections("environmentId");
        }

        [TestMethod]
        public void ListCollections_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            ListCollectionsResponse response = new ListCollectionsResponse()
            {
                Collections = new List<Collection>()
               {
                   new Collection()
                   {
                       Status = Collection.StatusEnum.PENDING,
                       Name = "name",
                       Description = "description",
                       ConfigurationId = "configurationId",
                       Language = "language",
                       DocumentCounts = new DocumentCounts() {}
                   }
               }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ListCollectionsResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.ListCollections("environmentId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Collections);
            Assert.IsTrue(result.Collections.Count > 0);
            Assert.IsTrue(result.Collections[0].Name == "name");
            Assert.IsTrue(result.Collections[0].Description == "description");
            Assert.IsTrue(result.Collections[0].ConfigurationId == "configurationId");
            Assert.IsTrue(result.Collections[0].Language == "language");
        }
        #endregion

        #region Create Collection
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCollection_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateCollection(null, new CreateCollectionRequest());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCollection_No_Body()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateCollection("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCollection_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.CreateCollection("environmentId", new CreateCollectionRequest());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateCollection_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.CreateCollection("environmentId", new CreateCollectionRequest());
        }

        [TestMethod]
        public void CreateCollection_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            Collection Collection = new Collection()
            {
                Name = "name",
                Description = "description",
                Status = Collection.StatusEnum.PENDING,
                ConfigurationId = "configurationId",
                Language = "language",
                DocumentCounts = new DocumentCounts() { }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateCollectionRequest>(Arg.Any<CreateCollectionRequest>())
                .Returns(request);
            request.As<Collection>()
                .Returns(Task.FromResult(Collection));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.CreateCollection("environmentId", new CreateCollectionRequest());

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.Status == Collection.StatusEnum.PENDING);
            Assert.IsTrue(result.ConfigurationId == "configurationId");
            Assert.IsTrue(result.Language == "language");
        }
        #endregion

        #region Delete Collection
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCollection_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteCollection(null, "collectionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCollection_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteCollection("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCollection_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteCollection("environmentId", "collectionId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteCollection_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.DeleteCollection("environmentId", "collectionId");
        }

        [TestMethod]
        public void DeleteCollection_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DeleteCollectionResponse response = new DeleteCollectionResponse()
            {
                CollectionId = "collectionId",
                Status = DeleteCollectionResponse.StatusEnum.DELETED
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<DeleteCollectionResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteCollection("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
            Assert.IsTrue(result.CollectionId == "collectionId");
            Assert.IsTrue(result.Status == DeleteCollectionResponse.StatusEnum.DELETED);
        }
        #endregion

        #region Get Collection
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollection_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetCollection(null, "collectionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollection_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetCollection("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCollection_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetCollection("environmentId", "collectionId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetCollection_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.GetCollection("environmentId", "collectionId");
        }

        [TestMethod]
        public void GetCollection_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            Collection response = new Collection()
            {
                Status = Collection.StatusEnum.PENDING,
                Name = "name",
                Description = "description",
                ConfigurationId = "configurationId",
                Language = "language",
                DocumentCounts = new DocumentCounts() { }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<Collection>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.GetCollection("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.ConfigurationId == "configurationId");
            Assert.IsTrue(result.Language == "language");
        }
        #endregion

        #region Update Collection
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCollection_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateCollection(null, "collectionId", new UpdateCollectionRequest());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCollection_No_collectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateCollection("environmentId", null, new UpdateCollectionRequest());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCollection_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.UpdateCollection("environmentId", "collectionId", new UpdateCollectionRequest());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateCollection_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.UpdateCollection("environmentId", "collectionId", new UpdateCollectionRequest());
        }

        [TestMethod]
        public void UpdateCollection_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            #region response
            var documentCounts = Substitute.For<DocumentCounts>();
            documentCounts.Available.Returns(0);
            documentCounts.Processing.Returns(0);
            documentCounts.Failed.Returns(0);

            var response = Substitute.For<Collection>();
            response.Status = Collection.StatusEnum.PENDING;
            response.Name = "name";
            response.Description = "description";
            response.ConfigurationId = "configurationId";
            response.Language = "language";
            response.DocumentCounts = documentCounts;
            response.CollectionId.Returns("collectionId");
            response.Created.Returns(DateTime.MinValue);
            response.Updated.Returns(DateTime.MinValue);

            #endregion

            UpdateCollectionRequest updateCollectionRequest = new UpdateCollectionRequest()
            {
                Name = "name",
                Description = "description",
                ConfigurationId = "configurationId"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<UpdateCollectionRequest>(Arg.Any<UpdateCollectionRequest>())
                .Returns(request);
            request.As<Collection>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateCollection("environmentId", "collectionId", updateCollectionRequest);

            Assert.IsNotNull(result);
            client.Received().PutAsync(Arg.Any<string>());
            Assert.IsTrue(result.Name == "name");
            Assert.IsTrue(result.Description == "description");
            Assert.IsTrue(result.ConfigurationId == "configurationId");
            Assert.IsTrue(result.Language == "language");
            Assert.IsTrue(result.CollectionId == "collectionId");
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
            Assert.IsNotNull(result.DocumentCounts.Available);
            Assert.IsNotNull(result.DocumentCounts.Processing);
            Assert.IsNotNull(result.DocumentCounts.Failed);
        }
        #endregion

        #region List Collection Fields
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCollectionFields_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.ListCollectionFields(null, "collectionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCollectionFields_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.ListCollectionFields("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCollectionFields_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListCollectionFields("environmentId", "collectionId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListCollectionFields_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.ListCollectionFields("environmentId", "collectionId");
        }

        [TestMethod]
        public void ListCollectionFields_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var field = Substitute.For<Field>();
            field.FieldName.Returns("field");
            field.FieldType = Field.FieldTypeEnum.STRING;

            ListCollectionFieldsResponse response = new ListCollectionFieldsResponse()
            {
                Fields = new List<Field>()
                {
                    field
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ListCollectionFieldsResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.ListCollectionFields("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Fields);
            Assert.IsTrue(result.Fields.Count > 0);
            Assert.IsTrue(result.Fields[0].FieldType == Field.FieldTypeEnum.STRING);
            Assert.IsTrue(result.Fields[0].FieldName == "field");
        }
        #endregion
        #endregion

        #region Documents
        #region Add Document
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddDocument_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.AddDocument(null, "collectionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddDocument_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.AddDocument("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddDocument_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.AddDocument("environmentId", "collectionId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void AddDocument_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.AddDocument("environmentId", "collectionId");
        }

        [TestMethod]
        public void AddDocument_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            DocumentAccepted documentAccepted = new DocumentAccepted()
            {
                Status = DocumentAccepted.StatusEnum.PROCESSING,
                DocumentId = "documentId",
                Notices = new List<Notice>()
                {
                    notice
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<DocumentAccepted>()
                .Returns(Task.FromResult(documentAccepted));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.AddDocument("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == DocumentAccepted.StatusEnum.PROCESSING);
            Assert.IsTrue(result.DocumentId == "documentId");
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
        }

        [TestMethod]
        public void AddDocument_Success_WithConfiguration()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            DocumentAccepted documentAccepted = new DocumentAccepted()
            {
                Status = DocumentAccepted.StatusEnum.PROCESSING,
                DocumentId = "documentId",
                Notices = new List<Notice>()
                {
                    notice
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<DocumentAccepted>()
                .Returns(Task.FromResult(documentAccepted));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.AddDocument("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == DocumentAccepted.StatusEnum.PROCESSING);
            Assert.IsTrue(result.DocumentId == "documentId");
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
        }
        #endregion

        #region Delete Document
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteDocument_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteDocument(null, "collectionId", "doucmentId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteDocument_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteDocument("environmentId", null, "documentId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteDocument_No_DocumentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteDocument("environmentId", "collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteDocument_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteDocument("environmentId", "collectionId", "doucmentId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteDocument_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.DeleteDocument("environmentId", "collectionId", "doucmentId");
        }

        [TestMethod]
        public void DeleteDocument_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DeleteDocumentResponse response = new DeleteDocumentResponse()
            {
                DocumentId = "doucmentId",
                Status = DeleteDocumentResponse.StatusEnum.DELETED
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<DeleteDocumentResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteDocument("environmentId", "collectionId", "doucmentId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
            Assert.IsTrue(result.DocumentId == "doucmentId");
            Assert.IsTrue(result.Status == DeleteDocumentResponse.StatusEnum.DELETED);
        }
        #endregion

        #region Get Document
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetDocument_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetDocumentStatus(null, "collectionId", "documentId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetDocument_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetDocumentStatus("environmentId", null, "documentId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetDocument_No_DocumentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetDocumentStatus("environmentId", "collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetDocument_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetDocumentStatus("environmentId", "collectionId", "documentId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetDocument_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.GetDocumentStatus("environmentId", "collectionId", "documentId");
        }

        [TestMethod]
        public void GetDocument_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            //DocumentStatus response = new DocumentStatus()
            //{
            //    Status = DocumentStatus.StatusEnum.AVAILABLE,
            //    ConfigurationId = "",
            //    Notices = new List<Notice>()
            //    {
            //        notice
            //    }
            //};

            var response = Substitute.For<DocumentStatus>();
            response.Status = DocumentStatus.StatusEnum.AVAILABLE;
            response.FileType = DocumentStatus.FileTypeEnum.HTML;
            response.Filename = "fileName";
            response.Sha1 = "sha1";
            response.Notices = new List<Notice>()
            {
                notice
            };
            response.DocumentId.Returns("documentId");
            response.ConfigurationId.Returns("configurationId");
            response.Created.Returns(DateTime.MinValue);
            response.Updated.Returns(DateTime.MinValue);
            response.StatusDescription.Returns("statusDescription");
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<DocumentStatus>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.GetDocumentStatus("environmentId", "collectionId", "documentId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == DocumentStatus.StatusEnum.AVAILABLE);
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
            Assert.IsNotNull(result.DocumentId);
            Assert.IsNotNull(result.ConfigurationId);
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
            Assert.IsNotNull(result.StatusDescription);
        }
        #endregion

        #region Update Document
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateDocument_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateDocument(null, "collectionId", "documentId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateDocument_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateDocument("environmentId", null, "documentId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateDocument_No_DocumentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateDocument("environmentId", "collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateDocument_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.UpdateDocument("environmentId", "collectionId", "documentId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateDocument_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.UpdateDocument("environmentId", "collectionId", "documentId");
        }

        [TestMethod]
        public void UpdateDocument_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            DocumentAccepted documentAccepted = new DocumentAccepted()
            {
                Status = DocumentAccepted.StatusEnum.PROCESSING,
                DocumentId = "documentId",
                Notices = new List<Notice>()
                {
                    notice
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<DocumentAccepted>()
                .Returns(Task.FromResult(documentAccepted));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateDocument("environmentId", "collectionId", "documentId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == DocumentAccepted.StatusEnum.PROCESSING);
            Assert.IsTrue(result.DocumentId == "documentId");
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
        }

        [TestMethod]
        public void UpdateDocument_Success_WithConfiguration()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            var notice = Substitute.For<Notice>();
            notice.NoticeId.Returns("noticeId");
            notice.Created.Returns(DateTime.MinValue);
            notice.DocumentId.Returns("documentId");
            notice.Step.Returns("step");
            notice.Description.Returns("description");
            notice.QueryId.Returns("queryId");
            notice.Severity = Notice.SeverityEnum.ERROR;

            DocumentAccepted documentAccepted = new DocumentAccepted()
            {
                Status = DocumentAccepted.StatusEnum.PROCESSING,
                DocumentId = "documentId",
                Notices = new List<Notice>()
                {
                    notice
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<DocumentAccepted>()
                .Returns(Task.FromResult(documentAccepted));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateDocument("environmentId", "collectionId", "documentId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == DocumentAccepted.StatusEnum.PROCESSING);
            Assert.IsTrue(result.DocumentId == "documentId");
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNotNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNotNull(result.Notices[0].DocumentId);
            Assert.IsNotNull(result.Notices[0].Step);
            Assert.IsNotNull(result.Notices[0].Description);
        }
        #endregion
        #endregion

        #region Query
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Query_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.Query(null, "collectionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Query_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.Query("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Query_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.Query("environmentId", "collectionId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Query_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.Query("environmentId", "collectionId");
        }

        [TestMethod]
        public void Query_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            QueryResponse response = new QueryResponse()
            {
                MatchingResults = 1,
                Results = new List<QueryResult>()
                {
                    new QueryResult()
                    {
                        Id = "id",
                        Score = 1.0,
                        Metadata = new object() { }
                    }
                },
                Aggregations = new List<QueryAggregation>()
                {
                    new QueryAggregation()
                    {
                        Type = "type",
                        Field = "field",
                        Results = new List<AggregationResult>()
                        {
                            new AggregationResult()
                            {
                                Key = "key",
                                MatchingResults = 1,
                                Aggregations = new List<QueryAggregation>()
                                {
                                    new QueryAggregation()
                                    {

                                    }
                                }
                            }
                        },
                        Match = "match",
                        MatchingResults = 1,
                        Aggregations = new List<QueryAggregation>()
                        {
                            new QueryAggregation()
                            {

                            }
                        }
                    }
                }
            };
            #endregion





            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<List<string>>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<List<string>>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<List<string>>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<QueryResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.Query("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
        }
        #endregion

        #region Notices
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Notices_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.QueryNotices(null, "collectionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Notices_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.QueryNotices("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Notices_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.QueryNotices("environmentId", "collectionId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Notices_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.QueryNotices("environmentId", "collectionId");
        }

        [TestMethod]
        public void Notices_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            QueryNoticesResponse response = new QueryNoticesResponse()
            {
                MatchingResults = 1,
                Results = new List<QueryNoticesResult>()
                {
                    new QueryNoticesResult()
                    {
                        Id = "id",
                        Score = 1.0,
                        Metadata = new object() { }
                    }
                },
                Aggregations = new List<QueryAggregation>()
                {
                    new QueryAggregation()
                    {
                        Type = "type",
                        Field = "field",
                        Results = new List<AggregationResult>()
                        {
                            new AggregationResult()
                            {
                                Key = "key",
                                MatchingResults = 1,
                                Aggregations = new List<QueryAggregation>()
                                {
                                    new QueryAggregation()
                                    {

                                    }
                                }
                            }
                        },
                        Match = "match",
                        MatchingResults = 1,
                        Aggregations = new List<QueryAggregation>()
                        {
                            new QueryAggregation()
                            {

                            }
                        }
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<QueryNoticesResponse>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.QueryNotices("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Results);
            Assert.IsTrue(result.Results.Count > 0);
            Assert.IsNotNull(result.Results[0].Id);
            Assert.IsNotNull(result.Results[0].Score);
            Assert.IsNotNull(result.Results[0].Metadata);
        }
        #endregion

        #region Training Data
        #region Delete Training Data
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteAllTrainingData_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteAllTrainingData(null, "collectionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteAllTrainingData_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteAllTrainingData("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteAllTrainingData_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteAllTrainingData("environmentId", "collectionId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteAllTrainingData_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.DeleteAllTrainingData("environmentId", "collectionId");
        }

        [TestMethod]
        public void DeleteAllTrainingData_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteAllTrainingData("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region List Training Data
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListTrainingData_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.ListTrainingData(null, "collectionId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListTrainingData_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.ListTrainingData("environmentId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListTrainingData_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.ListTrainingData("environmentId", "collectionId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListTrainingData_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.ListTrainingData("environmentId", "collectionId");
        }

        [TestMethod]
        public void ListTrainingData_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = new TrainingDataSet()
            {
                EnvironmentId = "environmentId",
                CollectionId = "collectionId",
                Queries = new List<TrainingQuery>()
                {
                    new TrainingQuery()
                    {
                        QueryId = "queryId",
                        NaturalLanguageQuery = "naturalLanguageQuery",
                        Filter = "filter",
                        Examples = new List<TrainingExample>()
                        {
                            new TrainingExample()
                            {
                                DocumentId = "documentId",
                                CrossReference = "crossReference",
                                Relevance = 1
                            }
                        }
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<TrainingDataSet>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.ListTrainingData("environmentId", "collectionId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(response.EnvironmentId == "environmentId");
            Assert.IsTrue(response.CollectionId == "collectionId");
            Assert.IsNotNull(response.Queries);
            Assert.IsTrue(response.Queries.Count > 0);
            Assert.IsTrue(response.Queries[0].QueryId == "queryId");
            Assert.IsTrue(response.Queries[0].NaturalLanguageQuery == "naturalLanguageQuery");
            Assert.IsTrue(response.Queries[0].Filter == "filter");
            Assert.IsNotNull(response.Queries[0].Examples);
            Assert.IsTrue(response.Queries[0].Examples.Count > 0);
            Assert.IsTrue(response.Queries[0].Examples[0].DocumentId == "documentId");
            Assert.IsTrue(response.Queries[0].Examples[0].CrossReference == "crossReference");
            Assert.IsTrue(response.Queries[0].Examples[0].Relevance == 1.0f);
        }
        #endregion

        #region Add Training Data
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddTrainingData_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.AddTrainingData(null, "collectionId", new NewTrainingQuery());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddTrainingData_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.AddTrainingData("environmentId", null, new NewTrainingQuery());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddTrainingData_No_Body()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.AddTrainingData("environmentId", "collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddTrainingData_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.AddTrainingData("environmentId", "collectionId", new NewTrainingQuery());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void AddTrainingData_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.AddTrainingData("environmentId", "collectionId", new NewTrainingQuery());
        }

        [TestMethod]
        public void AddTrainingData_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region response
            var response = new TrainingQuery()
            {
                QueryId = "queryId",
                NaturalLanguageQuery = "naturalLanguageQuery",
                Filter = "filter",
                Examples = new List<TrainingExample>()
                {
                    new TrainingExample()
                    {
                        DocumentId = "documentId",
                        CrossReference = "crossReference",
                        Relevance = 1
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody(Arg.Any<NewTrainingQuery>())
                .Returns(request);
            request.As<TrainingQuery>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var newTrainingQuery = new NewTrainingQuery()
            {
                NaturalLanguageQuery = "naturalLanguageQuery",
                Filter = "filter",
                Examples = new List<TrainingExample>()
                {
                    new TrainingExample()
                    {
                        DocumentId = "documentId",
                        CrossReference = "crossReference",
                        Relevance = 1
                    }
                }
            };

            var result = service.AddTrainingData("environmentId", "collectionId", newTrainingQuery);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.QueryId == "queryId");
            Assert.IsTrue(result.NaturalLanguageQuery == "naturalLanguageQuery");
            Assert.IsTrue(result.Filter == "filter");
            Assert.IsNotNull(result.Examples);
            Assert.IsTrue(result.Examples.Count > 0);
            Assert.IsTrue(result.Examples[0].DocumentId == "documentId");
            Assert.IsTrue(result.Examples[0].CrossReference == "crossReference");
            Assert.IsTrue(result.Examples[0].Relevance == 1.0f);
        }
        #endregion

        #region Delete Query
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingData_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteTrainingData(null, "collectionId", "queryId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingData_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteTrainingData("environmentId", null, "queryId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingData_No_QueryId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteTrainingData("environmentId", "collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingData_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteTrainingData("environmentId", "collectionId", "queryId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteTrainingData_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.DeleteTrainingData("environmentId", "collectionId", "queryId");
        }

        [TestMethod]
        public void DeleteTrainingData_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteTrainingData("environmentId", "collectionId", "queryId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Training Data
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingData_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetTrainingData(null, "collectionId", "queryId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingData_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetTrainingData("environmentId", null, "queryId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingData_No_QueryId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetTrainingData("environmentId", "collectionId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingData_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetTrainingData("environmentId", "collectionId", "queryId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetTrainingData_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.GetTrainingData("environmentId", "collectionId", "queryId");
        }

        [TestMethod]
        public void GetTrainingData_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = new TrainingQuery()
            {
                QueryId = "queryId",
                NaturalLanguageQuery = "naturalLanguageQuery",
                Filter = "filter",
                Examples = new List<TrainingExample>()
                {
                    new TrainingExample()
                    {
                        DocumentId = "documentId",
                        CrossReference = "crossReference",
                        Relevance = 1
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<TrainingQuery>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.GetTrainingData("environmentId", "collectionId", "queryId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(response.QueryId == "queryId");
            Assert.IsTrue(response.NaturalLanguageQuery == "naturalLanguageQuery");
            Assert.IsTrue(response.Filter == "filter");
            Assert.IsNotNull(response.Examples);
            Assert.IsTrue(response.Examples.Count > 0);
            Assert.IsTrue(response.Examples[0].DocumentId == "documentId");
            Assert.IsTrue(response.Examples[0].CrossReference == "crossReference");
            Assert.IsTrue(response.Examples[0].Relevance == 1.0f);
        }
        #endregion

        #region Get Training Examples
        //  Not implemented.
        #endregion

        #region Add Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateTrainingExample_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateTrainingExample(null, "collectionId", "queryId", new TrainingExample());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateTrainingExample_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateTrainingExample("environmentId", null, "queryId", new TrainingExample());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateTrainingExample_No_QueryId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateTrainingExample("environmentId", "collectionId", null, new TrainingExample());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateTrainingExample_No_Body()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.CreateTrainingExample("environmentId", "collectionId", "queryId",null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateTrainingExample_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;

            service.CreateTrainingExample("environmentId", "collectionId", "queryId", new TrainingExample());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateTrainingExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.CreateTrainingExample("environmentId", "collectionId", "queryId", new TrainingExample());
        }

        [TestMethod]
        public void CreateTrainingExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region response
            var response = new TrainingExample()
            {
                DocumentId = "documentId",
                CrossReference = "crossReference",
                Relevance = 1
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody(Arg.Any<TrainingExample>())
                .Returns(request);
            request.As<TrainingExample>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var trainingExample = new TrainingExample()
            {
                DocumentId = "documentId",
                CrossReference = "crossReference",
                Relevance = 1
            };

            var result = service.CreateTrainingExample("environmentId", "collectionId", "queryId", trainingExample);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.DocumentId == "documentId");
            Assert.IsTrue(result.CrossReference == "crossReference");
            Assert.IsTrue(result.Relevance == 1.0f);
        }
        #endregion

        #region Remove Example Document
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingExample_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteTrainingExample(null, "collectionId", "queryId", "exampleId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingExample_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteTrainingExample("environmentId", null, "queryId", "exampleId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingExample_No_QueryId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteTrainingExample("environmentId", "collectionId", null, "exampleId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingExample_No_ExampleId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.DeleteTrainingExample("environmentId", "collectionId", "queryId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteTrainingExample_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.DeleteTrainingExample("environmentId", "collectionId", "queryId", "exampleId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteTrainingExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.DeleteTrainingExample("environmentId", "collectionId", "queryId", "exampleId");
        }

        [TestMethod]
        public void DeleteTrainingExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            object response = new object();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteTrainingExample("environmentId", "collectionId", "queryId", "exampleId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Example Details
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingExample_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetTrainingExample(null, "collectionId", "queryId", "exampleId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingExample_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetTrainingExample("environmentId", null, "queryId", "exampleId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingExample_No_QueryId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetTrainingExample("environmentId", "collectionId", null, "exampleId");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingExample_No_ExampleId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.GetTrainingExample("environmentId", "collectionId", "queryId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTrainingExample_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.GetTrainingExample("environmentId", "collectionId", "queryId", "exampleId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetTrainingExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";
            service.GetTrainingExample("environmentId", "collectionId", "queryId", "exampleId");
        }

        [TestMethod]
        public void GetTrainingExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = new TrainingExample()
            {
                DocumentId = "documentId",
                CrossReference = "crossReference",
                Relevance = 1
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<TrainingExample>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.GetTrainingExample("environmentId", "collectionId", "queryId", "exampleId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(response.DocumentId == "documentId");
            Assert.IsTrue(response.CrossReference == "crossReference");
            Assert.IsTrue(response.Relevance == 1.0f);
        }
        #endregion

        #region Update Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateTrainingExample_No_EnvironmentId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateTrainingExample(null, "collectionId", "queryId", "exampleId", new TrainingExamplePatch());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateTrainingExample_No_CollectionId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateTrainingExample("environmentId", null, "queryId", "exampleId", new TrainingExamplePatch());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateTrainingExample_No_QueryId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateTrainingExample("environmentId", "collectionId", null, "exampleId", new TrainingExamplePatch());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateTrainingExample_No_ExampleId()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateTrainingExample("environmentId", "collectionId", "queryId", null, new TrainingExamplePatch());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateTrainingExample_No_Body()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.UpdateTrainingExample("environmentId", "collectionId", "queryId", "exampleId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateTrainingExample_No_VersionDate()
        {
            DiscoveryService service = new DiscoveryService("username", "password", "versionDate");
            service.VersionDate = null;
            service.UpdateTrainingExample("environmentId", "collectionId", "queryId", "exampleId", new TrainingExamplePatch());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateTrainingExample_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "2017-11-07";

            service.UpdateTrainingExample("environmentId", "collectionId", "queryId", "exampleId", new TrainingExamplePatch());
        }

        [TestMethod]
        public void UpdateTrainingExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = new TrainingExample()
            {
                DocumentId = "documentId",
                CrossReference = "crossReference",
                Relevance = 1
            };
            #endregion

            var trainingExample = new TrainingExamplePatch()
            {
                CrossReference = "crossReference",
                Relevance = 1
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<TrainingExamplePatch>(trainingExample)
                .Returns(request);
            request.As<TrainingExample>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateTrainingExample("environmentId", "collectionId", "queryId", "exampleId", trainingExample);

            Assert.IsNotNull(result);
            client.Received().PutAsync(Arg.Any<string>());
            Assert.IsTrue(result.DocumentId == "documentId");
            Assert.IsTrue(result.CrossReference == "crossReference");
            Assert.IsTrue(result.Relevance == 1.0f);
        }
        #endregion
        #endregion
    }
}
