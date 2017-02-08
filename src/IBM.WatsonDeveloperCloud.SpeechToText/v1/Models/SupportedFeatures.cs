using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Models
{
    public class SupportedFeatures
    {
        [JsonProperty("custom_language_model")]
        public bool CustomLanguageModel { get; set; }

        [JsonProperty("speaker_labels")]
        public bool SpeakerLabels { get; set; }
    }
}