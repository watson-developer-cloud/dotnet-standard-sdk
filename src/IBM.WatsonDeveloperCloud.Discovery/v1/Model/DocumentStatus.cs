

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

    public partial class DocumentStatus
    {
        /// <summary>
        /// Initializes a new instance of the DocumentStatus class.
        /// </summary>
        public DocumentStatus() { }

        /// <summary>
        /// Initializes a new instance of the DocumentStatus class.
        /// </summary>
        /// <param name="status">Possible values include: 'available',
        /// 'available with notices', 'failed', 'processing'</param>
        public DocumentStatus(string documentId = default(string), string configurationId = default(string), Datetime created = default(Datetime), Datetime updated = default(Datetime), string status = default(string), string statusDescription = default(string),List<Notice> notices = default(System.Collections.Generic.IList<Notice>))
        {
            DocumentId = documentId;
            ConfigurationId = configurationId;
            Created = created;
            Updated = updated;
            Status = status;
            StatusDescription = statusDescription;
            Notices = notices;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "configuration_id")]
        public string ConfigurationId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public Datetime Created { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "updated")]
        public Datetime Updated { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'available', 'available with
        /// notices', 'failed', 'processing'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "status_description")]
        public string StatusDescription { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "notices")]
        public List<Notice> Notices { get; set; }

    }
}
