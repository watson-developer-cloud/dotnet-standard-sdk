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
