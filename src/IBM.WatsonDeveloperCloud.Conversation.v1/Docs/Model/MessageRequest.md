# .IBM.WatsonDeveloperCloud.Conversation.v1.Model.MessageRequest
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Input** | [**InputData**](InputData.md) | An input object that includes the input text. | 
**AlternateIntents** | **bool?** | Whether to return more than one intent. Set to `true` to return all matching intents. | [optional] [default to false]
**Context** | [**Context**](Context.md) | State information for the conversation. Continue a conversation by including the context object from the previous response. | [optional] 
**Entities** | [**List<RuntimeEntity>**](RuntimeEntity.md) | Include the entities from the previous response when they do not need to change and to prevent Watson from trying to identify them. | [optional] 
**Intents** | [**List<RuntimeIntent>**](RuntimeIntent.md) | An array of name-confidence pairs for the user input. Include the intents from the previous response when they do not need to change and to prevent Watson from trying to identify them. | [optional] 
**Output** | [**OutputData**](OutputData.md) | System output. Include the output from the request when you have several requests within the same Dialog turn to pass back in the intermediate information. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

