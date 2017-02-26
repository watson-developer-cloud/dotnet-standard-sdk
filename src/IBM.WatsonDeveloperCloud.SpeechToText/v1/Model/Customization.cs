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

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public partial class Customization
    {
        /// <summary>
        /// Initializes a new instance of the Customization class.
        /// </summary>
        public Customization() { }

        /// <summary>
        /// Initializes a new instance of the Customization class.
        /// </summary>
        /// <param name="customizationId">The GUID of the custom language
        /// model.</param>
        /// <param name="created">The date and time in Coordinated Universal
        /// Time (UTC) at which the custom language model was created. The
        /// value is provided in full ISO 8601 format
        /// (`YYYY-MM-DDThh:mm:ss.sTZD`).</param>
        /// <param name="language">The language of the custom language model,
        /// `en-US` or `ja-JP`.</param>
        /// <param name="owner">The GUID of the service credentials for the
        /// owner of the custom language model.</param>
        /// <param name="name">The name of the custom language model.</param>
        /// <param name="description">The description of the custom language
        /// model.</param>
        /// <param name="baseModelName">The name of the base language model
        /// for which the custom language model was created. Possible values
        /// include: 'en-US_BroadbandModel', 'en-US_NarrowbandModel'</param>
        /// <param name="status">The current status of the custom language
        /// model: `pending` indicates that the model was created but is
        /// waiting either for training data to be added or for the service
        /// to finish analyzing added data. `ready` indicates that the model
        /// contains data and is ready to be trained. `training` indicates
        /// that the model is currently being trained. `available` indicates
        /// that the model is trained and ready to use. `failed` indicates
        /// that training of the model failed. Possible values include:
        /// 'pending', 'ready', 'training', 'available', 'failed'</param>
        /// <param name="progress">A percentage that indicates the progress of
        /// the model's current training. A value of `100` means that the
        /// model is fully trained. For this beta release, the `progress`
        /// field does not reflect the current progress of the training; the
        /// field changes from `0` to `100` when training is complete.</param>
        /// <param name="warnings">If the request included unknown query
        /// parameters, the following message: `Unexpected query parameter(s)
        /// ['parameters'] detected`, where `parameters` is a list that
        /// includes a quoted string for each unknown parameter.</param>
        public Customization(string customizationId, string created, string language, string owner, string name, string description, string baseModelName, string status, int progress, string warnings = default(string))
        {
            CustomizationId = customizationId;
            Created = created;
            Language = language;
            Owner = owner;
            Name = name;
            Description = description;
            BaseModelName = baseModelName;
            Status = status;
            Progress = progress;
            Warnings = warnings;
        }

        /// <summary>
        /// Gets or sets the GUID of the custom language model.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "customization_id")]
        public string CustomizationId { get; set; }

        /// <summary>
        /// Gets or sets the date and time in Coordinated Universal Time (UTC)
        /// at which the custom language model was created. The value is
        /// provided in full ISO 8601 format (`YYYY-MM-DDThh:mm:ss.sTZD`).
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "created")]
        public string Created { get; set; }

        /// <summary>
        /// Gets or sets the language of the custom language model, `en-US` or
        /// `ja-JP`.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the GUID of the service credentials for the owner of
        /// the custom language model.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the name of the custom language model.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the custom language model.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the base language model for which the
        /// custom language model was created. Possible values include:
        /// 'en-US_BroadbandModel', 'en-US_NarrowbandModel'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "base_model_name")]
        public string BaseModelName { get; set; }

        /// <summary>
        /// Gets or sets the current status of the custom language model:
        /// `pending` indicates that the model was created but is waiting
        /// either for training data to be added or for the service to finish
        /// analyzing added data. `ready` indicates that the model contains
        /// data and is ready to be trained. `training` indicates that the
        /// model is currently being trained. `available` indicates that the
        /// model is trained and ready to use. `failed` indicates that
        /// training of the model failed. Possible values include: 'pending',
        /// 'ready', 'training', 'available', 'failed'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a percentage that indicates the progress of the
        /// model's current training. A value of `100` means that the model
        /// is fully trained. For this beta release, the `progress` field
        /// does not reflect the current progress of the training; the field
        /// changes from `0` to `100` when training is complete.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "progress")]
        public int Progress { get; set; }

        /// <summary>
        /// Gets or sets if the request included unknown query parameters, the
        /// following message: `Unexpected query parameter(s) ['parameters']
        /// detected`, where `parameters` is a list that includes a quoted
        /// string for each unknown parameter.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "warnings")]
        public string Warnings { get; set; }
    }
}