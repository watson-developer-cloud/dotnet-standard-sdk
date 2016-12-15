using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class SpeechRecognitionAlternative
    {
        [JsonProperty("transcript")]
        public string Transcript { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("timestamps")]
        public List<string> Timestamps { get; set; }

        [JsonProperty("word_confidence")]
        public List<string> WordConfidence { get; set; }
    }
}