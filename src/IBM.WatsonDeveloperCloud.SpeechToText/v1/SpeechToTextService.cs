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

using System;
using System.IO;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Filters;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Models;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1
{
    public class SpeechToTextService : WatsonService, ISpeechToTextService
    {
        const string SERVICE_NAME = "speech_to_text";

        const string RELATIVE_PATH = "/speech-to-text/api";

        const string PATH_MODELS = "/v1/models";
        const string PATH_CREATE_SESSION = "/v1/sessions";
        const string PATH_DELETE_SESSION = "/v1/sessions";
        const string PATH_RECOGNIZE = "/v1/recognize";
        const string PATH_SESSION_RECOGNIZE = "/v1/sessions/{0}/recognize";
        const string PATH_OBSERVE_RESULT = "/v1/sessions/{0}/observe_result";

        const string URL = "https://stream.watsonplatform.net/speech-to-text/api";

        public SpeechToTextService()
            : base(SERVICE_NAME, URL) { }

        public SpeechToTextService(IClient client)
            : base(SERVICE_NAME, URL, client) { }

        public SpeechToTextService(string userName, string password)
            : this()
        {
            this.SetCredential(userName, password);
        }

        public ModelSet GetModels()
        {
            ModelSet result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                            .GetAsync(RELATIVE_PATH + PATH_MODELS)
                            .As<ModelSet>()
                            .Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.Flatten().InnerException).Throw();
            }

            return result;
        }

        public Model GetModel(string modelName)
        {
            if (string.IsNullOrEmpty(modelName))
                throw new ArgumentNullException("modelName can not be null or empty");

            Model result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                        .GetAsync(RELATIVE_PATH + PATH_MODELS + "/" + modelName)
                        .As<Model>()
                        .Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.Flatten().InnerException).Throw();
            }

            return result;
        }

        public Session CreateSession(string modelName)
        {
            Session result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync(RELATIVE_PATH + PATH_CREATE_SESSION)
                               .WithArgument("model", modelName)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<Session>()
                               .Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.Flatten().InnerException).Throw();
            }

            return result;
        }

        public RecognizeStatus GetSessionStatus(Session session)
        {
            return this.GetSessionStatus(session.SessionId);
        }

        public RecognizeStatus GetSessionStatus(string sessionId)
        {
            RecognizeStatus result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync(RELATIVE_PATH + string.Format(PATH_SESSION_RECOGNIZE, sessionId))
                               .WithHeader("Cookie", sessionId)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<RecognizeStatus>()
                               .Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.Flatten().InnerException).Throw();
            }

            return result;
        }

        public void DeleteSession(Session session)
        {
            this.DeleteSession(session.SessionId);
        }

        public void DeleteSession(string sessionId)
        {
            try
            {
                var response =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .DeleteAsync(string.Format("{0}{1}/{2}", RELATIVE_PATH, PATH_DELETE_SESSION, sessionId))
                               .AsMessage()
                               .Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.Flatten().InnerException).Throw();
            }
        }

        public SpeechRecognitionEvent Recognize(FileStream audio)
        {
            return this.Recognize(audio, new RecognizeOptions());
        }

        public SpeechRecognitionEvent Recognize(FileStream audio, RecognizeOptions options)
        {
            if (audio == null)
                throw new ArgumentNullException("The audio file is null or does not exist");

            if (options == null)
                throw new ArgumentNullException("The options is null or does not exist");

            SpeechRecognitionEvent result = null;

            try
            {
                double fileSize = audio.Length / Math.Pow(1024, 2);

                if (fileSize > 100.0)
                    throw new Exception("The audio file is greater than 100MB.");

                string contentType = audio.GetMediaTypeFromFile();

                if (string.IsNullOrEmpty(contentType))
                    throw new Exception("The audio format cannot be recognized");

                StreamContent content = new StreamContent(audio);
                content.Headers.Add("Content-Type", contentType);

                string path = string.Empty;

                if (options != null && !string.IsNullOrEmpty(options.SessionId))
                    path = string.Format(PATH_SESSION_RECOGNIZE, options.SessionId);

                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync(RELATIVE_PATH + PATH_RECOGNIZE)
                               .WithArguments(options.GetArguments())
                               .WithBodyContent(content)
                               .As<SpeechRecognitionEvent>()
                               .Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.Flatten().InnerException).Throw();
            }

            return result;
        }
    }
}