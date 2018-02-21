/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Enrichment.
    /// </summary>
    public class Enrichment
    {
        /// <summary>
        /// Describes what the enrichment step does.
        /// </summary>
        /// <value>Describes what the enrichment step does.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// Field where enrichments will be stored. This field must already exist or be at most 1 level deeper than an existing field. For example, if `text` is a top-level field with no sub-fields, `text.foo` is a valid destination but `text.foo.bar` is not.
        /// </summary>
        /// <value>Field where enrichments will be stored. This field must already exist or be at most 1 level deeper than an existing field. For example, if `text` is a top-level field with no sub-fields, `text.foo` is a valid destination but `text.foo.bar` is not.</value>
        [JsonProperty("destination_field", NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationField { get; set; }
        /// <summary>
        /// Field to be enriched.
        /// </summary>
        /// <value>Field to be enriched.</value>
        [JsonProperty("source_field", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceField { get; set; }
        /// <summary>
        /// Indicates that the enrichments will overwrite the destination_field field if it already exists.
        /// </summary>
        /// <value>Indicates that the enrichments will overwrite the destination_field field if it already exists.</value>
        [JsonProperty("overwrite", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Overwrite { get; set; }
        /// <summary>
        /// Name of the enrichment service to call. Current options are `natural_language_understanding` and `elements`.   When using `natual_language_understanding`, the `options` object must contain Natural Language Understanding Options.   When using `elements` the `options` object must contain Element Classification options. Additionally, when using the `elements` enrichment the configuration specified and files ingested must meet all the criteria specified in [the documentation](https://console.bluemix.net/docs/services/discovery/element-classification.html)     Previous API versions also supported `alchemy_language`.
        /// </summary>
        /// <value>Name of the enrichment service to call. Current options are `natural_language_understanding` and `elements`.   When using `natual_language_understanding`, the `options` object must contain Natural Language Understanding Options.   When using `elements` the `options` object must contain Element Classification options. Additionally, when using the `elements` enrichment the configuration specified and files ingested must meet all the criteria specified in [the documentation](https://console.bluemix.net/docs/services/discovery/element-classification.html)     Previous API versions also supported `alchemy_language`.</value>
        [JsonProperty("enrichment", NullValueHandling = NullValueHandling.Ignore)]
        public string EnrichmentName { get; set; }
        /// <summary>
        /// If true, then most errors generated during the enrichment process will be treated as warnings and will not cause the document to fail processing.
        /// </summary>
        /// <value>If true, then most errors generated during the enrichment process will be treated as warnings and will not cause the document to fail processing.</value>
        [JsonProperty("ignore_downstream_errors", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IgnoreDownstreamErrors { get; set; }
        /// <summary>
        /// A list of options specific to the enrichment.
        /// </summary>
        /// <value>A list of options specific to the enrichment.</value>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public EnrichmentOptions Options { get; set; }
    }

}
