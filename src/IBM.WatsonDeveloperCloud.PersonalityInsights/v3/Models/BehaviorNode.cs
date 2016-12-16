using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models
{
    public class BehaviorNode
    {
        [JsonProperty("trait_id")]
        public string TraitId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("percentage")]
        public double Percentage { get; set; }
    }
}