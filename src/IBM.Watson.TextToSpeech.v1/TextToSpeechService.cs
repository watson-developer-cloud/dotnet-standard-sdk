/**
* (C) Copyright IBM Corp. 2023.
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

/**
* IBM OpenAPI SDK Code Generator Version: 3.64.1-cee95189-20230124-211647
*/
 
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Extensions;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.TextToSpeech.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.TextToSpeech.v1
{
    public partial class TextToSpeechService : IBMService, ITextToSpeechService
    {
        const string defaultServiceName = "text_to_speech";
        private const string defaultServiceUrl = "https://api.us-south.text-to-speech.watson.cloud.ibm.com";

        public TextToSpeechService() : this(defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public TextToSpeechService(IAuthenticator authenticator) : this(defaultServiceName, authenticator) {}
        public TextToSpeechService(string serviceName) : this(serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public TextToSpeechService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public TextToSpeechService(string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
        {

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
        }

        /// <summary>
        /// List voices.
        ///
        /// Lists all voices available for use with the service. The information includes the name, language, gender,
        /// and other details about the voice. The ordering of the list of voices can change from call to call; do not
        /// rely on an alphabetized or static list of voices. To see information about a specific voice, use the [Get a
        /// voice](#getvoice).
        ///
        /// **Note:** Effective **31 March 2022**, all *neural voices* are deprecated. The deprecated voices remain
        /// available to existing users until 31 March 2023, when they will be removed from the service and the
        /// documentation. *No enhanced neural voices or expressive neural voices are deprecated.* For more information,
        /// see the [1 March 2023 service
        /// update](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-release-notes#text-to-speech-1march2023)
        /// in the release notes.
        ///
        /// **See also:** [Listing all
        /// voices](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-voices-list#list-all-voices).
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
        /// details about the voice. Specify a customization ID to obtain information for a custom model that is defined
        /// for the language of the specified voice. To list information about all available voices, use the [List
        /// voices](#listvoices) method.
        ///
        /// **See also:** [Listing a specific
        /// voice](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-voices-list#list-specific-voice).
        ///
        /// **Note:** Effective **31 March 2022**, all *neural voices* are deprecated. The deprecated voices remain
        /// available to existing users until 31 March 2023, when they will be removed from the service and the
        /// documentation. *No enhanced neural voices or expressive neural voices are deprecated.* For more information,
        /// see the [1 March 2023 service
        /// update](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-release-notes#text-to-speech-1march2023)
        /// in the release notes.
        /// </summary>
        /// <param name="voice">The voice for which information is to be returned.</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom model for which information is to be
        /// returned. You must make the request with credentials for the instance of the service that owns the custom
        /// model. Omit the parameter to see information about the specified voice with no customization.
        /// (optional)</param>
        /// <returns><see cref="Voice" />Voice</returns>
        public DetailedResponse<Voice> GetVoice(string voice, string customizationId = null)
        {
            if (string.IsNullOrEmpty(voice))
            {
                throw new ArgumentNullException("`voice` is required for `GetVoice`");
            }
            else
            {
                voice = Uri.EscapeDataString(voice);
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
        /// Enum values for GetVoice.
        /// </summary>
        public class GetVoiceEnums
        {
            /// <summary>
            /// The voice for which information is to be returned.
            /// </summary>
            public class VoiceValue
            {
                /// <summary>
                /// Constant AR_MS_OMARVOICE for ar-MS_OmarVoice
                /// </summary>
                public const string AR_MS_OMARVOICE = "ar-MS_OmarVoice";
                /// <summary>
                /// Constant CS_CZ_ALENAVOICE for cs-CZ_AlenaVoice
                /// </summary>
                public const string CS_CZ_ALENAVOICE = "cs-CZ_AlenaVoice";
                /// <summary>
                /// Constant DE_DE_BIRGITV3VOICE for de-DE_BirgitV3Voice
                /// </summary>
                public const string DE_DE_BIRGITV3VOICE = "de-DE_BirgitV3Voice";
                /// <summary>
                /// Constant DE_DE_DIETERV3VOICE for de-DE_DieterV3Voice
                /// </summary>
                public const string DE_DE_DIETERV3VOICE = "de-DE_DieterV3Voice";
                /// <summary>
                /// Constant DE_DE_ERIKAV3VOICE for de-DE_ErikaV3Voice
                /// </summary>
                public const string DE_DE_ERIKAV3VOICE = "de-DE_ErikaV3Voice";
                /// <summary>
                /// Constant EN_AU_CRAIGVOICE for en-AU_CraigVoice
                /// </summary>
                public const string EN_AU_CRAIGVOICE = "en-AU_CraigVoice";
                /// <summary>
                /// Constant EN_AU_HEIDIEXPRESSIVE for en-AU_HeidiExpressive
                /// </summary>
                public const string EN_AU_HEIDIEXPRESSIVE = "en-AU_HeidiExpressive";
                /// <summary>
                /// Constant EN_AU_JACKEXPRESSIVE for en-AU_JackExpressive
                /// </summary>
                public const string EN_AU_JACKEXPRESSIVE = "en-AU_JackExpressive";
                /// <summary>
                /// Constant EN_AU_MADISONVOICE for en-AU_MadisonVoice
                /// </summary>
                public const string EN_AU_MADISONVOICE = "en-AU_MadisonVoice";
                /// <summary>
                /// Constant EN_AU_STEVEVOICE for en-AU_SteveVoice
                /// </summary>
                public const string EN_AU_STEVEVOICE = "en-AU_SteveVoice";
                /// <summary>
                /// Constant EN_GB_CHARLOTTEV3VOICE for en-GB_CharlotteV3Voice
                /// </summary>
                public const string EN_GB_CHARLOTTEV3VOICE = "en-GB_CharlotteV3Voice";
                /// <summary>
                /// Constant EN_GB_JAMESV3VOICE for en-GB_JamesV3Voice
                /// </summary>
                public const string EN_GB_JAMESV3VOICE = "en-GB_JamesV3Voice";
                /// <summary>
                /// Constant EN_GB_KATEV3VOICE for en-GB_KateV3Voice
                /// </summary>
                public const string EN_GB_KATEV3VOICE = "en-GB_KateV3Voice";
                /// <summary>
                /// Constant EN_US_ALLISONEXPRESSIVE for en-US_AllisonExpressive
                /// </summary>
                public const string EN_US_ALLISONEXPRESSIVE = "en-US_AllisonExpressive";
                /// <summary>
                /// Constant EN_US_ALLISONV3VOICE for en-US_AllisonV3Voice
                /// </summary>
                public const string EN_US_ALLISONV3VOICE = "en-US_AllisonV3Voice";
                /// <summary>
                /// Constant EN_US_EMILYV3VOICE for en-US_EmilyV3Voice
                /// </summary>
                public const string EN_US_EMILYV3VOICE = "en-US_EmilyV3Voice";
                /// <summary>
                /// Constant EN_US_EMMAEXPRESSIVE for en-US_EmmaExpressive
                /// </summary>
                public const string EN_US_EMMAEXPRESSIVE = "en-US_EmmaExpressive";
                /// <summary>
                /// Constant EN_US_HENRYV3VOICE for en-US_HenryV3Voice
                /// </summary>
                public const string EN_US_HENRYV3VOICE = "en-US_HenryV3Voice";
                /// <summary>
                /// Constant EN_US_KEVINV3VOICE for en-US_KevinV3Voice
                /// </summary>
                public const string EN_US_KEVINV3VOICE = "en-US_KevinV3Voice";
                /// <summary>
                /// Constant EN_US_LISAEXPRESSIVE for en-US_LisaExpressive
                /// </summary>
                public const string EN_US_LISAEXPRESSIVE = "en-US_LisaExpressive";
                /// <summary>
                /// Constant EN_US_LISAV3VOICE for en-US_LisaV3Voice
                /// </summary>
                public const string EN_US_LISAV3VOICE = "en-US_LisaV3Voice";
                /// <summary>
                /// Constant EN_US_MICHAELEXPRESSIVE for en-US_MichaelExpressive
                /// </summary>
                public const string EN_US_MICHAELEXPRESSIVE = "en-US_MichaelExpressive";
                /// <summary>
                /// Constant EN_US_MICHAELV3VOICE for en-US_MichaelV3Voice
                /// </summary>
                public const string EN_US_MICHAELV3VOICE = "en-US_MichaelV3Voice";
                /// <summary>
                /// Constant EN_US_OLIVIAV3VOICE for en-US_OliviaV3Voice
                /// </summary>
                public const string EN_US_OLIVIAV3VOICE = "en-US_OliviaV3Voice";
                /// <summary>
                /// Constant ES_ES_ENRIQUEV3VOICE for es-ES_EnriqueV3Voice
                /// </summary>
                public const string ES_ES_ENRIQUEV3VOICE = "es-ES_EnriqueV3Voice";
                /// <summary>
                /// Constant ES_ES_LAURAV3VOICE for es-ES_LauraV3Voice
                /// </summary>
                public const string ES_ES_LAURAV3VOICE = "es-ES_LauraV3Voice";
                /// <summary>
                /// Constant ES_LA_SOFIAV3VOICE for es-LA_SofiaV3Voice
                /// </summary>
                public const string ES_LA_SOFIAV3VOICE = "es-LA_SofiaV3Voice";
                /// <summary>
                /// Constant ES_US_SOFIAV3VOICE for es-US_SofiaV3Voice
                /// </summary>
                public const string ES_US_SOFIAV3VOICE = "es-US_SofiaV3Voice";
                /// <summary>
                /// Constant FR_CA_LOUISEV3VOICE for fr-CA_LouiseV3Voice
                /// </summary>
                public const string FR_CA_LOUISEV3VOICE = "fr-CA_LouiseV3Voice";
                /// <summary>
                /// Constant FR_FR_NICOLASV3VOICE for fr-FR_NicolasV3Voice
                /// </summary>
                public const string FR_FR_NICOLASV3VOICE = "fr-FR_NicolasV3Voice";
                /// <summary>
                /// Constant FR_FR_RENEEV3VOICE for fr-FR_ReneeV3Voice
                /// </summary>
                public const string FR_FR_RENEEV3VOICE = "fr-FR_ReneeV3Voice";
                /// <summary>
                /// Constant IT_IT_FRANCESCAV3VOICE for it-IT_FrancescaV3Voice
                /// </summary>
                public const string IT_IT_FRANCESCAV3VOICE = "it-IT_FrancescaV3Voice";
                /// <summary>
                /// Constant JA_JP_EMIV3VOICE for ja-JP_EmiV3Voice
                /// </summary>
                public const string JA_JP_EMIV3VOICE = "ja-JP_EmiV3Voice";
                /// <summary>
                /// Constant KO_KR_HYUNJUNVOICE for ko-KR_HyunjunVoice
                /// </summary>
                public const string KO_KR_HYUNJUNVOICE = "ko-KR_HyunjunVoice";
                /// <summary>
                /// Constant KO_KR_JINV3VOICE for ko-KR_JinV3Voice
                /// </summary>
                public const string KO_KR_JINV3VOICE = "ko-KR_JinV3Voice";
                /// <summary>
                /// Constant KO_KR_SIWOOVOICE for ko-KR_SiWooVoice
                /// </summary>
                public const string KO_KR_SIWOOVOICE = "ko-KR_SiWooVoice";
                /// <summary>
                /// Constant KO_KR_YOUNGMIVOICE for ko-KR_YoungmiVoice
                /// </summary>
                public const string KO_KR_YOUNGMIVOICE = "ko-KR_YoungmiVoice";
                /// <summary>
                /// Constant KO_KR_YUNAVOICE for ko-KR_YunaVoice
                /// </summary>
                public const string KO_KR_YUNAVOICE = "ko-KR_YunaVoice";
                /// <summary>
                /// Constant NL_BE_ADELEVOICE for nl-BE_AdeleVoice
                /// </summary>
                public const string NL_BE_ADELEVOICE = "nl-BE_AdeleVoice";
                /// <summary>
                /// Constant NL_BE_BRAMVOICE for nl-BE_BramVoice
                /// </summary>
                public const string NL_BE_BRAMVOICE = "nl-BE_BramVoice";
                /// <summary>
                /// Constant NL_NL_EMMAVOICE for nl-NL_EmmaVoice
                /// </summary>
                public const string NL_NL_EMMAVOICE = "nl-NL_EmmaVoice";
                /// <summary>
                /// Constant NL_NL_LIAMVOICE for nl-NL_LiamVoice
                /// </summary>
                public const string NL_NL_LIAMVOICE = "nl-NL_LiamVoice";
                /// <summary>
                /// Constant PT_BR_ISABELAV3VOICE for pt-BR_IsabelaV3Voice
                /// </summary>
                public const string PT_BR_ISABELAV3VOICE = "pt-BR_IsabelaV3Voice";
                /// <summary>
                /// Constant SV_SE_INGRIDVOICE for sv-SE_IngridVoice
                /// </summary>
                public const string SV_SE_INGRIDVOICE = "sv-SE_IngridVoice";
                /// <summary>
                /// Constant ZH_CN_LINAVOICE for zh-CN_LiNaVoice
                /// </summary>
                public const string ZH_CN_LINAVOICE = "zh-CN_LiNaVoice";
                /// <summary>
                /// Constant ZH_CN_WANGWEIVOICE for zh-CN_WangWeiVoice
                /// </summary>
                public const string ZH_CN_WANGWEIVOICE = "zh-CN_WangWeiVoice";
                /// <summary>
                /// Constant ZH_CN_ZHANGJINGVOICE for zh-CN_ZhangJingVoice
                /// </summary>
                public const string ZH_CN_ZHANGJINGVOICE = "zh-CN_ZhangJingVoice";
                
            }
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
        /// interface](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-usingHTTP#usingHTTP).
        ///
        /// **Note:** Effective **31 March 2022**, all *neural voices* are deprecated. The deprecated voices remain
        /// available to existing users until 31 March 2023, when they will be removed from the service and the
        /// documentation. *No enhanced neural voices or expressive neural voices are deprecated.* For more information,
        /// see the [1 March 2023 service
        /// update](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-release-notes#text-to-speech-1march2023)
        /// in the release notes.
        ///
        /// ### Audio formats (accept types)
        ///
        ///  The service can return audio in the following formats (MIME types).
        /// * Where indicated, you can optionally specify the sampling rate (`rate`) of the audio. You must specify a
        /// sampling rate for the `audio/alaw`, `audio/l16`,  and `audio/mulaw` formats. A specified sampling rate must
        /// lie in the range of 8 kHz to 192 kHz. Some formats restrict the sampling rate to certain values, as noted.
        /// * For the `audio/l16` format, you can optionally specify the endianness (`endianness`) of the audio:
        /// `endianness=big-endian` or `endianness=little-endian`.
        ///
        /// Use the `Accept` header or the `accept` parameter to specify the requested format of the response audio. If
        /// you omit an audio format altogether, the service returns the audio in Ogg format with the Opus codec
        /// (`audio/ogg;codecs=opus`). The service always returns single-channel audio.
        /// * `audio/alaw` - You must specify the `rate` of the audio.
        /// * `audio/basic` - The service returns audio with a sampling rate of 8000 Hz.
        /// * `audio/flac` - You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/l16` - You must specify the `rate` of the audio. You can optionally specify the `endianness` of the
        /// audio. The default endianness is `little-endian`.
        /// * `audio/mp3` - You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/mpeg` - You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/mulaw` - You must specify the `rate` of the audio.
        /// * `audio/ogg` - The service returns the audio in the `vorbis` codec. You can optionally specify the `rate`
        /// of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/ogg;codecs=opus` - You can optionally specify the `rate` of the audio. Only the following values
        /// are valid sampling rates: `48000`, `24000`, `16000`, `12000`, or `8000`. If you specify a value other than
        /// one of these, the service returns an error. The default sampling rate is 48,000 Hz.
        /// * `audio/ogg;codecs=vorbis` - You can optionally specify the `rate` of the audio. The default sampling rate
        /// is 22,050 Hz.
        /// * `audio/wav` - You can optionally specify the `rate` of the audio. The default sampling rate is 22,050 Hz.
        /// * `audio/webm` - The service returns the audio in the `opus` codec. The service returns audio with a
        /// sampling rate of 48,000 Hz.
        /// * `audio/webm;codecs=opus` - The service returns audio with a sampling rate of 48,000 Hz.
        /// * `audio/webm;codecs=vorbis` - You can optionally specify the `rate` of the audio. The default sampling rate
        /// is 22,050 Hz.
        ///
        /// For more information about specifying an audio format, including additional details about some of the
        /// formats, see [Using audio
        /// formats](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-audio-formats).
        ///
        /// **Note:** By default, the service returns audio in the Ogg audio format with the Opus codec
        /// (`audio/ogg;codecs=opus`). However, the Ogg audio format is not supported with the Safari browser. If you
        /// are using the service with the Safari browser, you must use the `Accept` request header or the `accept`
        /// query parameter specify a different format in which you want the service to return the audio.
        ///
        /// ### Warning messages
        ///
        ///  If a request includes invalid query parameters, the service returns a `Warnings` response header that
        /// provides messages about the invalid parameters. The warning includes a descriptive message and a list of
        /// invalid argument strings. For example, a message such as `"Unknown arguments:"` or `"Unknown url query
        /// arguments:"` followed by a list of the form `"{invalid_arg_1}, {invalid_arg_2}."` The request succeeds
        /// despite the warnings.
        /// </summary>
        /// <param name="text">The text to synthesize.</param>
        /// <param name="accept">The requested format (MIME type) of the audio. You can use the `Accept` header or the
        /// `accept` parameter to specify the audio format. For more information about specifying an audio format, see
        /// **Audio formats (accept types)** in the method description. (optional, default to
        /// audio/ogg;codecs=opus)</param>
        /// <param name="voice">The voice to use for speech synthesis. If you omit the `voice` parameter, the service
        /// uses the US English `en-US_MichaelV3Voice` by default.
        ///
        /// _For IBM Cloud Pak for Data,_ if you do not install the `en-US_MichaelV3Voice`, you must either specify a
        /// voice with the request or specify a new default voice for your installation of the service.
        ///
        /// **See also:**
        /// * [Languages and voices](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-voices)
        /// * [Using the default
        /// voice](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-voices-use#specify-voice-default).
        /// (optional, default to en-US_MichaelV3Voice)</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom model to use for the synthesis. If a
        /// custom model is specified, it works only if it matches the language of the indicated voice. You must make
        /// the request with credentials for the instance of the service that owns the custom model. Omit the parameter
        /// to use the specified voice with no customization. (optional)</param>
        /// <param name="spellOutMode">*For German voices,* indicates how the service is to spell out strings of
        /// individual letters. To indicate the pace of the spelling, specify one of the following values:
        /// * `default` - The service reads the characters at the rate at which it synthesizes speech for the request.
        /// You can also omit the parameter entirely to achieve the default behavior.
        /// * `singles` - The service reads the characters one at a time, with a brief pause between each character.
        /// * `pairs` - The service reads the characters two at a time, with a brief pause between each pair.
        /// * `triples` - The service reads the characters three at a time, with a brief pause between each triplet.
        ///
        /// For more information, see [Specifying how strings are spelled
        /// out](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-synthesis-params#params-spell-out-mode).
        /// (optional, default to default)</param>
        /// <param name="ratePercentage">The percentage change from the default speaking rate of the voice that is used
        /// for speech synthesis. Each voice has a default speaking rate that is optimized to represent a normal rate of
        /// speech. The parameter accepts an integer that represents the percentage change from the voice's default
        /// rate:
        /// * Specify a signed negative integer to reduce the speaking rate by that percentage. For example, -10 reduces
        /// the rate by ten percent.
        /// * Specify an unsigned or signed positive integer to increase the speaking rate by that percentage. For
        /// example, 10 and +10 increase the rate by ten percent.
        /// * Specify 0 or omit the parameter to get the default speaking rate for the voice.
        ///
        /// The parameter affects the rate for an entire request.
        ///
        /// For more information, see [Modifying the speaking
        /// rate](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-synthesis-params#params-rate-percentage).
        /// (optional)</param>
        /// <param name="pitchPercentage">The percentage change from the default speaking pitch of the voice that is
        /// used for speech synthesis. Each voice has a default speaking pitch that is optimized to represent a normal
        /// tone of voice. The parameter accepts an integer that represents the percentage change from the voice's
        /// default tone:
        /// * Specify a signed negative integer to lower the voice's pitch by that percentage. For example, -5 reduces
        /// the tone by five percent.
        /// * Specify an unsigned or signed positive integer to increase the voice's pitch by that percentage. For
        /// example, 5 and +5 increase the tone by five percent.
        /// * Specify 0 or omit the parameter to get the default speaking pitch for the voice.
        ///
        /// The parameter affects the pitch for an entire request.
        ///
        /// For more information, see [Modifying the speaking
        /// pitch](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-synthesis-params#params-pitch-percentage).
        /// (optional)</param>
        /// <returns><see cref="byte[]" />byte[]</returns>
        public DetailedResponse<System.IO.MemoryStream> Synthesize(string text, string accept = null, string voice = null, string customizationId = null, string spellOutMode = null, long? ratePercentage = null, long? pitchPercentage = null)
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
                if (!string.IsNullOrEmpty(spellOutMode))
                {
                    restRequest.WithArgument("spell_out_mode", spellOutMode);
                }
                if (ratePercentage != null)
                {
                    restRequest.WithArgument("rate_percentage", ratePercentage);
                }
                if (pitchPercentage != null)
                {
                    restRequest.WithArgument("pitch_percentage", pitchPercentage);
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
        /// Enum values for Synthesize.
        /// </summary>
        public class SynthesizeEnums
        {
            /// <summary>
            /// The requested format (MIME type) of the audio. You can use the `Accept` header or the `accept` parameter
            /// to specify the audio format. For more information about specifying an audio format, see **Audio formats
            /// (accept types)** in the method description.
            /// </summary>
            public class AcceptValue
            {
                /// <summary>
                /// Constant AUDIO_ALAW for audio/alaw
                /// </summary>
                public const string AUDIO_ALAW = "audio/alaw";
                /// <summary>
                /// Constant AUDIO_BASIC for audio/basic
                /// </summary>
                public const string AUDIO_BASIC = "audio/basic";
                /// <summary>
                /// Constant AUDIO_FLAC for audio/flac
                /// </summary>
                public const string AUDIO_FLAC = "audio/flac";
                /// <summary>
                /// Constant AUDIO_L16 for audio/l16
                /// </summary>
                public const string AUDIO_L16 = "audio/l16";
                /// <summary>
                /// Constant AUDIO_OGG for audio/ogg
                /// </summary>
                public const string AUDIO_OGG = "audio/ogg";
                /// <summary>
                /// Constant AUDIO_OGG_CODECS_OPUS for audio/ogg;codecs=opus
                /// </summary>
                public const string AUDIO_OGG_CODECS_OPUS = "audio/ogg;codecs=opus";
                /// <summary>
                /// Constant AUDIO_OGG_CODECS_VORBIS for audio/ogg;codecs=vorbis
                /// </summary>
                public const string AUDIO_OGG_CODECS_VORBIS = "audio/ogg;codecs=vorbis";
                /// <summary>
                /// Constant AUDIO_MP3 for audio/mp3
                /// </summary>
                public const string AUDIO_MP3 = "audio/mp3";
                /// <summary>
                /// Constant AUDIO_MPEG for audio/mpeg
                /// </summary>
                public const string AUDIO_MPEG = "audio/mpeg";
                /// <summary>
                /// Constant AUDIO_MULAW for audio/mulaw
                /// </summary>
                public const string AUDIO_MULAW = "audio/mulaw";
                /// <summary>
                /// Constant AUDIO_WAV for audio/wav
                /// </summary>
                public const string AUDIO_WAV = "audio/wav";
                /// <summary>
                /// Constant AUDIO_WEBM for audio/webm
                /// </summary>
                public const string AUDIO_WEBM = "audio/webm";
                /// <summary>
                /// Constant AUDIO_WEBM_CODECS_OPUS for audio/webm;codecs=opus
                /// </summary>
                public const string AUDIO_WEBM_CODECS_OPUS = "audio/webm;codecs=opus";
                /// <summary>
                /// Constant AUDIO_WEBM_CODECS_VORBIS for audio/webm;codecs=vorbis
                /// </summary>
                public const string AUDIO_WEBM_CODECS_VORBIS = "audio/webm;codecs=vorbis";
                
            }
            /// <summary>
            /// The voice to use for speech synthesis. If you omit the `voice` parameter, the service uses the US
            /// English `en-US_MichaelV3Voice` by default.
            ///
            /// _For IBM Cloud Pak for Data,_ if you do not install the `en-US_MichaelV3Voice`, you must either specify
            /// a voice with the request or specify a new default voice for your installation of the service.
            ///
            /// **See also:**
            /// * [Languages and voices](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-voices)
            /// * [Using the default
            /// voice](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-voices-use#specify-voice-default).
            /// </summary>
            public class VoiceValue
            {
                /// <summary>
                /// Constant AR_MS_OMARVOICE for ar-MS_OmarVoice
                /// </summary>
                public const string AR_MS_OMARVOICE = "ar-MS_OmarVoice";
                /// <summary>
                /// Constant CS_CZ_ALENAVOICE for cs-CZ_AlenaVoice
                /// </summary>
                public const string CS_CZ_ALENAVOICE = "cs-CZ_AlenaVoice";
                /// <summary>
                /// Constant DE_DE_BIRGITV3VOICE for de-DE_BirgitV3Voice
                /// </summary>
                public const string DE_DE_BIRGITV3VOICE = "de-DE_BirgitV3Voice";
                /// <summary>
                /// Constant DE_DE_DIETERV3VOICE for de-DE_DieterV3Voice
                /// </summary>
                public const string DE_DE_DIETERV3VOICE = "de-DE_DieterV3Voice";
                /// <summary>
                /// Constant DE_DE_ERIKAV3VOICE for de-DE_ErikaV3Voice
                /// </summary>
                public const string DE_DE_ERIKAV3VOICE = "de-DE_ErikaV3Voice";
                /// <summary>
                /// Constant EN_AU_CRAIGVOICE for en-AU_CraigVoice
                /// </summary>
                public const string EN_AU_CRAIGVOICE = "en-AU_CraigVoice";
                /// <summary>
                /// Constant EN_AU_HEIDIEXPRESSIVE for en-AU_HeidiExpressive
                /// </summary>
                public const string EN_AU_HEIDIEXPRESSIVE = "en-AU_HeidiExpressive";
                /// <summary>
                /// Constant EN_AU_JACKEXPRESSIVE for en-AU_JackExpressive
                /// </summary>
                public const string EN_AU_JACKEXPRESSIVE = "en-AU_JackExpressive";
                /// <summary>
                /// Constant EN_AU_MADISONVOICE for en-AU_MadisonVoice
                /// </summary>
                public const string EN_AU_MADISONVOICE = "en-AU_MadisonVoice";
                /// <summary>
                /// Constant EN_AU_STEVEVOICE for en-AU_SteveVoice
                /// </summary>
                public const string EN_AU_STEVEVOICE = "en-AU_SteveVoice";
                /// <summary>
                /// Constant EN_GB_CHARLOTTEV3VOICE for en-GB_CharlotteV3Voice
                /// </summary>
                public const string EN_GB_CHARLOTTEV3VOICE = "en-GB_CharlotteV3Voice";
                /// <summary>
                /// Constant EN_GB_JAMESV3VOICE for en-GB_JamesV3Voice
                /// </summary>
                public const string EN_GB_JAMESV3VOICE = "en-GB_JamesV3Voice";
                /// <summary>
                /// Constant EN_GB_KATEV3VOICE for en-GB_KateV3Voice
                /// </summary>
                public const string EN_GB_KATEV3VOICE = "en-GB_KateV3Voice";
                /// <summary>
                /// Constant EN_US_ALLISONEXPRESSIVE for en-US_AllisonExpressive
                /// </summary>
                public const string EN_US_ALLISONEXPRESSIVE = "en-US_AllisonExpressive";
                /// <summary>
                /// Constant EN_US_ALLISONV3VOICE for en-US_AllisonV3Voice
                /// </summary>
                public const string EN_US_ALLISONV3VOICE = "en-US_AllisonV3Voice";
                /// <summary>
                /// Constant EN_US_EMILYV3VOICE for en-US_EmilyV3Voice
                /// </summary>
                public const string EN_US_EMILYV3VOICE = "en-US_EmilyV3Voice";
                /// <summary>
                /// Constant EN_US_EMMAEXPRESSIVE for en-US_EmmaExpressive
                /// </summary>
                public const string EN_US_EMMAEXPRESSIVE = "en-US_EmmaExpressive";
                /// <summary>
                /// Constant EN_US_HENRYV3VOICE for en-US_HenryV3Voice
                /// </summary>
                public const string EN_US_HENRYV3VOICE = "en-US_HenryV3Voice";
                /// <summary>
                /// Constant EN_US_KEVINV3VOICE for en-US_KevinV3Voice
                /// </summary>
                public const string EN_US_KEVINV3VOICE = "en-US_KevinV3Voice";
                /// <summary>
                /// Constant EN_US_LISAEXPRESSIVE for en-US_LisaExpressive
                /// </summary>
                public const string EN_US_LISAEXPRESSIVE = "en-US_LisaExpressive";
                /// <summary>
                /// Constant EN_US_LISAV3VOICE for en-US_LisaV3Voice
                /// </summary>
                public const string EN_US_LISAV3VOICE = "en-US_LisaV3Voice";
                /// <summary>
                /// Constant EN_US_MICHAELEXPRESSIVE for en-US_MichaelExpressive
                /// </summary>
                public const string EN_US_MICHAELEXPRESSIVE = "en-US_MichaelExpressive";
                /// <summary>
                /// Constant EN_US_MICHAELV3VOICE for en-US_MichaelV3Voice
                /// </summary>
                public const string EN_US_MICHAELV3VOICE = "en-US_MichaelV3Voice";
                /// <summary>
                /// Constant EN_US_OLIVIAV3VOICE for en-US_OliviaV3Voice
                /// </summary>
                public const string EN_US_OLIVIAV3VOICE = "en-US_OliviaV3Voice";
                /// <summary>
                /// Constant ES_ES_ENRIQUEV3VOICE for es-ES_EnriqueV3Voice
                /// </summary>
                public const string ES_ES_ENRIQUEV3VOICE = "es-ES_EnriqueV3Voice";
                /// <summary>
                /// Constant ES_ES_LAURAV3VOICE for es-ES_LauraV3Voice
                /// </summary>
                public const string ES_ES_LAURAV3VOICE = "es-ES_LauraV3Voice";
                /// <summary>
                /// Constant ES_LA_SOFIAV3VOICE for es-LA_SofiaV3Voice
                /// </summary>
                public const string ES_LA_SOFIAV3VOICE = "es-LA_SofiaV3Voice";
                /// <summary>
                /// Constant ES_US_SOFIAV3VOICE for es-US_SofiaV3Voice
                /// </summary>
                public const string ES_US_SOFIAV3VOICE = "es-US_SofiaV3Voice";
                /// <summary>
                /// Constant FR_CA_LOUISEV3VOICE for fr-CA_LouiseV3Voice
                /// </summary>
                public const string FR_CA_LOUISEV3VOICE = "fr-CA_LouiseV3Voice";
                /// <summary>
                /// Constant FR_FR_NICOLASV3VOICE for fr-FR_NicolasV3Voice
                /// </summary>
                public const string FR_FR_NICOLASV3VOICE = "fr-FR_NicolasV3Voice";
                /// <summary>
                /// Constant FR_FR_RENEEV3VOICE for fr-FR_ReneeV3Voice
                /// </summary>
                public const string FR_FR_RENEEV3VOICE = "fr-FR_ReneeV3Voice";
                /// <summary>
                /// Constant IT_IT_FRANCESCAV3VOICE for it-IT_FrancescaV3Voice
                /// </summary>
                public const string IT_IT_FRANCESCAV3VOICE = "it-IT_FrancescaV3Voice";
                /// <summary>
                /// Constant JA_JP_EMIV3VOICE for ja-JP_EmiV3Voice
                /// </summary>
                public const string JA_JP_EMIV3VOICE = "ja-JP_EmiV3Voice";
                /// <summary>
                /// Constant KO_KR_HYUNJUNVOICE for ko-KR_HyunjunVoice
                /// </summary>
                public const string KO_KR_HYUNJUNVOICE = "ko-KR_HyunjunVoice";
                /// <summary>
                /// Constant KO_KR_JINV3VOICE for ko-KR_JinV3Voice
                /// </summary>
                public const string KO_KR_JINV3VOICE = "ko-KR_JinV3Voice";
                /// <summary>
                /// Constant KO_KR_SIWOOVOICE for ko-KR_SiWooVoice
                /// </summary>
                public const string KO_KR_SIWOOVOICE = "ko-KR_SiWooVoice";
                /// <summary>
                /// Constant KO_KR_YOUNGMIVOICE for ko-KR_YoungmiVoice
                /// </summary>
                public const string KO_KR_YOUNGMIVOICE = "ko-KR_YoungmiVoice";
                /// <summary>
                /// Constant KO_KR_YUNAVOICE for ko-KR_YunaVoice
                /// </summary>
                public const string KO_KR_YUNAVOICE = "ko-KR_YunaVoice";
                /// <summary>
                /// Constant NL_BE_ADELEVOICE for nl-BE_AdeleVoice
                /// </summary>
                public const string NL_BE_ADELEVOICE = "nl-BE_AdeleVoice";
                /// <summary>
                /// Constant NL_BE_BRAMVOICE for nl-BE_BramVoice
                /// </summary>
                public const string NL_BE_BRAMVOICE = "nl-BE_BramVoice";
                /// <summary>
                /// Constant NL_NL_EMMAVOICE for nl-NL_EmmaVoice
                /// </summary>
                public const string NL_NL_EMMAVOICE = "nl-NL_EmmaVoice";
                /// <summary>
                /// Constant NL_NL_LIAMVOICE for nl-NL_LiamVoice
                /// </summary>
                public const string NL_NL_LIAMVOICE = "nl-NL_LiamVoice";
                /// <summary>
                /// Constant PT_BR_ISABELAV3VOICE for pt-BR_IsabelaV3Voice
                /// </summary>
                public const string PT_BR_ISABELAV3VOICE = "pt-BR_IsabelaV3Voice";
                /// <summary>
                /// Constant SV_SE_INGRIDVOICE for sv-SE_IngridVoice
                /// </summary>
                public const string SV_SE_INGRIDVOICE = "sv-SE_IngridVoice";
                /// <summary>
                /// Constant ZH_CN_LINAVOICE for zh-CN_LiNaVoice
                /// </summary>
                public const string ZH_CN_LINAVOICE = "zh-CN_LiNaVoice";
                /// <summary>
                /// Constant ZH_CN_WANGWEIVOICE for zh-CN_WangWeiVoice
                /// </summary>
                public const string ZH_CN_WANGWEIVOICE = "zh-CN_WangWeiVoice";
                /// <summary>
                /// Constant ZH_CN_ZHANGJINGVOICE for zh-CN_ZhangJingVoice
                /// </summary>
                public const string ZH_CN_ZHANGJINGVOICE = "zh-CN_ZhangJingVoice";
                
            }
            /// <summary>
            /// *For German voices,* indicates how the service is to spell out strings of individual letters. To
            /// indicate the pace of the spelling, specify one of the following values:
            /// * `default` - The service reads the characters at the rate at which it synthesizes speech for the
            /// request. You can also omit the parameter entirely to achieve the default behavior.
            /// * `singles` - The service reads the characters one at a time, with a brief pause between each character.
            /// * `pairs` - The service reads the characters two at a time, with a brief pause between each pair.
            /// * `triples` - The service reads the characters three at a time, with a brief pause between each triplet.
            ///
            ///
            /// For more information, see [Specifying how strings are spelled
            /// out](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-synthesis-params#params-spell-out-mode).
            /// </summary>
            public class SpellOutModeValue
            {
                /// <summary>
                /// Constant DEFAULT for default
                /// </summary>
                public const string DEFAULT = "default";
                /// <summary>
                /// Constant SINGLES for singles
                /// </summary>
                public const string SINGLES = "singles";
                /// <summary>
                /// Constant PAIRS for pairs
                /// </summary>
                public const string PAIRS = "pairs";
                /// <summary>
                /// Constant TRIPLES for triples
                /// </summary>
                public const string TRIPLES = "triples";
                
            }
        }
        /// <summary>
        /// Get pronunciation.
        ///
        /// Gets the phonetic pronunciation for the specified word. You can request the pronunciation for a specific
        /// format. You can also request the pronunciation for a specific voice to see the default translation for the
        /// language of that voice or for a specific custom model to see the translation for that model.
        ///
        /// **Note:** Effective **31 March 2022**, all *neural voices* are deprecated. The deprecated voices remain
        /// available to existing users until 31 March 2023, when they will be removed from the service and the
        /// documentation. *No enhanced neural voices or expressive neural voices are deprecated.* For more information,
        /// see the [1 March 2023 service
        /// update](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-release-notes#text-to-speech-1march2023)
        /// in the release notes.
        ///
        /// **See also:** [Querying a word from a
        /// language](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuWordsQueryLanguage).
        /// </summary>
        /// <param name="text">The word for which the pronunciation is requested.</param>
        /// <param name="voice">A voice that specifies the language in which the pronunciation is to be returned. If you
        /// omit the `voice` parameter, the service uses the US English `en-US_MichaelV3Voice` by default. All voices
        /// for the same language (for example, `en-US`) return the same translation.
        ///
        /// _For IBM Cloud Pak for Data,_ if you do not install the `en-US_MichaelV3Voice`, you must either specify a
        /// voice with the request or specify a new default voice for your installation of the service.
        ///
        /// **See also:** [Using the default
        /// voice](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-voices-use#specify-voice-default).
        /// (optional, default to en-US_MichaelV3Voice)</param>
        /// <param name="format">The phoneme format in which to return the pronunciation. The Arabic, Chinese, Dutch,
        /// Australian English, and Korean languages support only IPA. Omit the parameter to obtain the pronunciation in
        /// the default format. (optional, default to ipa)</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom model for which the pronunciation is
        /// to be returned. The language of a specified custom model must match the language of the specified voice. If
        /// the word is not defined in the specified custom model, the service returns the default translation for the
        /// custom model's language. You must make the request with credentials for the instance of the service that
        /// owns the custom model. Omit the parameter to see the translation for the specified voice with no
        /// customization. (optional)</param>
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
        /// Enum values for GetPronunciation.
        /// </summary>
        public class GetPronunciationEnums
        {
            /// <summary>
            /// A voice that specifies the language in which the pronunciation is to be returned. If you omit the
            /// `voice` parameter, the service uses the US English `en-US_MichaelV3Voice` by default. All voices for the
            /// same language (for example, `en-US`) return the same translation.
            ///
            /// _For IBM Cloud Pak for Data,_ if you do not install the `en-US_MichaelV3Voice`, you must either specify
            /// a voice with the request or specify a new default voice for your installation of the service.
            ///
            /// **See also:** [Using the default
            /// voice](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-voices-use#specify-voice-default).
            /// </summary>
            public class VoiceValue
            {
                /// <summary>
                /// Constant AR_MS_OMARVOICE for ar-MS_OmarVoice
                /// </summary>
                public const string AR_MS_OMARVOICE = "ar-MS_OmarVoice";
                /// <summary>
                /// Constant CS_CZ_ALENAVOICE for cs-CZ_AlenaVoice
                /// </summary>
                public const string CS_CZ_ALENAVOICE = "cs-CZ_AlenaVoice";
                /// <summary>
                /// Constant DE_DE_BIRGITV3VOICE for de-DE_BirgitV3Voice
                /// </summary>
                public const string DE_DE_BIRGITV3VOICE = "de-DE_BirgitV3Voice";
                /// <summary>
                /// Constant DE_DE_DIETERV3VOICE for de-DE_DieterV3Voice
                /// </summary>
                public const string DE_DE_DIETERV3VOICE = "de-DE_DieterV3Voice";
                /// <summary>
                /// Constant DE_DE_ERIKAV3VOICE for de-DE_ErikaV3Voice
                /// </summary>
                public const string DE_DE_ERIKAV3VOICE = "de-DE_ErikaV3Voice";
                /// <summary>
                /// Constant EN_AU_CRAIGVOICE for en-AU_CraigVoice
                /// </summary>
                public const string EN_AU_CRAIGVOICE = "en-AU_CraigVoice";
                /// <summary>
                /// Constant EN_AU_HEIDIEXPRESSIVE for en-AU_HeidiExpressive
                /// </summary>
                public const string EN_AU_HEIDIEXPRESSIVE = "en-AU_HeidiExpressive";
                /// <summary>
                /// Constant EN_AU_JACKEXPRESSIVE for en-AU_JackExpressive
                /// </summary>
                public const string EN_AU_JACKEXPRESSIVE = "en-AU_JackExpressive";
                /// <summary>
                /// Constant EN_AU_MADISONVOICE for en-AU_MadisonVoice
                /// </summary>
                public const string EN_AU_MADISONVOICE = "en-AU_MadisonVoice";
                /// <summary>
                /// Constant EN_AU_STEVEVOICE for en-AU_SteveVoice
                /// </summary>
                public const string EN_AU_STEVEVOICE = "en-AU_SteveVoice";
                /// <summary>
                /// Constant EN_GB_CHARLOTTEV3VOICE for en-GB_CharlotteV3Voice
                /// </summary>
                public const string EN_GB_CHARLOTTEV3VOICE = "en-GB_CharlotteV3Voice";
                /// <summary>
                /// Constant EN_GB_JAMESV3VOICE for en-GB_JamesV3Voice
                /// </summary>
                public const string EN_GB_JAMESV3VOICE = "en-GB_JamesV3Voice";
                /// <summary>
                /// Constant EN_GB_KATEV3VOICE for en-GB_KateV3Voice
                /// </summary>
                public const string EN_GB_KATEV3VOICE = "en-GB_KateV3Voice";
                /// <summary>
                /// Constant EN_US_ALLISONEXPRESSIVE for en-US_AllisonExpressive
                /// </summary>
                public const string EN_US_ALLISONEXPRESSIVE = "en-US_AllisonExpressive";
                /// <summary>
                /// Constant EN_US_ALLISONV3VOICE for en-US_AllisonV3Voice
                /// </summary>
                public const string EN_US_ALLISONV3VOICE = "en-US_AllisonV3Voice";
                /// <summary>
                /// Constant EN_US_EMILYV3VOICE for en-US_EmilyV3Voice
                /// </summary>
                public const string EN_US_EMILYV3VOICE = "en-US_EmilyV3Voice";
                /// <summary>
                /// Constant EN_US_EMMAEXPRESSIVE for en-US_EmmaExpressive
                /// </summary>
                public const string EN_US_EMMAEXPRESSIVE = "en-US_EmmaExpressive";
                /// <summary>
                /// Constant EN_US_HENRYV3VOICE for en-US_HenryV3Voice
                /// </summary>
                public const string EN_US_HENRYV3VOICE = "en-US_HenryV3Voice";
                /// <summary>
                /// Constant EN_US_KEVINV3VOICE for en-US_KevinV3Voice
                /// </summary>
                public const string EN_US_KEVINV3VOICE = "en-US_KevinV3Voice";
                /// <summary>
                /// Constant EN_US_LISAEXPRESSIVE for en-US_LisaExpressive
                /// </summary>
                public const string EN_US_LISAEXPRESSIVE = "en-US_LisaExpressive";
                /// <summary>
                /// Constant EN_US_LISAV3VOICE for en-US_LisaV3Voice
                /// </summary>
                public const string EN_US_LISAV3VOICE = "en-US_LisaV3Voice";
                /// <summary>
                /// Constant EN_US_MICHAELEXPRESSIVE for en-US_MichaelExpressive
                /// </summary>
                public const string EN_US_MICHAELEXPRESSIVE = "en-US_MichaelExpressive";
                /// <summary>
                /// Constant EN_US_MICHAELV3VOICE for en-US_MichaelV3Voice
                /// </summary>
                public const string EN_US_MICHAELV3VOICE = "en-US_MichaelV3Voice";
                /// <summary>
                /// Constant EN_US_OLIVIAV3VOICE for en-US_OliviaV3Voice
                /// </summary>
                public const string EN_US_OLIVIAV3VOICE = "en-US_OliviaV3Voice";
                /// <summary>
                /// Constant ES_ES_ENRIQUEV3VOICE for es-ES_EnriqueV3Voice
                /// </summary>
                public const string ES_ES_ENRIQUEV3VOICE = "es-ES_EnriqueV3Voice";
                /// <summary>
                /// Constant ES_ES_LAURAV3VOICE for es-ES_LauraV3Voice
                /// </summary>
                public const string ES_ES_LAURAV3VOICE = "es-ES_LauraV3Voice";
                /// <summary>
                /// Constant ES_LA_SOFIAV3VOICE for es-LA_SofiaV3Voice
                /// </summary>
                public const string ES_LA_SOFIAV3VOICE = "es-LA_SofiaV3Voice";
                /// <summary>
                /// Constant ES_US_SOFIAV3VOICE for es-US_SofiaV3Voice
                /// </summary>
                public const string ES_US_SOFIAV3VOICE = "es-US_SofiaV3Voice";
                /// <summary>
                /// Constant FR_CA_LOUISEV3VOICE for fr-CA_LouiseV3Voice
                /// </summary>
                public const string FR_CA_LOUISEV3VOICE = "fr-CA_LouiseV3Voice";
                /// <summary>
                /// Constant FR_FR_NICOLASV3VOICE for fr-FR_NicolasV3Voice
                /// </summary>
                public const string FR_FR_NICOLASV3VOICE = "fr-FR_NicolasV3Voice";
                /// <summary>
                /// Constant FR_FR_RENEEV3VOICE for fr-FR_ReneeV3Voice
                /// </summary>
                public const string FR_FR_RENEEV3VOICE = "fr-FR_ReneeV3Voice";
                /// <summary>
                /// Constant IT_IT_FRANCESCAV3VOICE for it-IT_FrancescaV3Voice
                /// </summary>
                public const string IT_IT_FRANCESCAV3VOICE = "it-IT_FrancescaV3Voice";
                /// <summary>
                /// Constant JA_JP_EMIV3VOICE for ja-JP_EmiV3Voice
                /// </summary>
                public const string JA_JP_EMIV3VOICE = "ja-JP_EmiV3Voice";
                /// <summary>
                /// Constant KO_KR_HYUNJUNVOICE for ko-KR_HyunjunVoice
                /// </summary>
                public const string KO_KR_HYUNJUNVOICE = "ko-KR_HyunjunVoice";
                /// <summary>
                /// Constant KO_KR_JINV3VOICE for ko-KR_JinV3Voice
                /// </summary>
                public const string KO_KR_JINV3VOICE = "ko-KR_JinV3Voice";
                /// <summary>
                /// Constant KO_KR_SIWOOVOICE for ko-KR_SiWooVoice
                /// </summary>
                public const string KO_KR_SIWOOVOICE = "ko-KR_SiWooVoice";
                /// <summary>
                /// Constant KO_KR_YOUNGMIVOICE for ko-KR_YoungmiVoice
                /// </summary>
                public const string KO_KR_YOUNGMIVOICE = "ko-KR_YoungmiVoice";
                /// <summary>
                /// Constant KO_KR_YUNAVOICE for ko-KR_YunaVoice
                /// </summary>
                public const string KO_KR_YUNAVOICE = "ko-KR_YunaVoice";
                /// <summary>
                /// Constant NL_BE_ADELEVOICE for nl-BE_AdeleVoice
                /// </summary>
                public const string NL_BE_ADELEVOICE = "nl-BE_AdeleVoice";
                /// <summary>
                /// Constant NL_BE_BRAMVOICE for nl-BE_BramVoice
                /// </summary>
                public const string NL_BE_BRAMVOICE = "nl-BE_BramVoice";
                /// <summary>
                /// Constant NL_NL_EMMAVOICE for nl-NL_EmmaVoice
                /// </summary>
                public const string NL_NL_EMMAVOICE = "nl-NL_EmmaVoice";
                /// <summary>
                /// Constant NL_NL_LIAMVOICE for nl-NL_LiamVoice
                /// </summary>
                public const string NL_NL_LIAMVOICE = "nl-NL_LiamVoice";
                /// <summary>
                /// Constant PT_BR_ISABELAV3VOICE for pt-BR_IsabelaV3Voice
                /// </summary>
                public const string PT_BR_ISABELAV3VOICE = "pt-BR_IsabelaV3Voice";
                /// <summary>
                /// Constant SV_SE_INGRIDVOICE for sv-SE_IngridVoice
                /// </summary>
                public const string SV_SE_INGRIDVOICE = "sv-SE_IngridVoice";
                /// <summary>
                /// Constant ZH_CN_LINAVOICE for zh-CN_LiNaVoice
                /// </summary>
                public const string ZH_CN_LINAVOICE = "zh-CN_LiNaVoice";
                /// <summary>
                /// Constant ZH_CN_WANGWEIVOICE for zh-CN_WangWeiVoice
                /// </summary>
                public const string ZH_CN_WANGWEIVOICE = "zh-CN_WangWeiVoice";
                /// <summary>
                /// Constant ZH_CN_ZHANGJINGVOICE for zh-CN_ZhangJingVoice
                /// </summary>
                public const string ZH_CN_ZHANGJINGVOICE = "zh-CN_ZhangJingVoice";
                
            }
            /// <summary>
            /// The phoneme format in which to return the pronunciation. The Arabic, Chinese, Dutch, Australian English,
            /// and Korean languages support only IPA. Omit the parameter to obtain the pronunciation in the default
            /// format.
            /// </summary>
            public class FormatValue
            {
                /// <summary>
                /// Constant IBM for ibm
                /// </summary>
                public const string IBM = "ibm";
                /// <summary>
                /// Constant IPA for ipa
                /// </summary>
                public const string IPA = "ipa";
                
            }
        }
        /// <summary>
        /// Create a custom model.
        ///
        /// Creates a new empty custom model. You must specify a name for the new custom model. You can optionally
        /// specify the language and a description for the new model. The model is owned by the instance of the service
        /// whose credentials are used to create it.
        ///
        /// **See also:** [Creating a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customModels#cuModelsCreate).
        ///
        /// **Note:** Effective **31 March 2022**, all *neural voices* are deprecated. The deprecated voices remain
        /// available to existing users until 31 March 2023, when they will be removed from the service and the
        /// documentation. *No enhanced neural voices or expressive neural voices are deprecated.* For more information,
        /// see the [1 March 2023 service
        /// update](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-release-notes#text-to-speech-1march2023)
        /// in the release notes.
        /// </summary>
        /// <param name="name">The name of the new custom model. Use a localized name that matches the language of the
        /// custom model. Use a name that describes the purpose of the custom model, such as `Medical custom model` or
        /// `Legal custom model`. Use a name that is unique among all custom models that you own.
        ///
        /// Include a maximum of 256 characters in the name. Do not use backslashes, slashes, colons, equal signs,
        /// ampersands, or question marks in the name.</param>
        /// <param name="language">The language of the new custom model. You create a custom model for a specific
        /// language, not for a specific voice. A custom model can be used with any voice for its specified language.
        /// Omit the parameter to use the the default language, `en-US`. (optional, default to en-US)</param>
        /// <param name="description">A recommended description of the new custom model. Use a localized description
        /// that matches the language of the custom model. Include a maximum of 128 characters in the description.
        /// (optional)</param>
        /// <returns><see cref="CustomModel" />CustomModel</returns>
        public DetailedResponse<CustomModel> CreateCustomModel(string name, string language = null, string description = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateCustomModel`");
            }
            DetailedResponse<CustomModel> result = null;

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

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "CreateCustomModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CustomModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CustomModel>();
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
        /// Lists metadata such as the name and description for all custom models that are owned by an instance of the
        /// service. Specify a language to list the custom models for that language only. To see the words and prompts
        /// in addition to the metadata for a specific custom model, use the [Get a custom model](#getcustommodel)
        /// method. You must use credentials for the instance of the service that owns a model to list information about
        /// it.
        ///
        /// **See also:** [Querying all custom
        /// models](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customModels#cuModelsQueryAll).
        /// </summary>
        /// <param name="language">The language for which custom models that are owned by the requesting credentials are
        /// to be returned. Omit the parameter to see all custom models that are owned by the requester.
        /// (optional)</param>
        /// <returns><see cref="CustomModels" />CustomModels</returns>
        public DetailedResponse<CustomModels> ListCustomModels(string language = null)
        {
            DetailedResponse<CustomModels> result = null;

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

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "ListCustomModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CustomModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CustomModels>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListCustomModels.
        /// </summary>
        public class ListCustomModelsEnums
        {
            /// <summary>
            /// The language for which custom models that are owned by the requesting credentials are to be returned.
            /// Omit the parameter to see all custom models that are owned by the requester.
            /// </summary>
            public class LanguageValue
            {
                /// <summary>
                /// Constant AR_MS for ar-MS
                /// </summary>
                public const string AR_MS = "ar-MS";
                /// <summary>
                /// Constant CS_CZ for cs-CZ
                /// </summary>
                public const string CS_CZ = "cs-CZ";
                /// <summary>
                /// Constant DE_DE for de-DE
                /// </summary>
                public const string DE_DE = "de-DE";
                /// <summary>
                /// Constant EN_AU for en-AU
                /// </summary>
                public const string EN_AU = "en-AU";
                /// <summary>
                /// Constant EN_GB for en-GB
                /// </summary>
                public const string EN_GB = "en-GB";
                /// <summary>
                /// Constant EN_US for en-US
                /// </summary>
                public const string EN_US = "en-US";
                /// <summary>
                /// Constant ES_ES for es-ES
                /// </summary>
                public const string ES_ES = "es-ES";
                /// <summary>
                /// Constant ES_LA for es-LA
                /// </summary>
                public const string ES_LA = "es-LA";
                /// <summary>
                /// Constant ES_US for es-US
                /// </summary>
                public const string ES_US = "es-US";
                /// <summary>
                /// Constant FR_CA for fr-CA
                /// </summary>
                public const string FR_CA = "fr-CA";
                /// <summary>
                /// Constant FR_FR for fr-FR
                /// </summary>
                public const string FR_FR = "fr-FR";
                /// <summary>
                /// Constant IT_IT for it-IT
                /// </summary>
                public const string IT_IT = "it-IT";
                /// <summary>
                /// Constant JA_JP for ja-JP
                /// </summary>
                public const string JA_JP = "ja-JP";
                /// <summary>
                /// Constant KO_KR for ko-KR
                /// </summary>
                public const string KO_KR = "ko-KR";
                /// <summary>
                /// Constant NL_BE for nl-BE
                /// </summary>
                public const string NL_BE = "nl-BE";
                /// <summary>
                /// Constant NL_NL for nl-NL
                /// </summary>
                public const string NL_NL = "nl-NL";
                /// <summary>
                /// Constant PT_BR for pt-BR
                /// </summary>
                public const string PT_BR = "pt-BR";
                /// <summary>
                /// Constant SV_SE for sv-SE
                /// </summary>
                public const string SV_SE = "sv-SE";
                /// <summary>
                /// Constant ZH_CN for zh-CN
                /// </summary>
                public const string ZH_CN = "zh-CN";
                
            }
        }

        /// <summary>
        /// Update a custom model.
        ///
        /// Updates information for the specified custom model. You can update metadata such as the name and description
        /// of the model. You can also update the words in the model and their translations. Adding a new translation
        /// for a word that already exists in a custom model overwrites the word's existing translation. A custom model
        /// can contain no more than 20,000 entries. You must use credentials for the instance of the service that owns
        /// a model to update it.
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
        /// **See also:**
        /// * [Updating a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customModels#cuModelsUpdate)
        /// * [Adding words to a Japanese custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuJapaneseAdd)
        /// * [Understanding
        /// customization](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customIntro#customIntro).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <param name="name">A new name for the custom model. (optional)</param>
        /// <param name="description">A new description for the custom model. (optional)</param>
        /// <param name="words">An array of `Word` objects that provides the words and their translations that are to be
        /// added or updated for the custom model. Pass an empty array to make no additions or updates.
        /// (optional)</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> UpdateCustomModel(string customizationId, string name = null, string description = null, List<Word> words = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `UpdateCustomModel`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
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

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "UpdateCustomModel"));
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
        /// Gets all information about a specified custom model. In addition to metadata such as the name and
        /// description of the custom model, the output includes the words and their translations that are defined for
        /// the model, as well as any prompts that are defined for the model. To see just the metadata for a model, use
        /// the [List custom models](#listcustommodels) method.
        ///
        /// **See also:** [Querying a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customModels#cuModelsQuery).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="CustomModel" />CustomModel</returns>
        public DetailedResponse<CustomModel> GetCustomModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetCustomModel`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<CustomModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "GetCustomModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<CustomModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<CustomModel>();
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
        /// Deletes the specified custom model. You must use credentials for the instance of the service that owns a
        /// model to delete it.
        ///
        /// **See also:** [Deleting a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customModels#cuModelsDelete).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteCustomModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteCustomModel`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}");


                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "DeleteCustomModel"));
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
        /// Adds one or more words and their translations to the specified custom model. Adding a new translation for a
        /// word that already exists in a custom model overwrites the word's existing translation. A custom model can
        /// contain no more than 20,000 entries. You must use credentials for the instance of the service that owns a
        /// model to add words to it.
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
        /// **See also:**
        /// * [Adding multiple words to a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuWordsAdd)
        /// * [Adding words to a Japanese custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuJapaneseAdd)
        /// * [Understanding
        /// customization](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customIntro#customIntro).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <param name="words">The [Add custom words](#addwords) method accepts an array of `Word` objects. Each object
        /// provides a word that is to be added or updated for the custom model and the word's translation.
        ///
        /// The [List custom words](#listwords) method returns an array of `Word` objects. Each object shows a word and
        /// its translation from the custom model. The words are listed in alphabetical order, with uppercase letters
        /// listed before lowercase letters. The array is empty if the custom model contains no words.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddWords(string customizationId, List<Word> words)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddWords`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
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
        /// Lists all of the words and their translations for the specified custom model. The output shows the
        /// translations as they are defined in the model. You must use credentials for the instance of the service that
        /// owns a model to list its words.
        ///
        /// **See also:** [Querying all words from a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuWordsQueryModel).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="Words" />Words</returns>
        public DetailedResponse<Words> ListWords(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `ListWords`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
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
        /// Adds a single word and its translation to the specified custom model. Adding a new translation for a word
        /// that already exists in a custom model overwrites the word's existing translation. A custom model can contain
        /// no more than 20,000 entries. You must use credentials for the instance of the service that owns a model to
        /// add a word to it.
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
        /// **See also:**
        /// * [Adding a single word to a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuWordAdd)
        /// * [Adding words to a Japanese custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuJapaneseAdd)
        /// * [Understanding
        /// customization](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customIntro#customIntro).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be added or updated for the custom model.</param>
        /// <param name="translation">The phonetic or sounds-like translation for the word. A phonetic translation is
        /// based on the SSML format for representing the phonetic string of a word either as an IPA translation or as
        /// an IBM SPR translation. The Arabic, Chinese, Dutch, Australian English, and Korean languages support only
        /// IPA. A sounds-like is one or more words that, when combined, sound like the word.</param>
        /// <param name="partOfSpeech">**Japanese only.** The part of speech for the word. The service uses the value to
        /// produce the correct intonation for the word. You can create only a single entry, with or without a single
        /// part of speech, for any word; you cannot create multiple entries with different parts of speech for the same
        /// word. For more information, see [Working with Japanese
        /// entries](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-rules#jaNotes). (optional)</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddWord(string customizationId, string word, string translation, string partOfSpeech = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddWord`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("`word` is required for `AddWord`");
            }
            else
            {
                word = Uri.EscapeDataString(word);
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
        /// **See also:** [Querying a single word from a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuWordQueryModel).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be queried from the custom model.</param>
        /// <returns><see cref="Translation" />Translation</returns>
        public DetailedResponse<Translation> GetWord(string customizationId, string word)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetWord`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("`word` is required for `GetWord`");
            }
            else
            {
                word = Uri.EscapeDataString(word);
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
        /// Deletes a single word from the specified custom model. You must use credentials for the instance of the
        /// service that owns a model to delete its words.
        ///
        /// **See also:** [Deleting a word from a custom
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-customWords#cuWordDelete).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <param name="word">The word that is to be deleted from the custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteWord(string customizationId, string word)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteWord`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("`word` is required for `DeleteWord`");
            }
            else
            {
                word = Uri.EscapeDataString(word);
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
        /// List custom prompts.
        ///
        /// Lists information about all custom prompts that are defined for a custom model. The information includes the
        /// prompt ID, prompt text, status, and optional speaker ID for each prompt of the custom model. You must use
        /// credentials for the instance of the service that owns the custom model. The same information about all of
        /// the prompts for a custom model is also provided by the [Get a custom model](#getcustommodel) method. That
        /// method provides complete details about a specified custom model, including its language, owner, custom
        /// words, and more. Custom prompts are supported only for use with US English custom models and voices.
        ///
        /// **See also:** [Listing custom
        /// prompts](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-custom-prompts#tbe-custom-prompts-list).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <returns><see cref="Prompts" />Prompts</returns>
        public DetailedResponse<Prompts> ListCustomPrompts(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `ListCustomPrompts`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<Prompts> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/prompts");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "ListCustomPrompts"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Prompts>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Prompts>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Add a custom prompt.
        ///
        /// Adds a custom prompt to a custom model. A prompt is defined by the text that is to be spoken, the audio for
        /// that text, a unique user-specified ID for the prompt, and an optional speaker ID. The information is used to
        /// generate prosodic data that is not visible to the user. This data is used by the service to produce the
        /// synthesized audio upon request. You must use credentials for the instance of the service that owns a custom
        /// model to add a prompt to it. You can add a maximum of 1000 custom prompts to a single custom model.
        ///
        /// You are recommended to assign meaningful values for prompt IDs. For example, use `goodbye` to identify a
        /// prompt that speaks a farewell message. Prompt IDs must be unique within a given custom model. You cannot
        /// define two prompts with the same name for the same custom model. If you provide the ID of an existing
        /// prompt, the previously uploaded prompt is replaced by the new information. The existing prompt is
        /// reprocessed by using the new text and audio and, if provided, new speaker model, and the prosody data
        /// associated with the prompt is updated.
        ///
        /// The quality of a prompt is undefined if the language of a prompt does not match the language of its custom
        /// model. This is consistent with any text or SSML that is specified for a speech synthesis request. The
        /// service makes a best-effort attempt to render the specified text for the prompt; it does not validate that
        /// the language of the text matches the language of the model.
        ///
        /// Adding a prompt is an asynchronous operation. Although it accepts less audio than speaker enrollment, the
        /// service must align the audio with the provided text. The time that it takes to process a prompt depends on
        /// the prompt itself. The processing time for a reasonably sized prompt generally matches the length of the
        /// audio (for example, it takes 20 seconds to process a 20-second prompt).
        ///
        /// For shorter prompts, you can wait for a reasonable amount of time and then check the status of the prompt
        /// with the [Get a custom prompt](#getcustomprompt) method. For longer prompts, consider using that method to
        /// poll the service every few seconds to determine when the prompt becomes available. No prompt can be used for
        /// speech synthesis if it is in the `processing` or `failed` state. Only prompts that are in the `available`
        /// state can be used for speech synthesis.
        ///
        /// When it processes a request, the service attempts to align the text and the audio that are provided for the
        /// prompt. The text that is passed with a prompt must match the spoken audio as closely as possible. Optimally,
        /// the text and audio match exactly. The service does its best to align the specified text with the audio, and
        /// it can often compensate for mismatches between the two. But if the service cannot effectively align the text
        /// and the audio, possibly because the magnitude of mismatches between the two is too great, processing of the
        /// prompt fails.
        ///
        /// ### Evaluating a prompt
        ///
        ///  Always listen to and evaluate a prompt to determine its quality before using it in production. To evaluate
        /// a prompt, include only the single prompt in a speech synthesis request by using the following SSML
        /// extension, in this case for a prompt whose ID is `goodbye`:
        ///
        /// `<ibm:prompt id="goodbye"/>`
        ///
        /// In some cases, you might need to rerecord and resubmit a prompt as many as five times to address the
        /// following possible problems:
        /// * The service might fail to detect a mismatch between the prompt’s text and audio. The longer the prompt,
        /// the greater the chance for misalignment between its text and audio. Therefore, multiple shorter prompts are
        /// preferable to a single long prompt.
        /// * The text of a prompt might include a word that the service does not recognize. In this case, you can
        /// create a custom word and pronunciation pair to tell the service how to pronounce the word. You must then
        /// re-create the prompt.
        /// * The quality of the input audio might be insufficient or the service’s processing of the audio might fail
        /// to detect the intended prosody. Submitting new audio for the prompt can correct these issues.
        ///
        /// If a prompt that is created without a speaker ID does not adequately reflect the intended prosody, enrolling
        /// the speaker and providing a speaker ID for the prompt is one recommended means of potentially improving the
        /// quality of the prompt. This is especially important for shorter prompts such as "good-bye" or "thank you,"
        /// where less audio data makes it more difficult to match the prosody of the speaker. Custom prompts are
        /// supported only for use with US English custom models and voices.
        ///
        /// **See also:**
        /// * [Add a custom
        /// prompt](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-create#tbe-create-add-prompt)
        /// * [Evaluate a custom
        /// prompt](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-create#tbe-create-evaluate-prompt)
        /// * [Rules for creating custom
        /// prompts](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-rules#tbe-rules-prompts).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <param name="promptId">The identifier of the prompt that is to be added to the custom model:
        /// * Include a maximum of 49 characters in the ID.
        /// * Include only alphanumeric characters and `_` (underscores) in the ID.
        /// * Do not include XML sensitive characters (double quotes, single quotes, ampersands, angle brackets, and
        /// slashes) in the ID.
        /// * To add a new prompt, the ID must be unique for the specified custom model. Otherwise, the new information
        /// for the prompt overwrites the existing prompt that has that ID.</param>
        /// <param name="metadata">Information about the prompt that is to be added to a custom model. The following
        /// example of a `PromptMetadata` object includes both the required prompt text and an optional speaker model
        /// ID:
        ///
        /// `{ "prompt_text": "Thank you and good-bye!", "speaker_id": "823068b2-ed4e-11ea-b6e0-7b6456aa95cc"
        /// }`.</param>
        /// <param name="file">An audio file that speaks the text of the prompt with intonation and prosody that matches
        /// how you would like the prompt to be spoken.
        /// * The prompt audio must be in WAV format and must have a minimum sampling rate of 16 kHz. The service
        /// accepts audio with higher sampling rates. The service transcodes all audio to 16 kHz before processing it.
        /// * The length of the prompt audio is limited to 30 seconds.</param>
        /// <returns><see cref="Prompt" />Prompt</returns>
        public DetailedResponse<Prompt> AddCustomPrompt(string customizationId, string promptId, PromptMetadata metadata, System.IO.MemoryStream file)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddCustomPrompt`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(promptId))
            {
                throw new ArgumentNullException("`promptId` is required for `AddCustomPrompt`");
            }
            else
            {
                promptId = Uri.EscapeDataString(promptId);
            }
            if (metadata == null)
            {
                throw new ArgumentNullException("`metadata` is required for `AddCustomPrompt`");
            }
            if (file == null)
            {
                throw new ArgumentNullException("`file` is required for `AddCustomPrompt`");
            }
            DetailedResponse<Prompt> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (metadata != null)
                {
                    var metadataContent = new StringContent(JsonConvert.SerializeObject(metadata), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                    metadataContent.Headers.ContentType = null;
                    formData.Add(metadataContent, "metadata");
                }

                if (file != null)
                {
                    var fileContent = new ByteArrayContent(file.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("audio/wav", out contentType);
                    fileContent.Headers.ContentType = contentType;
                    formData.Add(fileContent, "file", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/prompts/{promptId}");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "AddCustomPrompt"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Prompt>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Prompt>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a custom prompt.
        ///
        /// Gets information about a specified custom prompt for a specified custom model. The information includes the
        /// prompt ID, prompt text, status, and optional speaker ID for each prompt of the custom model. You must use
        /// credentials for the instance of the service that owns the custom model. Custom prompts are supported only
        /// for use with US English custom models and voices.
        ///
        /// **See also:** [Listing custom
        /// prompts](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-custom-prompts#tbe-custom-prompts-list).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <param name="promptId">The identifier (name) of the prompt.</param>
        /// <returns><see cref="Prompt" />Prompt</returns>
        public DetailedResponse<Prompt> GetCustomPrompt(string customizationId, string promptId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetCustomPrompt`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(promptId))
            {
                throw new ArgumentNullException("`promptId` is required for `GetCustomPrompt`");
            }
            else
            {
                promptId = Uri.EscapeDataString(promptId);
            }
            DetailedResponse<Prompt> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/prompts/{promptId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "GetCustomPrompt"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Prompt>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Prompt>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a custom prompt.
        ///
        /// Deletes an existing custom prompt from a custom model. The service deletes the prompt with the specified ID.
        /// You must use credentials for the instance of the service that owns the custom model from which the prompt is
        /// to be deleted.
        ///
        /// **Caution:** Deleting a custom prompt elicits a 400 response code from synthesis requests that attempt to
        /// use the prompt. Make sure that you do not attempt to use a deleted prompt in a production application.
        /// Custom prompts are supported only for use with US English custom models and voices.
        ///
        /// **See also:** [Deleting a custom
        /// prompt](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-custom-prompts#tbe-custom-prompts-delete).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom model. You must make the request
        /// with credentials for the instance of the service that owns the custom model.</param>
        /// <param name="promptId">The identifier (name) of the prompt that is to be deleted.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteCustomPrompt(string customizationId, string promptId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteCustomPrompt`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(promptId))
            {
                throw new ArgumentNullException("`promptId` is required for `DeleteCustomPrompt`");
            }
            else
            {
                promptId = Uri.EscapeDataString(promptId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/prompts/{promptId}");


                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "DeleteCustomPrompt"));
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
        /// List speaker models.
        ///
        /// Lists information about all speaker models that are defined for a service instance. The information includes
        /// the speaker ID and speaker name of each defined speaker. You must use credentials for the instance of a
        /// service to list its speakers. Speaker models and the custom prompts with which they are used are supported
        /// only for use with US English custom models and voices.
        ///
        /// **See also:** [Listing speaker
        /// models](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-speaker-models#tbe-speaker-models-list).
        /// </summary>
        /// <returns><see cref="Speakers" />Speakers</returns>
        public DetailedResponse<Speakers> ListSpeakerModels()
        {
            DetailedResponse<Speakers> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/speakers");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "ListSpeakerModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Speakers>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Speakers>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create a speaker model.
        ///
        /// Creates a new speaker model, which is an optional enrollment token for users who are to add prompts to
        /// custom models. A speaker model contains information about a user's voice. The service extracts this
        /// information from a WAV audio sample that you pass as the body of the request. Associating a speaker model
        /// with a prompt is optional, but the information that is extracted from the speaker model helps the service
        /// learn about the speaker's voice.
        ///
        /// A speaker model can make an appreciable difference in the quality of prompts, especially short prompts with
        /// relatively little audio, that are associated with that speaker. A speaker model can help the service produce
        /// a prompt with more confidence; the lack of a speaker model can potentially compromise the quality of a
        /// prompt.
        ///
        /// The gender of the speaker who creates a speaker model does not need to match the gender of a voice that is
        /// used with prompts that are associated with that speaker model. For example, a speaker model that is created
        /// by a male speaker can be associated with prompts that are spoken by female voices.
        ///
        /// You create a speaker model for a given instance of the service. The new speaker model is owned by the
        /// service instance whose credentials are used to create it. That same speaker can then be used to create
        /// prompts for all custom models within that service instance. No language is associated with a speaker model,
        /// but each custom model has a single specified language. You can add prompts only to US English models.
        ///
        /// You specify a name for the speaker when you create it. The name must be unique among all speaker names for
        /// the owning service instance. To re-create a speaker model for an existing speaker name, you must first
        /// delete the existing speaker model that has that name.
        ///
        /// Speaker enrollment is a synchronous operation. Although it accepts more audio data than a prompt, the
        /// process of adding a speaker is very fast. The service simply extracts information about the speaker’s voice
        /// from the audio. Unlike prompts, speaker models neither need nor accept a transcription of the audio. When
        /// the call returns, the audio is fully processed and the speaker enrollment is complete.
        ///
        /// The service returns a speaker ID with the request. A speaker ID is globally unique identifier (GUID) that
        /// you use to identify the speaker in subsequent requests to the service. Speaker models and the custom prompts
        /// with which they are used are supported only for use with US English custom models and voices.
        ///
        /// **See also:**
        /// * [Create a speaker
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-create#tbe-create-speaker-model)
        /// * [Rules for creating speaker
        /// models](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-rules#tbe-rules-speakers).
        /// </summary>
        /// <param name="speakerName">The name of the speaker that is to be added to the service instance.
        /// * Include a maximum of 49 characters in the name.
        /// * Include only alphanumeric characters and `_` (underscores) in the name.
        /// * Do not include XML sensitive characters (double quotes, single quotes, ampersands, angle brackets, and
        /// slashes) in the name.
        /// * Do not use the name of an existing speaker that is already defined for the service instance.</param>
        /// <param name="audio">An enrollment audio file that contains a sample of the speaker’s voice.
        /// * The enrollment audio must be in WAV format and must have a minimum sampling rate of 16 kHz. The service
        /// accepts audio with higher sampling rates. It transcodes all audio to 16 kHz before processing it.
        /// * The length of the enrollment audio is limited to 1 minute. Speaking one or two paragraphs of text that
        /// include five to ten sentences is recommended.</param>
        /// <returns><see cref="SpeakerModel" />SpeakerModel</returns>
        public DetailedResponse<SpeakerModel> CreateSpeakerModel(string speakerName, System.IO.MemoryStream audio)
        {
            if (string.IsNullOrEmpty(speakerName))
            {
                throw new ArgumentNullException("`speakerName` is required for `CreateSpeakerModel`");
            }
            if (audio == null)
            {
                throw new ArgumentNullException("`audio` is required for `CreateSpeakerModel`");
            }
            DetailedResponse<SpeakerModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/speakers");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(speakerName))
                {
                    restRequest.WithArgument("speaker_name", speakerName);
                }
                restRequest.WithHeader("Content-Type", "audio/wav");
                var httpContent = new ByteArrayContent(audio.ToArray());
                httpContent.Headers.Add("Content-Type", "audio/wav");
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "CreateSpeakerModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SpeakerModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SpeakerModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a speaker model.
        ///
        /// Gets information about all prompts that are defined by a specified speaker for all custom models that are
        /// owned by a service instance. The information is grouped by the customization IDs of the custom models. For
        /// each custom model, the information lists information about each prompt that is defined for that custom model
        /// by the speaker. You must use credentials for the instance of the service that owns a speaker model to list
        /// its prompts. Speaker models and the custom prompts with which they are used are supported only for use with
        /// US English custom models and voices.
        ///
        /// **See also:** [Listing the custom prompts for a speaker
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-speaker-models#tbe-speaker-models-list-prompts).
        /// </summary>
        /// <param name="speakerId">The speaker ID (GUID) of the speaker model. You must make the request with service
        /// credentials for the instance of the service that owns the speaker model.</param>
        /// <returns><see cref="SpeakerCustomModels" />SpeakerCustomModels</returns>
        public DetailedResponse<SpeakerCustomModels> GetSpeakerModel(string speakerId)
        {
            if (string.IsNullOrEmpty(speakerId))
            {
                throw new ArgumentNullException("`speakerId` is required for `GetSpeakerModel`");
            }
            else
            {
                speakerId = Uri.EscapeDataString(speakerId);
            }
            DetailedResponse<SpeakerCustomModels> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/speakers/{speakerId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "GetSpeakerModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SpeakerCustomModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SpeakerCustomModels>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a speaker model.
        ///
        /// Deletes an existing speaker model from the service instance. The service deletes the enrolled speaker with
        /// the specified speaker ID. You must use credentials for the instance of the service that owns a speaker model
        /// to delete the speaker.
        ///
        /// Any prompts that are associated with the deleted speaker are not affected by the speaker's deletion. The
        /// prosodic data that defines the quality of a prompt is established when the prompt is created. A prompt is
        /// static and remains unaffected by deletion of its associated speaker. However, the prompt cannot be
        /// resubmitted or updated with its original speaker once that speaker is deleted. Speaker models and the custom
        /// prompts with which they are used are supported only for use with US English custom models and voices.
        ///
        /// **See also:** [Deleting a speaker
        /// model](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-tbe-speaker-models#tbe-speaker-models-delete).
        /// </summary>
        /// <param name="speakerId">The speaker ID (GUID) of the speaker model. You must make the request with service
        /// credentials for the instance of the service that owns the speaker model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteSpeakerModel(string speakerId)
        {
            if (string.IsNullOrEmpty(speakerId))
            {
                throw new ArgumentNullException("`speakerId` is required for `DeleteSpeakerModel`");
            }
            else
            {
                speakerId = Uri.EscapeDataString(speakerId);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/speakers/{speakerId}");


                restRequest.WithHeaders(Common.GetSdkHeaders("text_to_speech", "v1", "DeleteSpeakerModel"));
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
        /// of the service that was used to associate the customer ID with the data. You associate a customer ID with
        /// data by passing the `X-Watson-Metadata` header with a request that passes the data.
        ///
        /// **Note:** If you delete an instance of the service from the service console, all data associated with that
        /// service instance is automatically deleted. This includes all custom models and word/translation pairs, and
        /// all data related to speech synthesis requests.
        ///
        /// **See also:** [Information
        /// security](https://cloud.ibm.com/docs/text-to-speech?topic=text-to-speech-information-security#information-security).
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
