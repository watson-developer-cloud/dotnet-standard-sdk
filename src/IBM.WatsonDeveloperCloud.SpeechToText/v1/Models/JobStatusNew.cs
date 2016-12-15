using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class JobStatusNew
    {
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}