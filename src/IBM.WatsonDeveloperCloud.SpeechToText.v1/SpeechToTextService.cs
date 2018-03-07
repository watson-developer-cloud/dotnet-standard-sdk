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
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public class SpeechToTextService : WatsonService, ISpeechToTextService
    {
        const string SERVICE_NAME = "speech_to_text";
        const string URL = "https://stream.watsonplatform.net/speech-to-text/api";
        public SpeechToTextService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }


        public SpeechToTextService(string userName, string password) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);

        }

        public SpeechToTextService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public SpeechModel GetModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));
            SpeechModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/models/{modelId}");
                result = request.As<SpeechModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public SpeechModels ListModels()
        {
            SpeechModels result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/models");
                result = request.As<SpeechModels>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public SpeechRecognitionResults RecognizeSessionless(string model = null, string customizationId = null, string acousticCustomizationId = null, double? customizationWeight = null, string version = null, byte[] audio = null, string contentType = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null)
        {
            SpeechRecognitionResults result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/recognize");
                request.WithHeader("Content-Type", contentType);
                if (!string.IsNullOrEmpty(model))
                    request.WithArgument("model", model);
                if (!string.IsNullOrEmpty(customizationId))
                    request.WithArgument("customization_id", customizationId);
                if (!string.IsNullOrEmpty(acousticCustomizationId))
                    request.WithArgument("acoustic_customization_id", acousticCustomizationId);
                if (customizationWeight != null)
                    request.WithArgument("customization_weight", customizationWeight);
                if (!string.IsNullOrEmpty(version))
                    request.WithArgument("version", version);
                if (inactivityTimeout != null)
                    request.WithArgument("inactivity_timeout", inactivityTimeout);
                request.WithArgument("keywords", keywords != null && keywords.Count > 0 ? string.Join(",", keywords.ToArray()) : null);
                if (keywordsThreshold != null)
                    request.WithArgument("keywords_threshold", keywordsThreshold);
                if (maxAlternatives != null)
                    request.WithArgument("max_alternatives", maxAlternatives);
                if (wordAlternativesThreshold != null)
                    request.WithArgument("word_alternatives_threshold", wordAlternativesThreshold);
                if (wordConfidence != null)
                    request.WithArgument("word_confidence", wordConfidence);
                if (timestamps != null)
                    request.WithArgument("timestamps", timestamps);
                if (profanityFilter != null)
                    request.WithArgument("profanity_filter", profanityFilter);
                if (smartFormatting != null)
                    request.WithArgument("smart_formatting", smartFormatting);
                if (speakerLabels != null)
                    request.WithArgument("speaker_labels", speakerLabels);
                request.WithBody<byte[]>(audio);
                result = request.As<SpeechRecognitionResults>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public RecognitionJob CheckJob(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            RecognitionJob result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/recognitions/{id}");
                result = request.As<RecognitionJob>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public RecognitionJobs CheckJobs()
        {
            RecognitionJobs result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/recognitions");
                result = request.As<RecognitionJobs>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public RecognitionJob CreateJob(byte[] audio, string contentType, string transferEncoding = null, string model = null, string callbackUrl = null, string events = null, string userToken = null, long? resultsTtl = null, string customizationId = null, string acousticCustomizationId = null, double? customizationWeight = null, string version = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null)
        {
            if (audio == null)
                throw new ArgumentNullException(nameof(audio));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            RecognitionJob result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/recognitions");
                request.WithHeader("Content-Type", contentType);
                request.WithHeader("Transfer-Encoding", transferEncoding);
                if (!string.IsNullOrEmpty(model))
                    request.WithArgument("model", model);
                if (!string.IsNullOrEmpty(callbackUrl))
                    request.WithArgument("callback_url", callbackUrl);
                if (!string.IsNullOrEmpty(events))
                    request.WithArgument("events", events);
                if (!string.IsNullOrEmpty(userToken))
                    request.WithArgument("user_token", userToken);
                if (resultsTtl != null)
                    request.WithArgument("results_ttl", resultsTtl);
                if (!string.IsNullOrEmpty(customizationId))
                    request.WithArgument("customization_id", customizationId);
                if (!string.IsNullOrEmpty(acousticCustomizationId))
                    request.WithArgument("acoustic_customization_id", acousticCustomizationId);
                if (customizationWeight != null)
                    request.WithArgument("customization_weight", customizationWeight);
                if (!string.IsNullOrEmpty(version))
                    request.WithArgument("version", version);
                if (inactivityTimeout != null)
                    request.WithArgument("inactivity_timeout", inactivityTimeout);
                request.WithArgument("keywords", keywords != null && keywords.Count > 0 ? string.Join(",", keywords.ToArray()) : null);
                if (keywordsThreshold != null)
                    request.WithArgument("keywords_threshold", keywordsThreshold);
                if (maxAlternatives != null)
                    request.WithArgument("max_alternatives", maxAlternatives);
                if (wordAlternativesThreshold != null)
                    request.WithArgument("word_alternatives_threshold", wordAlternativesThreshold);
                if (wordConfidence != null)
                    request.WithArgument("word_confidence", wordConfidence);
                if (timestamps != null)
                    request.WithArgument("timestamps", timestamps);
                if (profanityFilter != null)
                    request.WithArgument("profanity_filter", profanityFilter);
                if (smartFormatting != null)
                    request.WithArgument("smart_formatting", smartFormatting);
                if (speakerLabels != null)
                    request.WithArgument("speaker_labels", speakerLabels);
                request.WithBody<byte[]>(audio);
                result = request.As<RecognitionJob>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteJob(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/recognitions/{id}");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public RegisterStatus RegisterCallback(string callbackUrl, string userSecret = null)
        {
            if (string.IsNullOrEmpty(callbackUrl))
                throw new ArgumentNullException(nameof(callbackUrl));
            RegisterStatus result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/register_callback");
                if (!string.IsNullOrEmpty(callbackUrl))
                    request.WithArgument("callback_url", callbackUrl);
                if (!string.IsNullOrEmpty(userSecret))
                    request.WithArgument("user_secret", userSecret);
                result = request.As<RegisterStatus>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object UnregisterCallback(string callbackUrl)
        {
            if (string.IsNullOrEmpty(callbackUrl))
                throw new ArgumentNullException(nameof(callbackUrl));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/unregister_callback");
                if (!string.IsNullOrEmpty(callbackUrl))
                    request.WithArgument("callback_url", callbackUrl);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public LanguageModel CreateLanguageModel(CreateLanguageModel createLanguageModel)
        {
            if (createLanguageModel == null)
                throw new ArgumentNullException(nameof(createLanguageModel));
            LanguageModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations");
                request.WithBody<CreateLanguageModel>(createLanguageModel);
                result = request.As<LanguageModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public LanguageModel GetLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            LanguageModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}");
                result = request.As<LanguageModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public LanguageModels ListLanguageModels(string language = null)
        {
            LanguageModels result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations");
                if (!string.IsNullOrEmpty(language))
                    request.WithArgument("language", language);
                result = request.As<LanguageModels>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object ResetLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/reset");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/train");
                if (!string.IsNullOrEmpty(wordTypeToAdd))
                    request.WithArgument("word_type_to_add", wordTypeToAdd);
                if (customizationWeight != null)
                    request.WithArgument("customization_weight", customizationWeight);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object UpgradeLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/upgrade_model");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public object AddCorpus(string customizationId, string corpusName, System.IO.Stream corpusFile, bool? allowOverwrite = null, string corpusFileContentType = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            if (corpusFile == null)
                throw new ArgumentNullException(nameof(corpusFile));
            object result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (corpusFile != null)
                {
                    var corpusFileContent = new ByteArrayContent((corpusFile as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(corpusFileContentType, out contentType);
                    corpusFileContent.Headers.ContentType = contentType;
                    formData.Add(corpusFileContent, "corpus_file", "filename");
                }

                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");
                if (allowOverwrite != null)
                    request.WithArgument("allow_overwrite", allowOverwrite);
                request.WithBodyContent(formData);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteCorpus(string customizationId, string corpusName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Corpus GetCorpus(string customizationId, string corpusName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            Corpus result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");
                result = request.As<Corpus>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Corpora ListCorpora(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            Corpora result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora");
                result = request.As<Corpora>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public object AddWord(string customizationId, string wordName, CustomWord customWord)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            if (customWord == null)
                throw new ArgumentNullException(nameof(customWord));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");
                request.WithBody<CustomWord>(customWord);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object AddWords(string customizationId, CustomWords customWords)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (customWords == null)
                throw new ArgumentNullException(nameof(customWords));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");
                request.WithBody<CustomWords>(customWords);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteWord(string customizationId, string wordName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Word GetWord(string customizationId, string wordName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            Word result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");
                result = request.As<Word>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public Words ListWords(string customizationId, string wordType = null, string sort = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            Words result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");
                if (!string.IsNullOrEmpty(wordType))
                    request.WithArgument("word_type", wordType);
                if (!string.IsNullOrEmpty(sort))
                    request.WithArgument("sort", sort);
                result = request.As<Words>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public AcousticModel CreateAcousticModel(CreateAcousticModel createAcousticModel)
        {
            if (createAcousticModel == null)
                throw new ArgumentNullException(nameof(createAcousticModel));
            AcousticModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations");
                request.WithBody<CreateAcousticModel>(createAcousticModel);
                result = request.As<AcousticModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public AcousticModel GetAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            AcousticModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}");
                result = request.As<AcousticModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public AcousticModels ListAcousticModels(string language = null)
        {
            AcousticModels result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations");
                if (!string.IsNullOrEmpty(language))
                    request.WithArgument("language", language);
                result = request.As<AcousticModels>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object ResetAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/reset");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object TrainAcousticModel(string customizationId, string customLanguageModelId = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/train");
                if (!string.IsNullOrEmpty(customLanguageModelId))
                    request.WithArgument("custom_language_model_id", customLanguageModelId);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object UpgradeAcousticModel(string customizationId, string customLanguageModelId = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/upgrade_model");
                if (!string.IsNullOrEmpty(customLanguageModelId))
                    request.WithArgument("custom_language_model_id", customLanguageModelId);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public object AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            if (audioResource == null)
                throw new ArgumentNullException(nameof(audioResource));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
                request.WithHeader("Content-Type", contentType);
                request.WithHeader("Contained-Content-Type", containedContentType);
                if (allowOverwrite != null)
                    request.WithArgument("allow_overwrite", allowOverwrite);
                var audioResourceContent = new ByteArrayContent(audioResource);
                System.Net.Http.Headers.MediaTypeHeaderValue audioResourceType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out audioResourceType);
                trainingDataContent.Headers.ContentType = audioResourceType;
                request.WithBodyContent(trainingDataContent);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object DeleteAudio(string customizationId, string audioName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public AudioListing GetAudio(string customizationId, string audioName)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            AudioListing result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
                result = request.As<AudioListing>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public AudioResources ListAudio(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            AudioResources result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio");
                result = request.As<AudioResources>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
