using System.Collections.Generic;
using IBM.Watson.TextToSpeech.v1.Websockets;

namespace IBM.Watson.TextToSpeech.v1
{
    public partial interface ITextToSpeechService
    {
        WebSocketClient SynthesizeUsingWebSocket(SynthesizeCallback callback, string text, string voice = null, string customizationId = null, string accept = null, List<string> timings = null);
    }
}
