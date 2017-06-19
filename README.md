# Watson Developer Cloud .NET Standard SDK
[![Build status](https://ci.appveyor.com/api/projects/status/bcbl2ripwdmh1918/branch/development?svg=true)](https://ci.appveyor.com/project/mediumTaj/dotnet-standard-sdk/branch/development)
[![Coverage Status](https://coveralls.io/repos/github/watson-developer-cloud/dotnet-standard-sdk/badge.svg?branch=development)](https://coveralls.io/github/watson-developer-cloud/dotnet-standard-sdk?branch=development)

The .Net SDK uses the [Watson Developer Cloud][wdc] services, a collection of REST APIs and SDKs that use cognitive computing to solve complex problems.

## Table of Contents
* [Before you begin](#before-you-begin)
* [Installing the Watson .NET Standard SDK](#installing-the-watson-net-standard-sdk)
* [Documentation](#documentation)
* [Questions](#questions)
* [Open Source @ IBM](#open-source--ibm)
* [License](#license)
* [Contributing](#contributing)

## Before you begin
Ensure you have the following prerequisites:

* An IBM Bluemix account. If you don't have one, [sign up][bluemix_registration].
* Install [Visual Studio][visual-studio-download] for Windows or [Visual Studio Code][visual-studio-code-download] for OSX or Linux.
* Install [.NET Core][dotnet-core-download].

## Installing the Watson .NET Standard SDK
You can get the latest SDK packages through NuGet. Installation instructions can be found in the ReadMe of each package.

* [Conversation](/src/IBM.WatsonDeveloperCloud.Conversation.v1)
* [Discovery](/src/IBM.WatsonDeveloperCloud.Discovery.v1)
* [Language Translator](/src/IBM.WatsonDeveloperCloud.LanguageTranslator.v2)
* [Natural Language Understanding](/src/IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1)
* [Personality Insights](/src/IBM.WatsonDeveloperCloud.PersonalityInsights.v3)
* [Speech to Text](/src/IBM.WatsonDeveloperCloud.SpeechToText.v1)
* [Text to Speech](/src/IBM.WatsonDeveloperCloud.TextToSpeech.v1)
* [Tone Analyzer](/src/IBM.WatsonDeveloperCloud.ToneAnalyzer.v3)
* [Visual Recognition](/src/IBM.WatsonDeveloperCloud.VisualRecognition.v3)

Or manually [here][latest_release].

## Documentation
Click [here][dotnet-standard-sdk-documentation] for documentation by release and branch.

## Questions

If you are having difficulties using the APIs or have a question about the IBM Watson Services, please ask a question on
[dW Answers][dw-answers]
or [Stack Overflow][stack-overflow].

## Open Source @ IBM
Find more open source projects on the [IBM Github Page][ibm-github].

## License
This library is licensed under Apache 2.0. Full license text is available in [LICENSE](LICENSE).

## Contributing
See [CONTRIBUTING.md](.github/CONTRIBUTING.md).<TODO revise coding standard>

[wdc]: http://www.ibm.com/watson/developercloud/
[bluemix_registration]: http://bluemix.net/registration
[ibm-github]: http://ibm.github.io/

[latest_release]: https://github.com/watson-developer-cloud/dotnet-standard-sdk/releases/latest
[dw-answers]: https://developer.ibm.com/answers/questions/ask/?topics=watson
[stack-overflow]: http://stackoverflow.com/questions/ask?tags=ibm-watson

[conversation]:https://www.ibm.com/watson/developercloud/conversation/api/v1/
[discovery]: https://www.ibm.com/watson/developercloud/discovery/api/v1/
[language_translator]: https://www.ibm.com/watson/developercloud/language-translator/api/v2/
[natural_language_understanding]: https://www.ibm.com/watson/developercloud/natural-language-understanding/api/v1/
[personality_insights]: https://www.ibm.com/watson/developercloud/personality-insights/api/v2/
[speech_to_text]: https://www.ibm.com/watson/developercloud/speech-to-text/api/v1/
[text_to_speech]: https://www.ibm.com/watson/developercloud/text-to-speech/api/v1/
[tone_analyzer]: https://www.ibm.com/watson/developercloud/tone-analyzer/api/v3/
[visual_recognition]: https://www.ibm.com/watson/developercloud/visual-recognition/api/v3/

[document_conversion]: https://www.ibm.com/watson/developercloud/document-conversion/api/v1/
[retrieve_and_rank]: https://www.ibm.com/watson/developercloud/retrieve-and-rank/api/v1/
[natural_language_classifier]: https://www.ibm.com/watson/developercloud/natural-language-classifier/api/v1/
[tradeoff_analytics]: https://www.ibm.com/watson/developercloud/tradeoff-analytics/api/v1/

[dotnet-core-download]: https://www.microsoft.com/net/download/core
[visual-studio-download]: https://www.visualstudio.com/vs/community/
[visual-studio-code-download]: https://code.visualstudio.com/
[dotnet-standard-sdk-documentation]: https://watson-developer-cloud.github.io/dotnet-standard-sdk/
