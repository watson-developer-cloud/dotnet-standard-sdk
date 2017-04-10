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
        /// Lists information about the specified voice. Specify a customization_id to obtain information for that custom voice model of the specified voice. 
        /// </summary>
        /// <param name="voiceName">The voice about which information is to be returned</param>
        /// <returns>Returns details the specified voice.</returns>
        Voice GetVoice(string voiceName);
        /// <summary>
        /// Returns the phonetic pronunciation for the specified word. You can request the pronunciation for a specific format. You can also request the pronunciation for a specific voice to see the default translation for the language of that voice or for a specific custom voice model to see the translation for that voice model.
        /// </summary>
        /// <param name="text">The word for which the pronunciation is requested.</param>
        /// <returns>Returns the pronunication of the specified text.</returns>
        Pronunciation GetPronunciation(string text);
        /// <summary>
        /// Returns the phonetic pronunciation for the specified word. You can request the pronunciation for a specific format. You can also request the pronunciation for a specific voice to see the default translation for the language of that voice or for a specific custom voice model to see the translation for that voice model.
        /// </summary>
        /// <param name="text">The word for which the pronunciation is requested.</param>
        /// <param name="voice">The voice in which the pronunciation for the specified word is to be returned</param>
        /// <param name="phoneme">The phoneme set in which the pronunciation is to be returned</param>
        /// <returns>Returns the pronunication of the specified text.</returns>
        Pronunciation GetPronunciation(string text, Voice voice, Phoneme phoneme);
        /// <summary>
        /// Synthesizes text to spoken audio, returning the synthesized audio stream as an array of bytes. 
        /// </summary>
        /// <param name="text">The text to be synthesized. Provide plain text or text that is annotated with SSML. Text size is limited by the maximum length of the HTTP request line and headers (about 6 KB) or by system limits, whichever is less</param>
        /// <param name="voice">The voice that is to be used for the synthesis</param>
        /// <param name="audioType">The voice that is to be used for the synthesis</param>
        /// <returns>Returns a stream of the specified text.</returns>
        Stream Synthesize(string text, Voice voice, AudioType audioType);
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
