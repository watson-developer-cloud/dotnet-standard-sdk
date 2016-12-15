using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public class WordPronunciation
    {
        [JsonProperty("pronunciation")]
        public string Pronunciation { get; set; }
    }
}