using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Models
{
    public class SentenceAnalysis
    {
        [JsonProperty("sentence_id")]
        public int SentenceId { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("input_from")]
        public int InputFrom { get; set; }
        [JsonProperty("input_to")]
        public int InputTo { get; set; }
        [JsonProperty("tone_categories")]
        public List<ToneCategory> ToneCategories { get; set; }
    }
}