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

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{

    public partial class CustomVoice
    {
        /// <summary>
        /// Gets or sets name of the new custom voice model.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets language of the new custom voice model. Omit the
        /// parameter to use the default language, `en-US`. Possible values
        /// include: 'de-DE', 'en-US', 'en-GB', 'es-ES', 'es-LA', 'es-US',
        /// 'fr-FR', 'it-IT', 'ja-JP', 'pt-BR'
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets description of the new custom voice model.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
