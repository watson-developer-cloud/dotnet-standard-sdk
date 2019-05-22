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
using IBM.Cloud.SDK.Core.Util;
using Newtonsoft.Json.Linq;
using IBM.Cloud.SDK.Core.Http;
using Environment = IBM.Watson.Discovery.v1.Model.Environment;

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
        private static string configurationId;
        private static string documentId;
        private static string queryId;
        private static string exampleId;

        private string createdConfigurationName;
        private string updatedConfigurationName;
        private string createdConfigurationDescription = "configDescription - safe to delete";
        private string filepathToIngest = @"DiscoveryTestData\watson_beats_jeopardy.html";
        private string metadata = "{\"Creator\": \".NET SDK Test\",\"Subject\": \"Discovery service\"}";
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

            service.WithHeader("X-Watson-Test", "1");
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
            service.WithHeader("X-Watson-Test", "1");
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

            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription,
                enrichments: enrichments
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            service.WithHeader("X-Watson-Test", "1");
            var getConfigurationResults = service.GetConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Configuration updateConfiguration = new Configuration()
            {
                Name = updatedConfigurationName
            };

            service.WithHeader("X-Watson-Test", "1");
            var updateConfigurationResults = service.UpdateConfiguration(
                environmentId: environmentId,
                configurationId: configurationId,
                name: updatedConfigurationName
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Assert.IsNotNull(deleteConfigurationResults);
            Assert.IsTrue(deleteConfigurationResults.Result.Status == DeleteConfigurationResponse.StatusEnumValue.DELETED);
            Assert.IsNotNull(updateConfigurationResults);
            Assert.IsTrue(updateConfigurationResults.Result.ConfigurationId == configurationId);
            Assert.IsTrue(updateConfigurationResults.Result.Description == createdConfigurationDescription);
            Assert.IsNotNull(getConfigurationResults);
            Assert.IsTrue(getConfigurationResults.Result.ConfigurationId == configurationId);
            Assert.IsTrue(getConfigurationResults.Result.Description == createdConfigurationDescription);
            Assert.IsTrue(createConfigurationResults.Result.Enrichments[0].Options.Features.Concepts.Limit == 10);
            Assert.IsNotNull(createConfigurationResults);
            Assert.IsTrue(createConfigurationResults.Result.Description == createdConfigurationDescription);
            Assert.IsNotNull(listConfigurationsResults);
            Assert.IsNotNull(listConfigurationsResults.Result.Configurations);
            Assert.IsTrue(listConfigurationsResults.Result.Configurations.Count > 0);

            configurationId = null;
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

            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            service.WithHeader("X-Watson-Test", "1");
            var listCollectionsResult = service.ListCollections(
                environmentId: environmentId
                );

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            service.WithHeader("X-Watson-Test", "1");
            var getCollectionResult = service.GetCollection(environmentId, collectionId);

            service.WithHeader("X-Watson-Test", "1");
            var updateCollectionResult = service.UpdateCollection(
                environmentId: environmentId,
                collectionId: collectionId,
                name: updatedCollectionName,
                description: createdCollectionDescription,
                configurationId: configurationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var listCollectionFieldsResult = service.ListCollectionFields(
                environmentId: environmentId,
                collectionId: collectionId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
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
            configurationId = null;
            collectionId = null;
        }
        #endregion

        #region Preview Environment
        [TestMethod]
        public void PreviewEnvironment()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    var testConfigurationInEnvironmentResult = service.TestConfigurationInEnvironment(
                        environmentId: environmentId,
                        configurationId: configurationId,
                        file: ms, filename: "watson_beats_jeopardy.html",
                        fileContentType: "text/html"
                        );

                    service.WithHeader("X-Watson-Test", "1");
                    var deleteConfigurationResults = service.DeleteConfiguration(
                        environmentId: environmentId,
                        configurationId: configurationId
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
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                    documentId = addDocumentResult.Result.DocumentId;
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var getDocumentStatusResult = service.GetDocumentStatus(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );

            DetailedResponse<DocumentAccepted> updateDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    updateDocumentResult = service.UpdateDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    documentId: documentId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(environmentId, collectionId, documentId);
            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(environmentId, collectionId);
            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(environmentId, configurationId);

            Assert.IsNotNull(deleteDocumentResult);
            Assert.IsTrue(deleteDocumentResult.Result.Status == DeleteDocumentResponse.StatusEnumValue.DELETED);
            Assert.IsNotNull(updateDocumentResult);
            Assert.IsTrue(updateDocumentResult.Result.DocumentId == documentId);
            Assert.IsNotNull(getDocumentStatusResult);
            Assert.IsTrue(getDocumentStatusResult.Result.DocumentId == documentId);
            Assert.IsNotNull(addDocumentResult);

            documentId = null;
            collectionId = null;
            configurationId = null;
            environmentId = null;
        }
        #endregion

        #region Query
        [TestMethod]
        public void TestQuery()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                    documentId = addDocumentResult.Result.DocumentId;
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var queryResult = service.Query(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                returnFields: "extracted_metadata.sha1"
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(environmentId: environmentId,
                configurationId: configurationId
                );

            Assert.IsNotNull(queryResult);

            documentId = null;
            collectionId = null;
            configurationId = null;
            environmentId = null;
        }
        #endregion

        #region Notices
        [TestMethod]
        public void TestGetNotices()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                    documentId = addDocumentResult.Result.DocumentId;
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var queryResult = service.Query(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery
                );

            service.WithHeader("X-Watson-Test", "1");
            var queryNoticesResult = service.QueryNotices(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                passages: true
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Assert.IsNotNull(queryNoticesResult.Result);
            Assert.IsNotNull(queryNoticesResult.Result.MatchingResults);

            environmentId = null;
            configurationId = null;
            documentId = null;
        }
        #endregion

        #region Training Data
        [TestMethod]
        public void TestTrainingData()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );

                    documentId = addDocumentResult.Result.DocumentId;
                }
            }

            service.WithHeader("X-Watson-Test", "1");
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

            service.WithHeader("X-Watson-Test", "1");
            var addTrainingDataResult = service.AddTrainingData(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                filter: "filter",
                examples: examples
                );
            queryId = addTrainingDataResult.Result.QueryId;

            service.WithHeader("X-Watson-Test", "1");
            var getTrainingDataResult = service.GetTrainingData(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId
                );

            var trainingExample = new TrainingExample()
            {
                DocumentId = documentId,
                Relevance = 1
            };

            service.WithHeader("X-Watson-Test", "1");
            var createTrainingExampleResult = service.CreateTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                documentId: documentId,
                relevance: 1
                );
            exampleId = createTrainingExampleResult.Result.DocumentId;

            service.WithHeader("X-Watson-Test", "1");
            var getTrainingExampleResult = service.GetTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                exampleId: exampleId
                );

            service.WithHeader("X-Watson-Test", "1");
            var updateTrainingExampleResult = service.UpdateTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                exampleId: exampleId,
                crossReference: "crossReference",
                relevance: 1
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteTrainingExampleResult = service.DeleteTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                exampleId: exampleId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteTrainingDataResult = service.DeleteTrainingData(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteAllTrainingDataResult = service.DeleteAllTrainingData(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Assert.IsTrue(deleteAllTrainingDataResult.StatusCode == 204);
            Assert.IsTrue(deleteTrainingDataResult.StatusCode == 204);
            Assert.IsTrue(deleteTrainingExampleResult.StatusCode == 204);
            Assert.IsNotNull(updateTrainingExampleResult.Result);
            Assert.IsTrue(updateTrainingExampleResult.Result.CrossReference == "crossReference");
            Assert.IsNotNull(getTrainingExampleResult.Result);
            Assert.IsTrue(getTrainingExampleResult.Result.DocumentId == documentId);
            Assert.IsNotNull(createTrainingExampleResult.Result);
            Assert.IsTrue(createTrainingExampleResult.Result.DocumentId == documentId);
            Assert.IsNotNull(getTrainingDataResult.Result);
            Assert.IsTrue(getTrainingDataResult.Result.QueryId == queryId);
            Assert.IsNotNull(addTrainingDataResult.Result);
            Assert.IsTrue(addTrainingDataResult.Result.NaturalLanguageQuery == naturalLanguageQuery);
            Assert.IsNotNull(listTrainingDataResult.Result);
            Assert.IsTrue(listTrainingDataResult.Result.EnvironmentId == environmentId);

            exampleId = null;
            queryId = null;
            configurationId = null;
            environmentId = null;
            documentId = null;
        }
        #endregion

        #region Credentials
        [TestMethod]
        public void TestCredentials_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listCredentialsResult = service.ListCredentials(
                environmentId: environmentId
                );

            var credentialDetails = new CredentialDetails()
            {
                CredentialType = CredentialDetails.CredentialTypeEnumValue.OAUTH2,
                EnterpriseId = "myEnterpriseId",
                ClientId = "myClientId",
                ClientSecret = "myClientSecret",
                PublicKeyId = "myPublicIdKey",
                Passphrase = "myPassphrase",
                PrivateKey = "myPrivateKey"
            };

            service.WithHeader("X-Watson-Test", "1");
            var createCredentialsResult = service.CreateCredentials(
                environmentId: environmentId,
                sourceType: Credentials.SourceTypeEnumValue.BOX,
                credentialDetails: credentialDetails
                );
            string credentialId = createCredentialsResult.Result.CredentialId;

            service.WithHeader("X-Watson-Test", "1");
            var getCredentialResult = service.GetCredentials(
                environmentId: environmentId,
                credentialId: credentialId
                );

            string privateKey = "privatekey";
            var privateKeyBytes = System.Text.Encoding.UTF8.GetBytes(privateKey);
            var base64PrivateKey = Convert.ToBase64String(privateKeyBytes);

            var updatedCredentialDetails = new CredentialDetails()
            {
                CredentialType = CredentialDetails.CredentialTypeEnumValue.OAUTH2,
                EnterpriseId = "myEnterpriseIdUpdated",
                ClientId = "myClientIdUpdated",
                ClientSecret = "myClientSecretUpdated",
                PublicKeyId = "myPublicIdKeyUpdated",
                Passphrase = "myPassphraseUpdated",
                PrivateKey = base64PrivateKey
            };

            service.WithHeader("X-Watson-Test", "1");
            var updateCredentialResult = service.UpdateCredentials(
                environmentId: environmentId,
                credentialId: credentialId,
                sourceType: Credentials.SourceTypeEnumValue.BOX,
                credentialDetails: updatedCredentialDetails
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteCredentialsResult = service.DeleteCredentials(
                environmentId: environmentId,
                credentialId: credentialId
                );

            Assert.IsNotNull(listCredentialsResult);
            Assert.IsTrue(!string.IsNullOrEmpty(listCredentialsResult.Response));
            Assert.IsNotNull(createCredentialsResult);
            Assert.IsTrue(!string.IsNullOrEmpty(createCredentialsResult.Result.CredentialId));
            Assert.IsTrue(createCredentialsResult.Result.SourceType == Credentials.SourceTypeEnumValue.BOX);
            Assert.IsTrue(createCredentialsResult.Result.CredentialDetails.CredentialType == CredentialDetails.CredentialTypeEnumValue.OAUTH2);
            Assert.IsTrue(createCredentialsResult.Result.CredentialDetails.EnterpriseId == "myEnterpriseId");
            Assert.IsNotNull(getCredentialResult);
            Assert.IsTrue(getCredentialResult.Result.SourceType == Credentials.SourceTypeEnumValue.BOX);
            Assert.IsTrue(getCredentialResult.Result.CredentialDetails.CredentialType == CredentialDetails.CredentialTypeEnumValue.OAUTH2);
            Assert.IsTrue(getCredentialResult.Result.CredentialDetails.EnterpriseId == "myEnterpriseId");
            Assert.IsNotNull(updateCredentialResult);
            Assert.IsTrue(updateCredentialResult.Result.CredentialDetails.EnterpriseId == "myEnterpriseIdUpdated");
            Assert.IsNotNull(deleteCredentialsResult);
            Assert.IsTrue(deleteCredentialsResult.Result.CredentialId == credentialId);
            Assert.IsTrue(deleteCredentialsResult.Result.Status == Model.DeleteCredentials.StatusEnumValue.DELETED);
        }
        #endregion

        #region Events
        [TestMethod]
        public void TestCreateEvent_Success()
        {
            Configuration configuration = new Configuration();
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            DetailedResponse<DocumentAccepted> addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    addDocumentResult = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );
                    documentId = addDocumentResult.Result.DocumentId;
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var queryResult = service.Query(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                returnFields: "extracted_metadata.sha1"
                );

            var data = new EventData()
            {
                EnvironmentId = environmentId,
                SessionToken = queryResult.Result.SessionToken,
                CollectionId = collectionId,
                DocumentId = documentId
            };

            service.WithHeader("X-Watson-Test", "1");
            var createEventResult = service.CreateEvent(
                type: CreateEventResponse.TypeEnumValue.CLICK,
                data: data
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(createEventResult);
            Assert.IsNotNull(createEventResult.Result.Data);
            Assert.IsTrue(createEventResult.Result.Data.EnvironmentId == environmentId);
            Assert.IsTrue(createEventResult.Result.Data.SessionToken == queryResult.Result.SessionToken);
            Assert.IsTrue(createEventResult.Result.Data.CollectionId == collectionId);
            Assert.IsTrue(createEventResult.Result.Data.DocumentId == documentId);
        }
        #endregion

        #region Metrics
        [TestMethod]
        public void TestMetrics_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var getMetricsEventRateResult = service.GetMetricsEventRate();

            service.WithHeader("X-Watson-Test", "1");
            var getMetricsQueryResult = service.GetMetricsQuery();

            service.WithHeader("X-Watson-Test", "1");
            var getMetricsQueryEventResult = service.GetMetricsQueryEvent();

            service.WithHeader("X-Watson-Test", "1");
            var getMetricsQueryNoResultsResult = service.GetMetricsQueryNoResults();

            service.WithHeader("X-Watson-Test", "1");
            var getMetricsQueryTokenEventResult = service.GetMetricsQueryTokenEvent();

            service.WithHeader("X-Watson-Test", "1");
            var queryLogResult = service.QueryLog();

            Assert.IsNotNull(queryLogResult);
            Assert.IsNotNull(queryLogResult.Result.Results);
            Assert.IsNotNull(getMetricsQueryTokenEventResult);
            Assert.IsNotNull(getMetricsQueryTokenEventResult.Result.Aggregations);
            Assert.IsNotNull(getMetricsQueryNoResultsResult);
            Assert.IsNotNull(getMetricsQueryNoResultsResult.Result.Aggregations);
            Assert.IsNotNull(getMetricsQueryEventResult);
            Assert.IsNotNull(getMetricsQueryEventResult.Result.Aggregations);
            Assert.IsNotNull(getMetricsQueryResult);
            Assert.IsNotNull(getMetricsQueryResult.Result.Aggregations);
            Assert.IsNotNull(getMetricsEventRateResult);
            Assert.IsNotNull(getMetricsEventRateResult.Result.Aggregations);
        }
        #endregion

        #region Expansions
        [TestMethod]
        public void TestExpansions_Success()
        {
            Configuration configuration = new Configuration();
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            var expansions = new List<Expansion>()
            {
                new Expansion()
                {
                    InputTerms = new List<string>()
                    {
                        "input-term"
                    },
                    ExpandedTerms = new List<string>()
                    {
                        "expanded-term"
                    }
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var createExpansionsResult = service.CreateExpansions(
                environmentId: environmentId,
                collectionId: collectionId,
                expansions: expansions
                );
            service.WithHeader("X-Watson-Test", "1");
            var listExpansionsResult = service.ListExpansions(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteExpansionResult = service.DeleteExpansions(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Assert.IsNotNull(deleteExpansionResult);
            Assert.IsNotNull(listExpansionsResult);
            Assert.IsTrue(listExpansionsResult.Result._Expansions[0].ExpandedTerms[0] == "expanded-term");
            Assert.IsTrue(listExpansionsResult.Result._Expansions[0].InputTerms[0] == "input-term");
            Assert.IsNotNull(createExpansionsResult);
            Assert.IsTrue(createExpansionsResult.Result._Expansions[0].ExpandedTerms[0] == "expanded-term");
            Assert.IsTrue(createExpansionsResult.Result._Expansions[0].InputTerms[0] == "input-term");
        }
        #endregion

        #region Tokenization
        [TestMethod]
        public void TestTokenization_Success()
        {
            Configuration configuration = new Configuration();
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            IsCollectionReady(
                environmentId: environmentId,
                collectionId: collectionId
                );
            autoEvent.WaitOne();

            var tokenizationRules = new List<TokenDictRule>()
            {
                new TokenDictRule()
                {
                    Text = "すしネコ",
                    Tokens = new List<string>()
                    {
                        "すし", "ネコ"
                    },
                    Readings = new List<string>()
                    {
                        "寿司", "ネコ"
                    },
                    PartOfSpeech = "カスタム名詞"
                }
            };

            IsEnvironmentReady(
                environmentId: environmentId
                );
            autoEvent.WaitOne();

            try
            {
                service.WithHeader("X-Watson-Test", "1");
                var createTokenizationDictionaryResult = service.CreateTokenizationDictionary(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    tokenizationRules: tokenizationRules
                    );
                service.WithHeader("X-Watson-Test", "1");
                var getTokenizationDictionaryStatusResult = service.GetTokenizationDictionaryStatus(
                    environmentId: environmentId,
                    collectionId: collectionId
                    );

                IsDictionaryReady(
                    environmentId: environmentId,
                    tokenizationCollectionId: collectionId
                    );
                autoEvent.WaitOne();

                service.WithHeader("X-Watson-Test", "1");
                var deleteTokenizationDictionary = service.DeleteTokenizationDictionary(
                    environmentId: environmentId,
                    collectionId: collectionId
                    );

                Assert.IsNotNull(deleteTokenizationDictionary);
                Assert.IsNotNull(getTokenizationDictionaryStatusResult);
                Assert.IsTrue(getTokenizationDictionaryStatusResult.Result.Status == TokenDictStatusResponse.StatusEnumValue.PENDING);
                Assert.IsNotNull(createTokenizationDictionaryResult);
                Assert.IsTrue(createTokenizationDictionaryResult.Result.Status == TokenDictStatusResponse.StatusEnumValue.PENDING);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );
        }
        #endregion

        #region Stopword
        //[TestMethod]
        public void TestStopword_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var collectionsList = service.ListCollections(
                environmentId: environmentId
                );

            foreach (Collection collection in collectionsList.Result.Collections)
            {
                if (!string.IsNullOrEmpty(collection.Description))
                {
                    if (collection.Description.ToLower().Contains("safe to delete") || collection.Description.Contains("Please delete me") || collection.Name.Contains("-updated") || collection.Name.Contains("-collection"))
                    {
                        service.WithHeader("X-Watson-Test", "1");
                        service.DeleteCollection(
                            environmentId: environmentId,
                            collectionId: collection.CollectionId
                            );
                        Console.WriteLine("deleted " + collection.CollectionId);
                    }
                }
            }

            IsEnvironmentReady(
                environmentId: environmentId
                );
            autoEvent.WaitOne();

            Configuration configuration = new Configuration();
            service.WithHeader("X-Watson-Test", "1");
            var createConfigurationResults = service.CreateConfiguration(
                environmentId: environmentId,
                name: createdConfigurationName,
                description: createdConfigurationDescription
                );

            configurationId = createConfigurationResults.Result.ConfigurationId;

            string collectionName = createdCollectionName + "-" + Guid.NewGuid();
            service.WithHeader("X-Watson-Test", "1");
            var createCollectionResult = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName,
                description: createdCollectionDescription,
                configurationId: configurationId,
                language: createdCollectionLanguage
                );
            var collectionId = createCollectionResult.Result.CollectionId;

            IsCollectionReady(
                environmentId: environmentId,
                collectionId: collectionId
                );
            autoEvent.WaitOne();

            DetailedResponse<TokenDictStatusResponse> createStopwordListResult;
            using (FileStream fs = File.OpenRead(stopwordFileToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    createStopwordListResult = service.CreateStopwordList(
                        environmentId: environmentId,
                        collectionId: collectionId,
                        stopwordFile: ms,
                        stopwordFilename: Path.GetFileName(stopwordFileToIngest)
                        );
                }
            }

            IsStopwordsReady(
                environmentId: environmentId,
                collectionId: collectionId
                );
            autoEvent.WaitOne();

            service.WithHeader("X-Watson-Test", "1");
            var deleteStopwordListResult = service.DeleteStopwordList(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteCollectionResult = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteConfigurationResults = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Assert.IsNotNull(createStopwordListResult);
            Assert.IsTrue(createStopwordListResult.Result.Status == TokenDictStatusResponse.StatusEnumValue.PENDING);
            Assert.IsNotNull(deleteStopwordListResult);
        }
        #endregion

        #region Gateway
        [TestMethod]
        public void TestGateway_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listGatewaysResult = service.ListGateways(
                environmentId: environmentId
                );
            service.WithHeader("X-Watson-Test", "1");
            var createGatewayResult = service.CreateGateway(
                environmentId: environmentId,
                name: dotnetGatewayName
                );
            var gatewayId = createGatewayResult.Result.GatewayId;
            service.WithHeader("X-Watson-Test", "1");
            var getGatewayResult = service.GetGateway(
                environmentId: environmentId,
                gatewayId: gatewayId
                );
            service.WithHeader("X-Watson-Test", "1");
            var deleteGatewayResult = service.DeleteGateway(
                environmentId: environmentId,
                gatewayId: gatewayId
                );

            Assert.IsNotNull(deleteGatewayResult);
            Assert.IsTrue(deleteGatewayResult.Result.GatewayId == gatewayId);
            Assert.IsTrue(!string.IsNullOrEmpty(deleteGatewayResult.Result.Status));
            Assert.IsNotNull(getGatewayResult);
            Assert.IsTrue(getGatewayResult.Result.GatewayId == gatewayId);
            Assert.IsTrue(getGatewayResult.Result.Name == dotnetGatewayName);
            Assert.IsNotNull(createGatewayResult);
            Assert.IsTrue(createGatewayResult.Result.Name == dotnetGatewayName);
            Assert.IsNotNull(listGatewaysResult);
        }
        #endregion

        #region IsEnvironmentReady
        private void IsEnvironmentReady(string environmentId)
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.GetEnvironment(
                environmentId: environmentId
                );
            Console.WriteLine(string.Format("\tEnvironment {0} status is {1}.", environmentId, result.Result.Status));

            if (result.Result.Status == Environment.StatusEnumValue.ACTIVE)
            {
                autoEvent.Set();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(30000);
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    IsEnvironmentReady(environmentId);
                });
            }
        }
        #endregion

        #region IsDictionaryReady
        private void IsDictionaryReady(string environmentId, string tokenizationCollectionId)
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.GetTokenizationDictionaryStatus(
                environmentId: environmentId,
                collectionId: tokenizationCollectionId
                );
            Console.WriteLine(string.Format("\tTokenization dictionary {0} status is {1}.", environmentId, result.Result.Status));

            if (result.Result.Status == TokenDictStatusResponse.StatusEnumValue.ACTIVE)
            {
                autoEvent.Set();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(30000);
                    Console.WriteLine("Checking tokenization dictionary status in 30 seconds...");
                    IsDictionaryReady(environmentId, tokenizationCollectionId);
                });
            }
        }
        #endregion

        #region IsStopwordsReady
        private void IsStopwordsReady(string environmentId, string collectionId)
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.GetStopwordListStatus(
                environmentId: environmentId,
                collectionId: collectionId
                );
            Console.WriteLine(string.Format("\tTokenization dictionary {0} status is {1}.", environmentId, result.Result.Status));

            if (result.Result.Status == TokenDictStatusResponse.StatusEnumValue.ACTIVE)
            {
                autoEvent.Set();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(30000);
                    Console.WriteLine("Checking tokenization dictionary status in 30 seconds...");
                    IsStopwordsReady(environmentId, collectionId);
                });
            }
        }
        #endregion

        #region IsCollectionReady
        private void IsCollectionReady(string environmentId, string collectionId)
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.GetCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );
            Console.WriteLine(string.Format("\tCollection {0} status is {1}.", environmentId, result.Result.Status));

            if (result.Result.Status == Collection.StatusEnumValue.ACTIVE)
            {
                autoEvent.Set();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(30000);
                    Console.WriteLine("Checking collection status in 30 seconds...");
                    IsCollectionReady(environmentId, collectionId);
                });
            }
        }
        #endregion
    }
}
