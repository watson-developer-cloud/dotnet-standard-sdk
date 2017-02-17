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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public interface ISpeechToTextService
    {
        /// <summary>
        /// Retrieves a list of all models available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz.
        /// </summary>
        /// <returns>Returns a ModelSet object that contains the information about an available model that is provided in a JSON Model object.</returns>
        SpeechModelSet GetModels();

        /// <summary>
        /// Retrieves information about a single specified model that is available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz. 
        /// </summary>
        /// <param name="modelName">The identifier of the desired model</param>
        /// <returns>Returns a Model object that contains the information about the specified model that is provided in a JSON Model object</returns>
        SpeechModel GetModel(string modelName);

        /// <summary>
        /// Creates a session and locks recognition requests to that engine.You can use the session for multiple recognition requests so that each request is processed with the same engine.
        /// The session expires after 30 seconds of inactivity.Use getRecognizeStatus to prevent the session from expiring.
        /// </summary>
        /// <param name="modelName">The identifier of the model to be used by the new session</param>
        /// <returns>Returns a <see cref="Session" /> object that contains the information about the new session that is provided in a JSON <see cref="Session" /> object.</returns>
        Session CreateSession(string modelName);

        /// <summary>
        /// Provides a way to check whether the specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The returned state must be initialized to indicate that you can send another recognition request.
        /// </summary>
        /// <param name="session">A <see cref="Session" /> object that identifies the session whose status is to be checked.</param>
        /// <returns>Returns <see cref="RecognizeStatus" /> object that contains the information about the session that is provided in a JSON <see cref="SessionStatus" /> object.</returns>
        RecognizeStatus GetSessionStatus(Session session);

        /// <summary>
        /// Provides a way to check whether the specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The returned state must be initialized to indicate that you can send another recognition request.
        /// </summary>
        /// <param name="sessionId">The identifier for the session.</param>
        /// <returns>Returns <see cref="RecognizeStatus" /> object that contains the information about the session that is provided in a JSON <see cref="SessionStatus" /> object.</returns>
        RecognizeStatus GetSessionStatus(string sessionId);

        /// <summary>
        /// Deletes an existing session and its engine. You cannot send requests to a session after it is deleted.
        /// </summary>
        /// <param name="session">A <see cref="Session" /> object that identifies the session to be deleted</param>
        void DeleteSession(Session session);

        /// <summary>
        /// Deletes an existing session and its engine. You cannot send requests to a session after it is deleted.
        /// </summary>
        /// <param name="sessionId">A Session Id that identifies the session to be deleted</param>
        void DeleteSession(string sessionId);

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. By default, returns only the final results; to enable interim results, set the interimResults parameter to true. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// </summary>
        /// <param name="options">The audio to be transcribed in the format specified by the contentType parameter.</param>
        /// <returns>Returns SpeechRecognitionEvent object that contains the results that are provided in a JSON SpeechRecognitionEvent object. The response includes one or more instances of the object depending on the value of the interimResults parameter.</returns>
        SpeechRecognitionEvent Recognize(RecognizeOptions options);

        /// <summary>
        /// Sends audio and returns transcription results for a session-based recognition request. By default, returns only the final transcription results for the request. To see interim results, set the parameter interim_results to true in a call to the <see cref="ObserveResult(ObserveResultOptions)"/>  result method. 
        /// The service imposes a data size limit of 100 MB per session. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding. The request must pass the cookie that was returned by the <see cref="CreateSession(string)" /> method.
        /// You specify the parameters of the request as a path parameter, request headers, and query parameters. You provide the audio as the body of the request. This method is preferred to the multipart approach for submitting a session-based recognition request.
        /// For requests to transcribe live audio as it becomes available, you must set the Transfer-Encoding header to chunked to use streaming mode. In streaming mode, the server closes the session (response code 408) if the service receives no data chunk for 30 seconds and the service has no audio to transcribe for 30 seconds. The server also closes the session (response code 400) if no speech is detected for inactivity_timeout seconds of audio (not processing time); use the inactivity_timeout parameter to change the default of 30 seconds.
        /// To enable polling by the observe_result method for large audio requests, specify an integer with the sequence_id query parameter.
        /// </summary>
        /// <param name="sessionId">The identifier of the session to be used.</param>
        /// <param name="options">The audio to be transcribed in the format specified by the contentType parameter.</param>
        /// <returns>Returns SpeechRecognitionEvent object that contains the results that are provided in a JSON SpeechRecognitionEvent object. The response includes one or more instances of the object depending on the value of the interimResults parameter.</returns>
        SpeechRecognitionEvent Recognize(string sessionId, RecognizeOptions options);

        /// <summary>
        /// Requests results for a recognition task within a specified session. You can submit this method multiple times for the same recognition task. To see interim results, set the interim_results parameter to true. The request must pass the cookie that was returned by the <see cref="ISpeechToTextService.CreateSession(string)" /> method.
        /// To see results for a specific recognition task, specify a sequence ID (with the sequence_id query parameter) that matches the sequence ID of the recognition request. A request with a sequence ID can arrive before, during, or after the matching recognition request, but it must arrive no later than 30 seconds after the recognition completes to avoid a session timeout (response code 408). Send multiple requests for the sequence ID with a maximum gap of 30 seconds to avoid the timeout.
        /// Omit the sequence ID to observe results for an ongoing recognition task. If no recognition task is ongoing, the method returns results for the next recognition task regardless of whether it specifies a sequence ID.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Returns one or more instances of a <see cref="SpeechRecognitionEvent" /> object depending on the input and the value of the interim_results paramete</returns>
        List<SpeechRecognitionEvent> ObserveResult(ObserveResultOptions options);

        /// <summary>
        /// Creates a new custom language model for a specified base language model. The custom language model can be used only with the base language model for which it is created. The new model is owned by the individual whose service credentials are used to create it.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        CustomizationID CreateCustomModel(CustomModelOptions options);
    }
}