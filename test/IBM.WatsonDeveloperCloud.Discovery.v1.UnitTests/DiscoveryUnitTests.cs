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
                new DiscoveryService(null, "password", DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Password_Null()
        {
            DiscoveryService service =
                new DiscoveryService("username", null, DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01);
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
                new DiscoveryService("username", "password", DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01);

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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
            ListEnvironmentsResponse response = new ListEnvironmentsResponse()
            {
                Environments = new List<ModelEnvironment>()
               {
                   new ModelEnvironment()
                   {
                       Status = ModelEnvironment.StatusEnum.PENDING,
                       Name = "name",
                       Description = "description",
                       Size = 1,
                       IndexCapacity = new IndexCapacity()
                       {
                           DiskUsage = new DiskUsage(){},
                           MemoryUsage = new MemoryUsage(){}
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
            Assert.IsTrue(result.Environments[0].Status == ModelEnvironment.StatusEnum.PENDING);
            Assert.IsTrue(result.Environments[0].Name == "name");
            Assert.IsTrue(result.Environments[0].Description == "description");
            Assert.IsTrue(result.Environments[0].Size == 1);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
            ModelEnvironment response = new ModelEnvironment()
            {
                Status = ModelEnvironment.StatusEnum.PENDING,
                Name = "name",
                Description = "description",
                Size = 1,
                IndexCapacity = new IndexCapacity()
                {
                    DiskUsage = new DiskUsage(){},
                    MemoryUsage = new MemoryUsage(){}
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
            request.As<ModelEnvironment>()
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
            ModelEnvironment response = new ModelEnvironment()
            {
                Status = ModelEnvironment.StatusEnum.PENDING,
                Name = "name",
                Description = "description",
                Size = 1,
                IndexCapacity = new IndexCapacity()
                {
                    DiskUsage = new DiskUsage(){},
                    MemoryUsage = new MemoryUsage(){}
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ModelEnvironment>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.GetEnvironment("environmentId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == ModelEnvironment.StatusEnum.PENDING);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
            ModelEnvironment response = new ModelEnvironment()
            {
                Status = ModelEnvironment.StatusEnum.PENDING,
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
            request.As<ModelEnvironment>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.UpdateEnvironment("environmentId", environment);

            Assert.IsNotNull(result);
            client.Received().PutAsync(Arg.Any<string>());
            Assert.IsTrue(result.Status == ModelEnvironment.StatusEnum.PENDING);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
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
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
            Assert.IsNull(result.ConfigurationId);
            Assert.IsNull(result.Status);
            Assert.IsNull(result.EnrichedFieldUnits);
            Assert.IsNull(result.OriginalMediaType);
        }

        [TestMethod]
        public void TestConfigurationInEnvronment_Success_WithConfiguration()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
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
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
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
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<TestDocument>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.TestConfigurationInEnvironment("environmentId", configuration:"configuration");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Snapshots);
            Assert.IsTrue(result.Snapshots.Count > 0);
            Assert.IsTrue(result.Snapshots[0].Step == DocumentSnapshot.StepEnum.HTML_INPUT);
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
        }

        [TestMethod]
        public void TestConfigurationInEnvronment_Success_WithFile()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
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
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
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
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
        }

        [TestMethod]
        public void TestConfigurationInEnvronment_Success_WithMetadata()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
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
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
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
            request.WithBodyContent(Arg.Any<MultipartFormDataContent>())
                .Returns(request);
            request.As<TestDocument>()
                .Returns(Task.FromResult(response));

            DiscoveryService service = new DiscoveryService(client);
            service.VersionDate = "versionDate";

            var result = service.TestConfigurationInEnvironment("environmentId", metadata:"metadata");
            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Snapshots);
            Assert.IsTrue(result.Snapshots.Count > 0);
            Assert.IsTrue(result.Snapshots[0].Step == DocumentSnapshot.StepEnum.HTML_INPUT);
            Assert.IsNotNull(result.Notices);
            Assert.IsTrue(result.Notices.Count > 0);
            Assert.IsTrue(result.Notices[0].Severity == Notice.SeverityEnum.ERROR);
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
            ListConfigurationsResponse response = new ListConfigurationsResponse()
            {
                Configurations = new List<Configuration>()
               {
                   new Configuration()
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
                                           Level = 1f,
                                           MinSize = 1f,
                                           MaxSize = 1f,
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
                                           Level = 1f,
                                           MinSize = 1f,
                                           MaxSize = 1f,
                                           Bold = false,
                                           Italic = false,
                                           Name = "name"
                                       }
                                   },
                                   Styles = new List<WordStyle>()
                                   {
                                       new WordStyle()
                                       {
                                           Level = 1.0f,
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
                                   Language = EnrichmentOptions.LanguageEnum.EN,
                                   Extract = "extract",
                                   Sentiment = false,
                                   Quotations = false,
                                   ShowSourceText = false,
                                   HierarchicalTypedRelations = false,
                                   _Model= "model"
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
                   }
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
            Assert.IsNull(result.Configurations[0].ConfigurationId);
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
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Options.Language == EnrichmentOptions.LanguageEnum.EN);
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Options.Extract == "extract");
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Options.Sentiment == false);
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Options.Quotations == false);
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Options.ShowSourceText== false);
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Options.HierarchicalTypedRelations == false);
            Assert.IsTrue(result.Configurations[0].Enrichments[0].Options._Model == "model");
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
                                           Level = 1f,
                                           MinSize = 1f,
                                           MaxSize = 1f,
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
                            Language = EnrichmentOptions.LanguageEnum.EN,
                            Extract = "extract",
                            Sentiment = false,
                            Quotations = false,
                            ShowSourceText = false,
                            HierarchicalTypedRelations = false,
                            _Model = "model"
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

            var result = service.CreateConfiguration("environmentId",Configuration);

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
            service.DeleteConfiguration("environmentId","ConfigurationId");
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
            DeleteConfigurationResponse response = new DeleteConfigurationResponse()
            {
                ConfigurationId = "ConfigurationId",
                Status = DeleteConfigurationResponse.StatusEnum.DELETED,
                Notices = new List<Notice>()
                {
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
                    }
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
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
                                           Level = 1f,
                                           MinSize = 1f,
                                           MaxSize = 1f,
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
                            Language = EnrichmentOptions.LanguageEnum.EN,
                            Extract = "extract",
                            Sentiment = false,
                            Quotations = false,
                            ShowSourceText = false,
                            HierarchicalTypedRelations = false,
                            _Model = "model"
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
                                           Level = 1f,
                                           MinSize = 1f,
                                           MaxSize = 1f,
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
                            Language = EnrichmentOptions.LanguageEnum.EN,
                            Extract = "extract",
                            Sentiment = false,
                            Quotations = false,
                            ShowSourceText = false,
                            HierarchicalTypedRelations = false,
                            _Model = "model"
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
                DocumentCounts = new DocumentCounts() {}
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
            Assert.IsNull(result.CollectionId);
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
            Assert.IsNull(result.DocumentCounts.Available);
            Assert.IsNull(result.DocumentCounts.Processing);
            Assert.IsNull(result.DocumentCounts.Failed);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
            ListCollectionFieldsResponse response = new ListCollectionFieldsResponse()
            {
                Fields = new List<Field>()
                {
                    new Field()
                    {
                        FieldType = Field.FieldTypeEnum.STRING
                    }
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
            Assert.IsNull(result.Fields[0].FieldName);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

            service.AddDocument("environmentId", "collectionId");
        }

        [TestMethod]
        public void AddDocument_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DocumentAccepted documentAccepted = new DocumentAccepted()
            {
                Status = DocumentAccepted.StatusEnum.PROCESSING,
                DocumentId = "documentId",
                Notices = new List<Notice>()
                {
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
                    }
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
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
        }

        [TestMethod]
        public void AddDocument_Success_WithConfiguration()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DocumentAccepted documentAccepted = new DocumentAccepted()
            {
                Status = DocumentAccepted.StatusEnum.PROCESSING,
                DocumentId = "documentId",
                Notices = new List<Notice>()
                {
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
                    }
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
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
            DocumentStatus response = new DocumentStatus()
            {
                Status = DocumentStatus.StatusEnum.AVAILABLE,
                Notices = new List<Notice>()
                {
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
                    }
                }
            };
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
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
            Assert.IsNull(result.DocumentId);
            Assert.IsNull(result.ConfigurationId);
            Assert.IsNotNull(result.Created);
            Assert.IsNotNull(result.Updated);
            Assert.IsNull(result.StatusDescription);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;

            service.UpdateDocument("environmentId", "collectionId", "documentId");
        }

        [TestMethod]
        public void UpdateDocument_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DocumentAccepted documentAccepted = new DocumentAccepted()
            {
                Status = DocumentAccepted.StatusEnum.PROCESSING,
                DocumentId = "documentId",
                Notices = new List<Notice>()
                {
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
                    }
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
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
        }

        [TestMethod]
        public void UpdateDocument_Success_WithConfiguration()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DocumentAccepted documentAccepted = new DocumentAccepted()
            {
                Status = DocumentAccepted.StatusEnum.PROCESSING,
                DocumentId = "documentId",
                Notices = new List<Notice>()
                {
                    new Notice()
                    {
                        Severity = Notice.SeverityEnum.ERROR
                    }
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
            Assert.IsNull(result.Notices[0].NoticeId);
            Assert.IsNotNull(result.Notices[0].Created);
            Assert.IsNull(result.Notices[0].DocumentId);
            Assert.IsNull(result.Notices[0].Step);
            Assert.IsNull(result.Notices[0].Description);
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
                        },
                        Interval = 1,
                        Value = 1.0
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
            service.VersionDate = DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01;
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
                        },
                        Interval = 1,
                        Value = 1.0
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
    }
}
