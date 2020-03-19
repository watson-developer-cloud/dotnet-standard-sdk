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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IBM.Watson.Discovery.v2.Model;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

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
            var apikey = System.Environment.GetEnvironmentVariable("DISCOVERY_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("DISCOVERY_URL");
            System.Environment.SetEnvironmentVariable("DISCOVERY_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("DISCOVERY_URL", "http://www.url.com");
            DiscoveryService service = Substitute.For<DiscoveryService>("testString");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("DISCOVERY_URL", url);
            System.Environment.SetEnvironmentVariable("DISCOVERY_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            DiscoveryService service = new DiscoveryService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        #endregion

        [TestMethod]
        public void TestQueryLargePassagesModel()
        {

            var QueryLargePassagesFields = new List<string> { "testString" };
            QueryLargePassages testRequestModel = new QueryLargePassages()
            {
                Enabled = true,
                PerDocument = true,
                MaxPerDocument = 38,
                Fields = QueryLargePassagesFields,
                Count = 38,
                Characters = 38
            };

            Assert.IsTrue(testRequestModel.Enabled == true);
            Assert.IsTrue(testRequestModel.PerDocument == true);
            Assert.IsTrue(testRequestModel.MaxPerDocument == 38);
            Assert.IsTrue(testRequestModel.Fields == QueryLargePassagesFields);
            Assert.IsTrue(testRequestModel.Count == 38);
            Assert.IsTrue(testRequestModel.Characters == 38);
        }

        [TestMethod]
        public void TestQueryLargeSuggestedRefinementsModel()
        {

            QueryLargeSuggestedRefinements testRequestModel = new QueryLargeSuggestedRefinements()
            {
                Enabled = true,
                Count = 38
            };

            Assert.IsTrue(testRequestModel.Enabled == true);
            Assert.IsTrue(testRequestModel.Count == 38);
        }

        [TestMethod]
        public void TestQueryLargeTableResultsModel()
        {

            QueryLargeTableResults testRequestModel = new QueryLargeTableResults()
            {
                Enabled = true,
                Count = 38
            };

            Assert.IsTrue(testRequestModel.Enabled == true);
            Assert.IsTrue(testRequestModel.Count == 38);
        }

        [TestMethod]
        public void TestTrainingExampleModel()
        {

            TrainingExample testRequestModel = new TrainingExample()
            {
                DocumentId = "testString",
                CollectionId = "testString",
                Relevance = 38,
            };

            Assert.IsTrue(testRequestModel.DocumentId == "testString");
            Assert.IsTrue(testRequestModel.CollectionId == "testString");
            Assert.IsTrue(testRequestModel.Relevance == 38);
        }

        [TestMethod]
        public void TestTrainingQueryModel()
        {

            var TrainingQueryExamples = new List<TrainingExample> { TrainingExampleModel };
            TrainingQuery testRequestModel = new TrainingQuery()
            {
                NaturalLanguageQuery = "testString",
                Filter = "testString",
                Examples = TrainingQueryExamples
            };

            Assert.IsTrue(testRequestModel.NaturalLanguageQuery == "testString");
            Assert.IsTrue(testRequestModel.Filter == "testString");
            Assert.IsTrue(testRequestModel.Examples == TrainingQueryExamples);
        }

        [TestMethod]
        public void TestTestListCollectionsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'collections': [{'collection_id': 'CollectionId', 'name': 'Name'}]}";
            var response = new DetailedResponse<ListCollectionsResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ListCollectionsResponse>(responseJson),
                StatusCode = 200
            };

            string projectId = "testString";

            request.As<ListCollectionsResponse>().Returns(Task.FromResult(response));

            var result = service.ListCollections(projectId: projectId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/collections";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestQueryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'matching_results': 15, 'results': [{'document_id': 'DocumentId', 'metadata': {'mapKey': 'unknown property type: Inner'}, 'result_metadata': {'document_retrieval_source': 'search', 'collection_id': 'CollectionId', 'confidence': 10}, 'document_passages': [{'passage_text': 'PassageText', 'start_offset': 11, 'end_offset': 9, 'field': 'Field'}]}], 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': [{'type': 'filter', 'match': 'Match', 'matching_results': 15, 'aggregations': []}]}]}]}]}]}]}]}]}], 'retrieval_details': {'document_retrieval_strategy': 'untrained'}, 'suggested_query': 'SuggestedQuery', 'suggested_refinements': [{'text': 'Text'}], 'table_results': [{'table_id': 'TableId', 'source_document_id': 'SourceDocumentId', 'collection_id': 'CollectionId', 'table_html': 'TableHtml', 'table_html_offset': 15, 'table': {'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'section_title': {'text': 'Text', 'location': {'begin': 5, 'end': 3}}, 'title': {'text': 'Text', 'location': {'begin': 5, 'end': 3}}, 'table_headers': [{'cell_id': 'CellId', 'location': 'unknown property type: Location', 'text': 'Text', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'row_headers': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'text_normalized': 'TextNormalized', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'column_headers': [{'cell_id': 'CellId', 'location': 'unknown property type: Location', 'text': 'Text', 'text_normalized': 'TextNormalized', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14}], 'key_value_pairs': [{'key': {'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text'}, 'value': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text'}]}], 'body_cells': [{'cell_id': 'CellId', 'location': {'begin': 5, 'end': 3}, 'text': 'Text', 'row_index_begin': 13, 'row_index_end': 11, 'column_index_begin': 16, 'column_index_end': 14, 'row_header_ids': [{'id': 'Id'}], 'row_header_texts': [{'text': 'Text'}], 'row_header_texts_normalized': [{'text_normalized': 'TextNormalized'}], 'column_header_ids': [{'id': 'Id'}], 'column_header_texts': [{'text': 'Text'}], 'column_header_texts_normalized': [{'text_normalized': 'TextNormalized'}], 'attributes': [{'type': 'Type', 'text': 'Text', 'location': {'begin': 5, 'end': 3}}]}], 'contexts': [{'text': 'Text', 'location': {'begin': 5, 'end': 3}}]}}]}";
            var response = new DetailedResponse<QueryResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<QueryResponse>(responseJson),
                StatusCode = 200
            };

            QueryLargeTableResults QueryLargeTableResultsModel = new QueryLargeTableResults()
            {
                Enabled = true,
                Count = 38
            };

            QueryLargeSuggestedRefinements QueryLargeSuggestedRefinementsModel = new QueryLargeSuggestedRefinements()
            {
                Enabled = true,
                Count = 38
            };

            var QueryLargePassagesFields = new List<string> { "testString" };
            QueryLargePassages QueryLargePassagesModel = new QueryLargePassages()
            {
                Enabled = true,
                PerDocument = true,
                MaxPerDocument = 38,
                Fields = QueryLargePassagesFields,
                Count = 38,
                Characters = 38
            };
            string projectId = "testString";
            List<string> collectionIds = new List<string> { "testString" };
            string filter = "testString";
            string query = "testString";
            string naturalLanguageQuery = "testString";
            string aggregation = "testString";
            long? count = 38;
            List<string> _return = new List<string> { "testString" };
            long? offset = 38;
            string sort = "testString";
            bool? highlight = true;
            bool? spellingSuggestions = true;
            QueryLargeTableResults tableResults = QueryLargeTableResultsModel;
            QueryLargeSuggestedRefinements suggestedRefinements = QueryLargeSuggestedRefinementsModel;
            QueryLargePassages passages = QueryLargePassagesModel;

            request.As<QueryResponse>().Returns(Task.FromResult(response));

            var result = service.Query(projectId: projectId, collectionIds: collectionIds, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, aggregation: aggregation, count: count, _return: _return, offset: offset, sort: sort, highlight: highlight, spellingSuggestions: spellingSuggestions, tableResults: tableResults, suggestedRefinements: suggestedRefinements, passages: passages);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/query";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetAutocompletionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'completions': ['Completions']}";
            var response = new DetailedResponse<Completions>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Completions>(responseJson),
                StatusCode = 200
            };

            string projectId = "testString";
            string prefix = "testString";
            List<string> collectionIds = new List<string> { "testString" };
            string field = "testString";
            long? count = 38;

            request.As<Completions>().Returns(Task.FromResult(response));

            var result = service.GetAutocompletion(projectId: projectId, prefix: prefix, collectionIds: collectionIds, field: field, count: count);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/autocompletion";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestQueryNoticesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'matching_results': 15, 'notices': [{'notice_id': 'NoticeId', 'created': '2019-01-01T12:00:00', 'document_id': 'DocumentId', 'collection_id': 'CollectionId', 'query_id': 'QueryId', 'severity': 'warning', 'step': 'Step', 'description': 'Description'}]}";
            var response = new DetailedResponse<QueryNoticesResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<QueryNoticesResponse>(responseJson),
                StatusCode = 200
            };

            string projectId = "testString";
            string filter = "testString";
            string query = "testString";
            string naturalLanguageQuery = "testString";
            long? count = 38;
            long? offset = 38;

            request.As<QueryNoticesResponse>().Returns(Task.FromResult(response));

            var result = service.QueryNotices(projectId: projectId, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, count: count, offset: offset);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/notices";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListFieldsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'fields': [{'field': '_Field', 'type': 'nested', 'collection_id': 'CollectionId'}]}";
            var response = new DetailedResponse<ListFieldsResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ListFieldsResponse>(responseJson),
                StatusCode = 200
            };

            string projectId = "testString";
            List<string> collectionIds = new List<string> { "testString" };

            request.As<ListFieldsResponse>().Returns(Task.FromResult(response));

            var result = service.ListFields(projectId: projectId, collectionIds: collectionIds);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/fields";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetComponentSettingsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'fields_shown': {'body': {'use_passage': true, 'field': 'Field'}, 'title': {'field': 'Field'}}, 'autocomplete': true, 'structured_search': true, 'results_per_page': 14, 'aggregations': [{'name': 'Name', 'label': 'Label', 'multiple_selections_allowed': false, 'visualization_type': 'auto'}]}";
            var response = new DetailedResponse<ComponentSettingsResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ComponentSettingsResponse>(responseJson),
                StatusCode = 200
            };

            string projectId = "testString";

            request.As<ComponentSettingsResponse>().Returns(Task.FromResult(response));

            var result = service.GetComponentSettings(projectId: projectId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/component_settings";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'document_id': 'DocumentId', 'status': 'processing'}";
            var response = new DetailedResponse<DocumentAccepted>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DocumentAccepted>(responseJson),
                StatusCode = 202
            };

            string projectId = "testString";
            string collectionId = "testString";
            System.IO.MemoryStream file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string filename = "testString";
            string fileContentType = "application/json";
            string metadata = "testString";
            bool? xWatsonDiscoveryForce = true;

            request.As<DocumentAccepted>().Returns(Task.FromResult(response));

            var result = service.AddDocument(projectId: projectId, collectionId: collectionId, file: file, filename: filename, fileContentType: fileContentType, metadata: metadata, xWatsonDiscoveryForce: xWatsonDiscoveryForce);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/collections/{collectionId}/documents";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'document_id': 'DocumentId', 'status': 'processing'}";
            var response = new DetailedResponse<DocumentAccepted>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DocumentAccepted>(responseJson),
                StatusCode = 202
            };

            string projectId = "testString";
            string collectionId = "testString";
            string documentId = "testString";
            System.IO.MemoryStream file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string filename = "testString";
            string fileContentType = "application/json";
            string metadata = "testString";
            bool? xWatsonDiscoveryForce = true;

            request.As<DocumentAccepted>().Returns(Task.FromResult(response));

            var result = service.UpdateDocument(projectId: projectId, collectionId: collectionId, documentId: documentId, file: file, filename: filename, fileContentType: fileContentType, metadata: metadata, xWatsonDiscoveryForce: xWatsonDiscoveryForce);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/collections/{collectionId}/documents/{documentId}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'document_id': 'DocumentId', 'status': 'deleted'}";
            var response = new DetailedResponse<DeleteDocumentResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DeleteDocumentResponse>(responseJson),
                StatusCode = 200
            };

            string projectId = "testString";
            string collectionId = "testString";
            string documentId = "testString";
            bool? xWatsonDiscoveryForce = true;

            request.As<DeleteDocumentResponse>().Returns(Task.FromResult(response));

            var result = service.DeleteDocument(projectId: projectId, collectionId: collectionId, documentId: documentId, xWatsonDiscoveryForce: xWatsonDiscoveryForce);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/collections/{collectionId}/documents/{documentId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListTrainingQueriesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'queries': [{'query_id': 'QueryId', 'natural_language_query': 'NaturalLanguageQuery', 'filter': 'Filter', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'examples': [{'document_id': 'DocumentId', 'collection_id': 'CollectionId', 'relevance': 9, 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00'}]}]}";
            var response = new DetailedResponse<TrainingQuerySet>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingQuerySet>(responseJson),
                StatusCode = 200
            };

            string projectId = "testString";

            request.As<TrainingQuerySet>().Returns(Task.FromResult(response));

            var result = service.ListTrainingQueries(projectId: projectId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteTrainingQueriesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string projectId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteTrainingQueries(projectId: projectId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateTrainingQueryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'query_id': 'QueryId', 'natural_language_query': 'NaturalLanguageQuery', 'filter': 'Filter', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'examples': [{'document_id': 'DocumentId', 'collection_id': 'CollectionId', 'relevance': 9, 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00'}]}";
            var response = new DetailedResponse<TrainingQuery>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingQuery>(responseJson),
                StatusCode = 201
            };

            TrainingExample TrainingExampleModel = new TrainingExample()
            {
                DocumentId = "testString",
                CollectionId = "testString",
                Relevance = 38,
            };
            string projectId = "testString";
            string naturalLanguageQuery = "testString";
            List<TrainingExample> examples = new List<TrainingExample> { TrainingExampleModel };
            string filter = "testString";

            request.As<TrainingQuery>().Returns(Task.FromResult(response));

            var result = service.CreateTrainingQuery(projectId: projectId, naturalLanguageQuery: naturalLanguageQuery, examples: examples, filter: filter);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetTrainingQueryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'query_id': 'QueryId', 'natural_language_query': 'NaturalLanguageQuery', 'filter': 'Filter', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'examples': [{'document_id': 'DocumentId', 'collection_id': 'CollectionId', 'relevance': 9, 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00'}]}";
            var response = new DetailedResponse<TrainingQuery>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingQuery>(responseJson),
                StatusCode = 200
            };

            string projectId = "testString";
            string queryId = "testString";

            request.As<TrainingQuery>().Returns(Task.FromResult(response));

            var result = service.GetTrainingQuery(projectId: projectId, queryId: queryId);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries/{queryId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateTrainingQueryAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            DiscoveryService service = new DiscoveryService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'query_id': 'QueryId', 'natural_language_query': 'NaturalLanguageQuery', 'filter': 'Filter', 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00', 'examples': [{'document_id': 'DocumentId', 'collection_id': 'CollectionId', 'relevance': 9, 'created': '2019-01-01T12:00:00', 'updated': '2019-01-01T12:00:00'}]}";
            var response = new DetailedResponse<TrainingQuery>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingQuery>(responseJson),
                StatusCode = 201
            };

            TrainingExample TrainingExampleModel = new TrainingExample()
            {
                DocumentId = "testString",
                CollectionId = "testString",
                Relevance = 38,
            };
            string projectId = "testString";
            string queryId = "testString";
            string naturalLanguageQuery = "testString";
            List<TrainingExample> examples = new List<TrainingExample> { TrainingExampleModel };
            string filter = "testString";

            request.As<TrainingQuery>().Returns(Task.FromResult(response));

            var result = service.UpdateTrainingQuery(projectId: projectId, queryId: queryId, naturalLanguageQuery: naturalLanguageQuery, examples: examples, filter: filter);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/projects/{projectId}/training_data/queries/{queryId}";
            client.Received().PostAsync(messageUrl);
        }

    }
}
