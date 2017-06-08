# .IBM.WatsonDeveloperCloud.Conversation.v1.IntentsAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateIntent**](IntentsAPI.md#createintent) | **POST** /v1/workspaces/{workspaceId}/intents | Create intent.
[**DeleteIntent**](IntentsAPI.md#deleteintent) | **DELETE** /v1/workspaces/{workspaceId}/intents/{intent} | Delete intent.
[**GetIntent**](IntentsAPI.md#getintent) | **GET** /v1/workspaces/{workspaceId}/intents/{intent} | Get intent.
[**ListIntents**](IntentsAPI.md#listintents) | **GET** /v1/workspaces/{workspaceId}/intents | List intents.
[**UpdateIntent**](IntentsAPI.md#updateintent) | **POST** /v1/workspaces/{workspaceId}/intents/{intent} | Update intent.


<a name="createintent"></a>
# **CreateIntent**
> IntentResponse CreateIntent (string workspaceId, CreateIntent body)

Create intent.

Create a new intent.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class CreateIntentExample
    {
        public void main()
        {
            
            var apiInstance = new IntentsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var body = new CreateIntent(); // CreateIntent | A CreateIntent object defining the content of the new intent.

            try
            {
                // Create intent.
                IntentResponse result = apiInstance.CreateIntent(workspaceId, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling IntentsAPI.CreateIntent: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **body** | [**CreateIntent**](CreateIntent.md)| A CreateIntent object defining the content of the new intent. | 

### Return type

[**IntentResponse**](IntentResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteintent"></a>
# **DeleteIntent**
> object DeleteIntent (string workspaceId, string intent)

Delete intent.

Delete an intent from a workspace.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class DeleteIntentExample
    {
        public void main()
        {
            
            var apiInstance = new IntentsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var intent = intent_example;  // string | The intent name (for example, `pizza_order`).

            try
            {
                // Delete intent.
                object result = apiInstance.DeleteIntent(workspaceId, intent);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling IntentsAPI.DeleteIntent: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **intent** | **string**| The intent name (for example, `pizza_order`). | 

### Return type

[**object**](.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getintent"></a>
# **GetIntent**
> IntentExportResponse GetIntent (string workspaceId, string intent, bool? export)

Get intent.

Get information about an intent, optionally including all intent content.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class GetIntentExample
    {
        public void main()
        {
            
            var apiInstance = new IntentsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var intent = intent_example;  // string | The intent name (for example, `pizza_order`).
            var export = true;  // bool? | Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional)  (default to false)

            try
            {
                // Get intent.
                IntentExportResponse result = apiInstance.GetIntent(workspaceId, intent, export);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling IntentsAPI.GetIntent: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **intent** | **string**| The intent name (for example, `pizza_order`). | 
 **export** | **bool?**| Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. | [optional] [default to false]

### Return type

[**IntentExportResponse**](IntentExportResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listintents"></a>
# **ListIntents**
> IntentCollectionResponse ListIntents (string workspaceId, bool? export, int? pageLimit, bool? includeCount, string sort, string cursor)

List intents.

List the intents for a workspace.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class ListIntentsExample
    {
        public void main()
        {
            
            var apiInstance = new IntentsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var export = true;  // bool? | Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional)  (default to false)
            var pageLimit = 56;  // int? | The number of records to return in each page of results. The default page limit is 100. (optional) 
            var includeCount = true;  // bool? | Whether to include information about the number of records returned. (optional)  (default to false)
            var sort = sort_example;  // string | Sorts the response according to the value of the specified property, in ascending or descending order. (optional) 
            var cursor = cursor_example;  // string | A token identifying the last value from the previous page of results. (optional) 

            try
            {
                // List intents.
                IntentCollectionResponse result = apiInstance.ListIntents(workspaceId, export, pageLimit, includeCount, sort, cursor);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling IntentsAPI.ListIntents: " + e.Message );
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

[**IntentCollectionResponse**](IntentCollectionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateintent"></a>
# **UpdateIntent**
> IntentResponse UpdateIntent (string workspaceId, string intent, UpdateIntent body)

Update intent.

Update an existing intent with new or modified data. You must provide data defining the content of the updated intent.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class UpdateIntentExample
    {
        public void main()
        {
            
            var apiInstance = new IntentsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var intent = intent_example;  // string | The intent name (for example, `pizza_order`).
            var body = new UpdateIntent(); // UpdateIntent | An UpdateIntent object defining the updated content of the intent.

            try
            {
                // Update intent.
                IntentResponse result = apiInstance.UpdateIntent(workspaceId, intent, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling IntentsAPI.UpdateIntent: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **intent** | **string**| The intent name (for example, `pizza_order`). | 
 **body** | [**UpdateIntent**](UpdateIntent.md)| An UpdateIntent object defining the updated content of the intent. | 

### Return type

[**IntentResponse**](IntentResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

