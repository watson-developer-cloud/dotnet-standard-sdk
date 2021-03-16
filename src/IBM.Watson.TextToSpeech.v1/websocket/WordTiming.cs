namespace IBM.Watson.TextToSpeech.v1.websocket
{
    public class WordTiming
    {
        public string word { get;}
        public double startTime { get;}
        public double endTime { get;}

        public WordTiming(string word, double startTime, double endTime)
        {
            this.word = word;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public override string ToString()
        {
            return word + " " + startTime + "-" + endTime;
        }
    }
}
