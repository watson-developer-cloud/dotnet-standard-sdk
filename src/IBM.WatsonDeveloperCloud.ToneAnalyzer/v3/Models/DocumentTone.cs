using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Models
{
    public class DocumentTone
    {
        [JsonProperty("tone_categories")]
        public List<ToneCategory> ToneCategories { get; set; }
    }
}