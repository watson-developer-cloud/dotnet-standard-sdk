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
   "IBM.WatsonDeveloperCloud.Conversation": "0.1.0-alpha"
}

```
### Usage
You complete these steps to implement your application:

* Configure a workspace. With the easy-to-use graphical environment, you set up the dialog flow and training data for your application.

* Develop your application. You code your application to connect to the Conversation workspace through API calls. You then integrate your app with other systems that you need, including back-end systems and third-party services such as chat services or social media.

#### List workspaces
```cs
```

##### Create workspace
```cs
```

##### Delete workspace
```cs
```

##### Get workspace details
```cs
```

##### Update workspace details
```cs
```

##### Message
```cs
```

[conversation]:http://www.ibm.com/watson/developercloud/doc/conversation/
