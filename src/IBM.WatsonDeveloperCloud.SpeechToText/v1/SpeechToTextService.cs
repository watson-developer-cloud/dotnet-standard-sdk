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
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;
using IBM.WatsonDeveloperCloud.Http.Extensions;
using System.Net.Http.Headers;
using System.Collections.Generic;

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

        public SpeechModelSet GetModels()
        {
            SpeechModelSet result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                            .GetAsync($"{RELATIVE_PATH}{PATH_MODELS}")
                            .As<SpeechModelSet>()
                            .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public SpeechModel GetModel(string modelName)
        {
            if (string.IsNullOrEmpty(modelName))
                throw new ArgumentNullException("modelName can not be null or empty");

            SpeechModel result = null;

            try
            {
                result =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                        .GetAsync($"{RELATIVE_PATH}{PATH_MODELS}/{modelName}")
                        .As<SpeechModel>()
                        .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
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
                               .PostAsync($"{RELATIVE_PATH}{PATH_CREATE_SESSION}")
                               .WithArgument("model", modelName)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<Session>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
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
                               .GetAsync($"{RELATIVE_PATH}{string.Format(PATH_SESSION_RECOGNIZE, sessionId)}")
                               .WithHeader("Cookie", sessionId)
                               .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                               .As<RecognizeStatus>()
                               .Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
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
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public SpeechRecognitionEvent Recognize(RecognizeOptions options)
        {
            SpeechRecognitionEvent result = null;

            if (options == null)
                throw new ArgumentNullException("The options is null or does not exist");

            try
            {
                string path = string.Empty;

                if (options != null && !string.IsNullOrEmpty(options.SessionId))
                    path = string.Format(PATH_SESSION_RECOGNIZE, options.SessionId);

                IRequest request =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync(RELATIVE_PATH + PATH_RECOGNIZE);

                if(options.BodyContent != null)
                {
                    request.WithArgument("model", options.Model);

                    if (!string.IsNullOrEmpty(options.CustomizationId))
                        request.WithArgument("customization_id", options.CustomizationId);

                    if (options.Continuous)
                        request.WithArgument("continuous", options.Continuous);

                    if (options.InactivityTimeout > 0)
                        request.WithArgument("inactivity_timeout", options.InactivityTimeout);

                    if (options.Keywords != null && options.Keywords.Length > 0)
                        request.WithArgument("keywords", options.Keywords);

                    if (options.KeywordsThreshold > 0)
                        request.WithArgument("keywords_threshold", options.KeywordsThreshold);

                    if (options.MaxAlternatives > 0)
                        request.WithArgument("max_alternatives", options.MaxAlternatives);

                    if (options.WordAlternativesThreshold > 0)
                        request.WithArgument("word_alternatives_threshold", options.WordAlternativesThreshold);

                    if (options.WordConfidence)
                        request.WithArgument("word_confidence", options.WordConfidence);

                    if (options.Timestamps)
                        request.WithArgument("timestamps", options.Timestamps);

                    if (options.ProfanityFilter)
                        request.WithArgument("profanity_filter", options.ProfanityFilter);

                    if (options.SmartFormatting)
                        request.WithArgument("smart_formatting", options.SmartFormatting);

                    if (options.SpeakerLabels)
                        request.WithArgument("speaker_labels", options.SpeakerLabels);

                    request.WithBodyContent(options.BodyContent);
                }
                else
                {
                    request.WithBodyContent(options.FormData);
                }

                result =
                    request.As<SpeechRecognitionEvent>()
                           .Result;

            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}