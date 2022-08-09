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
using System;

namespace IBM.Watson.Discovery.v2.Model
{
    /// <summary>
    /// Information about a document classifier.
    /// </summary>
    public class DocumentClassifier
    {
        /// <summary>
        /// A unique identifier of the document classifier.
        /// </summary>
        [JsonProperty("classifier_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string ClassifierId { get; private set; }
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
        /// The date that the document classifier was created.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Created { get; private set; }
        /// <summary>
        /// The language of the training data that is associated with the document classifier. Language is specified by
        /// using the ISO 639-1 language code, such as `en` for English or `ja` for Japanese.
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// An array of enrichments to apply to the data that is used to train and test the document classifier. The
        /// output from the enrichments is used as features by the classifier to classify the document content both
        /// during training and at run time.
        /// </summary>
        [JsonProperty("enrichments", NullValueHandling = NullValueHandling.Ignore)]
        public List<DocumentClassifierEnrichment> Enrichments { get; set; }
        /// <summary>
        /// An array of fields that are used to train the document classifier. The same set of fields must exist in the
        /// training data, the test data, and the documents where the resulting document classifier enrichment is
        /// applied at run time.
        /// </summary>
        [JsonProperty("recognized_fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RecognizedFields { get; set; }
        /// <summary>
        /// The name of the field from the training and test data that contains the classification labels.
        /// </summary>
        [JsonProperty("answer_field", NullValueHandling = NullValueHandling.Ignore)]
        public string AnswerField { get; set; }
        /// <summary>
        /// Name of the CSV file with training data that is used to train the document classifier.
        /// </summary>
        [JsonProperty("training_data_file", NullValueHandling = NullValueHandling.Ignore)]
        public string TrainingDataFile { get; set; }
        /// <summary>
        /// Name of the CSV file with data that is used to test the document classifier. If no test data is provided, a
        /// subset of the training data is used for testing purposes.
        /// </summary>
        [JsonProperty("test_data_file", NullValueHandling = NullValueHandling.Ignore)]
        public string TestDataFile { get; set; }
        /// <summary>
        /// An object with details for creating federated document classifier models.
        /// </summary>
        [JsonProperty("federated_classification", NullValueHandling = NullValueHandling.Ignore)]
        public ClassifierFederatedModel FederatedClassification { get; set; }
    }

}
