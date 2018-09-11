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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// CreateAcousticModel.
    /// </summary>
    public class CreateAcousticModel : BaseModel
    {
        /// <summary>
        /// The name of the base language model that is to be customized by the new custom acoustic model. The new
        /// custom model can be used only with the base model that it customizes. To determine whether a base model
        /// supports acoustic model customization, refer to [Language support for
        /// customization](https://console.bluemix.net/docs/services/speech-to-text/custom.html#languageSupport).
        /// </summary>
        /// <value>
        /// The name of the base language model that is to be customized by the new custom acoustic model. The new
        /// custom model can be used only with the base model that it customizes. To determine whether a base model
        /// supports acoustic model customization, refer to [Language support for
        /// customization](https://console.bluemix.net/docs/services/speech-to-text/custom.html#languageSupport).
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum BaseModelNameEnum
        {
            
            /// <summary>
            /// Enum AR_AR_BROADBANDMODEL for ar-AR_BroadbandModel
            /// </summary>
            [EnumMember(Value = "ar-AR_BroadbandModel")]
            AR_AR_BROADBANDMODEL,
            
            /// <summary>
            /// Enum DE_DE_BROADBANDMODEL for de-DE_BroadbandModel
            /// </summary>
            [EnumMember(Value = "de-DE_BroadbandModel")]
            DE_DE_BROADBANDMODEL,
            
            /// <summary>
            /// Enum EN_GB_BROADBANDMODEL for en-GB_BroadbandModel
            /// </summary>
            [EnumMember(Value = "en-GB_BroadbandModel")]
            EN_GB_BROADBANDMODEL,
            
            /// <summary>
            /// Enum EN_GB_NARROWBANDMODEL for en-GB_NarrowbandModel
            /// </summary>
            [EnumMember(Value = "en-GB_NarrowbandModel")]
            EN_GB_NARROWBANDMODEL,
            
            /// <summary>
            /// Enum EN_US_BROADBANDMODEL for en-US_BroadbandModel
            /// </summary>
            [EnumMember(Value = "en-US_BroadbandModel")]
            EN_US_BROADBANDMODEL,
            
            /// <summary>
            /// Enum EN_US_NARROWBANDMODEL for en-US_NarrowbandModel
            /// </summary>
            [EnumMember(Value = "en-US_NarrowbandModel")]
            EN_US_NARROWBANDMODEL,
            
            /// <summary>
            /// Enum ES_ES_BROADBANDMODEL for es-ES_BroadbandModel
            /// </summary>
            [EnumMember(Value = "es-ES_BroadbandModel")]
            ES_ES_BROADBANDMODEL,
            
            /// <summary>
            /// Enum ES_ES_NARROWBANDMODEL for es-ES_NarrowbandModel
            /// </summary>
            [EnumMember(Value = "es-ES_NarrowbandModel")]
            ES_ES_NARROWBANDMODEL,
            
            /// <summary>
            /// Enum FR_FR_BROADBANDMODEL for fr-FR_BroadbandModel
            /// </summary>
            [EnumMember(Value = "fr-FR_BroadbandModel")]
            FR_FR_BROADBANDMODEL,
            
            /// <summary>
            /// Enum JA_JP_BROADBANDMODEL for ja-JP_BroadbandModel
            /// </summary>
            [EnumMember(Value = "ja-JP_BroadbandModel")]
            JA_JP_BROADBANDMODEL,
            
            /// <summary>
            /// Enum JA_JP_NARROWBANDMODEL for ja-JP_NarrowbandModel
            /// </summary>
            [EnumMember(Value = "ja-JP_NarrowbandModel")]
            JA_JP_NARROWBANDMODEL,
            
            /// <summary>
            /// Enum KO_KR_BROADBANDMODEL for ko-KR_BroadbandModel
            /// </summary>
            [EnumMember(Value = "ko-KR_BroadbandModel")]
            KO_KR_BROADBANDMODEL,
            
            /// <summary>
            /// Enum KO_KR_NARROWBANDMODEL for ko-KR_NarrowbandModel
            /// </summary>
            [EnumMember(Value = "ko-KR_NarrowbandModel")]
            KO_KR_NARROWBANDMODEL,
            
            /// <summary>
            /// Enum PT_BR_BROADBANDMODEL for pt-BR_BroadbandModel
            /// </summary>
            [EnumMember(Value = "pt-BR_BroadbandModel")]
            PT_BR_BROADBANDMODEL,
            
            /// <summary>
            /// Enum PT_BR_NARROWBANDMODEL for pt-BR_NarrowbandModel
            /// </summary>
            [EnumMember(Value = "pt-BR_NarrowbandModel")]
            PT_BR_NARROWBANDMODEL,
            
            /// <summary>
            /// Enum ZH_CN_BROADBANDMODEL for zh-CN_BroadbandModel
            /// </summary>
            [EnumMember(Value = "zh-CN_BroadbandModel")]
            ZH_CN_BROADBANDMODEL,
            
            /// <summary>
            /// Enum ZH_CN_NARROWBANDMODEL for zh-CN_NarrowbandModel
            /// </summary>
            [EnumMember(Value = "zh-CN_NarrowbandModel")]
            ZH_CN_NARROWBANDMODEL
        }

        /// <summary>
        /// The name of the base language model that is to be customized by the new custom acoustic model. The new
        /// custom model can be used only with the base model that it customizes. To determine whether a base model
        /// supports acoustic model customization, refer to [Language support for
        /// customization](https://console.bluemix.net/docs/services/speech-to-text/custom.html#languageSupport).
        /// </summary>
        [JsonProperty("base_model_name", NullValueHandling = NullValueHandling.Ignore)]
        public BaseModelNameEnum? BaseModelName { get; set; }
        /// <summary>
        /// A user-defined name for the new custom acoustic model. Use a name that is unique among all custom acoustic
        /// models that you own. Use a localized name that matches the language of the custom model. Use a name that
        /// describes the acoustic environment of the custom model, such as `Mobile custom model` or `Noisy car custom
        /// model`.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// A description of the new custom acoustic model. Use a localized description that matches the language of the
        /// custom model.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

}
