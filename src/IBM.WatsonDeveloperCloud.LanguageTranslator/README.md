### Language Translator

[![NuGet version](https://img.shields.io/nuget/v/IBM.WatsonDeveloperCloud.LanguageTranslator.svg)](https://www.nuget.org/packages/IBM.WatsonDeveloperCloud.LanguageTranslator/)

The IBM Watson™ Language Translator service provides an Application Programming Interface (API) that lets you choose a domain-specific translation model, optionally customize it, select or automatically identify the language used in your input text, and then translate text from one supported language to another.

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

```cs
 // create a Language Translator Service
 LanguageTranslationService service =
     new LanguageTranslationService();

 // set the credentials
 service.SetCredential("<username>", "<password>");

 // Translate '"Hello! How are you?' from English to Portuguese using the Language Translator service
 var results = service.Translate("en", "pt", "Hello! How are you?");

```
