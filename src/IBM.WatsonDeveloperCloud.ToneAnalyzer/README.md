### Tone Analyzer

The [IBM Watson™ Tone Analyzer Service](/src/IBM.WatsonDeveloperCloud.ToneAnalyzer) uses linguistic analysis to detect three types of tones from written text: emotions, social tendencies, and writing style. Emotions identified include things like anger, fear, joy, sadness, and disgust. Identified social tendencies include things from the Big Five personality traits used by some psychologists. These include openness, conscientiousness, extraversion, agreeableness, and emotional range. Identified writing styles include confident, analytical, and tentative.

### Installation
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
### Usage
Use [Tone Analyzer](/src/IBM.WatsonDeveloperCloud.ToneAnalyzer) to detect three types of tones from written text: emotions, social tendencies, and language style. Emotions identified include things like anger, cheerfulness and sadness. Identified social tendencies include things from the Big Five personality traits used by some psychologists. These include openness, conscientiousness, extraversion, agreeableness, and neuroticism. Identified language styles include things like confident, analytical, and tentative. Input email and other written media into the [Tone Analyzer](/src/IBM.WatsonDeveloperCloud.ToneAnalyzer) service, and use the results to determine if your writing comes across with the tone, personality traits, and writing style that you want for your intended audience.

```cs
 // create a Tone Analyzer Service
 ToneAnalyzerService service =
     new ToneAnalyzerService();

 // set the credentials
 service.SetCredential("<username>", "<password>");

 // Analyze Tone
 var results = service.AnalyzeTone("A word is dead when it is said, some say. Emily Dickinson");

```
