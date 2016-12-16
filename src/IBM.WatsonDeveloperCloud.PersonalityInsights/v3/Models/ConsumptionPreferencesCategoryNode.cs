using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models
{
    public class ConsumptionPreferencesCategoryNode
    {
        [JsonProperty("consumption_preference_category_id")]
        public string ConsumptionPreferenceCategoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("consumption_preferences")]
        public List<ConsumptionPreferencesNode> ConsumptionPreferences { get; set; }
    }
}