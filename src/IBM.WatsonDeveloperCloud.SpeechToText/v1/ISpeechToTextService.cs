/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

using System.IO;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using System.Collections.Generic;
using System.Net.Http;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public interface ISpeechToTextService
    {
        /// <summary>
        /// Retrieves a list of all models available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz.
        /// </summary>
        /// <returns>Returns a ModelSet object that contains the information about an available model that is provided in a JSON Model object.</returns>
        SpeechModelSet GetModels();

        /// <summary>
        /// Retrieves information about a single specified model that is available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz. 
        /// </summary>
        /// <param name="modelName">The identifier of the desired model</param>
        /// <returns>Returns a Model object that contains the information about the specified model that is provided in a JSON Model object</returns>
        SpeechModel GetModel(string modelName);

        /// <summary>
        /// Creates a session and locks recognition requests to that engine.You can use the session for multiple recognition requests so that each request is processed with the same engine.
        /// The session expires after 30 seconds of inactivity.Use getRecognizeStatus to prevent the session from expiring.
        /// </summary>
        /// <param name="modelName">The identifier of the model to be used by the new session</param>
        /// <returns>Returns a Session object that contains the information about the new session that is provided in a JSON Session object.</returns>
        Session CreateSession(string modelName);

        /// <summary>
        /// Provides a way to check whether the specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The returned state must be initialized to indicate that you can send another recognition request.
        /// </summary>
        /// <param name="session">A Session object that identifies the session whose status is to be checked.</param>
        /// <returns>Returns RecognizeStatus object that contains the information about the session that is provided in a JSON SessionStatusobject.</returns>
        RecognizeStatus GetSessionStatus(Session session);

        /// <summary>
        /// Provides a way to check whether the specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The returned state must be initialized to indicate that you can send another recognition request.
        /// </summary>
        /// <param name="sessionId">The identifier for the session.</param>
        /// <returns>Returns RecognizeStatus object that contains the information about the session that is provided in a JSON SessionStatus object.</returns>
        RecognizeStatus GetSessionStatus(string sessionId);

        /// <summary>
        /// Deletes an existing session and its engine. You cannot send requests to a session after it is deleted.
        /// </summary>
        /// <param name="session">A Session object that identifies the session to be deleted</param>
        object DeleteSession(Session session);

        /// <summary>
        /// Deletes an existing session and its engine. You cannot send requests to a session after it is deleted.
        /// </summary>
        /// <param name="sessionId">A Session Id that identifies the session to be deleted</param>
        object DeleteSession(string sessionId);

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="audio"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="continuous"></param>
        /// <param name="inactivityTimeout"></param>
        /// <param name="keywords"></param>
        /// <param name="keywordsThreshold"></param>
        /// <param name="maxAlternatives"></param>
        /// <param name="wordAlternativesThreshold"></param>
        /// <param name="wordConfidence"></param>
        /// <param name="timestamps"></param>
        /// <param name="profanityFilter"></param>
        /// <param name="smartFormatting"></param>
        /// <param name="speakerLabels"></param>
        /// <returns></returns>
        SpeechRecognitionEvent Recognize(string contentType,
                                         Stream audio,
                                         string transferEncoding = "",
                                         string model = "en-US_BroadbandModel",
                                         string customizationId = "",
                                         bool? continuous = null,
                                         int? inactivityTimeout = null,
                                         string[] keywords = null,
                                         double? keywordsThreshold = null,
                                         int? maxAlternatives = null,
                                         double? wordAlternativesThreshold = null,
                                         bool? wordConfidence = null,
                                         bool? timestamps = null,
                                         bool profanityFilter = false,
                                         bool? smartFormatting = null,
                                         bool? speakerLabels = null);

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="metaData"></param>
        /// <param name="audio"></param>
        /// <returns></returns>
        SpeechRecognitionEvent Recognize(string contentType,
                                         Metadata metaData,
                                         Stream audio,
                                         string transferEncoding = "",
                                         string model = "en-US_BroadbandModel",
                                         string customizationId = "");

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="audio"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="continuous"></param>
        /// <param name="inactivityTimeout"></param>
        /// <param name="keywords"></param>
        /// <param name="keywordsThreshold"></param>
        /// <param name="maxAlternatives"></param>
        /// <param name="wordAlternativesThreshold"></param>
        /// <param name="wordConfidence"></param>
        /// <param name="timestamps"></param>
        /// <param name="profanityFilter"></param>
        /// <param name="smartFormatting"></param>
        /// <param name="speakerLabels"></param>
        /// <returns></returns>
        SpeechRecognitionEvent RecognizeWithSession(string sessionId,
                                                    string contentType,
                                                    Stream audio,
                                                    string transferEncoding = "",
                                                    string model = "en-US_BroadbandModel",
                                                    string customizationId = "",
                                                    bool? continuous = null,
                                                    int? inactivityTimeout = null,
                                                    string[] keywords = null,
                                                    double? keywordsThreshold = null,
                                                    int? maxAlternatives = null,
                                                    double? wordAlternativesThreshold = null,
                                                    bool? wordConfidence = null,
                                                    bool? timestamps = null,
                                                    bool profanityFilter = false,
                                                    bool? smartFormatting = null,
                                                    bool? speakerLabels = null);

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="metaData"></param>
        /// <param name="audio"></param>
        /// <returns></returns>
        SpeechRecognitionEvent RecognizeWithSession(string sessionId,
                                                    string contentType,
                                                    Metadata metaData,
                                                    Stream audio,
                                                    string transferEncoding,
                                                    string model,
                                                    string customizationId);

        /// <summary>
        /// Requests results for a recognition task within a specified session. You can submit this method multiple times for the same recognition task. To see interim results, set the interim_results parameter to true. The request must pass the cookie that was returned by the SpeechToTextService.CreateSession(string)  method. 
        /// To see results for a specific recognition task, specify a sequence ID(with the sequence_id query parameter) that matches the sequence ID of the recognition request.A request with a sequence ID can arrive before, during, or after the matching recognition request, but it must arrive no later than 30 seconds after the recognition completes to avoid a session timeout(response code 408). Send multiple requests for the sequence ID with a maximum gap of 30 seconds to avoid the timeout.
        /// Omit the sequence ID to observe results for an ongoing recognition task.If no recognition task is ongoing, the method returns results for the next recognition task regardless of whether it specifies a sequence ID. 
        /// </summary>
        /// <param name="sessionId">The identifier of the session whose results you want to observe.</param>
        /// <param name="sequenceId">The sequence ID of the recognition task whose results you want to observe. Omit the parameter to obtain results either for an ongoing recognition, if any, or for the next recognition task regardless of whether it specifies a sequence ID.</param>
        /// <param name="interimResults">Indicates whether the service is to return interim results. If true, interim results are returned as a stream of JSON objects; each object represents a single SpeechRecognitionEvent. If false, the response is a single SpeechRecognitionEvent with final results only.</param>
        /// <returns></returns>
        List<SpeechRecognitionEvent> ObserveResult(string sessionId, int? sequenceId = (int?)null, bool interimResults = false);

        /// <summary>
        /// Creates a new custom language model for a specified base language model. The custom language model can be used only with the base language model for which it is created. The new model is owned by the individual whose service credentials are used to create it.
        /// </summary>
        /// <param name="model">The model name.</param>
        /// <param name="baseModelName">The name of the base model to use.</param>
        /// <param name="description">Description of the custom model.</param>
        /// <returns></returns>
        CustomizationID CreateCustomModel(string model, string baseModelName, string description);

        /// <summary>
        /// Creates a new custom language model for a specified base language model. The custom language model can be used only with the base language model for which it is created. The new model is owned by the individual whose service credentials are used to create it.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        CustomizationID CreateCustomModel(CustomModel options);

        /// <summary>
        /// Lists information about all custom language models that are owned by the calling user. Use the language parameter to see all custom models for the specified language; omit the parameter to see the custom models for all languages. 
        /// </summary>
        /// <param name="language">The language for which custom models are to be returned: en-US, ja-JP </param>
        /// <returns></returns>
        Customizations ListCustomModels(string language = "en-US");

        /// <summary>
        /// Lists information about a specified custom language model. Only the owner of a custom model can use this method to query information about the model. 
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model for which information is to be returned. You must make the request with the service credentials of the model's owner.</param>
        /// <returns>The method returns a single instance of a Customization object that provides information about the specified model. </returns>
        Customization ListCustomModel(string customizationId);

        /// <summary>
        /// Initiates the training of a custom language model with new corpora, custom words, or both. After adding corpora or words to the custom model, use this method to begin the actual training of the model on the new data. You can specify whether the custom model is to be trained with all words from its words resources or only with words that were added or modified by the user. Only the owner of a custom model can use this method to train the model.
        /// This method is asynchronous and can take on the order of minutes to complete depending on the amount of data on which the service is being trained and the current load on the service. The method returns an HTTP 200 response code to indicate that the training process has begun.
        /// You can monitor the status of the training by using the List a custom model method to poll the model's status. Use a loop to check the status every 10 seconds. The method returns a Customization object that includes status and progress fields. A status of available means that the custom model is trained and ready to use. If training is in progress, the progress field indicates the progress of the training as a percentage complete. The service cannot accept subsequent training requests, or requests to add new corpora or words, until the existing request completes.
        /// </param>
        object TrainCustomModel(string customizationId, string wordTypeToAdd = "all");

        /// <summary>
        /// Resets a custom language model by removing all corpora and words from the model. Resetting a custom model initializes the model to its state when it was first created. Metadata such as the name and language of the model are preserved. Only the owner of a custom model can use this method to reset the model.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model that is to be reset. You must make the request with the service credentials of the model's owner.</param>
        object ResetCustomModel(string customizationId);

        /// <summary>
        /// Upgrades a custom language model to the latest release level of the Speech to Text service. The method bases the upgrade on the latest trained data stored for the custom model. If the corpora or words for the model have changed since the model was last trained, you must use the Train a custom model method to train the model on the new data. Only the owner of a custom model can use this method to upgrade the model. 
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model that is to be upgraded. You must make the request with the service credentials of the model's owner.</param>
        object UpgradeCustomModel(string customizationId);

        /// <summary>
        /// Deletes an existing custom language model. The custom model cannot be deleted if another request, such as adding a corpus to the model, is currently being processed. Only the owner of a custom model can use this method to delete the model.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model that is to be deleted. You must make the request with the service credentials of the model's owner.</param>
        object DeleteCustomModel(string customizationId);

        /// <summary>
        /// Adds a single corpus text file of new training data to the custom language model. Use multiple requests to submit multiple corpus text files. Only the owner of a custom model can use this method to add a corpus to the model. Note that adding a corpus does not affect the custom model until you train the model for the new data by using the Train a custom model method.
        /// Submit a plain text file that contains sample sentences from the domain of interest to enable the service to extract words in context. The more sentences you add that represent the context in which speakers use words from the domain, the better the service's recognition accuracy. For guidelines about adding a corpus text file and for information about how the service parses a corpus file, see Preparing a corpus file.
        /// The call returns an HTTP 201 response code if the corpus is valid. The service then asynchronously pre-processes the contents of the corpus and automatically extracts new words that it finds. This can take on the order of a minute or two to complete depending on the total number of words and the number of new words in the corpus, as well as the current load on the service. You cannot submit requests to add additional corpora or words to the custom model, or to train the model, until the service's analysis of the corpus for the current request completes. Use the List a corpus method to check the status of the analysis. 
        /// The service auto-populates the model's words resource with any word that is not found in its base vocabulary; these are referred to as out-of-vocabulary (OOV) words. You can use the List custom words method to examine the words resource. If necessary, you can use the Add custom words or Add a custom word method to correct problems, eliminate typographical errors, and modify how words are pronounced.
        /// To add a corpus file that has the same name as an existing corpus, set the allow_overwrite parameter to true; otherwise, the request fails. Overwriting an existing corpus causes the service to process the corpus text file and extract OOV words anew. Before doing so, it removes any OOV words associated with the existing corpus from the model's words resource unless they were also added by another corpus or they have been modified in some way by the user.
        /// The service limits the overall amount of data that you can add to a custom model to a maximum of 10 million total words from all corpora combined. Also, you can add no more than 30 thousand new custom words to a model; this includes words that the service extracts from corpora and words that you add directly.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model to which a corpus is to be added. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="corpusName">The name of the corpus that is to be added. The name cannot contain spaces and cannot be the string user, which is reserved by the service to denote custom words added or modified by the user. Use a localized name that matches the language of the custom model.</param>
        /// <param name="allowOverwrite">Indicates whether the specified corpus is to overwrite an existing corpus with the same name. If a corpus with the same name already exists, the request fails unless allow_overwrite is set to true; by default, the parameter is false. The parameter has no effect if a corpus with the same name does not already exist.</param>
        /// <param name="body">A plain text file that contains the training data for the corpus. Encode the file in UTF-8 if it contains non-ASCII characters; the service assumes UTF-8 encoding if it encounters non-ASCII characters. With cURL, use the --data-binary option to upload the file for the request.</param>
        object AddCorpus(string customizationId, string corpusName, bool allowOverwrite, Stream body);

        /// <summary>
        /// Lists information about all corpora that have been added to the specified custom language model. The information includes the total number of words and out-of-vocabulary (OOV) words, name, and status of each corpus. Only the owner of a custom model can use this method to list the model's corpora.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model for which corpora are to be listed. You must make the request with the service credentials of the model's owner.</param>
        /// <returns></returns>
        Corpora ListCorpora(string customizationId);

        /// <summary>
        /// Lists information about a specified corpus. The information includes the total number of words and out-of-vocabulary (OOV) words, name, and status of the corpus. Only the owner of a custom model can use this method to list information about a corpus from the model.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model for which a corpus is be listed. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="corpusName">The name of the corpus about which information is to be listed.</param>
        /// <returns>a single instance of a Corpus object that provides information about the specified corpus. </returns>
        Corpus GetCorpus(string customizationId, string corpusName);


        /// <summary>
        /// Deletes an existing corpus from a custom language model. The service removes any out-of-vocabulary (OOV) words associated with the corpus from the custom model's words resource unless they were also added by another corpus or they have been modified in some way with the Add custom words or Add a custom word method. Removing a corpus does not affect the custom model until you train the model with the Train a custom model method. Only the owner of a custom model can use this method to delete a corpus from the model. 
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model from which a corpus is to be deleted. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="name">The name of the corpus that is to be deleted.</param>
        object DeleteCorpus(string customizationId, string name);

        /// <summary>
        /// Adds one or more custom words to a custom language model. The service populates the words resource for a custom model with out-of-vocabulary (OOV) words found in each corpus added to the model. You can use this method to add additional words or to modify existing words in the words resource. Only the owner of a custom model can use this method to add or modify custom words associated with the model. Adding or modifying custom words does not affect the custom model until you train the model for the new data by using the Train a custom model method.
        /// You add custom words by providing a comma-separated list of Words objects, one per word. You must use the Word object's word parameter to identify the word that is to be added. You can also provide one or both of the following optional parameters for each word: 
        /// If you add a custom word that already exists in the words resource for the custom model, the new definition overrides the existing data for the word. If the service encounters an error with the input data, it returns a failure code and does not add any of the words to the words resource.
        /// The call succeeds if the input data is valid. The service then asynchronously pre-processes the words to add them to the model's words resource. The time that it takes for the analysis to complete depends on the number of new words that you add but is generally faster than adding a corpus or training a model.
        /// You can monitor the status of the request by using the "ListCustomModel(string)"List a custom model</see> method to poll the model's status. Use a loop to check the status every 10 seconds. The method returns a Customization object that includes a status field. A status of ready means that the words have been added to the custom model. The service cannot accept requests to add new corpora or words or to train the model until the existing request completes.
        /// You can use the List custom words or List a custom word method to review the words that you add. Words with an invalid sounds_like field include an error field that describes the problem. If necessary, you can use the Add custom words or Add a custom word method to correct problems, eliminate typographical errors, and modify how words are pronounced.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model to which words are to be added. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="body">A comma-separated list of Words objects, each of which provides information about a custom word to be added.</param>
        object AddCustomWords(string customizationId, Words body);

        /// <summary>
        /// Adds a custom word to a custom language model. The service populates the words resource for a custom model with out-of-vocabulary (OOV) words found in each corpus added to the model. You can use this method to add additional words or to modify existing words in the words resource. Only the owner of a custom model can use this method to add or modify a custom word for the model. Adding or modifying a custom word does not affect the custom model until you train the model for the new data by using the Train a custom model method.
        /// Use the word_name path parameter to specify the custom word that is to be added or modified. Use the "WordDefinition" WordDefinition object</see> to provide one or both of the following optional parameters for the word:
        /// The display_as parameter provides a different way of spelling the word in a transcript. Use the parameter when you want the word to appear different from its usual representation or from its spelling in corpora training data. For example, you might indicate that the word IBM(trademark) is to be displayed as IBM™. For more information, see Using the display_as field.
        /// The sounds_like parameter provides an array of one or more pronunciations for the word. Use the parameter to specify how the word can be pronounced by users. Use the parameter for words that are difficult to pronounce, foreign words, acronyms, and so on. For example, you might specify that the word IEEE can sound like I. triple E.. You can specify a maximum of five sounds-like pronunciations for a word. For information about pronunciation rules, see Using the sounds_like field.
        /// If you add a custom word that already exists in the words resource for the custom model, the new definition overrides the existing data for the word. If the service encounters an error, it does not add the word to the words resource. Use the List a custom word method to review the word that you add.
        /// </summary>
        /// <param name="customizationId"></param>
        /// <param name="wordname"></param>
        /// <param name="body"></param>
        object AddCustomWord(string customizationId, string wordname, WordDefinition body);

        /// <summary>
        /// Lists information about custom words from a custom language model. You can list all words from the custom model's words resource, only custom words that were added or modified by the user, or only OOV words that were extracted from corpora. You can also indicate the order in which the service is to return words; by default, words are listed in ascending alphabetical order. Only the owner of a custom model can use this method to query the words from the model.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model from which words are to be queried. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="wordType">
        /// The type of words to be listed from the custom language model's words resource:
        /// For alphabetical ordering, the lexicographical precedence is numeric values, uppercase letters, and lowercase letters. For count ordering, values with the same count are not ordered. With cURL, URL encode the + symbol as %2B.
        /// </param>
        /// <param name="sort">How to sort the results.</param>
        /// <returns></returns>
        WordsList ListCustomWords(string customizationId, WordType? wordType, Sort? sort);

        /// <summary>
        /// Lists information about a custom word from a custom language model. Only the owner of a custom model can use this method to query a word from the model. 
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model from which a word is to be queried. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="wordname">The custom word that is to be queried from the custom model.</param>
        /// <returns>Returns a single instance of a WordData object that provides information about the specified word. </returns>
        WordData ListCustomWord(string customizationId, string wordname);

        /// <summary>
        /// Deletes a custom word from a custom language model. You can remove any word that you added to the custom model's words resource via any means. However, if the word also exists in the service's base vocabulary, the service removes only the custom pronunciation for the word; the word remains in the base vocabulary.
        /// Removing a custom word does not affect the custom model until you train the model with the Train a custom model method. Only the owner of a custom model can use this method to delete a word from the model. 
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language model from which a word is to be deleted. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="wordname">The custom word that is to be deleted from the custom model.</param>
        object DeleteCustomWord(string customizationId, string wordname);
    }
}