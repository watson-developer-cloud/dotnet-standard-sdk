# .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model.SentenceAnalysis
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**SentenceId** | **int?** | The unique identifier of a sentence of the input content. The first sentence has ID 0, and the ID of each subsequent sentence is incremented by one. | 
**Text** | **string** | The text of the input sentence. | 
**InputFrom** | **int?** | The offset of the first character of the sentence in the overall input content. | 
**InputTo** | **int?** | The offset of the last character of the sentence in the overall input content. | 
**ToneCategories** | [**List<ToneCategory>**](ToneCategory.md) | An array of `ToneCategory` objects that provides the results for the tone analysis of the sentence. The service returns results only for the tones specified with the `tones` parameter of the request. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

