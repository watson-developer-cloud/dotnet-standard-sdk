using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public class Pronunciation
    {
        [JsonProperty("pronunciation")]
        public string Value { get; set; }
    }
}