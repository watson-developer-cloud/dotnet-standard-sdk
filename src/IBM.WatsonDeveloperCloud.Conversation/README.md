### Conversation

Watson [Conversation][conversation] allows you to quickly build, test and deploy a bot or virtual agent across mobile devices, messaging platforms like Slack or even on a physical robot. Conversation has a visual dialog builder to help you create natural conversations between your apps and users, without any coding experience required.

### Instalation
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
#### Usage
You complete these steps to implement your application:

* Configure a workspace. With the easy-to-use graphical environment, you set up the dialog flow and training data for your application.

* Develop your application. You code your application to connect to the Conversation workspace through API calls. You then integrate your app with other systems that you need, including back-end systems and third-party services such as chat services or social media.

```C#
 // create a Tone Analyzer Service
 ConversationService service =
     new ConversationService();

 // set the credentials
 service.SetCredential("<username>", "<password>");

 // list workspaces

 // create workspace

 // delete workspace

 // get workspace details

 // update workspace

 // message

```

[conversation]:http://www.ibm.com/watson/developercloud/doc/conversation/
