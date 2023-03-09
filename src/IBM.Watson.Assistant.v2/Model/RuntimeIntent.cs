/**
* (C) Copyright IBM Corp. 2023.
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

namespace IBM.Watson.Assistant.v2.Model
{
    /// <summary>
    /// An intent identified in the user input.
    /// </summary>
    public class RuntimeIntent
    {
        /// <summary>
        /// The name of the recognized intent.
        /// </summary>
        [JsonProperty("intent", NullValueHandling = NullValueHandling.Ignore)]
        public string Intent { get; set; }
        /// <summary>
        /// A decimal percentage that represents Watson's confidence in the intent. If you are specifying an intent as
        /// part of a request, but you do not have a calculated confidence value, specify `1`.
        /// </summary>
        [JsonProperty("confidence", NullValueHandling = NullValueHandling.Ignore)]
        public double? Confidence { get; set; }
        /// <summary>
        /// The skill that identified the intent. Currently, the only possible values are `main skill` for the dialog
        /// skill (if enabled) and `actions skill` for the action skill.
        ///
        /// This property is present only if the assistant has both a dialog skill and an action skill.
        /// </summary>
        [JsonProperty("skill", NullValueHandling = NullValueHandling.Ignore)]
        public string Skill { get; set; }
    }

}
