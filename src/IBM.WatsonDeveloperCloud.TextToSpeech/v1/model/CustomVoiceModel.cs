using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public class CustomVoiceModel
    {
        [JsonProperty("customization_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; private set; }

        [JsonConverter(typeof(CustomDateFormat), "dd/MM/yyyy HH:mm:ss")]
        [JsonProperty("created")]
        public DateTime Created { get; private set; }

        [JsonProperty("last_modified")]
        [JsonConverter(typeof(CustomDateFormat), "dd/MM/yyyy HH:mm:ss")]
        public DateTime LastModified { get; set; }

        [JsonProperty("words")]
        public List<CustomWordTranslation> Words { get; set; }
    }
    
    internal class CustomVoiceModels
    {
        [JsonProperty("customizations")]
        internal List<CustomVoiceModel> CustomVoiceList { get; set; }
    }

    internal class CustomVoiceModelCreate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    internal class CustomVoiceModelUpdate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("words")]
        public List<CustomWordTranslation> Words { get; set; }
    }

    public class CustomDateFormat : IsoDateTimeConverter
    {
        public CustomDateFormat(string format)
        {
            DateTimeFormat = format;
        }
    }
}
