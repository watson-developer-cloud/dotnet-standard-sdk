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

using Environment = IBM.Watson.Discovery.v1.Model.Environment;
using System;
using System.Collections.Generic;
using System.IO;
using IBM.Watson.Discovery.v1.Model;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Authentication.Iam;

namespace IBM.Watson.Discovery.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
        string versionDate = "{versionDate}";
        private string environmentId;
        private string collectionId;
        private string configurationId;
        private string documentId;
        private string queryId;
        private string exampleId;
        private string filepathToIngest = @"TestData\watson_beats_jeopardy.html";
        private string stopwordFileToIngest = @"TestData\stopwords.txt";
        private string metadata = "{\"Creator\": \".NET SDK Example\",\"Subject\": \"Discovery service\"}";
        private string naturalLanguageQuery = "Who beat Ken Jennings in Jeopardy!";
        private string gatewayId;
        private string credentialId;
        private string sessionToken;
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

            example.ListTrainingData();
            example.AddTrainingData();
            example.GetTrainingData();
            example.ListTrainingExamples();
            example.CreateTrainingExample();
            example.UpdateTrainingExample();
            example.GetTrainingExample();

            example.DeleteUserData();

            example.CreateEvent();
            example.QueryLog();
            example.GetMetricsQuery();
            example.GetMetricsQueryEvent();
            example.GetMetricsQueryNoResult();
            example.GetMetricsQueryEventRate();
            example.GetMetricsQueryTokenEvent();

            example.ListCredentials();
            example.CreateCredentials();
            example.GetCredentials();
            example.UpdateCredentials();

            example.ListGateways();
            example.CreateGateway();
            example.GetGateway();

            example.DeleteGateway();
            example.DeleteCredentials();
            example.DeleteTrainingExample();
            example.DeleteTrainingData();
            example.DeleteAllTraininData();
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
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

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
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateEnvironment(
                name: "my_environment",
                description: "My environment"
                );

            Console.WriteLine(result.Response);
        }

        public void GetEnvironment()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetEnvironment(
                environmentId: "{environmentId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateEnvironment()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateEnvironment(
                environmentId: "{environmentId}",
                name: "Updated name",
                description: "Updated description"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteEnvironment()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteEnvironment(
                environmentId: "{environmentId}"
                );

            Console.WriteLine(result.Response);
        }

        public void ListFields()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListFields(
                environmentId: "{environmentId}",
                collectionIds: new List<string>() { "{collection_id1}", "{collection_id2}" }
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Configurations
        public void ListConfigurations()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListConfigurations(
                environmentId: "{environmentId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateConfiguration()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateConfiguration(
                environmentId: "{environmentId}",
                name: "doc-config"
                );

            Console.WriteLine(result.Response);

            configurationId = result.Result.ConfigurationId;
        }

        public void GetConfiguration()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetConfiguration(
                environmentId: "{environmentId}",
                configurationId: "{configurationId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateConfiguration()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateConfiguration(
                environmentId: "{environmentId}",
                configurationId: "{configurationId}",
                name: "new-config"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteConfiguration()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteConfiguration(
                environmentId: "{environmentId}",
                configurationId: "{configurationId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Collections
        public void ListCollections()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListCollections(
                environmentId: "{environmentId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateCollection(
                environmentId: "{environmentId}",
                configurationId: "{configurationId}",
                name: "{collectionName}",
                language: "{language}"
                );

            Console.WriteLine(result.Response);

            collectionId = result.Result.CollectionId;
        }

        public void GetCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetCollection(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateCollection(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                name: "new_name"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteCollection(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void ListCollectionFields()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListCollectionFields(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Query Modifications
        public void ListExpansions()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListExpansions(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateExpansions()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

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
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                expansions: expansions
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteExpansion()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteExpansions(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void GetTokenizationDictionaryStatus()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetTokenizationDictionaryStatus(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateTokenizationDictionary()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

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
                    environmentId: "{environmentId}",
                    collectionId: "{collectionId}",
                    tokenizationRules: tokenizationRules
                    );

            Console.WriteLine(result.Response);
        }

        public void DeleteTokenizationDictionary()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteTokenizationDictionary(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void GetStopwordListStatus()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetStopwordListStatus(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateStopwordList()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            DetailedResponse<TokenDictStatusResponse> result;
            using (FileStream fs = File.OpenRead("{filepath}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.CreateStopwordList(
                        environmentId: "{environmentId}",
                        collectionId: "{collectionId}",
                        stopwordFile: ms,
                        stopwordFilename: "{filepath}"
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void DeleteStopwordList()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteStopwordList(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Documents
        public void AddDocument()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            DetailedResponse<DocumentAccepted> result;
            using (FileStream fs = File.OpenRead("{filePath}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.AddDocument(
                    environmentId: "{environmentId}",
                    collectionId: "{collectionId}",
                    file: ms,
                    filename: "{fileName}",
                    fileContentType: "{fileContentType}",
                    metadata: metadata
                    );

                    documentId = result.Result.DocumentId;
                }
            }

            Console.WriteLine(result.Response);
        }

        public void GetDocument()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetDocumentStatus(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                documentId: "{documentId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateDocument()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            DetailedResponse<DocumentAccepted> result;
            using (FileStream fs = File.OpenRead("{filePath}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.UpdateDocument(
                        environmentId: "{environmentId}",
                        collectionId: "{collectionId}",
                        documentId: "{documentId}",
                        file: ms,
                        filename: "{fileName}",
                        fileContentType: "{fileContentType}",
                        metadata: metadata
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void DeleteDocument()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteDocument(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                documentId: "{documentId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Queries
        public void Query()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Query(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                filter: "{filter}",
                query: "{query}",
                aggregation: "{aggregation}",
                _return: "{return_fields}"
                );

            Console.WriteLine(result.Response);

            sessionToken = result.Result.SessionToken;
        }

        public void QueryNotices()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.QueryNotices(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                query: "{query}"
                );

            Console.WriteLine(result.Response);
        }

        public void FederatedQuery()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.FederatedQuery(
                environmentId: "{environmentId}",
                collectionIds: "{collectionIds}",
                naturalLanguageQuery: "{naturalLanguageQuery}",
                _return: "{returnFields}"
                );

            Console.WriteLine(result.Response);
        }

        public void FederatedQueryNotices()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.FederatedQueryNotices(
                environmentId: "{environmentId}",
                naturalLanguageQuery: "{naturalLanguageQuery}",
                collectionIds: new List<string> { "{collectionId}" }
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Training Data
        public void ListTrainingData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListTrainingData(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void AddTrainingData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var examples = new List<TrainingExample>()
            {
                new TrainingExample()
                {
                    DocumentId = "{documentId}",
                    CrossReference = "{crossReference}"
                }
            };

            var result = service.AddTrainingData(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                naturalLanguageQuery: "{naturalLanguageQuery}",
                filter: "{filter}",
                examples: examples
                );
            queryId = result.Result.QueryId;

            Console.WriteLine(result.Response);
        }

        public void DeleteAllTraininData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteAllTrainingData(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}"
                );

            Console.WriteLine(result.Response);
        }

        public void GetTrainingData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetTrainingData(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                queryId: "{queryId}"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteTrainingData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteTrainingData(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                queryId: "{queryId}"
                );

            Console.WriteLine(result.Response);
        }

        public void ListTrainingExamples()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListTrainingExamples(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                queryId: "{queryId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateTrainingExample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateTrainingExample(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                queryId: "{queryId}",
                documentId: "{documentId}"
                );

            Console.WriteLine(result.Response);

            exampleId = result.Result.DocumentId;
        }

        public void DeleteTrainingExample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteTrainingExample(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                queryId: "{queryId}",
                exampleId: "{exampleId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateTrainingExample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.UpdateTrainingExample(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                queryId: "{queryId}",
                exampleId: "{exampleId}",
                crossReference: "{crossReference}",
                relevance: 1
                );

            Console.WriteLine(result.Response);
        }

        public void GetTrainingExample()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetTrainingExample(
                environmentId: "{environmentId}",
                collectionId: "{collectionId}",
                queryId: "{queryId}",
                exampleId: "{exampleId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region User data
        public void DeleteUserData()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteUserData(
                customerId: "{id}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Events and Feedback
        public void CreateEvent()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var data = new EventData()
            {
                EnvironmentId = "{environmentId}",
                SessionToken = "{sessionToken}",
                CollectionId = "{collectionId}",
                DocumentId = "{documentId}"
            };

            var result = service.CreateEvent(
                type: CreateEventResponse.TypeEnumValue.CLICK,
                data: data
                );

            Console.WriteLine(result.Response);
        }

        public void QueryLog()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.QueryLog();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQuery()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetMetricsQuery();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQueryEvent()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetMetricsQueryEvent();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQueryNoResult()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetMetricsQueryNoResults();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQueryEventRate()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetMetricsEventRate();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQueryTokenEvent()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetMetricsQueryTokenEvent();

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Credentials
        public void ListCredentials()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListCredentials(
                environmentId: "{environmentId}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateCredentials()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var credentialDetails = new CredentialDetails()
            {
                CredentialType = "{credentialType}",
                EnterpriseId = "{EnterpriseId}",
                ClientId = "{ClientId}",
                ClientSecret = "{ClientSecret}",
                PublicKeyId = "{PublicKeyId}",
                Passphrase = "{Passphrase}",
                PrivateKey = "{PrivateKey}"
            };

            var result = service.CreateCredentials(
                environmentId: "{environmentId}",
                sourceType: "{sourceType}",
                credentialDetails: credentialDetails
                );

            Console.WriteLine(result.Response);

            credentialId = result.Result.CredentialId;
        }

        public void GetCredentials()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetCredentials(
                environmentId: "{environmentId}",
                credentialId: "{credentialId}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateCredentials()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            string privateKey = "{privatekey}";
            var privateKeyBytes = System.Text.Encoding.UTF8.GetBytes(privateKey);
            var base64PrivateKey = Convert.ToBase64String(privateKeyBytes);

            var updatedCredentialDetails = new CredentialDetails()
            {
                CredentialType = "{credentialType}",
                EnterpriseId = "{EnterpriseId}",
                ClientId = "{ClientId}",
                ClientSecret = "{ClientSecret}",
                PublicKeyId = "{PublicKeyId}",
                Passphrase = "{Passphrase}",
                PrivateKey = "{PrivateKey}"
            };

            var result = service.UpdateCredentials(
                environmentId: "{environmentId}",
                credentialId: "{credentialId}",
                sourceType: "{sourceType}",
                credentialDetails: updatedCredentialDetails
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteCredentials()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteCredentials(
                environmentId: "{environmentId}",
                credentialId: "{credentialId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Gateway Configuration
        public void ListGateways()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListGateways(
                environmentId: "{environmentId}"
                );
                
            Console.WriteLine(result.Response);
        }

        public void CreateGateway()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateGateway(
                environmentId: "{environmentId}",
                name: "gateway"
                );

            Console.WriteLine(result.Response);

            gatewayId = result.Result.GatewayId;
        }

        public void GetGateway()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.GetGateway(
                environmentId: "{environmentId}",
                gatewayId: "{gatewayId}"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteGateway()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            DiscoveryService service = new DiscoveryService("2019-04-30", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteGateway(
                environmentId: "{environmentId}",
                gatewayId: "{gatewayId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
