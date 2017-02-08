using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class Model
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("supported_features")]
        public SupportedFeatures SupportedFeatures { get; set; }

        [JsonProperty("sessions")]
        public string Sessions { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}