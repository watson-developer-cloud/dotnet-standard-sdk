

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

    public partial class AddCollectionRequest
    {
        /// <summary>
        /// Initializes a new instance of the AddCollectionRequest class.
        /// </summary>
        public AddCollectionRequest() { }

        /// <summary>
        /// Initializes a new instance of the AddCollectionRequest class.
        /// </summary>
        /// <param name="name">The name of the collection to be created</param>
        /// <param name="description">A description of the collection</param>
        /// <param name="configurationId">The ID of the configuration in which
        /// the collection is to be created</param>
        public AddCollectionRequest(string name, string description = default(string), string configurationId = default(string))
        {
            Name = name;
            Description = description;
            ConfigurationId = configurationId;
        }

        /// <summary>
        /// Gets or sets the name of the collection to be created
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a description of the collection
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the configuration in which the collection
        /// is to be created
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
        }
    }
}
