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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// QueryNoticesResult.
    /// </summary>
    public class QueryNoticesResult : BaseModel
    {
        /// <summary>
        /// The type of the original source file.
        /// </summary>
        /// <value>The type of the original source file.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum FileTypeEnum
        {
            
            /// <summary>
            /// Enum PDF for pdf
            /// </summary>
            [EnumMember(Value = "pdf")]
            PDF,
            
            /// <summary>
            /// Enum HTML for html
            /// </summary>
            [EnumMember(Value = "html")]
            HTML,
            
            /// <summary>
            /// Enum WORD for word
            /// </summary>
            [EnumMember(Value = "word")]
            WORD,
            
            /// <summary>
            /// Enum JSON for json
            /// </summary>
            [EnumMember(Value = "json")]
            JSON
        }

        /// <summary>
        /// The type of the original source file.
        /// </summary>
        /// <value>The type of the original source file.</value>
        [JsonProperty("file_type", NullValueHandling = NullValueHandling.Ignore)]
        public FileTypeEnum? FileType { get; set; }
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
        /// Metadata of the query result.
        /// </summary>
        /// <value>Metadata of the query result.</value>
        [JsonProperty("result_metadata", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic ResultMetadata { get; set; }
        /// <summary>
        /// The internal status code returned by the ingestion subsystem indicating the overall result of ingesting the source document.
        /// </summary>
        /// <value>The internal status code returned by the ingestion subsystem indicating the overall result of ingesting the source document.</value>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Code { get; set; }
        /// <summary>
        /// Name of the original source file (if available).
        /// </summary>
        /// <value>Name of the original source file (if available).</value>
        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Filename { get; set; }
        /// <summary>
        /// The SHA-1 hash of the original source file (formatted as a hexadecimal string).
        /// </summary>
        /// <value>The SHA-1 hash of the original source file (formatted as a hexadecimal string).</value>
        [JsonProperty("sha1", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Sha1 { get; set; }
        /// <summary>
        /// Array of notices for the document.
        /// </summary>
        /// <value>Array of notices for the document.</value>
        [JsonProperty("notices", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Notices { get; set; }
    }

}
