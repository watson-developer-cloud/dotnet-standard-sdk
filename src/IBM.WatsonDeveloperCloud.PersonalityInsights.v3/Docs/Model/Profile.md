# .IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model.Profile
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ProcessedLanguage** | **string** | The language model that was used to process the input; for example, `en`. | 
**WordCount** | **int?** | The number of words that were found in the input. | 
**WordCountMessage** | **string** | When guidance is appropriate, a string that provides a message that indicates the number of words found and where that value falls in the range of required or suggested number of words. | [optional] 
**Personality** | [**List<TraitTreeNode>**](TraitTreeNode.md) | Detailed results for the Big Five personality characteristics (dimensions and facets) inferred from the input text. | 
**Values** | [**List<TraitTreeNode>**](TraitTreeNode.md) | Detailed results for the Needs characteristics inferred from the input text. | 
**Needs** | [**List<TraitTreeNode>**](TraitTreeNode.md) | Detailed results for the Values characteristics inferred from the input text. | 
**Behavior** | [**List<BehaviorNode>**](BehaviorNode.md) | For JSON content that is timestamped, detailed results about the social behavior disclosed by the input in terms of temporal characteristics. The results include information about the distribution of the content over the days of the week and the hours of the day. | [optional] 
**ConsumptionPreferences** | [**List<ConsumptionPreferencesCategoryNode>**](ConsumptionPreferencesCategoryNode.md) | If the `consumption_preferences` query parameter is `true`, detailed results for each category of consumption preferences. Each element of the array provides information inferred from the input text for the individual preferences of that category. | [optional] 
**Warnings** | [**List<Warning>**](Warning.md) | Warning messages associated with the input text submitted with the request. The array is empty if the input generated no warnings. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

