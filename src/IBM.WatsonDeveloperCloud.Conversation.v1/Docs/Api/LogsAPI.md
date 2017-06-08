# .IBM.WatsonDeveloperCloud.Conversation.v1.LogsAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ListLogs**](LogsAPI.md#listlogs) | **GET** /v1/workspaces/{workspaceId}/logs | List log events.


<a name="listlogs"></a>
# **ListLogs**
> LogCollectionResponse ListLogs (string workspaceId, string sort, string filter, int? pageLimit, string cursor)

List log events.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class ListLogsExample
    {
        public void main()
        {
            
            var apiInstance = new LogsAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var sort = sort_example;  // string | Sorts the response according to the value of the specified property, in ascending or descending order. (optional) 
            var filter = filter_example;  // string | A cacheable parameter that limits the results to those matching the specified filter. (optional) 
            var pageLimit = 56;  // int? | The number of records to return in each page of results. The default page limit is 100. (optional) 
            var cursor = cursor_example;  // string | A token identifying the last value from the previous page of results. (optional) 

            try
            {
                // List log events.
                LogCollectionResponse result = apiInstance.ListLogs(workspaceId, sort, filter, pageLimit, cursor);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling LogsAPI.ListLogs: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **sort** | **string**| Sorts the response according to the value of the specified property, in ascending or descending order. | [optional] 
 **filter** | **string**| A cacheable parameter that limits the results to those matching the specified filter. | [optional] 
 **pageLimit** | **int?**| The number of records to return in each page of results. The default page limit is 100. | [optional] 
 **cursor** | **string**| A token identifying the last value from the previous page of results. | [optional] 

### Return type

[**LogCollectionResponse**](LogCollectionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

