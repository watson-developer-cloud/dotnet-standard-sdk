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
    /// An object that manages the settings and data that is required to train a document classification model.
    /// </summary>
    public class CreateDocumentClassifier
    {
        /// <summary>
        /// A human-readable name of the document classifier.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// A description of the document classifier.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The language of the training data that is associated with the document classifier. Language is specified by
        /// using the ISO 639-1 language code, such as `en` for English or `ja` for Japanese.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// The name of the field from the training and test data that contains the classification labels.
        /// </summary>
        [JsonProperty("answer_field", NullValueHandling = NullValueHandling.Ignore)]
        public string AnswerField { get; set; }
        /// <summary>
        /// An array of enrichments to apply to the data that is used to train and test the document classifier. The
        /// output from the enrichments is used as features by the classifier to classify the document content both
        /// during training and at run time.
        /// </summary>
        [JsonProperty("enrichments", NullValueHandling = NullValueHandling.Ignore)]
        public List<DocumentClassifierEnrichment> Enrichments { get; set; }
        /// <summary>
        /// An object with details for creating federated document classifier models.
        /// </summary>
        [JsonProperty("federated_classification", NullValueHandling = NullValueHandling.Ignore)]
        public ClassifierFederatedModel FederatedClassification { get; set; }
    }

}
