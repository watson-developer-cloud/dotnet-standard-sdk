using System;
using IBM.WatsonDeveloperCloud.Http;

namespace IBM.WatsonDeveloperCloud.Service
{
    public abstract class WatsonService : IWatsonService
    {
        const string PATH_AUTHORIZATION_V1_TOKEN = "/authorization/api/v1/token";

        public IClient Client { get; set; }

        public string ServiceName { get; set; }
        public string ApiKey { get; set; }
        public string Endpoint { get
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

        public void SetCredential(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}