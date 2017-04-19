using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public class SupportedFeatures
    {
        /// <summary>
        /// Gets or sets indicates whether the customization interface can be used with the language model.
        /// </summary>
        [JsonProperty("custom_language_model")]
        public bool CustomLanguageModel { get; set; }

        /// <summary>
        /// Gets or sets indicates whether the `speaker_labels` parameter can be used with the language model.
        /// </summary>
        [JsonProperty("speaker_labels")]
        public bool SpeakerLabels { get; set; }
    }
}