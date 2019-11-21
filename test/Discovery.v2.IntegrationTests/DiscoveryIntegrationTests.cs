/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Authentication.Bearer;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.Discovery.v2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.Discovery.v2.IntegrationTests
{
    //[TestClass]
    public class DiscoveryIntegrationTests
    {
        private DiscoveryService service;
        private string versionDate = "2019-11-20";
        private string filepathToIngest = @"DiscoveryTestData\watson_beats_jeopardy.html";
        private string metadata = "{\"Creator\": \".NET SDK Test\",\"Subject\": \"Discovery service\"}";
        private string bearerToken = "";
        private string serviceUrl = "";
        private string projectId = "";
        private string collectionId = "";

        [TestInitialize]
        public void Setup()
        {
            Authenticator discoveryAuthenticator = new BearerTokenAuthenticator(bearerToken: bearerToken);
            service = new DiscoveryService(versionDate: versionDate, authenticator: discoveryAuthenticator);
            service.SetServiceUrl(serviceUrl: serviceUrl);
            service.DisableSslVerification(true);
        }

        #region Collections
        [TestMethod]
        public void TestListCollections()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listCollectionResult = service.ListCollections(
                projectId: projectId
                );

            Assert.IsNotNull(listCollectionResult.Result.Collections);
            Assert.IsTrue(listCollectionResult.Result.Collections.Count > 0);
            Assert.IsNotNull(listCollectionResult.Result.Collections[0].CollectionId);
            Assert.IsNotNull(listCollectionResult.Result.Collections[0].Name);
        }
        #endregion

        #region Queries
        [TestMethod]
        public void TestQuery()
        {
            string filter = "entities.text:IBM";
            string query = "relations.action.lemmatized:acquire";
            string aggregation = "filter(enriched_text.concepts.text:cloud computing)";
            long count = 5;
            List<string> _return = new List<string>() { "title", "url" };
            long offset = 1;
            string sort = "sort=enriched_text.sentiment.document.score";
            bool highlight = true;
            bool spellingSuggestions = true;
            QueryLargeTableResults tableResults = new QueryLargeTableResults()
            {
                Enabled = true,
                Count = 3
            };
            QueryLargeSuggestedRefinements suggestedRefinements = new QueryLargeSuggestedRefinements()
            {
                Enabled = true,
                Count = 3
            };
            QueryLargePassages passages = new QueryLargePassages()
            {
                Enabled = true,
                PerDocument = true,
                MaxPerDocument = 3,
                Fields = new List<string>()
                {
                    "text",
                    "abstract",
                    "conclusion"
                },
                Count = 3,
                Characters = 100
            };

            service.WithHeader("X-Watson-Test", "1");
            var queryResult = service.Query(
                projectId: projectId,
                collectionIds: new List<string>() { collectionId },
                filter: filter,
                query: query,
                aggregation: aggregation,
                count: count,
                _return: _return,
                offset: offset,
                sort: sort,
                highlight: highlight,
                spellingSuggestions: spellingSuggestions,
                tableResults: tableResults,
                suggestedRefinements: suggestedRefinements,
                passages: passages
                );

            Assert.IsNotNull(queryResult.Result);
            Assert.IsTrue(queryResult.Result.Aggregations[0].Type == "filter");
            Assert.IsTrue((queryResult.Result.Aggregations[0] as QueryFilterAggregation).Match == "enriched_text.concepts.text:cloud computing");
        }

        [TestMethod]
        public void TestNaturalLanguageQuery()
        {
            string filter = "entities.text:IBM";
            string naturalLanguageQuery = "What is IBM's stock price?";

            service.WithHeader("X-Watson-Test", "1");
            var queryResult = service.Query(
                projectId: projectId,
                collectionIds: new List<string>() { collectionId },
                filter: filter,
                naturalLanguageQuery: naturalLanguageQuery
                );

            Assert.IsNotNull(queryResult.Result);
            Assert.IsNotNull(queryResult.Result.Aggregations[0]);
            Assert.IsTrue((queryResult.Result.Aggregations[0] as QueryTermAggregation).Field == "enriched_text.entities.text");
            Assert.IsTrue((queryResult.Result.Aggregations[0] as QueryTermAggregation).Type == "term");
        }

        [TestMethod]
        public void TestGetAutocompletion()
        {
            service.WithHeader("X-Watson-Test", "1");
            var getAutocompletionResult = service.GetAutocompletion(
                projectId: projectId,
                prefix: "ha"
                );

            Assert.IsNotNull(getAutocompletionResult.Result);
            Assert.IsNotNull(getAutocompletionResult.Result._Completions);
            Assert.IsTrue(getAutocompletionResult.Result._Completions.Count > 0);
        }

        [TestMethod]
        public void TestQueryNotices()
        {
            service.WithHeader("X-Watson-Test", "1");
            var queryNoticesResult0 = service.QueryNotices(
                projectId: projectId
                );

            Assert.IsNotNull(queryNoticesResult0.Result);
            Assert.IsNotNull(queryNoticesResult0.Result.MatchingResults);
            Assert.IsNotNull(queryNoticesResult0.Result.Notices);

            string filter = "entities.text:IBM";
            string query = "relations.action.lemmatized:acquire";

            service.WithHeader("X-Watson-Test", "1");
            var queryNoticesResult1 = service.QueryNotices(
                projectId: projectId,
                filter: filter,
                query: query,
                count: 3,
                offset: 1
                );

            Assert.IsNotNull(queryNoticesResult1.Result);
            Assert.IsNotNull(queryNoticesResult1.Result.MatchingResults);
            Assert.IsNotNull(queryNoticesResult1.Result.Notices);

            string naturalLanguageQuery = "What is IBM's stock price?";

            service.WithHeader("X-Watson-Test", "1");
            var queryNoticesResult2 = service.QueryNotices(
                projectId: projectId,
                filter: filter,
                naturalLanguageQuery: naturalLanguageQuery,
                count: 3,
                offset: 1
                );

            Assert.IsNotNull(queryNoticesResult2.Result);
            Assert.IsNotNull(queryNoticesResult2.Result.MatchingResults);
            Assert.IsNotNull(queryNoticesResult2.Result.Notices);
        }

        [TestMethod]
        public void TestListFields()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listFieldsResult = service.ListFields(
                projectId: projectId
                );

            Assert.IsNotNull(listFieldsResult.Result);
            Assert.IsNotNull(listFieldsResult.Result.Fields);
            Assert.IsTrue(listFieldsResult.Result.Fields.Count > 0);
        }
        #endregion

        #region Component Settings
        [TestMethod]
        public void TestGetComponentSettings()
        {
            service.WithHeader("X-Watson-Test", "1");
            var getComponentSettingsResult = service.GetComponentSettings(
                projectId: projectId
                );

            Assert.IsNotNull(getComponentSettingsResult.Result);
            Assert.IsNotNull(getComponentSettingsResult.Result.ResultsPerPage);
            Assert.IsNotNull(getComponentSettingsResult.Result.FieldsShown);
            Assert.IsNotNull(getComponentSettingsResult.Result.Aggregations);
            Assert.IsNotNull(getComponentSettingsResult.Result.Autocomplete);
        }
        #endregion

        #region Documents
        [TestMethod]
        public void TestAddDeleteDocument()
        {
            DetailedResponse<DocumentAccepted> addDocumentResult = null;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);

                    service.WithHeader("X-Watson-Test", "1");
                    addDocumentResult = service.AddDocument(
                        projectId: projectId,
                        collectionId: collectionId,
                        file: ms,
                        filename: "watson_beats_jeopardy.html",
                        fileContentType: "text/html",
                        metadata: metadata,
                        xWatsonDiscoveryForce: false
                        );
                }
            }

            Assert.IsNotNull(addDocumentResult.Result);
            Assert.IsNotNull(addDocumentResult.Result.DocumentId);
            Assert.IsNotNull(addDocumentResult.Result.Status);

            var documentId = addDocumentResult.Result.DocumentId;

            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(
                projectId: projectId,
                collectionId: collectionId,
                documentId: documentId,
                xWatsonDiscoveryForce: false
                );

            Assert.IsNotNull(deleteDocumentResult.Result);
            Assert.IsNotNull(deleteDocumentResult.Result.DocumentId);
            Assert.IsNotNull(deleteDocumentResult.Result.Status);
        }
        #endregion

        #region Training Data
        [TestMethod]
        public void TestTrainingQueries()
        {
            DetailedResponse<DocumentAccepted> addDocumentResult = null;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);

                    service.WithHeader("X-Watson-Test", "1");
                    addDocumentResult = service.AddDocument(
                        projectId: projectId,
                        collectionId: collectionId,
                        file: ms,
                        filename: "watson_beats_jeopardy.html",
                        fileContentType: "text/html",
                        metadata: metadata,
                        xWatsonDiscoveryForce: false
                        );
                }
            }

            var documentId = addDocumentResult.Result.DocumentId;

            service.WithHeader("X-Watson-Test", "1");
            var listTrainingQueriesResult = service.ListTrainingQueries(
                projectId: projectId
                );

            Assert.IsNotNull(listTrainingQueriesResult.Result);
            Assert.IsNotNull(listTrainingQueriesResult.Result.Queries);

            var naturalLanguageQuery = "What is IBM's stock price?";
            var filters = "entities.text:IBM";
            var examples = new List<TrainingExample>()
            {
                new TrainingExample()
                {
                    DocumentId = documentId,
                    CollectionId = collectionId,
                    Relevance = 1

                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var createTrainingQueryResult = service.CreateTrainingQuery(
                projectId: projectId,
                naturalLanguageQuery: naturalLanguageQuery,
                filter: filters,
                examples: examples
                );

            var queryId = createTrainingQueryResult.Result.QueryId;
            Assert.IsNotNull(createTrainingQueryResult.Result);
            Assert.IsNotNull(createTrainingQueryResult.Result.QueryId);
            Assert.IsNotNull(createTrainingQueryResult.Result.Created);
            Assert.IsNotNull(createTrainingQueryResult.Result.Updated);
            Assert.IsNotNull(createTrainingQueryResult.Result.NaturalLanguageQuery);
            Assert.IsNotNull(createTrainingQueryResult.Result.Examples);
            Assert.IsTrue(createTrainingQueryResult.Result.NaturalLanguageQuery == naturalLanguageQuery);

            service.WithHeader("X-Watson-Test", "1");
            var getTrainingQueryResult = service.GetTrainingQuery(
                projectId: projectId,
                queryId: queryId
                );

            Assert.IsNotNull(getTrainingQueryResult.Result);
            Assert.IsTrue(getTrainingQueryResult.Result.QueryId == queryId);
            Assert.IsTrue(getTrainingQueryResult.Result.NaturalLanguageQuery == naturalLanguageQuery);

            var updatedNaturalLanguageQuery = "Who did Watson beat on Jeopardy?";
            var updatedFilter = "entities.text:Jeopardy";
            var updatedExamples = new List<TrainingExample>()
            {
                new TrainingExample()
                {
                    DocumentId = documentId,
                    CollectionId = collectionId,
                    Relevance = 2

                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var updateTrainingQueryResult = service.UpdateTrainingQuery(
                projectId: projectId,
                queryId: queryId,
                naturalLanguageQuery: updatedNaturalLanguageQuery,
                filter: updatedFilter,
                examples: updatedExamples
                );

            queryId = updateTrainingQueryResult.Result.QueryId;

            Assert.IsTrue(updateTrainingQueryResult.Result.QueryId == queryId);
            Assert.IsTrue(updateTrainingQueryResult.Result.NaturalLanguageQuery == updatedNaturalLanguageQuery);

            service.WithHeader("X-Watson-Test", "1");
            var deleteTrainingQueryResult = service.DeleteTrainingQueries(
                projectId: projectId
                );

            Assert.IsTrue(deleteTrainingQueryResult.StatusCode == 204);

            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(
                projectId: projectId,
                collectionId: collectionId,
                documentId: documentId,
                xWatsonDiscoveryForce: false
                );
        }
        #endregion
    }
}
