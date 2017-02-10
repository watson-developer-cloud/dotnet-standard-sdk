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

using System.Collections.Generic;
using System.IO;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2
{
    public interface ILanguageTranslatorService
    {
        /// <summary>
        /// Lists available models for the Language translator service with option to filter by source or by target language.
        /// </summary>
        /// <param name="isDefault">Valid values are leaving it unset, 'true' and 'false'. When 'true', it filters models to return the default model or models. When 'false' it returns the non-default model or models. If not set, all models (default and non-default) return.</param>
        /// <param name="_source">Define with target. Filters models by source language.</param>
        /// <param name="_target">Define with source. Filters models by target language.</param>
        /// <returns></returns>
        TranslationModels ListModels(bool _isDefault, string _source, string _target);

        /// <summary>
        /// Uploads a TMX glossary file on top of a domain to customize a translation model.
        /// Depending on the size of the file, training can range from minutes for a glossary to several hours for a large parallel corpus. Glossary files must be less than 10 MB. The cumulative file size of all uploaded glossary and corpus files is limited to 250 MB.
        /// </summary>
        /// <param name="baseModelId">The base model to use to create the custom model.</param>
        /// <param name="name">The name of the custom model.</param>
        /// <param name="file">The file data used to create the custom model.</param>
        /// <returns></returns>
        CustomModels CreateModel(CreateModelOptions _options);

        /// <summary>
        /// Deletes trained translation models.
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <returns></returns>
        DeleteModels DeleteModel(string _modelId);

        /// <summary>
        /// Returns information, including training status, about a specified translation model.
        /// </summary>
        /// <param name="_modelId">The model identifier.</param>
        /// <returns></returns>
        ModelPayload GetModelDetails(string _modelId);

        /// <summary>
        /// Translates input text from the source language to the target language.
        /// </summary>
        /// <param name="_modelId">The unique model_id of the translation model used to translate text. The model_id inherently specifies source, target language, and domain. If the model_id is specified, there is no need for the source and target parameters, and the values will be ignored.</param>
        /// <param name="_text">Input text in UTF-8 encoding. Multiple text query parameters indicate multiple input paragraphs, and a single string is valid input.</param>
        /// <returns></returns>
        TranslateResponse Translate(string _modelId, string _text);

        /// <summary>
        /// Translates input text from the source language to the target language.
        /// </summary>
        /// <param name="_source">Used in combination with target as an alternative way to select the model for translation. When target and source are set, and model_id is not set, the system choose a default model with the right language pair to translate (usually the model based on the news domain).</param>
        /// <param name="_target">Translation target language in 2 or 5 letter language code. Should use 2 letter codes except for when clarifying between multiple supported languages. When model_id is used directly, it will override the source-target language combination. Also, when a 2 letter language code is used, and no suitable default is found (such as “zh”), it returns an error.</param>
        /// <param name="_text">Input text in UTF-8 encoding. Multiple text query parameters indicate multiple input paragraphs, and a single string is valid input.</param>
        /// <returns></returns>
        TranslateResponse Translate(string _source, string _target, string _text);

        /// <summary>
        /// Translates input text from the source language to the target language.
        /// </summary>
        /// <param name="_modelId">The unique model_id of the translation model used to translate text. The model_id inherently specifies source, target language, and domain. If the model_id is specified, there is no need for the source and target parameters, and the values will be ignored.</param>
        /// <param name="_text">Input text in UTF-8 encoding. Multiple text query parameters indicate multiple input paragraphs, and a single string is valid input.</param>
        /// <returns></returns>
        TranslateResponse Translate(string _modelId, List<string> _text);

        /// <summary>
        /// Translates input text from the source language to the target language.
        /// </summary>
        /// <param name="_source">Used in combination with target as an alternative way to select the model for translation. When target and source are set, and model_id is not set, the system choose a default model with the right language pair to translate (usually the model based on the news domain).</param>
        /// <param name="_target">Translation target language in 2 or 5 letter language code. Should use 2 letter codes except for when clarifying between multiple supported languages. When model_id is used directly, it will override the source-target language combination. Also, when a 2 letter language code is used, and no suitable default is found (such as “zh”), it returns an error.</param>
        /// <param name="_text">Input text in UTF-8 encoding. Multiple text query parameters indicate multiple input paragraphs, and a single string is valid input.</param>
        /// <returns></returns>
        TranslateResponse Translate(string _source, string _target, List<string> _text);

        /// <summary>
        /// Return the list of languages it can detect.
        /// </summary>
        /// <returns></returns>
        IdentifiableLanguages GetIdentifiableLanguages();

        /// <summary>
        /// Identify the language in which a text is written.
        /// </summary>
        /// <param name="_text">Input text in UTF-8 format</param>
        /// <returns></returns>
        IdentifiedLanguages Identify(string _text);
    }
}