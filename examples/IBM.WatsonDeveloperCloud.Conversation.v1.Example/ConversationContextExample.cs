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

using IBM.WatsonDeveloperCloud.Conversation.v1.Model;
using Newtonsoft.Json;
using System;

namespace IBM.WatsonDeveloperCloud.Conversation.v1.Example
{
    public class ConversationContextExample
    {
        private ConversationService _conversation;
        private string _workspaceID;
        private string[] _questionArray = { "hello", "can you turn up the AC", "can you turn on the wipers", "can you turn off the wipers", "can you turn down the ac", "can you unlock the door" };
        private int _questionIndex = 0;
        private dynamic _questionContext = null;

        public ConversationContextExample(string url, string username, string password, string workspaceId)
        {
            _conversation = new ConversationService(username, password, "2018-02-16");
            _conversation.Endpoint = url;

            _workspaceID = workspaceId;

            CallConversation(_questionIndex);
        }

        public void CallConversation(int questionIndex)
        {
            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new InputData()
                {
                    Text = _questionArray[questionIndex]
                },
                AlternateIntents = true
            };

            if(_questionContext != null)
            {
                messageRequest.Context = new Context();
                messageRequest.Context.ConversationId = _questionContext.conversation_id;
                messageRequest.Context.System = _questionContext.system;
            }

            var result = _conversation.Message(_workspaceID, messageRequest);
            Console.WriteLine(string.Format("result: {0}", JsonConvert.SerializeObject(result, Formatting.Indented)));
            _questionIndex++;
            _questionContext = result.Context;

            if (questionIndex < _questionArray.Length - 1)
                CallConversation(_questionIndex);
        }
    }
}

