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


namespace IBM.Watson.Discovery.v2.UnitTests
{
    [TestClass]
    public class DiscoveryServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            DiscoveryService service = new DiscoveryService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            DiscoveryService service = new DiscoveryService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            DiscoveryService service = Substitute.For<DiscoveryService>("versionDate");
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            DiscoveryService service = new DiscoveryService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            DiscoveryService service = new DiscoveryService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            DiscoveryService service = new DiscoveryService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var url = System.Environment.GetEnvironmentVariable("DISCOVERY_SERVICE_URL");
            System.Environment.SetEnvironmentVariable("DISCOVERY_SERVICE_URL", null);
            DiscoveryService service = Substitute.For<DiscoveryService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "");
            System.Environment.SetEnvironmentVariable("DISCOVERY_SERVICE_URL", url);
        }
        #endregion

        [TestMethod]
        public void ListCollections_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v2/projects/{projectId}/collections");
        }
        [TestMethod]
        public void Query_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var collectionIds = new List<string>();
            var filter = "filter";
            var query = "query";
            var naturalLanguageQuery = "naturalLanguageQuery";
            var aggregation = "aggregation";
            var count = 1;
            var _return = new List<string>();
            var offset = 1;
            var sort = "sort";
            var highlight = false;
            var spellingSuggestions = false;
            var tableResults = new QueryLargeTableResults();
            var suggestedRefinements = new QueryLargeSuggestedRefinements();
            var passages = new QueryLargePassages();

            var result = service.;

            JObject bodyObject = new JObject();
            if (collectionIds != null && collectionIds.Count > 0)
            {
                bodyObject["collection_ids"] = JToken.FromObject(collectionIds);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                bodyObject["filter"] = JToken.FromObject(filter);
            }
            if (!string.IsNullOrEmpty(query))
            {
                bodyObject["query"] = JToken.FromObject(query);
            }
            if (!string.IsNullOrEmpty(naturalLanguageQuery))
            {
                bodyObject["natural_language_query"] = JToken.FromObject(naturalLanguageQuery);
            }
            if (!string.IsNullOrEmpty(aggregation))
            {
                bodyObject["aggregation"] = JToken.FromObject(aggregation);
            }
            if (count != null)
            {
                bodyObject["count"] = JToken.FromObject(count);
            }
            if (_return != null && _return.Count > 0)
            {
                bodyObject["return"] = JToken.FromObject(_return);
            }
            if (offset != null)
            {
                bodyObject["offset"] = JToken.FromObject(offset);
            }
            if (!string.IsNullOrEmpty(sort))
            {
                bodyObject["sort"] = JToken.FromObject(sort);
            }
            bodyObject["highlight"] = JToken.FromObject(highlight);
            bodyObject["spelling_suggestions"] = JToken.FromObject(spellingSuggestions);
            if (tableResults != null)
            {
                bodyObject["table_results"] = JToken.FromObject(tableResults);
            }
            if (suggestedRefinements != null)
            {
                bodyObject["suggested_refinements"] = JToken.FromObject(suggestedRefinements);
            }
            if (passages != null)
            {
                bodyObject["passages"] = JToken.FromObject(passages);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v2/projects/{projectId}/query");
        }
        [TestMethod]
        public void GetAutocompletion_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var prefix = "prefix";
            var collectionIds = new List<string>() { "collectionIds0", "collectionIds1" };
            var field = "field";
            long? count = 1;

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v2/projects/{projectId}/autocompletion");
        }
        [TestMethod]
        public void QueryNotices_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var filter = "filter";
            var query = "query";
            var naturalLanguageQuery = "naturalLanguageQuery";
            long? count = 1;
            long? offset = 1;

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v2/projects/{projectId}/notices");
        }
        [TestMethod]
        public void ListFields_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var collectionIds = new List<string>() { "collectionIds0", "collectionIds1" };

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v2/projects/{projectId}/fields");
        }
        [TestMethod]
        public void GetComponentSettings_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v2/projects/{projectId}/component_settings");
        }
        [TestMethod]
        public void AddDocument_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var collectionId = "collectionId";
            var file = new MemoryStream();
            var filename = "filename";
            var fileContentType = "fileContentType";
            var metadata = "metadata";
            var xWatsonDiscoveryForce = false;

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().PostAsync($"{service.ServiceUrl}/v2/projects/{projectId}/collections/{collectionId}/documents");
        }
        [TestMethod]
        public void UpdateDocument_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var collectionId = "collectionId";
            var documentId = "documentId";
            var file = new MemoryStream();
            var filename = "filename";
            var fileContentType = "fileContentType";
            var metadata = "metadata";
            var xWatsonDiscoveryForce = false;

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().PostAsync($"{service.ServiceUrl}/v2/projects/{projectId}/collections/{collectionId}/documents/{documentId}");
        }
        [TestMethod]
        public void DeleteDocument_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var collectionId = "collectionId";
            var documentId = "documentId";
            var xWatsonDiscoveryForce = false;

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v2/projects/{projectId}/collections/{collectionId}/documents/{documentId}");
        }
        [TestMethod]
        public void ListTrainingQueries_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries");
        }
        [TestMethod]
        public void DeleteTrainingQueries_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries");
        }
        [TestMethod]
        public void CreateTrainingQuery_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var naturalLanguageQuery = "naturalLanguageQuery";
            var examples = new List<TrainingExample>();
            var filter = "filter";

            var result = service.;

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(naturalLanguageQuery))
            {
                bodyObject["natural_language_query"] = JToken.FromObject(naturalLanguageQuery);
            }
            if (examples != null && examples.Count > 0)
            {
                bodyObject["examples"] = JToken.FromObject(examples);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                bodyObject["filter"] = JToken.FromObject(filter);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries");
        }
        [TestMethod]
        public void GetTrainingQuery_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var queryId = "queryId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries/{queryId}");
        }
        [TestMethod]
        public void UpdateTrainingQuery_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var projectId = "projectId";
            var queryId = "queryId";
            var naturalLanguageQuery = "naturalLanguageQuery";
            var examples = new List<TrainingExample>();
            var filter = "filter";

            var result = service.;

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(naturalLanguageQuery))
            {
                bodyObject["natural_language_query"] = JToken.FromObject(naturalLanguageQuery);
            }
            if (examples != null && examples.Count > 0)
            {
                bodyObject["examples"] = JToken.FromObject(examples);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                bodyObject["filter"] = JToken.FromObject(filter);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries/{queryId}");
        }
    }
}
