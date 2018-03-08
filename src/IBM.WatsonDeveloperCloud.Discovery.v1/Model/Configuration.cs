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
using System;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// A custom configuration for the environment.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The unique identifier of the configuration.
        /// </summary>
        /// <value>The unique identifier of the configuration.</value>
        [JsonProperty("configuration_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string ConfigurationId { get; private set; }
        /// <summary>
        /// The name of the configuration.
        /// </summary>
        /// <value>The name of the configuration.</value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// The creation date of the configuration in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.
        /// </summary>
        /// <value>The creation date of the configuration in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Created { get; private set; }
        /// <summary>
        /// The timestamp of when the configuration was last updated in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.
        /// </summary>
        /// <value>The timestamp of when the configuration was last updated in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime Updated { get; private set; }
        /// <summary>
        /// The description of the configuration, if available.
        /// </summary>
        /// <value>The description of the configuration, if available.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// The document conversion settings for the configuration.
        /// </summary>
        /// <value>The document conversion settings for the configuration.</value>
        [JsonProperty("conversions", NullValueHandling = NullValueHandling.Ignore)]
        public Conversions Conversions { get; set; }
        /// <summary>
        /// An array of document enrichment settings for the configuration.
        /// </summary>
        /// <value>An array of document enrichment settings for the configuration.</value>
        [JsonProperty("enrichments", NullValueHandling = NullValueHandling.Ignore)]
        public List<Enrichment> Enrichments { get; set; }
        /// <summary>
        /// Defines operations that can be used to transform the final output JSON into a normalized form. Operations are executed in the order that they appear in the array.
        /// </summary>
        /// <value>Defines operations that can be used to transform the final output JSON into a normalized form. Operations are executed in the order that they appear in the array.</value>
        [JsonProperty("normalizations", NullValueHandling = NullValueHandling.Ignore)]
        public List<NormalizationOperation> Normalizations { get; set; }
    }

}
