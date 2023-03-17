/**
* (C) Copyright IBM Corp. 2016, 2023.
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
using IBM.Watson.SpeechToText.v1.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace IBM.Watson.SpeechToText.v1
{
    public partial class SpeechToTextService : IBMService, ISpeechToTextService
    {
        const string defaultServiceName = "speech_to_text";
        private const string defaultServiceUrl = "https://api.us-south.speech-to-text.watson.cloud.ibm.com";

        public SpeechToTextService() : this(defaultServiceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(defaultServiceName)) { }
        public SpeechToTextService(IAuthenticator authenticator) : this(defaultServiceName, authenticator) {}
        public SpeechToTextService(string serviceName) : this(serviceName, ConfigBasedAuthenticatorFactory.GetAuthenticator(serviceName)) { }
        public SpeechToTextService(IClient httpClient) : base(defaultServiceName, httpClient) { }

        public SpeechToTextService(string serviceName, IAuthenticator authenticator) : base(serviceName, authenticator)
        {

            if (string.IsNullOrEmpty(ServiceUrl))
            {
                SetServiceUrl(defaultServiceUrl);
            }
        }

        /// <summary>
        /// List models.
        ///
        /// Lists all language models that are available for use with the service. The information includes the name of
        /// the model and its minimum sampling rate in Hertz, among other things. The ordering of the list of models can
        /// change from call to call; do not rely on an alphabetized or static list of models.
        ///
        /// **See also:** [Listing all
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-list#models-list-all).
        /// </summary>
        /// <returns><see cref="SpeechModels" />SpeechModels</returns>
        public DetailedResponse<SpeechModels> ListModels()
        {
            DetailedResponse<SpeechModels> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ListModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SpeechModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SpeechModels>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a model.
        ///
        /// Gets information for a single specified language model that is available for use with the service. The
        /// information includes the name of the model and its minimum sampling rate in Hertz, among other things.
        ///
        /// **See also:** [Listing a specific
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-list#models-list-specific).
        /// </summary>
        /// <param name="modelId">The identifier of the model in the form of its name from the output of the [List
        /// models](#listmodels) method.</param>
        /// <returns><see cref="SpeechModel" />SpeechModel</returns>
        public DetailedResponse<SpeechModel> GetModel(string modelId)
        {
            if (string.IsNullOrEmpty(modelId))
            {
                throw new ArgumentNullException("`modelId` is required for `GetModel`");
            }
            else
            {
                modelId = Uri.EscapeDataString(modelId);
            }
            DetailedResponse<SpeechModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models/{modelId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "GetModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SpeechModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SpeechModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for GetModel.
        /// </summary>
        public class GetModelEnums
        {
            /// <summary>
            /// The identifier of the model in the form of its name from the output of the [List models](#listmodels)
            /// method.
            /// </summary>
            public class ModelIdValue
            {
                /// <summary>
                /// Constant AR_MS_BROADBANDMODEL for ar-MS_BroadbandModel
                /// </summary>
                public const string AR_MS_BROADBANDMODEL = "ar-MS_BroadbandModel";
                /// <summary>
                /// Constant AR_MS_TELEPHONY for ar-MS_Telephony
                /// </summary>
                public const string AR_MS_TELEPHONY = "ar-MS_Telephony";
                /// <summary>
                /// Constant CS_CZ_TELEPHONY for cs-CZ_Telephony
                /// </summary>
                public const string CS_CZ_TELEPHONY = "cs-CZ_Telephony";
                /// <summary>
                /// Constant DE_DE_BROADBANDMODEL for de-DE_BroadbandModel
                /// </summary>
                public const string DE_DE_BROADBANDMODEL = "de-DE_BroadbandModel";
                /// <summary>
                /// Constant DE_DE_MULTIMEDIA for de-DE_Multimedia
                /// </summary>
                public const string DE_DE_MULTIMEDIA = "de-DE_Multimedia";
                /// <summary>
                /// Constant DE_DE_NARROWBANDMODEL for de-DE_NarrowbandModel
                /// </summary>
                public const string DE_DE_NARROWBANDMODEL = "de-DE_NarrowbandModel";
                /// <summary>
                /// Constant DE_DE_TELEPHONY for de-DE_Telephony
                /// </summary>
                public const string DE_DE_TELEPHONY = "de-DE_Telephony";
                /// <summary>
                /// Constant EN_AU_BROADBANDMODEL for en-AU_BroadbandModel
                /// </summary>
                public const string EN_AU_BROADBANDMODEL = "en-AU_BroadbandModel";
                /// <summary>
                /// Constant EN_AU_MULTIMEDIA for en-AU_Multimedia
                /// </summary>
                public const string EN_AU_MULTIMEDIA = "en-AU_Multimedia";
                /// <summary>
                /// Constant EN_AU_NARROWBANDMODEL for en-AU_NarrowbandModel
                /// </summary>
                public const string EN_AU_NARROWBANDMODEL = "en-AU_NarrowbandModel";
                /// <summary>
                /// Constant EN_AU_TELEPHONY for en-AU_Telephony
                /// </summary>
                public const string EN_AU_TELEPHONY = "en-AU_Telephony";
                /// <summary>
                /// Constant EN_GB_BROADBANDMODEL for en-GB_BroadbandModel
                /// </summary>
                public const string EN_GB_BROADBANDMODEL = "en-GB_BroadbandModel";
                /// <summary>
                /// Constant EN_GB_MULTIMEDIA for en-GB_Multimedia
                /// </summary>
                public const string EN_GB_MULTIMEDIA = "en-GB_Multimedia";
                /// <summary>
                /// Constant EN_GB_NARROWBANDMODEL for en-GB_NarrowbandModel
                /// </summary>
                public const string EN_GB_NARROWBANDMODEL = "en-GB_NarrowbandModel";
                /// <summary>
                /// Constant EN_GB_TELEPHONY for en-GB_Telephony
                /// </summary>
                public const string EN_GB_TELEPHONY = "en-GB_Telephony";
                /// <summary>
                /// Constant EN_IN_TELEPHONY for en-IN_Telephony
                /// </summary>
                public const string EN_IN_TELEPHONY = "en-IN_Telephony";
                /// <summary>
                /// Constant EN_US_BROADBANDMODEL for en-US_BroadbandModel
                /// </summary>
                public const string EN_US_BROADBANDMODEL = "en-US_BroadbandModel";
                /// <summary>
                /// Constant EN_US_MULTIMEDIA for en-US_Multimedia
                /// </summary>
                public const string EN_US_MULTIMEDIA = "en-US_Multimedia";
                /// <summary>
                /// Constant EN_US_NARROWBANDMODEL for en-US_NarrowbandModel
                /// </summary>
                public const string EN_US_NARROWBANDMODEL = "en-US_NarrowbandModel";
                /// <summary>
                /// Constant EN_US_SHORTFORM_NARROWBANDMODEL for en-US_ShortForm_NarrowbandModel
                /// </summary>
                public const string EN_US_SHORTFORM_NARROWBANDMODEL = "en-US_ShortForm_NarrowbandModel";
                /// <summary>
                /// Constant EN_US_TELEPHONY for en-US_Telephony
                /// </summary>
                public const string EN_US_TELEPHONY = "en-US_Telephony";
                /// <summary>
                /// Constant EN_WW_MEDICAL_TELEPHONY for en-WW_Medical_Telephony
                /// </summary>
                public const string EN_WW_MEDICAL_TELEPHONY = "en-WW_Medical_Telephony";
                /// <summary>
                /// Constant ES_AR_BROADBANDMODEL for es-AR_BroadbandModel
                /// </summary>
                public const string ES_AR_BROADBANDMODEL = "es-AR_BroadbandModel";
                /// <summary>
                /// Constant ES_AR_NARROWBANDMODEL for es-AR_NarrowbandModel
                /// </summary>
                public const string ES_AR_NARROWBANDMODEL = "es-AR_NarrowbandModel";
                /// <summary>
                /// Constant ES_CL_BROADBANDMODEL for es-CL_BroadbandModel
                /// </summary>
                public const string ES_CL_BROADBANDMODEL = "es-CL_BroadbandModel";
                /// <summary>
                /// Constant ES_CL_NARROWBANDMODEL for es-CL_NarrowbandModel
                /// </summary>
                public const string ES_CL_NARROWBANDMODEL = "es-CL_NarrowbandModel";
                /// <summary>
                /// Constant ES_CO_BROADBANDMODEL for es-CO_BroadbandModel
                /// </summary>
                public const string ES_CO_BROADBANDMODEL = "es-CO_BroadbandModel";
                /// <summary>
                /// Constant ES_CO_NARROWBANDMODEL for es-CO_NarrowbandModel
                /// </summary>
                public const string ES_CO_NARROWBANDMODEL = "es-CO_NarrowbandModel";
                /// <summary>
                /// Constant ES_ES_BROADBANDMODEL for es-ES_BroadbandModel
                /// </summary>
                public const string ES_ES_BROADBANDMODEL = "es-ES_BroadbandModel";
                /// <summary>
                /// Constant ES_ES_NARROWBANDMODEL for es-ES_NarrowbandModel
                /// </summary>
                public const string ES_ES_NARROWBANDMODEL = "es-ES_NarrowbandModel";
                /// <summary>
                /// Constant ES_ES_MULTIMEDIA for es-ES_Multimedia
                /// </summary>
                public const string ES_ES_MULTIMEDIA = "es-ES_Multimedia";
                /// <summary>
                /// Constant ES_ES_TELEPHONY for es-ES_Telephony
                /// </summary>
                public const string ES_ES_TELEPHONY = "es-ES_Telephony";
                /// <summary>
                /// Constant ES_LA_TELEPHONY for es-LA_Telephony
                /// </summary>
                public const string ES_LA_TELEPHONY = "es-LA_Telephony";
                /// <summary>
                /// Constant ES_MX_BROADBANDMODEL for es-MX_BroadbandModel
                /// </summary>
                public const string ES_MX_BROADBANDMODEL = "es-MX_BroadbandModel";
                /// <summary>
                /// Constant ES_MX_NARROWBANDMODEL for es-MX_NarrowbandModel
                /// </summary>
                public const string ES_MX_NARROWBANDMODEL = "es-MX_NarrowbandModel";
                /// <summary>
                /// Constant ES_PE_BROADBANDMODEL for es-PE_BroadbandModel
                /// </summary>
                public const string ES_PE_BROADBANDMODEL = "es-PE_BroadbandModel";
                /// <summary>
                /// Constant ES_PE_NARROWBANDMODEL for es-PE_NarrowbandModel
                /// </summary>
                public const string ES_PE_NARROWBANDMODEL = "es-PE_NarrowbandModel";
                /// <summary>
                /// Constant FR_CA_BROADBANDMODEL for fr-CA_BroadbandModel
                /// </summary>
                public const string FR_CA_BROADBANDMODEL = "fr-CA_BroadbandModel";
                /// <summary>
                /// Constant FR_CA_MULTIMEDIA for fr-CA_Multimedia
                /// </summary>
                public const string FR_CA_MULTIMEDIA = "fr-CA_Multimedia";
                /// <summary>
                /// Constant FR_CA_NARROWBANDMODEL for fr-CA_NarrowbandModel
                /// </summary>
                public const string FR_CA_NARROWBANDMODEL = "fr-CA_NarrowbandModel";
                /// <summary>
                /// Constant FR_CA_TELEPHONY for fr-CA_Telephony
                /// </summary>
                public const string FR_CA_TELEPHONY = "fr-CA_Telephony";
                /// <summary>
                /// Constant FR_FR_BROADBANDMODEL for fr-FR_BroadbandModel
                /// </summary>
                public const string FR_FR_BROADBANDMODEL = "fr-FR_BroadbandModel";
                /// <summary>
                /// Constant FR_FR_MULTIMEDIA for fr-FR_Multimedia
                /// </summary>
                public const string FR_FR_MULTIMEDIA = "fr-FR_Multimedia";
                /// <summary>
                /// Constant FR_FR_NARROWBANDMODEL for fr-FR_NarrowbandModel
                /// </summary>
                public const string FR_FR_NARROWBANDMODEL = "fr-FR_NarrowbandModel";
                /// <summary>
                /// Constant FR_FR_TELEPHONY for fr-FR_Telephony
                /// </summary>
                public const string FR_FR_TELEPHONY = "fr-FR_Telephony";
                /// <summary>
                /// Constant HI_IN_TELEPHONY for hi-IN_Telephony
                /// </summary>
                public const string HI_IN_TELEPHONY = "hi-IN_Telephony";
                /// <summary>
                /// Constant IT_IT_BROADBANDMODEL for it-IT_BroadbandModel
                /// </summary>
                public const string IT_IT_BROADBANDMODEL = "it-IT_BroadbandModel";
                /// <summary>
                /// Constant IT_IT_NARROWBANDMODEL for it-IT_NarrowbandModel
                /// </summary>
                public const string IT_IT_NARROWBANDMODEL = "it-IT_NarrowbandModel";
                /// <summary>
                /// Constant IT_IT_MULTIMEDIA for it-IT_Multimedia
                /// </summary>
                public const string IT_IT_MULTIMEDIA = "it-IT_Multimedia";
                /// <summary>
                /// Constant IT_IT_TELEPHONY for it-IT_Telephony
                /// </summary>
                public const string IT_IT_TELEPHONY = "it-IT_Telephony";
                /// <summary>
                /// Constant JA_JP_BROADBANDMODEL for ja-JP_BroadbandModel
                /// </summary>
                public const string JA_JP_BROADBANDMODEL = "ja-JP_BroadbandModel";
                /// <summary>
                /// Constant JA_JP_MULTIMEDIA for ja-JP_Multimedia
                /// </summary>
                public const string JA_JP_MULTIMEDIA = "ja-JP_Multimedia";
                /// <summary>
                /// Constant JA_JP_NARROWBANDMODEL for ja-JP_NarrowbandModel
                /// </summary>
                public const string JA_JP_NARROWBANDMODEL = "ja-JP_NarrowbandModel";
                /// <summary>
                /// Constant JA_JP_TELEPHONY for ja-JP_Telephony
                /// </summary>
                public const string JA_JP_TELEPHONY = "ja-JP_Telephony";
                /// <summary>
                /// Constant KO_KR_BROADBANDMODEL for ko-KR_BroadbandModel
                /// </summary>
                public const string KO_KR_BROADBANDMODEL = "ko-KR_BroadbandModel";
                /// <summary>
                /// Constant KO_KR_MULTIMEDIA for ko-KR_Multimedia
                /// </summary>
                public const string KO_KR_MULTIMEDIA = "ko-KR_Multimedia";
                /// <summary>
                /// Constant KO_KR_NARROWBANDMODEL for ko-KR_NarrowbandModel
                /// </summary>
                public const string KO_KR_NARROWBANDMODEL = "ko-KR_NarrowbandModel";
                /// <summary>
                /// Constant KO_KR_TELEPHONY for ko-KR_Telephony
                /// </summary>
                public const string KO_KR_TELEPHONY = "ko-KR_Telephony";
                /// <summary>
                /// Constant NL_BE_TELEPHONY for nl-BE_Telephony
                /// </summary>
                public const string NL_BE_TELEPHONY = "nl-BE_Telephony";
                /// <summary>
                /// Constant NL_NL_BROADBANDMODEL for nl-NL_BroadbandModel
                /// </summary>
                public const string NL_NL_BROADBANDMODEL = "nl-NL_BroadbandModel";
                /// <summary>
                /// Constant NL_NL_MULTIMEDIA for nl-NL_Multimedia
                /// </summary>
                public const string NL_NL_MULTIMEDIA = "nl-NL_Multimedia";
                /// <summary>
                /// Constant NL_NL_NARROWBANDMODEL for nl-NL_NarrowbandModel
                /// </summary>
                public const string NL_NL_NARROWBANDMODEL = "nl-NL_NarrowbandModel";
                /// <summary>
                /// Constant NL_NL_TELEPHONY for nl-NL_Telephony
                /// </summary>
                public const string NL_NL_TELEPHONY = "nl-NL_Telephony";
                /// <summary>
                /// Constant PT_BR_BROADBANDMODEL for pt-BR_BroadbandModel
                /// </summary>
                public const string PT_BR_BROADBANDMODEL = "pt-BR_BroadbandModel";
                /// <summary>
                /// Constant PT_BR_MULTIMEDIA for pt-BR_Multimedia
                /// </summary>
                public const string PT_BR_MULTIMEDIA = "pt-BR_Multimedia";
                /// <summary>
                /// Constant PT_BR_NARROWBANDMODEL for pt-BR_NarrowbandModel
                /// </summary>
                public const string PT_BR_NARROWBANDMODEL = "pt-BR_NarrowbandModel";
                /// <summary>
                /// Constant PT_BR_TELEPHONY for pt-BR_Telephony
                /// </summary>
                public const string PT_BR_TELEPHONY = "pt-BR_Telephony";
                /// <summary>
                /// Constant SV_SE_TELEPHONY for sv-SE_Telephony
                /// </summary>
                public const string SV_SE_TELEPHONY = "sv-SE_Telephony";
                /// <summary>
                /// Constant ZH_CN_BROADBANDMODEL for zh-CN_BroadbandModel
                /// </summary>
                public const string ZH_CN_BROADBANDMODEL = "zh-CN_BroadbandModel";
                /// <summary>
                /// Constant ZH_CN_NARROWBANDMODEL for zh-CN_NarrowbandModel
                /// </summary>
                public const string ZH_CN_NARROWBANDMODEL = "zh-CN_NarrowbandModel";
                /// <summary>
                /// Constant ZH_CN_TELEPHONY for zh-CN_Telephony
                /// </summary>
                public const string ZH_CN_TELEPHONY = "zh-CN_Telephony";
                
            }
        }
        /// <summary>
        /// Recognize audio.
        ///
        /// Sends audio and returns transcription results for a recognition request. You can pass a maximum of 100 MB
        /// and a minimum of 100 bytes of audio with a request. The service automatically detects the endianness of the
        /// incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono
        /// during transcoding. The method returns only final results; to enable interim results, use the WebSocket API.
        /// (With the `curl` command, use the `--data-binary` option to upload the file for the request.)
        ///
        /// **See also:** [Making a basic HTTP
        /// request](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-http#HTTP-basic).
        ///
        /// ### Streaming mode
        ///
        ///  For requests to transcribe live audio as it becomes available, you must set the `Transfer-Encoding` header
        /// to `chunked` to use streaming mode. In streaming mode, the service closes the connection (status code 408)
        /// if it does not receive at least 15 seconds of audio (including silence) in any 30-second period. The service
        /// also closes the connection (status code 400) if it detects no speech for `inactivity_timeout` seconds of
        /// streaming audio; use the `inactivity_timeout` parameter to change the default of 30 seconds.
        ///
        /// **See also:**
        /// * [Audio transmission](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-input#transmission)
        /// * [Timeouts](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-input#timeouts)
        ///
        /// ### Audio formats (content types)
        ///
        ///  The service accepts audio in the following formats (MIME types).
        /// * For formats that are labeled **Required**, you must use the `Content-Type` header with the request to
        /// specify the format of the audio.
        /// * For all other formats, you can omit the `Content-Type` header or specify `application/octet-stream` with
        /// the header to have the service automatically detect the format of the audio. (With the `curl` command, you
        /// can specify either `"Content-Type:"` or `"Content-Type: application/octet-stream"`.)
        ///
        /// Where indicated, the format that you specify must include the sampling rate and can optionally include the
        /// number of channels and the endianness of the audio.
        /// * `audio/alaw` (**Required.** Specify the sampling rate (`rate`) of the audio.)
        /// * `audio/basic` (**Required.** Use only with narrowband models.)
        /// * `audio/flac`
        /// * `audio/g729` (Use only with narrowband models.)
        /// * `audio/l16` (**Required.** Specify the sampling rate (`rate`) and optionally the number of channels
        /// (`channels`) and endianness (`endianness`) of the audio.)
        /// * `audio/mp3`
        /// * `audio/mpeg`
        /// * `audio/mulaw` (**Required.** Specify the sampling rate (`rate`) of the audio.)
        /// * `audio/ogg` (The service automatically detects the codec of the input audio.)
        /// * `audio/ogg;codecs=opus`
        /// * `audio/ogg;codecs=vorbis`
        /// * `audio/wav` (Provide audio with a maximum of nine channels.)
        /// * `audio/webm` (The service automatically detects the codec of the input audio.)
        /// * `audio/webm;codecs=opus`
        /// * `audio/webm;codecs=vorbis`
        ///
        /// The sampling rate of the audio must match the sampling rate of the model for the recognition request: for
        /// broadband models, at least 16 kHz; for narrowband models, at least 8 kHz. If the sampling rate of the audio
        /// is higher than the minimum required rate, the service down-samples the audio to the appropriate rate. If the
        /// sampling rate of the audio is lower than the minimum required rate, the request fails.
        ///
        ///  **See also:** [Supported audio
        /// formats](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-audio-formats).
        ///
        /// ### Next-generation models
        ///
        ///  The service supports next-generation `Multimedia` (16 kHz) and `Telephony` (8 kHz) models for many
        /// languages. Next-generation models have higher throughput than the service's previous generation of
        /// `Broadband` and `Narrowband` models. When you use next-generation models, the service can return
        /// transcriptions more quickly and also provide noticeably better transcription accuracy.
        ///
        /// You specify a next-generation model by using the `model` query parameter, as you do a previous-generation
        /// model. Most next-generation models support the `low_latency` parameter, and all next-generation models
        /// support the `character_insertion_bias` parameter. These parameters are not available with
        /// previous-generation models.
        ///
        /// Next-generation models do not support all of the speech recognition parameters that are available for use
        /// with previous-generation models. Next-generation models do not support the following parameters:
        /// * `acoustic_customization_id`
        /// * `keywords` and `keywords_threshold`
        /// * `processing_metrics` and `processing_metrics_interval`
        /// * `word_alternatives_threshold`
        ///
        /// **Important:** Effective **31 July 2023**, all previous-generation models will be removed from the service
        /// and the documentation. Most previous-generation models were deprecated on 15 March 2022. You must migrate to
        /// the equivalent next-generation model by 31 July 2023. For more information, see [Migrating to
        /// next-generation models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-migrate).
        ///
        /// **See also:**
        /// * [Next-generation languages and
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-ng)
        /// * [Supported features for next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-ng#models-ng-features)
        ///
        /// ### Multipart speech recognition
        ///
        ///  **Note:** The asynchronous HTTP interface, WebSocket interface, and Watson SDKs do not support multipart
        /// speech recognition.
        ///
        /// The HTTP `POST` method of the service also supports multipart speech recognition. With multipart requests,
        /// you pass all audio data as multipart form data. You specify some parameters as request headers and query
        /// parameters, but you pass JSON metadata as form data to control most aspects of the transcription. You can
        /// use multipart recognition to pass multiple audio files with a single request.
        ///
        /// Use the multipart approach with browsers for which JavaScript is disabled or when the parameters used with
        /// the request are greater than the 8 KB limit imposed by most HTTP servers and proxies. You can encounter this
        /// limit, for example, if you want to spot a very large number of keywords.
        ///
        /// **See also:** [Making a multipart HTTP
        /// request](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-http#HTTP-multi).
        /// </summary>
        /// <param name="audio">The audio to transcribe.</param>
        /// <param name="contentType">The format (MIME type) of the audio. For more information about specifying an
        /// audio format, see **Audio formats (content types)** in the method description. (optional)</param>
        /// <param name="model">The model to use for speech recognition. If you omit the `model` parameter, the service
        /// uses the US English `en-US_BroadbandModel` by default.
        ///
        /// _For IBM Cloud Pak for Data,_ if you do not install the `en-US_BroadbandModel`, you must either specify a
        /// model with the request or specify a new default model for your installation of the service.
        ///
        /// **See also:**
        /// * [Using a model for speech
        /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-use)
        /// * [Using the default
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-use#models-use-default).
        /// (optional, default to en-US_BroadbandModel)</param>
        /// <param name="languageCustomizationId">The customization ID (GUID) of a custom language model that is to be
        /// used with the recognition request. The base model of the specified custom language model must match the
        /// model specified with the `model` parameter. You must make the request with credentials for the instance of
        /// the service that owns the custom model. By default, no custom language model is used. See [Using a custom
        /// language model for speech
        /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageUse).
        ///
        /// **Note:** Use this parameter instead of the deprecated `customization_id` parameter. (optional)</param>
        /// <param name="acousticCustomizationId">The customization ID (GUID) of a custom acoustic model that is to be
        /// used with the recognition request. The base model of the specified custom acoustic model must match the
        /// model specified with the `model` parameter. You must make the request with credentials for the instance of
        /// the service that owns the custom model. By default, no custom acoustic model is used. See [Using a custom
        /// acoustic model for speech
        /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-acousticUse). (optional)</param>
        /// <param name="baseModelVersion">The version of the specified base model that is to be used with the
        /// recognition request. Multiple versions of a base model can exist when a model is updated for internal
        /// improvements. The parameter is intended primarily for use with custom models that have been upgraded for a
        /// new base model. The default value depends on whether the parameter is used with or without a custom model.
        /// See [Making speech recognition requests with upgraded custom
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-upgrade-use#custom-upgrade-use-recognition).
        /// (optional)</param>
        /// <param name="customizationWeight">If you specify the customization ID (GUID) of a custom language model with
        /// the recognition request, the customization weight tells the service how much weight to give to words from
        /// the custom language model compared to those from the base model for the current request.
        ///
        /// Specify a value between 0.0 and 1.0. Unless a different customization weight was specified for the custom
        /// model when the model was trained, the default value is:
        /// * 0.3 for previous-generation models
        /// * 0.2 for most next-generation models
        /// * 0.1 for next-generation English and Japanese models
        ///
        /// A customization weight that you specify overrides a weight that was specified when the custom model was
        /// trained. The default value yields the best performance in general. Assign a higher value if your audio makes
        /// frequent use of OOV words from the custom model. Use caution when setting the weight: a higher value can
        /// improve the accuracy of phrases from the custom model's domain, but it can negatively affect performance on
        /// non-domain phrases.
        ///
        /// See [Using customization
        /// weight](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageUse#weight).
        /// (optional)</param>
        /// <param name="inactivityTimeout">The time in seconds after which, if only silence (no speech) is detected in
        /// streaming audio, the connection is closed with a 400 error. The parameter is useful for stopping audio
        /// submission from a live microphone when a user simply walks away. Use `-1` for infinity. See [Inactivity
        /// timeout](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-input#timeouts-inactivity).
        /// (optional)</param>
        /// <param name="keywords">An array of keyword strings to spot in the audio. Each keyword string can include one
        /// or more string tokens. Keywords are spotted only in the final results, not in interim hypotheses. If you
        /// specify any keywords, you must also specify a keywords threshold. Omit the parameter or specify an empty
        /// array if you do not need to spot keywords.
        ///
        /// You can spot a maximum of 1000 keywords with a single request. A single keyword can have a maximum length of
        /// 1024 characters, though the maximum effective length for double-byte languages might be shorter. Keywords
        /// are case-insensitive.
        ///
        /// See [Keyword
        /// spotting](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-spotting#keyword-spotting).
        /// (optional)</param>
        /// <param name="keywordsThreshold">A confidence value that is the lower bound for spotting a keyword. A word is
        /// considered to match a keyword if its confidence is greater than or equal to the threshold. Specify a
        /// probability between 0.0 and 1.0. If you specify a threshold, you must also specify one or more keywords. The
        /// service performs no keyword spotting if you omit either parameter. See [Keyword
        /// spotting](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-spotting#keyword-spotting).
        /// (optional)</param>
        /// <param name="maxAlternatives">The maximum number of alternative transcripts that the service is to return.
        /// By default, the service returns a single transcript. If you specify a value of `0`, the service uses the
        /// default value, `1`. See [Maximum
        /// alternatives](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metadata#max-alternatives).
        /// (optional)</param>
        /// <param name="wordAlternativesThreshold">A confidence value that is the lower bound for identifying a
        /// hypothesis as a possible word alternative (also known as "Confusion Networks"). An alternative word is
        /// considered if its confidence is greater than or equal to the threshold. Specify a probability between 0.0
        /// and 1.0. By default, the service computes no alternative words. See [Word
        /// alternatives](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-spotting#word-alternatives).
        /// (optional)</param>
        /// <param name="wordConfidence">If `true`, the service returns a confidence measure in the range of 0.0 to 1.0
        /// for each word. By default, the service returns no word confidence scores. See [Word
        /// confidence](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metadata#word-confidence).
        /// (optional, default to false)</param>
        /// <param name="timestamps">If `true`, the service returns time alignment for each word. By default, no
        /// timestamps are returned. See [Word
        /// timestamps](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metadata#word-timestamps).
        /// (optional, default to false)</param>
        /// <param name="profanityFilter">If `true`, the service filters profanity from all output except for keyword
        /// results by replacing inappropriate words with a series of asterisks. Set the parameter to `false` to return
        /// results with no censoring.
        ///
        /// **Note:** The parameter can be used with US English and Japanese transcription only. See [Profanity
        /// filtering](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-formatting#profanity-filtering).
        /// (optional, default to true)</param>
        /// <param name="smartFormatting">If `true`, the service converts dates, times, series of digits and numbers,
        /// phone numbers, currency values, and internet addresses into more readable, conventional representations in
        /// the final transcript of a recognition request. For US English, the service also converts certain keyword
        /// strings to punctuation symbols. By default, the service performs no smart formatting.
        ///
        /// **Note:** The parameter can be used with US English, Japanese, and Spanish (all dialects) transcription
        /// only.
        ///
        /// See [Smart
        /// formatting](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-formatting#smart-formatting).
        /// (optional, default to false)</param>
        /// <param name="speakerLabels">If `true`, the response includes labels that identify which words were spoken by
        /// which participants in a multi-person exchange. By default, the service returns no speaker labels. Setting
        /// `speaker_labels` to `true` forces the `timestamps` parameter to be `true`, regardless of whether you specify
        /// `false` for the parameter.
        /// * _For previous-generation models,_ the parameter can be used with Australian English, US English, German,
        /// Japanese, Korean, and Spanish (both broadband and narrowband models) and UK English (narrowband model)
        /// transcription only.
        /// * _For next-generation models,_ the parameter can be used with Czech, English (Australian, Indian, UK, and
        /// US), German, Japanese, Korean, and Spanish transcription only.
        ///
        /// See [Speaker labels](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-speaker-labels).
        /// (optional, default to false)</param>
        /// <param name="grammarName">The name of a grammar that is to be used with the recognition request. If you
        /// specify a grammar, you must also use the `language_customization_id` parameter to specify the name of the
        /// custom language model for which the grammar is defined. The service recognizes only strings that are
        /// recognized by the specified grammar; it does not recognize other custom words from the model's words
        /// resource.
        ///
        /// See [Using a grammar for speech
        /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-grammarUse). (optional)</param>
        /// <param name="redaction">If `true`, the service redacts, or masks, numeric data from final transcripts. The
        /// feature redacts any number that has three or more consecutive digits by replacing each digit with an `X`
        /// character. It is intended to redact sensitive numeric data, such as credit card numbers. By default, the
        /// service performs no redaction.
        ///
        /// When you enable redaction, the service automatically enables smart formatting, regardless of whether you
        /// explicitly disable that feature. To ensure maximum security, the service also disables keyword spotting
        /// (ignores the `keywords` and `keywords_threshold` parameters) and returns only a single final transcript
        /// (forces the `max_alternatives` parameter to be `1`).
        ///
        /// **Note:** The parameter can be used with US English, Japanese, and Korean transcription only.
        ///
        /// See [Numeric
        /// redaction](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-formatting#numeric-redaction).
        /// (optional, default to false)</param>
        /// <param name="audioMetrics">If `true`, requests detailed information about the signal characteristics of the
        /// input audio. The service returns audio metrics with the final transcription results. By default, the service
        /// returns no audio metrics.
        ///
        /// See [Audio metrics](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metrics#audio-metrics).
        /// (optional, default to false)</param>
        /// <param name="endOfPhraseSilenceTime">Specifies the duration of the pause interval at which the service
        /// splits a transcript into multiple final results. If the service detects pauses or extended silence before it
        /// reaches the end of the audio stream, its response can include multiple final results. Silence indicates a
        /// point at which the speaker pauses between spoken words or phrases.
        ///
        /// Specify a value for the pause interval in the range of 0.0 to 120.0.
        /// * A value greater than 0 specifies the interval that the service is to use for speech recognition.
        /// * A value of 0 indicates that the service is to use the default interval. It is equivalent to omitting the
        /// parameter.
        ///
        /// The default pause interval for most languages is 0.8 seconds; the default for Chinese is 0.6 seconds.
        ///
        /// See [End of phrase silence
        /// time](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-parsing#silence-time).
        /// (optional)</param>
        /// <param name="splitTranscriptAtPhraseEnd">If `true`, directs the service to split the transcript into
        /// multiple final results based on semantic features of the input, for example, at the conclusion of meaningful
        /// phrases such as sentences. The service bases its understanding of semantic features on the base language
        /// model that you use with a request. Custom language models and grammars can also influence how and where the
        /// service splits a transcript.
        ///
        /// By default, the service splits transcripts based solely on the pause interval. If the parameters are used
        /// together on the same request, `end_of_phrase_silence_time` has precedence over
        /// `split_transcript_at_phrase_end`.
        ///
        /// See [Split transcript at phrase
        /// end](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-parsing#split-transcript). (optional,
        /// default to false)</param>
        /// <param name="speechDetectorSensitivity">The sensitivity of speech activity detection that the service is to
        /// perform. Use the parameter to suppress word insertions from music, coughing, and other non-speech events.
        /// The service biases the audio it passes for speech recognition by evaluating the input audio against prior
        /// models of speech and non-speech activity.
        ///
        /// Specify a value between 0.0 and 1.0:
        /// * 0.0 suppresses all audio (no speech is transcribed).
        /// * 0.5 (the default) provides a reasonable compromise for the level of sensitivity.
        /// * 1.0 suppresses no audio (speech detection sensitivity is disabled).
        ///
        /// The values increase on a monotonic curve. Specifying one or two decimal places of precision (for example,
        /// `0.55`) is typically more than sufficient.
        ///
        /// The parameter is supported with all next-generation models and with most previous-generation models. See
        /// [Speech detector
        /// sensitivity](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-detection#detection-parameters-sensitivity)
        /// and [Language model
        /// support](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-detection#detection-support).
        /// (optional)</param>
        /// <param name="backgroundAudioSuppression">The level to which the service is to suppress background audio
        /// based on its volume to prevent it from being transcribed as speech. Use the parameter to suppress side
        /// conversations or background noise.
        ///
        /// Specify a value in the range of 0.0 to 1.0:
        /// * 0.0 (the default) provides no suppression (background audio suppression is disabled).
        /// * 0.5 provides a reasonable level of audio suppression for general usage.
        /// * 1.0 suppresses all audio (no audio is transcribed).
        ///
        /// The values increase on a monotonic curve. Specifying one or two decimal places of precision (for example,
        /// `0.55`) is typically more than sufficient.
        ///
        /// The parameter is supported with all next-generation models and with most previous-generation models. See
        /// [Background audio
        /// suppression](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-detection#detection-parameters-suppression)
        /// and [Language model
        /// support](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-detection#detection-support).
        /// (optional)</param>
        /// <param name="lowLatency">If `true` for next-generation `Multimedia` and `Telephony` models that support low
        /// latency, directs the service to produce results even more quickly than it usually does. Next-generation
        /// models produce transcription results faster than previous-generation models. The `low_latency` parameter
        /// causes the models to produce results even more quickly, though the results might be less accurate when the
        /// parameter is used.
        ///
        /// The parameter is not available for previous-generation `Broadband` and `Narrowband` models. It is available
        /// for most next-generation models.
        /// * For a list of next-generation models that support low latency, see [Supported next-generation language
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-ng#models-ng-supported).
        /// * For more information about the `low_latency` parameter, see [Low
        /// latency](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-interim#low-latency). (optional,
        /// default to false)</param>
        /// <param name="characterInsertionBias">For next-generation models, an indication of whether the service is
        /// biased to recognize shorter or longer strings of characters when developing transcription hypotheses. By
        /// default, the service is optimized to produce the best balance of strings of different lengths.
        ///
        /// The default bias is 0.0. The allowable range of values is -1.0 to 1.0.
        /// * Negative values bias the service to favor hypotheses with shorter strings of characters.
        /// * Positive values bias the service to favor hypotheses with longer strings of characters.
        ///
        /// As the value approaches -1.0 or 1.0, the impact of the parameter becomes more pronounced. To determine the
        /// most effective value for your scenario, start by setting the value of the parameter to a small increment,
        /// such as -0.1, -0.05, 0.05, or 0.1, and assess how the value impacts the transcription results. Then
        /// experiment with different values as necessary, adjusting the value by small increments.
        ///
        /// The parameter is not available for previous-generation models.
        ///
        /// See [Character insertion
        /// bias](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-parsing#insertion-bias).
        /// (optional)</param>
        /// <returns><see cref="SpeechRecognitionResults" />SpeechRecognitionResults</returns>
        public DetailedResponse<SpeechRecognitionResults> Recognize(System.IO.MemoryStream audio, string contentType = null, string model = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string grammarName = null, bool? redaction = null, bool? audioMetrics = null, double? endOfPhraseSilenceTime = null, bool? splitTranscriptAtPhraseEnd = null, float? speechDetectorSensitivity = null, float? backgroundAudioSuppression = null, bool? lowLatency = null, float? characterInsertionBias = null)
        {
            if (audio == null)
            {
                throw new ArgumentNullException("`audio` is required for `Recognize`");
            }
            DetailedResponse<SpeechRecognitionResults> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/recognize");

                restRequest.WithHeader("Accept", "application/json");

                if (!string.IsNullOrEmpty(contentType))
                {
                    restRequest.WithHeader("Content-Type", contentType);
                }
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }
                if (!string.IsNullOrEmpty(languageCustomizationId))
                {
                    restRequest.WithArgument("language_customization_id", languageCustomizationId);
                }
                if (!string.IsNullOrEmpty(acousticCustomizationId))
                {
                    restRequest.WithArgument("acoustic_customization_id", acousticCustomizationId);
                }
                if (!string.IsNullOrEmpty(baseModelVersion))
                {
                    restRequest.WithArgument("base_model_version", baseModelVersion);
                }
                if (customizationWeight != null)
                {
                    restRequest.WithArgument("customization_weight", customizationWeight);
                }
                if (inactivityTimeout != null)
                {
                    restRequest.WithArgument("inactivity_timeout", inactivityTimeout);
                }
                if (keywords != null && keywords.Count > 0)
                {
                    restRequest.WithArgument("keywords", string.Join(",", keywords.ToArray()));
                }
                if (keywordsThreshold != null)
                {
                    restRequest.WithArgument("keywords_threshold", keywordsThreshold);
                }
                if (maxAlternatives != null)
                {
                    restRequest.WithArgument("max_alternatives", maxAlternatives);
                }
                if (wordAlternativesThreshold != null)
                {
                    restRequest.WithArgument("word_alternatives_threshold", wordAlternativesThreshold);
                }
                if (wordConfidence != null)
                {
                    restRequest.WithArgument("word_confidence", wordConfidence);
                }
                if (timestamps != null)
                {
                    restRequest.WithArgument("timestamps", timestamps);
                }
                if (profanityFilter != null)
                {
                    restRequest.WithArgument("profanity_filter", profanityFilter);
                }
                if (smartFormatting != null)
                {
                    restRequest.WithArgument("smart_formatting", smartFormatting);
                }
                if (speakerLabels != null)
                {
                    restRequest.WithArgument("speaker_labels", speakerLabels);
                }
                if (!string.IsNullOrEmpty(grammarName))
                {
                    restRequest.WithArgument("grammar_name", grammarName);
                }
                if (redaction != null)
                {
                    restRequest.WithArgument("redaction", redaction);
                }
                if (audioMetrics != null)
                {
                    restRequest.WithArgument("audio_metrics", audioMetrics);
                }
                if (endOfPhraseSilenceTime != null)
                {
                    restRequest.WithArgument("end_of_phrase_silence_time", endOfPhraseSilenceTime);
                }
                if (splitTranscriptAtPhraseEnd != null)
                {
                    restRequest.WithArgument("split_transcript_at_phrase_end", splitTranscriptAtPhraseEnd);
                }
                if (speechDetectorSensitivity != null)
                {
                    restRequest.WithArgument("speech_detector_sensitivity", speechDetectorSensitivity);
                }
                if (backgroundAudioSuppression != null)
                {
                    restRequest.WithArgument("background_audio_suppression", backgroundAudioSuppression);
                }
                if (lowLatency != null)
                {
                    restRequest.WithArgument("low_latency", lowLatency);
                }
                if (characterInsertionBias != null)
                {
                    restRequest.WithArgument("character_insertion_bias", characterInsertionBias);
                }
                var httpContent = new ByteArrayContent(audio.ToArray());
                httpContent.Headers.Add("Content-Type", "application/octet-stream");
                System.Net.Http.Headers.MediaTypeHeaderValue audioContentType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out audioContentType);
                httpContent.Headers.ContentType = audioContentType;
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "Recognize"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<SpeechRecognitionResults>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<SpeechRecognitionResults>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for Recognize.
        /// </summary>
        public class RecognizeEnums
        {
            /// <summary>
            /// The format (MIME type) of the audio. For more information about specifying an audio format, see **Audio
            /// formats (content types)** in the method description.
            /// </summary>
            public class ContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_OCTET_STREAM for application/octet-stream
                /// </summary>
                public const string APPLICATION_OCTET_STREAM = "application/octet-stream";
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
                /// Constant AUDIO_G729 for audio/g729
                /// </summary>
                public const string AUDIO_G729 = "audio/g729";
                /// <summary>
                /// Constant AUDIO_L16 for audio/l16
                /// </summary>
                public const string AUDIO_L16 = "audio/l16";
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
            /// The model to use for speech recognition. If you omit the `model` parameter, the service uses the US
            /// English `en-US_BroadbandModel` by default.
            ///
            /// _For IBM Cloud Pak for Data,_ if you do not install the `en-US_BroadbandModel`, you must either specify
            /// a model with the request or specify a new default model for your installation of the service.
            ///
            /// **See also:**
            /// * [Using a model for speech
            /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-use)
            /// * [Using the default
            /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-use#models-use-default).
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant AR_MS_BROADBANDMODEL for ar-MS_BroadbandModel
                /// </summary>
                public const string AR_MS_BROADBANDMODEL = "ar-MS_BroadbandModel";
                /// <summary>
                /// Constant AR_MS_TELEPHONY for ar-MS_Telephony
                /// </summary>
                public const string AR_MS_TELEPHONY = "ar-MS_Telephony";
                /// <summary>
                /// Constant CS_CZ_TELEPHONY for cs-CZ_Telephony
                /// </summary>
                public const string CS_CZ_TELEPHONY = "cs-CZ_Telephony";
                /// <summary>
                /// Constant DE_DE_BROADBANDMODEL for de-DE_BroadbandModel
                /// </summary>
                public const string DE_DE_BROADBANDMODEL = "de-DE_BroadbandModel";
                /// <summary>
                /// Constant DE_DE_MULTIMEDIA for de-DE_Multimedia
                /// </summary>
                public const string DE_DE_MULTIMEDIA = "de-DE_Multimedia";
                /// <summary>
                /// Constant DE_DE_NARROWBANDMODEL for de-DE_NarrowbandModel
                /// </summary>
                public const string DE_DE_NARROWBANDMODEL = "de-DE_NarrowbandModel";
                /// <summary>
                /// Constant DE_DE_TELEPHONY for de-DE_Telephony
                /// </summary>
                public const string DE_DE_TELEPHONY = "de-DE_Telephony";
                /// <summary>
                /// Constant EN_AU_BROADBANDMODEL for en-AU_BroadbandModel
                /// </summary>
                public const string EN_AU_BROADBANDMODEL = "en-AU_BroadbandModel";
                /// <summary>
                /// Constant EN_AU_MULTIMEDIA for en-AU_Multimedia
                /// </summary>
                public const string EN_AU_MULTIMEDIA = "en-AU_Multimedia";
                /// <summary>
                /// Constant EN_AU_NARROWBANDMODEL for en-AU_NarrowbandModel
                /// </summary>
                public const string EN_AU_NARROWBANDMODEL = "en-AU_NarrowbandModel";
                /// <summary>
                /// Constant EN_AU_TELEPHONY for en-AU_Telephony
                /// </summary>
                public const string EN_AU_TELEPHONY = "en-AU_Telephony";
                /// <summary>
                /// Constant EN_IN_TELEPHONY for en-IN_Telephony
                /// </summary>
                public const string EN_IN_TELEPHONY = "en-IN_Telephony";
                /// <summary>
                /// Constant EN_GB_BROADBANDMODEL for en-GB_BroadbandModel
                /// </summary>
                public const string EN_GB_BROADBANDMODEL = "en-GB_BroadbandModel";
                /// <summary>
                /// Constant EN_GB_MULTIMEDIA for en-GB_Multimedia
                /// </summary>
                public const string EN_GB_MULTIMEDIA = "en-GB_Multimedia";
                /// <summary>
                /// Constant EN_GB_NARROWBANDMODEL for en-GB_NarrowbandModel
                /// </summary>
                public const string EN_GB_NARROWBANDMODEL = "en-GB_NarrowbandModel";
                /// <summary>
                /// Constant EN_GB_TELEPHONY for en-GB_Telephony
                /// </summary>
                public const string EN_GB_TELEPHONY = "en-GB_Telephony";
                /// <summary>
                /// Constant EN_US_BROADBANDMODEL for en-US_BroadbandModel
                /// </summary>
                public const string EN_US_BROADBANDMODEL = "en-US_BroadbandModel";
                /// <summary>
                /// Constant EN_US_MULTIMEDIA for en-US_Multimedia
                /// </summary>
                public const string EN_US_MULTIMEDIA = "en-US_Multimedia";
                /// <summary>
                /// Constant EN_US_NARROWBANDMODEL for en-US_NarrowbandModel
                /// </summary>
                public const string EN_US_NARROWBANDMODEL = "en-US_NarrowbandModel";
                /// <summary>
                /// Constant EN_US_SHORTFORM_NARROWBANDMODEL for en-US_ShortForm_NarrowbandModel
                /// </summary>
                public const string EN_US_SHORTFORM_NARROWBANDMODEL = "en-US_ShortForm_NarrowbandModel";
                /// <summary>
                /// Constant EN_US_TELEPHONY for en-US_Telephony
                /// </summary>
                public const string EN_US_TELEPHONY = "en-US_Telephony";
                /// <summary>
                /// Constant EN_WW_MEDICAL_TELEPHONY for en-WW_Medical_Telephony
                /// </summary>
                public const string EN_WW_MEDICAL_TELEPHONY = "en-WW_Medical_Telephony";
                /// <summary>
                /// Constant ES_AR_BROADBANDMODEL for es-AR_BroadbandModel
                /// </summary>
                public const string ES_AR_BROADBANDMODEL = "es-AR_BroadbandModel";
                /// <summary>
                /// Constant ES_AR_NARROWBANDMODEL for es-AR_NarrowbandModel
                /// </summary>
                public const string ES_AR_NARROWBANDMODEL = "es-AR_NarrowbandModel";
                /// <summary>
                /// Constant ES_CL_BROADBANDMODEL for es-CL_BroadbandModel
                /// </summary>
                public const string ES_CL_BROADBANDMODEL = "es-CL_BroadbandModel";
                /// <summary>
                /// Constant ES_CL_NARROWBANDMODEL for es-CL_NarrowbandModel
                /// </summary>
                public const string ES_CL_NARROWBANDMODEL = "es-CL_NarrowbandModel";
                /// <summary>
                /// Constant ES_CO_BROADBANDMODEL for es-CO_BroadbandModel
                /// </summary>
                public const string ES_CO_BROADBANDMODEL = "es-CO_BroadbandModel";
                /// <summary>
                /// Constant ES_CO_NARROWBANDMODEL for es-CO_NarrowbandModel
                /// </summary>
                public const string ES_CO_NARROWBANDMODEL = "es-CO_NarrowbandModel";
                /// <summary>
                /// Constant ES_ES_BROADBANDMODEL for es-ES_BroadbandModel
                /// </summary>
                public const string ES_ES_BROADBANDMODEL = "es-ES_BroadbandModel";
                /// <summary>
                /// Constant ES_ES_NARROWBANDMODEL for es-ES_NarrowbandModel
                /// </summary>
                public const string ES_ES_NARROWBANDMODEL = "es-ES_NarrowbandModel";
                /// <summary>
                /// Constant ES_ES_MULTIMEDIA for es-ES_Multimedia
                /// </summary>
                public const string ES_ES_MULTIMEDIA = "es-ES_Multimedia";
                /// <summary>
                /// Constant ES_ES_TELEPHONY for es-ES_Telephony
                /// </summary>
                public const string ES_ES_TELEPHONY = "es-ES_Telephony";
                /// <summary>
                /// Constant ES_LA_TELEPHONY for es-LA_Telephony
                /// </summary>
                public const string ES_LA_TELEPHONY = "es-LA_Telephony";
                /// <summary>
                /// Constant ES_MX_BROADBANDMODEL for es-MX_BroadbandModel
                /// </summary>
                public const string ES_MX_BROADBANDMODEL = "es-MX_BroadbandModel";
                /// <summary>
                /// Constant ES_MX_NARROWBANDMODEL for es-MX_NarrowbandModel
                /// </summary>
                public const string ES_MX_NARROWBANDMODEL = "es-MX_NarrowbandModel";
                /// <summary>
                /// Constant ES_PE_BROADBANDMODEL for es-PE_BroadbandModel
                /// </summary>
                public const string ES_PE_BROADBANDMODEL = "es-PE_BroadbandModel";
                /// <summary>
                /// Constant ES_PE_NARROWBANDMODEL for es-PE_NarrowbandModel
                /// </summary>
                public const string ES_PE_NARROWBANDMODEL = "es-PE_NarrowbandModel";
                /// <summary>
                /// Constant FR_CA_BROADBANDMODEL for fr-CA_BroadbandModel
                /// </summary>
                public const string FR_CA_BROADBANDMODEL = "fr-CA_BroadbandModel";
                /// <summary>
                /// Constant FR_CA_MULTIMEDIA for fr-CA_Multimedia
                /// </summary>
                public const string FR_CA_MULTIMEDIA = "fr-CA_Multimedia";
                /// <summary>
                /// Constant FR_CA_NARROWBANDMODEL for fr-CA_NarrowbandModel
                /// </summary>
                public const string FR_CA_NARROWBANDMODEL = "fr-CA_NarrowbandModel";
                /// <summary>
                /// Constant FR_CA_TELEPHONY for fr-CA_Telephony
                /// </summary>
                public const string FR_CA_TELEPHONY = "fr-CA_Telephony";
                /// <summary>
                /// Constant FR_FR_BROADBANDMODEL for fr-FR_BroadbandModel
                /// </summary>
                public const string FR_FR_BROADBANDMODEL = "fr-FR_BroadbandModel";
                /// <summary>
                /// Constant FR_FR_MULTIMEDIA for fr-FR_Multimedia
                /// </summary>
                public const string FR_FR_MULTIMEDIA = "fr-FR_Multimedia";
                /// <summary>
                /// Constant FR_FR_NARROWBANDMODEL for fr-FR_NarrowbandModel
                /// </summary>
                public const string FR_FR_NARROWBANDMODEL = "fr-FR_NarrowbandModel";
                /// <summary>
                /// Constant FR_FR_TELEPHONY for fr-FR_Telephony
                /// </summary>
                public const string FR_FR_TELEPHONY = "fr-FR_Telephony";
                /// <summary>
                /// Constant HI_IN_TELEPHONY for hi-IN_Telephony
                /// </summary>
                public const string HI_IN_TELEPHONY = "hi-IN_Telephony";
                /// <summary>
                /// Constant IT_IT_BROADBANDMODEL for it-IT_BroadbandModel
                /// </summary>
                public const string IT_IT_BROADBANDMODEL = "it-IT_BroadbandModel";
                /// <summary>
                /// Constant IT_IT_NARROWBANDMODEL for it-IT_NarrowbandModel
                /// </summary>
                public const string IT_IT_NARROWBANDMODEL = "it-IT_NarrowbandModel";
                /// <summary>
                /// Constant IT_IT_MULTIMEDIA for it-IT_Multimedia
                /// </summary>
                public const string IT_IT_MULTIMEDIA = "it-IT_Multimedia";
                /// <summary>
                /// Constant IT_IT_TELEPHONY for it-IT_Telephony
                /// </summary>
                public const string IT_IT_TELEPHONY = "it-IT_Telephony";
                /// <summary>
                /// Constant JA_JP_BROADBANDMODEL for ja-JP_BroadbandModel
                /// </summary>
                public const string JA_JP_BROADBANDMODEL = "ja-JP_BroadbandModel";
                /// <summary>
                /// Constant JA_JP_MULTIMEDIA for ja-JP_Multimedia
                /// </summary>
                public const string JA_JP_MULTIMEDIA = "ja-JP_Multimedia";
                /// <summary>
                /// Constant JA_JP_NARROWBANDMODEL for ja-JP_NarrowbandModel
                /// </summary>
                public const string JA_JP_NARROWBANDMODEL = "ja-JP_NarrowbandModel";
                /// <summary>
                /// Constant JA_JP_TELEPHONY for ja-JP_Telephony
                /// </summary>
                public const string JA_JP_TELEPHONY = "ja-JP_Telephony";
                /// <summary>
                /// Constant KO_KR_BROADBANDMODEL for ko-KR_BroadbandModel
                /// </summary>
                public const string KO_KR_BROADBANDMODEL = "ko-KR_BroadbandModel";
                /// <summary>
                /// Constant KO_KR_MULTIMEDIA for ko-KR_Multimedia
                /// </summary>
                public const string KO_KR_MULTIMEDIA = "ko-KR_Multimedia";
                /// <summary>
                /// Constant KO_KR_NARROWBANDMODEL for ko-KR_NarrowbandModel
                /// </summary>
                public const string KO_KR_NARROWBANDMODEL = "ko-KR_NarrowbandModel";
                /// <summary>
                /// Constant KO_KR_TELEPHONY for ko-KR_Telephony
                /// </summary>
                public const string KO_KR_TELEPHONY = "ko-KR_Telephony";
                /// <summary>
                /// Constant NL_BE_TELEPHONY for nl-BE_Telephony
                /// </summary>
                public const string NL_BE_TELEPHONY = "nl-BE_Telephony";
                /// <summary>
                /// Constant NL_NL_BROADBANDMODEL for nl-NL_BroadbandModel
                /// </summary>
                public const string NL_NL_BROADBANDMODEL = "nl-NL_BroadbandModel";
                /// <summary>
                /// Constant NL_NL_MULTIMEDIA for nl-NL_Multimedia
                /// </summary>
                public const string NL_NL_MULTIMEDIA = "nl-NL_Multimedia";
                /// <summary>
                /// Constant NL_NL_NARROWBANDMODEL for nl-NL_NarrowbandModel
                /// </summary>
                public const string NL_NL_NARROWBANDMODEL = "nl-NL_NarrowbandModel";
                /// <summary>
                /// Constant NL_NL_TELEPHONY for nl-NL_Telephony
                /// </summary>
                public const string NL_NL_TELEPHONY = "nl-NL_Telephony";
                /// <summary>
                /// Constant PT_BR_BROADBANDMODEL for pt-BR_BroadbandModel
                /// </summary>
                public const string PT_BR_BROADBANDMODEL = "pt-BR_BroadbandModel";
                /// <summary>
                /// Constant PT_BR_MULTIMEDIA for pt-BR_Multimedia
                /// </summary>
                public const string PT_BR_MULTIMEDIA = "pt-BR_Multimedia";
                /// <summary>
                /// Constant PT_BR_NARROWBANDMODEL for pt-BR_NarrowbandModel
                /// </summary>
                public const string PT_BR_NARROWBANDMODEL = "pt-BR_NarrowbandModel";
                /// <summary>
                /// Constant PT_BR_TELEPHONY for pt-BR_Telephony
                /// </summary>
                public const string PT_BR_TELEPHONY = "pt-BR_Telephony";
                /// <summary>
                /// Constant SV_SE_TELEPHONY for sv-SE_Telephony
                /// </summary>
                public const string SV_SE_TELEPHONY = "sv-SE_Telephony";
                /// <summary>
                /// Constant ZH_CN_BROADBANDMODEL for zh-CN_BroadbandModel
                /// </summary>
                public const string ZH_CN_BROADBANDMODEL = "zh-CN_BroadbandModel";
                /// <summary>
                /// Constant ZH_CN_NARROWBANDMODEL for zh-CN_NarrowbandModel
                /// </summary>
                public const string ZH_CN_NARROWBANDMODEL = "zh-CN_NarrowbandModel";
                /// <summary>
                /// Constant ZH_CN_TELEPHONY for zh-CN_Telephony
                /// </summary>
                public const string ZH_CN_TELEPHONY = "zh-CN_Telephony";
                
            }
        }
        /// <summary>
        /// Register a callback.
        ///
        /// Registers a callback URL with the service for use with subsequent asynchronous recognition requests. The
        /// service attempts to register, or allowlist, the callback URL if it is not already registered by sending a
        /// `GET` request to the callback URL. The service passes a random alphanumeric challenge string via the
        /// `challenge_string` parameter of the request. The request includes an `Accept` header that specifies
        /// `text/plain` as the required response type.
        ///
        /// To be registered successfully, the callback URL must respond to the `GET` request from the service. The
        /// response must send status code 200 and must include the challenge string in its body. Set the `Content-Type`
        /// response header to `text/plain`. Upon receiving this response, the service responds to the original
        /// registration request with response code 201.
        ///
        /// The service sends only a single `GET` request to the callback URL. If the service does not receive a reply
        /// with a response code of 200 and a body that echoes the challenge string sent by the service within five
        /// seconds, it does not allowlist the URL; it instead sends status code 400 in response to the request to
        /// register a callback. If the requested callback URL is already allowlisted, the service responds to the
        /// initial registration request with response code 200.
        ///
        /// If you specify a user secret with the request, the service uses it as a key to calculate an HMAC-SHA1
        /// signature of the challenge string in its response to the `POST` request. It sends this signature in the
        /// `X-Callback-Signature` header of its `GET` request to the URL during registration. It also uses the secret
        /// to calculate a signature over the payload of every callback notification that uses the URL. The signature
        /// provides authentication and data integrity for HTTP communications.
        ///
        /// After you successfully register a callback URL, you can use it with an indefinite number of recognition
        /// requests. You can register a maximum of 20 callback URLS in a one-hour span of time.
        ///
        /// **See also:** [Registering a callback
        /// URL](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-async#register).
        /// </summary>
        /// <param name="callbackUrl">An HTTP or HTTPS URL to which callback notifications are to be sent. To be
        /// allowlisted, the URL must successfully echo the challenge string during URL verification. During
        /// verification, the client can also check the signature that the service sends in the `X-Callback-Signature`
        /// header to verify the origin of the request.</param>
        /// <param name="userSecret">A user-specified string that the service uses to generate the HMAC-SHA1 signature
        /// that it sends via the `X-Callback-Signature` header. The service includes the header during URL verification
        /// and with every notification sent to the callback URL. It calculates the signature over the payload of the
        /// notification. If you omit the parameter, the service does not send the header. (optional)</param>
        /// <returns><see cref="RegisterStatus" />RegisterStatus</returns>
        public DetailedResponse<RegisterStatus> RegisterCallback(string callbackUrl, string userSecret = null)
        {
            if (string.IsNullOrEmpty(callbackUrl))
            {
                throw new ArgumentNullException("`callbackUrl` is required for `RegisterCallback`");
            }
            DetailedResponse<RegisterStatus> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/register_callback");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(callbackUrl))
                {
                    restRequest.WithArgument("callback_url", callbackUrl);
                }
                if (!string.IsNullOrEmpty(userSecret))
                {
                    restRequest.WithArgument("user_secret", userSecret);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "RegisterCallback"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<RegisterStatus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<RegisterStatus>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Unregister a callback.
        ///
        /// Unregisters a callback URL that was previously allowlisted with a [Register a callback](#registercallback)
        /// request for use with the asynchronous interface. Once unregistered, the URL can no longer be used with
        /// asynchronous recognition requests.
        ///
        /// **See also:** [Unregistering a callback
        /// URL](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-async#unregister).
        /// </summary>
        /// <param name="callbackUrl">The callback URL that is to be unregistered.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> UnregisterCallback(string callbackUrl)
        {
            if (string.IsNullOrEmpty(callbackUrl))
            {
                throw new ArgumentNullException("`callbackUrl` is required for `UnregisterCallback`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/unregister_callback");

                if (!string.IsNullOrEmpty(callbackUrl))
                {
                    restRequest.WithArgument("callback_url", callbackUrl);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "UnregisterCallback"));
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
        /// Create a job.
        ///
        /// Creates a job for a new asynchronous recognition request. The job is owned by the instance of the service
        /// whose credentials are used to create it. How you learn the status and results of a job depends on the
        /// parameters you include with the job creation request:
        /// * By callback notification: Include the `callback_url` parameter to specify a URL to which the service is to
        /// send callback notifications when the status of the job changes. Optionally, you can also include the
        /// `events` and `user_token` parameters to subscribe to specific events and to specify a string that is to be
        /// included with each notification for the job.
        /// * By polling the service: Omit the `callback_url`, `events`, and `user_token` parameters. You must then use
        /// the [Check jobs](#checkjobs) or [Check a job](#checkjob) methods to check the status of the job, using the
        /// latter to retrieve the results when the job is complete.
        ///
        /// The two approaches are not mutually exclusive. You can poll the service for job status or obtain results
        /// from the service manually even if you include a callback URL. In both cases, you can include the
        /// `results_ttl` parameter to specify how long the results are to remain available after the job is complete.
        /// Using the HTTPS [Check a job](#checkjob) method to retrieve results is more secure than receiving them via
        /// callback notification over HTTP because it provides confidentiality in addition to authentication and data
        /// integrity.
        ///
        /// The method supports the same basic parameters as other HTTP and WebSocket recognition requests. It also
        /// supports the following parameters specific to the asynchronous interface:
        /// * `callback_url`
        /// * `events`
        /// * `user_token`
        /// * `results_ttl`
        ///
        /// You can pass a maximum of 1 GB and a minimum of 100 bytes of audio with a request. The service automatically
        /// detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the
        /// audio to one-channel mono during transcoding. The method returns only final results; to enable interim
        /// results, use the WebSocket API. (With the `curl` command, use the `--data-binary` option to upload the file
        /// for the request.)
        ///
        /// **See also:** [Creating a job](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-async#create).
        ///
        ///
        /// ### Streaming mode
        ///
        ///  For requests to transcribe live audio as it becomes available, you must set the `Transfer-Encoding` header
        /// to `chunked` to use streaming mode. In streaming mode, the service closes the connection (status code 408)
        /// if it does not receive at least 15 seconds of audio (including silence) in any 30-second period. The service
        /// also closes the connection (status code 400) if it detects no speech for `inactivity_timeout` seconds of
        /// streaming audio; use the `inactivity_timeout` parameter to change the default of 30 seconds.
        ///
        /// **See also:**
        /// * [Audio transmission](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-input#transmission)
        /// * [Timeouts](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-input#timeouts)
        ///
        /// ### Audio formats (content types)
        ///
        ///  The service accepts audio in the following formats (MIME types).
        /// * For formats that are labeled **Required**, you must use the `Content-Type` header with the request to
        /// specify the format of the audio.
        /// * For all other formats, you can omit the `Content-Type` header or specify `application/octet-stream` with
        /// the header to have the service automatically detect the format of the audio. (With the `curl` command, you
        /// can specify either `"Content-Type:"` or `"Content-Type: application/octet-stream"`.)
        ///
        /// Where indicated, the format that you specify must include the sampling rate and can optionally include the
        /// number of channels and the endianness of the audio.
        /// * `audio/alaw` (**Required.** Specify the sampling rate (`rate`) of the audio.)
        /// * `audio/basic` (**Required.** Use only with narrowband models.)
        /// * `audio/flac`
        /// * `audio/g729` (Use only with narrowband models.)
        /// * `audio/l16` (**Required.** Specify the sampling rate (`rate`) and optionally the number of channels
        /// (`channels`) and endianness (`endianness`) of the audio.)
        /// * `audio/mp3`
        /// * `audio/mpeg`
        /// * `audio/mulaw` (**Required.** Specify the sampling rate (`rate`) of the audio.)
        /// * `audio/ogg` (The service automatically detects the codec of the input audio.)
        /// * `audio/ogg;codecs=opus`
        /// * `audio/ogg;codecs=vorbis`
        /// * `audio/wav` (Provide audio with a maximum of nine channels.)
        /// * `audio/webm` (The service automatically detects the codec of the input audio.)
        /// * `audio/webm;codecs=opus`
        /// * `audio/webm;codecs=vorbis`
        ///
        /// The sampling rate of the audio must match the sampling rate of the model for the recognition request: for
        /// broadband models, at least 16 kHz; for narrowband models, at least 8 kHz. If the sampling rate of the audio
        /// is higher than the minimum required rate, the service down-samples the audio to the appropriate rate. If the
        /// sampling rate of the audio is lower than the minimum required rate, the request fails.
        ///
        ///  **See also:** [Supported audio
        /// formats](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-audio-formats).
        ///
        /// ### Next-generation models
        ///
        ///  The service supports next-generation `Multimedia` (16 kHz) and `Telephony` (8 kHz) models for many
        /// languages. Next-generation models have higher throughput than the service's previous generation of
        /// `Broadband` and `Narrowband` models. When you use next-generation models, the service can return
        /// transcriptions more quickly and also provide noticeably better transcription accuracy.
        ///
        /// You specify a next-generation model by using the `model` query parameter, as you do a previous-generation
        /// model. Most next-generation models support the `low_latency` parameter, and all next-generation models
        /// support the `character_insertion_bias` parameter. These parameters are not available with
        /// previous-generation models.
        ///
        /// Next-generation models do not support all of the speech recognition parameters that are available for use
        /// with previous-generation models. Next-generation models do not support the following parameters:
        /// * `acoustic_customization_id`
        /// * `keywords` and `keywords_threshold`
        /// * `processing_metrics` and `processing_metrics_interval`
        /// * `word_alternatives_threshold`
        ///
        /// **Important:** Effective **31 July 2023**, all previous-generation models will be removed from the service
        /// and the documentation. Most previous-generation models were deprecated on 15 March 2022. You must migrate to
        /// the equivalent next-generation model by 31 July 2023. For more information, see [Migrating to
        /// next-generation models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-migrate).
        ///
        /// **See also:**
        /// * [Next-generation languages and
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-ng)
        /// * [Supported features for next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-ng#models-ng-features).
        /// </summary>
        /// <param name="audio">The audio to transcribe.</param>
        /// <param name="contentType">The format (MIME type) of the audio. For more information about specifying an
        /// audio format, see **Audio formats (content types)** in the method description. (optional)</param>
        /// <param name="model">The model to use for speech recognition. If you omit the `model` parameter, the service
        /// uses the US English `en-US_BroadbandModel` by default.
        ///
        /// _For IBM Cloud Pak for Data,_ if you do not install the `en-US_BroadbandModel`, you must either specify a
        /// model with the request or specify a new default model for your installation of the service.
        ///
        /// **See also:**
        /// * [Using a model for speech
        /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-use)
        /// * [Using the default
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-use#models-use-default).
        /// (optional, default to en-US_BroadbandModel)</param>
        /// <param name="callbackUrl">A URL to which callback notifications are to be sent. The URL must already be
        /// successfully allowlisted by using the [Register a callback](#registercallback) method. You can include the
        /// same callback URL with any number of job creation requests. Omit the parameter to poll the service for job
        /// completion and results.
        ///
        /// Use the `user_token` parameter to specify a unique user-specified string with each job to differentiate the
        /// callback notifications for the jobs. (optional)</param>
        /// <param name="events">If the job includes a callback URL, a comma-separated list of notification events to
        /// which to subscribe. Valid events are
        /// * `recognitions.started` generates a callback notification when the service begins to process the job.
        /// * `recognitions.completed` generates a callback notification when the job is complete. You must use the
        /// [Check a job](#checkjob) method to retrieve the results before they time out or are deleted.
        /// * `recognitions.completed_with_results` generates a callback notification when the job is complete. The
        /// notification includes the results of the request.
        /// * `recognitions.failed` generates a callback notification if the service experiences an error while
        /// processing the job.
        ///
        /// The `recognitions.completed` and `recognitions.completed_with_results` events are incompatible. You can
        /// specify only of the two events.
        ///
        /// If the job includes a callback URL, omit the parameter to subscribe to the default events:
        /// `recognitions.started`, `recognitions.completed`, and `recognitions.failed`. If the job does not include a
        /// callback URL, omit the parameter. (optional)</param>
        /// <param name="userToken">If the job includes a callback URL, a user-specified string that the service is to
        /// include with each callback notification for the job; the token allows the user to maintain an internal
        /// mapping between jobs and notification events. If the job does not include a callback URL, omit the
        /// parameter. (optional)</param>
        /// <param name="resultsTtl">The number of minutes for which the results are to be available after the job has
        /// finished. If not delivered via a callback, the results must be retrieved within this time. Omit the
        /// parameter to use a time to live of one week. The parameter is valid with or without a callback URL.
        /// (optional)</param>
        /// <param name="languageCustomizationId">The customization ID (GUID) of a custom language model that is to be
        /// used with the recognition request. The base model of the specified custom language model must match the
        /// model specified with the `model` parameter. You must make the request with credentials for the instance of
        /// the service that owns the custom model. By default, no custom language model is used. See [Using a custom
        /// language model for speech
        /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageUse).
        ///
        /// **Note:** Use this parameter instead of the deprecated `customization_id` parameter. (optional)</param>
        /// <param name="acousticCustomizationId">The customization ID (GUID) of a custom acoustic model that is to be
        /// used with the recognition request. The base model of the specified custom acoustic model must match the
        /// model specified with the `model` parameter. You must make the request with credentials for the instance of
        /// the service that owns the custom model. By default, no custom acoustic model is used. See [Using a custom
        /// acoustic model for speech
        /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-acousticUse). (optional)</param>
        /// <param name="baseModelVersion">The version of the specified base model that is to be used with the
        /// recognition request. Multiple versions of a base model can exist when a model is updated for internal
        /// improvements. The parameter is intended primarily for use with custom models that have been upgraded for a
        /// new base model. The default value depends on whether the parameter is used with or without a custom model.
        /// See [Making speech recognition requests with upgraded custom
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-upgrade-use#custom-upgrade-use-recognition).
        /// (optional)</param>
        /// <param name="customizationWeight">If you specify the customization ID (GUID) of a custom language model with
        /// the recognition request, the customization weight tells the service how much weight to give to words from
        /// the custom language model compared to those from the base model for the current request.
        ///
        /// Specify a value between 0.0 and 1.0. Unless a different customization weight was specified for the custom
        /// model when the model was trained, the default value is:
        /// * 0.3 for previous-generation models
        /// * 0.2 for most next-generation models
        /// * 0.1 for next-generation English and Japanese models
        ///
        /// A customization weight that you specify overrides a weight that was specified when the custom model was
        /// trained. The default value yields the best performance in general. Assign a higher value if your audio makes
        /// frequent use of OOV words from the custom model. Use caution when setting the weight: a higher value can
        /// improve the accuracy of phrases from the custom model's domain, but it can negatively affect performance on
        /// non-domain phrases.
        ///
        /// See [Using customization
        /// weight](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageUse#weight).
        /// (optional)</param>
        /// <param name="inactivityTimeout">The time in seconds after which, if only silence (no speech) is detected in
        /// streaming audio, the connection is closed with a 400 error. The parameter is useful for stopping audio
        /// submission from a live microphone when a user simply walks away. Use `-1` for infinity. See [Inactivity
        /// timeout](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-input#timeouts-inactivity).
        /// (optional)</param>
        /// <param name="keywords">An array of keyword strings to spot in the audio. Each keyword string can include one
        /// or more string tokens. Keywords are spotted only in the final results, not in interim hypotheses. If you
        /// specify any keywords, you must also specify a keywords threshold. Omit the parameter or specify an empty
        /// array if you do not need to spot keywords.
        ///
        /// You can spot a maximum of 1000 keywords with a single request. A single keyword can have a maximum length of
        /// 1024 characters, though the maximum effective length for double-byte languages might be shorter. Keywords
        /// are case-insensitive.
        ///
        /// See [Keyword
        /// spotting](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-spotting#keyword-spotting).
        /// (optional)</param>
        /// <param name="keywordsThreshold">A confidence value that is the lower bound for spotting a keyword. A word is
        /// considered to match a keyword if its confidence is greater than or equal to the threshold. Specify a
        /// probability between 0.0 and 1.0. If you specify a threshold, you must also specify one or more keywords. The
        /// service performs no keyword spotting if you omit either parameter. See [Keyword
        /// spotting](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-spotting#keyword-spotting).
        /// (optional)</param>
        /// <param name="maxAlternatives">The maximum number of alternative transcripts that the service is to return.
        /// By default, the service returns a single transcript. If you specify a value of `0`, the service uses the
        /// default value, `1`. See [Maximum
        /// alternatives](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metadata#max-alternatives).
        /// (optional)</param>
        /// <param name="wordAlternativesThreshold">A confidence value that is the lower bound for identifying a
        /// hypothesis as a possible word alternative (also known as "Confusion Networks"). An alternative word is
        /// considered if its confidence is greater than or equal to the threshold. Specify a probability between 0.0
        /// and 1.0. By default, the service computes no alternative words. See [Word
        /// alternatives](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-spotting#word-alternatives).
        /// (optional)</param>
        /// <param name="wordConfidence">If `true`, the service returns a confidence measure in the range of 0.0 to 1.0
        /// for each word. By default, the service returns no word confidence scores. See [Word
        /// confidence](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metadata#word-confidence).
        /// (optional, default to false)</param>
        /// <param name="timestamps">If `true`, the service returns time alignment for each word. By default, no
        /// timestamps are returned. See [Word
        /// timestamps](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metadata#word-timestamps).
        /// (optional, default to false)</param>
        /// <param name="profanityFilter">If `true`, the service filters profanity from all output except for keyword
        /// results by replacing inappropriate words with a series of asterisks. Set the parameter to `false` to return
        /// results with no censoring.
        ///
        /// **Note:** The parameter can be used with US English and Japanese transcription only. See [Profanity
        /// filtering](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-formatting#profanity-filtering).
        /// (optional, default to true)</param>
        /// <param name="smartFormatting">If `true`, the service converts dates, times, series of digits and numbers,
        /// phone numbers, currency values, and internet addresses into more readable, conventional representations in
        /// the final transcript of a recognition request. For US English, the service also converts certain keyword
        /// strings to punctuation symbols. By default, the service performs no smart formatting.
        ///
        /// **Note:** The parameter can be used with US English, Japanese, and Spanish (all dialects) transcription
        /// only.
        ///
        /// See [Smart
        /// formatting](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-formatting#smart-formatting).
        /// (optional, default to false)</param>
        /// <param name="speakerLabels">If `true`, the response includes labels that identify which words were spoken by
        /// which participants in a multi-person exchange. By default, the service returns no speaker labels. Setting
        /// `speaker_labels` to `true` forces the `timestamps` parameter to be `true`, regardless of whether you specify
        /// `false` for the parameter.
        /// * _For previous-generation models,_ the parameter can be used with Australian English, US English, German,
        /// Japanese, Korean, and Spanish (both broadband and narrowband models) and UK English (narrowband model)
        /// transcription only.
        /// * _For next-generation models,_ the parameter can be used with Czech, English (Australian, Indian, UK, and
        /// US), German, Japanese, Korean, and Spanish transcription only.
        ///
        /// See [Speaker labels](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-speaker-labels).
        /// (optional, default to false)</param>
        /// <param name="grammarName">The name of a grammar that is to be used with the recognition request. If you
        /// specify a grammar, you must also use the `language_customization_id` parameter to specify the name of the
        /// custom language model for which the grammar is defined. The service recognizes only strings that are
        /// recognized by the specified grammar; it does not recognize other custom words from the model's words
        /// resource.
        ///
        /// See [Using a grammar for speech
        /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-grammarUse). (optional)</param>
        /// <param name="redaction">If `true`, the service redacts, or masks, numeric data from final transcripts. The
        /// feature redacts any number that has three or more consecutive digits by replacing each digit with an `X`
        /// character. It is intended to redact sensitive numeric data, such as credit card numbers. By default, the
        /// service performs no redaction.
        ///
        /// When you enable redaction, the service automatically enables smart formatting, regardless of whether you
        /// explicitly disable that feature. To ensure maximum security, the service also disables keyword spotting
        /// (ignores the `keywords` and `keywords_threshold` parameters) and returns only a single final transcript
        /// (forces the `max_alternatives` parameter to be `1`).
        ///
        /// **Note:** The parameter can be used with US English, Japanese, and Korean transcription only.
        ///
        /// See [Numeric
        /// redaction](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-formatting#numeric-redaction).
        /// (optional, default to false)</param>
        /// <param name="processingMetrics">If `true`, requests processing metrics about the service's transcription of
        /// the input audio. The service returns processing metrics at the interval specified by the
        /// `processing_metrics_interval` parameter. It also returns processing metrics for transcription events, for
        /// example, for final and interim results. By default, the service returns no processing metrics.
        ///
        /// See [Processing
        /// metrics](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metrics#processing-metrics).
        /// (optional, default to false)</param>
        /// <param name="processingMetricsInterval">Specifies the interval in real wall-clock seconds at which the
        /// service is to return processing metrics. The parameter is ignored unless the `processing_metrics` parameter
        /// is set to `true`.
        ///
        /// The parameter accepts a minimum value of 0.1 seconds. The level of precision is not restricted, so you can
        /// specify values such as 0.25 and 0.125.
        ///
        /// The service does not impose a maximum value. If you want to receive processing metrics only for
        /// transcription events instead of at periodic intervals, set the value to a large number. If the value is
        /// larger than the duration of the audio, the service returns processing metrics only for transcription events.
        ///
        ///
        /// See [Processing
        /// metrics](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metrics#processing-metrics).
        /// (optional)</param>
        /// <param name="audioMetrics">If `true`, requests detailed information about the signal characteristics of the
        /// input audio. The service returns audio metrics with the final transcription results. By default, the service
        /// returns no audio metrics.
        ///
        /// See [Audio metrics](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-metrics#audio-metrics).
        /// (optional, default to false)</param>
        /// <param name="endOfPhraseSilenceTime">Specifies the duration of the pause interval at which the service
        /// splits a transcript into multiple final results. If the service detects pauses or extended silence before it
        /// reaches the end of the audio stream, its response can include multiple final results. Silence indicates a
        /// point at which the speaker pauses between spoken words or phrases.
        ///
        /// Specify a value for the pause interval in the range of 0.0 to 120.0.
        /// * A value greater than 0 specifies the interval that the service is to use for speech recognition.
        /// * A value of 0 indicates that the service is to use the default interval. It is equivalent to omitting the
        /// parameter.
        ///
        /// The default pause interval for most languages is 0.8 seconds; the default for Chinese is 0.6 seconds.
        ///
        /// See [End of phrase silence
        /// time](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-parsing#silence-time).
        /// (optional)</param>
        /// <param name="splitTranscriptAtPhraseEnd">If `true`, directs the service to split the transcript into
        /// multiple final results based on semantic features of the input, for example, at the conclusion of meaningful
        /// phrases such as sentences. The service bases its understanding of semantic features on the base language
        /// model that you use with a request. Custom language models and grammars can also influence how and where the
        /// service splits a transcript.
        ///
        /// By default, the service splits transcripts based solely on the pause interval. If the parameters are used
        /// together on the same request, `end_of_phrase_silence_time` has precedence over
        /// `split_transcript_at_phrase_end`.
        ///
        /// See [Split transcript at phrase
        /// end](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-parsing#split-transcript). (optional,
        /// default to false)</param>
        /// <param name="speechDetectorSensitivity">The sensitivity of speech activity detection that the service is to
        /// perform. Use the parameter to suppress word insertions from music, coughing, and other non-speech events.
        /// The service biases the audio it passes for speech recognition by evaluating the input audio against prior
        /// models of speech and non-speech activity.
        ///
        /// Specify a value between 0.0 and 1.0:
        /// * 0.0 suppresses all audio (no speech is transcribed).
        /// * 0.5 (the default) provides a reasonable compromise for the level of sensitivity.
        /// * 1.0 suppresses no audio (speech detection sensitivity is disabled).
        ///
        /// The values increase on a monotonic curve. Specifying one or two decimal places of precision (for example,
        /// `0.55`) is typically more than sufficient.
        ///
        /// The parameter is supported with all next-generation models and with most previous-generation models. See
        /// [Speech detector
        /// sensitivity](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-detection#detection-parameters-sensitivity)
        /// and [Language model
        /// support](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-detection#detection-support).
        /// (optional)</param>
        /// <param name="backgroundAudioSuppression">The level to which the service is to suppress background audio
        /// based on its volume to prevent it from being transcribed as speech. Use the parameter to suppress side
        /// conversations or background noise.
        ///
        /// Specify a value in the range of 0.0 to 1.0:
        /// * 0.0 (the default) provides no suppression (background audio suppression is disabled).
        /// * 0.5 provides a reasonable level of audio suppression for general usage.
        /// * 1.0 suppresses all audio (no audio is transcribed).
        ///
        /// The values increase on a monotonic curve. Specifying one or two decimal places of precision (for example,
        /// `0.55`) is typically more than sufficient.
        ///
        /// The parameter is supported with all next-generation models and with most previous-generation models. See
        /// [Background audio
        /// suppression](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-detection#detection-parameters-suppression)
        /// and [Language model
        /// support](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-detection#detection-support).
        /// (optional)</param>
        /// <param name="lowLatency">If `true` for next-generation `Multimedia` and `Telephony` models that support low
        /// latency, directs the service to produce results even more quickly than it usually does. Next-generation
        /// models produce transcription results faster than previous-generation models. The `low_latency` parameter
        /// causes the models to produce results even more quickly, though the results might be less accurate when the
        /// parameter is used.
        ///
        /// The parameter is not available for previous-generation `Broadband` and `Narrowband` models. It is available
        /// for most next-generation models.
        /// * For a list of next-generation models that support low latency, see [Supported next-generation language
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-ng#models-ng-supported).
        /// * For more information about the `low_latency` parameter, see [Low
        /// latency](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-interim#low-latency). (optional,
        /// default to false)</param>
        /// <param name="characterInsertionBias">For next-generation models, an indication of whether the service is
        /// biased to recognize shorter or longer strings of characters when developing transcription hypotheses. By
        /// default, the service is optimized to produce the best balance of strings of different lengths.
        ///
        /// The default bias is 0.0. The allowable range of values is -1.0 to 1.0.
        /// * Negative values bias the service to favor hypotheses with shorter strings of characters.
        /// * Positive values bias the service to favor hypotheses with longer strings of characters.
        ///
        /// As the value approaches -1.0 or 1.0, the impact of the parameter becomes more pronounced. To determine the
        /// most effective value for your scenario, start by setting the value of the parameter to a small increment,
        /// such as -0.1, -0.05, 0.05, or 0.1, and assess how the value impacts the transcription results. Then
        /// experiment with different values as necessary, adjusting the value by small increments.
        ///
        /// The parameter is not available for previous-generation models.
        ///
        /// See [Character insertion
        /// bias](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-parsing#insertion-bias).
        /// (optional)</param>
        /// <returns><see cref="RecognitionJob" />RecognitionJob</returns>
        public DetailedResponse<RecognitionJob> CreateJob(System.IO.MemoryStream audio, string contentType = null, string model = null, string callbackUrl = null, string events = null, string userToken = null, long? resultsTtl = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string grammarName = null, bool? redaction = null, bool? processingMetrics = null, float? processingMetricsInterval = null, bool? audioMetrics = null, double? endOfPhraseSilenceTime = null, bool? splitTranscriptAtPhraseEnd = null, float? speechDetectorSensitivity = null, float? backgroundAudioSuppression = null, bool? lowLatency = null, float? characterInsertionBias = null)
        {
            if (audio == null)
            {
                throw new ArgumentNullException("`audio` is required for `CreateJob`");
            }
            DetailedResponse<RecognitionJob> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/recognitions");

                restRequest.WithHeader("Accept", "application/json");

                if (!string.IsNullOrEmpty(contentType))
                {
                    restRequest.WithHeader("Content-Type", contentType);
                }
                if (!string.IsNullOrEmpty(model))
                {
                    restRequest.WithArgument("model", model);
                }
                if (!string.IsNullOrEmpty(callbackUrl))
                {
                    restRequest.WithArgument("callback_url", callbackUrl);
                }
                if (!string.IsNullOrEmpty(events))
                {
                    restRequest.WithArgument("events", events);
                }
                if (!string.IsNullOrEmpty(userToken))
                {
                    restRequest.WithArgument("user_token", userToken);
                }
                if (resultsTtl != null)
                {
                    restRequest.WithArgument("results_ttl", resultsTtl);
                }
                if (!string.IsNullOrEmpty(languageCustomizationId))
                {
                    restRequest.WithArgument("language_customization_id", languageCustomizationId);
                }
                if (!string.IsNullOrEmpty(acousticCustomizationId))
                {
                    restRequest.WithArgument("acoustic_customization_id", acousticCustomizationId);
                }
                if (!string.IsNullOrEmpty(baseModelVersion))
                {
                    restRequest.WithArgument("base_model_version", baseModelVersion);
                }
                if (customizationWeight != null)
                {
                    restRequest.WithArgument("customization_weight", customizationWeight);
                }
                if (inactivityTimeout != null)
                {
                    restRequest.WithArgument("inactivity_timeout", inactivityTimeout);
                }
                if (keywords != null && keywords.Count > 0)
                {
                    restRequest.WithArgument("keywords", string.Join(",", keywords.ToArray()));
                }
                if (keywordsThreshold != null)
                {
                    restRequest.WithArgument("keywords_threshold", keywordsThreshold);
                }
                if (maxAlternatives != null)
                {
                    restRequest.WithArgument("max_alternatives", maxAlternatives);
                }
                if (wordAlternativesThreshold != null)
                {
                    restRequest.WithArgument("word_alternatives_threshold", wordAlternativesThreshold);
                }
                if (wordConfidence != null)
                {
                    restRequest.WithArgument("word_confidence", wordConfidence);
                }
                if (timestamps != null)
                {
                    restRequest.WithArgument("timestamps", timestamps);
                }
                if (profanityFilter != null)
                {
                    restRequest.WithArgument("profanity_filter", profanityFilter);
                }
                if (smartFormatting != null)
                {
                    restRequest.WithArgument("smart_formatting", smartFormatting);
                }
                if (speakerLabels != null)
                {
                    restRequest.WithArgument("speaker_labels", speakerLabels);
                }
                if (!string.IsNullOrEmpty(grammarName))
                {
                    restRequest.WithArgument("grammar_name", grammarName);
                }
                if (redaction != null)
                {
                    restRequest.WithArgument("redaction", redaction);
                }
                if (processingMetrics != null)
                {
                    restRequest.WithArgument("processing_metrics", processingMetrics);
                }
                if (processingMetricsInterval != null)
                {
                    restRequest.WithArgument("processing_metrics_interval", processingMetricsInterval);
                }
                if (audioMetrics != null)
                {
                    restRequest.WithArgument("audio_metrics", audioMetrics);
                }
                if (endOfPhraseSilenceTime != null)
                {
                    restRequest.WithArgument("end_of_phrase_silence_time", endOfPhraseSilenceTime);
                }
                if (splitTranscriptAtPhraseEnd != null)
                {
                    restRequest.WithArgument("split_transcript_at_phrase_end", splitTranscriptAtPhraseEnd);
                }
                if (speechDetectorSensitivity != null)
                {
                    restRequest.WithArgument("speech_detector_sensitivity", speechDetectorSensitivity);
                }
                if (backgroundAudioSuppression != null)
                {
                    restRequest.WithArgument("background_audio_suppression", backgroundAudioSuppression);
                }
                if (lowLatency != null)
                {
                    restRequest.WithArgument("low_latency", lowLatency);
                }
                if (characterInsertionBias != null)
                {
                    restRequest.WithArgument("character_insertion_bias", characterInsertionBias);
                }
                var httpContent = new ByteArrayContent(audio.ToArray());
                httpContent.Headers.Add("Content-Type", "application/octet-stream");
                System.Net.Http.Headers.MediaTypeHeaderValue audioContentType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out audioContentType);
                httpContent.Headers.ContentType = audioContentType;
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "CreateJob"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<RecognitionJob>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<RecognitionJob>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for CreateJob.
        /// </summary>
        public class CreateJobEnums
        {
            /// <summary>
            /// The format (MIME type) of the audio. For more information about specifying an audio format, see **Audio
            /// formats (content types)** in the method description.
            /// </summary>
            public class ContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_OCTET_STREAM for application/octet-stream
                /// </summary>
                public const string APPLICATION_OCTET_STREAM = "application/octet-stream";
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
                /// Constant AUDIO_G729 for audio/g729
                /// </summary>
                public const string AUDIO_G729 = "audio/g729";
                /// <summary>
                /// Constant AUDIO_L16 for audio/l16
                /// </summary>
                public const string AUDIO_L16 = "audio/l16";
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
            /// The model to use for speech recognition. If you omit the `model` parameter, the service uses the US
            /// English `en-US_BroadbandModel` by default.
            ///
            /// _For IBM Cloud Pak for Data,_ if you do not install the `en-US_BroadbandModel`, you must either specify
            /// a model with the request or specify a new default model for your installation of the service.
            ///
            /// **See also:**
            /// * [Using a model for speech
            /// recognition](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-use)
            /// * [Using the default
            /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-use#models-use-default).
            /// </summary>
            public class ModelValue
            {
                /// <summary>
                /// Constant AR_MS_BROADBANDMODEL for ar-MS_BroadbandModel
                /// </summary>
                public const string AR_MS_BROADBANDMODEL = "ar-MS_BroadbandModel";
                /// <summary>
                /// Constant AR_MS_TELEPHONY for ar-MS_Telephony
                /// </summary>
                public const string AR_MS_TELEPHONY = "ar-MS_Telephony";
                /// <summary>
                /// Constant CS_CZ_TELEPHONY for cs-CZ_Telephony
                /// </summary>
                public const string CS_CZ_TELEPHONY = "cs-CZ_Telephony";
                /// <summary>
                /// Constant DE_DE_BROADBANDMODEL for de-DE_BroadbandModel
                /// </summary>
                public const string DE_DE_BROADBANDMODEL = "de-DE_BroadbandModel";
                /// <summary>
                /// Constant DE_DE_MULTIMEDIA for de-DE_Multimedia
                /// </summary>
                public const string DE_DE_MULTIMEDIA = "de-DE_Multimedia";
                /// <summary>
                /// Constant DE_DE_NARROWBANDMODEL for de-DE_NarrowbandModel
                /// </summary>
                public const string DE_DE_NARROWBANDMODEL = "de-DE_NarrowbandModel";
                /// <summary>
                /// Constant DE_DE_TELEPHONY for de-DE_Telephony
                /// </summary>
                public const string DE_DE_TELEPHONY = "de-DE_Telephony";
                /// <summary>
                /// Constant EN_AU_BROADBANDMODEL for en-AU_BroadbandModel
                /// </summary>
                public const string EN_AU_BROADBANDMODEL = "en-AU_BroadbandModel";
                /// <summary>
                /// Constant EN_AU_MULTIMEDIA for en-AU_Multimedia
                /// </summary>
                public const string EN_AU_MULTIMEDIA = "en-AU_Multimedia";
                /// <summary>
                /// Constant EN_AU_NARROWBANDMODEL for en-AU_NarrowbandModel
                /// </summary>
                public const string EN_AU_NARROWBANDMODEL = "en-AU_NarrowbandModel";
                /// <summary>
                /// Constant EN_AU_TELEPHONY for en-AU_Telephony
                /// </summary>
                public const string EN_AU_TELEPHONY = "en-AU_Telephony";
                /// <summary>
                /// Constant EN_IN_TELEPHONY for en-IN_Telephony
                /// </summary>
                public const string EN_IN_TELEPHONY = "en-IN_Telephony";
                /// <summary>
                /// Constant EN_GB_BROADBANDMODEL for en-GB_BroadbandModel
                /// </summary>
                public const string EN_GB_BROADBANDMODEL = "en-GB_BroadbandModel";
                /// <summary>
                /// Constant EN_GB_MULTIMEDIA for en-GB_Multimedia
                /// </summary>
                public const string EN_GB_MULTIMEDIA = "en-GB_Multimedia";
                /// <summary>
                /// Constant EN_GB_NARROWBANDMODEL for en-GB_NarrowbandModel
                /// </summary>
                public const string EN_GB_NARROWBANDMODEL = "en-GB_NarrowbandModel";
                /// <summary>
                /// Constant EN_GB_TELEPHONY for en-GB_Telephony
                /// </summary>
                public const string EN_GB_TELEPHONY = "en-GB_Telephony";
                /// <summary>
                /// Constant EN_US_BROADBANDMODEL for en-US_BroadbandModel
                /// </summary>
                public const string EN_US_BROADBANDMODEL = "en-US_BroadbandModel";
                /// <summary>
                /// Constant EN_US_MULTIMEDIA for en-US_Multimedia
                /// </summary>
                public const string EN_US_MULTIMEDIA = "en-US_Multimedia";
                /// <summary>
                /// Constant EN_US_NARROWBANDMODEL for en-US_NarrowbandModel
                /// </summary>
                public const string EN_US_NARROWBANDMODEL = "en-US_NarrowbandModel";
                /// <summary>
                /// Constant EN_US_SHORTFORM_NARROWBANDMODEL for en-US_ShortForm_NarrowbandModel
                /// </summary>
                public const string EN_US_SHORTFORM_NARROWBANDMODEL = "en-US_ShortForm_NarrowbandModel";
                /// <summary>
                /// Constant EN_US_TELEPHONY for en-US_Telephony
                /// </summary>
                public const string EN_US_TELEPHONY = "en-US_Telephony";
                /// <summary>
                /// Constant EN_WW_MEDICAL_TELEPHONY for en-WW_Medical_Telephony
                /// </summary>
                public const string EN_WW_MEDICAL_TELEPHONY = "en-WW_Medical_Telephony";
                /// <summary>
                /// Constant ES_AR_BROADBANDMODEL for es-AR_BroadbandModel
                /// </summary>
                public const string ES_AR_BROADBANDMODEL = "es-AR_BroadbandModel";
                /// <summary>
                /// Constant ES_AR_NARROWBANDMODEL for es-AR_NarrowbandModel
                /// </summary>
                public const string ES_AR_NARROWBANDMODEL = "es-AR_NarrowbandModel";
                /// <summary>
                /// Constant ES_CL_BROADBANDMODEL for es-CL_BroadbandModel
                /// </summary>
                public const string ES_CL_BROADBANDMODEL = "es-CL_BroadbandModel";
                /// <summary>
                /// Constant ES_CL_NARROWBANDMODEL for es-CL_NarrowbandModel
                /// </summary>
                public const string ES_CL_NARROWBANDMODEL = "es-CL_NarrowbandModel";
                /// <summary>
                /// Constant ES_CO_BROADBANDMODEL for es-CO_BroadbandModel
                /// </summary>
                public const string ES_CO_BROADBANDMODEL = "es-CO_BroadbandModel";
                /// <summary>
                /// Constant ES_CO_NARROWBANDMODEL for es-CO_NarrowbandModel
                /// </summary>
                public const string ES_CO_NARROWBANDMODEL = "es-CO_NarrowbandModel";
                /// <summary>
                /// Constant ES_ES_BROADBANDMODEL for es-ES_BroadbandModel
                /// </summary>
                public const string ES_ES_BROADBANDMODEL = "es-ES_BroadbandModel";
                /// <summary>
                /// Constant ES_ES_NARROWBANDMODEL for es-ES_NarrowbandModel
                /// </summary>
                public const string ES_ES_NARROWBANDMODEL = "es-ES_NarrowbandModel";
                /// <summary>
                /// Constant ES_ES_MULTIMEDIA for es-ES_Multimedia
                /// </summary>
                public const string ES_ES_MULTIMEDIA = "es-ES_Multimedia";
                /// <summary>
                /// Constant ES_ES_TELEPHONY for es-ES_Telephony
                /// </summary>
                public const string ES_ES_TELEPHONY = "es-ES_Telephony";
                /// <summary>
                /// Constant ES_LA_TELEPHONY for es-LA_Telephony
                /// </summary>
                public const string ES_LA_TELEPHONY = "es-LA_Telephony";
                /// <summary>
                /// Constant ES_MX_BROADBANDMODEL for es-MX_BroadbandModel
                /// </summary>
                public const string ES_MX_BROADBANDMODEL = "es-MX_BroadbandModel";
                /// <summary>
                /// Constant ES_MX_NARROWBANDMODEL for es-MX_NarrowbandModel
                /// </summary>
                public const string ES_MX_NARROWBANDMODEL = "es-MX_NarrowbandModel";
                /// <summary>
                /// Constant ES_PE_BROADBANDMODEL for es-PE_BroadbandModel
                /// </summary>
                public const string ES_PE_BROADBANDMODEL = "es-PE_BroadbandModel";
                /// <summary>
                /// Constant ES_PE_NARROWBANDMODEL for es-PE_NarrowbandModel
                /// </summary>
                public const string ES_PE_NARROWBANDMODEL = "es-PE_NarrowbandModel";
                /// <summary>
                /// Constant FR_CA_BROADBANDMODEL for fr-CA_BroadbandModel
                /// </summary>
                public const string FR_CA_BROADBANDMODEL = "fr-CA_BroadbandModel";
                /// <summary>
                /// Constant FR_CA_MULTIMEDIA for fr-CA_Multimedia
                /// </summary>
                public const string FR_CA_MULTIMEDIA = "fr-CA_Multimedia";
                /// <summary>
                /// Constant FR_CA_NARROWBANDMODEL for fr-CA_NarrowbandModel
                /// </summary>
                public const string FR_CA_NARROWBANDMODEL = "fr-CA_NarrowbandModel";
                /// <summary>
                /// Constant FR_CA_TELEPHONY for fr-CA_Telephony
                /// </summary>
                public const string FR_CA_TELEPHONY = "fr-CA_Telephony";
                /// <summary>
                /// Constant FR_FR_BROADBANDMODEL for fr-FR_BroadbandModel
                /// </summary>
                public const string FR_FR_BROADBANDMODEL = "fr-FR_BroadbandModel";
                /// <summary>
                /// Constant FR_FR_MULTIMEDIA for fr-FR_Multimedia
                /// </summary>
                public const string FR_FR_MULTIMEDIA = "fr-FR_Multimedia";
                /// <summary>
                /// Constant FR_FR_NARROWBANDMODEL for fr-FR_NarrowbandModel
                /// </summary>
                public const string FR_FR_NARROWBANDMODEL = "fr-FR_NarrowbandModel";
                /// <summary>
                /// Constant FR_FR_TELEPHONY for fr-FR_Telephony
                /// </summary>
                public const string FR_FR_TELEPHONY = "fr-FR_Telephony";
                /// <summary>
                /// Constant HI_IN_TELEPHONY for hi-IN_Telephony
                /// </summary>
                public const string HI_IN_TELEPHONY = "hi-IN_Telephony";
                /// <summary>
                /// Constant IT_IT_BROADBANDMODEL for it-IT_BroadbandModel
                /// </summary>
                public const string IT_IT_BROADBANDMODEL = "it-IT_BroadbandModel";
                /// <summary>
                /// Constant IT_IT_NARROWBANDMODEL for it-IT_NarrowbandModel
                /// </summary>
                public const string IT_IT_NARROWBANDMODEL = "it-IT_NarrowbandModel";
                /// <summary>
                /// Constant IT_IT_MULTIMEDIA for it-IT_Multimedia
                /// </summary>
                public const string IT_IT_MULTIMEDIA = "it-IT_Multimedia";
                /// <summary>
                /// Constant IT_IT_TELEPHONY for it-IT_Telephony
                /// </summary>
                public const string IT_IT_TELEPHONY = "it-IT_Telephony";
                /// <summary>
                /// Constant JA_JP_BROADBANDMODEL for ja-JP_BroadbandModel
                /// </summary>
                public const string JA_JP_BROADBANDMODEL = "ja-JP_BroadbandModel";
                /// <summary>
                /// Constant JA_JP_MULTIMEDIA for ja-JP_Multimedia
                /// </summary>
                public const string JA_JP_MULTIMEDIA = "ja-JP_Multimedia";
                /// <summary>
                /// Constant JA_JP_NARROWBANDMODEL for ja-JP_NarrowbandModel
                /// </summary>
                public const string JA_JP_NARROWBANDMODEL = "ja-JP_NarrowbandModel";
                /// <summary>
                /// Constant JA_JP_TELEPHONY for ja-JP_Telephony
                /// </summary>
                public const string JA_JP_TELEPHONY = "ja-JP_Telephony";
                /// <summary>
                /// Constant KO_KR_BROADBANDMODEL for ko-KR_BroadbandModel
                /// </summary>
                public const string KO_KR_BROADBANDMODEL = "ko-KR_BroadbandModel";
                /// <summary>
                /// Constant KO_KR_MULTIMEDIA for ko-KR_Multimedia
                /// </summary>
                public const string KO_KR_MULTIMEDIA = "ko-KR_Multimedia";
                /// <summary>
                /// Constant KO_KR_NARROWBANDMODEL for ko-KR_NarrowbandModel
                /// </summary>
                public const string KO_KR_NARROWBANDMODEL = "ko-KR_NarrowbandModel";
                /// <summary>
                /// Constant KO_KR_TELEPHONY for ko-KR_Telephony
                /// </summary>
                public const string KO_KR_TELEPHONY = "ko-KR_Telephony";
                /// <summary>
                /// Constant NL_BE_TELEPHONY for nl-BE_Telephony
                /// </summary>
                public const string NL_BE_TELEPHONY = "nl-BE_Telephony";
                /// <summary>
                /// Constant NL_NL_BROADBANDMODEL for nl-NL_BroadbandModel
                /// </summary>
                public const string NL_NL_BROADBANDMODEL = "nl-NL_BroadbandModel";
                /// <summary>
                /// Constant NL_NL_MULTIMEDIA for nl-NL_Multimedia
                /// </summary>
                public const string NL_NL_MULTIMEDIA = "nl-NL_Multimedia";
                /// <summary>
                /// Constant NL_NL_NARROWBANDMODEL for nl-NL_NarrowbandModel
                /// </summary>
                public const string NL_NL_NARROWBANDMODEL = "nl-NL_NarrowbandModel";
                /// <summary>
                /// Constant NL_NL_TELEPHONY for nl-NL_Telephony
                /// </summary>
                public const string NL_NL_TELEPHONY = "nl-NL_Telephony";
                /// <summary>
                /// Constant PT_BR_BROADBANDMODEL for pt-BR_BroadbandModel
                /// </summary>
                public const string PT_BR_BROADBANDMODEL = "pt-BR_BroadbandModel";
                /// <summary>
                /// Constant PT_BR_MULTIMEDIA for pt-BR_Multimedia
                /// </summary>
                public const string PT_BR_MULTIMEDIA = "pt-BR_Multimedia";
                /// <summary>
                /// Constant PT_BR_NARROWBANDMODEL for pt-BR_NarrowbandModel
                /// </summary>
                public const string PT_BR_NARROWBANDMODEL = "pt-BR_NarrowbandModel";
                /// <summary>
                /// Constant PT_BR_TELEPHONY for pt-BR_Telephony
                /// </summary>
                public const string PT_BR_TELEPHONY = "pt-BR_Telephony";
                /// <summary>
                /// Constant SV_SE_TELEPHONY for sv-SE_Telephony
                /// </summary>
                public const string SV_SE_TELEPHONY = "sv-SE_Telephony";
                /// <summary>
                /// Constant ZH_CN_BROADBANDMODEL for zh-CN_BroadbandModel
                /// </summary>
                public const string ZH_CN_BROADBANDMODEL = "zh-CN_BroadbandModel";
                /// <summary>
                /// Constant ZH_CN_NARROWBANDMODEL for zh-CN_NarrowbandModel
                /// </summary>
                public const string ZH_CN_NARROWBANDMODEL = "zh-CN_NarrowbandModel";
                /// <summary>
                /// Constant ZH_CN_TELEPHONY for zh-CN_Telephony
                /// </summary>
                public const string ZH_CN_TELEPHONY = "zh-CN_Telephony";
                
            }
            /// <summary>
            /// If the job includes a callback URL, a comma-separated list of notification events to which to subscribe.
            /// Valid events are
            /// * `recognitions.started` generates a callback notification when the service begins to process the job.
            /// * `recognitions.completed` generates a callback notification when the job is complete. You must use the
            /// [Check a job](#checkjob) method to retrieve the results before they time out or are deleted.
            /// * `recognitions.completed_with_results` generates a callback notification when the job is complete. The
            /// notification includes the results of the request.
            /// * `recognitions.failed` generates a callback notification if the service experiences an error while
            /// processing the job.
            ///
            /// The `recognitions.completed` and `recognitions.completed_with_results` events are incompatible. You can
            /// specify only of the two events.
            ///
            /// If the job includes a callback URL, omit the parameter to subscribe to the default events:
            /// `recognitions.started`, `recognitions.completed`, and `recognitions.failed`. If the job does not include
            /// a callback URL, omit the parameter.
            /// </summary>
            public class EventsValue
            {
                /// <summary>
                /// Constant RECOGNITIONS_STARTED for recognitions.started
                /// </summary>
                public const string RECOGNITIONS_STARTED = "recognitions.started";
                /// <summary>
                /// Constant RECOGNITIONS_COMPLETED for recognitions.completed
                /// </summary>
                public const string RECOGNITIONS_COMPLETED = "recognitions.completed";
                /// <summary>
                /// Constant RECOGNITIONS_COMPLETED_WITH_RESULTS for recognitions.completed_with_results
                /// </summary>
                public const string RECOGNITIONS_COMPLETED_WITH_RESULTS = "recognitions.completed_with_results";
                /// <summary>
                /// Constant RECOGNITIONS_FAILED for recognitions.failed
                /// </summary>
                public const string RECOGNITIONS_FAILED = "recognitions.failed";
                
            }
        }

        /// <summary>
        /// Check jobs.
        ///
        /// Returns the ID and status of the latest 100 outstanding jobs associated with the credentials with which it
        /// is called. The method also returns the creation and update times of each job, and, if a job was created with
        /// a callback URL and a user token, the user token for the job. To obtain the results for a job whose status is
        /// `completed` or not one of the latest 100 outstanding jobs, use the [Check a job[(#checkjob) method. A job
        /// and its results remain available until you delete them with the [Delete a job](#deletejob) method or until
        /// the job's time to live expires, whichever comes first.
        ///
        /// **See also:** [Checking the status of the latest
        /// jobs](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-async#jobs).
        /// </summary>
        /// <returns><see cref="RecognitionJobs" />RecognitionJobs</returns>
        public DetailedResponse<RecognitionJobs> CheckJobs()
        {
            DetailedResponse<RecognitionJobs> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/recognitions");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "CheckJobs"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<RecognitionJobs>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<RecognitionJobs>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Check a job.
        ///
        /// Returns information about the specified job. The response always includes the status of the job and its
        /// creation and update times. If the status is `completed`, the response includes the results of the
        /// recognition request. You must use credentials for the instance of the service that owns a job to list
        /// information about it.
        ///
        /// You can use the method to retrieve the results of any job, regardless of whether it was submitted with a
        /// callback URL and the `recognitions.completed_with_results` event, and you can retrieve the results multiple
        /// times for as long as they remain available. Use the [Check jobs](#checkjobs) method to request information
        /// about the most recent jobs associated with the calling credentials.
        ///
        /// **See also:** [Checking the status and retrieving the results of a
        /// job](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-async#job).
        /// </summary>
        /// <param name="id">The identifier of the asynchronous job that is to be used for the request. You must make
        /// the request with credentials for the instance of the service that owns the job.</param>
        /// <returns><see cref="RecognitionJob" />RecognitionJob</returns>
        public DetailedResponse<RecognitionJob> CheckJob(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("`id` is required for `CheckJob`");
            }
            else
            {
                id = Uri.EscapeDataString(id);
            }
            DetailedResponse<RecognitionJob> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/recognitions/{id}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "CheckJob"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<RecognitionJob>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<RecognitionJob>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a job.
        ///
        /// Deletes the specified job. You cannot delete a job that the service is actively processing. Once you delete
        /// a job, its results are no longer available. The service automatically deletes a job and its results when the
        /// time to live for the results expires. You must use credentials for the instance of the service that owns a
        /// job to delete it.
        ///
        /// **See also:** [Deleting a
        /// job](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-async#delete-async).
        /// </summary>
        /// <param name="id">The identifier of the asynchronous job that is to be used for the request. You must make
        /// the request with credentials for the instance of the service that owns the job.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteJob(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("`id` is required for `DeleteJob`");
            }
            else
            {
                id = Uri.EscapeDataString(id);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/recognitions/{id}");


                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "DeleteJob"));
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
        /// Create a custom language model.
        ///
        /// Creates a new custom language model for a specified base model. The custom language model can be used only
        /// with the base model for which it is created. The model is owned by the instance of the service whose
        /// credentials are used to create it.
        ///
        /// You can create a maximum of 1024 custom language models per owning credentials. The service returns an error
        /// if you attempt to create more than 1024 models. You do not lose any models, but you cannot create any more
        /// until your model count is below the limit.
        ///
        /// **Important:** Effective **31 July 2023**, all previous-generation models will be removed from the service
        /// and the documentation. Most previous-generation models were deprecated on 15 March 2022. You must migrate to
        /// the equivalent next-generation model by 31 July 2023. For more information, see [Migrating to
        /// next-generation models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-migrate).
        ///
        /// **See also:**
        /// * [Create a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageCreate#createModel-language)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="name">A user-defined name for the new custom language model. Use a localized name that matches
        /// the language of the custom model. Use a name that describes the domain of the custom model, such as `Medical
        /// custom model` or `Legal custom model`. Use a name that is unique among all custom language models that you
        /// own.
        ///
        /// Include a maximum of 256 characters in the name. Do not use backslashes, slashes, colons, equal signs,
        /// ampersands, or question marks in the name.</param>
        /// <param name="baseModelName">The name of the base language model that is to be customized by the new custom
        /// language model. The new custom model can be used only with the base model that it customizes.
        ///
        /// To determine whether a base model supports language model customization, use the [Get a model](#getmodel)
        /// method and check that the attribute `custom_language_model` is set to `true`. You can also refer to
        /// [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).</param>
        /// <param name="dialect">The dialect of the specified language that is to be used with the custom language
        /// model. _For all languages, it is always safe to omit this field._ The service automatically uses the
        /// language identifier from the name of the base model. For example, the service automatically uses `en-US` for
        /// all US English models.
        ///
        /// If you specify the `dialect` for a new custom model, follow these guidelines. _For non-Spanish
        /// previous-generation models and for next-generation models,_ you must specify a value that matches the
        /// five-character language identifier from the name of the base model. _For Spanish previous-generation
        /// models,_ you must specify one of the following values:
        /// * `es-ES` for Castilian Spanish (`es-ES` models)
        /// * `es-LA` for Latin American Spanish (`es-AR`, `es-CL`, `es-CO`, and `es-PE` models)
        /// * `es-US` for Mexican (North American) Spanish (`es-MX` models)
        ///
        /// All values that you pass for the `dialect` field are case-insensitive. (optional)</param>
        /// <param name="description">A recommended description of the new custom language model. Use a localized
        /// description that matches the language of the custom model. Include a maximum of 128 characters in the
        /// description. (optional)</param>
        /// <returns><see cref="LanguageModel" />LanguageModel</returns>
        public DetailedResponse<LanguageModel> CreateLanguageModel(string name, string baseModelName, string dialect = null, string description = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateLanguageModel`");
            }
            if (string.IsNullOrEmpty(baseModelName))
            {
                throw new ArgumentNullException("`baseModelName` is required for `CreateLanguageModel`");
            }
            DetailedResponse<LanguageModel> result = null;

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
                if (!string.IsNullOrEmpty(baseModelName))
                {
                    bodyObject["base_model_name"] = baseModelName;
                }
                if (!string.IsNullOrEmpty(dialect))
                {
                    bodyObject["dialect"] = dialect;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "CreateLanguageModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<LanguageModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<LanguageModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List custom language models.
        ///
        /// Lists information about all custom language models that are owned by an instance of the service. Use the
        /// `language` parameter to see all custom language models for the specified language. Omit the parameter to see
        /// all custom language models for all languages. You must use credentials for the instance of the service that
        /// owns a model to list information about it.
        ///
        /// **See also:**
        /// * [Listing custom language
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageLanguageModels#listModels-language)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="language">The identifier of the language for which custom language or custom acoustic models
        /// are to be returned. Specify the five-character language identifier; for example, specify `en-US` to see all
        /// custom language or custom acoustic models that are based on US English models. Omit the parameter to see all
        /// custom language or custom acoustic models that are owned by the requesting credentials.
        ///
        /// To determine the languages for which customization is available, see [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// (optional)</param>
        /// <returns><see cref="LanguageModels" />LanguageModels</returns>
        public DetailedResponse<LanguageModels> ListLanguageModels(string language = null)
        {
            DetailedResponse<LanguageModels> result = null;

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

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ListLanguageModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<LanguageModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<LanguageModels>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListLanguageModels.
        /// </summary>
        public class ListLanguageModelsEnums
        {
            /// <summary>
            /// The identifier of the language for which custom language or custom acoustic models are to be returned.
            /// Specify the five-character language identifier; for example, specify `en-US` to see all custom language
            /// or custom acoustic models that are based on US English models. Omit the parameter to see all custom
            /// language or custom acoustic models that are owned by the requesting credentials.
            ///
            /// To determine the languages for which customization is available, see [Language support for
            /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
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
                /// Constant EN_IN for en-IN
                /// </summary>
                public const string EN_IN = "en-IN";
                /// <summary>
                /// Constant EN_US for en-US
                /// </summary>
                public const string EN_US = "en-US";
                /// <summary>
                /// Constant EN_WW for en-WW
                /// </summary>
                public const string EN_WW = "en-WW";
                /// <summary>
                /// Constant ES_AR for es-AR
                /// </summary>
                public const string ES_AR = "es-AR";
                /// <summary>
                /// Constant ES_CL for es-CL
                /// </summary>
                public const string ES_CL = "es-CL";
                /// <summary>
                /// Constant ES_CO for es-CO
                /// </summary>
                public const string ES_CO = "es-CO";
                /// <summary>
                /// Constant ES_ES for es-ES
                /// </summary>
                public const string ES_ES = "es-ES";
                /// <summary>
                /// Constant ES_LA for es-LA
                /// </summary>
                public const string ES_LA = "es-LA";
                /// <summary>
                /// Constant ES_MX for es-MX
                /// </summary>
                public const string ES_MX = "es-MX";
                /// <summary>
                /// Constant ES_PE for es-PE
                /// </summary>
                public const string ES_PE = "es-PE";
                /// <summary>
                /// Constant FR_CA for fr-CA
                /// </summary>
                public const string FR_CA = "fr-CA";
                /// <summary>
                /// Constant FR_FR for fr-FR
                /// </summary>
                public const string FR_FR = "fr-FR";
                /// <summary>
                /// Constant HI_IN for hi-IN
                /// </summary>
                public const string HI_IN = "hi-IN";
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
        /// Get a custom language model.
        ///
        /// Gets information about a specified custom language model. You must use credentials for the instance of the
        /// service that owns a model to list information about it.
        ///
        /// **See also:**
        /// * [Listing custom language
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageLanguageModels#listModels-language)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="LanguageModel" />LanguageModel</returns>
        public DetailedResponse<LanguageModel> GetLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetLanguageModel`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<LanguageModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "GetLanguageModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<LanguageModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<LanguageModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a custom language model.
        ///
        /// Deletes an existing custom language model. The custom model cannot be deleted if another request, such as
        /// adding a corpus or grammar to the model, is currently being processed. You must use credentials for the
        /// instance of the service that owns a model to delete it.
        ///
        /// **See also:**
        /// * [Deleting a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageLanguageModels#deleteModel-language)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteLanguageModel`");
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

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "DeleteLanguageModel"));
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
        /// Train a custom language model.
        ///
        /// Initiates the training of a custom language model with new resources such as corpora, grammars, and custom
        /// words. After adding, modifying, or deleting resources for a custom language model, use this method to begin
        /// the actual training of the model on the latest data. You can specify whether the custom language model is to
        /// be trained with all words from its words resource or only with words that were added or modified by the user
        /// directly. You must use credentials for the instance of the service that owns a model to train it.
        ///
        /// The training method is asynchronous. It can take on the order of minutes to complete depending on the amount
        /// of data on which the service is being trained and the current load on the service. The method returns an
        /// HTTP 200 response code to indicate that the training process has begun.
        ///
        /// You can monitor the status of the training by using the [Get a custom language model](#getlanguagemodel)
        /// method to poll the model's status. Use a loop to check the status every 10 seconds. If you added custom
        /// words directly to a custom model that is based on a next-generation model, allow for some minutes of extra
        /// training time for the model.
        ///
        /// The method returns a `LanguageModel` object that includes `status` and `progress` fields. A status of
        /// `available` means that the custom model is trained and ready to use. The service cannot accept subsequent
        /// training requests or requests to add new resources until the existing request completes.
        ///
        /// For custom models that are based on improved base language models, training also performs an automatic
        /// upgrade to a newer version of the base model. You do not need to use the [Upgrade a custom language
        /// model](#upgradelanguagemodel) method to perform the upgrade.
        ///
        /// **See also:**
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support)
        /// * [Train the custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageCreate#trainModel-language)
        /// * [Upgrading custom language models that are based on improved next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-upgrade#custom-upgrade-language-ng)
        ///
        ///
        /// ### Training failures
        ///
        ///  Training can fail to start for the following reasons:
        /// * The service is currently handling another request for the custom model, such as another training request
        /// or a request to add a corpus or grammar to the model.
        /// * No training data have been added to the custom model.
        /// * The custom model contains one or more invalid corpora, grammars, or words (for example, a custom word has
        /// an invalid sounds-like pronunciation). You can correct the invalid resources or set the `strict` parameter
        /// to `false` to exclude the invalid resources from the training. The model must contain at least one valid
        /// resource for training to succeed.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="wordTypeToAdd">_For custom models that are based on previous-generation models_, the type of
        /// words from the custom language model's words resource on which to train the model:
        /// * `all` (the default) trains the model on all new words, regardless of whether they were extracted from
        /// corpora or grammars or were added or modified by the user.
        /// * `user` trains the model only on custom words that were added or modified by the user directly. The model
        /// is not trained on new words extracted from corpora or grammars.
        ///
        /// _For custom models that are based on next-generation models_, the service ignores the parameter. The words
        /// resource contains only custom words that the user adds or modifies directly, so the parameter is
        /// unnecessary. (optional, default to all)</param>
        /// <param name="customizationWeight">Specifies a customization weight for the custom language model. The
        /// customization weight tells the service how much weight to give to words from the custom language model
        /// compared to those from the base model for speech recognition. Specify a value between 0.0 and 1.0. The
        /// default value is:
        /// * 0.3 for previous-generation models
        /// * 0.2 for most next-generation models
        /// * 0.1 for next-generation English and Japanese models
        ///
        /// The default value yields the best performance in general. Assign a higher value if your audio makes frequent
        /// use of OOV words from the custom model. Use caution when setting the weight: a higher value can improve the
        /// accuracy of phrases from the custom model's domain, but it can negatively affect performance on non-domain
        /// phrases.
        ///
        /// The value that you assign is used for all recognition requests that use the model. You can override it for
        /// any recognition request by specifying a customization weight for that request.
        ///
        /// See [Using customization
        /// weight](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageUse#weight).
        /// (optional)</param>
        /// <param name="strict">If `false`, allows training of the custom language model to proceed as long as the
        /// model contains at least one valid resource. The method returns an array of `TrainingWarning` objects that
        /// lists any invalid resources. By default (`true`), training of a custom language model fails (status code
        /// 400) if the model contains one or more invalid resources (corpus files, grammar files, or custom words).
        /// (optional, default to true)</param>
        /// <returns><see cref="TrainingResponse" />TrainingResponse</returns>
        public DetailedResponse<TrainingResponse> TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null, bool? strict = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `TrainLanguageModel`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<TrainingResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/train");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(wordTypeToAdd))
                {
                    restRequest.WithArgument("word_type_to_add", wordTypeToAdd);
                }
                if (customizationWeight != null)
                {
                    restRequest.WithArgument("customization_weight", customizationWeight);
                }
                if (strict != null)
                {
                    restRequest.WithArgument("strict", strict);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "TrainLanguageModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for TrainLanguageModel.
        /// </summary>
        public class TrainLanguageModelEnums
        {
            /// <summary>
            /// _For custom models that are based on previous-generation models_, the type of words from the custom
            /// language model's words resource on which to train the model:
            /// * `all` (the default) trains the model on all new words, regardless of whether they were extracted from
            /// corpora or grammars or were added or modified by the user.
            /// * `user` trains the model only on custom words that were added or modified by the user directly. The
            /// model is not trained on new words extracted from corpora or grammars.
            ///
            /// _For custom models that are based on next-generation models_, the service ignores the parameter. The
            /// words resource contains only custom words that the user adds or modifies directly, so the parameter is
            /// unnecessary.
            /// </summary>
            public class WordTypeToAddValue
            {
                /// <summary>
                /// Constant ALL for all
                /// </summary>
                public const string ALL = "all";
                /// <summary>
                /// Constant USER for user
                /// </summary>
                public const string USER = "user";
                
            }
        }

        /// <summary>
        /// Reset a custom language model.
        ///
        /// Resets a custom language model by removing all corpora, grammars, and words from the model. Resetting a
        /// custom language model initializes the model to its state when it was first created. Metadata such as the
        /// name and language of the model are preserved, but the model's words resource is removed and must be
        /// re-created. You must use credentials for the instance of the service that owns a model to reset it.
        ///
        /// **See also:**
        /// * [Resetting a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageLanguageModels#resetModel-language)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> ResetLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `ResetLanguageModel`");
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

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/reset");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ResetLanguageModel"));
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
        /// Upgrade a custom language model.
        ///
        /// Initiates the upgrade of a custom language model to the latest version of its base language model. The
        /// upgrade method is asynchronous. It can take on the order of minutes to complete depending on the amount of
        /// data in the custom model and the current load on the service. A custom model must be in the `ready` or
        /// `available` state to be upgraded. You must use credentials for the instance of the service that owns a model
        /// to upgrade it.
        ///
        /// The method returns an HTTP 200 response code to indicate that the upgrade process has begun successfully.
        /// You can monitor the status of the upgrade by using the [Get a custom language model](#getlanguagemodel)
        /// method to poll the model's status. The method returns a `LanguageModel` object that includes `status` and
        /// `progress` fields. Use a loop to check the status every 10 seconds.
        ///
        /// While it is being upgraded, the custom model has the status `upgrading`. When the upgrade is complete, the
        /// model resumes the status that it had prior to upgrade. The service cannot accept subsequent requests for the
        /// model until the upgrade completes.
        ///
        /// For custom models that are based on improved base language models, the [Train a custom language
        /// model](#trainlanguagemodel) method also performs an automatic upgrade to a newer version of the base model.
        /// You do not need to use the upgrade method.
        ///
        /// **See also:**
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support)
        /// * [Upgrading a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-upgrade#custom-upgrade-language)
        /// * [Upgrading custom language models that are based on improved next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-upgrade#custom-upgrade-language-ng).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> UpgradeLanguageModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `UpgradeLanguageModel`");
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

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/upgrade_model");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "UpgradeLanguageModel"));
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
        /// List corpora.
        ///
        /// Lists information about all corpora from a custom language model. The information includes the name, status,
        /// and total number of words for each corpus. _For custom models that are based on previous-generation models_,
        /// it also includes the number of out-of-vocabulary (OOV) words from the corpus. You must use credentials for
        /// the instance of the service that owns a model to list its corpora.
        ///
        /// **See also:** [Listing corpora for a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageCorpora#listCorpora).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="Corpora" />Corpora</returns>
        public DetailedResponse<Corpora> ListCorpora(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `ListCorpora`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<Corpora> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ListCorpora"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Corpora>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Corpora>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Add a corpus.
        ///
        /// Adds a single corpus text file of new training data to a custom language model. Use multiple requests to
        /// submit multiple corpus text files. You must use credentials for the instance of the service that owns a
        /// model to add a corpus to it. Adding a corpus does not affect the custom language model until you train the
        /// model for the new data by using the [Train a custom language model](#trainlanguagemodel) method.
        ///
        /// Submit a plain text file that contains sample sentences from the domain of interest to enable the service to
        /// parse the words in context. The more sentences you add that represent the context in which speakers use
        /// words from the domain, the better the service's recognition accuracy.
        ///
        /// The call returns an HTTP 201 response code if the corpus is valid. The service then asynchronously processes
        /// and automatically extracts data from the contents of the corpus. This operation can take on the order of
        /// minutes to complete depending on the current load on the service, the total number of words in the corpus,
        /// and, _for custom models that are based on previous-generation models_, the number of new (out-of-vocabulary)
        /// words in the corpus. You cannot submit requests to add additional resources to the custom model or to train
        /// the model until the service's analysis of the corpus for the current request completes. Use the [Get a
        /// corpus](#getcorpus) method to check the status of the analysis.
        ///
        /// _For custom models that are based on previous-generation models_, the service auto-populates the model's
        /// words resource with words from the corpus that are not found in its base vocabulary. These words are
        /// referred to as out-of-vocabulary (OOV) words. After adding a corpus, you must validate the words resource to
        /// ensure that each OOV word's definition is complete and valid. You can use the [List custom
        /// words](#listwords) method to examine the words resource. You can use other words method to eliminate typos
        /// and modify how words are pronounced and displayed as needed.
        ///
        /// To add a corpus file that has the same name as an existing corpus, set the `allow_overwrite` parameter to
        /// `true`; otherwise, the request fails. Overwriting an existing corpus causes the service to process the
        /// corpus text file and extract its data anew. _For a custom model that is based on a previous-generation
        /// model_, the service first removes any OOV words that are associated with the existing corpus from the
        /// model's words resource unless they were also added by another corpus or grammar, or they have been modified
        /// in some way with the [Add custom words](#addwords) or [Add a custom word](#addword) method.
        ///
        /// The service limits the overall amount of data that you can add to a custom model to a maximum of 10 million
        /// total words from all sources combined. _For a custom model that is based on a previous-generation model_,
        /// you can add no more than 90 thousand custom (OOV) words to a model. This includes words that the service
        /// extracts from corpora and grammars, and words that you add directly.
        ///
        /// **See also:**
        /// * [Add a corpus to the custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageCreate#addCorpus)
        /// * [Working with corpora for previous-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#workingCorpora)
        /// * [Working with corpora for next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords-ng#workingCorpora-ng)
        /// * [Validating a words resource for previous-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#validateModel)
        /// * [Validating a words resource for next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords-ng#validateModel-ng).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="corpusName">The name of the new corpus for the custom language model. Use a localized name that
        /// matches the language of the custom model and reflects the contents of the corpus.
        /// * Include a maximum of 128 characters in the name.
        /// * Do not use characters that need to be URL-encoded. For example, do not use spaces, slashes, backslashes,
        /// colons, ampersands, double quotes, plus signs, equals signs, questions marks, and so on in the name. (The
        /// service does not prevent the use of these characters. But because they must be URL-encoded wherever used,
        /// their use is strongly discouraged.)
        /// * Do not use the name of an existing corpus or grammar that is already defined for the custom model.
        /// * Do not use the name `user`, which is reserved by the service to denote custom words that are added or
        /// modified by the user.
        /// * Do not use the name `base_lm` or `default_lm`. Both names are reserved for future use by the
        /// service.</param>
        /// <param name="corpusFile">A plain text file that contains the training data for the corpus. Encode the file
        /// in UTF-8 if it contains non-ASCII characters; the service assumes UTF-8 encoding if it encounters non-ASCII
        /// characters.
        ///
        /// Make sure that you know the character encoding of the file. You must use that same encoding when working
        /// with the words in the custom language model. For more information, see [Character encoding for custom
        /// words](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageWords#charEncoding).
        ///
        /// With the `curl` command, use the `--data-binary` option to upload the file for the request.</param>
        /// <param name="allowOverwrite">If `true`, the specified corpus overwrites an existing corpus with the same
        /// name. If `false`, the request fails if a corpus with the same name already exists. The parameter has no
        /// effect if a corpus with the same name does not already exist. (optional, default to false)</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddCorpus(string customizationId, string corpusName, System.IO.MemoryStream corpusFile, bool? allowOverwrite = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddCorpus`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(corpusName))
            {
                throw new ArgumentNullException("`corpusName` is required for `AddCorpus`");
            }
            else
            {
                corpusName = Uri.EscapeDataString(corpusName);
            }
            if (corpusFile == null)
            {
                throw new ArgumentNullException("`corpusFile` is required for `AddCorpus`");
            }
            DetailedResponse<object> result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (corpusFile != null)
                {
                    var corpusFileContent = new ByteArrayContent(corpusFile.ToArray());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/plain", out contentType);
                    corpusFileContent.Headers.ContentType = contentType;
                    formData.Add(corpusFileContent, "corpus_file", "filename");
                }

                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");

                restRequest.WithHeader("Accept", "application/json");
                if (allowOverwrite != null)
                {
                    restRequest.WithArgument("allow_overwrite", allowOverwrite);
                }
                restRequest.WithBodyContent(formData);

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "AddCorpus"));
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
        /// Get a corpus.
        ///
        /// Gets information about a corpus from a custom language model. The information includes the name, status, and
        /// total number of words for the corpus. _For custom models that are based on previous-generation models_, it
        /// also includes the number of out-of-vocabulary (OOV) words from the corpus. You must use credentials for the
        /// instance of the service that owns a model to list its corpora.
        ///
        /// **See also:** [Listing corpora for a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageCorpora#listCorpora).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="corpusName">The name of the corpus for the custom language model.</param>
        /// <returns><see cref="Corpus" />Corpus</returns>
        public DetailedResponse<Corpus> GetCorpus(string customizationId, string corpusName)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetCorpus`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(corpusName))
            {
                throw new ArgumentNullException("`corpusName` is required for `GetCorpus`");
            }
            else
            {
                corpusName = Uri.EscapeDataString(corpusName);
            }
            DetailedResponse<Corpus> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "GetCorpus"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Corpus>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Corpus>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a corpus.
        ///
        /// Deletes an existing corpus from a custom language model. Removing a corpus does not affect the custom model
        /// until you train the model with the [Train a custom language model](#trainlanguagemodel) method. You must use
        /// credentials for the instance of the service that owns a model to delete its corpora.
        ///
        /// _For custom models that are based on previous-generation models_, the service removes any out-of-vocabulary
        /// (OOV) words that are associated with the corpus from the custom model's words resource unless they were also
        /// added by another corpus or grammar, or they were modified in some way with the [Add custom words](#addwords)
        /// or [Add a custom word](#addword) method.
        ///
        ///
        ///
        /// **See also:** [Deleting a corpus from a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageCorpora#deleteCorpus).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="corpusName">The name of the corpus for the custom language model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteCorpus(string customizationId, string corpusName)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteCorpus`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(corpusName))
            {
                throw new ArgumentNullException("`corpusName` is required for `DeleteCorpus`");
            }
            else
            {
                corpusName = Uri.EscapeDataString(corpusName);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "DeleteCorpus"));
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
        /// Lists information about custom words from a custom language model. You can list all words from the custom
        /// model's words resource, only custom words that were added or modified by the user, or, _for a custom model
        /// that is based on a previous-generation model_, only out-of-vocabulary (OOV) words that were extracted from
        /// corpora or are recognized by grammars. _For a custom model that is based on a next-generation model_, you
        /// can list all words or only those words that were added directly by a user, which return the same results.
        ///
        /// You can also indicate the order in which the service is to return words; by default, the service lists words
        /// in ascending alphabetical order. You must use credentials for the instance of the service that owns a model
        /// to list information about its words.
        ///
        /// **See also:** [Listing words from a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageWords#listWords).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="wordType">The type of words to be listed from the custom language model's words resource:
        /// * `all` (the default) shows all words.
        /// * `user` shows only custom words that were added or modified by the user directly.
        /// * `corpora` shows only OOV that were extracted from corpora.
        /// * `grammars` shows only OOV words that are recognized by grammars.
        ///
        /// _For a custom model that is based on a next-generation model_, only `all` and `user` apply. Both options
        /// return the same results. Words from other sources are not added to custom models that are based on
        /// next-generation models. (optional, default to all)</param>
        /// <param name="sort">Indicates the order in which the words are to be listed, `alphabetical` or by `count`.
        /// You can prepend an optional `+` or `-` to an argument to indicate whether the results are to be sorted in
        /// ascending or descending order. By default, words are sorted in ascending alphabetical order. For
        /// alphabetical ordering, the lexicographical precedence is numeric values, uppercase letters, and lowercase
        /// letters. For count ordering, values with the same count are ordered alphabetically. With the `curl` command,
        /// URL-encode the `+` symbol as `%2B`. (optional, default to alphabetical)</param>
        /// <returns><see cref="Words" />Words</returns>
        public DetailedResponse<Words> ListWords(string customizationId, string wordType = null, string sort = null)
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
                if (!string.IsNullOrEmpty(wordType))
                {
                    restRequest.WithArgument("word_type", wordType);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    restRequest.WithArgument("sort", sort);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ListWords"));
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
        /// Enum values for ListWords.
        /// </summary>
        public class ListWordsEnums
        {
            /// <summary>
            /// The type of words to be listed from the custom language model's words resource:
            /// * `all` (the default) shows all words.
            /// * `user` shows only custom words that were added or modified by the user directly.
            /// * `corpora` shows only OOV that were extracted from corpora.
            /// * `grammars` shows only OOV words that are recognized by grammars.
            ///
            /// _For a custom model that is based on a next-generation model_, only `all` and `user` apply. Both options
            /// return the same results. Words from other sources are not added to custom models that are based on
            /// next-generation models.
            /// </summary>
            public class WordTypeValue
            {
                /// <summary>
                /// Constant ALL for all
                /// </summary>
                public const string ALL = "all";
                /// <summary>
                /// Constant USER for user
                /// </summary>
                public const string USER = "user";
                /// <summary>
                /// Constant CORPORA for corpora
                /// </summary>
                public const string CORPORA = "corpora";
                /// <summary>
                /// Constant GRAMMARS for grammars
                /// </summary>
                public const string GRAMMARS = "grammars";
                
            }
            /// <summary>
            /// Indicates the order in which the words are to be listed, `alphabetical` or by `count`. You can prepend
            /// an optional `+` or `-` to an argument to indicate whether the results are to be sorted in ascending or
            /// descending order. By default, words are sorted in ascending alphabetical order. For alphabetical
            /// ordering, the lexicographical precedence is numeric values, uppercase letters, and lowercase letters.
            /// For count ordering, values with the same count are ordered alphabetically. With the `curl` command,
            /// URL-encode the `+` symbol as `%2B`.
            /// </summary>
            public class SortValue
            {
                /// <summary>
                /// Constant ALPHABETICAL for alphabetical
                /// </summary>
                public const string ALPHABETICAL = "alphabetical";
                /// <summary>
                /// Constant COUNT for count
                /// </summary>
                public const string COUNT = "count";
                
            }
        }

        /// <summary>
        /// Add custom words.
        ///
        /// Adds one or more custom words to a custom language model. You can use this method to add words or to modify
        /// existing words in a custom model's words resource. _For custom models that are based on previous-generation
        /// models_, the service populates the words resource for a custom model with out-of-vocabulary (OOV) words from
        /// each corpus or grammar that is added to the model. You can use this method to modify OOV words in the
        /// model's words resource.
        ///
        /// _For a custom model that is based on a previous-generation model_, the words resource for a model can
        /// contain a maximum of 90 thousand custom (OOV) words. This includes words that the service extracts from
        /// corpora and grammars and words that you add directly.
        ///
        /// You must use credentials for the instance of the service that owns a model to add or modify custom words for
        /// the model. Adding or modifying custom words does not affect the custom model until you train the model for
        /// the new data by using the [Train a custom language model](#trainlanguagemodel) method.
        ///
        /// You add custom words by providing a `CustomWords` object, which is an array of `CustomWord` objects, one per
        /// word. Use the object's `word` parameter to identify the word that is to be added. You can also provide one
        /// or both of the optional `display_as` or `sounds_like` fields for each word.
        /// * The `display_as` field provides a different way of spelling the word in a transcript. Use the parameter
        /// when you want the word to appear different from its usual representation or from its spelling in training
        /// data. For example, you might indicate that the word `IBM` is to be displayed as `IBM&trade;`.
        /// * The `sounds_like` field provides an array of one or more pronunciations for the word. Use the parameter to
        /// specify how the word can be pronounced by users. Use the parameter for words that are difficult to
        /// pronounce, foreign words, acronyms, and so on. For example, you might specify that the word `IEEE` can sound
        /// like `I triple E`. You can specify a maximum of five sounds-like pronunciations for a word. _For a custom
        /// model that is based on a previous-generation model_, if you omit the `sounds_like` field, the service
        /// attempts to set the field to its pronunciation of the word. It cannot generate a pronunciation for all
        /// words, so you must review the word's definition to ensure that it is complete and valid.
        ///
        /// If you add a custom word that already exists in the words resource for the custom model, the new definition
        /// overwrites the existing data for the word. If the service encounters an error with the input data, it
        /// returns a failure code and does not add any of the words to the words resource.
        ///
        /// The call returns an HTTP 201 response code if the input data is valid. It then asynchronously processes the
        /// words to add them to the model's words resource. The time that it takes for the analysis to complete depends
        /// on the number of new words that you add but is generally faster than adding a corpus or grammar.
        ///
        /// You can monitor the status of the request by using the [Get a custom language model](#getlanguagemodel)
        /// method to poll the model's status. Use a loop to check the status every 10 seconds. The method returns a
        /// `Customization` object that includes a `status` field. A status of `ready` means that the words have been
        /// added to the custom model. The service cannot accept requests to add new data or to train the model until
        /// the existing request completes.
        ///
        /// You can use the [List custom words](#listwords) or [Get a custom word](#getword) method to review the words
        /// that you add. Words with an invalid `sounds_like` field include an `error` field that describes the problem.
        /// You can use other words-related methods to correct errors, eliminate typos, and modify how words are
        /// pronounced as needed.
        ///
        /// **See also:**
        /// * [Add words to the custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageCreate#addWords)
        /// * [Working with custom words for previous-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#workingWords)
        /// * [Working with custom words for next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords-ng#workingWords-ng)
        /// * [Validating a words resource for previous-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#validateModel)
        /// * [Validating a words resource for next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords-ng#validateModel-ng).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="words">An array of `CustomWord` objects that provides information about each custom word that
        /// is to be added to or updated in the custom language model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddWords(string customizationId, List<CustomWord> words)
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

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "AddWords"));
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
        /// Add a custom word.
        ///
        /// Adds a custom word to a custom language model. You can use this method to add a word or to modify an
        /// existing word in the words resource. _For custom models that are based on previous-generation models_, the
        /// service populates the words resource for a custom model with out-of-vocabulary (OOV) words from each corpus
        /// or grammar that is added to the model. You can use this method to modify OOV words in the model's words
        /// resource.
        ///
        /// _For a custom model that is based on a previous-generation models_, the words resource for a model can
        /// contain a maximum of 90 thousand custom (OOV) words. This includes words that the service extracts from
        /// corpora and grammars and words that you add directly.
        ///
        /// You must use credentials for the instance of the service that owns a model to add or modify a custom word
        /// for the model. Adding or modifying a custom word does not affect the custom model until you train the model
        /// for the new data by using the [Train a custom language model](#trainlanguagemodel) method.
        ///
        /// Use the `word_name` parameter to specify the custom word that is to be added or modified. Use the
        /// `CustomWord` object to provide one or both of the optional `display_as` or `sounds_like` fields for the
        /// word.
        /// * The `display_as` field provides a different way of spelling the word in a transcript. Use the parameter
        /// when you want the word to appear different from its usual representation or from its spelling in training
        /// data. For example, you might indicate that the word `IBM` is to be displayed as `IBM&trade;`.
        /// * The `sounds_like` field provides an array of one or more pronunciations for the word. Use the parameter to
        /// specify how the word can be pronounced by users. Use the parameter for words that are difficult to
        /// pronounce, foreign words, acronyms, and so on. For example, you might specify that the word `IEEE` can sound
        /// like `i triple e`. You can specify a maximum of five sounds-like pronunciations for a word. _For custom
        /// models that are based on previous-generation models_, if you omit the `sounds_like` field, the service
        /// attempts to set the field to its pronunciation of the word. It cannot generate a pronunciation for all
        /// words, so you must review the word's definition to ensure that it is complete and valid.
        ///
        /// If you add a custom word that already exists in the words resource for the custom model, the new definition
        /// overwrites the existing data for the word. If the service encounters an error, it does not add the word to
        /// the words resource. Use the [Get a custom word](#getword) method to review the word that you add.
        ///
        /// **See also:**
        /// * [Add words to the custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-languageCreate#addWords)
        /// * [Working with custom words for previous-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#workingWords)
        /// * [Working with custom words for next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords-ng#workingWords-ng)
        /// * [Validating a words resource for previous-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#validateModel)
        /// * [Validating a words resource for next-generation
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords-ng#validateModel-ng).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="wordName">The custom word that is to be added to or updated in the custom language model. Do
        /// not include spaces in the word. Use a `-` (dash) or `_` (underscore) to connect the tokens of compound
        /// words. URL-encode the word if it includes non-ASCII characters. For more information, see [Character
        /// encoding](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#charEncoding).</param>
        /// <param name="word">For the [Add custom words](#addwords) method, you must specify the custom word that is to
        /// be added to or updated in the custom model. Do not include spaces in the word. Use a `-` (dash) or `_`
        /// (underscore) to connect the tokens of compound words.
        ///
        /// Omit this parameter for the [Add a custom word](#addword) method. (optional)</param>
        /// <param name="soundsLike">As array of sounds-like pronunciations for the custom word. Specify how words that
        /// are difficult to pronounce, foreign words, acronyms, and so on can be pronounced by users.
        /// * _For custom models that are based on previous-generation models_, for a word that is not in the service's
        /// base vocabulary, omit the parameter to have the service automatically generate a sounds-like pronunciation
        /// for the word.
        /// * For a word that is in the service's base vocabulary, use the parameter to specify additional
        /// pronunciations for the word. You cannot override the default pronunciation of a word; pronunciations you add
        /// augment the pronunciation from the base vocabulary.
        ///
        /// A word can have at most five sounds-like pronunciations. A pronunciation can include at most 40 characters
        /// not including spaces. (optional)</param>
        /// <param name="displayAs">An alternative spelling for the custom word when it appears in a transcript. Use the
        /// parameter when you want the word to have a spelling that is different from its usual representation or from
        /// its spelling in corpora training data.
        ///
        /// _For custom models that are based on next-generation models_, the service uses the spelling of the word as
        /// the display-as value if you omit the field. (optional)</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddWord(string customizationId, string wordName, string word = null, List<string> soundsLike = null, string displayAs = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddWord`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(wordName))
            {
                throw new ArgumentNullException("`wordName` is required for `AddWord`");
            }
            else
            {
                wordName = Uri.EscapeDataString(wordName);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PutAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(word))
                {
                    bodyObject["word"] = word;
                }
                if (soundsLike != null && soundsLike.Count > 0)
                {
                    bodyObject["sounds_like"] = JToken.FromObject(soundsLike);
                }
                if (!string.IsNullOrEmpty(displayAs))
                {
                    bodyObject["display_as"] = displayAs;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "AddWord"));
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
        /// Gets information about a custom word from a custom language model. You must use credentials for the instance
        /// of the service that owns a model to list information about its words.
        ///
        /// **See also:** [Listing words from a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageWords#listWords).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="wordName">The custom word that is to be read from the custom language model. URL-encode the
        /// word if it includes non-ASCII characters. For more information, see [Character
        /// encoding](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#charEncoding).</param>
        /// <returns><see cref="Word" />Word</returns>
        public DetailedResponse<Word> GetWord(string customizationId, string wordName)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetWord`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(wordName))
            {
                throw new ArgumentNullException("`wordName` is required for `GetWord`");
            }
            else
            {
                wordName = Uri.EscapeDataString(wordName);
            }
            DetailedResponse<Word> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "GetWord"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Word>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Word>();
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
        /// Deletes a custom word from a custom language model. You can remove any word that you added to the custom
        /// model's words resource via any means. However, if the word also exists in the service's base vocabulary, the
        /// service removes the word only from the words resource; the word remains in the base vocabulary. Removing a
        /// custom word does not affect the custom model until you train the model with the [Train a custom language
        /// model](#trainlanguagemodel) method. You must use credentials for the instance of the service that owns a
        /// model to delete its words.
        ///
        /// **See also:** [Deleting a word from a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageWords#deleteWord).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="wordName">The custom word that is to be deleted from the custom language model. URL-encode the
        /// word if it includes non-ASCII characters. For more information, see [Character
        /// encoding](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-corporaWords#charEncoding).</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteWord(string customizationId, string wordName)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteWord`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(wordName))
            {
                throw new ArgumentNullException("`wordName` is required for `DeleteWord`");
            }
            else
            {
                wordName = Uri.EscapeDataString(wordName);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "DeleteWord"));
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
        /// List grammars.
        ///
        /// Lists information about all grammars from a custom language model. For each grammar, the information
        /// includes the name, status, and (for grammars that are based on previous-generation models) the total number
        /// of out-of-vocabulary (OOV) words. You must use credentials for the instance of the service that owns a model
        /// to list its grammars.
        ///
        /// **See also:**
        /// * [Listing grammars from a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageGrammars#listGrammars)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="Grammars" />Grammars</returns>
        public DetailedResponse<Grammars> ListGrammars(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `ListGrammars`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<Grammars> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/grammars");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ListGrammars"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Grammars>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Grammars>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Add a grammar.
        ///
        /// Adds a single grammar file to a custom language model. Submit a plain text file in UTF-8 format that defines
        /// the grammar. Use multiple requests to submit multiple grammar files. You must use credentials for the
        /// instance of the service that owns a model to add a grammar to it. Adding a grammar does not affect the
        /// custom language model until you train the model for the new data by using the [Train a custom language
        /// model](#trainlanguagemodel) method.
        ///
        /// The call returns an HTTP 201 response code if the grammar is valid. The service then asynchronously
        /// processes the contents of the grammar and automatically extracts new words that it finds. This operation can
        /// take a few seconds or minutes to complete depending on the size and complexity of the grammar, as well as
        /// the current load on the service. You cannot submit requests to add additional resources to the custom model
        /// or to train the model until the service's analysis of the grammar for the current request completes. Use the
        /// [Get a grammar](#getgrammar) method to check the status of the analysis.
        ///
        /// _For grammars that are based on previous-generation models,_ the service populates the model's words
        /// resource with any word that is recognized by the grammar that is not found in the model's base vocabulary.
        /// These are referred to as out-of-vocabulary (OOV) words. You can use the [List custom words](#listwords)
        /// method to examine the words resource and use other words-related methods to eliminate typos and modify how
        /// words are pronounced as needed. _For grammars that are based on next-generation models,_ the service
        /// extracts no OOV words from the grammars.
        ///
        /// To add a grammar that has the same name as an existing grammar, set the `allow_overwrite` parameter to
        /// `true`; otherwise, the request fails. Overwriting an existing grammar causes the service to process the
        /// grammar file and extract OOV words anew. Before doing so, it removes any OOV words associated with the
        /// existing grammar from the model's words resource unless they were also added by another resource or they
        /// have been modified in some way with the [Add custom words](#addwords) or [Add a custom word](#addword)
        /// method.
        ///
        /// _For grammars that are based on previous-generation models,_ the service limits the overall amount of data
        /// that you can add to a custom model to a maximum of 10 million total words from all sources combined. Also,
        /// you can add no more than 90 thousand OOV words to a model. This includes words that the service extracts
        /// from corpora and grammars and words that you add directly.
        ///
        /// **See also:**
        /// * [Understanding
        /// grammars](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-grammarUnderstand#grammarUnderstand)
        /// * [Add a grammar to the custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-grammarAdd#addGrammar)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="grammarName">The name of the new grammar for the custom language model. Use a localized name
        /// that matches the language of the custom model and reflects the contents of the grammar.
        /// * Include a maximum of 128 characters in the name.
        /// * Do not use characters that need to be URL-encoded. For example, do not use spaces, slashes, backslashes,
        /// colons, ampersands, double quotes, plus signs, equals signs, questions marks, and so on in the name. (The
        /// service does not prevent the use of these characters. But because they must be URL-encoded wherever used,
        /// their use is strongly discouraged.)
        /// * Do not use the name of an existing grammar or corpus that is already defined for the custom model.
        /// * Do not use the name `user`, which is reserved by the service to denote custom words that are added or
        /// modified by the user.
        /// * Do not use the name `base_lm` or `default_lm`. Both names are reserved for future use by the
        /// service.</param>
        /// <param name="grammarFile">A plain text file that contains the grammar in the format specified by the
        /// `Content-Type` header. Encode the file in UTF-8 (ASCII is a subset of UTF-8). Using any other encoding can
        /// lead to issues when compiling the grammar or to unexpected results in decoding. The service ignores an
        /// encoding that is specified in the header of the grammar.
        ///
        /// With the `curl` command, use the `--data-binary` option to upload the file for the request.</param>
        /// <param name="contentType">The format (MIME type) of the grammar file:
        /// * `application/srgs` for Augmented Backus-Naur Form (ABNF), which uses a plain-text representation that is
        /// similar to traditional BNF grammars.
        /// * `application/srgs+xml` for XML Form, which uses XML elements to represent the grammar.</param>
        /// <param name="allowOverwrite">If `true`, the specified grammar overwrites an existing grammar with the same
        /// name. If `false`, the request fails if a grammar with the same name already exists. The parameter has no
        /// effect if a grammar with the same name does not already exist. (optional, default to false)</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddGrammar(string customizationId, string grammarName, System.IO.MemoryStream grammarFile, string contentType, bool? allowOverwrite = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddGrammar`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(grammarName))
            {
                throw new ArgumentNullException("`grammarName` is required for `AddGrammar`");
            }
            else
            {
                grammarName = Uri.EscapeDataString(grammarName);
            }
            if (grammarFile == null)
            {
                throw new ArgumentNullException("`grammarFile` is required for `AddGrammar`");
            }
            if (string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentNullException("`contentType` is required for `AddGrammar`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/grammars/{grammarName}");

                restRequest.WithHeader("Accept", "application/json");

                if (!string.IsNullOrEmpty(contentType))
                {
                    restRequest.WithHeader("Content-Type", contentType);
                }
                if (allowOverwrite != null)
                {
                    restRequest.WithArgument("allow_overwrite", allowOverwrite);
                }
                var httpContent = new ByteArrayContent(grammarFile.ToArray());
                httpContent.Headers.Add("Content-Type", "application/srgs");
                System.Net.Http.Headers.MediaTypeHeaderValue grammarFileContentType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out grammarFileContentType);
                httpContent.Headers.ContentType = grammarFileContentType;
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "AddGrammar"));
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
        /// Enum values for AddGrammar.
        /// </summary>
        public class AddGrammarEnums
        {
            /// <summary>
            /// The format (MIME type) of the grammar file:
            /// * `application/srgs` for Augmented Backus-Naur Form (ABNF), which uses a plain-text representation that
            /// is similar to traditional BNF grammars.
            /// * `application/srgs+xml` for XML Form, which uses XML elements to represent the grammar.
            /// </summary>
            public class ContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_SRGS for application/srgs
                /// </summary>
                public const string APPLICATION_SRGS = "application/srgs";
                /// <summary>
                /// Constant APPLICATION_SRGS_XML for application/srgs+xml
                /// </summary>
                public const string APPLICATION_SRGS_XML = "application/srgs+xml";
                
            }
        }

        /// <summary>
        /// Get a grammar.
        ///
        /// Gets information about a grammar from a custom language model. For each grammar, the information includes
        /// the name, status, and (for grammars that are based on previous-generation models) the total number of
        /// out-of-vocabulary (OOV) words. You must use credentials for the instance of the service that owns a model to
        /// list its grammars.
        ///
        /// **See also:**
        /// * [Listing grammars from a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageGrammars#listGrammars)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="grammarName">The name of the grammar for the custom language model.</param>
        /// <returns><see cref="Grammar" />Grammar</returns>
        public DetailedResponse<Grammar> GetGrammar(string customizationId, string grammarName)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetGrammar`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(grammarName))
            {
                throw new ArgumentNullException("`grammarName` is required for `GetGrammar`");
            }
            else
            {
                grammarName = Uri.EscapeDataString(grammarName);
            }
            DetailedResponse<Grammar> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/grammars/{grammarName}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "GetGrammar"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<Grammar>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<Grammar>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a grammar.
        ///
        /// Deletes an existing grammar from a custom language model. _For grammars that are based on
        /// previous-generation models,_ the service removes any out-of-vocabulary (OOV) words associated with the
        /// grammar from the custom model's words resource unless they were also added by another resource or they were
        /// modified in some way with the [Add custom words](#addwords) or [Add a custom word](#addword) method.
        /// Removing a grammar does not affect the custom model until you train the model with the [Train a custom
        /// language model](#trainlanguagemodel) method. You must use credentials for the instance of the service that
        /// owns a model to delete its grammar.
        ///
        /// **See also:**
        /// * [Deleting a grammar from a custom language
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageGrammars#deleteGrammar)
        /// * [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="grammarName">The name of the grammar for the custom language model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteGrammar(string customizationId, string grammarName)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteGrammar`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(grammarName))
            {
                throw new ArgumentNullException("`grammarName` is required for `DeleteGrammar`");
            }
            else
            {
                grammarName = Uri.EscapeDataString(grammarName);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/grammars/{grammarName}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "DeleteGrammar"));
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
        /// Create a custom acoustic model.
        ///
        /// Creates a new custom acoustic model for a specified base model. The custom acoustic model can be used only
        /// with the base model for which it is created. The model is owned by the instance of the service whose
        /// credentials are used to create it.
        ///
        /// You can create a maximum of 1024 custom acoustic models per owning credentials. The service returns an error
        /// if you attempt to create more than 1024 models. You do not lose any models, but you cannot create any more
        /// until your model count is below the limit.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **Important:** Effective **31 July 2023**, all previous-generation models will be removed from the service
        /// and the documentation. Most previous-generation models were deprecated on 15 March 2022. You must migrate to
        /// the equivalent next-generation model by 31 July 2023. For more information, see [Migrating to
        /// next-generation models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-models-migrate).
        ///
        /// **See also:** [Create a custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-acoustic#createModel-acoustic).
        /// </summary>
        /// <param name="name">A user-defined name for the new custom acoustic model. Use a localized name that matches
        /// the language of the custom model. Use a name that describes the acoustic environment of the custom model,
        /// such as `Mobile custom model` or `Noisy car custom model`. Use a name that is unique among all custom
        /// acoustic models that you own.
        ///
        /// Include a maximum of 256 characters in the name. Do not use backslashes, slashes, colons, equal signs,
        /// ampersands, or question marks in the name.</param>
        /// <param name="baseModelName">The name of the base language model that is to be customized by the new custom
        /// acoustic model. The new custom model can be used only with the base model that it customizes.
        ///
        /// To determine whether a base model supports acoustic model customization, refer to [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).</param>
        /// <param name="description">A recommended description of the new custom acoustic model. Use a localized
        /// description that matches the language of the custom model. Include a maximum of 128 characters in the
        /// description. (optional)</param>
        /// <returns><see cref="AcousticModel" />AcousticModel</returns>
        public DetailedResponse<AcousticModel> CreateAcousticModel(string name, string baseModelName, string description = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("`name` is required for `CreateAcousticModel`");
            }
            if (string.IsNullOrEmpty(baseModelName))
            {
                throw new ArgumentNullException("`baseModelName` is required for `CreateAcousticModel`");
            }
            DetailedResponse<AcousticModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations");

                restRequest.WithHeader("Accept", "application/json");
                restRequest.WithHeader("Content-Type", "application/json");

                JObject bodyObject = new JObject();
                if (!string.IsNullOrEmpty(name))
                {
                    bodyObject["name"] = name;
                }
                if (!string.IsNullOrEmpty(baseModelName))
                {
                    bodyObject["base_model_name"] = baseModelName;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    bodyObject["description"] = description;
                }
                var httpContent = new StringContent(JsonConvert.SerializeObject(bodyObject), Encoding.UTF8, HttpMediaType.APPLICATION_JSON);
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "CreateAcousticModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AcousticModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AcousticModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List custom acoustic models.
        ///
        /// Lists information about all custom acoustic models that are owned by an instance of the service. Use the
        /// `language` parameter to see all custom acoustic models for the specified language. Omit the parameter to see
        /// all custom acoustic models for all languages. You must use credentials for the instance of the service that
        /// owns a model to list information about it.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Listing custom acoustic
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageAcousticModels#listModels-acoustic).
        /// </summary>
        /// <param name="language">The identifier of the language for which custom language or custom acoustic models
        /// are to be returned. Specify the five-character language identifier; for example, specify `en-US` to see all
        /// custom language or custom acoustic models that are based on US English models. Omit the parameter to see all
        /// custom language or custom acoustic models that are owned by the requesting credentials.
        ///
        /// To determine the languages for which customization is available, see [Language support for
        /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
        /// (optional)</param>
        /// <returns><see cref="AcousticModels" />AcousticModels</returns>
        public DetailedResponse<AcousticModels> ListAcousticModels(string language = null)
        {
            DetailedResponse<AcousticModels> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/acoustic_customizations");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(language))
                {
                    restRequest.WithArgument("language", language);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ListAcousticModels"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AcousticModels>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AcousticModels>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Enum values for ListAcousticModels.
        /// </summary>
        public class ListAcousticModelsEnums
        {
            /// <summary>
            /// The identifier of the language for which custom language or custom acoustic models are to be returned.
            /// Specify the five-character language identifier; for example, specify `en-US` to see all custom language
            /// or custom acoustic models that are based on US English models. Omit the parameter to see all custom
            /// language or custom acoustic models that are owned by the requesting credentials.
            ///
            /// To determine the languages for which customization is available, see [Language support for
            /// customization](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-support).
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
                /// Constant EN_IN for en-IN
                /// </summary>
                public const string EN_IN = "en-IN";
                /// <summary>
                /// Constant EN_US for en-US
                /// </summary>
                public const string EN_US = "en-US";
                /// <summary>
                /// Constant EN_WW for en-WW
                /// </summary>
                public const string EN_WW = "en-WW";
                /// <summary>
                /// Constant ES_AR for es-AR
                /// </summary>
                public const string ES_AR = "es-AR";
                /// <summary>
                /// Constant ES_CL for es-CL
                /// </summary>
                public const string ES_CL = "es-CL";
                /// <summary>
                /// Constant ES_CO for es-CO
                /// </summary>
                public const string ES_CO = "es-CO";
                /// <summary>
                /// Constant ES_ES for es-ES
                /// </summary>
                public const string ES_ES = "es-ES";
                /// <summary>
                /// Constant ES_LA for es-LA
                /// </summary>
                public const string ES_LA = "es-LA";
                /// <summary>
                /// Constant ES_MX for es-MX
                /// </summary>
                public const string ES_MX = "es-MX";
                /// <summary>
                /// Constant ES_PE for es-PE
                /// </summary>
                public const string ES_PE = "es-PE";
                /// <summary>
                /// Constant FR_CA for fr-CA
                /// </summary>
                public const string FR_CA = "fr-CA";
                /// <summary>
                /// Constant FR_FR for fr-FR
                /// </summary>
                public const string FR_FR = "fr-FR";
                /// <summary>
                /// Constant HI_IN for hi-IN
                /// </summary>
                public const string HI_IN = "hi-IN";
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
        /// Get a custom acoustic model.
        ///
        /// Gets information about a specified custom acoustic model. You must use credentials for the instance of the
        /// service that owns a model to list information about it.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Listing custom acoustic
        /// models](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageAcousticModels#listModels-acoustic).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="AcousticModel" />AcousticModel</returns>
        public DetailedResponse<AcousticModel> GetAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetAcousticModel`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<AcousticModel> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "GetAcousticModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AcousticModel>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AcousticModel>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a custom acoustic model.
        ///
        /// Deletes an existing custom acoustic model. The custom model cannot be deleted if another request, such as
        /// adding an audio resource to the model, is currently being processed. You must use credentials for the
        /// instance of the service that owns a model to delete it.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Deleting a custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageAcousticModels#deleteModel-acoustic).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteAcousticModel`");
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

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "DeleteAcousticModel"));
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
        /// Train a custom acoustic model.
        ///
        /// Initiates the training of a custom acoustic model with new or changed audio resources. After adding or
        /// deleting audio resources for a custom acoustic model, use this method to begin the actual training of the
        /// model on the latest audio data. The custom acoustic model does not reflect its changed data until you train
        /// it. You must use credentials for the instance of the service that owns a model to train it.
        ///
        /// The training method is asynchronous. Training time depends on the cumulative amount of audio data that the
        /// custom acoustic model contains and the current load on the service. When you train or retrain a model, the
        /// service uses all of the model's audio data in the training. Training a custom acoustic model takes
        /// approximately as long as the length of its cumulative audio data. For example, it takes approximately 2
        /// hours to train a model that contains a total of 2 hours of audio. The method returns an HTTP 200 response
        /// code to indicate that the training process has begun.
        ///
        /// You can monitor the status of the training by using the [Get a custom acoustic model](#getacousticmodel)
        /// method to poll the model's status. Use a loop to check the status once a minute. The method returns an
        /// `AcousticModel` object that includes `status` and `progress` fields. A status of `available` indicates that
        /// the custom model is trained and ready to use. The service cannot train a model while it is handling another
        /// request for the model. The service cannot accept subsequent training requests, or requests to add new audio
        /// resources, until the existing training request completes.
        ///
        /// You can use the optional `custom_language_model_id` parameter to specify the GUID of a separately created
        /// custom language model that is to be used during training. Train with a custom language model if you have
        /// verbatim transcriptions of the audio files that you have added to the custom model or you have either
        /// corpora (text files) or a list of words that are relevant to the contents of the audio files. For training
        /// to succeed, both of the custom models must be based on the same version of the same base model, and the
        /// custom language model must be fully trained and available.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:**
        /// * [Train the custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-acoustic#trainModel-acoustic)
        /// * [Using custom acoustic and custom language models
        /// together](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-useBoth#useBoth)
        ///
        /// ### Training failures
        ///
        ///  Training can fail to start for the following reasons:
        /// * The service is currently handling another request for the custom model, such as another training request
        /// or a request to add audio resources to the model.
        /// * The custom model contains less than 10 minutes of audio that includes speech, not silence.
        /// * The custom model contains more than 50 hours of audio (for IBM Cloud) or more that 200 hours of audio (for
        /// IBM Cloud Pak for Data). **Note:** For IBM Cloud, the maximum hours of audio for a custom acoustic model was
        /// reduced from 200 to 50 hours in August and September 2022. For more information, see [Maximum hours of
        /// audio](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-audioResources#audioMaximum).
        /// * You passed a custom language model with the `custom_language_model_id` query parameter that is not in the
        /// available state. A custom language model must be fully trained and available to be used to train a custom
        /// acoustic model.
        /// * You passed an incompatible custom language model with the `custom_language_model_id` query parameter. Both
        /// custom models must be based on the same version of the same base model.
        /// * The custom model contains one or more invalid audio resources. You can correct the invalid audio resources
        /// or set the `strict` parameter to `false` to exclude the invalid resources from the training. The model must
        /// contain at least one valid resource for training to succeed.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="customLanguageModelId">The customization ID (GUID) of a custom language model that is to be
        /// used during training of the custom acoustic model. Specify a custom language model that has been trained
        /// with verbatim transcriptions of the audio resources or that contains words that are relevant to the contents
        /// of the audio resources. The custom language model must be based on the same version of the same base model
        /// as the custom acoustic model, and the custom language model must be fully trained and available. The
        /// credentials specified with the request must own both custom models. (optional)</param>
        /// <param name="strict">If `false`, allows training of the custom acoustic model to proceed as long as the
        /// model contains at least one valid audio resource. The method returns an array of `TrainingWarning` objects
        /// that lists any invalid resources. By default (`true`), training of a custom acoustic model fails (status
        /// code 400) if the model contains one or more invalid audio resources. (optional, default to true)</param>
        /// <returns><see cref="TrainingResponse" />TrainingResponse</returns>
        public DetailedResponse<TrainingResponse> TrainAcousticModel(string customizationId, string customLanguageModelId = null, bool? strict = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `TrainAcousticModel`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<TrainingResponse> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/train");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(customLanguageModelId))
                {
                    restRequest.WithArgument("custom_language_model_id", customLanguageModelId);
                }
                if (strict != null)
                {
                    restRequest.WithArgument("strict", strict);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "TrainAcousticModel"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<TrainingResponse>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<TrainingResponse>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Reset a custom acoustic model.
        ///
        /// Resets a custom acoustic model by removing all audio resources from the model. Resetting a custom acoustic
        /// model initializes the model to its state when it was first created. Metadata such as the name and language
        /// of the model are preserved, but the model's audio resources are removed and must be re-created. The service
        /// cannot reset a model while it is handling another request for the model. The service cannot accept
        /// subsequent requests for the model until the existing reset request completes. You must use credentials for
        /// the instance of the service that owns a model to reset it.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Resetting a custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageAcousticModels#resetModel-acoustic).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> ResetAcousticModel(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `ResetAcousticModel`");
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

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/reset");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ResetAcousticModel"));
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
        /// Upgrade a custom acoustic model.
        ///
        /// Initiates the upgrade of a custom acoustic model to the latest version of its base language model. The
        /// upgrade method is asynchronous. It can take on the order of minutes or hours to complete depending on the
        /// amount of data in the custom model and the current load on the service; typically, upgrade takes
        /// approximately twice the length of the total audio contained in the custom model. A custom model must be in
        /// the `ready` or `available` state to be upgraded. You must use credentials for the instance of the service
        /// that owns a model to upgrade it.
        ///
        /// The method returns an HTTP 200 response code to indicate that the upgrade process has begun successfully.
        /// You can monitor the status of the upgrade by using the [Get a custom acoustic model](#getacousticmodel)
        /// method to poll the model's status. The method returns an `AcousticModel` object that includes `status` and
        /// `progress` fields. Use a loop to check the status once a minute.
        ///
        /// While it is being upgraded, the custom model has the status `upgrading`. When the upgrade is complete, the
        /// model resumes the status that it had prior to upgrade. The service cannot upgrade a model while it is
        /// handling another request for the model. The service cannot accept subsequent requests for the model until
        /// the existing upgrade request completes.
        ///
        /// If the custom acoustic model was trained with a separately created custom language model, you must use the
        /// `custom_language_model_id` parameter to specify the GUID of that custom language model. The custom language
        /// model must be upgraded before the custom acoustic model can be upgraded. Omit the parameter if the custom
        /// acoustic model was not trained with a custom language model.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Upgrading a custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-upgrade#custom-upgrade-acoustic).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="customLanguageModelId">If the custom acoustic model was trained with a custom language model,
        /// the customization ID (GUID) of that custom language model. The custom language model must be upgraded before
        /// the custom acoustic model can be upgraded. The custom language model must be fully trained and available.
        /// The credentials specified with the request must own both custom models. (optional)</param>
        /// <param name="force">If `true`, forces the upgrade of a custom acoustic model for which no input data has
        /// been modified since it was last trained. Use this parameter only to force the upgrade of a custom acoustic
        /// model that is trained with a custom language model, and only if you receive a 400 response code and the
        /// message `No input data modified since last training`. See [Upgrading a custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-custom-upgrade#custom-upgrade-acoustic).
        /// (optional, default to false)</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> UpgradeAcousticModel(string customizationId, string customLanguageModelId = null, bool? force = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `UpgradeAcousticModel`");
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

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/upgrade_model");

                restRequest.WithHeader("Accept", "application/json");
                if (!string.IsNullOrEmpty(customLanguageModelId))
                {
                    restRequest.WithArgument("custom_language_model_id", customLanguageModelId);
                }
                if (force != null)
                {
                    restRequest.WithArgument("force", force);
                }

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "UpgradeAcousticModel"));
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
        /// List audio resources.
        ///
        /// Lists information about all audio resources from a custom acoustic model. The information includes the name
        /// of the resource and information about its audio data, such as its duration. It also includes the status of
        /// the audio resource, which is important for checking the service's analysis of the resource in response to a
        /// request to add it to the custom acoustic model. You must use credentials for the instance of the service
        /// that owns a model to list its audio resources.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Listing audio resources for a custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageAudio#listAudio).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <returns><see cref="AudioResources" />AudioResources</returns>
        public DetailedResponse<AudioResources> ListAudio(string customizationId)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `ListAudio`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            DetailedResponse<AudioResources> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "ListAudio"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AudioResources>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AudioResources>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Add an audio resource.
        ///
        /// Adds an audio resource to a custom acoustic model. Add audio content that reflects the acoustic
        /// characteristics of the audio that you plan to transcribe. You must use credentials for the instance of the
        /// service that owns a model to add an audio resource to it. Adding audio data does not affect the custom
        /// acoustic model until you train the model for the new data by using the [Train a custom acoustic
        /// model](#trainacousticmodel) method.
        ///
        /// You can add individual audio files or an archive file that contains multiple audio files. Adding multiple
        /// audio files via a single archive file is significantly more efficient than adding each file individually.
        /// You can add audio resources in any format that the service supports for speech recognition.
        ///
        /// You can use this method to add any number of audio resources to a custom model by calling the method once
        /// for each audio or archive file. You can add multiple different audio resources at the same time. You must
        /// add a minimum of 10 minutes of audio that includes speech, not just silence, to a custom acoustic model
        /// before you can train it. No audio resource, audio- or archive-type, can be larger than 100 MB. To add an
        /// audio resource that has the same name as an existing audio resource, set the `allow_overwrite` parameter to
        /// `true`; otherwise, the request fails. A custom model can contain no more than 50 hours of audio (for IBM
        /// Cloud) or 200 hours of audio (for IBM Cloud Pak for Data). **Note:** For IBM Cloud, the maximum hours of
        /// audio for a custom acoustic model was reduced from 200 to 50 hours in August and September 2022. For more
        /// information, see [Maximum hours of
        /// audio](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-audioResources#audioMaximum).
        ///
        /// The method is asynchronous. It can take several seconds or minutes to complete depending on the duration of
        /// the audio and, in the case of an archive file, the total number of audio files being processed. The service
        /// returns a 201 response code if the audio is valid. It then asynchronously analyzes the contents of the audio
        /// file or files and automatically extracts information about the audio such as its length, sampling rate, and
        /// encoding. You cannot submit requests to train or upgrade the model until the service's analysis of all audio
        /// resources for current requests completes.
        ///
        /// To determine the status of the service's analysis of the audio, use the [Get an audio resource](#getaudio)
        /// method to poll the status of the audio. The method accepts the customization ID of the custom model and the
        /// name of the audio resource, and it returns the status of the resource. Use a loop to check the status of the
        /// audio every few seconds until it becomes `ok`.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Add audio to the custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-acoustic#addAudio).
        ///
        /// ### Content types for audio-type resources
        ///
        ///  You can add an individual audio file in any format that the service supports for speech recognition. For an
        /// audio-type resource, use the `Content-Type` parameter to specify the audio format (MIME type) of the audio
        /// file, including specifying the sampling rate, channels, and endianness where indicated.
        /// * `audio/alaw` (Specify the sampling rate (`rate`) of the audio.)
        /// * `audio/basic` (Use only with narrowband models.)
        /// * `audio/flac`
        /// * `audio/g729` (Use only with narrowband models.)
        /// * `audio/l16` (Specify the sampling rate (`rate`) and optionally the number of channels (`channels`) and
        /// endianness (`endianness`) of the audio.)
        /// * `audio/mp3`
        /// * `audio/mpeg`
        /// * `audio/mulaw` (Specify the sampling rate (`rate`) of the audio.)
        /// * `audio/ogg` (The service automatically detects the codec of the input audio.)
        /// * `audio/ogg;codecs=opus`
        /// * `audio/ogg;codecs=vorbis`
        /// * `audio/wav` (Provide audio with a maximum of nine channels.)
        /// * `audio/webm` (The service automatically detects the codec of the input audio.)
        /// * `audio/webm;codecs=opus`
        /// * `audio/webm;codecs=vorbis`
        ///
        /// The sampling rate of an audio file must match the sampling rate of the base model for the custom model: for
        /// broadband models, at least 16 kHz; for narrowband models, at least 8 kHz. If the sampling rate of the audio
        /// is higher than the minimum required rate, the service down-samples the audio to the appropriate rate. If the
        /// sampling rate of the audio is lower than the minimum required rate, the service labels the audio file as
        /// `invalid`.
        ///
        ///  **See also:** [Supported audio
        /// formats](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-audio-formats).
        ///
        /// ### Content types for archive-type resources
        ///
        ///  You can add an archive file (**.zip** or **.tar.gz** file) that contains audio files in any format that the
        /// service supports for speech recognition. For an archive-type resource, use the `Content-Type` parameter to
        /// specify the media type of the archive file:
        /// * `application/zip` for a **.zip** file
        /// * `application/gzip` for a **.tar.gz** file.
        ///
        /// When you add an archive-type resource, the `Contained-Content-Type` header is optional depending on the
        /// format of the files that you are adding:
        /// * For audio files of type `audio/alaw`, `audio/basic`, `audio/l16`, or `audio/mulaw`, you must use the
        /// `Contained-Content-Type` header to specify the format of the contained audio files. Include the `rate`,
        /// `channels`, and `endianness` parameters where necessary. In this case, all audio files contained in the
        /// archive file must have the same audio format.
        /// * For audio files of all other types, you can omit the `Contained-Content-Type` header. In this case, the
        /// audio files contained in the archive file can have any of the formats not listed in the previous bullet. The
        /// audio files do not need to have the same format.
        ///
        /// Do not use the `Contained-Content-Type` header when adding an audio-type resource.
        ///
        /// ### Naming restrictions for embedded audio files
        ///
        ///  The name of an audio file that is contained in an archive-type resource can include a maximum of 128
        /// characters. This includes the file extension and all elements of the name (for example, slashes).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="audioName">The name of the new audio resource for the custom acoustic model. Use a localized
        /// name that matches the language of the custom model and reflects the contents of the resource.
        /// * Include a maximum of 128 characters in the name.
        /// * Do not use characters that need to be URL-encoded. For example, do not use spaces, slashes, backslashes,
        /// colons, ampersands, double quotes, plus signs, equals signs, questions marks, and so on in the name. (The
        /// service does not prevent the use of these characters. But because they must be URL-encoded wherever used,
        /// their use is strongly discouraged.)
        /// * Do not use the name of an audio resource that has already been added to the custom model.</param>
        /// <param name="audioResource">The audio resource that is to be added to the custom acoustic model, an
        /// individual audio file or an archive file.
        ///
        /// With the `curl` command, use the `--data-binary` option to upload the file for the request.</param>
        /// <param name="contentType">For an audio-type resource, the format (MIME type) of the audio. For more
        /// information, see **Content types for audio-type resources** in the method description.
        ///
        /// For an archive-type resource, the media type of the archive file. For more information, see **Content types
        /// for archive-type resources** in the method description. (optional)</param>
        /// <param name="containedContentType">_For an archive-type resource_, specify the format of the audio files
        /// that are contained in the archive file if they are of type `audio/alaw`, `audio/basic`, `audio/l16`, or
        /// `audio/mulaw`. Include the `rate`, `channels`, and `endianness` parameters where necessary. In this case,
        /// all audio files that are contained in the archive file must be of the indicated type.
        ///
        /// For all other audio formats, you can omit the header. In this case, the audio files can be of multiple types
        /// as long as they are not of the types listed in the previous paragraph.
        ///
        /// The parameter accepts all of the audio formats that are supported for use with speech recognition. For more
        /// information, see **Content types for audio-type resources** in the method description.
        ///
        /// _For an audio-type resource_, omit the header. (optional)</param>
        /// <param name="allowOverwrite">If `true`, the specified audio resource overwrites an existing audio resource
        /// with the same name. If `false`, the request fails if an audio resource with the same name already exists.
        /// The parameter has no effect if an audio resource with the same name does not already exist. (optional,
        /// default to false)</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> AddAudio(string customizationId, string audioName, System.IO.MemoryStream audioResource, string contentType = null, string containedContentType = null, bool? allowOverwrite = null)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `AddAudio`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(audioName))
            {
                throw new ArgumentNullException("`audioName` is required for `AddAudio`");
            }
            else
            {
                audioName = Uri.EscapeDataString(audioName);
            }
            if (audioResource == null)
            {
                throw new ArgumentNullException("`audioResource` is required for `AddAudio`");
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");

                restRequest.WithHeader("Accept", "application/json");

                if (!string.IsNullOrEmpty(contentType))
                {
                    restRequest.WithHeader("Content-Type", contentType);
                }

                if (!string.IsNullOrEmpty(containedContentType))
                {
                    restRequest.WithHeader("Contained-Content-Type", containedContentType);
                }
                if (allowOverwrite != null)
                {
                    restRequest.WithArgument("allow_overwrite", allowOverwrite);
                }
                var httpContent = new ByteArrayContent(audioResource.ToArray());
                httpContent.Headers.Add("Content-Type", "application/zip");
                System.Net.Http.Headers.MediaTypeHeaderValue audioResourceContentType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out audioResourceContentType);
                httpContent.Headers.ContentType = audioResourceContentType;
                restRequest.WithBodyContent(httpContent);

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "AddAudio"));
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
        /// Enum values for AddAudio.
        /// </summary>
        public class AddAudioEnums
        {
            /// <summary>
            /// For an audio-type resource, the format (MIME type) of the audio. For more information, see **Content
            /// types for audio-type resources** in the method description.
            ///
            /// For an archive-type resource, the media type of the archive file. For more information, see **Content
            /// types for archive-type resources** in the method description.
            /// </summary>
            public class ContentTypeValue
            {
                /// <summary>
                /// Constant APPLICATION_ZIP for application/zip
                /// </summary>
                public const string APPLICATION_ZIP = "application/zip";
                /// <summary>
                /// Constant APPLICATION_GZIP for application/gzip
                /// </summary>
                public const string APPLICATION_GZIP = "application/gzip";
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
                /// Constant AUDIO_G729 for audio/g729
                /// </summary>
                public const string AUDIO_G729 = "audio/g729";
                /// <summary>
                /// Constant AUDIO_L16 for audio/l16
                /// </summary>
                public const string AUDIO_L16 = "audio/l16";
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
            /// _For an archive-type resource_, specify the format of the audio files that are contained in the archive
            /// file if they are of type `audio/alaw`, `audio/basic`, `audio/l16`, or `audio/mulaw`. Include the `rate`,
            /// `channels`, and `endianness` parameters where necessary. In this case, all audio files that are
            /// contained in the archive file must be of the indicated type.
            ///
            /// For all other audio formats, you can omit the header. In this case, the audio files can be of multiple
            /// types as long as they are not of the types listed in the previous paragraph.
            ///
            /// The parameter accepts all of the audio formats that are supported for use with speech recognition. For
            /// more information, see **Content types for audio-type resources** in the method description.
            ///
            /// _For an audio-type resource_, omit the header.
            /// </summary>
            public class ContainedContentTypeValue
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
                /// Constant AUDIO_G729 for audio/g729
                /// </summary>
                public const string AUDIO_G729 = "audio/g729";
                /// <summary>
                /// Constant AUDIO_L16 for audio/l16
                /// </summary>
                public const string AUDIO_L16 = "audio/l16";
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
        }

        /// <summary>
        /// Get an audio resource.
        ///
        /// Gets information about an audio resource from a custom acoustic model. The method returns an `AudioListing`
        /// object whose fields depend on the type of audio resource that you specify with the method's `audio_name`
        /// parameter:
        /// * _For an audio-type resource_, the object's fields match those of an `AudioResource` object: `duration`,
        /// `name`, `details`, and `status`.
        /// * _For an archive-type resource_, the object includes a `container` field whose fields match those of an
        /// `AudioResource` object. It also includes an `audio` field, which contains an array of `AudioResource`
        /// objects that provides information about the audio files that are contained in the archive.
        ///
        /// The information includes the status of the specified audio resource. The status is important for checking
        /// the service's analysis of a resource that you add to the custom model.
        /// * _For an audio-type resource_, the `status` field is located in the `AudioListing` object.
        /// * _For an archive-type resource_, the `status` field is located in the `AudioResource` object that is
        /// returned in the `container` field.
        ///
        /// You must use credentials for the instance of the service that owns a model to list its audio resources.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Listing audio resources for a custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageAudio#listAudio).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="audioName">The name of the audio resource for the custom acoustic model.</param>
        /// <returns><see cref="AudioListing" />AudioListing</returns>
        public DetailedResponse<AudioListing> GetAudio(string customizationId, string audioName)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `GetAudio`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(audioName))
            {
                throw new ArgumentNullException("`audioName` is required for `GetAudio`");
            }
            else
            {
                audioName = Uri.EscapeDataString(audioName);
            }
            DetailedResponse<AudioListing> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "GetAudio"));
                restRequest.WithHeaders(customRequestHeaders);
                ClearCustomRequestHeaders();

                result = restRequest.As<AudioListing>().Result;
                if (result == null)
                {
                    result = new DetailedResponse<AudioListing>();
                }
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete an audio resource.
        ///
        /// Deletes an existing audio resource from a custom acoustic model. Deleting an archive-type audio resource
        /// removes the entire archive of files. The service does not allow deletion of individual files from an archive
        /// resource.
        ///
        /// Removing an audio resource does not affect the custom model until you train the model on its updated data by
        /// using the [Train a custom acoustic model](#trainacousticmodel) method. You can delete an existing audio
        /// resource from a model while a different resource is being added to the model. You must use credentials for
        /// the instance of the service that owns a model to delete its audio resources.
        ///
        /// **Note:** Acoustic model customization is supported only for use with previous-generation models. It is not
        /// supported for next-generation models.
        ///
        /// **See also:** [Deleting an audio resource from a custom acoustic
        /// model](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-manageAudio#deleteAudio).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model that is to be used
        /// for the request. You must make the request with credentials for the instance of the service that owns the
        /// custom model.</param>
        /// <param name="audioName">The name of the audio resource for the custom acoustic model.</param>
        /// <returns><see cref="object" />object</returns>
        public DetailedResponse<object> DeleteAudio(string customizationId, string audioName)
        {
            if (string.IsNullOrEmpty(customizationId))
            {
                throw new ArgumentNullException("`customizationId` is required for `DeleteAudio`");
            }
            else
            {
                customizationId = Uri.EscapeDataString(customizationId);
            }
            if (string.IsNullOrEmpty(audioName))
            {
                throw new ArgumentNullException("`audioName` is required for `DeleteAudio`");
            }
            else
            {
                audioName = Uri.EscapeDataString(audioName);
            }
            DetailedResponse<object> result = null;

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");

                restRequest.WithHeader("Accept", "application/json");

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "DeleteAudio"));
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
        /// service instance is automatically deleted. This includes all custom language models, corpora, grammars, and
        /// words; all custom acoustic models and audio resources; all registered endpoints for the asynchronous HTTP
        /// interface; and all data related to speech recognition requests.
        ///
        /// **See also:** [Information
        /// security](https://cloud.ibm.com/docs/speech-to-text?topic=speech-to-text-information-security#information-security).
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

                restRequest.WithHeaders(Common.GetSdkHeaders("speech_to_text", "v1", "DeleteUserData"));
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
