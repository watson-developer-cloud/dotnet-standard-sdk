# .IBM.WatsonDeveloperCloud.Conversation.v1.SynonymsAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateSynonym**](SynonymsAPI.md#createsynonym) | **POST** /v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms | Add entity value synonym.
[**DeleteSynonym**](SynonymsAPI.md#deletesynonym) | **DELETE** /v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym} | Delete entity value synonym.
[**GetSynonym**](SynonymsAPI.md#getsynonym) | **GET** /v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym} | Get entity value synonym.
[**ListSynonyms**](SynonymsAPI.md#listsynonyms) | **GET** /v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms | List entity value synonyms.
[**UpdateSynonym**](SynonymsAPI.md#updatesynonym) | **POST** /v1/workspaces/{workspaceId}/entities/{entity}/values/{value}/synonyms/{synonym} | Update entity value synonym.


<a name="createsynonym"></a>
# **CreateSynonym**
> SynonymResponse CreateSynonym (string workspaceId, string entity, string value, CreateSynonym body)

Add entity value synonym.

Add a new synonym to an entity value.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class CreateSynonymExample
    {
        public void main()
        {
            
            var apiInstance = new SynonymsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var value = value_example;  // string | The text of the entity value.
            var body = new CreateSynonym(); // CreateSynonym | A CreateSynonym object defining the new synonym for the entity value.

            try
            {
                // Add entity value synonym.
                SynonymResponse result = apiInstance.CreateSynonym(workspaceId, entity, value, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SynonymsAPI.CreateSynonym: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **entity** | **string**| The name of the entity. | 
 **value** | **string**| The text of the entity value. | 
 **body** | [**CreateSynonym**](CreateSynonym.md)| A CreateSynonym object defining the new synonym for the entity value. | 

### Return type

[**SynonymResponse**](SynonymResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletesynonym"></a>
# **DeleteSynonym**
> object DeleteSynonym (string workspaceId, string entity, string value, string synonym)

Delete entity value synonym.

Delete a synonym for an entity value.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class DeleteSynonymExample
    {
        public void main()
        {
            
            var apiInstance = new SynonymsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var value = value_example;  // string | The text of the entity value.
            var synonym = synonym_example;  // string | The text of the synonym.

            try
            {
                // Delete entity value synonym.
                object result = apiInstance.DeleteSynonym(workspaceId, entity, value, synonym);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SynonymsAPI.DeleteSynonym: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **entity** | **string**| The name of the entity. | 
 **value** | **string**| The text of the entity value. | 
 **synonym** | **string**| The text of the synonym. | 

### Return type

[**object**](.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getsynonym"></a>
# **GetSynonym**
> SynonymResponse GetSynonym (string workspaceId, string entity, string value, string synonym)

Get entity value synonym.

Get information about a synonym for an entity value.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class GetSynonymExample
    {
        public void main()
        {
            
            var apiInstance = new SynonymsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var value = value_example;  // string | The text of the entity value.
            var synonym = synonym_example;  // string | The text of the synonym.

            try
            {
                // Get entity value synonym.
                SynonymResponse result = apiInstance.GetSynonym(workspaceId, entity, value, synonym);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SynonymsAPI.GetSynonym: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **entity** | **string**| The name of the entity. | 
 **value** | **string**| The text of the entity value. | 
 **synonym** | **string**| The text of the synonym. | 

### Return type

[**SynonymResponse**](SynonymResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listsynonyms"></a>
# **ListSynonyms**
> SynonymCollectionResponse ListSynonyms (string workspaceId, string entity, string value, int? pageLimit, bool? includeCount, string sort, string cursor)

List entity value synonyms.

List the synonyms for an entity value.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class ListSynonymsExample
    {
        public void main()
        {
            
            var apiInstance = new SynonymsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var value = value_example;  // string | The text of the entity value.
            var pageLimit = 56;  // int? | The number of records to return in each page of results. The default page limit is 100. (optional) 
            var includeCount = true;  // bool? | Whether to include information about the number of records returned. (optional)  (default to false)
            var sort = sort_example;  // string | Sorts the response according to the value of the specified property, in ascending or descending order. (optional) 
            var cursor = cursor_example;  // string | A token identifying the last value from the previous page of results. (optional) 

            try
            {
                // List entity value synonyms.
                SynonymCollectionResponse result = apiInstance.ListSynonyms(workspaceId, entity, value, pageLimit, includeCount, sort, cursor);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SynonymsAPI.ListSynonyms: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **entity** | **string**| The name of the entity. | 
 **value** | **string**| The text of the entity value. | 
 **pageLimit** | **int?**| The number of records to return in each page of results. The default page limit is 100. | [optional] 
 **includeCount** | **bool?**| Whether to include information about the number of records returned. | [optional] [default to false]
 **sort** | **string**| Sorts the response according to the value of the specified property, in ascending or descending order. | [optional] 
 **cursor** | **string**| A token identifying the last value from the previous page of results. | [optional] 

### Return type

[**SynonymCollectionResponse**](SynonymCollectionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatesynonym"></a>
# **UpdateSynonym**
> SynonymResponse UpdateSynonym (string workspaceId, string entity, string value, string synonym, UpdateSynonym body)

Update entity value synonym.

Update the information about a synonym for an entity value.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class UpdateSynonymExample
    {
        public void main()
        {
            
            var apiInstance = new SynonymsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var value = value_example;  // string | The text of the entity value.
            var synonym = synonym_example;  // string | The text of the synonym.
            var body = new UpdateSynonym(); // UpdateSynonym | An UpdateSynonym object defining the new information for an entity value synonym.

            try
            {
                // Update entity value synonym.
                SynonymResponse result = apiInstance.UpdateSynonym(workspaceId, entity, value, synonym, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SynonymsAPI.UpdateSynonym: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **entity** | **string**| The name of the entity. | 
 **value** | **string**| The text of the entity value. | 
 **synonym** | **string**| The text of the synonym. | 
 **body** | [**UpdateSynonym**](UpdateSynonym.md)| An UpdateSynonym object defining the new information for an entity value synonym. | 

### Return type

[**SynonymResponse**](SynonymResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

