using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Models
{
    public class ModelPayload
    {
        /// <summary>
        /// A globally unique string that identifies the underlying model that is used for translation. This string contains all the information about source language, target language, domain, and various other related configurations. 
        /// </summary>
        [JsonProperty("model_id")]
        public string ModelId { get; set; }

        /// <summary>
        /// If a model is trained by a user, there could be an optional “name” parameter attached during training to help the user identify his model.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Source language in two letter language code. Use the five letter code when clarifying between multiple supported languages. When model_id is used directly, it will override the source-target language combination. Also, when a two letter language code is used, but no suitable default is found, it returns an error.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Translation target language in 2 or 5 letter language code. Should use 2 letter codes except for when clarifying between multiple supported languages. When model_id is used directly, it will override the source-target language combination. Also, when a 2 letter language code is used, and no suitable default is found, it returns an error.
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }

        /// <summary>
        /// For a model, returns the base model it was trained on. For a base model, the response value is empty.
        /// </summary>
        [JsonProperty("base_model_id")]
        public string BaseModelId { get; set; }

        /// <summary>
        /// The domain of the translation model.
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Describes whether this model can be used as a base for customization. Customized models are not further customizable, and we don't allow the customization of certain base models.
        /// </summary>
        [JsonProperty("customizable")]
        public bool Customizable { get; set; }

        /// <summary>
        /// Describes whether this model is considered default, and whether it is used when the source and target languages are specified without the model_id.
        /// </summary>
        [JsonProperty("default_model")]
        public bool DefaultModel { get; set; }

        /// <summary>
        /// Either an empty string, indicating it’s a model trained by IBM, or the bluemix-instance-id of the instance where the model was created.
        /// </summary>
        [JsonProperty("owner")]
        public string Owner { get; set; }

        /// <summary>
        /// Availability of a model. Valid response values are "available", "training", or "error".
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}