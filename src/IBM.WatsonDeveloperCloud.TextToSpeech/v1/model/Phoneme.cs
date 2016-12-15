namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public class Phoneme
    {
        public static readonly Phoneme IPA = new Phoneme("ipa");
        public static readonly Phoneme SPR = new Phoneme("spr");

        public string Value { get; private set; }

        public Phoneme(string value)
        {
            this.Value = value;
        }
    }
}