# .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model.AnalysisResults
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Language** | **string** | Language used to analyze the text. | [optional] 
**AnalyzedText** | **string** | Text that was used in the analysis. | [optional] 
**RetrievedUrl** | **string** | URL that was used to retrieve HTML content. | [optional] 
**Usage** | [**Usage**](Usage.md) | API usage information for the request. | [optional] 
**Concepts** | [**List<ConceptsResult>**](ConceptsResult.md) | The general concepts referenced or alluded to in the specified content. | [optional] 
**Entities** | [**List<EntitiesResult>**](EntitiesResult.md) | The important entities in the specified content. | [optional] 
**Keywords** | [**List<KeywordsResult>**](KeywordsResult.md) | The important keywords in content organized by relevance. | [optional] 
**Categories** | [**List<CategoriesResult>**](CategoriesResult.md) | The hierarchical 5-level taxonomy the content is categorized into. | [optional] 
**Emotion** | [**EmotionResult**](EmotionResult.md) | The anger, disgust, fear, joy, or sadness conveyed by the content. | [optional] 
**Metadata** | [**MetadataResult**](MetadataResult.md) | The metadata holds author information, publication date and the title of the text/HTML content. | [optional] 
**Relations** | [**List<RelationsResult>**](RelationsResult.md) | The relationships between entities in the content. | [optional] 
**SemanticRoles** | [**List<SemanticRolesResult>**](SemanticRolesResult.md) | The subjects of actions and the objects the actions act upon. | [optional] 
**Sentiment** | [**SentimentResult**](SentimentResult.md) | The sentiment of the content. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

