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

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model
{
    /// <summary>
    /// Usage information.
    /// </summary>
    public class Usage
    {
        /// <summary>
        /// Number of features used in the API call.
        /// </summary>
        /// <value>Number of features used in the API call.</value>
        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public long? Features { get; set; }
        /// <summary>
        /// Number of text characters processed.
        /// </summary>
        /// <value>Number of text characters processed.</value>
        [JsonProperty("text_characters", NullValueHandling = NullValueHandling.Ignore)]
        public long? TextCharacters { get; set; }
        /// <summary>
        /// Number of 10,000-character units processed.
        /// </summary>
        /// <value>Number of 10,000-character units processed.</value>
        [JsonProperty("text_units", NullValueHandling = NullValueHandling.Ignore)]
        public long? TextUnits { get; set; }
    }

}
