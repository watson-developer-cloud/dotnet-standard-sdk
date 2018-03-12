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
using System.Runtime.Serialization;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1
{
    public class TextToSpeechService : WatsonService, ITextToSpeechService
    {
        const string SERVICE_NAME = "text_to_speech";
        const string URL = "https://stream.watsonplatform.net/text-to-speech/api";
        public TextToSpeechService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }


        public TextToSpeechService(string userName, string password) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);

        }

        public TextToSpeechService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Retrieves a specific voice available for speech synthesis. Lists information about the voice specified with the `voice` path parameter. Specify the `customization_id` query parameter to obtain information for that custom voice model of the specified voice. Use the `GET /v1/voices` method to see a list of all available voices.
        /// </summary>
        /// <param name="voice">The voice for which information is to be returned. Retrieve available voices with the `GET /v1/voices` method.</param>
        /// <param name="customizationId">The GUID of a custom voice model for which information is to be returned. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to see information about the specified voice with no customization. (optional)</param>
        /// <returns><see cref="Voice" />Voice</returns>
        public Voice GetVoice(string voice, string customizationId = null)
        {
            if (string.IsNullOrEmpty(voice))
                throw new ArgumentNullException(nameof(voice));
            Voice result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/voices/{voice}");
                if (!string.IsNullOrEmpty(customizationId))
                    request.WithArgument("customization_id", customizationId);
                result = request.As<Voice>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Retrieves all voices available for speech synthesis. Lists information about all available voices. To see information about a specific voice, use the `GET /v1/voices/{voice}` method.
        /// </summary>
        /// <returns><see cref="Voices" />Voices</returns>
        public Voices ListVoices()
        {
            Voices result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/voices");
                result = request.As<Voices>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Streaming speech synthesis of the text in the body parameter. Synthesizes text to spoken audio, returning the synthesized audio stream as an array of bytes. Identical to the `GET` method but passes longer text in the body of the request, not with the URL. Text size is limited to 5 KB. (For the `audio/l16` format, you can optionally specify `endianness=big-endian` or `endianness=little-endian`; the default is little endian.)   If a request includes invalid query parameters, the service returns a `Warnings` response header that provides messages about the invalid parameters. The warning includes a descriptive message and a list of invalid argument strings. For example, a message such as `"Unknown arguments:"` or `"Unknown url query arguments:"` followed by a list of the form `"invalid_arg_1, invalid_arg_2."` The request succeeds despite the warnings.   **Note about the Try It Out feature:** The `Try it out!` button is **not** supported for use with the the `POST /v1/synthesize` method. For examples of calls to the method, see the [Text to Speech API reference](http://www.ibm.com/watson/developercloud/text-to-speech/api/v1/).
        /// </summary>
        /// <param name="text">A `Text` object that provides the text to synthesize. Specify either plain text or a subset of SSML. Text size is limited to 5 KB.</param>
        /// <param name="accept">The type of the response: audio/basic, audio/flac, audio/l16;rate=nnnn, audio/ogg, audio/ogg;codecs=opus, audio/ogg;codecs=vorbis, audio/mp3, audio/mpeg, audio/mulaw;rate=nnnn, audio/wav, audio/webm, audio/webm;codecs=opus, or audio/webm;codecs=vorbis. (optional)</param>
        /// <param name="voice">The voice to use for synthesis. Retrieve available voices with the `GET /v1/voices` method. (optional, default to en-US_MichaelVoice)</param>
        /// <param name="customizationId">The GUID of a custom voice model to use for the synthesis. If a custom voice model is specified, it is guaranteed to work only if it matches the language of the indicated voice. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to use the specified voice with no customization. (optional)</param>
        /// <returns><see cref="System.IO.Stream" />System.IO.Stream</returns>
        public System.IO.Stream Synthesize(Text text, string accept = null, string voice = null, string customizationId = null)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            System.IO.Stream result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/synthesize");
                request.WithArgument("accept", accept);
                if (!string.IsNullOrEmpty(voice))
                    request.WithArgument("voice", voice);
                if (!string.IsNullOrEmpty(customizationId))
                    request.WithArgument("customization_id", customizationId);
                request.WithBody<Text>(text);
                result = new System.IO.MemoryStream(request.AsByteArray().Result);
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Gets the pronunciation for a word. Returns the phonetic pronunciation for the word specified by the `text` parameter. You can request the pronunciation for a specific format. You can also request the pronunciation for a specific voice to see the default translation for the language of that voice or for a specific custom voice model to see the translation for that voice model.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="text">The word for which the pronunciation is requested.</param>
        /// <param name="voice">A voice that specifies the language in which the pronunciation is to be returned. All voices for the same language (for example, `en-US`) return the same translation. Retrieve available voices with the `GET /v1/voices` method. (optional, default to en-US_MichaelVoice)</param>
        /// <param name="format">The phoneme format in which to return the pronunciation. Omit the parameter to obtain the pronunciation in the default format. (optional, default to ipa)</param>
        /// <param name="customizationId">The GUID of a custom voice model for which the pronunciation is to be returned. The language of a specified custom model must match the language of the specified voice. If the word is not defined in the specified custom model, the service returns the default translation for the custom model's language. You must make the request with service credentials created for the instance of the service that owns the custom model. Omit the parameter to see the translation for the specified voice with no customization. (optional)</param>
        /// <returns><see cref="Pronunciation" />Pronunciation</returns>
        public Pronunciation GetPronunciation(string text, string voice = null, string format = null, string customizationId = null)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            Pronunciation result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/pronunciation");
                if (!string.IsNullOrEmpty(text))
                    request.WithArgument("text", text);
                if (!string.IsNullOrEmpty(voice))
                    request.WithArgument("voice", voice);
                if (!string.IsNullOrEmpty(format))
                    request.WithArgument("format", format);
                if (!string.IsNullOrEmpty(customizationId))
                    request.WithArgument("customization_id", customizationId);
                result = request.As<Pronunciation>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Creates a new custom voice model. Creates a new empty custom voice model. The model is owned by the instance of the service whose credentials are used to create it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="createVoiceModel">A `CreateVoiceModel` object that contains information about the new custom voice model.</param>
        /// <returns><see cref="VoiceModel" />VoiceModel</returns>
        public VoiceModel CreateVoiceModel(CreateVoiceModel createVoiceModel)
        {
            if (createVoiceModel == null)
                throw new ArgumentNullException(nameof(createVoiceModel));
            VoiceModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations");
                request.WithBody<CreateVoiceModel>(createVoiceModel);
                result = request.As<VoiceModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Deletes a custom voice model. Deletes the custom voice model with the specified `customization_id`. You must use credentials for the instance of the service that owns a model to delete it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be deleted. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteVoiceModel(string customizationId)
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

        /// <summary>
        /// Queries the contents of a custom voice model. Lists all information about the custom voice model with the specified `customization_id`. In addition to metadata such as the name and description of the voice model, the output includes the words in the model and their translations as defined in the model. To see just the metadata for a voice model, use the `GET /v1/customizations` method. You must use credentials for the instance of the service that owns a model to list information about it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be queried. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="VoiceModel" />VoiceModel</returns>
        public VoiceModel GetVoiceModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            VoiceModel result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}");
                result = request.As<VoiceModel>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Lists all available custom voice models for a language or for all languages. Lists metadata such as the name and description for the custom voice models that you own. Use the `language` query parameter to list the voice models that you own for the specified language only. Omit the parameter to see all voice models that you own for all languages. To see the words in addition to the metadata for a specific voice model, use the `GET /v1/customizations/{customization_id}` method. You must use credentials for the instance of the service that owns a model to list information about it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="language">The language for which custom voice models that are owned by the requesting service credentials are to be returned. Omit the parameter to see all custom voice models that are owned by the requester. (optional)</param>
        /// <returns><see cref="VoiceModels" />VoiceModels</returns>
        public VoiceModels ListVoiceModels(string language = null)
        {
            VoiceModels result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations");
                if (!string.IsNullOrEmpty(language))
                    request.WithArgument("language", language);
                result = request.As<VoiceModels>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Updates information and words for a custom voice model. Updates information for the custom voice model with the specified `customization_id`. You can update the metadata such as the name and description of the voice model. You can also update the words in the model and their translations. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. You must use credentials for the instance of the service that owns a model to update it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be updated. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="updateVoiceModel">An `UpdateVoiceModel` object that contains information that is to be updated for the custom voice model.</param>
        /// <returns><see cref="object" />object</returns>
        public object UpdateVoiceModel(string customizationId, UpdateVoiceModel updateVoiceModel)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (updateVoiceModel == null)
                throw new ArgumentNullException(nameof(updateVoiceModel));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}");
                request.WithBody<UpdateVoiceModel>(updateVoiceModel);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Adds a word to a custom voice model. Adds a single word and its translation to the custom voice model with the specified `customization_id`. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. You must use credentials for the instance of the service that owns a model to add a word to it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be updated. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be added or updated for the custom voice model.</param>
        /// <param name="translation">The translation for the word that is to be added or updated.</param>
        /// <returns><see cref="object" />object</returns>
        public object AddWord(string customizationId, string word, Translation translation)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException(nameof(word));
            if (translation == null)
                throw new ArgumentNullException(nameof(translation));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PutAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{word}");
                request.WithBody<Translation>(translation);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Adds one or more words to a custom voice model. Adds one or more words and their translations to the custom voice model with the specified `customization_id`. Adding a new translation for a word that already exists in a custom model overwrites the word's existing translation. A custom model can contain no more than 20,000 entries. You must use credentials for the instance of the service that owns a model to add words to it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be updated. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customWords">A `Words` object that contains the word or words that are to be added or updated for the custom voice model and the translation for each specified word.</param>
        /// <returns><see cref="object" />object</returns>
        public object AddWords(string customizationId, Words customWords)
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
                request.WithBody<Words>(customWords);
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Deletes a word from a custom voice model. Deletes a single word from the custom voice model with the specified `customization_id`. You must use credentials for the instance of the service that owns a model to delete it.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model from which to delete a word. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be deleted from the custom voice model.</param>
        /// <returns><see cref="object" />object</returns>
        public object DeleteWord(string customizationId, string word)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException(nameof(word));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{word}");
                result = request.As<object>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Queries details about a word in a custom voice model. Returns the translation for a single word from the custom model with the specified `customization_id`. The output shows the translation as it is defined in the model. You must use credentials for the instance of the service that owns a model to query information about its words.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model from which to query a word. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be queried from the custom voice model.</param>
        /// <returns><see cref="Translation" />Translation</returns>
        public Translation GetWord(string customizationId, string word)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException(nameof(word));
            Translation result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{word}");
                result = request.As<Translation>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Queries details about the words in a custom voice model. Lists all of the words and their translations for the custom voice model with the specified `customization_id`. The output shows the translations as they are defined in the model. You must use credentials for the instance of the service that owns a model to query information about its words.   **Note:** This method is currently a beta release.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom voice model that is to be queried. You must make the request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="Words" />Words</returns>
        public Words ListWords(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            Words result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                                .GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");
                result = request.As<Words>().Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
