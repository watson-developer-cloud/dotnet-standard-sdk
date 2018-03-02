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
    /// QueryPassages.
    /// </summary>
    public class QueryPassages
    {
        /// <summary>
        /// The unique identifier of the document from which the passage has been extracted.
        /// </summary>
        /// <value>The unique identifier of the document from which the passage has been extracted.</value>
        [JsonProperty("document_id", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentId { get; set; }
        /// <summary>
        /// The confidence score of the passages's analysis. A higher score indicates greater confidence.
        /// </summary>
        /// <value>The confidence score of the passages's analysis. A higher score indicates greater confidence.</value>
        [JsonProperty("passage_score", NullValueHandling = NullValueHandling.Ignore)]
        public double? PassageScore { get; set; }
        /// <summary>
        /// The content of the extracted passage.
        /// </summary>
        /// <value>The content of the extracted passage.</value>
        [JsonProperty("passage_text", NullValueHandling = NullValueHandling.Ignore)]
        public string PassageText { get; set; }
        /// <summary>
        /// The position of the first character of the extracted passage in the originating field.
        /// </summary>
        /// <value>The position of the first character of the extracted passage in the originating field.</value>
        [JsonProperty("start_offset", NullValueHandling = NullValueHandling.Ignore)]
        public long? StartOffset { get; set; }
        /// <summary>
        /// The position of the last character of the extracted passage in the originating field.
        /// </summary>
        /// <value>The position of the last character of the extracted passage in the originating field.</value>
        [JsonProperty("end_offset", NullValueHandling = NullValueHandling.Ignore)]
        public long? EndOffset { get; set; }
        /// <summary>
        /// The label of the field from which the passage has been extracted.
        /// </summary>
        /// <value>The label of the field from which the passage has been extracted.</value>
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }
    }

}
