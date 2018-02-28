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

using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    public interface ITextToSpeechService
    {
        /// <summary>
        /// Retrieves a specific voice available for speech synthesis. Lists information about the voice specified with the `voice` path parameter. Specify the `customization_id` query parameter to obtain information for that custom voice model of the specified voice. Use the `GET /v1/voices` method to see a list of all available voices.
        /// </summary>
        /// <param name="voice">The voice for which information is to be returned. Retrieve available voices with the `GET /v1/voices` method.</param>
        /// <param name="customizationId">The GUID of a custom voice model for which information is to be returned. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to see information about the specified voice with no customization. (optional)</param>
        /// <returns><see cref="Voice" />Voice</returns>
        Voice GetVoice(string voice, string customizationId = null);

        /// <summary>
        /// Retrieves all voices available for speech synthesis. Lists information about all available voices. To see information about a specific voice, use the `GET /v1/voices/{voice}` method.
        /// </summary>
        /// <returns><see cref="Voices" />Voices</returns>
        Voices ListVoices();
        /// <summary>
        /// Streaming speech synthesis of the text in the body parameter. Synthesizes text to spoken audio, returning the synthesized audio stream as an array of bytes. Identical to the `GET` method but passes longer text in the body of the request, not with the URL. Text size is limited to 5 KB. (For the `audio/l16` format, you can optionally specify `endianness=big-endian` or `endianness=little-endian`; the default is little endian.)   If a request includes invalid query parameters, the service returns a `Warnings` response header that provides messages about the invalid parameters. The warning includes a descriptive message and a list of invalid argument strings. For example, a message such as `"Unknown arguments:"` or `"Unknown url query arguments:"` followed by a list of the form `"invalid_arg_1, invalid_arg_2."` The request succeeds despite the warnings.   **Note about the Try It Out feature:** The `Try it out!` button is **not** supported for use with the the `POST /v1/synthesize` method. For examples of calls to the method, see the [Text to Speech API reference](http://www.ibm.com/watson/developercloud/text-to-speech/api/v1/).
        /// </summary>
        /// <param name="text">A `Text` object that provides the text to synthesize. Specify either plain text or a subset of SSML. Text size is limited to 5 KB.</param>
        /// <param name="voice">The voice to use for synthesis. Retrieve available voices with the `GET /v1/voices` method. (optional, default to en-US_MichaelVoice)</param>
        /// <param name="customizationId">The GUID of a custom voice model to use for the synthesis. If a custom voice model is specified, it is guaranteed to work only if it matches the language of the indicated voice. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to use the specified voice with no customization. (optional)</param>
        /// <returns><see cref="System.IO.Stream" />System.IO.Stream</returns>
        System.IO.Stream Synthesize(Text text, string voice = null, string customizationId = null);
        /// <summary>
        /// Gets the pronunciation for a word. Returns the phonetic pronunciation for the word specified by the `text` parameter. You can request the pronunciation for a specific format. You can also request the pronunciation for a specific voice to see the default translation for the language of that voice or for a specific custom voice model to see the translation for that voice model.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="text">The word for which the pronunciation is requested.</param>
        /// <param name="voice">A voice that specifies the language in which the pronunciation is to be returned. All voices for the same language (for example, `en-US`) return the same translation. Retrieve available voices with the `GET /v1/voices` method. (optional, default to en-US_MichaelVoice)</param>
        /// <param name="format">The phoneme format in which to return the pronunciation. Omit the parameter to obtain the pronunciation in the default format. (optional, default to ipa)</param>
        /// <param name="customizationId">The GUID of a custom voice model for which the pronunciation is to be returned. The language of a specified custom model must match the language of the specified voice. If the word is not defined in the specified custom model, the service returns the default translation for the custom model's language. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to see the translation for the specified voice with no customization. (optional)</param>
        /// <returns><see cref="Pronunciation" />Pronunciation</returns>
        Pronunciation GetPronunciation(string text, string voice = null, string format = null, string customizationId = null);
        /// <summary>
        /// Creates a new custom voice model. Creates a new empty custom voice model. The model is owned by the instance of the service whose credentials are used to create it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="createVoiceModel">A `CreateVoiceModel` object that contains information about the new custom voice model.</param>
        /// <returns><see cref="VoiceModel" />VoiceModel</returns>
        VoiceModel CreateVoiceModel(CreateVoiceModel createVoiceModel);

        /// <summary>
        /// Deletes a custom voice model. Deletes the custom voice model with the specified `customization_id`. You must use credentials for the instance of the service that owns a model to delete it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be deleted. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteVoiceModel(string customizationId);

        /// <summary>
        /// Queries the contents of a custom voice model. Lists all information about the custom voice model with the specified `customization_id`. In addition to metadata such as the name and description of the voice model, the output includes the words in the model and their translations as defined in the model. To see just the metadata for a voice model, use the `GET /v1/customizations` method. You must use credentials for the instance of the service that owns a model to list information about it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be queried. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="VoiceModel" />VoiceModel</returns>
        VoiceModel GetVoiceModel(string customizationId);

        /// <summary>
        /// Lists all available custom voice models for a language or for all languages. Lists metadata such as the name and description for the custom voice models that you own. Use the `language` query parameter to list the voice models that you own for the specified language only. Omit the parameter to see all voice models that you own for all languages. To see the words in addition to the metadata for a specific voice model, use the `GET /v1/customizations/{customization_id}` method. You must use credentials for the instance of the service that owns a model to list information about it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="language">The language for which custom voice models that are owned by the requesting service credentials are to be returned. Omit the parameter to see all custom voice models that are owned by the requester. (optional)</param>
        /// <returns><see cref="VoiceModels" />VoiceModels</returns>
        VoiceModels ListVoiceModels(string language = null);

        /// <summary>
        /// Updates information and words for a custom voice model. Updates information for the custom voice model with the specified `customization_id`. You can update the metadata such as the name and description of the voice model. You can also update the words in the model and their translations. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. You must use credentials for the instance of the service that owns a model to update it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be updated. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="updateVoiceModel">An `UpdateVoiceModel` object that contains information that is to be updated for the custom voice model.</param>
        /// <returns><see cref="object" />object</returns>
        object UpdateVoiceModel(string customizationId, UpdateVoiceModel updateVoiceModel);
        /// <summary>
        /// Adds a word to a custom voice model. Adds a single word and its translation to the custom voice model with the specified `customization_id`. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. You must use credentials for the instance of the service that owns a model to add a word to it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be updated. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be added or updated for the custom voice model.</param>
        /// <param name="translation">The translation for the word that is to be added or updated.</param>
        /// <returns><see cref="object" />object</returns>
        object AddWord(string customizationId, string word, Translation translation);

        /// <summary>
        /// Adds one or more words to a custom voice model. Adds one or more words and their translations to the custom voice model with the specified `customization_id`. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. You must use credentials for the instance of the service that owns a model to add words to it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be updated. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customWords">A `Words` object that contains the word or words that are to be added or updated for the custom voice model and the translation for each specified word.</param>
        /// <returns><see cref="object" />object</returns>
        object AddWords(string customizationId, Words customWords);

        /// <summary>
        /// Deletes a word from a custom voice model. Deletes a single word from the custom voice model with the specified `customization_id`. You must use credentials for the instance of the service that owns a model to delete it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model from which to delete a word. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be deleted from the custom voice model.</param>
        /// <returns><see cref="object" />object</returns>
        object DeleteWord(string customizationId, string word);

        /// <summary>
        /// Queries details about a word in a custom voice model. Returns the translation for a single word from the custom model with the specified `customization_id`. The output shows the translation as it is defined in the model. You must use credentials for the instance of the service that owns a model to query information about its words.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model from which to query a word. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be queried from the custom voice model.</param>
        /// <returns><see cref="Translation" />Translation</returns>
        Translation GetWord(string customizationId, string word);

        /// <summary>
        /// Queries details about the words in a custom voice model. Lists all of the words and their translations for the custom voice model with the specified `customization_id`. The output shows the translations as they are defined in the model. You must use credentials for the instance of the service that owns a model to query information about its words.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be queried. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="Words" />Words</returns>
        Words ListWords(string customizationId);
    }
}
