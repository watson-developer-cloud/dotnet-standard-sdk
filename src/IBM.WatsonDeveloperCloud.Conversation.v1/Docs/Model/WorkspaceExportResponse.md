# .IBM.WatsonDeveloperCloud.Conversation.v1.Model.WorkspaceExportResponse
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The name of the workspace. | 
**Description** | **string** | The description of the workspace. | 
**Language** | **string** | The language of the workspace. | 
**Metadata** | **object** | Any metadata that is required by the workspace. | 
**Created** | [**DateTime**](DateTime.md) | The timestamp for creation of the workspace. | 
**Updated** | [**DateTime**](DateTime.md) | The timestamp for the last update to the workspace. | 
**WorkspaceId** | **string** | The workspace ID. | 
**Status** | **string** | The current status of the workspace. | 
**Intents** | [**List<IntentExportResponse>**](IntentExportResponse.md) | An array of intents. | [optional] 
**Entities** | [**List<EntityExportResponse>**](EntityExportResponse.md) | An array of entities. | [optional] 
**Counterexamples** | [**List<ExampleResponse>**](ExampleResponse.md) | An array of counterexamples. | [optional] 
**DialogNodes** | [**List<DialogNodeResponse>**](DialogNodeResponse.md) | An array of objects describing the dialog nodes in the workspace. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

