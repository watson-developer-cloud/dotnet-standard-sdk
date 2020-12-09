/**
* (C) Copyright IBM Corp. 2019, 2020.
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
using System.Collections.Generic;
using System.IO;
using IBM.Watson.Discovery.v2.Model;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Authentication.Cp4d;

namespace IBM.Watson.Discovery.v2.Examples
{
    public class ServiceExample
    {
        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Collections
        public void ListCollections()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.ListCollections(
                projectId: "{project_id}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Queries
        public void Query()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.Query(
                projectId: "{project_id}",
                query: "{field}:{value}"
                );

            Console.WriteLine(result.Response);
        }

        public void GetAutocompletion()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.GetAutocompletion(
                projectId: "{project_id}",
                prefix: "Ho",
                count: 5
                );

            Console.WriteLine(result.Response);
        }

        public void QueryNotices()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.QueryNotices(
                projectId: "{project_id}",
                query: "{field}:{value}"
                );

            Console.WriteLine(result.Response);
        }

        public void ListFields()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.ListFields(
                projectId: "{project_id}",
                collectionIds: new List<string>() { "{collection_id}" }
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Component Settings
        public void GetComponentSettings()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.GetComponentSettings(
                projectId: "{project_id}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Documents
        public void AddDocument()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");


            DetailedResponse<DocumentAccepted> result = null;
            using (FileStream fs = File.OpenRead("path/to/file.pdf"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);

                    result = service.AddDocument(
                        projectId: "{project_id}",
                        collectionId: "{collection_id}",
                        file: ms,
                        filename: "example-file",
                        fileContentType: "application/pdf"
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void UpdateDocument()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.UpdateDocument(
                projectId: "{project_id}",
                collectionId: "{collection_id}",
                documentId: "{document_id}",
                filename: "updated-file-name"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteDocument()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.DeleteDocument(
                projectId: "{project_id}",
                collectionId: "{collection_id}",
                documentId: "{document_id}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Training Data
        public void ListTrainingQueries()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.ListTrainingQueries(
                projectId: "{project_id}"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteTrainingQueries()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.DeleteTrainingQueries(
                projectId: "{project_id}"
                );

            Console.WriteLine(result.Response);
        }

        public void CreateTrainingQuery()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            TrainingExample trainingExample = new TrainingExample()
            {
                CollectionId = "{collection_id}",
                DocumentId = "{document_id}"
            };

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.CreateTrainingQuery(
                projectId: "{project_id}",
                examples: new List<TrainingExample>() { trainingExample },
                naturalLanguageQuery: "This is an example of a query"
                );

            Console.WriteLine(result.Response);
        }

        public void GetTrainingQuery()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var result = service.GetTrainingQuery(
                projectId: "{project_id}",
                queryId: "{query_id}"
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateTrainingQuery()
        {
            CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
                url: "https://{cpd_cluster_host}{:port}",
                username: "{username}",
                password: "{password}"
                );

            DiscoveryService service = new DiscoveryService("2019-11-22", authenticator);
            service.SetServiceUrl("{https://{cpd_cluster_host}{:port}/discovery/{release}/instances/{instance_id}/api}");

            var newFilter = "field:1";
            TrainingExample newTrainingExample = new TrainingExample()
            {
                CollectionId = "{collection_id}",
                DocumentId = "{document_id}"
            };

            var result = service.UpdateTrainingQuery(
                projectId: "{project_id}",
                queryId: "{query_id}",
                naturalLanguageQuery: "This is a new example of a query",
                examples: new List<TrainingExample>() { newTrainingExample },
                filter: newFilter
                );
        }
        #endregion
    }
}
