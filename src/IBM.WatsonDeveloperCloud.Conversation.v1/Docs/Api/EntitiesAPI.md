# .IBM.WatsonDeveloperCloud.Conversation.v1.EntitiesAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateEntity**](EntitiesAPI.md#createentity) | **POST** /v1/workspaces/{workspaceId}/entities | Create entity.
[**DeleteEntity**](EntitiesAPI.md#deleteentity) | **DELETE** /v1/workspaces/{workspaceId}/entities/{entity} | Delete entity.
[**GetEntity**](EntitiesAPI.md#getentity) | **GET** /v1/workspaces/{workspaceId}/entities/{entity} | Get entity.
[**ListEntities**](EntitiesAPI.md#listentities) | **GET** /v1/workspaces/{workspaceId}/entities | List entities.
[**UpdateEntity**](EntitiesAPI.md#updateentity) | **POST** /v1/workspaces/{workspaceId}/entities/{entity} | Update entity.


<a name="createentity"></a>
# **CreateEntity**
> EntityResponse CreateEntity (string workspaceId, CreateEntity body)

Create entity.

Create a new entity.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class CreateEntityExample
    {
        public void main()
        {
            
            var apiInstance = new EntitiesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var body = new CreateEntity(); // CreateEntity | A CreateEntity object defining the content of the new entity.

            try
            {
                // Create entity.
                EntityResponse result = apiInstance.CreateEntity(workspaceId, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EntitiesAPI.CreateEntity: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **body** | [**CreateEntity**](CreateEntity.md)| A CreateEntity object defining the content of the new entity. | 

### Return type

[**EntityResponse**](EntityResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteentity"></a>
# **DeleteEntity**
> object DeleteEntity (string workspaceId, string entity)

Delete entity.

Delete an entity from a workspace.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class DeleteEntityExample
    {
        public void main()
        {
            
            var apiInstance = new EntitiesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.

            try
            {
                // Delete entity.
                object result = apiInstance.DeleteEntity(workspaceId, entity);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EntitiesAPI.DeleteEntity: " + e.Message );
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

### Return type

[**object**](.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getentity"></a>
# **GetEntity**
> EntityExportResponse GetEntity (string workspaceId, string entity, bool? export)

Get entity.

Get information about an entity, optionally including all entity content.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class GetEntityExample
    {
        public void main()
        {
            
            var apiInstance = new EntitiesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var export = true;  // bool? | Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional)  (default to false)

            try
            {
                // Get entity.
                EntityExportResponse result = apiInstance.GetEntity(workspaceId, entity, export);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EntitiesAPI.GetEntity: " + e.Message );
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
 **export** | **bool?**| Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. | [optional] [default to false]

### Return type

[**EntityExportResponse**](EntityExportResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listentities"></a>
# **ListEntities**
> EntityCollectionResponse ListEntities (string workspaceId, bool? export, int? pageLimit, bool? includeCount, string sort, string cursor)

List entities.

List the entities for a workspace.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class ListEntitiesExample
    {
        public void main()
        {
            
            var apiInstance = new EntitiesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var export = true;  // bool? | Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional)  (default to false)
            var pageLimit = 56;  // int? | The number of records to return in each page of results. The default page limit is 100. (optional) 
            var includeCount = true;  // bool? | Whether to include information about the number of records returned. (optional)  (default to false)
            var sort = sort_example;  // string | Sorts the response according to the value of the specified property, in ascending or descending order. (optional) 
            var cursor = cursor_example;  // string | A token identifying the last value from the previous page of results. (optional) 

            try
            {
                // List entities.
                EntityCollectionResponse result = apiInstance.ListEntities(workspaceId, export, pageLimit, includeCount, sort, cursor);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EntitiesAPI.ListEntities: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **export** | **bool?**| Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. | [optional] [default to false]
 **pageLimit** | **int?**| The number of records to return in each page of results. The default page limit is 100. | [optional] 
 **includeCount** | **bool?**| Whether to include information about the number of records returned. | [optional] [default to false]
 **sort** | **string**| Sorts the response according to the value of the specified property, in ascending or descending order. | [optional] 
 **cursor** | **string**| A token identifying the last value from the previous page of results. | [optional] 

### Return type

[**EntityCollectionResponse**](EntityCollectionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateentity"></a>
# **UpdateEntity**
> EntityResponse UpdateEntity (string workspaceId, string entity, UpdateEntity body)

Update entity.

Update an existing entity with new or modified data.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class UpdateEntityExample
    {
        public void main()
        {
            
            var apiInstance = new EntitiesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var body = new UpdateEntity(); // UpdateEntity | An UpdateEntity object defining the updated content of the entity.

            try
            {
                // Update entity.
                EntityResponse result = apiInstance.UpdateEntity(workspaceId, entity, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EntitiesAPI.UpdateEntity: " + e.Message );
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
 **body** | [**UpdateEntity**](UpdateEntity.md)| An UpdateEntity object defining the updated content of the entity. | 

### Return type

[**EntityResponse**](EntityResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

