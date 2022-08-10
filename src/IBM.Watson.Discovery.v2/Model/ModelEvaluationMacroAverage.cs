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

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// A macro-average computes metric independently for each class and then takes the average. Class refers to the
    /// classification label that is specified in the **answer_field**.
    /// </summary>
    public class ModelEvaluationMacroAverage
    {
        /// <summary>
        /// A metric that measures how many of the overall documents are classified correctly.
        /// </summary>
        [JsonProperty("precision", NullValueHandling = NullValueHandling.Ignore)]
        public double? Precision { get; set; }
        /// <summary>
        /// A metric that measures how often documents that should be classified into certain classes are classified
        /// into those classes.
        /// </summary>
        [JsonProperty("recall", NullValueHandling = NullValueHandling.Ignore)]
        public double? Recall { get; set; }
        /// <summary>
        /// A metric that measures whether the optimal balance between precision and recall is reached. The F1 score can
        /// be interpreted as a weighted average of the precision and recall values. An F1 score reaches its best value
        /// at 1 and worst value at 0.
        /// </summary>
        [JsonProperty("f1", NullValueHandling = NullValueHandling.Ignore)]
        public double? F1 { get; set; }
    }

}
