/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using System.Collections.Generic;
using System.Text;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using Newtonsoft.Json;
using System;
using Environment = IBM.WatsonDeveloperCloud.Discovery.v1.Model.Environment;

namespace IBM.WatsonDeveloperCloud.Discovery.v1
{
    public class DiscoveryService : WatsonService, IDiscoveryService
    {
        const string SERVICE_NAME = "discovery";
        const string URL = "https://gateway.watsonplatform.net/discovery/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        /** The Constant DISCOVERY_VERSION_DATE_2017_11_07. */
        public static string DISCOVERY_VERSION_DATE_2017_11_07 = "2017-11-07";

        public DiscoveryService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public DiscoveryService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            VersionDate = versionDate;
        }

        public DiscoveryService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public Environment CreateEnvironment(CreateEnvironmentRequest body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Environment result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateEnvironmentRequest>(body)
                                .As<Environment>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DeleteEnvironmentResponse DeleteEnvironment(string environmentId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            DeleteEnvironmentResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}")
                                .WithArgument("version", VersionDate)
                                .As<DeleteEnvironmentResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Environment GetEnvironment(string environmentId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Environment result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}")
                                .WithArgument("version", VersionDate)
                                .As<Environment>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ListEnvironmentsResponse ListEnvironments(string name = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            ListEnvironmentsResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments")
                                .WithArgument("version", VersionDate)
                                .WithArgument("name", name)
                                .As<ListEnvironmentsResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ListCollectionFieldsResponse ListFields(string environmentId, List<string> collectionIds)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (collectionIds == null)
                throw new ArgumentNullException(nameof(collectionIds));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            ListCollectionFieldsResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/fields")
                                .WithArgument("version", VersionDate)
                                .WithArgument("collection_ids", collectionIds != null && collectionIds.Count > 0 ? string.Join(",", collectionIds.ToArray()) : null)
                                .As<ListCollectionFieldsResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Environment UpdateEnvironment(string environmentId, UpdateEnvironmentRequest body)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Environment result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateEnvironmentRequest>(body)
                                .As<Environment>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public Configuration CreateConfiguration(string environmentId, Configuration configuration)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Configuration result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations")
                                .WithArgument("version", VersionDate)
                                .WithBody<Configuration>(configuration)
                                .As<Configuration>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DeleteConfigurationResponse DeleteConfiguration(string environmentId, string configurationId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(configurationId))
                throw new ArgumentNullException(nameof(configurationId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            DeleteConfigurationResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}")
                                .WithArgument("version", VersionDate)
                                .As<DeleteConfigurationResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Configuration GetConfiguration(string environmentId, string configurationId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(configurationId))
                throw new ArgumentNullException(nameof(configurationId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Configuration result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}")
                                .WithArgument("version", VersionDate)
                                .As<Configuration>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ListConfigurationsResponse ListConfigurations(string environmentId, string name = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            ListConfigurationsResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations")
                                .WithArgument("version", VersionDate)
                                .WithArgument("name", name)
                                .As<ListConfigurationsResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Configuration UpdateConfiguration(string environmentId, string configurationId, Configuration configuration)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(configurationId))
                throw new ArgumentNullException(nameof(configurationId));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Configuration result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}")
                                .WithArgument("version", VersionDate)
                                .WithBody<Configuration>(configuration)
                                .As<Configuration>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.Stream file = null, string metadata = null, string fileContentType = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            TestDocument result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (configuration != null)
                {
                    var configurationContent = new StringContent(configuration, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(configurationContent, "configuration");
                }

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", "filename");
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(metadataContent, "metadata");
                }

                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/preview")
                                .WithArgument("version", VersionDate)
                                .WithArgument("step", step)
                                .WithArgument("configuration_id", configurationId)
                                .WithBodyContent(formData)
                                .As<TestDocument>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public Collection CreateCollection(string environmentId, CreateCollectionRequest body)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Collection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateCollectionRequest>(body)
                                .As<Collection>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Expansions CreateExpansions(string environmentId, string collectionId, Expansions body)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Expansions result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions")
                                .WithArgument("version", VersionDate)
                                .WithBody<Expansions>(body)
                                .As<Expansions>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DeleteCollectionResponse DeleteCollection(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            DeleteCollectionResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}")
                                .WithArgument("version", VersionDate)
                                .As<DeleteCollectionResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteExpansions(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Collection GetCollection(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Collection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}")
                                .WithArgument("version", VersionDate)
                                .As<Collection>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ListCollectionFieldsResponse ListCollectionFields(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            ListCollectionFieldsResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/fields")
                                .WithArgument("version", VersionDate)
                                .As<ListCollectionFieldsResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ListCollectionsResponse ListCollections(string environmentId, string name = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            ListCollectionsResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections")
                                .WithArgument("version", VersionDate)
                                .WithArgument("name", name)
                                .As<ListCollectionsResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Expansions ListExpansions(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Expansions result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions")
                                .WithArgument("version", VersionDate)
                                .As<Expansions>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Collection UpdateCollection(string environmentId, string collectionId, UpdateCollectionRequest body = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            Collection result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateCollectionRequest>(body)
                                .As<Collection>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public DocumentAccepted AddDocument(string environmentId, string collectionId, System.IO.Stream file = null, string metadata = null, string fileContentType = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            DocumentAccepted result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", "filename");
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(metadataContent, "metadata");
                }

                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents")
                                .WithArgument("version", VersionDate)
                                .WithBodyContent(formData)
                                .As<DocumentAccepted>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DeleteDocumentResponse DeleteDocument(string environmentId, string collectionId, string documentId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(documentId))
                throw new ArgumentNullException(nameof(documentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            DeleteDocumentResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}")
                                .WithArgument("version", VersionDate)
                                .As<DeleteDocumentResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DocumentStatus GetDocumentStatus(string environmentId, string collectionId, string documentId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(documentId))
                throw new ArgumentNullException(nameof(documentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            DocumentStatus result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}")
                                .WithArgument("version", VersionDate)
                                .As<DocumentStatus>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.Stream file = null, string metadata = null, string fileContentType = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(documentId))
                throw new ArgumentNullException(nameof(documentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            DocumentAccepted result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", "filename");
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(metadataContent, "metadata");
                }

                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}")
                                .WithArgument("version", VersionDate)
                                .WithBodyContent(formData)
                                .As<DocumentAccepted>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public QueryResponse FederatedQuery(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, bool? deduplicate = null, string deduplicateField = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (collectionIds == null)
                throw new ArgumentNullException(nameof(collectionIds));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            QueryResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/query")
                                .WithArgument("version", VersionDate)
                                .WithArgument("collection_ids", collectionIds != null && collectionIds.Count > 0 ? string.Join(",", collectionIds.ToArray()) : null)
                                .WithArgument("filter", filter)
                                .WithArgument("query", query)
                                .WithArgument("natural_language_query", naturalLanguageQuery)
                                .WithArgument("aggregation", aggregation)
                                .WithArgument("count", count)
                                .WithArgument("return_fields", returnFields != null && returnFields.Count > 0 ? string.Join(",", returnFields.ToArray()) : null)
                                .WithArgument("offset", offset)
                                .WithArgument("sort", sort != null && sort.Count > 0 ? string.Join(",", sort.ToArray()) : null)
                                .WithArgument("highlight", highlight)
                                .WithArgument("deduplicate", deduplicate)
                                .WithArgument("deduplicate.field", deduplicateField)
                                .As<QueryResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryNoticesResponse FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (collectionIds == null)
                throw new ArgumentNullException(nameof(collectionIds));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            QueryNoticesResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/notices")
                                .WithArgument("version", VersionDate)
                                .WithArgument("collection_ids", collectionIds != null && collectionIds.Count > 0 ? string.Join(",", collectionIds.ToArray()) : null)
                                .WithArgument("filter", filter)
                                .WithArgument("query", query)
                                .WithArgument("natural_language_query", naturalLanguageQuery)
                                .WithArgument("aggregation", aggregation)
                                .WithArgument("count", count)
                                .WithArgument("return_fields", returnFields != null && returnFields.Count > 0 ? string.Join(",", returnFields.ToArray()) : null)
                                .WithArgument("offset", offset)
                                .WithArgument("sort", sort != null && sort.Count > 0 ? string.Join(",", sort.ToArray()) : null)
                                .WithArgument("highlight", highlight)
                                .WithArgument("deduplicate.field", deduplicateField)
                                .As<QueryNoticesResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryResponse Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            QueryResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query")
                                .WithArgument("version", VersionDate)
                                .WithArgument("filter", filter)
                                .WithArgument("query", query)
                                .WithArgument("natural_language_query", naturalLanguageQuery)
                                .WithArgument("passages", passages)
                                .WithArgument("aggregation", aggregation)
                                .WithArgument("count", count)
                                .WithArgument("return", returnFields != null && returnFields.Count > 0 ? string.Join(",", returnFields.ToArray()) : null)
                                .WithArgument("offset", offset)
                                .WithArgument("sort", sort != null && sort.Count > 0 ? string.Join(",", sort.ToArray()) : null)
                                .WithArgument("highlight", highlight)
                                .WithArgument("passages.fields", passagesFields != null && passagesFields.Count > 0 ? string.Join(",", passagesFields.ToArray()) : null)
                                .WithArgument("passages.count", passagesCount)
                                .WithArgument("passages.characters", passagesCharacters)
                                .WithArgument("deduplicate", deduplicate)
                                .WithArgument("deduplicate.field", deduplicateField)
                                .As<QueryResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryEntitiesResponse QueryEntities(string environmentId, string collectionId, QueryEntities entityQuery)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (entityQuery == null)
                throw new ArgumentNullException(nameof(entityQuery));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            QueryEntitiesResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query_entities")
                                .WithArgument("version", VersionDate)
                                .WithBody<QueryEntities>(entityQuery)
                                .As<QueryEntitiesResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            QueryNoticesResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/notices")
                                .WithArgument("version", VersionDate)
                                .WithArgument("filter", filter)
                                .WithArgument("query", query)
                                .WithArgument("natural_language_query", naturalLanguageQuery)
                                .WithArgument("passages", passages)
                                .WithArgument("aggregation", aggregation)
                                .WithArgument("count", count)
                                .WithArgument("return_fields", returnFields != null && returnFields.Count > 0 ? string.Join(",", returnFields.ToArray()) : null)
                                .WithArgument("offset", offset)
                                .WithArgument("sort", sort != null && sort.Count > 0 ? string.Join(",", sort.ToArray()) : null)
                                .WithArgument("highlight", highlight)
                                .WithArgument("passages.fields", passagesFields != null && passagesFields.Count > 0 ? string.Join(",", passagesFields.ToArray()) : null)
                                .WithArgument("passages.count", passagesCount)
                                .WithArgument("passages.characters", passagesCharacters)
                                .WithArgument("deduplicate.field", deduplicateField)
                                .As<QueryNoticesResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryRelationsResponse QueryRelations(string environmentId, string collectionId, QueryRelations relationshipQuery)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (relationshipQuery == null)
                throw new ArgumentNullException(nameof(relationshipQuery));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            QueryRelationsResponse result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query_relations")
                                .WithArgument("version", VersionDate)
                                .WithBody<QueryRelations>(relationshipQuery)
                                .As<QueryRelationsResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public TrainingQuery AddTrainingData(string environmentId, string collectionId, NewTrainingQuery body)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            TrainingQuery result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data")
                                .WithArgument("version", VersionDate)
                                .WithBody<NewTrainingQuery>(body)
                                .As<TrainingQuery>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public TrainingExample CreateTrainingExample(string environmentId, string collectionId, string queryId, TrainingExample body)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(queryId))
                throw new ArgumentNullException(nameof(queryId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            TrainingExample result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples")
                                .WithArgument("version", VersionDate)
                                .WithBody<TrainingExample>(body)
                                .As<TrainingExample>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteAllTrainingData(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteTrainingData(string environmentId, string collectionId, string queryId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(queryId))
                throw new ArgumentNullException(nameof(queryId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteTrainingExample(string environmentId, string collectionId, string queryId, string exampleId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(queryId))
                throw new ArgumentNullException(nameof(queryId));
            if (string.IsNullOrEmpty(exampleId))
                throw new ArgumentNullException(nameof(exampleId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}")
                                .WithArgument("version", VersionDate)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public TrainingQuery GetTrainingData(string environmentId, string collectionId, string queryId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(queryId))
                throw new ArgumentNullException(nameof(queryId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            TrainingQuery result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}")
                                .WithArgument("version", VersionDate)
                                .As<TrainingQuery>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public TrainingExample GetTrainingExample(string environmentId, string collectionId, string queryId, string exampleId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(queryId))
                throw new ArgumentNullException(nameof(queryId));
            if (string.IsNullOrEmpty(exampleId))
                throw new ArgumentNullException(nameof(exampleId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            TrainingExample result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}")
                                .WithArgument("version", VersionDate)
                                .As<TrainingExample>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public TrainingDataSet ListTrainingData(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            TrainingDataSet result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data")
                                .WithArgument("version", VersionDate)
                                .As<TrainingDataSet>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public TrainingExampleList ListTrainingExamples(string environmentId, string collectionId, string queryId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(queryId))
                throw new ArgumentNullException(nameof(queryId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            TrainingExampleList result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples")
                                .WithArgument("version", VersionDate)
                                .As<TrainingExampleList>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public TrainingExample UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, TrainingExamplePatch body)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(queryId))
                throw new ArgumentNullException(nameof(queryId));
            if (string.IsNullOrEmpty(exampleId))
                throw new ArgumentNullException(nameof(exampleId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2017_11_07'");

            TrainingExample result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}")
                                .WithArgument("version", VersionDate)
                                .WithBody<TrainingExamplePatch>(body)
                                .As<TrainingExample>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
