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
using System.Runtime.Serialization;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    public partial interface ITextToSpeechService
    {
        /// <summary>
        /// Get a voice. Gets information about the specified voice. The information includes the name, language, gender, and other details about the voice. Specify a customization ID to obtain information for that custom voice model of the specified voice. To list information about all available voices, use the **List voices** method.
        /// </summary>
        /// <param name="voice">The voice for which information is to be returned.</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom voice model for which information is to be returned. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to see information about the specified voice with no customization. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Voice" />Voice</returns>
        Voice GetVoice(string voice, string customizationId = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// List voices. Lists all voices available for use with the service. The information includes the name, language, gender, and other details about the voice. To see information about a specific voice, use the **Get a voice** method.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Voices" />Voices</returns>
        Voices ListVoices(Dictionary<string, object> customData = null);
        /// <summary>
        /// Synthesize audio. Synthesizes text to spoken audio, returning the synthesized audio stream as an array of bytes. You can pass a maximum of 5 KB of text.  Use the `Accept` header or the `accept` query parameter to specify the requested format (MIME type) of the response audio. By default, the service uses `audio/ogg;codecs=opus`. For detailed information about the supported audio formats and sampling rates, see [Specifying an audio format](https://console.bluemix.net/docs/services/text-to-speech/http.html#format). Specify a value of `application/json` for the `Content-Type` header.   If a request includes invalid query parameters, the service returns a `Warnings` response header that provides messages about the invalid parameters. The warning includes a descriptive message and a list of invalid argument strings. For example, a message such as `"Unknown arguments:"` or `"Unknown url query arguments:"` followed by a list of the form `"invalid_arg_1, invalid_arg_2."` The request succeeds despite the warnings.
        /// </summary>
        /// <param name="text">A `Text` object that provides the text to synthesize. Specify either plain text or a subset of SSML. Pass a maximum of 5 KB of text.</param>
        /// <param name="accept">The type of the response: audio/basic, audio/flac, audio/l16;rate=nnnn, audio/ogg, audio/ogg;codecs=opus, audio/ogg;codecs=vorbis, audio/mp3, audio/mpeg, audio/mulaw;rate=nnnn, audio/wav, audio/webm, audio/webm;codecs=opus, or audio/webm;codecs=vorbis. (optional)</param>
        /// <param name="voice">The voice to use for synthesis. (optional, default to en-US_MichaelVoice)</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom voice model to use for the synthesis. If a custom voice model is specified, it is guaranteed to work only if it matches the language of the indicated voice. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to use the specified voice with no customization. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="System.IO.FileStream" />System.IO.FileStream</returns>
        System.IO.MemoryStream Synthesize(Text text, string accept = null, string voice = null, string customizationId = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get pronunciation. Gets the phonetic pronunciation for the specified word. You can request the pronunciation for a specific format. You can also request the pronunciation for a specific voice to see the default translation for the language of that voice or for a specific custom voice model to see the translation for that voice model.  **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="text">The word for which the pronunciation is requested.</param>
        /// <param name="voice">A voice that specifies the language in which the pronunciation is to be returned. All voices for the same language (for example, `en-US`) return the same translation. (optional, default to en-US_MichaelVoice)</param>
        /// <param name="format">The phoneme format in which to return the pronunciation. Omit the parameter to obtain the pronunciation in the default format. (optional, default to ipa)</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom voice model for which the pronunciation is to be returned. The language of a specified custom model must match the language of the specified voice. If the word is not defined in the specified custom model, the service returns the default translation for the custom model's language. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to see the translation for the specified voice with no customization. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Pronunciation" />Pronunciation</returns>
        Pronunciation GetPronunciation(string text, string voice = null, string format = null, string customizationId = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Create a custom model. Creates a new empty custom voice model. You must specify a name for the new custom model. You can optionally specify the language and a description for the new model. Specify a value of `application/json` for the `Content-Type` header. The model is owned by the instance of the service whose credentials are used to create it.  **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="createVoiceModel">A `CreateVoiceModel` object that contains information about the new custom voice model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="VoiceModel" />VoiceModel</returns>
        VoiceModel CreateVoiceModel(CreateVoiceModel createVoiceModel, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete a custom model. Deletes the specified custom voice model. You must use credentials for the instance of the service that owns a model to delete it.  **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteVoiceModel(string customizationId, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get a custom model. Gets all information about a specified custom voice model. In addition to metadata such as the name and description of the voice model, the output includes the words and their translations as defined in the model. To see just the metadata for a voice model, use the **List custom models** method.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="VoiceModel" />VoiceModel</returns>
        VoiceModel GetVoiceModel(string customizationId, Dictionary<string, object> customData = null);
        /// <summary>
        /// List custom models. Lists metadata such as the name and description for all custom voice models that are owned by an instance of the service. Specify a language to list the voice models for that language only. To see the words in addition to the metadata for a specific voice model, use the **List a custom model** method. You must use credentials for the instance of the service that owns a model to list information about it.  **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="language">The language for which custom voice models that are owned by the requesting service credentials are to be returned. Omit the parameter to see all custom voice models that are owned by the requester. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="VoiceModels" />VoiceModels</returns>
        VoiceModels ListVoiceModels(string language = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Update a custom model. Updates information for the specified custom voice model. You can update metadata such as the name and description of the voice model. You can also update the words in the model and their translations. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. Specify a value of `application/json` for the `Content-Type` header. You must use credentials for the instance of the service that owns a model to update it.  **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="updateVoiceModel">An `UpdateVoiceModel` object that contains information that is to be updated for the custom voice model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel UpdateVoiceModel(string customizationId, UpdateVoiceModel updateVoiceModel, Dictionary<string, object> customData = null);
        /// <summary>
        /// Add a custom word. Adds a single word and its translation to the specified custom voice model. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. Specify a value of `application/json` for the `Content-Type` header. You must use credentials for the instance of the service that owns a model to add a word to it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be added or updated for the custom voice model.</param>
        /// <param name="translation">The translation for the word that is to be added or updated.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel AddWord(string customizationId, string word, Translation translation, Dictionary<string, object> customData = null);
        /// <summary>
        /// Add custom words. Adds one or more words and their translations to the specified custom voice model. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. Specify a value of `application/json` for the `Content-Type` header. You must use credentials for the instance of the service that owns a model to add words to it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customWords">A `Words` object that provides one or more words that are to be added or updated for the custom voice model and the translation for each specified word.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel AddWords(string customizationId, Words customWords, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete a custom word. Deletes a single word from the specified custom voice model. You must use credentials for the instance of the service that owns a model to delete its words.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be deleted from the custom voice model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteWord(string customizationId, string word, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get a custom word. Gets the translation for a single word from the specified custom model. The output shows the translation as it is defined in the model. You must use credentials for the instance of the service that owns a model to list its words.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be queried from the custom voice model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Translation" />Translation</returns>
        Translation GetWord(string customizationId, string word, Dictionary<string, object> customData = null);
        /// <summary>
        /// List custom words. Lists all of the words and their translations for the specified custom voice model. The output shows the translations as they are defined in the model. You must use credentials for the instance of the service that owns a model to list its words.  **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Words" />Words</returns>
        Words ListWords(string customizationId, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete labeled data. Deletes all data that is associated with a specified customer ID. The method deletes all data for the customer ID, regardless of the method by which the information was added. The method has no effect if no data is associated with the customer ID. You must issue the request with credentials for the same instance of the service that was used to associate the customer ID with the data.   You associate a customer ID with data by passing the `X-Watson-Metadata` header with a request that passes the data. For more information about customer IDs and about using this method, see [Information security](https://console.bluemix.net/docs/services/text-to-speech/information-security.html).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null);
    }
}
