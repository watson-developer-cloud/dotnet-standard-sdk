using System.Net.Http.Headers;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Service;

namespace IBM.WatsonDeveloperCloud.Conversation.v1
{
    public class ConversationService : WatsonService, IConversationService
    {
        const string PATH_MESSAGE = "/v1/workspaces/{0}/message";
        const string VERSION_DATE_2016_07_11 = "2016-07-11";
        const string SERVICE_NAME = "conversation";
        const string URL = "https://gateway.watsonplatform.net/conversation/api";

        public ConversationService()
            : base(SERVICE_NAME, URL)
        {
            if (!string.IsNullOrEmpty(this.Endpoint))
                this.Endpoint = URL;
        }

        public MessageResponse Message(string workspaceId, MessageRequest request)
        {
            MessageResponse result = null;

            using (IClient client = new WatsonHttpClient(this.Endpoint, this.UserName, this.Password))
            {
                result =
                    client.WithAuthentication(this.UserName, this.Password)
                          .PostAsync(this.Endpoint + string.Format(PATH_MESSAGE, workspaceId))
                          .WithArgument("version", VERSION_DATE_2016_07_11)
                          .WithHeader("accept", HttpMediaType.APPLICATION_JSON)
                          .WithBody<MessageRequest>(request, MediaTypeHeaderValue.Parse(HttpMediaType.APPLICATION_JSON))
                          .As<MessageResponse>()
                          .Result;
            }

            return result;
        }
    }
}
