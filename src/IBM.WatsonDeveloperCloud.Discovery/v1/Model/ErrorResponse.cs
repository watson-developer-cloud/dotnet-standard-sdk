

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

    public partial class ErrorResponse
    {
        /// <summary>
        /// Initializes a new instance of the ErrorResponse class.
        /// </summary>
        public ErrorResponse() { }

        /// <summary>
        /// Initializes a new instance of the ErrorResponse class.
        /// </summary>
        /// <param name="code">the HTTP error status code</param>
        /// <param name="error">a message describing the error</param>
        public ErrorResponse(int code, string error)
        {
            Code = code;
            Error = error;
        }

        /// <summary>
        /// Gets or sets the HTTP error status code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets a message describing the error
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Error == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "Error");
            }
            if (this.Code > 600)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.InclusiveMaximum, "Code", 600);
            }
            if (this.Code < 100)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.InclusiveMinimum, "Code", 100);
            }
        }
    }
}
