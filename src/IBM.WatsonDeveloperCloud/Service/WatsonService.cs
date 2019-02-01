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
using System.Net.Http;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Util;

namespace IBM.WatsonDeveloperCloud.Service
{
    public abstract class WatsonService : IWatsonService
    {
        const string PATH_AUTHORIZATION_V1_TOKEN = "/authorization/api/v1/token";
        const string ICP_PREFIX = "icp-";
        const string APIKEY_AS_USERNAME = "apikey";
        public string SERVICE_NAME;
        private string username;
        private string password;
        public IClient Client { get; set; }

        public string ServiceName { get; set; }
        public string ApiKey { get; set; }
        public string Url { get { return Endpoint; } }

        protected string Endpoint
        {
            get
            {
                if (this.Client.BaseClient == null ||
                    this.Client.BaseClient.BaseAddress == null)
                    return string.Empty;

                return this.Client.BaseClient.BaseAddress.AbsoluteUri;
            }
            set
            {
                if (this.Client.BaseClient == null)
                {
                    this.Client.BaseClient = new HttpClient();
                }
                this.Client.BaseClient.BaseAddress = new Uri(value);
            }
        }
        public string UserName
        {
            get { return username; }
            set
            {
                if (!Utility.HasBadFirstOrLastCharacter(value))
                {
                    username = value;
                }
                else
                {
                    throw new ArgumentException("The username shouldn't start or end with curly brackets or quotes. Be sure to remove any {} and \" characters surrounding your username.");
                }
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (!Utility.HasBadFirstOrLastCharacter(value))
                {
                    password = value;
                }
                else
                {
                    throw new ArgumentException("The password shouldn't start or end with curly brackets or quotes. Be sure to remove any {} and \" characters surrounding your password.");
                }
            }
        }
        protected TokenManager _tokenManager = null;
        protected bool _userSetEndpoint = false;

        protected WatsonService(string serviceName)
        {
            this.Client = new WatsonHttpClient();
            ServiceName = serviceName;

            var credentialsPaths = Utility.GetCredentialsPaths();
            if (credentialsPaths.Count > 0)
            {
                foreach (string path in credentialsPaths)
                {
                    if (Utility.LoadEnvFile(path))
                    {
                        break;
                    }
                }

                string apiKey = Environment.GetEnvironmentVariable(ServiceName.ToUpper() + "_APIKEY");
                if (!string.IsNullOrEmpty(apiKey))
                    ApiKey = apiKey;
                string un = Environment.GetEnvironmentVariable(ServiceName.ToUpper() + "_USERNAME");
                if (!string.IsNullOrEmpty(un))
                    UserName = un;
                string pw = Environment.GetEnvironmentVariable(ServiceName.ToUpper() + "_PASSWORD");
                if (!string.IsNullOrEmpty(pw))
                    Password = pw;
                string ServiceUrl = Environment.GetEnvironmentVariable(ServiceName.ToUpper() + "_URL");

                if (string.IsNullOrEmpty(ApiKey) && (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)))
                {
                    throw new NullReferenceException(string.Format("Either {0}_APIKEY or {0}_USERNAME and {0}_PASSWORD did not exist. Please add credentials with this key in ibm-credentials.env.", ServiceName.ToUpper()));
                }

                if (!string.IsNullOrEmpty(ApiKey))
                {
                    TokenOptions tokenOptions = new TokenOptions()
                    {
                        IamApiKey = ApiKey
                    };

                    if (!string.IsNullOrEmpty(ServiceUrl))
                        tokenOptions.ServiceUrl = ServiceUrl;

                    if (!string.IsNullOrEmpty(tokenOptions.ServiceUrl))
                    {
                        Endpoint = tokenOptions.ServiceUrl;
                    }
                    else
                    {
                        tokenOptions.ServiceUrl = Url;
                    }

                    SetCredential(tokenOptions);
                }

                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                {
                    if (!string.IsNullOrEmpty(ServiceUrl))
                        Endpoint = ServiceUrl;

                    SetCredential(UserName, Password);
                }
            }
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
            if (userName == APIKEY_AS_USERNAME && !password.StartsWith(ICP_PREFIX))
            {
                TokenOptions tokenOptions = new TokenOptions()
                {
                    IamApiKey = password
                };

                SetCredential(tokenOptions);
            }
            else
            {
                this.UserName = userName;
                this.Password = password;
            }
        }

        /// <summary>
        /// Sets the tokenOptions for the service. 
        /// Also sets the endpoint if the user has not set the endpoint.
        /// </summary>
        /// <param name="options"></param>
        public void SetCredential(TokenOptions options)
        {
            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                if (!_userSetEndpoint)
                {
                    this.Endpoint = options.ServiceUrl;
                }
            }
            else
            {
                options.ServiceUrl = this.Endpoint;
            }

            if (!string.IsNullOrEmpty(options.IamApiKey))
            {
                if (options.IamApiKey.StartsWith(ICP_PREFIX))
                {
                    SetCredential(APIKEY_AS_USERNAME, options.IamApiKey);
                }
                else
                {
                    _tokenManager = new TokenManager(options);
                }
            }
            else if (!string.IsNullOrEmpty(options.IamAccessToken))
            {
                _tokenManager = new TokenManager(options);
            }
            else
            {
                throw new ArgumentNullException("An iamApikey or iamAccessToken is required.");
            }
        }

        public void SetEndpoint(string url)
        {
            _userSetEndpoint = true;
            this.Endpoint = url;
        }

        public void SendAsInsecure(bool insecure)
        {
            this.Client.SendAsInsecure(insecure);
        }
    }
}
