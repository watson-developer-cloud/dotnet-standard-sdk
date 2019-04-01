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

using IBM.Cloud.SDK.Core;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.Watson.TextToSpeech.v1.Model
{
    /// <summary>
    /// CreateVoiceModel.
    /// </summary>
    public class CreateVoiceModel : BaseModel
    {
        /// <summary>
        /// The language of the new custom voice model. Omit the parameter to use the the default language, `en-US`.
        /// </summary>
        /// <value>
        /// The language of the new custom voice model. Omit the parameter to use the the default language, `en-US`.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum LanguageEnum
        {
            
            /// <summary>
            /// Enum DE_DE for de-DE
            /// </summary>
            [EnumMember(Value = "de-DE")]
            DE_DE,
            
            /// <summary>
            /// Enum EN_US for en-US
            /// </summary>
            [EnumMember(Value = "en-US")]
            EN_US,
            
            /// <summary>
            /// Enum EN_GB for en-GB
            /// </summary>
            [EnumMember(Value = "en-GB")]
            EN_GB,
            
            /// <summary>
            /// Enum ES_ES for es-ES
            /// </summary>
            [EnumMember(Value = "es-ES")]
            ES_ES,
            
            /// <summary>
            /// Enum ES_LA for es-LA
            /// </summary>
            [EnumMember(Value = "es-LA")]
            ES_LA,
            
            /// <summary>
            /// Enum ES_US for es-US
            /// </summary>
            [EnumMember(Value = "es-US")]
            ES_US,
            
            /// <summary>
            /// Enum FR_FR for fr-FR
            /// </summary>
            [EnumMember(Value = "fr-FR")]
            FR_FR,
            
            /// <summary>
            /// Enum IT_IT for it-IT
            /// </summary>
            [EnumMember(Value = "it-IT")]
            IT_IT,
            
            /// <summary>
            /// Enum JA_JP for ja-JP
            /// </summary>
            [EnumMember(Value = "ja-JP")]
            JA_JP,
            
            /// <summary>
            /// Enum PT_BR for pt-BR
            /// </summary>
            [EnumMember(Value = "pt-BR")]
            PT_BR
        }

        /// <summary>
        /// The language of the new custom voice model. Omit the parameter to use the the default language, `en-US`.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public LanguageEnum? Language { get; set; }
        /// <summary>
        /// The name of the new custom voice model.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// A description of the new custom voice model. Specifying a description is recommended.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

}
