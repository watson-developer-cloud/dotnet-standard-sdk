/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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
        /// Input text in UTF-8 encoding. Multiple entries will result in multiple translations in the response.
        /// </summary>
        /// <value>Input text in UTF-8 encoding. Multiple entries will result in multiple translations in the response.</value>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Text { get; set; }
        /// <summary>
        /// Model ID of the translation model to use. If this is specified, the `source` and `target` parameters will be ignored. The method requires either a model ID or both the `source` and `target` parameters.
        /// </summary>
        /// <value>Model ID of the translation model to use. If this is specified, the `source` and `target` parameters will be ignored. The method requires either a model ID or both the `source` and `target` parameters.</value>
        [JsonProperty("model_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ModelId { get; set; }
        /// <summary>
        /// Language code of the source text language. Use with `target` as an alternative way to select a translation model. When `source` and `target` are set, and a model ID is not set, the system chooses a default model for the language pair (usually the model based on the news domain).
        /// </summary>
        /// <value>Language code of the source text language. Use with `target` as an alternative way to select a translation model. When `source` and `target` are set, and a model ID is not set, the system chooses a default model for the language pair (usually the model based on the news domain).</value>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        /// <summary>
        /// Language code of the translation target language. Use with source as an alternative way to select a translation model.
        /// </summary>
        /// <value>Language code of the translation target language. Use with source as an alternative way to select a translation model.</value>
        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; set; }
    }

}
