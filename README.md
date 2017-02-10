# Watson Developer Cloud .NET Standard SDK
[![Build status](https://ci.appveyor.com/api/projects/status/onwtuv5a6qsg4jd8?svg=true)](https://ci.appveyor.com/project/atilatosta/dotnet-standard-sdk)
[![Coverage Status](https://coveralls.io/repos/github/atilatosta/dotnet-standard-sdk/badge.svg?branch=gh-8-continuousIntegration)](https://coveralls.io/github/atilatosta/dotnet-standard-sdk?branch=gh-8-continuousIntegration)

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

* [Speech to Text](/src/IBM.WatsonDeveloperCloud.SpeechToText)
* [Text to Speech](/src/IBM.WatsonDeveloperCloud.TextToSpeech)
* [Conversation](/src/IBM.WatsonDeveloperCloud.Conversation)
<!-- * [Discovery](/src/IBM.WatsonDeveloperCloud.Discovery) -->
<!-- * [Visual Recognition](/src/IBM.WatsonDeveloperCloud.VisualRecognition) -->
* [Language Translator](/src/IBM.WatsonDeveloperCloud.LanguageTranslator)
* [Tone Analyzer](/src/IBM.WatsonDeveloperCloud.ToneAnalyzer)
* [Personality Insights](/src/IBM.WatsonDeveloperCloud.PersonalityInsights)

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
<TODO latest release url>
[latest_release]: https://github.com/watson-developer-cloud/dotnet-standard-sdk/releases/latest
[dw-answers]: https://developer.ibm.com/answers/questions/ask/?topics=watson
[stack-overflow]: http://stackoverflow.com/questions/ask?tags=ibm-watson

[conversation]:http://www.ibm.com/watson/developercloud/doc/conversation/
[discovery]: http://www.ibm.com/watson/developercloud/discovery/api/v1/
[speech_to_text]: http://www.ibm.com/watson/developercloud/doc/speech-to-text/
[text_to_speech]: http://www.ibm.com/watson/developercloud/doc/text-to-speech/
[visual_recognition]: http://www.ibm.com/watson/developercloud/visual-recognition/api/v3/
[language_translator]: http://www.ibm.com/watson/developercloud/doc/language-translator/
[personality_insights]: http://www.ibm.com/watson/developercloud/personality-insights/api/v2/
[tone_analyzer]: http://www.ibm.com/watson/developercloud/doc/tone-analyzer/

[document_conversion]: http://www.ibm.com/watson/developercloud/document-conversion/api/v1/
[retrieve_and_rank]: http://www.ibm.com/watson/developercloud/retrieve-and-rank/api/v1/
[natural_language_classifier]: http://www.ibm.com/watson/developercloud/doc/natural-language-classifier/index.html
[alchemyData_news]: http://www.ibm.com/watson/developercloud/alchemy-data-news.html
[tradeoff_analytics]: http://www.ibm.com/watson/developercloud/doc/tradeoff-analytics/

[dotnet-core-download]: https://www.microsoft.com/net/download/core
[visual-studio-download]: https://www.visualstudio.com/vs/community/
[visual-studio-code-download]: https://code.visualstudio.com/
[dotnet-standard-sdk-documentation]: https://watson-developer-cloud.github.io/dotnet-standard-sdk/
