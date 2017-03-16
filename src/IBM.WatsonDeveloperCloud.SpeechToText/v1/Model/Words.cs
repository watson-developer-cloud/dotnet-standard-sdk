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
    public partial class Words
    {
        /// <summary>
        /// Initializes a new instance of the Words class.
        /// </summary>
        public Words() { }

        /// <summary>
        /// Initializes a new instance of the Words class.
        /// </summary>
        /// <param name="wordsProperty">Information about each custom word
        /// that is to be added to the custom model.</param>
        public Words(IList<Word> wordsProperty)
        {
            WordsProperty = wordsProperty;
        }

        /// <summary>
        /// Gets or sets information about each custom word that is to be
        /// added to the custom model.
        /// </summary>
        [JsonProperty(PropertyName = "words")]
        public IList<Word> WordsProperty { get; set; }
    }
}