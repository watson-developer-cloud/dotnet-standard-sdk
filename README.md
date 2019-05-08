# Watson Developer Cloud .NET Standard SDK
[![Build status](https://ci.appveyor.com/api/projects/status/bcbl2ripwdmh1918/branch/development?svg=true)](https://ci.appveyor.com/project/mediumTaj/dotnet-standard-sdk/branch/development)
[![Coverage Status](https://coveralls.io/repos/github/watson-developer-cloud/dotnet-standard-sdk/badge.svg?branch=development)](https://coveralls.io/github/watson-developer-cloud/dotnet-standard-sdk?branch=development)
[![wdc-community.slack.com](https://wdc-slack-inviter.mybluemix.net/badge.svg)](http://wdc-slack-inviter.mybluemix.net/)
[![CLA assistant](https://cla-assistant.io/readme/badge/watson-developer-cloud/dotnet-standard-sdk)](https://cla-assistant.io/watson-developer-cloud/dotnet-standard-sdk)
[![semantic-release](https://img.shields.io/badge/%20%20%F0%9F%93%A6%F0%9F%9A%80-semantic--release-e10079.svg)](https://github.com/semantic-release/semantic-release)
[![Documentation](https://img.shields.io/badge/documentation-API-blue.svg)][dotnet-standard-sdk-documentation]

The .Net SDK uses [Watson][wdc] services, a collection of REST APIs and SDKs that use cognitive computing to solve complex problems.

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

* You need an [IBM Cloud][ibm-cloud-onboarding] account.
* Install [Visual Studio][visual-studio-download] for Windows or [Visual Studio Code][visual-studio-code-download] for OSX or Linux.
* Install [.NET Core][dotnet-core-download].

## Installing the Watson .NET Standard SDK
You can get the latest SDK packages through NuGet. Installation instructions can be found in the ReadMe of each package.

* [Assistant V1](/src/IBM.WatsonDeveloperCloud.Assistant.v1)
* [Assistant V2](/src/IBM.WatsonDeveloperCloud.Assistant.v2)
* [Compare Comply](/src/IBM.WatsonDeveloperCloud.CompareComply.v1)
* [Discovery](/src/IBM.WatsonDeveloperCloud.Discovery.v1)
* [Language Translator V3](/src/IBM.WatsonDeveloperCloud.LanguageTranslator.v3)
* [Natural Language Understanding](/src/IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1)
* [Natural Language Classifier](/src/IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1)
* [Personality Insights](/src/IBM.WatsonDeveloperCloud.PersonalityInsights.v3)
* [Speech to Text](/src/IBM.WatsonDeveloperCloud.SpeechToText.v1)
* [Text to Speech](/src/IBM.WatsonDeveloperCloud.TextToSpeech.v1)
* [Tone Analyzer](/src/IBM.WatsonDeveloperCloud.ToneAnalyzer.v3)
* [Visual Recognition](/src/IBM.WatsonDeveloperCloud.VisualRecognition.v3)

Or manually [here][latest_release].

## Authentication
Watson services are migrating to token-based Identity and Access Management (IAM) authentication.

- With some service instances, you authenticate to the API by using **[IAM](#iam)**.
- In other instances, you authenticate by providing the **[username and password](#username-and-password)** for the service instance.

### Getting credentials
To find out which authentication to use, view the service credentials. You find the service credentials for authentication the same way for all Watson services:

1. Go to the IBM Cloud [Dashboard](https://cloud.ibm.com/) page.
1. Either click an existing Watson service instance or click [**Create resource > AI**](https://cloud.ibm.com/catalog?category=ai) and create a service instance.
1. Copy the `url` and either `apikey` or `username` and `password`. Click **Show** if the credentials are masked.

In your code, you can use these values in the service constructor or with a method call after instantiating your service.

### IAM

Some services use token-based Identity and Access Management (IAM) authentication. IAM authentication uses a service API key to get an access token that is passed with the call. Access tokens are valid for approximately one hour and must be regenerated.

You supply either an IAM service **API key** or an **access token**:

- Use the API key to have the SDK manage the lifecycle of the access token. The SDK requests an access token, ensures that the access token is valid, and refreshes it if necessary.
- Use the access token if you want to manage the lifecycle yourself. For details, see [Authenticating with IAM tokens](https://cloud.ibm.com/docs/services/watson?topic=watson-iam). If you want to switch to API key override your stored IAM credentials with an IAM API key.

#### Supplying the IAM API key
```cs
void Example()
{
    TokenOptions iamAssistantTokenOptions = new TokenOptions()
    {
        IamApiKey = "<iam-apikey>",
        ServiceUrl = "<service-endpoint>"
    };

    _assistant = new AssistantService(iamAssistantTokenOptions, "<version-date>");
}
```

#### Supplying the access token
```cs
void Example()
{
    TokenOptions iamAssistantTokenOptions = new TokenOptions()
    {
        IamAccessToken = "<iam-access-token>"
    };

    _assistant = new AssistantService(iamAssistantTokenOptions, "<version-date>");
}
```

### Username and password
```cs
void Example()
{
    _assistant = new AssistantService("<username>", "<password>", "<version-date>");
}
```

### Supplying credentials

There are two ways to supply the credentials you found above to the SDK for authentication.

#### Credential file (easier!)

With a credential file, you just need to put the file in the right place and the SDK will do the work of parsing it and authenticating. You can get this file by clicking the **Download** button for the credentials in the **Manage** tab of your service instance.

The file downloaded will be called `ibm-credentials.env`. This is the name the SDK will search for and **must** be preserved unless you want to configure the file path (more on that later). The SDK will look for your `ibm-credentials.env` file in the following places (in order):

- Your system's home directory
- The top-level directory of the project you're using the SDK in

As long as you set that up correctly, you don't have to worry about setting any authentication options in your code. So, for example, if you created and downloaded the credential file for your Discovery instance, you just need to do the following:

```cs
AssistantService assistantService = new AssistantService();
var listWorkspacesResult = assistantService.ListWorkspaces();
```

And that's it!

If you're using more than one service at a time in your code and get two different `ibm-credentials.env` files, just put the contents together in one `ibm-credentials.env` file and the SDK will handle assigning credentials to their appropriate services.

If you would like to configure the location/name of your credential file, you can set an environment variable called `IBM_CREDENTIALS_FILE`. **This will take precedence over the locations specified above.** Here's how you can do that:

```bash
export IBM_CREDENTIALS_FILE="<path>"
```

where `<path>` is something like `/home/user/Downloads/<file_name>.env`.

#### Manually

If you'd prefer to set authentication values manually in your code, the SDK supports that as well. The way you'll do this depends on what type of credentials your service instance gives you.

## Custom Request Headers
You can send custom request headers by adding them to the service using `.WithHeader(<key>, <value>)`.
```cs
void Example()
{
    AssistantService assistant = new AssistantService("<username>", "<password>", "<version-date>");
    assistant.WithHeader("X-Watson-Metadata", "customer_id=some-assistant-customer-id");
    var results = assistant.Message("<workspace-id>", "<message-request>");
}
```

## Response Headers, Status Code and Raw Json
You can get the response headers, status code and the raw json response in the result object.
```cs
void Example()
{
    AssistantService assistant = new AssistantService("<username>", "<password>", "<version-date>");
    var results = assistant.Message("<workspace-id>", "<message-request>");
    
    var responseHeaders = results.Headers;  //  The response headers
    var responseJson = results.Response;    //  The raw response json
    var statusCode = results.StatusCode;    //  The response status code
}
```

## Self signed certificates
You can disable SSL verification on calls to Watson (only do this if you really mean to!).
```cs
void Example()
{
    AssistantService assistant = new AssistantService("<username>", "<password>", "<version-date>");
    assistant.DisableSslVerification(true);
    var results = assistant.Message("<workspace-id>", "<message-request>");
}
```

## Documentation
Click [here][dotnet-standard-sdk-documentation] for documentation by release and branch.

## Questions

If you are having difficulties using the APIs or have a question about the IBM Watson Services, please ask a question on [dW Answers][dw-answers] or [Stack Overflow][stack-overflow].

## Open Source @ IBM
Find more open source projects on the [IBM Github Page][ibm-github].

## License
This library is licensed under Apache 2.0. Full license text is available in [LICENSE](LICENSE).

## Contributing
See [CONTRIBUTING.md](.github/CONTRIBUTING.md).<TODO revise coding standard>

## Featured projects
We'd love to highlight cool open-source projects that use this SDK! If you'd like to get your project added to the list, feel free to make an issue linking us to it.

[wdc]: https://www.ibm.com/watson/developer/
[ibm-github]: http://ibm.github.io/

[latest_release]: https://github.com/watson-developer-cloud/dotnet-standard-sdk/releases/latest
[dw-answers]: https://developer.ibm.com/answers/questions/ask/?topics=watson
[stack-overflow]: http://stackoverflow.com/questions/ask?tags=ibm-watson

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
[ibm-cloud-onboarding]: http://cloud.ibm.com/registration?target=/developer/watson&cm_sp=WatsonPlatform-WatsonServices-_-OnPageNavLink-IBMWatson_SDKs-_-DotNet
