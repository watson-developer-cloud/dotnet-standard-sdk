using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Models
{
    public class ToneAnalysis
    {
        [JsonProperty("document_tone")]
        public DocumentTone DocumentTone { get; set; }
        [JsonProperty("sentences_tone")]
        public List<SentenceAnalysis> SentencesTone { get; set; }
    }
}