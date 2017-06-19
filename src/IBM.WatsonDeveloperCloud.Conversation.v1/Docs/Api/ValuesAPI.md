# .IBM.WatsonDeveloperCloud.Conversation.v1.ValuesAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateValue**](ValuesAPI.md#createvalue) | **POST** /v1/workspaces/{workspaceId}/entities/{entity}/values | Add entity value.
[**DeleteValue**](ValuesAPI.md#deletevalue) | **DELETE** /v1/workspaces/{workspaceId}/entities/{entity}/values/{value} | Delete entity value.
[**GetValue**](ValuesAPI.md#getvalue) | **GET** /v1/workspaces/{workspaceId}/entities/{entity}/values/{value} | Get entity value.
[**ListValues**](ValuesAPI.md#listvalues) | **GET** /v1/workspaces/{workspaceId}/entities/{entity}/values | List entity values.
[**UpdateValue**](ValuesAPI.md#updatevalue) | **POST** /v1/workspaces/{workspaceId}/entities/{entity}/values/{value} | Update entity value.


<a name="createvalue"></a>
# **CreateValue**
> ValueResponse CreateValue (string workspaceId, string entity, CreateValue body)

Add entity value.

Create a new value for an entity.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class CreateValueExample
    {
        public void main()
        {
            
            var apiInstance = new ValuesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var body = new CreateValue(); // CreateValue | A CreateValue object defining the content of the new value for the entity.

            try
            {
                // Add entity value.
                ValueResponse result = apiInstance.CreateValue(workspaceId, entity, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ValuesAPI.CreateValue: " + e.Message );
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
 **body** | [**CreateValue**](CreateValue.md)| A CreateValue object defining the content of the new value for the entity. | 

### Return type

[**ValueResponse**](ValueResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletevalue"></a>
# **DeleteValue**
> object DeleteValue (string workspaceId, string entity, string value)

Delete entity value.

Delete a value for an entity.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class DeleteValueExample
    {
        public void main()
        {
            
            var apiInstance = new ValuesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var value = value_example;  // string | The text of the entity value.

            try
            {
                // Delete entity value.
                object result = apiInstance.DeleteValue(workspaceId, entity, value);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ValuesAPI.DeleteValue: " + e.Message );
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

### Return type

[**object**](.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getvalue"></a>
# **GetValue**
> ValueExportResponse GetValue (string workspaceId, string entity, string value, bool? export)

Get entity value.

Get information about an entity value.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class GetValueExample
    {
        public void main()
        {
            
            var apiInstance = new ValuesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var value = value_example;  // string | The text of the entity value.
            var export = true;  // bool? | Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional)  (default to false)

            try
            {
                // Get entity value.
                ValueExportResponse result = apiInstance.GetValue(workspaceId, entity, value, export);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ValuesAPI.GetValue: " + e.Message );
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
 **export** | **bool?**| Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. | [optional] [default to false]

### Return type

[**ValueExportResponse**](ValueExportResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listvalues"></a>
# **ListValues**
> ValueCollectionResponse ListValues (string workspaceId, string entity, bool? export, int? pageLimit, bool? includeCount, string sort, string cursor)

List entity values.

List the values for an entity.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class ListValuesExample
    {
        public void main()
        {
            
            var apiInstance = new ValuesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var export = true;  // bool? | Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional)  (default to false)
            var pageLimit = 56;  // int? | The number of records to return in each page of results. The default page limit is 100. (optional) 
            var includeCount = true;  // bool? | Whether to include information about the number of records returned. (optional)  (default to false)
            var sort = sort_example;  // string | Sorts the response according to the value of the specified property, in ascending or descending order. (optional) 
            var cursor = cursor_example;  // string | A token identifying the last value from the previous page of results. (optional) 

            try
            {
                // List entity values.
                ValueCollectionResponse result = apiInstance.ListValues(workspaceId, entity, export, pageLimit, includeCount, sort, cursor);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ValuesAPI.ListValues: " + e.Message );
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
 **pageLimit** | **int?**| The number of records to return in each page of results. The default page limit is 100. | [optional] 
 **includeCount** | **bool?**| Whether to include information about the number of records returned. | [optional] [default to false]
 **sort** | **string**| Sorts the response according to the value of the specified property, in ascending or descending order. | [optional] 
 **cursor** | **string**| A token identifying the last value from the previous page of results. | [optional] 

### Return type

[**ValueCollectionResponse**](ValueCollectionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatevalue"></a>
# **UpdateValue**
> ValueResponse UpdateValue (string workspaceId, string entity, string value, UpdateValue body)

Update entity value.

Update the content of a value for an entity.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class UpdateValueExample
    {
        public void main()
        {
            
            var apiInstance = new ValuesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var entity = entity_example;  // string | The name of the entity.
            var value = value_example;  // string | The text of the entity value.
            var body = new UpdateValue(); // UpdateValue | An UpdateValue object defining the new content for value for the entity.

            try
            {
                // Update entity value.
                ValueResponse result = apiInstance.UpdateValue(workspaceId, entity, value, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ValuesAPI.UpdateValue: " + e.Message );
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
 **body** | [**UpdateValue**](UpdateValue.md)| An UpdateValue object defining the new content for value for the entity. | 

### Return type

[**ValueResponse**](ValueResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

