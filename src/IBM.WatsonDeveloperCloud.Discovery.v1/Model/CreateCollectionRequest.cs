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
    /// CreateCollectionRequest.
    /// </summary>
    public class CreateCollectionRequest
    {
        /// <summary>
        /// The language of the documents stored in the collection, in the form of an ISO 639-1 language code.
        /// </summary>
        /// <value>The language of the documents stored in the collection, in the form of an ISO 639-1 language code.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum LanguageEnum
        {
            
            /// <summary>
            /// Enum EN for en
            /// </summary>
            [EnumMember(Value = "en")]
            EN,
            
            /// <summary>
            /// Enum ES for es
            /// </summary>
            [EnumMember(Value = "es")]
            ES,
            
            /// <summary>
            /// Enum DE for de
            /// </summary>
            [EnumMember(Value = "de")]
            DE,
            
            /// <summary>
            /// Enum AR for ar
            /// </summary>
            [EnumMember(Value = "ar")]
            AR,
            
            /// <summary>
            /// Enum FR for fr
            /// </summary>
            [EnumMember(Value = "fr")]
            FR,
            
            /// <summary>
            /// Enum IT for it
            /// </summary>
            [EnumMember(Value = "it")]
            IT,
            
            /// <summary>
            /// Enum JA for ja
            /// </summary>
            [EnumMember(Value = "ja")]
            JA,
            
            /// <summary>
            /// Enum KO for ko
            /// </summary>
            [EnumMember(Value = "ko")]
            KO,
            
            /// <summary>
            /// Enum PT for pt
            /// </summary>
            [EnumMember(Value = "pt")]
            PT
        }

        /// <summary>
        /// The language of the documents stored in the collection, in the form of an ISO 639-1 language code.
        /// </summary>
        /// <value>The language of the documents stored in the collection, in the form of an ISO 639-1 language code.</value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public LanguageEnum? Language { get; set; }
        /// <summary>
        /// The name of the collection to be created.
        /// </summary>
        /// <value>The name of the collection to be created.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// A description of the collection.
        /// </summary>
        /// <value>A description of the collection.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The ID of the configuration in which the collection is to be created.
        /// </summary>
        /// <value>The ID of the configuration in which the collection is to be created.</value>
        [JsonProperty("configuration_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfigurationId { get; set; }
    }

}
