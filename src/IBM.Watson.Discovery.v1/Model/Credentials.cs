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

namespace IBM.Watson.Discovery.v1.Model
{
    /// <summary>
    /// Object containing credential information.
    /// </summary>
    public class Credentials : BaseModel
    {
        /// <summary>
        /// The source that this credentials object connects to.
        /// -  `box` indicates the credentials are used to connect an instance of Enterprise Box.
        /// -  `salesforce` indicates the credentials are used to connect to Salesforce.
        /// -  `sharepoint` indicates the credentials are used to connect to Microsoft SharePoint Online.
        /// -  `web_crawl` indicates the credentials are used to perform a web crawl.
        /// </summary>
        /// <value>
        /// The source that this credentials object connects to.
        /// -  `box` indicates the credentials are used to connect an instance of Enterprise Box.
        /// -  `salesforce` indicates the credentials are used to connect to Salesforce.
        /// -  `sharepoint` indicates the credentials are used to connect to Microsoft SharePoint Online.
        /// -  `web_crawl` indicates the credentials are used to perform a web crawl.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SourceTypeEnum
        {
            
            /// <summary>
            /// Enum BOX for box
            /// </summary>
            [EnumMember(Value = "box")]
            BOX,
            
            /// <summary>
            /// Enum SALESFORCE for salesforce
            /// </summary>
            [EnumMember(Value = "salesforce")]
            SALESFORCE,
            
            /// <summary>
            /// Enum SHAREPOINT for sharepoint
            /// </summary>
            [EnumMember(Value = "sharepoint")]
            SHAREPOINT,
            
            /// <summary>
            /// Enum WEB_CRAWL for web_crawl
            /// </summary>
            [EnumMember(Value = "web_crawl")]
            WEB_CRAWL
        }

        /// <summary>
        /// The source that this credentials object connects to.
        /// -  `box` indicates the credentials are used to connect an instance of Enterprise Box.
        /// -  `salesforce` indicates the credentials are used to connect to Salesforce.
        /// -  `sharepoint` indicates the credentials are used to connect to Microsoft SharePoint Online.
        /// -  `web_crawl` indicates the credentials are used to perform a web crawl.
        /// </summary>
        [JsonProperty("source_type", NullValueHandling = NullValueHandling.Ignore)]
        public SourceTypeEnum? SourceType { get; set; }
        /// <summary>
        /// Unique identifier for this set of credentials.
        /// </summary>
        [JsonProperty("credential_id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string CredentialId { get; private set; }
        /// <summary>
        /// Object containing details of the stored credentials.
        ///
        /// Obtain credentials for your source from the administrator of the source.
        /// </summary>
        [JsonProperty("credential_details", NullValueHandling = NullValueHandling.Ignore)]
        public CredentialDetails CredentialDetails { get; set; }
    }

}
