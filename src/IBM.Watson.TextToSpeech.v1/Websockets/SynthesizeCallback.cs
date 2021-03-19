using System;
namespace IBM.Watson.TextToSpeech.v1.Websockets
{
    public class SynthesizeCallback
    {
        public Action OnOpen = () => { };
        public Action<byte[]> OnMessage = (message) => { };
        public Action<string> OnContentType = (contentType) => { };
        public Action<MarkTiming> OnMarks = (marks) => { };
        public Action<WordTiming> onTimings = (timings) => { };
        public Action<Exception> OnError = (ex) => { };
        public Action OnClose = () => { };
    }
}