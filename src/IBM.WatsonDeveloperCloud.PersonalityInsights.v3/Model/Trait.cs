/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model
{
    /// <summary>
    /// Trait.
    /// </summary>
    public class Trait
    {
        /// <summary>
        /// The unique identifier of the characteristic to which the results pertain. IDs have the form `big5_{characteristic}` for Big Five personality characteristics, `need_{characteristic}` for Needs, or `value_{characteristic}` for Values.
        /// </summary>
        /// <value>The unique identifier of the characteristic to which the results pertain. IDs have the form `big5_{characteristic}` for Big Five personality characteristics, `need_{characteristic}` for Needs, or `value_{characteristic}` for Values.</value>
        [JsonProperty("trait_id", NullValueHandling = NullValueHandling.Ignore)]
        public string TraitId { get; set; }
        /// <summary>
        /// The user-visible name of the characteristic.
        /// </summary>
        /// <value>The user-visible name of the characteristic.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The category of the characteristic: `personality` for Big Five personality characteristics, `needs` for Needs, or `values` for Values.
        /// </summary>
        /// <value>The category of the characteristic: `personality` for Big Five personality characteristics, `needs` for Needs, or `values` for Values.</value>
        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }
        /// <summary>
        /// The normalized percentile score for the characteristic. The range is 0 to 1. For example, if the percentage for Openness is 0.60, the author scored in the 60th percentile; the author is more open than 59 percent of the population and less open than 39 percent of the population.
        /// </summary>
        /// <value>The normalized percentile score for the characteristic. The range is 0 to 1. For example, if the percentage for Openness is 0.60, the author scored in the 60th percentile; the author is more open than 59 percent of the population and less open than 39 percent of the population.</value>
        [JsonProperty("percentile", NullValueHandling = NullValueHandling.Ignore)]
        public double? Percentile { get; set; }
        /// <summary>
        /// The raw score for the characteristic. The range is 0 to 1. A higher score generally indicates a greater likelihood that the author has that characteristic, but raw scores must be considered in aggregate: The range of values in practice might be much smaller than 0 to 1, so an individual score must be considered in the context of the overall scores and their range. The raw score is computed based on the input and the service model; it is not normalized or compared with a sample population. The raw score enables comparison of the results against a different sampling population and with a custom normalization approach.
        /// </summary>
        /// <value>The raw score for the characteristic. The range is 0 to 1. A higher score generally indicates a greater likelihood that the author has that characteristic, but raw scores must be considered in aggregate: The range of values in practice might be much smaller than 0 to 1, so an individual score must be considered in the context of the overall scores and their range. The raw score is computed based on the input and the service model; it is not normalized or compared with a sample population. The raw score enables comparison of the results against a different sampling population and with a custom normalization approach.</value>
        [JsonProperty("raw_score", NullValueHandling = NullValueHandling.Ignore)]
        public double? RawScore { get; set; }
        /// <summary>
        /// For `personality` (Big Five) dimensions, more detailed results for the facets of each dimension as inferred from the input text.
        /// </summary>
        /// <value>For `personality` (Big Five) dimensions, more detailed results for the facets of each dimension as inferred from the input text.</value>
        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public List<Trait> Children { get; set; }
    }

}
