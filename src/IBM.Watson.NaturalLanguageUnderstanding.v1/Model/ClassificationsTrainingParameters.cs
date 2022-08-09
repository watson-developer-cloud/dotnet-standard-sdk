/**
* (C) Copyright IBM Corp. 2022.
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

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.Model
{
    /// <summary>
    /// Optional classifications training parameters along with model train requests.
    /// </summary>
    public class ClassificationsTrainingParameters
    {
        /// <summary>
        /// Model type selector to train either a single_label or a multi_label classifier.
        /// </summary>
        public class ModelTypeEnumValue
        {
            /// <summary>
            /// Constant SINGLE_LABEL for single_label
            /// </summary>
            public const string SINGLE_LABEL = "single_label";
            /// <summary>
            /// Constant MULTI_LABEL for multi_label
            /// </summary>
            public const string MULTI_LABEL = "multi_label";
            
        }

        /// <summary>
        /// Model type selector to train either a single_label or a multi_label classifier.
        /// Constants for possible values can be found using ClassificationsTrainingParameters.ModelTypeEnumValue
        /// </summary>
        [JsonProperty("model_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ModelType { get; set; }
    }

}
