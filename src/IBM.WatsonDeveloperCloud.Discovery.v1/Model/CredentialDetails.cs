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

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.Discovery.v1.Model
{
    /// <summary>
    /// Object containing details of the stored credentials.
    ///
    /// Obtain credentials for your source from the administrator of the source.
    /// </summary>
    public class CredentialDetails : BaseModel
    {
        /// <summary>
        /// The authentication method for this credentials definition. The  **credential_type** specified must be
        /// supported by the **source_type**. The following combinations are possible:
        ///
        /// -  `\"source_type\": \"box\"` - valid `credential_type`s: `oauth2`
        /// -  `\"source_type\": \"salesforce\"` - valid `credential_type`s: `username_password`
        /// -  `\"source_type\": \"sharepoint\"` - valid `credential_type`s: `saml`.
        /// </summary>
        /// <value>
        /// The authentication method for this credentials definition. The  **credential_type** specified must be
        /// supported by the **source_type**. The following combinations are possible:
        ///
        /// -  `\"source_type\": \"box\"` - valid `credential_type`s: `oauth2`
        /// -  `\"source_type\": \"salesforce\"` - valid `credential_type`s: `username_password`
        /// -  `\"source_type\": \"sharepoint\"` - valid `credential_type`s: `saml`.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CredentialTypeEnum
        {
            
            /// <summary>
            /// Enum OAUTH2 for oauth2
            /// </summary>
            [EnumMember(Value = "oauth2")]
            OAUTH2,
            
            /// <summary>
            /// Enum SAML for saml
            /// </summary>
            [EnumMember(Value = "saml")]
            SAML,
            
            /// <summary>
            /// Enum USERNAME_PASSWORD for username_password
            /// </summary>
            [EnumMember(Value = "username_password")]
            USERNAME_PASSWORD
        }

        /// <summary>
        /// The authentication method for this credentials definition. The  **credential_type** specified must be
        /// supported by the **source_type**. The following combinations are possible:
        ///
        /// -  `\"source_type\": \"box\"` - valid `credential_type`s: `oauth2`
        /// -  `\"source_type\": \"salesforce\"` - valid `credential_type`s: `username_password`
        /// -  `\"source_type\": \"sharepoint\"` - valid `credential_type`s: `saml`.
        /// </summary>
        [JsonProperty("credential_type", NullValueHandling = NullValueHandling.Ignore)]
        public CredentialTypeEnum? CredentialType { get; set; }
        /// <summary>
        /// The **client_id** of the source that these credentials connect to. Only valid, and required, with a
        /// **credential_type** of `oauth2`.
        /// </summary>
        [JsonProperty("client_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientId { get; set; }
        /// <summary>
        /// The **enterprise_id** of the Box site that these credentials connect to. Only valid, and required, with a
        /// **source_type** of `box`.
        /// </summary>
        [JsonProperty("enterprise_id", NullValueHandling = NullValueHandling.Ignore)]
        public string EnterpriseId { get; set; }
        /// <summary>
        /// The **url** of the source that these credentials connect to. Only valid, and required, with a
        /// **credential_type** of `username_password`.
        /// </summary>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        /// <summary>
        /// The **username** of the source that these credentials connect to. Only valid, and required, with a
        /// **credential_type** of `saml` and `username_password`.
        /// </summary>
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }
        /// <summary>
        /// The **organization_url** of the source that these credentials connect to. Only valid, and required, with a
        /// **credential_type** of `saml`.
        /// </summary>
        [JsonProperty("organization_url", NullValueHandling = NullValueHandling.Ignore)]
        public string OrganizationUrl { get; set; }
        /// <summary>
        /// The **site_collection.path** of the source that these credentials connect to. Only valid, and required, with
        /// a **source_type** of `sharepoint`.
        /// </summary>
        [JsonProperty("site_collection.path", NullValueHandling = NullValueHandling.Ignore)]
        public string SiteCollectionPath { get; set; }
        /// <summary>
        /// The **client_secret** of the source that these credentials connect to. Only valid, and required, with a
        /// **credential_type** of `oauth2`. This value is never returned and is only used when creating or modifying
        /// **credentials**.
        /// </summary>
        [JsonProperty("client_secret", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientSecret { get; set; }
        /// <summary>
        /// The **public_key_id** of the source that these credentials connect to. Only valid, and required, with a
        /// **credential_type** of `oauth2`. This value is never returned and is only used when creating or modifying
        /// **credentials**.
        /// </summary>
        [JsonProperty("public_key_id", NullValueHandling = NullValueHandling.Ignore)]
        public string PublicKeyId { get; set; }
        /// <summary>
        /// The **private_key** of the source that these credentials connect to. Only valid, and required, with a
        /// **credential_type** of `oauth2`. This value is never returned and is only used when creating or modifying
        /// **credentials**.
        /// </summary>
        [JsonProperty("private_key", NullValueHandling = NullValueHandling.Ignore)]
        public string PrivateKey { get; set; }
        /// <summary>
        /// The **passphrase** of the source that these credentials connect to. Only valid, and required, with a
        /// **credential_type** of `oauth2`. This value is never returned and is only used when creating or modifying
        /// **credentials**.
        /// </summary>
        [JsonProperty("passphrase", NullValueHandling = NullValueHandling.Ignore)]
        public string Passphrase { get; set; }
        /// <summary>
        /// The **password** of the source that these credentials connect to. Only valid, and required, with
        /// **credential_type**s of `saml` and `username_password`.
        ///
        /// **Note:** When used with a **source_type** of `salesforce`, the password consists of the Salesforce password
        /// and a valid Salesforce security token concatenated. This value is never returned and is only used when
        /// creating or modifying **credentials**.
        /// </summary>
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
    }

}
