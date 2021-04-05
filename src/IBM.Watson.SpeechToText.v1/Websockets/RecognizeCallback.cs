using System;
using IBM.Watson.SpeechToText.v1.Model;

namespace IBM.Watson.SpeechToText.v1.Websockets
{
    public class RecognizeCallback
    {
        public Action OnOpen = () => { };
        public Action<SpeechRecognitionResults> OnMessage = (speechResults) => { };
        public Action<Exception> OnError = (e) => { };
        public Action OnClose = () => { };
    }
}