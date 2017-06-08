# .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model.ToneScore
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Score** | **double?** | The score for the tone in the range of 0 to 1. A score less than 0.5 indicates that the tone is unlikely to be perceived in the content; a score greater than 0.75 indicates a high likelihood that the tone is perceived. | 
**ToneId** | **string** | The unique, non-localized identifier of the tone for the results. The service can return results for the following tone IDs of the different categories: * For the `emotion` category: `anger`, `disgust`, `fear`, `joy`, and `sadness` * For the `language` category: `analytical`, `confident`, and `tentative` * For the `social` category: `openness_big5`, `conscientiousness_big5`, `extraversion_big5`, `agreeableness_big5`, and `emotional_range_big5`   The service returns scores for all tones of a category, regardless of their values. | 
**ToneName** | **string** | The user-visible, localized name of the tone. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

