using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models
{
    public class TraitTreeNode
    {
        [JsonProperty("trait_id")]
        public string TraitId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("percentile")]
        public double Percentile { get; set; }

        [JsonProperty("raw_score")]
        public double RawScore { get; set; }

        [JsonProperty("children")]
        public List<TraitTreeNode> Children { get; set; }
    }
}