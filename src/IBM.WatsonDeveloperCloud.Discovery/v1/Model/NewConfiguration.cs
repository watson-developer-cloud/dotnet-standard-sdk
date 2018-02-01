

/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 the "License";
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

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    using System.Linq;

    public partial class NewConfiguration : Configuration
    {
        /// <summary>
        /// Initializes a new instance of the NewConfiguration class.
        /// </summary>
        public NewConfiguration() { }

        /// <summary>
        /// Initializes a new instance of the NewConfiguration class.
        /// </summary>
        /// <param name="name">The name of the configuration</param>
        /// <param name="configurationId">The unique identifier of the
        /// configuration</param>
        /// <param name="created">The creation date of the configuration in
        /// the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'</param>
        /// <param name="updated">The timestamp of when the configuration was
        /// last updated in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'</param>
        /// <param name="description">The description of the configuration, if
        /// available</param>
        /// <param name="conversions">An array of document conversion settings
        /// for the configuration</param>
        /// <param name="enrichments">An array of document enrichment settings
        /// for the configuration</param>
        /// <param name="normalizations">An array of document normalization
        /// settings for the configuration</param>
        public NewConfiguration(string name, string configurationId = default(string), Datetime created = default(Datetime), Datetime updated = default(Datetime), string description = default(string), Conversions conversions = default(Conversions),List<Enrichment> enrichments = default(System.Collections.Generic.IList<Enrichment>),List<NormalizationOperation> normalizations = default(System.Collections.Generic.IList<NormalizationOperation>))
            : base(name, configurationId, created, updated, description, conversions, enrichments, normalizations)
        {
        }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public override void Validate()
        {
            base.Validate();
        }
    }
}
