using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models
{
    public class ConsumptionPreferencesNode
    {
        [JsonProperty("consumption_preference_id")]
        public string ConsumptionPreferenceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }
}