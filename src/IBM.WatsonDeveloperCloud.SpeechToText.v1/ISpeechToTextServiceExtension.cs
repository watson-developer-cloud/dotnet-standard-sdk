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

using System.IO;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using System.Collections.Generic;
using System.Net.Http;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public partial interface ISpeechToTextService
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
        SpeechRecognitionResults Recognize(string contentType,
                                         Stream audio,
                                         string transferEncoding = "",
                                         string model = "en-US_BroadbandModel",
                                         string customizationId = "",
                                         bool? continuous = null,
                                         int? inactivityTimeout = null,
                                         string[] keywords = null,
                                         double? keywordsThreshold = null,
                                         int? maxAlternatives = null,
                                         double? wordAlternativesThreshold = null,
                                         bool? wordConfidence = null,
                                         bool? timestamps = null,
                                         bool profanityFilter = false,
                                         bool? smartFormatting = null,
                                         bool? speakerLabels = null);

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
        SpeechRecognitionResults Recognize(string contentType,
                                         Metadata metaData,
                                         Stream audio,
                                         string transferEncoding = "",
                                         string model = "en-US_BroadbandModel",
                                         string customizationId = "");

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
        SpeechRecognitionResults RecognizeWithSession(string sessionId,
                                                    string contentType,
                                                    Stream audio,
                                                    string transferEncoding = "",
                                                    string model = "en-US_BroadbandModel",
                                                    string customizationId = "",
                                                    bool? continuous = null,
                                                    int? inactivityTimeout = null,
                                                    string[] keywords = null,
                                                    double? keywordsThreshold = null,
                                                    int? maxAlternatives = null,
                                                    double? wordAlternativesThreshold = null,
                                                    bool? wordConfidence = null,
                                                    bool? timestamps = null,
                                                    bool profanityFilter = false,
                                                    bool? smartFormatting = null,
                                                    bool? speakerLabels = null);

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
        SpeechRecognitionResults RecognizeWithSession(string sessionId,
                                                    string contentType,
                                                    Metadata metaData,
                                                    Stream audio,
                                                    string transferEncoding,
                                                    string model,
                                                    string customizationId);

        /// <summary>
        /// Requests results for a recognition task within a specified session. You can submit this method multiple times for the same recognition task. To see interim results, set the interim_results parameter to true. The request must pass the cookie that was returned by the SpeechToTextService.CreateSession(string)  method. 
        /// To see results for a specific recognition task, specify a sequence ID(with the sequence_id query parameter) that matches the sequence ID of the recognition request.A request with a sequence ID can arrive before, during, or after the matching recognition request, but it must arrive no later than 30 seconds after the recognition completes to avoid a session timeout(response code 408). Send multiple requests for the sequence ID with a maximum gap of 30 seconds to avoid the timeout.
        /// Omit the sequence ID to observe results for an ongoing recognition task.If no recognition task is ongoing, the method returns results for the next recognition task regardless of whether it specifies a sequence ID. 
        /// </summary>
        /// <param name="sessionId">The identifier of the session whose results you want to observe.</param>
        /// <param name="sequenceId">The sequence ID of the recognition task whose results you want to observe. Omit the parameter to obtain results either for an ongoing recognition, if any, or for the next recognition task regardless of whether it specifies a sequence ID.</param>
        /// <param name="interimResults">Indicates whether the service is to return interim results. If true, interim results are returned as a stream of JSON objects; each object represents a single SpeechRecognitionEvent. If false, the response is a single SpeechRecognitionEvent with final results only.</param>
        /// <returns></returns>
        List<SpeechRecognitionResults> ObserveResult(string sessionId, int? sequenceId = (int?)null, bool interimResults = false);
    }
}