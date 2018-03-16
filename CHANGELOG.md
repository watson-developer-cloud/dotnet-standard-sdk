Change Log
==========
## Version 2.1.0
_2018-03-16_
* New: Generate Watson Assistant.

## Version 2.0.0
_2018-03-09_
* Breaking Change: Migrate to Visual Studio 2017.
* Breaking Change: Regenerate all services using latest code generator & templates.
* New: Revised integration tests.
* New: Added documentation about publishing a release.
* New: Generate Speech to Text service.
* New: Generate Text to Speech service.
* New: Generate Natural Language Classifier service.
* New: Generate Visual Recognition service.

## Version 1.3.1
_2018-01-09_
* Fix: Change `Context` in `MessageRequest` to `dynamic`.

## Version 1.3.0
_2018-01-07_
* New: Generate Language Translator service.
* Fix: Update Visual Recognition service to reflect API changes.
* New: Use credential service for testing credentials.

## Version 1.2.0
_2017-07-18_
* Fix: Update input and context in MessageRequest and MessageResponse for `Conversation` to be dynamic.
* Fix: Changes in error handling to check if return error is a string or json object.
* New: Conversation example showing messaging with conversation context.

## Version 1.1.0
_2017-06-30_
* Fix: Include support for Xamarin Android, Xamarin iOS and .NET Core App (4.6>).

## Version 1.0.0
_2017-06_19_
* Breaking Change: Refactor SDK to integrate generated services including adding service version to the package namespace.
* New: Generate services for `Personality Insights`, `Tone Analyzer`, `Conversation`, `Natural Language Understanding` and `Discovery` via Swagger Codegen.
* Integration and unit test for all generated services.

## Version 0.2.0
_2017-04-25_

* New: Abstracted `Visual Recognition` service.
* New: Completed `Speech to Text` abstraction.
* New: Completed `Conversation` abstraction.
* New: Integration and Unit tests.
* New: Example service calls.
* Fix: Enabled fail build on failed test in AppVeyor.
* Fix: Fixed all integration and unit tests.

## Version 0.1.0-alpha
_2017-02-12_

Initial alpha release of .NET Standard SDK. Service supported include:
* Conversation
* Speech to Text
* Text to Speech
* Language Translator
* Personality Insights
* Tone Analyzer
