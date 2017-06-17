# .IBM.WatsonDeveloperCloud.Conversation.v1.Model.CreateWorkspace
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The name of the workspace. | [optional] 
**Description** | **string** | The description of the workspace. | [optional] 
**Language** | **string** | The language of the workspace. | [optional] 
**Intents** | [**List<CreateIntent>**](CreateIntent.md) | An array of objects defining the intents for the workspace. | [optional] 
**Entities** | [**List<CreateEntity>**](CreateEntity.md) | An array of objects defining the entities for the workspace. | [optional] 
**DialogNodes** | [**List<CreateDialogNode>**](CreateDialogNode.md) | An array of objects defining the nodes in the workspace dialog. | [optional] 
**Counterexamples** | [**List<CreateExample>**](CreateExample.md) | An array of objects defining input examples that have been marked as irrelevant input. | [optional] 
**Metadata** | **object** | Any metadata related to the workspace. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

