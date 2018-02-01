

using Newtonsoft.Json;
using System;
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
    /// <summary>
    /// Details about an environment
    /// </summary>
    public partial class Environment
    {
        /// <summary>
        /// Initializes a new instance of the Environment class.
        /// </summary>
        public Environment() { }

        /// <summary>
        /// Initializes a new instance of the Environment class.
        /// </summary>
        /// <param name="environmentId">Unique identifier for this
        /// environment</param>
        /// <param name="name">Name that identifies this environment</param>
        /// <param name="description">Description of the environment</param>
        /// <param name="created">Creation date of the environment, in the
        /// format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'</param>
        /// <param name="updated">Date of most recent environment update, in
        /// the format yyyy-MM-dd'T'HH:mm:ss.SSS'Z'</param>
        /// <param name="status">Status of the environment. Possible values
        /// include: 'active', 'pending'</param>
        /// <param name="readOnly">If true, then the environment contains
        /// read-only collections which are maintained by IBM.</param>
        /// <param name="indexCapacity">Object containing information about
        /// disk and memory usage</param>
        public Environment(string environmentId = default(string), string name = default(string), string description = default(string), Datetime created = default(Datetime), Datetime updated = default(Datetime), string status = default(string), bool? readOnly = default(bool?), double? size = default(double?), IndexCapacity indexCapacity = default(IndexCapacity))
        {
            EnvironmentId = environmentId;
            Name = name;
            Description = description;
            Created = created;
            Updated = updated;
            Status = status;
            ReadOnly = readOnly;
            Size = size;
            IndexCapacity = indexCapacity;
        }

        /// <summary>
        /// Gets unique identifier for this environment
        /// </summary>
        [JsonProperty(PropertyName = "environment_id")]
        public string EnvironmentId { get; private set; }

        /// <summary>
        /// Gets or sets name that identifies this environment
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description of the environment
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets creation date of the environment, in the format
        /// yyyy-MM-dd'T'HH:mm:ss.SSS'Z'
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; private set; }

        /// <summary>
        /// Gets date of most recent environment update, in the format
        /// yyyy-MM-dd'T'HH:mm:ss.SSS'Z'
        /// </summary>
        [JsonProperty(PropertyName = "updated")]
        public Datetime Updated { get; private set; }

        /// <summary>
        /// Gets status of the environment. Possible values include: 'active',
        /// 'pending'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; private set; }

        /// <summary>
        /// Gets if true, then the environment contains read-only collections
        /// which are maintained by IBM.
        /// </summary>
        [JsonProperty(PropertyName = "read_only")]
        public bool? ReadOnly { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public double? Size { get; set; }

        /// <summary>
        /// Gets or sets object containing information about disk and memory
        /// usage
        /// </summary>
        [JsonProperty(PropertyName = "index_capacity")]
        public IndexCapacity IndexCapacity { get; set; }

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
