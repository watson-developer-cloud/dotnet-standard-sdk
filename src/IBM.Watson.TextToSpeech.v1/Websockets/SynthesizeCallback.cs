using System;
using IBM.Watson.TextToSpeech.v1.Websockets.Model;

namespace IBM.Watson.TextToSpeech.v1.Websockets
{
    public class SynthesizeCallback
    {
        public Action OnOpen = () => { };
        public Action<byte[]> OnMessage = (message) => { };
        public Action<string> OnContentType = (contentType) => { };
        public Action<MarkTiming> OnMarks = (marks) => { };
        public Action<WordTiming> OnTimings = (timings) => { };
        public Action<Exception> OnError = (e) => { };
        public Action OnClose = () => { };
    }
}