using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class CustomModels
    {
        /// <summary>
        /// Returns the base model that this translation model was trained on
        /// </summary>
        [JsonProperty("model_id")]
        public string ModelId { get; set; }
    }
}