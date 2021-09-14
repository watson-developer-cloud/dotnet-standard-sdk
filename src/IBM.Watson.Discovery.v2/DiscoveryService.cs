/**
* (C) Copyright IBM Corp. 2019, 2021.
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
* IBM OpenAPI SDK Code Generator Version: 3.38.0-07189efd-20210827-205025
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
        /// <param name="language">The language of the collection. (optional, default to en)</param>
        /// <param name="enrichments">An array of enrichments that are applied to this collection. (optional)</param>
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
        /// <param name="name">The name of the collection. (optional)</param>
        /// <param name="description">A description of the collection. (optional)</param>
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
        /// Query a project.
        ///
        /// By using this method, you can construct queries. For details, see the [Discovery
        /// documentation](https://cloud.ibm.com/docs/discovery-data?topic=discovery-data-query-concepts). The default
        /// query parameters are defined by the settings for this project, see the [Discovery
        /// documentation](https://cloud.ibm.com/docs/discovery-data?topic=discovery-data-project-defaults) for an
        /// overview of the standard default settings, and see [the Projects API documentation](#create-project) for
        /// details about how to set custom default query settings.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.
        /// (optional)</param>
        /// <param name="filter">A cacheable query that excludes documents that don't mention the query content. Filter
        /// searches are better for metadata-type searches and for assessing the concepts in the data set.
        /// (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full
        /// text, but with the most relevant documents listed first. Use a query search when you want to find the most
        /// relevant search results. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing
        /// training data and natural language understanding. (optional)</param>
        /// <param name="aggregation">An aggregation search that returns an exact answer by combining query search with
        /// filters. Useful for applications to build lists, tables, and time series. For a full list of possible
        /// aggregations, see the Query reference. (optional)</param>
        /// <param name="count">Number of results to return. (optional)</param>
        /// <param name="_return">A list of the fields in the document hierarchy to return. If this parameter is an
        /// empty list, then all fields are returned. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number
        /// of results that are returned is 10 and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma-separated list of fields in the document to sort on. You can optionally specify a
        /// sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default
        /// sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When `true`, a highlight field is returned for each result which contains the fields
        /// which match the query with `<em></em>` tags around the matching query terms. (optional)</param>
        /// <param name="spellingSuggestions">When `true` and the **natural_language_query** parameter is used, the
        /// **natural_language_query** parameter is spell checked. The most likely correction is returned in the
        /// **suggested_query** field of the response (if one exists). (optional)</param>
        /// <param name="tableResults">Configuration for table retrieval. (optional)</param>
        /// <param name="suggestedRefinements">Configuration for suggested refinements. Available with Premium plans
        /// only. (optional)</param>
        /// <param name="passages">Configuration for passage retrieval. (optional)</param>
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
        /// List fields.
        ///
        /// Gets a list of the unique fields (and their types) stored in the the specified collections.
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
        /// Add a document.
        ///
        /// Add a document to a collection with optional metadata.
        ///
        /// Returns immediately after the system has accepted the document for processing.
        ///
        ///   * The user must provide document content, metadata, or both. If the request is missing both document
        /// content and metadata, it is rejected.
        ///
        ///   * You can set the **Content-Type** parameter on the **file** part to indicate the media type of the
        /// document. If the **Content-Type** parameter is missing or is one of the generic media types (for example,
        /// `application/octet-stream`), then the service attempts to automatically detect the document's media type.
        ///
        ///   * The following field names are reserved and are filtered out if present after normalization: `id`,
        /// `score`, `highlight`, and any field with the prefix of: `_`, `+`, or `-`
        ///
        ///   * Fields with empty name values after normalization are filtered out before indexing.
        ///
        ///   * Fields that contain the following characters after normalization are filtered out before indexing: `#`
        /// and `,`
        ///
        ///   If the document is uploaded to a collection that shares its data with another collection, the
        /// **X-Watson-Discovery-Force** header must be set to `true`.
        ///
        /// **Note:** You can assign an ID to a document that you add by appending the ID to the endpoint
        /// (`/v2/projects/{project_id}/collections/{collection_id}/documents/{document_id}`). If a document already
        /// exists with the specified ID, it is replaced.
        ///
        /// **Note:** This operation works with a file upload collection. It cannot be used to modify a collection that
        /// crawls an external data source.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="file">The content of the document to ingest. For maximum supported file size limits, see [the
        /// documentation](https://cloud.ibm.com/docs/discovery-data?topic=discovery-data-collections#collections-doc-limits).
        /// (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are
        /// rejected.
        ///
        ///
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
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
        /// Update a document.
        ///
        /// Replace an existing document or add a document with a specified **document_id**. Starts ingesting a document
        /// with optional metadata.
        ///
        /// If the document is uploaded to a collection that shares its data with another collection, the
        /// **X-Watson-Discovery-Force** header must be set to `true`.
        ///
        /// **Note:** When uploading a new document with this method it automatically replaces any document stored with
        /// the same **document_id** if it exists.
        ///
        /// **Note:** This operation only works on collections created to accept direct file uploads. It cannot be used
        /// to modify a collection that connects to an external source such as Microsoft SharePoint.
        ///
        /// **Note:** If an uploaded document is segmented, all segments are overwritten, even if the updated version of
        /// the document has fewer segments.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="file">The content of the document to ingest. For maximum supported file size limits, see [the
        /// documentation](https://cloud.ibm.com/docs/discovery-data?topic=discovery-data-collections#collections-doc-limits).
        /// (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are
        /// rejected.
        ///
        ///
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
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
        /// If the given document ID is invalid, or if the document is not found, then the a success response is
        /// returned (HTTP status code `200`) with the status set to 'deleted'.
        ///
        /// **Note:** This operation only works on collections created to accept direct file uploads. It cannot be used
        /// to modify a collection that connects to an external source such as Microsoft SharePoint.
        ///
        /// **Note:** Segments of an uploaded document cannot be deleted individually. Delete all segments by deleting
        /// using the `parent_document_id` of a segment result.
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
        /// <param name="naturalLanguageQuery">The natural text query for the training query.</param>
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
        /// <param name="naturalLanguageQuery">The natural text query for the training query.</param>
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
        /// Analyze a Document.
        ///
        /// Process a document and return it for realtime use. Supports JSON files only.
        ///
        /// The document is processed according to the collection's configuration settings but is not stored in the
        /// collection.
        ///
        /// **Note:** This method is supported on installed instances of Discovery only.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="file">The content of the document to ingest. For maximum supported file size limits, see [the
        /// documentation](https://cloud.ibm.com/docs/discovery-data?topic=discovery-data-collections#collections-doc-limits).
        /// (optional)</param>
        /// <param name="filename">The filename for file. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="metadata">The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are
        /// rejected.
        ///
        ///
        /// Example:  ``` {
        ///   "Creator": "Johnny Appleseed",
        ///   "Subject": "Apples"
        /// } ```. (optional)</param>
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
                    var fileContent = new StreamContent(file);
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
        /// List Enrichments.
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
        /// Create an enrichment for use with the specified project.
        /// </summary>
        /// <param name="projectId">The ID of the project. This information can be found from the *Integrate and Deploy*
        /// page in Discovery.</param>
        /// <param name="enrichment">Information about a specific enrichment.</param>
        /// <param name="file">The enrichment file to upload. (optional)</param>
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
        /// Create a Project.
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
        /// Delete labeled data.
        ///
        /// Deletes all data associated with a specified customer ID. The method has no effect if no data is associated
        /// with the customer ID.
        ///
        /// You associate a customer ID with data by passing the **X-Watson-Metadata** header with a request that passes
        /// data. For more information about personal data and customer IDs, see [Information
        /// security](https://cloud.ibm.com/docs/discovery-data?topic=discovery-data-information-security#information-security).
        ///
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
