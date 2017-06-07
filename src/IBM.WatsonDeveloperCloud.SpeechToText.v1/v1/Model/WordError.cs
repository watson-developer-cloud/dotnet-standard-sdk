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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public partial class WordError
    {
        /// <summary>
        /// Gets or sets a key-value pair that describes an error associated
        /// with the word's definition in the format `"element": "message"`,
        /// where `element` is the aspect of the definition that caused the
        /// problem and `message` describes the problem. The following
        /// example describes a problem with one of the word's sounds-like
        /// definitions: `"sounds_like_string": "Numbers are not allowed in
        /// sounds-like. You can try for example 'suggested_string'."` You
        /// must correct the error before you can train the model.
        /// </summary>
        [JsonProperty("element")]
        public string Element { get; set; }
    }
}
