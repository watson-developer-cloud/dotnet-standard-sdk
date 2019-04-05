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
using System.Runtime.Serialization;
using IBM.Cloud.SDK.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.Model
{
    /// <summary>
    /// TokenResult.
    /// </summary>
    public class TokenResult : BaseModel
    {
        /// <summary>
        /// The part of speech of the token. For descriptions of the values, see [Universal Dependencies POS
        /// tags](https://universaldependencies.org/u/pos/).
        /// </summary>
        /// <value>
        /// The part of speech of the token. For descriptions of the values, see [Universal Dependencies POS
        /// tags](https://universaldependencies.org/u/pos/).
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PartOfSpeechEnum
        {
            
            /// <summary>
            /// Enum ADJ for ADJ
            /// </summary>
            [EnumMember(Value = "ADJ")]
            ADJ,
            
            /// <summary>
            /// Enum ADP for ADP
            /// </summary>
            [EnumMember(Value = "ADP")]
            ADP,
            
            /// <summary>
            /// Enum ADV for ADV
            /// </summary>
            [EnumMember(Value = "ADV")]
            ADV,
            
            /// <summary>
            /// Enum AUX for AUX
            /// </summary>
            [EnumMember(Value = "AUX")]
            AUX,
            
            /// <summary>
            /// Enum CCONJ for CCONJ
            /// </summary>
            [EnumMember(Value = "CCONJ")]
            CCONJ,
            
            /// <summary>
            /// Enum DET for DET
            /// </summary>
            [EnumMember(Value = "DET")]
            DET,
            
            /// <summary>
            /// Enum INTJ for INTJ
            /// </summary>
            [EnumMember(Value = "INTJ")]
            INTJ,
            
            /// <summary>
            /// Enum NOUN for NOUN
            /// </summary>
            [EnumMember(Value = "NOUN")]
            NOUN,
            
            /// <summary>
            /// Enum NUM for NUM
            /// </summary>
            [EnumMember(Value = "NUM")]
            NUM,
            
            /// <summary>
            /// Enum PART for PART
            /// </summary>
            [EnumMember(Value = "PART")]
            PART,
            
            /// <summary>
            /// Enum PRON for PRON
            /// </summary>
            [EnumMember(Value = "PRON")]
            PRON,
            
            /// <summary>
            /// Enum PROPN for PROPN
            /// </summary>
            [EnumMember(Value = "PROPN")]
            PROPN,
            
            /// <summary>
            /// Enum PUNCT for PUNCT
            /// </summary>
            [EnumMember(Value = "PUNCT")]
            PUNCT,
            
            /// <summary>
            /// Enum SCONJ for SCONJ
            /// </summary>
            [EnumMember(Value = "SCONJ")]
            SCONJ,
            
            /// <summary>
            /// Enum SYM for SYM
            /// </summary>
            [EnumMember(Value = "SYM")]
            SYM,
            
            /// <summary>
            /// Enum VERB for VERB
            /// </summary>
            [EnumMember(Value = "VERB")]
            VERB,
            
            /// <summary>
            /// Enum X for X
            /// </summary>
            [EnumMember(Value = "X")]
            X
        }

        /// <summary>
        /// The part of speech of the token. For descriptions of the values, see [Universal Dependencies POS
        /// tags](https://universaldependencies.org/u/pos/).
        /// </summary>
        [JsonProperty("part_of_speech", NullValueHandling = NullValueHandling.Ignore)]
        public PartOfSpeechEnum? PartOfSpeech { get; set; }
        /// <summary>
        /// The token as it appears in the analyzed text.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// Character offsets indicating the beginning and end of the token in the analyzed text.
        /// </summary>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public List<long?> Location { get; set; }
        /// <summary>
        /// The [lemma](https://wikipedia.org/wiki/Lemma_%28morphology%29) of the token.
        /// </summary>
        [JsonProperty("lemma", NullValueHandling = NullValueHandling.Ignore)]
        public string Lemma { get; set; }
    }

}
