using IBM.WatsonDeveloperCloud.Conversation.v1;
using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBM.WatsonDeveloperCloud.Conversation.IntegrationTests
{
    [TestClass]
    public class ConversationServiceIntegrationTest
    {   
        const string WORKSPACE_ID = "0a0c06c1-8e31-4655-9067-58fcac5134fc";

        [TestMethod]
        public void Message()
        {
            #region messageRequest

            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = "Turn on the lights"
                }
            };

            #endregion messageRequest

            ConversationService service = new ConversationService();
            service.Endpoint = "https://watson-api-explorer.mybluemix.net/conversation/api";

            var results = service.Message(WORKSPACE_ID, messageRequest);

            //Assert.IsNotNull(results);
            //Assert.IsTrue(results.Intents.Count >= 1);
        }
    }

}
