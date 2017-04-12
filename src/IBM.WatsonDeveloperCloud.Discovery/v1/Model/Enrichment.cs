

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

    public partial class Enrichment
    {
        /// <summary>
        /// Initializes a new instance of the Enrichment class.
        /// </summary>
        public Enrichment() { }

        /// <summary>
        /// Initializes a new instance of the Enrichment class.
        /// </summary>
        /// <param name="destinationField">Field where enrichments will be
        /// stored. This field must already exist or be at most 1 level
        /// deeper than an existing field. For example, if `text` is a
        /// top-level field with no sub-fields, `text.foo` is a valid
        /// destination but `text.foo.bar` is not.
        /// </param>
        /// <param name="sourceField">Field to be enriched.</param>
        /// <param name="description">Describes what the enrichment step
        /// does</param>
        /// <param name="overwrite">Indicates that the enrichments will
        /// overwrite the destination_field field if it already
        /// exists.</param>
        /// <param name="ignoreDownstreamErrors">If true, then most errors
        /// generated during the enrichment process will be treated as
        /// warnings and wil not cause the document to fail processing.
        /// </param>
        /// <param name="options">A list of options specific to the
        /// enrichment</param>
        public Enrichment(string destinationField, string sourceField, string description = default(string), bool? overwrite = default(bool?), bool? ignoreDownstreamErrors = default(bool?), EnrichmentOptions options = default(EnrichmentOptions))
        {
            Description = description;
            DestinationField = destinationField;
            SourceField = sourceField;
            Overwrite = overwrite;
            IgnoreDownstreamErrors = ignoreDownstreamErrors;
            Options = options;
        }
        /// <summary>
        /// Static constructor for Enrichment class.
        /// </summary>
        static Enrichment()
        {
            EnrichmentProperty = "alchemy_language";
        }

        /// <summary>
        /// Gets or sets describes what the enrichment step does
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets field where enrichments will be stored. This field
        /// must already exist or be at most 1 level deeper than an existing
        /// field. For example, if `text` is a top-level field with no
        /// sub-fields, `text.foo` is a valid destination but `text.foo.bar`
        /// is not.
        /// </summary>
        [JsonProperty(PropertyName = "destination_field")]
        public string DestinationField { get; set; }

        /// <summary>
        /// Gets or sets field to be enriched.
        /// </summary>
        [JsonProperty(PropertyName = "source_field")]
        public string SourceField { get; set; }

        /// <summary>
        /// Gets or sets indicates that the enrichments will overwrite the
        /// destination_field field if it already exists.
        /// </summary>
        [JsonProperty(PropertyName = "overwrite")]
        public bool? Overwrite { get; set; }

        /// <summary>
        /// Gets or sets if true, then most errors generated during the
        /// enrichment process will be treated as warnings and wil not cause
        /// the document to fail processing.
        /// </summary>
        [JsonProperty(PropertyName = "ignore_downstream_errors")]
        public bool? IgnoreDownstreamErrors { get; set; }

        /// <summary>
        /// Gets or sets a list of options specific to the enrichment
        /// </summary>
        [JsonProperty(PropertyName = "options")]
        public EnrichmentOptions Options { get; set; }

        /// <summary>
        /// Name of the enrichment service to call. Currently the only valid
        /// value is `alchemy_language`.
        /// </summary>
        [JsonProperty(PropertyName = "enrichment")]
        public static string EnrichmentProperty { get; private set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (DestinationField == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "DestinationField");
            }
            if (SourceField == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "SourceField");
            }
        }
    }
}
