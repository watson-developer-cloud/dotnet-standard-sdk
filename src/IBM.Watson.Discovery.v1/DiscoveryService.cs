/**
* (C) Copyright IBM Corp. 2020.
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

/**
* IBM OpenAPI SDK Code Generator Version: 99-SNAPSHOT-be3b4618-20201201-123423
*/
 
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.Discovery.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Environment = IBM.Watson.Discovery.v1.Model.Environment;

namespace IBM.Watson.Discovery.v1
{
    public partial class DiscoveryService : IBMService, IDiscoveryService
    {
        const string defaultServiceName = "discovery";
        private const string defaultServiceUrl = "https://api.us-south.discovery.watson.cloud.ibm.com";
        public string Version { get; set; }

        public DiscoveryService(string version) : this(version, defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public DiscoveryService(string version, IAuthenticator authenticator) : this(version, defaultServiceName, authenticator) {}
        public DiscoveryService(string version, string serviceName) : this(version, serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public DiscoveryService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public DiscoveryService(string version, string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentNullException("`version` is required");
            }
            Version = version;

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
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
        /// <param name="name">Name that identifies the environment.</param>
        /// <param name="description">Description of the environment. (optional, default to )</param>
        /// <param name="size">Size of the environment. In the Lite plan the default and only accepted value is `LT`, in
        /// all other plans the default is `S`. (optional)</param>
        /// <returns><see cref="Environment" />Environment</returns>
        public DetailedResponse<Environment> CreateEnvironment(string name, string description = null, string size = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateEnvironment`");
            }
            DetailedResponse<Environment> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (!string.IsNullOrEmpty(size))
                {
                    bodyObject["size"] = size;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateEnvironment"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Environment>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Environment>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ListEnvironmentsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    restRequest.WithArgument("name", name);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListEnvironments"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListEnvironmentsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListEnvironmentsResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetEnvironment`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<Environment> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetEnvironment"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Environment>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Environment>();
                }
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
        /// <param name="name">Name that identifies the environment. (optional, default to )</param>
        /// <param name="description">Description of the environment. (optional, default to )</param>
        /// <param name="size">Size that the environment should be increased to. Environment size cannot be modified
        /// when using a Lite plan. Environment size can only increased and not decreased. (optional)</param>
        /// <returns><see cref="Environment" />Environment</returns>
        public DetailedResponse<Environment> UpdateEnvironment(string environmentId, string name = null, string description = null, string size = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `UpdateEnvironment`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<Environment> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (!string.IsNullOrEmpty(size))
                {
                    bodyObject["size"] = size;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "UpdateEnvironment"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Environment>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Environment>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteEnvironment`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<DeleteEnvironmentResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteEnvironment"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteEnvironmentResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteEnvironmentResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListFields`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (collectionIds == null)
            {
                throw new ArgumentNullException("`collectionIds` is required for `ListFields`");
            }
            DetailedResponse<ListCollectionFieldsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/fields");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (collectionIds != null && collectionIds.Count > 0)
                {
                    restRequest.WithArgument("collection_ids", string.Join(",", collectionIds.ToArray()));
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListFields"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListCollectionFieldsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListCollectionFieldsResponse>();
                }
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
        /// <param name="name">The name of the configuration.</param>
        /// <param name="description">The description of the configuration, if available. (optional)</param>
        /// <param name="conversions">Document conversion settings. (optional)</param>
        /// <param name="enrichments">An array of document enrichment settings for the configuration. (optional)</param>
        /// <param name="normalizations">Defines operations that can be used to transform the final output JSON into a
        /// normalized form. Operations are executed in the order that they appear in the array. (optional)</param>
        /// <param name="source">Object containing source parameters for the configuration. (optional)</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        public DetailedResponse<Configuration> CreateConfiguration(string environmentId, string name, string description = null, Conversions conversions = null, List<Enrichment> enrichments = null, List<NormalizationOperation> normalizations = null, Source source = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `CreateConfiguration`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateConfiguration`");
            }
            DetailedResponse<Configuration> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (conversions != null)
                {
                    bodyObject["conversions"] = JToken.FromObject(conversions);
                }
                if (enrichments != null && enrichments.Count > 0)
                {
                    bodyObject["enrichments"] = JToken.FromObject(enrichments);
                }
                if (normalizations != null && normalizations.Count > 0)
                {
                    bodyObject["normalizations"] = JToken.FromObject(normalizations);
                }
                if (source != null)
                {
                    bodyObject["source"] = JToken.FromObject(source);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateConfiguration"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Configuration>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Configuration>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListConfigurations`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<ListConfigurationsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    restRequest.WithArgument("name", name);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListConfigurations"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListConfigurationsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListConfigurationsResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetConfiguration`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(configurationId))
            {
                throw new ArgumentNullException("`configurationId` is required for `GetConfiguration`");
            }
            else
            {
                configurationId = Uri.EscapeDataString(configurationId);
            }
            DetailedResponse<Configuration> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetConfiguration"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Configuration>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Configuration>();
                }
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
        /// <param name="name">The name of the configuration.</param>
        /// <param name="description">The description of the configuration, if available. (optional)</param>
        /// <param name="conversions">Document conversion settings. (optional)</param>
        /// <param name="enrichments">An array of document enrichment settings for the configuration. (optional)</param>
        /// <param name="normalizations">Defines operations that can be used to transform the final output JSON into a
        /// normalized form. Operations are executed in the order that they appear in the array. (optional)</param>
        /// <param name="source">Object containing source parameters for the configuration. (optional)</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        public DetailedResponse<Configuration> UpdateConfiguration(string environmentId, string configurationId, string name, string description = null, Conversions conversions = null, List<Enrichment> enrichments = null, List<NormalizationOperation> normalizations = null, Source source = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `UpdateConfiguration`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(configurationId))
            {
                throw new ArgumentNullException("`configurationId` is required for `UpdateConfiguration`");
            }
            else
            {
                configurationId = Uri.EscapeDataString(configurationId);
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `UpdateConfiguration`");
            }
            DetailedResponse<Configuration> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (conversions != null)
                {
                    bodyObject["conversions"] = JToken.FromObject(conversions);
                }
                if (enrichments != null && enrichments.Count > 0)
                {
                    bodyObject["enrichments"] = JToken.FromObject(enrichments);
                }
                if (normalizations != null && normalizations.Count > 0)
                {
                    bodyObject["normalizations"] = JToken.FromObject(normalizations);
                }
                if (source != null)
                {
                    bodyObject["source"] = JToken.FromObject(source);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "UpdateConfiguration"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Configuration>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Configuration>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteConfiguration`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(configurationId))
            {
                throw new ArgumentNullException("`configurationId` is required for `DeleteConfiguration`");
            }
            else
            {
                configurationId = Uri.EscapeDataString(configurationId);
            }
            DetailedResponse<DeleteConfigurationResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/configurations/{configurationId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteConfiguration"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteConfigurationResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteConfigurationResponse>();
                }
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
        /// <param name="name">The name of the collection to be created.</param>
        /// <param name="description">A description of the collection. (optional, default to )</param>
        /// <param name="configurationId">The ID of the configuration in which the collection is to be created.
        /// (optional, default to )</param>
        /// <param name="language">The language of the documents stored in the collection, in the form of an ISO 639-1
        /// language code. (optional, default to en)</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> CreateCollection(string environmentId, string name, string description = null, string configurationId = null, string language = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `CreateCollection`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateCollection`");
            }
            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (!string.IsNullOrEmpty(configurationId))
                {
                    bodyObject["configuration_id"] = configurationId;
                }
                if (!string.IsNullOrEmpty(language))
                {
                    bodyObject["language"] = language;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Collection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Collection>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListCollections`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<ListCollectionsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    restRequest.WithArgument("name", name);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListCollections"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListCollectionsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListCollectionsResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetCollection`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetCollection`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Collection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Collection>();
                }
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
        /// <param name="name">The name of the collection.</param>
        /// <param name="description">A description of the collection. (optional, default to )</param>
        /// <param name="configurationId">The ID of the configuration in which the collection is to be updated.
        /// (optional, default to )</param>
        /// <returns><see cref="Collection" />Collection</returns>
        public DetailedResponse<Collection> UpdateCollection(string environmentId, string collectionId, string name, string description = null, string configurationId = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `UpdateCollection`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `UpdateCollection`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `UpdateCollection`");
            }
            DetailedResponse<Collection> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (!string.IsNullOrEmpty(configurationId))
                {
                    bodyObject["configuration_id"] = configurationId;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "UpdateCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Collection>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Collection>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteCollection`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteCollection`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<DeleteCollectionResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteCollectionResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteCollectionResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListCollectionFields`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `ListCollectionFields`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<ListCollectionFieldsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/fields");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListCollectionFields"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListCollectionFieldsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListCollectionFieldsResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListExpansions`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `ListExpansions`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<Expansions> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListExpansions"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Expansions>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Expansions>();
                }
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
        /// collection is `500`. The current expansion list is replaced with the uploaded content.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="expansions">An array of query expansion definitions.
        ///
        ///  Each object in the **expansions** array represents a term or set of terms that will be expanded into other
        /// terms. Each expansion object can be configured as bidirectional or unidirectional. Bidirectional means that
        /// all terms are expanded to all other terms in the object. Unidirectional means that a set list of terms can
        /// be expanded into a second list of terms.
        ///
        ///  To create a bi-directional expansion specify an **expanded_terms** array. When found in a query, all items
        /// in the **expanded_terms** array are then expanded to the other items in the same array.
        ///
        ///  To create a uni-directional expansion, specify both an array of **input_terms** and an array of
        /// **expanded_terms**. When items in the **input_terms** array are present in a query, they are expanded using
        /// the items listed in the **expanded_terms** array.</param>
        /// <returns><see cref="Expansions" />Expansions</returns>
        public DetailedResponse<Expansions> CreateExpansions(string environmentId, string collectionId, List<Expansion> expansions)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `CreateExpansions`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `CreateExpansions`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (expansions == null)
            {
                throw new ArgumentNullException("`expansions` is required for `CreateExpansions`");
            }
            DetailedResponse<Expansions> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (expansions != null && expansions.Count > 0)
                {
                    bodyObject["expansions"] = JToken.FromObject(expansions);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateExpansions"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Expansions>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Expansions>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteExpansions`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteExpansions`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/expansions");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteExpansions"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetTokenizationDictionaryStatus`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetTokenizationDictionaryStatus`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<TokenDictStatusResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetTokenizationDictionaryStatus"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TokenDictStatusResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TokenDictStatusResponse>();
                }
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
        /// <param name="tokenizationRules">An array of tokenization rules. Each rule contains, the original `text`
        /// string, component `tokens`, any alternate character set `readings`, and which `part_of_speech` the text is
        /// from. (optional)</param>
        /// <returns><see cref="TokenDictStatusResponse" />TokenDictStatusResponse</returns>
        public DetailedResponse<TokenDictStatusResponse> CreateTokenizationDictionary(string environmentId, string collectionId, List<TokenDictRule> tokenizationRules = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `CreateTokenizationDictionary`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `CreateTokenizationDictionary`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<TokenDictStatusResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (tokenizationRules != null && tokenizationRules.Count > 0)
                {
                    bodyObject["tokenization_rules"] = JToken.FromObject(tokenizationRules);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateTokenizationDictionary"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TokenDictStatusResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TokenDictStatusResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteTokenizationDictionary`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteTokenizationDictionary`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/tokenization_dictionary");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteTokenizationDictionary"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetStopwordListStatus`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetStopwordListStatus`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<TokenDictStatusResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetStopwordListStatus"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TokenDictStatusResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TokenDictStatusResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `CreateStopwordList`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `CreateStopwordList`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (stopwordFile == null)
            {
                throw new ArgumentNullException("`stopwordFile` is required for `CreateStopwordList`");
            }
            if (string.IsNullOrEmpty(stopwordFilename))
            {
                throw new ArgumentNullException("`stopwordFilename` is required for `CreateStopwordList`");
            }
            DetailedResponse<TokenDictStatusResponse> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (stopwordFile != null)
                {
                    var stopwordFileContent = new ByteArrayContent(stopwordFile.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    stopwordFileContent.Headers.ContentType = contentType;
                    formData.Add(stopwordFileContent, "stopword_file", stopwordFilename);
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateStopwordList"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TokenDictStatusResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TokenDictStatusResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteStopwordList`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteStopwordList`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/word_lists/stopwords");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteStopwordList"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
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
        /// to a collection is 50 megabytes, the maximum supported file size when testing a configuration is 1 megabyte.
        /// Files larger than the supported size are rejected. (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are
        /// rejected. Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DetailedResponse<DocumentAccepted> AddDocument(string environmentId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `AddDocument`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `AddDocument`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<DocumentAccepted> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
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
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "AddDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentAccepted>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentAccepted>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for AddDocument.
        /// </summary>
        public class AddDocumentEnums
        {
            /// <summary>
            /// The content type of file.
            /// </summary>
            public class FileContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant TEXT_HTML for text/html
                /// </summary>
                public const string TEXT_HTML = "text/html";
                /// <summary>
                /// Constant APPLICATION_XHTML_XML for application/xhtml+xml
                /// </summary>
                public const string APPLICATION_XHTML_XML = "application/xhtml+xml";
                
            }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetDocumentStatus`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetDocumentStatus`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `GetDocumentStatus`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }
            DetailedResponse<DocumentStatus> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetDocumentStatus"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentStatus>();
                }
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
        /// to a collection is 50 megabytes, the maximum supported file size when testing a configuration is 1 megabyte.
        /// Files larger than the supported size are rejected. (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are
        /// rejected. Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DetailedResponse<DocumentAccepted> UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `UpdateDocument`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `UpdateDocument`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `UpdateDocument`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }
            DetailedResponse<DocumentAccepted> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
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
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "UpdateDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentAccepted>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentAccepted>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for UpdateDocument.
        /// </summary>
        public class UpdateDocumentEnums
        {
            /// <summary>
            /// The content type of file.
            /// </summary>
            public class FileContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant TEXT_HTML for text/html
                /// </summary>
                public const string TEXT_HTML = "text/html";
                /// <summary>
                /// Constant APPLICATION_XHTML_XML for application/xhtml+xml
                /// </summary>
                public const string APPLICATION_XHTML_XML = "application/xhtml+xml";
                
            }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteDocument`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteDocument`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `DeleteDocument`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }
            DetailedResponse<DeleteDocumentResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteDocumentResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteDocumentResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Query a collection.
        ///
        /// By using this method, you can construct long queries. For details, see the [Discovery
        /// documentation](https://cloud.ibm.com/docs/discovery?topic=discovery-query-concepts#query-concepts).
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. Use a query search when you want to find the most
        /// relevant search results. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing
        /// training data and natural language understanding. (optional)</param>
        /// <param name="passages">A passages query that returns the most relevant passages from the results.
        /// (optional)</param>
        /// <param name="aggregation">An aggregation search that returns an exact answer by combining query search with
        /// filters. Useful for applications to build lists, tables, and time series. For a full list of possible
        /// aggregations, see the Query reference. (optional)</param>
        /// <param name="count">Number of results to return. (optional)</param>
        /// <param name="_return">A comma-separated list of the portion of the document hierarchy to return.
        /// (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. This parameter cannot be used in the same query as the **bias**
        /// parameter. (optional)</param>
        /// <param name="highlight">When true, a highlight field is returned for each result which contains the fields
        /// which match the query with `<em></em>` tags around the matching query terms. (optional, default to
        /// false)</param>
        /// <param name="passagesFields">A comma-separated list of fields that passages are drawn from. If this
        /// parameter not specified, then all top-level fields are included. (optional)</param>
        /// <param name="passagesCount">The maximum number of passages to return. The search returns fewer passages if
        /// the requested total is not found. The default is `10`. The maximum is `100`. (optional)</param>
        /// <param name="passagesCharacters">The approximate number of characters that any one passage will have.
        /// (optional)</param>
        /// <param name="deduplicate">When `true`, and used with a Watson Discovery News collection, duplicate results
        /// (based on the contents of the **title** field) are removed. Duplicate comparison is limited to the current
        /// query only; **offset** is not considered. This parameter is currently Beta functionality. (optional, default
        /// to false)</param>
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
        /// <param name="bias">Field which the returned results will be biased against. The specified field must be
        /// either a **date** or **number** format. When a **date** type field is specified returned results are biased
        /// towards field values closer to the current date. When a **number** type field is specified, returned results
        /// are biased towards higher field values. This parameter cannot be used in the same query as the **sort**
        /// parameter. (optional)</param>
        /// <param name="spellingSuggestions">When `true` and the **natural_language_query** parameter is used, the
        /// **natural_languge_query** parameter is spell checked. The most likely correction is retunred in the
        /// **suggested_query** field of the response (if one exists).
        ///
        /// **Important:** this parameter is only valid when using the Cloud Pak version of Discovery. (optional,
        /// default to false)</param>
        /// <param name="xWatsonLoggingOptOut">If `true`, queries are not stored in the Discovery **Logs** endpoint.
        /// (optional, default to false)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        public DetailedResponse<QueryResponse> Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, string _return = null, long? offset = null, string sort = null, bool? highlight = null, string passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, string similarDocumentIds = null, string similarFields = null, string bias = null, bool? spellingSuggestions = null, bool? xWatsonLoggingOptOut = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `Query`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `Query`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<QueryResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/query");

                restRequest.WithHeader("Accept", "application/json");

                if (xWatsonLoggingOptOut != null)
                {
                    restRequest.WithHeader("X-Watson-Logging-Opt-Out", (bool)xWatsonLoggingOptOut ? "true" : "false");
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(filter))
                {
                    bodyObject["filter"] = filter;
                }
                if (!string.IsNullOrEmpty(query))
                {
                    bodyObject["query"] = query;
                }
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                {
                    bodyObject["natural_language_query"] = naturalLanguageQuery;
                }
                if (passages != null)
                {
                    bodyObject["passages"] = JToken.FromObject(passages);
                }
                if (!string.IsNullOrEmpty(aggregation))
                {
                    bodyObject["aggregation"] = aggregation;
                }
                if (count != null)
                {
                    bodyObject["count"] = JToken.FromObject(count);
                }
                if (!string.IsNullOrEmpty(_return))
                {
                    bodyObject["return"] = _return;
                }
                if (offset != null)
                {
                    bodyObject["offset"] = JToken.FromObject(offset);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    bodyObject["sort"] = sort;
                }
                if (highlight != null)
                {
                    bodyObject["highlight"] = JToken.FromObject(highlight);
                }
                if (!string.IsNullOrEmpty(passagesFields))
                {
                    bodyObject["passages.fields"] = passagesFields;
                }
                if (passagesCount != null)
                {
                    bodyObject["passages.count"] = JToken.FromObject(passagesCount);
                }
                if (passagesCharacters != null)
                {
                    bodyObject["passages.characters"] = JToken.FromObject(passagesCharacters);
                }
                if (deduplicate != null)
                {
                    bodyObject["deduplicate"] = JToken.FromObject(deduplicate);
                }
                if (!string.IsNullOrEmpty(deduplicateField))
                {
                    bodyObject["deduplicate.field"] = deduplicateField;
                }
                if (similar != null)
                {
                    bodyObject["similar"] = JToken.FromObject(similar);
                }
                if (!string.IsNullOrEmpty(similarDocumentIds))
                {
                    bodyObject["similar.document_ids"] = similarDocumentIds;
                }
                if (!string.IsNullOrEmpty(similarFields))
                {
                    bodyObject["similar.fields"] = similarFields;
                }
                if (!string.IsNullOrEmpty(bias))
                {
                    bodyObject["bias"] = bias;
                }
                if (spellingSuggestions != null)
                {
                    bodyObject["spelling_suggestions"] = JToken.FromObject(spellingSuggestions);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "Query"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<QueryResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<QueryResponse>();
                }
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
        /// when ingesting documents and performing relevance training. See the [Discovery
        /// documentation](https://cloud.ibm.com/docs/discovery?topic=discovery-query-concepts#query-concepts) for more
        /// details on the query language.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing
        /// training data and natural language understanding. (optional)</param>
        /// <param name="passages">A passages query that returns the most relevant passages from the results.
        /// (optional)</param>
        /// <param name="aggregation">An aggregation search that returns an exact answer by combining query search with
        /// filters. Useful for applications to build lists, tables, and time series. For a full list of possible
        /// aggregations, see the Query reference. (optional)</param>
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10000**. (optional)</param>
        /// <param name="_return">A comma-separated list of the portion of the document hierarchy to return.
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
        public DetailedResponse<QueryNoticesResponse> QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `QueryNotices`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `QueryNotices`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<QueryNoticesResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/notices");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    restRequest.WithArgument("filter", filter);
                }
                if (!string.IsNullOrEmpty(query))
                {
                    restRequest.WithArgument("query", query);
                }
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                {
                    restRequest.WithArgument("natural_language_query", naturalLanguageQuery);
                }
                if (passages != null)
                {
                    restRequest.WithArgument("passages", passages);
                }
                if (!string.IsNullOrEmpty(aggregation))
                {
                    restRequest.WithArgument("aggregation", aggregation);
                }
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }
                if (_return != null && _return.Count > 0)
                {
                    restRequest.WithArgument("return", string.Join(",", _return.ToArray()));
                }
                if (offset != null)
                {
                    restRequest.WithArgument("offset", offset);
                }
                if (sort != null && sort.Count > 0)
                {
                    restRequest.WithArgument("sort", string.Join(",", sort.ToArray()));
                }
                if (highlight != null)
                {
                    restRequest.WithArgument("highlight", highlight);
                }
                if (passagesFields != null && passagesFields.Count > 0)
                {
                    restRequest.WithArgument("passages.fields", string.Join(",", passagesFields.ToArray()));
                }
                if (passagesCount != null)
                {
                    restRequest.WithArgument("passages.count", passagesCount);
                }
                if (passagesCharacters != null)
                {
                    restRequest.WithArgument("passages.characters", passagesCharacters);
                }
                if (!string.IsNullOrEmpty(deduplicateField))
                {
                    restRequest.WithArgument("deduplicate.field", deduplicateField);
                }
                if (similar != null)
                {
                    restRequest.WithArgument("similar", similar);
                }
                if (similarDocumentIds != null && similarDocumentIds.Count > 0)
                {
                    restRequest.WithArgument("similar.document_ids", string.Join(",", similarDocumentIds.ToArray()));
                }
                if (similarFields != null && similarFields.Count > 0)
                {
                    restRequest.WithArgument("similar.fields", string.Join(",", similarFields.ToArray()));
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "QueryNotices"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<QueryNoticesResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<QueryNoticesResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Query multiple collections.
        ///
        /// By using this method, you can construct long queries that search multiple collection. For details, see the
        /// [Discovery
        /// documentation](https://cloud.ibm.com/docs/discovery?topic=discovery-query-concepts#query-concepts).
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.</param>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. Use a query search when you want to find the most
        /// relevant search results. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing
        /// training data and natural language understanding. (optional)</param>
        /// <param name="passages">A passages query that returns the most relevant passages from the results.
        /// (optional)</param>
        /// <param name="aggregation">An aggregation search that returns an exact answer by combining query search with
        /// filters. Useful for applications to build lists, tables, and time series. For a full list of possible
        /// aggregations, see the Query reference. (optional)</param>
        /// <param name="count">Number of results to return. (optional)</param>
        /// <param name="_return">A comma-separated list of the portion of the document hierarchy to return.
        /// (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. This parameter cannot be used in the same query as the **bias**
        /// parameter. (optional)</param>
        /// <param name="highlight">When true, a highlight field is returned for each result which contains the fields
        /// which match the query with `<em></em>` tags around the matching query terms. (optional, default to
        /// false)</param>
        /// <param name="passagesFields">A comma-separated list of fields that passages are drawn from. If this
        /// parameter not specified, then all top-level fields are included. (optional)</param>
        /// <param name="passagesCount">The maximum number of passages to return. The search returns fewer passages if
        /// the requested total is not found. The default is `10`. The maximum is `100`. (optional)</param>
        /// <param name="passagesCharacters">The approximate number of characters that any one passage will have.
        /// (optional)</param>
        /// <param name="deduplicate">When `true`, and used with a Watson Discovery News collection, duplicate results
        /// (based on the contents of the **title** field) are removed. Duplicate comparison is limited to the current
        /// query only; **offset** is not considered. This parameter is currently Beta functionality. (optional, default
        /// to false)</param>
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
        /// <param name="bias">Field which the returned results will be biased against. The specified field must be
        /// either a **date** or **number** format. When a **date** type field is specified returned results are biased
        /// towards field values closer to the current date. When a **number** type field is specified, returned results
        /// are biased towards higher field values. This parameter cannot be used in the same query as the **sort**
        /// parameter. (optional)</param>
        /// <param name="xWatsonLoggingOptOut">If `true`, queries are not stored in the Discovery **Logs** endpoint.
        /// (optional, default to false)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        public DetailedResponse<QueryResponse> FederatedQuery(string environmentId, string collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, string _return = null, long? offset = null, string sort = null, bool? highlight = null, string passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, string similarDocumentIds = null, string similarFields = null, string bias = null, bool? xWatsonLoggingOptOut = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `FederatedQuery`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionIds))
            {
                throw new ArgumentNullException("`collectionIds` is required for `FederatedQuery`");
            }
            DetailedResponse<QueryResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/query");

                restRequest.WithHeader("Accept", "application/json");

                if (xWatsonLoggingOptOut != null)
                {
                    restRequest.WithHeader("X-Watson-Logging-Opt-Out", (bool)xWatsonLoggingOptOut ? "true" : "false");
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(collectionIds))
                {
                    bodyObject["collection_ids"] = collectionIds;
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    bodyObject["filter"] = filter;
                }
                if (!string.IsNullOrEmpty(query))
                {
                    bodyObject["query"] = query;
                }
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                {
                    bodyObject["natural_language_query"] = naturalLanguageQuery;
                }
                if (passages != null)
                {
                    bodyObject["passages"] = JToken.FromObject(passages);
                }
                if (!string.IsNullOrEmpty(aggregation))
                {
                    bodyObject["aggregation"] = aggregation;
                }
                if (count != null)
                {
                    bodyObject["count"] = JToken.FromObject(count);
                }
                if (!string.IsNullOrEmpty(_return))
                {
                    bodyObject["return"] = _return;
                }
                if (offset != null)
                {
                    bodyObject["offset"] = JToken.FromObject(offset);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    bodyObject["sort"] = sort;
                }
                if (highlight != null)
                {
                    bodyObject["highlight"] = JToken.FromObject(highlight);
                }
                if (!string.IsNullOrEmpty(passagesFields))
                {
                    bodyObject["passages.fields"] = passagesFields;
                }
                if (passagesCount != null)
                {
                    bodyObject["passages.count"] = JToken.FromObject(passagesCount);
                }
                if (passagesCharacters != null)
                {
                    bodyObject["passages.characters"] = JToken.FromObject(passagesCharacters);
                }
                if (deduplicate != null)
                {
                    bodyObject["deduplicate"] = JToken.FromObject(deduplicate);
                }
                if (!string.IsNullOrEmpty(deduplicateField))
                {
                    bodyObject["deduplicate.field"] = deduplicateField;
                }
                if (similar != null)
                {
                    bodyObject["similar"] = JToken.FromObject(similar);
                }
                if (!string.IsNullOrEmpty(similarDocumentIds))
                {
                    bodyObject["similar.document_ids"] = similarDocumentIds;
                }
                if (!string.IsNullOrEmpty(similarFields))
                {
                    bodyObject["similar.fields"] = similarFields;
                }
                if (!string.IsNullOrEmpty(bias))
                {
                    bodyObject["bias"] = bias;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "FederatedQuery"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<QueryResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<QueryResponse>();
                }
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
        /// when ingesting documents and performing relevance training. See the [Discovery
        /// documentation](https://cloud.ibm.com/docs/discovery?topic=discovery-query-concepts#query-concepts) for more
        /// details on the query language.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.</param>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing
        /// training data and natural language understanding. (optional)</param>
        /// <param name="aggregation">An aggregation search that returns an exact answer by combining query search with
        /// filters. Useful for applications to build lists, tables, and time series. For a full list of possible
        /// aggregations, see the Query reference. (optional)</param>
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10000**. (optional)</param>
        /// <param name="_return">A comma-separated list of the portion of the document hierarchy to return.
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
        public DetailedResponse<QueryNoticesResponse> FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null, bool? similar = null, List<string> similarDocumentIds = null, List<string> similarFields = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `FederatedQueryNotices`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (collectionIds == null)
            {
                throw new ArgumentNullException("`collectionIds` is required for `FederatedQueryNotices`");
            }
            DetailedResponse<QueryNoticesResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/notices");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (collectionIds != null && collectionIds.Count > 0)
                {
                    restRequest.WithArgument("collection_ids", string.Join(",", collectionIds.ToArray()));
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    restRequest.WithArgument("filter", filter);
                }
                if (!string.IsNullOrEmpty(query))
                {
                    restRequest.WithArgument("query", query);
                }
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                {
                    restRequest.WithArgument("natural_language_query", naturalLanguageQuery);
                }
                if (!string.IsNullOrEmpty(aggregation))
                {
                    restRequest.WithArgument("aggregation", aggregation);
                }
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }
                if (_return != null && _return.Count > 0)
                {
                    restRequest.WithArgument("return", string.Join(",", _return.ToArray()));
                }
                if (offset != null)
                {
                    restRequest.WithArgument("offset", offset);
                }
                if (sort != null && sort.Count > 0)
                {
                    restRequest.WithArgument("sort", string.Join(",", sort.ToArray()));
                }
                if (highlight != null)
                {
                    restRequest.WithArgument("highlight", highlight);
                }
                if (!string.IsNullOrEmpty(deduplicateField))
                {
                    restRequest.WithArgument("deduplicate.field", deduplicateField);
                }
                if (similar != null)
                {
                    restRequest.WithArgument("similar", similar);
                }
                if (similarDocumentIds != null && similarDocumentIds.Count > 0)
                {
                    restRequest.WithArgument("similar.document_ids", string.Join(",", similarDocumentIds.ToArray()));
                }
                if (similarFields != null && similarFields.Count > 0)
                {
                    restRequest.WithArgument("similar.fields", string.Join(",", similarFields.ToArray()));
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "FederatedQueryNotices"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<QueryNoticesResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<QueryNoticesResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get Autocomplete Suggestions.
        ///
        /// Returns completion query suggestions for the specified prefix.  /n/n **Important:** this method is only
        /// valid when using the Cloud Pak version of Discovery.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="prefix">The prefix to use for autocompletion. For example, the prefix `Ho` could autocomplete
        /// to `Hot`, `Housing`, or `How do I upgrade`. Possible completions are.</param>
        /// <param name="field">The field in the result documents that autocompletion suggestions are identified from.
        /// (optional)</param>
        /// <param name="count">The number of autocompletion suggestions to return. (optional)</param>
        /// <returns><see cref="Completions" />Completions</returns>
        public DetailedResponse<Completions> GetAutocompletion(string environmentId, string collectionId, string prefix, string field = null, long? count = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetAutocompletion`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetAutocompletion`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentNullException("`prefix` is required for `GetAutocompletion`");
            }
            DetailedResponse<Completions> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/autocompletion");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(prefix))
                {
                    restRequest.WithArgument("prefix", prefix);
                }
                if (!string.IsNullOrEmpty(field))
                {
                    restRequest.WithArgument("field", field);
                }
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetAutocompletion"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Completions>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Completions>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListTrainingData`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `ListTrainingData`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<TrainingDataSet> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListTrainingData"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingDataSet>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingDataSet>();
                }
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
        /// <param name="naturalLanguageQuery">The natural text query for the new training query. (optional)</param>
        /// <param name="filter">The filter used on the collection before the **natural_language_query** is applied.
        /// (optional)</param>
        /// <param name="examples">Array of training examples. (optional)</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> AddTrainingData(string environmentId, string collectionId, string naturalLanguageQuery = null, string filter = null, List<TrainingExample> examples = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `AddTrainingData`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `AddTrainingData`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(naturalLanguageQuery))
                {
                    bodyObject["natural_language_query"] = naturalLanguageQuery;
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    bodyObject["filter"] = filter;
                }
                if (examples != null && examples.Count > 0)
                {
                    bodyObject["examples"] = JToken.FromObject(examples);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "AddTrainingData"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingQuery>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingQuery>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteAllTrainingData`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteAllTrainingData`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteAllTrainingData"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetTrainingData`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetTrainingData`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `GetTrainingData`");
            }
            else
            {
                queryId = Uri.EscapeDataString(queryId);
            }
            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetTrainingData"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingQuery>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingQuery>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteTrainingData`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteTrainingData`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `DeleteTrainingData`");
            }
            else
            {
                queryId = Uri.EscapeDataString(queryId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteTrainingData"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListTrainingExamples`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `ListTrainingExamples`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `ListTrainingExamples`");
            }
            else
            {
                queryId = Uri.EscapeDataString(queryId);
            }
            DetailedResponse<TrainingExampleList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListTrainingExamples"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingExampleList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingExampleList>();
                }
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
        /// <param name="documentId">The document ID associated with this training example. (optional)</param>
        /// <param name="crossReference">The cross reference associated with this training example. (optional)</param>
        /// <param name="relevance">The relevance of the training example. (optional)</param>
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        public DetailedResponse<TrainingExample> CreateTrainingExample(string environmentId, string collectionId, string queryId, string documentId = null, string crossReference = null, long? relevance = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `CreateTrainingExample`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `CreateTrainingExample`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `CreateTrainingExample`");
            }
            else
            {
                queryId = Uri.EscapeDataString(queryId);
            }
            DetailedResponse<TrainingExample> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(documentId))
                {
                    bodyObject["document_id"] = documentId;
                }
                if (!string.IsNullOrEmpty(crossReference))
                {
                    bodyObject["cross_reference"] = crossReference;
                }
                if (relevance != null)
                {
                    bodyObject["relevance"] = JToken.FromObject(relevance);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateTrainingExample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingExample>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingExample>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteTrainingExample`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteTrainingExample`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `DeleteTrainingExample`");
            }
            else
            {
                queryId = Uri.EscapeDataString(queryId);
            }
            if (string.IsNullOrEmpty(exampleId))
            {
                throw new ArgumentNullException("`exampleId` is required for `DeleteTrainingExample`");
            }
            else
            {
                exampleId = Uri.EscapeDataString(exampleId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteTrainingExample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
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
        /// <param name="crossReference">The example to add. (optional)</param>
        /// <param name="relevance">The relevance value for this example. (optional)</param>
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        public DetailedResponse<TrainingExample> UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, string crossReference = null, long? relevance = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `UpdateTrainingExample`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `UpdateTrainingExample`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `UpdateTrainingExample`");
            }
            else
            {
                queryId = Uri.EscapeDataString(queryId);
            }
            if (string.IsNullOrEmpty(exampleId))
            {
                throw new ArgumentNullException("`exampleId` is required for `UpdateTrainingExample`");
            }
            else
            {
                exampleId = Uri.EscapeDataString(exampleId);
            }
            DetailedResponse<TrainingExample> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(crossReference))
                {
                    bodyObject["cross_reference"] = crossReference;
                }
                if (relevance != null)
                {
                    bodyObject["relevance"] = JToken.FromObject(relevance);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "UpdateTrainingExample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingExample>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingExample>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetTrainingExample`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetTrainingExample`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `GetTrainingExample`");
            }
            else
            {
                queryId = Uri.EscapeDataString(queryId);
            }
            if (string.IsNullOrEmpty(exampleId))
            {
                throw new ArgumentNullException("`exampleId` is required for `GetTrainingExample`");
            }
            else
            {
                exampleId = Uri.EscapeDataString(exampleId);
            }
            DetailedResponse<TrainingExample> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/collections/{collectionId}/training_data/{queryId}/examples/{exampleId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetTrainingExample"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingExample>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingExample>();
                }
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
        /// security](https://cloud.ibm.com/docs/discovery?topic=discovery-information-security#information-security).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteUserData(string customerId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentNullException("`customerId` is required for `DeleteUserData`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/user_data");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(customerId))
                {
                    restRequest.WithArgument("customer_id", customerId);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteUserData"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
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
        /// you can record which documents in the results set were "clicked" by a user and when that click occurred.
        /// </summary>
        /// <param name="type">The event type to be created.</param>
        /// <param name="data">Query event data object.</param>
        /// <returns><see cref="CreateEventResponse" />CreateEventResponse</returns>
        public DetailedResponse<CreateEventResponse> CreateEvent(string type, EventData data)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException("`type` is required for `CreateEvent`");
            }
            if (data == null)
            {
                throw new ArgumentNullException("`data` is required for `CreateEvent`");
            }
            DetailedResponse<CreateEventResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/events");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(type))
                {
                    bodyObject["type"] = type;
                }
                if (data != null)
                {
                    bodyObject["data"] = JToken.FromObject(data);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateEvent"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CreateEventResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CreateEventResponse>();
                }
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
        /// text, but with the most relevant documents listed first. (optional)</param>
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<LogQueryResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/logs");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    restRequest.WithArgument("filter", filter);
                }
                if (!string.IsNullOrEmpty(query))
                {
                    restRequest.WithArgument("query", query);
                }
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }
                if (offset != null)
                {
                    restRequest.WithArgument("offset", offset);
                }
                if (sort != null && sort.Count > 0)
                {
                    restRequest.WithArgument("sort", string.Join(",", sort.ToArray()));
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "QueryLog"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<LogQueryResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<LogQueryResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<MetricResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (startTime != null)
                {
                    restRequest.WithArgument("start_time", startTime?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }
                if (endTime != null)
                {
                    restRequest.WithArgument("end_time", endTime?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }
                if (!string.IsNullOrEmpty(resultType))
                {
                    restRequest.WithArgument("result_type", resultType);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetMetricsQuery"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<MetricResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<MetricResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetMetricsQuery.
        /// </summary>
        public class GetMetricsQueryEnums
        {
            /// <summary>
            /// The type of result to consider when calculating the metric.
            /// </summary>
            public class ResultTypeValue
            {
                /// <summary>
                /// Constant DOCUMENT for document
                /// </summary>
                public const string DOCUMENT = "document";
                
            }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<MetricResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries_with_event");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (startTime != null)
                {
                    restRequest.WithArgument("start_time", startTime?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }
                if (endTime != null)
                {
                    restRequest.WithArgument("end_time", endTime?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }
                if (!string.IsNullOrEmpty(resultType))
                {
                    restRequest.WithArgument("result_type", resultType);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetMetricsQueryEvent"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<MetricResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<MetricResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetMetricsQueryEvent.
        /// </summary>
        public class GetMetricsQueryEventEnums
        {
            /// <summary>
            /// The type of result to consider when calculating the metric.
            /// </summary>
            public class ResultTypeValue
            {
                /// <summary>
                /// Constant DOCUMENT for document
                /// </summary>
                public const string DOCUMENT = "document";
                
            }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<MetricResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/number_of_queries_with_no_search_results");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (startTime != null)
                {
                    restRequest.WithArgument("start_time", startTime?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }
                if (endTime != null)
                {
                    restRequest.WithArgument("end_time", endTime?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }
                if (!string.IsNullOrEmpty(resultType))
                {
                    restRequest.WithArgument("result_type", resultType);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetMetricsQueryNoResults"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<MetricResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<MetricResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetMetricsQueryNoResults.
        /// </summary>
        public class GetMetricsQueryNoResultsEnums
        {
            /// <summary>
            /// The type of result to consider when calculating the metric.
            /// </summary>
            public class ResultTypeValue
            {
                /// <summary>
                /// Constant DOCUMENT for document
                /// </summary>
                public const string DOCUMENT = "document";
                
            }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<MetricResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/event_rate");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (startTime != null)
                {
                    restRequest.WithArgument("start_time", startTime?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }
                if (endTime != null)
                {
                    restRequest.WithArgument("end_time", endTime?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }
                if (!string.IsNullOrEmpty(resultType))
                {
                    restRequest.WithArgument("result_type", resultType);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetMetricsEventRate"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<MetricResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<MetricResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetMetricsEventRate.
        /// </summary>
        public class GetMetricsEventRateEnums
        {
            /// <summary>
            /// The type of result to consider when calculating the metric.
            /// </summary>
            public class ResultTypeValue
            {
                /// <summary>
                /// Constant DOCUMENT for document
                /// </summary>
                public const string DOCUMENT = "document";
                
            }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<MetricTokenResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/metrics/top_query_tokens_with_event_rate");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetMetricsQueryTokenEvent"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<MetricTokenResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<MetricTokenResponse>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListCredentials`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<CredentialsList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListCredentials"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CredentialsList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CredentialsList>();
                }
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
        /// <param name="sourceType">The source that this credentials object connects to.
        /// -  `box` indicates the credentials are used to connect an instance of Enterprise Box.
        /// -  `salesforce` indicates the credentials are used to connect to Salesforce.
        /// -  `sharepoint` indicates the credentials are used to connect to Microsoft SharePoint Online.
        /// -  `web_crawl` indicates the credentials are used to perform a web crawl.
        /// =  `cloud_object_storage` indicates the credentials are used to connect to an IBM Cloud Object Store.
        /// (optional)</param>
        /// <param name="credentialDetails">Object containing details of the stored credentials.
        ///
        /// Obtain credentials for your source from the administrator of the source. (optional)</param>
        /// <param name="status">The current status of this set of credentials. `connected` indicates that the
        /// credentials are available to use with the source configuration of a collection. `invalid` refers to the
        /// credentials (for example, the password provided has expired) and must be corrected before they can be used
        /// with a collection. (optional)</param>
        /// <returns><see cref="Credentials" />Credentials</returns>
        public DetailedResponse<Credentials> CreateCredentials(string environmentId, string sourceType = null, CredentialDetails credentialDetails = null, string status = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `CreateCredentials`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<Credentials> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(sourceType))
                {
                    bodyObject["source_type"] = sourceType;
                }
                if (credentialDetails != null)
                {
                    bodyObject["credential_details"] = JToken.FromObject(credentialDetails);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    bodyObject["status"] = status;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateCredentials"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Credentials>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Credentials>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetCredentials`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(credentialId))
            {
                throw new ArgumentNullException("`credentialId` is required for `GetCredentials`");
            }
            else
            {
                credentialId = Uri.EscapeDataString(credentialId);
            }
            DetailedResponse<Credentials> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetCredentials"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Credentials>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Credentials>();
                }
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
        /// <param name="sourceType">The source that this credentials object connects to.
        /// -  `box` indicates the credentials are used to connect an instance of Enterprise Box.
        /// -  `salesforce` indicates the credentials are used to connect to Salesforce.
        /// -  `sharepoint` indicates the credentials are used to connect to Microsoft SharePoint Online.
        /// -  `web_crawl` indicates the credentials are used to perform a web crawl.
        /// =  `cloud_object_storage` indicates the credentials are used to connect to an IBM Cloud Object Store.
        /// (optional)</param>
        /// <param name="credentialDetails">Object containing details of the stored credentials.
        ///
        /// Obtain credentials for your source from the administrator of the source. (optional)</param>
        /// <param name="status">The current status of this set of credentials. `connected` indicates that the
        /// credentials are available to use with the source configuration of a collection. `invalid` refers to the
        /// credentials (for example, the password provided has expired) and must be corrected before they can be used
        /// with a collection. (optional)</param>
        /// <returns><see cref="Credentials" />Credentials</returns>
        public DetailedResponse<Credentials> UpdateCredentials(string environmentId, string credentialId, string sourceType = null, CredentialDetails credentialDetails = null, string status = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `UpdateCredentials`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(credentialId))
            {
                throw new ArgumentNullException("`credentialId` is required for `UpdateCredentials`");
            }
            else
            {
                credentialId = Uri.EscapeDataString(credentialId);
            }
            DetailedResponse<Credentials> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(sourceType))
                {
                    bodyObject["source_type"] = sourceType;
                }
                if (credentialDetails != null)
                {
                    bodyObject["credential_details"] = JToken.FromObject(credentialDetails);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    bodyObject["status"] = status;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "UpdateCredentials"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Credentials>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Credentials>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteCredentials`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(credentialId))
            {
                throw new ArgumentNullException("`credentialId` is required for `DeleteCredentials`");
            }
            else
            {
                credentialId = Uri.EscapeDataString(credentialId);
            }
            DetailedResponse<DeleteCredentials> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/credentials/{credentialId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteCredentials"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteCredentials>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteCredentials>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `ListGateways`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<GatewayList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/gateways");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "ListGateways"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<GatewayList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<GatewayList>();
                }
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
        /// <param name="name">User-defined name. (optional)</param>
        /// <returns><see cref="Gateway" />Gateway</returns>
        public DetailedResponse<Gateway> CreateGateway(string environmentId, string name = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `CreateGateway`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            DetailedResponse<Gateway> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/environments/{environmentId}/gateways");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "CreateGateway"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Gateway>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Gateway>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `GetGateway`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(gatewayId))
            {
                throw new ArgumentNullException("`gatewayId` is required for `GetGateway`");
            }
            else
            {
                gatewayId = Uri.EscapeDataString(gatewayId);
            }
            DetailedResponse<Gateway> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/environments/{environmentId}/gateways/{gatewayId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "GetGateway"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Gateway>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Gateway>();
                }
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
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(environmentId))
            {
                throw new ArgumentNullException("`environmentId` is required for `DeleteGateway`");
            }
            else
            {
                environmentId = Uri.EscapeDataString(environmentId);
            }
            if (string.IsNullOrEmpty(gatewayId))
            {
                throw new ArgumentNullException("`gatewayId` is required for `DeleteGateway`");
            }
            else
            {
                gatewayId = Uri.EscapeDataString(gatewayId);
            }
            DetailedResponse<GatewayDelete> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/environments/{environmentId}/gateways/{gatewayId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v1", "DeleteGateway"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<GatewayDelete>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<GatewayDelete>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
