using System;
using System.Net.Http;
using System.Text;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Models;
using IBM.WatsonDeveloperCloud.Service;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3
{
    public class PersonalityInsightsService : WatsonService, IPersonalityInsightsService
    {
        const string SERVICE_NAME = "language_translator";

        const string URL = "https://gateway.watsonplatform.net/personality-insights/api";
        const string PATH_GET_PROFILES = "/v3/profile";

        public PersonalityInsightsService()
            : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public PersonalityInsightsService(string userName, string password)
            : this()
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            this.SetCredential(userName, password);
        }

        public PersonalityInsightsService(IClient httpClient)
            : this()
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            this.Client = httpClient;
        }

        public Profile GetProfile(ProfileOptions options)
        {
            Profile result = null;

            try
            {
                Encoding enc = null;

                switch (options.ContentType)
                {
                    case HttpMediaType.TEXT_HTML:
                    case HttpMediaType.TEXT_PLAIN:
                        enc = Encoding.ASCII;
                        break;

                    case HttpMediaType.APPLICATION_JSON:
                        enc = Encoding.UTF8;
                        break;
                }

                int bodySize = enc.GetByteCount(options.Text) / 1024 / 1024;



                result =
                     this.Client.WithAuthentication(UserName, Password)
                                .PostAsync($"{this.Endpoint}{PATH_GET_PROFILES}")
                                .As<Profile>()
                                .Result;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}