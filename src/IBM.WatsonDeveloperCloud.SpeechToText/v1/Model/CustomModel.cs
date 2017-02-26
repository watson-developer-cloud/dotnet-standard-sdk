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
using System;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public partial class CustomModel
    {
        /// <summary>
        /// Initializes a new instance of the CustomModel class.
        /// </summary>
        public CustomModel() { }

        /// <summary>
        /// Initializes a new instance of the CustomModel class.
        /// </summary>
        /// <param name="name">The name of the new custom model. Use a name
        /// that is unique among all custom models that are owned by the
        /// calling user. Use a localized name that matches the language of
        /// the custom model.</param>
        /// <param name="baseModelName">The name of the language model that is
        /// to be customized by the new model. You must use the name of one
        /// of the US English or Japanese models that is returned by the `GET
        /// /v1/models` method. The new custom model can be used only with
        /// the base language model that it customizes. Possible values
        /// include: 'en-US_BroadbandModel', 'en-US_NarrowbandModel',
        /// 'ja-JP_BroadbandModel', 'ja-JP_NarrowbandModel'</param>
        /// <param name="description">A description of the new custom model.
        /// Use a localized description that matches the language of the
        /// custom model.</param>
        public CustomModel(string name, string baseModelName, string description = default(string))
        {
            Name = name;
            BaseModelName = baseModelName;
            Description = description;
        }

        /// <summary>
        /// Gets or sets the name of the new custom model. Use a name that is
        /// unique among all custom models that are owned by the calling
        /// user. Use a localized name that matches the language of the
        /// custom model.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the language model that is to be
        /// customized by the new model. You must use the name of one of the
        /// US English or Japanese models that is returned by the `GET
        /// /v1/models` method. The new custom model can be used only with
        /// the base language model that it customizes. Possible values
        /// include: 'en-US_BroadbandModel', 'en-US_NarrowbandModel',
        /// 'ja-JP_BroadbandModel', 'ja-JP_NarrowbandModel'
        /// </summary>
        [JsonProperty(PropertyName = "base_model_name")]
        public string BaseModelName { get; set; }

        /// <summary>
        /// Gets or sets a description of the new custom model. Use a
        /// localized description that matches the language of the custom
        /// model.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}