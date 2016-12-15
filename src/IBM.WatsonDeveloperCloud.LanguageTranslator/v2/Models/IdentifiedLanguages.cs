using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class IdentifiedLanguages
    {
        /// <summary>
        /// A ranking of identified languages with confidence scores.
        /// </summary>
        [JsonProperty("languages")]
        public List<IdentifiedLanguage> Languages { get; set; }
    }
}