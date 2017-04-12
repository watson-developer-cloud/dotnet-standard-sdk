

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

    public partial class DeleteConfigurationResponse
    {
        /// <summary>
        /// Initializes a new instance of the DeleteConfigurationResponse
        /// class.
        /// </summary>
        public DeleteConfigurationResponse() { }

        /// <summary>
        /// Initializes a new instance of the DeleteConfigurationResponse
        /// class.
        /// </summary>
        public DeleteConfigurationResponse(string configurationId,List<Notice> notices = default(System.Collections.Generic.IList<Notice>))
        {
            ConfigurationId = configurationId;
            Notices = notices;
        }
        /// <summary>
        /// Static constructor for DeleteConfigurationResponse class.
        /// </summary>
        static DeleteConfigurationResponse()
        {
            Status = "deleted";
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "configuration_id")]
        public string ConfigurationId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "notices")]
        public List<Notice> Notices { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public static string Status { get; private set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (ConfigurationId == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "ConfigurationId");
            }
        }
    }
}
