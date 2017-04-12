

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

    public partial class DocumentAccepted
    {
        /// <summary>
        /// Initializes a new instance of the DocumentAccepted class.
        /// </summary>
        public DocumentAccepted() { }

        /// <summary>
        /// Initializes a new instance of the DocumentAccepted class.
        /// </summary>
        /// <param name="status">Possible values include: 'processing'</param>
        public DocumentAccepted(string documentId = default(string), string status = default(string))
        {
            DocumentId = documentId;
            Status = status;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'processing'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

    }
}
