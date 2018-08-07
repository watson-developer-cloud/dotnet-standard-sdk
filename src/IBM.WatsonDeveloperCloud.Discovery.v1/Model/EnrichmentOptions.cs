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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Options which are specific to a particular enrichment.
    /// </summary>
    public class EnrichmentOptions : BaseModel
    {
        /// <summary>
        /// ISO 639-1 code indicating the language to use for the analysis. This code overrides the automatic language
        /// detection performed by the service. Valid codes are `ar` (Arabic), `en` (English), `fr` (French), `de`
        /// (German), `it` (Italian), `pt` (Portuguese), `ru` (Russian), `es` (Spanish), and `sv` (Swedish). **Note:**
        /// Not all features support all languages, automatic detection is recommended.
        /// </summary>
        /// <value>
        /// ISO 639-1 code indicating the language to use for the analysis. This code overrides the automatic language
        /// detection performed by the service. Valid codes are `ar` (Arabic), `en` (English), `fr` (French), `de`
        /// (German), `it` (Italian), `pt` (Portuguese), `ru` (Russian), `es` (Spanish), and `sv` (Swedish). **Note:**
        /// Not all features support all languages, automatic detection is recommended.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum LanguageEnum
        {
            
            /// <summary>
            /// Enum AR for ar
            /// </summary>
            [EnumMember(Value = "ar")]
            AR,
            
            /// <summary>
            /// Enum EN for en
            /// </summary>
            [EnumMember(Value = "en")]
            EN,
            
            /// <summary>
            /// Enum FR for fr
            /// </summary>
            [EnumMember(Value = "fr")]
            FR,
            
            /// <summary>
            /// Enum DE for de
            /// </summary>
            [EnumMember(Value = "de")]
            DE,
            
            /// <summary>
            /// Enum IT for it
            /// </summary>
            [EnumMember(Value = "it")]
            IT,
            
            /// <summary>
            /// Enum PT for pt
            /// </summary>
            [EnumMember(Value = "pt")]
            PT,
            
            /// <summary>
            /// Enum RU for ru
            /// </summary>
            [EnumMember(Value = "ru")]
            RU,
            
            /// <summary>
            /// Enum ES for es
            /// </summary>
            [EnumMember(Value = "es")]
            ES,
            
            /// <summary>
            /// Enum SV for sv
            /// </summary>
            [EnumMember(Value = "sv")]
            SV
        }

        /// <summary>
        /// ISO 639-1 code indicating the language to use for the analysis. This code overrides the automatic language
        /// detection performed by the service. Valid codes are `ar` (Arabic), `en` (English), `fr` (French), `de`
        /// (German), `it` (Italian), `pt` (Portuguese), `ru` (Russian), `es` (Spanish), and `sv` (Swedish). **Note:**
        /// Not all features support all languages, automatic detection is recommended.
        /// </summary>
        /// <value>
        /// ISO 639-1 code indicating the language to use for the analysis. This code overrides the automatic language
        /// detection performed by the service. Valid codes are `ar` (Arabic), `en` (English), `fr` (French), `de`
        /// (German), `it` (Italian), `pt` (Portuguese), `ru` (Russian), `es` (Spanish), and `sv` (Swedish). **Note:**
        /// Not all features support all languages, automatic detection is recommended.
        /// </value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public LanguageEnum? Language { get; set; }
        /// <summary>
        /// An object representing the enrichment features that will be applied to the specified field.
        /// </summary>
        /// <value>
        /// An object representing the enrichment features that will be applied to the specified field.
        /// </value>
        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public NluEnrichmentFeatures Features { get; set; }
        /// <summary>
        /// *For use with `elements` enrichments only.* The element extraction model to use. Models available are:
        /// `contract`.
        /// </summary>
        /// <value>
        /// *For use with `elements` enrichments only.* The element extraction model to use. Models available are:
        /// `contract`.
        /// </value>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    }

}
