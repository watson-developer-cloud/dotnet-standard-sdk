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
    public partial class Word
    {
        /// <summary>
        /// Gets or sets word from the custom voice model.
        /// </summary>
        [JsonProperty(PropertyName = "word")]
        public string WordProperty { get; set; }

        /// <summary>
        /// Gets or sets phonetic or sounds-like translation for the word. A
        /// phonetic translation is based on the SSML format for representing
        /// the phonetic string of a word either as an IPA or IBM SPR
        /// translation. A sounds-like translation consists of one or more
        /// words that, when combined, sound like the word.
        /// </summary>
        [JsonProperty(PropertyName = "translation")]
        public string Translation { get; set; }

        /// <summary>
        /// Gets or sets **Japanese only.** The part of speech for the word.
        /// The service uses the value to produce the correct intonation for
        /// the word. You can create only a single entry, with or without a
        /// single part of speech, for any word; you cannot create multiple
        /// entries with different parts of speech for the same word. For
        /// more information, see [Working with Japanese
        /// entries](http://www.ibm.com/watson/developercloud/doc/text-to-speech/custom-using.shtml#jaNotes).
        /// Possible values include: 'Josi', 'Mesi', 'Kigo', 'Gobi', 'Dosi',
        /// 'Jodo', 'Koyu', 'Stbi', 'Suji', 'Kedo', 'Fuku', 'Keyo', 'Stto',
        /// 'Reta', 'Stzo', 'Kato', 'Hoka'
        /// </summary>
        [JsonProperty(PropertyName = "part_of_speech")]
        public string PartOfSpeech { get; set; }
    }
}
