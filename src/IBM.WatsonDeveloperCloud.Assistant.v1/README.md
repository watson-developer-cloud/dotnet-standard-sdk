[![NuGet](https://img.shields.io/badge/nuget-v2.1.0-green.svg?style=flat)](https://www.nuget.org/packages/IBM.WatsonDeveloperCloud.Assistant.v1/)

### Assistant

With the IBM Watson™ [Assistant][assistant] service, you can create an application that understands natural-language input and uses machine learning to respond to customers in a way that simulates a assistant between humans.

### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.Assistant.v1

```
#### Project.json
```JSON

"dependencies": {
   "IBM.WatsonDeveloperCloud.Assistant.v1": "2.1.0"
}

```
### Usage
You complete these steps to implement your application:

* Configure a workspace. With the easy-to-use graphical environment, you set up the dialog flow and training data for your application.

* Develop your application. You code your application to connect to the Assistant workspace through API calls. You then integrate your app with other systems that you need, including back-end systems and third-party services such as chat services or social media.

#### Instantiating and authenticating the service
Before you can send requests to the service it must be instantiated and credentials must be set.
```cs
// create a Assistant Service instance
AssistantService _assistant = new AssistantService();

// set the credentials
_assistant.SetCredential(<username>, <password>);

// set the versionDate
_assistant.VersionDate = "<version-date>";
```

#### List workspaces
List existing workspaces for the service instance.
```cs
var result = _assistant.ListWorkspaces();
```

#### Create workspace
Create a new workspace.
```cs
CreateWorkspace workspace = new CreateWorkspace()
{
    Name = <workspace-name>,
    Description = <workspace-description>,
    Language = <workspace-language>
};

var result = _assistant.CreateWorkspace(workspace);
```

#### Delete workspace
Delete an existing workspace.
```cs
var result = _assistant.DeleteWorkspace(<workspace-id>);
```

#### Get workspace details
Get detailed information about a specific workspace.
```cs
var result = _assistant.GetWorkspace(<workspace-id>);
```

#### Update workspace details
Update an existing workspace.
```cs
UpdateWorkspace updatedWorkspace = new UpdateWorkspace()
{
    Name = <updated-workspace-name>,
    Description = <updated-workspace-description>,
    Language = <updated-workspace-language>
};

var result = _assistant.UpdateWorkspace(<workspace-id>, updatedWorkspace);
```

#### Message
Get a response to a user's input.
```cs
//  create message request
MessageRequest messageRequest0 = new MessageRequest()
{
  Input = new InputData()
  {
    Text = <input-string0>
  }
};

//  send a message to the assistant instance
var result0 = _assistant.Message(<workspace-id>, messageRequest0);

//  reference the message context to continue a assistant
messageRequest messageRequest1 = new MessageRequest()
{
  Input = new InputData()
  {
    Text = <input-string1>
  },
  Context = result.Context
};

//  Send another message including message context.
result1 = _assistant.Message(<workspace-id>, messageRequest1);
```

#### List Counterexamples
List the counterexamples for a workspace. Counterexamples are examples that have been marked as irrelevant input.
```cs
var result = _assistant.ListCounterexamples(<workspaceId>);
```

#### Create Counterexamples
Add a new counterexample to a workspace. Counterexamples are examples that have been marked as irrelevant input.
```cs
CreateExample example = new CreateExample()
{
    Text = <counterExample>
};

var result = _assistant.CreateCounterexample(<workspaceId>, example);
```

#### Delete Counterexample
Delete a counterexample from a workspace. Counterexamples are examples that have been marked as irrelevant input.
```cs
var result = _assistant.DeleteCounterexample(<workspaceId>, <counterExample>);
```

#### Get Counterexample
Get information about a counterexample. Counterexamples are examples that have been marked as irrelevant input.
```cs
var result = _assistant.GetCounterexample(<workspaceId>, <counterExample>);
```

#### Update Counterexample
Update the text of a counterexample. Counterexamples are examples that have been marked as irrelevant input.
```cs
UpdateExample updatedExample = new UpdateExample()
{
    Text = <updatedCounterExample>
};

var result = _assistant.UpdateCounterexample(<workspaceId>, <counterExample>, updatedExample);
```

#### List Entities
List the entities for a workspace.
```cs
var result = _assistant.ListEntities(<workspaceId>);
```

#### Create Entity
Create a new entity.
```cs
CreateEntity entity = new CreateEntity()
{
    Entity = <entity>,
    Description = <entity-description>
};

var result = _assistant.CreateEntity(<workspaceId>, entity);
```

#### Delete Entity
Delete an entity from a workspace.
```cs
var result = _assistant.DeleteEntity(<workspaceId>, <entity>);
```

#### Get Entity
Get information about an entity, optionally including all entity content.
```cs
var result = _assistant.GetEntity(<workspaceId>, <entity>);
```

#### Update Entity
Update an existing entity with new or modified data. You must provide JSON data defining the content of the updated entity.

Any elements included in the new JSON will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are included in the new JSON.) For example, if you update the values for an entity, the previously existing values are discarded and replaced with the new values specified in the JSON input.
```cs
UpdateEntity updatedEntity = new UpdateEntity()
{
    Entity = updatedEntity,
    Description = updatedEntityDescription
};

var result = _assistant.UpdateEntity(<workspaceId>, <entity>, updatedEntity);
```

#### List Entity Values
List the values for an entity.
```cs
var result = _assistant.ListValues(<workspaceId>, <entity>);
```

#### Add Entity Value
Add a new value to an entity.
```cs
CreateValue value = new CreateValue()
{
    Value = <value>
};

var result = _assistant.CreateValue(<workspaceId>, <entity>, value);
```

#### Delete Entity Value
Delete a value from an entity.
```cs
var result = _assistant.DeleteValue(<workspaceId>, <entity>, <value>);
```

#### Get Entity Value
Get information about an entity value.
```cs
var result = _assistant.GetValue(<workspaceId>, <entity>, <value>);
```

#### Update Entity Value
Update an existing entity value with new or modified data. You must provide JSON data defining the content of the updated entity value.

Any elements included in the new JSON will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are included in the new JSON.) For example, if you update the synonyms for an entity value, the previously existing synonyms are discarded and replaced with the new synonyms specified in the JSON input.
```cs
UpdateValue updatedValue = new UpdateValue()
{
    Value = <updatedValue>
};

var result = _assistant.UpdateValue(<workspaceId>, <entity>, <value>, updatedValue);
```

#### List Synonyms
List the synonyms for an entity value.
```cs
var result = _assistant.ListSynonyms(<workspaceId>, <entity>, <value>);
```

#### Add Synonym
Add a new synonym to an entity value.
```cs
CreateSynonym synonym = new CreateSynonym()
{
    Synonym = <synonym>
};

var result = _assistant.CreateSynonym(<workspaceId>, <entity>, <value>, synonym);
```

#### Delete Synonym
Delete a synonym from an entity value.
```cs
var result = _assistant.DeleteSynonym(<workspaceId>, <entity>, <value>, <synonym>);
```

#### Get Synonym
Get information about a synonym of an entity value.
```cs
var result = _assistant.GetSynonym(<workspaceId>, <entity>, <value>, <synonym>);
```

#### Update Synonym
Update an existing entity value synonym with new text.
```cs
UpdateSynonym updatedSynonym = new UpdateSynonym()
{
    Synonym = <synonym>
};

var result = _assistant.UpdateSynonym(<workspaceId>, <entity>, <value>, <synonym>, updatedSynonym);
```

#### List Intents
List the intents for a workspace.
```cs
var result = _assistant.ListIntents(<workspaceId>);
```

#### Create Intent
Create a new intent.
```cs
CreateIntent intent = new CreateIntent()
{
    Intent = <intent>,
    Description = <intent-description>
};

var result = _assistant.CreateIntent(<workspaceId>, intent);
```

#### Delete Intent
Delete an intent from a workspace.
```cs
var result = _assistant.DeleteIntent(<workspaceId>, <intent>);
```

#### Get Intent
Get information about an intent, optionally including all intent content.
```cs
var result = _assistant.GetIntent(<workspaceId>, <intent>);
```

#### Update Intent
Update an existing intent with new or modified data. You must provide JSON data defining the content of the updated intent.

Any elements included in the new JSON will completely replace the equivalent existing elements, including all subelements. (Previously existing subelements are not retained unless they are included in the new JSON.) For example, if you update the user input examples for an intent, the previously existing examples are discarded and replaced with the new examples specified in the JSON input.
```cs
UpdateIntent intent = new UpdateIntent()
{
    Intent = <intent>,
    Description = <intent-description>
};

var result = _assistant.UpdateIntent(<workspaceId>, <intent>, intent);
```

#### List Examples
List the user input examples for an intent.
```cs
var result = _assistant.ListExamples(<workspaceId>, <intent>);
```

#### Create Example
Add a new user input example to an intent.
```cs
CreateExample example = new CreateExample()
{
    Text = <example>
};

var result = _assistant.CreateExample(<workspaceId>, <intent>, example);
```

#### Delete Example
Delete a user input example from an intent.
```cs
var result = _assistant.DeleteExample(<workspaceId>, <intent>, <example>);
```

#### Get Example
Get information about a user input example.
```cs
var result = _assistant.GetExample(<workspaceId>, <intent>, <example>);
```

#### Update Example
Update the text of a user input example.
```cs
UpdateExample updatedExample = new UpdateExample()
{
    Text = <example>
};

var result = _assistant.UpdateExample(<workspaceId>, <intent>, <example>, updatedExample);
```

#### List Log Events
List the events from the log of a workspace.
```cs
var result = _assistant.ListLogs(<workspaceId>);
```

[assistant]:https://console.bluemix.net/docs/services/assistant/index.html#about
