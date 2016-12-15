using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class TranslationModels
    {
        /// <summary>
        /// An List of available models.
        /// </summary>
        [JsonProperty("models")]
        public List<ModelPayload> Models { get; set; }
    }
}