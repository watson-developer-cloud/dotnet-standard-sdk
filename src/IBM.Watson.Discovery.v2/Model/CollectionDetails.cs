/**
* (C) Copyright IBM Corp. 2020, 2023.
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
    /// A collection for storing documents.
    /// </summary>
    public class CollectionDetails
    {
        /// <summary>
        /// The unique identifier of the collection.
        /// </summary>
        [JsonProperty("collection_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CollectionId { get; private set; }
        /// <summary>
        /// The name of the collection.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// A description of the collection.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The date that the collection was created.
        /// </summary>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? Created { get; private set; }
        /// <summary>
        /// The language of the collection. For a list of supported languages, see the [product
        /// documentation](/docs/discovery-data?topic=discovery-data-language-support).
        /// </summary>
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        /// <summary>
        /// An array of enrichments that are applied to this collection. To get a list of enrichments that are available
        /// for a project, use the [List enrichments](#listenrichments) method.
        ///
        /// If no enrichments are specified when the collection is created, the default enrichments for the project type
        /// are applied. For more information about project default settings, see the [product
        /// documentation](/docs/discovery-data?topic=discovery-data-project-defaults).
        /// </summary>
        [JsonProperty("enrichments", NullValueHandling = NullValueHandling.Ignore)]
        public List<CollectionEnrichment> Enrichments { get; set; }
        /// <summary>
        /// An object that describes the Smart Document Understanding model for a collection.
        /// </summary>
        [JsonProperty("smart_document_understanding", NullValueHandling = NullValueHandling.Ignore)]
        public virtual CollectionDetailsSmartDocumentUnderstanding SmartDocumentUnderstanding { get; private set; }
    }

}
