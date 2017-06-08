# .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.AnalyzeAPI

All URIs are relative to *https://localhost/natural-language-understanding/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Analyze**](AnalyzeAPI.md#analyze) | **POST** /v1/analyze | Analyze text, HTML, or a public webpage.


<a name="analyze"></a>
# **Analyze**
> AnalysisResults Analyze (Parameters parameters)

Analyze text, HTML, or a public webpage.

Analyzes text, HTML, or a public webpage with one or more text analysis features.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1;
using .Client;
using .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;

namespace Example
{
    public class AnalyzeExample
    {
        public void main()
        {
            
            var apiInstance = new AnalyzeAPI();
            var parameters = new Parameters(); // Parameters | An object containing request parameters. The `features` object and one of the `text`, `html`, or `url` attributes are required. (optional) 

            try
            {
                // Analyze text, HTML, or a public webpage.
                AnalysisResults result = apiInstance.Analyze(parameters);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AnalyzeAPI.Analyze: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **parameters** | [**Parameters**](Parameters.md)| An object containing request parameters. The `features` object and one of the `text`, `html`, or `url` attributes are required. | [optional] 

### Return type

[**AnalysisResults**](AnalysisResults.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

