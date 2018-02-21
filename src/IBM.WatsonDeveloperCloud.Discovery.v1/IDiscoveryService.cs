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
using System.Collections.Generic;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;

namespace IBM.WatsonDeveloperCloud.Discovery.v1
{
    public interface IDiscoveryService
    {
        /// <summary>
        /// Add an environment. Creates a new environment.  You can create only one environment per service instance. An attempt to create another environment results in an error.
        /// </summary>
        /// <param name="body">An object that defines an environment name and optional description.</param>
        /// <returns><see cref="Environment" />Environment</returns>
        Environment CreateEnvironment(CreateEnvironmentRequest body);

        /// <summary>
        /// Delete environment. 
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <returns><see cref="DeleteEnvironmentResponse" />DeleteEnvironmentResponse</returns>
        DeleteEnvironmentResponse DeleteEnvironment(string environmentId);

        /// <summary>
        /// Get environment info. 
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <returns><see cref="Environment" />Environment</returns>
        Environment GetEnvironment(string environmentId);

        /// <summary>
        /// List environments. List existing environments for the service instance.
        /// </summary>
        /// <param name="name">Show only the environment with the given name. (optional)</param>
        /// <returns><see cref="ListEnvironmentsResponse" />ListEnvironmentsResponse</returns>
        ListEnvironmentsResponse ListEnvironments(string name = null);

        /// <summary>
        /// List fields in specified collecitons. Gets a list of the unique fields (and their types) stored in the indexes of the specified collecitons.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.</param>
        /// <returns><see cref="ListCollectionFieldsResponse" />ListCollectionFieldsResponse</returns>
        ListCollectionFieldsResponse ListFields(string environmentId, List<string> collectionIds);

        /// <summary>
        /// Update an environment. Updates an environment. The environment's `name` and  `description` parameters can be changed. You must specify a `name` for the environment.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="body">An object that defines the environment's name and, optionally, description.</param>
        /// <returns><see cref="Environment" />Environment</returns>
        Environment UpdateEnvironment(string environmentId, UpdateEnvironmentRequest body);
        /// <summary>
        /// Add configuration. Creates a new configuration.  If the input configuration contains the `configuration_id`, `created`, or `updated` properties, then they are ignored and overridden by the system, and an error is not returned so that the overridden fields do not need to be removed when copying a configuration.  The configuration can contain unrecognized JSON fields. Any such fields are ignored and do not generate an error. This makes it easier to use newer configuration files with older versions of the API and the service. It also makes it possible for the tooling to add additional metadata and information to the configuration.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configuration">Input an object that enables you to customize how your content is ingested and what enrichments are added to your data.   `name` is required and must be unique within the current `environment`. All other properties are optional.  If the input configuration contains the `configuration_id`, `created`, or `updated` properties, then they will be ignored and overridden by the system (an error is not returned so that the overridden fields do not need to be removed when copying a configuration).   The configuration can contain unrecognized JSON fields. Any such fields will be ignored and will not generate an error. This makes it easier to use newer configuration files with older versions of the API and the service. It also makes it possible for the tooling to add additional metadata and information to the configuration.</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        Configuration CreateConfiguration(string environmentId, Configuration configuration);

        /// <summary>
        /// Delete a configuration. The deletion is performed unconditionally. A configuration deletion request succeeds even if the configuration is referenced by a collection or document ingestion. However, documents that have already been submitted for processing continue to use the deleted configuration. Documents are always processed with a snapshot of the configuration as it existed at the time the document was submitted.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configurationId">The ID of the configuration.</param>
        /// <returns><see cref="DeleteConfigurationResponse" />DeleteConfigurationResponse</returns>
        DeleteConfigurationResponse DeleteConfiguration(string environmentId, string configurationId);

        /// <summary>
        /// Get configuration details. 
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configurationId">The ID of the configuration.</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        Configuration GetConfiguration(string environmentId, string configurationId);

        /// <summary>
        /// List configurations. Lists existing configurations for the service instance.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="name">Find configurations with the given name. (optional)</param>
        /// <returns><see cref="ListConfigurationsResponse" />ListConfigurationsResponse</returns>
        ListConfigurationsResponse ListConfigurations(string environmentId, string name = null);

        /// <summary>
        /// Update a configuration. Replaces an existing configuration.   * Completely replaces the original configuration.   * The `configuration_id`, `updated`, and `created` fields are accepted in the request, but they are ignored, and an error is not generated. It is also acceptable for users to submit an updated configuration with none of the three properties.   * Documents are processed with a snapshot of the configuration as it was at the time the document was submitted to be ingested. This means that already submitted documents will not see any updates made to the configuration.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configurationId">The ID of the configuration.</param>
        /// <param name="configuration">Input an object that enables you to update and customize how your data is ingested and what enrichments are added to your data.  The `name` parameter is required and must be unique within the current `environment`. All other properties are optional, but if they are omitted  the default values replace the current value of each omitted property.  If the input configuration contains the `configuration_id`, `created`, or `updated` properties, they are ignored and overridden by the system, and an error is not returned so that the overridden fields do not need to be removed when updating a configuration.   The configuration can contain unrecognized JSON fields. Any such fields are ignored and do not generate an error. This makes it easier to use newer configuration files with older versions of the API and the service. It also makes it possible for the tooling to add additional metadata and information to the configuration.</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        Configuration UpdateConfiguration(string environmentId, string configurationId, Configuration configuration);
        /// <summary>
        /// Test configuration. Runs a sample document through the default or your configuration and returns diagnostic information designed to help you understand how the document was processed. The document is not added to the index.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="configuration">The configuration to use to process the document. If this part is provided, then the provided configuration is used to process the document. If the `configuration_id` is also provided (both are present at the same time), then request is rejected. The maximum supported configuration size is 1 MB. Configuration parts larger than 1 MB are rejected. See the `GET /configurations/{configuration_id}` operation for an example configuration. (optional)</param>
        /// <param name="step">Specify to only run the input document through the given step instead of running the input document through the entire ingestion workflow. Valid values are `convert`, `enrich`, and `normalize`. (optional)</param>
        /// <param name="configurationId">The ID of the configuration to use to process the document. If the `configuration` form part is also provided (both are present at the same time), then request will be rejected. (optional)</param>
        /// <param name="file">The content of the document to ingest. The maximum supported file size is 50 megabytes. Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected. Example:  ``` {   "Creator": "Johnny Appleseed",   "Subject": "Apples" } ```. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <returns><see cref="TestDocument" />TestDocument</returns>
        TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.Stream file = null, string metadata = null, string fileContentType = null);
        /// <summary>
        /// Create a collection. 
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="body">Input an object that allows you to add a collection.</param>
        /// <returns><see cref="Collection" />Collection</returns>
        Collection CreateCollection(string environmentId, CreateCollectionRequest body);

        /// <summary>
        /// Set the expansion list. Create or replace the Expansion list for this collection. The maximum number of expanded terms per collection is `500`. The current expansion list is replaced with the uploaded content.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="body">An object that defines the expansion list.</param>
        /// <returns><see cref="Expansions" />Expansions</returns>
        Expansions CreateExpansions(string environmentId, string collectionId, Expansions body);

        /// <summary>
        /// Delete a collection. 
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="DeleteCollectionResponse" />DeleteCollectionResponse</returns>
        DeleteCollectionResponse DeleteCollection(string environmentId, string collectionId);

        /// <summary>
        /// Delete the expansions list. Remove the expansion information for this collection. The expansion list must be deleted to disable query expansion for a collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteExpansions(string environmentId, string collectionId);

        /// <summary>
        /// Get collection details. 
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="Collection" />Collection</returns>
        Collection GetCollection(string environmentId, string collectionId);

        /// <summary>
        /// List unique fields. Gets a list of the unique fields (and their types) stored in the index.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="ListCollectionFieldsResponse" />ListCollectionFieldsResponse</returns>
        ListCollectionFieldsResponse ListCollectionFields(string environmentId, string collectionId);

        /// <summary>
        /// List collections. Lists existing collections for the service instance.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="name">Find collections with the given name. (optional)</param>
        /// <returns><see cref="ListCollectionsResponse" />ListCollectionsResponse</returns>
        ListCollectionsResponse ListCollections(string environmentId, string name = null);

        /// <summary>
        /// List current expansions. Returns the current expansion list for the specified collection. If an expansion list is not specified, an object with empty expansion arrays is returned.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="Expansions" />Expansions</returns>
        Expansions ListExpansions(string environmentId, string collectionId);

        /// <summary>
        /// Update a collection. 
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="body">Input an object that allows you to update a collection. (optional)</param>
        /// <returns><see cref="Collection" />Collection</returns>
        Collection UpdateCollection(string environmentId, string collectionId, UpdateCollectionRequest body = null);
        /// <summary>
        /// Add a document. Add a document to a collection with optional metadata.    * The `version` query parameter is still required.    * Returns immediately after the system has accepted the document for processing.    * The user must provide document content, metadata, or both. If the request is missing both document content and metadata, it is rejected.    * The user can set the `Content-Type` parameter on the `file` part to indicate the media type of the document. If the `Content-Type` parameter is missing or is one of the generic media types (for example, `application/octet-stream`), then the service attempts to automatically detect the document's media type.    * The following field names are reserved and will be filtered out if present after normalization: `id`, `score`, `highlight`, and any field with the prefix of: `_`, `+`, or `-`    * Fields with empty name values after normalization are filtered out before indexing.    * Fields containing the following characters after normalization are filtered out before indexing: `#` and `,`.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="file">The content of the document to ingest. The maximum supported file size is 50 megabytes. Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected. Example:  ``` {   "Creator": "Johnny Appleseed",   "Subject": "Apples" } ```. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        DocumentAccepted AddDocument(string environmentId, string collectionId, System.IO.Stream file = null, string metadata = null, string fileContentType = null);

        /// <summary>
        /// Delete a document. If the given document ID is invalid, or if the document is not found, then the a success response is returned (HTTP status code `200`) with the status set to 'deleted'.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <returns><see cref="DeleteDocumentResponse" />DeleteDocumentResponse</returns>
        DeleteDocumentResponse DeleteDocument(string environmentId, string collectionId, string documentId);

        /// <summary>
        /// Get document details. Fetch status details about a submitted document. **Note:** this operation does not return the document itself. Instead, it returns only the document's processing status and any notices (warnings or errors) that were generated when the document was ingested. Use the query API to retrieve the actual document content.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <returns><see cref="DocumentStatus" />DocumentStatus</returns>
        DocumentStatus GetDocumentStatus(string environmentId, string collectionId, string documentId);

        /// <summary>
        /// Update a document. Replace an existing document. Starts ingesting a document with optional metadata.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="documentId">The ID of the document.</param>
        /// <param name="file">The content of the document to ingest. The maximum supported file size is 50 megabytes. Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected. Example:  ``` {   "Creator": "Johnny Appleseed",   "Subject": "Apples" } ```. (optional)</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, System.IO.Stream file = null, string metadata = null, string fileContentType = null);
        /// <summary>
        /// Query documents in multiple collections. See the [Discovery service documentation](https://console.bluemix.net/docs/services/discovery/using.html) for more details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.</param>
        /// <param name="filter">A cacheable query that limits the documents returned to exclude any documents that don't mention the query content. Filter searches are better for metadata type searches and when you are trying to get a sense of concepts in the data set. (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full text, but with the most relevant documents listed first. Use a query search when you want to find the most relevant search results. You cannot use `natural_language_query` and `query` at the same time. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing training data and natural language understanding. You cannot use `naturalLanguageQuery` and `query` at the same time. (optional)</param>
        /// <param name="aggregation">An aggregation search uses combinations of filters and query search to return an exact answer. Aggregations are useful for building applications, because you can use them to build lists, tables, and time series. For a full list of possible aggregrations, see the Query reference. (optional)</param>
        /// <param name="count">Number of documents to return. (optional, default to 10)</param>
        /// <param name="returnFields">A comma separated list of the portion of the document hierarchy to return. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number of results that are returned is 10, and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma separated list of fields in the document to sort on. You can optionally specify a sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true a highlight field is returned for each result which contains the fields that match the query with `<em></em>` tags around the matching query terms. Defaults to false. (optional)</param>
        /// <param name="deduplicate">When `true` and used with a Watson Discovery News collection, duplicate results (based on the contents of the `title` field) are removed. Duplicate comparison is limited to the current query only, `offset` is not considered. Defaults to `false`. This parameter is currently Beta functionality. (optional)</param>
        /// <param name="deduplicateField">When specified, duplicate results based on the field specified are removed from the returned results. Duplicate comparison is limited to the current query only, `offset` is not considered. This parameter is currently Beta functionality. (optional)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        QueryResponse FederatedQuery(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, bool? deduplicate = null, string deduplicateField = null);

        /// <summary>
        /// Query multiple collection system notices. Queries for notices (errors or warnings) that might have been generated by the system. Notices are generated when ingesting documents and performing relevance training. See the [Discovery service documentation](https://console.bluemix.net/docs/services/discovery/using.html) for more details on the query language.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionIds">A comma-separated list of collection IDs to be queried against.</param>
        /// <param name="filter">A cacheable query that limits the documents returned to exclude any documents that don't mention the query content. Filter searches are better for metadata type searches and when you are trying to get a sense of concepts in the data set. (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full text, but with the most relevant documents listed first. Use a query search when you want to find the most relevant search results. You cannot use `natural_language_query` and `query` at the same time. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing training data and natural language understanding. You cannot use `naturalLanguageQuery` and `query` at the same time. (optional)</param>
        /// <param name="aggregation">An aggregation search uses combinations of filters and query search to return an exact answer. Aggregations are useful for building applications, because you can use them to build lists, tables, and time series. For a full list of possible aggregrations, see the Query reference. (optional)</param>
        /// <param name="count">Number of documents to return. (optional, default to 10)</param>
        /// <param name="returnFields">A comma separated list of the portion of the document hierarchy to return. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number of results that are returned is 10, and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma separated list of fields in the document to sort on. You can optionally specify a sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true a highlight field is returned for each result which contains the fields that match the query with `<em></em>` tags around the matching query terms. Defaults to false. (optional)</param>
        /// <param name="deduplicateField">When specified, duplicate results based on the field specified are removed from the returned results. Duplicate comparison is limited to the current query only, `offset` is not considered. This parameter is currently Beta functionality. (optional)</param>
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        QueryNoticesResponse FederatedQueryNotices(string environmentId, List<string> collectionIds, string filter = null, string query = null, string naturalLanguageQuery = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, string deduplicateField = null);

        /// <summary>
        /// Query documents. See the [Discovery service documentation](https://console.bluemix.net/docs/services/discovery/using.html) for more details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="filter">A cacheable query that limits the documents returned to exclude any documents that don't mention the query content. Filter searches are better for metadata type searches and when you are trying to get a sense of concepts in the data set. (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full text, but with the most relevant documents listed first. Use a query search when you want to find the most relevant search results. You cannot use `natural_language_query` and `query` at the same time. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing training data and natural language understanding. You cannot use `naturalLanguageQuery` and `query` at the same time. (optional)</param>
        /// <param name="passages">A passages query that returns the most relevant passages from the results. (optional)</param>
        /// <param name="aggregation">An aggregation search uses combinations of filters and query search to return an exact answer. Aggregations are useful for building applications, because you can use them to build lists, tables, and time series. For a full list of possible aggregrations, see the Query reference. (optional)</param>
        /// <param name="count">Number of documents to return. (optional, default to 10)</param>
        /// <param name="returnFields">A comma separated list of the portion of the document hierarchy to returnFields. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number of results that are returned is 10, and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma separated list of fields in the document to sort on. You can optionally specify a sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true a highlight field is returned for each result which contains the fields that match the query with `<em></em>` tags around the matching query terms. Defaults to false. (optional)</param>
        /// <param name="passagesFields">A comma-separated list of fields that passages are drawn from. If this parameter not specified, then all top-level fields are included. (optional)</param>
        /// <param name="passagesCount">The maximum number of passages to return. The search returns fewer passages if the requested total is not found. The default is `10`. The maximum is `100`. (optional)</param>
        /// <param name="passagesCharacters">The approximate number of characters that any one passage will have. The default is `400`. The minimum is `50`. The maximum is `2000`. (optional)</param>
        /// <param name="deduplicate">When `true` and used with a Watson Discovery News collection, duplicate results (based on the contents of the `title` field) are removed. Duplicate comparison is limited to the current query only, `offset` is not considered. Defaults to `false`. This parameter is currently Beta functionality. (optional)</param>
        /// <param name="deduplicateField">When specified, duplicate results based on the field specified are removed from the returned results. Duplicate comparison is limited to the current query only, `offset` is not considered. This parameter is currently Beta functionality. (optional)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        QueryResponse Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null);

        /// <summary>
        /// Knowledge Graph entity query. See the [Knowledge Graph documentation](https://console.bluemix.net/docs/services/discovery/building-kg.html) for more details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="entityQuery">An object specifying the entities to query, which functions to perform, and any additional constraints.</param>
        /// <returns><see cref="QueryEntitiesResponse" />QueryEntitiesResponse</returns>
        QueryEntitiesResponse QueryEntities(string environmentId, string collectionId, QueryEntities entityQuery);

        /// <summary>
        /// Query system notices. Queries for notices (errors or warnings) that might have been generated by the system. Notices are generated when ingesting documents and performing relevance training. See the [Discovery service documentation](https://console.bluemix.net/docs/services/discovery/using.html) for more details on the query language.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="filter">A cacheable query that limits the documents returned to exclude any documents that don't mention the query content. Filter searches are better for metadata type searches and when you are trying to get a sense of concepts in the data set. (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full text, but with the most relevant documents listed first. Use a query search when you want to find the most relevant search results. You cannot use `natural_language_query` and `query` at the same time. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing training data and natural language understanding. You cannot use `naturalLanguageQuery` and `query` at the same time. (optional)</param>
        /// <param name="passages">A passages query that returns the most relevant passages from the results. (optional)</param>
        /// <param name="aggregation">An aggregation search uses combinations of filters and query search to return an exact answer. Aggregations are useful for building applications, because you can use them to build lists, tables, and time series. For a full list of possible aggregrations, see the Query reference. (optional)</param>
        /// <param name="count">Number of documents to return. (optional, default to 10)</param>
        /// <param name="returnFields">A comma separated list of the portion of the document hierarchy to return. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number of results that are returned is 10, and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma separated list of fields in the document to sort on. You can optionally specify a sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true a highlight field is returned for each result which contains the fields that match the query with `<em></em>` tags around the matching query terms. Defaults to false. (optional)</param>
        /// <param name="passagesFields">A comma-separated list of fields that passages are drawn from. If this parameter not specified, then all top-level fields are included. (optional)</param>
        /// <param name="passagesCount">The maximum number of passages to return. The search returns fewer passages if the requested total is not found. The default is `10`. The maximum is `100`. (optional)</param>
        /// <param name="passagesCharacters">The approximate number of characters that any one passage will have. The default is `400`. The minimum is `50`. The maximum is `2000`. (optional)</param>
        /// <param name="deduplicateField">When specified, duplicate results based on the field specified are removed from the returned results. Duplicate comparison is limited to the current query only, `offset` is not considered. This parameter is currently Beta functionality. (optional)</param>
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> returnFields = null, long? offset = null, List<string> sort = null, bool? highlight = null, List<string> passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, string deduplicateField = null);

        /// <summary>
        /// Knowledge Graph relationship query. See the [Knowledge Graph documentation](https://console.bluemix.net/docs/services/discovery/building-kg.html) for more details.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="relationshipQuery">An object that describes the relationships to be queried and any query constraints (such as filters).</param>
        /// <returns><see cref="QueryRelationsResponse" />QueryRelationsResponse</returns>
        QueryRelationsResponse QueryRelations(string environmentId, string collectionId, QueryRelations relationshipQuery);
        /// <summary>
        ///  Adds a query to the training data for this collection. The query can contain a filter and natural language query.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="body"></param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        TrainingQuery AddTrainingData(string environmentId, string collectionId, NewTrainingQuery body);

        /// <summary>
        ///  Adds a new example to this training data query.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="body"></param>
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        TrainingExample CreateTrainingExample(string environmentId, string collectionId, string queryId, TrainingExample body);

        /// <summary>
        ///  Clears all training data for this collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteAllTrainingData(string environmentId, string collectionId);

        /// <summary>
        ///  Removes the training data and all associated examples from the training data set.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteTrainingData(string environmentId, string collectionId, string queryId);

        /// <summary>
        ///  Removes the example with the given ID for the training data query.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="exampleId">The ID of the document as it is indexed.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteTrainingExample(string environmentId, string collectionId, string queryId, string exampleId);

        /// <summary>
        ///  Shows details for a specific training data query, including the query string and all examples.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <returns><see cref="TrainingQuery" />TrainingQuery</returns>
        TrainingQuery GetTrainingData(string environmentId, string collectionId, string queryId);

        /// <summary>
        ///  Gets the details for this training example.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="exampleId">The ID of the document as it is indexed.</param>
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        TrainingExample GetTrainingExample(string environmentId, string collectionId, string queryId, string exampleId);

        /// <summary>
        ///  Lists the training data for this collection.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <returns><see cref="TrainingDataSet" />TrainingDataSet</returns>
        TrainingDataSet ListTrainingData(string environmentId, string collectionId);

        /// <summary>
        ///  List all examples for this training data query.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <returns><see cref="TrainingExampleList" />TrainingExampleList</returns>
        TrainingExampleList ListTrainingExamples(string environmentId, string collectionId, string queryId);

        /// <summary>
        ///  Changes the label or cross reference query for this training example.
        /// </summary>
        /// <param name="environmentId">The ID of the environment.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="queryId">The ID of the query used for training.</param>
        /// <param name="exampleId">The ID of the document as it is indexed.</param>
        /// <param name="body"></param>
        /// <returns><see cref="TrainingExample" />TrainingExample</returns>
        TrainingExample UpdateTrainingExample(string environmentId, string collectionId, string queryId, string exampleId, TrainingExamplePatch body);
    }
}
