using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Models
{
    public class ToneScore
    {
        [JsonProperty("score")]
        public double Score { get; set; }
        [JsonProperty("tone_id")]
        public string ToneId { get; set; }
        [JsonProperty("tone_name")]
        public string ToneName { get; set; }
    }
}