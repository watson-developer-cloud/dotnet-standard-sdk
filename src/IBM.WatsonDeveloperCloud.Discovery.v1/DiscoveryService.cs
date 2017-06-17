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

using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using Newtonsoft.Json;
using System;

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

        /** The Constant DISCOVERY_VERSION_DATE_2016_12_01. */
        public static string DISCOVERY_VERSION_DATE_2016_12_01 = "2016-12-01";

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

            VersionDate = versionDate;
        }

        public DiscoveryService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public Collection CreateCollection(string environmentId, CreateCollectionRequest body)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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

        public DeleteCollectionResponse DeleteCollection(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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

        public Collection GetCollection(string environmentId, string collectionId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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

        public Collection UpdateCollection(string environmentId, string collectionId, UpdateCollectionRequest body = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
        public Configuration CreateConfiguration(string environmentId, Configuration configuration)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
        public DocumentAccepted AddDocument(string environmentId, string collectionId, string configurationId = null, System.IO.Stream file = null, string metadata = null, string configuration = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

            DocumentAccepted result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    formData.Add(fileContent, "file");
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(metadataContent, "metadata");
                }

                if (configuration != null)
                {
                    var configurationContent = new StringContent(configuration, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(configurationContent, "configuration");
                }

                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents")
                                .WithArgument("version", VersionDate)
                                .WithArgument("configuration_id", configurationId)
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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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

        public DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, string configurationId = null, System.IO.Stream file = null, string metadata = null, string configuration = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(documentId))
                throw new ArgumentNullException(nameof(documentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

            DocumentAccepted result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    formData.Add(fileContent, "file");
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(metadataContent, "metadata");
                }

                if (configuration != null)
                {
                    var configurationContent = new StringContent(configuration, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    formData.Add(configurationContent, "configuration");
                }

                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}")
                                .WithArgument("version", VersionDate)
                                .WithArgument("configuration_id", configurationId)
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
        public ModelEnvironment CreateEnvironment(CreateEnvironmentRequest body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

            ModelEnvironment result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments")
                                .WithArgument("version", VersionDate)
                                .WithBody<CreateEnvironmentRequest>(body)
                                .As<ModelEnvironment>()
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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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

        public ModelEnvironment GetEnvironment(string environmentId)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

            ModelEnvironment result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}")
                                .WithArgument("version", VersionDate)
                                .As<ModelEnvironment>()
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
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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

        public ModelEnvironment UpdateEnvironment(string environmentId, UpdateEnvironmentRequest body)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

            ModelEnvironment result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}")
                                .WithArgument("version", VersionDate)
                                .WithBody<UpdateEnvironmentRequest>(body)
                                .As<ModelEnvironment>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public QueryResponse Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, string sort = null, bool? highlight = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                                .WithArgument("return", _return)
                                .WithArgument("offset", offset)
                                .WithArgument("sort", sort)
                                .WithArgument("highlight", highlight)
                                .As<QueryResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, string sort = null, bool? highlight = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                                .WithArgument("return", _return)
                                .WithArgument("offset", offset)
                                .WithArgument("sort", sort)
                                .WithArgument("highlight", highlight)
                                .As<QueryNoticesResponse>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.Stream file = null, string metadata = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null. Use 'DISCOVERY_VERSION_DATE_2016_12_01'");

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
                    formData.Add(fileContent, "file");
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
    }
}
