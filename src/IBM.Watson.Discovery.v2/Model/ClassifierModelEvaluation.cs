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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// An object that contains information about a trained document classifier model.
    /// </summary>
    public class ClassifierModelEvaluation
    {
        /// <summary>
        /// A micro-average aggregates the contributions of all classes to compute the average metric. Classes refers to
        /// the classification labels that are specified in the **answer_field**.
        /// </summary>
        [JsonProperty("micro_average", NullValueHandling = NullValueHandling.Ignore)]
        public ModelEvaluationMicroAverage MicroAverage { get; set; }
        /// <summary>
        /// A macro-average computes metric independently for each class and then takes the average. Class refers to the
        /// classification label that is specified in the **answer_field**.
        /// </summary>
        [JsonProperty("macro_average", NullValueHandling = NullValueHandling.Ignore)]
        public ModelEvaluationMacroAverage MacroAverage { get; set; }
        /// <summary>
        /// An array of evaluation metrics, one set of metrics for each class, where class refers to the classification
        /// label that is specified in the **answer_field**.
        /// </summary>
        [JsonProperty("per_class", NullValueHandling = NullValueHandling.Ignore)]
        public List<PerClassModelEvaluation> PerClass { get; set; }
    }

}
