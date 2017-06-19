# .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model.ToneChatScore
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Score** | **double?** | The score for the tone in the range of 0.5 to 1. A score greater than 0.75 indicates a high likelihood that the tone is perceived in the utterance. | 
**ToneId** | **string** | The unique, non-localized identifier of the tone for the results. The service can return results for the following tone IDs: `sad`, `frustrated`, `satisfied`, `excited`, `polite`, `impolite`, and `sympathetic`. The service returns results only for tones whose scores meet a minimum threshold of 0.5. | 
**ToneName** | **string** | The user-visible, localized name of the tone. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

