

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

    public partial class TestDocument
    {
        /// <summary>
        /// Initializes a new instance of the TestDocument class.
        /// </summary>
        public TestDocument() { }

        /// <summary>
        /// Initializes a new instance of the TestDocument class.
        /// </summary>
        /// <param name="enrichedFieldUnits">The number of 10 Kilobytes of
        /// field data that was enriched. This can be used to estimate the
        /// cost of running a real ingestion.</param>
        /// <param name="originalMediaType">Format of the test document</param>
        public TestDocument(string configurationId = default(string), string status = default(string), double? enrichedFieldUnits = default(double?), string originalMediaType = default(string),List<DocumentSnapshot> snapshots = default(System.Collections.Generic.IList<DocumentSnapshot>),List<Notice> notices = default(System.Collections.Generic.IList<Notice>))
        {
            ConfigurationId = configurationId;
            Status = status;
            EnrichedFieldUnits = enrichedFieldUnits;
            OriginalMediaType = originalMediaType;
            Snapshots = snapshots;
            Notices = notices;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "configuration_id")]
        public string ConfigurationId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the number of 10 Kilobytes of field data that was
        /// enriched. This can be used to estimate the cost of running a real
        /// ingestion.
        /// </summary>
        [JsonProperty(PropertyName = "enriched_field_units")]
        public double? EnrichedFieldUnits { get; set; }

        /// <summary>
        /// Gets or sets format of the test document
        /// </summary>
        [JsonProperty(PropertyName = "original_media_type")]
        public string OriginalMediaType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "snapshots")]
        public List<DocumentSnapshot> Snapshots { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "notices")]
        public List<Notice> Notices { get; set; }

    }
}
