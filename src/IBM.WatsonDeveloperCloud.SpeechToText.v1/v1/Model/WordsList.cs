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
    public partial class WordsList
    {
        /// <summary>
        /// Gets or sets information about each word in the custom model's
        /// words resource. The array is empty if the custom model has no
        /// words.
        /// </summary>
        [JsonProperty(PropertyName = "words")]
        public IList<WordData> Words { get; set; }
    }
}