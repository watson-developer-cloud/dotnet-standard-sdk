# .IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model.ContentItem
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Content** | **string** | Content that is to be analyzed. The service supports up to 20 MB of content for all items combined. | 
**Id** | **string** | Unique identifier for this content item. | [optional] 
**Created** | **long?** | Timestamp that identifies when this content was created. Specify a value in milliseconds since the UNIX Epoch (January 1, 1970, at 0:00 UTC). Required only for results that include temporal behavior data. | [optional] 
**Updated** | **long?** | Timestamp that identifies when this content was last updated. Specify a value in milliseconds since the UNIX Epoch (January 1, 1970, at 0:00 UTC). Required only for results that include temporal behavior data. | [optional] 
**Contenttype** | **string** | MIME type of the content. The default is plain text. The tags are stripped from HTML content before it is analyzed; plain text is processed as submitted. | [optional] [default to ContenttypeEnum.TEXT_PLAIN]
**Language** | **string** | Language identifier (two-letter ISO 639-1 identifier) for the language of the content item. The default is `en` (English). Regional variants are treated as their parent language; for example, `en-US` is interpreted as `en`. A language specified with the `Content-Type` header overrides the value of this parameter; any content items that specify a different language are ignored. Omit the `Content-Type` header to base the language on the most prevalent specification among the content items; again, content items that specify a different language are ignored. You can specify any combination of languages for the input and response content. | [optional] [default to LanguageEnum.EN]
**Parentid** | **string** | Unique ID of the parent content item for this item. Used to identify hierarchical relationships between posts/replies, messages/replies, and so on. | [optional] [default to "null"]
**Reply** | **bool?** | Indicates whether this content item is a reply to another content item. | [optional] [default to false]
**Forward** | **bool?** | Indicates whether this content item is a forwarded/copied version of another content item. | [optional] [default to false]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

