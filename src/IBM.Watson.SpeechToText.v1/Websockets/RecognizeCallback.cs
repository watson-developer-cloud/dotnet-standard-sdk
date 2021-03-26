using System;
using IBM.Watson.SpeechToText.v1.Model;

namespace IBM.Watson.SpeechToText.v1.Websockets
{
    public class RecognizeCallback
    {
        public Action OnOpen = () => { };
        public Action<SpeechRecognitionResults> OnTranscription = (speechResults) => { };
        public Action<Exception> OnError = (ex) => { };
        public Action OnClose = () => { };
    }
}