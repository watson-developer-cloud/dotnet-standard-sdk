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
using IBM.WatsonDeveloperCloud.LanguageTranslator.v3.Model;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v3
{
    public partial interface ILanguageTranslatorService
    {
        /// <summary>
        /// Translate.
        ///
        /// Translates the input text from the source language to the target language.
        /// </summary>
        /// <param name="request">The translate request containing the text, and either a model ID or source and target
        /// language pair.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TranslationResult" />TranslationResult</returns>
        TranslationResult Translate(TranslateRequest request, Dictionary<string, object> customData = null);
        /// <summary>
        /// Identify language.
        ///
        /// Identifies the language of the input text.
        /// </summary>
        /// <param name="text">Input text in UTF-8 format.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="IdentifiedLanguages" />IdentifiedLanguages</returns>
        IdentifiedLanguages Identify(string text, Dictionary<string, object> customData = null);
        /// <summary>
        /// List identifiable languages.
        ///
        /// Lists the languages that the service can identify. Returns the language code (for example, `en` for English
        /// or `es` for Spanish) and name of each language.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="IdentifiableLanguages" />IdentifiableLanguages</returns>
        IdentifiableLanguages ListIdentifiableLanguages(Dictionary<string, object> customData = null);
        /// <summary>
        /// Create model.
        ///
        /// Uploads Translation Memory eXchange (TMX) files to customize a translation model.
        ///
        /// You can either customize a model with a forced glossary or with a corpus that contains parallel sentences.
        /// To create a model that is customized with a parallel corpus <b>and</b> a forced glossary, proceed in two
        /// steps: customize with a parallel corpus first and then customize the resulting model with a glossary.
        /// Depending on the type of customization and the size of the uploaded corpora, training can range from minutes
        /// for a glossary to several hours for a large parallel corpus. You can upload a single forced glossary file
        /// and this file must be less than <b>10 MB</b>. You can upload multiple parallel corpora tmx files. The
        /// cumulative file size of all uploaded files is limited to <b>250 MB</b>. To successfully train with a
        /// parallel corpus you must have at least <b>5,000 parallel sentences</b> in your corpus.
        ///
        /// You can have a <b>maxium of 10 custom models per language pair</b>.
        /// </summary>
        /// <param name="baseModelId">The model ID of the model to use as the base for customization. To see available
        /// models, use the `List models` method. Usually all IBM provided models are customizable. In addition, all
        /// your models that have been created via parallel corpus customization, can be further customized with a
        /// forced glossary.</param>
        /// <param name="name">An optional model name that you can use to identify the model. Valid characters are
        /// letters, numbers, dashes, underscores, spaces and apostrophes. The maximum length is 32 characters.
        /// (optional)</param>
        /// <param name="forcedGlossary">A TMX file with your customizations. The customizations in the file completely
        /// overwrite the domain translaton data, including high frequency or high confidence phrase translations. You
        /// can upload only one glossary with a file size less than 10 MB per call. A forced glossary should contain
        /// single words or short phrases. (optional)</param>
        /// <param name="parallelCorpus">A TMX file with parallel sentences for source and target language. You can
        /// upload multiple parallel_corpus files in one request. All uploaded parallel_corpus files combined, your
        /// parallel corpus must contain at least 5,000 parallel sentences to train successfully. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        TranslationModel CreateModel(string baseModelId, string name = null, System.IO.FileStream forcedGlossary = null, System.IO.FileStream parallelCorpus = null, Dictionary<string, object> customData = null);
        /// <summary>
        /// Delete model.
        ///
        /// Deletes a custom translation model.
        /// </summary>
        /// <param name="modelId">Model ID of the model to delete.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DeleteModelResult" />DeleteModelResult</returns>
        DeleteModelResult DeleteModel(string modelId, Dictionary<string, object> customData = null);
        /// <summary>
        /// Get model details.
        ///
        /// Gets information about a translation model, including training status for custom models. Use this API call
        /// to poll the status of your customization request. A successfully completed training will have a status of
        /// `available`.
        /// </summary>
        /// <param name="modelId">Model ID of the model to get.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        TranslationModel GetModel(string modelId, Dictionary<string, object> customData = null);
        /// <summary>
        /// List models.
        ///
        /// Lists available translation models.
        /// </summary>
        /// <param name="source">Specify a language code to filter results by source language. (optional)</param>
        /// <param name="target">Specify a language code to filter results by target language. (optional)</param>
        /// <param name="defaultModels">If the default parameter isn't specified, the service will return all models
        /// (default and non-default) for each language pair. To return only default models, set this to `true`. To
        /// return only non-default models, set this to `false`. There is exactly one default model per language pair,
        /// the IBM provided base model. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TranslationModels" />TranslationModels</returns>
        TranslationModels ListModels(string source = null, string target = null, bool? defaultModels = null, Dictionary<string, object> customData = null);
    }
}
