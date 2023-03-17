# [7.0.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v6.1.0...v7.0.0) (2023-03-17)


### Bug Fixes

* **all:** use correct csproj version ([7c40755](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/7c4075580b0122b271b4b55f7a61b92f7bca4b27))
* **assistantv2:** discriminator hand edits ([6c51a6e](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/6c51a6e24b6eb068891932de164a51bf59eeb21c))
* **assistantv2:** final edits ([1b032ce](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/1b032ce382522e51e80751fb9995019abd2c02e2))
* **nlu:** hand edits ([de18cc7](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/de18cc7e340bfb256c205ca1fc5006900605d17a))


### Features

* **assistantv2:** add several new functions ([0f5dff8](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/0f5dff8eab633a207a9ed9025361c6d8c98c62e6))
* **discov2:** new aggregation types ([f0ab5dd](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/f0ab5ddd41348c75227f259bff20e378cc1be0ba))
* **nlu:** remove all sentimentModel functions ([b70f481](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/b70f481763abc25f437a060a8b4fe9d2e961aa4e))
* **stt:** add and remove models ([3096572](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/3096572712914686d1b4d088d3df0050b970aee4))
* **tts:** add params and model constants ([f32894a](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/f32894aed31599727a5fda15ec1b77e7cc4504e8))


### BREAKING CHANGES

* **discov2:** smartDocumentUnderstanding param removed
* **discov2:** QueryAggregation structure changed
* **discov2:** object type properties changed to Dictionaries
* **assistantv2:** removing and changing of classes
* **nlu:** remove all sentimentModel functions and models

# [6.1.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v6.0.0...v6.1.0) (2022-08-10)


### Features

* **assistant-v1:** update models and add new methods ([2b8a2fc](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/2b8a2fcec27d79f26222a69fc46a0e56eff8ec6e))
* **assistant-v2:** update models and add new methods ([ca838f9](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/ca838f90913c41866b80acff978a42ec72c3f852))
* **discovery-v2:** update models and add new methods ([495295f](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/495295f8008e02ccb882265ae9ca1347c16ef6a1))
* **nlu:** add new parameter to create/updateClassificationsModel ([e7d0b01](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/e7d0b0118b4d67115f44fd79cbbdb29f2898f7c5))
* **stt:** add and remove method parameters ([dbbc35f](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/dbbc35f727ad9988bab05f434e8904ac7f7a8ad9))
* **tts:** add method parameters ([44f4095](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/44f409504b6c552e8c18f525c0bc62a5f968c69f))

# [6.0.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v5.3.0...v6.0.0) (2022-03-21)


### Features

* **assistant-v1:** add models for DialogNodeOutputGeneric ([fe49277](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/fe49277d12b6795df234907aff0c247b4a46b2ec))
* **assistant-v1:** generated using api-def: master & generator: 3.46.0 ([67d58b7](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/67d58b78d98a4b9af7d545430996e1ebfacd313a))
* **assistant-v2:** generated using api-def: master & generator: 3.46.0 ([d0d6ca2](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/d0d6ca2db261939fcfd5f7879f43906c65893859))
* **deprecation:** remove CC, NLC, PI, Tone Analyzer, and Visual Recognition ([a70c7a5](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/a70c7a5b6fdfc6f5905fbe0ceb5a28680c42a50a))
* **discovery-v1:** document status & query aggregation update ([4faac38](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/4faac38941b570f03aa02af3ed44e1c36d0cc9b6))
* **language-translator-v3:** update core version ([21b564c](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/21b564c46b32bd73abf38ac4814f7366cea0e51e))
* **natural-language-understanding-v1:** metadata as a dictionary and comments updated ([42cc44c](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/42cc44ca129a3f3c109c4978cd032b4b85946269))
* **speech-to-text-v1:** add de-de_multimedia & update comments ([30fa39c](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/30fa39c63ecd1ec0de58b62f589229b7a3a698ed))
* **speech-to-text-v1:** supportedFeatures: customAcousticModel property added & update comments ([7224286](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/722428602e96a5fbc0def0177651e48922bc723e))
* **text-to-speech-v1:** add voices and update comments ([2ebd9df](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/2ebd9dfd61489a8456d0e872d4610087bdca2d18))


### BREAKING CHANGES

* **discovery-v1:** QueryAggregation: BREAKING QueryAggregation subclasses changed.
* **assistant-v2:** MessageOutputDebug: BREAKING nodesVisited type DialogNodesVisited changed to
DialogNodeVisited, RuntimeEntity: BREAKING optional metadata property removed
* **assistant-v1:** OutputData: BREAKING required text property removed, RuntimeEntity: BREAKING
optional metadata property removed

# [5.3.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v5.2.1...v5.3.0) (2021-09-14)


### Bug Fixes

* **assistant-v2:** add transferInfo property for assistant v2 ([8d25195](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/8d25195001d59bb07ffbdb385a20beaea46666a0))
* **assistant-v2:** update assistant v2 add new Model, SearchResultAnswer ([856ffd8](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/856ffd8d5edffdfd6cccc2bdb60d3825ccbd0faa))
* **discovery-v1:** fix Status Details property change from authentication to authenticated ([7b9769a](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/7b9769a41efaaedd5dd7f65c8af46e2a1defdaaf))
* **discovery-v1:** update status from string to StatusDetails ([c39d890](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/c39d890d819a9d08bb511249ae00541d5621d52d))
* **discovery-v2:** manual revert fileContent method under AnalyzeDocument to the original code ([b047d7f](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/b047d7f67010d0b8659429675a6449878e97e49b))
* **formatting:** update comments and fomatting changes ([05c7a67](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/05c7a6786b06009104f4ce6af716158e8dccf56c))
* **formatting:** update comments and formatting changes ([c506219](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/c5062191f026e710a64c6db2cd37b93751527981))
* **integration-test:** update return type from response.Result.Models in TestListClassificationModel ([a72c99b](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/a72c99b002008c7a3cd905ca2db66edd5fd5ba8a))
* **manual-changes:** fix runtime error ([9dc83ea](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/9dc83ea2f6f6e3eca73ac496ed9f50fe1f510383))
* **manual-changes:** remote transfer_infor out from runtimeResponseGeneric for assistant-v2 ([295ce72](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/295ce72da051338b05c441c88dca747b5980cbba))
* **nlu:** fix listClassificationsModels through return type change and removal of unnecessary model ([c488dac](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/c488dac28259fc696718f6c4f4eeb5cbcb1e603c))
* **nlu:** manual change: convert MetadataOptions back to Object on Model/Features ([b545942](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/b5459426755cfb3d26d0c8fbc1f6602a3a037a9a))
* **unit-test:** fix unit test for diccovery-v1 ([3dddd1e](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/3dddd1e02f5223450bcfce5bf55c7d5f4eaa9085))
* **unit-test:** update unit test for discovery v1 for converting status from string to StatusDetails ([06438cc](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/06438ccc94995139c1123e260bacdd4f2cdfd5d7))


### Features

* **assistant-v1:** add more enums for Disambiguation settings ([3508067](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/35080676f062a578de277c72b492a467c4c98ae5))
* **assistant-v1:** alt_text property added to Image response type ([b805a7b](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/b805a7b8a15e2e8c9cddb8266abe8a2076bffd2e))
* **assistant-v2:** alt_text property added to Image response type ([98448da](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/98448daaf5bc8035a92ed090a07d699e38fdacc4))
* **assistant-v2:** session_start_time and state properties added to MessageContextGlobalSystem ([fcecbd2](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/fcecbd29878d29ed6768cdce3f9546412492748c))
* **discovery-2:** enum update for CreateProjectConstants ([b16b165](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/b16b1651dc25b390fefa280d36d97bb5f2683c62))
* **stt:** more languages supported for next generation models ([603bd99](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/603bd99ae8bd9a733298c85f64776575a6596370))
* **tts:** new voice models added ([7ed8976](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/7ed8976747c165176899b7cd7183e6dbfcc2356d))

## [5.2.1](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v5.2.0...v5.2.1) (2021-08-25)


### Bug Fixes

* **nlc:** add deprecation warning ([a2f15e9](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/a2f15e9fa232ae13eca16d290cb2e80be8653c41)), closes [#9626](https://github.com/watson-developer-cloud/dotnet-standard-sdk/issues/9626)

# [5.2.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v5.1.0...v5.2.0) (2021-06-09)


### Bug Fixes

* **appveyor:** update appveyor env variables ([2807fa5](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/2807fa5552874a6351c165f355d7fab3443dda87))
* **appveyor:** update credentials ([1e2a691](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/1e2a6912293ccb50e84c5ff7a181038aedcedc8a))
* **appveyor:** update sdk-credentials ([39b025f](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/39b025f1b8406c3d80a3ebd2c43ae67f168c2bae))
* **readme:** trigger release ([af21e9e](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/af21e9eb6811c5340140aae20edb4211386c1cc3))
* **text-to-speech-v1:** generated using api def sdk-2021-05-11-rerelease and gen 3.31.0 ([0e2378a](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/0e2378a4370312db75084677f4c869cd03fa2f55))


### Features

* **generation:** generated using api def sdk-2021-05-11 & gen 3.31.0 ([7cca912](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/7cca912bdce601135bc29dbb89530803299d71df))

# [5.1.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v5.0.1...v5.1.0) (2021-02-16)


### Features

* **csproj:** update core & common version ([d89daea](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/d89daeaca3da8c7fb15e18c913f9df18f5c52466))

## [5.0.1](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v5.0.0...v5.0.1) (2020-12-21)


### Bug Fixes

* **generation:** api def '8be1cdc78c7998b055bc8ea895dddd7c8496b2a4' gen tag 3.19.0 ([9b96f89](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/9b96f89879332eda362390b0c24fd3a9421cfbfa))

# [5.0.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v4.6.0...v5.0.0) (2020-12-10)


### Bug Fixes

* **discovery-v2:** change fileContent from ByteArrayContent to StreamContent ([b6d5d70](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/b6d5d7040a976e45bfeef9fdaff732ea3354ed81))
* **examples:** update examples ([5e85f39](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/5e85f398cdffb41b51e3cb4d881665d0b7a446f3))


### Features

* **generation:** api def tag 'sdk-major-release-2020' gen tag 3.19.0 ([d691cf8](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/d691cf89df175550e7bdd281e298038a39481b24))
* **generation:** generated with 3.17.0 generator and sdk-major-release-2020-rc01 api def ([d28fa35](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/d28fa355501971df36c5b975f9474b2d4cb4a933))


### BREAKING CHANGES

* **generation:** api def tag 'sdk-major-release-2020' gen tag 3.19.0
* **generation:** generated with 3.17.0 generator and sdk-major-release-2020-rc01 api def

# [4.6.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v4.5.0...v4.6.0) (2020-08-25)


### Features

* **generation:** api def commit '5da1939e280efa7018a4ea31adf574f9d8db4c5b' and gen tag '2.3.1' ([4c90711](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/4c907119540d9c1f09170a9b910cc827dd97d4c5))

# [4.5.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v4.4.0...v4.5.0) (2020-06-03)


### Features

* Regenerated with support for Assistant v2 stateless messaging and Visual Recognition v4 local models ([97a2e72](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/97a2e72142f7f37d533eff7488e72f0763102cdd))

# [4.4.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v4.3.1...v4.4.0) (2020-04-24)


### Features

* Regenerate using 2.2.5 and 1de49c12418c0baece9d6e81cad91142fdd201c9 ([5eeffe3](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/5eeffe35f201bcbdbb8e381aadc5c313a3d816f3))
* Regenerate using sdk-2020-04-16 and generator 2.2.5 ([4fdf2c6](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/4fdf2c6ad1ea346c5c76e197307a5c4b13a105e4))
* Regenerate with 2.2.5 and c13e63c37da6e360d26e73be35da3b34ae95a075 ([0efef30](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/0efef30ddb54b156f77ed7ccceb138689901273e))
* Regenerated using 2.2.5 and 293b8fd00ce3041b7d899af4534b85a89f5b963f ([279978b](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/279978b6ad349681f8c1100558ca545ed216de94))

## [4.3.1](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v4.3.0...v4.3.1) (2020-02-14)


### Bug Fixes

* **NSubstitute:** Restore dependency version ([9249310](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/9249310be8964616d4242c5d2b2fca645fa0ae31))

# [4.3.0](https://github.com/watson-developer-cloud/dotnet-standard-sdk/compare/v4.2.1...v4.3.0) (2020-02-13)


### Bug Fixes

* **Build:** Update gh token, added quiet to troublesome powershell commands ([a785427](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/a785427f9f01def472982fe8bb830d4af233dfd4))


### Features

* **Regeneration:** Regenerate using generator v2.2.4 and API def sdk-2020-02-10 ([78e83d6](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/78e83d69f3d10296c51f3e5d1754d3461deb6577))
* Regenerate SDK ([412a693](https://github.com/watson-developer-cloud/dotnet-standard-sdk/commit/412a69388e54ed28cc46c39595970d0eac6ea20b))

Moved to [https://github.com/watson-developer-cloud/dotnet-standard-sdk/wiki/Change-Log](https://github.com/watson-developer-cloud/dotnet-standard-sdk/wiki/Change-Log)
