using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class DeleteModels
    {
        /// <summary>
        /// Indicates that the model was successfully deleted.
        /// </summary>
        [JsonProperty("deleted")]
        public string Deleted { get; set; }
    }
}