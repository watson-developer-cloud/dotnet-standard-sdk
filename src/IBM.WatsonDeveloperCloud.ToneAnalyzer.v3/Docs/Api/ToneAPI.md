# .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.ToneAPI

All URIs are relative to *https://localhost/tone-analyzer/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Tone**](ToneAPI.md#tone) | **POST** /v3/tone | Analyze general purpose tone.
[**ToneChat**](ToneAPI.md#tonechat) | **POST** /v3/tone_chat | Analyze customer engagement tone.


<a name="tone"></a>
# **Tone**
> ToneAnalysis Tone (ToneInput body, string tones, bool? sentences)

Analyze general purpose tone.

Uses the general purpose endpoint to analyze the tone of your input content. The service can analyze the input for several tones: emotion, language, and social. It derives various characteristics for each tone that it analyzes. The method always analyzes the tone of the full document; by default, it also analyzes the tone of each individual sentence of the input. You can submit a maximum of 128 KB of content in JSON, plain text, or HTML format.   Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8; per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the ASCII character set). When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the character encoding of the input text; for example: `Content-Type: text/plain;charset=utf-8`. For `text/html`, the service removes HTML tags and analyzes only the textual content.   Use the `POST` request method to analyze larger amounts of content in any of the available formats. Use the `GET` request method to analyze smaller quantities of plain text content.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3;
using .Client;
using .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;

namespace Example
{
    public class ToneExample
    {
        public void main()
        {
            
            var apiInstance = new ToneAPI();
            var body = new ToneInput(); // ToneInput | JSON, plain text, or HTML input that contains the content to be analyzed. For JSON input, provide an object of type `ToneInput`. Submit a maximum of 128 KB of content. Sentences with fewer than three words cannot be analyzed.
            var tones = tones_example;  // string | A comma-separated list of tones for which the service is to return its analysis of the input; the indicated tones apply both to the full document and to individual sentences of the document. You can specify one or more of the following values: `emotion`, `language`, and `social`. Omit the parameter to request results for all three tones. (optional) 
            var sentences = true;  // bool? | Indicates whether the service is to return an analysis of each individual sentence in addition to its analysis of the full document. If `true` (the default), the service returns results for each sentence. The service returns results only for the first 100 sentences of the input. (optional)  (default to true)

            try
            {
                // Analyze general purpose tone.
                ToneAnalysis result = apiInstance.Tone(body, tones, sentences);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ToneAPI.Tone: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ToneInput**](ToneInput.md)| JSON, plain text, or HTML input that contains the content to be analyzed. For JSON input, provide an object of type `ToneInput`. Submit a maximum of 128 KB of content. Sentences with fewer than three words cannot be analyzed. | 
 **tones** | **string**| A comma-separated list of tones for which the service is to return its analysis of the input; the indicated tones apply both to the full document and to individual sentences of the document. You can specify one or more of the following values: `emotion`, `language`, and `social`. Omit the parameter to request results for all three tones. | [optional] 
 **sentences** | **bool?**| Indicates whether the service is to return an analysis of each individual sentence in addition to its analysis of the full document. If `true` (the default), the service returns results for each sentence. The service returns results only for the first 100 sentences of the input. | [optional] [default to true]

### Return type

[**ToneAnalysis**](ToneAnalysis.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/plain, text/html
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="tonechat"></a>
# **ToneChat**
> UtteranceAnalyses ToneChat (ToneChatInput utterances)

Analyze customer engagement tone.

Uses the customer engagement endpoint to analyze the tone of customer service and customer support conversations. For each utterance of a conversation, the method reports the most prevalent subset of the following seven tones: sad, frustrated, satisfied, excited, polite, impolite, and sympathetic. You can submit a maximum of 128 KB of JSON input. Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8.   **Note**: The `tone_chat` method is currently beta functionality.

### Example
```csharp
using System;
using System.Diagnostics;
using .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3;
using .Client;
using .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;

namespace Example
{
    public class ToneChatExample
    {
        public void main()
        {
            
            var apiInstance = new ToneAPI();
            var utterances = new ToneChatInput(); // ToneChatInput | A JSON object that contains the content to be analyzed.

            try
            {
                // Analyze customer engagement tone.
                UtteranceAnalyses result = apiInstance.ToneChat(utterances);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ToneAPI.ToneChat: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **utterances** | [**ToneChatInput**](ToneChatInput.md)| A JSON object that contains the content to be analyzed. | 

### Return type

[**UtteranceAnalyses**](UtteranceAnalyses.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

