using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class ModelSet
    {
        [JsonProperty("models")]
        public List<Model> Models { get; set; }
    }
}