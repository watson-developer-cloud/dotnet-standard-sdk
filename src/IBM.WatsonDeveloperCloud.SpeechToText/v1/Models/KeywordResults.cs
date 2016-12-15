using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class KeywordResults
    {
        [JsonProperty("keyword")]
        public List<KeywordResult> Keyword { get; set; }
    }
}
