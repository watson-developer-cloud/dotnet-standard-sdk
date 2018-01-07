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
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.IO;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Example
{
    public class DiscoveryServiceExample
    {
        public string _username;
        public string _password;
        public string _endpoint;
        public DiscoveryService _discovery;

        private static string _existingEnvironmentId;

        private static string _createdEnvironmentId;
        private static string _createdConfigurationId;
        private static string _createdCollectionId;
        private static string _createdDocumentId;
        private string _createdTrainingQueryId;
        private string _createdTrainingExampleId;

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

        #region Constructor
        public DiscoveryServiceExample(string url, string username, string password)
        {
            _discovery = new DiscoveryService(username, password, DiscoveryService.DISCOVERY_VERSION_DATE_2017_11_07);
            _discovery.Endpoint = url;

            GetEnvironments();
            if(!string.IsNullOrEmpty(_existingEnvironmentId))
                DeleteExistingEnvironment();
            CreateEnvironment();
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("\nChecking environment status in 30 seconds.");
                System.Threading.Thread.Sleep(30000);
                IsEnvironmentReady(_createdEnvironmentId);
            });
            autoEvent.WaitOne();
            GetEnvironment();
            UpdateEnvironment();

            GetConfigurations();
            CreateConfiguration();
            GetConfiguration();
            UpdateConfiguration();

            //PreviewEnvironment();

            GetCollections();
            CreateCollection();
            GetCollection();
            GetCollectionFields();
            UpdateCollection();

            AddDocument();
            GetDocument();
            UpdateDocument();

            Query();
            GetNotices();

            ListTrainingData();
            AddTrainingData();
            GetTrainingData();
            CreateTrainingExample();
            GetTrainingExample();
            UpdateTrainingExample();

            DeleteTrainingExample();
            DeleteTrainingData();
            DeleteAllTrainingData();
            DeleteDocument();
            DeleteCollection();
            DeleteConfiguration();
            DeleteEnvironment();

            Console.WriteLine("\n~ Discovery examples complete.");
        }
        #endregion

        #region Environments
        public void GetEnvironments()
        {
            Console.WriteLine(string.Format("\nCalling GetEnvironments()..."));
            var result = _discovery.ListEnvironments();

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

                foreach (Model.Environment environment in result.Environments)
                {
                    if (!(bool)environment._ReadOnly)
                    {
                        _existingEnvironmentId = environment.EnvironmentId;
                        Console.WriteLine(string.Format("\nEnvironment found, Setting environment {0} to delete", environment.Name));
                    }
                }
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }

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

            if(result != null)
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
        }

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
        }

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
        }

        public void DeleteEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling DeleteEnvironment()..."));
            var result = _discovery.DeleteEnvironment(_createdEnvironmentId);

            if(result != null)
            {
                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("result is null.");
                }

                _createdEnvironmentId = null;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Is Environment Ready
        private bool IsEnvironmentReady(string environmentId)
        {
            var result = _discovery.GetEnvironment(environmentId);
            Console.WriteLine(string.Format("\tEnvironment {0} status is {1}.", environmentId, result.Status));

            if (result.Status == Model.Environment.StatusEnum.ACTIVE)
            {
                autoEvent.Set();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(30000);
                    IsEnvironmentReady(environmentId);
                });
            }

            return result.Status == Model.Environment.StatusEnum.ACTIVE;
        }
        #endregion

        #region Preview Environment
        private void PreviewEnvironment()
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
            }
        }
        #endregion

        #region Configurations
        private void GetConfigurations()
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
        }

        private void CreateConfiguration()
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
        }

        private void GetConfiguration()
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
        }

        private void UpdateConfiguration()
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
        }

        private void DeleteConfiguration()
        {
            Console.WriteLine(string.Format("\nCalling DeleteConfiguration()..."));

            var result = _discovery.DeleteConfiguration(_createdEnvironmentId, _createdConfigurationId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Collections
        private void GetCollections()
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
        }

        private void CreateCollection()
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
        }

        private void GetCollection()
        {
            Console.WriteLine(string.Format("\nCalling GetCollection()..."));

            var result = _discovery.GetCollection(_createdEnvironmentId, _createdCollectionId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }

        private void UpdateCollection()
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
        }

        private void GetCollectionFields()
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
        }

        private void DeleteCollection()
        {
            Console.WriteLine(string.Format("\nCalling DeleteCollection()..."));

            if (string.IsNullOrEmpty(_createdEnvironmentId))
                throw new ArgumentNullException("_createdEnvironmentId is null");

            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException("_createdCollectionId is null");

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
        }
        #endregion

        #region Documents
        private void AddDocument()
        {
            Console.WriteLine(string.Format("\nCalling AddDocument()..."));
            using (FileStream fs = File.OpenRead(_filepathToIngest))
            {
                var result = _discovery.AddDocument(_createdEnvironmentId, _createdCollectionId, fs as Stream, _metadata);

                if (result != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                    _createdDocumentId = result.DocumentId;
                }
                else
                {
                    Console.WriteLine("result is null.");
                }
            }
        }

        private void GetDocument()
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
        }

        private void UpdateDocument()
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
            }
        }

        private void DeleteDocument()
        {
            Console.WriteLine(string.Format("\nCalling DeleteDocument()..."));

            if (string.IsNullOrEmpty(_createdEnvironmentId))
                throw new ArgumentNullException("_createdEnvironmentId is null");

            if (string.IsNullOrEmpty(_createdCollectionId))
                throw new ArgumentNullException("_createdCollectionId is null");

            if (string.IsNullOrEmpty(_createdDocumentId))
                throw new ArgumentNullException("_createdDocumentId is null");

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
        }
        #endregion

        #region Query
        private void Query()
        {
            var result = _discovery.Query(_createdEnvironmentId, _createdCollectionId, null, null, _naturalLanguageQuery);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Notices
        private void GetNotices()
        {
            var result = _discovery.QueryNotices(_createdEnvironmentId, _createdCollectionId, null, null, _naturalLanguageQuery, true);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region List Training Data
        public void ListTrainingData()
        {
            Console.WriteLine(string.Format("\nCalling ListTrainingData()..."));

            var result = _discovery.ListTrainingData(_createdEnvironmentId, _createdCollectionId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Add Training Data
        public void AddTrainingData()
        {
            Console.WriteLine(string.Format("\nCalling AddTrainingData()..."));

            var newTrainingQuery = new NewTrainingQuery()
            {
                NaturalLanguageQuery = "why is the sky blue",
                Filter = "text:meteorology"
            };

            var result = _discovery.AddTrainingData(_createdEnvironmentId, _createdCollectionId, newTrainingQuery);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdTrainingQueryId = result.QueryId;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Get Training Data
        public void GetTrainingData()
        {
            Console.WriteLine(string.Format("\nCalling GetTrainingData()..."));

            var result = _discovery.GetTrainingData(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Create Training Example
        public void CreateTrainingExample()
        {
            Console.WriteLine(string.Format("\nCalling CreateTrainingExample()..."));

            var trainingExample = new TrainingExample()
            {
                DocumentId = _createdDocumentId,
                Relevance = 1
            };

            var result = _discovery.CreateTrainingExample(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId, trainingExample);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdTrainingExampleId = result.DocumentId;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Get Training Example
        public void GetTrainingExample()
        {
            Console.WriteLine(string.Format("\nCalling GetTrainingExample()..."));

            var result = _discovery.GetTrainingExample(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId, _createdTrainingExampleId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Update Training Example
        public void UpdateTrainingExample()
        {
            Console.WriteLine(string.Format("\nCalling UpdateTrainingExample()..."));

            var trainingExample = new TrainingExamplePatch()
            {
                CrossReference = "crossReference",
                Relevance = 1
            };

            var result = _discovery.UpdateTrainingExample(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId, _createdTrainingExampleId, trainingExample);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Delete Training Example
        public void DeleteTrainingExample()
        {
            Console.WriteLine(string.Format("\nCalling DeleteTrainingExample()..."));

            var result = _discovery.DeleteTrainingExample(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId, _createdTrainingExampleId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdTrainingExampleId = null;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Delete Training Data
        public void DeleteTrainingData()
        {
            Console.WriteLine(string.Format("\nCalling DeleteTrainingData()..."));

            var result = _discovery.DeleteTrainingData(_createdEnvironmentId, _createdCollectionId, _createdTrainingQueryId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                _createdTrainingQueryId = null;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Delete All Training Data
        public void DeleteAllTrainingData()
        {
            Console.WriteLine(string.Format("\nCalling ListTrainingData()..."));

            var result = _discovery.DeleteAllTrainingData(_createdEnvironmentId, _createdCollectionId);

            if (result != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion

        #region Delete Existing Environment
        public void DeleteExistingEnvironment()
        {
            Console.WriteLine(string.Format("\nCalling DeleteExistingEnvironment({0})...", _existingEnvironmentId));
            var result = _discovery.DeleteEnvironment(_existingEnvironmentId);

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

                _existingEnvironmentId = null;
            }
            else
            {
                Console.WriteLine("result is null.");
            }
        }
        #endregion
    }
}
