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
    /// Information about a specific enrichment.
    /// </summary>
    public class CreateEnrichment
    {
        /// <summary>
        /// The type of this enrichment. The following types are supported:
        ///
        /// * `classifier`: Creates a document classifier enrichment from a document classifier model that you create by
        /// using the [Document classifier API](/apidocs/discovery-data#createdocumentclassifier). **Note**: A text
        /// classifier enrichment can be created only from the product user interface.
        ///
        /// * `dictionary`: Creates a custom dictionary enrichment that you define in a CSV file.
        ///
        /// * `regular_expression`: Creates a custom regular expression enrichment from regex syntax that you specify in
        /// the request.
        ///
        /// * `rule_based`: Creates an enrichment from an advanced rules model that is created and exported as a ZIP
        /// file from Watson Knowledge Studio.
        ///
        /// * `uima_annotator`: Creates an enrichment from a custom UIMA text analysis model that is defined in a PEAR
        /// file created in one of the following ways:
        ///
        ///     * Watson Explorer Content Analytics Studio. **Note**: Supported in IBM Cloud Pak for Data instances
        /// only.
        ///
        ///     * Rule-based model that is created in Watson Knowledge Studio.
        ///
        /// * `watson_knowledge_studio_model`: Creates an enrichment from a Watson Knowledge Studio machine learning
        /// model that is defined in a ZIP file.
        /// </summary>
        public class TypeEnumValue
        {
            /// <summary>
            /// Constant CLASSIFIER for classifier
            /// </summary>
            public const string CLASSIFIER = "classifier";
            /// <summary>
            /// Constant DICTIONARY for dictionary
            /// </summary>
            public const string DICTIONARY = "dictionary";
            /// <summary>
            /// Constant REGULAR_EXPRESSION for regular_expression
            /// </summary>
            public const string REGULAR_EXPRESSION = "regular_expression";
            /// <summary>
            /// Constant UIMA_ANNOTATOR for uima_annotator
            /// </summary>
            public const string UIMA_ANNOTATOR = "uima_annotator";
            /// <summary>
            /// Constant RULE_BASED for rule_based
            /// </summary>
            public const string RULE_BASED = "rule_based";
            /// <summary>
            /// Constant WATSON_KNOWLEDGE_STUDIO_MODEL for watson_knowledge_studio_model
            /// </summary>
            public const string WATSON_KNOWLEDGE_STUDIO_MODEL = "watson_knowledge_studio_model";
            
        }

        /// <summary>
        /// The type of this enrichment. The following types are supported:
        ///
        /// * `classifier`: Creates a document classifier enrichment from a document classifier model that you create by
        /// using the [Document classifier API](/apidocs/discovery-data#createdocumentclassifier). **Note**: A text
        /// classifier enrichment can be created only from the product user interface.
        ///
        /// * `dictionary`: Creates a custom dictionary enrichment that you define in a CSV file.
        ///
        /// * `regular_expression`: Creates a custom regular expression enrichment from regex syntax that you specify in
        /// the request.
        ///
        /// * `rule_based`: Creates an enrichment from an advanced rules model that is created and exported as a ZIP
        /// file from Watson Knowledge Studio.
        ///
        /// * `uima_annotator`: Creates an enrichment from a custom UIMA text analysis model that is defined in a PEAR
        /// file created in one of the following ways:
        ///
        ///     * Watson Explorer Content Analytics Studio. **Note**: Supported in IBM Cloud Pak for Data instances
        /// only.
        ///
        ///     * Rule-based model that is created in Watson Knowledge Studio.
        ///
        /// * `watson_knowledge_studio_model`: Creates an enrichment from a Watson Knowledge Studio machine learning
        /// model that is defined in a ZIP file.
        /// Constants for possible values can be found using CreateEnrichment.TypeEnumValue
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        /// <summary>
        /// The human readable name for this enrichment.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The description of this enrichment.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// An object that contains options for the current enrichment. Starting with version `2020-08-30`, the
        /// enrichment options are not included in responses from the List Enrichments method.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public EnrichmentOptions Options { get; set; }
    }

}
