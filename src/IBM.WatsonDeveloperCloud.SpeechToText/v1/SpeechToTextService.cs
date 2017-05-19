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

using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public class SpeechToTextService : WatsonService, ISpeechToTextService
    {
        const string SERVICE_NAME = "speech_to_text";

        //const string RELATIVE_PATH = "/speech-to-text/api";

        const string PATH_MODELS = "/v1/models";
        const string PATH_CREATE_SESSION = "/v1/sessions";
        const string PATH_DELETE_SESSION = "/v1/sessions";
        const string PATH_RECOGNIZE = "/v1/recognize";
        const string PATH_SESSION_RECOGNIZE = "/v1/sessions/{0}/recognize";
        const string PATH_OBSERVE_RESULT = "/v1/sessions/{0}/observe_result";
        const string PATH_CUSTOM_MODEL = "/v1/customizations";

        const string URL = "https://stream.watsonplatform.net/speech-to-text/api";

        const string MODEL_NAME_DEFUALT = "en-US_BroadbandModel";

        public SpeechToTextService()
            : base(SERVICE_NAME, URL)
        {
            this.Endpoint = URL;
        }

        public SpeechToTextService(IClient client)
            : base(SERVICE_NAME, URL, client) { }

        public SpeechToTextService(string userName, string password)
            : this()
        {
            this.SetCredential(userName, password);
        }

        public SpeechModelSet GetModels()
        {
            SpeechModelSet result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                            .GetAsync($"{this.Endpoint}{PATH_MODELS}")
                            .As<SpeechModelSet>()
                            .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public SpeechModel GetModel(string modelName)
        {
            if (string.IsNullOrEmpty(modelName))
                throw new ArgumentNullException("modelName can not be null or empty");

            SpeechModel result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                        .GetAsync($"{this.Endpoint}{PATH_MODELS}/{modelName}")
                        .As<SpeechModel>()
                        .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public Session CreateSession(string modelName)
        {
            Session result = null;

            try
            {
                if (string.IsNullOrEmpty(modelName))
                    modelName = MODEL_NAME_DEFUALT;

                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CREATE_SESSION}")
                               .WithArgument("model", modelName)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<Session>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public RecognizeStatus GetSessionStatus(Session session)
        {
            return this.GetSessionStatus(session.SessionId);
        }

        public RecognizeStatus GetSessionStatus(string sessionId)
        {
            RecognizeStatus result = null;

            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("session id can not be null or empty");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{string.Format(PATH_SESSION_RECOGNIZE, sessionId)}")
                               .WithHeader("Cookie", sessionId)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<RecognizeStatus>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object DeleteSession(Session session)
        {
            return this.DeleteSession(session.SessionId);
        }

        public object DeleteSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("session id can not be null or empty");

            object result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .DeleteAsync(string.Format("{0}{1}/{2}", this.Endpoint, PATH_DELETE_SESSION, sessionId))
                               .AsMessage()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public SpeechRecognitionEvent Recognize(string contentType, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string customizationId = "", bool? continuous = null, int? inactivityTimeout = null, string[] keywords = null, double? keywordsThreshold = null, int? maxAlternatives = null, double? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool profanityFilter = false, bool? smartFormatting = null, bool? speakerLabels = null)
        {
            if (audio == null)
                throw new ArgumentNullException($"{nameof(audio)}");

            return
                this.Recognize(sessionId: string.Empty,
                               contentType: contentType,
                               transferEncoding: transferEncoding,
                               metaData: null,
                               audio: audio,
                               customizationId: customizationId,
                               continuous: continuous,
                               keywords: keywords,
                               keywordsThreshold: keywordsThreshold,
                               wordAlternativesThreshold: wordAlternativesThreshold,
                               wordConfidence: wordConfidence,
                               timestamps: timestamps,
                               smartFormatting: smartFormatting,
                               speakerLabels: speakerLabels,
                               profanityFilter: profanityFilter,
                               maxAlternatives: maxAlternatives,
                               inactivityTimeout: inactivityTimeout,
                               model: model);
        }

        public SpeechRecognitionEvent Recognize(string contentType, Metadata metaData, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string customizationId = "")
        {
            if (metaData == null)
                throw new ArgumentNullException($"{nameof(metaData)}");

            if (audio == null)
                throw new ArgumentNullException($"{nameof(audio)}");

            return
                this.Recognize(sessionId: string.Empty,
                               contentType: contentType,
                               transferEncoding: transferEncoding,
                               metaData: metaData,
                               audio: audio,
                               customizationId: customizationId,
                               model: model);
        }

        public SpeechRecognitionEvent RecognizeWithSession(string sessionId, string contentType, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string customizationId = "", bool? continuous = null, int? inactivityTimeout = null, string[] keywords = null, double? keywordsThreshold = null, int? maxAlternatives = null, double? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool profanityFilter = false, bool? smartFormatting = null, bool? speakerLabels = null)
        {
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException($"{nameof(sessionId)}");

            if (audio == null)
                throw new ArgumentNullException($"{nameof(audio)}");

            return
                this.Recognize(sessionId,
                               contentType: contentType,
                               transferEncoding: transferEncoding,
                               metaData: null,
                               audio: audio,
                               customizationId: customizationId,
                               continuous: continuous,
                               keywords: keywords,
                               keywordsThreshold: keywordsThreshold,
                               wordAlternativesThreshold: wordAlternativesThreshold,
                               wordConfidence: wordConfidence,
                               timestamps: timestamps,
                               smartFormatting: smartFormatting,
                               speakerLabels: speakerLabels,
                               profanityFilter: profanityFilter,
                               maxAlternatives: maxAlternatives,
                               inactivityTimeout: inactivityTimeout,
                               model: model);
        }

        public SpeechRecognitionEvent RecognizeWithSession(string sessionId, string contentType, Metadata metaData, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string customizationId = "")
        {
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException($"{nameof(sessionId)}");

            if (metaData == null)
                throw new ArgumentNullException($"{nameof(metaData)}");

            if (audio == null)
                throw new ArgumentNullException($"{nameof(audio)}");

            return
                this.Recognize(sessionId,
                               contentType: contentType,
                               transferEncoding: transferEncoding,
                               metaData: metaData,
                               audio: audio,
                               customizationId: customizationId,
                               model: model);
        }

        private SpeechRecognitionEvent Recognize(string sessionId, string contentType, Metadata metaData, Stream audio, string transferEncoding = "", string model = "", string customizationId = "", bool? continuous = null, int? inactivityTimeout = null, string[] keywords = null, double? keywordsThreshold = null, int? maxAlternatives = null, double? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool profanityFilter = false, bool? smartFormatting = null, bool? speakerLabels = null)
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException($"{nameof(contentType)}");

            SpeechRecognitionEvent result = null;

            try
            {
                string urlService = string.Empty;
                IRequest request = null;

                if (string.IsNullOrEmpty(sessionId))
                {
                    request =
                        this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_RECOGNIZE}");
                }
                else
                {
                    request =
                        this.Client.WithAuthentication(this.UserName, this.Password)
                                   .PostAsync($"{this.Endpoint}{string.Format(PATH_SESSION_RECOGNIZE, sessionId)}")
                                   .WithHeader("Cookie", sessionId);
                }

                if (!string.IsNullOrEmpty(transferEncoding))
                    request.WithHeader("Transfer-Encoding", transferEncoding);

                if (metaData == null)
                {
                    // if a session exists, the model should not be sent
                    if (string.IsNullOrEmpty(sessionId))
                        request.WithArgument("model", model);

                    if (!string.IsNullOrEmpty(customizationId))
                        request.WithArgument("customization_id", customizationId);

                    if (continuous.HasValue)
                        request.WithArgument("continuous", continuous.Value);

                    if (inactivityTimeout.HasValue && inactivityTimeout.Value > 0)
                        request.WithArgument("inactivity_timeout", inactivityTimeout.Value);

                    if (keywords != null && keywords.Length > 0)
                        request.WithArgument("keywords", keywords);

                    if (keywordsThreshold.HasValue && keywordsThreshold.Value > 0)
                        request.WithArgument("keywords_threshold", keywordsThreshold.Value);

                    if (maxAlternatives.HasValue && maxAlternatives.Value > 0)
                        request.WithArgument("max_alternatives", maxAlternatives.Value);

                    if (wordAlternativesThreshold.HasValue && wordAlternativesThreshold.Value > 0)
                        request.WithArgument("word_alternatives_threshold", wordAlternativesThreshold.Value);

                    if (wordConfidence.HasValue)
                        request.WithArgument("word_confidence", wordConfidence.Value);

                    if (timestamps.HasValue)
                        request.WithArgument("timestamps", timestamps.Value);

                    if (profanityFilter)
                        request.WithArgument("profanity_filter", profanityFilter);

                    if (smartFormatting.HasValue)
                        request.WithArgument("smart_formatting", smartFormatting.Value);

                    if (speakerLabels.HasValue)
                        request.WithArgument("speaker_labels", speakerLabels.Value);

                    StreamContent bodyContent = new StreamContent(audio);
                    bodyContent.Headers.Add("Content-Type", contentType);

                    request.WithBodyContent(bodyContent);
                }
                else
                {
                    var json = JsonConvert.SerializeObject(metaData);

                    StringContent metadata = new StringContent(json);
                    metadata.Headers.ContentType = MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON);

                    var audioContent = new ByteArrayContent((audio as Stream).ReadAllBytes());
                    audioContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);

                    MultipartFormDataContent formData = new MultipartFormDataContent();

                    // if a session exists, the model should not be sent
                    if (string.IsNullOrEmpty(sessionId))
                        request.WithArgument("model", model);

                    formData.Add(metadata, "metadata");
                    formData.Add(audioContent, "upload");

                    request.WithBodyContent(formData);
                }

                result =
                    request.As<SpeechRecognitionEvent>()
                           .Result;

            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public List<SpeechRecognitionEvent> ObserveResult(string sessionId, int? sequenceId = (int?)null, bool interimResults = false)
        {
            List<SpeechRecognitionEvent> result = null;

            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("SessionId can not be null or empty");

            try
            {
                var request =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{string.Format(PATH_OBSERVE_RESULT, sessionId)}");

                if (sequenceId.HasValue)
                    request.WithArgument("sequence_id", sequenceId);

                if (interimResults)
                    request.WithArgument("interim_results", interimResults);

                var strResult =
                    request.AsString()
                           .Result;
                var serializer = new JsonSerializer();

                using (var jsonReader = new JsonTextReader(new StringReader(strResult)))
                {
                    jsonReader.SupportMultipleContent = true;
                    result = new List<SpeechRecognitionEvent>();

                    while (jsonReader.Read())
                    {
                        var speechRecognitionEvent = serializer.Deserialize<SpeechRecognitionEvent>(jsonReader);
                        result.Add(speechRecognitionEvent);
                    }
                }
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public CustomizationID CreateCustomModel(string model, string baseModelName, string description)
        {
            return CreateCustomModel(new CustomModel()
            {
                Name = model,
                BaseModelName = baseModelName,
                Description = description
            });
        }

        public CustomizationID CreateCustomModel(CustomModel options)
        {
            CustomizationID result = null;

            if (string.IsNullOrEmpty(options.Name))
                throw new ArgumentNullException(nameof(options.Name));

            if (string.IsNullOrEmpty(options.BaseModelName))
                throw new ArgumentNullException(nameof(options.BaseModelName));

            try
            {
                var json =
                    JObject.FromObject(options);

                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}")
                               .WithBody<JObject>(json, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                               .As<CustomizationID>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public Customizations ListCustomModels(string language = "en-US")
        {
            Customizations result;

            if (string.IsNullOrEmpty(language))
                throw new ArgumentNullException($"{nameof(language)}");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}")
                               .WithArgument("language", language)
                               .As<Customizations>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public Customization ListCustomModel(string customizationId)
        {
            Customization result;

            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            try
            {
                result =
                   this.Client.WithAuthentication(this.UserName, this.Password)
                              .GetAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}")
                              .As<Customization>()
                              .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object TrainCustomModel(string customizationId, string wordTypeToAdd = "all")
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                              .PostAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/train")
                              .WithArgument("word_type_to_add", wordTypeToAdd)
                              .AsString();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object ResetCustomModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                              .PostAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/reset")
                              .AsString();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object UpgradeCustomModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            object result = null;
            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                              .PostAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/upgrade_model")
                              .AsString();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object DeleteCustomModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                              .DeleteAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}")
                              .AsString();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object AddCorpus(string customizationId, string corpusName, bool allowOverwrite, Stream body)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException($"{nameof(corpusName)}");

            if (corpusName.ToLower().Equals("user"))
                throw new ArgumentException($"The {nameof(corpusName)} can not be the string 'user'");

            if (corpusName.Any(Char.IsWhiteSpace))
                throw new ArgumentException($"The {nameof(corpusName)} cannot contain spaces");

            if (body == null)
                throw new ArgumentNullException($"The {nameof(body)} is required");

            object result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                var forcedGlossaryContent = new ByteArrayContent((body as Stream).ReadAllBytes());
                forcedGlossaryContent.Headers.ContentType = MediaTypeHeaderValue.Parse(HttpMediaType.TEXT);
                formData.Add(forcedGlossaryContent, "body");

                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                                  .PostAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/corpora/{corpusName}")
                                  .WithArgument("allow_overwrite ", allowOverwrite)
                                  .WithBodyContent(formData)
                                  .AsString()
                                  .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public Corpora ListCorpora(string customizationId)
        {
            Corpora result = null;

            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/corpora")
                               .As<Corpora>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public Corpus GetCorpus(string customizationId, string corpusName)
        {
            Corpus result = null;

            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException($"{nameof(corpusName)}");

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/corpora/{corpusName}")
                               .As<Corpus>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object DeleteCorpus(string customizationId, string name)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException($"{nameof(name)}");

            object result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .DeleteAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/corpora/{name}")
                               .AsString()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result; 
        }

        public object AddCustomWords(string customizationId, Words body)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            if (body == null)
                throw new ArgumentNullException($"{nameof(body)}");

            object result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/words")
                               .WithBody<Words>(body)
                               .AsString()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object AddCustomWord(string customizationId, string wordname, WordDefinition body)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            if (string.IsNullOrEmpty(wordname))
                throw new ArgumentNullException($"{nameof(wordname)}");

            if (body == null)
                throw new ArgumentNullException($"{nameof(body)}");

            object result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PutAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/words/{wordname}")
                               .WithBody<WordDefinition>(body)
                               .AsString()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public WordsList ListCustomWords(string customizationId, WordType? wordType, Sort? sort)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            WordsList result = null;

            try
            {

                var request =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/words");

                if (wordType.HasValue)
                {
                    request.WithArgument("word_type", wordType.Value.ToString().ToLower());
                }

                if (sort.HasValue)
                {
                    switch (sort.Value)
                    {
                        case Sort.AscendingAlphabetical:
                            request.WithArgument("sort", "+alphabetical");
                            break;
                        case Sort.DescendingAlphabetical:
                            request.WithArgument("sort", "-alphabetical");
                            break;
                        case Sort.AscendingCount:
                            request.WithArgument("sort", "+count");
                            break;
                        case Sort.DescendingCount:
                            request.WithArgument("sort", "-count");
                            break;
                    }
                }

                result =
                    request.As<WordsList>()
                           .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public WordData ListCustomWord(string customizationId, string wordname)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            if (string.IsNullOrEmpty(wordname))
                throw new ArgumentNullException($"{nameof(wordname)}");

            WordData result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/words/{wordname}")
                               .As<WordData>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public object DeleteCustomWord(string customizationId, string wordname)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"{nameof(customizationId)}");

            if (string.IsNullOrEmpty(wordname))
                throw new ArgumentNullException($"{nameof(wordname)}");

            object result = null;
            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                           .DeleteAsync($"{this.Endpoint}{PATH_CUSTOM_MODEL}/{customizationId}/words/{wordname}");
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }
    }
}