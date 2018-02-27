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

        private SpeechRecognitionResults Recognize(string sessionId, string contentType, Metadata metaData, Stream audio, string transferEncoding = "", string model = "", string customizationId = "", bool? continuous = null, int? inactivityTimeout = null, string[] keywords = null, double? keywordsThreshold = null, int? maxAlternatives = null, double? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool profanityFilter = false, bool? smartFormatting = null, bool? speakerLabels = null)
        {
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException($"{nameof(contentType)}");

            SpeechRecognitionResults result = null;

            try
            {
                string urlService = string.Empty;
                IRequest request = null;

                if (string.IsNullOrEmpty(sessionId))
                {
                    request =
                        this.Client.WithAuthentication(this.UserName, this.Password)
                               .PostAsync($"{this.Endpoint}/v1/recognize");
                }
                else
                {
                    request =
                        this.Client.WithAuthentication(this.UserName, this.Password)
                                   .PostAsync($"{this.Endpoint}/v1/sessions/{sessionId}")
                                   .WithHeader("Cookie", sessionId);
                }

                if (!string.IsNullOrEmpty(transferEncoding))
                    request.WithHeader("Transfer-Encoding", transferEncoding);

                if (metaData == null)
                {
                    // if a session exists, the model should not be sent
                    if (string.IsNullOrEmpty(sessionId))
                        request.WithArgument("model", model);

                    if (!string.IsNullOrEmpty(customizationId))
                        request.WithArgument("customization_id", customizationId);

                    if (continuous.HasValue)
                        request.WithArgument("continuous", continuous.Value);

                    if (inactivityTimeout.HasValue && inactivityTimeout.Value > 0)
                        request.WithArgument("inactivity_timeout", inactivityTimeout.Value);

                    if (keywords != null && keywords.Length > 0)
                        request.WithArgument("keywords", keywords);

                    if (keywordsThreshold.HasValue && keywordsThreshold.Value > 0)
                        request.WithArgument("keywords_threshold", keywordsThreshold.Value);

                    if (maxAlternatives.HasValue && maxAlternatives.Value > 0)
                        request.WithArgument("max_alternatives", maxAlternatives.Value);

                    if (wordAlternativesThreshold.HasValue && wordAlternativesThreshold.Value > 0)
                        request.WithArgument("word_alternatives_threshold", wordAlternativesThreshold.Value);

                    if (wordConfidence.HasValue)
                        request.WithArgument("word_confidence", wordConfidence.Value);

                    if (timestamps.HasValue)
                        request.WithArgument("timestamps", timestamps.Value);

                    if (profanityFilter)
                        request.WithArgument("profanity_filter", profanityFilter);

                    if (smartFormatting.HasValue)
                        request.WithArgument("smart_formatting", smartFormatting.Value);

                    if (speakerLabels.HasValue)
                        request.WithArgument("speaker_labels", speakerLabels.Value);

                    StreamContent bodyContent = new StreamContent(audio);
                    bodyContent.Headers.Add("Content-Type", contentType);

                    request.WithBodyContent(bodyContent);
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
                        request.WithArgument("model", model);

                    formData.Add(metadata, "metadata");
                    formData.Add(audioContent, "upload");

                    request.WithBodyContent(formData);
                }

                result =
                    request.As<SpeechRecognitionResults>()
                           .Result;

            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }

        public List<SpeechRecognitionResults> ObserveResult(string sessionId, int? sequenceId = (int?)null, bool interimResults = false)
        {
            List<SpeechRecognitionResults> result = null;

            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("SessionId can not be null or empty");

            try
            {
                var request =
                    this.Client.WithAuthentication(this.UserName, this.Password)
                               .GetAsync($"{this.Endpoint}/v1/sessions/{sessionId}/observe_result");

                if (sequenceId.HasValue)
                    request.WithArgument("sequence_id", sequenceId);

                if (interimResults)
                    request.WithArgument("interim_results", interimResults);

                var strResult =
                    request.AsString()
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

        public object AddAudio(string customizationId, string audioName, byte[] audioResource, string contentType, string containedContentType = null, bool? allowOverwrite = null)
        {
            if (string.IsNullOrEmpty(customizationId))
                throw new ArgumentNullException(nameof(customizationId));
            if (string.IsNullOrEmpty(audioName))
                throw new ArgumentNullException(nameof(audioName));
            if (audioResource == null)
                throw new ArgumentNullException(nameof(audioResource));
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException(nameof(contentType));
            object result = null;

            try
            {
                var request = this.Client.WithAuthentication(this.UserName, this.Password)
                    .PostAsync($"{this.Endpoint}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");

                request.WithHeader("Content-Type", contentType);
                request.WithHeader("Contained-Content-Type", containedContentType);

                if (allowOverwrite != null)
                    request.WithArgument("allow_overwrite", allowOverwrite);

                var trainingDataContent = new ByteArrayContent(audioResource);
                MediaTypeHeaderValue audioType;
                MediaTypeHeaderValue.TryParse(contentType, out audioType);
                trainingDataContent.Headers.ContentType = audioType;

                request.WithBodyContent(trainingDataContent);

                result = request.As<object>().Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }

            return result;
        }
    }
}