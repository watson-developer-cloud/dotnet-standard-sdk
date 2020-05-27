/**
* (C) Copyright IBM Corp. 2018, 2020.
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
        const string serviceName = "discovery";
        public string VersionDate { get; set; }

        public DiscoveryService(string versionDate) : this(versionDate, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public DiscoveryService(IClient httpClient) : base(serviceName, httpClient) { }

        public DiscoveryService(string versionDate, IAuthenticator authenticator) : base(serviceName, authenticator)
        {
            if (string.IsNullOrEmpty(versionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            VersionDate = versionDate;
        }

        /// <summary>
        /// List collections.
        ///
        /// Lists existing collections for the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<ListCollectionsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/collections");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

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
        /// Query a project.
        ///
        /// By using this method, you can construct queries. For details, see the [Discovery
        /// documentation](https://cloud.ibm.com/docs/discovery-data?topic=discovery-data-query-concepts).
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <param name="queryLong">An object that represents the query to be submitted. (optional)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        public DetailedResponse<QueryResponse> Query(string projectId, List<string> collectionIds = null, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, string sort = null, bool? highlight = null, bool? spellingSuggestions = null, QueryLargeTableResults tableResults = null, QueryLargeSuggestedRefinements suggestedRefinements = null, QueryLargePassages passages = null)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `Query`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<QueryResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/query");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <param name="prefix">The prefix to use for autocompletion. For example, the prefix `Ho` could autocomplete
        /// to `Hot`, `Housing`, or `How do I upgrade`. Possible completions are.</param>
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
            if (string.IsNullOrEmpty(prefix))
            {
                throw new ArgumentNullException("`prefix` is required for `GetAutocompletion`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<Completions> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/autocompletion");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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
        /// Query system notices.
        ///
        /// Queries for notices (errors or warnings) that might have been generated by the system. Notices are generated
        /// when ingesting documents and performing relevance training.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing
        /// training data and natural language understanding. (optional)</param>
        /// <param name="count">Number of results to return. The maximum for the **count** and **offset** values
        /// together in any one query is **10000**. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. The maximum for the
        /// **count** and **offset** values together in any one query is **10000**. (optional)</param>
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        public DetailedResponse<QueryNoticesResponse> QueryNotices(string projectId, string filter = null, string query = null, string naturalLanguageQuery = null, long? count = null, long? offset = null)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `QueryNotices`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<QueryNoticesResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/notices");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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
        /// List fields.
        ///
        /// Gets a list of the unique fields (and their types) stored in the the specified collections.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <param name="collectionIds">Comma separated list of the collection IDs. If this parameter is not specified,
        /// all collections in the project are used. (optional)</param>
        /// <returns><see cref="ListFieldsResponse" />ListFieldsResponse</returns>
        public DetailedResponse<ListFieldsResponse> ListFields(string projectId, List<string> collectionIds = null)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListFields`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<ListFieldsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/fields");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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
        /// Configuration settings for components.
        ///
        /// Returns default configuration settings for components.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<ComponentSettingsResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/component_settings");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

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
        /// Add a document.
        ///
        /// Add a document to a collection with optional metadata.
        ///
        ///  Returns immediately after the system has accepted the document for processing.
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
        ///   If the document is uploaded to a collection that has it's data shared with another collection, the
        /// **X-Watson-Discovery-Force** header must be set to `true`.
        ///
        ///  **Note:** Documents can be added with a specific **document_id** by using the
        /// **_/v2/projects/{project_id}/collections/{collection_id}/documents** method.
        ///
        /// **Note:** This operation only works on collections created to accept direct file uploads. It cannot be used
        /// to modify a collection that connects to an external source such as Microsoft SharePoint.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
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
        /// <param name="xWatsonDiscoveryForce">When `true`, the uploaded document is added to the collection even if
        /// the data for that collection is shared with other collections. (optional, default to false)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DetailedResponse<DocumentAccepted> AddDocument(string projectId, string collectionId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, bool? xWatsonDiscoveryForce = null)
        {
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
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

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                if (xWatsonDiscoveryForce != null)
                {
                    restRequest.WithHeader("X-Watson-Discovery-Force", (bool)xWatsonDiscoveryForce ? "true" : "false");
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
        /// Update a document.
        ///
        /// Replace an existing document or add a document with a specified **document_id**. Starts ingesting a document
        /// with optional metadata.
        ///
        /// If the document is uploaded to a collection that has it's data shared with another collection, the
        /// **X-Watson-Discovery-Force** header must be set to `true`.
        ///
        /// **Note:** When uploading a new document with this method it automatically replaces any document stored with
        /// the same **document_id** if it exists.
        ///
        /// **Note:** This operation only works on collections created to accept direct file uploads. It cannot be used
        /// to modify a collection that connects to an external source such as Microsoft SharePoint.
        ///
        /// **Note:** If an uploaded document is segmented, all segments will be overwritten, even if the updated
        /// version of the document has fewer segments.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
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
        /// <param name="xWatsonDiscoveryForce">When `true`, the uploaded document is added to the collection even if
        /// the data for that collection is shared with other collections. (optional, default to false)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        public DetailedResponse<DocumentAccepted> UpdateDocument(string projectId, string collectionId, string documentId, System.IO.MemoryStream file = null, string filename = null, string fileContentType = null, string metadata = null, bool? xWatsonDiscoveryForce = null)
        {
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
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

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                if (xWatsonDiscoveryForce != null)
                {
                    restRequest.WithHeader("X-Watson-Discovery-Force", (bool)xWatsonDiscoveryForce ? "true" : "false");
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
        /// Delete a document.
        ///
        /// If the given document ID is invalid, or if the document is not found, then the a success response is
        /// returned (HTTP status code `200`) with the status set to 'deleted'.
        ///
        /// **Note:** This operation only works on collections created to accept direct file uploads. It cannot be used
        /// to modify a collection that connects to an external source such as Microsoft SharePoint.
        ///
        /// **Note:** Segments of an uploaded document cannot be deleted individually. Delete all segments by deleting
        /// using the `parent_document_id` of a segment result.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="xWatsonDiscoveryForce">When `true`, the uploaded document is added to the collection even if
        /// the data for that collection is shared with other collections. (optional, default to false)</param>
        /// <returns><see cref="DeleteDocumentResponse" />DeleteDocumentResponse</returns>
        public DetailedResponse<DeleteDocumentResponse> DeleteDocument(string projectId, string collectionId, string documentId, bool? xWatsonDiscoveryForce = null)
        {
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<DeleteDocumentResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/collections/{collectionId}/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                if (xWatsonDiscoveryForce != null)
                {
                    restRequest.WithHeader("X-Watson-Discovery-Force", (bool)xWatsonDiscoveryForce ? "true" : "false");
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
        /// List training queries.
        ///
        /// List the training queries for the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <returns><see cref="TrainingQuerySet" />TrainingQuerySet</returns>
        public DetailedResponse<TrainingQuerySet> ListTrainingQueries(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `ListTrainingQueries`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TrainingQuerySet> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

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
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteTrainingQueries(string projectId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException("`projectId` is required for `DeleteTrainingQueries`");
            }
            else
            {
                projectId = Uri.EscapeDataString(projectId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries");

                restRequest.WithArgument("version", VersionDate);

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
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <param name="trainingQuery">An object that represents the query to be submitted.</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> CreateTrainingQuery(string projectId, string naturalLanguageQuery, List<TrainingExample> examples, string filter = null)
        {
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> GetTrainingQuery(string projectId, string queryId)
        {
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries/{queryId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

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
        /// <param name="projectId">The ID of the project. This information can be found from the deploy page of the
        /// Discovery administrative tooling.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="body">The body of the example that is to be added to the specified query.</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        public DetailedResponse<TrainingQuery> UpdateTrainingQuery(string projectId, string queryId, string naturalLanguageQuery, List<TrainingExample> examples, string filter = null)
        {
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

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TrainingQuery> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v2/projects/{projectId}/training_data/queries/{queryId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
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
    }
}
