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
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    /// <summary>
    /// KeywordResults.
    /// </summary>
    public class KeywordResults
    {
        /// <summary>
        /// A list of each keyword entered via the `keywords` parameter and, for each keyword, an array of `KeywordResult` objects that provides information about its occurrences in the input audio. The keys of the list are the actual keyword strings. A keyword for which no matches are spotted in the input is omitted from the array.
        /// </summary>
        /// <value>A list of each keyword entered via the `keywords` parameter and, for each keyword, an array of `KeywordResult` objects that provides information about its occurrences in the input audio. The keys of the list are the actual keyword strings. A keyword for which no matches are spotted in the input is omitted from the array.</value>
        [JsonProperty("keyword", NullValueHandling = NullValueHandling.Ignore)]
        public List<KeywordResult> Keyword { get; set; }
    }

}
