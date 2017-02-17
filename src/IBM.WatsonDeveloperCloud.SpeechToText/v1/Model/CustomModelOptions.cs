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

using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.SpeechToText.v1.Model
{
    public class CustomModelOptions
    {
        /// <summary>
        /// The name of the new custom model. Use a name that is unique among all custom models that are owned by the calling user. Use a localized name that matches the language of the custom model.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// The name of the language model that is to be customized by the new model. You must use the name of one of the US English or Japanese models that is returned by the <see cref="ISpeechToTextService.GetModels" /> method.
        /// The new custom model can be used only with the base language model that it customizes.
        /// </summary>
        [JsonProperty("base_model_name")]
        public string BaseModelName { get; private set; }

        /// <summary>
        /// A description of the new custom model. Use a localized description that matches the language of the custom model.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        private CustomModelOptions() { }

        public interface IRequiredName
        {
            IRequiredBaseModel WithName(string _name);
        }

        public interface IRequiredBaseModel
        {
            IOptional WithBaseModel(string _baseModelName);
        }

        public interface IOptional
        {
            IOptional WithDescription(string _description);
            CustomModelOptions Build();
        }

        class CustomModelOptionsBuilder : IRequiredName, IRequiredBaseModel, IOptional
        {
            string _name;
            string _baseModelName;
            string _description;

            public IRequiredBaseModel WithName(string _name)
            {
                this._name = _name;
                return this;
            }

            public IOptional WithBaseModel(string _baseModelName)
            {
                this._baseModelName = _baseModelName;
                return this;
            }

            public IOptional WithDescription(string _description)
            {
                this._description = _description;
                return this;
            }

            public CustomModelOptions Build()
            {
                return this;
            }

            public static implicit operator CustomModelOptions(CustomModelOptionsBuilder builder)
            {
                return new CustomModelOptions()
                {
                    Name = builder._name,
                    BaseModelName = builder._baseModelName,
                    Description = builder._description
                };
            }
        }

        public static IRequiredName Builder => new CustomModelOptionsBuilder();
    }
}