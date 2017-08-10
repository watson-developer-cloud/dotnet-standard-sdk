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
using Newtonsoft.Json.Linq;
using System.Threading;
using Newtonsoft.Json;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Http.Extensions;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.IntegrationTests
{
    [TestClass]
    public class DiscoveryIntegrationTests
    {
        public string _username;
        public string _password;
        public string _endpoint;
        public DiscoveryService _discovery;

        private static string _createdEnvironmentId;
        private static string _createdConfigurationId;
        private static string _createdCollectionId;
        private static string _createdDocumentId;

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
            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
                File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            _endpoint = vcapServices["discovery"][0]["credentials"]["url"].Value<string>();
            _username = vcapServices["discovery"][0]["credentials"]["username"].Value<string>();
            _password = vcapServices["discovery"][0]["credentials"]["password"].Value<string>();

            _discovery = new DiscoveryService(_username, _password, DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01);
        }

        #region Environments
        [TestMethod]
        public void GetEnvironments()
        {
            Console.WriteLine(string.Format("\nCalling GetEnvironments()..."));
            var result = _discovery.ListEnvironments();

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Environments.Count > 0);
        }

        [TestMethod]
        public void CreateEnvironment()
        {
            CreateEnvironmentRequest createEnvironmentRequest = new CreateEnvironmentRequest()
            {
                Name = _createdEnvironmentName,
                Description = _createdEnvironmentDescription,
                Size = _createdEnvironmentSize
            };

            Console.WriteLine(string.Format("\nCalling CreateEnvironment()..."));
            var result = _discovery.CreateEnvironment(createEnvironmentRequest);

            if (result != null)
            {
                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("result is null.");
                }

                _createdEnvironmentId = result.EnvironmentId;
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.EnvironmentId);
            Assert.IsTrue(result.Name == _createdEnvironmentName);
            Assert.IsTrue(result.Description == _createdEnvironmentDescription);
        }

        #region Is Environment Ready
        [TestMethod]
        public void WaitForEnvironment()
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Checking environment status in 30 seconds...");
                System.Threading.Thread.Sleep(30000);
            });

            IsEnvironmentReady(_createdEnvironmentId);
            autoEvent.WaitOne();

            Assert.IsTrue(true);
        }

        private void IsEnvironmentReady(string environmentId)
        {
            var result = _discovery.GetEnvironment(environmentId);
            Console.WriteLine(string.Format("\tEnvironment {0} status is {1}.", environmentId, result.Status));

            if (result.Status == ModelEnvironment.StatusEnum.ACTIVE)
            {
                autoEvent.Set();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(30000);
                    Console.WriteLine("Checking environment status in 30 seconds...");
                    IsEnvironmentReady(environmentId);
                });
            }
        }
        #endregion

        [TestMethod]
        public void GetEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling GetEnvironment()..."));
            var result = _discovery.GetEnvironment(_createdEnvironmentId);

            if (result != null)
            {
                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("result is null.");
                }
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.EnvironmentId == _createdEnvironmentId);
        }

        [TestMethod]
        public void UpdateEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling UpdateEnvironment()..."));

            UpdateEnvironmentRequest updateEnvironmentRequest = new UpdateEnvironmentRequest()
            {
                Name = _updatedEnvironmentName,
                Description = _updatedEnvironmentDescription
            };

            var result = _discovery.UpdateEnvironment(_createdEnvironmentId, updateEnvironmentRequest);

            if (result != null)
            {
                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("result is null.");
                }
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == _updatedEnvironmentName);
            Assert.IsTrue(result.Description == _updatedEnvironmentDescription);
        }
        #endregion

        #region Configurations
        [TestMethod]
        public void GetConfigurations()
        {
            Console.WriteLine(string.Format("\nCalling GetConfigurations()..."));

            var result = _discovery.ListConfigurations(_createdEnvironmentId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Configurations);
            Assert.IsTrue(result.Configurations.Count > 0);
        }

        [TestMethod]
        public void CreateConfiguration()
        {
            Console.WriteLine(string.Format("\nCalling CreateConfiguration()..."));

            Configuration configuration = new Configuration()
            {
                Name = _createdConfigurationName,
                Description = _createdConfigurationDescription,

            };

            var result = _discovery.CreateConfiguration(_createdEnvironmentId, configuration);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdConfigurationId = result.ConfigurationId;
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == _createdConfigurationName);
            Assert.IsTrue(result.Description == _createdConfigurationDescription);
        }

        [TestMethod]
        public void GetConfiguration()
        {
            Console.WriteLine(string.Format("\nCalling GetConfiguration()..."));

            var result = _discovery.GetConfiguration(_createdEnvironmentId, _createdConfigurationId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ConfigurationId == _createdConfigurationId);
            Assert.IsTrue(result.Description == _createdConfigurationDescription);
            Assert.IsTrue(result.Name == _createdConfigurationName);
        }

        [TestMethod]
        public void UpdateConfiguration()
        {
            Console.WriteLine(string.Format("\nCalling UpdateConfiguration()..."));

            Configuration configuration = new Configuration()
            {
                Name = _updatedConfigurationName
            };

            var result = _discovery.UpdateConfiguration(_createdEnvironmentId, _createdConfigurationId, configuration);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ConfigurationId == _createdConfigurationId);
            Assert.IsTrue(result.Description == _createdConfigurationDescription);
            Assert.IsTrue(result.Name == _updatedConfigurationName);
        }
        #endregion

        #region Collections
        [TestMethod]
        public void GetCollections()
        {
            Console.WriteLine(string.Format("\nCalling GetCollections()..."));

            var result = _discovery.ListCollections(_createdEnvironmentId);

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

        [TestMethod]
        public void CreateCollection()
        {
            Console.WriteLine(string.Format("\nCalling CreateCollection()..."));

            CreateCollectionRequest createCollectionRequest = new CreateCollectionRequest()
            {
                Language = _createdCollectionLanguage,
                Name = _createdCollectionName,
                Description = _createdCollectionDescription,
                ConfigurationId = _createdConfigurationId
            };

            var result = _discovery.CreateCollection(_createdEnvironmentId, createCollectionRequest);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdCollectionId = result.CollectionId;
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == _createdCollectionName);
            Assert.IsTrue(result.Description == _createdCollectionDescription);
        }

        [TestMethod]
        public void GetCollection()
        {
            Console.WriteLine(string.Format("\nCalling GetCollection()..."));

            if (string.IsNullOrEmpty(_createdEnvironmentId))
                Assert.Fail("_createdEnvironmentId is null");

            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("_createdCollectionId is null");

            var result = _discovery.GetCollection(_createdEnvironmentId, _createdCollectionId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.CollectionId == _createdCollectionId);
            Assert.IsTrue(result.Name == _createdCollectionName);
            Assert.IsTrue(result.Description == _createdCollectionDescription);
        }

        [TestMethod]
        public void UpdateCollection()
        {
            Console.WriteLine(string.Format("\nCalling UpdateCollection()..."));

            UpdateCollectionRequest updateCollectionRequest = new UpdateCollectionRequest()
            {
                Name = _updatedCollectionName,
            };

            var result = _discovery.UpdateCollection(_createdEnvironmentId, _createdCollectionId, updateCollectionRequest);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == _updatedCollectionName);
            Assert.IsTrue(result.CollectionId == _createdCollectionId);
        }

        [TestMethod]
        public void GetCollectionFields()
        {
            Console.WriteLine(string.Format("\nCalling GetCollectionFields()..."));

            var result = _discovery.ListCollectionFields(_createdEnvironmentId, _createdCollectionId);

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
        #endregion

        #region Preview Environment
        [TestMethod]
        public void PreviewEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling PreviewEnvironment()..."));

            using (FileStream fs = File.OpenRead(_filepathToIngest))
            {
                var result = _discovery.TestConfigurationInEnvironment(_createdEnvironmentId, null, "enrich", _createdConfigurationId, fs as Stream, _metadata);

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
        public void AddDocument()
        {
            Console.WriteLine(string.Format("\nCalling AddDocument()..."));
            using (FileStream fs = File.OpenRead(_filepathToIngest))
            {
                var result = _discovery.AddDocument(_createdEnvironmentId, _createdCollectionId, _createdConfigurationId, fs as Stream, _metadata);

                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                    _createdDocumentId = result.DocumentId;
                }
                else
                {
                    Console.WriteLine("result is null.");
                }

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void GetDocument()
        {
            Console.WriteLine(string.Format("\nCalling GetDocument()..."));

            var result = _discovery.GetDocumentStatus(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.DocumentId == _createdDocumentId);
        }

        [TestMethod]
        public void UpdateDocument()
        {
            Console.WriteLine(string.Format("\nCalling UpdateDocument()..."));

            using (FileStream fs = File.OpenRead(_filepathToIngest))
            {
                var result = _discovery.UpdateDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId, fs as Stream, _metadata);

                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("result is null.");
                }

                Assert.IsNotNull(result);
                Assert.IsTrue(result.DocumentId == _createdDocumentId);
            }
        }
        #endregion

        #region Query
        [TestMethod]
        public void Query()
        {
            Console.WriteLine(string.Format("\nCalling Query()..."));

            var result = _discovery.Query(_createdEnvironmentId, _createdCollectionId, null, null, _naturalLanguageQuery, true);

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
        #endregion

        #region Notices
        [TestMethod]
        public void GetNotices()
        {
            Console.WriteLine(string.Format("\nCalling GetNoticies()..."));

            var result = _discovery.QueryNotices(_createdEnvironmentId, _createdCollectionId, null, null, _naturalLanguageQuery, true);

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
        #endregion

        #region Tear Down
        [TestMethod]
        public void DeleteDocument()
        {
            Console.WriteLine(string.Format("\nCalling DeleteDocument()..."));

            if (string.IsNullOrEmpty(_createdEnvironmentId))
                Assert.Fail("_createdEnvironmentId is null");

            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("_createdCollectionId is null");

            if (string.IsNullOrEmpty(_createdDocumentId))
                Assert.Fail("_createdDocumentId is null");

            var result = _discovery.DeleteDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdDocumentId = null;
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == DeleteDocumentResponse.StatusEnum.DELETED);
        }

        [TestMethod]
        public void DeleteCollection()
        {
            Console.WriteLine(string.Format("\nCalling DeleteCollection()..."));

            if (string.IsNullOrEmpty(_createdEnvironmentId))
                Assert.Fail("_createdEnvironmentId is null");

            if (string.IsNullOrEmpty(_createdCollectionId))
                Assert.Fail("_createdCollectionId is null");

            var result = _discovery.DeleteCollection(_createdEnvironmentId, _createdCollectionId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdCollectionId = null;
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == DeleteCollectionResponse.StatusEnum.DELETED);
        }

        [TestMethod]
        public void DeleteConfiguration()
        {
            Console.WriteLine(string.Format("\nCalling DeleteConfiguration()..."));

            if (string.IsNullOrEmpty(_createdEnvironmentId))
                Assert.Fail("_createdEnvironmentId is null");

            if (string.IsNullOrEmpty(_createdConfigurationId))
                Assert.Fail("_createdConfigurationId is null");

            var result = _discovery.DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdConfigurationId = null;
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == DeleteConfigurationResponse.StatusEnum.DELETED);
        }

        [TestMethod]
        public void DeleteEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling DeleteEnvironment()..."));

            if (string.IsNullOrEmpty(_createdEnvironmentId))
                Assert.Fail("_createdEnvironmentId is null");

            var result = _discovery.DeleteEnvironment(_createdEnvironmentId);

            if (result != null)
            {
                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                    _createdEnvironmentId = null;
                }
                else
                {
                    Console.WriteLine("result is null.");
                }
            }
            else
            {
                Console.WriteLine("result is null.");
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == DeleteEnvironmentResponse.StatusEnum.DELETED);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
                File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            string _endpoint = vcapServices["discovery"][0]["credentials"]["url"].Value<string>();
            string _username = vcapServices["discovery"][0]["credentials"]["username"].Value<string>();
            string _password = vcapServices["discovery"][0]["credentials"]["password"].Value<string>();

            DiscoveryService _discovery = new DiscoveryService(_username, _password, DiscoveryService.DISCOVERY_VERSION_DATE_2017_08_01);

            if (!string.IsNullOrEmpty(_createdEnvironmentId) && !string.IsNullOrEmpty(_createdCollectionId) && !string.IsNullOrEmpty(_createdDocumentId))
                _discovery.DeleteDocument(_createdEnvironmentId, _createdCollectionId, _createdDocumentId);

            if (!string.IsNullOrEmpty(_createdEnvironmentId) && !string.IsNullOrEmpty(_createdCollectionId))
                _discovery.DeleteCollection(_createdEnvironmentId, _createdCollectionId);

            if (!string.IsNullOrEmpty(_createdEnvironmentId) && !string.IsNullOrEmpty(_createdConfigurationId))
                _discovery.DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);

            if (!string.IsNullOrEmpty(_createdEnvironmentId))
                _discovery.DeleteEnvironment(_createdEnvironmentId);
        }
        #endregion
    }
}
