/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

using System;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Util;

namespace IBM.WatsonDeveloperCloud.Service
{
    public abstract class WatsonService : IWatsonService
    {
        const string PATH_AUTHORIZATION_V1_TOKEN = "/authorization/api/v1/token";
        
        public IClient Client { get; set; }

        public string ServiceName { get; set; }
        public string ApiKey { get; set; }
        protected string Endpoint { get
            {
                if (this.Client.BaseClient == null ||
                    this.Client.BaseClient.BaseAddress == null)
                    return string.Empty;

                return this.Client.BaseClient.BaseAddress.AbsoluteUri;
            }
            set
            {
                this.Client.BaseClient.BaseAddress = new Uri(value);
            }
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        protected TokenManager _tokenManager = null;
        protected bool _userSetEndpoint = false;

        protected WatsonService(string serviceName)
        {
            this.ServiceName = serviceName;
            this.Client = new WatsonHttpClient(this.Endpoint, this.UserName, this.Password);
            
            //TODO: verificar como iremos obter de um arquivo json por injeção de dependencia
            //this.ApiKey = CredentialUtils.GetApiKey(serviceName);
            //this.Endpoint = CredentialUtils.GetApiUrl(serviceName);
        }

        protected WatsonService(string serviceName, string url)
        {
            this.ServiceName = serviceName;
            this.Client = new WatsonHttpClient(url, this.UserName, this.Password);

            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = url;
            //TODO: verificar como iremos obter de um arquivo json por injeção de dependencia
            //this.ApiKey = CredentialUtils.GetApiKey(serviceName);
            //this.Endpoint = CredentialUtils.GetApiUrl(serviceName);
        }

        protected WatsonService(string serviceName, string url, IClient httpClient)
        {
            this.ServiceName = serviceName;
            this.Client = httpClient;

            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = url;

            //TODO: verificar como iremos obter de um arquivo json por injeção de dependencia
            //this.ApiKey = CredentialUtils.GetApiKey(serviceName);
            //this.Endpoint = CredentialUtils.GetApiUrl(serviceName);
        }

        /// <summary>
        /// Sets the username and password credentials.
        /// </summary>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        public void SetCredential(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        /// <summary>
        /// Sets the apikey of the service. 
        /// Also sets the endpoint if the user has not set the endpoint.
        /// </summary>
        /// <param name="apikey"></param>
        public void SetCredential(string apikey)
        {
            this.ApiKey = apikey;
            if (!_userSetEndpoint)
                this.Endpoint = "https://gateway-a.watsonplatform.net/visual-recognition/api";
        }

        /// <summary>
        /// Sets the tokenOptions for the service. 
        /// Also sets the endpoint if the user has not set the endpoint.
        /// </summary>
        /// <param name="options"></param>
        public void SetCredential(TokenOptions options)
        {
            if(!_userSetEndpoint)
                this.Endpoint = options.IamUrl;
            _tokenManager = new TokenManager(options);
        }

        public void SetEndpoint(string url)
        {
            _userSetEndpoint = true;
            this.Endpoint = url;
        }
    }
}
