# .IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model.Parameters
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Text** | **string** | The plain text to analyze. | [optional] 
**Html** | **string** | The HTML file to analyze. | [optional] 
**Url** | **string** | The web page to analyze. | [optional] 
**Features** | [**Features**](Features.md) | Specific features to analyze the document for. | 
**Clean** | **bool?** | Remove website elements, such as links, ads, etc. | [optional] [default to true]
**Xpath** | **string** | XPath query for targeting nodes in HTML. | [optional] 
**FallbackToRaw** | **bool?** | Whether to use raw HTML content if text cleaning fails. | [optional] [default to true]
**ReturnAnalyzedText** | **bool?** | Whether or not to return the analyzed text. | [optional] [default to false]
**Language** | **string** | ISO 639-1 code indicating the language to use in the analysis. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

