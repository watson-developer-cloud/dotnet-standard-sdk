﻿/**
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
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model
{
    public class TranslateResponse
    {
        /// <summary>
        /// List of translation output in UTF-8, corresponding to the list of input text.
        /// </summary>
        [JsonProperty("translations")]
        public List<Translations> Translations { get; set; }

        /// <summary>
        /// Number of words of the complete input text.
        /// </summary>
        [JsonProperty("word_count")]
        public int WordCount { get; set; }

        /// <summary>
        /// Number of characters of the complete input text.
        /// </summary>
        [JsonProperty("character_count")]
        public int CharacterCount { get; set; }
    }
}