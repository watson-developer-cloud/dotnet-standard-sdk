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
        #endregion

        #region Documents
        #endregion

        #region Queries
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
