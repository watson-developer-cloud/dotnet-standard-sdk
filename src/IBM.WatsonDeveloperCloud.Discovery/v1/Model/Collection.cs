

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
    /// A collection for storing documents
    /// </summary>
    public partial class Collection
    {
        /// <summary>
        /// Initializes a new instance of the Collection class.
        /// </summary>
        public Collection() { }

        /// <summary>
        /// Initializes a new instance of the Collection class.
        /// </summary>
        /// <param name="collectionId">The unique identifier of the
        /// collection</param>
        /// <param name="name">The name of the collection with a maximum
        /// length of 255 characters</param>
        /// <param name="description">The description of the collection</param>
        /// <param name="created">The creation date of the collection in the
        /// format yyyy-MM-dd'T'HH:mmcon:ss.SSS'Z'</param>
        /// <param name="updated">The timestamp of when the collection was
        /// last updated in the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'</param>
        /// <param name="status">The status of the collection. Possible values
        /// include: 'active', 'pending'</param>
        /// <param name="configurationId">The unique identifier of the
        /// collection's configuration</param>
        /// <param name="documentCounts">The object providing information
        /// about the documents in the collection. Seen only when retrieving
        /// details of a colleciton</param>
        public Collection(string collectionId = default(string), string name = default(string), string description = default(string), Datetime created = default(Datetime), Datetime updated = default(Datetime), string status = default(string), string configurationId = default(string), DocumentCounts documentCounts = default(DocumentCounts))
        {
            CollectionId = collectionId;
            Name = name;
            Description = description;
            Created = created;
            Updated = updated;
            Status = status;
            ConfigurationId = configurationId;
            DocumentCounts = documentCounts;
        }

        /// <summary>
        /// Gets the unique identifier of the collection
        /// </summary>
        [JsonProperty(PropertyName = "collection_id")]
        public string CollectionId { get; private set; }

        /// <summary>
        /// Gets or sets the name of the collection with a maximum length of
        /// 255 characters
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the collection
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the creation date of the collection in the format
        /// yyyy-MM-dd'T'HH:mmcon:ss.SSS'Z'
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public Datetime Created { get; private set; }

        /// <summary>
        /// Gets the timestamp of when the collection was last updated in the
        /// format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'
        /// </summary>
        [JsonProperty(PropertyName = "updated")]
        public Datetime Updated { get; private set; }

        /// <summary>
        /// Gets the status of the collection. Possible values include:
        /// 'active', 'pending'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; private set; }

        /// <summary>
        /// Gets or sets the unique identifier of the collection's
        /// configuration
        /// </summary>
        [JsonProperty(PropertyName = "configuration_id")]
        public string ConfigurationId { get; set; }

        /// <summary>
        /// Gets or sets the object providing information about the documents
        /// in the collection. Seen only when retrieving details of a
        /// colleciton
        /// </summary>
        [JsonProperty(PropertyName = "document_counts")]
        public DocumentCounts DocumentCounts { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
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
        }
    }
}
