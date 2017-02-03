### Language Translator

[![NuGet version](https://img.shields.io/nuget/v/IBM.WatsonDeveloperCloud.LanguageTranslator.svg)](https://www.nuget.org/packages/IBM.WatsonDeveloperCloud.LanguageTranslator/)

[Language Translator][language_translator] translates text from one language to another. The service offers multiple domain-specific models that you can customize based on your unique terminology and language. Use Language Translator to take news from across the globe and present it in your language, communicate with your customers in their own language, and more.

### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.LanguageTranslator -Pre

```
#### Project.json
```JSON

"dependencies": {
   "IBM.WatsonDeveloperCloud.LanguageTranslator": "0.1.0-alpha"
}

```
### Usage
Select a domain, then identify or select the language of text, and then translate the text from one supported language to another.

#### Translate
```cs
// create a Language Translator Service
LanguageTranslationService service =
    new LanguageTranslationService();

// set the credentials
service.SetCredential("<username>", "<password>");

// Translate '"Hello! How are you?' from English to Portuguese using the Language Translator service
var results = service.Translate("en", "pt", "Hello! How are you?");
```

#### Identifiable languages
```cs
```

#### Identify language
```cs
```

#### List models
```cs
```

#### Create a model
```cs
```

#### Delete a model
```cs
```

#### Get a model details
```cs
```

[language_translator]: http://www.ibm.com/watson/developercloud/doc/language-translator/
