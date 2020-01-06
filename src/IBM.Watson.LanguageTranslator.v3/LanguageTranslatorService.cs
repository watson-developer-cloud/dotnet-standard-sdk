/**
* (C) Copyright IBM Corp. 2018, 2020.
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
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.LanguageTranslator.v3.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.LanguageTranslator.v3
{
    public partial class LanguageTranslatorService : IBMService, ILanguageTranslatorService
    {
        const string defaultServiceName = "language_translator";
        private const string defaultServiceUrl = "https://gateway.watsonplatform.net/language-translator/api";
        public string VersionDate { get; set; }

        public LanguageTranslatorService(string versionDate) : this(versionDate, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public LanguageTranslatorService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public LanguageTranslatorService(string versionDate, IAuthenticator authenticator) : base(defaultServiceName, authenticator)
        {
            if (string.IsNullOrEmpty(versionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            VersionDate = versionDate;

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
        }

        /// <summary>
        /// Translate.
        ///
        /// Translates the input text from the source language to the target language.
        /// </summary>
        /// <param name="text">Input text in UTF-8 encoding. Multiple entries will result in multiple translations in
        /// the response.</param>
        /// <param name="modelId">A globally unique string that identifies the underlying model that is used for
        /// translation. (optional)</param>
        /// <param name="source">Translation source language code. (optional)</param>
        /// <param name="target">Translation target language code. (optional)</param>
        /// <returns><see cref="TranslationResult" />TranslationResult</returns>
        public DetailedResponse<TranslationResult> Translate(List<string> text, string modelId = null, string source = null, string target = null)
        {
            if (text == null)
            {
                throw new ArgumentNullException("`text` is required for `Translate`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TranslationResult> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/translate");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (text != null && text.Count > 0)
                {
                    bodyObject["text"] = JToken.FromObject(text);
                }
                if (!string.IsNullOrEmpty(modelId))
                {
                    bodyObject["model_id"] = modelId;
                }
                if (!string.IsNullOrEmpty(source))
                {
                    bodyObject["source"] = source;
                }
                if (!string.IsNullOrEmpty(target))
                {
                    bodyObject["target"] = target;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "Translate"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TranslationResult>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TranslationResult>();
                }
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="IdentifiableLanguages" />IdentifiableLanguages</returns>
        public DetailedResponse<IdentifiableLanguages> ListIdentifiableLanguages()
        {

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<IdentifiableLanguages> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/identifiable_languages");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "ListIdentifiableLanguages"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<IdentifiableLanguages>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<IdentifiableLanguages>();
                }
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="IdentifiedLanguages" />IdentifiedLanguages</returns>
        public DetailedResponse<IdentifiedLanguages> Identify(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `Identify`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<IdentifiedLanguages> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/identify");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "text/plain");
                var httpContent = new StringContent(text, Encoding.UTF8);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "Identify"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<IdentifiedLanguages>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<IdentifiedLanguages>();
                }
            }
            catch (AggregateException ae)
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
        /// <param name="_default">If the default parameter isn't specified, the service will return all models (default
        /// and non-default) for each language pair. To return only default models, set this to `true`. To return only
        /// non-default models, set this to `false`. There is exactly one default model per language pair, the IBM
        /// provided base model. (optional)</param>
        /// <returns><see cref="TranslationModels" />TranslationModels</returns>
        public DetailedResponse<TranslationModels> ListModels(string source = null, string target = null, bool? _default = null)
        {

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TranslationModels> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/models");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(source))
                {
                    restRequest.WithArgument("source", source);
                }
                if (!string.IsNullOrEmpty(target))
                {
                    restRequest.WithArgument("target", target);
                }
                if (_default != null)
                {
                    restRequest.WithArgument("default", _default);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "ListModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TranslationModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TranslationModels>();
                }
            }
            catch (AggregateException ae)
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
        /// You can have a <b>maximum of 10 custom models per language pair</b>.
        /// </summary>
        /// <param name="baseModelId">The model ID of the model to use as the base for customization. To see available
        /// models, use the `List models` method. Usually all IBM provided models are customizable. In addition, all
        /// your models that have been created via parallel corpus customization, can be further customized with a
        /// forced glossary.</param>
        /// <param name="forcedGlossary">A TMX file with your customizations. The customizations in the file completely
        /// overwrite the domain translaton data, including high frequency or high confidence phrase translations. You
        /// can upload only one glossary with a file size less than 10 MB per call. A forced glossary should contain
        /// single words or short phrases. (optional)</param>
        /// <param name="parallelCorpus">A TMX file with parallel sentences for source and target language. You can
        /// upload multiple parallel_corpus files in one request. All uploaded parallel_corpus files combined, your
        /// parallel corpus must contain at least 5,000 parallel sentences to train successfully. (optional)</param>
        /// <param name="name">An optional model name that you can use to identify the model. Valid characters are
        /// letters, numbers, dashes, underscores, spaces and apostrophes. The maximum length is 32 characters.
        /// (optional)</param>
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        public DetailedResponse<TranslationModel> CreateModel(string baseModelId, System.IO.MemoryStream forcedGlossary = null, System.IO.MemoryStream parallelCorpus = null, string name = null)
        {
            if (string.IsNullOrEmpty(baseModelId))
            {
                throw new ArgumentNullException("`baseModelId` is required for `CreateModel`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TranslationModel> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (forcedGlossary != null)
                {
                    var forcedGlossaryContent = new ByteArrayContent(forcedGlossary.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    forcedGlossaryContent.Headers.ContentType = contentType;
                    formData.Add(forcedGlossaryContent, "forced_glossary", "filename");
                }

                if (parallelCorpus != null)
                {
                    var parallelCorpusContent = new ByteArrayContent(parallelCorpus.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("application/octet-stream", out contentType);
                    parallelCorpusContent.Headers.ContentType = contentType;
                    formData.Add(parallelCorpusContent, "parallel_corpus", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/models");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(baseModelId))
                {
                    restRequest.WithArgument("base_model_id", baseModelId);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    restRequest.WithArgument("name", name);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "CreateModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TranslationModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TranslationModel>();
                }
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="DeleteModelResult" />DeleteModelResult</returns>
        public DetailedResponse<DeleteModelResult> DeleteModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `DeleteModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<DeleteModelResult> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v3/models/{modelId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "DeleteModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DeleteModelResult>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DeleteModelResult>();
                }
            }
            catch (AggregateException ae)
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
        /// <returns><see cref="TranslationModel" />TranslationModel</returns>
        public DetailedResponse<TranslationModel> GetModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `GetModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<TranslationModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/models/{modelId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "GetModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TranslationModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TranslationModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// List documents.
        ///
        /// Lists documents that have been submitted for translation.
        /// </summary>
        /// <returns><see cref="DocumentList" />DocumentList</returns>
        public DetailedResponse<DocumentList> ListDocuments()
        {

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<DocumentList> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/documents");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "ListDocuments"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentList>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentList>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Translate document.
        ///
        /// Submit a document for translation. You can submit the document contents in the `file` parameter, or you can
        /// reference a previously submitted document by document ID.
        /// </summary>
        /// <param name="file">The source file to translate.
        ///
        /// [Supported file
        /// types](https://cloud.ibm.com/docs/services/language-translator?topic=language-translator-document-translator-tutorial#supported-file-formats)
        ///
        /// Maximum file size: **20 MB**.</param>
        /// <param name="filename">The filename for file.</param>
        /// <param name="fileContentType">The content type of file. (optional)</param>
        /// <param name="modelId">The model to use for translation. `model_id` or both `source` and `target` are
        /// required. (optional)</param>
        /// <param name="source">Language code that specifies the language of the source document. (optional)</param>
        /// <param name="target">Language code that specifies the target language for translation. (optional)</param>
        /// <param name="documentId">To use a previously submitted document as the source for a new translation, enter
        /// the `document_id` of the document. (optional)</param>
        /// <returns><see cref="DocumentStatus" />DocumentStatus</returns>
        public DetailedResponse<DocumentStatus> TranslateDocument(System.IO.MemoryStream file, string filename, string fileContentType = null, string modelId = null, string source = null, string target = null, string documentId = null)
        {
            if (file == null)
            {
                throw new ArgumentNullException("`file` is required for `TranslateDocument`");
            }
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("`filename` is required for `TranslateDocument`");
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<DocumentStatus> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(fileContentType, out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", filename);
                }

                if (modelId != null)
                {
                    var modelIdContent = new StringContent(modelId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    modelIdContent.Headers.ContentType = null;
                    formData.Add(modelIdContent, "model_id");
                }

                if (source != null)
                {
                    var sourceContent = new StringContent(source, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    sourceContent.Headers.ContentType = null;
                    formData.Add(sourceContent, "source");
                }

                if (target != null)
                {
                    var targetContent = new StringContent(target, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    targetContent.Headers.ContentType = null;
                    formData.Add(targetContent, "target");
                }

                if (documentId != null)
                {
                    var documentIdContent = new StringContent(documentId, Encoding.UTF8, HttpMediaType.TEXT_PLAIN);
                    documentIdContent.Headers.ContentType = null;
                    formData.Add(documentIdContent, "document_id");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/documents");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "TranslateDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentStatus>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for TranslateDocument.
        /// </summary>
        public class TranslateDocumentEnums
        {
            /// <summary>
            /// The content type of file.
            /// </summary>
            public class FileContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_POWERPOINT for application/powerpoint
                /// </summary>
                public const string APPLICATION_POWERPOINT = "application/powerpoint";
                /// <summary>
                /// Constant APPLICATION_MSPOWERPOINT for application/mspowerpoint
                /// </summary>
                public const string APPLICATION_MSPOWERPOINT = "application/mspowerpoint";
                /// <summary>
                /// Constant APPLICATION_X_RTF for application/x-rtf
                /// </summary>
                public const string APPLICATION_X_RTF = "application/x-rtf";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant APPLICATION_XML for application/xml
                /// </summary>
                public const string APPLICATION_XML = "application/xml";
                /// <summary>
                /// Constant APPLICATION_VND_MS_EXCEL for application/vnd.ms-excel
                /// </summary>
                public const string APPLICATION_VND_MS_EXCEL = "application/vnd.ms-excel";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET for application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                /// <summary>
                /// Constant APPLICATION_VND_MS_POWERPOINT for application/vnd.ms-powerpoint
                /// </summary>
                public const string APPLICATION_VND_MS_POWERPOINT = "application/vnd.ms-powerpoint";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION for application/vnd.openxmlformats-officedocument.presentationml.presentation
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET for application/vnd.oasis.opendocument.spreadsheet
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET = "application/vnd.oasis.opendocument.spreadsheet";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION for application/vnd.oasis.opendocument.presentation
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION = "application/vnd.oasis.opendocument.presentation";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT for application/vnd.oasis.opendocument.text
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT = "application/vnd.oasis.opendocument.text";
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_RTF for application/rtf
                /// </summary>
                public const string APPLICATION_RTF = "application/rtf";
                /// <summary>
                /// Constant TEXT_HTML for text/html
                /// </summary>
                public const string TEXT_HTML = "text/html";
                /// <summary>
                /// Constant TEXT_JSON for text/json
                /// </summary>
                public const string TEXT_JSON = "text/json";
                /// <summary>
                /// Constant TEXT_PLAIN for text/plain
                /// </summary>
                public const string TEXT_PLAIN = "text/plain";
                /// <summary>
                /// Constant TEXT_RICHTEXT for text/richtext
                /// </summary>
                public const string TEXT_RICHTEXT = "text/richtext";
                /// <summary>
                /// Constant TEXT_RTF for text/rtf
                /// </summary>
                public const string TEXT_RTF = "text/rtf";
                /// <summary>
                /// Constant TEXT_XML for text/xml
                /// </summary>
                public const string TEXT_XML = "text/xml";
                
            }
        }

        /// <summary>
        /// The content type of file.
        /// </summary>
        public class TranslateDocumentFileContentTypeEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_POWERPOINT for application/powerpoint
            /// </summary>
            public const string APPLICATION_POWERPOINT = "application/powerpoint";
            /// <summary>
            /// Constant APPLICATION_MSPOWERPOINT for application/mspowerpoint
            /// </summary>
            public const string APPLICATION_MSPOWERPOINT = "application/mspowerpoint";
            /// <summary>
            /// Constant APPLICATION_X_RTF for application/x-rtf
            /// </summary>
            public const string APPLICATION_X_RTF = "application/x-rtf";
            /// <summary>
            /// Constant APPLICATION_JSON for application/json
            /// </summary>
            public const string APPLICATION_JSON = "application/json";
            /// <summary>
            /// Constant APPLICATION_XML for application/xml
            /// </summary>
            public const string APPLICATION_XML = "application/xml";
            /// <summary>
            /// Constant APPLICATION_VND_MS_EXCEL for application/vnd.ms-excel
            /// </summary>
            public const string APPLICATION_VND_MS_EXCEL = "application/vnd.ms-excel";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET for application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            /// <summary>
            /// Constant APPLICATION_VND_MS_POWERPOINT for application/vnd.ms-powerpoint
            /// </summary>
            public const string APPLICATION_VND_MS_POWERPOINT = "application/vnd.ms-powerpoint";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION for application/vnd.openxmlformats-officedocument.presentationml.presentation
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            /// <summary>
            /// Constant APPLICATION_MSWORD for application/msword
            /// </summary>
            public const string APPLICATION_MSWORD = "application/msword";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET for application/vnd.oasis.opendocument.spreadsheet
            /// </summary>
            public const string APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET = "application/vnd.oasis.opendocument.spreadsheet";
            /// <summary>
            /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION for application/vnd.oasis.opendocument.presentation
            /// </summary>
            public const string APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION = "application/vnd.oasis.opendocument.presentation";
            /// <summary>
            /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT for application/vnd.oasis.opendocument.text
            /// </summary>
            public const string APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT = "application/vnd.oasis.opendocument.text";
            /// <summary>
            /// Constant APPLICATION_PDF for application/pdf
            /// </summary>
            public const string APPLICATION_PDF = "application/pdf";
            /// <summary>
            /// Constant APPLICATION_RTF for application/rtf
            /// </summary>
            public const string APPLICATION_RTF = "application/rtf";
            /// <summary>
            /// Constant TEXT_HTML for text/html
            /// </summary>
            public const string TEXT_HTML = "text/html";
            /// <summary>
            /// Constant TEXT_JSON for text/json
            /// </summary>
            public const string TEXT_JSON = "text/json";
            /// <summary>
            /// Constant TEXT_PLAIN for text/plain
            /// </summary>
            public const string TEXT_PLAIN = "text/plain";
            /// <summary>
            /// Constant TEXT_RICHTEXT for text/richtext
            /// </summary>
            public const string TEXT_RICHTEXT = "text/richtext";
            /// <summary>
            /// Constant TEXT_RTF for text/rtf
            /// </summary>
            public const string TEXT_RTF = "text/rtf";
            /// <summary>
            /// Constant TEXT_XML for text/xml
            /// </summary>
            public const string TEXT_XML = "text/xml";
            
        }
        /// <summary>
        /// Get document status.
        ///
        /// Gets the translation status of a document.
        /// </summary>
        /// <param name="documentId">The document ID of the document.</param>
        /// <returns><see cref="DocumentStatus" />DocumentStatus</returns>
        public DetailedResponse<DocumentStatus> GetDocumentStatus(string documentId)
        {
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `GetDocumentStatus`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<DocumentStatus> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);
                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "GetDocumentStatus"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<DocumentStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<DocumentStatus>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete document.
        ///
        /// Deletes a document.
        /// </summary>
        /// <param name="documentId">Document ID of the document to delete.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteDocument(string documentId)
        {
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `DeleteDocument`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v3/documents/{documentId}");

                restRequest.WithArgument("version", VersionDate);

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "DeleteDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<object>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<object>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get translated document.
        ///
        /// Gets the translated document associated with the given document ID.
        /// </summary>
        /// <param name="documentId">The document ID of the document that was submitted for translation.</param>
        /// <param name="accept">The type of the response: application/powerpoint, application/mspowerpoint,
        /// application/x-rtf, application/json, application/xml, application/vnd.ms-excel,
        /// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-powerpoint,
        /// application/vnd.openxmlformats-officedocument.presentationml.presentation, application/msword,
        /// application/vnd.openxmlformats-officedocument.wordprocessingml.document,
        /// application/vnd.oasis.opendocument.spreadsheet, application/vnd.oasis.opendocument.presentation,
        /// application/vnd.oasis.opendocument.text, application/pdf, application/rtf, text/html, text/json, text/plain,
        /// text/richtext, text/rtf, or text/xml. A character encoding can be specified by including a `charset`
        /// parameter. For example, 'text/html;charset=utf-8'. (optional)</param>
        /// <returns><see cref="byte[]" />byte[]</returns>
        public DetailedResponse<System.IO.MemoryStream> GetTranslatedDocument(string documentId, string accept = null)
        {
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentNullException("`documentId` is required for `GetTranslatedDocument`");
            }
            else
            {
                documentId = Uri.EscapeDataString(documentId);
            }

            if (string.IsNullOrEmpty(VersionDate))
            {
                throw new ArgumentNullException("versionDate cannot be null.");
            }

            DetailedResponse<System.IO.MemoryStream> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v3/documents/{documentId}/translated_document");

                restRequest.WithArgument("version", VersionDate);

                if (!string.IsNullOrEmpty(accept))
                {
                    restRequest.WithHeader("Accept", accept);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("language_translator", "v3", "GetTranslatedDocument"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = new DetailedResponse<System.IO.MemoryStream>();
                result.Result = new System.IO.MemoryStream(restRequest.AsByteArray().Result);
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetTranslatedDocument.
        /// </summary>
        public class GetTranslatedDocumentEnums
        {
            /// <summary>
            /// The type of the response: application/powerpoint, application/mspowerpoint, application/x-rtf,
            /// application/json, application/xml, application/vnd.ms-excel,
            /// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-powerpoint,
            /// application/vnd.openxmlformats-officedocument.presentationml.presentation, application/msword,
            /// application/vnd.openxmlformats-officedocument.wordprocessingml.document,
            /// application/vnd.oasis.opendocument.spreadsheet, application/vnd.oasis.opendocument.presentation,
            /// application/vnd.oasis.opendocument.text, application/pdf, application/rtf, text/html, text/json,
            /// text/plain, text/richtext, text/rtf, or text/xml. A character encoding can be specified by including a
            /// `charset` parameter. For example, 'text/html;charset=utf-8'.
            /// </summary>
            public class AcceptValue
            {
                /// <summary>
                /// Constant APPLICATION_POWERPOINT for application/powerpoint
                /// </summary>
                public const string APPLICATION_POWERPOINT = "application/powerpoint";
                /// <summary>
                /// Constant APPLICATION_MSPOWERPOINT for application/mspowerpoint
                /// </summary>
                public const string APPLICATION_MSPOWERPOINT = "application/mspowerpoint";
                /// <summary>
                /// Constant APPLICATION_X_RTF for application/x-rtf
                /// </summary>
                public const string APPLICATION_X_RTF = "application/x-rtf";
                /// <summary>
                /// Constant APPLICATION_JSON for application/json
                /// </summary>
                public const string APPLICATION_JSON = "application/json";
                /// <summary>
                /// Constant APPLICATION_XML for application/xml
                /// </summary>
                public const string APPLICATION_XML = "application/xml";
                /// <summary>
                /// Constant APPLICATION_VND_MS_EXCEL for application/vnd.ms-excel
                /// </summary>
                public const string APPLICATION_VND_MS_EXCEL = "application/vnd.ms-excel";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET for application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                /// <summary>
                /// Constant APPLICATION_VND_MS_POWERPOINT for application/vnd.ms-powerpoint
                /// </summary>
                public const string APPLICATION_VND_MS_POWERPOINT = "application/vnd.ms-powerpoint";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION for application/vnd.openxmlformats-officedocument.presentationml.presentation
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                /// <summary>
                /// Constant APPLICATION_MSWORD for application/msword
                /// </summary>
                public const string APPLICATION_MSWORD = "application/msword";
                /// <summary>
                /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
                /// </summary>
                public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET for application/vnd.oasis.opendocument.spreadsheet
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET = "application/vnd.oasis.opendocument.spreadsheet";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION for application/vnd.oasis.opendocument.presentation
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION = "application/vnd.oasis.opendocument.presentation";
                /// <summary>
                /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT for application/vnd.oasis.opendocument.text
                /// </summary>
                public const string APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT = "application/vnd.oasis.opendocument.text";
                /// <summary>
                /// Constant APPLICATION_PDF for application/pdf
                /// </summary>
                public const string APPLICATION_PDF = "application/pdf";
                /// <summary>
                /// Constant APPLICATION_RTF for application/rtf
                /// </summary>
                public const string APPLICATION_RTF = "application/rtf";
                /// <summary>
                /// Constant TEXT_HTML for text/html
                /// </summary>
                public const string TEXT_HTML = "text/html";
                /// <summary>
                /// Constant TEXT_JSON for text/json
                /// </summary>
                public const string TEXT_JSON = "text/json";
                /// <summary>
                /// Constant TEXT_PLAIN for text/plain
                /// </summary>
                public const string TEXT_PLAIN = "text/plain";
                /// <summary>
                /// Constant TEXT_RICHTEXT for text/richtext
                /// </summary>
                public const string TEXT_RICHTEXT = "text/richtext";
                /// <summary>
                /// Constant TEXT_RTF for text/rtf
                /// </summary>
                public const string TEXT_RTF = "text/rtf";
                /// <summary>
                /// Constant TEXT_XML for text/xml
                /// </summary>
                public const string TEXT_XML = "text/xml";
                
            }
        }
        /// <summary>
        /// The type of the response: application/powerpoint, application/mspowerpoint, application/x-rtf,
        /// application/json, application/xml, application/vnd.ms-excel,
        /// application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-powerpoint,
        /// application/vnd.openxmlformats-officedocument.presentationml.presentation, application/msword,
        /// application/vnd.openxmlformats-officedocument.wordprocessingml.document,
        /// application/vnd.oasis.opendocument.spreadsheet, application/vnd.oasis.opendocument.presentation,
        /// application/vnd.oasis.opendocument.text, application/pdf, application/rtf, text/html, text/json, text/plain,
        /// text/richtext, text/rtf, or text/xml. A character encoding can be specified by including a `charset`
        /// parameter. For example, 'text/html;charset=utf-8'.
        /// </summary>
        public class GetTranslatedDocumentAcceptEnumValue
        {
            /// <summary>
            /// Constant APPLICATION_POWERPOINT for application/powerpoint
            /// </summary>
            public const string APPLICATION_POWERPOINT = "application/powerpoint";
            /// <summary>
            /// Constant APPLICATION_MSPOWERPOINT for application/mspowerpoint
            /// </summary>
            public const string APPLICATION_MSPOWERPOINT = "application/mspowerpoint";
            /// <summary>
            /// Constant APPLICATION_X_RTF for application/x-rtf
            /// </summary>
            public const string APPLICATION_X_RTF = "application/x-rtf";
            /// <summary>
            /// Constant APPLICATION_JSON for application/json
            /// </summary>
            public const string APPLICATION_JSON = "application/json";
            /// <summary>
            /// Constant APPLICATION_XML for application/xml
            /// </summary>
            public const string APPLICATION_XML = "application/xml";
            /// <summary>
            /// Constant APPLICATION_VND_MS_EXCEL for application/vnd.ms-excel
            /// </summary>
            public const string APPLICATION_VND_MS_EXCEL = "application/vnd.ms-excel";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET for application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_SPREADSHEETML_SHEET = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            /// <summary>
            /// Constant APPLICATION_VND_MS_POWERPOINT for application/vnd.ms-powerpoint
            /// </summary>
            public const string APPLICATION_VND_MS_POWERPOINT = "application/vnd.ms-powerpoint";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION for application/vnd.openxmlformats-officedocument.presentationml.presentation
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_PRESENTATIONML_PRESENTATION = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            /// <summary>
            /// Constant APPLICATION_MSWORD for application/msword
            /// </summary>
            public const string APPLICATION_MSWORD = "application/msword";
            /// <summary>
            /// Constant APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT for application/vnd.openxmlformats-officedocument.wordprocessingml.document
            /// </summary>
            public const string APPLICATION_VND_OPENXMLFORMATS_OFFICEDOCUMENT_WORDPROCESSINGML_DOCUMENT = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            /// <summary>
            /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET for application/vnd.oasis.opendocument.spreadsheet
            /// </summary>
            public const string APPLICATION_VND_OASIS_OPENDOCUMENT_SPREADSHEET = "application/vnd.oasis.opendocument.spreadsheet";
            /// <summary>
            /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION for application/vnd.oasis.opendocument.presentation
            /// </summary>
            public const string APPLICATION_VND_OASIS_OPENDOCUMENT_PRESENTATION = "application/vnd.oasis.opendocument.presentation";
            /// <summary>
            /// Constant APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT for application/vnd.oasis.opendocument.text
            /// </summary>
            public const string APPLICATION_VND_OASIS_OPENDOCUMENT_TEXT = "application/vnd.oasis.opendocument.text";
            /// <summary>
            /// Constant APPLICATION_PDF for application/pdf
            /// </summary>
            public const string APPLICATION_PDF = "application/pdf";
            /// <summary>
            /// Constant APPLICATION_RTF for application/rtf
            /// </summary>
            public const string APPLICATION_RTF = "application/rtf";
            /// <summary>
            /// Constant TEXT_HTML for text/html
            /// </summary>
            public const string TEXT_HTML = "text/html";
            /// <summary>
            /// Constant TEXT_JSON for text/json
            /// </summary>
            public const string TEXT_JSON = "text/json";
            /// <summary>
            /// Constant TEXT_PLAIN for text/plain
            /// </summary>
            public const string TEXT_PLAIN = "text/plain";
            /// <summary>
            /// Constant TEXT_RICHTEXT for text/richtext
            /// </summary>
            public const string TEXT_RICHTEXT = "text/richtext";
            /// <summary>
            /// Constant TEXT_RTF for text/rtf
            /// </summary>
            public const string TEXT_RTF = "text/rtf";
            /// <summary>
            /// Constant TEXT_XML for text/xml
            /// </summary>
            public const string TEXT_XML = "text/xml";
            
        }
    }
}
