

/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 the "License";
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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    using System.Linq;

    /// <summary>
    /// A notice produced by the ingestion process.
    /// </summary>
    public partial class Notice
    {
        /// <summary>
        /// Initializes a new instance of the Notice class.
        /// </summary>
        public Notice() { }

        /// <summary>
        /// Initializes a new instance of the Notice class.
        /// </summary>
        /// <param name="noticeId">Identifies the notice. Many notices may
        /// have the same ID. This field exists so that user applications can
        /// programatically identify a notice and take automatic corrective
        /// action.</param>
        /// <param name="created">The creation date of the collection in the
        /// format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'</param>
        /// <param name="documentId">Unique identifier of the ingested
        /// document</param>
        /// <param name="severity">Severity level of the notice. Possible
        /// values include: 'warning', 'error'</param>
        /// <param name="step">Ingestion step in which the notice
        /// occurred</param>
        /// <param name="description">The description of the notice</param>
        public Notice(string noticeId = default(string), Datetime created = default(Datetime), string documentId = default(string), string severity = default(string), string step = default(string), string description = default(string))
        {
            NoticeId = noticeId;
            Created = created;
            DocumentId = documentId;
            Severity = severity;
            Step = step;
            Description = description;
        }

        /// <summary>
        /// Gets or sets identifies the notice. Many notices may have the same
        /// ID. This field exists so that user applications can
        /// programatically identify a notice and take automatic corrective
        /// action.
        /// </summary>
        [JsonProperty(PropertyName = "notice_id")]
        public string NoticeId { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the collection in the format
        /// yyyy-MM-dd'T'HH:mm:ss.SSS'Z'
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public Datetime Created { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the ingested document
        /// </summary>
        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// Gets or sets severity level of the notice. Possible values
        /// include: 'warning', 'error'
        /// </summary>
        [JsonProperty(PropertyName = "severity")]
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets ingestion step in which the notice occurred
        /// </summary>
        [JsonProperty(PropertyName = "step")]
        public string Step { get; set; }

        /// <summary>
        /// Gets or sets the description of the notice
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

    }
}
