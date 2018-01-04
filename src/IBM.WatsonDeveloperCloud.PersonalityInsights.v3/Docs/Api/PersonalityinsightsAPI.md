# .IBM.WatsonDeveloperCloud.PersonalityInsights.v3.PersonalityinsightsAPI

All URIs are relative to *https://localhost/personality-insights/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Profile**](PersonalityinsightsAPI.md#profile) | **POST** /v3/profile | Generates a personality profile based on input text.


<a name="profile"></a>
# **Profile**
> Profile Profile (string contentType, string accept, ContentListContainer body, string contentLanguage, string acceptLanguage, bool? rawScores, bool? csvHeaders, bool? consumptionPreferences)

Generates a personality profile based on input text.

Derives personality insights for up to 20 MB of input content written by an author, though the service requires much less text to produce an accurate profile; for more information, see [Guidelines for providing sufficient input](https://console.bluemix.net/docs/services/personality-insights/input.html#sufficient). Accepts input in Arabic, English, Japanese, or Spanish and produces output in one of eleven languages. Provide plain text, HTML, or JSON content, and receive results in JSON or CSV format.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.PersonalityInsights.v3;
using .Client;
using .IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;

namespace Example
{
    public class ProfileExample
    {
        public void main()
        {
            
            var apiInstance = new PersonalityinsightsAPI();
            var contentType = contentType_example;  // string | The content type of the request: plain text (the default), HTML, or JSON. Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8; per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the ASCII character set). When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the character encoding of the input text, for example, `Content-Type: text/plain;charset=utf-8`. (default to text/plain)
            var accept = accept_example;  // string | The desired content type of the response: JSON (the default) or CSV. CSV output includes a fixed number of columns and optional headers. (default to application/json)
            var body = new ContentListContainer(); // ContentListContainer | A maximum of 20 MB of content to analyze, though the service requires much less text; for more information, see [Guidelines for providing sufficient input](https://console.bluemix.net/docs/services/personality-insights/input.html#sufficient). A JSON request must conform to the `ContentListContainer` model.
            var contentLanguage = contentLanguage_example;  // string | The language of the input text for the request: Arabic, English, Spanish, or Japanese. Regional variants are treated as their parent language; for example, `en-US` is interpreted as `en`. The effect of the `Content-Language` header depends on the `Content-Type` header. When `Content-Type` is `text/plain` or `text/html`, `Content-Language` is the only way to specify the language. When `Content-Type` is `application/json`, `Content-Language` overrides a language specified with the `language` parameter of a `ContentItem` object, and content items that specify a different language are ignored; omit this header to base the language on the specification of the content items. You can specify any combination of languages for `Content-Language` and `Accept-Language`. (optional)  (default to en)
            var acceptLanguage = acceptLanguage_example;  // string | The desired language of the response. For two-character arguments, regional variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify any combination of languages for the input and response content. (optional)  (default to en)
            var rawScores = true;  // bool? | If `true`, a raw score in addition to a normalized percentile is returned for each characteristic; raw scores are not compared with a sample population. If `false` (the default), only normalized percentiles are returned. (optional)  (default to false)
            var csvHeaders = true;  // bool? | If `true`, column labels are returned with a CSV response; if `false` (the default), they are not. Applies only when the `Accept` header is set to `text/csv`. (optional)  (default to false)
            var consumptionPreferences = true;  // bool? | If `true`, information about consumption preferences is returned with the results; if `false` (the default), the response does not include the information. (optional)  (default to false)

            try
            {
                // Generates a personality profile based on input text.
                Profile result = apiInstance.Profile(contentType, accept, body, contentLanguage, acceptLanguage, rawScores, csvHeaders, consumptionPreferences);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PersonalityinsightsAPI.Profile: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| The content type of the request: plain text (the default), HTML, or JSON. Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8; per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the ASCII character set). When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the character encoding of the input text, for example, `Content-Type: text/plain;charset=utf-8`. | [default to text/plain]
 **accept** | **string**| The desired content type of the response: JSON (the default) or CSV. CSV output includes a fixed number of columns and optional headers. | [default to application/json]
 **body** | [**ContentListContainer**](ContentListContainer.md)| A maximum of 20 MB of content to analyze, though the service requires much less text; for more information, see [Guidelines for providing sufficient input](https://console.bluemix.net/docs/services/personality-insights/input.html#sufficient). A JSON request must conform to the `ContentListContainer` model. | 
 **contentLanguage** | **string**| The language of the input text for the request: Arabic, English, Spanish, or Japanese. Regional variants are treated as their parent language; for example, `en-US` is interpreted as `en`. The effect of the `Content-Language` header depends on the `Content-Type` header. When `Content-Type` is `text/plain` or `text/html`, `Content-Language` is the only way to specify the language. When `Content-Type` is `application/json`, `Content-Language` overrides a language specified with the `language` parameter of a `ContentItem` object, and content items that specify a different language are ignored; omit this header to base the language on the specification of the content items. You can specify any combination of languages for `Content-Language` and `Accept-Language`. | [optional] [default to en]
 **acceptLanguage** | **string**| The desired language of the response. For two-character arguments, regional variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify any combination of languages for the input and response content. | [optional] [default to en]
 **rawScores** | **bool?**| If `true`, a raw score in addition to a normalized percentile is returned for each characteristic; raw scores are not compared with a sample population. If `false` (the default), only normalized percentiles are returned. | [optional] [default to false]
 **csvHeaders** | **bool?**| If `true`, column labels are returned with a CSV response; if `false` (the default), they are not. Applies only when the `Accept` header is set to `text/csv`. | [optional] [default to false]
 **consumptionPreferences** | **bool?**| If `true`, information about consumption preferences is returned with the results; if `false` (the default), the response does not include the information. | [optional] [default to false]

### Return type

[**Profile**](Profile.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/html, text/plain
 - **Accept**: application/json, text/csv

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

