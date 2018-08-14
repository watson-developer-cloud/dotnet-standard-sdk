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
    public partial class SpeechToTextService : WatsonService, ISpeechToTextService
    {
        
        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="audio"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="continuous"></param>
        /// <param name="inactivityTimeout"></param>
        /// <param name="keywords"></param>
        /// <param name="keywordsThreshold"></param>
        /// <param name="maxAlternatives"></param>
        /// <param name="wordAlternativesThreshold"></param>
        /// <param name="wordConfidence"></param>
        /// <param name="timestamps"></param>
        /// <param name="profanityFilter"></param>
        /// <param name="smartFormatting"></param>
        /// <param name="speakerLabels"></param>
        /// <returns></returns>
        public SpeechRecognitionResults Recognize(string contentType, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string customizationId = "", bool? continuous = null, int? inactivityTimeout = null, string[] keywords = null, double? keywordsThreshold = null, int? maxAlternatives = null, double? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool profanityFilter = false, bool? smartFormatting = null, bool? speakerLabels = null)
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

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="metaData"></param>
        /// <param name="audio"></param>
        /// <returns></returns>
        public SpeechRecognitionResults Recognize(string contentType, Metadata metaData, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string customizationId = "")
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

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="audio"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="continuous"></param>
        /// <param name="inactivityTimeout"></param>
        /// <param name="keywords"></param>
        /// <param name="keywordsThreshold"></param>
        /// <param name="maxAlternatives"></param>
        /// <param name="wordAlternativesThreshold"></param>
        /// <param name="wordConfidence"></param>
        /// <param name="timestamps"></param>
        /// <param name="profanityFilter"></param>
        /// <param name="smartFormatting"></param>
        /// <param name="speakerLabels"></param>
        /// <returns></returns>
        [Obsolete("RecognizeWithSession is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
        public SpeechRecognitionResults RecognizeWithSession(string sessionId, string contentType, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string customizationId = "", bool? continuous = null, int? inactivityTimeout = null, string[] keywords = null, double? keywordsThreshold = null, int? maxAlternatives = null, double? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool profanityFilter = false, bool? smartFormatting = null, bool? speakerLabels = null)
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

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="metaData"></param>
        /// <param name="audio"></param>
        /// <returns></returns>
        [Obsolete("RecognizeWithSession is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
        public SpeechRecognitionResults RecognizeWithSession(string sessionId, string contentType, Metadata metaData, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string customizationId = "")
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

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. Returns only the final results; to enable interim results, use Sessions or WebSockets. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a sessionless recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the connection (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the connection (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds. 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="contentType"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="audio"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="continuous"></param>
        /// <param name="inactivityTimeout"></param>
        /// <param name="keywords"></param>
        /// <param name="keywordsThreshold"></param>
        /// <param name="maxAlternatives"></param>
        /// <param name="wordAlternativesThreshold"></param>
        /// <param name="wordConfidence"></param>
        /// <param name="timestamps"></param>
        /// <param name="profanityFilter"></param>
        /// <param name="smartFormatting"></param>
        /// <param name="speakerLabels"></param>
        /// <returns></returns>
        private SpeechRecognitionResults Recognize(string sessionId, string contentType, Metadata metaData, Stream audio, string transferEncoding = "", string model = "", string customizationId = "", bool? continuous = null, int? inactivityTimeout = null, string[] keywords = null, double? keywordsThreshold = null, int? maxAlternatives = null, double? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool profanityFilter = false, bool? smartFormatting = null, bool? speakerLabels = null)
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException($"{nameof(contentType)}");

            SpeechRecognitionResults result = null;

            try
            {
                string urlService = string.Empty;
                IRequest restRequest = null;

                IClient client;
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);

                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }

                if (string.IsNullOrEmpty(sessionId))
                {
                    restRequest = client.PostAsync($"{this.Endpoint}/v1/recognize");
                }
                else
                {
                    restRequest = client.PostAsync($"{this.Endpoint}/v1/sessions/{sessionId}")
                                   .WithHeader("Cookie", sessionId);
                }

                if (!string.IsNullOrEmpty(transferEncoding))
                    restRequest.WithHeader("Transfer-Encoding", transferEncoding);

                if (metaData == null)
                {
                    // if a session exists, the model should not be sent
                    if (string.IsNullOrEmpty(sessionId))
                        restRequest.WithArgument("model", model);

                    if (!string.IsNullOrEmpty(customizationId))
                        restRequest.WithArgument("customization_id", customizationId);

                    if (continuous.HasValue)
                        restRequest.WithArgument("continuous", continuous.Value);

                    if (inactivityTimeout.HasValue && inactivityTimeout.Value > 0)
                        restRequest.WithArgument("inactivity_timeout", inactivityTimeout.Value);

                    if (keywords != null && keywords.Length > 0)
                        restRequest.WithArgument("keywords", keywords);

                    if (keywordsThreshold.HasValue && keywordsThreshold.Value > 0)
                        restRequest.WithArgument("keywords_threshold", keywordsThreshold.Value);

                    if (maxAlternatives.HasValue && maxAlternatives.Value > 0)
                        restRequest.WithArgument("max_alternatives", maxAlternatives.Value);

                    if (wordAlternativesThreshold.HasValue && wordAlternativesThreshold.Value > 0)
                        restRequest.WithArgument("word_alternatives_threshold", wordAlternativesThreshold.Value);

                    if (wordConfidence.HasValue)
                        restRequest.WithArgument("word_confidence", wordConfidence.Value);

                    if (timestamps.HasValue)
                        restRequest.WithArgument("timestamps", timestamps.Value);

                    if (profanityFilter)
                        restRequest.WithArgument("profanity_filter", profanityFilter);

                    if (smartFormatting.HasValue)
                        restRequest.WithArgument("smart_formatting", smartFormatting.Value);

                    if (speakerLabels.HasValue)
                        restRequest.WithArgument("speaker_labels", speakerLabels.Value);

                    StreamContent bodyContent = new StreamContent(audio);
                    bodyContent.Headers.Add("Content-Type", contentType);

                    restRequest.WithBodyContent(bodyContent);
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
                        restRequest.WithArgument("model", model);

                    formData.Add(metadata, "metadata");
                    formData.Add(audioContent, "upload");

                    restRequest.WithBodyContent(formData);
                }

                result = restRequest.As<SpeechRecognitionResults>()
                           .Result;

            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        /// <summary>
        /// Requests results for a recognition task within a specified session. You can submit this method multiple times for the same recognition task. To see interim results, set the interim_results parameter to true. The request must pass the cookie that was returned by the SpeechToTextService.CreateSession(string)  method. 
        /// To see results for a specific recognition task, specify a sequence ID(with the sequence_id query parameter) that matches the sequence ID of the recognition request.A request with a sequence ID can arrive before, during, or after the matching recognition request, but it must arrive no later than 30 seconds after the recognition completes to avoid a session timeout(response code 408). Send multiple requests for the sequence ID with a maximum gap of 30 seconds to avoid the timeout.
        /// Omit the sequence ID to observe results for an ongoing recognition task.If no recognition task is ongoing, the method returns results for the next recognition task regardless of whether it specifies a sequence ID. 
        /// </summary>
        /// <param name="sessionId">The identifier of the session whose results you want to observe.</param>
        /// <param name="sequenceId">The sequence ID of the recognition task whose results you want to observe. Omit the parameter to obtain results either for an ongoing recognition, if any, or for the next recognition task regardless of whether it specifies a sequence ID.</param>
        /// <param name="interimResults">Indicates whether the service is to return interim results. If true, interim results are returned as a stream of JSON objects; each object represents a single SpeechRecognitionEvent. If false, the response is a single SpeechRecognitionEvent with final results only.</param>
        /// <returns></returns>
        [Obsolete("ObserveResult is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
        public List<SpeechRecognitionResults> ObserveResult(string sessionId, int? sequenceId = (int?)null, bool interimResults = false)
        {
            List<SpeechRecognitionResults> result = null;

            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("SessionId can not be null or empty");

            try
            {
                IRequest restRequest = null;
                IClient client;
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }

                restRequest = client.GetAsync($"{this.Endpoint}/v1/sessions/{sessionId}/observe_result");

                if (sequenceId.HasValue)
                    restRequest.WithArgument("sequence_id", sequenceId);

                if (interimResults)
                    restRequest.WithArgument("interim_results", interimResults);

                var strResult =
                    restRequest.AsString()
                           .Result;
                var serializer = new JsonSerializer();

                using (var jsonReader = new JsonTextReader(new StringReader(strResult)))
                {
                    jsonReader.SupportMultipleContent = true;
                    result = new List<SpeechRecognitionResults>();

                    while (jsonReader.Read())
                    {
                        var speechRecognitionEvent = serializer.Deserialize<SpeechRecognitionResults>(jsonReader);
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

        /// <summary>
        /// Creates a session and locks recognition requests to that engine. You can use the session for multiple recognition requests so that each request is processed with the same Speech to Text engine. The session expires after 30 seconds of inactivity. For information about avoiding session timeouts, see Timeouts (https://console.bluemix.net/docs/services/speech-to-text/input.html#timeouts). 
        /// 
        /// The method returns a cookie in the Set-Cookie response header.You must pass this cookie with each request that uses the session. For more information, see Using cookies with sessions (https://console.bluemix.net/docs/services/speech-to-text/http.html#cookies).
        /// </summary>
        /// <param name="model">The identifier of the model that is to be used by the new session.</param>
        /// <param name="customizationId">The GUID of a custom language model that is to be used with the new session. The base model of the specified custom language model must match the model specified with the model parameter. You must make the request with service credentials created for the instance of the service that owns the custom model. By default, no custom language model is used.</param>
        /// <param name="acousticCustomizationId">The GUID of a custom acoustic model that is to be used with the new session. The base model of the specified custom acoustic model must match the model specified with the model parameter. You must make the request with service credentials created for the instance of the service that owns the custom model. By default, no custom acoustic model is used.</param>
        /// <param name="baseModelVersion">The version of the specified base model that is to be used with the new session. Multiple versions of a base model can exist when a model is updated for internal improvements. The parameter is intended primarily for use with custom models that have been upgraded for a new base model. The default value depends on whether the parameter is used with or without a custom model. For more information, see Base model version (https://console.bluemix.net/docs/services/speech-to-text/input.html#version).</param>
        /// <returns>The created session</returns>
        [Obsolete("CreateSession is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
        public SpeechSession CreateSession(string model, string customizationId = null, string acousticCustomizationId = null, string baseModelVersion = null)
        {
            SpeechSession result = null;

            try
            {
                if (string.IsNullOrEmpty(model))
                    throw new ArgumentNullException(nameof(model));

                IRequest restRequest = null;
                IClient client;
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }

                restRequest = client.PostAsync($"{this.Endpoint}/v1/sessions");
                restRequest.WithArgument("model", model);
                if (!string.IsNullOrEmpty(customizationId))
                    restRequest.WithArgument("customization_id", customizationId);
                if (!string.IsNullOrEmpty(acousticCustomizationId))
                    restRequest.WithArgument("acoustic_customization_id", acousticCustomizationId);
                if (!string.IsNullOrEmpty(baseModelVersion))
                    restRequest.WithArgument("base_model_version", baseModelVersion);
                restRequest.WithHeader("accept", HttpMediaType.APPLICATION_JSON);
                result = restRequest.As<SpeechSession>().Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        /// <summary>
        /// Checks whether a specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The method blocks until the session is in the initialized state to indicate that you can send another recognition request. The request must pass the cookie that was returned by the Create a session method.
        /// </summary>
        /// <param name="session">The session to get.</param>
        /// <returns></returns>
        [Obsolete("GetSessionStatus is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
        public SessionStatus GetSessionStatus(SpeechSession session)
        {
            return this.GetSessionStatus(session.SessionId);
        }

        /// <summary>
        /// Checks whether a specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The method blocks until the session is in the initialized state to indicate that you can send another recognition request. The request must pass the cookie that was returned by the Create a session method.
        /// </summary>
        /// <param name="sessionId">The ID of the session for the recognition task.</param>
        /// <returns>The session status.</returns>
        [Obsolete("GetSessionStatus is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
        public SessionStatus GetSessionStatus(string sessionId)
        {
            SessionStatus result = null;

            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("session id can not be null or empty");

            try
            {
                IRequest restRequest = null;

                IClient client;
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }

                restRequest = client.GetAsync($"{this.Endpoint}{string.Format("/v1/sessions/{0}/recognize", sessionId)}");
                restRequest.WithHeader("Cookie", sessionId);
                restRequest.WithHeader("accept", HttpMediaType.APPLICATION_JSON);
                result = restRequest.As<SessionStatus>().Result;
                               
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        /// <summary>
        /// Deletes an existing session and its engine. The request must pass the cookie that was returned by the Create a session method. You cannot send requests to a session after it is deleted. By default, a session expires after 30 seconds of inactivity if you do not delete it first.
        /// </summary>
        /// <param name="session">The session to be deleted.</param>
        /// <returns></returns>
        [Obsolete("DeleteSession is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
        public object DeleteSession(SpeechSession session)
        {
            return this.DeleteSession(session.SessionId);
        }

        /// <summary>
        /// Deletes an existing session and its engine. The request must pass the cookie that was returned by the Create a session method. You cannot send requests to a session after it is deleted. By default, a session expires after 30 seconds of inactivity if you do not delete it first.
        /// </summary>
        /// <param name="session">The ID of the session to be deleted.</param>
        /// <returns></returns>
        [Obsolete("DeleteSession is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
        public object DeleteSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("session id can not be null or empty");

            object result = null;

            try
            {
                IRequest restRequest = null;
                IClient client;
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }
                else
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }

                restRequest = client.DeleteAsync(string.Format("{0}{1}/{2}", this.Endpoint, "/v1/sessions", sessionId));

                result = restRequest.AsMessage().Result;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        /// <summary>
        /// Requests results for a recognition task within the specified session. You can submit multiple requests for the same recognition task. To see interim results, set the parameter interim_results=true. The request must pass the cookie that was returned by the Create a session method.
        /// 
        /// To see results for a specific recognition task, specify a sequence ID(with the sequence_id parameter) that matches the sequence ID of the recognition request.A request with a sequence ID can arrive before, during, or after the matching recognition request, but it must arrive no later than 30 seconds after the recognition completes to avoid a session timeout(response code 408). Send multiple requests for the sequence ID with a maximum gap of 30 seconds to avoid the timeout.
        /// 
        /// Omit the sequence ID to observe results for an ongoing recognition task.If no recognition task is ongoing, the method returns results for the next recognition task regardless of whether it specifies a sequence ID.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="contentType"></param>
        /// <param name="metaData"></param>
        /// <param name="audio"></param>
        /// <param name="transferEncoding"></param>
        /// <param name="model"></param>
        /// <param name="customizationId"></param>
        /// <param name="continuous"></param>
        /// <param name="inactivityTimeout"></param>
        /// <param name="keywords"></param>
        /// <param name="keywordsThreshold"></param>
        /// <param name="maxAlternatives"></param>
        /// <param name="wordAlternativesThreshold"></param>
        /// <param name="wordConfidence"></param>
        /// <param name="timestamps"></param>
        /// <param name="profanityFilter"></param>
        /// <param name="smartFormatting"></param>
        /// <param name="speakerLabels"></param>
        /// <returns></returns>
        SpeechRecognitionResults ISpeechToTextService.Recognize(string sessionId, string contentType, Metadata metaData, Stream audio, string transferEncoding, string model, string customizationId, bool? continuous, int? inactivityTimeout, string[] keywords, double? keywordsThreshold, int? maxAlternatives, double? wordAlternativesThreshold, bool? wordConfidence, bool? timestamps, bool profanityFilter, bool? smartFormatting, bool? speakerLabels)
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
    }

    public class Metadata
    {
        /// <summary>
        /// The MIME type of the data in the following parts. All data parts must have the same MIME type. = ['audio/flac', 'audio/l16', 'audio/wav', 'audio/mulaw', 'audio/basic', 'audio/ogg;codecs=opus']
        /// </summary>
        [JsonProperty("part_content_type")]
        public string PartContentType { get; set; }

        /// <summary>
        /// The number of audio data parts (audio files) sent with the request. Server-side end-of-stream detection is applied to the last (and possibly the only) data part. If omitted, the number of parts is determined from the request itself. 
        /// </summary>
        [JsonProperty("data_parts_count", NullValueHandling = NullValueHandling.Ignore)]
        public int? DataPartsCount { get; set; }

        /// <summary>
        /// The sequence ID for all data parts of this recognition task in the form of a user-specified integer. If omitted, no sequence ID is associated with the recognition task. Used only for session-based requests. 
        /// </summary>
        [JsonProperty("sequence_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SequenceId { get; set; }

        /// <summary>
        /// If true, multiple final results that represent consecutive phrases separated by pauses are returned. If false (the default), recognition ends after the first "end of speech" incident is detected. 
        /// </summary>
        [JsonProperty("continuous", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Continuous { get; set; }

        /// <summary>
        /// The time in seconds after which, if only silence (no speech) is detected in submitted audio, the connection is closed with a 400 error and, for session-based methods, with session_closed set to true. Useful for stopping audio submission from a live microphone when a user simply walks away. Use -1 for infinity. See also the continuous parameter.
        /// </summary>
        [JsonProperty("inactivity_timeout", NullValueHandling = NullValueHandling.Ignore)]
        public double? InactivityTimeout { get; set; }

        /// <summary>
        /// Array of keyword strings to spot in the audio. Each keyword string can include one or more tokens. Keywords are spotted only in the final hypothesis, not in interim results. Omit the parameter or specify an empty array if you do not need to spot keywords. 
        /// </summary>
        [JsonProperty("keywords", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Keywords { get; set; }

        /// <summary>
        /// Confidence value that is the lower bound for spotting a keyword. A word is considered to match a keyword if its confidence is greater than or equal to the threshold. Specify a probability between 0 and 1 inclusive. No keyword spotting is performed if you omit the parameter. If you specify a threshold, you must also specify one or more keywords. 
        /// </summary>
        [JsonProperty("keywords_threshold", NullValueHandling = NullValueHandling.Ignore)]
        public double? KeywordsThreshold { get; set; }

        /// <summary>
        /// Maximum number of alternative transcripts to be returned. By default, a single transcription is returned.
        /// </summary>
        [JsonProperty("max_alternatives", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxAlternatives { get; set; }

        /// <summary>
        /// Confidence value that is the lower bound for identifying a hypothesis as a possible word alternative (also known as "Confusion Networks"). An alternative word is considered if its confidence is greater than or equal to the threshold. Specify a probability between 0 and 1 inclusive. No alternative words are computed if you omit the parameter. 
        /// </summary>
        [JsonProperty("word_alternatives_threshold", NullValueHandling = NullValueHandling.Ignore)]
        public double? WordAlternativesThreshold { get; set; }

        /// <summary>
        /// If true, a confidence measure in the range 0 to 1 is returned for each word. 
        /// </summary>
        [JsonProperty("word_confidence", NullValueHandling = NullValueHandling.Ignore)]
        public bool? WordConfidence { get; set; }

        /// <summary>
        /// If true, time alignment for each word is returned.
        /// </summary>
        [JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Timestamps { get; set; }

        /// <summary>
        /// If true (the default), filters profanity from all output except for keyword results by replacing inappropriate words with a series of asterisks. Set the parameter to false to return results with no censoring. Applies to US English transcription only.
        /// </summary>
        [JsonProperty("profanity_filter", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ProfanityFilter { get; set; }

        /// <summary>
        /// If true, converts dates, times, series of digits and numbers, phone numbers, currency values, and Internet addresses into more readable, conventional representations in the final transcript of a recognition request. If false (the default), no formatting is performed. Applies to US English transcription only.
        /// </summary>
        [JsonProperty("smart_formatting", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SmartFormatting { get; set; }

        /// <summary>
        /// Indicates whether labels that identify which words were spoken by which participants in a multi-person exchange are to be included in the response. If true, speaker labels are returned; if false (the default), they are not. Speaker labels can be returned only for the following language models: en-US_NarrowbandModel, es-ES_NarrowbandModel, and ja-JP_NarrowbandModel. Setting speaker_labels to true forces the continuous and timestamps parameters to be true, as well, regardless of whether the user specifies false for the parameters.
        /// </summary>
        [JsonProperty("speaker_labels", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SpeakerLabels { get; set; }
    }

    [Obsolete("SpeechSession is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
    public class SpeechSession
    {
        /// <summary>
        /// URI for HTTP REST recognition requests.
        /// </summary>
        [JsonProperty("recognize", NullValueHandling = NullValueHandling.Ignore)]
        public string Recognize { get; set; }

        /// <summary>
        /// URI for WebSocket recognition requests.Needed only for working with the WebSocket interface.
        /// </summary>
        [JsonProperty("recognizeWS", NullValueHandling = NullValueHandling.Ignore)]
        public string RecognizeWS { get; set; }

        /// <summary>
        /// Identifier for the new session. Note: This field is returned only when you create a new session. 
        /// </summary>
        [JsonProperty("session_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionId { get; set; }

        /// <summary>
        /// URI for HTTP REST results observers.
        /// </summary>
        [JsonProperty("observe_result", NullValueHandling = NullValueHandling.Ignore)]
        public string ObserveResult { get; set; }

        /// <summary>
        /// URI for the new session.Note: This field is returned only when you create a new session.
        /// </summary>
        [JsonProperty("new_session_uri", NullValueHandling = NullValueHandling.Ignore)]
        public string NewSessionUrl { get; set; }

        /// <summary>
        /// State of the session.The state must be initialized for the session to accept another recognition request.Other internal states are possible, but they have no meaning for the user.Note: This field is returned only when you request the status of an existing session.
        /// </summary>
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        /// <summary>
        /// URI for information about the model that is used with the session. Note: This field is returned only when you request the status of an existing session.
        /// </summary>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    }

    [Obsolete("SessionStatus is deprecated as of August 8, 2018, and will be removed from service on September 7, 2018.")]
    public class SessionStatus
    {
        /// <summary>
        /// Information about the specified existing session.
        /// </summary>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public SpeechSession Session { get; set; }
    }
}