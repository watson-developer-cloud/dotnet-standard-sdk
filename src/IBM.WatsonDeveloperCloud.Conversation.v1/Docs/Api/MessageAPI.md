# .IBM.WatsonDeveloperCloud.Conversation.v1.MessageAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Message**](MessageAPI.md#message) | **POST** /v1/workspaces/{workspaceId}/message | Get a response to a user's input.


<a name="message"></a>
# **Message**
> MessageResponse Message (string workspaceId, MessageRequest body)

Get a response to a user's input.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class MessageExample
    {
        public void main()
        {
            
            var apiInstance = new MessageAPI();
            var workspaceId = workspaceId_example;  // string | Unique identifier of the workspace.
            var body = new MessageRequest(); // MessageRequest | The user's input, with optional intents, entities, and other properties from the response. (optional) 

            try
            {
                // Get a response to a user's input.
                MessageResponse result = apiInstance.Message(workspaceId, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MessageAPI.Message: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| Unique identifier of the workspace. | 
 **body** | [**MessageRequest**](MessageRequest.md)| The user's input, with optional intents, entities, and other properties from the response. | [optional] 

### Return type

[**MessageResponse**](MessageResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

