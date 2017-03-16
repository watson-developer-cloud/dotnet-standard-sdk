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
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public partial class Word
    {
        /// <summary>
        /// Initializes a new instance of the Word class.
        /// </summary>
        public Word() { }

        /// <summary>
        /// Initializes a new instance of the Word class.
        /// </summary>
        /// <param name="wordProperty">The custom word that is to be added to
        /// the custom model. Do not include spaces in the word. Use a -
        /// (dash) or _ (underscore) to connect the tokens of compound
        /// words.</param>
        /// <param name="soundsLike">An array of sounds-like pronunciations
        /// for the custom word. Specify how words that are difficult to
        /// pronounce, foreign words, acronyms, and so on can be pronounced
        /// by users. For a word that is not in the service's base
        /// vocabulary, omit the parameter to have the service automatically
        /// generate a sounds-like pronunciation for the word. For a word
        /// that is in the service's base vocabulary, use the parameter to
        /// specify additional pronunciations for the word. You cannot
        /// override the default pronunciation of a word; pronunciations you
        /// add augment the pronunciation from the base vocabulary. A word
        /// can have at most five sounds-like pronunciations, and a
        /// pronunciation can include at most 40 characters not including
        /// spaces.</param>
        /// <param name="displayAs">An alternative spelling for the custom
        /// word when it appears in a transcript. Use the parameter when you
        /// want the word to have a spelling that is different from its usual
        /// representation or from its spelling in corpora training
        /// data.</param>
        public Word(string wordProperty, System.Collections.Generic.IList<string> soundsLike = default(System.Collections.Generic.IList<string>), string displayAs = default(string))
        {
            WordProperty = wordProperty;
            SoundsLike = soundsLike;
            DisplayAs = displayAs;
        }

        /// <summary>
        /// Gets or sets the custom word that is to be added to the custom
        /// model. Do not include spaces in the word. Use a - (dash) or _
        /// (underscore) to connect the tokens of compound words.
        /// </summary>
        [JsonProperty(PropertyName = "word")]
        public string WordProperty { get; set; }

        /// <summary>
        /// Gets or sets an array of sounds-like pronunciations for the custom
        /// word. Specify how words that are difficult to pronounce, foreign
        /// words, acronyms, and so on can be pronounced by users. For a word
        /// that is not in the service's base vocabulary, omit the parameter
        /// to have the service automatically generate a sounds-like
        /// pronunciation for the word. For a word that is in the service's
        /// base vocabulary, use the parameter to specify additional
        /// pronunciations for the word. You cannot override the default
        /// pronunciation of a word; pronunciations you add augment the
        /// pronunciation from the base vocabulary. A word can have at most
        /// five sounds-like pronunciations, and a pronunciation can include
        /// at most 40 characters not including spaces.
        /// </summary>
        [JsonProperty(PropertyName = "sounds_like")]
        public IList<string> SoundsLike { get; set; }

        /// <summary>
        /// Gets or sets an alternative spelling for the custom word when it
        /// appears in a transcript. Use the parameter when you want the word
        /// to have a spelling that is different from its usual
        /// representation or from its spelling in corpora training data.
        /// </summary>
        [JsonProperty(PropertyName = "display_as")]
        public string DisplayAs { get; set; }
    }
}
