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

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using Newtonsoft.Json;
using IBM.Watson.Discovery.v1.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Environment = IBM.Watson.Discovery.v1.Model.Environment;
using IBM.Cloud.SDK.Core.Util;
using Newtonsoft.Json.Linq;
using IBM.Cloud.SDK.Core;
using IBM.Cloud.SDK.Core.Http;

namespace IBM.Watson.Discovery.v1.IntegrationTests
{
    [TestClass]
    public class DiscoveryIntegrationTests
    {
        private DiscoveryService service;
        private static string endpoint;
        private static string apikey;
        private static string credentials = string.Empty;
        private static string version = "2019-01-15";

        private static string environmentId;
        private static string createdConfigurationId;
        private static string createdDocumentId;
        private static string queryId;
        private static string exampleId;

        private string createdConfigurationName;
        private string updatedConfigurationName;
        private string createdConfigurationDescription = "configDescription - safe to delete";
        private string filepathToIngest = @"DiscoveryTestData\watson_beats_jeopardy.html";
        private string metadata = "{\"Creator\": \"DotnetSDK Test\",\"Subject\": \"Discovery service\"}";
        private string stopwordFileToIngest = @"DiscoveryTestData\stopwords.txt";

        private string createdCollectionName;
        private string createdCollectionLanguage = "en";
        private string createdCollectionDescription = "createdCollectionDescription - safe to delete";
        private string updatedCollectionName;
        private string dotnetGatewayName = "dotnet-sdk-integration-test-gateway";


        private string naturalLanguageQuery = "Who beat Ken Jennings in Jeopardy!";
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";
                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                var vcapServices = JObject.Parse(credentials);

                Credential credential = vcapCredentials.GetCredentialByname("discovery-sdk")[0].Credentials;
                endpoint = credential.Url;
                apikey = credential.IamApikey;
            }
            #endregion

            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = endpoint
            };

            service = new DiscoveryService(tokenOptions, version);
            service.SetEndpoint(endpoint);

            var environments = service.ListEnvironments();
            environmentId = environments.Result.Environments[1].EnvironmentId;

            createdConfigurationName = Guid.NewGuid().ToString();
            updatedConfigurationName = createdConfigurationName + "-updated";
            createdCollectionName = Guid.NewGuid().ToString();
            updatedCollectionName = createdCollectionName + "-updated";
        }

        #region Configurations
        [TestMethod]
        public void TestConfigurations_Success()
        {
            var listConfigurationsResults = service.ListConfigurations(environmentId);

            List<Enrichment> enrichments = new List<Enrichment>()
            {
                new Enrichment()
                {
                    Options = new EnrichmentOptions()
                    {
                        Features = new NluEnrichmentFeatures()
                        {
                            Concepts = new NluEnrichmentConcepts()
                            {
                                Limit = 10
                            }
                        }
                    }
                }
            };

            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription,
                enrichments: enrichments
                );

            createdConfigurationId = createConfigurationResults.Result.ConfigurationId;

            var getConfigurationResults = service.GetConfiguration(
                environmentId: environmentId,
                configurationId: createdConfigurationId
                );

            Configuration updateConfiguration = new Configuration()
            {
                Name = updatedConfigurationName
            };

            var updateConfigurationResults = service.UpdateConfiguration(
                environmentId: environmentId,
                configurationId: createdConfigurationId,
                name: updatedConfigurationName
                );

            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: createdConfigurationId
                );

            Assert.IsNotNull(deleteConfigurationResults);
            Assert.IsTrue(deleteConfigurationResults.Result.Status == DeleteConfigurationResponse.StatusEnumValue.DELETED);
            Assert.IsNotNull(updateConfigurationResults);
            Assert.IsTrue(updateConfigurationResults.Result.ConfigurationId == createdConfigurationId);
            Assert.IsTrue(updateConfigurationResults.Result.Description == createdConfigurationDescription);
            Assert.IsNotNull(getConfigurationResults);
            Assert.IsTrue(getConfigurationResults.Result.ConfigurationId == createdConfigurationId);
            Assert.IsTrue(getConfigurationResults.Result.Description == createdConfigurationDescription);
            Assert.IsTrue(createConfigurationResults.Result.Enrichments[0].Options.Features.Concepts.Limit == 10);
            Assert.IsNotNull(createConfigurationResults);
            Assert.IsTrue(createConfigurationResults.Result.Description == createdConfigurationDescription);
            Assert.IsNotNull(listConfigurationsResults);
            Assert.IsNotNull(listConfigurationsResults.Result.Configurations);
            Assert.IsTrue(listConfigurationsResults.Result.Configurations.Count > 0);

            createdConfigurationId = null;
            environmentId = null;
        }
        #endregion

        #region Collections
        [TestMethod]
        public void TestCollections_Success()
        {
            Configuration configuration = new Configuration()
            {
                Name = createdConfigurationName,
                Description = createdConfigurationDescription
            };

            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            createdConfigurationId = createConfigurationResults.Result.ConfigurationId;

            var listCollectionsResult = service.ListCollections(
                environmentId: environmentId
                );

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: createdConfigurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            var getCollectionResult = service.GetCollection(environmentId, collectionId);

            var updateCollectionResult = service.UpdateCollection(
                environmentId: environmentId,
                collectionId: collectionId,
                name: updatedCollectionName,
                description: createdCollectionDescription,
                configurationId: createdConfigurationId
                );

            var listCollectionFieldsResult = service.ListCollectionFields(
                environmentId: environmentId,
                collectionId: collectionId
                );

            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: createdConfigurationId
                );

            Assert.IsNotNull(deleteCollectionResult);
            Assert.IsTrue(deleteCollectionResult.Result.Status == DeleteCollectionResponse.StatusEnumValue.DELETED);
            Assert.IsNotNull(listCollectionFieldsResult);
            Assert.IsNotNull(updateCollectionResult);
            Assert.IsTrue(updateCollectionResult.Result.CollectionId == collectionId);
            Assert.IsNotNull(getCollectionResult);
            Assert.IsTrue(getCollectionResult.Result.CollectionId == collectionId);
            Assert.IsTrue(getCollectionResult.Result.Description == createdCollectionDescription);
            Assert.IsNotNull(createCollectionResult);
            Assert.IsTrue(createCollectionResult.Result.Description == createdCollectionDescription);
            Assert.IsNotNull(listCollectionsResult);

            environmentId = null;
            createdConfigurationId = null;
            collectionId = null;
        }
        #endregion

        #region Preview Environment
        [TestMethod]
        public void PreviewEnvironment()
        {
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            createdConfigurationId = createConfigurationResults.Result.ConfigurationId;

            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    var testConfigurationInEnvironmentResult = service.TestConfigurationInEnvironment(
                        environmentId: environmentId,
                        configurationId: createdConfigurationId,
                        file: ms, filename: "watson_beats_jeopardy.html",
                        fileContentType: "text/html"
                        );

                    var deleteConfigurationResults = service.DeleteConfiguration(
                        environmentId: environmentId,
                        configurationId: createdConfigurationId
                        );

                    Assert.IsNotNull(testConfigurationInEnvironmentResult.Result);
                    Assert.IsNotNull(testConfigurationInEnvironmentResult.Result.Status);
                }
            }


        }
        #endregion

        #region Documents
        [TestMethod]
        public void TestDocuments_Success()
        {
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            createdConfigurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: createdConfigurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                    createdDocumentId = addDocumentResult.Result.DocumentId;
                }
            }

            var getDocumentStatusResult = service.GetDocumentStatus(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: createdDocumentId
                );

            DetailedResponse<DocumentAccepted> updateDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    updateDocumentResult = service.UpdateDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    documentId: createdDocumentId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata);
                }
            }

            var deleteDocumentResult = service.DeleteDocument(environmentId, collectionId, createdDocumentId);
            var deleteCollectionResult = service.DeleteCollection(environmentId, collectionId);
            var deleteConfigurationResults = service.DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsNotNull(deleteDocumentResult);
            Assert.IsTrue(deleteDocumentResult.Result.Status == DeleteDocumentResponse.StatusEnumValue.DELETED);
            Assert.IsNotNull(updateDocumentResult);
            Assert.IsTrue(updateDocumentResult.Result.DocumentId == createdDocumentId);
            Assert.IsNotNull(getDocumentStatusResult);
            Assert.IsTrue(getDocumentStatusResult.Result.DocumentId == createdDocumentId);
            Assert.IsNotNull(addDocumentResult);

            createdDocumentId = null;
            collectionId = null;
            createdConfigurationId = null;
            environmentId = null;
        }
        #endregion

        #region Query
        [TestMethod]
        public void TestQuery()
        {
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            createdConfigurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: createdConfigurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                    createdDocumentId = addDocumentResult.Result.DocumentId;
                }
            }

            var queryResult = service.Query(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                returnFields: "extracted_metadata.sha1"
                );

            var deleteDocumentResult = service.DeleteDocument(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: createdDocumentId
                );

            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );

            var deleteConfigurationResults = service.DeleteConfiguration(environmentId: environmentId,
                configurationId: createdConfigurationId
                );

            Assert.IsNotNull(queryResult);

            createdDocumentId = null;
            collectionId = null;
            createdConfigurationId = null;
            environmentId = null;
        }
        #endregion

        #region Notices
        [TestMethod]
        public void TestGetNotices()
        {
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            createdConfigurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: createdConfigurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                    createdDocumentId = addDocumentResult.Result.DocumentId;
                }
            }

            var queryResult = service.Query(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery
                );

            var queryNoticesResult = service.QueryNotices(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                passages: true
                );

            var deleteDocumentResult = service.DeleteDocument(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: createdDocumentId
                );

            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );

            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: createdConfigurationId
                );

            Assert.IsNotNull(queryNoticesResult.Result);
            Assert.IsNotNull(queryNoticesResult.Result.MatchingResults);

            environmentId = null;
            createdConfigurationId = null;
            createdDocumentId = null;
        }
        #endregion

        #region Training Data
        [TestMethod]
        public void TestTrainingData()
        {
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            createdConfigurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: createdConfigurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                    createdDocumentId = addDocumentResult.Result.DocumentId;
                }
            }

            var listTrainingDataResult = service.ListTrainingData(
                environmentId: environmentId,
                collectionId: collectionId
                );

            var examples = new List<TrainingExample>()
            {
                new TrainingExample()
                {
                    DocumentId = "documentId",
                    CrossReference = "crossReference",
                    Relevance = 1
                }
            };

            var addTrainingDataResult = service.AddTrainingData(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery, 
                filter: "filter", 
                examples: examples
                );
            queryId = addTrainingDataResult.Result.QueryId;

            var getTrainingDataResult = service.GetTrainingData(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId
                );

            var trainingExample = new TrainingExample()
            {
                DocumentId = createdDocumentId,
                Relevance = 1
            };

            var createTrainingExampleResult = service.CreateTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId, 
                documentId: createdDocumentId, 
                relevance: 1
                ); 
            exampleId = createTrainingExampleResult.Result.DocumentId;

            var getTrainingExampleResult = service.GetTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId, 
                exampleId: exampleId
                );

            var updateTrainingExampleResult = service.UpdateTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                exampleId: exampleId, 
                crossReference: "crossReference", 
                relevance: 1
                );

            var deleteTrainingExampleResult = service.DeleteTrainingExample(environmentId, collectionId, queryId, exampleId);
            var deleteTrainingDataResult = service.DeleteTrainingData(environmentId, collectionId, queryId);
            var deleteAllTrainingDataResult = service.DeleteAllTrainingData(environmentId, collectionId);
            var deleteDocumentResult = service.DeleteDocument(environmentId, collectionId, createdDocumentId);
            var deleteCollectionResult = service.DeleteCollection(environmentId, collectionId);
            var deleteConfigurationResults = service.DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsTrue(deleteAllTrainingDataResult.StatusCode == 204);
            Assert.IsTrue(deleteTrainingDataResult.StatusCode == 204);
            Assert.IsTrue(deleteTrainingExampleResult.StatusCode == 204);
            Assert.IsNotNull(updateTrainingExampleResult.Result);
            Assert.IsTrue(updateTrainingExampleResult.Result.CrossReference == "crossReference");
            Assert.IsNotNull(getTrainingExampleResult.Result);
            Assert.IsTrue(getTrainingExampleResult.Result.DocumentId == createdDocumentId);
            Assert.IsNotNull(createTrainingExampleResult.Result);
            Assert.IsTrue(createTrainingExampleResult.Result.DocumentId == createdDocumentId);
            Assert.IsNotNull(getTrainingDataResult.Result);
            Assert.IsTrue(getTrainingDataResult.Result.QueryId == queryId);
            Assert.IsNotNull(addTrainingDataResult.Result);
            Assert.IsTrue(addTrainingDataResult.Result.NaturalLanguageQuery == naturalLanguageQuery);
            Assert.IsNotNull(listTrainingDataResult.Result);
            Assert.IsTrue(listTrainingDataResult.Result.EnvironmentId == environmentId);

            exampleId = null;
            queryId = null;
            createdConfigurationId = null;
            environmentId = null;
            createdDocumentId = null;
        }
        #endregion

        //#region Credentials
        ////[TestMethod]
        //public void TestCredentials_Success()
        //{
        //    var listCredentialsResult = ListCredentials(environmentId);

        //    Model.Credentials credentials = new Model.Credentials()
        //    {
        //        SourceType = Model.Credentials.SourceTypeEnum.BOX,
        //        CredentialDetails = new CredentialDetails()
        //        {
        //            CredentialType = CredentialDetails.CredentialTypeEnum.OAUTH2,
        //            EnterpriseId = "myEnterpriseId",
        //            ClientId = "myClientId",
        //            ClientSecret = "myClientSecret",
        //            PublicKeyId = "myPublicIdKey",
        //            Passphrase = "myPassphrase",
        //            PrivateKey = "myPrivateKey"
        //        }
        //    };

        //    var createCredentialsResult = CreateCredentials(environmentId, credentials);
        //    string credentialId = createCredentialsResult.CredentialId;

        //    var getCredentialResult = GetCredentials(environmentId, credentialId);
        //    string privateKey = "privatekey";
        //    var privateKeyBytes = System.Text.Encoding.UTF8.GetBytes(privateKey);
        //    var base64PrivateKey = System.Convert.ToBase64String(privateKeyBytes);

        //    Model.Credentials updatedCredentials = new Model.Credentials()
        //    {
        //        SourceType = Model.Credentials.SourceTypeEnum.BOX,
        //        CredentialDetails = new CredentialDetails()
        //        {
        //            CredentialType = CredentialDetails.CredentialTypeEnum.OAUTH2,
        //            EnterpriseId = "myEnterpriseIdUpdated",
        //            ClientId = "myClientIdUpdated",
        //            ClientSecret = "myClientSecretUpdated",
        //            PublicKeyId = "myPublicIdKeyUpdated",
        //            Passphrase = "myPassphraseUpdated",
        //            PrivateKey = base64PrivateKey
        //        }
        //    };

        //    //var updateCredentialResult = UpdateCredentials(_environmentId, credentialId, updatedCredentials);

        //    var deleteCredentialsResult = DeleteCredentials(environmentId, credentialId);

        //    Assert.IsNotNull(listCredentialsResult);
        //    Assert.IsTrue(!string.IsNullOrEmpty(listCredentialsResult.ResponseJson));
        //    Assert.IsNotNull(createCredentialsResult);
        //    Assert.IsTrue(!string.IsNullOrEmpty(createCredentialsResult.CredentialId));
        //    Assert.IsTrue(createCredentialsResult.SourceType == Credentials.SourceTypeEnum.BOX);
        //    Assert.IsTrue(createCredentialsResult.CredentialDetails.CredentialType == CredentialDetails.CredentialTypeEnum.OAUTH2);
        //    Assert.IsTrue(createCredentialsResult.CredentialDetails.EnterpriseId == "myEnterpriseId");
        //    Assert.IsNotNull(getCredentialResult);
        //    Assert.IsTrue(getCredentialResult.SourceType == Credentials.SourceTypeEnum.BOX);
        //    Assert.IsTrue(getCredentialResult.CredentialDetails.CredentialType == CredentialDetails.CredentialTypeEnum.OAUTH2);
        //    Assert.IsTrue(getCredentialResult.CredentialDetails.EnterpriseId == "myEnterpriseId");
        //    //Assert.IsNotNull(updateCredentialResult);
        //    //Assert.IsTrue(updateCredentialResult.CredentialDetails.EnterpriseId == "myEnterpriseIdUpdated");
        //    Assert.IsNotNull(deleteCredentialsResult);
        //    Assert.IsTrue(deleteCredentialsResult.CredentialId == credentialId);
        //    Assert.IsTrue(deleteCredentialsResult.Status == Model.DeleteCredentials.StatusEnum.DELETED);
        //}
        //#endregion

        //#region Events
        //[TestMethod]
        //public void TestCreateEvent_Success()
        //{
        //    Configuration configuration = new Configuration()
        //    {
        //        Name = createdConfigurationName,
        //        Description = createdConfigurationDescription,

        //    };

        //    var createConfigurationResults = CreateConfiguration(environmentId, configuration);
        //    createdConfigurationId = createConfigurationResults.ConfigurationId;

        //    var listCollectionsResult = ListCollections(environmentId);

        //    CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
        //    {
        //        Language = createdCollectionLanguage,
        //        Name = createdCollectionName + "-" + Guid.NewGuid(),
        //        Description = createdCollectionDescription
        //    };

        //    var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
        //    var _createdCollectionId = createCollectionResult.CollectionId;

        //    DocumentAccepted addDocumentResult;
        //    using (FileStream fs = File.OpenRead(filepathToIngest))
        //    {
        //        addDocumentResult = AddDocument(environmentId, _createdCollectionId, fs, metadata);
        //        createdDocumentId = addDocumentResult.DocumentId;
        //    }

        //    var getDocumentStatusResult = GetDocumentStatus(environmentId, _createdCollectionId, createdDocumentId);

        //    var queryResult = Query(environmentId, _createdCollectionId, naturalLanguageQuery: "what year did watson play jeopardy");

        //    CreateEventObject queryEvent = new CreateEventObject()
        //    {
        //        Type = CreateEventObject.TypeEnum.CLICK,
        //        Data = new EventData()
        //        {
        //            EnvironmentId = environmentId,
        //            SessionToken = queryResult.SessionToken,
        //            CollectionId = _createdCollectionId,
        //            DocumentId = createdDocumentId
        //        }
        //    };

        //    var createEventResult = CreateEvent(queryEvent);

        //    var deleteDocumentResult = DeleteDocument(environmentId, _createdCollectionId, createdDocumentId);
        //    var deleteCollectionResult = DeleteCollection(environmentId, _createdCollectionId);
        //    var deleteConfigurationResults = DeleteConfiguration(environmentId, createdConfigurationId);

        //    Assert.IsNotNull(queryResult);
        //    Assert.IsNotNull(createEventResult);
        //    Assert.IsNotNull(createEventResult.Data);
        //    Assert.IsTrue(createEventResult.Data.EnvironmentId == environmentId);
        //    Assert.IsTrue(createEventResult.Data.SessionToken == queryResult.SessionToken);
        //    Assert.IsTrue(createEventResult.Data.CollectionId== _createdCollectionId);
        //    Assert.IsTrue(createEventResult.Data.DocumentId == createdDocumentId);
        //}
        //#endregion

        //#region Metrics
        //[TestMethod]
        //public void TestMetrics_Success()
        //{
        //    var getMetricsEventRateResult = GetMetricsEventRate();

        //    var getMetricsQueryResult = GetMetricsQuery();

        //    var getMetricsQueryEventResult = GetMetricsQueryEvent();

        //    var getMetricsQueryNoResultsResult = GetMetricsQueryNoResults();

        //    var getMetricsQueryTokenEventResult = GetMetricsQueryTokenEvent();

        //    var queryLogResult = QueryLog();

        //    Assert.IsNotNull(queryLogResult);
        //    Assert.IsNotNull(queryLogResult.Results);
        //    Assert.IsNotNull(getMetricsQueryTokenEventResult);
        //    Assert.IsNotNull(getMetricsQueryTokenEventResult.Aggregations);
        //    Assert.IsNotNull(getMetricsQueryNoResultsResult);
        //    Assert.IsNotNull(getMetricsQueryNoResultsResult.Aggregations);
        //    Assert.IsNotNull(getMetricsQueryEventResult);
        //    Assert.IsNotNull(getMetricsQueryEventResult.Aggregations);
        //    Assert.IsNotNull(getMetricsQueryResult);
        //    Assert.IsNotNull(getMetricsQueryResult.Aggregations);
        //    Assert.IsNotNull(getMetricsEventRateResult);
        //    Assert.IsNotNull(getMetricsEventRateResult.Aggregations);
        //}
        //#endregion

        //#region Expansions
        //[TestMethod]
        //public void TestExpansions_Success()
        //{
        //    CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
        //    {
        //        Language = CreateCollectionRequest.LanguageEnum.JA,
        //        Name = "expansions-collection-please-delete-" + Guid.NewGuid(),
        //        Description = createdCollectionDescription
        //    };

        //    var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
        //    var expansionCollectionId = createCollectionResult.CollectionId;

        //    Expansions body = new Expansions()
        //    {
        //        _Expansions = new List<Expansion>()
        //        {
        //            new Expansion()
        //            {
        //                InputTerms = new List<string>()
        //                {
        //                    "input-term"
        //                },
        //                ExpandedTerms = new List<string>()
        //                {
        //                    "expanded-term"
        //                }
        //            }
        //        }
        //    };

        //    var createExpansionsResult = CreateExpansions(environmentId, expansionCollectionId, body);
        //    var listExpansionsResult = ListExpansions(environmentId, expansionCollectionId);
        //    var deleteExpansionResult = DeleteExpansions(environmentId, expansionCollectionId);
        //    DeleteCollection(environmentId, expansionCollectionId);

        //    Assert.IsNotNull(deleteExpansionResult);
        //    Assert.IsNotNull(listExpansionsResult);
        //    Assert.IsTrue(listExpansionsResult._Expansions[0].ExpandedTerms[0] == "expanded-term");
        //    Assert.IsTrue(listExpansionsResult._Expansions[0].InputTerms[0] == "input-term");
        //    Assert.IsNotNull(createExpansionsResult);
        //    Assert.IsTrue(createExpansionsResult._Expansions[0].ExpandedTerms[0] == "expanded-term");
        //    Assert.IsTrue(createExpansionsResult._Expansions[0].InputTerms[0] == "input-term");
        //}
        //#endregion

        //#region Tokenization
        //[TestMethod]
        //public void TestTokenization_Success()
        //{
        //    var collectionsList = ListCollections(environmentId);

        //    foreach (Collection collection in collectionsList.Collections)
        //    {
        //        if (!string.IsNullOrEmpty(collection.Description))
        //        {
        //            if (collection.Description.ToLower().Contains("safe to delete") || collection.Description.Contains("Please delete me") || collection.Name.Contains("-updated") || collection.Name.Contains("-collection"))
        //            {
        //                DeleteCollection(environmentId, collection.CollectionId);
        //                Console.WriteLine("deleted " + collection.CollectionId);
        //            }
        //        }
        //    }

        //    CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
        //    {
        //        Language = CreateCollectionRequest.LanguageEnum.JA,
        //        Name = "tokenization-collection-please-delete-" + Guid.NewGuid(),
        //        Description = createdCollectionDescription
        //    };

        //    var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
        //    var tokenizationCollectionId = createCollectionResult.CollectionId;

        //    IsCollectionReady(environmentId, tokenizationCollectionId);
        //    autoEvent.WaitOne();

        //    TokenDict tokenizationDictionary = new TokenDict()
        //    {
        //        TokenizationRules = new List<TokenDictRule>()
        //        {
        //            new TokenDictRule()
        //            {
        //                Text = "すしネコ",
        //                Tokens = new List<string>()
        //                {
        //                    "すし", "ネコ"
        //                },
        //                Readings = new List<string>()
        //                {
        //                    "寿司", "ネコ"
        //                },
        //                PartOfSpeech = "カスタム名詞"
        //            }
        //        }
        //    };

        //    IsEnvironmentReady(environmentId);
        //    autoEvent.WaitOne();

        //    try
        //    {
        //        var createTokenizationDictionaryResult = CreateTokenizationDictionary(environmentId, tokenizationCollectionId, tokenizationDictionary);
        //        var getTokenizationDictionaryStatusResult = GetTokenizationDictionaryStatus(environmentId, tokenizationCollectionId);

        //        IsDictionaryReady(environmentId, tokenizationCollectionId);
        //        autoEvent.WaitOne();

        //        var deleteTokenizationDictionary = DeleteTokenizationDictionary(environmentId, tokenizationCollectionId);

        //        Assert.IsNotNull(deleteTokenizationDictionary);
        //        Assert.IsNotNull(getTokenizationDictionaryStatusResult);
        //        Assert.IsTrue(getTokenizationDictionaryStatusResult.Status == TokenDictStatusResponse.StatusEnum.PENDING);
        //        Assert.IsNotNull(createTokenizationDictionaryResult);
        //        Assert.IsTrue(createTokenizationDictionaryResult.Status == TokenDictStatusResponse.StatusEnum.PENDING);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    DeleteCollection(environmentId, tokenizationCollectionId);
        //}
        //#endregion

        //#region Stopword
        //[TestMethod]
        //public void TestStopword_Success()
        //{
        //    var collectionsList = ListCollections(environmentId);

        //    foreach (Collection collection in collectionsList.Collections)
        //    {
        //        if (!string.IsNullOrEmpty(collection.Description))
        //        {
        //            if (collection.Description.ToLower().Contains("safe to delete") || collection.Description.Contains("Please delete me") || collection.Name.Contains("-updated") || collection.Name.Contains("-collection"))
        //            {
        //                DeleteCollection(environmentId, collection.CollectionId);
        //                Console.WriteLine("deleted " + collection.CollectionId);
        //            }
        //        }
        //    }

        //    IsEnvironmentReady(environmentId);
        //    autoEvent.WaitOne();

        //    CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
        //    {
        //        Language = CreateCollectionRequest.LanguageEnum.EN,
        //        Name = "stopword-collection-please-delete-" + Guid.NewGuid(),
        //        Description = createdCollectionDescription
        //    };

        //    var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
        //    var stopwordCollectionId = createCollectionResult.CollectionId;

        //    IsCollectionReady(environmentId, stopwordCollectionId);
        //    autoEvent.WaitOne();

        //    TokenDictStatusResponse createStopwordListResult;
        //    using (FileStream fs = File.OpenRead(stopwordFileToIngest))
        //    {
        //        createStopwordListResult = service.CreateStopwordList(environmentId, stopwordCollectionId, fs);
        //    }

        //    IsStopwordsReady(environmentId, stopwordCollectionId);
        //    autoEvent.WaitOne();

        //    var deleteStopwordListResult = service.DeleteStopwordList(environmentId, stopwordCollectionId);

        //    Assert.IsNotNull(createStopwordListResult);
        //    Assert.IsTrue(createStopwordListResult.Status == TokenDictStatusResponse.StatusEnum.PENDING);
        //    Assert.IsNotNull(deleteStopwordListResult);
        //}
        //#endregion

        //#region Gateway
        //[TestMethod]
        //public void TestGateway_Success()
        //{
        //    var listGatewaysResult = service.ListGateways(environmentId);
        //    GatewayName gatewayName = new GatewayName()
        //    {
        //        Name = dotnetGatewayName
        //    };
        //    var createGatewayResult = service.CreateGateway(environmentId, gatewayName);
        //    var gatewayId = createGatewayResult.GatewayId;
        //    var getGatewayResult = service.GetGateway(environmentId, gatewayId);
        //    var deleteGatewayResult = service.DeleteGateway(environmentId, gatewayId);

        //    Assert.IsNotNull(deleteGatewayResult);
        //    Assert.IsTrue(deleteGatewayResult.GatewayId == gatewayId);
        //    Assert.IsTrue(!string.IsNullOrEmpty(deleteGatewayResult.Status));
        //    Assert.IsNotNull(getGatewayResult);
        //    Assert.IsTrue(getGatewayResult.GatewayId == gatewayId);
        //    Assert.IsTrue(getGatewayResult.Name == dotnetGatewayName);
        //    Assert.IsNotNull(createGatewayResult);
        //    Assert.IsTrue(createGatewayResult.Name == dotnetGatewayName);
        //    Assert.IsNotNull(listGatewaysResult);
        //}
        //#endregion

        //#region IsEnvironmentReady
        //private void IsEnvironmentReady(string environmentId)
        //{
        //    var result = GetEnvironment(environmentId);
        //    Console.WriteLine(string.Format("\tEnvironment {0} status is {1}.", environmentId, result.Status));

        //    if (result.Status == Environment.StatusEnum.ACTIVE)
        //    {
        //        autoEvent.Set();
        //    }
        //    else
        //    {
        //        Task.Factory.StartNew(() =>
        //        {
        //            Thread.Sleep(30000);
        //            Console.WriteLine("Checking environment status in 30 seconds...");
        //            IsEnvironmentReady(environmentId);
        //        });
        //    }
        //}
        //#endregion

        //#region IsDictionaryReady
        //private void IsDictionaryReady(string environmentId, string tokenizationCollectionId)
        //{
        //    var result = service.GetTokenizationDictionaryStatus(environmentId, tokenizationCollectionId);
        //    Console.WriteLine(string.Format("\tTokenization dictionary {0} status is {1}.", environmentId, result.Status));

        //    if(result.Status == TokenDictStatusResponse.StatusEnum.ACTIVE)
        //    {
        //        autoEvent.Set();
        //    }
        //    else
        //    {
        //        Task.Factory.StartNew(() =>
        //        {
        //            Thread.Sleep(30000);
        //            Console.WriteLine("Checking tokenization dictionary status in 30 seconds...");
        //            IsDictionaryReady(environmentId, tokenizationCollectionId);
        //        });
        //    }
        //}
        //#endregion

        //#region IsStopwordsReady
        //private void IsStopwordsReady(string environmentId, string collectionId)
        //{
        //    var result = service.GetStopwordListStatus(environmentId, collectionId);
        //    Console.WriteLine(string.Format("\tTokenization dictionary {0} status is {1}.", environmentId, result.Status));

        //    if (result.Status == TokenDictStatusResponse.StatusEnum.ACTIVE)
        //    {
        //        autoEvent.Set();
        //    }
        //    else
        //    {
        //        Task.Factory.StartNew(() =>
        //        {
        //            Thread.Sleep(30000);
        //            Console.WriteLine("Checking tokenization dictionary status in 30 seconds...");
        //            IsStopwordsReady(environmentId, collectionId);
        //        });
        //    }
        //}
        //#endregion

        //#region IsCollectionReady
        //private void IsCollectionReady(string environmentId, string collectionId)
        //{
        //    var result = service.GetCollection(environmentId, collectionId);
        //    Console.WriteLine(string.Format("\tCollection {0} status is {1}.", environmentId, result.Status));

        //    if (result.Status == Collection.StatusEnum.ACTIVE)
        //    {
        //        autoEvent.Set();
        //    }
        //    else
        //    {
        //        Task.Factory.StartNew(() =>
        //        {
        //            Thread.Sleep(30000);
        //            Console.WriteLine("Checking collection status in 30 seconds...");
        //            IsCollectionReady(environmentId, collectionId);
        //        });
        //    }
        //}
        //#endregion
    }
}
