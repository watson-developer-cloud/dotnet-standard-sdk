/**
* Copyright 2018 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public partial interface ISpeechToTextService
    {
        /// <summary>
        /// Retrieves information about the model. Returns information about a single specified language model that is available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz, among other things.
        /// </summary>
        /// <param name="modelId">The identifier of the desired model in the form of its `name` from the output of `GET /v1/models`.</param>
        /// <returns><see cref="SpeechModel" />SpeechModel</returns>
        SpeechModel GetModel(string modelId);

        /// <summary>
        /// Retrieves the models available for the service. Returns a list of all language models that are available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz, among other things.
        /// </summary>
        /// <returns><see cref="SpeechModels" />SpeechModels</returns>
        SpeechModels ListModels();
        
        /// <summary>
        /// Creates a custom language model. Creates a new custom language model for a specified base model. The custom language model can be used only with the base model for which it is created. The model is owned by the instance of the service whose credentials are used to create it.
        /// </summary>
        /// <param name="contentType">The type of the input.</param>
        /// <param name="createLanguageModel">A `CreateLanguageModel` object that provides basic information about the new custom language model.</param>
        /// <returns><see cref="LanguageModel" />LanguageModel</returns>
        LanguageModel CreateLanguageModel(string contentType, CreateLanguageModel createLanguageModel);

        /// <summary>
        /// Deletes a custom language model. Deletes an existing custom language model. The custom model cannot be deleted if another request, such as adding a corpus to the model, is currently being processed. You must use credentials for the instance of the service that owns a model to delete it.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model that is to be deleted. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteLanguageModel(string customizationId);

        /// <summary>
        /// Lists information about a custom language model. Lists information about a specified custom language model. You must use credentials for the instance of the service that owns a model to list information about it.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model for which information is to be returned. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="LanguageModel" />LanguageModel</returns>
        LanguageModel GetLanguageModel(string customizationId);

        /// <summary>
        /// Lists information about all custom language models. Lists information about all custom language models that are owned by an instance of the service. Use the `language` parameter to see all custom language models for the specified language; omit the parameter to see all custom language models for all languages. You must use credentials for the instance of the service that owns a model to list information about it.
        /// </summary>
        /// <param name="language">The identifier of the language for which custom language models are to be returned (for example, `en-US`). Omit the parameter to see all custom language models owned by the requesting service credentials. (optional)</param>
        /// <returns><see cref="LanguageModels" />LanguageModels</returns>
        LanguageModels ListLanguageModels(string language = null);

        /// <summary>
        /// Resets a custom language model. Resets a custom language model by removing all corpora and words from the model. Resetting a custom language model initializes the model to its state when it was first created. Metadata such as the name and language of the model are preserved, but the model's words resource is removed and must be re-created. You must use credentials for the instance of the service that owns a model to reset it.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model that is to be reset. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        object ResetLanguageModel(string customizationId);

        /// <summary>
        /// Trains a custom language model. Initiates the training of a custom language model with new corpora, custom words, or both. After adding, modifying, or deleting corpora or words for a custom language model, use this method to begin the actual training of the model on the latest data. You can specify whether the custom language model is to be trained with all words from its words resource or only with words that were added or modified by the user. You must use credentials for the instance of the service that owns a model to train it.   The training method is asynchronous. It can take on the order of minutes to complete depending on the amount of data on which the service is being trained and the current load on the service. The method returns an HTTP 200 response code to indicate that the training process has begun.   You can monitor the status of the training by using the `GET /v1/customizations/{customization_id}` method to poll the model's status. Use a loop to check the status every 10 seconds. The method returns a `Customization` object that includes `status` and `progress` fields. A status of `available` means that the custom model is trained and ready to use. The service cannot accept subsequent training requests, or requests to add new corpora or words, until the existing request completes.   Training can fail to start for the following reasons: * The service is currently handling another request for the custom model, such as another training request or a request to add a corpus or words to the model. * No training data (corpora or words) have been added to the custom model. * One or more words that were added to the custom model have invalid sounds-like pronunciations that you must fix.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model that is to be trained. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordTypeToAdd">The type of words from the custom language model's words resource on which to train the model: * `all` (the default) trains the model on all new words, regardless of whether they were extracted from corpora or were added or modified by the user. * `user` trains the model only on new words that were added or modified by the user; the model is not trained on new words extracted from corpora. (optional, default to all)</param>
        /// <param name="customizationWeight">Specifies a customization weight for the custom language model. The customization weight tells the service how much weight to give to words from the custom language model compared to those from the base model for speech recognition. Specify a value between 0.0 and 1.0. The default value is 0.3.   The default value yields the best performance in general. Assign a higher value if your audio makes frequent use of OOV words from the custom model. Use caution when setting the weight: a higher value can improve the accuracy of phrases from the custom model's domain, but it can negatively affect performance on non-domain phrases.   The value that you assign is used for all recognition requests that use the model. You can override it for any recognition request by specifying a customization weight for that request. (optional)</param>
        /// <returns><see cref="object" />object</returns>
        object TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null);

        /// <summary>
        /// Upgrades a custom language model. Initiates the upgrade of a custom language model to the latest version of its base language model. The upgrade method is asynchronous. It can take on the order of minutes to complete depending on the amount of data in the custom model and the current load on the service. A custom model must be in the `ready` or `available` state to be upgraded. You must use credentials for the instance of the service that owns a model to upgrade it.   The method returns an HTTP 200 response code to indicate that the upgrade process has begun successfully. You can monitor the status of the upgrade by using the `GET /v1/customizations/{customization_id}` method to poll the model's status. Use a loop to check the status every 10 seconds. While it is being upgraded, the custom model has the status `upgrading`. When the upgrade is complete, the model resumes the status that it had prior to upgrade. The service cannot accept subsequent requests for the model until the upgrade completes.   For more information, see [Upgrading custom models](https://console.bluemix.net/docs/services/speech-to-text/custom-upgrade.html).
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model that is to be upgraded. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        object UpgradeLanguageModel(string customizationId);
        /// <summary>
        /// Adds a corpus text file to a custom language model. Adds a single corpus text file of new training data to a custom language model. Use multiple requests to submit multiple corpus text files. You must use credentials for the instance of the service that owns a model to add a corpus to it. Note that adding a corpus does not affect the custom language model until you train the model for the new data by using the `POST /v1/customizations/{customization_id}/train` method.   Submit a plain text file that contains sample sentences from the domain of interest to enable the service to extract words in context. The more sentences you add that represent the context in which speakers use words from the domain, the better the service's recognition accuracy. For guidelines about adding a corpus text file and for information about how the service parses a corpus file, see [Preparing a corpus text file](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#prepareCorpus).   The call returns an HTTP 201 response code if the corpus is valid. The service then asynchronously processes the contents of the corpus and automatically extracts new words that it finds. This can take on the order of a minute or two to complete depending on the total number of words and the number of new words in the corpus, as well as the current load on the service. You cannot submit requests to add additional corpora or words to the custom model, or to train the model, until the service's analysis of the corpus for the current request completes. Use the `GET /v1/customizations/{customization_id}/corpora/{corpus_name}` method to check the status of the analysis.   The service auto-populates the model's words resource with any word that is not found in its base vocabulary; these are referred to as out-of-vocabulary (OOV) words. You can use the `GET /v1/customizations/{customization_id}/words` method to examine the words resource, using other words method to eliminate typos and modify how words are pronounced as needed.   To add a corpus file that has the same name as an existing corpus, set the allow_overwrite query parameter to true; otherwise, the request fails. Overwriting an existing corpus causes the service to process the corpus text file and extract OOV words anew. Before doing so, it removes any OOV words associated with the existing corpus from the model's words resource unless they were also added by another corpus or they have been modified in some way with the `POST /v1/customizations/{customization_id}/words` or `PUT /v1/customizations/{customization_id}/words/{word_name}` method.   The service limits the overall amount of data that you can add to a custom model to a maximum of 10 million total words from all corpora combined. Also, you can add no more than 30 thousand new custom words to a model; this includes words that the service extracts from corpora and words that you add directly.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model to which a corpus is to be added. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="corpusName">The name of the corpus that is to be added to the custom language model. The name cannot contain spaces and cannot be the string `user`, which is reserved by the service to denote custom words added or modified by the user. Use a localized name that matches the language of the custom model.</param>
        /// <param name="corpusFile">A plain text file that contains the training data for the corpus. Encode the file in UTF-8 if it contains non-ASCII characters; the service assumes UTF-8 encoding if it encounters non-ASCII characters. With cURL, use the `--data-binary` option to upload the file for the request.</param>
        /// <param name="allowOverwrite">Indicates whether the specified corpus is to overwrite an existing corpus with the same name. If a corpus with the same name already exists, the request fails unless `allowOverwrite` is set to `true`; by default, the parameter is `false`. The parameter has no effect if a corpus with the same name does not already exist. (optional, default to false)</param>
        /// <param name="corpusFileContentType">The content type of corpusFile. (optional)</param>
        /// <returns><see cref="object" />object</returns>
        object AddCorpus(string customizationId, string corpusName, System.IO.Stream corpusFile, bool? allowOverwrite = null, string corpusFileContentType = null);

        /// <summary>
        /// Deletes a corpus from a custom language model. Deletes an existing corpus from a custom language model. The service removes any out-of-vocabulary (OOV) words associated with the corpus from the custom model's words resource unless they were also added by another corpus or they have been modified in some way with the `POST /v1/customizations/{customization_id}/words` or `PUT /v1/customizations/{customization_id}/words/{word_name}` method. Removing a corpus does not affect the custom model until you train the model with the `POST /v1/customizations/{customization_id}/train` method. You must use credentials for the instance of the service that owns a model to delete its corpora.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model from which a corpus is to be deleted. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="corpusName">The name of the corpus that is to be deleted from the custom language model.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteCorpus(string customizationId, string corpusName);

        /// <summary>
        /// Lists information about a corpus for a custom language model. Lists information about a corpus from a custom language model. The information includes the total number of words and out-of-vocabulary (OOV) words, name, and status of the corpus. You must use credentials for the instance of the service that owns a model to list its corpora.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model for which a corpus is to be listed. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="corpusName">The name of the corpus about which information is to be listed.</param>
        /// <returns><see cref="Corpus" />Corpus</returns>
        Corpus GetCorpus(string customizationId, string corpusName);

        /// <summary>
        /// Lists information about all corpora for a custom language model. Lists information about all corpora from a custom language model. The information includes the total number of words and out-of-vocabulary (OOV) words, name, and status of each corpus. You must use credentials for the instance of the service that owns a model to list its corpora.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model for which corpora are to be listed. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="Corpora" />Corpora</returns>
        Corpora ListCorpora(string customizationId);
        /// <summary>
        /// Adds a custom word to a custom language model. Adds a custom word to a custom language model. The service populates the words resource for a custom model with out-of-vocabulary (OOV) words found in each corpus added to the model. You can use this method to add additional words or to modify existing words in the words resource. You must use credentials for the instance of the service that owns a model to add or modify a custom word for the model. Adding or modifying a custom word does not affect the custom model until you train the model for the new data by using the `POST /v1/customizations/{customization_id}/train` method.   Use the `word_name` path parameter to specify the custom word that is to be added or modified. Use the `CustomWord` object to provide one or both of the optional `sounds_like` and `display_as` fields for the word. * The `sounds_like` field provides an array of one or more pronunciations for the word. Use the parameter to specify how the word can be pronounced by users. Use the parameter for words that are difficult to pronounce, foreign words, acronyms, and so on. For example, you might specify that the word `IEEE` can sound like `i triple e`. You can specify a maximum of five sounds-like pronunciations for a word. For information about pronunciation rules, see [Using the sounds_like field](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#soundsLike). * The `display_as` field provides a different way of spelling the word in a transcript. Use the parameter when you want the word to appear different from its usual representation or from its spelling in corpora training data. For example, you might indicate that the word `IBM(trademark)` is to be displayed as `IBM`. For more information, see [Using the display_as field](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#displayAs).    If you add a custom word that already exists in the words resource for the custom model, the new definition overwrites the existing data for the word. If the service encounters an error, it does not add the word to the words resource. Use the `GET /v1/customizations/{customization_id}/words/{word_name}` method to review the word that you add.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model to which a word is to be added. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordName">The custom word that is to be added to or updated in the custom model. Do not include spaces in the word. Use a - (dash) or _ (underscore) to connect the tokens of compound words.</param>
        /// <param name="contentType">The type of the input.</param>
        /// <param name="customWord">A `CustomWord` object that provides information about the specified custom word. Specify an empty JSON object to add a word with no sounds-like or display-as information.</param>
        /// <returns><see cref="object" />object</returns>
        object AddWord(string customizationId, string wordName, string contentType, CustomWord customWord);

        /// <summary>
        /// Adds one or more custom words to a custom language model. Adds one or more custom words to a custom language model. The service populates the words resource for a custom model with out-of-vocabulary (OOV) words found in each corpus added to the model. You can use this method to add additional words or to modify existing words in the words resource. You must use credentials for the instance of the service that owns a model to add or modify custom words for the model. Adding or modifying custom words does not affect the custom model until you train the model for the new data by using the `POST /v1/customizations/{customization_id}/train` method.   You add custom words by providing a `Words` object, which is an array of `Word` objects, one per word. You must use the object's word parameter to identify the word that is to be added. You can also provide one or both of the optional `sounds_like` and `display_as` fields for each word. * The `sounds_like` field provides an array of one or more pronunciations for the word. Use the parameter to specify how the word can be pronounced by users. Use the parameter for words that are difficult to pronounce, foreign words, acronyms, and so on. For example, you might specify that the word `IEEE` can sound like `i triple e`. You can specify a maximum of five sounds-like pronunciations for a word. For information about pronunciation rules, see [Using the sounds_like field](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#soundsLike). * The `display_as` field provides a different way of spelling the word in a transcript. Use the parameter when you want the word to appear different from its usual representation or from its spelling in corpora training data. For example, you might indicate that the word `IBM(trademark)` is to be displayed as `IBM`. For more information, see [Using the display_as field](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#displayAs).    If you add a custom word that already exists in the words resource for the custom model, the new definition overwrites the existing data for the word. If the service encounters an error with the input data, it returns a failure code and does not add any of the words to the words resource.   The call returns an HTTP 201 response code if the input data is valid. It then asynchronously processes the words to add them to the model's words resource. The time that it takes for the analysis to complete depends on the number of new words that you add but is generally faster than adding a corpus or training a model.   You can monitor the status of the request by using the `GET /v1/customizations/{customization_id}` method to poll the model's status. Use a loop to check the status every 10 seconds. The method returns a `Customization` object that includes a `status` field. A status of `ready` means that the words have been added to the custom model. The service cannot accept requests to add new corpora or words or to train the model until the existing request completes.   You can use the `GET /v1/customizations/{customization_id}/words` or `GET /v1/customizations/{customization_id}/words/{word_name}` method to review the words that you add. Words with an invalid `sounds_like` field include an `error` field that describes the problem. You can use other words methods to correct errors, eliminate typos, and modify how words are pronounced as needed.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model to which words are to be added. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="contentType">The type of the input.</param>
        /// <param name="customWords">A `CustomWords` object that provides information about one or more custom words that are to be added to or updated in the custom language model.</param>
        /// <returns><see cref="object" />object</returns>
        object AddWords(string customizationId, string contentType, CustomWords customWords);

        /// <summary>
        /// Deletes a custom word from a custom language model. Deletes a custom word from a custom language model. You can remove any word that you added to the custom model's words resource via any means. However, if the word also exists in the service's base vocabulary, the service removes only the custom pronunciation for the word; the word remains in the base vocabulary. Removing a custom word does not affect the custom model until you train the model with the `POST /v1/customizations/{customization_id}/train` method. You must use credentials for the instance of the service that owns a model to delete its words.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model from which a word is to be deleted. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordName">The custom word that is to be deleted from the custom language model.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteWord(string customizationId, string wordName);

        /// <summary>
        /// Lists a custom word from a custom language model. Lists information about a custom word from a custom language model. You must use credentials for the instance of the service that owns a model to query information about its words.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model from which a word is to be queried. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordName">The custom word that is to be queried from the custom language model.</param>
        /// <returns><see cref="Word" />Word</returns>
        Word GetWord(string customizationId, string wordName);

        /// <summary>
        /// Lists all custom words from a custom language model. Lists information about custom words from a custom language model. You can list all words from the custom model's words resource, only custom words that were added or modified by the user, or only out-of-vocabulary (OOV) words that were extracted from corpora. You can also indicate the order in which the service is to return words; by default, words are listed in ascending alphabetical order. You must use credentials for the instance of the service that owns a model to query information about its words.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model from which words are to be queried. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordType">The type of words to be listed from the custom language model's words resource: * `all` (the default) shows all words. * `user` shows only custom words that were added or modified by the user. * `corpora` shows only OOV that were extracted from corpora. (optional, default to all)</param>
        /// <param name="sort">Indicates the order in which the words are to be listed, `alphabetical` or by `count`. You can prepend an optional `+` or `-` to an argument to indicate whether the results are to be sorted in ascending or descending order. By default, words are sorted in ascending alphabetical order. For alphabetical ordering, the lexicographical precedence is numeric values, uppercase letters, and lowercase letters. For count ordering, values with the same count are ordered alphabetically. With cURL, URL encode the `+` symbol as `%2B`. (optional, default to alphabetical)</param>
        /// <returns><see cref="Words" />Words</returns>
        Words ListWords(string customizationId, string wordType = null, string sort = null);
        /// <summary>
        /// Creates a custom acoustic model. Creates a new custom acoustic model for a specified base model. The custom acoustic model can be used only with the base model for which it is created. The model is owned by the instance of the service whose credentials are used to create it.
        /// </summary>
        /// <param name="contentType">The type of the input.</param>
        /// <param name="createAcousticModel">A `CreateAcousticModel` object that provides basic information about the new custom acoustic model.</param>
        /// <returns><see cref="AcousticModel" />AcousticModel</returns>
        AcousticModel CreateAcousticModel(string contentType, CreateAcousticModel createAcousticModel);

        /// <summary>
        /// Deletes a custom acoustic model. Deletes an existing custom acoustic model. The custom model cannot be deleted if another request, such as adding an audio resource to the model, is currently being processed. You must use credentials for the instance of the service that owns a model to delete it.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model that is to be deleted. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteAcousticModel(string customizationId);

        /// <summary>
        /// Lists information about a custom acoustic model. Lists information about a specified custom acoustic model. You must use credentials for the instance of the service that owns a model to list information about it.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model for which information is to be returned. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="AcousticModel" />AcousticModel</returns>
        AcousticModel GetAcousticModel(string customizationId);

        /// <summary>
        /// Lists information about all custom acoustic models. Lists information about all custom acoustic models that are owned by an instance of the service. Use the `language` parameter to see all custom acoustic models for the specified language; omit the parameter to see all custom acoustic models for all languages. You must use credentials for the instance of the service that owns a model to list information about it.
        /// </summary>
        /// <param name="language">The identifier of the language for which custom acoustic models are to be returned (for example, `en-US`). Omit the parameter to see all custom acoustic models owned by the requesting service credentials. (optional)</param>
        /// <returns><see cref="AcousticModels" />AcousticModels</returns>
        AcousticModels ListAcousticModels(string language = null);

        /// <summary>
        /// Resets a custom acoustic model. Resets a custom acoustic model by removing all audio resources from the model. Resetting a custom acoustic model initializes the model to its state when it was first created. Metadata such as the name and language of the model are preserved, but the model's audio resources are removed and must be re-created. You must use credentials for the instance of the service that owns a model to reset it.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model that is to be reset. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        object ResetAcousticModel(string customizationId);

        /// <summary>
        /// Trains a custom acoustic model. Initiates the training of a custom acoustic model with new or changed audio resources. After adding or deleting audio resources for a custom acoustic model, use this method to begin the actual training of the model on the latest audio data. The custom acoustic model does not reflect its changed data until you train it. You must use credentials for the instance of the service that owns a model to train it.   The training method is asynchronous. It can take on the order of minutes or hours to complete depending on the total amount of audio data on which the model is being trained and the current load on the service. Typically, training takes approximately twice the length of the total audio contained in the custom model. The method returns an HTTP 200 response code to indicate that the training process has begun.   You can monitor the status of the training by using the `GET /v1/acoustic_customizations/{customization_id}` method to poll the model's status. Use a loop to check the status once a minute. The method returns an `Customization` object that includes `status` and `progress` fields. A status of `available` indicates that the custom model is trained and ready to use. The service cannot accept subsequent training requests, or requests to add new audio resources, until the existing request completes.   You can use the optional `custom_language_model_id` query parameter to specify the GUID of a separately created custom language model that is to be used during training. Specify a custom language model if you have verbatim transcriptions of the audio files that you have added to the custom model or you have either corpora (text files) or a list of words that are relevant to the contents of the audio files. For information about creating a separate custom language model, see [Creating a custom language model](https://console.bluemix.net/docs/services/speech-to-text/language-create.html).   Training can fail to start for the following reasons: * The service is currently handling another request for the custom model, such as another training request or a request to add audio resources to the model. * The custom model contains less than 10 minutes or more than 50 hours of audio data. * One or more of the custom model's audio resources is invalid.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model that is to be trained. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customLanguageModelId">The GUID of a custom language model that is to be used during training of the custom acoustic model. Specify a custom language model that has been trained with verbatim transcriptions of the audio resources or that contains words that are relevant to the contents of the audio resources. (optional)</param>
        /// <returns><see cref="object" />object</returns>
        object TrainAcousticModel(string customizationId, string customLanguageModelId = null);

        /// <summary>
        /// Upgrades a custom acoustic model. Initiates the upgrade of a custom acoustic model to the latest version of its base language model. The upgrade method is asynchronous. It can take on the order of minutes or hours to complete depending on the amount of data in the custom model and the current load on the service; typically, upgrade takes approximately twice the length of the total audio contained in the custom model. A custom model must be in the `ready` or `available` state to be upgraded. You must use credentials for the instance of the service that owns a model to upgrade it.   The method returns an HTTP 200 response code to indicate that the upgrade process has begun successfully. You can monitor the status of the upgrade by using the `GET /v1/acoustic_customizations/{customization_id}` method to poll the model's status. Use a loop to check the status once a minute. While it is being upgraded, the custom model has the status `upgrading`. When the upgrade is complete, the model resumes the status that it had prior to upgrade. The service cannot accept subsequent requests for the model until the upgrade completes.   If the custom acoustic model was trained with a separately created custom language model, you must use the `custom_language_model_id` query parameter to specify the GUID of that custom language model. The custom language model must be upgraded before the custom acoustic model can be upgraded. Omit the parameter if the custom acoustic model was not trained with a custom language model.   For more information, see [Upgrading custom models](https://console.bluemix.net/docs/services/speech-to-text/custom-upgrade.html).
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model that is to be upgraded. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customLanguageModelId">If the custom acoustic model was trained with a custom language model, the GUID of that custom language model. The custom language model must be upgraded before the custom acoustic model can be upgraded. (optional)</param>
        /// <returns><see cref="object" />object</returns>
        object UpgradeAcousticModel(string customizationId, string customLanguageModelId = null);
        /// <summary>
        /// Adds an audio resource to a custom acoustic model. Adds an audio resource to a custom acoustic model. Add audio content that reflects the acoustic characteristics of the audio that you plan to transcribe. You must use credentials for the instance of the service that owns a model to add an audio resource to it. Adding audio data does not affect the custom acoustic model until you train the model for the new data by using the `POST /v1/acoustic_customizations/{customization_id}/train` method.   You can add individual audio files or an archive file that contains multiple audio files. Adding multiple audio files via a single archive file is significantly more efficient than adding each file individually. * You can add an individual audio file in any format that the service supports for speech recognition. Use the `Content-Type` header to specify the format of the audio file. * You can add an archive file (**.zip** or **.tar.gz** file) that contains audio files in any format that the service supports for speech recognition. All audio files added with the same archive file must have the same audio format. Use the `Content-Type` header to specify the archive type, `application/zip` or `application/gzip`. Use the `Contained-Content-Type` header to specify the format of the contained audio files; the default format is `audio/wav`.   You can use this method to add any number of audio resources to a custom model by calling the method once for each audio or archive file. But the addition of one audio resource must be fully complete before you can add another. You must add a minimum of 10 minutes and a maximum of 50 hours of audio that includes speech, not just silence, to a custom acoustic model before you can train it. No audio resource, audio- or archive-type, can be larger than 100 MB.   The method is asynchronous. It can take several seconds to complete depending on the duration of the audio and, in the case of an archive file, the total number of audio files being processed. The service returns a 201 response code if the audio is valid. It then asynchronously analyzes the contents of the audio file or files and automatically extracts information about the audio such as its length, sampling rate, and encoding. You cannot submit requests to add additional audio resources to a custom acoustic model, or to train the model, until the service's analysis of all audio files for the current request completes.   To determine the status of the service's analysis of the audio, use the `GET /v1/acoustic_customizations/{customization_id}/audio/{audio_name}` method to poll the status of the audio. The method accepts the GUID of the custom model and the name of the audio resource, and it returns the status of the resource. Use a loop to check the status of the audio every few seconds until it becomes `ok`.   **Note:** The sampling rate of an audio file must match the sampling rate of the base model for the custom model: for broadband models, at least 16 kHz; for narrowband models, at least 8 kHz. If the sampling rate of the audio is higher than the minimum required rate, the service down-samples the audio to the appropriate rate. If the sampling rate of the audio is lower than the minimum required rate, the service labels the audio file as `invalid`.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model to which an audio resource is to be added. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="audioName">The name of the audio resource that is to be added to the custom acoustic model. The name cannot contain spaces. Use a localized name that matches the language of the custom model.</param>
        /// <param name="audioResource">The audio resource that is to be added to the custom acoustic model, an individual audio file or an archive file.</param>
        /// <param name="contentType">The type of the input: application/zip, application/gzip, audio/basic, audio/flac, audio/l16, audio/mp3, audio/mpeg, audio/mulaw, audio/ogg, audio/ogg;codecs=opus, audio/ogg;codecs=vorbis, audio/wav, audio/webm, audio/webm;codecs=opus, or audio/webm;codecs=vorbis.</param>
        /// <param name="containedContentType">For an archive-type resource that contains audio files whose format is not `audio/wav`, specifies the format of the audio files. The header accepts all of the audio formats supported for use with speech recognition and with the `Content-Type` header, including the `rate`, `channels`, and `endianness` parameters that are used with some formats. For a complete list of supported audio formats, see [Audio formats](/docs/services/speech-to-text/input.html#formats). (optional, default to audio/wav)</param>
        /// <param name="allowOverwrite">Indicates whether the specified audio resource is to overwrite an existing resource with the same name. If a resource with the same name already exists, the request fails unless `allowOverwrite` is set to `true`; by default, the parameter is `false`. The parameter has no effect if a resource with the same name does not already exist. (optional, default to false)</param>
        /// <returns><see cref="object" />object</returns>
        object AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null);

        /// <summary>
        /// Deletes an audio resource from a custom acoustic model. Deletes an existing audio resource from a custom acoustic model. Deleting an archive-type audio resource removes the entire archive of files; the current interface does not allow deletion of individual files from an archive resource. Removing an audio resource does not affect the custom model until you train the model on its updated data by using the `POST /v1/acoustic_customizations/{customization_id}/train` method. You must use credentials for the instance of the service that owns a model to delete its audio resources.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model from which an audio resource is to be deleted. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="audioName">The name of the audio resource that is to be deleted from the custom acoustic model.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteAudio(string customizationId, string audioName);

        /// <summary>
        /// Lists information about an audio resource for a custom acoustic model. Lists information about an audio resource from a custom acoustic model. The method returns an `AudioListing` object whose fields depend on the type of audio resource you specify with the method's `audio_name` parameter: * **For an audio-type resource,** the object's fields match those of an `AudioResource` object: `duration`, `name`, `details`, and `status`. * **For an archive-type resource,** the object includes a `container` field whose fields match those of an `AudioResource` object. It also includes an `audio` field, which contains an array of `AudioResource` objects that provides information about the audio files that are contained in the archive.   The information includes the status of the specified audio resource, which is important for checking the service's analysis of the resource in response to a request to add it to the custom model. You must use credentials for the instance of the service that owns a model to list its audio resources.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model for which an audio resource is to be listed. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="audioName">The name of the audio resource about which information is to be listed.</param>
        /// <returns><see cref="AudioListing" />AudioListing</returns>
        AudioListing GetAudio(string customizationId, string audioName);

        /// <summary>
        /// Lists information about all audio resources for a custom acoustic model. Lists information about all audio resources from a custom acoustic model. The information includes the name of the resource and information about its audio data, such as its duration. It also includes the status of the audio resource, which is important for checking the service's analysis of the resource in response to a request to add it to the custom acoustic model. You must use credentials for the instance of the service that owns a model to list its audio resources.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom acoustic model for which audio resources are to be listed. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="AudioResources" />AudioResources</returns>
        AudioResources ListAudio(string customizationId);
    }
}
