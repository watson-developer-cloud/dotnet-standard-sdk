

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

    /// <summary>
    /// A custom configuration for the environment.
    /// </summary>
    public partial class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the Configuration class.
        /// </summary>
        public Configuration() { }

        /// <summary>
        /// Initializes a new instance of the Configuration class.
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
        public Configuration(string name, string configurationId = default(string), Datetime created = default(Datetime), Datetime updated = default(Datetime), string description = default(string), Conversions conversions = default(Conversions),List<Enrichment> enrichments = default(System.Collections.Generic.IList<Enrichment>),List<NormalizationOperation> normalizations = default(System.Collections.Generic.IList<NormalizationOperation>))
        {
            ConfigurationId = configurationId;
            Name = name;
            Created = created;
            Updated = updated;
            Description = description;
            Conversions = conversions;
            Enrichments = enrichments;
            Normalizations = normalizations;
        }

        /// <summary>
        /// Gets the unique identifier of the configuration
        /// </summary>
        [JsonProperty(PropertyName = "configuration_id")]
        public string ConfigurationId { get; private set; }

        /// <summary>
        /// Gets or sets the name of the configuration
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the creation date of the configuration in the format
        /// yyyy-MM-dd'T'HH:mm:ss.SSS'Z'
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public Datetime Created { get; private set; }

        /// <summary>
        /// Gets the timestamp of when the configuration was last updated in
        /// the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'
        /// </summary>
        [JsonProperty(PropertyName = "updated")]
        public Datetime Updated { get; private set; }

        /// <summary>
        /// Gets or sets the description of the configuration, if available
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets an array of document conversion settings for the
        /// configuration
        /// </summary>
        [JsonProperty(PropertyName = "conversions")]
        public Conversions Conversions { get; set; }

        /// <summary>
        /// Gets or sets an array of document enrichment settings for the
        /// configuration
        /// </summary>
        [JsonProperty(PropertyName = "enrichments")]
        public List<Enrichment> Enrichments { get; set; }

        /// <summary>
        /// Gets or sets an array of document normalization settings for the
        /// configuration
        /// </summary>
        [JsonProperty(PropertyName = "normalizations")]
        public List<NormalizationOperation> Normalizations { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "Name");
            }
            if (this.Name != null)
            {
                if (this.Name.Length > 255)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.MaxLength, "Name", 255);
                }
                if (this.Name.Length < 0)
                {
                    throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.MinLength, "Name", 0);
                }
            }
            if (this.Enrichments != null)
            {
                foreach (var element in this.Enrichments)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}
