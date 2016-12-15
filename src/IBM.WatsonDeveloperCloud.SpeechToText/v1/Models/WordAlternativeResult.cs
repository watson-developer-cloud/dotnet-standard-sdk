using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class WordAlternativeResult
    {
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("word")]
        public double Word { get; set; }
    }
}