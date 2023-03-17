/**
* (C) Copyright IBM Corp. 2018, 2023.
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

namespace IBM.Watson.Assistant.v1.Model
{
    /// <summary>
    /// An array of dialog nodes.
    /// </summary>
    public class DialogNodeCollection
    {
        /// <summary>
        /// An array of objects describing the dialog nodes defined for the workspace.
        /// </summary>
        [JsonProperty("dialog_nodes", NullValueHandling = NullValueHandling.Ignore)]
        public List<DialogNode> DialogNodes { get; set; }
        /// <summary>
        /// The pagination data for the returned objects. For more information about using pagination, see
        /// [Pagination](#pagination).
        /// </summary>
        [JsonProperty("pagination", NullValueHandling = NullValueHandling.Ignore)]
        public Pagination Pagination { get; set; }
    }

}
