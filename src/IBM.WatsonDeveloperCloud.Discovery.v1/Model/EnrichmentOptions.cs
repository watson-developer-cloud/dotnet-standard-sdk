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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Options which are specific to a particular enrichment.
    /// </summary>
    public class EnrichmentOptions
    {
        /// <summary>
        /// If provided, then do not attempt to detect the language of the input document. Instead, assume the language is the one specified in this field.  You can set this property to work around `unsupported-text-language` errors.  Supported languages include English, German, French, Italian, Portuguese, Russian, Spanish and Swedish. Supported language codes are the ISO-639-1, ISO-639-2, ISO-639-3, and the plain english name of the language (for example \"russian\").
        /// </summary>
        /// <value>If provided, then do not attempt to detect the language of the input document. Instead, assume the language is the one specified in this field.  You can set this property to work around `unsupported-text-language` errors.  Supported languages include English, German, French, Italian, Portuguese, Russian, Spanish and Swedish. Supported language codes are the ISO-639-1, ISO-639-2, ISO-639-3, and the plain english name of the language (for example \"russian\").</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum LanguageEnum
        {
            
            /// <summary>
            /// Enum ENGLISH for english
            /// </summary>
            [EnumMember(Value = "english")]
            ENGLISH,
            
            /// <summary>
            /// Enum GERMAN for german
            /// </summary>
            [EnumMember(Value = "german")]
            GERMAN,
            
            /// <summary>
            /// Enum FRENCH for french
            /// </summary>
            [EnumMember(Value = "french")]
            FRENCH,
            
            /// <summary>
            /// Enum ITALIAN for italian
            /// </summary>
            [EnumMember(Value = "italian")]
            ITALIAN,
            
            /// <summary>
            /// Enum PORTUGUESE for portuguese
            /// </summary>
            [EnumMember(Value = "portuguese")]
            PORTUGUESE,
            
            /// <summary>
            /// Enum RUSSIAN for russian
            /// </summary>
            [EnumMember(Value = "russian")]
            RUSSIAN,
            
            /// <summary>
            /// Enum SPANISH for spanish
            /// </summary>
            [EnumMember(Value = "spanish")]
            SPANISH,
            
            /// <summary>
            /// Enum SWEDISH for swedish
            /// </summary>
            [EnumMember(Value = "swedish")]
            SWEDISH,
            
            /// <summary>
            /// Enum EN for en
            /// </summary>
            [EnumMember(Value = "en")]
            EN,
            
            /// <summary>
            /// Enum ENG for eng
            /// </summary>
            [EnumMember(Value = "eng")]
            ENG,
            
            /// <summary>
            /// Enum DE for de
            /// </summary>
            [EnumMember(Value = "de")]
            DE,
            
            /// <summary>
            /// Enum GER for ger
            /// </summary>
            [EnumMember(Value = "ger")]
            GER,
            
            /// <summary>
            /// Enum DEU for deu
            /// </summary>
            [EnumMember(Value = "deu")]
            DEU,
            
            /// <summary>
            /// Enum FR for fr
            /// </summary>
            [EnumMember(Value = "fr")]
            FR,
            
            /// <summary>
            /// Enum FRE for fre
            /// </summary>
            [EnumMember(Value = "fre")]
            FRE,
            
            /// <summary>
            /// Enum FRA for fra
            /// </summary>
            [EnumMember(Value = "fra")]
            FRA,
            
            /// <summary>
            /// Enum IT for it
            /// </summary>
            [EnumMember(Value = "it")]
            IT,
            
            /// <summary>
            /// Enum ITA for ita
            /// </summary>
            [EnumMember(Value = "ita")]
            ITA,
            
            /// <summary>
            /// Enum PT for pt
            /// </summary>
            [EnumMember(Value = "pt")]
            PT,
            
            /// <summary>
            /// Enum POR for por
            /// </summary>
            [EnumMember(Value = "por")]
            POR,
            
            /// <summary>
            /// Enum RU for ru
            /// </summary>
            [EnumMember(Value = "ru")]
            RU,
            
            /// <summary>
            /// Enum RUS for rus
            /// </summary>
            [EnumMember(Value = "rus")]
            RUS,
            
            /// <summary>
            /// Enum ES for es
            /// </summary>
            [EnumMember(Value = "es")]
            ES,
            
            /// <summary>
            /// Enum SPA for spa
            /// </summary>
            [EnumMember(Value = "spa")]
            SPA,
            
            /// <summary>
            /// Enum SV for sv
            /// </summary>
            [EnumMember(Value = "sv")]
            SV,
            
            /// <summary>
            /// Enum SWE for swe
            /// </summary>
            [EnumMember(Value = "swe")]
            SWE
        }

        /// <summary>
        /// If provided, then do not attempt to detect the language of the input document. Instead, assume the language is the one specified in this field.  You can set this property to work around `unsupported-text-language` errors.  Supported languages include English, German, French, Italian, Portuguese, Russian, Spanish and Swedish. Supported language codes are the ISO-639-1, ISO-639-2, ISO-639-3, and the plain english name of the language (for example \"russian\").
        /// </summary>
        /// <value>If provided, then do not attempt to detect the language of the input document. Instead, assume the language is the one specified in this field.  You can set this property to work around `unsupported-text-language` errors.  Supported languages include English, German, French, Italian, Portuguese, Russian, Spanish and Swedish. Supported language codes are the ISO-639-1, ISO-639-2, ISO-639-3, and the plain english name of the language (for example \"russian\").</value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public LanguageEnum? Language { get; set; }
        /// <summary>
        /// A comma-separated list of analyses that will be applied when using the `alchemy_language` enrichment. See the service documentation for details on each extract option.  Possible values include:    * entity   * keyword   * taxonomy   * concept   * relation   * doc-sentiment   * doc-emotion   * typed-rels.
        /// </summary>
        /// <value>A comma-separated list of analyses that will be applied when using the `alchemy_language` enrichment. See the service documentation for details on each extract option.  Possible values include:    * entity   * keyword   * taxonomy   * concept   * relation   * doc-sentiment   * doc-emotion   * typed-rels.</value>
        [JsonProperty("extract", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Extract { get; set; }
        /// <summary>
        /// Gets or Sets Sentiment
        /// </summary>
        [JsonProperty("sentiment", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Sentiment { get; set; }
        /// <summary>
        /// Gets or Sets Quotations
        /// </summary>
        [JsonProperty("quotations", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Quotations { get; set; }
        /// <summary>
        /// Gets or Sets ShowSourceText
        /// </summary>
        [JsonProperty("showSourceText", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowSourceText { get; set; }
        /// <summary>
        /// Gets or Sets HierarchicalTypedRelations
        /// </summary>
        [JsonProperty("hierarchicalTypedRelations", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HierarchicalTypedRelations { get; set; }
        /// <summary>
        /// Required when using the `typed-rel` extract option. Should be set to the ID of a previously published custom Watson Knowledge Studio model.
        /// </summary>
        /// <value>Required when using the `typed-rel` extract option. Should be set to the ID of a previously published custom Watson Knowledge Studio model.</value>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string _Model { get; set; }
    }

}
