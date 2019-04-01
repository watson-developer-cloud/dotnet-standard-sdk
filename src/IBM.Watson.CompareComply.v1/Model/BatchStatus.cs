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

using IBM.Cloud.SDK.Core;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.Watson.CompareComply.v1.Model
{
    /// <summary>
    /// The batch-request status.
    /// </summary>
    public class BatchStatus : BaseModel
    {
        /// <summary>
        /// The method to be run against the documents. Possible values are `html_conversion`, `element_classification`,
        /// and `tables`.
        /// </summary>
        /// <value>
        /// The method to be run against the documents. Possible values are `html_conversion`, `element_classification`,
        /// and `tables`.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum FunctionEnum
        {
            
            /// <summary>
            /// Enum ELEMENT_CLASSIFICATION for element_classification
            /// </summary>
            [EnumMember(Value = "element_classification")]
            ELEMENT_CLASSIFICATION,
            
            /// <summary>
            /// Enum HTML_CONVERSION for html_conversion
            /// </summary>
            [EnumMember(Value = "html_conversion")]
            HTML_CONVERSION,
            
            /// <summary>
            /// Enum TABLES for tables
            /// </summary>
            [EnumMember(Value = "tables")]
            TABLES
        }

        /// <summary>
        /// The method to be run against the documents. Possible values are `html_conversion`, `element_classification`,
        /// and `tables`.
        /// </summary>
        [JsonProperty("function", NullValueHandling = NullValueHandling.Ignore)]
        public FunctionEnum? Function { get; set; }
        /// <summary>
        /// The geographical location of the Cloud Object Storage input bucket as listed on the **Endpoint** tab of your
        /// COS instance; for example, `us-geo`, `eu-geo`, or `ap-geo`.
        /// </summary>
        [JsonProperty("input_bucket_location", NullValueHandling = NullValueHandling.Ignore)]
        public string InputBucketLocation { get; set; }
        /// <summary>
        /// The name of the Cloud Object Storage input bucket.
        /// </summary>
        [JsonProperty("input_bucket_name", NullValueHandling = NullValueHandling.Ignore)]
        public string InputBucketName { get; set; }
        /// <summary>
        /// The geographical location of the Cloud Object Storage output bucket as listed on the **Endpoint** tab of
        /// your COS instance; for example, `us-geo`, `eu-geo`, or `ap-geo`.
        /// </summary>
        [JsonProperty("output_bucket_location", NullValueHandling = NullValueHandling.Ignore)]
        public string OutputBucketLocation { get; set; }
        /// <summary>
        /// The name of the Cloud Object Storage output bucket.
        /// </summary>
        [JsonProperty("output_bucket_name", NullValueHandling = NullValueHandling.Ignore)]
        public string OutputBucketName { get; set; }
        /// <summary>
        /// The unique identifier for the batch request.
        /// </summary>
        [JsonProperty("batch_id", NullValueHandling = NullValueHandling.Ignore)]
        public string BatchId { get; set; }
        /// <summary>
        /// Document counts.
        /// </summary>
        [JsonProperty("document_counts", NullValueHandling = NullValueHandling.Ignore)]
        public DocCounts DocumentCounts { get; set; }
        /// <summary>
        /// The status of the batch request.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// The creation time of the batch request.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }
        /// <summary>
        /// The time of the most recent update to the batch request.
        /// </summary>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Updated { get; set; }
    }

}
