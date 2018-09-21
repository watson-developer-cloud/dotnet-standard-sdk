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
*/using Newtonsoft.Json;
using System;

using System.Collections.Generic;
using System.Text;

namespace IBM.WatsonDeveloperCloud.Util
{
    /// <summary>
    /// Vcap credentials object.
    /// </summary>
    public class VcapCredentials
    {
        /// <summary>
        /// List of credentials by service name.
        /// </summary>
        [JsonProperty("VCAP_SERVICES", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<VcapCredential>> VCAP_SERVICES { get; set; }

        /// <summary>
        /// Gets a credential by name.
        /// </summary>
        /// <param name="name">Name of requested credential</param>
        /// <returns>A List of credentials who's names match the request name.</returns>
        public List<VcapCredential> GetCredentialByname(string name)
        {
            List<VcapCredential> credentialsList = new List<VcapCredential>();
            foreach (KeyValuePair<string, List<VcapCredential>> kvp in VCAP_SERVICES)
            {
                foreach (VcapCredential credential in kvp.Value)
                {
                    if (credential.Name == name)
                        credentialsList.Add(credential);
                }
            }

            return credentialsList;
        }
    }

    /// <summary>
    /// The Credential to a single service.
    /// </summary>
    public class VcapCredential
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }
        [JsonProperty("plan", NullValueHandling = NullValueHandling.Ignore)]
        public string Plan { get; set; }
        [JsonProperty("credentials", NullValueHandling = NullValueHandling.Ignore)]
        public Credential Credentials { get; set; }
    }

    /// <summary>
    /// The Credentials.
    /// </summary>
    public class Credential
    {
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        [JsonProperty("api_key", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiKey { get; set; }
        [JsonProperty("apikey", NullValueHandling = NullValueHandling.Ignore)]
        public string IamApikey { get; set; }
        [JsonProperty("workspace_id", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkspaceId { get; set; }
        [JsonProperty("environment_id", NullValueHandling = NullValueHandling.Ignore)]
        public string EnvironmentId { get; set; }
        [JsonProperty("classifier_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ClassifierId { get; set; }
        [JsonProperty("assistant_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AssistantId { get; set; }
    }
    
    /// <summary>
    /// IAM token options.
    /// </summary>
    public class TokenOptions
    {
        /// <summary>
        /// The IAM Apikey for the service instance. If provided, The SDK will manage authentication through tokens.
        /// </summary>
        public string IamApiKey { get; set; }
        /// <summary>
        /// The access token for the service instance. If provided, the SDK will not manage authentication through tokens. 
        /// </summary>
        public string IamAccessToken { get; set; }
        /// <summary>
        /// The service URL.
        /// </summary>
        public string ServiceUrl { get; set; }
        /// <summary>
        /// The IAM authentication URL. If omitted the value defaults to "https://iam.bluemix.net/identity/token".
        /// </summary>
        public string IamUrl { get; set; }
    }

    /// <summary>
    /// IAM Token data.
    /// </summary>
    public class IamTokenData
    {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; set; }
        [JsonProperty("token_type", NullValueHandling = NullValueHandling.Ignore)]
        public string TokenType { get; set; }
        [JsonProperty("expires_in", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpiresIn { get; set; }
        [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
        public long? Expiration { get; set; }
    }
}
