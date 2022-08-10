/**
* (C) Copyright IBM Corp. 2022.
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

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// An object containing properties that indicate how many intents, entities, and dialog nodes are defined in the
    /// workspace. This property is included only in responses from the **Export workspace asynchronously** method, and
    /// only when the **verbose** query parameter is set to `true`.
    /// </summary>
    public class WorkspaceCounts
    {
        /// <summary>
        /// The number of intents defined in the workspace.
        /// </summary>
        [JsonProperty("intent", NullValueHandling = NullValueHandling.Ignore)]
        public long? Intent { get; set; }
        /// <summary>
        /// The number of entities defined in the workspace.
        /// </summary>
        [JsonProperty("entity", NullValueHandling = NullValueHandling.Ignore)]
        public long? Entity { get; set; }
        /// <summary>
        /// The number of nodes defined in the workspace.
        /// </summary>
        [JsonProperty("node", NullValueHandling = NullValueHandling.Ignore)]
        public long? Node { get; set; }
    }

}
