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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageClassifier.v1.Model
{
    /// <summary>
    /// A classifier for natural language phrases.
    /// </summary>
    public class Classifier
    {
        /// <summary>
        /// The state of the classifier.
        /// </summary>
        /// <value>The state of the classifier.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum NON_EXISTENT for Non Existent
            /// </summary>
            [EnumMember(Value = "Non Existent")]
            NON_EXISTENT,
            
            /// <summary>
            /// Enum TRAINING for Training
            /// </summary>
            [EnumMember(Value = "Training")]
            TRAINING,
            
            /// <summary>
            /// Enum FAILED for Failed
            /// </summary>
            [EnumMember(Value = "Failed")]
            FAILED,
            
            /// <summary>
            /// Enum AVAILABLE for Available
            /// </summary>
            [EnumMember(Value = "Available")]
            AVAILABLE,
            
            /// <summary>
            /// Enum UNAVAILABLE for Unavailable
            /// </summary>
            [EnumMember(Value = "Unavailable")]
            UNAVAILABLE
        }

        /// <summary>
        /// The state of the classifier.
        /// </summary>
        /// <value>The state of the classifier.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// User-supplied name for the classifier.
        /// </summary>
        /// <value>User-supplied name for the classifier.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Link to the classifier.
        /// </summary>
        /// <value>Link to the classifier.</value>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        /// <summary>
        /// Unique identifier for this classifier.
        /// </summary>
        /// <value>Unique identifier for this classifier.</value>
        [JsonProperty("classifier_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ClassifierId { get; set; }
        /// <summary>
        /// Date and time (UTC) the classifier was created.
        /// </summary>
        /// <value>Date and time (UTC) the classifier was created.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Created { get; private set; }
        /// <summary>
        /// Additional detail about the status.
        /// </summary>
        /// <value>Additional detail about the status.</value>
        [JsonProperty("status_description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string StatusDescription { get; private set; }
        /// <summary>
        /// The language used for the classifier.
        /// </summary>
        /// <value>The language used for the classifier.</value>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
    }

}
