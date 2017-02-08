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
using System;

namespace IBM.WatsonDeveloperCloud.Conversation.Example
{
    public class ConversationServiceExample
    {
        private ConversationService _conversation = new ConversationService();
        private string _workspaceID;
        private string _inputString = "Can you turn on the lights?";

        public ConversationServiceExample(string username, string password, string workspaceID)
        {
            _conversation.SetCredential(username, password);
            _workspaceID = workspaceID;

            Message();
        }

        #region Message
        private void Message()
        {
            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = _inputString
                }
            };

            Console.WriteLine("Calling Message()...");
            var result = _conversation.Message(_workspaceID, messageRequest);

            if(result != null)
            {
                Console.WriteLine(string.Format("Output: {0}: ", result.Output.Text));
            }
            else
            {
                Console.WriteLine("Failed to message.");
            }
        }
        #endregion
    }
}
