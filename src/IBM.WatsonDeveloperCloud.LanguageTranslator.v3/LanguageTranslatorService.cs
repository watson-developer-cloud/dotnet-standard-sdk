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
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v3.Model;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.Util;
using System;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v3
{
    public partial class LanguageTranslatorService : WatsonService, ILanguageTranslatorService
    {
        const string SERVICE_NAME = "language_translator";
        const string URL = "https://gateway.watsonplatform.net/language-translator/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public LanguageTranslatorService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public LanguageTranslatorService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public LanguageTranslatorService(TokenOptions options, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;

            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                this.Endpoint = options.ServiceUrl;
            }
            else
            {
                options.ServiceUrl = this.Endpoint;
            }

            _tokenManager = new TokenManager(options);
        }

        public LanguageTranslatorService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Translate.
        ///
        /// Translates the input text from the source language to the target language.
        /// </summary>
        /// <param name="request">The translate request containing the text, and either a model ID or source and target
        /// language pair.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="TranslationResult" />TranslationResult</returns>
        public TranslationResult Translate(TranslateRequest request, Dictionary<string, object> customData = null)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            TranslationResult result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/translate");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBody<TranslateRequest>(request);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TranslationResult>().Result;
                if(result == null)
                    result = new TranslationResult();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Identify language.
        ///
        /// Identifies the language of the input text.
        /// </summary>
        /// <param name="text">Input text in UTF-8 format.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="IdentifiedLanguages" />IdentifiedLanguages</returns>
        public IdentifiedLanguages Identify(string text, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            IdentifiedLanguages result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/identify");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithBodyContent(new StringContent(text, Encoding.UTF8, HttpMediaType.TEXT_PLAIN));
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<IdentifiedLanguages>().Result;
                if(result == null)
                    result = new IdentifiedLanguages();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List identifiable languages.
        ///
        /// Lists the languages that the service can identify. Returns the language code (for example, `en` for English
        /// or `es` for Spanish) and name of each language.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="IdentifiableLanguages" />IdentifiableLanguages</returns>
        public IdentifiableLanguages ListIdentifiableLanguages(Dictionary<string, object> customData = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            IdentifiableLanguages result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v3/identifiable_languages");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<IdentifiableLanguages>().Result;
                if(result == null)
                    result = new IdentifiableLanguages();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
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
        public TranslationModel CreateModel(string baseModelId, string name = null, System.IO.FileStream forcedGlossary = null, System.IO.FileStream parallelCorpus = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(baseModelId))
                throw new ArgumentNullException(nameof(baseModelId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            TranslationModel result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (forcedGlossary != null)
                {
                    var forcedGlossaryContent = new ByteArrayContent((forcedGlossary as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    forcedGlossaryContent.Headers.ContentType = contentType;
                    formData.Add(forcedGlossaryContent, "forced_glossary", forcedGlossary.Name);
                }

                if (parallelCorpus != null)
                {
                    var parallelCorpusContent = new ByteArrayContent((parallelCorpus as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    parallelCorpusContent.Headers.ContentType = contentType;
                    formData.Add(parallelCorpusContent, "parallel_corpus", parallelCorpus.Name);
                }

                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v3/models");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(baseModelId))
                    restRequest.WithArgument("base_model_id", baseModelId);
                if (!string.IsNullOrEmpty(name))
                    restRequest.WithArgument("name", name);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TranslationModel>().Result;
                if(result == null)
                    result = new TranslationModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete model.
        ///
        /// Deletes a custom translation model.
        /// </summary>
        /// <param name="modelId">Model ID of the model to delete.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="DeleteModelResult" />DeleteModelResult</returns>
        public DeleteModelResult DeleteModel(string modelId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            DeleteModelResult result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v3/models/{modelId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<DeleteModelResult>().Result;
                if(result == null)
                    result = new DeleteModelResult();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

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
        public TranslationModel GetModel(string modelId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            TranslationModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v3/models/{modelId}");

                restRequest.WithArgument("version", VersionDate);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TranslationModel>().Result;
                if(result == null)
                    result = new TranslationModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

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
        public TranslationModels ListModels(string source = null, string target = null, bool? defaultModels = null, Dictionary<string, object> customData = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            TranslationModels result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v3/models");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(source))
                    restRequest.WithArgument("source", source);
                if (!string.IsNullOrEmpty(target))
                    restRequest.WithArgument("target", target);
                if (defaultModels != null)
                    restRequest.WithArgument("default", defaultModels);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<TranslationModels>().Result;
                if(result == null)
                    result = new TranslationModels();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
