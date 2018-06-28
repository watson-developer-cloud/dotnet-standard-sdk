[![NuGet](https://img.shields.io/badge/nuget-v2.4.1-green.svg?style=flat)](https://www.nuget.org/packages/IBM.WatsonDeveloperCloud.ToneAnalyzer.v3/)

### Tone Analyzer

The IBM Watson™ [Tone Analyzer Service][tone-analyzer] uses linguistic analysis to detect three types of tones from written text: emotions, social tendencies, and writing style. Emotions identified include things like anger, fear, joy, sadness, and disgust. Identified social tendencies include things from the Big Five personality traits used by some psychologists. These include openness, conscientiousness, extraversion, agreeableness, and emotional range. Identified writing styles include confident, analytical, and tentative.

### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.ToneAnalyzer.v3

```
#### .csproj
```xml

<ItemGroup>
    <PackageReference Include="IBM.WatsonDeveloperCloud.ToneAnalyzer.v3" Version="2.4.1" />
</ItemGroup>

```
### Usage
Use [Tone Analyzer][tone-analyzer] to detect three types of tones from written text: emotions, social tendencies, and language style. Emotions identified include things like anger, cheerfulness and sadness. Identified social tendencies include things from the Big Five personality traits used by some psychologists. These include openness, conscientiousness, extraversion, agreeableness, and neuroticism. Identified language styles include things like confident, analytical, and tentative. Input email and other written media into the [Tone Analyzer][tone-analyzer] service, and use the results to determine if your writing comes across with the tone, personality traits, and writing style that you want for your intended audience.

#### Analyze tone
Analyzes the tone of a piece of text. The message is analyzed for several tones - social, emotional, and language. For each tone, various traits are derived. For example, conscientiousness, agreeableness, and openness.
```cs
 // Analyze Tone
 var results = service.AnalyzeTone("A word is dead when it is said, some say. Emily Dickinson");

```

[tone-analyzer]: https://console.bluemix.net/docs/services/tone-analyzer/index.html
