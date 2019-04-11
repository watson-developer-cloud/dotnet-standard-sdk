/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.Discovery.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Environment = IBM.Watson.Discovery.v1.Model.Environment;

namespace IBM.Watson.Discovery.v1
{
    public partial class DiscoveryService : IBMService, IDiscoveryService
    {
        new const string SERVICE_NAME = "discovery";
        const string URL = "https://gateway.watsonplatform.net/discovery/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public DiscoveryService() : base(SERVICE_NAME) { }
        
        public DiscoveryService(string userName, string password, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if (string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }
        
        public DiscoveryService(TokenOptions options, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if (string.IsNullOrEmpty(versionDate))
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

        public DiscoveryService(IClient httpClient) : base(SERVICE_NAME, URL)
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
        /// <returns><see cref="Environment" />Environment</returns>
        public DetailedResponse<Environment> CreateEnvironment(string name, string description = null, string size = null)
        {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("`name` is required for `CreateEnvironment`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Environment> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (!string.IsNullOrEmpty(size))
                    bodyObject["size"] = size;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateEnvironment"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Environment>().Result;
                if (result == null)
                    result = new DetailedResponse<Environment>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete environment.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <returns><see cref="DeleteEnvironmentResponse" />DeleteEnvironmentResponse</returns>
        public DetailedResponse<DeleteEnvironmentResponse> DeleteEnvironment(string environmentId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteEnvironment`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DeleteEnvironmentResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteEnvironment"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DeleteEnvironmentResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<DeleteEnvironmentResponse>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get environment info.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <returns><see cref="Environment" />Environment</returns>
        public DetailedResponse<Environment> GetEnvironment(string environmentId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetEnvironment`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Environment> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetEnvironment"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Environment>().Result;
                if (result == null)
                    result = new DetailedResponse<Environment>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="ListEnvironmentsResponse" />ListEnvironmentsResponse</returns>
        public DetailedResponse<ListEnvironmentsResponse> ListEnvironments(string name = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<ListEnvironmentsResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(name))
                    restRequest.WithArgument("name", name);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListEnvironments"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ListEnvironmentsResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<ListEnvironmentsResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="ListCollectionFieldsResponse" />ListCollectionFieldsResponse</returns>
        public DetailedResponse<ListCollectionFieldsResponse> ListFields(string environmentId, List<string> collectionIds)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListFields`");
        if (collectionIds == null)
            throw new ArgumentNullException("`collectionIds` is required for `ListFields`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<ListCollectionFieldsResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/fields");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (collectionIds != null && collectionIds.Count > 0)
                    restRequest.WithArgument("collection_ids", string.Join(",", collectionIds.ToArray()));

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListFields"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ListCollectionFieldsResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<ListCollectionFieldsResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Environment" />Environment</returns>
        public DetailedResponse<Environment> UpdateEnvironment(string environmentId, string name = null, string description = null, string size = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `UpdateEnvironment`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Environment> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (!string.IsNullOrEmpty(size))
                    bodyObject["size"] = size;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "UpdateEnvironment"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Environment>().Result;
                if (result == null)
                    result = new DetailedResponse<Environment>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Configuration" />Configuration</returns>
        public DetailedResponse<Configuration> CreateConfiguration(string environmentId, string name, string description = null, Conversions conversions = null, List<Enrichment> enrichments = null, List<NormalizationOperation> normalizations = null, Source source = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `CreateConfiguration`");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("`name` is required for `CreateConfiguration`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Configuration> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (conversions != null)
                    bodyObject["conversions"] = JToken.FromObject(conversions);
                if (enrichments != null && enrichments.Count > 0)
                    bodyObject["enrichments"] = JToken.FromObject(enrichments);
                if (normalizations != null && normalizations.Count > 0)
                    bodyObject["normalizations"] = JToken.FromObject(normalizations);
                if (source != null)
                    bodyObject["source"] = JToken.FromObject(source);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateConfiguration"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Configuration>().Result;
                if (result == null)
                    result = new DetailedResponse<Configuration>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="DeleteConfigurationResponse" />DeleteConfigurationResponse</returns>
        public DetailedResponse<DeleteConfigurationResponse> DeleteConfiguration(string environmentId, string configurationId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteConfiguration`");
        if (string.IsNullOrEmpty(configurationId))
            throw new ArgumentNullException("`configurationId` is required for `DeleteConfiguration`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DeleteConfigurationResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteConfiguration"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DeleteConfigurationResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<DeleteConfigurationResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Configuration" />Configuration</returns>
        public DetailedResponse<Configuration> GetConfiguration(string environmentId, string configurationId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetConfiguration`");
        if (string.IsNullOrEmpty(configurationId))
            throw new ArgumentNullException("`configurationId` is required for `GetConfiguration`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Configuration> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetConfiguration"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Configuration>().Result;
                if (result == null)
                    result = new DetailedResponse<Configuration>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="ListConfigurationsResponse" />ListConfigurationsResponse</returns>
        public DetailedResponse<ListConfigurationsResponse> ListConfigurations(string environmentId, string name = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListConfigurations`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<ListConfigurationsResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(name))
                    restRequest.WithArgument("name", name);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListConfigurations"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ListConfigurationsResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<ListConfigurationsResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Configuration" />Configuration</returns>
        public DetailedResponse<Configuration> UpdateConfiguration(string environmentId, string configurationId, string name, string description = null, Conversions conversions = null, List<Enrichment> enrichments = null, List<NormalizationOperation> normalizations = null, Source source = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `UpdateConfiguration`");
        if (string.IsNullOrEmpty(configurationId))
            throw new ArgumentNullException("`configurationId` is required for `UpdateConfiguration`");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("`name` is required for `UpdateConfiguration`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Configuration> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (conversions != null)
                    bodyObject["conversions"] = JToken.FromObject(conversions);
                if (enrichments != null && enrichments.Count > 0)
                    bodyObject["enrichments"] = JToken.FromObject(enrichments);
                if (normalizations != null && normalizations.Count > 0)
                    bodyObject["normalizations"] = JToken.FromObject(normalizations);
                if (source != null)
                    bodyObject["source"] = JToken.FromObject(source);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "UpdateConfiguration"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Configuration>().Result;
                if (result == null)
                    result = new DetailedResponse<Configuration>();
            }
            catch (AggregateException ae)
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
        /// <param name="file">The content of the document to ingest. The maximum supported file size when adding a file
        /// to a collection is 50 megabytes, the maximum supported file size when testing a confiruration is 1 megabyte.
        /// Files larger than the supported size are rejected. (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document
        /// against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1
        /// MB. Metadata parts larger than 1 MB are rejected.
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
        /// <param name="step">Specify to only run the input document through the given step instead of running the
        /// input document through the entire ingestion workflow. Valid values are `convert`, `enrich`, and `normalize`.
        /// (optional)</param>
        /// <param name="configurationId">The ID of the configuration to use to process the document. If the
        /// **configuration** form part is also provided (both are present at the same time), then the request will be
        /// rejected. (optional)</param>
        /// <returns><see cref="TestDocument" />TestDocument</returns>
        public DetailedResponse<TestDocument> TestConfigurationInEnvironment(string environmentId, string configuration = null, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, string step = null, string configurationId = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `TestConfigurationInEnvironment`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TestDocument> result = null;

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
                    formData.Add(fileContent, "file", filename);
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    metadataContent.Headers.ContentType = null;
                    formData.Add(metadataContent, "metadata");
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/preview");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(step))
                    restRequest.WithArgument("step", step);
                if (!string.IsNullOrEmpty(configurationId))
                    restRequest.WithArgument("configuration_id", configurationId);
                restRequest.WithBodyContent(formData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "TestConfigurationInEnvironment"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TestDocument>().Result;
                if (result == null)
                    result = new DetailedResponse<TestDocument>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> CreateCollection(string environmentId, string name, string description = null, string configurationId = null, string language = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `CreateCollection`");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("`name` is required for `CreateCollection`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (!string.IsNullOrEmpty(configurationId))
                    bodyObject["configuration_id"] = configurationId;
                if (!string.IsNullOrEmpty(language))
                    bodyObject["language"] = language;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateCollection"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Collection>().Result;
                if (result == null)
                    result = new DetailedResponse<Collection>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="DeleteCollectionResponse" />DeleteCollectionResponse</returns>
        public DetailedResponse<DeleteCollectionResponse> DeleteCollection(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteCollection`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `DeleteCollection`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DeleteCollectionResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteCollection"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DeleteCollectionResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<DeleteCollectionResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> GetCollection(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetCollection`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `GetCollection`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetCollection"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Collection>().Result;
                if (result == null)
                    result = new DetailedResponse<Collection>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="ListCollectionFieldsResponse" />ListCollectionFieldsResponse</returns>
        public DetailedResponse<ListCollectionFieldsResponse> ListCollectionFields(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListCollectionFields`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `ListCollectionFields`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<ListCollectionFieldsResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/fields");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListCollectionFields"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ListCollectionFieldsResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<ListCollectionFieldsResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="ListCollectionsResponse" />ListCollectionsResponse</returns>
        public DetailedResponse<ListCollectionsResponse> ListCollections(string environmentId, string name = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListCollections`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<ListCollectionsResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(name))
                    restRequest.WithArgument("name", name);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListCollections"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<ListCollectionsResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<ListCollectionsResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> UpdateCollection(string environmentId, string collectionId, string name, string description = null, string configurationId = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `UpdateCollection`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `UpdateCollection`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                if (!string.IsNullOrEmpty(description))
                    bodyObject["description"] = description;
                if (!string.IsNullOrEmpty(configurationId))
                    bodyObject["configuration_id"] = configurationId;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "UpdateCollection"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Collection>().Result;
                if (result == null)
                    result = new DetailedResponse<Collection>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Expansions" />Expansions</returns>
        public DetailedResponse<Expansions> CreateExpansions(string environmentId, string collectionId, List<Expansion> expansions)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `CreateExpansions`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `CreateExpansions`");
        if (expansions == null)
            throw new ArgumentNullException("`expansions` is required for `CreateExpansions`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Expansions> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (expansions != null && expansions.Count > 0)
                    bodyObject["expansions"] = JToken.FromObject(expansions);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateExpansions"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Expansions>().Result;
                if (result == null)
                    result = new DetailedResponse<Expansions>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create stopword list.
        ///
        /// Upload a custom stopword list to use with the specified collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="stopwordFile">The content of the stopword list to ingest.</param>
        /// <param name="stopwordFilename">The filename for stopwordFile.</param>
        /// <returns><see cref="TokenDictStatusResponse" />TokenDictStatusResponse</returns>
        public DetailedResponse<TokenDictStatusResponse> CreateStopwordList(string environmentId, string collectionId, System.IO.MemoryStream stopwordFile, string stopwordFilename)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `CreateStopwordList`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `CreateStopwordList`");
        if (stopwordFile == null)
            throw new ArgumentNullException("`stopwordFile` is required for `CreateStopwordList`");
        if (string.IsNullOrEmpty(stopwordFilename))
            throw new ArgumentNullException("`stopwordFilename` is required for `CreateStopwordList`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TokenDictStatusResponse> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (stopwordFile != null)
                {
                    var stopwordFileContent = new ByteArrayContent((stopwordFile as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    stopwordFileContent.Headers.ContentType = contentType;
                    formData.Add(stopwordFileContent, "stopword_file", stopwordFilename);
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithBodyContent(formData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateStopwordList"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TokenDictStatusResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<TokenDictStatusResponse>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create tokenization dictionary.
        ///
        /// Upload a custom tokenization dictionary to use with the specified collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="tokenizationDictionary">An object that represents the tokenization dictionary to be uploaded.
        /// (optional)</param>
        /// <returns><see cref="TokenDictStatusResponse" />TokenDictStatusResponse</returns>
        public DetailedResponse<TokenDictStatusResponse> CreateTokenizationDictionary(string environmentId, string collectionId, List<TokenDictRule> tokenizationRules = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `CreateTokenizationDictionary`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `CreateTokenizationDictionary`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TokenDictStatusResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (tokenizationRules != null && tokenizationRules.Count > 0)
                    bodyObject["tokenization_rules"] = JToken.FromObject(tokenizationRules);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateTokenizationDictionary"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TokenDictStatusResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<TokenDictStatusResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteExpansions(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteExpansions`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `DeleteExpansions`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                restRequest.WithArgument("version", VersionDate);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteExpansions"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a custom stopword list.
        ///
        /// Delete a custom stopword list from the collection. After a custom stopword list is deleted, the default list
        /// is used for the collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteStopwordList(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteStopwordList`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `DeleteStopwordList`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords");

                restRequest.WithArgument("version", VersionDate);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteStopwordList"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete tokenization dictionary.
        ///
        /// Delete the tokenization dictionary from the collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteTokenizationDictionary(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteTokenizationDictionary`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `DeleteTokenizationDictionary`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary");

                restRequest.WithArgument("version", VersionDate);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteTokenizationDictionary"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get stopword list status.
        ///
        /// Returns the current status of the stopword list for the specified collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="TokenDictStatusResponse" />TokenDictStatusResponse</returns>
        public DetailedResponse<TokenDictStatusResponse> GetStopwordListStatus(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetStopwordListStatus`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `GetStopwordListStatus`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TokenDictStatusResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetStopwordListStatus"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TokenDictStatusResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<TokenDictStatusResponse>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get tokenization dictionary status.
        ///
        /// Returns the current status of the tokenization dictionary for the specified collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="TokenDictStatusResponse" />TokenDictStatusResponse</returns>
        public DetailedResponse<TokenDictStatusResponse> GetTokenizationDictionaryStatus(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetTokenizationDictionaryStatus`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `GetTokenizationDictionaryStatus`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TokenDictStatusResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetTokenizationDictionaryStatus"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TokenDictStatusResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<TokenDictStatusResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Expansions" />Expansions</returns>
        public DetailedResponse<Expansions> ListExpansions(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListExpansions`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `ListExpansions`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Expansions> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListExpansions"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Expansions>().Result;
                if (result == null)
                    result = new DetailedResponse<Expansions>();
            }
            catch (AggregateException ae)
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
        /// `,`
        ///
        ///  **Note:** Documents can be added with a specific **document_id** by using the
        /// **_/v1/environments/{environment_id}/collections/{collection_id}/documents** method.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="file">The content of the document to ingest. The maximum supported file size when adding a file
        /// to a collection is 50 megabytes, the maximum supported file size when testing a confiruration is 1 megabyte.
        /// Files larger than the supported size are rejected. (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document
        /// against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1
        /// MB. Metadata parts larger than 1 MB are rejected.
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DetailedResponse<DocumentAccepted> AddDocument(string environmentId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `AddDocument`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `AddDocument`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DocumentAccepted> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", filename);
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    metadataContent.Headers.ContentType = null;
                    formData.Add(metadataContent, "metadata");
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithBodyContent(formData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "AddDocument"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DocumentAccepted>().Result;
                if (result == null)
                    result = new DetailedResponse<DocumentAccepted>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="DeleteDocumentResponse" />DeleteDocumentResponse</returns>
        public DetailedResponse<DeleteDocumentResponse> DeleteDocument(string environmentId, string collectionId, string documentId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteDocument`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `DeleteDocument`");
        if (string.IsNullOrEmpty(documentId))
            throw new ArgumentNullException("`documentId` is required for `DeleteDocument`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DeleteDocumentResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteDocument"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DeleteDocumentResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<DeleteDocumentResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="DocumentStatus" />DocumentStatus</returns>
        public DetailedResponse<DocumentStatus> GetDocumentStatus(string environmentId, string collectionId, string documentId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetDocumentStatus`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `GetDocumentStatus`");
        if (string.IsNullOrEmpty(documentId))
            throw new ArgumentNullException("`documentId` is required for `GetDocumentStatus`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DocumentStatus> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetDocumentStatus"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DocumentStatus>().Result;
                if (result == null)
                    result = new DetailedResponse<DocumentStatus>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a document.
        ///
        /// Replace an existing document or add a document with a specified **document_id**. Starts ingesting a document
        /// with optional metadata.
        ///
        /// **Note:** When uploading a new document with this method it automatically replaces any document stored with
        /// the same **document_id** if it exists.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="file">The content of the document to ingest. The maximum supported file size when adding a file
        /// to a collection is 50 megabytes, the maximum supported file size when testing a confiruration is 1 megabyte.
        /// Files larger than the supported size are rejected. (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document
        /// against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1
        /// MB. Metadata parts larger than 1 MB are rejected.
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DetailedResponse<DocumentAccepted> UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `UpdateDocument`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `UpdateDocument`");
        if (string.IsNullOrEmpty(documentId))
            throw new ArgumentNullException("`documentId` is required for `UpdateDocument`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DocumentAccepted> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent((file as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", filename);
                }

                if (metadata != null)
                {
                    var metadataContent = new StringContent(metadata, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    metadataContent.Headers.ContentType = null;
                    formData.Add(metadataContent, "metadata");
                }

                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithBodyContent(formData);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "UpdateDocument"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DocumentAccepted>().Result;
                if (result == null)
                    result = new DetailedResponse<DocumentAccepted>();
            }
            catch (AggregateException ae)
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
        /// see the [Discovery service
        /// documentation](https://cloud.ibm.com/docs/services/discovery?topic=discovery-query-concepts#query-concepts).
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="queryLong">An object that represents the query to be submitted. (optional)</param>
        /// <param name="loggingOptOut">If `true`, queries are not stored in the Discovery **Logs** endpoint. (optional,
        /// default to false)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        public DetailedResponse<QueryResponse> FederatedQuery(string environmentId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, string returnFields = null, long? offset = null, string sort = null, bool? highlight = null, string passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, string collectionIds = null, bool? similar = null, string similarDocumentIds = null, string similarFields = null, string bias = null, bool? loggingOptOut = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `FederatedQuery`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<QueryResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/query");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (loggingOptOut != null)
                    restRequest.WithHeader("X-Watson-Logging-Opt-Out", loggingOptOut.ToString());
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                if (loggingOptOut != null)
                {
                    restRequest.WithHeader("X-Watson-Logging-Opt-Out", (bool)loggingOptOut ? "true" : "false");
                }

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(filter))
                    bodyObject["filter"] = filter;
                if (!string.IsNullOrEmpty(query))
                    bodyObject["query"] = query;
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    bodyObject["natural_language_query"] = naturalLanguageQuery;
                if (passages != null)
                    bodyObject["passages"] = JToken.FromObject(passages);
                if (!string.IsNullOrEmpty(aggregation))
                    bodyObject["aggregation"] = aggregation;
                if (count != null)
                    bodyObject["count"] = JToken.FromObject(count);
                if (!string.IsNullOrEmpty(returnFields))
                    bodyObject["return"] = returnFields;
                if (offset != null)
                    bodyObject["offset"] = JToken.FromObject(offset);
                if (!string.IsNullOrEmpty(sort))
                    bodyObject["sort"] = sort;
                if (highlight != null)
                    bodyObject["highlight"] = JToken.FromObject(highlight);
                if (!string.IsNullOrEmpty(passagesFields))
                    bodyObject["passages.fields"] = passagesFields;
                if (passagesCount != null)
                    bodyObject["passages.count"] = JToken.FromObject(passagesCount);
                if (passagesCharacters != null)
                    bodyObject["passages.characters"] = JToken.FromObject(passagesCharacters);
                if (deduplicate != null)
                    bodyObject["deduplicate"] = JToken.FromObject(deduplicate);
                if (!string.IsNullOrEmpty(deduplicateField))
                    bodyObject["deduplicate.field"] = deduplicateField;
                if (!string.IsNullOrEmpty(collectionIds))
                    bodyObject["collection_ids"] = collectionIds;
                if (similar != null)
                    bodyObject["similar"] = JToken.FromObject(similar);
                if (!string.IsNullOrEmpty(similarDocumentIds))
                    bodyObject["similar.document_ids"] = similarDocumentIds;
                if (!string.IsNullOrEmpty(similarFields))
                    bodyObject["similar.fields"] = similarFields;
                if (!string.IsNullOrEmpty(bias))
                    bodyObject["bias"] = bias;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "FederatedQuery"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<QueryResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<QueryResponse>();
            }
            catch (AggregateException ae)
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
        /// documentation](https://cloud.ibm.com/docs/services/discovery?topic=discovery-query-concepts#query-concepts)
        /// for more details on the query language.
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
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10000**. (optional)</param>
        /// <param name="returnFields">A comma-separated list of the portion of the document hierarchy to return.
        /// (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. The maximum for the
        /// **count** and **offset** values together in any one query is **10000**. (optional)</param>
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
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        public DetailedResponse<QueryNoticesResponse> FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `FederatedQueryNotices`");
        if (collectionIds == null)
            throw new ArgumentNullException("`collectionIds` is required for `FederatedQueryNotices`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<QueryNoticesResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/notices");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "FederatedQueryNotices"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<QueryNoticesResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<QueryNoticesResponse>();
            }
            catch (AggregateException ae)
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
        /// see the [Discovery service
        /// documentation](https://cloud.ibm.com/docs/services/discovery?topic=discovery-query-concepts#query-concepts).
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryLong">An object that represents the query to be submitted. (optional)</param>
        /// <param name="loggingOptOut">If `true`, queries are not stored in the Discovery **Logs** endpoint. (optional,
        /// default to false)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        public DetailedResponse<QueryResponse> Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, string returnFields = null, long? offset = null, string sort = null, bool? highlight = null, string passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, string collectionIds = null, bool? similar = null, string similarDocumentIds = null, string similarFields = null, string bias = null, bool? loggingOptOut = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `Query`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `Query`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<QueryResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (loggingOptOut != null)
                    restRequest.WithHeader("X-Watson-Logging-Opt-Out", loggingOptOut.ToString());
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                if (loggingOptOut != null)
                {
                    restRequest.WithHeader("X-Watson-Logging-Opt-Out", (bool)loggingOptOut ? "true" : "false");
                }

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(filter))
                    bodyObject["filter"] = filter;
                if (!string.IsNullOrEmpty(query))
                    bodyObject["query"] = query;
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    bodyObject["natural_language_query"] = naturalLanguageQuery;
                if (passages != null)
                    bodyObject["passages"] = JToken.FromObject(passages);
                if (!string.IsNullOrEmpty(aggregation))
                    bodyObject["aggregation"] = aggregation;
                if (count != null)
                    bodyObject["count"] = JToken.FromObject(count);
                if (!string.IsNullOrEmpty(returnFields))
                    bodyObject["return"] = returnFields;
                if (offset != null)
                    bodyObject["offset"] = JToken.FromObject(offset);
                if (!string.IsNullOrEmpty(sort))
                    bodyObject["sort"] = sort;
                if (highlight != null)
                    bodyObject["highlight"] = JToken.FromObject(highlight);
                if (!string.IsNullOrEmpty(passagesFields))
                    bodyObject["passages.fields"] = passagesFields;
                if (passagesCount != null)
                    bodyObject["passages.count"] = JToken.FromObject(passagesCount);
                if (passagesCharacters != null)
                    bodyObject["passages.characters"] = JToken.FromObject(passagesCharacters);
                if (deduplicate != null)
                    bodyObject["deduplicate"] = JToken.FromObject(deduplicate);
                if (!string.IsNullOrEmpty(deduplicateField))
                    bodyObject["deduplicate.field"] = deduplicateField;
                if (!string.IsNullOrEmpty(collectionIds))
                    bodyObject["collection_ids"] = collectionIds;
                if (similar != null)
                    bodyObject["similar"] = JToken.FromObject(similar);
                if (!string.IsNullOrEmpty(similarDocumentIds))
                    bodyObject["similar.document_ids"] = similarDocumentIds;
                if (!string.IsNullOrEmpty(similarFields))
                    bodyObject["similar.fields"] = similarFields;
                if (!string.IsNullOrEmpty(bias))
                    bodyObject["bias"] = bias;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "Query"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<QueryResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<QueryResponse>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Knowledge Graph entity query.
        ///
        /// See the [Knowledge Graph documentation](https://cloud.ibm.com/docs/services/discovery?topic=discovery-kg#kg)
        /// for more details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="entityQuery">An object specifying the entities to query, which functions to perform, and any
        /// additional constraints.</param>
        /// <returns><see cref="QueryEntitiesResponse" />QueryEntitiesResponse</returns>
        public DetailedResponse<QueryEntitiesResponse> QueryEntities(string environmentId, string collectionId, string feature = null, QueryEntitiesEntity entity = null, QueryEntitiesContext context = null, long? count = null, long? evidenceCount = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `QueryEntities`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `QueryEntities`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<QueryEntitiesResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query_entities");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(feature))
                    bodyObject["feature"] = feature;
                if (entity != null)
                    bodyObject["entity"] = JToken.FromObject(entity);
                if (context != null)
                    bodyObject["context"] = JToken.FromObject(context);
                if (count != null)
                    bodyObject["count"] = JToken.FromObject(count);
                if (evidenceCount != null)
                    bodyObject["evidence_count"] = JToken.FromObject(evidenceCount);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "QueryEntities"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<QueryEntitiesResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<QueryEntitiesResponse>();
            }
            catch (AggregateException ae)
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
        /// documentation](https://cloud.ibm.com/docs/services/discovery?topic=discovery-query-concepts#query-concepts)
        /// for more details on the query language.
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
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10000**. (optional)</param>
        /// <param name="returnFields">A comma-separated list of the portion of the document hierarchy to return.
        /// (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. The maximum for the
        /// **count** and **offset** values together in any one query is **10000**. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true, a highlight field is returned for each result which contains the fields
        /// which match the query with `<em></em>` tags around the matching query terms. (optional, default to
        /// false)</param>
        /// <param name="passagesFields">A comma-separated list of fields that passages are drawn from. If this
        /// parameter not specified, then all top-level fields are included. (optional)</param>
        /// <param name="passagesCount">The maximum number of passages to return. The search returns fewer passages if
        /// the requested total is not found. (optional)</param>
        /// <param name="passagesCharacters">The approximate number of characters that any one passage will have.
        /// (optional)</param>
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
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        public DetailedResponse<QueryNoticesResponse> QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `QueryNotices`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `QueryNotices`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<QueryNoticesResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/notices");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "QueryNotices"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<QueryNoticesResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<QueryNoticesResponse>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Knowledge Graph relationship query.
        ///
        /// See the [Knowledge Graph documentation](https://cloud.ibm.com/docs/services/discovery?topic=discovery-kg#kg)
        /// for more details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="relationshipQuery">An object that describes the relationships to be queried and any query
        /// constraints (such as filters).</param>
        /// <returns><see cref="QueryRelationsResponse" />QueryRelationsResponse</returns>
        public DetailedResponse<QueryRelationsResponse> QueryRelations(string environmentId, string collectionId, List<QueryRelationsEntity> entities = null, QueryEntitiesContext context = null, string sort = null, QueryRelationsFilter filter = null, long? count = null, long? evidenceCount = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `QueryRelations`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `QueryRelations`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<QueryRelationsResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query_relations");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (entities != null && entities.Count > 0)
                    bodyObject["entities"] = JToken.FromObject(entities);
                if (context != null)
                    bodyObject["context"] = JToken.FromObject(context);
                if (!string.IsNullOrEmpty(sort))
                    bodyObject["sort"] = sort;
                if (filter != null)
                    bodyObject["filter"] = JToken.FromObject(filter);
                if (count != null)
                    bodyObject["count"] = JToken.FromObject(count);
                if (evidenceCount != null)
                    bodyObject["evidence_count"] = JToken.FromObject(evidenceCount);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "QueryRelations"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<QueryRelationsResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<QueryRelationsResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> AddTrainingData(string environmentId, string collectionId, string naturalLanguageQuery = null, string filter = null, List<TrainingExample> examples = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `AddTrainingData`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `AddTrainingData`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                    bodyObject["natural_language_query"] = naturalLanguageQuery;
                if (!string.IsNullOrEmpty(filter))
                    bodyObject["filter"] = filter;
                if (examples != null && examples.Count > 0)
                    bodyObject["examples"] = JToken.FromObject(examples);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "AddTrainingData"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TrainingQuery>().Result;
                if (result == null)
                    result = new DetailedResponse<TrainingQuery>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        public DetailedResponse<TrainingExample> CreateTrainingExample(string environmentId, string collectionId, string queryId, string documentId = null, string crossReference = null, long? relevance = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `CreateTrainingExample`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `CreateTrainingExample`");
        if (string.IsNullOrEmpty(queryId))
            throw new ArgumentNullException("`queryId` is required for `CreateTrainingExample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TrainingExample> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(documentId))
                    bodyObject["document_id"] = documentId;
                if (!string.IsNullOrEmpty(crossReference))
                    bodyObject["cross_reference"] = crossReference;
                if (relevance != null)
                    bodyObject["relevance"] = JToken.FromObject(relevance);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateTrainingExample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TrainingExample>().Result;
                if (result == null)
                    result = new DetailedResponse<TrainingExample>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteAllTrainingData(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteAllTrainingData`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `DeleteAllTrainingData`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                restRequest.WithArgument("version", VersionDate);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteAllTrainingData"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteTrainingData(string environmentId, string collectionId, string queryId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteTrainingData`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `DeleteTrainingData`");
        if (string.IsNullOrEmpty(queryId))
            throw new ArgumentNullException("`queryId` is required for `DeleteTrainingData`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}");

                restRequest.WithArgument("version", VersionDate);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteTrainingData"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteTrainingExample(string environmentId, string collectionId, string queryId, string exampleId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteTrainingExample`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `DeleteTrainingExample`");
        if (string.IsNullOrEmpty(queryId))
            throw new ArgumentNullException("`queryId` is required for `DeleteTrainingExample`");
        if (string.IsNullOrEmpty(exampleId))
            throw new ArgumentNullException("`exampleId` is required for `DeleteTrainingExample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                restRequest.WithArgument("version", VersionDate);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteTrainingExample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> GetTrainingData(string environmentId, string collectionId, string queryId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetTrainingData`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `GetTrainingData`");
        if (string.IsNullOrEmpty(queryId))
            throw new ArgumentNullException("`queryId` is required for `GetTrainingData`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetTrainingData"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TrainingQuery>().Result;
                if (result == null)
                    result = new DetailedResponse<TrainingQuery>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        public DetailedResponse<TrainingExample> GetTrainingExample(string environmentId, string collectionId, string queryId, string exampleId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetTrainingExample`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `GetTrainingExample`");
        if (string.IsNullOrEmpty(queryId))
            throw new ArgumentNullException("`queryId` is required for `GetTrainingExample`");
        if (string.IsNullOrEmpty(exampleId))
            throw new ArgumentNullException("`exampleId` is required for `GetTrainingExample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TrainingExample> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetTrainingExample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TrainingExample>().Result;
                if (result == null)
                    result = new DetailedResponse<TrainingExample>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="TrainingDataSet" />TrainingDataSet</returns>
        public DetailedResponse<TrainingDataSet> ListTrainingData(string environmentId, string collectionId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListTrainingData`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `ListTrainingData`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TrainingDataSet> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListTrainingData"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TrainingDataSet>().Result;
                if (result == null)
                    result = new DetailedResponse<TrainingDataSet>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="TrainingExampleList" />TrainingExampleList</returns>
        public DetailedResponse<TrainingExampleList> ListTrainingExamples(string environmentId, string collectionId, string queryId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListTrainingExamples`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `ListTrainingExamples`");
        if (string.IsNullOrEmpty(queryId))
            throw new ArgumentNullException("`queryId` is required for `ListTrainingExamples`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TrainingExampleList> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListTrainingExamples"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TrainingExampleList>().Result;
                if (result == null)
                    result = new DetailedResponse<TrainingExampleList>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        public DetailedResponse<TrainingExample> UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, string crossReference = null, long? relevance = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `UpdateTrainingExample`");
        if (string.IsNullOrEmpty(collectionId))
            throw new ArgumentNullException("`collectionId` is required for `UpdateTrainingExample`");
        if (string.IsNullOrEmpty(queryId))
            throw new ArgumentNullException("`queryId` is required for `UpdateTrainingExample`");
        if (string.IsNullOrEmpty(exampleId))
            throw new ArgumentNullException("`exampleId` is required for `UpdateTrainingExample`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<TrainingExample> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(crossReference))
                    bodyObject["cross_reference"] = crossReference;
                if (relevance != null)
                    bodyObject["relevance"] = JToken.FromObject(relevance);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "UpdateTrainingExample"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<TrainingExample>().Result;
                if (result == null)
                    result = new DetailedResponse<TrainingExample>();
            }
            catch (AggregateException ae)
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
        /// security](https://cloud.ibm.com/docs/services/discovery?topic=discovery-information-security#information-security).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteUserData(string customerId)
        {
        if (string.IsNullOrEmpty(customerId))
            throw new ArgumentNullException("`customerId` is required for `DeleteUserData`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/user_data");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(customerId))
                    restRequest.WithArgument("customer_id", customerId);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteUserData"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<object>().Result;
                if (result == null)
                    result = new DetailedResponse<object>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="CreateEventResponse" />CreateEventResponse</returns>
        public DetailedResponse<CreateEventResponse> CreateEvent(string type, EventData data)
        {
        if (string.IsNullOrEmpty(type))
            throw new ArgumentNullException("`type` is required for `CreateEvent`");
        if (data == null)
            throw new ArgumentNullException("`data` is required for `CreateEvent`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<CreateEventResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/events");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(type))
                    bodyObject["type"] = type;
                if (data != null)
                    bodyObject["data"] = JToken.FromObject(data);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateEvent"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<CreateEventResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<CreateEventResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="MetricResponse" />MetricResponse</returns>
        public DetailedResponse<MetricResponse> GetMetricsEventRate(DateTime? startTime = null, DateTime? endTime = null, string resultType = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<MetricResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/event_rate");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (startTime != null)
                    restRequest.WithArgument("start_time", startTime);
                if (endTime != null)
                    restRequest.WithArgument("end_time", endTime);
                if (!string.IsNullOrEmpty(resultType))
                    restRequest.WithArgument("result_type", resultType);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetMetricsEventRate"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<MetricResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<MetricResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="MetricResponse" />MetricResponse</returns>
        public DetailedResponse<MetricResponse> GetMetricsQuery(DateTime? startTime = null, DateTime? endTime = null, string resultType = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<MetricResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (startTime != null)
                    restRequest.WithArgument("start_time", startTime);
                if (endTime != null)
                    restRequest.WithArgument("end_time", endTime);
                if (!string.IsNullOrEmpty(resultType))
                    restRequest.WithArgument("result_type", resultType);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetMetricsQuery"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<MetricResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<MetricResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="MetricResponse" />MetricResponse</returns>
        public DetailedResponse<MetricResponse> GetMetricsQueryEvent(DateTime? startTime = null, DateTime? endTime = null, string resultType = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<MetricResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries_with_event");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (startTime != null)
                    restRequest.WithArgument("start_time", startTime);
                if (endTime != null)
                    restRequest.WithArgument("end_time", endTime);
                if (!string.IsNullOrEmpty(resultType))
                    restRequest.WithArgument("result_type", resultType);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetMetricsQueryEvent"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<MetricResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<MetricResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="MetricResponse" />MetricResponse</returns>
        public DetailedResponse<MetricResponse> GetMetricsQueryNoResults(DateTime? startTime = null, DateTime? endTime = null, string resultType = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<MetricResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries_with_no_search_results");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (startTime != null)
                    restRequest.WithArgument("start_time", startTime);
                if (endTime != null)
                    restRequest.WithArgument("end_time", endTime);
                if (!string.IsNullOrEmpty(resultType))
                    restRequest.WithArgument("result_type", resultType);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetMetricsQueryNoResults"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<MetricResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<MetricResponse>();
            }
            catch (AggregateException ae)
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
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10000**. (optional)</param>
        /// <returns><see cref="MetricTokenResponse" />MetricTokenResponse</returns>
        public DetailedResponse<MetricTokenResponse> GetMetricsQueryTokenEvent(long? count = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<MetricTokenResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/top_query_tokens_with_event_rate");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (count != null)
                    restRequest.WithArgument("count", count);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetMetricsQueryTokenEvent"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<MetricTokenResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<MetricTokenResponse>();
            }
            catch (AggregateException ae)
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
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10000**. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. The maximum for the
        /// **count** and **offset** values together in any one query is **10000**. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. (optional)</param>
        /// <returns><see cref="LogQueryResponse" />LogQueryResponse</returns>
        public DetailedResponse<LogQueryResponse> QueryLog(string filter = null, string query = null, long? count = null, long? offset = null, List<string> sort = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<LogQueryResponse> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/logs");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "QueryLog"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<LogQueryResponse>().Result;
                if (result == null)
                    result = new DetailedResponse<LogQueryResponse>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Credentials" />Credentials</returns>
        public DetailedResponse<Credentials> CreateCredentials(string environmentId, string sourceType = null, CredentialDetails credentialDetails = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `CreateCredentials`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Credentials> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(sourceType))
                    bodyObject["source_type"] = sourceType;
                if (credentialDetails != null)
                    bodyObject["credential_details"] = JToken.FromObject(credentialDetails);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateCredentials"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Credentials>().Result;
                if (result == null)
                    result = new DetailedResponse<Credentials>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="DeleteCredentials" />DeleteCredentials</returns>
        public DetailedResponse<DeleteCredentials> DeleteCredentials(string environmentId, string credentialId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteCredentials`");
        if (string.IsNullOrEmpty(credentialId))
            throw new ArgumentNullException("`credentialId` is required for `DeleteCredentials`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<DeleteCredentials> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteCredentials"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<DeleteCredentials>().Result;
                if (result == null)
                    result = new DetailedResponse<DeleteCredentials>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Credentials" />Credentials</returns>
        public DetailedResponse<Credentials> GetCredentials(string environmentId, string credentialId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetCredentials`");
        if (string.IsNullOrEmpty(credentialId))
            throw new ArgumentNullException("`credentialId` is required for `GetCredentials`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Credentials> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetCredentials"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Credentials>().Result;
                if (result == null)
                    result = new DetailedResponse<Credentials>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="CredentialsList" />CredentialsList</returns>
        public DetailedResponse<CredentialsList> ListCredentials(string environmentId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListCredentials`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<CredentialsList> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListCredentials"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<CredentialsList>().Result;
                if (result == null)
                    result = new DetailedResponse<CredentialsList>();
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="Credentials" />Credentials</returns>
        public DetailedResponse<Credentials> UpdateCredentials(string environmentId, string credentialId, string sourceType = null, CredentialDetails credentialDetails = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `UpdateCredentials`");
        if (string.IsNullOrEmpty(credentialId))
            throw new ArgumentNullException("`credentialId` is required for `UpdateCredentials`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Credentials> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(sourceType))
                    bodyObject["source_type"] = sourceType;
                if (credentialDetails != null)
                    bodyObject["credential_details"] = JToken.FromObject(credentialDetails);
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "UpdateCredentials"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Credentials>().Result;
                if (result == null)
                    result = new DetailedResponse<Credentials>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create Gateway.
        ///
        /// Create a gateway configuration to use with a remotely installed gateway.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="gatewayName">The name of the gateway to created. (optional)</param>
        /// <returns><see cref="Gateway" />Gateway</returns>
        public DetailedResponse<Gateway> CreateGateway(string environmentId, string name = null)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `CreateGateway`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Gateway> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/gateways");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");
                restRequest.WithHeader("Accept", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                    bodyObject["name"] = name;
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, "application/json");
                restRequest.WithBodyContent(httpContent);

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "CreateGateway"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Gateway>().Result;
                if (result == null)
                    result = new DetailedResponse<Gateway>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete Gateway.
        ///
        /// Delete the specified gateway configuration.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="gatewayId">The requested gateway ID.</param>
        /// <returns><see cref="GatewayDelete" />GatewayDelete</returns>
        public DetailedResponse<GatewayDelete> DeleteGateway(string environmentId, string gatewayId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `DeleteGateway`");
        if (string.IsNullOrEmpty(gatewayId))
            throw new ArgumentNullException("`gatewayId` is required for `DeleteGateway`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<GatewayDelete> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/gateways/{gatewayId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "DeleteGateway"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<GatewayDelete>().Result;
                if (result == null)
                    result = new DetailedResponse<GatewayDelete>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List Gateway Details.
        ///
        /// List information about the specified gateway.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="gatewayId">The requested gateway ID.</param>
        /// <returns><see cref="Gateway" />Gateway</returns>
        public DetailedResponse<Gateway> GetGateway(string environmentId, string gatewayId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `GetGateway`");
        if (string.IsNullOrEmpty(gatewayId))
            throw new ArgumentNullException("`gatewayId` is required for `GetGateway`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<Gateway> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/gateways/{gatewayId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "GetGateway"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<Gateway>().Result;
                if (result == null)
                    result = new DetailedResponse<Gateway>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List Gateways.
        ///
        /// List the currently configured gateways.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <returns><see cref="GatewayList" />GatewayList</returns>
        public DetailedResponse<GatewayList> ListGateways(string environmentId)
        {
        if (string.IsNullOrEmpty(environmentId))
            throw new ArgumentNullException("`environmentId` is required for `ListGateways`");

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DetailedResponse<GatewayList> result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/gateways");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "v1", "ListGateways"))
                {
                   restRequest.WithHeader(kvp.Key, kvp.Value);
                }

                result = restRequest.As<GatewayList>().Result;
                if (result == null)
                    result = new DetailedResponse<GatewayList>();
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
