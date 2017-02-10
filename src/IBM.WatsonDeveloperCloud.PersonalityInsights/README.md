### Personality Insights

The IBM Watson™ [Personality Insights][personality-insights] service enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts.

The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can infer consumption preferences based on the results of its analysis and, for JSON content that is timestamped, can report temporal behavior.

For information about the meaning of the models that the service uses to describe personality characteristics, see [Personality models][personality-models]. For information about the meaning of the consumption preferences, see [Consumption preferences][consumption-preferences].

### Installation
#### Nuget
```

PM > Install-Package IBM.WatsonDeveloperCloud.PersonalityInsights -Pre

```
#### Project.json
```JSON

"dependencies": {
   "IBM.WatsonDeveloperCloud.PersonalityInsights": "0.1.0-alpha"
}

```
### Usage
The service offers a single `profile` method that accepts up to 20 MB of input data and produces results in JSON or CSV format. The service accepts input in Arabic, English, Japanese, or Spanish and can produce output in a variety of languages.

#### Instantiating and authenticating the service
Before you can send requests to the service it must be instantiated and credentials must be set.
```cs
// create a Personality Insights Service instance
PersonalityInsightsService _personalityInsights = new PersonalityInsightsService();

// set the credentials
_personalityInsights.SetCredential("<username>", "<password>");
```

#### Profile
Extract personality characteristics based on how a person writes.
```Cs
 // profile
 var results = _personalityInsights.GetProfile(ProfileOptions.CreateOptions()
                                                             .WithTextPlain()
                                                             .AsEnglish()
                                                             .AcceptJson()
                                                             .AcceptEnglishLanguage()
                                                             .WithBody("some text"));
```

[personality-insights]: http://www.ibm.com/watson/developercloud/personality-insights/api/v2/
[personality-models]: http://www.ibm.com/watson/developercloud/doc/personality-insights/models.shtml
[consumption-preferences]:http://www.ibm.com/watson/developercloud/doc/personality-insights/preferences.shtml
