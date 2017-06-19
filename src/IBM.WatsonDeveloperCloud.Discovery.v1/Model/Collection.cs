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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// A collection for storing documents.
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// The status of the collection.
        /// </summary>
        /// <value>The status of the collection.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum ACTIVE for active
            /// </summary>
            [EnumMember(Value = "active")]
            ACTIVE,
            
            /// <summary>
            /// Enum PENDING for pending
            /// </summary>
            [EnumMember(Value = "pending")]
            PENDING
        }

        /// <summary>
        /// The status of the collection.
        /// </summary>
        /// <value>The status of the collection.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// The unique identifier of the collection.
        /// </summary>
        /// <value>The unique identifier of the collection.</value>
        [JsonProperty("collection_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CollectionId { get; private set; }
        /// <summary>
        /// The name of the collection.
        /// </summary>
        /// <value>The name of the collection.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of the collection.
        /// </summary>
        /// <value>The description of the collection.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The creation date of the collection in the format yyyy-MM-dd'T'HH:mmcon:ss.SSS'Z'.
        /// </summary>
        /// <value>The creation date of the collection in the format yyyy-MM-dd'T'HH:mmcon:ss.SSS'Z'.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; private set; }
        /// <summary>
        /// The timestamp of when the collection was last updated in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.
        /// </summary>
        /// <value>The timestamp of when the collection was last updated in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Updated { get; private set; }
        /// <summary>
        /// The unique identifier of the collection's configuration.
        /// </summary>
        /// <value>The unique identifier of the collection's configuration.</value>
        [JsonProperty("configuration_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfigurationId { get; set; }
        /// <summary>
        /// The language of the documents stored in the collection. The value should be in the form of an ISO 639-1 language code.
        /// </summary>
        /// <value>The language of the documents stored in the collection. The value should be in the form of an ISO 639-1 language code.</value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// The object providing information about the documents in the collection. Present only when retrieving details of a collection.
        /// </summary>
        /// <value>The object providing information about the documents in the collection. Present only when retrieving details of a collection.</value>
        [JsonProperty("document_counts", NullValueHandling = NullValueHandling.Ignore)]
        public DocumentCounts DocumentCounts { get; set; }
    }

}
