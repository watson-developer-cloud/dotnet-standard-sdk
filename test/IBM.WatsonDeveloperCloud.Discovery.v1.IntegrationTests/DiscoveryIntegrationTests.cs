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
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Environment = IBM.WatsonDeveloperCloud.Discovery.v1.Model.Environment;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json.Linq;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.IntegrationTests
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
        private static string createdTrainingQueryId;
        private static string createdTrainingExampleId;

        private string createdConfigurationName;
        private string updatedConfigurationName;
        private string createdConfigurationDescription = "configDescription - safe to delete";
        private string filepathToIngest = @"DiscoveryTestData\watson_beats_jeopardy.html";
        private string metadata = "{\"Creator\": \"DotnetSDK Test\",\"Subject\": \"Discovery service\"}";
        private string stopwordFileToIngest = @"DiscoveryTestData\stopwords.txt";

        private string createdCollectionName;
        private string createdCollectionDescription = "createdCollectionDescription - safe to delete";
        private string updatedCollectionName;
        private string dotnetGatewayName = "dotnet-sdk-integration-test-gateway";
        private CreateCollectionRequest.LanguageEnum createdCollectionLanguage = CreateCollectionRequest.LanguageEnum.EN;


        private string naturalLanguageQuery = "Who beat Ken Jennings in Jeopardy!";
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
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

            var environments = ListEnvironments();
            environmentId = environments.Environments[1].EnvironmentId;

            createdConfigurationName = Guid.NewGuid().ToString();
            updatedConfigurationName = createdConfigurationName + "-updated";
            createdCollectionName = Guid.NewGuid().ToString();
            updatedCollectionName = createdCollectionName + "-updated";
        }

        [TestCleanup]
        public void Teardown() { }
        
        #region Configurations
        [TestMethod]
        public void TestConfigurations_Success()
        {
            var listConfigurationsResults = ListConfigurations(environmentId);

            Configuration configuration = new Configuration()
            {
                Name = Guid.NewGuid().ToString(),
                Description = createdConfigurationDescription,
                Enrichments = new List<Enrichment>()
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
                }
            };

            var createConfigurationResults = CreateConfiguration(environmentId, configuration);
            createdConfigurationId = createConfigurationResults.ConfigurationId;

            var getConfigurationResults = GetConfiguration(environmentId, createdConfigurationId);

            Configuration updateConfiguration = new Configuration()
            {
                Name = updatedConfigurationName
            };

            var updateConfigurationResults = UpdateConfiguration(environmentId, createdConfigurationId, updateConfiguration);

            var deleteConfigurationResults = DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsNotNull(deleteConfigurationResults);
            Assert.IsTrue(deleteConfigurationResults.Status == DeleteConfigurationResponse.StatusEnum.DELETED);
            Assert.IsNotNull(updateConfigurationResults);
            Assert.IsTrue(updateConfigurationResults.ConfigurationId == createdConfigurationId);
            Assert.IsTrue(updateConfigurationResults.Description == createdConfigurationDescription);
            Assert.IsNotNull(getConfigurationResults);
            Assert.IsTrue(getConfigurationResults.ConfigurationId == createdConfigurationId);
            Assert.IsTrue(getConfigurationResults.Description == createdConfigurationDescription);
            Assert.IsTrue(createConfigurationResults.Enrichments[0].Options.Features.Concepts.Limit == 10);
            Assert.IsNotNull(createConfigurationResults);
            Assert.IsTrue(createConfigurationResults.Description == createdConfigurationDescription);
            Assert.IsNotNull(listConfigurationsResults);
            Assert.IsNotNull(listConfigurationsResults.Configurations);
            Assert.IsTrue(listConfigurationsResults.Configurations.Count > 0);

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

            var createConfigurationResults = CreateConfiguration(environmentId, configuration);
            createdConfigurationId = createConfigurationResults.ConfigurationId;

            var listCollectionsResult = ListCollections(environmentId);

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = createdCollectionLanguage,
                Name = createdCollectionName + "-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var _createdCollectionId = createCollectionResult.CollectionId;

            var getCollectionResult = GetCollection(environmentId, _createdCollectionId);

            UpdateCollectionRequest updateCollectionRequest = new UpdateCollectionRequest()
            {
                Name = updatedCollectionName,
            };

            var updateCollectionResult = UpdateCollection(environmentId, _createdCollectionId, updateCollectionRequest);

            var listCollectionFieldsResult = ListCollectionFields(environmentId, _createdCollectionId);

            var deleteCollectionResult = DeleteCollection(environmentId, _createdCollectionId);
            var deleteConfigurationResults = DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsNotNull(deleteCollectionResult);
            Assert.IsTrue(deleteCollectionResult.Status == DeleteCollectionResponse.StatusEnum.DELETED);
            Assert.IsNotNull(listCollectionFieldsResult);
            Assert.IsNotNull(updateCollectionResult);
            Assert.IsTrue(updateCollectionResult.CollectionId == _createdCollectionId);
            Assert.IsNotNull(getCollectionResult);
            Assert.IsTrue(getCollectionResult.CollectionId == _createdCollectionId);
            Assert.IsTrue(getCollectionResult.Description == createdCollectionDescription);
            Assert.IsNotNull(createCollectionResult);
            Assert.IsTrue(createCollectionResult.Description == createdCollectionDescription);
            Assert.IsNotNull(listCollectionsResult);

            environmentId = null;
            createdConfigurationId = null;
            _createdCollectionId = null;
        }
        #endregion

        #region Preview Environment
        //[TestMethod]
        public void PreviewEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling PreviewEnvironment()..."));

            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                var result = TestConfigurationInEnvironment(environmentId, createdConfigurationId, "html_input");

                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("result is null.");
                }

                Assert.IsNotNull(result);
            }
        }
        #endregion

        #region Documents
        [TestMethod]
        public void TestDocuments_Success()
        {
            Configuration configuration = new Configuration()
            {
                Name = createdConfigurationName,
                Description = createdConfigurationDescription,

            };

            var createConfigurationResults = CreateConfiguration(environmentId, configuration);
            createdConfigurationId = createConfigurationResults.ConfigurationId;

            var listCollectionsResult = ListCollections(environmentId);

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = createdCollectionLanguage,
                Name = createdCollectionName + "-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var _createdCollectionId = createCollectionResult.CollectionId;

            DocumentAccepted addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                addDocumentResult = AddDocument(environmentId, _createdCollectionId, fs, metadata);
                createdDocumentId = addDocumentResult.DocumentId;
            }

            var getDocumentStatusResult = GetDocumentStatus(environmentId, _createdCollectionId, createdDocumentId);

            DocumentAccepted updateDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                updateDocumentResult = UpdateDocument(environmentId, _createdCollectionId, createdDocumentId, fs, metadata);
            }

            var deleteDocumentResult = DeleteDocument(environmentId, _createdCollectionId, createdDocumentId);
            var deleteCollectionResult = DeleteCollection(environmentId, _createdCollectionId);
            var deleteConfigurationResults = DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsNotNull(deleteDocumentResult);
            Assert.IsTrue(deleteDocumentResult.Status == DeleteDocumentResponse.StatusEnum.DELETED);
            Assert.IsNotNull(updateDocumentResult);
            Assert.IsTrue(updateDocumentResult.DocumentId == createdDocumentId);
            Assert.IsNotNull(getDocumentStatusResult);
            Assert.IsTrue(getDocumentStatusResult.DocumentId == createdDocumentId);
            Assert.IsNotNull(addDocumentResult);

            createdDocumentId = null;
            _createdCollectionId = null;
            createdConfigurationId = null;
            environmentId = null;
        }
        #endregion

        #region Query
        [TestMethod]
        public void TestQuery()
        {
            Configuration configuration = new Configuration()
            {
                Name = createdConfigurationName,
                Description = createdConfigurationDescription,

            };

            var createConfigurationResults = CreateConfiguration(environmentId, configuration);
            createdConfigurationId = createConfigurationResults.ConfigurationId;

            var listCollectionsResult = ListCollections(environmentId);

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = createdCollectionLanguage,
                Name = createdCollectionName + "-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var _createdCollectionId = createCollectionResult.CollectionId;

            DocumentAccepted addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                addDocumentResult = AddDocument(environmentId, _createdCollectionId, fs, metadata);
                createdDocumentId = addDocumentResult.DocumentId;
            }

            List<string> rFields = new List<string>();
            rFields.Add("extracted_metadata.sha1");
            var queryResult = Query(environmentId, _createdCollectionId, null, null, naturalLanguageQuery, returnFields:rFields);

            var deleteDocumentResult = DeleteDocument(environmentId, _createdCollectionId, createdDocumentId);
            var deleteCollectionResult = DeleteCollection(environmentId, _createdCollectionId);
            var deleteConfigurationResults = DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsNotNull(queryResult);

            createdDocumentId = null;
            _createdCollectionId = null;
            createdConfigurationId = null;
            environmentId = null;
        }
        #endregion

        #region Notices
        [TestMethod]
        public void TestGetNotices()
        {
            Configuration configuration = new Configuration()
            {
                Name = createdConfigurationName,
                Description = createdConfigurationDescription,

            };

            var createConfigurationResults = CreateConfiguration(environmentId, configuration);
            createdConfigurationId = createConfigurationResults.ConfigurationId;

            var listCollectionsResult = ListCollections(environmentId);

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = createdCollectionLanguage,
                Name = createdCollectionName + "-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var noticesCollectionId = createCollectionResult.CollectionId;

            DocumentAccepted addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                addDocumentResult = AddDocument(environmentId, noticesCollectionId, fs, metadata);
                createdDocumentId = addDocumentResult.DocumentId;
            }

            var queryResult = Query(environmentId, noticesCollectionId, null, null, naturalLanguageQuery);
            var queryNoticesResult = QueryNotices(environmentId, noticesCollectionId, null, null, naturalLanguageQuery, true);

            var deleteDocumentResult = DeleteDocument(environmentId, noticesCollectionId, createdDocumentId);
            var deleteCollectionResult = DeleteCollection(environmentId, noticesCollectionId);
            var deleteConfigurationResults = DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsNotNull(queryNoticesResult);

            environmentId = null;
            createdConfigurationId = null;
            createdDocumentId = null;
        }
        #endregion

        #region Training Data
        [TestMethod]
        public void TestTrainingData()
        {
            Configuration configuration = new Configuration()
            {
                Name = createdConfigurationName,
                Description = createdConfigurationDescription,

            };

            var createConfigurationResults = CreateConfiguration(environmentId, configuration);
            createdConfigurationId = createConfigurationResults.ConfigurationId;

            var listCollectionsResult = ListCollections(environmentId);

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = createdCollectionLanguage,
                Name = createdCollectionName + "-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var trainingCollectionId = createCollectionResult.CollectionId;

            DocumentAccepted addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                addDocumentResult = AddDocument(environmentId, trainingCollectionId, fs, metadata);
                createdDocumentId = addDocumentResult.DocumentId;
            }

            var getDocumentStatusResult = GetDocumentStatus(environmentId, trainingCollectionId, createdDocumentId);

            DocumentAccepted updateDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                updateDocumentResult = UpdateDocument(environmentId, trainingCollectionId, createdDocumentId, fs, metadata);
            }

            var listTrainingDataResult = ListTrainingData(environmentId, trainingCollectionId);

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

            var addTrainingDataResult = AddTrainingData(environmentId, trainingCollectionId, newTrainingQuery);
            createdTrainingQueryId = addTrainingDataResult.QueryId;

            var getTrainingDataResult = GetTrainingData(environmentId, trainingCollectionId, createdTrainingQueryId);

            var trainingExample = new TrainingExample()
            {
                DocumentId = createdDocumentId,
                Relevance = 1
            };

            var createTrainingExampleResult = CreateTrainingExample(environmentId, trainingCollectionId, createdTrainingQueryId, trainingExample);
            createdTrainingExampleId = createTrainingExampleResult.DocumentId;

            var getTrainingExampleResult = GetTrainingExample(environmentId, trainingCollectionId, createdTrainingQueryId, createdTrainingExampleId);

            var updateTrainingExample = new TrainingExamplePatch()
            {
                CrossReference = "crossReference",
                Relevance = 1
            };

            var updateTrainingExampleResult = UpdateTrainingExample(environmentId, trainingCollectionId, createdTrainingQueryId, createdTrainingExampleId, updateTrainingExample);

            var deleteTrainingExampleResult = DeleteTrainingExample(environmentId, trainingCollectionId, createdTrainingQueryId, createdTrainingExampleId);
            var deleteTrainingDataResult = DeleteTrainingData(environmentId, trainingCollectionId, createdTrainingQueryId);
            var deleteAllTrainingDataResult = DeleteAllTrainingData(environmentId, trainingCollectionId);
            var deleteDocumentResult = DeleteDocument(environmentId, trainingCollectionId, createdDocumentId);
            var deleteCollectionResult = DeleteCollection(environmentId, trainingCollectionId);
            var deleteConfigurationResults = DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsNotNull(deleteAllTrainingDataResult);
            Assert.IsNotNull(deleteTrainingDataResult);
            Assert.IsNotNull(deleteTrainingExampleResult);
            Assert.IsNotNull(updateTrainingExampleResult);
            Assert.IsNotNull(getTrainingExampleResult);
            Assert.IsNotNull(createTrainingExampleResult);
            Assert.IsNotNull(getTrainingDataResult);
            Assert.IsNotNull(addTrainingDataResult);
            Assert.IsNotNull(listTrainingDataResult);

            createdTrainingExampleId = null;
            createdTrainingQueryId = null;
            createdConfigurationId = null;
            environmentId = null;
            createdDocumentId = null;
        }
        #endregion

        #region Credentials
        //[TestMethod]
        public void TestCredentials_Success()
        {
            var listCredentialsResult = ListCredentials(environmentId);

            Model.Credentials credentials = new Model.Credentials()
            {
                SourceType = Model.Credentials.SourceTypeEnum.BOX,
                CredentialDetails = new CredentialDetails()
                {
                    CredentialType = CredentialDetails.CredentialTypeEnum.OAUTH2,
                    EnterpriseId = "myEnterpriseId",
                    ClientId = "myClientId",
                    ClientSecret = "myClientSecret",
                    PublicKeyId = "myPublicIdKey",
                    Passphrase = "myPassphrase",
                    PrivateKey = "myPrivateKey"
                }
            };

            var createCredentialsResult = CreateCredentials(environmentId, credentials);
            string credentialId = createCredentialsResult.CredentialId;

            var getCredentialResult = GetCredentials(environmentId, credentialId);
            string privateKey = "privatekey";
            var privateKeyBytes = System.Text.Encoding.UTF8.GetBytes(privateKey);
            var base64PrivateKey = System.Convert.ToBase64String(privateKeyBytes);

            Model.Credentials updatedCredentials = new Model.Credentials()
            {
                SourceType = Model.Credentials.SourceTypeEnum.BOX,
                CredentialDetails = new CredentialDetails()
                {
                    CredentialType = CredentialDetails.CredentialTypeEnum.OAUTH2,
                    EnterpriseId = "myEnterpriseIdUpdated",
                    ClientId = "myClientIdUpdated",
                    ClientSecret = "myClientSecretUpdated",
                    PublicKeyId = "myPublicIdKeyUpdated",
                    Passphrase = "myPassphraseUpdated",
                    PrivateKey = base64PrivateKey
                }
            };

            //var updateCredentialResult = UpdateCredentials(_environmentId, credentialId, updatedCredentials);

            var deleteCredentialsResult = DeleteCredentials(environmentId, credentialId);

            Assert.IsNotNull(listCredentialsResult);
            Assert.IsTrue(!string.IsNullOrEmpty(listCredentialsResult.ResponseJson));
            Assert.IsNotNull(createCredentialsResult);
            Assert.IsTrue(!string.IsNullOrEmpty(createCredentialsResult.CredentialId));
            Assert.IsTrue(createCredentialsResult.SourceType == Credentials.SourceTypeEnum.BOX);
            Assert.IsTrue(createCredentialsResult.CredentialDetails.CredentialType == CredentialDetails.CredentialTypeEnum.OAUTH2);
            Assert.IsTrue(createCredentialsResult.CredentialDetails.EnterpriseId == "myEnterpriseId");
            Assert.IsNotNull(getCredentialResult);
            Assert.IsTrue(getCredentialResult.SourceType == Credentials.SourceTypeEnum.BOX);
            Assert.IsTrue(getCredentialResult.CredentialDetails.CredentialType == CredentialDetails.CredentialTypeEnum.OAUTH2);
            Assert.IsTrue(getCredentialResult.CredentialDetails.EnterpriseId == "myEnterpriseId");
            //Assert.IsNotNull(updateCredentialResult);
            //Assert.IsTrue(updateCredentialResult.CredentialDetails.EnterpriseId == "myEnterpriseIdUpdated");
            Assert.IsNotNull(deleteCredentialsResult);
            Assert.IsTrue(deleteCredentialsResult.CredentialId == credentialId);
            Assert.IsTrue(deleteCredentialsResult.Status == Model.DeleteCredentials.StatusEnum.DELETED);
        }
        #endregion

        #region Events
        [TestMethod]
        public void TestCreateEvent_Success()
        {
            Configuration configuration = new Configuration()
            {
                Name = createdConfigurationName,
                Description = createdConfigurationDescription,

            };

            var createConfigurationResults = CreateConfiguration(environmentId, configuration);
            createdConfigurationId = createConfigurationResults.ConfigurationId;

            var listCollectionsResult = ListCollections(environmentId);

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = createdCollectionLanguage,
                Name = createdCollectionName + "-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var _createdCollectionId = createCollectionResult.CollectionId;

            DocumentAccepted addDocumentResult;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                addDocumentResult = AddDocument(environmentId, _createdCollectionId, fs, metadata);
                createdDocumentId = addDocumentResult.DocumentId;
            }

            var getDocumentStatusResult = GetDocumentStatus(environmentId, _createdCollectionId, createdDocumentId);

            var queryResult = Query(environmentId, _createdCollectionId, naturalLanguageQuery: "what year did watson play jeopardy");

            CreateEventObject queryEvent = new CreateEventObject()
            {
                Type = CreateEventObject.TypeEnum.CLICK,
                Data = new EventData()
                {
                    EnvironmentId = environmentId,
                    SessionToken = queryResult.SessionToken,
                    CollectionId = _createdCollectionId,
                    DocumentId = createdDocumentId
                }
            };

            var createEventResult = CreateEvent(queryEvent);

            var deleteDocumentResult = DeleteDocument(environmentId, _createdCollectionId, createdDocumentId);
            var deleteCollectionResult = DeleteCollection(environmentId, _createdCollectionId);
            var deleteConfigurationResults = DeleteConfiguration(environmentId, createdConfigurationId);

            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(createEventResult);
            Assert.IsNotNull(createEventResult.Data);
            Assert.IsTrue(createEventResult.Data.EnvironmentId == environmentId);
            Assert.IsTrue(createEventResult.Data.SessionToken == queryResult.SessionToken);
            Assert.IsTrue(createEventResult.Data.CollectionId== _createdCollectionId);
            Assert.IsTrue(createEventResult.Data.DocumentId == createdDocumentId);
        }
        #endregion

        #region Metrics
        [TestMethod]
        public void TestMetrics_Success()
        {
            var getMetricsEventRateResult = GetMetricsEventRate();

            var getMetricsQueryResult = GetMetricsQuery();

            var getMetricsQueryEventResult = GetMetricsQueryEvent();

            var getMetricsQueryNoResultsResult = GetMetricsQueryNoResults();

            var getMetricsQueryTokenEventResult = GetMetricsQueryTokenEvent();

            var queryLogResult = QueryLog();

            Assert.IsNotNull(queryLogResult);
            Assert.IsNotNull(queryLogResult.Results);
            Assert.IsNotNull(getMetricsQueryTokenEventResult);
            Assert.IsNotNull(getMetricsQueryTokenEventResult.Aggregations);
            Assert.IsNotNull(getMetricsQueryNoResultsResult);
            Assert.IsNotNull(getMetricsQueryNoResultsResult.Aggregations);
            Assert.IsNotNull(getMetricsQueryEventResult);
            Assert.IsNotNull(getMetricsQueryEventResult.Aggregations);
            Assert.IsNotNull(getMetricsQueryResult);
            Assert.IsNotNull(getMetricsQueryResult.Aggregations);
            Assert.IsNotNull(getMetricsEventRateResult);
            Assert.IsNotNull(getMetricsEventRateResult.Aggregations);
        }
        #endregion

        #region Expansions
        [TestMethod]
        public void TestExpansions_Success()
        {
            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = CreateCollectionRequest.LanguageEnum.JA,
                Name = "expansions-collection-please-delete-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var expansionCollectionId = createCollectionResult.CollectionId;

            Expansions body = new Expansions()
            {
                _Expansions = new List<Expansion>()
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
                }
            };

            var createExpansionsResult = CreateExpansions(environmentId, expansionCollectionId, body);
            var listExpansionsResult = ListExpansions(environmentId, expansionCollectionId);
            var deleteExpansionResult = DeleteExpansions(environmentId, expansionCollectionId);
            DeleteCollection(environmentId, expansionCollectionId);

            Assert.IsNotNull(deleteExpansionResult);
            Assert.IsNotNull(listExpansionsResult);
            Assert.IsTrue(listExpansionsResult._Expansions[0].ExpandedTerms[0] == "expanded-term");
            Assert.IsTrue(listExpansionsResult._Expansions[0].InputTerms[0] == "input-term");
            Assert.IsNotNull(createExpansionsResult);
            Assert.IsTrue(createExpansionsResult._Expansions[0].ExpandedTerms[0] == "expanded-term");
            Assert.IsTrue(createExpansionsResult._Expansions[0].InputTerms[0] == "input-term");
        }
        #endregion

        #region Tokenization
        [TestMethod]
        public void TestTokenization_Success()
        {
            var collectionsList = ListCollections(environmentId);

            foreach (Collection collection in collectionsList.Collections)
            {
                if (!string.IsNullOrEmpty(collection.Description))
                {
                    if (collection.Description.ToLower().Contains("safe to delete") || collection.Description.Contains("Please delete me") || collection.Name.Contains("-updated") || collection.Name.Contains("-collection"))
                    {
                        DeleteCollection(environmentId, collection.CollectionId);
                        Console.WriteLine("deleted " + collection.CollectionId);
                    }
                }
            }

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = CreateCollectionRequest.LanguageEnum.JA,
                Name = "tokenization-collection-please-delete-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var tokenizationCollectionId = createCollectionResult.CollectionId;

            TokenDict tokenizationDictionary = new TokenDict()
            {
                TokenizationRules = new List<TokenDictRule>()
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
                }
            };

            try
            {
                var createTokenizationDictionaryResult = CreateTokenizationDictionary(environmentId, tokenizationCollectionId, tokenizationDictionary);
                var getTokenizationDictionaryStatusResult = GetTokenizationDictionaryStatus(environmentId, tokenizationCollectionId);
                var deleteTokenizationDictionary = DeleteTokenizationDictionary(environmentId, tokenizationCollectionId);

                Assert.IsNotNull(deleteTokenizationDictionary);
                Assert.IsNotNull(getTokenizationDictionaryStatusResult);
                Assert.IsTrue(getTokenizationDictionaryStatusResult.Status == TokenDictStatusResponse.StatusEnum.PENDING);
                Assert.IsNotNull(createTokenizationDictionaryResult);
                Assert.IsTrue(createTokenizationDictionaryResult.Status == TokenDictStatusResponse.StatusEnum.PENDING);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            DeleteCollection(environmentId, tokenizationCollectionId);
        }
        #endregion

        #region Stopword
        //[TestMethod]
        public void TestStopword_Success()
        {
            var collectionsList = ListCollections(environmentId);

            foreach (Collection collection in collectionsList.Collections)
            {
                if (!string.IsNullOrEmpty(collection.Description))
                {
                    if (collection.Description.ToLower().Contains("safe to delete") || collection.Description.Contains("Please delete me") || collection.Name.Contains("-updated") || collection.Name.Contains("-collection"))
                    {
                        DeleteCollection(environmentId, collection.CollectionId);
                        Console.WriteLine("deleted " + collection.CollectionId);
                    }
                }
            }

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = CreateCollectionRequest.LanguageEnum.JA,
                Name = "tokenization-collection-please-delete-" + Guid.NewGuid(),
                Description = createdCollectionDescription
            };

            var createCollectionResult = CreateCollection(environmentId, createCollectionRequest);
            var stopwordCollectionId = createCollectionResult.CollectionId;

            TokenDictStatusResponse createStopwordListResult;
            using (FileStream fs = File.OpenRead(stopwordFileToIngest))
            {
                createStopwordListResult = service.CreateStopwordList(environmentId, stopwordCollectionId, fs);
            }

            var deleteStopwordListResult = service.DeleteStopwordList(environmentId, stopwordCollectionId);

            Assert.IsNotNull(createStopwordListResult);
            Assert.IsTrue(createStopwordListResult.Status == TokenDictStatusResponse.StatusEnum.PENDING);
            Assert.IsNotNull(deleteStopwordListResult);
        }
        #endregion

        #region Gateway
        [TestMethod]
        public void TestGateway_Success()
        {
            var listGatewaysResult = service.ListGateways(environmentId);
            GatewayName gatewayName = new GatewayName()
            {
                Name = dotnetGatewayName
            };
            var createGatewayResult = service.CreateGateway(environmentId, gatewayName);
            var gatewayId = createGatewayResult.GatewayId;
            var getGatewayResult = service.GetGateway(environmentId, gatewayId);
            var deleteGatewayResult = service.DeleteGateway(environmentId, gatewayId);

            Assert.IsNotNull(deleteGatewayResult);
            Assert.IsTrue(deleteGatewayResult.GatewayId == gatewayId);
            Assert.IsTrue(!string.IsNullOrEmpty(deleteGatewayResult.Status));
            Assert.IsNotNull(getGatewayResult);
            Assert.IsTrue(getGatewayResult.GatewayId == gatewayId);
            Assert.IsTrue(getGatewayResult.Name == dotnetGatewayName);
            Assert.IsNotNull(createGatewayResult);
            Assert.IsTrue(createGatewayResult.Name == dotnetGatewayName);
            Assert.IsNotNull(listGatewaysResult);
        }
        #endregion

        #region IsEnvironmentReady
        private void IsEnvironmentReady(string environmentId)
        {
            var result = GetEnvironment(environmentId);
            Console.WriteLine(string.Format("\tEnvironment {0} status is {1}.", environmentId, result.Status));

            if (result.Status == Environment.StatusEnum.ACTIVE)
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

        #region Generated
        #region CreateEnvironment
        private Environment CreateEnvironment(CreateEnvironmentRequest body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateEnvironment()");
            var result = service.CreateEnvironment(body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateEnvironment() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateEnvironment()");
            }

            return result;
        }
        #endregion

        #region DeleteEnvironment
        private DeleteEnvironmentResponse DeleteEnvironment(string environmentId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteEnvironment()");
            var result = service.DeleteEnvironment(environmentId: environmentId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteEnvironment() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteEnvironment()");
            }

            return result;
        }
        #endregion

        #region GetEnvironment
        private Environment GetEnvironment(string environmentId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetEnvironment()");
            var result = service.GetEnvironment(environmentId: environmentId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetEnvironment() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetEnvironment()");
            }

            return result;
        }
        #endregion

        #region ListEnvironments
        private ListEnvironmentsResponse ListEnvironments(string name = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListEnvironments()");
            var result = service.ListEnvironments(name: name, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListEnvironments() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListEnvironments()");
            }

            return result;
        }
        #endregion

        #region ListFields
        private ListCollectionFieldsResponse ListFields(string environmentId, List<string> collectionIds, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListFields()");
            var result = service.ListFields(environmentId: environmentId, collectionIds: collectionIds, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListFields() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListFields()");
            }

            return result;
        }
        #endregion

        #region UpdateEnvironment
        private Environment UpdateEnvironment(string environmentId, UpdateEnvironmentRequest body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateEnvironment()");
            var result = service.UpdateEnvironment(environmentId: environmentId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateEnvironment() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateEnvironment()");
            }

            return result;
        }
        #endregion

        #region CreateConfiguration
        private Configuration CreateConfiguration(string environmentId, Configuration configuration, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateConfiguration()");
            var result = service.CreateConfiguration(environmentId: environmentId, configuration: configuration, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateConfiguration() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateConfiguration()");
            }

            return result;
        }
        #endregion

        #region DeleteConfiguration
        private DeleteConfigurationResponse DeleteConfiguration(string environmentId, string configurationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteConfiguration()");
            var result = service.DeleteConfiguration(environmentId: environmentId, configurationId: configurationId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteConfiguration() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteConfiguration()");
            }

            return result;
        }
        #endregion

        #region GetConfiguration
        private Configuration GetConfiguration(string environmentId, string configurationId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetConfiguration()");
            var result = service.GetConfiguration(environmentId: environmentId, configurationId: configurationId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetConfiguration() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetConfiguration()");
            }

            return result;
        }
        #endregion

        #region ListConfigurations
        private ListConfigurationsResponse ListConfigurations(string environmentId, string name = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListConfigurations()");
            var result = service.ListConfigurations(environmentId: environmentId, name: name, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListConfigurations() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListConfigurations()");
            }

            return result;
        }
        #endregion

        #region UpdateConfiguration
        private Configuration UpdateConfiguration(string environmentId, string configurationId, Configuration configuration, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateConfiguration()");
            var result = service.UpdateConfiguration(environmentId: environmentId, configurationId: configurationId, configuration: configuration, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateConfiguration() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateConfiguration()");
            }

            return result;
        }
        #endregion

        #region TestConfigurationInEnvironment
        private TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to TestConfigurationInEnvironment()");
            var result = service.TestConfigurationInEnvironment(environmentId: environmentId, configuration: configuration, step: step, configurationId: configurationId, file: file, metadata: metadata, fileContentType: fileContentType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("TestConfigurationInEnvironment() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to TestConfigurationInEnvironment()");
            }

            return result;
        }
        #endregion

        #region CreateCollection
        private Collection CreateCollection(string environmentId, CreateCollectionRequest body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateCollection()");
            var result = service.CreateCollection(environmentId: environmentId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateCollection() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateCollection()");
            }

            return result;
        }
        #endregion

        #region DeleteCollection
        private DeleteCollectionResponse DeleteCollection(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteCollection()");
            var result = service.DeleteCollection(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteCollection() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteCollection()");
            }

            return result;
        }
        #endregion

        #region GetCollection
        private Collection GetCollection(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetCollection()");
            var result = service.GetCollection(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetCollection() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetCollection()");
            }

            return result;
        }
        #endregion

        #region ListCollectionFields
        private ListCollectionFieldsResponse ListCollectionFields(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListCollectionFields()");
            var result = service.ListCollectionFields(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListCollectionFields() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListCollectionFields()");
            }

            return result;
        }
        #endregion

        #region ListCollections
        private ListCollectionsResponse ListCollections(string environmentId, string name = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListCollections()");
            var result = service.ListCollections(environmentId: environmentId, name: name, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListCollections() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListCollections()");
            }

            return result;
        }
        #endregion

        #region UpdateCollection
        private Collection UpdateCollection(string environmentId, string collectionId, UpdateCollectionRequest body = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateCollection()");
            var result = service.UpdateCollection(environmentId: environmentId, collectionId: collectionId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateCollection() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateCollection()");
            }

            return result;
        }
        #endregion

        #region CreateExpansions
        private Expansions CreateExpansions(string environmentId, string collectionId, Expansions body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateExpansions()");
            var result = service.CreateExpansions(environmentId: environmentId, collectionId: collectionId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateExpansions() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateExpansions()");
            }

            return result;
        }
        #endregion

        #region DeleteExpansions
        private BaseModel DeleteExpansions(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteExpansions()");
            var result = service.DeleteExpansions(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteExpansions() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteExpansions()");
            }

            return result;
        }
        #endregion

        #region ListExpansions
        private Expansions ListExpansions(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListExpansions()");
            var result = service.ListExpansions(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListExpansions() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListExpansions()");
            }

            return result;
        }
        #endregion

        #region CreateTokenizationDictionary
        private TokenDictStatusResponse CreateTokenizationDictionary(string environmentId, string collectionId, TokenDict tokenizationDictionary, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateTokenizationDictionary()");
            var result = service.CreateTokenizationDictionary(environmentId: environmentId, collectionId: collectionId, tokenizationDictionary: tokenizationDictionary, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateTokenizationDictionary() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateTokenizationDictionary()");
            }

            return result;
        }
        #endregion

        #region DeleteTokenizationDictionary
        private BaseModel DeleteTokenizationDictionary(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteTokenizationDictionary()");
            var result = service.DeleteTokenizationDictionary(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteTokenizationDictionary() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteTokenizationDictionary()");
            }

            return result;
        }
        #endregion

        #region GetTokenizationDictionaryStatus
        private TokenDictStatusResponse GetTokenizationDictionaryStatus(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetTokenizationDictionaryStatus()");
            var result = service.GetTokenizationDictionaryStatus(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetTokenizationDictionaryStatus() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetTokenizationDictionaryStatus()");
            }

            return result;
        }
        #endregion

        #region AddDocument
        private DocumentAccepted AddDocument(string environmentId, string collectionId, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddDocument()");
            var result = service.AddDocument(environmentId: environmentId, collectionId: collectionId, file: file, metadata: metadata, fileContentType: fileContentType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("AddDocument() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to AddDocument()");
            }

            return result;
        }
        #endregion

        #region DeleteDocument
        private DeleteDocumentResponse DeleteDocument(string environmentId, string collectionId, string documentId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteDocument()");
            var result = service.DeleteDocument(environmentId: environmentId, collectionId: collectionId, documentId: documentId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteDocument() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteDocument()");
            }

            return result;
        }
        #endregion

        #region GetDocumentStatus
        private DocumentStatus GetDocumentStatus(string environmentId, string collectionId, string documentId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetDocumentStatus()");
            var result = service.GetDocumentStatus(environmentId: environmentId, collectionId: collectionId, documentId: documentId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetDocumentStatus() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetDocumentStatus()");
            }

            return result;
        }
        #endregion

        #region UpdateDocument
        private DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateDocument()");
            var result = service.UpdateDocument(environmentId: environmentId, collectionId: collectionId, documentId: documentId, file: file, metadata: metadata, fileContentType: fileContentType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateDocument() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateDocument()");
            }

            return result;
        }
        #endregion

        #region FederatedQuery
        private QueryResponse FederatedQuery(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, bool? passages = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to FederatedQuery()");
            var result = service.FederatedQuery(environmentId: environmentId, collectionIds: collectionIds, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, aggregation: aggregation, count: count, returnFields: returnFields, offset: offset, sort: sort, highlight: highlight, deduplicate: deduplicate, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields, passages: passages, passagesFields: passagesFields, passagesCount: passagesCount, passagesCharacters: passagesCharacters, customData: customData);

            if (result != null)
            {
                Console.WriteLine("FederatedQuery() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to FederatedQuery()");
            }

            return result;
        }
        #endregion

        #region FederatedQueryNotices
        private QueryNoticesResponse FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to FederatedQueryNotices()");
            var result = service.FederatedQueryNotices(environmentId: environmentId, collectionIds: collectionIds, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, aggregation: aggregation, count: count, returnFields: returnFields, offset: offset, sort: sort, highlight: highlight, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields, customData: customData);

            if (result != null)
            {
                Console.WriteLine("FederatedQueryNotices() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to FederatedQueryNotices()");
            }

            return result;
        }
        #endregion

        #region Query
        private QueryResponse Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, bool? loggingOptOut = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Query()");
            var result = service.Query(environmentId: environmentId, collectionId: collectionId, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, passages: passages, aggregation: aggregation, count: count, returnFields: returnFields, offset: offset, sort: sort, highlight: highlight, passagesFields: passagesFields, passagesCount: passagesCount, passagesCharacters: passagesCharacters, deduplicate: deduplicate, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields, loggingOptOut: loggingOptOut, customData: customData);

            if (result != null)
            {
                Console.WriteLine("Query() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Query()");
            }

            return result;
        }
        #endregion

        #region QueryEntities
        private QueryEntitiesResponse QueryEntities(string environmentId, string collectionId, QueryEntities entityQuery, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to QueryEntities()");
            var result = service.QueryEntities(environmentId: environmentId, collectionId: collectionId, entityQuery: entityQuery, customData: customData);

            if (result != null)
            {
                Console.WriteLine("QueryEntities() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to QueryEntities()");
            }

            return result;
        }
        #endregion

        #region QueryNotices
        private QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to QueryNotices()");
            var result = service.QueryNotices(environmentId: environmentId, collectionId: collectionId, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, passages: passages, aggregation: aggregation, count: count, returnFields: returnFields, offset: offset, sort: sort, highlight: highlight, passagesFields: passagesFields, passagesCount: passagesCount, passagesCharacters: passagesCharacters, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields, customData: customData);

            if (result != null)
            {
                Console.WriteLine("QueryNotices() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to QueryNotices()");
            }

            return result;
        }
        #endregion

        #region QueryRelations
        private QueryRelationsResponse QueryRelations(string environmentId, string collectionId, QueryRelations relationshipQuery, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to QueryRelations()");
            var result = service.QueryRelations(environmentId: environmentId, collectionId: collectionId, relationshipQuery: relationshipQuery, customData: customData);

            if (result != null)
            {
                Console.WriteLine("QueryRelations() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to QueryRelations()");
            }

            return result;
        }
        #endregion

        #region AddTrainingData
        private TrainingQuery AddTrainingData(string environmentId, string collectionId, NewTrainingQuery body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddTrainingData()");
            var result = service.AddTrainingData(environmentId: environmentId, collectionId: collectionId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("AddTrainingData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to AddTrainingData()");
            }

            return result;
        }
        #endregion

        #region CreateTrainingExample
        private TrainingExample CreateTrainingExample(string environmentId, string collectionId, string queryId, TrainingExample body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateTrainingExample()");
            var result = service.CreateTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateTrainingExample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateTrainingExample()");
            }

            return result;
        }
        #endregion

        #region DeleteAllTrainingData
        private BaseModel DeleteAllTrainingData(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteAllTrainingData()");
            var result = service.DeleteAllTrainingData(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteAllTrainingData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteAllTrainingData()");
            }

            return result;
        }
        #endregion

        #region DeleteTrainingData
        private BaseModel DeleteTrainingData(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteTrainingData()");
            var result = service.DeleteTrainingData(environmentId: environmentId, collectionId: collectionId, queryId: queryId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteTrainingData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteTrainingData()");
            }

            return result;
        }
        #endregion

        #region DeleteTrainingExample
        private BaseModel DeleteTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteTrainingExample()");
            var result = service.DeleteTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteTrainingExample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteTrainingExample()");
            }

            return result;
        }
        #endregion

        #region GetTrainingData
        private TrainingQuery GetTrainingData(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetTrainingData()");
            var result = service.GetTrainingData(environmentId: environmentId, collectionId: collectionId, queryId: queryId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetTrainingData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetTrainingData()");
            }

            return result;
        }
        #endregion

        #region GetTrainingExample
        private TrainingExample GetTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetTrainingExample()");
            var result = service.GetTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetTrainingExample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetTrainingExample()");
            }

            return result;
        }
        #endregion

        #region ListTrainingData
        private TrainingDataSet ListTrainingData(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListTrainingData()");
            var result = service.ListTrainingData(environmentId: environmentId, collectionId: collectionId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListTrainingData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListTrainingData()");
            }

            return result;
        }
        #endregion

        #region ListTrainingExamples
        private TrainingExampleList ListTrainingExamples(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListTrainingExamples()");
            var result = service.ListTrainingExamples(environmentId: environmentId, collectionId: collectionId, queryId: queryId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListTrainingExamples() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListTrainingExamples()");
            }

            return result;
        }
        #endregion

        #region UpdateTrainingExample
        private TrainingExample UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, TrainingExamplePatch body, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateTrainingExample()");
            var result = service.UpdateTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId, body: body, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateTrainingExample() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateTrainingExample()");
            }

            return result;
        }
        #endregion

        #region DeleteUserData
        private BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteUserData()");
            var result = service.DeleteUserData(customerId: customerId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteUserData() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteUserData()");
            }

            return result;
        }
        #endregion

        #region CreateEvent
        private CreateEventResponse CreateEvent(CreateEventObject queryEvent, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateEvent()");
            var result = service.CreateEvent(queryEvent: queryEvent, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateEvent() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateEvent()");
            }

            return result;
        }
        #endregion

        #region GetMetricsEventRate
        private MetricResponse GetMetricsEventRate(DateTime? startTime = null, DateTime? endTime = null, string resultType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetMetricsEventRate()");
            var result = service.GetMetricsEventRate(startTime: startTime, endTime: endTime, resultType: resultType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetMetricsEventRate() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetMetricsEventRate()");
            }

            return result;
        }
        #endregion

        #region GetMetricsQuery
        private MetricResponse GetMetricsQuery(DateTime? startTime = null, DateTime? endTime = null, string resultType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetMetricsQuery()");
            var result = service.GetMetricsQuery(startTime: startTime, endTime: endTime, resultType: resultType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetMetricsQuery() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetMetricsQuery()");
            }

            return result;
        }
        #endregion

        #region GetMetricsQueryEvent
        private MetricResponse GetMetricsQueryEvent(DateTime? startTime = null, DateTime? endTime = null, string resultType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetMetricsQueryEvent()");
            var result = service.GetMetricsQueryEvent(startTime: startTime, endTime: endTime, resultType: resultType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetMetricsQueryEvent() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetMetricsQueryEvent()");
            }

            return result;
        }
        #endregion

        #region GetMetricsQueryNoResults
        private MetricResponse GetMetricsQueryNoResults(DateTime? startTime = null, DateTime? endTime = null, string resultType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetMetricsQueryNoResults()");
            var result = service.GetMetricsQueryNoResults(startTime: startTime, endTime: endTime, resultType: resultType, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetMetricsQueryNoResults() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetMetricsQueryNoResults()");
            }

            return result;
        }
        #endregion

        #region GetMetricsQueryTokenEvent
        private MetricTokenResponse GetMetricsQueryTokenEvent(long? count = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetMetricsQueryTokenEvent()");
            var result = service.GetMetricsQueryTokenEvent(count: count, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetMetricsQueryTokenEvent() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetMetricsQueryTokenEvent()");
            }

            return result;
        }
        #endregion

        #region QueryLog
        private LogQueryResponse QueryLog(string filter = null, string query = null, long? count = null, long? offset = null, List<string> sort = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to QueryLog()");
            var result = service.QueryLog(filter: filter, query: query, count: count, offset: offset, sort: sort, customData: customData);

            if (result != null)
            {
                Console.WriteLine("QueryLog() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to QueryLog()");
            }

            return result;
        }
        #endregion

        #region CreateCredentials
        private Credentials CreateCredentials(string environmentId, Credentials credentialsParameter, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateCredentials()");
            var result = service.CreateCredentials(environmentId: environmentId, credentialsParameter: credentialsParameter, customData: customData);

            if (result != null)
            {
                Console.WriteLine("CreateCredentials() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateCredentials()");
            }

            return result;
        }
        #endregion

        #region DeleteCredentials
        private DeleteCredentials DeleteCredentials(string environmentId, string credentialId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteCredentials()");
            var result = service.DeleteCredentials(environmentId: environmentId, credentialId: credentialId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("DeleteCredentials() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteCredentials()");
            }

            return result;
        }
        #endregion

        #region GetCredentials
        private Credentials GetCredentials(string environmentId, string credentialId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetCredentials()");
            var result = service.GetCredentials(environmentId: environmentId, credentialId: credentialId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("GetCredentials() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetCredentials()");
            }

            return result;
        }
        #endregion

        #region ListCredentials
        private CredentialsList ListCredentials(string environmentId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListCredentials()");
            var result = service.ListCredentials(environmentId: environmentId, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ListCredentials() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListCredentials()");
            }

            return result;
        }
        #endregion

        #region UpdateCredentials
        private Credentials UpdateCredentials(string environmentId, string credentialId, Credentials credentialsParameter, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateCredentials()");
            var result = service.UpdateCredentials(environmentId: environmentId, credentialId: credentialId, credentialsParameter: credentialsParameter, customData: customData);

            if (result != null)
            {
                Console.WriteLine("UpdateCredentials() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to UpdateCredentials()");
            }

            return result;
        }
        #endregion

        #endregion
    }
}
