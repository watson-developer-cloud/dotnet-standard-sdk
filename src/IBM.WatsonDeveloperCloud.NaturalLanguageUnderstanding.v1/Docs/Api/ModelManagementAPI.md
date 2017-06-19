# .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.ModelManagementAPI

All URIs are relative to *https://localhost/natural-language-understanding/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeleteModel**](ModelManagementAPI.md#deletemodel) | **DELETE** /v1/models/{modelId} | Delete model.
[**GetModels**](ModelManagementAPI.md#getmodels) | **GET** /v1/models | List models.


<a name="deletemodel"></a>
# **DeleteModel**
> object DeleteModel (string modelId)

Delete model.

Deletes a custom model.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;

namespace Example
{
    public class DeleteModelExample
    {
        public void main()
        {
            
            var apiInstance = new ModelManagementAPI();
            var modelId = modelId_example;  // string | model_id of the model to delete

            try
            {
                // Delete model.
                object result = apiInstance.DeleteModel(modelId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ModelManagementAPI.DeleteModel: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **modelId** | **string**| model_id of the model to delete | 

### Return type

[**object**](.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getmodels"></a>
# **GetModels**
> ListModelsResults GetModels ()

List models.

Lists available models for Relations and Entities features, including Watson Knowledge Studio custom models that you have created and linked to your Natural Language Understanding service.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;

namespace Example
{
    public class GetModelsExample
    {
        public void main()
        {
            
            var apiInstance = new ModelManagementAPI();

            try
            {
                // List models.
                ListModelsResults result = apiInstance.GetModels();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ModelManagementAPI.GetModels: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**ListModelsResults**](ListModelsResults.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

