/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

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
