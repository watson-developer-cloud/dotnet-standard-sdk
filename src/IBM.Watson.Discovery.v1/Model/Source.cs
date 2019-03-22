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
    /// Object containing source parameters for the configuration.
    /// </summary>
    public class Source : BaseModel
    {
        /// <summary>
        /// The type of source to connect to.
        /// -  `box` indicates the configuration is to connect an instance of Enterprise Box.
        /// -  `salesforce` indicates the configuration is to connect to Salesforce.
        /// -  `sharepoint` indicates the configuration is to connect to Microsoft SharePoint Online.
        /// -  `web_crawl` indicates the configuration is to perform a web page crawl.
        /// </summary>
        /// <value>
        /// The type of source to connect to.
        /// -  `box` indicates the configuration is to connect an instance of Enterprise Box.
        /// -  `salesforce` indicates the configuration is to connect to Salesforce.
        /// -  `sharepoint` indicates the configuration is to connect to Microsoft SharePoint Online.
        /// -  `web_crawl` indicates the configuration is to perform a web page crawl.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TypeEnum
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
        /// The type of source to connect to.
        /// -  `box` indicates the configuration is to connect an instance of Enterprise Box.
        /// -  `salesforce` indicates the configuration is to connect to Salesforce.
        /// -  `sharepoint` indicates the configuration is to connect to Microsoft SharePoint Online.
        /// -  `web_crawl` indicates the configuration is to perform a web page crawl.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public TypeEnum? Type { get; set; }
        /// <summary>
        /// The **credential_id** of the credentials to use to connect to the source. Credentials are defined using the
        /// **credentials** method. The **source_type** of the credentials used must match the **type** field specified
        /// in this object.
        /// </summary>
        [JsonProperty("credential_id", NullValueHandling = NullValueHandling.Ignore)]
        public string CredentialId { get; set; }
        /// <summary>
        /// Object containing the schedule information for the source.
        /// </summary>
        [JsonProperty("schedule", NullValueHandling = NullValueHandling.Ignore)]
        public SourceSchedule Schedule { get; set; }
        /// <summary>
        /// The **options** object defines which items to crawl from the source system.
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public SourceOptions Options { get; set; }
    }

}
