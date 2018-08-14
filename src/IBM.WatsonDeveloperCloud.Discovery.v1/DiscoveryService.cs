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
using System.Runtime.Serialization;
using System.Text;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using Environment = IBM.WatsonDeveloperCloud.Discovery.v1.Model.Environment;

namespace IBM.WatsonDeveloperCloud.Discovery.v1
{
    public partial class DiscoveryService : WatsonService, IDiscoveryService
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
        

        public DiscoveryService(TokenOptions options, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;

            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                this.Endpoint = options.ServiceUrl;
            }
            else
            {
                options.ServiceUrl = this.Endpoint;
            }

            _tokenManager = new TokenManager(options);
        }

        public DiscoveryService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Create an environment.
        ///
        /// Creates a new environment for private data. An environment must be created before collections can be
        /// created.
        ///
        /// **Note**: You can create only one environment for private data per service instance. An attempt to create
        /// another environment results in an error.
        /// </summary>
        /// <param name="body">An object that defines an environment name and optional description. The fields in this
        /// object are not approved for personal information and cannot be deleted based on customer ID.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Environment" />Environment</returns>
        public Environment CreateEnvironment(CreateEnvironmentRequest body, Dictionary<string, object> customData = null)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Environment result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<CreateEnvironmentRequest>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Environment>().Result;
                if(result == null)
                    result = new Environment();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete environment.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DeleteEnvironmentResponse" />DeleteEnvironmentResponse</returns>
        public DeleteEnvironmentResponse DeleteEnvironment(string environmentId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DeleteEnvironmentResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DeleteEnvironmentResponse>().Result;
                if(result == null)
                    result = new DeleteEnvironmentResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get environment info.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Environment" />Environment</returns>
        public Environment GetEnvironment(string environmentId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Environment result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Environment>().Result;
                if(result == null)
                    result = new Environment();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List environments.
        ///
        /// List existing environments for the service instance.
        /// </summary>
        /// <param name="name">Show only the environment with the given name. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ListEnvironmentsResponse" />ListEnvironmentsResponse</returns>
        public ListEnvironmentsResponse ListEnvironments(string name = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ListEnvironmentsResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(name))
                    restRequest.WithArgument("name", name);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<ListEnvironmentsResponse>().Result;
                if(result == null)
                    result = new ListEnvironmentsResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List fields across collections.
        ///
        /// Gets a list of the unique fields (and their types) stored in the indexes of the specified collections.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ListCollectionFieldsResponse" />ListCollectionFieldsResponse</returns>
        public ListCollectionFieldsResponse ListFields(string environmentId, List<string> collectionIds, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/fields");

                restRequest.WithArgument("version", VersionDate);
                if (collectionIds != null && collectionIds.Count > 0)
                    restRequest.WithArgument("collection_ids", string.Join(",", collectionIds.ToArray()));
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<ListCollectionFieldsResponse>().Result;
                if(result == null)
                    result = new ListCollectionFieldsResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update an environment.
        ///
        /// Updates an environment. The environment's **name** and  **description** parameters can be changed. You must
        /// specify a **name** for the environment.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="body">An object that defines the environment's name and, optionally, description.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Environment" />Environment</returns>
        public Environment UpdateEnvironment(string environmentId, UpdateEnvironmentRequest body, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<UpdateEnvironmentRequest>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Environment>().Result;
                if(result == null)
                    result = new Environment();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Add configuration.
        ///
        /// Creates a new configuration.
        ///
        /// If the input configuration contains the **configuration_id**, **created**, or **updated** properties, then
        /// they are ignored and overridden by the system, and an error is not returned so that the overridden fields do
        /// not need to be removed when copying a configuration.
        ///
        /// The configuration can contain unrecognized JSON fields. Any such fields are ignored and do not generate an
        /// error. This makes it easier to use newer configuration files with older versions of the API and the service.
        /// It also makes it possible for the tooling to add additional metadata and information to the configuration.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configuration">Input an object that enables you to customize how your content is ingested and
        /// what enrichments are added to your data.
        ///
        /// **name** is required and must be unique within the current **environment**. All other properties are
        /// optional.
        ///
        /// If the input configuration contains the **configuration_id**, **created**, or **updated** properties, then
        /// they will be ignored and overridden by the system (an error is not returned so that the overridden fields do
        /// not need to be removed when copying a configuration).
        ///
        /// The configuration can contain unrecognized JSON fields. Any such fields will be ignored and will not
        /// generate an error. This makes it easier to use newer configuration files with older versions of the API and
        /// the service. It also makes it possible for the tooling to add additional metadata and information to the
        /// configuration.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        public Configuration CreateConfiguration(string environmentId, Configuration configuration, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<Configuration>(configuration);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Configuration>().Result;
                if(result == null)
                    result = new Configuration();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a configuration.
        ///
        /// The deletion is performed unconditionally. A configuration deletion request succeeds even if the
        /// configuration is referenced by a collection or document ingestion. However, documents that have already been
        /// submitted for processing continue to use the deleted configuration. Documents are always processed with a
        /// snapshot of the configuration as it existed at the time the document was submitted.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configurationId">The ID of the configuration.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DeleteConfigurationResponse" />DeleteConfigurationResponse</returns>
        public DeleteConfigurationResponse DeleteConfiguration(string environmentId, string configurationId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DeleteConfigurationResponse>().Result;
                if(result == null)
                    result = new DeleteConfigurationResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get configuration details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configurationId">The ID of the configuration.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        public Configuration GetConfiguration(string environmentId, string configurationId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Configuration>().Result;
                if(result == null)
                    result = new Configuration();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List configurations.
        ///
        /// Lists existing configurations for the service instance.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="name">Find configurations with the given name. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ListConfigurationsResponse" />ListConfigurationsResponse</returns>
        public ListConfigurationsResponse ListConfigurations(string environmentId, string name = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ListConfigurationsResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(name))
                    restRequest.WithArgument("name", name);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<ListConfigurationsResponse>().Result;
                if(result == null)
                    result = new ListConfigurationsResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a configuration.
        ///
        /// Replaces an existing configuration.
        ///   * Completely replaces the original configuration.
        ///   * The **configuration_id**, **updated**, and **created** fields are accepted in the request, but they are
        /// ignored, and an error is not generated. It is also acceptable for users to submit an updated configuration
        /// with none of the three properties.
        ///   * Documents are processed with a snapshot of the configuration as it was at the time the document was
        /// submitted to be ingested. This means that already submitted documents will not see any updates made to the
        /// configuration.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configurationId">The ID of the configuration.</param>
        /// <param name="configuration">Input an object that enables you to update and customize how your data is
        /// ingested and what enrichments are added to your data.
        /// The **name** parameter is required and must be unique within the current **environment**. All other
        /// properties are optional, but if they are omitted  the default values replace the current value of each
        /// omitted property.
        ///
        /// If the input configuration contains the **configuration_id**, **created**, or **updated** properties, they
        /// are ignored and overridden by the system, and an error is not returned so that the overridden fields do not
        /// need to be removed when updating a configuration.
        ///
        /// The configuration can contain unrecognized JSON fields. Any such fields are ignored and do not generate an
        /// error. This makes it easier to use newer configuration files with older versions of the API and the service.
        /// It also makes it possible for the tooling to add additional metadata and information to the
        /// configuration.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        public Configuration UpdateConfiguration(string environmentId, string configurationId, Configuration configuration, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<Configuration>(configuration);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Configuration>().Result;
                if(result == null)
                    result = new Configuration();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Test configuration.
        ///
        /// Runs a sample document through the default or your configuration and returns diagnostic information designed
        /// to help you understand how the document was processed. The document is not added to the index.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configuration">The configuration to use to process the document. If this part is provided, then
        /// the provided configuration is used to process the document. If the **configuration_id** is also provided
        /// (both are present at the same time), then request is rejected. The maximum supported configuration size is 1
        /// MB. Configuration parts larger than 1 MB are rejected.
        /// See the `GET /configurations/{configuration_id}` operation for an example configuration. (optional)</param>
        /// <param name="step">Specify to only run the input document through the given step instead of running the
        /// input document through the entire ingestion workflow. Valid values are `convert`, `enrich`, and `normalize`.
        /// (optional)</param>
        /// <param name="configurationId">The ID of the configuration to use to process the document. If the
        /// **configuration** form part is also provided (both are present at the same time), then the request will be
        /// rejected. (optional)</param>
        /// <param name="file">The content of the document to ingest. The maximum supported file size is 50 megabytes.
        /// Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document
        /// against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1
        /// MB. Metadata parts larger than 1 MB are rejected.
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TestDocument" />TestDocument</returns>
        public TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
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
                    configurationContent.Headers.ContentType = null;
                    formData.Add(configurationContent, "configuration");
                }

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", file.Name);
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    metadataContent.Headers.ContentType = null;
                    formData.Add(metadataContent, "metadata");
                }

                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/preview");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(step))
                    restRequest.WithArgument("step", step);
                if (!string.IsNullOrEmpty(configurationId))
                    restRequest.WithArgument("configuration_id", configurationId);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TestDocument>().Result;
                if(result == null)
                    result = new TestDocument();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create a collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="body">Input an object that allows you to add a collection.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public Collection CreateCollection(string environmentId, CreateCollectionRequest body, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<CreateCollectionRequest>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Collection>().Result;
                if(result == null)
                    result = new Collection();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DeleteCollectionResponse" />DeleteCollectionResponse</returns>
        public DeleteCollectionResponse DeleteCollection(string environmentId, string collectionId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DeleteCollectionResponse>().Result;
                if(result == null)
                    result = new DeleteCollectionResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get collection details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public Collection GetCollection(string environmentId, string collectionId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Collection>().Result;
                if(result == null)
                    result = new Collection();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List collection fields.
        ///
        /// Gets a list of the unique fields (and their types) stored in the index.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ListCollectionFieldsResponse" />ListCollectionFieldsResponse</returns>
        public ListCollectionFieldsResponse ListCollectionFields(string environmentId, string collectionId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/fields");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<ListCollectionFieldsResponse>().Result;
                if(result == null)
                    result = new ListCollectionFieldsResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List collections.
        ///
        /// Lists existing collections for the service instance.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="name">Find collections with the given name. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ListCollectionsResponse" />ListCollectionsResponse</returns>
        public ListCollectionsResponse ListCollections(string environmentId, string name = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ListCollectionsResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(name))
                    restRequest.WithArgument("name", name);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<ListCollectionsResponse>().Result;
                if(result == null)
                    result = new ListCollectionsResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="body">Input an object that allows you to update a collection. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public Collection UpdateCollection(string environmentId, string collectionId, UpdateCollectionRequest body = null, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<UpdateCollectionRequest>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Collection>().Result;
                if(result == null)
                    result = new Collection();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create or update expansion list.
        ///
        /// Create or replace the Expansion list for this collection. The maximum number of expanded terms per
        /// collection is `500`.
        /// The current expansion list is replaced with the uploaded content.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="body">An object that defines the expansion list.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Expansions" />Expansions</returns>
        public Expansions CreateExpansions(string environmentId, string collectionId, Expansions body, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<Expansions>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Expansions>().Result;
                if(result == null)
                    result = new Expansions();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete the expansion list.
        ///
        /// Remove the expansion information for this collection. The expansion list must be deleted to disable query
        /// expansion for a collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteExpansions(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BaseModel result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get the expansion list.
        ///
        /// Returns the current expansion list for the specified collection. If an expansion list is not specified, an
        /// object with empty expansion arrays is returned.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Expansions" />Expansions</returns>
        public Expansions ListExpansions(string environmentId, string collectionId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Expansions>().Result;
                if(result == null)
                    result = new Expansions();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Add a document.
        ///
        /// Add a document to a collection with optional metadata.
        ///
        ///   * The **version** query parameter is still required.
        ///
        ///   * Returns immediately after the system has accepted the document for processing.
        ///
        ///   * The user must provide document content, metadata, or both. If the request is missing both document
        /// content and metadata, it is rejected.
        ///
        ///   * The user can set the **Content-Type** parameter on the **file** part to indicate the media type of the
        /// document. If the **Content-Type** parameter is missing or is one of the generic media types (for example,
        /// `application/octet-stream`), then the service attempts to automatically detect the document's media type.
        ///
        ///   * The following field names are reserved and will be filtered out if present after normalization: `id`,
        /// `score`, `highlight`, and any field with the prefix of: `_`, `+`, or `-`
        ///
        ///   * Fields with empty name values after normalization are filtered out before indexing.
        ///
        ///   * Fields containing the following characters after normalization are filtered out before indexing: `#` and
        /// `,`.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="file">The content of the document to ingest. The maximum supported file size is 50 megabytes.
        /// Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document
        /// against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1
        /// MB. Metadata parts larger than 1 MB are rejected.
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DocumentAccepted AddDocument(string environmentId, string collectionId, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
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
                    formData.Add(fileContent, "file", file.Name);
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    metadataContent.Headers.ContentType = null;
                    formData.Add(metadataContent, "metadata");
                }

                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DocumentAccepted>().Result;
                if(result == null)
                    result = new DocumentAccepted();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a document.
        ///
        /// If the given document ID is invalid, or if the document is not found, then the a success response is
        /// returned (HTTP status code `200`) with the status set to 'deleted'.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DeleteDocumentResponse" />DeleteDocumentResponse</returns>
        public DeleteDocumentResponse DeleteDocument(string environmentId, string collectionId, string documentId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DeleteDocumentResponse>().Result;
                if(result == null)
                    result = new DeleteDocumentResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get document details.
        ///
        /// Fetch status details about a submitted document. **Note:** this operation does not return the document
        /// itself. Instead, it returns only the document's processing status and any notices (warnings or errors) that
        /// were generated when the document was ingested. Use the query API to retrieve the actual document content.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DocumentStatus" />DocumentStatus</returns>
        public DocumentStatus GetDocumentStatus(string environmentId, string collectionId, string documentId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DocumentStatus>().Result;
                if(result == null)
                    result = new DocumentStatus();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a document.
        ///
        /// Replace an existing document. Starts ingesting a document with optional metadata.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="file">The content of the document to ingest. The maximum supported file size is 50 megabytes.
        /// Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document
        /// against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1
        /// MB. Metadata parts larger than 1 MB are rejected.
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.FileStream file = null, string metadata = null, string fileContentType = null, Dictionary<string, object> customData = null)
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
                    formData.Add(fileContent, "file", file.Name);
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    metadataContent.Headers.ContentType = null;
                    formData.Add(metadataContent, "metadata");
                }

                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DocumentAccepted>().Result;
                if(result == null)
                    result = new DocumentAccepted();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Long environment queries.
        ///
        /// Complex queries might be too long for a standard method query. By using this method, you can construct
        /// longer queries. However, these queries may take longer to complete than the standard method. For details,
        /// see the [Discovery service documentation](https://console.bluemix.net/docs/services/discovery/using.html).
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="queryLong">An object that represents the query to be submitted. (optional)</param>
        /// <param name="loggingOptOut">If `true`, queries are not stored in the Discovery **Logs** endpoint. (optional,
        /// default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        public QueryResponse FederatedQuery(string environmentId, QueryLarge queryLong = null, bool? loggingOptOut = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            QueryResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/query");

                restRequest.WithArgument("version", VersionDate);
                if (loggingOptOut != null)
                    restRequest.WithHeader("X-Watson-Logging-Opt-Out", loggingOptOut.ToString());
                restRequest.WithBody<QueryLarge>(queryLong);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<QueryResponse>().Result;
                if(result == null)
                    result = new QueryResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Query multiple collection system notices.
        ///
        /// Queries for notices (errors or warnings) that might have been generated by the system. Notices are generated
        /// when ingesting documents and performing relevance training. See the [Discovery service
        /// documentation](https://console.bluemix.net/docs/services/discovery/using.html) for more details on the query
        /// language.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.</param>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. Use a query search when you want to find the most
        /// relevant search results. You cannot use **natural_language_query** and **query** at the same time.
        /// (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing
        /// training data and natural language understanding. You cannot use **natural_language_query** and **query** at
        /// the same time. (optional)</param>
        /// <param name="aggregation">An aggregation search that returns an exact answer by combining query search with
        /// filters. Useful for applications to build lists, tables, and time series. For a full list of possible
        /// aggregations, see the Query reference. (optional)</param>
        /// <param name="count">Number of results to return. (optional, default to 10)</param>
        /// <param name="returnFields">A comma-separated list of the portion of the document hierarchy to return.
        /// (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true, a highlight field is returned for each result which contains the fields
        /// which match the query with `<em></em>` tags around the matching query terms. (optional, default to
        /// false)</param>
        /// <param name="deduplicateField">When specified, duplicate results based on the field specified are removed
        /// from the returned results. Duplicate comparison is limited to the current query only, **offset** is not
        /// considered. This parameter is currently Beta functionality. (optional)</param>
        /// <param name="similar">When `true`, results are returned based on their similarity to the document IDs
        /// specified in the **similar.document_ids** parameter. (optional, default to false)</param>
        /// <param name="similarDocumentIds">A comma-separated list of document IDs to find similar documents.
        ///
        /// **Tip:** Include the **natural_language_query** parameter to expand the scope of the document similarity
        /// search with the natural language query. Other query parameters, such as **filter** and **query**, are
        /// subsequently applied and reduce the scope. (optional)</param>
        /// <param name="similarFields">A comma-separated list of field names that are used as a basis for comparison to
        /// identify similar documents. If not specified, the entire document is used for comparison. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        public QueryNoticesResponse FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/notices");

                restRequest.WithArgument("version", VersionDate);
                if (collectionIds != null && collectionIds.Count > 0)
                    restRequest.WithArgument("collection_ids", string.Join(",", collectionIds.ToArray()));
                if (!string.IsNullOrEmpty(filter))
                    restRequest.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(query))
                    restRequest.WithArgument("query", query);
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    restRequest.WithArgument("natural_language_query", naturalLanguageQuery);
                if (!string.IsNullOrEmpty(aggregation))
                    restRequest.WithArgument("aggregation", aggregation);
                if (count != null)
                    restRequest.WithArgument("count", count);
                if (returnFields != null && returnFields.Count > 0)
                    restRequest.WithArgument("return", string.Join(",", returnFields.ToArray()));
                if (offset != null)
                    restRequest.WithArgument("offset", offset);
                if (sort != null && sort.Count > 0)
                    restRequest.WithArgument("sort", string.Join(",", sort.ToArray()));
                if (highlight != null)
                    restRequest.WithArgument("highlight", highlight);
                if (!string.IsNullOrEmpty(deduplicateField))
                    restRequest.WithArgument("deduplicate.field", deduplicateField);
                if (similar != null)
                    restRequest.WithArgument("similar", similar);
                if (similarDocumentIds != null && similarDocumentIds.Count > 0)
                    restRequest.WithArgument("similar.document_ids", string.Join(",", similarDocumentIds.ToArray()));
                if (similarFields != null && similarFields.Count > 0)
                    restRequest.WithArgument("similar.fields", string.Join(",", similarFields.ToArray()));
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<QueryNoticesResponse>().Result;
                if(result == null)
                    result = new QueryNoticesResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Long collection queries.
        ///
        /// Complex queries might be too long for a standard method query. By using this method, you can construct
        /// longer queries. However, these queries may take longer to complete than the standard method. For details,
        /// see the [Discovery service documentation](https://console.bluemix.net/docs/services/discovery/using.html).
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryLong">An object that represents the query to be submitted. (optional)</param>
        /// <param name="loggingOptOut">If `true`, queries are not stored in the Discovery **Logs** endpoint. (optional,
        /// default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        public QueryResponse Query(string environmentId, string collectionId, QueryLarge queryLong = null, bool? loggingOptOut = null, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query");

                restRequest.WithArgument("version", VersionDate);
                if (loggingOptOut != null)
                    restRequest.WithHeader("X-Watson-Logging-Opt-Out", loggingOptOut.ToString());
                restRequest.WithBody<QueryLarge>(queryLong);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<QueryResponse>().Result;
                if(result == null)
                    result = new QueryResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Knowledge Graph entity query.
        ///
        /// See the [Knowledge Graph
        /// documentation](https://console.bluemix.net/docs/services/discovery/building-kg.html) for more details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="entityQuery">An object specifying the entities to query, which functions to perform, and any
        /// additional constraints.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="QueryEntitiesResponse" />QueryEntitiesResponse</returns>
        public QueryEntitiesResponse QueryEntities(string environmentId, string collectionId, QueryEntities entityQuery, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query_entities");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<QueryEntities>(entityQuery);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<QueryEntitiesResponse>().Result;
                if(result == null)
                    result = new QueryEntitiesResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Query system notices.
        ///
        /// Queries for notices (errors or warnings) that might have been generated by the system. Notices are generated
        /// when ingesting documents and performing relevance training. See the [Discovery service
        /// documentation](https://console.bluemix.net/docs/services/discovery/using.html) for more details on the query
        /// language.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. Use a query search when you want to find the most
        /// relevant search results. You cannot use **natural_language_query** and **query** at the same time.
        /// (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing
        /// training data and natural language understanding. You cannot use **natural_language_query** and **query** at
        /// the same time. (optional)</param>
        /// <param name="passages">A passages query that returns the most relevant passages from the results.
        /// (optional)</param>
        /// <param name="aggregation">An aggregation search that returns an exact answer by combining query search with
        /// filters. Useful for applications to build lists, tables, and time series. For a full list of possible
        /// aggregations, see the Query reference. (optional)</param>
        /// <param name="count">Number of results to return. (optional, default to 10)</param>
        /// <param name="returnFields">A comma-separated list of the portion of the document hierarchy to return.
        /// (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true, a highlight field is returned for each result which contains the fields
        /// which match the query with `<em></em>` tags around the matching query terms. (optional, default to
        /// false)</param>
        /// <param name="passagesFields">A comma-separated list of fields that passages are drawn from. If this
        /// parameter not specified, then all top-level fields are included. (optional)</param>
        /// <param name="passagesCount">The maximum number of passages to return. The search returns fewer passages if
        /// the requested total is not found. (optional, default to 10)</param>
        /// <param name="passagesCharacters">The approximate number of characters that any one passage will have.
        /// (optional, default to 400)</param>
        /// <param name="deduplicateField">When specified, duplicate results based on the field specified are removed
        /// from the returned results. Duplicate comparison is limited to the current query only, **offset** is not
        /// considered. This parameter is currently Beta functionality. (optional)</param>
        /// <param name="similar">When `true`, results are returned based on their similarity to the document IDs
        /// specified in the **similar.document_ids** parameter. (optional, default to false)</param>
        /// <param name="similarDocumentIds">A comma-separated list of document IDs to find similar documents.
        ///
        /// **Tip:** Include the **natural_language_query** parameter to expand the scope of the document similarity
        /// search with the natural language query. Other query parameters, such as **filter** and **query**, are
        /// subsequently applied and reduce the scope. (optional)</param>
        /// <param name="similarFields">A comma-separated list of field names that are used as a basis for comparison to
        /// identify similar documents. If not specified, the entire document is used for comparison. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        public QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/notices");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(filter))
                    restRequest.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(query))
                    restRequest.WithArgument("query", query);
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    restRequest.WithArgument("natural_language_query", naturalLanguageQuery);
                if (passages != null)
                    restRequest.WithArgument("passages", passages);
                if (!string.IsNullOrEmpty(aggregation))
                    restRequest.WithArgument("aggregation", aggregation);
                if (count != null)
                    restRequest.WithArgument("count", count);
                if (returnFields != null && returnFields.Count > 0)
                    restRequest.WithArgument("return", string.Join(",", returnFields.ToArray()));
                if (offset != null)
                    restRequest.WithArgument("offset", offset);
                if (sort != null && sort.Count > 0)
                    restRequest.WithArgument("sort", string.Join(",", sort.ToArray()));
                if (highlight != null)
                    restRequest.WithArgument("highlight", highlight);
                if (passagesFields != null && passagesFields.Count > 0)
                    restRequest.WithArgument("passages.fields", string.Join(",", passagesFields.ToArray()));
                if (passagesCount != null)
                    restRequest.WithArgument("passages.count", passagesCount);
                if (passagesCharacters != null)
                    restRequest.WithArgument("passages.characters", passagesCharacters);
                if (!string.IsNullOrEmpty(deduplicateField))
                    restRequest.WithArgument("deduplicate.field", deduplicateField);
                if (similar != null)
                    restRequest.WithArgument("similar", similar);
                if (similarDocumentIds != null && similarDocumentIds.Count > 0)
                    restRequest.WithArgument("similar.document_ids", string.Join(",", similarDocumentIds.ToArray()));
                if (similarFields != null && similarFields.Count > 0)
                    restRequest.WithArgument("similar.fields", string.Join(",", similarFields.ToArray()));
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<QueryNoticesResponse>().Result;
                if(result == null)
                    result = new QueryNoticesResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Knowledge Graph relationship query.
        ///
        /// See the [Knowledge Graph
        /// documentation](https://console.bluemix.net/docs/services/discovery/building-kg.html) for more details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="relationshipQuery">An object that describes the relationships to be queried and any query
        /// constraints (such as filters).</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="QueryRelationsResponse" />QueryRelationsResponse</returns>
        public QueryRelationsResponse QueryRelations(string environmentId, string collectionId, QueryRelations relationshipQuery, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query_relations");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<QueryRelations>(relationshipQuery);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<QueryRelationsResponse>().Result;
                if(result == null)
                    result = new QueryRelationsResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Add query to training data.
        ///
        /// Adds a query to the training data for this collection. The query can contain a filter and natural language
        /// query.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="body">The body of the training data query that is to be added to the collection's training
        /// data.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public TrainingQuery AddTrainingData(string environmentId, string collectionId, NewTrainingQuery body, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<NewTrainingQuery>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TrainingQuery>().Result;
                if(result == null)
                    result = new TrainingQuery();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Add example to training data query.
        ///
        /// Adds a example to this training data query.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="body">The body of the example that is to be added to the specified query.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        public TrainingExample CreateTrainingExample(string environmentId, string collectionId, string queryId, TrainingExample body, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<TrainingExample>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TrainingExample>().Result;
                if(result == null)
                    result = new TrainingExample();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete all training data.
        ///
        /// Deletes all training data from a collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteAllTrainingData(string environmentId, string collectionId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BaseModel result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a training data query.
        ///
        /// Removes the training data query and all associated examples from the training data set.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteTrainingData(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(collectionId))
                throw new ArgumentNullException(nameof(collectionId));
            if (string.IsNullOrEmpty(queryId))
                throw new ArgumentNullException(nameof(queryId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BaseModel result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete example for training data query.
        ///
        /// Deletes the example document with the given ID from the training data query.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="exampleId">The ID of the document as it is indexed.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, Dictionary<string, object> customData = null)
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

            BaseModel result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get details about a query.
        ///
        /// Gets details for a specific training data query, including the query string and all examples.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public TrainingQuery GetTrainingData(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TrainingQuery>().Result;
                if(result == null)
                    result = new TrainingQuery();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get details for training data example.
        ///
        /// Gets the details for this training example.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="exampleId">The ID of the document as it is indexed.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        public TrainingExample GetTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TrainingExample>().Result;
                if(result == null)
                    result = new TrainingExample();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List training data.
        ///
        /// Lists the training data for the specified collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TrainingDataSet" />TrainingDataSet</returns>
        public TrainingDataSet ListTrainingData(string environmentId, string collectionId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TrainingDataSet>().Result;
                if(result == null)
                    result = new TrainingDataSet();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List examples for a training data query.
        ///
        /// List all examples for this training data query.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TrainingExampleList" />TrainingExampleList</returns>
        public TrainingExampleList ListTrainingExamples(string environmentId, string collectionId, string queryId, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TrainingExampleList>().Result;
                if(result == null)
                    result = new TrainingExampleList();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Change label or cross reference for example.
        ///
        /// Changes the label or cross reference query for this training data example.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="exampleId">The ID of the document as it is indexed.</param>
        /// <param name="body">The body of the example that is to be added to the specified query.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        public TrainingExample UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, TrainingExamplePatch body, Dictionary<string, object> customData = null)
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
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<TrainingExamplePatch>(body);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TrainingExample>().Result;
                if(result == null)
                    result = new TrainingExample();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Delete labeled data.
        ///
        /// Deletes all data associated with a specified customer ID. The method has no effect if no data is associated
        /// with the customer ID.
        ///
        /// You associate a customer ID with data by passing the **X-Watson-Metadata** header with a request that passes
        /// data. For more information about personal data and customer IDs, see [Information
        /// security](https://console.bluemix.net/docs/services/discovery/information-security.html).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customerId))
                throw new ArgumentNullException(nameof(customerId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            BaseModel result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/user_data");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(customerId))
                    restRequest.WithArgument("customer_id", customerId);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create event.
        ///
        /// The **Events** API can be used to create log entries that are associated with specific queries. For example,
        /// you can record which documents in the results set were "clicked" by a user and when that click occured.
        /// </summary>
        /// <param name="queryEvent">An object that defines a query event to be added to the log.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="CreateEventResponse" />CreateEventResponse</returns>
        public CreateEventResponse CreateEvent(CreateEventObject queryEvent, Dictionary<string, object> customData = null)
        {
            if (queryEvent == null)
                throw new ArgumentNullException(nameof(queryEvent));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            CreateEventResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/events");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<CreateEventObject>(queryEvent);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<CreateEventResponse>().Result;
                if(result == null)
                    result = new CreateEventResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Percentage of queries with an associated event.
        ///
        /// The percentage of queries using the **natural_language_query** parameter that have a corresponding "click"
        /// event over a specified time window.  This metric requires having integrated event tracking in your
        /// application using the **Events** API.
        /// </summary>
        /// <param name="startTime">Metric is computed from data recorded after this timestamp; must be in
        /// `YYYY-MM-DDThh:mm:ssZ` format. (optional)</param>
        /// <param name="endTime">Metric is computed from data recorded before this timestamp; must be in
        /// `YYYY-MM-DDThh:mm:ssZ` format. (optional)</param>
        /// <param name="resultType">The type of result to consider when calculating the metric. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="MetricResponse" />MetricResponse</returns>
        public MetricResponse GetMetricsEventRate(DateTime startTime = null, DateTime endTime = null, string resultType = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            MetricResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/event_rate");

                restRequest.WithArgument("version", VersionDate);
                if (startTime != null)
                    restRequest.WithArgument("start_time", startTime);
                if (endTime != null)
                    restRequest.WithArgument("end_time", endTime);
                if (!string.IsNullOrEmpty(resultType))
                    restRequest.WithArgument("result_type", resultType);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<MetricResponse>().Result;
                if(result == null)
                    result = new MetricResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Number of queries over time.
        ///
        /// Total number of queries using the **natural_language_query** parameter over a specific time window.
        /// </summary>
        /// <param name="startTime">Metric is computed from data recorded after this timestamp; must be in
        /// `YYYY-MM-DDThh:mm:ssZ` format. (optional)</param>
        /// <param name="endTime">Metric is computed from data recorded before this timestamp; must be in
        /// `YYYY-MM-DDThh:mm:ssZ` format. (optional)</param>
        /// <param name="resultType">The type of result to consider when calculating the metric. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="MetricResponse" />MetricResponse</returns>
        public MetricResponse GetMetricsQuery(DateTime startTime = null, DateTime endTime = null, string resultType = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            MetricResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries");

                restRequest.WithArgument("version", VersionDate);
                if (startTime != null)
                    restRequest.WithArgument("start_time", startTime);
                if (endTime != null)
                    restRequest.WithArgument("end_time", endTime);
                if (!string.IsNullOrEmpty(resultType))
                    restRequest.WithArgument("result_type", resultType);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<MetricResponse>().Result;
                if(result == null)
                    result = new MetricResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Number of queries with an event over time.
        ///
        /// Total number of queries using the **natural_language_query** parameter that have a corresponding "click"
        /// event over a specified time window. This metric requires having integrated event tracking in your
        /// application using the **Events** API.
        /// </summary>
        /// <param name="startTime">Metric is computed from data recorded after this timestamp; must be in
        /// `YYYY-MM-DDThh:mm:ssZ` format. (optional)</param>
        /// <param name="endTime">Metric is computed from data recorded before this timestamp; must be in
        /// `YYYY-MM-DDThh:mm:ssZ` format. (optional)</param>
        /// <param name="resultType">The type of result to consider when calculating the metric. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="MetricResponse" />MetricResponse</returns>
        public MetricResponse GetMetricsQueryEvent(DateTime startTime = null, DateTime endTime = null, string resultType = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            MetricResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries_with_event");

                restRequest.WithArgument("version", VersionDate);
                if (startTime != null)
                    restRequest.WithArgument("start_time", startTime);
                if (endTime != null)
                    restRequest.WithArgument("end_time", endTime);
                if (!string.IsNullOrEmpty(resultType))
                    restRequest.WithArgument("result_type", resultType);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<MetricResponse>().Result;
                if(result == null)
                    result = new MetricResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Number of queries with no search results over time.
        ///
        /// Total number of queries using the **natural_language_query** parameter that have no results returned over a
        /// specified time window.
        /// </summary>
        /// <param name="startTime">Metric is computed from data recorded after this timestamp; must be in
        /// `YYYY-MM-DDThh:mm:ssZ` format. (optional)</param>
        /// <param name="endTime">Metric is computed from data recorded before this timestamp; must be in
        /// `YYYY-MM-DDThh:mm:ssZ` format. (optional)</param>
        /// <param name="resultType">The type of result to consider when calculating the metric. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="MetricResponse" />MetricResponse</returns>
        public MetricResponse GetMetricsQueryNoResults(DateTime startTime = null, DateTime endTime = null, string resultType = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            MetricResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries_with_no_search_results");

                restRequest.WithArgument("version", VersionDate);
                if (startTime != null)
                    restRequest.WithArgument("start_time", startTime);
                if (endTime != null)
                    restRequest.WithArgument("end_time", endTime);
                if (!string.IsNullOrEmpty(resultType))
                    restRequest.WithArgument("result_type", resultType);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<MetricResponse>().Result;
                if(result == null)
                    result = new MetricResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Most frequent query tokens with an event.
        ///
        /// The most frequent query tokens parsed from the **natural_language_query** parameter and their corresponding
        /// "click" event rate within the recording period (queries and events are stored for 30 days). A query token is
        /// an individual word or unigram within the query string.
        /// </summary>
        /// <param name="count">Number of results to return. (optional, default to 10)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="MetricTokenResponse" />MetricTokenResponse</returns>
        public MetricTokenResponse GetMetricsQueryTokenEvent(long? count = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            MetricTokenResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/top_query_tokens_with_event_rate");

                restRequest.WithArgument("version", VersionDate);
                if (count != null)
                    restRequest.WithArgument("count", count);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<MetricTokenResponse>().Result;
                if(result == null)
                    result = new MetricTokenResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Search the query and event log.
        ///
        /// Searches the query and event log to find query sessions that match the specified criteria. Searching the
        /// **logs** endpoint uses the standard Discovery query syntax for the parameters that are supported.
        /// </summary>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. Use a query search when you want to find the most
        /// relevant search results. You cannot use **natural_language_query** and **query** at the same time.
        /// (optional)</param>
        /// <param name="count">Number of results to return. (optional, default to 10)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="LogQueryResponse" />LogQueryResponse</returns>
        public LogQueryResponse QueryLog(string filter = null, string query = null, long? count = null, long? offset = null, List<string> sort = null, Dictionary<string, object> customData = null)
        {

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            LogQueryResponse result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/logs");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(filter))
                    restRequest.WithArgument("filter", filter);
                if (!string.IsNullOrEmpty(query))
                    restRequest.WithArgument("query", query);
                if (count != null)
                    restRequest.WithArgument("count", count);
                if (offset != null)
                    restRequest.WithArgument("offset", offset);
                if (sort != null && sort.Count > 0)
                    restRequest.WithArgument("sort", string.Join(",", sort.ToArray()));
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<LogQueryResponse>().Result;
                if(result == null)
                    result = new LogQueryResponse();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create credentials.
        ///
        /// Creates a set of credentials to connect to a remote source. Created credentials are used in a configuration
        /// to associate a collection with the remote source.
        ///
        /// **Note:** All credentials are sent over an encrypted connection and encrypted at rest.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="credentialsParameter">An object that defines an individual set of source credentials.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Credentials" />Credentials</returns>
        public Credentials CreateCredentials(string environmentId, Credentials credentialsParameter, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (credentialsParameter == null)
                throw new ArgumentNullException(nameof(credentialsParameter));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Credentials result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<Credentials>(credentialsParameter);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Credentials>().Result;
                if(result == null)
                    result = new Credentials();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete credentials.
        ///
        /// Deletes a set of stored credentials from your Discovery instance.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="credentialId">The unique identifier for a set of source credentials.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DeleteCredentials" />DeleteCredentials</returns>
        public DeleteCredentials DeleteCredentials(string environmentId, string credentialId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(credentialId))
                throw new ArgumentNullException(nameof(credentialId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DeleteCredentials result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DeleteCredentials>().Result;
                if(result == null)
                    result = new DeleteCredentials();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// View Credentials.
        ///
        /// Returns details about the specified credentials.
        ///
        ///  **Note:** Secure credential information such as a password or SSH key is never returned and must be
        /// obtained from the source system.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="credentialId">The unique identifier for a set of source credentials.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Credentials" />Credentials</returns>
        public Credentials GetCredentials(string environmentId, string credentialId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(credentialId))
                throw new ArgumentNullException(nameof(credentialId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Credentials result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Credentials>().Result;
                if(result == null)
                    result = new Credentials();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List credentials.
        ///
        /// List all the source credentials that have been created for this service instance.
        ///
        ///  **Note:**  All credentials are sent over an encrypted connection and encrypted at rest.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="CredentialsList" />CredentialsList</returns>
        public CredentialsList ListCredentials(string environmentId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            CredentialsList result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<CredentialsList>().Result;
                if(result == null)
                    result = new CredentialsList();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update credentials.
        ///
        /// Updates an existing set of source credentials.
        ///
        /// **Note:** All credentials are sent over an encrypted connection and encrypted at rest.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="credentialId">The unique identifier for a set of source credentials.</param>
        /// <param name="credentialsParameter">An object that defines an individual set of source credentials.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Credentials" />Credentials</returns>
        public Credentials UpdateCredentials(string environmentId, string credentialId, Credentials credentialsParameter, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(environmentId))
                throw new ArgumentNullException(nameof(environmentId));
            if (string.IsNullOrEmpty(credentialId))
                throw new ArgumentNullException(nameof(credentialId));
            if (credentialsParameter == null)
                throw new ArgumentNullException(nameof(credentialsParameter));

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            Credentials result = null;

            try
            {
                IClient client = null;
                if(_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                else
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<Credentials>(credentialsParameter);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Credentials>().Result;
                if(result == null)
                    result = new Credentials();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
