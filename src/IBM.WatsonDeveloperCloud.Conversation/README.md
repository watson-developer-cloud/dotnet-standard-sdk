### Conversation

With the IBM Watson™ [Conversation][conversation] service, you can create an application that understands natural-language input and uses machine learning to respond to customers in a way that simulates a conversation between humans.

### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.Conversation -Pre

```
#### Project.json
```JSON

"dependencies": {
   "IBM.WatsonDeveloperCloud.Conversation": "0.2.0"
}

```
### Usage
You complete these steps to implement your application:

* Configure a workspace. With the easy-to-use graphical environment, you set up the dialog flow and training data for your application.

* Develop your application. You code your application to connect to the Conversation workspace through API calls. You then integrate your app with other systems that you need, including back-end systems and third-party services such as chat services or social media.

#### Instantiating and authenticating the service
Before you can send requests to the service it must be instantiated and credentials must be set.
```cs
// create a Language Translator Service instance
ConversationService _conversation = new ConversationService();

// set the credentials
_conversation.SetCredential("<username>", "<password>");
```
<!-- #### List workspaces
List existing workspaces for the service instance.
```cs
``` -->

<!-- ##### Create workspace
Create a new workspace.
```cs
``` -->

<!-- ##### Delete workspace
Delete an existing workspace.
```cs
``` -->

<!-- ##### Get workspace details
Get detailed information about a specific workspace.
```cs
``` -->

<!-- ##### Update workspace details
Update an existing workspace.
```cs
``` -->

#### Message
Get a response to a user's input.
```cs
//  create message request
MessageRequest messageRequest = new MessageRequest()
{
  Input = new InputData()
  {
    Text = "<input-string>"
  }
};

//  send a message to the conversation instance
var result = _conversation.Message("<workspace-id>", messageRequest);
```

[conversation]:http://www.ibm.com/watson/developercloud/doc/conversation/
