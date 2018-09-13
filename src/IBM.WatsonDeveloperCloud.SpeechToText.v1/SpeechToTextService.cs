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
using System.Runtime.Serialization;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        public SpeechToTextService(TokenOptions options) : this()
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

            _tokenManager = new TokenManager(options);
        }

        public SpeechToTextService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Get a model.
        ///
        /// Gets information for a single specified language model that is available for use with the service. The
        /// information includes the name of the model and its minimum sampling rate in Hertz, among other things.
        /// </summary>
        /// <param name="modelId">The identifier of the model in the form of its name from the output of the **Get
        /// models** method.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="SpeechModel" />SpeechModel</returns>
        public SpeechModel GetModel(string modelId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException(nameof(modelId));
            SpeechModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models/{modelId}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<SpeechModel>().Result;
                if(result == null)
                    result = new SpeechModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List models.
        ///
        /// Lists all language models that are available for use with the service. The information includes the name of
        /// the model and its minimum sampling rate in Hertz, among other things.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="SpeechModels" />SpeechModels</returns>
        public SpeechModels ListModels(Dictionary<string, object> customData = null)
        {
            SpeechModels result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/models");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<SpeechModels>().Result;
                if(result == null)
                    result = new SpeechModels();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Recognize audio.
        ///
        /// Sends audio and returns transcription results for a recognition request. Returns only the final results; to
        /// enable interim results, use the WebSocket API. The service imposes a data size limit of 100 MB. It
        /// automatically detects the endianness of the incoming audio and, for audio that includes multiple channels,
        /// downmixes the audio to one-channel mono during transcoding. (For the `audio/l16` format, you can specify the
        /// endianness.)
        ///
        /// ### Streaming mode
        ///
        ///  For requests to transcribe live audio as it becomes available, you must set the `Transfer-Encoding` header
        /// to `chunked` to use streaming mode. In streaming mode, the server closes the connection (status code 408) if
        /// the service receives no data chunk for 30 seconds and it has no audio to transcribe for 30 seconds. The
        /// server also closes the connection (status code 400) if no speech is detected for `inactivity_timeout`
        /// seconds of audio (not processing time); use the `inactivity_timeout` parameter to change the default of 30
        /// seconds.
        ///
        /// ### Audio formats (content types)
        ///
        ///  Use the `Content-Type` header to specify the audio format (MIME type) of the audio. The service accepts the
        /// following formats:
        /// * `audio/basic` (Use only with narrowband models.)
        /// * `audio/flac`
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
        /// For information about the supported audio formats, including specifying the sampling rate, channels, and
        /// endianness for the indicated formats, see [Audio
        /// formats](https://console.bluemix.net/docs/services/speech-to-text/audio-formats.html).
        ///
        /// ### Multipart speech recognition
        ///
        ///  The method also supports multipart recognition requests. With multipart requests, you pass all audio data
        /// as multipart form data. You specify some parameters as request headers and query parameters, but you pass
        /// JSON metadata as form data to control most aspects of the transcription.
        ///
        /// The multipart approach is intended for use with browsers for which JavaScript is disabled or when the
        /// parameters used with the request are greater than the 8 KB limit imposed by most HTTP servers and proxies.
        /// You can encounter this limit, for example, if you want to spot a very large number of keywords.
        ///
        /// For information about submitting a multipart request, see [Making a multipart HTTP
        /// request](https://console.bluemix.net/docs/services/speech-to-text/http.html#HTTP-multi).
        /// </summary>
        /// <param name="audio">The audio to transcribe in the format specified by the `Content-Type` header.</param>
        /// <param name="contentType">The type of the input.</param>
        /// <param name="model">The identifier of the model that is to be used for the recognition request. (optional,
        /// default to en-US_BroadbandModel)</param>
        /// <param name="customizationId">The customization ID (GUID) of a custom language model that is to be used with
        /// the recognition request. The base model of the specified custom language model must match the model
        /// specified with the `model` parameter. You must make the request with service credentials created for the
        /// instance of the service that owns the custom model. By default, no custom language model is used.
        /// (optional)</param>
        /// <param name="acousticCustomizationId">The customization ID (GUID) of a custom acoustic model that is to be
        /// used with the recognition request. The base model of the specified custom acoustic model must match the
        /// model specified with the `model` parameter. You must make the request with service credentials created for
        /// the instance of the service that owns the custom model. By default, no custom acoustic model is used.
        /// (optional)</param>
        /// <param name="baseModelVersion">The version of the specified base model that is to be used with recognition
        /// request. Multiple versions of a base model can exist when a model is updated for internal improvements. The
        /// parameter is intended primarily for use with custom models that have been upgraded for a new base model. The
        /// default value depends on whether the parameter is used with or without a custom model. For more information,
        /// see [Base model version](https://console.bluemix.net/docs/services/speech-to-text/input.html#version).
        /// (optional)</param>
        /// <param name="customizationWeight">If you specify the customization ID (GUID) of a custom language model with
        /// the recognition request, the customization weight tells the service how much weight to give to words from
        /// the custom language model compared to those from the base model for the current request.
        ///
        /// Specify a value between 0.0 and 1.0. Unless a different customization weight was specified for the custom
        /// model when it was trained, the default value is 0.3. A customization weight that you specify overrides a
        /// weight that was specified when the custom model was trained.
        ///
        /// The default value yields the best performance in general. Assign a higher value if your audio makes frequent
        /// use of OOV words from the custom model. Use caution when setting the weight: a higher value can improve the
        /// accuracy of phrases from the custom model's domain, but it can negatively affect performance on non-domain
        /// phrases. (optional)</param>
        /// <param name="inactivityTimeout">The time in seconds after which, if only silence (no speech) is detected in
        /// submitted audio, the connection is closed with a 400 error. The parameter is useful for stopping audio
        /// submission from a live microphone when a user simply walks away. Use `-1` for infinity. (optional, default
        /// to 30)</param>
        /// <param name="keywords">An array of keyword strings to spot in the audio. Each keyword string can include one
        /// or more string tokens. Keywords are spotted only in the final results, not in interim hypotheses. If you
        /// specify any keywords, you must also specify a keywords threshold. You can spot a maximum of 1000 keywords.
        /// Omit the parameter or specify an empty array if you do not need to spot keywords. (optional)</param>
        /// <param name="keywordsThreshold">A confidence value that is the lower bound for spotting a keyword. A word is
        /// considered to match a keyword if its confidence is greater than or equal to the threshold. Specify a
        /// probability between 0.0 and 1.0. No keyword spotting is performed if you omit the parameter. If you specify
        /// a threshold, you must also specify one or more keywords. (optional)</param>
        /// <param name="maxAlternatives">The maximum number of alternative transcripts that the service is to return.
        /// By default, a single transcription is returned. (optional, default to 1)</param>
        /// <param name="wordAlternativesThreshold">A confidence value that is the lower bound for identifying a
        /// hypothesis as a possible word alternative (also known as "Confusion Networks"). An alternative word is
        /// considered if its confidence is greater than or equal to the threshold. Specify a probability between 0.0
        /// and 1.0. No alternative words are computed if you omit the parameter. (optional)</param>
        /// <param name="wordConfidence">If `true`, the service returns a confidence measure in the range of 0.0 to 1.0
        /// for each word. By default, no word confidence measures are returned. (optional, default to false)</param>
        /// <param name="timestamps">If `true`, the service returns time alignment for each word. By default, no
        /// timestamps are returned. (optional, default to false)</param>
        /// <param name="profanityFilter">If `true`, the service filters profanity from all output except for keyword
        /// results by replacing inappropriate words with a series of asterisks. Set the parameter to `false` to return
        /// results with no censoring. Applies to US English transcription only. (optional, default to true)</param>
        /// <param name="smartFormatting">If `true`, the service converts dates, times, series of digits and numbers,
        /// phone numbers, currency values, and internet addresses into more readable, conventional representations in
        /// the final transcript of a recognition request. For US English, the service also converts certain keyword
        /// strings to punctuation symbols. By default, no smart formatting is performed. Applies to US English and
        /// Spanish transcription only. (optional, default to false)</param>
        /// <param name="speakerLabels">If `true`, the response includes labels that identify which words were spoken by
        /// which participants in a multi-person exchange. By default, no speaker labels are returned. Setting
        /// `speaker_labels` to `true` forces the `timestamps` parameter to be `true`, regardless of whether you specify
        /// `false` for the parameter.
        ///
        ///  To determine whether a language model supports speaker labels, use the **Get models** method and check that
        /// the attribute `speaker_labels` is set to `true`. You can also refer to [Speaker
        /// labels](https://console.bluemix.net/docs/services/speech-to-text/output.html#speaker_labels). (optional,
        /// default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="SpeechRecognitionResults" />SpeechRecognitionResults</returns>
        public SpeechRecognitionResults RecognizeSessionless(byte[] audio, string contentType, string model = null, string customizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, Dictionary<string, object> customData = null)
        {
            if (audio == null)
                throw new ArgumentNullException(nameof(audio));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            SpeechRecognitionResults result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/recognize");

                if (!string.IsNullOrEmpty(contentType))
                    restRequest.WithHeader("Content-Type", contentType);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                if (!string.IsNullOrEmpty(customizationId))
                    restRequest.WithArgument("customization_id", customizationId);
                if (!string.IsNullOrEmpty(acousticCustomizationId))
                    restRequest.WithArgument("acoustic_customization_id", acousticCustomizationId);
                if (!string.IsNullOrEmpty(baseModelVersion))
                    restRequest.WithArgument("base_model_version", baseModelVersion);
                if (customizationWeight != null)
                    restRequest.WithArgument("customization_weight", customizationWeight);
                if (inactivityTimeout != null)
                    restRequest.WithArgument("inactivity_timeout", inactivityTimeout);
                if (keywords != null)
                    restRequest.WithArgument("keywords", keywords != null && keywords.Count > 0 ? string.Join(",", keywords.ToArray()) : null);
                if (keywordsThreshold != null)
                    restRequest.WithArgument("keywords_threshold", keywordsThreshold);
                if (maxAlternatives != null)
                    restRequest.WithArgument("max_alternatives", maxAlternatives);
                if (wordAlternativesThreshold != null)
                    restRequest.WithArgument("word_alternatives_threshold", wordAlternativesThreshold);
                if (wordConfidence != null)
                    restRequest.WithArgument("word_confidence", wordConfidence);
                if (timestamps != null)
                    restRequest.WithArgument("timestamps", timestamps);
                if (profanityFilter != null)
                    restRequest.WithArgument("profanity_filter", profanityFilter);
                if (smartFormatting != null)
                    restRequest.WithArgument("smart_formatting", smartFormatting);
                if (speakerLabels != null)
                    restRequest.WithArgument("speaker_labels", speakerLabels);
                var audioContent = new ByteArrayContent(audio);
                System.Net.Http.Headers.MediaTypeHeaderValue audioType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out audioType);
                audioContent.Headers.ContentType = audioType;
                restRequest.WithBodyContent(audioContent);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<SpeechRecognitionResults>().Result;
                if(result == null)
                    result = new SpeechRecognitionResults();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// recognition request. You must submit the request with the service credentials of the user who created the
        /// job.
        ///
        /// You can use the method to retrieve the results of any job, regardless of whether it was submitted with a
        /// callback URL and the `recognitions.completed_with_results` event, and you can retrieve the results multiple
        /// times for as long as they remain available. Use the **Check jobs** method to request information about the
        /// most recent jobs associated with the calling user.
        /// </summary>
        /// <param name="id">The ID of the asynchronous job.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="RecognitionJob" />RecognitionJob</returns>
        public RecognitionJob CheckJob(string id, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            RecognitionJob result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/recognitions/{id}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<RecognitionJob>().Result;
                if(result == null)
                    result = new RecognitionJob();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Check jobs.
        ///
        /// Returns the ID and status of the latest 100 outstanding jobs associated with the service credentials with
        /// which it is called. The method also returns the creation and update times of each job, and, if a job was
        /// created with a callback URL and a user token, the user token for the job. To obtain the results for a job
        /// whose status is `completed` or not one of the latest 100 outstanding jobs, use the **Check a job** method. A
        /// job and its results remain available until you delete them with the **Delete a job** method or until the
        /// job's time to live expires, whichever comes first.
        /// </summary>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="RecognitionJobs" />RecognitionJobs</returns>
        public RecognitionJobs CheckJobs(Dictionary<string, object> customData = null)
        {
            RecognitionJobs result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/recognitions");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<RecognitionJobs>().Result;
                if(result == null)
                    result = new RecognitionJobs();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Create a job.
        ///
        /// Creates a job for a new asynchronous recognition request. The job is owned by the user whose service
        /// credentials are used to create it. How you learn the status and results of a job depends on the parameters
        /// you include with the job creation request:
        /// * By callback notification: Include the `callback_url` parameter to specify a URL to which the service is to
        /// send callback notifications when the status of the job changes. Optionally, you can also include the
        /// `events` and `user_token` parameters to subscribe to specific events and to specify a string that is to be
        /// included with each notification for the job.
        /// * By polling the service: Omit the `callback_url`, `events`, and `user_token` parameters. You must then use
        /// the **Check jobs** or **Check a job** methods to check the status of the job, using the latter to retrieve
        /// the results when the job is complete.
        ///
        /// The two approaches are not mutually exclusive. You can poll the service for job status or obtain results
        /// from the service manually even if you include a callback URL. In both cases, you can include the
        /// `results_ttl` parameter to specify how long the results are to remain available after the job is complete.
        /// For detailed usage information about the two approaches, including callback notifications, see [Creating a
        /// job](https://console.bluemix.net/docs/services/speech-to-text/async.html#create). Using the HTTPS **Check a
        /// job** method to retrieve results is more secure than receiving them via callback notification over HTTP
        /// because it provides confidentiality in addition to authentication and data integrity.
        ///
        /// The method supports the same basic parameters as other HTTP and WebSocket recognition requests. It also
        /// supports the following parameters specific to the asynchronous interface:
        /// * `callback_url`
        /// * `events`
        /// * `user_token`
        /// * `results_ttl`
        ///
        /// The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming
        /// audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during
        /// transcoding. (For the `audio/l16` format, you can specify the endianness.)
        ///
        /// ### Audio formats (content types)
        ///
        ///  Use the `Content-Type` parameter to specify the audio format (MIME type) of the audio:
        /// * `audio/basic` (Use only with narrowband models.)
        /// * `audio/flac`
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
        /// For information about the supported audio formats, including specifying the sampling rate, channels, and
        /// endianness for the indicated formats, see [Audio
        /// formats](https://console.bluemix.net/docs/services/speech-to-text/audio-formats.html).
        /// </summary>
        /// <param name="audio">The audio to transcribe in the format specified by the `Content-Type` header.</param>
        /// <param name="contentType">The type of the input.</param>
        /// <param name="model">The identifier of the model that is to be used for the recognition request. (optional,
        /// default to en-US_BroadbandModel)</param>
        /// <param name="callbackUrl">A URL to which callback notifications are to be sent. The URL must already be
        /// successfully white-listed by using the **Register a callback** method. You can include the same callback URL
        /// with any number of job creation requests. Omit the parameter to poll the service for job completion and
        /// results.
        ///
        /// Use the `user_token` parameter to specify a unique user-specified string with each job to differentiate the
        /// callback notifications for the jobs. (optional)</param>
        /// <param name="events">If the job includes a callback URL, a comma-separated list of notification events to
        /// which to subscribe. Valid events are
        /// * `recognitions.started` generates a callback notification when the service begins to process the job.
        /// * `recognitions.completed` generates a callback notification when the job is complete. You must use the
        /// **Check a job** method to retrieve the results before they time out or are deleted.
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
        /// <param name="customizationId">The customization ID (GUID) of a custom language model that is to be used with
        /// the recognition request. The base model of the specified custom language model must match the model
        /// specified with the `model` parameter. You must make the request with service credentials created for the
        /// instance of the service that owns the custom model. By default, no custom language model is used.
        /// (optional)</param>
        /// <param name="acousticCustomizationId">The customization ID (GUID) of a custom acoustic model that is to be
        /// used with the recognition request. The base model of the specified custom acoustic model must match the
        /// model specified with the `model` parameter. You must make the request with service credentials created for
        /// the instance of the service that owns the custom model. By default, no custom acoustic model is used.
        /// (optional)</param>
        /// <param name="baseModelVersion">The version of the specified base model that is to be used with recognition
        /// request. Multiple versions of a base model can exist when a model is updated for internal improvements. The
        /// parameter is intended primarily for use with custom models that have been upgraded for a new base model. The
        /// default value depends on whether the parameter is used with or without a custom model. For more information,
        /// see [Base model version](https://console.bluemix.net/docs/services/speech-to-text/input.html#version).
        /// (optional)</param>
        /// <param name="customizationWeight">If you specify the customization ID (GUID) of a custom language model with
        /// the recognition request, the customization weight tells the service how much weight to give to words from
        /// the custom language model compared to those from the base model for the current request.
        ///
        /// Specify a value between 0.0 and 1.0. Unless a different customization weight was specified for the custom
        /// model when it was trained, the default value is 0.3. A customization weight that you specify overrides a
        /// weight that was specified when the custom model was trained.
        ///
        /// The default value yields the best performance in general. Assign a higher value if your audio makes frequent
        /// use of OOV words from the custom model. Use caution when setting the weight: a higher value can improve the
        /// accuracy of phrases from the custom model's domain, but it can negatively affect performance on non-domain
        /// phrases. (optional)</param>
        /// <param name="inactivityTimeout">The time in seconds after which, if only silence (no speech) is detected in
        /// submitted audio, the connection is closed with a 400 error. The parameter is useful for stopping audio
        /// submission from a live microphone when a user simply walks away. Use `-1` for infinity. (optional, default
        /// to 30)</param>
        /// <param name="keywords">An array of keyword strings to spot in the audio. Each keyword string can include one
        /// or more string tokens. Keywords are spotted only in the final results, not in interim hypotheses. If you
        /// specify any keywords, you must also specify a keywords threshold. You can spot a maximum of 1000 keywords.
        /// Omit the parameter or specify an empty array if you do not need to spot keywords. (optional)</param>
        /// <param name="keywordsThreshold">A confidence value that is the lower bound for spotting a keyword. A word is
        /// considered to match a keyword if its confidence is greater than or equal to the threshold. Specify a
        /// probability between 0.0 and 1.0. No keyword spotting is performed if you omit the parameter. If you specify
        /// a threshold, you must also specify one or more keywords. (optional)</param>
        /// <param name="maxAlternatives">The maximum number of alternative transcripts that the service is to return.
        /// By default, a single transcription is returned. (optional, default to 1)</param>
        /// <param name="wordAlternativesThreshold">A confidence value that is the lower bound for identifying a
        /// hypothesis as a possible word alternative (also known as "Confusion Networks"). An alternative word is
        /// considered if its confidence is greater than or equal to the threshold. Specify a probability between 0.0
        /// and 1.0. No alternative words are computed if you omit the parameter. (optional)</param>
        /// <param name="wordConfidence">If `true`, the service returns a confidence measure in the range of 0.0 to 1.0
        /// for each word. By default, no word confidence measures are returned. (optional, default to false)</param>
        /// <param name="timestamps">If `true`, the service returns time alignment for each word. By default, no
        /// timestamps are returned. (optional, default to false)</param>
        /// <param name="profanityFilter">If `true`, the service filters profanity from all output except for keyword
        /// results by replacing inappropriate words with a series of asterisks. Set the parameter to `false` to return
        /// results with no censoring. Applies to US English transcription only. (optional, default to true)</param>
        /// <param name="smartFormatting">If `true`, the service converts dates, times, series of digits and numbers,
        /// phone numbers, currency values, and internet addresses into more readable, conventional representations in
        /// the final transcript of a recognition request. For US English, the service also converts certain keyword
        /// strings to punctuation symbols. By default, no smart formatting is performed. Applies to US English and
        /// Spanish transcription only. (optional, default to false)</param>
        /// <param name="speakerLabels">If `true`, the response includes labels that identify which words were spoken by
        /// which participants in a multi-person exchange. By default, no speaker labels are returned. Setting
        /// `speaker_labels` to `true` forces the `timestamps` parameter to be `true`, regardless of whether you specify
        /// `false` for the parameter.
        ///
        ///  To determine whether a language model supports speaker labels, use the **Get models** method and check that
        /// the attribute `speaker_labels` is set to `true`. You can also refer to [Speaker
        /// labels](https://console.bluemix.net/docs/services/speech-to-text/output.html#speaker_labels). (optional,
        /// default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="RecognitionJob" />RecognitionJob</returns>
        public RecognitionJob CreateJob(byte[] audio, string contentType, string model = null, string callbackUrl = null, string events = null, string userToken = null, long? resultsTtl = null, string customizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, Dictionary<string, object> customData = null)
        {
            if (audio == null)
                throw new ArgumentNullException(nameof(audio));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            RecognitionJob result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/recognitions");

                if (!string.IsNullOrEmpty(contentType))
                    restRequest.WithHeader("Content-Type", contentType);
                if (!string.IsNullOrEmpty(model))
                    restRequest.WithArgument("model", model);
                if (!string.IsNullOrEmpty(callbackUrl))
                    restRequest.WithArgument("callback_url", callbackUrl);
                if (!string.IsNullOrEmpty(events))
                    restRequest.WithArgument("events", events);
                if (!string.IsNullOrEmpty(userToken))
                    restRequest.WithArgument("user_token", userToken);
                if (resultsTtl != null)
                    restRequest.WithArgument("results_ttl", resultsTtl);
                if (!string.IsNullOrEmpty(customizationId))
                    restRequest.WithArgument("customization_id", customizationId);
                if (!string.IsNullOrEmpty(acousticCustomizationId))
                    restRequest.WithArgument("acoustic_customization_id", acousticCustomizationId);
                if (!string.IsNullOrEmpty(baseModelVersion))
                    restRequest.WithArgument("base_model_version", baseModelVersion);
                if (customizationWeight != null)
                    restRequest.WithArgument("customization_weight", customizationWeight);
                if (inactivityTimeout != null)
                    restRequest.WithArgument("inactivity_timeout", inactivityTimeout);
                if (keywords != null)
                    restRequest.WithArgument("keywords", keywords != null && keywords.Count > 0 ? string.Join(",", keywords.ToArray()) : null);
                if (keywordsThreshold != null)
                    restRequest.WithArgument("keywords_threshold", keywordsThreshold);
                if (maxAlternatives != null)
                    restRequest.WithArgument("max_alternatives", maxAlternatives);
                if (wordAlternativesThreshold != null)
                    restRequest.WithArgument("word_alternatives_threshold", wordAlternativesThreshold);
                if (wordConfidence != null)
                    restRequest.WithArgument("word_confidence", wordConfidence);
                if (timestamps != null)
                    restRequest.WithArgument("timestamps", timestamps);
                if (profanityFilter != null)
                    restRequest.WithArgument("profanity_filter", profanityFilter);
                if (smartFormatting != null)
                    restRequest.WithArgument("smart_formatting", smartFormatting);
                if (speakerLabels != null)
                    restRequest.WithArgument("speaker_labels", speakerLabels);
                var audioContent = new ByteArrayContent(audio);
                System.Net.Http.Headers.MediaTypeHeaderValue audioType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out audioType);
                audioContent.Headers.ContentType = audioType;
                restRequest.WithBodyContent(audioContent);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<RecognitionJob>().Result;
                if(result == null)
                    result = new RecognitionJob();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// time to live for the results expires. You must submit the request with the service credentials of the user
        /// who created the job.
        /// </summary>
        /// <param name="id">The ID of the asynchronous job.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteJob(string id, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/recognitions/{id}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Register a callback.
        ///
        /// Registers a callback URL with the service for use with subsequent asynchronous recognition requests. The
        /// service attempts to register, or white-list, the callback URL if it is not already registered by sending a
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
        /// seconds, it does not white-list the URL; it instead sends status code 400 in response to the **Register a
        /// callback** request. If the requested callback URL is already white-listed, the service responds to the
        /// initial registration request with response code 200.
        ///
        /// If you specify a user secret with the request, the service uses it as a key to calculate an HMAC-SHA1
        /// signature of the challenge string in its response to the `POST` request. It sends this signature in the
        /// `X-Callback-Signature` header of its `GET` request to the URL during registration. It also uses the secret
        /// to calculate a signature over the payload of every callback notification that uses the URL. The signature
        /// provides authentication and data integrity for HTTP communications.
        ///
        /// After you successfully register a callback URL, you can use it with an indefinite number of recognition
        /// requests. You can register a maximum of 20 callback URLS in a one-hour span of time. For more information,
        /// see [Registering a callback
        /// URL](https://console.bluemix.net/docs/services/speech-to-text/async.html#register).
        /// </summary>
        /// <param name="callbackUrl">An HTTP or HTTPS URL to which callback notifications are to be sent. To be
        /// white-listed, the URL must successfully echo the challenge string during URL verification. During
        /// verification, the client can also check the signature that the service sends in the `X-Callback-Signature`
        /// header to verify the origin of the request.</param>
        /// <param name="userSecret">A user-specified string that the service uses to generate the HMAC-SHA1 signature
        /// that it sends via the `X-Callback-Signature` header. The service includes the header during URL verification
        /// and with every notification sent to the callback URL. It calculates the signature over the payload of the
        /// notification. If you omit the parameter, the service does not send the header. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="RegisterStatus" />RegisterStatus</returns>
        public RegisterStatus RegisterCallback(string callbackUrl, string userSecret = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(callbackUrl))
                throw new ArgumentNullException(nameof(callbackUrl));
            RegisterStatus result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/register_callback");

                if (!string.IsNullOrEmpty(callbackUrl))
                    restRequest.WithArgument("callback_url", callbackUrl);
                if (!string.IsNullOrEmpty(userSecret))
                    restRequest.WithArgument("user_secret", userSecret);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<RegisterStatus>().Result;
                if(result == null)
                    result = new RegisterStatus();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Unregister a callback.
        ///
        /// Unregisters a callback URL that was previously white-listed with a **Register a callback** request for use
        /// with the asynchronous interface. Once unregistered, the URL can no longer be used with asynchronous
        /// recognition requests.
        /// </summary>
        /// <param name="callbackUrl">The callback URL that is to be unregistered.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel UnregisterCallback(string callbackUrl, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(callbackUrl))
                throw new ArgumentNullException(nameof(callbackUrl));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/unregister_callback");

                if (!string.IsNullOrEmpty(callbackUrl))
                    restRequest.WithArgument("callback_url", callbackUrl);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// </summary>
        /// <param name="createLanguageModel">A `CreateLanguageModel` object that provides basic information about the
        /// new custom language model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="LanguageModel" />LanguageModel</returns>
        public LanguageModel CreateLanguageModel(CreateLanguageModel createLanguageModel, Dictionary<string, object> customData = null)
        {
            if (createLanguageModel == null)
                throw new ArgumentNullException(nameof(createLanguageModel));
            LanguageModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations");

                restRequest.WithBody<CreateLanguageModel>(createLanguageModel);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<LanguageModel>().Result;
                if(result == null)
                    result = new LanguageModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a custom language model.
        ///
        /// Deletes an existing custom language model. The custom model cannot be deleted if another request, such as
        /// adding a corpus to the model, is currently being processed. You must use credentials for the instance of the
        /// service that owns a model to delete it.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteLanguageModel(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a custom language model.
        ///
        /// Gets information about a specified custom language model. You must use credentials for the instance of the
        /// service that owns a model to list information about it.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="LanguageModel" />LanguageModel</returns>
        public LanguageModel GetLanguageModel(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            LanguageModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<LanguageModel>().Result;
                if(result == null)
                    result = new LanguageModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// </summary>
        /// <param name="language">The identifier of the language for which custom language or custom acoustic models
        /// are to be returned (for example, `en-US`). Omit the parameter to see all custom language or custom acoustic
        /// models owned by the requesting service credentials. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="LanguageModels" />LanguageModels</returns>
        public LanguageModels ListLanguageModels(string language = null, Dictionary<string, object> customData = null)
        {
            LanguageModels result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations");

                if (!string.IsNullOrEmpty(language))
                    restRequest.WithArgument("language", language);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<LanguageModels>().Result;
                if(result == null)
                    result = new LanguageModels();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Reset a custom language model.
        ///
        /// Resets a custom language model by removing all corpora and words from the model. Resetting a custom language
        /// model initializes the model to its state when it was first created. Metadata such as the name and language
        /// of the model are preserved, but the model's words resource is removed and must be re-created. You must use
        /// credentials for the instance of the service that owns a model to reset it.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel ResetLanguageModel(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/reset");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Train a custom language model.
        ///
        /// Initiates the training of a custom language model with new corpora, custom words, or both. After adding,
        /// modifying, or deleting corpora or words for a custom language model, use this method to begin the actual
        /// training of the model on the latest data. You can specify whether the custom language model is to be trained
        /// with all words from its words resource or only with words that were added or modified by the user. You must
        /// use credentials for the instance of the service that owns a model to train it.
        ///
        /// The training method is asynchronous. It can take on the order of minutes to complete depending on the amount
        /// of data on which the service is being trained and the current load on the service. The method returns an
        /// HTTP 200 response code to indicate that the training process has begun.
        ///
        /// You can monitor the status of the training by using the **Get a custom language model** method to poll the
        /// model's status. Use a loop to check the status every 10 seconds. The method returns a `LanguageModel` object
        /// that includes `status` and `progress` fields. A status of `available` means that the custom model is trained
        /// and ready to use. The service cannot accept subsequent training requests, or requests to add new corpora or
        /// words, until the existing request completes.
        ///
        /// Training can fail to start for the following reasons:
        /// * The service is currently handling another request for the custom model, such as another training request
        /// or a request to add a corpus or words to the model.
        /// * No training data (corpora or words) have been added to the custom model.
        /// * One or more words that were added to the custom model have invalid sounds-like pronunciations that you
        /// must fix.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordTypeToAdd">The type of words from the custom language model's words resource on which to
        /// train the model:
        /// * `all` (the default) trains the model on all new words, regardless of whether they were extracted from
        /// corpora or were added or modified by the user.
        /// * `user` trains the model only on new words that were added or modified by the user; the model is not
        /// trained on new words extracted from corpora. (optional, default to all)</param>
        /// <param name="customizationWeight">Specifies a customization weight for the custom language model. The
        /// customization weight tells the service how much weight to give to words from the custom language model
        /// compared to those from the base model for speech recognition. Specify a value between 0.0 and 1.0; the
        /// default is 0.3.
        ///
        /// The default value yields the best performance in general. Assign a higher value if your audio makes frequent
        /// use of OOV words from the custom model. Use caution when setting the weight: a higher value can improve the
        /// accuracy of phrases from the custom model's domain, but it can negatively affect performance on non-domain
        /// phrases.
        ///
        /// The value that you assign is used for all recognition requests that use the model. You can override it for
        /// any recognition request by specifying a customization weight for that request. (optional, default to
        /// 0.3)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel TrainLanguageModel(string customizationId, string wordTypeToAdd = null, double? customizationWeight = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/train");

                if (!string.IsNullOrEmpty(wordTypeToAdd))
                    restRequest.WithArgument("word_type_to_add", wordTypeToAdd);
                if (customizationWeight != null)
                    restRequest.WithArgument("customization_weight", customizationWeight);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// You can monitor the status of the upgrade by using the **Get a custom language model** method to poll the
        /// model's status. The method returns a `LanguageModel` object that includes `status` and `progress` fields.
        /// Use a loop to check the status every 10 seconds. While it is being upgraded, the custom model has the status
        /// `upgrading`. When the upgrade is complete, the model resumes the status that it had prior to upgrade. The
        /// service cannot accept subsequent requests for the model until the upgrade completes.
        ///
        /// For more information, see [Upgrading custom
        /// models](https://console.bluemix.net/docs/services/speech-to-text/custom-upgrade.html).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel UpgradeLanguageModel(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/upgrade_model");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// model for the new data by using the **Train a custom language model** method.
        ///
        /// Submit a plain text file that contains sample sentences from the domain of interest to enable the service to
        /// extract words in context. The more sentences you add that represent the context in which speakers use words
        /// from the domain, the better the service's recognition accuracy. For guidelines about adding a corpus text
        /// file and for information about how the service parses a corpus file, see [Preparing a corpus text
        /// file](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#prepareCorpus).
        ///
        /// The call returns an HTTP 201 response code if the corpus is valid. The service then asynchronously processes
        /// the contents of the corpus and automatically extracts new words that it finds. This can take on the order of
        /// a minute or two to complete depending on the total number of words and the number of new words in the
        /// corpus, as well as the current load on the service. You cannot submit requests to add additional corpora or
        /// words to the custom model, or to train the model, until the service's analysis of the corpus for the current
        /// request completes. Use the **List a corpus** method to check the status of the analysis.
        ///
        /// The service auto-populates the model's words resource with any word that is not found in its base
        /// vocabulary; these are referred to as out-of-vocabulary (OOV) words. You can use the **List custom words**
        /// method to examine the words resource, using other words method to eliminate typos and modify how words are
        /// pronounced as needed.
        ///
        /// To add a corpus file that has the same name as an existing corpus, set the `allow_overwrite` parameter to
        /// `true`; otherwise, the request fails. Overwriting an existing corpus causes the service to process the
        /// corpus text file and extract OOV words anew. Before doing so, it removes any OOV words associated with the
        /// existing corpus from the model's words resource unless they were also added by another corpus or they have
        /// been modified in some way with the **Add custom words** or **Add a custom word** method.
        ///
        /// The service limits the overall amount of data that you can add to a custom model to a maximum of 10 million
        /// total words from all corpora combined. Also, you can add no more than 30 thousand custom (OOV) words to a
        /// model; this includes words that the service extracts from corpora and words that you add directly.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="corpusName">The name of the corpus for the custom language model. When adding a corpus, do not
        /// include spaces in the name; use a localized name that matches the language of the custom model; and do not
        /// use the name `user`, which is reserved by the service to denote custom words added or modified by the
        /// user.</param>
        /// <param name="corpusFile">A plain text file that contains the training data for the corpus. Encode the file
        /// in UTF-8 if it contains non-ASCII characters; the service assumes UTF-8 encoding if it encounters non-ASCII
        /// characters. With cURL, use the `--data-binary` option to upload the file for the request.</param>
        /// <param name="allowOverwrite">If `true`, the specified corpus or audio resource overwrites an existing corpus
        /// or audio resource with the same name. If `false`, the request fails if a corpus or audio resource with the
        /// same name already exists. The parameter has no effect if a corpus or audio resource with the same name does
        /// not already exist. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel AddCorpus(string customizationId, string corpusName, System.IO.FileStream corpusFile, bool? allowOverwrite = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            if (corpusFile == null)
                throw new ArgumentNullException(nameof(corpusFile));
            BaseModel result = null;

            try
            {
                var formData = new MultipartFormDataContent();

                if (corpusFile != null)
                {
                    var corpusFileContent = new ByteArrayContent((corpusFile as Stream).ReadAllBytes());
                    System.Net.Http.Headers.MediaTypeHeaderValue contentType;
                    System.Net.Http.Headers.MediaTypeHeaderValue.TryParse("text/plain", out contentType);
                    corpusFileContent.Headers.ContentType = contentType;
                    formData.Add(corpusFileContent, "corpus_file", corpusFile.Name);
                }

                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");

                if (allowOverwrite != null)
                    restRequest.WithArgument("allow_overwrite", allowOverwrite);
                restRequest.WithBodyContent(formData);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete a corpus.
        ///
        /// Deletes an existing corpus from a custom language model. The service removes any out-of-vocabulary (OOV)
        /// words associated with the corpus from the custom model's words resource unless they were also added by
        /// another corpus or they have been modified in some way with the **Add custom words** or **Add a custom word**
        /// method. Removing a corpus does not affect the custom model until you train the model with the **Train a
        /// custom language model** method. You must use credentials for the instance of the service that owns a model
        /// to delete its corpora.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="corpusName">The name of the corpus for the custom language model. When adding a corpus, do not
        /// include spaces in the name; use a localized name that matches the language of the custom model; and do not
        /// use the name `user`, which is reserved by the service to denote custom words added or modified by the
        /// user.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteCorpus(string customizationId, string corpusName, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a corpus.
        ///
        /// Gets information about a corpus from a custom language model. The information includes the total number of
        /// words and out-of-vocabulary (OOV) words, name, and status of the corpus. You must use credentials for the
        /// instance of the service that owns a model to list its corpora.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="corpusName">The name of the corpus for the custom language model. When adding a corpus, do not
        /// include spaces in the name; use a localized name that matches the language of the custom model; and do not
        /// use the name `user`, which is reserved by the service to denote custom words added or modified by the
        /// user.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Corpus" />Corpus</returns>
        public Corpus GetCorpus(string customizationId, string corpusName, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(corpusName))
                throw new ArgumentNullException(nameof(corpusName));
            Corpus result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora/{corpusName}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Corpus>().Result;
                if(result == null)
                    result = new Corpus();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List corpora.
        ///
        /// Lists information about all corpora from a custom language model. The information includes the total number
        /// of words and out-of-vocabulary (OOV) words, name, and status of each corpus. You must use credentials for
        /// the instance of the service that owns a model to list its corpora.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Corpora" />Corpora</returns>
        public Corpora ListCorpora(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            Corpora result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/corpora");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Corpora>().Result;
                if(result == null)
                    result = new Corpora();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
        /// <summary>
        /// Add a custom word.
        ///
        /// Adds a custom word to a custom language model. The service populates the words resource for a custom model
        /// with out-of-vocabulary (OOV) words found in each corpus added to the model. You can use this method to add a
        /// word or to modify an existing word in the words resource. The words resource for a model can contain a
        /// maximum of 30 thousand custom (OOV) words, including words that the service extracts from corpora and words
        /// that you add directly.
        ///
        /// You must use credentials for the instance of the service that owns a model to add or modify a custom word
        /// for the model. Adding or modifying a custom word does not affect the custom model until you train the model
        /// for the new data by using the **Train a custom language model** method.
        ///
        /// Use the `word_name` parameter to specify the custom word that is to be added or modified. Use the
        /// `CustomWord` object to provide one or both of the optional `sounds_like` and `display_as` fields for the
        /// word.
        /// * The `sounds_like` field provides an array of one or more pronunciations for the word. Use the parameter to
        /// specify how the word can be pronounced by users. Use the parameter for words that are difficult to
        /// pronounce, foreign words, acronyms, and so on. For example, you might specify that the word `IEEE` can sound
        /// like `i triple e`. You can specify a maximum of five sounds-like pronunciations for a word. For information
        /// about pronunciation rules, see [Using the sounds_like
        /// field](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#soundsLike).
        /// * The `display_as` field provides a different way of spelling the word in a transcript. Use the parameter
        /// when you want the word to appear different from its usual representation or from its spelling in corpora
        /// training data. For example, you might indicate that the word `IBM(trademark)` is to be displayed as
        /// `IBM&trade;`. For more information, see [Using the display_as
        /// field](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#displayAs).
        ///
        ///
        /// If you add a custom word that already exists in the words resource for the custom model, the new definition
        /// overwrites the existing data for the word. If the service encounters an error, it does not add the word to
        /// the words resource. Use the **List a custom word** method to review the word that you add.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordName">The custom word for the custom language model. When you add or update a custom word
        /// with the **Add a custom word** method, do not include spaces in the word. Use a `-` (dash) or `_`
        /// (underscore) to connect the tokens of compound words.</param>
        /// <param name="customWord">A `CustomWord` object that provides information about the specified custom word.
        /// Specify an empty object to add a word with no sounds-like or display-as information.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel AddWord(string customizationId, string wordName, CustomWord customWord, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            if (customWord == null)
                throw new ArgumentNullException(nameof(customWord));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PutAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");

                restRequest.WithBody<CustomWord>(customWord);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Add custom words.
        ///
        /// Adds one or more custom words to a custom language model. The service populates the words resource for a
        /// custom model with out-of-vocabulary (OOV) words found in each corpus added to the model. You can use this
        /// method to add additional words or to modify existing words in the words resource. The words resource for a
        /// model can contain a maximum of 30 thousand custom (OOV) words, including words that the service extracts
        /// from corpora and words that you add directly.
        ///
        /// You must use credentials for the instance of the service that owns a model to add or modify custom words for
        /// the model. Adding or modifying custom words does not affect the custom model until you train the model for
        /// the new data by using the **Train a custom language model** method.
        ///
        /// You add custom words by providing a `CustomWords` object, which is an array of `CustomWord` objects, one per
        /// word. You must use the object's `word` parameter to identify the word that is to be added. You can also
        /// provide one or both of the optional `sounds_like` and `display_as` fields for each word.
        /// * The `sounds_like` field provides an array of one or more pronunciations for the word. Use the parameter to
        /// specify how the word can be pronounced by users. Use the parameter for words that are difficult to
        /// pronounce, foreign words, acronyms, and so on. For example, you might specify that the word `IEEE` can sound
        /// like `i triple e`. You can specify a maximum of five sounds-like pronunciations for a word. For information
        /// about pronunciation rules, see [Using the sounds_like
        /// field](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#soundsLike).
        /// * The `display_as` field provides a different way of spelling the word in a transcript. Use the parameter
        /// when you want the word to appear different from its usual representation or from its spelling in corpora
        /// training data. For example, you might indicate that the word `IBM(trademark)` is to be displayed as
        /// `IBM&trade;`. For more information, see [Using the display_as
        /// field](https://console.bluemix.net/docs/services/speech-to-text/language-resource.html#displayAs).
        ///
        ///
        /// If you add a custom word that already exists in the words resource for the custom model, the new definition
        /// overwrites the existing data for the word. If the service encounters an error with the input data, it
        /// returns a failure code and does not add any of the words to the words resource.
        ///
        /// The call returns an HTTP 201 response code if the input data is valid. It then asynchronously processes the
        /// words to add them to the model's words resource. The time that it takes for the analysis to complete depends
        /// on the number of new words that you add but is generally faster than adding a corpus or training a model.
        ///
        /// You can monitor the status of the request by using the **List a custom language model** method to poll the
        /// model's status. Use a loop to check the status every 10 seconds. The method returns a `Customization` object
        /// that includes a `status` field. A status of `ready` means that the words have been added to the custom
        /// model. The service cannot accept requests to add new corpora or words or to train the model until the
        /// existing request completes.
        ///
        /// You can use the **List custom words** or **List a custom word** method to review the words that you add.
        /// Words with an invalid `sounds_like` field include an `error` field that describes the problem. You can use
        /// other words-related methods to correct errors, eliminate typos, and modify how words are pronounced as
        /// needed.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customWords">A `CustomWords` object that provides information about one or more custom words
        /// that are to be added to or updated in the custom language model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel AddWords(string customizationId, CustomWords customWords, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (customWords == null)
                throw new ArgumentNullException(nameof(customWords));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");

                restRequest.WithBody<CustomWords>(customWords);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// service removes only the custom pronunciation for the word; the word remains in the base vocabulary.
        /// Removing a custom word does not affect the custom model until you train the model with the **Train a custom
        /// language model** method. You must use credentials for the instance of the service that owns a model to
        /// delete its words.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordName">The custom word for the custom language model. When you add or update a custom word
        /// with the **Add a custom word** method, do not include spaces in the word. Use a `-` (dash) or `_`
        /// (underscore) to connect the tokens of compound words.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteWord(string customizationId, string wordName, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a custom word.
        ///
        /// Gets information about a custom word from a custom language model. You must use credentials for the instance
        /// of the service that owns a model to query information about its words.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordName">The custom word for the custom language model. When you add or update a custom word
        /// with the **Add a custom word** method, do not include spaces in the word. Use a `-` (dash) or `_`
        /// (underscore) to connect the tokens of compound words.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Word" />Word</returns>
        public Word GetWord(string customizationId, string wordName, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(wordName))
                throw new ArgumentNullException(nameof(wordName));
            Word result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words/{wordName}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Word>().Result;
                if(result == null)
                    result = new Word();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// List custom words.
        ///
        /// Lists information about custom words from a custom language model. You can list all words from the custom
        /// model's words resource, only custom words that were added or modified by the user, or only out-of-vocabulary
        /// (OOV) words that were extracted from corpora. You can also indicate the order in which the service is to
        /// return words; by default, words are listed in ascending alphabetical order. You must use credentials for the
        /// instance of the service that owns a model to query information about its words.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom language model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="wordType">The type of words to be listed from the custom language model's words resource:
        /// * `all` (the default) shows all words.
        /// * `user` shows only custom words that were added or modified by the user.
        /// * `corpora` shows only OOV that were extracted from corpora. (optional, default to all)</param>
        /// <param name="sort">Indicates the order in which the words are to be listed, `alphabetical` or by `count`.
        /// You can prepend an optional `+` or `-` to an argument to indicate whether the results are to be sorted in
        /// ascending or descending order. By default, words are sorted in ascending alphabetical order. For
        /// alphabetical ordering, the lexicographical precedence is numeric values, uppercase letters, and lowercase
        /// letters. For count ordering, values with the same count are ordered alphabetically. With cURL, URL encode
        /// the `+` symbol as `%2B`. (optional, default to alphabetical)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="Words" />Words</returns>
        public Words ListWords(string customizationId, string wordType = null, string sort = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            Words result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/customizations/{customizationId}/words");

                if (!string.IsNullOrEmpty(wordType))
                    restRequest.WithArgument("word_type", wordType);
                if (!string.IsNullOrEmpty(sort))
                    restRequest.WithArgument("sort", sort);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<Words>().Result;
                if(result == null)
                    result = new Words();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// </summary>
        /// <param name="createAcousticModel">A `CreateAcousticModel` object that provides basic information about the
        /// new custom acoustic model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="AcousticModel" />AcousticModel</returns>
        public AcousticModel CreateAcousticModel(CreateAcousticModel createAcousticModel, Dictionary<string, object> customData = null)
        {
            if (createAcousticModel == null)
                throw new ArgumentNullException(nameof(createAcousticModel));
            AcousticModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations");

                restRequest.WithBody<CreateAcousticModel>(createAcousticModel);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<AcousticModel>().Result;
                if(result == null)
                    result = new AcousticModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteAcousticModel(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get a custom acoustic model.
        ///
        /// Gets information about a specified custom acoustic model. You must use credentials for the instance of the
        /// service that owns a model to list information about it.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="AcousticModel" />AcousticModel</returns>
        public AcousticModel GetAcousticModel(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            AcousticModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<AcousticModel>().Result;
                if(result == null)
                    result = new AcousticModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// </summary>
        /// <param name="language">The identifier of the language for which custom language or custom acoustic models
        /// are to be returned (for example, `en-US`). Omit the parameter to see all custom language or custom acoustic
        /// models owned by the requesting service credentials. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="AcousticModels" />AcousticModels</returns>
        public AcousticModels ListAcousticModels(string language = null, Dictionary<string, object> customData = null)
        {
            AcousticModels result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/acoustic_customizations");

                if (!string.IsNullOrEmpty(language))
                    restRequest.WithArgument("language", language);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<AcousticModels>().Result;
                if(result == null)
                    result = new AcousticModels();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// of the model are preserved, but the model's audio resources are removed and must be re-created. You must use
        /// credentials for the instance of the service that owns a model to reset it.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel ResetAcousticModel(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/reset");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// The training method is asynchronous. It can take on the order of minutes or hours to complete depending on
        /// the total amount of audio data on which the custom acoustic model is being trained and the current load on
        /// the service. Typically, training a custom acoustic model takes approximately two to four times the length of
        /// its audio data. The range of time depends on the model being trained and the nature of the audio, such as
        /// whether the audio is clean or noisy. The method returns an HTTP 200 response code to indicate that the
        /// training process has begun.
        ///
        /// You can monitor the status of the training by using the **Get a custom acoustic model** method to poll the
        /// model's status. Use a loop to check the status once a minute. The method returns an `AcousticModel` object
        /// that includes `status` and `progress` fields. A status of `available` indicates that the custom model is
        /// trained and ready to use. The service cannot accept subsequent training requests, or requests to add new
        /// audio resources, until the existing request completes.
        ///
        /// You can use the optional `custom_language_model_id` parameter to specify the GUID of a separately created
        /// custom language model that is to be used during training. Specify a custom language model if you have
        /// verbatim transcriptions of the audio files that you have added to the custom model or you have either
        /// corpora (text files) or a list of words that are relevant to the contents of the audio files. For
        /// information about creating a separate custom language model, see [Creating a custom language
        /// model](https://console.bluemix.net/docs/services/speech-to-text/language-create.html).
        ///
        /// Training can fail to start for the following reasons:
        /// * The service is currently handling another request for the custom model, such as another training request
        /// or a request to add audio resources to the model.
        /// * The custom model contains less than 10 minutes or more than 50 hours of audio data.
        /// * One or more of the custom model's audio resources is invalid.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customLanguageModelId">The customization ID (GUID) of a custom language model that is to be
        /// used during training of the custom acoustic model. Specify a custom language model that has been trained
        /// with verbatim transcriptions of the audio resources or that contains words that are relevant to the contents
        /// of the audio resources. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel TrainAcousticModel(string customizationId, string customLanguageModelId = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/train");

                if (!string.IsNullOrEmpty(customLanguageModelId))
                    restRequest.WithArgument("custom_language_model_id", customLanguageModelId);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// You can monitor the status of the upgrade by using the **Get a custom acoustic model** method to poll the
        /// model's status. The method returns an `AcousticModel` object that includes `status` and `progress` fields.
        /// Use a loop to check the status once a minute. While it is being upgraded, the custom model has the status
        /// `upgrading`. When the upgrade is complete, the model resumes the status that it had prior to upgrade. The
        /// service cannot accept subsequent requests for the model until the upgrade completes.
        ///
        /// If the custom acoustic model was trained with a separately created custom language model, you must use the
        /// `custom_language_model_id` parameter to specify the GUID of that custom language model. The custom language
        /// model must be upgraded before the custom acoustic model can be upgraded. Omit the parameter if the custom
        /// acoustic model was not trained with a custom language model.
        ///
        /// For more information, see [Upgrading custom
        /// models](https://console.bluemix.net/docs/services/speech-to-text/custom-upgrade.html).
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customLanguageModelId">If the custom acoustic model was trained with a custom language model,
        /// the customization ID (GUID) of that custom language model. The custom language model must be upgraded before
        /// the custom acoustic model can be upgraded. (optional)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel UpgradeAcousticModel(string customizationId, string customLanguageModelId = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/upgrade_model");

                if (!string.IsNullOrEmpty(customLanguageModelId))
                    restRequest.WithArgument("custom_language_model_id", customLanguageModelId);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// acoustic model until you train the model for the new data by using the **Train a custom acoustic model**
        /// method.
        ///
        /// You can add individual audio files or an archive file that contains multiple audio files. Adding multiple
        /// audio files via a single archive file is significantly more efficient than adding each file individually.
        /// You can add audio resources in any format that the service supports for speech recognition.
        ///
        /// You can use this method to add any number of audio resources to a custom model by calling the method once
        /// for each audio or archive file. But the addition of one audio resource must be fully complete before you can
        /// add another. You must add a minimum of 10 minutes and a maximum of 50 hours of audio that includes speech,
        /// not just silence, to a custom acoustic model before you can train it. No audio resource, audio- or
        /// archive-type, can be larger than 100 MB. To add an audio resource that has the same name as an existing
        /// audio resource, set the `allow_overwrite` parameter to `true`; otherwise, the request fails.
        ///
        /// The method is asynchronous. It can take several seconds to complete depending on the duration of the audio
        /// and, in the case of an archive file, the total number of audio files being processed. The service returns a
        /// 201 response code if the audio is valid. It then asynchronously analyzes the contents of the audio file or
        /// files and automatically extracts information about the audio such as its length, sampling rate, and
        /// encoding. You cannot submit requests to add additional audio resources to a custom acoustic model, or to
        /// train the model, until the service's analysis of all audio files for the current request completes.
        ///
        /// To determine the status of the service's analysis of the audio, use the **Get an audio resource** method to
        /// poll the status of the audio. The method accepts the customization ID of the custom model and the name of
        /// the audio resource, and it returns the status of the resource. Use a loop to check the status of the audio
        /// every few seconds until it becomes `ok`.
        ///
        /// ### Content types for audio-type resources
        ///
        ///  You can add an individual audio file in any format that the service supports for speech recognition. For an
        /// audio-type resource, use the `Content-Type` parameter to specify the audio format (MIME type) of the audio
        /// file:
        /// * `audio/basic` (Use only with narrowband models.)
        /// * `audio/flac`
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
        /// For information about the supported audio formats, including specifying the sampling rate, channels, and
        /// endianness for the indicated formats, see [Audio
        /// formats](https://console.bluemix.net/docs/services/speech-to-text/audio-formats.html).
        ///
        /// **Note:** The sampling rate of an audio file must match the sampling rate of the base model for the custom
        /// model: for broadband models, at least 16 kHz; for narrowband models, at least 8 kHz. If the sampling rate of
        /// the audio is higher than the minimum required rate, the service down-samples the audio to the appropriate
        /// rate. If the sampling rate of the audio is lower than the minimum required rate, the service labels the
        /// audio file as `invalid`.
        ///
        /// ### Content types for archive-type resources
        ///
        ///  You can add an archive file (**.zip** or **.tar.gz** file) that contains audio files in any format that the
        /// service supports for speech recognition. For an archive-type resource, use the `Content-Type` parameter to
        /// specify the media type of the archive file:
        /// * `application/zip` for a **.zip** file
        /// * `application/gzip` for a **.tar.gz** file.
        ///
        /// All audio files contained in the archive must have the same audio format. Use the `Contained-Content-Type`
        /// parameter to specify the format of the contained audio files. The parameter accepts all of the audio formats
        /// supported for use with speech recognition and with the `Content-Type` header, including the `rate`,
        /// `channels`, and `endianness` parameters that are used with some formats. The default contained audio format
        /// is `audio/wav`.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="audioName">The name of the audio resource for the custom acoustic model. When adding an audio
        /// resource, do not include spaces in the name; use a localized name that matches the language of the custom
        /// model.</param>
        /// <param name="audioResource">The audio resource that is to be added to the custom acoustic model, an
        /// individual audio file or an archive file.</param>
        /// <param name="contentType">The type of the input.</param>
        /// <param name="containedContentType">For an archive-type resource, specifies the format of the audio files
        /// contained in the archive file. The parameter accepts all of the audio formats supported for use with speech
        /// recognition, including the `rate`, `channels`, and `endianness` parameters that are used with some formats.
        /// For a complete list of supported audio formats, see [Audio
        /// formats](/docs/services/speech-to-text/input.html#formats). (optional, default to audio/wav)</param>
        /// <param name="allowOverwrite">If `true`, the specified corpus or audio resource overwrites an existing corpus
        /// or audio resource with the same name. If `false`, the request fails if a corpus or audio resource with the
        /// same name already exists. The parameter has no effect if a corpus or audio resource with the same name does
        /// not already exist. (optional, default to false)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            if (audioResource == null)
                throw new ArgumentNullException(nameof(audioResource));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");

                if (!string.IsNullOrEmpty(contentType))
                    restRequest.WithHeader("Content-Type", contentType);
                if (!string.IsNullOrEmpty(containedContentType))
                    restRequest.WithHeader("Contained-Content-Type", containedContentType);
                if (allowOverwrite != null)
                    restRequest.WithArgument("allow_overwrite", allowOverwrite);
                var audioResourceContent = new ByteArrayContent(audioResource);
                System.Net.Http.Headers.MediaTypeHeaderValue audioResourceType;
                System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out audioResourceType);
                audioResourceContent.Headers.ContentType = audioResourceType;
                restRequest.WithBodyContent(audioResourceContent);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Delete an audio resource.
        ///
        /// Deletes an existing audio resource from a custom acoustic model. Deleting an archive-type audio resource
        /// removes the entire archive of files; the current interface does not allow deletion of individual files from
        /// an archive resource. Removing an audio resource does not affect the custom model until you train the model
        /// on its updated data by using the **Train a custom acoustic model** method. You must use credentials for the
        /// instance of the service that owns a model to delete its audio resources.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="audioName">The name of the audio resource for the custom acoustic model. When adding an audio
        /// resource, do not include spaces in the name; use a localized name that matches the language of the custom
        /// model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteAudio(string customizationId, string audioName, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Get an audio resource.
        ///
        /// Gets information about an audio resource from a custom acoustic model. The method returns an `AudioListing`
        /// object whose fields depend on the type of audio resource that you specify with the method's `audio_name`
        /// parameter:
        /// * **For an audio-type resource,** the object's fields match those of an `AudioResource` object: `duration`,
        /// `name`, `details`, and `status`.
        /// * **For an archive-type resource,** the object includes a `container` field whose fields match those of an
        /// `AudioResource` object. It also includes an `audio` field, which contains an array of `AudioResource`
        /// objects that provides information about the audio files that are contained in the archive.
        ///
        /// The information includes the status of the specified audio resource. The status is important for checking
        /// the service's analysis of a resource that you add to the custom model.
        /// * For an audio-type resource, the `status` field is located in the `AudioListing` object.
        /// * For an archive-type resource, the `status` field is located in the `AudioResource` object that is returned
        /// in the `container` field.
        ///
        /// You must use credentials for the instance of the service that owns a model to list its audio resources.
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="audioName">The name of the audio resource for the custom acoustic model. When adding an audio
        /// resource, do not include spaces in the name; use a localized name that matches the language of the custom
        /// model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="AudioListing" />AudioListing</returns>
        public AudioListing GetAudio(string customizationId, string audioName, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            AudioListing result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<AudioListing>().Result;
                if(result == null)
                    result = new AudioListing();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// </summary>
        /// <param name="customizationId">The customization ID (GUID) of the custom acoustic model. You must make the
        /// request with service credentials created for the instance of the service that owns the custom model.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="AudioResources" />AudioResources</returns>
        public AudioResources ListAudio(string customizationId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            AudioResources result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.GetAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio");

                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<AudioResources>().Result;
                if(result == null)
                    result = new AudioResources();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
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
        /// the data. For more information about customer IDs and about using this method, see [Information
        /// security](https://console.bluemix.net/docs/services/speech-to-text/information-security.html).
        /// </summary>
        /// <param name="customerId">The customer ID for which all data is to be deleted.</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="BaseModel" />BaseModel</returns>
        public BaseModel DeleteUserData(string customerId, Dictionary<string, object> customData = null)
        {
            if (string.IsNullOrEmpty(customerId))
                throw new ArgumentNullException(nameof(customerId));
            BaseModel result = null;

            try
            {
                IClient client;
                if(_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                var restRequest = client.DeleteAsync($"{this.Endpoint}/v1/user_data");

                if (!string.IsNullOrEmpty(customerId))
                    restRequest.WithArgument("customer_id", customerId);
                if (customData != null)
                    restRequest.WithCustomData(customData);
                result = restRequest.As<BaseModel>().Result;
                if(result == null)
                    result = new BaseModel();
                result.CustomData = restRequest.CustomData;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
