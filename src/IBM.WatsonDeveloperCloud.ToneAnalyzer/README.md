### Tone Analyzer


The IBM Watson™ Tone Analyzer Service uses linguistic analysis to detect three types of tones from written text: emotions, social tendencies, and writing style. Emotions identified include things like anger, fear, joy, sadness, and disgust. Identified social tendencies include things from the Big Five personality traits used by some psychologists. These include openness, conscientiousness, extraversion, agreeableness, and emotional range. Identified writing styles include confident, analytical, and tentative.

### Instalation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.ToneAnalyzer -Pre

```
#### Project.json
```JSON
"dependencies": {
   "IBM.WatsonDeveloperCloud.ToneAnalyzer": "0.1.0-alpha"
}
```
#### Usage
Select a domain, then identify or select the language of text, and then translate the text from one supported language to another.

```C#
 // create a Language Translator Service 
 ToneAnalizerService service =
     new ToneAnalizerService();
 
 // set the credentials
 service.SetCredential("<username>", "<password>");
 
 // Translate '"Hello! How are you?' from English to Portuguese using the Language Translator service
 var results = service.AnalyzeTone("A word is dead when it is said, some say. Emily Dickinson");

```
