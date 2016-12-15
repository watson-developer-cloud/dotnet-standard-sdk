using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class TranslateResponse
    {
        /// <summary>
        /// List of translation output in UTF-8, corresponding to the list of input text.
        /// </summary>
        [JsonProperty("translations")]
        public List<Translations> Translations { get; set; }

        /// <summary>
        /// Number of words of the complete input text.
        /// </summary>
        [JsonProperty("word_count")]
        public int WordCount { get; set; }

        /// <summary>
        /// Number of characters of the complete input text.
        /// </summary>
        [JsonProperty("character_count")]
        public int CharacterCount { get; set; }
    }
}