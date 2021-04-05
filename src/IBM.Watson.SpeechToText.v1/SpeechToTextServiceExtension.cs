using System;
using System.Collections.Generic;
using System.Linq;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using IBM.Watson.SpeechToText.v1.Websockets;

namespace IBM.Watson.SpeechToText.v1
{
    public partial class SpeechToTextService : IBMService, ISpeechToTextService
    {
        public WebSocketClient RecognizeUsingWebSocket(RecognizeCallback callback, System.IO.MemoryStream audio, string contentType = null, string model = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, bool? interimResults = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string grammarName = null, bool? redaction = null, bool? processingMetrics = null, float? processingMetricsInterval = null, bool? audioMetrics = null, double? endOfPhraseSilenceTime = null, bool? splitTranscriptAtPhraseEnd = null, float? speechDetectorSensitivity = null, float? backgroundAudioSuppression = null)
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
                if (!string.IsNullOrEmpty(languageCustomizationId))
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

                // Websocket Open Message
                if (!string.IsNullOrEmpty(contentType))
                {
                    webSocketClient.AddWebSocketParameter("content-type", contentType);
                }
                if (customizationWeight != null)
                {
                    webSocketClient.AddWebSocketParameter("customization_weight", customizationWeight);
                }
                if (inactivityTimeout != null)
                {
                    webSocketClient.AddWebSocketParameter("inactivity_timeout", inactivityTimeout);
                }
                if (interimResults != null)
                {
                    webSocketClient.AddWebSocketParameter("interim_results", interimResults);
                }
                if (keywords != null && keywords.Count > 0)
                {
                    webSocketClient.AddWebSocketParameter("keywords", string.Join(",", keywords.Select(x => "\"" + x + "\"")));
                }
                if (keywordsThreshold != null)
                {
                    webSocketClient.AddWebSocketParameter("keywords_threshold", keywordsThreshold);
                }
                if (maxAlternatives != null)
                {
                    webSocketClient.AddWebSocketParameter("max_alternatives", maxAlternatives);
                }
                if (wordAlternativesThreshold != null)
                {
                    webSocketClient.AddWebSocketParameter("word_alternatives_threshold", wordAlternativesThreshold);
                }
                if (wordConfidence != null)
                {
                    webSocketClient.AddWebSocketParameter("word_confidence", wordConfidence);
                }
                if (timestamps != null)
                {
                    webSocketClient.AddWebSocketParameter("timestamps", timestamps);
                }
                if (profanityFilter != null)
                {
                    webSocketClient.AddWebSocketParameter("profanity_filter", profanityFilter);
                }
                if (smartFormatting != null)
                {
                    webSocketClient.AddWebSocketParameter("smart_formatting", smartFormatting);
                }
                if (speakerLabels != null)
                {
                    webSocketClient.AddWebSocketParameter("speaker_labels", speakerLabels);
                }
                if (!string.IsNullOrEmpty(grammarName))
                {
                    webSocketClient.AddWebSocketParameter("grammar_name", grammarName);
                }
                if (redaction != null)
                {
                    webSocketClient.AddWebSocketParameter("redaction", redaction);
                }
                if (processingMetrics != null)
                {
                    webSocketClient.AddWebSocketParameter("processing_metrics", processingMetrics);
                }
                if (processingMetricsInterval != null)
                {
                    webSocketClient.AddWebSocketParameter("processing_metrics_interval", processingMetricsInterval);
                }
                if (audioMetrics != null)
                {
                    webSocketClient.AddWebSocketParameter("audio_metrics", audioMetrics);
                }
                if (endOfPhraseSilenceTime != null)
                {
                    webSocketClient.AddWebSocketParameter("end_of_phrase_silence_time", endOfPhraseSilenceTime);
                }
                if (splitTranscriptAtPhraseEnd != null)
                {
                    webSocketClient.AddWebSocketParameter("split_transcript_at_phrase_end", splitTranscriptAtPhraseEnd);
                }
                if (speechDetectorSensitivity != null)
                {
                    webSocketClient.AddWebSocketParameter("speech_detector_sensitivity", speechDetectorSensitivity);
                }
                if (backgroundAudioSuppression != null)
                {
                    webSocketClient.AddWebSocketParameter("background_audio_suppression", backgroundAudioSuppression);
                }

                var sdkHeaders = Common.GetSdkHeaders("speech_to_text", "v1", "RecognizeUsingWebSocket");
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
