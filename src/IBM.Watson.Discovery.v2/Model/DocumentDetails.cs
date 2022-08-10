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

using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// Information about a document.
    /// </summary>
    public class DocumentDetails
    {
        /// <summary>
        /// The status of the ingestion of the document. The possible values are:
        ///
        /// * `available`: Ingestion is finished and the document is indexed.
        ///
        /// * `failed`: Ingestion is finished, but the document is not indexed because of an error.
        ///
        /// * `pending`: The document is uploaded, but the ingestion process is not started.
        ///
        /// * `processing`: Ingestion is in progress.
        /// </summary>
        public class StatusEnumValue
        {
            /// <summary>
            /// Constant AVAILABLE for available
            /// </summary>
            public const string AVAILABLE = "available";
            /// <summary>
            /// Constant FAILED for failed
            /// </summary>
            public const string FAILED = "failed";
            /// <summary>
            /// Constant PENDING for pending
            /// </summary>
            public const string PENDING = "pending";
            /// <summary>
            /// Constant PROCESSING for processing
            /// </summary>
            public const string PROCESSING = "processing";
            
        }

        /// <summary>
        /// The status of the ingestion of the document. The possible values are:
        ///
        /// * `available`: Ingestion is finished and the document is indexed.
        ///
        /// * `failed`: Ingestion is finished, but the document is not indexed because of an error.
        ///
        /// * `pending`: The document is uploaded, but the ingestion process is not started.
        ///
        /// * `processing`: Ingestion is in progress.
        /// Constants for possible values can be found using DocumentDetails.StatusEnumValue
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// The unique identifier of the document.
        /// </summary>
        [JsonProperty("document_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string DocumentId { get; private set; }
        /// <summary>
        /// Date and time that the document is added to the collection. For a child document, the date and time when the
        /// process that generates the child document runs. The date-time format is `yyyy-MM-dd'T'HH:mm:ss.SSS'Z'`.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Created { get; private set; }
        /// <summary>
        /// Date and time that the document is finished being processed and is indexed. This date changes whenever the
        /// document is reprocessed, including for enrichment changes. The date-time format is
        /// `yyyy-MM-dd'T'HH:mm:ss.SSS'Z'`.
        /// </summary>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Updated { get; private set; }
        /// <summary>
        /// Array of JSON objects for notices, meaning warning or error messages, that are produced by the document
        /// ingestion process. The array does not include notices that are produced for child documents that are
        /// generated when a document is processed.
        /// </summary>
        [JsonProperty("notices", NullValueHandling = NullValueHandling.Ignore)]
        public List<Notice> Notices { get; set; }
        /// <summary>
        /// Information about the child documents that are generated from a single document during ingestion or other
        /// processing.
        /// </summary>
        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public DocumentDetailsChildren Children { get; set; }
        /// <summary>
        /// Name of the original source file (if available).
        /// </summary>
        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        public string Filename { get; set; }
        /// <summary>
        /// The type of the original source file, such as `csv`, `excel`, `html`, `json`, `pdf`, `text`, `word`, and so
        /// on.
        /// </summary>
        [JsonProperty("file_type", NullValueHandling = NullValueHandling.Ignore)]
        public string FileType { get; set; }
        /// <summary>
        /// The SHA-256 hash of the original source file. The hash is formatted as a hexadecimal string.
        /// </summary>
        [JsonProperty("sha256", NullValueHandling = NullValueHandling.Ignore)]
        public string Sha256 { get; set; }
    }

}
