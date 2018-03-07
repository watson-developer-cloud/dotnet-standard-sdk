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
    /// Options which are specific to a particular enrichment.
    /// </summary>
    public class EnrichmentOptions
    {
        /// <summary>
        /// An object representing the enrichment features that will be applied to the specified field.
        /// </summary>
        /// <value>An object representing the enrichment features that will be applied to the specified field.</value>
        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public NluEnrichmentFeatures Features { get; set; }
        /// <summary>
        /// *For use with `elements` enrichments only.* The element extraction model to use. Models available are: `contract`.
        /// </summary>
        /// <value>*For use with `elements` enrichments only.* The element extraction model to use. Models available are: `contract`.</value>
        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    }

}
