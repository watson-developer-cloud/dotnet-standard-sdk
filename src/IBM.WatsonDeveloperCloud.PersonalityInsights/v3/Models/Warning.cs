using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models
{
    public class Warning
    {
        [JsonProperty("warning_id")]
        public string WarningId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}