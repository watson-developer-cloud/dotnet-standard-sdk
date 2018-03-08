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

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model
{
    /// <summary>
    /// Warning.
    /// </summary>
    public class Warning
    {
        /// <summary>
        /// The identifier of the warning message.
        /// </summary>
        /// <value>The identifier of the warning message.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum WarningIdEnum
        {
            
            /// <summary>
            /// Enum WORD_COUNT_MESSAGE for WORD_COUNT_MESSAGE
            /// </summary>
            [EnumMember(Value = "WORD_COUNT_MESSAGE")]
            WORD_COUNT_MESSAGE,
            
            /// <summary>
            /// Enum JSON_AS_TEXT for JSON_AS_TEXT
            /// </summary>
            [EnumMember(Value = "JSON_AS_TEXT")]
            JSON_AS_TEXT,
            
            /// <summary>
            /// Enum CONTENT_TRUNCATED for CONTENT_TRUNCATED
            /// </summary>
            [EnumMember(Value = "CONTENT_TRUNCATED")]
            CONTENT_TRUNCATED,
            
            /// <summary>
            /// Enum PARTIAL_TEXT_USED for PARTIAL_TEXT_USED
            /// </summary>
            [EnumMember(Value = "PARTIAL_TEXT_USED")]
            PARTIAL_TEXT_USED
        }

        /// <summary>
        /// The identifier of the warning message.
        /// </summary>
        /// <value>The identifier of the warning message.</value>
        [JsonProperty("warning_id", NullValueHandling = NullValueHandling.Ignore)]
        public WarningIdEnum? WarningId { get; set; }
        /// <summary>
        /// The message associated with the `warning_id`: * `WORD_COUNT_MESSAGE`: "There were {number} words in the input. We need a minimum of 600, preferably 1,200 or more, to compute statistically significant estimates." * `JSON_AS_TEXT`: "Request input was processed as text/plain as indicated, however detected a JSON input. Did you mean application/json?" * `CONTENT_TRUNCATED`: "For maximum accuracy while also optimizing processing time, only the first 250KB of input text (excluding markup) was analyzed. Accuracy levels off at approximately 3,000 words so this did not affect the accuracy of the profile." * `PARTIAL_TEXT_USED`, "The text provided to compute the profile was trimmed for performance reasons. This action does not affect the accuracy of the output, as not all of the input text was required." Applies only when Arabic input text exceeds a threshold at which additional words do not contribute to the accuracy of the profile.
        /// </summary>
        /// <value>The message associated with the `warning_id`: * `WORD_COUNT_MESSAGE`: "There were {number} words in the input. We need a minimum of 600, preferably 1,200 or more, to compute statistically significant estimates." * `JSON_AS_TEXT`: "Request input was processed as text/plain as indicated, however detected a JSON input. Did you mean application/json?" * `CONTENT_TRUNCATED`: "For maximum accuracy while also optimizing processing time, only the first 250KB of input text (excluding markup) was analyzed. Accuracy levels off at approximately 3,000 words so this did not affect the accuracy of the profile." * `PARTIAL_TEXT_USED`, "The text provided to compute the profile was trimmed for performance reasons. This action does not affect the accuracy of the output, as not all of the input text was required." Applies only when Arabic input text exceeds a threshold at which additional words do not contribute to the accuracy of the profile.</value>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

}
