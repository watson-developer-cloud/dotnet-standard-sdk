# Watson Developer Cloud .NET Standard SDK
[![Build status](https://ci.appveyor.com/api/projects/status/bcbl2ripwdmh1918/branch/master?svg=true)](https://ci.appveyor.com/project/mediumTaj/dotnet-standard-sdk/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/watson-developer-cloud/dotnet-standard-sdk/badge.svg?branch=master)](https://coveralls.io/github/watson-developer-cloud/dotnet-standard-sdk?branch=master)
[![wdc-community.slack.com](https://wdc-slack-inviter.mybluemix.net/badge.svg)](http://wdc-slack-inviter.mybluemix.net/)
[![CLA assistant](https://cla-assistant.io/readme/badge/watson-developer-cloud/dotnet-standard-sdk)](https://cla-assistant.io/watson-developer-cloud/dotnet-standard-sdk)
[![semantic-release](https://img.shields.io/badge/%20%20%F0%9F%93%A6%F0%9F%9A%80-semantic--release-e10079.svg)](https://github.com/semantic-release/semantic-release)
[![Documentation](https://img.shields.io/badge/documentation-API-blue.svg)][dotnet-standard-sdk-documentation]

The .NET Standard SDK uses [Watson][wdc] services, a collection of REST APIs that use cognitive computing to solve complex problems.

## Announcements
### Natural Language Classifier deprecation
On 9 August 2021, IBM announced the deprecation of the Natural Language Classifier service.The service will no longer be available from 8 August 2022. As of 9 September 2021, you will not be able to create new instances. Existing instances will be supported until 8 August 2022. Any instance that still exists on that date will be deleted.

As an alternative, we encourage you to consider migrating to the Natural Language Understanding service on IBM Cloud that uses deep learning to extract data and insights from text such as keywords, categories, sentiment, emotion, and syntax, along with advanced multi-label text classification capabilities, to provide even richer insights for your business or industry. For more information, see [Migrating to Natural Language Understanding](https://cloud.ibm.com/docs/natural-language-classifier?topic=natural-language-classifier-migrating).

### Updating endpoint URLs from watsonplatform.net
Watson API endpoint URLs at watsonplatform.net are changing and will not work after 26 May 2021. Update your calls to use the newer endpoint URLs. For more information, see https://cloud.ibm.com/docs/watson?topic=watson-endpoint-change.

### Personality Insights deprecation
IBM Watson™ Personality Insights is discontinued. For a period of one year from 1 December 2020, you will still be able to use Watson Personality Insights. However, as of 1 December 2021, the offering will no longer be available.

As an alternative, we encourage you to consider migrating to IBM Watson™ [Natural Language Understanding](https://cloud.ibm.com/docs/natural-language-understanding), a service on IBM Cloud® that uses deep learning to extract data and insights from text such as keywords, categories, sentiment, emotion, and syntax to provide insights for your business or industry. For more information, see About Natural Language Understanding.

### Visual Recognition deprecation
IBM Watson™ Visual Recognition is discontinued. Existing instances are supported until 1 December 2021, but as of 7 January 2021, you can't create instances. Any instance that is provisioned on 1 December 2021 will be deleted.

### Compare and Comply deprecation
IBM Watson™ Compare and Comply is discontinued. Existing instances are supported until 30 November 2021, but as of 1 December 2020, you can't create instances. Any instance that exists on 30 November 2021 will be deleted. Consider migrating to Watson Discovery Premium on IBM Cloud for your Compare and Comply use cases. To start the migration process, visit https://ibm.biz/contact-wdc-premium.

## Before you begin
Ensure you have the following prerequisites:

* You need an [IBM Cloud][ibm-cloud-onboarding] account.
* Install [Visual Studio][visual-studio-download] for Windows, OSX or Linux.
* Install [.NET Core][dotnet-core-download].

## Installing the Watson .NET Standard SDK
This SDK provides classes and methods to access the following Watson services:

* [Assistant](https://www.ibm.com/cloud/watson-assistant)
* Compare Comply (deprecated)
* [Discovery](https://www.ibm.com/cloud/watson-discovery)
* [Language Translator](https://www.ibm.com/cloud/watson-language-translator)
* [Natural Language Understanding](https://www.ibm.com/cloud/watson-natural-language-understanding)
* [Natural Language Classifier](https://www.ibm.com/cloud/watson-natural-language-classifier)
* Personality Insights (deprecated)
* [Speech to Text](https://www.ibm.com/cloud/watson-speech-to-text)
* [Text to Speech](https://www.ibm.com/cloud/watson-text-to-speech)
* [Tone Analyzer](https://www.ibm.com/cloud/watson-tone-analyzer)
* Visual Recognition (deprecated)

You can get the latest SDK packages through [NuGet](https://www.nuget.org) or manually [here][latest_release].

## .NET Standard 2.0
The Watson .NET Standard SDK conforms to .NET Standard 2.0. It is implemented by .NET Core 2.0, .NET Framework 4.6.1 and Mono 5.4. See [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) for details.

## Authentication
Watson services are migrating to token-based Identity and Access Management (IAM) authentication.

- With some service instances, you authenticate to the API by using **[IAM](#iam)**.
- In other instances, you authenticate by providing the **[username and password](#username-and-password)** for the service instance.
- If you're using a Watson service on ICP, you'll need to authenticate in a [specific way](#icp).

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
    IamAuthenticator authenticator = new IamAuthenticator(
        apikey: "{apikey}");
    var service = new AssistantService("{versionDate}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
}
```

#### Supplying the access token
```cs
void Example()
{
    BearerTokenAuthenticator authenticator = new BearerTokenAuthenticator(
        bearerToken: "{bearerToken}");
    var service = new AssistantService("{versionDate}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
}
```

### Username and password
```cs
void Example()
{
    BasicAuthenticator authenticator = new BasicAuthenticator(
        username: "{username}",
        password: "{password}");
    var service = new AssistantService("{versionDate}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
}
```

#### ICP
Like IAM, you can pass in credentials to let the SDK manage an access token for you or directly supply an access token to do it yourself.

##### Letting the SDK manage the token
```cs
void Example()
{
    CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
        url: "https://{cp4d_cluster_host}{:port}",
        username: "{username}",
        password: "{password}");
    var service = new AssistantService("{version-date}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
    var results = service.Message("{workspace-id}", "{message-request}");
}
```

##### Managing the token yourself
```cs
void Example()
{
    BearerTokenAuthenticator authenticator = new BearerTokenAuthenticator(
        bearerToken: "{bearerToken}");
    var service = new AssistantService("{version-date}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
    var results = service.Message("{workspace-id}", "{message-request}");
}
```
Be sure to both [disable SSL verification](#self-signed-certificates) when authenticating and set the endpoint explicitly to the URL given in ICP.

### Supplying credentials

There are two ways to supply the credentials you found above to the SDK for authentication.

#### Credential file (easier!)

With a credential file, you just need to put the file in the right place and the SDK will do the work of parsing it and authenticating. You can get this file by clicking the **Download** button for the credentials in the **Manage** tab of your service instance.

The file downloaded will be called `ibm-credentials.env`. This is the name the SDK will search for and **must** be preserved unless you want to configure the file path (more on that later). The SDK will look for your `ibm-credentials.env` file in the following places (in order):

- The top-level directory of the project you're using the SDK in
- Your system's home directory

As long as you set that up correctly, you don't have to worry about setting any authentication options in your code. So, for example, if you created and downloaded the credential file for your Discovery instance, you just need to do the following:

```cs
AssistantService service = new AssistantService("{version-date}");
var listWorkspacesResult = service.ListWorkspaces();
```

And that's it!

If you're using more than one service at a time in your code and get two different `ibm-credentials.env` files, just put the contents together in one `ibm-credentials.env` file and the SDK will handle assigning credentials to their appropriate services.

If you would like to configure the location/name of your credential file, you can set an environment variable called `IBM_CREDENTIALS_FILE`. **This will take precedence over the locations specified above.** Here's how you can do that:

```bash
export IBM_CREDENTIALS_FILE="{path}"
```

where `{path}` is something like `/home/user/Downloads/{file_name}.env`.

#### Manually

If you'd prefer to set authentication values manually in your code, the SDK supports that as well. The way you'll do this depends on what type of credentials your service instance gives you.

## Custom Request Headers
You can send custom request headers by adding them to the service using `.WithHeader({key}, {value})`.
```cs
void Example()
{
    IamAuthenticator authenticator = new IamAuthenticator(
        apikey: "{apikey}");
    var service = new AssistantService("{version-date}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
    service.WithHeader("X-Watson-Metadata", "customer_id=some-assistant-customer-id");
    var results = service.Message("{workspace-id}", "{message-request}");
}
```

## Response Headers, Status Code and Raw Json
You can get the response headers, status code and the raw json response in the result object.
```cs
void Example()
{
    IamAuthenticator authenticator = new IamAuthenticator(
        apikey: "{apikey}");
    var service = new AssistantService("{version-date}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
    var results = service.Message("{workspace-id}", "{message-request}");
    
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
    CloudPakForDataAuthenticator authenticator = new CloudPakForDataAuthenticator(
        url: "https://{cp4d_cluster_host}{:port}",
        username: "{username}",
        password: "{password}",
        disableSslVerification: true);
    var service = new AssistantService("{version-date}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
    var results = service.Message("{workspace-id}", "{message-request}");
}
```

## Transaction IDs
Every SDK call returns a response with a transaction ID in the `X-Global-Transaction-Id` header. Together the service instance region, this ID helps support teams troubleshoot issues from relevant logs.

```cs
AssistantService service = new AssistantService("{version-date}");
DetailedResponse<WorkspaceCollection> listWorkspacesResult = null;

try
{
    listWorkspacesResult = service.ListWorkspaces();

    //  Global transaction id on successful api call
    listWorkspacesResult.Headers.TryGetValue("x-global-transaction-id", out object globalTransactionId);
}
catch(Exception e)
{
    //  Global transaction on failed api call is contained in the error message
    Console.WriteLine("error: " + e.Message);
}
```

However, the transaction ID isn't available when the API doesn't return a response for some reason. In that case, you can set your own transaction ID in the request. For example, replace `<my-unique-transaction-id>` in the following example with a unique transaction ID.

```cs
void Example()
{
    IamAuthenticator authenticator = new IamAuthenticator(
        apikey: "{apikey}");
    var service = new AssistantService("{version-date}", authenticator);
    service.SetServiceUrl("{serviceUrl}");
    service.WithHeader("X-Global-Transaction-Id", "<my-unique-transaction-id>");
    var results = service.Message("{workspace-id}", "{message-request}");
}
```

## Use behind a proxy
To use the SDK behind an HTTP proxy, you need to set either the `http_proxy` or `https_proxy` environment variable.
You can set this in the application environment using:
```
set http_proxy=http://proxy.server.com:3128
```
from the cmd.

You can also set this in the application using:
```cs
Environment.SetEnvironmentVariable("http_proxy", "http://proxy.server.com:3128");
```

## Documentation
Click [here][dotnet-standard-sdk-documentation] for documentation by release and branch.

## Questions

If you have issues with the APIs or have a question about the Watson services, see [Stack Overflow](https://stackoverflow.com/questions/tagged/ibm-watson+dotnet).

## Open Source @ IBM
Find more open source projects on the [IBM Github Page][ibm-github].

## License
This library is licensed under Apache 2.0. Full license text is available in [LICENSE](LICENSE).

## Contributing
See [CONTRIBUTING.md](.github/CONTRIBUTING.md).

## Featured projects
We'd love to highlight cool open-source projects that use this SDK! If you'd like to get your project added to the list, feel free to make an issue linking us to it.

[wdc]: https://www.ibm.com/watson/developer/
[ibm-github]: http://ibm.github.io/

[latest_release]: https://github.com/watson-developer-cloud/dotnet-standard-sdk/releases/latest
[dw-answers]: https://developer.ibm.com/answers/questions/ask/?topics=watson
[stack-overflow]: http://stackoverflow.com/questions/ask?tags=ibm-watson

[language_translator]: https://www.ibm.com/watson/developercloud/language-translator/api/v2/
[natural_language_understanding]: https://www.ibm.com/watson/developercloud/natural-language-understanding/api/v1/
[personality_insights]: https://www.ibm.com/watson/developercloud/personality-insights/api/v2/
[speech_to_text]: https://www.ibm.com/watson/developercloud/speech-to-text/api/v1/
[text_to_speech]: https://www.ibm.com/watson/developercloud/text-to-speech/api/v1/
[tone_analyzer]: https://www.ibm.com/watson/developercloud/tone-analyzer/api/v3/
[visual_recognition]: https://www.ibm.com/watson/developercloud/visual-recognition/api/v3/

[natural_language_classifier]: https://www.ibm.com/watson/developercloud/natural-language-classifier/api/v1/

[dotnet-core-download]: https://www.microsoft.com/net/download/core
[visual-studio-download]: https://www.visualstudio.com/vs/community/
[dotnet-standard-sdk-documentation]: https://watson-developer-cloud.github.io/dotnet-standard-sdk/
[ibm-cloud-onboarding]: http://cloud.ibm.com/registration?target=/developer/watson&cm_sp=WatsonPlatform-WatsonServices-_-OnPageNavLink-IBMWatson_SDKs-_-DotNet
