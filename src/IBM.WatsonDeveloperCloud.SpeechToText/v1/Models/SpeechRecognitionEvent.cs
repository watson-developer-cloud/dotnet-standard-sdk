using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class SpeechRecognitionEvent
    {
        [JsonProperty("results")]
        public List<SpeechRecognitionResult> Results { get; set; }
        
        [JsonProperty("result_index")]
        public string ResultIndex { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }
    }
}