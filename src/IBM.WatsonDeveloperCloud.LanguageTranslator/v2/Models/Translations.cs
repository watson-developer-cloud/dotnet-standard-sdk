using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class Translations
    {
        /// <summary>
        /// Translation output in UTF-8.
        /// </summary>
        [JsonProperty("translation")]
        public string Translation { get; set; }
    }
}