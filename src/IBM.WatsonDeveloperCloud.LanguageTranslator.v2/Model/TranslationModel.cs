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

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model
{
    /// <summary>
    /// Response payload for models.
    /// </summary>
    public class TranslationModel
    {
        /// <summary>
        /// Availability of a model.
        /// </summary>
        /// <value>Availability of a model.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum UPLOADING for uploading
            /// </summary>
            [EnumMember(Value = "uploading")]
            UPLOADING,
            
            /// <summary>
            /// Enum UPLOADED for uploaded
            /// </summary>
            [EnumMember(Value = "uploaded")]
            UPLOADED,
            
            /// <summary>
            /// Enum DISPATCHING for dispatching
            /// </summary>
            [EnumMember(Value = "dispatching")]
            DISPATCHING,
            
            /// <summary>
            /// Enum QUEUED for queued
            /// </summary>
            [EnumMember(Value = "queued")]
            QUEUED,
            
            /// <summary>
            /// Enum TRAINING for training
            /// </summary>
            [EnumMember(Value = "training")]
            TRAINING,
            
            /// <summary>
            /// Enum TRAINED for trained
            /// </summary>
            [EnumMember(Value = "trained")]
            TRAINED,
            
            /// <summary>
            /// Enum PUBLISHING for publishing
            /// </summary>
            [EnumMember(Value = "publishing")]
            PUBLISHING,
            
            /// <summary>
            /// Enum AVAILABLE for available
            /// </summary>
            [EnumMember(Value = "available")]
            AVAILABLE,
            
            /// <summary>
            /// Enum DELETED for deleted
            /// </summary>
            [EnumMember(Value = "deleted")]
            DELETED,
            
            /// <summary>
            /// Enum ERROR for error
            /// </summary>
            [EnumMember(Value = "error")]
            ERROR
        }

        /// <summary>
        /// Availability of a model.
        /// </summary>
        /// <value>Availability of a model.</value>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StatusEnum? Status { get; set; }
        /// <summary>
        /// A globally unique string that identifies the underlying model that is used for translation.
        /// </summary>
        /// <value>A globally unique string that identifies the underlying model that is used for translation.</value>
        [JsonProperty("model_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ModelId { get; set; }
        /// <summary>
        /// Optional name that can be specified when the model is created.
        /// </summary>
        /// <value>Optional name that can be specified when the model is created.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Translation source language code.
        /// </summary>
        /// <value>Translation source language code.</value>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        /// <summary>
        /// Translation target language code.
        /// </summary>
        /// <value>Translation target language code.</value>
        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; set; }
        /// <summary>
        /// Model ID of the base model that was used to customize the model. If the model is not a custom model, this will be an empty string.
        /// </summary>
        /// <value>Model ID of the base model that was used to customize the model. If the model is not a custom model, this will be an empty string.</value>
        [JsonProperty("base_model_id", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseModelId { get; set; }
        /// <summary>
        /// The domain of the translation model.
        /// </summary>
        /// <value>The domain of the translation model.</value>
        [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
        public string Domain { get; set; }
        /// <summary>
        /// Whether this model can be used as a base for customization. Customized models are not further customizable, and some base models are not customizable.
        /// </summary>
        /// <value>Whether this model can be used as a base for customization. Customized models are not further customizable, and some base models are not customizable.</value>
        [JsonProperty("customizable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Customizable { get; set; }
        /// <summary>
        /// Whether or not the model is a default model. A default model is the model for a given language pair that will be used when that language pair is specified in the source and target parameters.
        /// </summary>
        /// <value>Whether or not the model is a default model. A default model is the model for a given language pair that will be used when that language pair is specified in the source and target parameters.</value>
        [JsonProperty("default_model", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DefaultModel { get; set; }
        /// <summary>
        /// Either an empty string, indicating the model is not a custom model, or the ID of the service instance that created the model.
        /// </summary>
        /// <value>Either an empty string, indicating the model is not a custom model, or the ID of the service instance that created the model.</value>
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }
    }

}
