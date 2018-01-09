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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model
{
    /// <summary>
    /// TranslateRequest.
    /// </summary>
    public class TranslateRequest
    {
        /// <summary>
        /// Input text in UTF-8 encoding. It is a list so that multiple paragraphs can be submitted. Also accept a single string, instead of an array, as valid input.
        /// </summary>
        /// <value>Input text in UTF-8 encoding. It is a list so that multiple paragraphs can be submitted. Also accept a single string, instead of an array, as valid input.</value>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Text { get; set; }
        /// <summary>
        /// The unique model_id of the translation model being used to translate text. The model_id inherently specifies source language, target language, and domain. If the model_id is specified, there is no need for the source and target parameters and the values are ignored.
        /// </summary>
        /// <value>The unique model_id of the translation model being used to translate text. The model_id inherently specifies source language, target language, and domain. If the model_id is specified, there is no need for the source and target parameters and the values are ignored.</value>
        [JsonProperty("model_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ModelId { get; set; }
        /// <summary>
        /// Used in combination with target as an alternative way to select the model for translation. When target and source are set, and model_id is not set, the system chooses a default model with the right language pair to translate (usually the model based on the news domain).
        /// </summary>
        /// <value>Used in combination with target as an alternative way to select the model for translation. When target and source are set, and model_id is not set, the system chooses a default model with the right language pair to translate (usually the model based on the news domain).</value>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        /// <summary>
        /// Used in combination with source as an alternative way to select the model for translation. When target and source are set, and model_id is not set, the system chooses a default model with the right language pair to translate (usually the model based on the news domain).
        /// </summary>
        /// <value>Used in combination with source as an alternative way to select the model for translation. When target and source are set, and model_id is not set, the system chooses a default model with the right language pair to translate (usually the model based on the news domain).</value>
        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; set; }
    }

}
