using System;
using System.Collections.Generic;
using System.IO;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.SpeechToText.v1.Websockets;

namespace IBM.Watson.SpeechToText.v1
{
    public partial class SpeechToTextService : IBMService, ISpeechToTextService
    {
        public WebSocketClient RecognizeUsingWebsockets(RecognizeCallback callback, System.IO.MemoryStream audio, string contentType = null, string model = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, bool? interimResults = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string customizationId = null, string grammarName = null, bool? redaction = null, bool? processingMetrics = null, float? processingMetricsInterval = null, bool? audioMetrics = null, double? endOfPhraseSilenceTime = null, bool? splitTranscriptAtPhraseEnd = null, float? speechDetectorSensitivity = null, float? backgroundAudioSuppression = null)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback cannot be null");
            }
            if (audio == null)
            {
                throw new ArgumentNullException("`audio` is required for `Recognize`");
            }

            try
            {
                IClient client = this.Client;
                SetAuthentication();

                string url = ($"{this.Endpoint}/v1/recognize").Replace("https://", "wss://");
                WebSocketClient webSocketClient = new WebSocketClient(url, callback);

                if (!string.IsNullOrEmpty(model))
                {
                    webSocketClient.AddArgument("model", model);
                }
                if (!string.IsNullOrEmpty(customizationId))
                {
                    webSocketClient.AddArgument("language_customization_id", languageCustomizationId);
                }
                if (!string.IsNullOrEmpty(acousticCustomizationId))
                {
                    webSocketClient.AddArgument("acoustic_customization_id", acousticCustomizationId);
                }
                if (!string.IsNullOrEmpty(baseModelVersion))
                {
                    webSocketClient.AddArgument("base_model_version", baseModelVersion);
                }
                if(!string.IsNullOrEmpty(customizationId))
                {
                    webSocketClient.AddArgument("customization_id", customizationId);
                }


                if (!string.IsNullOrEmpty(contentType))
                {
                    webSocketClient.AddWebsocketParameter("content-type", contentType);
                }
                if (customizationWeight != null)
                {
                    webSocketClient.AddWebsocketParameter("customization_weight", customizationWeight);
                }
                if (inactivityTimeout != null)
                {
                    webSocketClient.AddWebsocketParameter("inactivity_timeout", inactivityTimeout);
                }
                if (interimResults != null)
                {
                    webSocketClient.AddWebsocketParameter("interim_results", interimResults);
                }
                if (keywords != null && keywords.Count > 0)
                {
                    webSocketClient.AddWebsocketParameter("keywords", string.Join(",", keywords.ToArray()));
                }
                if (keywordsThreshold != null)
                {
                    webSocketClient.AddWebsocketParameter("keywords_threshold", keywordsThreshold);
                }
                if (maxAlternatives != null)
                {
                    webSocketClient.AddWebsocketParameter("max_alternatives", maxAlternatives);
                }
                if (wordAlternativesThreshold != null)
                {
                    webSocketClient.AddWebsocketParameter("word_alternatives_threshold", wordAlternativesThreshold);
                }
                if (wordConfidence != null)
                {
                    webSocketClient.AddWebsocketParameter("word_confidence", wordConfidence);
                }
                if (timestamps != null)
                {
                    webSocketClient.AddWebsocketParameter("timestamps", timestamps);
                }
                if (profanityFilter != null)
                {
                    webSocketClient.AddWebsocketParameter("profanity_filter", profanityFilter);
                }
                if (smartFormatting != null)
                {
                    webSocketClient.AddWebsocketParameter("smart_formatting", smartFormatting);
                }
                if (speakerLabels != null)
                {
                    webSocketClient.AddWebsocketParameter("speaker_labels", speakerLabels);
                }
                if (!string.IsNullOrEmpty(grammarName))
                {
                    webSocketClient.AddWebsocketParameter("grammar_name", grammarName);
                }
                if (redaction != null)
                {
                    webSocketClient.AddWebsocketParameter("redaction", redaction);
                }
                if (processingMetrics != null)
                {
                    webSocketClient.AddWebsocketParameter("processing_metrics", processingMetrics);
                }
                if (processingMetricsInterval != null)
                {
                    webSocketClient.AddWebsocketParameter("processing_metrics_interval", processingMetricsInterval);
                }
                if (audioMetrics != null)
                {
                    webSocketClient.AddWebsocketParameter("audio_metrics", audioMetrics);
                }
                if (endOfPhraseSilenceTime != null)
                {
                    webSocketClient.AddWebsocketParameter("end_of_phrase_silence_time", endOfPhraseSilenceTime);
                }
                if (splitTranscriptAtPhraseEnd != null)
                {
                    webSocketClient.AddWebsocketParameter("split_transcript_at_phrase_end", splitTranscriptAtPhraseEnd);
                }
                if (speechDetectorSensitivity != null)
                {
                    webSocketClient.AddWebsocketParameter("speech_detector_sensitivity", speechDetectorSensitivity);
                }
                if (backgroundAudioSuppression != null)
                {
                    webSocketClient.AddWebsocketParameter("background_audio_suppression", backgroundAudioSuppression);
                }

                //webSocketClient.AddWebsocketParameter("interim_results", true);
                var sdkHeaders = Common.GetSdkHeaders("speech_to_text", "v1", "RecognizeUsingWebsockets");
                foreach (var header in sdkHeaders)
                {
                    webSocketClient.WithHeader(header.Key, header.Value);
                }

                foreach (var header in customRequestHeaders)
                {
                    webSocketClient.WithHeader(header.Key, header.Value);
                }

                foreach (var header in client.BaseClient.DefaultRequestHeaders)
                {
                    var enumerator = header.Value.GetEnumerator();
                    enumerator.MoveNext();
                    var value = enumerator.Current;
                    webSocketClient = (WebSocketClient)webSocketClient.WithHeader(header.Key, value);
                }
                webSocketClient.Send(audio);

                return webSocketClient;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }
    }
}
