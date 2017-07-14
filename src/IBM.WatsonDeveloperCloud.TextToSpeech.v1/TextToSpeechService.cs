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
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    public class TextToSpeechService : WatsonService, ITextToSpeechService
    {
        const string SERVICE_NAME = "text_to_speech";

        const string PATH_VOICES = "/v1/voices";
        const string PATH_SYNTHESIZE = "/v1/synthesize";
        const string PATH_PRONUNCIATION = "/v1/pronunciation";
        const string PATH_CUSTOMIZATIONS = "/v1/customizations";
        const string PATH_CUSTOMIZATION = PATH_CUSTOMIZATIONS + "/{0}";
        const string PATH_WORDS = PATH_CUSTOMIZATION + "/words";
        const string PATH_WORD = PATH_WORDS + "/{1}";

        const string URL = "https://stream.watsonplatform.net/text-to-speech/api";

        public TextToSpeechService()
            : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public new IClient Client
        {
            get
            {
                if (base.Client == null)
                    base.Client = new WatsonHttpClient(this.Endpoint, this.UserName, this.Password);

                return base.Client;
            }
            set { base.Client = value; }
        }

        public TextToSpeechService(string userName, string password)
            : this()
        {
            this.SetCredential(userName, password);
        }

        public TextToSpeechService(IClient httpClient)
            : this()
        {
            this.Client = httpClient;
        }

        public VoiceSet GetVoices()
        {
            VoiceSet result = null;

            try
            {
                result =
                    Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync($"{this.Endpoint}{PATH_VOICES}")
                          .As<VoiceSet>()
                          .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public VoiceCustomization GetVoice(string voiceName, string customizationId = "")
        {
            if (string.IsNullOrEmpty(voiceName))
                throw new ArgumentNullException($"The parameter {nameof(voiceName)} must be provided");

            VoiceCustomization result;

            try
            {
                var request =
                    Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync($"{this.Endpoint}{PATH_VOICES}/{voiceName}");

                if (!string.IsNullOrEmpty(customizationId))
                    request.WithArgument("customization_id", customizationId);

                result =
                    request.As<VoiceCustomization>()
                           .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public byte[] Synthesize(string text, string accept = "audio/ogg;codecs=opus", string voice = "en-US_MichaelVoice", string customizationId = "")
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException($"The parameter {nameof(text)} must be provided");

            return Synthesize(text: text,
                              body: null,
                              accept: accept,
                              voice: voice,
                              customizationId: customizationId);
        }

        public byte[] Synthesize(Text body, string accept = "audio/ogg;codecs=opus", string voice = "en-US_MichaelVoice", string customizationId = "")
        {
            if (body == null)
                throw new ArgumentNullException($"The parameter {nameof(body)} must be provided");

            if (string.IsNullOrEmpty(body.TextProperty))
                throw new ArgumentNullException($"The parameter {nameof(body.TextProperty)} must be provided");

            return Synthesize(text: null,
                              body: body,
                              accept: accept,
                              voice: voice,
                              customizationId: customizationId);
        }

        private byte[] Synthesize(string text, Text body, string accept, string voice, string customizationId)
        {
            byte[] result = null;

            IRequest request = null;

            try
            {
                if (body == null)
                {
                    request =
                        this.Client.WithAuthentication(this.UserName, this.Password)
                                   .GetAsync($"{this.Endpoint}{PATH_SYNTHESIZE}")
                                   .WithHeader("Accept", accept)
                                   .WithArgument("accept", accept)
                                   .WithArgument("voice", voice)
                                   .WithArgument("text", text);
                }
                else
                {
                    request =
                        this.Client.WithAuthentication(this.UserName, this.Password)
                                   .PostAsync($"{this.Endpoint}{PATH_SYNTHESIZE}")
                                   .WithHeader("Accept", accept)
                                   .WithArgument("accept", accept)
                                   .WithArgument("voice", voice)
                                   .WithBody<Text>(body);
                }

                if (!string.IsNullOrEmpty(customizationId))
                    request.WithArgument("customization_id", customizationId);

                result =
                    request.AsByteArray()
                           .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public Pronunciation GetPronunciation(string text, string voice = "en-US_MichaelVoice", string format = "ipa", string customizationId = "")
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException($"The parameter {nameof(text)} must be provided");

            Pronunciation result = null;

            try
            {
                var request =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_PRONUNCIATION}")
                               .WithArgument("text", text);

                if (!string.IsNullOrEmpty(voice))
                    request.WithArgument("voice", voice);

                if (!string.IsNullOrEmpty(format))
                    request.WithArgument("format", format);

                if (!string.IsNullOrEmpty(customizationId))
                    request.WithArgument("customizationId", customizationId);

                result =
                    request.As<Pronunciation>()
                           .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public Customizations ListCustomModels(string language = "")
        {
            Customizations result = null;

            try
            {
                var ret =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}{PATH_CUSTOMIZATIONS}");

                if (!string.IsNullOrEmpty(language))
                    ret.WithArgument("language", language);

                result =
                    ret.As<Customizations>()
                       .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public CustomizationWords ListCustomModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException($"The parameter {nameof(customizationId)} must be provided");

            CustomizationWords result = null;

            try
            {
                result =
                    Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync($"{this.Endpoint}{PATH_CUSTOMIZATIONS}/{customizationId}")
                          .As<CustomizationWords>()
                          .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public CustomizationID CreateCustomModel(CustomVoice customVoice)
        {
            if (customVoice == null)
                throw new ArgumentNullException($"The parameter {nameof(customVoice)} must be provided");

            if (string.IsNullOrEmpty(customVoice.Name))
                throw new ArgumentNullException($"The parameter {nameof(customVoice.Name)} must be provided");

            CustomizationID result = null;

            try
            {
                result =
                    Client.WithAuthentication(this.UserName, this.Password)
                          .PostAsync($"{this.Endpoint}{PATH_CUSTOMIZATIONS}")
                          .WithBody<CustomVoice>(customVoice)
                          .As<CustomizationID>()
                          .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public Remove_CustomVoiceModel updateCustomVoiceModel(Remove_CustomVoiceModel model)
        {
            string path = string.Format(PATH_CUSTOMIZATION, model.Id);

            CustomVoiceModelUpdate updateModel = new CustomVoiceModelUpdate()
            {
                Name = model.Name,
                Description = model.Description,
                Words = model.Words == null ? new List<CustomWordTranslation>() : model.Words
            };

            string s = JsonConvert.SerializeObject(updateModel);
            var retorno =
                Client.WithAuthentication(this.UserName, this.Password)
                    .PostAsync(this.Endpoint + path, updateModel)
                    .WithBody<CustomVoiceModelUpdate>(updateModel)
                    .AsMessage();

            return model;
        }

        private Remove_CustomVoiceModel saveNewCustomVoiceModel(Remove_CustomVoiceModel model)
        {
            string path = PATH_CUSTOMIZATIONS;

            CustomVoiceModelCreate createModel = new CustomVoiceModelCreate()
            {
                Name = model.Name,
                Description = model.Description,
                Language = model.Language
            };

            var retorno =
                Client.WithAuthentication(this.UserName, this.Password)
                    .PostAsync(this.Endpoint + path)
                    .WithBody<CustomVoiceModelCreate>(createModel)
                    .As<Remove_CustomVoiceModel>()
                    .Result;

            model.Id = retorno.Id;

            return model;
        }

        public void DeleteCustomVoiceModel(Remove_CustomVoiceModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new ArgumentNullException("Model id must not be empty");

            DeleteCustomVoiceModel(model.Id);
        }

        public void DeleteCustomVoiceModel(string modelID)
        {
            if (string.IsNullOrEmpty(modelID))
                throw new ArgumentNullException("Model id must not be empty");

            Client.WithAuthentication(this.UserName, this.Password)
                          .DeleteAsync(this.Endpoint + string.Format(PATH_CUSTOMIZATION, modelID))
                          .AsMessage();
        }

        public List<CustomWordTranslation> GetWords(Remove_CustomVoiceModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new ArgumentNullException("Model id must not be empty");

            return GetWords(model.Id);
        }

        public List<CustomWordTranslation> GetWords(string modelID)
        {
            if (string.IsNullOrEmpty(modelID))
                throw new ArgumentNullException("Model id must not be empty");

            return Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync(this.Endpoint + string.Format(PATH_WORDS, modelID))
                          .As<CustomWordTranslations>()
                          .Result.Words;
        }

        public void SaveWords(Remove_CustomVoiceModel model, params CustomWordTranslation[] translations)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new ArgumentNullException("Model id must not be empty");

            if (translations.Length == 0)
                throw new Exception("Must have at least one word to save");

            SaveWords(model.Id, translations);
        }

        public void SaveWords(string modelID, params CustomWordTranslation[] translations)
        {
            if (string.IsNullOrEmpty(modelID))
                throw new ArgumentNullException("Model id must not be empty");

            if (translations.Length == 0)
                throw new Exception("Must have at least one word to save");

            CustomWordTranslations customWordTranslations = new CustomWordTranslations()
            {
                Words = translations.ToList()
            };

            var x = Client.WithAuthentication(this.UserName, this.Password)
                      .PostAsync(this.Endpoint + string.Format(PATH_WORDS, modelID))
                      .WithBody<CustomWordTranslations>(customWordTranslations)
                      .AsMessage().Result;
        }

        public void DeleteWord(Remove_CustomVoiceModel model, CustomWordTranslation translation)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new ArgumentNullException("Model id must not be empty");

            if (string.IsNullOrEmpty(translation.Word))
                throw new ArgumentNullException("Word must not be empty");

            DeleteWord(model.Id, translation.Word);
        }

        public void DeleteWord(string modelID, CustomWordTranslation translation)
        {
            if (string.IsNullOrEmpty(modelID))
                throw new ArgumentNullException("Model id must not be empty");

            if (string.IsNullOrEmpty(translation.Word))
                throw new ArgumentNullException("Word must not be empty");

            DeleteWord(modelID, translation.Word);
        }

        public void DeleteWord(Remove_CustomVoiceModel model, string word)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new ArgumentNullException("Model id must not be empty");

            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException("Word must not be empty");

            DeleteWord(model.Id, word);
        }

        public void DeleteWord(string modelID, string word)
        {
            if (string.IsNullOrEmpty(modelID))
                throw new ArgumentNullException("Model id must not be empty");

            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException("Word must not be empty");

            var r = Client.WithAuthentication(this.UserName, this.Password)
              .DeleteAsync(this.Endpoint + string.Format(PATH_WORD, modelID, word))
              .AsMessage()
              .Result;
        }
    }
}
