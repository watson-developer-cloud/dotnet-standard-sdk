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

using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using System.Collections.Generic;
using System.IO;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    interface ITextToSpeechService
    {
        /// <summary>
        /// Retrieves a list of all voices available for use with the service. The information includes the voice's name, language, and gender, among other things.
        /// </summary>
        /// <returns>Returns a list of available voices.</returns>
        VoiceSet GetVoices();

        /// <summary>
        /// Lists information about the specified voice. The information includes the name, language, gender, and other details about the voice. Specify a customization ID to obtain information for that custom voice model of the specified voice. To see information about all available voices, use the <see cref="GetVoices">Get voices</see> method.
        /// </summary>
        /// <param name="voiceName">The voice about which information is to be returned</param>
        /// <param name="customizationId">The GUID of a custom voice model about which information is to be returned. You must make the request with the service credentials of the model's owner. Omit the parameter to see information about the voice with no customization.</param>
        /// <returns>Returns details the specified voice.</returns>
        VoiceCustomization GetVoice(string voiceName, string customizationId = "");

        /// <summary>
        /// Synthesizes text to spoken audio, returning the synthesized audio stream as an array of bytes. You can use two request methods to synthesize audio:
        /// <list type="bullet">
        ///     <item>The HTTP GET request method passes shorter text via a query parameter. The text size is limited by the maximum length of the HTTP request line and headers (about 6 KB) or by system limits, whichever is less.</item>
        ///     <item>The HTTP POST request method passes longer text in the body of the request. Text size is limited to 5 KB.</item>
        /// </list>
        /// With either request method, you can provide plain text or text that is annotated with SSML.
        /// </summary>
        /// <param name="text">For an HTTP GET request, the text to be synthesized. Provide plain text or text that is annotated with SSML. Text size is limited by the maximum length of the HTTP request line and headers (about 6 KB) or by system limits, whichever is less. Not supported for POST requests.</param>
        /// <param name="accept">
        /// The requested MIME type of the audio:
        /// <list type="bullet">
        ///     <item>audio/ogg (the service uses the Vorbis codec)</item>
        ///     <item>audio/ogg;codecs=opus (the default)</item>
        ///     <item>audio/ogg;codecs=vorbis</item>
        ///     <item>audio/wav</item>
        ///     <item>audio/flac</item>
        ///     <item>audio/webm (the service uses the Opus codec)</item>
        ///     <item>audio/webm;codecs=opus</item>
        ///     <item>audio/webm;codecs=vorbis</item>
        ///     <item>audio/l16;rate=rate</item>
        ///     <item>audio/mulaw;rate=rate</item>
        ///     <item>audio/basic</item>
        /// </list>
        /// You can use this header or the accept query parameter to specify the MIME type. For additional information about the supported audio formats and sampling rates, see Specifying an audio format. The information includes links to a number of Internet sites that provide technical and usage details about the different formats.
        /// </param>
        /// <param name="voice">
        /// The voice that is to be used for the synthesis:
        /// <list type="bullet">
        ///     <item>de-DE_BirgitVoice</item>
        ///     <item>de-DE_DieterVoice</item>
        ///     <item>en-GB_KateVoice</item>
        ///     <item>en-US_AllisonVoice</item>
        ///     <item>en-US_LisaVoice</item>
        ///     <item>en-US_MichaelVoice (the default)</item>
        ///     <item>es-ES_EnriqueVoice</item>
        ///     <item>es-ES_LauraVoice</item>
        ///     <item>es-LA_SofiaVoice</item>
        ///     <item>es-US_SofiaVoice</item>
        ///     <item>fr-FR_ReneeVoice</item>
        ///     <item>it-IT_FrancescaVoice</item>
        ///     <item>ja-JP_EmiVoice</item>
        ///     <item>pt-BR_IsabelaVoice</item>
        /// </list>
        /// </param>
        /// <param name="customizationId">The GUID of a custom voice model that is to be used for the synthesis. If you specify a custom voice model, it is guaranteed to work only if it matches the language of the indicated voice. You must make the request with the service credentials of the model's owner. Omit the parameter to use the specified voice with no customization.</param>
        /// <returns></returns>
        byte[] Synthesize(string text, string accept = "audio/ogg;codecs=opus", string voice = "en-US_MichaelVoice", string customizationId = "");

        /// <summary>
        /// Synthesizes text to spoken audio, returning the synthesized audio stream as an array of bytes. You can use two request methods to synthesize audio:
        /// <list type="bullet">
        ///     <item>The HTTP GET request method passes shorter text via a query parameter. The text size is limited by the maximum length of the HTTP request line and headers (about 6 KB) or by system limits, whichever is less.</item>
        ///     <item>The HTTP POST request method passes longer text in the body of the request. Text size is limited to 5 KB.</item>
        /// </list>
        /// With either request method, you can provide plain text or text that is annotated with SSML.
        /// </summary>
        /// <param name="text">For an HTTP POST request, a <see cref="Text">Text</see> object that provides the text to be synthesized. Provide plain text or text that is annotated with SSML. Text size is limited to 5 KB. Not supported for GET requests.</param>
        /// <param name="accept">
        /// The requested MIME type of the audio:
        /// <list type="bullet">
        ///     <item>audio/ogg (the service uses the Vorbis codec)</item>
        ///     <item>audio/ogg;codecs=opus (the default)</item>
        ///     <item>audio/ogg;codecs=vorbis</item>
        ///     <item>audio/wav</item>
        ///     <item>audio/flac</item>
        ///     <item>audio/webm (the service uses the Opus codec)</item>
        ///     <item>audio/webm;codecs=opus</item>
        ///     <item>audio/webm;codecs=vorbis</item>
        ///     <item>audio/l16;rate=rate</item>
        ///     <item>audio/mulaw;rate=rate</item>
        ///     <item>audio/basic</item>
        /// </list>
        /// You can use this header or the accept query parameter to specify the MIME type. For additional information about the supported audio formats and sampling rates, see Specifying an audio format. The information includes links to a number of Internet sites that provide technical and usage details about the different formats.
        /// </param>
        /// <param name="voice">
        /// The voice that is to be used for the synthesis:
        /// <list type="bullet">
        ///     <item>de-DE_BirgitVoice</item>
        ///     <item>de-DE_DieterVoice</item>
        ///     <item>en-GB_KateVoice</item>
        ///     <item>en-US_AllisonVoice</item>
        ///     <item>en-US_LisaVoice</item>
        ///     <item>en-US_MichaelVoice (the default)</item>
        ///     <item>es-ES_EnriqueVoice</item>
        ///     <item>es-ES_LauraVoice</item>
        ///     <item>es-LA_SofiaVoice</item>
        ///     <item>es-US_SofiaVoice</item>
        ///     <item>fr-FR_ReneeVoice</item>
        ///     <item>it-IT_FrancescaVoice</item>
        ///     <item>ja-JP_EmiVoice</item>
        ///     <item>pt-BR_IsabelaVoice</item>
        /// </list>
        /// </param>
        /// <param name="customizationId">The GUID of a custom voice model that is to be used for the synthesis. If you specify a custom voice model, it is guaranteed to work only if it matches the language of the indicated voice. You must make the request with the service credentials of the model's owner. Omit the parameter to use the specified voice with no customization.</param>
        /// <returns></returns>
        byte[] Synthesize(Text text, string accept = "audio/ogg;codecs=opus", string voice = "en-US_MichaelVoice", string customizationId = "");

        /// <summary>
        /// Returns the phonetic pronunciation for the specified word. You can request the pronunciation for a specific format. You can also request the pronunciation for a specific voice to see the default translation for the language of that voice or for a specific custom voice model to see the translation for that voice model.
        /// </summary>
        /// <param name="text">The word for which the pronunciation is requested.</param>
        /// <param name="voice">
        /// The voice that is to be used for the synthesis:
        /// <list type="bullet">
        ///     <item>de-DE_BirgitVoice</item>
        ///     <item>de-DE_DieterVoice</item>
        ///     <item>en-GB_KateVoice</item>
        ///     <item>en-US_AllisonVoice</item>
        ///     <item>en-US_LisaVoice</item>
        ///     <item>en-US_MichaelVoice (the default)</item>
        ///     <item>es-ES_EnriqueVoice</item>
        ///     <item>es-ES_LauraVoice</item>
        ///     <item>es-LA_SofiaVoice</item>
        ///     <item>es-US_SofiaVoice</item>
        ///     <item>fr-FR_ReneeVoice</item>
        ///     <item>it-IT_FrancescaVoice</item>
        ///     <item>ja-JP_EmiVoice</item>
        ///     <item>pt-BR_IsabelaVoice</item>
        /// </list>
        /// The translation is returned in the language of the specified voice; all voices for the same language (for example, en-US) return the same translation. Do not specify both a voice and a customization_id.
        /// </param>
        /// <param name="format">
        /// The phoneme set in which the pronunciation is to be returned:
        /// <list type="bullet">
        ///     <item>ipa (the default)</item>
        ///     <item>ibm</item>
        /// </list>
        /// </param>
        /// <param name="customizationId"></param>
        /// <returns>The pronunciation of the specified text in the requested voice and format and, if specified, for the requested custom voice model.</returns>
        Pronunciation GetPronunciation(string text, string voice = "en-US_MichaelVoice", string format = "ipa", string customizationId = "");

        /// <summary>
        /// Lists metadata such as the name and description for all custom voice models that are owned by the requesting user. Specify a language to list the voice models for that language only. To see the words in addition to the metadata for a specific voice model, use the List a voice model method. Only the owner of a custom voice model can use this method to list information about a model.
        /// </summary>
        /// <param name="language">Lists metadata such as the name and description for all custom voice models that are owned by the requesting user. Specify a language to list the voice models for that language only. To see the words in addition to the metadata for a specific voice model, use the List a voice model method. Only the owner of a custom voice model can use this method to list information about a model.</param>
        /// <returns>Returns a list of custom voice models.</returns>
        List<CustomVoiceModel> GetCustomVoiceModels(string language);
        /// <summary>
        /// Lists all information about a specified custom voice model. In addition to metadata such as the name and description of the voice model, the output includes the words in the model and their translations as defined in the model. To see just the metadata for a voice model, use the List voice models method. Only the owner of a custom voice model can use this method to query information about the model.
        /// </summary>
        /// <param name="modelId">The GUID of the custom voice model about which information is to be returned. You must make the request with the service credentials of the model's owner.</param>
        /// <returns>Returns details of the specified custom voice model.</returns>
        CustomVoiceModel GetCustomVoiceModel(string modelId);
        /// <summary>
        /// Save a custom voice model that is owned by the requesting user.
        /// </summary>
        /// <param name="model">The custom model identifier. If this is blank, a new custom voice model is created.</param>
        /// <returns>Returns the details of the spcified custom voice model.</returns>
        CustomVoiceModel SaveCustomVoiceModel(CustomVoiceModel model);
        /// <summary>
        /// Deletes the specified custom voice model. Only the owner of a custom voice model can use this method to delete the model.
        /// </summary>
        /// <param name="model">The custom voice model object including the model identifier.</param>
        void DeleteCustomVoiceModel(CustomVoiceModel model);
        /// <summary>
        /// Deletes the specified custom voice model. Only the owner of a custom voice model can use this method to delete the model.
        /// </summary>
        /// <param name="modelID">The model identifier to be deleted.</param>
        void DeleteCustomVoiceModel(string modelID);
        /// <summary>
        /// Lists all of the words and their translations for the specified custom voice model. The output shows the translations as they are defined in the model. Only the owner of a custom voice model can use this method to query information about the model's words.
        /// </summary>
        /// <param name="model">The custom voice model object whose words are to be returned including the custom voice model identifier. You must make the request with the service credentials of the model's owner.</param>
        /// <returns>Returns a list of custom words.</returns>
        List<CustomWordTranslation> GetWords(CustomVoiceModel model);
        /// <summary>
        /// Lists all of the words and their translations for the specified custom voice model. The output shows the translations as they are defined in the model. Only the owner of a custom voice model can use this method to query information about the model's words.
        /// </summary>
        /// <param name="modelID">The GUID of the custom voice model whose words are to be returned. You must make the request with the service credentials of the model's owner.</param>
        /// <returns>Returns a list of custom words.</returns>
        List<CustomWordTranslation> GetWords(string modelID);
        /// <summary>
        /// Lists all of the words and their translations for the specified custom voice model. The output shows the translations as they are defined in the model. Only the owner of a custom voice model can use this method to query information about the model's words.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="translations"></param>
        void SaveWords(CustomVoiceModel model, params CustomWordTranslation[] translations);
        /// <summary>
        /// Adds one or more words and their translations to the specified custom voice model. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. Only the owner of a custom voice model can use this method to add words to the model.
        /// </summary>
        /// <param name="modelID">The GUID of the custom voice model that is to be updated. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="translations">An array of WordAdd objects that provides information about the words to be added to the custom voice model.</param>
        void SaveWords(string modelID, params CustomWordTranslation[] translations);
        /// <summary>
        /// Deletes a single word from the specified custom voice model. Only the owner of a custom voice model can use this method to delete a word from the model.
        /// </summary>
        /// <param name="model">The GUID of the custom voice model from which the word is to be deleted. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="translation">The CustomWordTranslation object that is to be deleted from the custom voice model.</param>
        void DeleteWord(CustomVoiceModel model, CustomWordTranslation translation);
        /// <summary>
        /// Deletes a single word from the specified custom voice model. Only the owner of a custom voice model can use this method to delete a word from the model.
        /// </summary>
        /// <param name="modelID">The GUID of the custom voice model from which the word is to be deleted. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="translation">The CustomWordTranslation object that is to be deleted from the custom voice model.</param>
        void DeleteWord(string modelID, CustomWordTranslation translation);
        /// <summary>
        /// Deletes a single word from the specified custom voice model. Only the owner of a custom voice model can use this method to delete a word from the model.
        /// </summary>
        /// <param name="model">The ustom voice model object from which the word is to be deleted. You must make the request with the service credentials of the model's owner.</param>
        /// <param name="word">The word that is to be deleted from the custom voice model.</param>
        void DeleteWord(CustomVoiceModel model, string word);
        /// <summary>
        /// Deletes a single word from the specified custom voice model. Only the owner of a custom voice model can use this method to delete a word from the model.
        /// </summary>
        /// <param name="modelID">The GUID of the custom voice model from which the word is to be deleted. You must make the request with the service credentials of the model's owner.></param>
        /// <param name="word">The word that is to be deleted from the custom voice model.</param>
        void DeleteWord(string modelID, string word);
    }
}
