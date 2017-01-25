/**
* Copyright 2015 IBM Corp. All Rights Reserved.
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
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Models;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public interface ISpeechToTextService
    {
        /// <summary>
        /// Retrieves a list of all models available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz.
        /// </summary>
        /// <returns>Returns a ModelSet object that contains the information about an available model that is provided in a JSON Model object.</returns>
        ModelSet GetModels();

        /// <summary>
        /// Retrieves information about a single specified model that is available for use with the service. The information includes the name of the model and its minimum sampling rate in Hertz. 
        /// </summary>
        /// <param name="modelName">The identifier of the desired model</param>
        /// <returns>Returns a Model object that contains the information about the specified model that is provided in a JSON Model object</returns>
        Model GetModel(string modelName);

        /// <summary>
        /// Creates a session and locks recognition requests to that engine.You can use the session for multiple recognition requests so that each request is processed with the same engine.
        /// The session expires after 30 seconds of inactivity.Use getRecognizeStatus to prevent the session from expiring.
        /// </summary>
        /// <param name="modelName">The identifier of the model to be used by the new session</param>
        /// <returns>Returns a Session object that contains the information about the new session that is provided in a JSON Session object.</returns>
        Session CreateSession(string modelName);

        /// <summary>
        /// Provides a way to check whether the specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The returned state must be initialized to indicate that you can send another recognition request.
        /// </summary>
        /// <param name="session">A Session object that identifies the session whose status is to be checked.</param>
        /// <returns>Returns RecognizeStatus object that contains the information about the session that is provided in a JSON SessionStatus object.</returns>
        RecognizeStatus GetSessionStatus(Session session);

        /// <summary>
        /// Provides a way to check whether the specified session can accept another recognition request. Concurrent recognition tasks during the same session are not allowed. The returned state must be initialized to indicate that you can send another recognition request.
        /// </summary>
        /// <param name="sessionId">The identifier for the session.</param>
        /// <returns>Returns RecognizeStatus object that contains the information about the session that is provided in a JSON SessionStatus object.</returns>
        RecognizeStatus GetSessionStatus(string sessionId);

        /// <summary>
        /// Deletes an existing session and its engine. You cannot send requests to a session after it is deleted.
        /// </summary>
        /// <param name="session">A Session object that identifies the session to be deleted</param>
        void DeleteSession(Session session);

        /// <summary>
        /// Deletes an existing session and its engine. You cannot send requests to a session after it is deleted.
        /// </summary>
        /// <param name="session">A Session Id that identifies the session to be deleted</param>
        void DeleteSession(string sessionId);

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. By default, returns only the final results; to enable interim results, set the interimResults parameter to true. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// </summary>
        /// <param name="audio">The audio to be transcribed in the format specified by the contentType parameter.</param>
        /// <returns>Returns SpeechRecognitionEvent object that contains the results that are provided in a JSON SpeechRecognitionEvent object. The response includes one or more instances of the object depending on the value of the interimResults parameter.</returns>
        SpeechRecognitionEvent Recognize(FileStream audio);

        /// <summary>
        /// Sends audio and returns transcription results for a sessionless recognition request. By default, returns only the final results; to enable interim results, set the interimResults parameter to true. The service imposes a data size limit of 100 MB. It automatically detects the endianness of the incoming audio and, for audio that includes multiple channels, downmixes the audio to one-channel mono during transcoding.
        /// </summary>
        /// <param name="audio">The audio to be transcribed in the format specified by the contentType parameter.</param>
        /// <returns>Returns SpeechRecognitionEvent object that contains the results that are provided in a JSON SpeechRecognitionEvent object. The response includes one or more instances of the object depending on the value of the interimResults parameter.</returns>
        SpeechRecognitionEvent Recognize(FileStream audio, RecognizeOptions options);

        //JobStatusNew CreateJob();
    }
}