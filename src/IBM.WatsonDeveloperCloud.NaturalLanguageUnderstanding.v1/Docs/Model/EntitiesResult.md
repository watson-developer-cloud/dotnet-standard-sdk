# .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model.EntitiesResult
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Type** | **string** | Entity type. | [optional] 
**Relevance** | **float?** | Relevance score from 0 to 1. Higher values indicate greater relevance. | [optional] 
**Count** | **int?** | How many times the entity was mentioned in the text. | [optional] 
**Text** | **string** | The name of the entity. | [optional] 
**Emotion** | [**EmotionScores**](EmotionScores.md) | Emotion analysis results for the entity, enabled with the "emotion" option. | [optional] 
**Sentiment** | [**FeatureSentimentResults**](FeatureSentimentResults.md) | Sentiment analysis results for the entity, enabled with the "sentiment" option. | [optional] 
**Disambiguation** | [**DisambiguationResult**](DisambiguationResult.md) | Disambiguation information for the entity. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

