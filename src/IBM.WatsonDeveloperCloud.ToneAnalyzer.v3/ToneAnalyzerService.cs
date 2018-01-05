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

using System.Collections.Generic;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3
{
    public class ToneAnalyzerService : WatsonService, IToneAnalyzerService
    {
        const string SERVICE_NAME = "tone_analyzer";
        const string URL = "https://gateway.watsonplatform.net/tone-analyzer/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public ToneAnalyzerService() : base(SERVICE_NAME, URL)
        {
            if(!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public ToneAnalyzerService(string userName, string password, string versionDate) : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if(string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }

        public ToneAnalyzerService(IClient httpClient) : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public ToneAnalysis Tone(ToneInput toneInput, string contentType, bool? sentences = null, List<string> tones = null, string contentLanguage = null, string acceptLanguage = null)
        {
            if (toneInput == null)
                throw new ArgumentNullException("'toneInput' is required for 'Tone()'");
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException("'contentType' is required for 'Tone()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ToneAnalysis result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v3/tone")
                                .WithArgument("version", VersionDate)
                                .WithHeader("Content-Type", contentType)
                                .WithHeader("Content-Language", contentLanguage)
                                .WithHeader("Accept-Language", acceptLanguage)
                                .WithArgument("sentences", sentences)
                                .WithArgument("tones", tones != null && tones.Count > 0 ? string.Join(",", tones.ToArray()) : null)
                                .WithBody<ToneInput>(toneInput)
                                .As<ToneAnalysis>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        public UtteranceAnalyses ToneChat(ToneChatInput utterances, string acceptLanguage = null)
        {
            if (utterances == null)
                throw new ArgumentNullException("'utterances' is required for 'ToneChat()'");

            if(string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            UtteranceAnalyses result = null;

            try
            {
                result = this.Client.WithAuthentication(this.UserName, this.Password)
                                .PostAsync($"{this.Endpoint}/v3/tone_chat")
                                .WithArgument("version", VersionDate)
                                .WithHeader("Accept-Language", acceptLanguage)
                                .WithBody<ToneChatInput>(utterances)
                                .As<UtteranceAnalyses>()
                                .Result;
            }
            catch(AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
