/**
* (C) Copyright IBM Corp. 2021.
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
using Newtonsoft.Json;

namespace IBM.Watson.SpeechToText.v1.Model
{
    /// <summary>
    /// Information about an existing custom language model.
    /// </summary>
    public class LanguageModel
    {
        /// <summary>
        /// The current status of the custom language model:
        /// * `pending`: The model was created but is waiting either for valid training data to be added or for the
        /// service to finish analyzing added data.
        /// * `ready`: The model contains valid data and is ready to be trained. If the model contains a mix of valid
        /// and invalid resources, you need to set the `strict` parameter to `false` for the training to proceed.
        /// * `training`: The model is currently being trained.
        /// * `available`: The model is trained and ready to use.
        /// * `upgrading`: The model is currently being upgraded.
        /// * `failed`: Training of the model failed.
        /// </summary>
        public class StatusEnumValue
        {
            /// <summary>
            /// Constant PENDING for pending
            /// </summary>
            public const string PENDING = "pending";
            /// <summary>
            /// Constant READY for ready
            /// </summary>
            public const string READY = "ready";
            /// <summary>
            /// Constant TRAINING for training
            /// </summary>
            public const string TRAINING = "training";
            /// <summary>
            /// Constant AVAILABLE for available
            /// </summary>
            public const string AVAILABLE = "available";
            /// <summary>
            /// Constant UPGRADING for upgrading
            /// </summary>
            public const string UPGRADING = "upgrading";
            /// <summary>
            /// Constant FAILED for failed
            /// </summary>
            public const string FAILED = "failed";
            
        }

        /// <summary>
        /// The current status of the custom language model:
        /// * `pending`: The model was created but is waiting either for valid training data to be added or for the
        /// service to finish analyzing added data.
        /// * `ready`: The model contains valid data and is ready to be trained. If the model contains a mix of valid
        /// and invalid resources, you need to set the `strict` parameter to `false` for the training to proceed.
        /// * `training`: The model is currently being trained.
        /// * `available`: The model is trained and ready to use.
        /// * `upgrading`: The model is currently being upgraded.
        /// * `failed`: Training of the model failed.
        /// Constants for possible values can be found using LanguageModel.StatusEnumValue
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// The customization ID (GUID) of the custom language model. The [Create a custom language
        /// model](#createlanguagemodel) method returns only this field of the object; it does not return the other
        /// fields.
        /// </summary>
        [JsonProperty("customization_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomizationId { get; set; }
        /// <summary>
        /// The date and time in Coordinated Universal Time (UTC) at which the custom language model was created. The
        /// value is provided in full ISO 8601 format (`YYYY-MM-DDThh:mm:ss.sTZD`).
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public string Created { get; set; }
        /// <summary>
        /// The date and time in Coordinated Universal Time (UTC) at which the custom language model was last modified.
        /// The `created` and `updated` fields are equal when a language model is first added but has yet to be updated.
        /// The value is provided in full ISO 8601 format (YYYY-MM-DDThh:mm:ss.sTZD).
        /// </summary>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public string Updated { get; set; }
        /// <summary>
        /// The language identifier of the custom language model (for example, `en-US`).
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// The dialect of the language for the custom language model. For non-Spanish models, the field matches the
        /// language of the base model; for example, `en-US` for either of the US English language models. For Spanish
        /// models, the field indicates the dialect for which the model was created:
        /// * `es-ES` for Castilian Spanish (`es-ES` models)
        /// * `es-LA` for Latin American Spanish (`es-AR`, `es-CL`, `es-CO`, and `es-PE` models)
        /// * `es-US` for Mexican (North American) Spanish (`es-MX` models)
        ///
        /// Dialect values are case-insensitive.
        /// </summary>
        [JsonProperty("dialect", NullValueHandling = NullValueHandling.Ignore)]
        public string Dialect { get; set; }
        /// <summary>
        /// _For custom models that are based on previous-generation models_, a list of the available versions of the
        /// custom language model. Each element of the array indicates a version of the base model with which the custom
        /// model can be used. Multiple versions exist only if the custom model has been upgraded; otherwise, only a
        /// single version is shown.
        ///
        /// _For custom models that are based on next-generation models_, a single version of the custom model. Only one
        /// version of a custom model that is based on a next-generation model is ever available, and upgrading does not
        /// apply.
        /// </summary>
        [JsonProperty("versions", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Versions { get; set; }
        /// <summary>
        /// The GUID of the credentials for the instance of the service that owns the custom language model.
        /// </summary>
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }
        /// <summary>
        /// The name of the custom language model.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of the custom language model.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The name of the language model for which the custom language model was created.
        /// </summary>
        [JsonProperty("base_model_name", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseModelName { get; set; }
        /// <summary>
        /// A percentage that indicates the progress of the custom language model's current training. A value of `100`
        /// means that the model is fully trained. **Note:** The `progress` field does not currently reflect the
        /// progress of the training. The field changes from `0` to `100` when training is complete.
        /// </summary>
        [JsonProperty("progress", NullValueHandling = NullValueHandling.Ignore)]
        public long? Progress { get; set; }
        /// <summary>
        /// If an error occurred while adding a grammar file to the custom language model, a message that describes an
        /// `Internal Server Error` and includes the string `Cannot compile grammar`. The status of the custom model is
        /// not affected by the error, but the grammar cannot be used with the model.
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
        /// <summary>
        /// If the request included unknown parameters, the following message: `Unexpected query parameter(s)
        /// ['parameters'] detected`, where `parameters` is a list that includes a quoted string for each unknown
        /// parameter.
        /// </summary>
        [JsonProperty("warnings", NullValueHandling = NullValueHandling.Ignore)]
        public string Warnings { get; set; }
    }

}
