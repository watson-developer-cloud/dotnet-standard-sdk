# .IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model.UtteranceAnalysis
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**UtteranceId** | **string** | The unique identifier of the utterance. The first utterance has ID 0, and the ID of each subsequent utterance is incremented by one. | 
**UtteranceText** | **string** | The text of the utterance. | 
**Tones** | [**List<ToneChatScore>**](ToneChatScore.md) | An array of `ToneChatScore` objects that provides results for the most prevalent tones of the utterance. The array includes results for any tone whose score is at least 0.5. The array is empty if no tone has a score that meets this threshold. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

