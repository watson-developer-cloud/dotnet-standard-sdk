

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

    public partial class CollectionRef
    {
        /// <summary>
        /// Initializes a new instance of the CollectionRef class.
        /// </summary>
        public CollectionRef() { }

        /// <summary>
        /// Initializes a new instance of the CollectionRef class.
        /// </summary>
        /// <param name="status">Possible values include: 'active',
        /// 'pending'</param>
        public CollectionRef(string collectionId = default(string), string name = default(string), string description = default(string), Datetime created = default(Datetime), Datetime updated = default(Datetime), string status = default(string), string configurationId = default(string))
        {
            CollectionId = collectionId;
            Name = name;
            Description = description;
            Created = created;
            Updated = updated;
            Status = status;
            ConfigurationId = configurationId;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "collection_id")]
        public string CollectionId { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public Datetime Created { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "updated")]
        public Datetime Updated { get; private set; }

        /// <summary>
        /// Gets possible values include: 'active', 'pending'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "configuration_id")]
        public string ConfigurationId { get; set; }

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
