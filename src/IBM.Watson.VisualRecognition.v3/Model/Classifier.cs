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

namespace IBM.Watson.VisualRecognition.v3.Model
{
    /// <summary>
    /// Information about a classifier.
    /// </summary>
    public class Classifier : BaseModel
    {
        /// <summary>
        /// Training status of classifier.
        /// </summary>
        /// <value>
        /// Training status of classifier.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum READY for ready
            /// </summary>
            [EnumMember(Value = "ready")]
            READY,
            
            /// <summary>
            /// Enum TRAINING for training
            /// </summary>
            [EnumMember(Value = "training")]
            TRAINING,
            
            /// <summary>
            /// Enum RETRAINING for retraining
            /// </summary>
            [EnumMember(Value = "retraining")]
            RETRAINING,
            
            /// <summary>
            /// Enum FAILED for failed
            /// </summary>
            [EnumMember(Value = "failed")]
            FAILED
        }

        /// <summary>
        /// Training status of classifier.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// ID of a classifier identified in the image.
        /// </summary>
        [JsonProperty("classifier_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ClassifierId { get; set; }
        /// <summary>
        /// Name of the classifier.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Unique ID of the account who owns the classifier. Might not be returned by some requests.
        /// </summary>
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }
        /// <summary>
        /// Whether the classifier can be downloaded as a Core ML model after the training status is `ready`.
        /// </summary>
        [JsonProperty("core_ml_enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CoreMlEnabled { get; set; }
        /// <summary>
        /// If classifier training has failed, this field might explain why.
        /// </summary>
        [JsonProperty("explanation", NullValueHandling = NullValueHandling.Ignore)]
        public string Explanation { get; set; }
        /// <summary>
        /// Date and time in Coordinated Universal Time (UTC) that the classifier was created.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }
        /// <summary>
        /// Classes that define a classifier.
        /// </summary>
        [JsonProperty("classes", NullValueHandling = NullValueHandling.Ignore)]
        public List<ModelClass> Classes { get; set; }
        /// <summary>
        /// Date and time in Coordinated Universal Time (UTC) that the classifier was updated. Might not be returned by
        /// some requests. Identical to `updated` and retained for backward compatibility.
        /// </summary>
        [JsonProperty("retrained", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Retrained { get; set; }
        /// <summary>
        /// Date and time in Coordinated Universal Time (UTC) that the classifier was most recently updated. The field
        /// matches either `retrained` or `created`. Might not be returned by some requests.
        /// </summary>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Updated { get; set; }
    }

}
