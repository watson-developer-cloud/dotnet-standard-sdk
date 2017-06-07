### Discovery
The IBM Watson™ [Discovery][discovery] service will be abstracted into the .NET Standard SDK in the future.
<!-- The IBM Watson™ [Discovery][discovery] service makes it possible to rapidly build cognitive, cloud-based exploration applications that unlock actionable insights hidden in unstructured data - including your own proprietary data, as well as public and third-party data. -->

<!-- ### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.Discovery

```
#### Project.json
```JSON

"dependencies": {
   "IBM.WatsonDeveloperCloud.Discovery.v1": "0.2.0"
}

``` -->
<!-- ### Usage
The IBM Watson™ [Discovery][discovery] Service uses data analysis combined with cognitive intuition in order to take your unstructured data and enrich it so that you can query it to return the information that you need from it. -->

<!-- #### Create an environment
Creates an environment for the service instance. Note: You can create only one environment per service instance. Attempting to create another environment for the same service instance results in an error.
```cs
``` -->

<!-- #### List environments
List existing environments for the service instance.
```cs
``` -->

<!-- #### List environment details
Gets detailed information about the specified environment.
```cs
``` -->

<!-- #### Update an environment
Updates an existing environment.
```cs
``` -->

<!-- #### Delete an environment
Deletes an existing environment.
```cs
``` -->

<!-- #### Add an configuration
Adds a configuration to the service instance.
```cs
``` -->

<!-- #### List configurations
Lists existing configurations for the service instance.
```cs
``` -->

<!-- ##### List configuration details
Get information about the specified configuration.
```cs
``` -->

<!-- ##### Update a configuration
Replaces an existing configuration. This operation completely replaces the previous configuration. Important: Do not attempt to replace the default configuration.

The new configuration can contain one or more of the configuration_id, updated, or created elements, but the elements are ignored and do not generate errors to enable pasting in another existing configuration. You can also provide a new configuration with none of the three elements.

Documents are processed with a snapshot of the configuration that was in place at the time the document was submitted for ingestion. This means documents that were already submitted are not processed with the new configuration.
```cs
``` -->

<!-- ##### Delete a configuration
Deletes an existing configuration from the service instance.

The delete operation is performed unconditionally. A delete request succeeds even if the configuration is referenced by a collection or document ingestion. However, documents that have already been submitted for processing continue to use the deleted configuration; documents are always processed with a snapshot of the configuration as it existed at the time the document was submitted.
```cs
``` -->

<!-- ##### Test your configuration on a document
Run a sample document against your configuration or the default configuration and return diagnostic information designed to help you understand how the document was processed. The document is not added to a collection.
```cs
``` -->

<!-- ##### Create an collection
Creates a new collection for storing documents.
```cs
``` -->

<!-- ##### List collections
Display a list of existing collections.
```cs
``` -->

<!-- ##### List collection details
Show detailed information about an existing collection.
```cs
``` -->

<!-- ##### Update an collection
Creates a new collection for storing documents.
```cs
``` -->

<!-- ##### List fields
Gets a list of the unique fields, and each field's type, that are stored in a collection's index.
```cs
``` -->

<!-- ##### Delete an collection
Deletes an existing collection.
```cs
``` -->

<!-- ##### Add a document
Add a document to your collection.
```cs
``` -->

<!-- ##### Update a document
Update or partially update a document to create or replace an existing document.
```cs
``` -->

<!-- ##### List document details
Display status information about a submitted document.
```cs
``` -->

<!-- ##### Delete a document
Delete a document from a collection.
```cs
``` -->

<!-- ##### Queries
Query the documents in your collection.

Once your content is uploaded and enriched by the Discovery service, you can build queries to search your content. For a deep dive into queries, see [Building Queries and Delivering Content][discovery-query].
```cs
``` -->

[discovery]: http://www.ibm.com/watson/developercloud/discovery/api/v1/
[discovery-query]: http://www.ibm.com/watson/developercloud/doc/discovery/using.shtml
