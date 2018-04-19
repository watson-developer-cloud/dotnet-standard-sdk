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

//  Uncomment to skip environment creation
#define SHORT_TEST

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
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private DiscoveryService _service;
        private static string credentials = string.Empty;
        private static string version = "2017-11-07";

        private static string _createdEnvironmentId;
        private static string _createdConfigurationId;
        private static string _createdCollectionId;
        private static string _createdDocumentId;
        private static string _createdTrainingQueryId;
        private static string _createdTrainingExampleId;

        private string _createdEnvironmentName = "dotnet-test-environment";
        private string _createdEnvironmentDescription = "Environment created in the .NET SDK Examples";
        private int _createdEnvironmentSize = 1;
        private string _updatedEnvironmentName = "dotnet-test-environment-updated";
        private string _updatedEnvironmentDescription = "Environment created in the .NET SDK Examples - updated";
        private string _createdConfigurationName = "configName";
        private string _updatedConfigurationName = "configName-updated";
        private string _createdConfigurationDescription = "configDescription";
        private string _filepathToIngest = @"DiscoveryTestData\watson_beats_jeopardy.html";
        private string _metadata = "{\"Creator\": \"DotnetSDK Test\",\"Subject\": \"Discovery service\"}";

        private string _createdCollectionName = "createdCollectionName";
        private string _createdCollectionDescription = "createdCollectionDescription";
        private string _updatedCollectionName = "updatedCollectionName";
        private CreateCollectionRequest.LanguageEnum _createdCollectionLanguage = CreateCollectionRequest.LanguageEnum.EN;


        private string _naturalLanguageQuery = "Who beat Ken Jennings in Jeopardy!";
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        [TestInitialize]
        public void Setup()
        {
            if (string.IsNullOrEmpty(credentials))
            {
                try
                {
                    credentials = Utility.SimpleGet(
                        System.Environment.GetEnvironmentVariable("VCAP_URL"),
                        System.Environment.GetEnvironmentVariable("VCAP_USERNAME"),
                        System.Environment.GetEnvironmentVariable("VCAP_PASSWORD")).Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Failed to get credentials: {0}", e.Message));
                }

                Task.WaitAll();
                var vcapServices = JObject.Parse(credentials);

                _endpoint = vcapServices["discovery"]["url"].Value<string>();
                _username = vcapServices["discovery"]["username"].Value<string>();
                _password = vcapServices["discovery"]["password"].Value<string>();
            }

            _service = new DiscoveryService(_username, _password, "2017-11-07");
            _service.Endpoint = _endpoint;

            //DeleteExistingEnvironment();
        }

        [TestCleanup]
        public void Teardown()
        {
            //DeleteExistingEnvironment();
        }

        private void DeleteExistingEnvironment()
        {
            var listEnvironmentsResult = ListEnvironments();

            if (listEnvironmentsResult != null)
            {
                foreach (Environment environment in listEnvironmentsResult.Environments)
                {
                    if (!(bool)environment._ReadOnly)
                        DeleteEnvironment(environment.EnvironmentId);
                }
            }
        }

#region Environments
        [TestMethod]
        public void TestEnvironments_Success()
        {
#if SHORT_TEST
            var listEnvironmentsResult = ListEnvironments();

            if (listEnvironmentsResult.Environments.Count == 0)
            {
#endif
                CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
                {
                    Name = _createdEnvironmentName,
                    Description = _createdEnvironmentDescription
                };

                var createEnvironmentResults = CreateEnvironment(createEnvironmentRequest);
                _createdEnvironmentId = createEnvironmentResults.EnvironmentId;

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    Thread.Sleep(30000);
                });

                IsEnvironmentReady(_createdEnvironmentId);
                autoEvent.WaitOne();

                UpdateEnvironmentRequest updateEnvironmentRequest = new UpdateEnvironmentRequest()
                {
                    Name = _updatedEnvironmentName,
                    Description = _updatedEnvironmentDescription
                };

                var updateEnvironmentResult = UpdateEnvironment(_createdEnvironmentId, updateEnvironmentRequest);

                var deleteEnvironmentResult = DeleteEnvironment(_createdEnvironmentId);

                Assert.IsNotNull(updateEnvironmentResult);
                Assert.IsTrue(updateEnvironmentResult.Name == _updatedEnvironmentName);
                Assert.IsTrue(updateEnvironmentResult.Description == _updatedEnvironmentDescription);
                Assert.IsNotNull(createEnvironmentResults);
                Assert.IsNotNull(createEnvironmentResults.EnvironmentId);
                Assert.IsTrue(createEnvironmentResults.Name == _createdEnvironmentName);
                Assert.IsTrue(createEnvironmentResults.Description == _createdEnvironmentDescription);
                Assert.IsNotNull(deleteEnvironmentResult);
                Assert.IsTrue(deleteEnvironmentResult.Status == DeleteEnvironmentResponse.StatusEnum.DELETED);

                _createdEnvironmentId = null;
#if SHORT_TEST
            }
            else
            {
                Assert.IsTrue(true);
            }

            Assert.IsNotNull(listEnvironmentsResult);
#endif
        }
#endregion

#region Configurations
        [TestMethod]
        public void TestConfigurations_Success()
        {
#if SHORT_TEST
            var listEnvironmentsResult = ListEnvironments();

            if (listEnvironmentsResult.Environments.Count == 0)
            {
#endif
                CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
                {
                    Name = _createdEnvironmentName,
                    Description = _createdEnvironmentDescription,
                    Size = _createdEnvironmentSize
                };

                var createEnvironmentResults = CreateEnvironment(createEnvironmentRequest);
                _createdEnvironmentId = createEnvironmentResults.EnvironmentId;

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    Thread.Sleep(30000);
                });

                IsEnvironmentReady(_createdEnvironmentId);
                autoEvent.WaitOne();

                var listConfigurationsResults = ListConfigurations(_createdEnvironmentId);

                Configuration configuration = new Configuration()
                {
                    Name = _createdConfigurationName,
                    Description = _createdConfigurationDescription,

                };

                var createConfigurationResults = CreateConfiguration(_createdEnvironmentId, configuration);
                _createdConfigurationId = createConfigurationResults.ConfigurationId;

                var getConfigurationResults = GetConfiguration(_createdEnvironmentId, _createdConfigurationId);

                Configuration updateConfiguration = new Configuration()
                {
                    Name = _updatedConfigurationName
                };

                var updateConfigurationResults = UpdateConfiguration(_createdEnvironmentId, _createdConfigurationId, updateConfiguration);

                var deleteConfigurationResults = DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);
                var deleteEnvironmentResult = DeleteEnvironment(_createdEnvironmentId);

                Assert.IsNotNull(deleteConfigurationResults);
                Assert.IsTrue(deleteConfigurationResults.Status == DeleteConfigurationResponse.StatusEnum.DELETED);
                Assert.IsNotNull(updateConfigurationResults);
                Assert.IsTrue(updateConfigurationResults.ConfigurationId == _createdConfigurationId);
                Assert.IsTrue(updateConfigurationResults.Description == _createdConfigurationDescription);
                Assert.IsTrue(updateConfigurationResults.Name == _updatedConfigurationName);
                Assert.IsNotNull(getConfigurationResults);
                Assert.IsTrue(getConfigurationResults.ConfigurationId == _createdConfigurationId);
                Assert.IsTrue(getConfigurationResults.Description == _createdConfigurationDescription);
                Assert.IsTrue(getConfigurationResults.Name == _createdConfigurationName);
                Assert.IsNotNull(createConfigurationResults);
                Assert.IsTrue(createConfigurationResults.Name == _createdConfigurationName);
                Assert.IsTrue(createConfigurationResults.Description == _createdConfigurationDescription);
                Assert.IsNotNull(listConfigurationsResults);
                Assert.IsNotNull(listConfigurationsResults.Configurations);
                Assert.IsTrue(listConfigurationsResults.Configurations.Count > 0);

                _createdConfigurationId = null;
                _createdEnvironmentId = null;
#if SHORT_TEST
            }
            else
            {
                Assert.IsTrue(true);
            }
#endif
        }
#endregion

#region Collections
        [TestMethod]
        public void TestCollections_Success()
        {
#if SHORT_TEST
            var listEnvironmentsResult = ListEnvironments();

            if (listEnvironmentsResult.Environments.Count == 0)
            {
#endif
                CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
                {
                    Name = _createdEnvironmentName,
                    Description = _createdEnvironmentDescription,
                    Size = _createdEnvironmentSize
                };

                var createEnvironmentResults = CreateEnvironment(createEnvironmentRequest);
                _createdEnvironmentId = createEnvironmentResults.EnvironmentId;

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    Thread.Sleep(30000);
                });

                IsEnvironmentReady(_createdEnvironmentId);
                autoEvent.WaitOne();

                Configuration configuration = new Configuration()
                {
                    Name = _createdConfigurationName,
                    Description = _createdConfigurationDescription,

                };

                var createConfigurationResults = CreateConfiguration(_createdEnvironmentId, configuration);
                _createdConfigurationId = createConfigurationResults.ConfigurationId;

                var listCollectionsResult = ListCollections(_createdEnvironmentId);

                CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
                {
                    Language = _createdCollectionLanguage,
                    Name = _createdCollectionName,
                    Description = _createdCollectionDescription,
                    ConfigurationId = _createdConfigurationId
                };

                var createCollectionResult = CreateCollection(_createdEnvironmentId, createCollectionRequest);
                _createdCollectionId = createCollectionResult.CollectionId;

                var getCollectionResult = GetCollection(_createdEnvironmentId, _createdCollectionId);

                UpdateCollectionRequest updateCollectionRequest = new UpdateCollectionRequest()
                {
                    Name = _updatedCollectionName,
                };

                var updateCollectionResult = UpdateCollection(_createdEnvironmentId, _createdCollectionId, updateCollectionRequest);

                var listCollectionFieldsResult = ListCollectionFields(_createdEnvironmentId, _createdCollectionId);

                var deleteCollectionResult = DeleteCollection(_createdEnvironmentId, _createdCollectionId);
                var deleteConfigurationResults = DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);
                var deleteEnvironmentResult = DeleteEnvironment(_createdEnvironmentId);

                Assert.IsNotNull(deleteCollectionResult);
                Assert.IsTrue(deleteCollectionResult.Status == DeleteCollectionResponse.StatusEnum.DELETED);
                Assert.IsNotNull(listCollectionFieldsResult);
                Assert.IsNotNull(updateCollectionResult);
                Assert.IsTrue(updateCollectionResult.Name == _updatedCollectionName);
                Assert.IsTrue(updateCollectionResult.CollectionId == _createdCollectionId);
                Assert.IsNotNull(getCollectionResult);
                Assert.IsTrue(getCollectionResult.CollectionId == _createdCollectionId);
                Assert.IsTrue(getCollectionResult.Name == _createdCollectionName);
                Assert.IsTrue(getCollectionResult.Description == _createdCollectionDescription);
                Assert.IsNotNull(createCollectionResult);
                Assert.IsTrue(createCollectionResult.Name == _createdCollectionName);
                Assert.IsTrue(createCollectionResult.Description == _createdCollectionDescription);
                Assert.IsNotNull(listCollectionsResult);

                _createdEnvironmentId = null;
                _createdConfigurationId = null;
                _createdCollectionId = null;
#if SHORT_TEST
            }
            else
            {
                Assert.IsTrue(true);
            }
#endif
        }
#endregion

#region Preview Environment
        //[TestMethod]
        public void PreviewEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling PreviewEnvironment()..."));

            using (FileStream fs = File.OpenRead(_filepathToIngest))
            {
                var result = TestConfigurationInEnvironment(_createdEnvironmentId, _createdConfigurationId, "html_input");

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
#if SHORT_TEST
            var listEnvironmentsResult = ListEnvironments();

            if (listEnvironmentsResult.Environments.Count == 0)
            {
#endif
                CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
                {
                    Name = _createdEnvironmentName,
                    Description = _createdEnvironmentDescription,
                    Size = _createdEnvironmentSize
                };

                var createEnvironmentResults = CreateEnvironment(createEnvironmentRequest);
                _createdEnvironmentId = createEnvironmentResults.EnvironmentId;

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    Thread.Sleep(30000);
                });

                IsEnvironmentReady(_createdEnvironmentId);
                autoEvent.WaitOne();

                Configuration configuration = new Configuration()
                {
                    Name = _createdConfigurationName,
                    Description = _createdConfigurationDescription,

                };

                var createConfigurationResults = CreateConfiguration(_createdEnvironmentId, configuration);
                _createdConfigurationId = createConfigurationResults.ConfigurationId;

                var listCollectionsResult = ListCollections(_createdEnvironmentId);

                CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
                {
                    Language = _createdCollectionLanguage,
                    Name = _createdCollectionName,
                    Description = _createdCollectionDescription,
                    ConfigurationId = _createdConfigurationId
                };

                var createCollectionResult = CreateCollection(_createdEnvironmentId, createCollectionRequest);
                _createdCollectionId = createCollectionResult.CollectionId;

                DocumentAccepted addDocumentResult;
                using (FileStream fs = File.OpenRead(_filepathToIngest))
                {
                    addDocumentResult = AddDocument(_createdEnvironmentId, _createdCollectionId, fs as Stream, _metadata);
                    _createdDocumentId = addDocumentResult.DocumentId;
                }

                var getDocumentStatusResult = GetDocumentStatus(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);

                DocumentAccepted updateDocumentResult;
                using (FileStream fs = File.OpenRead(_filepathToIngest))
                {
                    updateDocumentResult = UpdateDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId, fs as Stream, _metadata);
                }

                var deleteDocumentResult = DeleteDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);
                var deleteCollectionResult = DeleteCollection(_createdEnvironmentId, _createdCollectionId);
                var deleteConfigurationResults = DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);
                var deleteEnvironmentResult = DeleteEnvironment(_createdEnvironmentId);

                Assert.IsNotNull(deleteDocumentResult);
                Assert.IsTrue(deleteDocumentResult.Status == DeleteDocumentResponse.StatusEnum.DELETED);
                Assert.IsNotNull(updateDocumentResult);
                Assert.IsTrue(updateDocumentResult.DocumentId == _createdDocumentId);
                Assert.IsNotNull(getDocumentStatusResult);
                Assert.IsTrue(getDocumentStatusResult.DocumentId == _createdDocumentId);
                Assert.IsNotNull(addDocumentResult);

                _createdDocumentId = null;
                _createdCollectionId = null;
                _createdConfigurationId = null;
                _createdEnvironmentId = null;
#if SHORT_TEST
            }
            else
            {
                Assert.IsTrue(true);
            }
#endif
        }
#endregion

#region Query
        [TestMethod]
        public void TestQuery()
        {
#if SHORT_TEST
            var listEnvironmentsResult = ListEnvironments();

            if (listEnvironmentsResult.Environments.Count == 0)
            {
#endif
                CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
                {
                    Name = _createdEnvironmentName,
                    Description = _createdEnvironmentDescription,
                    Size = _createdEnvironmentSize
                };

                var createEnvironmentResults = CreateEnvironment(createEnvironmentRequest);
                _createdEnvironmentId = createEnvironmentResults.EnvironmentId;

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    Thread.Sleep(30000);
                });

                IsEnvironmentReady(_createdEnvironmentId);
                autoEvent.WaitOne();

                Configuration configuration = new Configuration()
                {
                    Name = _createdConfigurationName,
                    Description = _createdConfigurationDescription,

                };

                var createConfigurationResults = CreateConfiguration(_createdEnvironmentId, configuration);
                _createdConfigurationId = createConfigurationResults.ConfigurationId;

                var listCollectionsResult = ListCollections(_createdEnvironmentId);

                CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
                {
                    Language = _createdCollectionLanguage,
                    Name = _createdCollectionName,
                    Description = _createdCollectionDescription,
                    ConfigurationId = _createdConfigurationId
                };

                var createCollectionResult = CreateCollection(_createdEnvironmentId, createCollectionRequest);
                _createdCollectionId = createCollectionResult.CollectionId;

                DocumentAccepted addDocumentResult;
                using (FileStream fs = File.OpenRead(_filepathToIngest))
                {
                    addDocumentResult = AddDocument(_createdEnvironmentId, _createdCollectionId, fs as Stream, _metadata);
                    _createdDocumentId = addDocumentResult.DocumentId;
                }

                var queryResult = Query(_createdEnvironmentId, _createdCollectionId, null, null, _naturalLanguageQuery);

                var deleteDocumentResult = DeleteDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);
                var deleteCollectionResult = DeleteCollection(_createdEnvironmentId, _createdCollectionId);
                var deleteConfigurationResults = DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);
                var deleteEnvironmentResult = DeleteEnvironment(_createdEnvironmentId);

                Assert.IsNotNull(queryResult);

                _createdDocumentId = null;
                _createdCollectionId = null;
                _createdConfigurationId = null;
                _createdEnvironmentId = null;
#if SHORT_TEST
            }
            else
            {
                Assert.IsTrue(true);
            }
#endif
        }
#endregion

#region Notices
        [TestMethod]
        public void TestGetNotices()
        {
#if SHORT_TEST
            var listEnvironmentsResult = ListEnvironments();

            if (listEnvironmentsResult.Environments.Count == 0)
            {
#endif
                CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
                {
                    Name = _createdEnvironmentName,
                    Description = _createdEnvironmentDescription,
                    Size = _createdEnvironmentSize
                };

                var createEnvironmentResults = CreateEnvironment(createEnvironmentRequest);
                _createdEnvironmentId = createEnvironmentResults.EnvironmentId;

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    Thread.Sleep(30000);
                });

                IsEnvironmentReady(_createdEnvironmentId);
                autoEvent.WaitOne();

                Configuration configuration = new Configuration()
                {
                    Name = _createdConfigurationName,
                    Description = _createdConfigurationDescription,

                };

                var createConfigurationResults = CreateConfiguration(_createdEnvironmentId, configuration);
                _createdConfigurationId = createConfigurationResults.ConfigurationId;

                var listCollectionsResult = ListCollections(_createdEnvironmentId);

                CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
                {
                    Language = _createdCollectionLanguage,
                    Name = _createdCollectionName,
                    Description = _createdCollectionDescription,
                    ConfigurationId = _createdConfigurationId
                };

                var createCollectionResult = CreateCollection(_createdEnvironmentId, createCollectionRequest);
                _createdCollectionId = createCollectionResult.CollectionId;

                DocumentAccepted addDocumentResult;
                using (FileStream fs = File.OpenRead(_filepathToIngest))
                {
                    addDocumentResult = AddDocument(_createdEnvironmentId, _createdCollectionId, fs as Stream, _metadata);
                    _createdDocumentId = addDocumentResult.DocumentId;
                }

                var queryResult = Query(_createdEnvironmentId, _createdCollectionId, null, null, _naturalLanguageQuery);
                var queryNoticesResult = QueryNotices(_createdEnvironmentId, _createdCollectionId, null, null, _naturalLanguageQuery, true);

                var deleteDocumentResult = DeleteDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);
                var deleteCollectionResult = DeleteCollection(_createdEnvironmentId, _createdCollectionId);
                var deleteConfigurationResults = DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);
                var deleteEnvironmentResult = DeleteEnvironment(_createdEnvironmentId);

                Assert.IsNotNull(queryNoticesResult);

                _createdEnvironmentId = null;
                _createdConfigurationId = null;
                _createdCollectionId = null;
                _createdDocumentId = null;
#if SHORT_TEST
            }
            else
            {
                Assert.IsTrue(true);
            }
#endif
        }
#endregion

#region Training Data
        [TestMethod]
        public void TestTrainingData()
        {
#if SHORT_TEST
            var listEnvironmentsResult = ListEnvironments();

            if (listEnvironmentsResult.Environments.Count == 0)
            {
#endif
                CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
                {
                    Name = _createdEnvironmentName,
                    Description = _createdEnvironmentDescription,
                    Size = _createdEnvironmentSize
                };

                var createEnvironmentResults = CreateEnvironment(createEnvironmentRequest);
                _createdEnvironmentId = createEnvironmentResults.EnvironmentId;

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    Thread.Sleep(30000);
                });

                IsEnvironmentReady(_createdEnvironmentId);
                autoEvent.WaitOne();

                Configuration configuration = new Configuration()
                {
                    Name = _createdConfigurationName,
                    Description = _createdConfigurationDescription,

                };

                var createConfigurationResults = CreateConfiguration(_createdEnvironmentId, configuration);
                _createdConfigurationId = createConfigurationResults.ConfigurationId;

                var listCollectionsResult = ListCollections(_createdEnvironmentId);

                CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
                {
                    Language = _createdCollectionLanguage,
                    Name = _createdCollectionName,
                    Description = _createdCollectionDescription,
                    ConfigurationId = _createdConfigurationId
                };

                var createCollectionResult = CreateCollection(_createdEnvironmentId, createCollectionRequest);
                _createdCollectionId = createCollectionResult.CollectionId;

                DocumentAccepted addDocumentResult;
                using (FileStream fs = File.OpenRead(_filepathToIngest))
                {
                    addDocumentResult = AddDocument(_createdEnvironmentId, _createdCollectionId, fs as Stream, _metadata);
                    _createdDocumentId = addDocumentResult.DocumentId;
                }

                var getDocumentStatusResult = GetDocumentStatus(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);

                DocumentAccepted updateDocumentResult;
                using (FileStream fs = File.OpenRead(_filepathToIngest))
                {
                    updateDocumentResult = UpdateDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId, fs as Stream, _metadata);
                }

                var listTrainingDataResult = ListTrainingData(_createdEnvironmentId, _createdCollectionId);

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

                var addTrainingDataResult = AddTrainingData(_createdEnvironmentId, _createdCollectionId, newTrainingQuery);
                _createdTrainingQueryId = addTrainingDataResult.QueryId;

                var getTrainingDataResult = GetTrainingData(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId);

                var trainingExample = new TrainingExample()
                {
                    DocumentId = _createdDocumentId,
                    Relevance = 1
                };

                var createTrainingExampleResult = CreateTrainingExample(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId, trainingExample);
                _createdTrainingExampleId = createTrainingExampleResult.DocumentId;

                var getTrainingExampleResult = GetTrainingExample(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId, _createdTrainingExampleId);

                var updateTrainingExample = new TrainingExamplePatch()
                {
                    CrossReference = "crossReference",
                    Relevance = 1
                };

                var updateTrainingExampleResult = UpdateTrainingExample(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId, _createdTrainingExampleId, updateTrainingExample);

                var deleteTrainingExampleResult = DeleteTrainingExample(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId, _createdTrainingExampleId);
                var deleteTrainingDataResult = DeleteTrainingData(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId);
                var deleteAllTrainingDataResult = DeleteAllTrainingData(_createdEnvironmentId, _createdCollectionId);
                var deleteDocumentResult = DeleteDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);
                var deleteCollectionResult = DeleteCollection(_createdEnvironmentId, _createdCollectionId);
                var deleteConfigurationResults = DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);
                var deleteEnvironmentResult = DeleteEnvironment(_createdEnvironmentId);

                Assert.IsNotNull(deleteAllTrainingDataResult);
                Assert.IsNotNull(deleteTrainingDataResult);
                Assert.IsNotNull(deleteTrainingExampleResult);
                Assert.IsNotNull(updateTrainingExampleResult);
                Assert.IsNotNull(getTrainingExampleResult);
                Assert.IsNotNull(createTrainingExampleResult);
                Assert.IsNotNull(getTrainingDataResult);
                Assert.IsNotNull(addTrainingDataResult);
                Assert.IsNotNull(listTrainingDataResult);

                _createdTrainingExampleId = null;
                _createdTrainingQueryId = null;
                _createdCollectionId = null;
                _createdConfigurationId = null;
                _createdEnvironmentId = null;
                _createdDocumentId = null;
#if SHORT_TEST
            }
            else
            {
                Assert.IsTrue(true);
            }
#endif
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
            var result = _service.CreateEnvironment(body: body, customData: customData);

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
            var result = _service.DeleteEnvironment(environmentId: environmentId, customData: customData);

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
            var result = _service.GetEnvironment(environmentId: environmentId, customData: customData);

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
            var result = _service.ListEnvironments(name: name, customData: customData);

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
            var result = _service.ListFields(environmentId: environmentId, collectionIds: collectionIds, customData: customData);

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
            var result = _service.UpdateEnvironment(environmentId: environmentId, body: body, customData: customData);

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
            var result = _service.CreateConfiguration(environmentId: environmentId, configuration: configuration, customData: customData);

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
            var result = _service.DeleteConfiguration(environmentId: environmentId, configurationId: configurationId, customData: customData);

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
            var result = _service.GetConfiguration(environmentId: environmentId, configurationId: configurationId, customData: customData);

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
            var result = _service.ListConfigurations(environmentId: environmentId, name: name, customData: customData);

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
            var result = _service.UpdateConfiguration(environmentId: environmentId, configurationId: configurationId, configuration: configuration, customData: customData);

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
        private TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.Stream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to TestConfigurationInEnvironment()");
            var result = _service.TestConfigurationInEnvironment(environmentId: environmentId, configuration: configuration, step: step, configurationId: configurationId, file: file, metadata: metadata, fileContentType: fileContentType, customData: customData);

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
            var result = _service.CreateCollection(environmentId: environmentId, body: body, customData: customData);

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
            var result = _service.DeleteCollection(environmentId: environmentId, collectionId: collectionId, customData: customData);

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
            var result = _service.GetCollection(environmentId: environmentId, collectionId: collectionId, customData: customData);

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
            var result = _service.ListCollectionFields(environmentId: environmentId, collectionId: collectionId, customData: customData);

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
            var result = _service.ListCollections(environmentId: environmentId, name: name, customData: customData);

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
            var result = _service.UpdateCollection(environmentId: environmentId, collectionId: collectionId, body: body, customData: customData);

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
            var result = _service.CreateExpansions(environmentId: environmentId, collectionId: collectionId, body: body, customData: customData);

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
            var result = _service.DeleteExpansions(environmentId: environmentId, collectionId: collectionId, customData: customData);

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
            var result = _service.ListExpansions(environmentId: environmentId, collectionId: collectionId, customData: customData);

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

#region AddDocument
        private DocumentAccepted AddDocument(string environmentId, string collectionId, System.IO.Stream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to AddDocument()");
            var result = _service.AddDocument(environmentId: environmentId, collectionId: collectionId, file: file, metadata: metadata, fileContentType: fileContentType, customData: customData);

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
            var result = _service.DeleteDocument(environmentId: environmentId, collectionId: collectionId, documentId: documentId, customData: customData);

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
            var result = _service.GetDocumentStatus(environmentId: environmentId, collectionId: collectionId, documentId: documentId, customData: customData);

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
        private DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.Stream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to UpdateDocument()");
            var result = _service.UpdateDocument(environmentId: environmentId, collectionId: collectionId, documentId: documentId, file: file, metadata: metadata, fileContentType: fileContentType, customData: customData);

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
        private QueryResponse FederatedQuery(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to FederatedQuery()");
            var result = _service.FederatedQuery(environmentId: environmentId, collectionIds: collectionIds, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, aggregation: aggregation, count: count, returnFields: returnFields, offset: offset, sort: sort, highlight: highlight, deduplicate: deduplicate, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields, customData: customData);

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
            var result = _service.FederatedQueryNotices(environmentId: environmentId, collectionIds: collectionIds, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, aggregation: aggregation, count: count, returnFields: returnFields, offset: offset, sort: sort, highlight: highlight, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields, customData: customData);

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
        private QueryResponse Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Query()");
            var result = _service.Query(environmentId: environmentId, collectionId: collectionId, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, passages: passages, aggregation: aggregation, count: count, returnFields: returnFields, offset: offset, sort: sort, highlight: highlight, passagesFields: passagesFields, passagesCount: passagesCount, passagesCharacters: passagesCharacters, deduplicate: deduplicate, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields, customData: customData);

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
            var result = _service.QueryEntities(environmentId: environmentId, collectionId: collectionId, entityQuery: entityQuery, customData: customData);

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
            var result = _service.QueryNotices(environmentId: environmentId, collectionId: collectionId, filter: filter, query: query, naturalLanguageQuery: naturalLanguageQuery, passages: passages, aggregation: aggregation, count: count, returnFields: returnFields, offset: offset, sort: sort, highlight: highlight, passagesFields: passagesFields, passagesCount: passagesCount, passagesCharacters: passagesCharacters, deduplicateField: deduplicateField, similar: similar, similarDocumentIds: similarDocumentIds, similarFields: similarFields, customData: customData);

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
            var result = _service.QueryRelations(environmentId: environmentId, collectionId: collectionId, relationshipQuery: relationshipQuery, customData: customData);

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
            var result = _service.AddTrainingData(environmentId: environmentId, collectionId: collectionId, body: body, customData: customData);

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
            var result = _service.CreateTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, body: body, customData: customData);

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
            var result = _service.DeleteAllTrainingData(environmentId: environmentId, collectionId: collectionId, customData: customData);

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
            var result = _service.DeleteTrainingData(environmentId: environmentId, collectionId: collectionId, queryId: queryId, customData: customData);

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
            var result = _service.DeleteTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId, customData: customData);

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
            var result = _service.GetTrainingData(environmentId: environmentId, collectionId: collectionId, queryId: queryId, customData: customData);

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
            var result = _service.GetTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId, customData: customData);

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
            var result = _service.ListTrainingData(environmentId: environmentId, collectionId: collectionId, customData: customData);

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
            var result = _service.ListTrainingExamples(environmentId: environmentId, collectionId: collectionId, queryId: queryId, customData: customData);

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
            var result = _service.UpdateTrainingExample(environmentId: environmentId, collectionId: collectionId, queryId: queryId, exampleId: exampleId, body: body, customData: customData);

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

#endregion
    }
}
