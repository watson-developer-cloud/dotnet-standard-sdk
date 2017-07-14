/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    public class CustomizationWords
    {
        /// <summary>
        /// Gets or sets GUID of the custom voice model.
        /// </summary>
        [JsonProperty(PropertyName = "customization_id")]
        public string CustomizationId { get; set; }

        /// <summary>
        /// Gets or sets name of the custom voice model.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets language of the custom voice model. Possible values
        /// include: 'de-DE', 'en-US', 'en-GB', 'es-ES', 'es-LA', 'es-US',
        /// 'fr-FR', 'it-IT', 'ja-JP', 'pt-BR'
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets GUID that associates the owning user with the custom
        /// voice model.
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the date and time in Coordinated Universal Time (UTC)
        /// at which the custom voice model was created. The value is
        /// provided in full ISO 8601 format (1YYYY-MM-DDThh:mm:ss.sTZD`)
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public string Created { get; set; }

        /// <summary>
        /// Gets or sets the date and time in Coordinated Universal Time (UTC)
        /// at which the custom voice model was last modified. Equals
        /// `created` when a new voice model is first added but has yet to be
        /// changed. The value is provided in full ISO 8601 format
        /// (`YYYY-MM-DDThh:mm:ss.sTZD`).
        /// </summary>
        [JsonProperty(PropertyName = "last_modified")]
        public string LastModified { get; set; }

        /// <summary>
        /// Gets or sets description of the custom voice model.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets list of words and their translations from the custom
        /// voice model. The words are listed in alphabetical order, with
        /// uppercase letters listed before lowercase letters. The array is
        /// empty if the custom model contains no words.
        /// </summary>
        [JsonProperty(PropertyName = "words")]
        public IList<Word> Words { get; set; }
    }
}
