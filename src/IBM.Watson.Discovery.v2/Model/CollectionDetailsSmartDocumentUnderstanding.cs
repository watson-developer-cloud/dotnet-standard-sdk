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
    /// An object that describes the Smart Document Understanding model for a collection.
    /// </summary>
    public class CollectionDetailsSmartDocumentUnderstanding
    {
        /// <summary>
        /// Specifies the type of Smart Document Understanding (SDU) model that is enabled for the collection. The
        /// following types of models are supported:
        ///
        ///  * `custom`: A user-trained model is applied.
        ///
        ///  * `pre_trained`: A pretrained model is applied. This type of model is applied automatically to *Document
        /// Retrieval for Contracts* projects.
        ///
        ///  * `text_extraction`: An SDU model that extracts text and metadata from the content. This model is enabled
        /// in collections by default regardless of the types of documents in the collection (as long as the service
        /// plan supports SDU models).
        ///
        /// You can apply user-trained or pretrained models to collections from the *Identify fields* page of the
        /// product user interface. For more information, see [the product
        /// documentation](/docs/discovery-data?topic=discovery-data-configuring-fields).
        /// </summary>
        public class ModelEnumValue
        {
            /// <summary>
            /// Constant CUSTOM for custom
            /// </summary>
            public const string CUSTOM = "custom";
            /// <summary>
            /// Constant PRE_TRAINED for pre_trained
            /// </summary>
            public const string PRE_TRAINED = "pre_trained";
            /// <summary>
            /// Constant TEXT_EXTRACTION for text_extraction
            /// </summary>
            public const string TEXT_EXTRACTION = "text_extraction";
            
        }

        /// <summary>
        /// Specifies the type of Smart Document Understanding (SDU) model that is enabled for the collection. The
        /// following types of models are supported:
        ///
        ///  * `custom`: A user-trained model is applied.
        ///
        ///  * `pre_trained`: A pretrained model is applied. This type of model is applied automatically to *Document
        /// Retrieval for Contracts* projects.
        ///
        ///  * `text_extraction`: An SDU model that extracts text and metadata from the content. This model is enabled
        /// in collections by default regardless of the types of documents in the collection (as long as the service
        /// plan supports SDU models).
        ///
        /// You can apply user-trained or pretrained models to collections from the *Identify fields* page of the
        /// product user interface. For more information, see [the product
        /// documentation](/docs/discovery-data?topic=discovery-data-configuring-fields).
        /// Constants for possible values can be found using CollectionDetailsSmartDocumentUnderstanding.ModelEnumValue
        /// </summary>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
        /// <summary>
        /// When `true`, smart document understanding conversion is enabled for the collection.
        /// </summary>
        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enabled { get; set; }
    }

}
