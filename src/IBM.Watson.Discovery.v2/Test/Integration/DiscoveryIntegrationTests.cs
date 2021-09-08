/**
* (C) Copyright IBM Corp. 2019, 2021.
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
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.Discovery.v2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.Discovery.v2.IntegrationTests
{
    //[TestClass]
    public class DiscoveryIntegrationTests
    {
        private DiscoveryService service;
        private string versionDate = "2019-11-22";
        private string filepathToIngest = @"DiscoveryTestData/watson_beats_jeopardy.html";
        private string filepathAnalyzeDoc = @"DiscoveryTestData/problem.json";
        private string enrichmentFile = @"DiscoveryTestData/test.csv";
        private string testPDF = @"DiscoveryTestData/test-pdf.pdf";
        private string metadata = "{\"Creator\": \".NET SDK Test\",\"Subject\": \"Discovery service\"}";
        private string bearerToken = "";
        private string serviceUrl = "";
        private string projectId = "";
        private string collectionId = "";

        [TestInitialize]
        public void Setup()
        {
            Authenticator discoveryAuthenticator = new BearerTokenAuthenticator(bearerToken: bearerToken);
            service = new DiscoveryService(version: versionDate, authenticator: discoveryAuthenticator);
            service.SetServiceUrl(serviceUrl: serviceUrl);
            service.DisableSslVerification(true);
            //service = new DiscoveryService(versionDate);
            //service.SetServiceUrl(serviceUrl);
            var creds = CredentialUtils.GetServiceProperties("discovery");
            creds.TryGetValue("PROJECT_ID", out projectId);
            creds.TryGetValue("COLLECTION_ID", out collectionId);
        }

        #region QueryPassages
        [TestMethod]
        public void TestQueryPassagesPerDocument()
        {
            var queryResult = service.Query(
                projectId: projectId,
                collectionIds: new List<string> { collectionId },
                passages: new QueryLargePassages() { PerDocument = true },
                query: "text:document",
                count: 2);

            Assert.IsNotNull(queryResult.Result.Results[0].DocumentPassages[0].PassageText);
        }

        [TestMethod]
        public void TestQueryPassages()
        {
            var queryResult = service.Query(
                projectId: projectId,
                collectionIds: new List<string> { collectionId },
                passages: new QueryLargePassages() { PerDocument = false },
                query: "text:document",
                count: 2);

            Assert.IsNotNull(queryResult.Result.Passages[0].CollectionId);
            Assert.IsNotNull(queryResult.Result.Passages[0].PassageText);
            Assert.IsNotNull(queryResult.Result.Passages[0].DocumentId);
        }
        #endregion

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
                prefix: "pd"
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

        [TestMethod]
        public void TestAnalyzeDocument()
        {
            using (FileStream fs = File.OpenRead(filepathAnalyzeDoc))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    var response = service.AnalyzeDocument(
                        projectId: projectId,
                        collectionId: collectionId,
                        file: ms,
                        filename: "problem.json",
                        fileContentType: DiscoveryService.AnalyzeDocumentEnums.FileContentTypeValue.APPLICATION_JSON,
                        metadata: "{ \"metadata\": \"value\" }"
                        );
                    Assert.IsNotNull(response);
                    Assert.IsNotNull(response.Result);
                }
            }
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

        #region Create Collection
        //[TestMethod]
        public void TestCreateCollection()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                projectId: projectId,
                name: "name test",
                description: "description test",
                language: "en"
                );

            Assert.IsNotNull(createCollectionResult.Response);
            Assert.IsNotNull(createCollectionResult.Result.CollectionId);
            Assert.IsTrue(createCollectionResult.Result.Name == "name test");
            Assert.IsTrue(createCollectionResult.Result.Description == "description test");
            Assert.IsTrue(createCollectionResult.Result.Language == "en");

            // Delete collection
            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                projectId: projectId,
                collectionId: createCollectionResult.Result.CollectionId
                );

            Assert.IsNotNull(deleteCollectionResult.Response);
            Assert.IsTrue(deleteCollectionResult.StatusCode == 204);
        }
        #endregion

        #region Get Collection
        //[TestMethod]
        public void TestGetCollection()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listCollectionsResult = service.ListCollections(
                projectId: projectId
                );

            var collections = listCollectionsResult.Result.Collections;
            if (collections.Count > 0)
            {

                var getCollectionResult = service.GetCollection(
                    projectId: projectId,
                    collectionId: collections[0].CollectionId

                    );

                Assert.IsNotNull(getCollectionResult.Response);
                Assert.IsNotNull(getCollectionResult.Result.CollectionId);
                Assert.IsTrue(getCollectionResult.Result.Name == collections[0].Name);
            }
        }
        #endregion

        #region Update Collection
        //[TestMethod]
        public void TestUpdateCollection()
        {
            // Get collection
            service.WithHeader("X-Watson-Test", "1");
            var getCollectionResult = service.GetCollection(
                projectId: projectId,
                collectionId: collectionId
                );

            Assert.IsNotNull(getCollectionResult.Response);
            Assert.IsNotNull(getCollectionResult.Result.CollectionId);

            // Update collection
            var updateCollectionResult = service.UpdateCollection(
                projectId: projectId,
                collectionId: collectionId,
                name: "name updated",
                description: "description updated"
                );

            Assert.IsNotNull(updateCollectionResult.Response);
            Assert.IsNotNull(updateCollectionResult.Result.CollectionId);
            Assert.IsTrue(updateCollectionResult.Result.Name == "name updated");
            Assert.IsTrue(updateCollectionResult.Result.Description == "description updated");

            // Rollback collection
            var rollbackCollectionResult = service.UpdateCollection(
                projectId: projectId,
                collectionId: collectionId,
                name: getCollectionResult.Result.Name,
                description: getCollectionResult.Result.Description
                );

            Assert.IsNotNull(updateCollectionResult.Response);
            Assert.IsNotNull(updateCollectionResult.Result.CollectionId);
        }
        #endregion

        #region Delete Collection
        //[TestMethod]
        public void TestDeleteCollection()
        {
            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                projectId: projectId,
                collectionId: "{collectionId}"
                );

            Assert.IsNotNull(deleteCollectionResult.Response);
            Assert.IsTrue(deleteCollectionResult.StatusCode == 204);
        }
        #endregion

        #region List Enrichments
        //[TestMethod]
        public void TestListEnrichments()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listEnrichmentsResult = service.ListEnrichments(
                projectId: projectId
                );

            Assert.IsNotNull(listEnrichmentsResult.Response);
            Assert.IsTrue(listEnrichmentsResult.Result._Enrichments.Count > 0);
        }
        #endregion

        #region Create Enrichment
        //[TestMethod]
        public void TestCreateEnrichment()
        {
            // Create Enrichment
            CreateEnrichment createEnrichment = new CreateEnrichment();
            createEnrichment.Name = "Dictionary1";
            createEnrichment.Description = "test dictionary";
            createEnrichment.Type = CreateEnrichment.TypeEnumValue.DICTIONARY;
            createEnrichment.Options = new EnrichmentOptions();
            createEnrichment.Options.Languages = new List<string>();
            createEnrichment.Options.Languages.Add("en");
            createEnrichment.Options.EntityType = "keyword";

            service.WithHeader("X-Watson-Test", "1");
            using (FileStream fs = File.OpenRead(enrichmentFile))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    var createEnrichmentResult = service.CreateEnrichment(
                        projectId: projectId,
                        enrichment: createEnrichment,
                        file: ms
                        );

                    Assert.IsNotNull(createEnrichmentResult.Response);

                    // Delete Enrichment
                    var deleteEnrichmentResult = service.DeleteEnrichment(
                        projectId: projectId,
                        enrichmentId: createEnrichmentResult.Result.EnrichmentId
                        );

                    Assert.IsNotNull(deleteEnrichmentResult);
                    Assert.IsTrue(deleteEnrichmentResult.StatusCode == 204);
                }
            }
        }
        #endregion

        #region Get Enrichment
        //[TestMethod]
        public void TestGetEnrichment()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listEnrichmentsResult = service.ListEnrichments(
                projectId: projectId
                );

            List<Enrichment> availableEnrichments = listEnrichmentsResult.Result._Enrichments;

            if (availableEnrichments.Count == 0)
            {
                Console.WriteLine("No enrichments available.");
            }
            else
            {
                var getEnrichmentResult = service.GetEnrichment(
                    projectId: projectId,
                    enrichmentId: availableEnrichments[0].EnrichmentId
                    );

                Assert.IsNotNull(getEnrichmentResult.Response);
                Assert.IsTrue(getEnrichmentResult.Result.Name == availableEnrichments[0].Name);
                Assert.IsTrue(getEnrichmentResult.Result.Description == availableEnrichments[0].Description);
                Assert.IsTrue(getEnrichmentResult.Result.Type == availableEnrichments[0].Type);
            }
        }
        #endregion

        #region Update Enrichment
        //[TestMethod]
        public void TestUpdateEnrichment()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listEnrichmentsResult = service.ListEnrichments(
                projectId: projectId
                );

            List<Enrichment> availableEnrichments = listEnrichmentsResult.Result._Enrichments;

            if (availableEnrichments.Count == 0)
            {
                Console.WriteLine("No enrichments available.");
            }
            else
            {
                // Update Enrichment
                var updateEnrichmentResult = service.UpdateEnrichment(
                    projectId: projectId,
                    enrichmentId: availableEnrichments[0].EnrichmentId,
                    name: "name updated",
                    description: "description updated"
                    );

                Assert.IsNotNull(updateEnrichmentResult.Response);
                Assert.IsTrue(updateEnrichmentResult.Result.Name == "name updated");
                Assert.IsTrue(updateEnrichmentResult.Result.Description == "description updated");

                // Rollback Enrichment
                var rollbackEnrichmentResult = service.UpdateEnrichment(
                    projectId: projectId,
                    enrichmentId: availableEnrichments[0].EnrichmentId,
                    name: availableEnrichments[0].Name,
                    description: availableEnrichments[0].Description
                    );

                Assert.IsNotNull(rollbackEnrichmentResult.Response);
            }
        }
        #endregion

        #region Delete Enrichment
        //[TestMethod]
        public void TestDeleteEnrichment()
        {
            service.WithHeader("X-Watson-Test", "1");
            var deleteEnrichmentResult = service.DeleteEnrichment(
                projectId: projectId,
                enrichmentId: "{enrichmentId}"
                );

            Assert.IsNotNull(deleteEnrichmentResult.Response);
            Assert.IsTrue(deleteEnrichmentResult.StatusCode == 204);
        }
        #endregion

        #region List Projects
        [TestMethod]
        public void TestListProjects()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listProjectsResult = service.ListProjects();

            Assert.IsNotNull(listProjectsResult.Response);
            Assert.IsNotNull(listProjectsResult.Result.Projects);
        }
        #endregion

        #region Create Project
        //[TestMethod]
        public void TestCreateProject()
        {
            // Create Project
            service.WithHeader("X-Watson-Test", "1");
            var createProjectResult = service.CreateProject(
                name: "name test",
                type: "other"
                );

            Assert.IsNotNull(createProjectResult.Response);
            Assert.IsTrue(createProjectResult.Result.Name == "name test");
            Assert.IsTrue(createProjectResult.Result.Type == "other");

            // Delete Project
            service.WithHeader("X-Watson-Test", "1");
            var deleteProjectResult = service.DeleteProject(
                projectId: createProjectResult.Result.ProjectId
                );

            Assert.IsNotNull(deleteProjectResult.Response);
            Assert.IsTrue(deleteProjectResult.StatusCode == 204);
        }
        #endregion

        #region Get Project
        //[TestMethod]
        public void TestGetProject()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listProjectsResult = service.ListProjects();

            var projects = listProjectsResult.Result.Projects;
            if (projects.Count > 0)
            {
                var getProjectResult = service.GetProject(
                    projectId: projects[0].ProjectId
                    );

                Assert.IsNotNull(getProjectResult.Response);
                Assert.IsTrue(getProjectResult.Result.Name == projects[0].Name);
                Assert.IsTrue(getProjectResult.Result.Type == projects[0].Type);
            }
        }
        #endregion

        #region Update Project
        //[TestMethod]
        public void TestUpdateProject()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listProjectsResult = service.ListProjects();

            var projects = listProjectsResult.Result.Projects;

            if (projects.Count > 0)
            {
                // Update Project
                var updateProjectResult = service.UpdateProject(
                    projectId: projects[0].ProjectId,
                    name: "name updated"
                    );

                Assert.IsNotNull(updateProjectResult.Response);
                Assert.IsTrue(updateProjectResult.Result.Name == "name updated");
                Assert.IsTrue(updateProjectResult.Result.ProjectId == projects[0].ProjectId);

                // Rollback Project
                var rollbackProjectResult = service.UpdateProject(
                    projectId: projects[0].ProjectId,
                    name: projects[0].Name
                    );

                Assert.IsNotNull(rollbackProjectResult.Response);
            }

        }
        #endregion

        #region Delete Project
        //[TestMethod]
        public void TestDeleteProject()
        {
            service.WithHeader("X-Watson-Test", "1");
            var deleteProjectResult = service.DeleteProject(
                projectId: "{projectId}"
                );

            Assert.IsNotNull(deleteProjectResult.Response);
            Assert.IsTrue(deleteProjectResult.StatusCode == 204);
        }
        #endregion

        #region Delete User Data
        //[TestMethod]
        public void TestDeleteUserData()
        {
            service.WithHeader("X-Watson-Test", "1");
            var deleteUserDataResults = service.DeleteUserData(
                customerId: "{customerId}"
                );

            Assert.IsNotNull(deleteUserDataResults.Response);
            Assert.IsTrue(deleteUserDataResults.StatusCode == 204);
        }
        #endregion

        #region Miscellaneous
        [TestMethod]
        public void TestQueryCollectionNotices()
        {
            service.WithHeader("X-Watson-Test", "1");

            var response = service.QueryCollectionNotices(
                projectId: projectId,
                collectionId: collectionId,
                naturalLanguageQuery: "warning"
                );
            Assert.IsNotNull(response.Result.Notices);
        }

        [TestMethod]
        public void TestDeleteTrainingQuery()
        {
            service.WithHeader("X-Watson-Test", "1");
            var documentId = "";
            var queryId = "";

            try
            {
                using (FileStream fs = File.OpenRead(enrichmentFile))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        var addResponse = service.AddDocument(
                            projectId: projectId,
                            collectionId: collectionId,
                            file: ms,
                            filename: "test-file",
                            fileContentType: DiscoveryService.AddDocumentEnums.FileContentTypeValue.APPLICATION_PDF,
                            xWatsonDiscoveryForce: true
                            );
                        Assert.IsNotNull(addResponse);

                        documentId = addResponse.Result.DocumentId;

                        TrainingExample trainingExample = new TrainingExample
                        {
                            CollectionId = collectionId,
                            DocumentId = documentId,
                            Relevance = 1L
                        };

                        List<TrainingExample> examples = new List<TrainingExample>();
                        examples.Add(trainingExample);
                        var createResponse = service.CreateTrainingQuery(
                            projectId: projectId,
                            examples: examples,
                            naturalLanguageQuery: "test query"
                            );

                        queryId = createResponse.Result.QueryId;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                service.DeleteTrainingQuery(
                    projectId: projectId,
                    queryId: queryId
                    );

                service.DeleteDocument(
                    projectId: projectId,
                    collectionId: collectionId,
                    documentId: documentId,
                    xWatsonDiscoveryForce: true
                    );
            }
        }
        #endregion
    }
}