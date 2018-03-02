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
using System;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Status information about a submitted document.
    /// </summary>
    public class DocumentStatus
    {
        /// <summary>
        /// Status of the document in the ingestion process.
        /// </summary>
        /// <value>Status of the document in the ingestion process.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum AVAILABLE for available
            /// </summary>
            [EnumMember(Value = "available")]
            AVAILABLE,
            
            /// <summary>
            /// Enum AVAILABLE_WITH_NOTICES for available with notices
            /// </summary>
            [EnumMember(Value = "available with notices")]
            AVAILABLE_WITH_NOTICES,
            
            /// <summary>
            /// Enum FAILED for failed
            /// </summary>
            [EnumMember(Value = "failed")]
            FAILED,
            
            /// <summary>
            /// Enum PROCESSING for processing
            /// </summary>
            [EnumMember(Value = "processing")]
            PROCESSING
        }

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
        /// Status of the document in the ingestion process.
        /// </summary>
        /// <value>Status of the document in the ingestion process.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
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
        [JsonProperty("document_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string DocumentId { get; private set; }
        /// <summary>
        /// The unique identifier for the configuration.
        /// </summary>
        /// <value>The unique identifier for the configuration.</value>
        [JsonProperty("configuration_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string ConfigurationId { get; private set; }
        /// <summary>
        /// The creation date of the document in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.
        /// </summary>
        /// <value>The creation date of the document in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Created { get; private set; }
        /// <summary>
        /// Date of the most recent document update, in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.
        /// </summary>
        /// <value>Date of the most recent document update, in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Updated { get; private set; }
        /// <summary>
        /// Description of the document status.
        /// </summary>
        /// <value>Description of the document status.</value>
        [JsonProperty("status_description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string StatusDescription { get; private set; }
        /// <summary>
        /// Name of the original source file (if available).
        /// </summary>
        /// <value>Name of the original source file (if available).</value>
        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        public string Filename { get; set; }
        /// <summary>
        /// The SHA-1 hash of the original source file (formatted as a hexadecimal string).
        /// </summary>
        /// <value>The SHA-1 hash of the original source file (formatted as a hexadecimal string).</value>
        [JsonProperty("sha1", NullValueHandling = NullValueHandling.Ignore)]
        public string Sha1 { get; set; }
        /// <summary>
        /// Array of notices produced by the document-ingestion process.
        /// </summary>
        /// <value>Array of notices produced by the document-ingestion process.</value>
        [JsonProperty("notices", NullValueHandling = NullValueHandling.Ignore)]
        public List<Notice> Notices { get; set; }
    }

}
