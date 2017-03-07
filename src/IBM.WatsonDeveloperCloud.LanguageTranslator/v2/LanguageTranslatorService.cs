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

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;
using IBM.WatsonDeveloperCloud.Service;
using Newtonsoft.Json.Linq;
using System.Runtime.ExceptionServices;
using IBM.WatsonDeveloperCloud.Http.Extensions;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2
{
    public class LanguageTranslatorService : WatsonService, ILanguageTranslatorService
    {
        const string SERVICE_NAME = "language_translator";

        const string URL = "https://gateway.watsonplatform.net/language-translator/api";
        const string PATH_IDENTIFIABLE_LANGUAGES = "/v2/identifiable_languages";
        const string PATH_IDENTIFY = "/v2/identify";
        const string PATH_TRANSLATE = "/v2/translate";
        const string PATH_LIST_MODELS = "/v2/models";
        const string PATH_MODEL = "/v2/models";

        public LanguageTranslatorService()
            : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public LanguageTranslatorService(string userName, string password)
            : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }

        public LanguageTranslatorService(IClient httpClient)
            : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public TranslationModels ListModels(bool isDefault = true, string source = null, string target = null)
        {
            TranslationModels result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_LIST_MODELS}")
                               .WithArgument("source", source)
                               .WithArgument("target", target)
                               .WithArgument("default", isDefault.ToString())
                               .As<TranslationModels>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                //ExceptionDispatchInfo.Capture(ae.Flatten().InnerException).Throw();

                throw ae.Flatten().InnerException;
            }

            return result;
        }

        public CustomModels CreateModel(CreateModelOptions _options)
        {
            CustomModels result = null;

            if (string.IsNullOrEmpty(_options.BaseModelId))
                throw new ArgumentNullException($"Argument is not valid: {nameof(_options.BaseModelId)}");

            if (_options.Name.Contains(" "))
                throw new ArgumentException($"Argument is not valid (No spaces): {nameof(_options.Name)}");

            if (_options.ForcedGlossary == null && _options.ParallelCorpus == null && _options.MonolingualCorpus == null)
                throw new ArgumentNullException($"Glossary or Corpus file is not valid");

            double fileSize = _options.ForcedGlossary != null ? (_options.ForcedGlossary.Length ) / Math.Pow(1024, 2) : 0;
            fileSize += _options.ParallelCorpus != null ? (_options.ParallelCorpus.Length) / Math.Pow(1024, 2) : 0;
            fileSize += _options.MonolingualCorpus != null ? (_options.MonolingualCorpus.Length) / Math.Pow(1024, 2) : 0;

            if (fileSize > 250)
                throw new Exception("The cumulative file size of all uploaded glossary and corpus files is limited to 250 MB.");

            try
            {
                var formData = new MultipartFormDataContent();

                if(_options.ForcedGlossary != null)
                {
                    var forcedGlossaryContent = new ByteArrayContent((_options.ForcedGlossary as Stream).ReadAllBytes());
                    forcedGlossaryContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");
                    formData.Add(forcedGlossaryContent, "forced_glossary", _options.ForcedGlossary.Name);
                }

                if (_options.ParallelCorpus != null)
                {
                    var parallelCorpusContent = new ByteArrayContent((_options.ParallelCorpus as Stream).ReadAllBytes());
                    parallelCorpusContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");
                    formData.Add(parallelCorpusContent, "forced_glossary", _options.ParallelCorpus.Name);
                }

                if (_options.MonolingualCorpus != null)
                {
                    var monolingualCorpusContent = new ByteArrayContent((_options.MonolingualCorpus as Stream).ReadAllBytes());
                    monolingualCorpusContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");
                    formData.Add(monolingualCorpusContent, "forced_glossary", _options.MonolingualCorpus.Name);
                }

                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}{PATH_MODEL}")
                                .WithArgument("base_model_id", _options.BaseModelId)
                                .WithArgument("name", _options.Name)
                                .WithBodyContent(formData)
                                .As<CustomModels>()
                                .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public DeleteModels DeleteModel(string modelId)
        {
            DeleteModels result = null;

            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException($"Argument is not valid: {nameof(modelId)}");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .DeleteAsync($"{this.Endpoint}{PATH_MODEL}{modelId}")
                               .As<DeleteModels>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public ModelPayload GetModelDetails(string modelId)
        {
            ModelPayload result = null;

            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException($"Argument is not valid: {nameof(modelId)}");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_MODEL}/{modelId}")
                               .As<ModelPayload>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public TranslateResponse Translate(string modelId, string text)
        {
            return this.Translate(modelId, null, null, new List<string>() { text });
        }

        public TranslateResponse Translate(string source, string target, string text)
        {
            return this.Translate(null, source, target, new List<string>() { text });
        }

        public TranslateResponse Translate(string modelId, List<string> text)
        {
            return this.Translate(modelId, null, null, text);
        }

        public TranslateResponse Translate(string source, string target, List<string> text)
        {
            return this.Translate(null, source, target, text);
        }

        private TranslateResponse Translate(string modelId, string source, string target, List<string> text)
        {
            TranslateResponse result = null;

            try
            {
                JObject json = null;

                if (!string.IsNullOrEmpty(modelId))
                    json =
                        new JObject(
                            new JProperty("model_id", modelId),
                            new JProperty("text", new JArray(text)));
                else
                    json =
                        new JObject(
                            new JProperty("source", source),
                            new JProperty("target", target),
                            new JProperty("text", new JArray(text)));

                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                          .PostAsync(this.Endpoint + PATH_TRANSLATE)
                          .WithBody<JObject>(json, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                          .As<TranslateResponse>()
                          .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public IdentifiableLanguages GetIdentifiableLanguages()
        {
            IdentifiableLanguages result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_IDENTIFIABLE_LANGUAGES}")
                               .As<IdentifiableLanguages>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public IdentifiedLanguages Identify(string _text)
        {
            IdentifiedLanguages result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_IDENTIFY}")
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .WithBodyContent(new StringContent(_text, Encoding.UTF8, HttpMediaType.TEXT_PLAIN))
                               .As<IdentifiedLanguages>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}