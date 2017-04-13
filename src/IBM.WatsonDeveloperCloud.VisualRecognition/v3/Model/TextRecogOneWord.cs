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

using System.Collections.Generic;
using Newtonsoft.Json;
namespace IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model
{
    /// <summary>
    /// TextRecogOneWord
    /// </summary>
    public class TextRecogOneWord
    {
        /// <summary>
        /// Gets or Sets Word
        /// </summary>
        [JsonProperty("word")]
        public string Word { get; set; }
        /// <summary>
        /// Gets or Sets Location
        /// </summary>
        [JsonProperty("location")]
        public TextRecogOneWordLocation Location { get; set; }
        /// <summary>
        /// Confidence score, on a scale of 0.0 to 1.0.
        /// </summary>
        [JsonProperty("score")]
        public float Score { get; set; }
        /// <summary>
        /// Line number the word is on.
        /// </summary>
        /// <value>Line number the word is on.</value>
        [JsonProperty("line_number")]
        public decimal? LineNumber { get; set; }
    }
}
