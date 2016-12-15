using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    public class TextToSpeechService : WatsonService, ITextToSpeechService
    {
        const string SERVICE_NAME = "text_to_speech";

        const string PATH_VOICES = "/v1/voices";
        const string PATH_VOICE = PATH_VOICES + "/{0}";
        const string PATH_SYNTHESIZE = "/v1/synthesize";
        const string PATH_PRONUNCIATION = "/v1/pronunciation";
        const string PATH_CUSTOMIZATIONS = "/v1/customizations";
        const string PATH_CUSTOMIZATION = PATH_CUSTOMIZATIONS + "/{0}";
        const string PATH_WORDS = PATH_CUSTOMIZATION + "/words";
        const string PATH_WORD = PATH_WORDS + "/{1}";

        const string URL = "https://gateway.watsonplatform.net/text-to-speech/api";

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

        public List<Voice> GetVoices()
        {
            return Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync(this.Endpoint + PATH_VOICES)
                          .As<Voices>()
                          .Result.VoiceList;
        }

        public Voice GetVoice(string voiceName)
        {
            if (string.IsNullOrEmpty(voiceName))
                throw new Exception("Parameter 'voiceName' must be provided");

            return Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync(this.Endpoint + string.Format(PATH_VOICE, voiceName))
                          .As<Voice>()
                          .Result;

        }

        public Pronunciation GetPronunciation(string text)
        {
            return GetPronunciation(text, null, null);
        }

        public Pronunciation GetPronunciation(string text, Voice voice)
        {
            return GetPronunciation(text, voice, null);
        }

        public Pronunciation GetPronunciation(string text, Voice voice = null, Phoneme phoneme = null)
        {
            return getPronunciation(text, voice, phoneme);
        }

        private Pronunciation getPronunciation(string text, Voice voice, Phoneme phoneme)
        {
            if (string.IsNullOrEmpty(text))
                throw new Exception("Parameter 'text' must be provided");

            var builder =
            Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync(this.Endpoint + PATH_PRONUNCIATION)
                          .WithArgument("text", text);

            if (voice != null)
                builder.WithArgument("voice", voice.Name);

            if (phoneme != null)
                builder.WithArgument("format", phoneme.Value);

            return builder.As<Pronunciation>().Result;
        }

        public Stream Synthesize(string text, Voice voice)
        {
            return synthesize(text, voice, AudioType.OGG);
        }

        public Stream Synthesize(string text, Voice voice, AudioType audioType)
        {
            return synthesize(text, voice, audioType);
        }

        private Stream synthesize(string text, Voice voice, AudioType audioType)
        {
            if (string.IsNullOrEmpty(text))
                throw new Exception("Parameter 'text' must be provided");

            if (voice == null)
                throw new Exception("Parameter 'voice' must be provided");

            if (audioType == null)
                throw new Exception("Parameter 'audioType' must be provided");

            var builder =
            Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync(this.Endpoint + PATH_SYNTHESIZE)
                          .WithArgument("text", text)
                          .WithArgument("voice", voice.Name)
                          .WithArgument("accept", audioType.Value);

            return new MemoryStream(builder.AsByteArray().Result);
        }

        public List<CustomVoiceModel> GetCustomVoiceModels()
        {
            return this.GetCustomVoiceModels(null);
        }

        public List<CustomVoiceModel> GetCustomVoiceModels(string language)
        {
            var ret = Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync(this.Endpoint + PATH_CUSTOMIZATIONS);

            if (!string.IsNullOrEmpty(language))
                ret.WithArgument("language", language);

            return ret.As<CustomVoiceModels>()
                          .Result.CustomVoiceList;
        }

        public CustomVoiceModel GetCustomVoiceModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new Exception("ModelId must not be empty");

            return Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync(this.Endpoint + string.Format(PATH_CUSTOMIZATION, modelId))
                          .As<CustomVoiceModel>()
                          .Result;
        }

        public CustomVoiceModel SaveCustomVoiceModel(CustomVoiceModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
                return saveNewCustomVoiceModel(model);
            else
                return updateCustomVoiceModel(model);
        }

        public CustomVoiceModel updateCustomVoiceModel(CustomVoiceModel model)
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

        private CustomVoiceModel saveNewCustomVoiceModel(CustomVoiceModel model)
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
                    .As<CustomVoiceModel>()
                    .Result;

            model.Id = retorno.Id;

            return model;
        }

        public void DeleteCustomVoiceModel(CustomVoiceModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new Exception("Model id must not be empty");

            Client.WithAuthentication(this.UserName, this.Password)
                          .DeleteAsync(this.Endpoint + string.Format(PATH_CUSTOMIZATION, model.Id))
                          .AsMessage();
        }

        public List<CustomWordTranslation> GetWords(CustomVoiceModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new Exception("Model id must not be empty");

            return Client.WithAuthentication(this.UserName, this.Password)
                          .GetAsync(this.Endpoint + string.Format(PATH_WORDS, model.Id))
                          .As<CustomWordTranslations>()
                          .Result.Words;
        }

        public void SaveWords(CustomVoiceModel model, params CustomWordTranslation[] translations)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new Exception("Model id must not be empty");

            if (translations.Length ==0)
                throw new Exception("Must have at least one word to save");

            CustomWordTranslations translantions = new CustomWordTranslations()
            {
                Words = translations.ToList()
            };

            var x = Client.WithAuthentication(this.UserName, this.Password)
                      .PostAsync(this.Endpoint + string.Format(PATH_WORDS, model.Id))
                      .WithBody<CustomWordTranslations>(translantions)
                      .AsMessage().Result;
        }

        public void DeleteWord(CustomVoiceModel model, CustomWordTranslation translation)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new Exception("Model id must not be empty");

            if (string.IsNullOrEmpty(translation.Word))
                throw new Exception("Word must not be empty");

            var r = Client.WithAuthentication(this.UserName, this.Password)
              .DeleteAsync(this.Endpoint + string.Format(PATH_WORD, model.Id, translation.Word))
              .AsMessage()
              .Result;
        }

    }
}
