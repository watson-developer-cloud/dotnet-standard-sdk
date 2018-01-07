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

using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2
{
    public interface ILanguageTranslatorService
    {
        /// <summary>
        /// Translates the input text from the source language to the target language. 
        /// </summary>
        /// <param name="body">The translate request containing the text, with optional source, target, and model_id.</param>
        /// <returns><see cref="TranslationResult" />TranslationResult</returns>
        TranslationResult Translate(TranslateRequest body);
        /// <summary>
        /// Identifies the language of the input text. 
        /// </summary>
        /// <param name="text">Input text in UTF-8 format.</param>
        /// <returns><see cref="IdentifiedLanguages" />IdentifiedLanguages</returns>
        IdentifiedLanguages Identify(string text);

        /// <summary>
        /// Lists all languages that can be identified by the API. Lists all languages that the service can identify. Returns the two-letter code (for example, `en` for English or `es` for Spanish) and name of each language.
        /// </summary>
        /// <returns><see cref="IdentifiableLanguages" />IdentifiableLanguages</returns>
        IdentifiableLanguages ListIdentifiableLanguages();
        /// <summary>
        /// Uploads a TMX glossary file on top of a domain to customize a translation model. 
        /// </summary>
        /// <param name="baseModelId">Specifies the domain model that is used as the base for the training. To see current supported domain models, use the GET /v2/models parameter.</param>
        /// <param name="name">The model name. Valid characters are letters, numbers, -, and _. No spaces. (optional)</param>
        /// <param name="forcedGlossary">A TMX file with your customizations. The customizations in the file completely overwrite the domain data translation, including high frequency or high confidence phrase translations. You can upload only one glossary with a file size less than 10 MB per call. (optional)</param>
        /// <param name="parallelCorpus">A TMX file that contains entries that are treated as a parallel corpus instead of a glossary. (optional)</param>
        /// <param name="monolingualCorpus">A UTF-8 encoded plain text file that is used to customize the target language model. (optional)</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        TranslationModel CreateModel(string baseModelId, string name = null, System.IO.Stream forcedGlossary = null, System.IO.Stream parallelCorpus = null, System.IO.Stream monolingualCorpus = null);

        /// <summary>
        /// Deletes a custom translation model. 
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <returns><see cref="DeleteModelResult" />DeleteModelResult</returns>
        DeleteModelResult DeleteModel(string modelId);

        /// <summary>
        /// Get information about the given translation model, including training status. 
        /// </summary>
        /// <param name="modelId">Model ID to use.</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        TranslationModel GetModel(string modelId);

        /// <summary>
        /// Lists available standard and custom models by source or target language. 
        /// </summary>
        /// <param name="source">Filter models by source language. (optional)</param>
        /// <param name="target">Filter models by target language. (optional)</param>
        /// <param name="defaultModels">Valid values are leaving it unset, `true`, and `false`. When `true`, it filters models to return the defaultModels model or models. When `false`, it returns the non-defaultModels model or models. If not set, it returns all models, defaultModels and non-defaultModels. (optional)</param>
        /// <returns><see cref="TranslationModels" />TranslationModels</returns>
        TranslationModels ListModels(string source = null, string target = null, bool? defaultModels = null);
    }
}
