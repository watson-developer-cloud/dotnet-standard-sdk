/**
* (C) Copyright IBM Corp. 2017, 2020.
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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.Assistant.v1.Model;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using Newtonsoft.Json;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;

namespace IBM.Watson.Assistant.v1.UnitTests
{
    [TestClass]
    public class AssistantUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            AssistantService service =
                new AssistantService(httpClient: null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            AssistantService service =
                new AssistantService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            AssistantService service =
                new AssistantService(new IBMHttpClient());

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

        #region Counter Examples
        #region Create Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCounterExample_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());

            service.CreateCounterexample(null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCounterExample_No_Body()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateCounterexample("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCounterExample_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;

            service.CreateCounterexample("workspaceId", "text");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateCounterExample_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.CreateCounterexample("workspaceId", "text");
        }

        [TestMethod]
        public void CreateCounterExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Counterexample>>();
            response.Result = Substitute.For<Counterexample>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MaxValue);
            response.Result.Text = "text";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("{\"text\": \"text\"}"))
                .Returns(request);
            request.As<Counterexample>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.CreateCounterexample("workspaceId", "text");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Text == "text");
        }
        #endregion

        #region Delete Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCounterExample_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteCounterexample(null, "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCounterExample_No_Example()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteCounterexample("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCounterExample_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.DeleteCounterexample("workspaceId", "example");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteCounterExample_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.DeleteCounterexample("workspaceId", "example");
        }

        [TestMethod]
        public void DeleteCounterExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DetailedResponse<object> response = new DetailedResponse<object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.DeleteCounterexample("workspaceId", "example");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCounterExample_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetCounterexample(null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCounterExample_No_Example()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetCounterexample("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCounterExample_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.GetCounterexample("workspaceId", "text");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetCounterExample_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.GetCounterexample("workspaceId", "text");
        }

        [TestMethod]
        public void GetCounterExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Counterexample>>();
            response.Result = Substitute.For<Counterexample>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MaxValue);
            response.Result.Text = "text";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<Counterexample>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.GetCounterexample("workspaceId", "text");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Text == "text");
        }
        #endregion

        #region List Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCounterExamples_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListCounterexamples(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCounterExamples_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.ListCounterexamples("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListCounterExamples_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.ListCounterexamples("workspaceId");
        }

        [TestMethod]
        public void ListCounterExamples_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var counterexample = Substitute.For<Counterexample>();
            counterexample.Created.Returns(DateTime.MinValue);
            counterexample.Updated.Returns(DateTime.MaxValue);
            counterexample.Text = "text";

            DetailedResponse<CounterexampleCollection> response = new DetailedResponse<CounterexampleCollection>()
            {
                Result = new CounterexampleCollection()
                {
                    Counterexamples = new List<Counterexample>()
                    {
                        counterexample
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
            request.As<CounterexampleCollection>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.ListCounterexamples("workspaceId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Counterexamples);
            Assert.IsTrue(result.Result.Counterexamples.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Counterexamples[0].Text));
            Assert.IsNotNull(result.Result.Counterexamples[0].Created);
            Assert.IsNotNull(result.Result.Counterexamples[0].Updated);
            Assert.IsTrue(result.Result.Counterexamples[0].Text == "text");
        }
        #endregion

        #region Update Counter Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCounterExample_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateCounterexample(null, "text", "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCounterExample_No_Text()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateCounterexample("workspaceId", null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCounterExample_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;

            service.UpdateCounterexample("workspaceId", "text", "text");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateCounterExample_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.UpdateCounterexample("workspaceId", "text", "text");
        }

        [TestMethod]
        public void UpdateCounterExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Counterexample>>();
            response.Result = Substitute.For<Counterexample>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MaxValue);
            response.Result.Text = "text";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("{\"text\": \"text\"}"))
                .Returns(request);
            request.As<Counterexample>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.UpdateCounterexample("workspaceId", "text", "text");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Text == "text");
        }
        #endregion
        #endregion

        #region Entities
        #region Create Entity
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateEntity_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateEntity(null, "entity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateEntity_No_Body()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateEntity("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateEntity_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;

            service.CreateEntity("workspaceId", "entity");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateEntity_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.CreateEntity("workspaceId", "entity");
        }

        [TestMethod]
        public void CreateEntity_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Entity>>();
            response.Result = Substitute.For<Entity>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MaxValue);
            response.Result._Entity = "entity";
            response.Result.Description = "description";
            #endregion

            CreateEntity entity = new CreateEntity()
            {
                Entity = "entity",
                Description = "description"
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBody<CreateEntity>(entity)
                .Returns(request);
            request.As<Entity>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.CreateEntity("workspaceId", "entity");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result._Entity);
            Assert.IsTrue(result.Result._Entity == "entity");
            Assert.IsNotNull(result.Result.Description);
            Assert.IsTrue(result.Result.Description == "description");
        }
        #endregion

        #region Delete Entity
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteEntity_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteEntity(null, "entity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteEntity_No_Example()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteEntity("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteEntity_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.DeleteEntity("workspaceId", "entity");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteEntity_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.DeleteEntity("workspaceId", "entity");
        }

        [TestMethod]
        public void DeleteEntity_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DetailedResponse<object> response = new DetailedResponse<object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.DeleteEntity("workspaceId", "entity");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Entity
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEntity_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetEntity(null, "entity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEntity_No_Example()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetEntity("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEntity_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.GetEntity("workspaceId", "entity");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetEntity_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.GetEntity("workspaceId", "entity");
        }

        [TestMethod]
        public void GetEntity_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Entity>>();
            response.Result = Substitute.For<Entity>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MaxValue);
            response.Result._Entity = "entity";
            response.Result.Description = "description";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<Entity>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.GetEntity("workspaceId", "entity");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result._Entity);
            Assert.IsTrue(result.Result._Entity == "entity");
            Assert.IsNotNull(result.Result.Description);
            Assert.IsTrue(result.Result.Description == "description");
        }
        #endregion

        #region List Entities
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListEntities_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListEntities(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListEntities_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.ListEntities("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListEntities_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.ListEntities("workspaceId");
        }

        [TestMethod]
        public void ListEntities_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var valueExport = Substitute.For<Value>();
            valueExport.Created.Returns(DateTime.MinValue);
            valueExport.Updated.Returns(DateTime.MinValue);
            valueExport._Value = "value";
            valueExport.Metadata = new Dictionary<string, object>();
            valueExport.Synonyms = new List<string>()
            {
                "synonym"
            };

            var entityExport = Substitute.For<Entity>();
            entityExport.Created.Returns(DateTime.MinValue);
            entityExport.Updated.Returns(DateTime.MinValue);
            entityExport._Entity = "entity";
            entityExport.Description = "description";
            entityExport.Metadata = new Dictionary<string, object>();
            entityExport.FuzzyMatch = true;
            entityExport.Values = new List<Value>()
            {
                valueExport
            };

            var response = Substitute.For<DetailedResponse<EntityCollection>>();
            response.Result = Substitute.For<EntityCollection>();
            response.Result.Entities = new List<Entity>()
            {
                entityExport
            };
            response.Result.Pagination = new Pagination()
            {
                RefreshUrl = "refreshUrl",
                NextUrl = "nextUrl",
                Total = 1,
                Matched = 1
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
            request.As<EntityCollection>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.ListEntities("workspaceId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Entities);
            Assert.IsTrue(result.Result.Entities.Count > 0);
            Assert.IsNotNull(result.Result.Entities[0].Created);
            Assert.IsNotNull(result.Result.Entities[0].Updated);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Entities[0]._Entity));
            Assert.IsTrue(result.Result.Entities[0]._Entity == "entity");
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Entities[0].Description));
            Assert.IsTrue(result.Result.Entities[0].Description == "description");
            Assert.IsNotNull(result.Result.Entities[0].Metadata);
            Assert.IsTrue((bool)result.Result.Entities[0].FuzzyMatch);
            Assert.IsNotNull(result.Result.Entities[0].Values);
            Assert.IsTrue(result.Result.Entities[0].Values.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Entities[0].Values[0]._Value));
            Assert.IsTrue(result.Result.Entities[0].Values[0]._Value == "value");
            Assert.IsNotNull(result.Result.Entities[0].Values[0].Metadata);
            Assert.IsNotNull(result.Result.Entities[0].Values[0].Created);
            Assert.IsNotNull(result.Result.Entities[0].Values[0].Updated);
            Assert.IsNotNull(result.Result.Entities[0].Values[0].Synonyms);
            Assert.IsTrue(result.Result.Entities[0].Values[0].Synonyms.Count > 0);
            Assert.IsTrue(result.Result.Entities[0].Values[0].Synonyms[0] == "synonym");
            Assert.IsTrue(result.Result.Pagination.RefreshUrl == "refreshUrl");
            Assert.IsTrue(result.Result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Result.Pagination.Total == 1);
            Assert.IsTrue(result.Result.Pagination.Matched == 1);
        }
        #endregion

        #region Update Entity
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEntity_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateEntity(null, "entity", "newEntity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEntity_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.UpdateEntity("workspaceId", "text", "newEntity");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateEntity_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.UpdateEntity("workspaceId", "text", "newEntity");
        }

        [TestMethod]
        public void UpdateEntity_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Entity>>();
            response.Result = Substitute.For<Entity>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Entity = "entity";
            response.Result.Description = "description";
            response.Result.Metadata = new Dictionary<string, object>();
            response.Result.FuzzyMatch = true;

            #endregion

            Dictionary<string, string> entity = new Dictionary<string, string>();
            entity.Add("entity", "entity");
            entity.Add("description", "description");

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent(JsonConvert.SerializeObject(entity)))
                .Returns(request);
            request.As<Entity>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.UpdateEntity("workspaceId", "text");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result._Entity));
            Assert.IsTrue(result.Result._Entity == "entity");
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Description));
            Assert.IsTrue(result.Result.Description == "description");
            Assert.IsNotNull(result.Result.Metadata);
            Assert.IsTrue((bool)result.Result.FuzzyMatch);
        }
        #endregion
        #endregion

        #region Examples
        #region Create Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateExample(null, "intent", "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_No_Intent()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateExample("workspaceId", null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_No_Text()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateExample("workspaceId", "intent", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateExample_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.CreateExample("workspaceId", "intent", "text");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateExample_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.CreateExample("workspaceId", "intent", "text");
        }

        [TestMethod]
        public void CreateExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Example>>();
            response.Result = Substitute.For<Example>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result.Text = "text";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("text"))
                .Returns(request);
            request.As<Example>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.CreateExample("workspaceId", "intent", "text");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Text == "text");
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
        }
        #endregion

        #region Delete Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteExample(null, "intent", "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_No_Intent()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteExample("workspaceId", null, "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_No_Example()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());

            service.DeleteExample("workspaceId", "intent", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteExample_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.DeleteExample("workspaceId", "intent", "example");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteExample_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.DeleteExample("workspaceId", "intent", "example");
        }

        [TestMethod]
        public void DeleteExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DetailedResponse<object> response = new DetailedResponse<object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.DeleteExample("workspaceId", "intent", "example");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetExample(null, "intent", "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_No_Intent()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetExample("workspaceId", null, "example");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_No_Example()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetExample("workspaceId", "intent", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetExample_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.GetExample("workspaceId", "intent", "example");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetExample_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.GetExample("workspaceId", "intent", "example");
        }

        [TestMethod]
        public void GetExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Example>>();
            response.Result = Substitute.For<Example>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result.Text = "text";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<Example>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.GetExample("workspaceId", "intent", "example");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Text == "text");
        }
        #endregion

        #region List Examples
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListExamples_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListExamples(null, "intent");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListExamples_No_Intent()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListExamples("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListExamples_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.ListExamples("workspaceId", "intent");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListExamples_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.ListExamples("workspaceId", "intent");
        }

        [TestMethod]
        public void ListExamples_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var example = Substitute.For<Example>();
            example.Created.Returns(DateTime.MinValue);
            example.Updated.Returns(DateTime.MinValue);
            example.Text = "text";

            DetailedResponse<ExampleCollection> response = new DetailedResponse<ExampleCollection>()
            {
                Result = new ExampleCollection()
                {
                    Examples = new List<Example>()
                    {
                        example
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
            request.As<ExampleCollection>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.ListExamples("workspaceId", "intent");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Examples);
            Assert.IsTrue(result.Result.Examples.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Examples[0].Text));
            Assert.IsNotNull(result.Result.Examples[0].Created);
            Assert.IsNotNull(result.Result.Examples[0].Updated);
            Assert.IsTrue(result.Result.Examples[0].Text == "text");
        }
        #endregion

        #region Update Example
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateExample(null, "intent", "text", "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_Intent()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateExample("workspaceId", null, "text", "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_Text()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateExample("workspaceId", "intent", null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateExample_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.UpdateExample("workspaceId", "intent", "text", "text2");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateExample_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.UpdateExample("workspaceId", "intent", "text", "text2");
        }

        [TestMethod]
        public void UpdateExample_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Example>>();
            response.Result = Substitute.For<Example>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result.Text = "text2";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("text2"))
                .Returns(request);
            request.As<Example>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.UpdateExample("workspaceId", "intent", "text", "text2");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Text == "text2");
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
        }
        #endregion
        #endregion

        #region Intents
        #region Create Intent
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateIntent_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateIntent(null, "intent");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateIntent_No_Intent()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateIntent("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateIntent_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.CreateIntent("workspaceId", "intent");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateIntent_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.CreateIntent("workspaceId", "intent");
        }

        [TestMethod]
        public void CreateIntent_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Intent>>();
            response.Result = Substitute.For<Intent>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Intent = "intent";
            response.Result.Description = "description";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("intent"))
                .Returns(request);
            request.As<Intent>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.CreateIntent("workspaceId", "intent");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result._Intent == "intent");
            Assert.IsTrue(result.Result.Description == "description");
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
        }
        #endregion

        #region Delete Intent
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteIntent_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteIntent(null, "intent");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteIntent_No_Intent()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteIntent("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteIntent_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.DeleteIntent("workspaceId", "intent");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteIntent_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.DeleteIntent("workspaceId", "intent");
        }

        [TestMethod]
        public void DeleteIntent_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DetailedResponse<object> response = new DetailedResponse<object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.DeleteIntent("workspaceId", "intent");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Intent
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetIntent_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetIntent(null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetIntent_No_Intent()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetIntent("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetIntent_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.GetIntent("workspaceId", "text");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetIntent_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.GetIntent("workspaceId", "text");
        }

        [TestMethod]
        public void GetIntent_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var example = Substitute.For<Example>();
            example.Created.Returns(DateTime.MinValue);
            example.Updated.Returns(DateTime.MinValue);
            example.Text = "text";

            var response = Substitute.For<DetailedResponse<Intent>>();
            response.Result = Substitute.For<Intent>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Intent = "intent";
            response.Result.Description = "description";
            response.Result.Examples = new List<Example>()
            {
                example
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.As<Intent>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.GetIntent("workspaceId", "text");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result._Intent == "intent");
            Assert.IsTrue(result.Result.Description == "description");
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
            Assert.IsNotNull(result.Result.Examples);
            Assert.IsTrue(result.Result.Examples.Count > 0);
            Assert.IsNotNull(result.Result.Examples[0].Created);
            Assert.IsNotNull(result.Result.Examples[0].Updated);
            Assert.IsTrue(result.Result.Examples[0].Text == "text");
        }
        #endregion

        #region List Intents
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListIntents_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListIntents(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListIntents_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.ListIntents("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListIntents_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.ListIntents("workspaceId");
        }

        [TestMethod]
        public void ListIntents_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var example = Substitute.For<Example>();
            example.Created.Returns(DateTime.MinValue);
            example.Updated.Returns(DateTime.MinValue);
            example.Text = "example";

            var intentExport = Substitute.For<Intent>();
            intentExport.Created.Returns(DateTime.MinValue);
            intentExport.Updated.Returns(DateTime.MinValue);
            intentExport._Intent = "intent";
            intentExport.Description = "description";
            intentExport.Examples = new List<Example>()
            {
                example
            };

            var response = new DetailedResponse<IntentCollection>()
            {
                Result = new IntentCollection()
                {
                    Intents = new List<Intent>()
                    {
                        intentExport
                    }
                }
            };

            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<IntentCollection>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.ListIntents("workspaceId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Intents);
            Assert.IsTrue(result.Result.Intents.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Intents[0]._Intent));
            Assert.IsTrue(result.Result.Intents[0]._Intent == "intent");
            Assert.IsNotNull(result.Result.Intents[0].Created);
            Assert.IsNotNull(result.Result.Intents[0].Updated);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Intents[0].Description));
            Assert.IsTrue(result.Result.Intents[0].Description == "description");
            Assert.IsNotNull(result.Result.Intents[0].Examples);
            Assert.IsTrue(result.Result.Intents[0].Examples.Count > 0);
            Assert.IsTrue(result.Result.Intents[0].Examples[0].Text == "example");
            Assert.IsNotNull(result.Result.Intents[0].Examples[0].Created);
            Assert.IsNotNull(result.Result.Intents[0].Examples[0].Updated);
        }
        #endregion

        #region Update Intent
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateIntent(null, "text");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_No_Text()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateIntent("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateIntent_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.UpdateIntent("workspaceId", "text");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateIntent_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.UpdateIntent("workspaceId", "text");
        }

        [TestMethod]
        public void UpdateIntent_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Intent>>();
            response.Result = Substitute.For<Intent>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Intent = "intent";
            response.Result.Description = "description";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("intent"))
                .Returns(request);
            request.As<Intent>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.UpdateIntent("workspaceId", "text");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result._Intent == "intent");
            Assert.IsTrue(result.Result.Description == "description");
        }
        #endregion
        #endregion

        #region List Logs
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListLogs_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListLogs(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListLogs_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.ListLogs("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListLogs_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.ListLogs("workspaceId");
        }

        [TestMethod]
        public void ListLogs_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DetailedResponse<LogCollection> response = Substitute.For<DetailedResponse<LogCollection>>();
            response.Result = Substitute.For<LogCollection>();
            var log = new Log()
            {
                LogId = "logId",
                RequestTimestamp = "requestTimestamp",
                ResponseTimestamp = "responseTimestamp"
            };
            response.Result.Logs = new List<Log>()
            {
                log
            };

            var pagination = new LogPagination()
            {
                NextUrl = "nextUrl",
                Matched = 1
            };
            response.Result.Pagination = pagination;
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<LogCollection>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.ListLogs("workspaceId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Logs);
            Assert.IsTrue(result.Result.Logs.Count > 0);
            Assert.IsNotNull(result.Result.Pagination);
            Assert.IsTrue(result.Result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Result.Pagination.Matched == 1);
            Assert.IsTrue(result.Result.Logs[0].LogId == "logId");
            Assert.IsTrue(result.Result.Logs[0].RequestTimestamp == "requestTimestamp");
            Assert.IsTrue(result.Result.Logs[0].ResponseTimestamp == "responseTimestamp");
        }
        #endregion

        #region Message
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Message_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Message(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Message_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.Message("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Message_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.Message("workspaceId");
        }

        [TestMethod]
        public void Message_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = new DetailedResponse<MessageResponse>();

            var messageResponse = new MessageResponse()
            {
                Input = new MessageInput()
                {
                    Text = "text"
                },
                Intents = new List<RuntimeIntent>()
                            {
                                new RuntimeIntent()
                                {
                                    Intent = "intent",
                                    Confidence = 1.0
                                }
                            },
                Entities = new List<RuntimeEntity>()
                            {
                                new RuntimeEntity()
                                {
                                    Entity = "entity",
                                    Location = new List<long?>()
                                    {
                                        0
                                    },
                                    Value = "value",
                                    Confidence = 1.0f
                                }
                            },
                AlternateIntents = true,
                Context = new Context()
                {
                    ConversationId = "conversationId",
                    System = new Dictionary<string, object>()
                },
                Output = new OutputData()
                {
                    LogMessages = new List<LogMessage>()
                                {
                                    new LogMessage()
                                    {
                                        Level = LogMessage.LevelEnumValue.INFO,
                                        Msg = "msg"
                                    }
                                },
                    NodesVisited = new List<string>()
                                {
                                    "node"
                                }
                }
            };

            response.Result = messageResponse;
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("message"))
                .Returns(request);
            request.As<MessageResponse>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.Message("workspaceId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Input);
            Assert.IsNotNull(result.Result.Intents);
            Assert.IsNotNull(result.Result.Entities);
            Assert.IsNotNull(result.Result.AlternateIntents);
            Assert.IsNotNull(result.Result.Context);
            Assert.IsNotNull(result.Result.Output);
        }
        #endregion

        #region Synonyms
        #region Create Synonym
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateSynonym(null, "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateSynonym("workspaceId", null, "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateSynonym("workspaceId", "entity", null, "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_Body()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateSynonym("workspaceId", "entity", "value", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateSynonym_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.CreateSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateSynonym_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.CreateSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod]
        public void CreateSynonym_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Synonym>>();
            response.Result = Substitute.For<Synonym>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Synonym = "synonym";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("synonym"))
                .Returns(request);
            request.As<Synonym>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.CreateSynonym("workspaceId", "entity", "value", "synonym");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result._Synonym);
            Assert.IsTrue(result.Result._Synonym == "synonym");
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
        }
        #endregion

        #region Delete Synonym
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteSynonym(null, "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteSynonym("workspaceId", null, "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteSynonym("workspaceId", "entity", null, "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_Synonym()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteSynonym("workspaceId", "entity", "value", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSynonym_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.DeleteSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteSynonym_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.DeleteSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod]
        public void DeleteSynonym_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DetailedResponse<object> response = new DetailedResponse<object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.DeleteSynonym("workspaceId", "entity", "value", "synonym");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Synonym
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetSynonym(null, "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetSynonym("workspaceId", null, "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetSynonym("workspaceId", "entity", null, "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_Synonym()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetSynonym("workspaceId", "entity", "value", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSynonym_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.GetSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetSynonym_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.GetSynonym("workspaceId", "entity", "value", "synonym");
        }

        [TestMethod]
        public void GetSynonym_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Synonym>>();
            response.Result = Substitute.For<Synonym>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Synonym = "synonym";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<Synonym>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.GetSynonym("workspaceId", "entity", "value", "synonym");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result._Synonym);
            Assert.IsTrue(result.Result._Synonym == "synonym");
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
        }
        #endregion

        #region List Synonyms
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListSynonyms_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListSynonyms(null, "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListSynonyms_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListSynonyms("workspaceId", null, "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListSynonyms_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListSynonyms("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListSynonyms_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.ListSynonyms("workspaceId", "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListSynonyms_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.ListSynonyms("workspaceId", "entity", "value");
        }

        [TestMethod]
        public void ListSynonyms_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var synonym = Substitute.For<Synonym>();
            synonym.Created.Returns(DateTime.MinValue);
            synonym.Updated.Returns(DateTime.MinValue);
            synonym._Synonym = "synonym";

            DetailedResponse<SynonymCollection> response = new DetailedResponse<SynonymCollection>()
            {
                Result = new SynonymCollection()
                {
                    Synonyms = new List<Synonym>()
                {
                    synonym
                },
                    Pagination = new Pagination()
                    {
                        RefreshUrl = "refreshUrl",
                        NextUrl = "nextUrl",
                        Total = 1,
                        Matched = 1
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<SynonymCollection>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.ListSynonyms("workspaceId", "entity", "value");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Synonyms);
            Assert.IsTrue(result.Result.Synonyms.Count > 0);
            Assert.IsNotNull(result.Result.Synonyms[0].Created);
            Assert.IsNotNull(result.Result.Synonyms[0].Updated);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Result.Synonyms[0]._Synonym));
            Assert.IsTrue(result.Result.Synonyms[0]._Synonym == "synonym");
            Assert.IsTrue(result.Result.Pagination.RefreshUrl == "refreshUrl");
            Assert.IsTrue(result.Result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Result.Pagination.Total == 1);
            Assert.IsTrue(result.Result.Pagination.Matched == 1);
        }
        #endregion

        #region Update Synonym
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateSynonym(null, "entity", "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateSynonym("workspaceId", null, "value", "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateSynonym("workspaceId", "entity", null, "synonym");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_Synonym()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateSynonym("workspaceId", "entity", "value", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateSynonym_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;

            service.UpdateSynonym("workspaceId", "entity", "value", "synonym", "synonym2");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateSynonym_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.UpdateSynonym("workspaceId", "entity", "value", "synonym", "synonym2");
        }

        [TestMethod]
        public void UpdateSynonym_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Synonym>>();
            response.Result = Substitute.For<Synonym>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Synonym = "synonym";
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("synonym"))
                .Returns(request);
            request.As<Synonym>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.UpdateSynonym("workspaceId", "entity", "value", "synonym");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
            Assert.IsTrue(result.Result._Synonym == "synonym");
        }
        #endregion
        #endregion

        #region Values
        #region Create Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateValue_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateValue(null, "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateValue_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateValue("workspaceId", null, "value");
        }
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateValue_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.CreateValue("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateValue_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.CreateValue("workspaceId", "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateValue_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.CreateValue("workspaceId", "entity", "value");
        }

        [TestMethod]
        public void CreateValue_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Value>>();
            response.Result = Substitute.For<Value>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Value = "value";
            response.Result.Metadata = new Dictionary<string, object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("value"))
                .Returns(request);
            request.As<Value>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.CreateValue("workspaceId", "entity", "value");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result._Value == "value");
            Assert.IsNotNull(result.Result.Metadata);
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
        }
        #endregion

        #region Delete Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteValue_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteValue(null, "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteValue_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteValue("workspaceId", null, "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteValue_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteValue("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteValue_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.DeleteValue("workspaceId", "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteValue_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.DeleteValue("workspaceId", "entity", "example");
        }

        [TestMethod]
        public void DeleteValue_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DetailedResponse<object> response = new DetailedResponse<object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.DeleteValue("workspaceId", "entity", "example");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetValue(null, "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetValue("workspaceId", null, "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetValue("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.GetValue("workspaceId", "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetValue_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.GetValue("workspaceId", "entity", "value");
        }

        [TestMethod]
        public void GetValue_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Value>>();
            response.Result = Substitute.For<Value>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Value = "value";
            response.Result.Metadata = new Dictionary<string, object>();
            response.Result.Synonyms = new List<string>()
            {
                "synonym"
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<Value>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.GetValue("workspaceId", "entity", "value");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
            Assert.IsNotNull(result.Result.Metadata);
            Assert.IsNotNull(result.Result.Synonyms);
            Assert.IsTrue(result.Result._Value == "value");
            Assert.IsTrue(result.Result.Synonyms.Count > 0);
            Assert.IsTrue(result.Result.Synonyms[0] == "synonym");
        }
        #endregion

        #region List Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListValues_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListValues(null, "entity");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListValues_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.ListValues("workspaceId", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListValues_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.ListValues("workspaceId", "entity");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListValues_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.ListValues("workspaceId", "entity");
        }

        [TestMethod]
        public void ListValues_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var valueExport = Substitute.For<Value>();
            valueExport.Created.Returns(DateTime.MinValue);
            valueExport.Updated.Returns(DateTime.MinValue);
            valueExport._Value = "value";
            valueExport.Metadata = new Dictionary<string, object>();
            valueExport.Synonyms = new List<string>()
            {
                "synonym"
            };

            DetailedResponse<ValueCollection> response = new DetailedResponse<ValueCollection>()
            {
                Result = new ValueCollection()
                {
                    Values = new List<Value>()
                    {
                        valueExport
                    },
                    Pagination = new Pagination()
                    {
                        RefreshUrl = "refreshUrl",
                        NextUrl = "nextUrl",
                        Total = 1,
                        Matched = 1
                    }
                }
            };

            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ValueCollection>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.ListValues("workspaceId", "entity");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Values);
            Assert.IsTrue(result.Result.Values.Count > 0);
            Assert.IsTrue(result.Result.Values[0]._Value == "value");
            Assert.IsNotNull(result.Result.Values[0].Metadata);
            Assert.IsNotNull(result.Result.Values[0].Created);
            Assert.IsNotNull(result.Result.Values[0].Updated);
            Assert.IsNotNull(result.Result.Values[0].Synonyms);
            Assert.IsTrue(result.Result.Values[0].Synonyms.Count > 0);
            Assert.IsTrue(result.Result.Values[0].Synonyms[0] == "synonym");
            Assert.IsTrue(result.Result.Pagination.RefreshUrl == "refreshUrl");
            Assert.IsTrue(result.Result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Result.Pagination.Total == 1);
            Assert.IsTrue(result.Result.Pagination.Matched == 1);
        }
        #endregion

        #region Update Value
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateValue(null, "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_Entity()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateValue("workspaceId", null, "value");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_Value()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateValue("workspaceId", "entity", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateValue_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;

            service.UpdateValue("workspaceId", "entity", "value");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateValue_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.UpdateValue("workspaceId", "entity", "value");
        }

        [TestMethod]
        public void UpdateValue_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Value>>();
            response.Result = Substitute.For<Value>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result._Value = "value";
            response.Result.Metadata = new Dictionary<string, object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("value"))
                .Returns(request);
            request.As<Value>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.UpdateValue("workspaceId", "entity", "value");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result._Value == "value");
            Assert.IsNotNull(result.Result.Metadata);
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Updated);
        }
        #endregion
        #endregion

        #region Workspaces
        #region Create Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateWorkspace_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.CreateWorkspace();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateWorkspace_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.CreateWorkspace();
        }

        [TestMethod]
        public void CreateWorkspace_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Workspace>>();
            response.Result = Substitute.For<Workspace>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result.WorkspaceId.Returns("workspaceId");
            response.Result.Name = "name";
            response.Result.Language = "en";
            response.Result.Description = "description";
            response.Result.Metadata = new Dictionary<string, object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("workspace"))
                .Returns(request);
            request.As<Workspace>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.CreateWorkspace();

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Metadata);
            Assert.IsTrue(result.Result.Name == "name");
            Assert.IsTrue(result.Result.Description == "description");
            Assert.IsTrue(result.Result.Language == "en");
            Assert.IsTrue(result.Result.WorkspaceId == "workspaceId");
            Assert.IsTrue(result.Result.Description == "description");
        }
        #endregion

        #region Delete Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWorkspace_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.DeleteWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteWorkspace_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.DeleteWorkspace("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteWorkspace_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.DeleteWorkspace("workspaceId");
        }

        [TestMethod]
        public void DeleteWorkspace_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            DetailedResponse<object> response = new DetailedResponse<object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<object>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.DeleteWorkspace("workspaceId");

            Assert.IsNotNull(result);
            client.Received().DeleteAsync(Arg.Any<string>());
        }
        #endregion

        #region Get Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetWorkspace_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.GetWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetWorkspace_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.GetWorkspace("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetWorkspace_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.GetWorkspace("workspaceId");
        }

        [TestMethod]
        public void GetWorkspace_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var example = Substitute.For<Example>();
            example.Created.Returns(DateTime.MinValue);
            example.Updated.Returns(DateTime.MinValue);
            example.Text = "text";

            var intentExport = Substitute.For<Intent>();
            intentExport.Created.Returns(DateTime.MinValue);
            intentExport.Updated.Returns(DateTime.MinValue);
            intentExport._Intent = "intent";
            intentExport.Description = "description";
            intentExport.Examples = new List<Example>()
            {
                example
            };

            var valueExport = Substitute.For<Value>();
            valueExport.Created.Returns(DateTime.MinValue);
            valueExport.Updated.Returns(DateTime.MinValue);
            valueExport._Value = "value";
            valueExport.Metadata = new Dictionary<string, object>();
            valueExport.Synonyms = new List<string>()
            {
                "synonym"
            };

            var entityExport = Substitute.For<Entity>();
            entityExport.Created.Returns(DateTime.MinValue);
            entityExport.Updated.Returns(DateTime.MinValue);
            entityExport._Entity = "entity";
            entityExport.Description = "description";
            entityExport.Metadata = new Dictionary<string, object>();
            entityExport.FuzzyMatch = true;
            entityExport.Values = new List<Value>()
            {
                valueExport
            };

            var counterexample = Substitute.For<Counterexample>();
            counterexample.Created.Returns(DateTime.MinValue);
            counterexample.Updated.Returns(DateTime.MaxValue);
            counterexample.Text = "text";

            var response = Substitute.For<DetailedResponse<Workspace>>();
            response.Result = Substitute.For<Workspace>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MinValue);
            response.Result.WorkspaceId.Returns("workspaceId");
            response.Result.Name = "name";
            response.Result.Language = "en";
            response.Result.Description = "description";
            response.Result.Metadata = new Dictionary<string, object>();
            response.Result.Intents = new List<Intent>()
            {
                intentExport
            };
            response.Result.Entities = new List<Entity>()
            {
                entityExport
            };
            response.Result.Counterexamples = new List<Counterexample>()
            {
                counterexample
            };
            response.Result.DialogNodes = new List<DialogNode>()
            {
                new DialogNode() { }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.As<Workspace>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.GetWorkspace("workspaceId");

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Metadata);
            Assert.IsTrue(result.Result.Name == "name");
            Assert.IsTrue(result.Result.Description == "description");
            Assert.IsTrue(result.Result.Language == "en");
            Assert.IsTrue(result.Result.WorkspaceId == "workspaceId");
            Assert.IsTrue(result.Result.Description == "description");
        }
        #endregion

        #region List Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListWorkspaces_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;
            service.ListWorkspaces();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListWorkspaces_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";
            service.ListWorkspaces();
        }

        [TestMethod]
        public void ListWorkspaces_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var workspace = Substitute.For<Workspace>();
            workspace.Created.Returns(DateTime.MinValue);
            workspace.Updated.Returns(DateTime.MaxValue);
            workspace.WorkspaceId.Returns("workspaceId");
            workspace.Name = "name";
            workspace.Language = "en";
            workspace.Description = "description";
            workspace.Metadata = new Dictionary<string, object>();

            DetailedResponse<WorkspaceCollection> response = new DetailedResponse<WorkspaceCollection>()
            {
                Result = new WorkspaceCollection()
                {
                    Workspaces = new List<Workspace>()
                {
                    workspace
                },
                    Pagination = new Pagination()
                    {
                        RefreshUrl = "refreshUrl",
                        NextUrl = "nextUrl",
                        Total = 1,
                        Matched = 1
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<long?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool?>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<WorkspaceCollection>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.ListWorkspaces();

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Pagination.RefreshUrl == "refreshUrl");
            Assert.IsTrue(result.Result.Pagination.NextUrl == "nextUrl");
            Assert.IsTrue(result.Result.Pagination.Total == 1);
            Assert.IsTrue(result.Result.Pagination.Matched == 1);
            Assert.IsNotNull(result.Result.Workspaces[0].Created);
            Assert.IsNotNull(result.Result.Workspaces[0].Created);
            Assert.IsNotNull(result.Result.Workspaces[0].Metadata);
            Assert.IsTrue(result.Result.Workspaces[0].Name == "name");
            Assert.IsTrue(result.Result.Workspaces[0].Description == "description");
            Assert.IsTrue(result.Result.Workspaces[0].Language == "en");
            Assert.IsTrue(result.Result.Workspaces[0].WorkspaceId == "workspaceId");
            Assert.IsTrue(result.Result.Workspaces[0].Description == "description");
        }
        #endregion

        #region Update Workspace
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWorkspace_No_WorkspaceId()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.UpdateWorkspace(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateWorkspace_No_VersionDate()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            service.Version = null;

            service.UpdateWorkspace("workspaceId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void UpdateWorkspace_Catch_Exception()
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

            AssistantService service = new AssistantService(client);
            service.Version = "2018-02-16";

            service.UpdateWorkspace("workspaceId");
        }

        [TestMethod]
        public void UpdateWorkspace_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = Substitute.For<DetailedResponse<Workspace>>();
            response.Result = Substitute.For<Workspace>();
            response.Result.Created.Returns(DateTime.MinValue);
            response.Result.Updated.Returns(DateTime.MaxValue);
            response.Result.WorkspaceId.Returns("workspaceId");
            response.Result.Name = "name";
            response.Result.Language = "en";
            response.Result.Description = "description";
            response.Result.Metadata = new Dictionary<string, object>();
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("workspace"))
                .Returns(request);
            request.As<Workspace>()
                .Returns(Task.FromResult(response));

            AssistantService service = new AssistantService(client);
            service.Version = "versionDate";

            var result = service.UpdateWorkspace("workspaceId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Created);
            Assert.IsNotNull(result.Result.Metadata);
            Assert.IsTrue(result.Result.Name == "name");
            Assert.IsTrue(result.Result.Description == "description");
            Assert.IsTrue(result.Result.Language == "en");
            Assert.IsTrue(result.Result.WorkspaceId == "workspaceId");
            Assert.IsTrue(result.Result.Description == "description");
        }
        #endregion
        #endregion
    }
}
