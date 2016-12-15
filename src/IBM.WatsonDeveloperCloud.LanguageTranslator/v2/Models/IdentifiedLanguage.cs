using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class IdentifiedLanguage
    {
        /// <summary>
        /// The code for an identified language.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// The confidence score for the identified language. numberMax. Value:1
        /// </summary>
        [JsonProperty("confidence")]
        public double Confidence { get; set; }
    }
}