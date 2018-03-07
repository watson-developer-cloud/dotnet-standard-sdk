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

namespace IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model
{
    /// <summary>
    /// Translation.
    /// </summary>
    public class Translation
    {
        /// <summary>
        /// **Japanese only.** The part of speech for the word. The service uses the value to produce the correct intonation for the word. You can create only a single entry, with or without a single part of speech, for any word; you cannot create multiple entries with different parts of speech for the same word. For more information, see [Working with Japanese entries](https://console.bluemix.net/docs/services/text-to-speech/custom-rules.html#jaNotes).
        /// </summary>
        /// <value>**Japanese only.** The part of speech for the word. The service uses the value to produce the correct intonation for the word. You can create only a single entry, with or without a single part of speech, for any word; you cannot create multiple entries with different parts of speech for the same word. For more information, see [Working with Japanese entries](https://console.bluemix.net/docs/services/text-to-speech/custom-rules.html#jaNotes).</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PartOfSpeechEnum
        {
            
            /// <summary>
            /// Enum JOSI for Josi
            /// </summary>
            [EnumMember(Value = "Josi")]
            JOSI,
            
            /// <summary>
            /// Enum MESI for Mesi
            /// </summary>
            [EnumMember(Value = "Mesi")]
            MESI,
            
            /// <summary>
            /// Enum KIGO for Kigo
            /// </summary>
            [EnumMember(Value = "Kigo")]
            KIGO,
            
            /// <summary>
            /// Enum GOBI for Gobi
            /// </summary>
            [EnumMember(Value = "Gobi")]
            GOBI,
            
            /// <summary>
            /// Enum DOSI for Dosi
            /// </summary>
            [EnumMember(Value = "Dosi")]
            DOSI,
            
            /// <summary>
            /// Enum JODO for Jodo
            /// </summary>
            [EnumMember(Value = "Jodo")]
            JODO,
            
            /// <summary>
            /// Enum KOYU for Koyu
            /// </summary>
            [EnumMember(Value = "Koyu")]
            KOYU,
            
            /// <summary>
            /// Enum STBI for Stbi
            /// </summary>
            [EnumMember(Value = "Stbi")]
            STBI,
            
            /// <summary>
            /// Enum SUJI for Suji
            /// </summary>
            [EnumMember(Value = "Suji")]
            SUJI,
            
            /// <summary>
            /// Enum KEDO for Kedo
            /// </summary>
            [EnumMember(Value = "Kedo")]
            KEDO,
            
            /// <summary>
            /// Enum FUKU for Fuku
            /// </summary>
            [EnumMember(Value = "Fuku")]
            FUKU,
            
            /// <summary>
            /// Enum KEYO for Keyo
            /// </summary>
            [EnumMember(Value = "Keyo")]
            KEYO,
            
            /// <summary>
            /// Enum STTO for Stto
            /// </summary>
            [EnumMember(Value = "Stto")]
            STTO,
            
            /// <summary>
            /// Enum RETA for Reta
            /// </summary>
            [EnumMember(Value = "Reta")]
            RETA,
            
            /// <summary>
            /// Enum STZO for Stzo
            /// </summary>
            [EnumMember(Value = "Stzo")]
            STZO,
            
            /// <summary>
            /// Enum KATO for Kato
            /// </summary>
            [EnumMember(Value = "Kato")]
            KATO,
            
            /// <summary>
            /// Enum HOKA for Hoka
            /// </summary>
            [EnumMember(Value = "Hoka")]
            HOKA
        }

        /// <summary>
        /// **Japanese only.** The part of speech for the word. The service uses the value to produce the correct intonation for the word. You can create only a single entry, with or without a single part of speech, for any word; you cannot create multiple entries with different parts of speech for the same word. For more information, see [Working with Japanese entries](https://console.bluemix.net/docs/services/text-to-speech/custom-rules.html#jaNotes).
        /// </summary>
        /// <value>**Japanese only.** The part of speech for the word. The service uses the value to produce the correct intonation for the word. You can create only a single entry, with or without a single part of speech, for any word; you cannot create multiple entries with different parts of speech for the same word. For more information, see [Working with Japanese entries](https://console.bluemix.net/docs/services/text-to-speech/custom-rules.html#jaNotes).</value>
        [JsonProperty("part_of_speech", NullValueHandling = NullValueHandling.Ignore)]
        public PartOfSpeechEnum? PartOfSpeech { get; set; }
        /// <summary>
        /// The phonetic or sounds-like translation for the word. A phonetic translation is based on the SSML format for representing the phonetic string of a word either as an IPA translation or as an IBM SPR translation. A sounds-like is one or more words that, when combined, sound like the word.
        /// </summary>
        /// <value>The phonetic or sounds-like translation for the word. A phonetic translation is based on the SSML format for representing the phonetic string of a word either as an IPA translation or as an IBM SPR translation. A sounds-like is one or more words that, when combined, sound like the word.</value>
        [JsonProperty("translation", NullValueHandling = NullValueHandling.Ignore)]
        public string Translation { get; set; }
    }

}
