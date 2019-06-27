/**
* (C) Copyright IBM Corp. 2018, 2019.
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
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.TextToSpeech.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.TextToSpeech.v1
{
    public partial class TextToSpeechService : IBMService, ITextToSpeechService
    {
        new const string SERVICE_NAME = "text_to_speech";
        const string URL = "https://stream.watsonplatform.net/text-to-speech/api";
        public new string DefaultEndpoint = "https://stream.watsonplatform.net/text-to-speech/api";
        public TextToSpeechService() : base(SERVICE_NAME) { }
        
        [Obsolete("Please use TextToSpeechService(IAuthenticatorConfig config) instead")]
        public TextToSpeechService(string userName, string password) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }
        
        [Obsolete("Please use TextToSpeechService(IAuthenticatorConfig config) instead")]
        public TextToSpeechService(TokenOptions options) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                this.Endpoint = options.ServiceUrl;
            }
            else
            {
                options.ServiceUrl = this.Endpoint;
            }

            IamConfig iamConfig = null;
            if (!string.IsNullOrEmpty(options.IamAccessToken))
            {
                iamConfig = new IamConfig(
                    userManagedAccessToken: options.IamAccessToken
                    );
            }
            else
            {
                iamConfig = new IamConfig(
                    apikey: options.IamApiKey,
                    iamUrl: options.IamUrl
                    );
            }

            SetAuthenticator(iamConfig);
        }

        public TextToSpeechService(IClient httpClient) : base(SERVICE_NAME, URL)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public TextToSpeechService(IAuthenticatorConfig config) : base(SERVICE_NAME, config)
        {
        }

        /// <summary>
        /// List voices.
        ///
        /// Lists all voices available for use with the service. The information includes the name, language, gender,
        /// and other details about the voice. To see information about a specific voice, use the **Get a voice**
        /// method.
        ///
        /// **See also:** [Listing all available
        /// voices](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-voices#listVoices).
        /// </summary>
        /// <returns><see cref="Voices" />Voices</returns>
        public DetailedResponse<Voices> ListVoices()
        {
            DetailedResponse<Voices> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/voices");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "ListVoices"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Voices>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Voices>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a voice.
        ///
        /// Gets information about the specified voice. The information includes the name, language, gender, and other
        /// details about the voice. Specify a customization ID to obtain information for that custom voice model of the
        /// specified voice. To list information about all available voices, use the **List voices** method.
        ///
        /// **See also:** [Listing a specific
        /// voice](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-voices#listVoice).
        /// </summary>
        /// <param name="voice">The voice for which information is to be returned.</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom voice model for which information is
        /// to be returned. You must make the request with service credentials created for the instance of the service
        /// that owns the custom model. Omit the parameter to see information about the specified voice with no
        /// customization. (optional)</param>
        /// <returns><see cref="Voice" />Voice</returns>
        public DetailedResponse<Voice> GetVoice(string voice, string customizationId = null)
        {
            if (string.IsNullOrEmpty(voice))
            {
                throw new ArgumentNullException("`voice` is required for `GetVoice`");
            }
            DetailedResponse<Voice> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/voices/{voice}");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(customizationId))
                {
                    restRequest.WithArgument("customization_id", customizationId);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "GetVoice"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Voice>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Voice>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Synthesize audio.
        ///
        /// Synthesizes text to audio that is spoken in the specified voice. The service bases its understanding of the
        /// language for the input text on the specified voice. Use a voice that matches the language of the input text.
        ///
        ///
        /// The method accepts a maximum of 5 KB of input text in the body of the request, and 8 KB for the URL and
        /// headers. The 5 KB limit includes any SSML tags that you specify. The service returns the synthesized audio
        /// stream as an array of bytes.
        ///
        /// **See also:** [The HTTP
        /// interface](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-usingHTTP#usingHTTP).
        ///
        /// ### Audio formats (accept types)
        ///
        ///  The service can return audio in the following formats (MIME types).
        /// * Where indicated, you can optionally specify the sampling rate (`rate`) of the audio. You must specify a
        /// sampling rate for the `audio/l16` and `audio/mulaw` formats. A specified sampling rate must lie in the range
        /// of 8 kHz to 192 kHz.
        /// * For the `audio/l16` format, you can optionally specify the endianness (`endianness`) of the audio:
        /// `endianness=big-endian` or `endianness=little-endian`.
        ///
        /// Use the `Accept` header or the `accept` parameter to specify the requested format of the response audio. If
        /// you omit an audio format altogether, the service returns the audio in Ogg format with the Opus codec
        /// (`audio/ogg;codecs=opus`). The service always returns single-channel audio.
        /// * `audio/basic`
        ///
        ///   The service returns audio with a sampling rate of 8000 Hz.
        /// * `audio/flac`
        ///
        ///   You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/l16`
        ///
        ///   You must specify the `rate` of the audio. You can optionally specify the `endianness` of the audio. The
        /// default endianness is `little-endian`.
        /// * `audio/mp3`
        ///
        ///   You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/mpeg`
        ///
        ///   You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/mulaw`
        ///
        ///   You must specify the `rate` of the audio.
        /// * `audio/ogg`
        ///
        ///   The service returns the audio in the `vorbis` codec. You can optionally specify the `rate` of the audio.
        /// The default sampling rate is 22,050 Hz.
        /// * `audio/ogg;codecs=opus`
        ///
        ///   You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/ogg;codecs=vorbis`
        ///
        ///   You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/wav`
        ///
        ///   You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/webm`
        ///
        ///   The service returns the audio in the `opus` codec. The service returns audio with a sampling rate of
        /// 48,000 Hz.
        /// * `audio/webm;codecs=opus`
        ///
        ///   The service returns audio with a sampling rate of 48,000 Hz.
        /// * `audio/webm;codecs=vorbis`
        ///
        ///   You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        ///
        /// For more information about specifying an audio format, including additional details about some of the
        /// formats, see [Audio
        /// formats](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-audioFormats#audioFormats).
        ///
        ///
        /// ### Warning messages
        ///
        ///  If a request includes invalid query parameters, the service returns a `Warnings` response header that
        /// provides messages about the invalid parameters. The warning includes a descriptive message and a list of
        /// invalid argument strings. For example, a message such as `"Unknown arguments:"` or `"Unknown url query
        /// arguments:"` followed by a list of the form `"{invalid_arg_1}, {invalid_arg_2}."` The request succeeds
        /// despite the warnings.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="accept">The requested format (MIME type) of the audio. You can use the `Accept` header or the
        /// `accept` parameter to specify the audio format. For more information about specifying an audio format, see
        /// **Audio formats (accept types)** in the method description.
        ///
        /// Default: `audio/ogg;codecs=opus`. (optional)</param>
        /// <param name="voice">The voice to use for synthesis. (optional, default to en-US_MichaelVoice)</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom voice model to use for the synthesis.
        /// If a custom voice model is specified, it is guaranteed to work only if it matches the language of the
        /// indicated voice. You must make the request with service credentials created for the instance of the service
        /// that owns the custom model. Omit the parameter to use the specified voice with no customization.
        /// (optional)</param>
        /// <returns><see cref="byte[]" />byte[]</returns>
        public DetailedResponse<System.IO.MemoryStream> Synthesize(string text, string accept = null, string voice = null, string customizationId = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `Synthesize`");
            }
            DetailedResponse<System.IO.MemoryStream> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/synthesize");


                if (!string.IsNullOrEmpty(accept))
                {
                    restRequest.WithHeader("Accept", accept);
                }
                if (!string.IsNullOrEmpty(voice))
                {
                    restRequest.WithArgument("voice", voice);
                }
                if (!string.IsNullOrEmpty(customizationId))
                {
                    restRequest.WithArgument("customization_id", customizationId);
                }
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(text))
                {
                    bodyObject["text"] = text;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "Synthesize"));
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
        /// Get pronunciation.
        ///
        /// Gets the phonetic pronunciation for the specified word. You can request the pronunciation for a specific
        /// format. You can also request the pronunciation for a specific voice to see the default translation for the
        /// language of that voice or for a specific custom voice model to see the translation for that voice model.
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:** [Querying a word from a
        /// language](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuWordsQueryLanguage).
        /// </summary>
        /// <param name="text">The word for which the pronunciation is requested.</param>
        /// <param name="voice">A voice that specifies the language in which the pronunciation is to be returned. All
        /// voices for the same language (for example, `en-US`) return the same translation. (optional, default to
        /// en-US_MichaelVoice)</param>
        /// <param name="format">The phoneme format in which to return the pronunciation. Omit the parameter to obtain
        /// the pronunciation in the default format. (optional, default to ipa)</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom voice model for which the
        /// pronunciation is to be returned. The language of a specified custom model must match the language of the
        /// specified voice. If the word is not defined in the specified custom model, the service returns the default
        /// translation for the custom model's language. You must make the request with service credentials created for
        /// the instance of the service that owns the custom model. Omit the parameter to see the translation for the
        /// specified voice with no customization. (optional)</param>
        /// <returns><see cref="Pronunciation" />Pronunciation</returns>
        public DetailedResponse<Pronunciation> GetPronunciation(string text, string voice = null, string format = null, string customizationId = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("`text` is required for `GetPronunciation`");
            }
            DetailedResponse<Pronunciation> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/pronunciation");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(text))
                {
                    restRequest.WithArgument("text", text);
                }
                if (!string.IsNullOrEmpty(voice))
                {
                    restRequest.WithArgument("voice", voice);
                }
                if (!string.IsNullOrEmpty(format))
                {
                    restRequest.WithArgument("format", format);
                }
                if (!string.IsNullOrEmpty(customizationId))
                {
                    restRequest.WithArgument("customization_id", customizationId);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "GetPronunciation"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Pronunciation>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Pronunciation>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Create a custom model.
        ///
        /// Creates a new empty custom voice model. You must specify a name for the new custom model. You can optionally
        /// specify the language and a description for the new model. The model is owned by the instance of the service
        /// whose credentials are used to create it.
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:** [Creating a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customModels#cuModelsCreate).
        /// </summary>
        /// <param name="createVoiceModel">A `CreateVoiceModel` object that contains information about the new custom
        /// voice model.</param>
        /// <returns><see cref="VoiceModel" />VoiceModel</returns>
        public DetailedResponse<VoiceModel> CreateVoiceModel(string name, string language = null, string description = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateVoiceModel`");
            }
            DetailedResponse<VoiceModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(language))
                {
                    bodyObject["language"] = language;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "CreateVoiceModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<VoiceModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<VoiceModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List custom models.
        ///
        /// Lists metadata such as the name and description for all custom voice models that are owned by an instance of
        /// the service. Specify a language to list the voice models for that language only. To see the words in
        /// addition to the metadata for a specific voice model, use the **List a custom model** method. You must use
        /// credentials for the instance of the service that owns a model to list information about it.
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:** [Querying all custom
        /// models](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customModels#cuModelsQueryAll).
        /// </summary>
        /// <param name="language">The language for which custom voice models that are owned by the requesting service
        /// credentials are to be returned. Omit the parameter to see all custom voice models that are owned by the
        /// requester. (optional)</param>
        /// <returns><see cref="VoiceModels" />VoiceModels</returns>
        public DetailedResponse<VoiceModels> ListVoiceModels(string language = null)
        {
            DetailedResponse<VoiceModels> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(language))
                {
                    restRequest.WithArgument("language", language);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "ListVoiceModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<VoiceModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<VoiceModels>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Update a custom model.
        ///
        /// Updates information for the specified custom voice model. You can update metadata such as the name and
        /// description of the voice model. You can also update the words in the model and their translations. Adding a
        /// new translation for a word that already exists in a custom model overwrites the word's existing translation.
        /// A custom model can contain no more than 20,000 entries. You must use credentials for the instance of the
        /// service that owns a model to update it.
        ///
        /// You can define sounds-like or phonetic translations for words. A sounds-like translation consists of one or
        /// more words that, when combined, sound like the word. Phonetic translations are based on the SSML phoneme
        /// format for representing a word. You can specify them in standard International Phonetic Alphabet (IPA)
        /// representation
        ///
        ///   <code>&lt;phoneme alphabet="ipa" ph="t&#601;m&#712;&#593;to"&gt;&lt;/phoneme&gt;</code>
        ///
        ///   or in the proprietary IBM Symbolic Phonetic Representation (SPR)
        ///
        ///   <code>&lt;phoneme alphabet="ibm" ph="1gAstroEntxrYFXs"&gt;&lt;/phoneme&gt;</code>
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:**
        /// * [Updating a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customModels#cuModelsUpdate)
        /// * [Adding words to a Japanese custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuJapaneseAdd)
        /// * [Understanding
        /// customization](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customIntro#customIntro).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="updateVoiceModel">An `UpdateVoiceModel` object that contains information that is to be updated
        /// for the custom voice model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> UpdateVoiceModel(string customizationId, string name = null, string description = null, List<Word> words = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `UpdateVoiceModel`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                if (words != null && words.Count > 0)
                {
                    bodyObject["words"] = JToken.FromObject(words);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "UpdateVoiceModel"));
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
        /// Get a custom model.
        ///
        /// Gets all information about a specified custom voice model. In addition to metadata such as the name and
        /// description of the voice model, the output includes the words and their translations as defined in the
        /// model. To see just the metadata for a voice model, use the **List custom models** method.
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:** [Querying a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customModels#cuModelsQuery).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="VoiceModel" />VoiceModel</returns>
        public DetailedResponse<VoiceModel> GetVoiceModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetVoiceModel`");
            }
            DetailedResponse<VoiceModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "GetVoiceModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<VoiceModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<VoiceModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a custom model.
        ///
        /// Deletes the specified custom voice model. You must use credentials for the instance of the service that owns
        /// a model to delete it.
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:** [Deleting a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customModels#cuModelsDelete).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteVoiceModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteVoiceModel`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}");


                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "DeleteVoiceModel"));
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
        /// Add custom words.
        ///
        /// Adds one or more words and their translations to the specified custom voice model. Adding a new translation
        /// for a word that already exists in a custom model overwrites the word's existing translation. A custom model
        /// can contain no more than 20,000 entries. You must use credentials for the instance of the service that owns
        /// a model to add words to it.
        ///
        /// You can define sounds-like or phonetic translations for words. A sounds-like translation consists of one or
        /// more words that, when combined, sound like the word. Phonetic translations are based on the SSML phoneme
        /// format for representing a word. You can specify them in standard International Phonetic Alphabet (IPA)
        /// representation
        ///
        ///   <code>&lt;phoneme alphabet="ipa" ph="t&#601;m&#712;&#593;to"&gt;&lt;/phoneme&gt;</code>
        ///
        ///   or in the proprietary IBM Symbolic Phonetic Representation (SPR)
        ///
        ///   <code>&lt;phoneme alphabet="ibm" ph="1gAstroEntxrYFXs"&gt;&lt;/phoneme&gt;</code>
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:**
        /// * [Adding multiple words to a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuWordsAdd)
        /// * [Adding words to a Japanese custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuJapaneseAdd)
        /// * [Understanding
        /// customization](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customIntro#customIntro).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customWords"></param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddWords(string customizationId, List<Word> words)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddWords`");
            }
            if (words == null)
            {
                throw new ArgumentNullException("`words` is required for `AddWords`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (words != null && words.Count > 0)
                {
                    bodyObject["words"] = JToken.FromObject(words);
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "AddWords"));
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
        /// List custom words.
        ///
        /// Lists all of the words and their translations for the specified custom voice model. The output shows the
        /// translations as they are defined in the model. You must use credentials for the instance of the service that
        /// owns a model to list its words.
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:** [Querying all words from a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuWordsQueryModel).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="Words" />Words</returns>
        public DetailedResponse<Words> ListWords(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `ListWords`");
            }
            DetailedResponse<Words> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "ListWords"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Words>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Words>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Add a custom word.
        ///
        /// Adds a single word and its translation to the specified custom voice model. Adding a new translation for a
        /// word that already exists in a custom model overwrites the word's existing translation. A custom model can
        /// contain no more than 20,000 entries. You must use credentials for the instance of the service that owns a
        /// model to add a word to it.
        ///
        /// You can define sounds-like or phonetic translations for words. A sounds-like translation consists of one or
        /// more words that, when combined, sound like the word. Phonetic translations are based on the SSML phoneme
        /// format for representing a word. You can specify them in standard International Phonetic Alphabet (IPA)
        /// representation
        ///
        ///   <code>&lt;phoneme alphabet="ipa" ph="t&#601;m&#712;&#593;to"&gt;&lt;/phoneme&gt;</code>
        ///
        ///   or in the proprietary IBM Symbolic Phonetic Representation (SPR)
        ///
        ///   <code>&lt;phoneme alphabet="ibm" ph="1gAstroEntxrYFXs"&gt;&lt;/phoneme&gt;</code>
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:**
        /// * [Adding a single word to a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuWordAdd)
        /// * [Adding words to a Japanese custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuJapaneseAdd)
        /// * [Understanding
        /// customization](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customIntro#customIntro).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be added or updated for the custom voice model.</param>
        /// <param name="translation">The translation for the word that is to be added or updated.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddWord(string customizationId, string word, string translation, string partOfSpeech = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddWord`");
            }
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("`word` is required for `AddWord`");
            }
            if (string.IsNullOrEmpty(translation))
            {
                throw new ArgumentNullException("`translation` is required for `AddWord`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{word}");

                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(translation))
                {
                    bodyObject["translation"] = translation;
                }
                if (!string.IsNullOrEmpty(partOfSpeech))
                {
                    bodyObject["part_of_speech"] = partOfSpeech;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "AddWord"));
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
        /// Get a custom word.
        ///
        /// Gets the translation for a single word from the specified custom model. The output shows the translation as
        /// it is defined in the model. You must use credentials for the instance of the service that owns a model to
        /// list its words.
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:** [Querying a single word from a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuWordQueryModel).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be queried from the custom voice model.</param>
        /// <returns><see cref="Translation" />Translation</returns>
        public DetailedResponse<Translation> GetWord(string customizationId, string word)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetWord`");
            }
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("`word` is required for `GetWord`");
            }
            DetailedResponse<Translation> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{word}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "GetWord"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Translation>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Translation>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a custom word.
        ///
        /// Deletes a single word from the specified custom voice model. You must use credentials for the instance of
        /// the service that owns a model to delete its words.
        ///
        /// **Note:** This method is currently a beta release.
        ///
        /// **See also:** [Deleting a word from a custom
        /// model](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-customWords#cuWordDelete).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom voice model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be deleted from the custom voice model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteWord(string customizationId, string word)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteWord`");
            }
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("`word` is required for `DeleteWord`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{word}");


                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "DeleteWord"));
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
        /// Delete labeled data.
        ///
        /// Deletes all data that is associated with a specified customer ID. The method deletes all data for the
        /// customer ID, regardless of the method by which the information was added. The method has no effect if no
        /// data is associated with the customer ID. You must issue the request with credentials for the same instance
        /// of the service that was used to associate the customer ID with the data.
        ///
        /// You associate a customer ID with data by passing the `X-Watson-Metadata` header with a request that passes
        /// the data.
        ///
        /// **See also:** [Information
        /// security](https://cloud.ibm.com/docs/services/text-to-speech?topic=text-to-speech-information-security#information-security).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteUserData(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentNullException("`customerId` is required for `DeleteUserData`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/user_data");

                if (!string.IsNullOrEmpty(customerId))
                {
                    restRequest.WithArgument("customer_id", customerId);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "DeleteUserData"));
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
    }
}
