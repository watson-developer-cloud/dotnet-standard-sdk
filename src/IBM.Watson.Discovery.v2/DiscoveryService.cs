/**
* (C) Copyright IBM Corp. 2019, 2023.
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
* IBM OpenAPI SDK Code Generator Version: 3.64.1-cee95189-20230124-211647
*/
 
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.Discovery.v2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.Discovery.v2
{
    public partial class DiscoveryService : IBMService, IDiscoveryService
    {
        const string defaultServiceName = "discovery";
        private const string defaultServiceUrl = "https://api.us-south.discovery.watson.cloud.ibm.com";
        public string Version { get; set; }

        public DiscoveryService(string version, WebProxy webProxy = null) : this(version, defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName), webProxy) { }
        public DiscoveryService(string version, IAuthenticator authenticator, WebProxy webProxy = null) : this(version, defaultServiceName, authenticator, webProxy) {}
        public DiscoveryService(string version, string serviceName, WebProxy webProxy = null) : this(version, serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName), webProxy) { }
        public DiscoveryService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public DiscoveryService(string version, string serviceName, IAuthenticator authenticator, WebProxy webProxy = null) : base(serviceName, authenticator, webProxy)
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
        /// List projects.
        ///
        /// Lists existing projects for this instance.
        /// </summary>
        /// <returns><see cref="ListProjectsResponse" />ListProjectsResponse</returns>
        public DetailedResponse<ListProjectsResponse> ListProjects()
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ListProjectsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListProjects"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListProjectsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListProjectsResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create a project.
        ///
        /// Create a new project for this instance.
        /// </summary>
        /// <param name="name">The human readable name of this project.</param>
        /// <param name="type">The type of project.
        ///
        /// The `content_intelligence` type is a *Document Retrieval for Contracts* project and the `other` type is a
        /// *Custom* project.
        ///
        /// The `content_mining` and `content_intelligence` types are available with Premium plan managed deployments
        /// and installed deployments only.</param>
        /// <param name="defaultQueryParameters">Default query parameters for this project. (optional)</param>
        /// <returns><see cref="ProjectDetails" />ProjectDetails</returns>
        public DetailedResponse<ProjectDetails> CreateProject(string name, string type, DefaultQueryParams defaultQueryParameters = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateProject`");
            }
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException("`type` is required for `CreateProject`");
            }
            DetailedResponse<ProjectDetails> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects");

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
                if (!string.IsNullOrEmpty(type))
                {
                    bodyObject["type"] = type;
                }
                if (defaultQueryParameters != null)
                {
                    bodyObject["default_query_parameters"] = JToken.FromObject(defaultQueryParameters);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "CreateProject"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ProjectDetails>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ProjectDetails>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get project.
        ///
        /// Get details on the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <returns><see cref="ProjectDetails" />ProjectDetails</returns>
        public DetailedResponse<ProjectDetails> GetProject(string projectId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetProject`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<ProjectDetails> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetProject"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ProjectDetails>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ProjectDetails>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a project.
        ///
        /// Update the specified project's name.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="name">The new name to give this project. (optional)</param>
        /// <returns><see cref="ProjectDetails" />ProjectDetails</returns>
        public DetailedResponse<ProjectDetails> UpdateProject(string projectId, string name = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `UpdateProject`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<ProjectDetails> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "UpdateProject"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ProjectDetails>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ProjectDetails>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a project.
        ///
        /// Deletes the specified project.
        ///
        /// **Important:** Deleting a project deletes everything that is part of the specified project, including all
        /// collections.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteProject(string projectId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteProject`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteProject"));
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
        /// List fields.
        ///
        /// Gets a list of the unique fields (and their types) stored in the specified collections.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionIds">Comma separated list of the collection IDs. If this parameter is not specified,
        /// all collections in the project are used. (optional)</param>
        /// <returns><see cref="ListFieldsResponse" />ListFieldsResponse</returns>
        public DetailedResponse<ListFieldsResponse> ListFields(string projectId, List<string> collectionIds = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListFields`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<ListFieldsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/fields");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (collectionIds != null && collectionIds.Count > 0)
                {
                    restRequest.WithArgument("collection_ids", string.Join(",", collectionIds.ToArray()));
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListFields"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListFieldsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListFieldsResponse>();
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
        /// Lists existing collections for the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <returns><see cref="ListCollectionsResponse" />ListCollectionsResponse</returns>
        public DetailedResponse<ListCollectionsResponse> ListCollections(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListCollections`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ListCollectionsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/collections");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListCollections"));
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
        /// Create a collection.
        ///
        /// Create a new collection in the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="name">The name of the collection.</param>
        /// <param name="description">A description of the collection. (optional)</param>
        /// <param name="language">The language of the collection. For a list of supported languages, see the [product
        /// documentation](/docs/discovery-data?topic=discovery-data-language-support). (optional, default to
        /// en)</param>
        /// <param name="enrichments">An array of enrichments that are applied to this collection. To get a list of
        /// enrichments that are available for a project, use the [List enrichments](#listenrichments) method.
        ///
        /// If no enrichments are specified when the collection is created, the default enrichments for the project type
        /// are applied. For more information about project default settings, see the [product
        /// documentation](/docs/discovery-data?topic=discovery-data-project-defaults). (optional)</param>
        /// <returns><see cref="CollectionDetails" />CollectionDetails</returns>
        public DetailedResponse<CollectionDetails> CreateCollection(string projectId, string name, string description = null, string language = null, List<CollectionEnrichment> enrichments = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `CreateCollection`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateCollection`");
            }
            DetailedResponse<CollectionDetails> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/collections");

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
                if (!string.IsNullOrEmpty(language))
                {
                    bodyObject["language"] = language;
                }
                if (enrichments != null && enrichments.Count > 0)
                {
                    bodyObject["enrichments"] = JToken.FromObject(enrichments);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "CreateCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CollectionDetails>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CollectionDetails>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get collection.
        ///
        /// Get details about the specified collection.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="CollectionDetails" />CollectionDetails</returns>
        public DetailedResponse<CollectionDetails> GetCollection(string projectId, string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetCollection`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetCollection`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<CollectionDetails> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CollectionDetails>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CollectionDetails>();
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
        ///
        /// Updates the specified collection's name, description, and enrichments.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="name">The new name of the collection. (optional)</param>
        /// <param name="description">The new description of the collection. (optional)</param>
        /// <param name="enrichments">An array of enrichments that are applied to this collection. (optional)</param>
        /// <returns><see cref="CollectionDetails" />CollectionDetails</returns>
        public DetailedResponse<CollectionDetails> UpdateCollection(string projectId, string collectionId, string name = null, string description = null, List<CollectionEnrichment> enrichments = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `UpdateCollection`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `UpdateCollection`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<CollectionDetails> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}");

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
                if (enrichments != null && enrichments.Count > 0)
                {
                    bodyObject["enrichments"] = JToken.FromObject(enrichments);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "UpdateCollection"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CollectionDetails>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CollectionDetails>();
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
        ///
        /// Deletes the specified collection from the project. All documents stored in the specified collection and not
        /// shared is also deleted.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteCollection(string projectId, string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteCollection`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `DeleteCollection`");
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

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteCollection"));
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
        /// List documents.
        ///
        /// Lists the documents in the specified collection. The list includes only the document ID of each document and
        /// returns information for up to 10,000 documents.
        ///
        /// **Note**: This method is available only from Cloud Pak for Data version 4.0.9 and later installed instances
        /// and from Plus and Enterprise plan IBM Cloud-managed instances. It is not currently available from Premium
        /// plan instances.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="count">The maximum number of documents to return. Up to 1,000 documents are returned by
        /// default. The maximum number allowed is 10,000. (optional)</param>
        /// <param name="status">Filters the documents to include only documents with the specified ingestion status.
        /// The options include:
        ///
        /// * `available`: Ingestion is finished and the document is indexed.
        ///
        /// * `failed`: Ingestion is finished, but the document is not indexed because of an error.
        ///
        /// * `pending`: The document is uploaded, but the ingestion process is not started.
        ///
        /// * `processing`: Ingestion is in progress.
        ///
        /// You can specify one status value or add a comma-separated list of more than one status value. For example,
        /// `available,failed`. (optional)</param>
        /// <param name="hasNotices">If set to `true`, only documents that have notices, meaning documents for which
        /// warnings or errors were generated during the ingestion, are returned. If set to `false`, only documents that
        /// don't have notices are returned. If unspecified, no filter based on notices is applied.
        ///
        /// Notice details are not available in the result, but you can use the [Query collection
        /// notices](#querycollectionnotices) method to find details by adding the parameter
        /// `query=notices.document_id:{document-id}`. (optional)</param>
        /// <param name="isParent">If set to `true`, only parent documents, meaning documents that were split during the
        /// ingestion process and resulted in two or more child documents, are returned. If set to `false`, only child
        /// documents are returned. If unspecified, no filter based on the parent or child relationship is applied.
        ///
        /// CSV files, for example, are split into separate documents per line and JSON files are split into separate
        /// documents per object. (optional)</param>
        /// <param name="parentDocumentId">Filters the documents to include only child documents that were generated
        /// when the specified parent document was processed. (optional)</param>
        /// <param name="sha256">Filters the documents to include only documents with the specified SHA-256 hash. Format
        /// the hash as a hexadecimal string. (optional)</param>
        /// <returns><see cref="ListDocumentsResponse" />ListDocumentsResponse</returns>
        public DetailedResponse<ListDocumentsResponse> ListDocuments(string projectId, string collectionId, long? count = null, string status = null, bool? hasNotices = null, bool? isParent = null, string parentDocumentId = null, string sha256 = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListDocuments`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `ListDocuments`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<ListDocumentsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/documents");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    restRequest.WithArgument("status", status);
                }
                if (hasNotices != null)
                {
                    restRequest.WithArgument("has_notices", hasNotices);
                }
                if (isParent != null)
                {
                    restRequest.WithArgument("is_parent", isParent);
                }
                if (!string.IsNullOrEmpty(parentDocumentId))
                {
                    restRequest.WithArgument("parent_document_id", parentDocumentId);
                }
                if (!string.IsNullOrEmpty(sha256))
                {
                    restRequest.WithArgument("sha256", sha256);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListDocuments"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ListDocumentsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ListDocumentsResponse>();
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
        /// Returns immediately after the system has accepted the document for processing.
        ///
        /// Use this method to upload a file to the collection. You cannot use this method to crawl an external data
        /// source.
        ///
        ///  * For a list of supported file types, see the [product
        /// documentation](/docs/discovery-data?topic=discovery-data-collections#supportedfiletypes).
        ///
        ///  * You must provide document content, metadata, or both. If the request is missing both document content and
        /// metadata, it is rejected.
        ///
        ///   * You can set the **Content-Type** parameter on the **file** part to indicate the media type of the
        /// document. If the **Content-Type** parameter is missing or is one of the generic media types (for example,
        /// `application/octet-stream`), then the service attempts to automatically detect the document's media type.
        ///
        ///  *  If the document is uploaded to a collection that shares its data with another collection, the
        /// **X-Watson-Discovery-Force** header must be set to `true`.
        ///
        ///  * In curl requests only, you can assign an ID to a document that you add by appending the ID to the
        /// endpoint (`/v2/projects/{project_id}/collections/{collection_id}/documents/{document_id}`). If a document
        /// already exists with the specified ID, it is replaced.
        ///
        /// For more information about how certain file types and field names are handled when a file is added to a
        /// collection, see the [product
        /// documentation](/docs/discovery-data?topic=discovery-data-index-overview#field-name-limits).
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="file">When adding a document, the content of the document to ingest. For maximum supported file
        /// size limits, see [the
        /// documentation](/docs/discovery-data?topic=discovery-data-collections#collections-doc-limits).
        ///
        /// When analyzing a document, the content of the document to analyze but not ingest. Only the
        /// `application/json` content type is supported currently. For maximum supported file size limits, see [the
        /// product documentation](/docs/discovery-data?topic=discovery-data-analyzeapi#analyzeapi-limits).
        /// (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">Add information about the file that you want to include in the response.
        ///
        /// The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected.
        ///
        /// Example:
        ///
        ///  ```
        ///  {
        ///   "filename": "favorites2.json",
        ///   "file_type": "json"
        ///  }. (optional)</param>
        /// <param name="xWatsonDiscoveryForce">When `true`, the uploaded document is added to the collection even if
        /// the data for that collection is shared with other collections. (optional, default to false)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DetailedResponse<DocumentAccepted> AddDocument(string projectId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, bool? xWatsonDiscoveryForce = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `AddDocument`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
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

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/documents");

                restRequest.WithHeader("Accept", "application/json");

                if (xWatsonDiscoveryForce != null)
                {
                    restRequest.WithHeader("X-Watson-Discovery-Force", (bool)xWatsonDiscoveryForce ? "true" : "false");
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "AddDocument"));
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
        /// Get details about a specific document, whether the document is added by uploading a file or by crawling an
        /// external data source.
        ///
        /// **Note**: This method is available only from Cloud Pak for Data version 4.0.9 and later installed instances
        /// and from Plus and Enterprise plan IBM Cloud-managed instances. It is not currently available from Premium
        /// plan instances.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <returns><see cref="DocumentDetails" />DocumentDetails</returns>
        public DetailedResponse<DocumentDetails> GetDocument(string projectId, string collectionId, string documentId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetDocument`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetDocument`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `GetDocument`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }
            DetailedResponse<DocumentDetails> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentDetails>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentDetails>();
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
        /// Replace an existing document or add a document with a specified document ID. Starts ingesting a document
        /// with optional metadata.
        ///
        /// Use this method to upload a file to a collection. You cannot use this method to crawl an external data
        /// source.
        ///
        /// If the document is uploaded to a collection that shares its data with another collection, the
        /// **X-Watson-Discovery-Force** header must be set to `true`.
        ///
        /// **Notes:**
        ///
        ///  * Uploading a new document with this method automatically replaces any existing document stored with the
        /// same document ID.
        ///
        ///  * If an uploaded document is split into child documents during ingestion, all existing child documents are
        /// overwritten, even if the updated version of the document has fewer child documents.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="file">When adding a document, the content of the document to ingest. For maximum supported file
        /// size limits, see [the
        /// documentation](/docs/discovery-data?topic=discovery-data-collections#collections-doc-limits).
        ///
        /// When analyzing a document, the content of the document to analyze but not ingest. Only the
        /// `application/json` content type is supported currently. For maximum supported file size limits, see [the
        /// product documentation](/docs/discovery-data?topic=discovery-data-analyzeapi#analyzeapi-limits).
        /// (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">Add information about the file that you want to include in the response.
        ///
        /// The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected.
        ///
        /// Example:
        ///
        ///  ```
        ///  {
        ///   "filename": "favorites2.json",
        ///   "file_type": "json"
        ///  }. (optional)</param>
        /// <param name="xWatsonDiscoveryForce">When `true`, the uploaded document is added to the collection even if
        /// the data for that collection is shared with other collections. (optional, default to false)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DetailedResponse<DocumentAccepted> UpdateDocument(string projectId, string collectionId, string documentId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, bool? xWatsonDiscoveryForce = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `UpdateDocument`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
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

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithHeader("Accept", "application/json");

                if (xWatsonDiscoveryForce != null)
                {
                    restRequest.WithHeader("X-Watson-Discovery-Force", (bool)xWatsonDiscoveryForce ? "true" : "false");
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "UpdateDocument"));
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
        /// Deletes the document with the document ID that you specify from the collection. Removes uploaded documents
        /// from the collection permanently. If you delete a document that was added by crawling an external data
        /// source, the document will be added again with the next scheduled crawl of the data source. The delete
        /// function removes the document from the collection, not from the external data source.
        ///
        /// **Note:** Files such as CSV or JSON files generate subdocuments when they are added to a collection. If you
        /// delete a subdocument, and then repeat the action that created it, the deleted document is added back in to
        /// your collection. To remove subdocuments that are generated by an uploaded file, delete the original document
        /// instead. You can get the document ID of the original document from the `parent_document_id` of the
        /// subdocument result.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="xWatsonDiscoveryForce">When `true`, the uploaded document is added to the collection even if
        /// the data for that collection is shared with other collections. (optional, default to false)</param>
        /// <returns><see cref="DeleteDocumentResponse" />DeleteDocumentResponse</returns>
        public DetailedResponse<DeleteDocumentResponse> DeleteDocument(string projectId, string collectionId, string documentId, bool? xWatsonDiscoveryForce = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteDocument`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
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

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithHeader("Accept", "application/json");

                if (xWatsonDiscoveryForce != null)
                {
                    restRequest.WithHeader("X-Watson-Discovery-Force", (bool)xWatsonDiscoveryForce ? "true" : "false");
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteDocument"));
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
        /// Query a project.
        ///
        /// Search your data by submitting queries that are written in natural language or formatted in the Discovery
        /// Query Language. For more information, see the [Discovery
        /// documentation](/docs/discovery-data?topic=discovery-data-query-concepts). The default query parameters
        /// differ by project type. For more information about the project default settings, see the [Discovery
        /// documentation](/docs/discovery-data?topic=discovery-data-query-defaults). See [the Projects API
        /// documentation](#create-project) for details about how to set custom default query settings.
        ///
        /// The length of the UTF-8 encoding of the POST body cannot exceed 10,000 bytes, which is roughly equivalent to
        /// 10,000 characters in English.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.
        /// (optional)</param>
        /// <param name="filter">Searches for documents that match the Discovery Query Language criteria that is
        /// specified as input. Filter calls are cached and are faster than query calls because the results are not
        /// ordered by relevance. When used with the **aggregation**, **query**, or **natural_language_query**
        /// parameters, the **filter** parameter runs first. This parameter is useful for limiting results to those that
        /// contain specific metadata values. (optional)</param>
        /// <param name="query">A query search that is written in the Discovery Query Language and returns all matching
        /// documents in your data set with full enrichments and full text, and with the most relevant documents listed
        /// first. Use a query search when you want to find the most relevant search results. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by using
        /// training data and natural language understanding. (optional)</param>
        /// <param name="aggregation">An aggregation search that returns an exact answer by combining query search with
        /// filters. Useful for applications to build lists, tables, and time series. For more information about the
        /// supported types of aggregations, see the [Discovery
        /// documentation](/docs/discovery-data?topic=discovery-data-query-aggregations). (optional)</param>
        /// <param name="count">Number of results to return. (optional)</param>
        /// <param name="_return">A list of the fields in the document hierarchy to return. You can specify both
        /// root-level (`text`) and nested (`extracted_metadata.filename`) fields. If this parameter is an empty list,
        /// then all fields are returned. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When `true`, a highlight field is returned for each result that contains fields that
        /// match the query. The matching query terms are emphasized with surrounding `<em></em>` tags. This parameter
        /// is ignored if **passages.enabled** and **passages.per_document** are `true`, in which case passages are
        /// returned for each document instead of highlights. (optional)</param>
        /// <param name="spellingSuggestions">When `true` and the **natural_language_query** parameter is used, the
        /// **natural_language_query** parameter is spell checked. The most likely correction is returned in the
        /// **suggested_query** field of the response (if one exists). (optional)</param>
        /// <param name="tableResults">Configuration for table retrieval. (optional)</param>
        /// <param name="suggestedRefinements">Configuration for suggested refinements.
        ///
        /// **Note**: The **suggested_refinements** parameter that identified dynamic facets from the data is
        /// deprecated. (optional)</param>
        /// <param name="passages">Configuration for passage retrieval. (optional)</param>
        /// <param name="similar">Finds results from documents that are similar to documents of interest. Use this
        /// parameter to add a *More like these* function to your search. You can include this parameter with or without
        /// a **query**, **filter** or **natural_language_query** parameter. (optional)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        public DetailedResponse<QueryResponse> Query(string projectId, List<string> collectionIds = null, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, string sort = null, bool? highlight = null, bool? spellingSuggestions = null, QueryLargeTableResults tableResults = null, QueryLargeSuggestedRefinements suggestedRefinements = null, QueryLargePassages passages = null, QueryLargeSimilar similar = null)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `Query`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<QueryResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/query");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (collectionIds != null && collectionIds.Count > 0)
                {
                    bodyObject["collection_ids"] = JToken.FromObject(collectionIds);
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
                if (!string.IsNullOrEmpty(aggregation))
                {
                    bodyObject["aggregation"] = aggregation;
                }
                if (count != null)
                {
                    bodyObject["count"] = JToken.FromObject(count);
                }
                if (_return != null && _return.Count > 0)
                {
                    bodyObject["return"] = JToken.FromObject(_return);
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
                if (spellingSuggestions != null)
                {
                    bodyObject["spelling_suggestions"] = JToken.FromObject(spellingSuggestions);
                }
                if (tableResults != null)
                {
                    bodyObject["table_results"] = JToken.FromObject(tableResults);
                }
                if (suggestedRefinements != null)
                {
                    bodyObject["suggested_refinements"] = JToken.FromObject(suggestedRefinements);
                }
                if (passages != null)
                {
                    bodyObject["passages"] = JToken.FromObject(passages);
                }
                if (similar != null)
                {
                    bodyObject["similar"] = JToken.FromObject(similar);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "Query"));
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
        /// Get Autocomplete Suggestions.
        ///
        /// Returns completion query suggestions for the specified prefix.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="prefix">The prefix to use for autocompletion. For example, the prefix `Ho` could autocomplete
        /// to `hot`, `housing`, or `how`.</param>
        /// <param name="collectionIds">Comma separated list of the collection IDs. If this parameter is not specified,
        /// all collections in the project are used. (optional)</param>
        /// <param name="field">The field in the result documents that autocompletion suggestions are identified from.
        /// (optional)</param>
        /// <param name="count">The number of autocompletion suggestions to return. (optional)</param>
        /// <returns><see cref="Completions" />Completions</returns>
        public DetailedResponse<Completions> GetAutocompletion(string projectId, string prefix, List<string> collectionIds = null, string field = null, long? count = null)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetAutocompletion`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
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

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/autocompletion");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(prefix))
                {
                    restRequest.WithArgument("prefix", prefix);
                }
                if (collectionIds != null && collectionIds.Count > 0)
                {
                    restRequest.WithArgument("collection_ids", string.Join(",", collectionIds.ToArray()));
                }
                if (!string.IsNullOrEmpty(field))
                {
                    restRequest.WithArgument("field", field);
                }
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetAutocompletion"));
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
        /// Query collection notices.
        ///
        /// Finds collection-level notices (errors and warnings) that are generated when documents are ingested.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="filter">Searches for documents that match the Discovery Query Language criteria that is
        /// specified as input. Filter calls are cached and are faster than query calls because the results are not
        /// ordered by relevance. When used with the `aggregation`, `query`, or `natural_language_query` parameters, the
        /// `filter` parameter runs first. This parameter is useful for limiting results to those that contain specific
        /// metadata values. (optional)</param>
        /// <param name="query">A query search that is written in the Discovery Query Language and returns all matching
        /// documents in your data set with full enrichments and full text, and with the most relevant documents listed
        /// first. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by using
        /// training data and natural language understanding. (optional)</param>
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10,000**. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. The maximum for the
        /// **count** and **offset** values together in any one query is **10000**. (optional)</param>
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        public DetailedResponse<QueryNoticesResponse> QueryCollectionNotices(string projectId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, long? count = null, long? offset = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `QueryCollectionNotices`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `QueryCollectionNotices`");
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

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/notices");

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
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }
                if (offset != null)
                {
                    restRequest.WithArgument("offset", offset);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "QueryCollectionNotices"));
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
        /// Query project notices.
        ///
        /// Finds project-level notices (errors and warnings). Currently, project-level notices are generated by
        /// relevancy training.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="filter">Searches for documents that match the Discovery Query Language criteria that is
        /// specified as input. Filter calls are cached and are faster than query calls because the results are not
        /// ordered by relevance. When used with the `aggregation`, `query`, or `natural_language_query` parameters, the
        /// `filter` parameter runs first. This parameter is useful for limiting results to those that contain specific
        /// metadata values. (optional)</param>
        /// <param name="query">A query search that is written in the Discovery Query Language and returns all matching
        /// documents in your data set with full enrichments and full text, and with the most relevant documents listed
        /// first. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by using
        /// training data and natural language understanding. (optional)</param>
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10,000**. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. The maximum for the
        /// **count** and **offset** values together in any one query is **10000**. (optional)</param>
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        public DetailedResponse<QueryNoticesResponse> QueryNotices(string projectId, string filter = null, string query = null, string naturalLanguageQuery = null, long? count = null, long? offset = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `QueryNotices`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<QueryNoticesResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/notices");

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
                if (count != null)
                {
                    restRequest.WithArgument("count", count);
                }
                if (offset != null)
                {
                    restRequest.WithArgument("offset", offset);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "QueryNotices"));
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
        /// Get a custom stop words list.
        ///
        /// Returns the custom stop words list that is used by the collection. For information about the default stop
        /// words lists that are applied to queries, see [the product
        /// documentation](/docs/discovery-data?topic=discovery-data-stopwords).
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="StopWordList" />StopWordList</returns>
        public DetailedResponse<StopWordList> GetStopwordList(string projectId, string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetStopwordList`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `GetStopwordList`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<StopWordList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/stopwords");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetStopwordList"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<StopWordList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<StopWordList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create a custom stop words list.
        ///
        /// Adds a list of custom stop words. Stop words are words that you want the service to ignore when they occur
        /// in a query because they're not useful in distinguishing the semantic meaning of the query. The stop words
        /// list cannot contain more than 1 million characters.
        ///
        /// A default stop words list is used by all collections. The default list is applied both at indexing time and
        /// at query time. A custom stop words list that you add is used at query time only.
        ///
        /// The custom stop words list augments the default stop words list; you cannot remove stop words. For
        /// information about the default stop words lists per language, see [the product
        /// documentation](/docs/discovery-data?topic=discovery-data-stopwords).
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="stopwords">List of stop words. (optional)</param>
        /// <returns><see cref="StopWordList" />StopWordList</returns>
        public DetailedResponse<StopWordList> CreateStopwordList(string projectId, string collectionId, List<string> stopwords = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `CreateStopwordList`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `CreateStopwordList`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<StopWordList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/stopwords");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (stopwords != null && stopwords.Count > 0)
                {
                    bodyObject["stopwords"] = JToken.FromObject(stopwords);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "CreateStopwordList"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<StopWordList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<StopWordList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a custom stop words list.
        ///
        /// Deletes a custom stop words list to stop using it in queries against the collection. After a custom stop
        /// words list is deleted, the default stop words list is used.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteStopwordList(string projectId, string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteStopwordList`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
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

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/stopwords");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteStopwordList"));
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
        /// Get the expansion list.
        ///
        /// Returns the current expansion list for the specified collection. If an expansion list is not specified, an
        /// empty expansions array is returned.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="Expansions" />Expansions</returns>
        public DetailedResponse<Expansions> ListExpansions(string projectId, string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListExpansions`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
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

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/expansions");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListExpansions"));
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
        /// Create or update an expansion list.
        ///
        /// Creates or replaces the expansion list for this collection. An expansion list introduces alternative wording
        /// for key terms that are mentioned in your collection. By identifying synonyms or common misspellings, you
        /// expand the scope of a query beyond exact matches. The maximum number of expanded terms allowed per
        /// collection is 5,000.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="expansions">An array of query expansion definitions.
        ///
        ///  Each object in the **expansions** array represents a term or set of terms that will be expanded into other
        /// terms. Each expansion object can be configured as `bidirectional` or `unidirectional`.
        ///
        /// * **Bidirectional**: Each entry in the `expanded_terms` list expands to include all expanded terms. For
        /// example, a query for `ibm` expands to `ibm OR international business machines OR big blue`.
        ///
        /// * **Unidirectional**: The terms in `input_terms` in the query are replaced by the terms in `expanded_terms`.
        /// For example, a query for the often misused term `on premise` is converted to `on premises OR on-premises`
        /// and does not contain the original term. If you want an input term to be included in the query, then repeat
        /// the input term in the expanded terms list.</param>
        /// <returns><see cref="Expansions" />Expansions</returns>
        public DetailedResponse<Expansions> CreateExpansions(string projectId, string collectionId, List<Expansion> expansions)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `CreateExpansions`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
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

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/expansions");

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

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "CreateExpansions"));
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
        /// Removes the expansion information for this collection. To disable query expansion for a collection, delete
        /// the expansion list.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteExpansions(string projectId, string collectionId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteExpansions`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
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

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/expansions");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteExpansions"));
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
        /// List component settings.
        ///
        /// Returns default configuration settings for components.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <returns><see cref="ComponentSettingsResponse" />ComponentSettingsResponse</returns>
        public DetailedResponse<ComponentSettingsResponse> GetComponentSettings(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetComponentSettings`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            DetailedResponse<ComponentSettingsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/component_settings");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetComponentSettings"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<ComponentSettingsResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<ComponentSettingsResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List training queries.
        ///
        /// List the training queries for the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <returns><see cref="TrainingQuerySet" />TrainingQuerySet</returns>
        public DetailedResponse<TrainingQuerySet> ListTrainingQueries(string projectId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListTrainingQueries`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<TrainingQuerySet> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListTrainingQueries"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingQuerySet>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingQuerySet>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete training queries.
        ///
        /// Removes all training queries for the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteTrainingQueries(string projectId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteTrainingQueries`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteTrainingQueries"));
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
        /// Create training query.
        ///
        /// Add a query to the training data for this project. The query can contain a filter and natural language
        /// query.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="naturalLanguageQuery">The natural text query that is used as the training query.</param>
        /// <param name="examples">Array of training examples.</param>
        /// <param name="filter">The filter used on the collection before the **natural_language_query** is applied.
        /// (optional)</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> CreateTrainingQuery(string projectId, string naturalLanguageQuery, List<TrainingExample> examples, string filter = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `CreateTrainingQuery`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(naturalLanguageQuery))
            {
                throw new ArgumentNullException("`naturalLanguageQuery` is required for `CreateTrainingQuery`");
            }
            if (examples == null)
            {
                throw new ArgumentNullException("`examples` is required for `CreateTrainingQuery`");
            }
            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries");

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
                if (examples != null && examples.Count > 0)
                {
                    bodyObject["examples"] = JToken.FromObject(examples);
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    bodyObject["filter"] = filter;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "CreateTrainingQuery"));
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
        /// Get a training data query.
        ///
        /// Get details for a specific training data query, including the query string and all examples.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> GetTrainingQuery(string projectId, string queryId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetTrainingQuery`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `GetTrainingQuery`");
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

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries/{queryId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetTrainingQuery"));
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
        /// Update a training query.
        ///
        /// Updates an existing training query and it's examples.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="naturalLanguageQuery">The natural text query that is used as the training query.</param>
        /// <param name="examples">Array of training examples.</param>
        /// <param name="filter">The filter used on the collection before the **natural_language_query** is applied.
        /// (optional)</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> UpdateTrainingQuery(string projectId, string queryId, string naturalLanguageQuery, List<TrainingExample> examples, string filter = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `UpdateTrainingQuery`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `UpdateTrainingQuery`");
            }
            else
            {
                queryId = Uri.EscapeDataString(queryId);
            }
            if (string.IsNullOrEmpty(naturalLanguageQuery))
            {
                throw new ArgumentNullException("`naturalLanguageQuery` is required for `UpdateTrainingQuery`");
            }
            if (examples == null)
            {
                throw new ArgumentNullException("`examples` is required for `UpdateTrainingQuery`");
            }
            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries/{queryId}");

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
                if (examples != null && examples.Count > 0)
                {
                    bodyObject["examples"] = JToken.FromObject(examples);
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    bodyObject["filter"] = filter;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "UpdateTrainingQuery"));
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
        /// Removes details from a training data query, including the query string and all examples.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteTrainingQuery(string projectId, string queryId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteTrainingQuery`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(queryId))
            {
                throw new ArgumentNullException("`queryId` is required for `DeleteTrainingQuery`");
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

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries/{queryId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteTrainingQuery"));
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
        /// List enrichments.
        ///
        /// Lists the enrichments available to this project. The *Part of Speech* and *Sentiment of Phrases* enrichments
        /// might be listed, but are reserved for internal use only.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <returns><see cref="Enrichments" />Enrichments</returns>
        public DetailedResponse<Enrichments> ListEnrichments(string projectId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListEnrichments`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<Enrichments> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/enrichments");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListEnrichments"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Enrichments>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Enrichments>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create an enrichment.
        ///
        /// Create an enrichment for use with the specified project. To apply the enrichment to a collection in the
        /// project, use the [Collections API](/apidocs/discovery-data#createcollection).
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="enrichment">Information about a specific enrichment.</param>
        /// <param name="file">The enrichment file to upload. Expected file types per enrichment are as follows:
        ///
        /// * CSV for `dictionary`
        ///
        /// * PEAR for `uima_annotator` and `rule_based` (Explorer)
        ///
        /// * ZIP for `watson_knowledge_studio_model` and `rule_based` (Studio Advanced Rule Editor). (optional)</param>
        /// <returns><see cref="Enrichment" />Enrichment</returns>
        public DetailedResponse<Enrichment> CreateEnrichment(string projectId, CreateEnrichment enrichment, System.IO.MemoryStream file = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `CreateEnrichment`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (enrichment == null)
            {
                throw new ArgumentNullException("`enrichment` is required for `CreateEnrichment`");
            }
            DetailedResponse<Enrichment> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (enrichment != null)
                {
                    var enrichmentContent = new StringContent(JsonConvert.SerializeObject(enrichment), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                    enrichmentContent.Headers.ContentType = null;
                    formData.Add(enrichmentContent, "enrichment");
                }

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/enrichments");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "CreateEnrichment"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Enrichment>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Enrichment>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get enrichment.
        ///
        /// Get details about a specific enrichment.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="enrichmentId">The ID of the enrichment.</param>
        /// <returns><see cref="Enrichment" />Enrichment</returns>
        public DetailedResponse<Enrichment> GetEnrichment(string projectId, string enrichmentId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetEnrichment`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(enrichmentId))
            {
                throw new ArgumentNullException("`enrichmentId` is required for `GetEnrichment`");
            }
            else
            {
                enrichmentId = Uri.EscapeDataString(enrichmentId);
            }
            DetailedResponse<Enrichment> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/enrichments/{enrichmentId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetEnrichment"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Enrichment>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Enrichment>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update an enrichment.
        ///
        /// Updates an existing enrichment's name and description.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="enrichmentId">The ID of the enrichment.</param>
        /// <param name="name">A new name for the enrichment.</param>
        /// <param name="description">A new description for the enrichment. (optional)</param>
        /// <returns><see cref="Enrichment" />Enrichment</returns>
        public DetailedResponse<Enrichment> UpdateEnrichment(string projectId, string enrichmentId, string name, string description = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `UpdateEnrichment`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(enrichmentId))
            {
                throw new ArgumentNullException("`enrichmentId` is required for `UpdateEnrichment`");
            }
            else
            {
                enrichmentId = Uri.EscapeDataString(enrichmentId);
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `UpdateEnrichment`");
            }
            DetailedResponse<Enrichment> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/enrichments/{enrichmentId}");

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
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "UpdateEnrichment"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Enrichment>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Enrichment>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete an enrichment.
        ///
        /// Deletes an existing enrichment from the specified project.
        ///
        /// **Note:** Only enrichments that have been manually created can be deleted.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="enrichmentId">The ID of the enrichment.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteEnrichment(string projectId, string enrichmentId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteEnrichment`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(enrichmentId))
            {
                throw new ArgumentNullException("`enrichmentId` is required for `DeleteEnrichment`");
            }
            else
            {
                enrichmentId = Uri.EscapeDataString(enrichmentId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/enrichments/{enrichmentId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteEnrichment"));
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
        /// List document classifiers.
        ///
        /// Get a list of the document classifiers in a project. Returns only the name and classifier ID of each
        /// document classifier.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <returns><see cref="DocumentClassifiers" />DocumentClassifiers</returns>
        public DetailedResponse<DocumentClassifiers> ListDocumentClassifiers(string projectId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListDocumentClassifiers`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            DetailedResponse<DocumentClassifiers> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListDocumentClassifiers"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentClassifiers>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentClassifiers>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create a document classifier.
        ///
        /// Create a document classifier. You can use the API to create a document classifier in any project type. After
        /// you create a document classifier, you can use the Enrichments API to create a classifier enrichment, and
        /// then the Collections API to apply the enrichment to a collection in the project.
        ///
        /// **Note:** This method is supported on installed instances (IBM Cloud Pak for Data) or IBM Cloud-managed
        /// Premium or Enterprise plan instances.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="trainingData">The training data CSV file to upload. The CSV file must have headers. The file
        /// must include a field that contains the text you want to classify and a field that contains the
        /// classification labels that you want to use to classify your data. If you want to specify multiple values in
        /// a single field, use a semicolon as the value separator. For a sample file, see [the product
        /// documentation](/docs/discovery-data?topic=discovery-data-cm-doc-classifier).</param>
        /// <param name="classifier">An object that manages the settings and data that is required to train a document
        /// classification model.</param>
        /// <param name="testData">The CSV with test data to upload. The column values in the test file must be the same
        /// as the column values in the training data file. If no test data is provided, the training data is split into
        /// two separate groups of training and test data. (optional)</param>
        /// <returns><see cref="DocumentClassifier" />DocumentClassifier</returns>
        public DetailedResponse<DocumentClassifier> CreateDocumentClassifier(string projectId, System.IO.MemoryStream trainingData, CreateDocumentClassifier classifier, System.IO.MemoryStream testData = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `CreateDocumentClassifier`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (trainingData == null)
            {
                throw new ArgumentNullException("`trainingData` is required for `CreateDocumentClassifier`");
            }
            if (classifier == null)
            {
                throw new ArgumentNullException("`classifier` is required for `CreateDocumentClassifier`");
            }
            DetailedResponse<DocumentClassifier> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/csv", out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                if (classifier != null)
                {
                    var classifierContent = new StringContent(JsonConvert.SerializeObject(classifier), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                    classifierContent.Headers.ContentType = null;
                    formData.Add(classifierContent, "classifier");
                }

                if (testData != null)
                {
                    var testDataContent = new ByteArrayContent(testData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/csv", out contentType);
                    testDataContent.Headers.ContentType = contentType;
                    formData.Add(testDataContent, "test_data", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "CreateDocumentClassifier"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentClassifier>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentClassifier>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a document classifier.
        ///
        /// Get details about a specific document classifier.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <returns><see cref="DocumentClassifier" />DocumentClassifier</returns>
        public DetailedResponse<DocumentClassifier> GetDocumentClassifier(string projectId, string classifierId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetDocumentClassifier`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `GetDocumentClassifier`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            DetailedResponse<DocumentClassifier> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers/{classifierId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetDocumentClassifier"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentClassifier>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentClassifier>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a document classifier.
        ///
        /// Update the document classifier name or description, update the training data, or add or update the test
        /// data.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="classifier">An object that contains a new name or description for a document classifier,
        /// updated training data, or new or updated test data.</param>
        /// <param name="trainingData">The training data CSV file to upload. The CSV file must have headers. The file
        /// must include a field that contains the text you want to classify and a field that contains the
        /// classification labels that you want to use to classify your data. If you want to specify multiple values in
        /// a single column, use a semicolon as the value separator. For a sample file, see [the product
        /// documentation](/docs/discovery-data?topic=discovery-data-cm-doc-classifier). (optional)</param>
        /// <param name="testData">The CSV with test data to upload. The column values in the test file must be the same
        /// as the column values in the training data file. If no test data is provided, the training data is split into
        /// two separate groups of training and test data. (optional)</param>
        /// <returns><see cref="DocumentClassifier" />DocumentClassifier</returns>
        public DetailedResponse<DocumentClassifier> UpdateDocumentClassifier(string projectId, string classifierId, UpdateDocumentClassifier classifier, System.IO.MemoryStream trainingData = null, System.IO.MemoryStream testData = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `UpdateDocumentClassifier`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `UpdateDocumentClassifier`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            if (classifier == null)
            {
                throw new ArgumentNullException("`classifier` is required for `UpdateDocumentClassifier`");
            }
            DetailedResponse<DocumentClassifier> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (classifier != null)
                {
                    var classifierContent = new StringContent(JsonConvert.SerializeObject(classifier), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                    classifierContent.Headers.ContentType = null;
                    formData.Add(classifierContent, "classifier");
                }

                if (trainingData != null)
                {
                    var trainingDataContent = new ByteArrayContent(trainingData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/csv", out contentType);
                    trainingDataContent.Headers.ContentType = contentType;
                    formData.Add(trainingDataContent, "training_data", "filename");
                }

                if (testData != null)
                {
                    var testDataContent = new ByteArrayContent(testData.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/csv", out contentType);
                    testDataContent.Headers.ContentType = contentType;
                    formData.Add(testDataContent, "test_data", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers/{classifierId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "UpdateDocumentClassifier"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentClassifier>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentClassifier>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a document classifier.
        ///
        /// Deletes an existing document classifier from the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteDocumentClassifier(string projectId, string classifierId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteDocumentClassifier`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `DeleteDocumentClassifier`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers/{classifierId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteDocumentClassifier"));
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
        /// List document classifier models.
        ///
        /// Get a list of the document classifier models in a project. Returns only the name and model ID of each
        /// document classifier model.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <returns><see cref="DocumentClassifierModels" />DocumentClassifierModels</returns>
        public DetailedResponse<DocumentClassifierModels> ListDocumentClassifierModels(string projectId, string classifierId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListDocumentClassifierModels`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `ListDocumentClassifierModels`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            DetailedResponse<DocumentClassifierModels> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers/{classifierId}/models");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "ListDocumentClassifierModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentClassifierModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentClassifierModels>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create a document classifier model.
        ///
        /// Create a document classifier model by training a model that uses the data and classifier settings defined in
        /// the specified document classifier.
        ///
        /// **Note:** This method is supported on installed intances (IBM Cloud Pak for Data) or IBM Cloud-managed
        /// Premium or Enterprise plan instances.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="name">The name of the document classifier model.</param>
        /// <param name="description">A description of the document classifier model. (optional)</param>
        /// <param name="learningRate">A tuning parameter in an optimization algorithm that determines the step size at
        /// each iteration of the training process. It influences how much of any newly acquired information overrides
        /// the existing information, and therefore is said to represent the speed at which a machine learning model
        /// learns. The default value is `0.1`. (optional)</param>
        /// <param name="l1RegularizationStrengths">Avoids overfitting by shrinking the coefficient of less important
        /// features to zero, which removes some features altogether. You can specify many values for hyper-parameter
        /// optimization. The default value is `[0.000001]`. (optional, default to [1.0E-6])</param>
        /// <param name="l2RegularizationStrengths">A method you can apply to avoid overfitting your model on the
        /// training data. You can specify many values for hyper-parameter optimization. The default value is
        /// `[0.000001]`. (optional, default to [1.0E-6])</param>
        /// <param name="trainingMaxSteps">Maximum number of training steps to complete. This setting is useful if you
        /// need the training process to finish in a specific time frame to fit into an automated process. The default
        /// value is ten million. (optional)</param>
        /// <param name="improvementRatio">Stops the training run early if the improvement ratio is not met by the time
        /// the process reaches a certain point. The default value is `0.00001`. (optional)</param>
        /// <returns><see cref="DocumentClassifierModel" />DocumentClassifierModel</returns>
        public DetailedResponse<DocumentClassifierModel> CreateDocumentClassifierModel(string projectId, string classifierId, string name, string description = null, double? learningRate = null, List<double?> l1RegularizationStrengths = null, List<double?> l2RegularizationStrengths = null, long? trainingMaxSteps = null, double? improvementRatio = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `CreateDocumentClassifierModel`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `CreateDocumentClassifierModel`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateDocumentClassifierModel`");
            }
            DetailedResponse<DocumentClassifierModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers/{classifierId}/models");

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
                if (learningRate != null)
                {
                    bodyObject["learning_rate"] = JToken.FromObject(learningRate);
                }
                if (l1RegularizationStrengths != null && l1RegularizationStrengths.Count > 0)
                {
                    bodyObject["l1_regularization_strengths"] = JToken.FromObject(l1RegularizationStrengths);
                }
                if (l2RegularizationStrengths != null && l2RegularizationStrengths.Count > 0)
                {
                    bodyObject["l2_regularization_strengths"] = JToken.FromObject(l2RegularizationStrengths);
                }
                if (trainingMaxSteps != null)
                {
                    bodyObject["training_max_steps"] = JToken.FromObject(trainingMaxSteps);
                }
                if (improvementRatio != null)
                {
                    bodyObject["improvement_ratio"] = JToken.FromObject(improvementRatio);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "CreateDocumentClassifierModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentClassifierModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentClassifierModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a document classifier model.
        ///
        /// Get details about a specific document classifier model.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="modelId">The ID of the classifier model.</param>
        /// <returns><see cref="DocumentClassifierModel" />DocumentClassifierModel</returns>
        public DetailedResponse<DocumentClassifierModel> GetDocumentClassifierModel(string projectId, string classifierId, string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `GetDocumentClassifierModel`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `GetDocumentClassifierModel`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `GetDocumentClassifierModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<DocumentClassifierModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers/{classifierId}/models/{modelId}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "GetDocumentClassifierModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentClassifierModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentClassifierModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a document classifier model.
        ///
        /// Update the document classifier model name or description.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="modelId">The ID of the classifier model.</param>
        /// <param name="name">A new name for the enrichment. (optional)</param>
        /// <param name="description">A new description for the enrichment. (optional)</param>
        /// <returns><see cref="DocumentClassifierModel" />DocumentClassifierModel</returns>
        public DetailedResponse<DocumentClassifierModel> UpdateDocumentClassifierModel(string projectId, string classifierId, string modelId, string name = null, string description = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `UpdateDocumentClassifierModel`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `UpdateDocumentClassifierModel`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `UpdateDocumentClassifierModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<DocumentClassifierModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers/{classifierId}/models/{modelId}");

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
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "UpdateDocumentClassifierModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentClassifierModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentClassifierModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a document classifier model.
        ///
        /// Deletes an existing document classifier model from the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="classifierId">The ID of the classifier.</param>
        /// <param name="modelId">The ID of the classifier model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteDocumentClassifierModel(string projectId, string classifierId, string modelId)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteDocumentClassifierModel`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(classifierId))
            {
                throw new ArgumentNullException("`classifierId` is required for `DeleteDocumentClassifierModel`");
            }
            else
            {
                classifierId = Uri.EscapeDataString(classifierId);
            }
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `DeleteDocumentClassifierModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/document_classifiers/{classifierId}/models/{modelId}");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteDocumentClassifierModel"));
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
        /// Analyze a Document.
        ///
        /// Process a document and return it for realtime use. Supports JSON files only.
        ///
        /// The file is not stored in the collection, but is processed according to the collection's configuration
        /// settings. To get results, enrichments must be applied to a field in the collection that also exists in the
        /// file that you want to analyze. For example, to analyze text in a `Quote` field, you must apply enrichments
        /// to the `Quote` field in the collection configuration. Then, when you analyze the file, the text in the
        /// `Quote` field is analyzed and results are written to a field named `enriched_Quote`.
        ///
        /// **Note:** This method is supported with Enterprise plan deployments and installed deployments only.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="file">When adding a document, the content of the document to ingest. For maximum supported file
        /// size limits, see [the
        /// documentation](/docs/discovery-data?topic=discovery-data-collections#collections-doc-limits).
        ///
        /// When analyzing a document, the content of the document to analyze but not ingest. Only the
        /// `application/json` content type is supported currently. For maximum supported file size limits, see [the
        /// product documentation](/docs/discovery-data?topic=discovery-data-analyzeapi#analyzeapi-limits).
        /// (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">Add information about the file that you want to include in the response.
        ///
        /// The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected.
        ///
        /// Example:
        ///
        ///  ```
        ///  {
        ///   "filename": "favorites2.json",
        ///   "file_type": "json"
        ///  }. (optional)</param>
        /// <returns><see cref="AnalyzedDocument" />AnalyzedDocument</returns>
        public DetailedResponse<AnalyzedDocument> AnalyzeDocument(string projectId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null)
        {
            if (string.IsNullOrEmpty(Version))
            {
                throw new ArgumentNullException("`Version` is required");
            }
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `AnalyzeDocument`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }
            if (string.IsNullOrEmpty(collectionId))
            {
                throw new ArgumentNullException("`collectionId` is required for `AnalyzeDocument`");
            }
            else
            {
                collectionId = Uri.EscapeDataString(collectionId);
            }
            DetailedResponse<AnalyzedDocument> result = null;

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

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/analyze");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "AnalyzeDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AnalyzedDocument>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AnalyzedDocument>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for AnalyzeDocument.
        /// </summary>
        public class AnalyzeDocumentEnums
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
        /// Delete labeled data.
        ///
        /// Deletes all data associated with a specified customer ID. The method has no effect if no data is associated
        /// with the customer ID.
        ///
        /// You associate a customer ID with data by passing the **X-Watson-Metadata** header with a request that passes
        /// data. For more information about personal data and customer IDs, see [Information
        /// security](/docs/discovery-data?topic=discovery-data-information-security#information-security).
        ///
        /// **Note:** This method is only supported on IBM Cloud instances of Discovery.
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

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/user_data");

                if (!string.IsNullOrEmpty(Version))
                {
                    restRequest.WithArgument("version", Version);
                }
                if (!string.IsNullOrEmpty(customerId))
                {
                    restRequest.WithArgument("customer_id", customerId);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("discovery", "v2", "DeleteUserData"));
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
    }
}
