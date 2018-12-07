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
using IBM.WatsonDeveloperCloud.Service;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using System;
using System.IO;
using System.Net.Http;


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
        public SpeechRecognitionResults Recognize(string contentType, Stream audio, string transferEncoding = "", string model = "en-US_BroadbandModel", string languageCustomizationId = null, bool? continuous = null, int? inactivityTimeout = null, string[] keywords = null, double? keywordsThreshold = null, int? maxAlternatives = null, double? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool profanityFilter = false, bool? smartFormatting = null, bool? speakerLabels = null, string customizationId = null)
        {
            if (audio == null)
                throw new ArgumentNullException($"{nameof(audio)}");

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

                restRequest = client.PostAsync($"{this.Endpoint}/v1/recognize");


                if (!string.IsNullOrEmpty(transferEncoding))
                    restRequest.WithHeader("Transfer-Encoding", transferEncoding);

                if (!string.IsNullOrEmpty(languageCustomizationId))
                    restRequest.WithArgument("language_customization_id", languageCustomizationId);

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

                if (!string.IsNullOrEmpty(customizationId))
                    restRequest.WithArgument("customization_id", customizationId);

                StreamContent bodyContent = new StreamContent(audio);
                if (!string.IsNullOrEmpty(contentType))
                    bodyContent.Headers.Add("Content-Type", contentType);

                restRequest.WithBodyContent(bodyContent);

                result = restRequest.As<SpeechRecognitionResults>()
                           .Result;

            }
            catch (AggregateException ae)
            {
                throw ae.InnerException as ServiceResponseException;
            }

            return result;
        }
    }
}
