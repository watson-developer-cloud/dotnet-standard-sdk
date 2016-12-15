using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class IdentifiableLanguage
    {
        /// <summary>
        /// The code for an identifiable language.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// The name of the identifiable language.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}