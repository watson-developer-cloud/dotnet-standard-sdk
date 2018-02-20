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
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using System;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public partial class SpeechToTextService : WatsonService, ISpeechToTextService
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/models/{modelId}")
                                .As<SpeechModel>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/models")
                                .As<SpeechModels>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        
        public LanguageModel CreateLanguageModel(string contentType, CreateLanguageModel createLanguageModel)
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (createLanguageModel == null)
                throw new ArgumentNullException(nameof(createLanguageModel));
            LanguageModel result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations")
                                .WithHeader("Content-Type", contentType)
                                .WithBody<CreateLanguageModel>(createLanguageModel)
                                .As<LanguageModel>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}")
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}")
                                .As<LanguageModel>()
                                .Result;
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

                result = request.As<LanguageModels>()
                    .Result;
                //result = this.Client.WithAuthentication(this.UserName, this.Password)
                //                .GetAsync($"{this.Endpoint}/v1/customizations")
                //                .WithArgument("language", language)
                //                .As<LanguageModels>()
                //                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/reset")
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/upgrade")
                                .As<object>()
                                .Result;
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

                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}")
                                .WithArgument("allow_overwrite", allowOverwrite)
                                .WithBodyContent(formData)
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}")
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}")
                                .As<Corpus>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora")
                                .As<Corpora>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public object AddWord(string customizationId, string wordName, string contentType, CustomWord customWord)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (customWord == null)
                throw new ArgumentNullException(nameof(customWord));
            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}")
                                .WithHeader("Content-Type", contentType)
                                .WithBody<CustomWord>(customWord)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public object AddWords(string customizationId, string contentType, CustomWords customWords)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (customWords == null)
                throw new ArgumentNullException(nameof(customWords));
            object result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words")
                                .WithHeader("Content-Type", contentType)
                                .WithBody<CustomWords>(customWords)
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}")
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}")
                                .As<Word>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words")
                                .WithArgument("word_type", wordType)
                                .WithArgument("sort", sort)
                                .As<Words>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public AcousticModel CreateAcousticModel(string contentType, CreateAcousticModel createAcousticModel)
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            if (createAcousticModel == null)
                throw new ArgumentNullException(nameof(createAcousticModel));
            AcousticModel result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations")
                                .WithHeader("Content-Type", contentType)
                                .WithBody<CreateAcousticModel>(createAcousticModel)
                                .As<AcousticModel>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}")
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}")
                                .As<AcousticModel>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations")
                                .WithArgument("language", language)
                                .As<AcousticModels>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/reset")
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/train")
                                .WithArgument("custom_language_model_id", customLanguageModelId)
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/upgrade")
                                .WithArgument("custom_language_model_id", customLanguageModelId)
                                .As<object>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        public object AddAudio(string customizationId, string audioName, List<byte[]> audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null)
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}")
                                .WithHeader("Content-Type", contentType)
                                .WithHeader("Contained-Content-Type", containedContentType)
                                .WithArgument("allow_overwrite", allowOverwrite)
                                .WithBody<List<byte[]>>(audioResource)
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}")
                                .As<object>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}")
                                .As<AudioListing>()
                                .Result;
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
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio")
                                .As<AudioResources>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
