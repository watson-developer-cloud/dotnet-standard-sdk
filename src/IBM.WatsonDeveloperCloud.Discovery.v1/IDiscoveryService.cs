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
using IBM.WatsonDeveloperCloud.Discovery.v1.Model;

namespace IBM.WatsonDeveloperCloud.Discovery.v1
{
    public interface IDiscoveryService
    {
        /// <summary>
        /// Create a collection. 
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="body">Input a JSON object that allows you to add a collection.</param>
        /// <returns><see cref="Collection" />Collection</returns>
        Collection CreateCollection(string environmentId, CreateCollectionRequest body);

        /// <summary>
        /// Delete a collection. 
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <returns><see cref="DeleteCollectionResponse" />DeleteCollectionResponse</returns>
        DeleteCollectionResponse DeleteCollection(string environmentId, string collectionId);

        /// <summary>
        /// Get collection details. 
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <returns><see cref="Collection" />Collection</returns>
        Collection GetCollection(string environmentId, string collectionId);

        /// <summary>
        /// List unique fields. Gets a list of the the unique fields (and their types) stored in the index.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <returns><see cref="ListCollectionFieldsResponse" />ListCollectionFieldsResponse</returns>
        ListCollectionFieldsResponse ListCollectionFields(string environmentId, string collectionId);

        /// <summary>
        /// List collections. Lists existing collections for the service instance.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="name">Find collections with the given name. (optional)</param>
        /// <returns><see cref="ListCollectionsResponse" />ListCollectionsResponse</returns>
        ListCollectionsResponse ListCollections(string environmentId, string name = null);

        /// <summary>
        /// Update a collection. 
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <param name="body">Input a JSON object that allows you to update a collection. (optional)</param>
        /// <returns><see cref="Collection" />Collection</returns>
        Collection UpdateCollection(string environmentId, string collectionId, UpdateCollectionRequest body = null);
        /// <summary>
        /// Add configuration. Creates a new configuration.  If the input configuration contains the `configuration_id`, `created`, or `updated` properties, then they are ignored and overridden by the system, and an error is not returned so that the overridden fields do not need to be removed when copying a configuration.  The configuration can contain unrecognized JSON fields. Any such fields are ignored and do not generate an error. This makes it easier to use newer configuration files with older versions of the API and the service. It also makes it possible for the tooling to add additional metadata and information to the configuration.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="configuration">Input a JSON object that enables you to customize how your content is ingested and what enrichments are added to your data.   `name` is required and must be unique within the current `environment`. All other properties are optional.  If the input configuration contains the `configuration_id`, `created`, or `updated` properties, then they will be ignored and overridden by the system (an error is not returned so that the overridden fields do not need to be removed when copying a configuration).   The configuration can contain unrecognized JSON fields. Any such fields will be ignored and will not generate an error. This makes it easier to use newer configuration files with older versions of the API and the service. It also makes it possible for the tooling to add additional metadata and information to the configuration.</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        Configuration CreateConfiguration(string environmentId, Configuration configuration);

        /// <summary>
        /// Delete a configuration. The deletion is performed unconditionally. A configuration deletion request succeeds even if the configuration is referenced by a collection or document ingestion. However, documents that have already been submitted for processing continue to use the deleted configuration. Documents are always processed with a snapshot of the configuration as it existed at the time the document was submitted.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="configurationId">the ID of your configuration</param>
        /// <returns><see cref="DeleteConfigurationResponse" />DeleteConfigurationResponse</returns>
        DeleteConfigurationResponse DeleteConfiguration(string environmentId, string configurationId);

        /// <summary>
        /// Get configuration details. 
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="configurationId">the ID of your configuration</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        Configuration GetConfiguration(string environmentId, string configurationId);

        /// <summary>
        /// List configurations. Lists existing configurations for the service instance.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="name">Find configurations with the given name. (optional)</param>
        /// <returns><see cref="ListConfigurationsResponse" />ListConfigurationsResponse</returns>
        ListConfigurationsResponse ListConfigurations(string environmentId, string name = null);

        /// <summary>
        /// Update a configuration. Replaces an existing configuration.   * Completely replaces the original configuration.   * The `configuration_id`, `updated`, and `created` fields are accepted in the request, but they are ignored, and an error is not generated. It is also acceptable for users to submit an updated configuration with none of the three properties.   * Documents are processed with a snapshot of the configuration as it was at the time the document was submitted to be ingested. This means that already submitted documents will not see any updates made to the configuration.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="configurationId">the ID of your configuration</param>
        /// <param name="configuration">Input a JSON object that enables you to update and customize how your data is ingested and what enrichments are added to your data.  The `name` parameter is required and must be unique within the current `environment`. All other properties are optional, but if they are omitted  the default values replace the current value of each omitted property.  If the input configuration contains the `configuration_id`, `created`, or `updated` properties, they are ignored and overridden by the system, and an error is not returned so that the overridden fields do not need to be removed when updating a configuration.   The configuration can contain unrecognized JSON fields. Any such fields are ignored and do not generate an error. This makes it easier to use newer configuration files with older versions of the API and the service. It also makes it possible for the tooling to add additional metadata and information to the configuration.</param>
        /// <returns><see cref="Configuration" />Configuration</returns>
        Configuration UpdateConfiguration(string environmentId, string configurationId, Configuration configuration);
        /// <summary>
        /// Add a document. Add a document to a collection with optional metadata and optional configuration.   Set the `Content-Type` header on the request to indicate the media type of the document. If the `Content-Type` header is missing or is one of the generic media types (for example,  `application/octet-stream`), then the service attempts to automatically detect the document's media type.       * The configuration to use to process the document can be provided by using the `configuration_id` query parameter.       * The `version` query parameter is still required.    * Returns immediately after the system has accepted the document for processing.    * The user must provide document content, metadata, or both. If the request is missing both document content and metadata, it is  rejected.       * The user can set the `Content-Type` parameter on the `file` part to indicate the media type of the document. If the `Content-Type` parameter is missing or is one of the generic media types (for example, `application/octet-stream`), then the service attempts to automatically detect the document's media type.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <param name="configurationId">The ID of the configuration to use to process the document. If the `configuration` form part is also provided (both are present at the same time), then request will be rejected. (optional)</param>
        /// <param name="file">The content of the document to ingest.The maximum supported file size is 50 megabytes. Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected. Example:  ``` {   "Creator": "Johnny Appleseed",   "Subject": "Apples" } ``` (optional)</param>
        /// <param name="configuration">The configuration to use to process the document. If this part is provided, then the provided configuration is used to process the document. If the `configuration_id` is also provided (both are present at the same time), then request is rejected. The maximum supported configuration size is 1 MB. Configuration parts larger than 1 MB are rejected. See the `GET /configurations/{configuration_id}` operation for an example configuration. (optional)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        DocumentAccepted AddDocument(string environmentId, string collectionId, string configurationId = null, System.IO.Stream file = null, string metadata = null, string configuration = null);

        /// <summary>
        /// Delete a document. If the given document id is invalid, or if the document is not found, then the a success response is returned (HTTP status code `200`) with the status set to 'deleted'.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <param name="documentId">the ID of your document</param>
        /// <returns><see cref="DeleteDocumentResponse" />DeleteDocumentResponse</returns>
        DeleteDocumentResponse DeleteDocument(string environmentId, string collectionId, string documentId);

        /// <summary>
        /// Get document details. Fetch status details about a submitted document. **Note:** this operation does not return the document itself. Instead, it returns only the document's processing status and any notices (warnings or errors) that were generated when the document was ingested. Use the query API to retrieve the actual document content.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <param name="documentId">the ID of your document</param>
        /// <returns><see cref="DocumentStatus" />DocumentStatus</returns>
        DocumentStatus GetDocumentStatus(string environmentId, string collectionId, string documentId);

        /// <summary>
        /// Update a document. Replace an existing document. Starts ingesting a document with optional metadata and optional configurations.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <param name="documentId">the ID of your document</param>
        /// <param name="configurationId">The ID of the configuration to use to process the document. If the `configuration` form part is also provided (both are present at the same time), then request will be rejected. (optional)</param>
        /// <param name="file">The content of the document to ingest.The maximum supported file size is 50 megabytes. Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected. Example:  ``` {   "Creator": "Johnny Appleseed",   "Subject": "Apples" } ``` (optional)</param>
        /// <param name="configuration">The configuration to use to process the document. If this part is provided, then the provided configuration is used to process the document. If the `configuration_id` is also provided (both are present at the same time), then request is rejected. The maximum supported configuration size is 1 MB. Configuration parts larger than 1 MB are rejected. See the `GET /configurations/{configuration_id}` operation for an example configuration. (optional)</param>
        /// <returns><see cref="DocumentAccepted" />DocumentAccepted</returns>
        DocumentAccepted UpdateDocument(string environmentId, string collectionId, string documentId, string configurationId = null, System.IO.Stream file = null, string metadata = null, string configuration = null);
        /// <summary>
        /// Add an environment. Creates a new environment.  You can create only one environment per service instance. An attempt to create another environment will result in an error.  The size of the new environment can be controlled by specifying the `size` parameter. Use the table below to map size values to the size of the environment which is created:  | Size | Disk (GB)  | RAM (GB) | Included Standard Enrichments | Notes | | ---:  | -----------: | -----------: | --------------------------------------------: | -------- | | 0  | 2              | 1              | n/a (effectively unlimited)   | Free Plan only, no HA (single node in elastic.co)| | 1     | 48             | 2              | 4,000    | | | 2     | 192            | 8              | 16,000   | | | 3     | 384            | 16             | 32,000   | |  **Note:** you cannot set the size property when using the free plan.
        /// </summary>
        /// <param name="body">A JSON object where you define an environment name, description, and size.</param>
        /// <returns><see cref="ModelEnvironment" />ModelEnvironment</returns>
        ModelEnvironment CreateEnvironment(CreateEnvironmentRequest body);

        /// <summary>
        /// Delete environment. 
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <returns><see cref="DeleteEnvironmentResponse" />DeleteEnvironmentResponse</returns>
        DeleteEnvironmentResponse DeleteEnvironment(string environmentId);

        /// <summary>
        /// Get environment info. 
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <returns><see cref="ModelEnvironment" />ModelEnvironment</returns>
        ModelEnvironment GetEnvironment(string environmentId);

        /// <summary>
        /// List environments. List existing environments for the service instance.
        /// </summary>
        /// <param name="name">Show only the environment with the given name. (optional)</param>
        /// <returns><see cref="ListEnvironmentsResponse" />ListEnvironmentsResponse</returns>
        ListEnvironmentsResponse ListEnvironments(string name = null);

        /// <summary>
        /// Update an environment. Updates an environment. The environment's `name` and  `description` parameters can be changed. You can increase the value of the `size` parameter. If you need to decrease an environment's size, contact IBM Support.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="body"></param>
        /// <returns><see cref="ModelEnvironment" />ModelEnvironment</returns>
        ModelEnvironment UpdateEnvironment(string environmentId, UpdateEnvironmentRequest body);
        /// <summary>
        /// Query documents. See the [Discovery service documentation](https://console.bluemix.net/docs/services/discovery/using.html) for more details.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <param name="filter">A cacheable query that limits the documents returned to exclude any documents that don't mention the query content. Filter searches are better for metadata type searches and when you are trying to get a sense of concepts in the data set. (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full text, but with the most relevant documents listed first. Use a query search when you want to find the most relevant search results. You cannot use `natural_language_query` and `query` at the same time. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing training data and natural language understanding. You cannot use `natural_language_query` and `query` at the same time. (optional)</param>
        /// <param name="passages">A passages query that returns the most relevant passages from the document. (optional)</param>
        /// <param name="aggregation">An aggregation search uses combinations of filters and query search to return an exact answer. Aggregations are useful for building applications, because you can use them to build lists, tables, and time series. For a full list of possible aggregrations, see the Query reference. (optional)</param>
        /// <param name="count">Number of documents to return (optional, default to 10)</param>
        /// <param name="_return">A comma separated list of the portion of the document hierarchy to return. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number of results that are returned is 10, and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma separated list of fields in the document to sort on. You can optionally specify a sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true a highlight field is returned for each result which contains the fields that match the query with `<em></em>` tags around the matching query terms. Defaults to false. (optional)</param>
        /// <returns><see cref="QueryResponse" />QueryResponse</returns>
        QueryResponse Query(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, string sort = null, bool? highlight = null);

        /// <summary>
        ///  Queries for notices (errors or warnings) that may have been generated by the system. Currently, notices are only generated when ingesting documents. See the [Discovery service documentation](https://console.bluemix.net/docs/services/discovery/using.html) for more details on the query language.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="collectionId">the ID of your collection</param>
        /// <param name="filter">A cacheable query that limits the documents returned to exclude any documents that don't mention the query content. Filter searches are better for metadata type searches and when you are trying to get a sense of concepts in the data set. (optional)</param>
        /// <param name="query">A query search returns all documents in your data set with full enrichments and full text, but with the most relevant documents listed first. Use a query search when you want to find the most relevant search results. You cannot use `natural_language_query` and `query` at the same time. (optional)</param>
        /// <param name="naturalLanguageQuery">A natural language query that returns relevant documents by utilizing training data and natural language understanding. You cannot use `natural_language_query` and `query` at the same time. (optional)</param>
        /// <param name="passages">A passages query that returns the most relevant passages from the document. (optional)</param>
        /// <param name="aggregation">An aggregation search uses combinations of filters and query search to return an exact answer. Aggregations are useful for building applications, because you can use them to build lists, tables, and time series. For a full list of possible aggregrations, see the Query reference. (optional)</param>
        /// <param name="count">Number of documents to return (optional, default to 10)</param>
        /// <param name="_return">A comma separated list of the portion of the document hierarchy to return. (optional)</param>
        /// <param name="offset">The number of query results to skip at the beginning. For example, if the total number of results that are returned is 10, and the offset is 8, it returns the last two results. (optional)</param>
        /// <param name="sort">A comma separated list of fields in the document to sort on. You can optionally specify a sort direction by prefixing the field with `-` for descending or `+` for ascending. Ascending is the default sort direction if no prefix is specified. (optional)</param>
        /// <param name="highlight">When true a highlight field is returned for each result which contains the fields that match the query with `<em></em>` tags around the matching query terms. Defaults to false. (optional)</param>
        /// <returns><see cref="QueryNoticesResponse" />QueryNoticesResponse</returns>
        QueryNoticesResponse QueryNotices(string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, List<string> _return = null, long? offset = null, string sort = null, bool? highlight = null);
        /// <summary>
        /// Test configuration. Runs a sample document through the default or your configuration and returns diagnostic information designed to help you understand how the document was processed. The document is not added to the index.
        /// </summary>
        /// <param name="environmentId">the ID of your environment</param>
        /// <param name="configuration">The configuration to use to process the document. If this part is provided, then the provided configuration is used to process the document. If the `configuration_id` is also provided (both are present at the same time), then request is rejected. The maximum supported configuration size is 1 MB. Configuration parts larger than 1 MB are rejected. See the `GET /configurations/{configuration_id}` operation for an example configuration. (optional)</param>
        /// <param name="step">Specify to only run the input document through the given step instead of running the input document through the entire ingestion workflow. Valid values are `convert`, `enrich`, and `normalize`. (optional)</param>
        /// <param name="configurationId">The ID of the configuration to use to process the document. If the `configuration` form part is also provided (both are present at the same time), then request will be rejected. (optional)</param>
        /// <param name="file">The content of the document to ingest.The maximum supported file size is 50 megabytes. Files larger than 50 megabytes is rejected. (optional)</param>
        /// <param name="metadata">If you're using the Data Crawler to upload your documents, you can test a document against the type of metadata that the Data Crawler might send. The maximum supported metadata file size is 1 MB. Metadata parts larger than 1 MB are rejected. Example:  ``` {   "Creator": "Johnny Appleseed",   "Subject": "Apples" } ``` (optional)</param>
        /// <returns><see cref="TestDocument" />TestDocument</returns>
        TestDocument TestConfigurationInEnvironment(string environmentId, string configuration = null, string step = null, string configurationId = null, System.IO.Stream file = null, string metadata = null);
    }
}
