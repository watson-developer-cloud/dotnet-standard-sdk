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
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;
using IBM.WatsonDeveloperCloud.Util;
using System;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3
{
    public partial class ToneAnalyzerService : WatsonService, IToneAnalyzerService
    {
        const string SERVICE_NAME = "tone_analyzer";
        const string URL = "https://gateway.watsonplatform.net/tone-analyzer/api";
        private string _versionDate;
        public string VersionDate
        {
            get { return _versionDate; }
            set { _versionDate = value; }
        }

        public ToneAnalyzerService() : base(SERVICE_NAME) { }
        
        public ToneAnalyzerService(string userName, string password, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
            if (string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;
        }
        
        public ToneAnalyzerService(TokenOptions options, string versionDate) : base(SERVICE_NAME, URL)
        {
            if (string.IsNullOrEmpty(options.IamApiKey) && string.IsNullOrEmpty(options.IamAccessToken))
                throw new ArgumentNullException(nameof(options.IamAccessToken) + ", " + nameof(options.IamApiKey));
            if (string.IsNullOrEmpty(versionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            VersionDate = versionDate;

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

        public ToneAnalyzerService(IClient httpClient) : base(SERVICE_NAME, URL)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        /// <summary>
        /// Analyze general tone.
        ///
        /// Use the general purpose endpoint to analyze the tone of your input content. The service analyzes the content
        /// for emotional and language tones. The method always analyzes the tone of the full document; by default, it
        /// also analyzes the tone of each individual sentence of the content.
        ///
        /// You can submit no more than 128 KB of total input content and no more than 1000 individual sentences in
        /// JSON, plain text, or HTML format. The service analyzes the first 1000 sentences for document-level analysis
        /// and only the first 100 sentences for sentence-level analysis.
        ///
        /// Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8; per
        /// the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the ASCII
        /// character set). When specifying a content type of plain text or HTML, include the `charset` parameter to
        /// indicate the character encoding of the input text; for example: `Content-Type: text/plain;charset=utf-8`.
        /// For `text/html`, the service removes HTML tags and analyzes only the textual content.
        ///
        /// **See also:** [Using the general-purpose
        /// endpoint](https://cloud.ibm.com/docs/services/tone-analyzer/using-tone.html#using-the-general-purpose-endpoint).
        /// </summary>
        /// <param name="toneInput">JSON, plain text, or HTML input that contains the content to be analyzed. For JSON
        /// input, provide an object of type `ToneInput`.</param>
        /// <param name="contentType">The type of the input. A character encoding can be specified by including a
        /// `charset` parameter. For example, 'text/plain;charset=utf-8'. (optional)</param>
        /// <param name="sentences">Indicates whether the service is to return an analysis of each individual sentence
        /// in addition to its analysis of the full document. If `true` (the default), the service returns results for
        /// each sentence. (optional, default to true)</param>
        /// <param name="tones">**`2017-09-21`:** Deprecated. The service continues to accept the parameter for
        /// backward-compatibility, but the parameter no longer affects the response.
        ///
        /// **`2016-05-19`:** A comma-separated list of tones for which the service is to return its analysis of the
        /// input; the indicated tones apply both to the full document and to individual sentences of the document. You
        /// can specify one or more of the valid values. Omit the parameter to request results for all three tones.
        /// (optional)</param>
        /// <param name="contentLanguage">The language of the input text for the request: English or French. Regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. The input
        /// content must match the specified language. Do not submit content that contains both languages. You can use
        /// different languages for **Content-Language** and **Accept-Language**.
        /// * **`2017-09-21`:** Accepts `en` or `fr`.
        /// * **`2016-05-19`:** Accepts only `en`. (optional, default to en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can use
        /// different languages for **Content-Language** and **Accept-Language**. (optional, default to en)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="ToneAnalysis" />ToneAnalysis</returns>
        public ToneAnalysis Tone(ToneInput toneInput, string contentType = null, bool? sentences = null, List<string> tones = null, string contentLanguage = null, string acceptLanguage = null, Dictionary<string, object> customData = null)
        {
            if (toneInput == null)
                throw new ArgumentNullException(nameof(toneInput));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            ToneAnalysis result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/tone");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(contentType))
                    restRequest.WithHeader("Content-Type", contentType);
                if (!string.IsNullOrEmpty(contentLanguage))
                    restRequest.WithHeader("Content-Language", contentLanguage);
                if (!string.IsNullOrEmpty(acceptLanguage))
                    restRequest.WithHeader("Accept-Language", acceptLanguage);
                if (sentences != null)
                    restRequest.WithArgument("sentences", sentences);
                if (tones != null && tones.Count > 0)
                    restRequest.WithArgument("tones", string.Join(",", tones.ToArray()));
                restRequest.WithBody<ToneInput>(toneInput);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                restRequest.WithHeader("X-IBMCloud-SDK-Analytics", "service_name=tone_analyzer;service_version=v3;operation_id=Tone");
                result = restRequest.As<ToneAnalysis>().Result;
                if (result == null)
                    result = new ToneAnalysis();
                result.CustomData = restRequest.CustomData;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }

        /// <summary>
        /// Analyze customer engagement tone.
        ///
        /// Use the customer engagement endpoint to analyze the tone of customer service and customer support
        /// conversations. For each utterance of a conversation, the method reports the most prevalent subset of the
        /// following seven tones: sad, frustrated, satisfied, excited, polite, impolite, and sympathetic.
        ///
        /// If you submit more than 50 utterances, the service returns a warning for the overall content and analyzes
        /// only the first 50 utterances. If you submit a single utterance that contains more than 500 characters, the
        /// service returns an error for that utterance and does not analyze the utterance. The request fails if all
        /// utterances have more than 500 characters. Per the JSON specification, the default character encoding for
        /// JSON content is effectively always UTF-8.
        ///
        /// **See also:** [Using the customer-engagement
        /// endpoint](https://cloud.ibm.com/docs/services/tone-analyzer/using-tone-chat.html#using-the-customer-engagement-endpoint).
        /// </summary>
        /// <param name="utterances">An object that contains the content to be analyzed.</param>
        /// <param name="contentLanguage">The language of the input text for the request: English or French. Regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. The input
        /// content must match the specified language. Do not submit content that contains both languages. You can use
        /// different languages for **Content-Language** and **Accept-Language**.
        /// * **`2017-09-21`:** Accepts `en` or `fr`.
        /// * **`2016-05-19`:** Accepts only `en`. (optional, default to en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can use
        /// different languages for **Content-Language** and **Accept-Language**. (optional, default to en)</param>
        /// <param name="customData">Custom data object to pass data including custom request headers.</param>
        /// <returns><see cref="UtteranceAnalyses" />UtteranceAnalyses</returns>
        public UtteranceAnalyses ToneChat(ToneChatInput utterances, string contentLanguage = null, string acceptLanguage = null, Dictionary<string, object> customData = null)
        {
            if (utterances == null)
                throw new ArgumentNullException(nameof(utterances));

            if (string.IsNullOrEmpty(VersionDate))
                throw new ArgumentNullException("versionDate cannot be null.");

            UtteranceAnalyses result = null;

            try
            {
                IClient client = this.Client;
                if (_tokenManager != null)
                {
                    client = this.Client.WithAuthentication(_tokenManager.GetToken());
                }
                if (_tokenManager == null)
                {
                    client = this.Client.WithAuthentication(this.UserName, this.Password);
                }

                var restRequest = client.PostAsync($"{this.Endpoint}/v3/tone_chat");

                restRequest.WithArgument("version", VersionDate);
                if (!string.IsNullOrEmpty(contentLanguage))
                    restRequest.WithHeader("Content-Language", contentLanguage);
                if (!string.IsNullOrEmpty(acceptLanguage))
                    restRequest.WithHeader("Accept-Language", acceptLanguage);
                restRequest.WithBody<ToneChatInput>(utterances);
                if (customData != null)
                    restRequest.WithCustomData(customData);

                restRequest.WithHeader("X-IBMCloud-SDK-Analytics", "service_name=tone_analyzer;service_version=v3;operation_id=ToneChat");
                result = restRequest.As<UtteranceAnalyses>().Result;
                if (result == null)
                    result = new UtteranceAnalyses();
                result.CustomData = restRequest.CustomData;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}
