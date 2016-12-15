using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Models
{
    public class ToneCategory
    {
        [JsonProperty("tones")]
        public List<ToneScore> Tones { get; set; }
        [JsonProperty("category_id")]
        public string CategoryId { get; set; }
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }
    }
}