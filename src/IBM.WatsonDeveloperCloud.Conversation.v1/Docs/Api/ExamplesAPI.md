# .IBM.WatsonDeveloperCloud.Conversation.v1.ExamplesAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateExample**](ExamplesAPI.md#createexample) | **POST** /v1/workspaces/{workspaceId}/intents/{intent}/examples | Create user input example.
[**DeleteExample**](ExamplesAPI.md#deleteexample) | **DELETE** /v1/workspaces/{workspaceId}/intents/{intent}/examples/{text} | Delete user input example.
[**GetExample**](ExamplesAPI.md#getexample) | **GET** /v1/workspaces/{workspaceId}/intents/{intent}/examples/{text} | Get user input example.
[**ListExamples**](ExamplesAPI.md#listexamples) | **GET** /v1/workspaces/{workspaceId}/intents/{intent}/examples | List user input examples.
[**UpdateExample**](ExamplesAPI.md#updateexample) | **POST** /v1/workspaces/{workspaceId}/intents/{intent}/examples/{text} | Update user input example.


<a name="createexample"></a>
# **CreateExample**
> ExampleResponse CreateExample (string workspaceId, string intent, CreateExample body)

Create user input example.

Add a new user input example to an intent.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class CreateExampleExample
    {
        public void main()
        {
            
            var apiInstance = new ExamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var intent = intent_example;  // string | The intent name (for example, `pizza_order`).
            var body = new CreateExample(); // CreateExample | A CreateExample object defining the content of the new user input example.

            try
            {
                // Create user input example.
                ExampleResponse result = apiInstance.CreateExample(workspaceId, intent, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ExamplesAPI.CreateExample: " + e.Message );
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
 **body** | [**CreateExample**](CreateExample.md)| A CreateExample object defining the content of the new user input example. | 

### Return type

[**ExampleResponse**](ExampleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteexample"></a>
# **DeleteExample**
> object DeleteExample (string workspaceId, string intent, string text)

Delete user input example.

Delete a user input example from an intent.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class DeleteExampleExample
    {
        public void main()
        {
            
            var apiInstance = new ExamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var intent = intent_example;  // string | The intent name (for example, `pizza_order`).
            var text = text_example;  // string | The text of the user input example.

            try
            {
                // Delete user input example.
                object result = apiInstance.DeleteExample(workspaceId, intent, text);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ExamplesAPI.DeleteExample: " + e.Message );
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
 **text** | **string**| The text of the user input example. | 

### Return type

[**object**](.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getexample"></a>
# **GetExample**
> ExampleResponse GetExample (string workspaceId, string intent, string text)

Get user input example.

Get information about a user input example.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class GetExampleExample
    {
        public void main()
        {
            
            var apiInstance = new ExamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var intent = intent_example;  // string | The intent name (for example, `pizza_order`).
            var text = text_example;  // string | The text of the user input example.

            try
            {
                // Get user input example.
                ExampleResponse result = apiInstance.GetExample(workspaceId, intent, text);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ExamplesAPI.GetExample: " + e.Message );
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
 **text** | **string**| The text of the user input example. | 

### Return type

[**ExampleResponse**](ExampleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listexamples"></a>
# **ListExamples**
> ExampleCollectionResponse ListExamples (string workspaceId, string intent, int? pageLimit, bool? includeCount, string sort, string cursor)

List user input examples.

List the user input examples for an intent.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class ListExamplesExample
    {
        public void main()
        {
            
            var apiInstance = new ExamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var intent = intent_example;  // string | The intent name (for example, `pizza_order`).
            var pageLimit = 56;  // int? | The number of records to return in each page of results. The default page limit is 100. (optional) 
            var includeCount = true;  // bool? | Whether to include information about the number of records returned. (optional)  (default to false)
            var sort = sort_example;  // string | Sorts the response according to the value of the specified property, in ascending or descending order. (optional) 
            var cursor = cursor_example;  // string | A token identifying the last value from the previous page of results. (optional) 

            try
            {
                // List user input examples.
                ExampleCollectionResponse result = apiInstance.ListExamples(workspaceId, intent, pageLimit, includeCount, sort, cursor);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ExamplesAPI.ListExamples: " + e.Message );
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
 **pageLimit** | **int?**| The number of records to return in each page of results. The default page limit is 100. | [optional] 
 **includeCount** | **bool?**| Whether to include information about the number of records returned. | [optional] [default to false]
 **sort** | **string**| Sorts the response according to the value of the specified property, in ascending or descending order. | [optional] 
 **cursor** | **string**| A token identifying the last value from the previous page of results. | [optional] 

### Return type

[**ExampleCollectionResponse**](ExampleCollectionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateexample"></a>
# **UpdateExample**
> ExampleResponse UpdateExample (string workspaceId, string intent, string text, UpdateExample body)

Update user input example.

Update the text of a user input example.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class UpdateExampleExample
    {
        public void main()
        {
            
            var apiInstance = new ExamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var intent = intent_example;  // string | The intent name (for example, `pizza_order`).
            var text = text_example;  // string | The text of the user input example.
            var body = new UpdateExample(); // UpdateExample | An UpdateExample object defining the new text for the user input example.

            try
            {
                // Update user input example.
                ExampleResponse result = apiInstance.UpdateExample(workspaceId, intent, text, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ExamplesAPI.UpdateExample: " + e.Message );
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
 **text** | **string**| The text of the user input example. | 
 **body** | [**UpdateExample**](UpdateExample.md)| An UpdateExample object defining the new text for the user input example. | 

### Return type

[**ExampleResponse**](ExampleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

