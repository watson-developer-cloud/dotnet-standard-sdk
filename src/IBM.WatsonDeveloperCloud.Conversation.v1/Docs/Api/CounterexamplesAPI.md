# .IBM.WatsonDeveloperCloud.Conversation.v1.CounterexamplesAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateCounterexample**](CounterexamplesAPI.md#createcounterexample) | **POST** /v1/workspaces/{workspaceId}/counterexamples | Create counterexample.
[**DeleteCounterexample**](CounterexamplesAPI.md#deletecounterexample) | **DELETE** /v1/workspaces/{workspaceId}/counterexamples/{text} | Delete counterexample.
[**GetCounterexample**](CounterexamplesAPI.md#getcounterexample) | **GET** /v1/workspaces/{workspaceId}/counterexamples/{text} | Get counterexample.
[**ListCounterexamples**](CounterexamplesAPI.md#listcounterexamples) | **GET** /v1/workspaces/{workspaceId}/counterexamples | List counterexamples.
[**UpdateCounterexample**](CounterexamplesAPI.md#updatecounterexample) | **POST** /v1/workspaces/{workspaceId}/counterexamples/{text} | Update counterexample.


<a name="createcounterexample"></a>
# **CreateCounterexample**
> ExampleResponse CreateCounterexample (string workspaceId, CreateExample body)

Create counterexample.

Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant input.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class CreateCounterexampleExample
    {
        public void main()
        {
            
            var apiInstance = new CounterexamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var body = new CreateExample(); // CreateExample | A CreateExample object defining the content of the new user input example.

            try
            {
                // Create counterexample.
                ExampleResponse result = apiInstance.CreateCounterexample(workspaceId, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CounterexamplesAPI.CreateCounterexample: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **body** | [**CreateExample**](CreateExample.md)| A CreateExample object defining the content of the new user input example. | 

### Return type

[**ExampleResponse**](ExampleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletecounterexample"></a>
# **DeleteCounterexample**
> object DeleteCounterexample (string workspaceId, string text)

Delete counterexample.

Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant input.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class DeleteCounterexampleExample
    {
        public void main()
        {
            
            var apiInstance = new CounterexamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var text = text_example;  // string | The text of a user input counterexample (for example, `What are you wearing?`).

            try
            {
                // Delete counterexample.
                object result = apiInstance.DeleteCounterexample(workspaceId, text);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CounterexamplesAPI.DeleteCounterexample: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **text** | **string**| The text of a user input counterexample (for example, `What are you wearing?`). | 

### Return type

[**object**](.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getcounterexample"></a>
# **GetCounterexample**
> ExampleResponse GetCounterexample (string workspaceId, string text)

Get counterexample.

Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant input.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class GetCounterexampleExample
    {
        public void main()
        {
            
            var apiInstance = new CounterexamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var text = text_example;  // string | The text of a user input counterexample (for example, `What are you wearing?`).

            try
            {
                // Get counterexample.
                ExampleResponse result = apiInstance.GetCounterexample(workspaceId, text);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CounterexamplesAPI.GetCounterexample: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **text** | **string**| The text of a user input counterexample (for example, `What are you wearing?`). | 

### Return type

[**ExampleResponse**](ExampleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listcounterexamples"></a>
# **ListCounterexamples**
> CounterexampleCollectionResponse ListCounterexamples (string workspaceId, int? pageLimit, bool? includeCount, string sort, string cursor)

List counterexamples.

List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant input.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class ListCounterexamplesExample
    {
        public void main()
        {
            
            var apiInstance = new CounterexamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var pageLimit = 56;  // int? | The number of records to return in each page of results. The default page limit is 100. (optional) 
            var includeCount = true;  // bool? | Whether to include information about the number of records returned. (optional)  (default to false)
            var sort = sort_example;  // string | Sorts the response according to the value of the specified property, in ascending or descending order. (optional) 
            var cursor = cursor_example;  // string | A token identifying the last value from the previous page of results. (optional) 

            try
            {
                // List counterexamples.
                CounterexampleCollectionResponse result = apiInstance.ListCounterexamples(workspaceId, pageLimit, includeCount, sort, cursor);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CounterexamplesAPI.ListCounterexamples: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **pageLimit** | **int?**| The number of records to return in each page of results. The default page limit is 100. | [optional] 
 **includeCount** | **bool?**| Whether to include information about the number of records returned. | [optional] [default to false]
 **sort** | **string**| Sorts the response according to the value of the specified property, in ascending or descending order. | [optional] 
 **cursor** | **string**| A token identifying the last value from the previous page of results. | [optional] 

### Return type

[**CounterexampleCollectionResponse**](CounterexampleCollectionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatecounterexample"></a>
# **UpdateCounterexample**
> ExampleResponse UpdateCounterexample (string workspaceId, string text, UpdateExample body)

Update counterexample.

Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class UpdateCounterexampleExample
    {
        public void main()
        {
            
            var apiInstance = new CounterexamplesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var text = text_example;  // string | The text of a user input counterexample (for example, `What are you wearing?`).
            var body = new UpdateExample(); // UpdateExample | An UpdateExample object defining the new text for the counterexample.

            try
            {
                // Update counterexample.
                ExampleResponse result = apiInstance.UpdateCounterexample(workspaceId, text, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CounterexamplesAPI.UpdateCounterexample: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **text** | **string**| The text of a user input counterexample (for example, `What are you wearing?`). | 
 **body** | [**UpdateExample**](UpdateExample.md)| An UpdateExample object defining the new text for the counterexample. | 

### Return type

[**ExampleResponse**](ExampleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

