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

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// Information about the child documents that are generated from a single document during ingestion or other
    /// processing.
    /// </summary>
    public class DocumentDetailsChildren
    {
        /// <summary>
        /// Indicates whether the child documents have any notices. The value is `false` if the document does not have
        /// child documents.
        /// </summary>
        [JsonProperty("have_notices", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HaveNotices { get; set; }
        /// <summary>
        /// Number of child documents. The value is `0` when processing of the document doesn't generate any child
        /// documents.
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }
    }

}
