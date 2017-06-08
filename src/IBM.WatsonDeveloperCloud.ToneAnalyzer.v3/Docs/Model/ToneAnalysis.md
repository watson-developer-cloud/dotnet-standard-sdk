# .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model.ToneAnalysis
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**DocumentTone** | [**DocumentAnalysis**](DocumentAnalysis.md) | An object of type `DocumentAnalysis` that provides the results for the full document of the input content. | 
**SentencesTone** | [**List<SentenceAnalysis>**](SentenceAnalysis.md) | An array of `SentenceAnalysis` objects that provides the results for the individual sentences of the input content. The service returns results only for the first 100 sentences of the input. The field is omitted if the `sentences` parameter of the request is set to `false`. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

