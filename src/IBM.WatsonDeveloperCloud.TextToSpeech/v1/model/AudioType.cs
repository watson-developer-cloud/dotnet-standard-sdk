namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public class AudioType
    {
        public string Value { get; private set; }

        public static readonly AudioType OGG = new AudioType("audio=ogg;codecs=opus");
        public static readonly AudioType WAV = new AudioType("audio/wav");
        public static readonly AudioType FLAC = new AudioType("audio/flac");
        public static readonly AudioType L16 = new AudioType("audio/l16");
        public static readonly AudioType BASIC = new AudioType("audio/basic");

        public AudioType(string type)
        {
            this.Value = type;
        }

    }
}
