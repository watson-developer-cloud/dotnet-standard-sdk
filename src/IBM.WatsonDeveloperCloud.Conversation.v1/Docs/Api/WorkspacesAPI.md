# .IBM.WatsonDeveloperCloud.Conversation.v1.WorkspacesAPI

All URIs are relative to *http://localhost/conversation/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateWorkspace**](WorkspacesAPI.md#createworkspace) | **POST** /v1/workspaces | Create workspace.
[**DeleteWorkspace**](WorkspacesAPI.md#deleteworkspace) | **DELETE** /v1/workspaces/{workspaceId} | Delete workspace.
[**GetWorkspace**](WorkspacesAPI.md#getworkspace) | **GET** /v1/workspaces/{workspaceId} | Get information about a workspace.
[**ListWorkspaces**](WorkspacesAPI.md#listworkspaces) | **GET** /v1/workspaces | List workspaces.
[**UpdateWorkspace**](WorkspacesAPI.md#updateworkspace) | **POST** /v1/workspaces/{workspaceId} | Update workspace.


<a name="createworkspace"></a>
# **CreateWorkspace**
> WorkspaceResponse CreateWorkspace (CreateWorkspace body)

Create workspace.

Create a workspace based on component objects. You must provide workspace components defining the content of the new workspace.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class CreateWorkspaceExample
    {
        public void main()
        {
            
            var apiInstance = new WorkspacesAPI();
            var body = new CreateWorkspace(); // CreateWorkspace | Valid data defining the content of the new workspace. (optional) 

            try
            {
                // Create workspace.
                WorkspaceResponse result = apiInstance.CreateWorkspace(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WorkspacesAPI.CreateWorkspace: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CreateWorkspace**](CreateWorkspace.md)| Valid data defining the content of the new workspace. | [optional] 

### Return type

[**WorkspaceResponse**](WorkspaceResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteworkspace"></a>
# **DeleteWorkspace**
> object DeleteWorkspace (string workspaceId)

Delete workspace.

Delete a workspace from the service instance.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class DeleteWorkspaceExample
    {
        public void main()
        {
            
            var apiInstance = new WorkspacesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.

            try
            {
                // Delete workspace.
                object result = apiInstance.DeleteWorkspace(workspaceId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WorkspacesAPI.DeleteWorkspace: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 

### Return type

[**object**](.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getworkspace"></a>
# **GetWorkspace**
> WorkspaceExportResponse GetWorkspace (string workspaceId, bool? export)

Get information about a workspace.

Get information about a workspace, optionally including all workspace content.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class GetWorkspaceExample
    {
        public void main()
        {
            
            var apiInstance = new WorkspacesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var export = true;  // bool? | Whether to include all element content in the returned data. If export=`false`, the returned data includes only information about the element itself. If export=`true`, all content, including subelements, is included. The default value is `false`. (optional)  (default to false)

            try
            {
                // Get information about a workspace.
                WorkspaceExportResponse result = apiInstance.GetWorkspace(workspaceId, export);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WorkspacesAPI.GetWorkspace: " + e.Message );
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

### Return type

[**WorkspaceExportResponse**](WorkspaceExportResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listworkspaces"></a>
# **ListWorkspaces**
> WorkspaceCollectionResponse ListWorkspaces (int? pageLimit, bool? includeCount, string sort, string cursor)

List workspaces.

List the workspaces associated with a Conversation service instance.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class ListWorkspacesExample
    {
        public void main()
        {
            
            var apiInstance = new WorkspacesAPI();
            var pageLimit = 56;  // int? | The number of records to return in each page of results. The default page limit is 100. (optional) 
            var includeCount = true;  // bool? | Whether to include information about the number of records returned. (optional)  (default to false)
            var sort = sort_example;  // string | Sorts the response according to the value of the specified property, in ascending or descending order. (optional) 
            var cursor = cursor_example;  // string | A token identifying the last value from the previous page of results. (optional) 

            try
            {
                // List workspaces.
                WorkspaceCollectionResponse result = apiInstance.ListWorkspaces(pageLimit, includeCount, sort, cursor);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WorkspacesAPI.ListWorkspaces: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **pageLimit** | **int?**| The number of records to return in each page of results. The default page limit is 100. | [optional] 
 **includeCount** | **bool?**| Whether to include information about the number of records returned. | [optional] [default to false]
 **sort** | **string**| Sorts the response according to the value of the specified property, in ascending or descending order. | [optional] 
 **cursor** | **string**| A token identifying the last value from the previous page of results. | [optional] 

### Return type

[**WorkspaceCollectionResponse**](WorkspaceCollectionResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateworkspace"></a>
# **UpdateWorkspace**
> WorkspaceResponse UpdateWorkspace (string workspaceId, UpdateWorkspace body)

Update workspace.

Update an existing workspace with new or modified data. You must provide component objects defining the content of the updated workspace.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.Conversation.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.Conversation.v1.Model;

namespace Example
{
    public class UpdateWorkspaceExample
    {
        public void main()
        {
            
            var apiInstance = new WorkspacesAPI();
            var workspaceId = workspaceId_example;  // string | The workspace ID.
            var body = new UpdateWorkspace(); // UpdateWorkspace | Valid data defining the new workspace content. Any elements included in the new data will completely replace the existing elements, including all subelements. Previously existing subelements are not retained unless they are included in the new data. (optional) 

            try
            {
                // Update workspace.
                WorkspaceResponse result = apiInstance.UpdateWorkspace(workspaceId, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WorkspacesAPI.UpdateWorkspace: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **workspaceId** | **string**| The workspace ID. | 
 **body** | [**UpdateWorkspace**](UpdateWorkspace.md)| Valid data defining the new workspace content. Any elements included in the new data will completely replace the existing elements, including all subelements. Previously existing subelements are not retained unless they are included in the new data. | [optional] 

### Return type

[**WorkspaceResponse**](WorkspaceResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

