using IBM.WatsonDeveloperCloud.Http;

namespace IBM.WatsonDeveloperCloud.Service
{
    public interface IWatsonService
    {
        IClient Client { get; set; }

        string ServiceName { get; set; }
        string ApiKey { get; set; }
        string Endpoint { get; set; }
        string UserName { get; set; }
        string Password { get; set; }

        void SetCredential(string userName, string password);
    }
}