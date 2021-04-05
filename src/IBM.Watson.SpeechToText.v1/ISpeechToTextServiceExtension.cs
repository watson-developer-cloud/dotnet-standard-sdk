using System.Collections.Generic;
using IBM.Watson.SpeechToText.v1.Websockets;

namespace IBM.Watson.SpeechToText.v1
{
    public partial interface ISpeechToTextService
    {
        WebSocketClient RecognizeUsingWebSocket(RecognizeCallback callback, System.IO.MemoryStream audio, string contentType = null, string model = null, string languageCustomizationId = null, string acousticCustomizationId = null, string baseModelVersion = null, double? customizationWeight = null, long? inactivityTimeout = null, bool? interimResults = null, List<string> keywords = null, float? keywordsThreshold = null, long? maxAlternatives = null, float? wordAlternativesThreshold = null, bool? wordConfidence = null, bool? timestamps = null, bool? profanityFilter = null, bool? smartFormatting = null, bool? speakerLabels = null, string grammarName = null, bool? redaction = null, bool? processingMetrics = null, float? processingMetricsInterval = null, bool? audioMetrics = null, double? endOfPhraseSilenceTime = null, bool? splitTranscriptAtPhraseEnd = null, float? speechDetectorSensitivity = null, float? backgroundAudioSuppression = null);
    }
}
