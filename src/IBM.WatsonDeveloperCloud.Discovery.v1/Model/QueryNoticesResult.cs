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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// QueryNoticesResult.
    /// </summary>
    public class QueryNoticesResult
    {
        /// <summary>
        /// The unique identifier of the document.
        /// </summary>
        /// <value>The unique identifier of the document.</value>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Id { get; set; }
        /// <summary>
        /// *Deprecated* This field is now part of the `result_metadata` object.
        /// </summary>
        /// <value>*Deprecated* This field is now part of the `result_metadata` object.</value>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Score { get; set; }
        /// <summary>
        /// Metadata of the document.
        /// </summary>
        /// <value>Metadata of the document.</value>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Metadata { get; set; }
        /// <summary>
        /// The collection ID of the collection containing the document for this result.
        /// </summary>
        /// <value>The collection ID of the collection containing the document for this result.</value>
        [JsonProperty("collection_id", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic CollectionId { get; set; }
        /// <summary>
        /// Gets or Sets ResultMetadata
        /// </summary>
        [JsonProperty("result_metadata", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic ResultMetadata { get; set; }
    }

}
