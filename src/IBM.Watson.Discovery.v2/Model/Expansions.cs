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
    /// The query expansion definitions for the specified collection.
    /// </summary>
    public class Expansions
    {
        /// <summary>
        /// An array of query expansion definitions.
        ///
        ///  Each object in the **expansions** array represents a term or set of terms that will be expanded into other
        /// terms. Each expansion object can be configured as `bidirectional` or `unidirectional`.
        ///
        /// * **Bidirectional**: Each entry in the `expanded_terms` list expands to include all expanded terms. For
        /// example, a query for `ibm` expands to `ibm OR international business machines OR big blue`.
        ///
        /// * **Unidirectional**: The terms in `input_terms` in the query are replaced by the terms in `expanded_terms`.
        /// For example, a query for the often misused term `on premise` is converted to `on premises OR on-premises`
        /// and does not contain the original term. If you want an input term to be included in the query, then repeat
        /// the input term in the expanded terms list.
        /// </summary>
        [JsonProperty("expansions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Expansion> _Expansions { get; set; }
    }

}
