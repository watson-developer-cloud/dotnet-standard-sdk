using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class SessionStatus
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("recognize")]
        public string Recognize { get; set; }

        [JsonProperty("observe_result")]
        public string ObserveResult { get; set; }

        [JsonProperty("recognizeWS")]
        public string RecognizeWS { get; set; }
    }
}