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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// An object specifying the sentiment extraction enrichment and related parameters.
    /// </summary>
    public class NluEnrichmentSentiment
    {
        /// <summary>
        /// When `true`, sentiment analysis is performed on the entire field.
        /// </summary>
        /// <value>When `true`, sentiment analysis is performed on the entire field.</value>
        [JsonProperty("document", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Document { get; set; }
        /// <summary>
        /// A comma-separated list of target strings that will have any associated sentiment analyzed.
        /// </summary>
        /// <value>A comma-separated list of target strings that will have any associated sentiment analyzed.</value>
        [JsonProperty("targets", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Targets { get; set; }
    }

}
