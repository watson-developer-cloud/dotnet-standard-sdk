/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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
        /// A globally unique string that identifies the underlying model that is used for translation. This string contains all the information about source language, target language, domain, and various other related configurations.
        /// </summary>
        /// <value>A globally unique string that identifies the underlying model that is used for translation. This string contains all the information about source language, target language, domain, and various other related configurations.</value>
        [JsonProperty("model_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ModelId { get; set; }
        /// <summary>
        /// If a model is trained by a user, there might be an optional “name” parameter attached during training to help the user identify the model.
        /// </summary>
        /// <value>If a model is trained by a user, there might be an optional “name” parameter attached during training to help the user identify the model.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Source language in two letter language code. Use the five letter code when clarifying between multiple supported languages. When model_id is used directly, it will override the source-target language combination. Also, when a two letter language code is used, but no suitable default is found, it returns an error.
        /// </summary>
        /// <value>Source language in two letter language code. Use the five letter code when clarifying between multiple supported languages. When model_id is used directly, it will override the source-target language combination. Also, when a two letter language code is used, but no suitable default is found, it returns an error.</value>
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        /// <summary>
        /// Target language in two letter language code.
        /// </summary>
        /// <value>Target language in two letter language code.</value>
        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; set; }
        /// <summary>
        /// If this model is a custom model, this returns the base model that it is trained on. For a base model, this response value is empty.
        /// </summary>
        /// <value>If this model is a custom model, this returns the base model that it is trained on. For a base model, this response value is empty.</value>
        [JsonProperty("base_model_id", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseModelId { get; set; }
        /// <summary>
        /// The domain of the translation model.
        /// </summary>
        /// <value>The domain of the translation model.</value>
        [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
        public string Domain { get; set; }
        /// <summary>
        /// Whether this model can be used as a base for customization. Customized models are not further customizable, and we don't allow the customization of certain base models.
        /// </summary>
        /// <value>Whether this model can be used as a base for customization. Customized models are not further customizable, and we don't allow the customization of certain base models.</value>
        [JsonProperty("customizable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Customizable { get; set; }
        /// <summary>
        /// Whether this model is considered a default model and is used when the source and target languages are specified without the model_id.
        /// </summary>
        /// <value>Whether this model is considered a default model and is used when the source and target languages are specified without the model_id.</value>
        [JsonProperty("default_model", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DefaultModel { get; set; }
        /// <summary>
        /// Returns the Bluemix ID of the instance that created the model, or an empty string if it is a model that is trained by IBM.
        /// </summary>
        /// <value>Returns the Bluemix ID of the instance that created the model, or an empty string if it is a model that is trained by IBM.</value>
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }
    }

}
