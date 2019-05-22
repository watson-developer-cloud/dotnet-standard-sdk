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

using IBM.Cloud.SDK.Core.Util;
using Environment = IBM.Watson.Discovery.v1.Model.Environment;
using System;
using System.Collections.Generic;
using System.IO;
using IBM.Watson.Discovery.v1.Model;
using IBM.Cloud.SDK.Core.Http;

namespace IBM.Watson.Discovery.v1.Examples
{
    class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        string versionDate = "{versionDate}";
        private string environmentId;
        private string collectionId;
        private string configurationId;
        private string documentId;
        private string queryId;
        private string exampleId;
        private string filepathToIngest = @"DiscoveryTestData\watson_beats_jeopardy.html";
        private string stopwordFileToIngest = @"DiscoveryTestData\stopwords.txt";
        private string metadata = "{\"Creator\": \".NET SDK Example\",\"Subject\": \"Discovery service\"}";
        private string naturalLanguageQuery = "Who beat Ken Jennings in Jeopardy!";
        private static string configurationName;
        private static string updatedConfigurationName;
        private static string collectionName;
        private static string updatedCollectionName;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            configurationName = Guid.NewGuid().ToString();
            updatedConfigurationName = configurationName + "-updated";
            collectionName = Guid.NewGuid().ToString();
            updatedCollectionName = collectionName + "-updated";

            example.ListEnvironments();
            //example.CreateEnvironment();      // Commented since we can only have one environment.
            example.GetEnvironment();
            //example.UpdateEnvironment();      // Commented since we do not want to change our current environment.

            example.ListConfigurations();
            example.CreateConfiguration();
            example.GetConfiguration();
            example.UpdateConfiguration();

            example.TestConfigurationInEnvironment();

            example.ListCollections();
            example.CreateCollection();
            example.GetCollection();
            example.UpdateCollection();
            example.ListCollectionFields();
            example.ListFields();

            example.ListExpansions();
            example.CreateExpansions();
            example.GetTokenizationDictionaryStatus();
            example.CreateTokenizationDictionary();
            example.GetStopwordListStatus();
            example.CreateStopwordList();

            example.AddDocument();
            example.GetDocument();
            example.UpdateDocument();

            example.Query();
            example.QueryNotices();
            example.FederatedQuery();
            example.FederatedQueryNotices();
            example.QueryEntities();
            example.QueryRelations();


            example.DeleteDocument();
            example.DeleteStopwordList();
            example.DeleteTokenizationDictionary();
            example.DeleteExpansion();
            example.DeleteCollection();
            example.DeleteConfiguration();
            //example.DeleteEnvironment();      // Commented since we do not want to delete our environment.

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Environments
        public void ListEnvironments()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.ListEnvironments();

            Console.WriteLine(result.Response);

            foreach (Environment environment in result.Result.Environments)
            {
                if (environment._ReadOnly != true)
                {
                    environmentId = environment.EnvironmentId;
                }
            }
        }

        public void CreateEnvironment()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.CreateEnvironment(
                name: "My environment"
                );

            Console.WriteLine(result.Response);
        }

        public void GetEnvironment()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.GetEnvironment(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateEnvironment()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.UpdateEnvironment(
                environmentId: environmentId,
                name: "My updated environment"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteEnvironment()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.DeleteEnvironment(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void ListFields()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.ListFields(
                environmentId: environmentId,
                collectionIds: new List<string>() { collectionId }
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Configurations
        public void ListConfigurations()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.ListConfigurations(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateConfiguration()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.CreateConfiguration(
                environmentId: environmentId,
                name: configurationName
                );

            Console.WriteLine(result.Response);

            configurationId = result.Result.ConfigurationId;
        }

        public void GetConfiguration()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.GetConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateConfiguration()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.UpdateConfiguration(
                environmentId: environmentId,
                configurationId: configurationId,
                name: updatedConfigurationName
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteConfiguration()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Test Configuration in Environment
        public void TestConfigurationInEnvironment()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    var result = service.TestConfigurationInEnvironment(
                        environmentId: environmentId,
                        configurationId: configurationId,
                        file: ms, filename: "watson_beats_jeopardy.html",
                        fileContentType: "text/html"
                        );

                    Console.WriteLine(result.Response);
                }
            }

        }
        #endregion

        #region Collections
        public void ListCollections()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.ListCollections(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateCollection()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName
                );

            Console.WriteLine(result.Response);

            collectionId = result.Result.CollectionId;
        }

        public void GetCollection()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.GetCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateCollection()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.UpdateCollection(
                environmentId: environmentId,
                collectionId: collectionId,
                name: updatedCollectionName
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteCollection()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void ListCollectionFields()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.ListCollectionFields(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Query Modifications
        public void ListExpansions()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.ListExpansions(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateExpansions()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

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

            var result = service.CreateExpansions(
                environmentId: environmentId,
                collectionId: collectionId,
                expansions: expansions
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteExpansion()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.DeleteExpansions(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void GetTokenizationDictionaryStatus()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.GetTokenizationDictionaryStatus(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateTokenizationDictionary()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

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

            var result = service.CreateTokenizationDictionary(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    tokenizationRules: tokenizationRules
                    );

            Console.WriteLine(result.Response);
        }

        public void DeleteTokenizationDictionary()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.DeleteTokenizationDictionary(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void GetStopwordListStatus()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.GetStopwordListStatus(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateStopwordList()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            DetailedResponse<TokenDictStatusResponse> result;
            using (FileStream fs = File.OpenRead(stopwordFileToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    result = service.CreateStopwordList(
                        environmentId: environmentId,
                        collectionId: collectionId, 
                        stopwordFile: ms,
                        stopwordFilename: Path.GetFileName(stopwordFileToIngest)
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void DeleteStopwordList()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.DeleteStopwordList(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Documents
        public void AddDocument()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            DetailedResponse<DocumentAccepted> result;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    result = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );

                    documentId = result.Result.DocumentId;
                }
            }

            Console.WriteLine(result.Response);
        }

        public void GetDocument()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.GetDocumentStatus(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateDocument()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            DetailedResponse<DocumentAccepted> result;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    result = service.UpdateDocument(
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

            Console.WriteLine(result.Response);
        }

        public void DeleteDocument()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.DeleteDocument(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Queries
        public void Query()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.Query(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                returnFields: "extracted_metadata.sha1"
                );

            Console.WriteLine(result.Response);
        }

        public void QueryNotices()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.QueryNotices(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                passages: true
                );

            Console.WriteLine(result.Response);
        }

        public void FederatedQuery()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.FederatedQuery(
                environmentId: environmentId,
                naturalLanguageQuery: naturalLanguageQuery,
                returnFields: "extracted_metadata.sha1"
                );

            Console.WriteLine(result.Response);
        }

        public void FederatedQueryNotices()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.FederatedQueryNotices(
                environmentId: environmentId,
                naturalLanguageQuery: naturalLanguageQuery,
                collectionIds: new List<string> { collectionId }
                );

            Console.WriteLine(result.Response);
        }

        public void QueryEntities()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.QueryEntities(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void QueryRelations()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            DiscoveryService service = new DiscoveryService(tokenOptions, versionDate);

            var result = service.QueryRelations(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Training Data
        #endregion

        #region User data
        #endregion

        #region Events and Feedback
        #endregion

        #region Credentials
        #endregion

        #region Gateway Configuration
        #endregion
    }
}
