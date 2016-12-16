using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models
{
    public class Profile
    {
        [JsonProperty("processed_language")]
        public string ProcessedLanguage { get; set; }

        [JsonProperty("word_count")]
        public int WordCount { get; set; }

        [JsonProperty("word_count_message")]
        public string WordCountMessage { get; set; }

        [JsonProperty("personality")]
        public List<TraitTreeNode> Personality { get; set; }

        [JsonProperty("values")]
        public List<TraitTreeNode> Values { get; set; }

        [JsonProperty("needs")]
        public List<TraitTreeNode> Needs { get; set; }

        [JsonProperty("behavior")]
        public List<BehaviorNode> Behavior { get; set; }

        [JsonProperty("consumption_preferences")]
        public List<ConsumptionPreferencesCategoryNode> ConsumptionPreferences { get; set; }

        [JsonProperty("warning")]
        public List<Warning> Warning { get; set; }
    }
}