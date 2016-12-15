using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class IdentifiableLanguages
    {
        /// <summary>
        /// A list of all languages that the service can identify.
        /// </summary>
        [JsonProperty("languages")]
        public List<IdentifiableLanguage> Languages { get; set; }
    }
}