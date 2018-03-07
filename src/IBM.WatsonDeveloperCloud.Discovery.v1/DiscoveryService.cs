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
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
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
                throw new ArgumentNullException("versionDate cannot be null.");

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
                throw new ArgumentNullException("versionDate cannot be null.");

            Environment result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateEnvironmentRequest>(body);
                result = request.As<Environment>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            DeleteEnvironmentResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}");
                request.WithArgument("version", VersionDate);
                result = request.As<DeleteEnvironmentResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Environment result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}");
                request.WithArgument("version", VersionDate);
                result = request.As<Environment>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            ListEnvironmentsResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments");
                request.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(name))
                    request.WithArgument("name", name);
                result = request.As<ListEnvironmentsResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            ListCollectionFieldsResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/fields");
                request.WithArgument("version", VersionDate);
                request.WithArgument("collection_ids", collectionIds != null && collectionIds.Count > 0 ? string.Join(",", collectionIds.ToArray()) : null);
                result = request.As<ListCollectionFieldsResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Environment result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateEnvironmentRequest>(body);
                result = request.As<Environment>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Configuration result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations");
                request.WithArgument("version", VersionDate);
                request.WithBody<Configuration>(configuration);
                result = request.As<Configuration>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            DeleteConfigurationResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");
                request.WithArgument("version", VersionDate);
                result = request.As<DeleteConfigurationResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Configuration result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");
                request.WithArgument("version", VersionDate);
                result = request.As<Configuration>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            ListConfigurationsResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations");
                request.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(name))
                    request.WithArgument("name", name);
                result = request.As<ListConfigurationsResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Configuration result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");
                request.WithArgument("version", VersionDate);
                request.WithBody<Configuration>(configuration);
                result = request.As<Configuration>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            TestDocument result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (configuration != null)
                {
                    var configurationContent = new StringContent(configuration, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    configurationContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
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
                    metadataContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    formData.Add(metadataContent, "metadata");
                }

                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/preview");
                request.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(step))
                    request.WithArgument("step", step);
                if (!string.IsNullOrEmpty(configurationId))
                    request.WithArgument("configuration_id", configurationId);
                request.WithBodyContent(formData);
                result = request.As<TestDocument>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Collection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections");
                request.WithArgument("version", VersionDate);
                request.WithBody<CreateCollectionRequest>(body);
                result = request.As<Collection>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Expansions result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");
                request.WithArgument("version", VersionDate);
                request.WithBody<Expansions>(body);
                result = request.As<Expansions>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            DeleteCollectionResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");
                request.WithArgument("version", VersionDate);
                result = request.As<DeleteCollectionResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Collection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");
                request.WithArgument("version", VersionDate);
                result = request.As<Collection>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            ListCollectionFieldsResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/fields");
                request.WithArgument("version", VersionDate);
                result = request.As<ListCollectionFieldsResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            ListCollectionsResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections");
                request.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(name))
                    request.WithArgument("name", name);
                result = request.As<ListCollectionsResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Expansions result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");
                request.WithArgument("version", VersionDate);
                result = request.As<Expansions>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            Collection result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");
                request.WithArgument("version", VersionDate);
                request.WithBody<UpdateCollectionRequest>(body);
                result = request.As<Collection>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

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
                    metadataContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    formData.Add(metadataContent, "metadata");
                }

                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents");
                request.WithArgument("version", VersionDate);
                request.WithBodyContent(formData);
                result = request.As<DocumentAccepted>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            DeleteDocumentResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");
                request.WithArgument("version", VersionDate);
                result = request.As<DeleteDocumentResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            DocumentStatus result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");
                request.WithArgument("version", VersionDate);
                result = request.As<DocumentStatus>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

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
                    metadataContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    formData.Add(metadataContent, "metadata");
                }

                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");
                request.WithArgument("version", VersionDate);
                request.WithBodyContent(formData);
                result = request.As<DocumentAccepted>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public QueryResponse FederatedQuery(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (collectionIds == null)
                throw new ArgumentNullException(nameof(collectionIds));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            QueryResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/query");
                request.WithArgument("version", VersionDate);
                request.WithArgument("collection_ids", collectionIds != null && collectionIds.Count > 0 ? string.Join(",", collectionIds.ToArray()) : null);
                if (!string.IsNullOrEmpty(filter))
                    request.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(query))
                    request.WithArgument("query", query);
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    request.WithArgument("natural_language_query", naturalLanguageQuery);
                if (!string.IsNullOrEmpty(aggregation))
                    request.WithArgument("aggregation", aggregation);
                if (count != null)
                    request.WithArgument("count", count);
                request.WithArgument("return_fields", returnFields != null && returnFields.Count > 0 ? string.Join(",", returnFields.ToArray()) : null);
                if (offset != null)
                    request.WithArgument("offset", offset);
                request.WithArgument("sort", sort != null && sort.Count > 0 ? string.Join(",", sort.ToArray()) : null);
                if (highlight != null)
                    request.WithArgument("highlight", highlight);
                if (deduplicate != null)
                    request.WithArgument("deduplicate", deduplicate);
                if (!string.IsNullOrEmpty(deduplicateField))
                    request.WithArgument("deduplicate.field", deduplicateField);
                if (similar != null)
                    request.WithArgument("similar", similar);
                request.WithArgument("similar.document_ids", similarDocumentIds != null && similarDocumentIds.Count > 0 ? string.Join(",", similarDocumentIds.ToArray()) : null);
                request.WithArgument("similar.fields", similarFields != null && similarFields.Count > 0 ? string.Join(",", similarFields.ToArray()) : null);
                result = request.As<QueryResponse>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryNoticesResponse FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (collectionIds == null)
                throw new ArgumentNullException(nameof(collectionIds));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            QueryNoticesResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/notices");
                request.WithArgument("version", VersionDate);
                request.WithArgument("collection_ids", collectionIds != null && collectionIds.Count > 0 ? string.Join(",", collectionIds.ToArray()) : null);
                if (!string.IsNullOrEmpty(filter))
                    request.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(query))
                    request.WithArgument("query", query);
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    request.WithArgument("natural_language_query", naturalLanguageQuery);
                if (!string.IsNullOrEmpty(aggregation))
                    request.WithArgument("aggregation", aggregation);
                if (count != null)
                    request.WithArgument("count", count);
                request.WithArgument("return_fields", returnFields != null && returnFields.Count > 0 ? string.Join(",", returnFields.ToArray()) : null);
                if (offset != null)
                    request.WithArgument("offset", offset);
                request.WithArgument("sort", sort != null && sort.Count > 0 ? string.Join(",", sort.ToArray()) : null);
                if (highlight != null)
                    request.WithArgument("highlight", highlight);
                if (!string.IsNullOrEmpty(deduplicateField))
                    request.WithArgument("deduplicate.field", deduplicateField);
                if (similar != null)
                    request.WithArgument("similar", similar);
                request.WithArgument("similar.document_ids", similarDocumentIds != null && similarDocumentIds.Count > 0 ? string.Join(",", similarDocumentIds.ToArray()) : null);
                request.WithArgument("similar.fields", similarFields != null && similarFields.Count > 0 ? string.Join(",", similarFields.ToArray()) : null);
                result = request.As<QueryNoticesResponse>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryResponse Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            QueryResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query");
                request.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(filter))
                    request.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(query))
                    request.WithArgument("query", query);
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    request.WithArgument("natural_language_query", naturalLanguageQuery);
                if (passages != null)
                    request.WithArgument("passages", passages);
                if (!string.IsNullOrEmpty(aggregation))
                    request.WithArgument("aggregation", aggregation);
                if (count != null)
                    request.WithArgument("count", count);
                request.WithArgument("return", returnFields != null && returnFields.Count > 0 ? string.Join(",", returnFields.ToArray()) : null);
                if (offset != null)
                    request.WithArgument("offset", offset);
                request.WithArgument("sort", sort != null && sort.Count > 0 ? string.Join(",", sort.ToArray()) : null);
                if (highlight != null)
                    request.WithArgument("highlight", highlight);
                request.WithArgument("passages.fields", passagesFields != null && passagesFields.Count > 0 ? string.Join(",", passagesFields.ToArray()) : null);
                if (passagesCount != null)
                    request.WithArgument("passages.count", passagesCount);
                if (passagesCharacters != null)
                    request.WithArgument("passages.characters", passagesCharacters);
                if (deduplicate != null)
                    request.WithArgument("deduplicate", deduplicate);
                if (!string.IsNullOrEmpty(deduplicateField))
                    request.WithArgument("deduplicate.field", deduplicateField);
                if (similar != null)
                    request.WithArgument("similar", similar);
                request.WithArgument("similar.document_ids", similarDocumentIds != null && similarDocumentIds.Count > 0 ? string.Join(",", similarDocumentIds.ToArray()) : null);
                request.WithArgument("similar.fields", similarFields != null && similarFields.Count > 0 ? string.Join(",", similarFields.ToArray()) : null);
                result = request.As<QueryResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            QueryEntitiesResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query_entities");
                request.WithArgument("version", VersionDate);
                request.WithBody<QueryEntities>(entityQuery);
                result = request.As<QueryEntitiesResponse>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            QueryNoticesResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/notices");
                request.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(filter))
                    request.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(query))
                    request.WithArgument("query", query);
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    request.WithArgument("natural_language_query", naturalLanguageQuery);
                if (passages != null)
                    request.WithArgument("passages", passages);
                if (!string.IsNullOrEmpty(aggregation))
                    request.WithArgument("aggregation", aggregation);
                if (count != null)
                    request.WithArgument("count", count);
                request.WithArgument("return_fields", returnFields != null && returnFields.Count > 0 ? string.Join(",", returnFields.ToArray()) : null);
                if (offset != null)
                    request.WithArgument("offset", offset);
                request.WithArgument("sort", sort != null && sort.Count > 0 ? string.Join(",", sort.ToArray()) : null);
                if (highlight != null)
                    request.WithArgument("highlight", highlight);
                request.WithArgument("passages.fields", passagesFields != null && passagesFields.Count > 0 ? string.Join(",", passagesFields.ToArray()) : null);
                if (passagesCount != null)
                    request.WithArgument("passages.count", passagesCount);
                if (passagesCharacters != null)
                    request.WithArgument("passages.characters", passagesCharacters);
                if (!string.IsNullOrEmpty(deduplicateField))
                    request.WithArgument("deduplicate.field", deduplicateField);
                if (similar != null)
                    request.WithArgument("similar", similar);
                request.WithArgument("similar.document_ids", similarDocumentIds != null && similarDocumentIds.Count > 0 ? string.Join(",", similarDocumentIds.ToArray()) : null);
                request.WithArgument("similar.fields", similarFields != null && similarFields.Count > 0 ? string.Join(",", similarFields.ToArray()) : null);
                result = request.As<QueryNoticesResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            QueryRelationsResponse result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query_relations");
                request.WithArgument("version", VersionDate);
                request.WithBody<QueryRelations>(relationshipQuery);
                result = request.As<QueryRelationsResponse>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            TrainingQuery result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");
                request.WithArgument("version", VersionDate);
                request.WithBody<NewTrainingQuery>(body);
                result = request.As<TrainingQuery>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            TrainingExample result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples");
                request.WithArgument("version", VersionDate);
                request.WithBody<TrainingExample>(body);
                result = request.As<TrainingExample>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");
                request.WithArgument("version", VersionDate);
                result = request.As<object>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            TrainingQuery result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}");
                request.WithArgument("version", VersionDate);
                result = request.As<TrainingQuery>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            TrainingExample result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");
                request.WithArgument("version", VersionDate);
                result = request.As<TrainingExample>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            TrainingDataSet result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");
                request.WithArgument("version", VersionDate);
                result = request.As<TrainingDataSet>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            TrainingExampleList result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples");
                request.WithArgument("version", VersionDate);
                result = request.As<TrainingExampleList>().Result;
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
                throw new ArgumentNullException("versionDate cannot be null.");

            TrainingExample result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");
                request.WithArgument("version", VersionDate);
                request.WithBody<TrainingExamplePatch>(body);
                result = request.As<TrainingExample>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
