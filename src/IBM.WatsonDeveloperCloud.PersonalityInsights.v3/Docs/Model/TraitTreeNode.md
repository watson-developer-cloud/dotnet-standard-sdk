# .IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model.TraitTreeNode
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**TraitId** | **string** | The unique identifier of the characteristic to which the results pertain. IDs have the form `big5_{characteristic}` for Big Five personality characteristics, `need_{characteristic}` for Needs, or `value_{characteristic}` for Values. | 
**Name** | **string** | The user-visible name of the characteristic. | 
**Category** | **string** | The category of the characteristic: `personality` for Big Five personality characteristics, `needs` for Needs, or `values` for Values. | 
**Percentile** | **double?** | The normalized percentile score for the characteristic. The range is 0 to 1. For example, if the percentage for Openness is 0.60, the author scored in the 60th percentile; the author is more open than 59 percent of the population and less open than 39 percent of the population. | 
**RawScore** | **double?** | The raw score for the characteristic. The range is 0 to 1. A higher score generally indicates a greater likelihood that the author has that characteristic, but raw scores must be considered in aggregate: The range of values in practice might be much smaller than 0 to 1, so an individual score must be considered in the context of the overall scores and their range. The raw score is computed based on the input and the service model; it is not normalized or compared with a sample population. The raw score enables comparison of the results against a different sampling population and with a custom normalization approach. | [optional] 
**Children** | [**List<TraitTreeNode>**](TraitTreeNode.md) | For `personality` (Big Five) dimensions, more detailed results for the facets of each dimension as inferred from the input text. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

