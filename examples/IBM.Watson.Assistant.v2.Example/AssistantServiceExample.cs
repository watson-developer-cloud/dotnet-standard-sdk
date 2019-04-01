/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using IBM.Watson.Assistant.v2.Model;
using Newtonsoft.Json;
using System;
namespace IBM.Watson.Assistant.v2.Example
{
    internal class AssistantServiceExample
    {
        private AssistantService _assistant;
        private string _assistantId;
        private string[] _questionArray = { "", "good", "i want a pizza", "large", "three" };
        private int _questionIndex = 0;
        private string _sessionId;

        public AssistantServiceExample(string url, string username, string password, string assistantId)
        {
            _assistant = new AssistantService(username, password, "2018-09-20");
            _assistant.SetEndpoint(url);

            _assistantId = assistantId;

            var session = _assistant.CreateSession(_assistantId);
            _sessionId = session.SessionId;

            CallAssistant(_questionIndex);
        }

        public void CallAssistant(int questionIndex)
        {
            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new MessageInput()
                {
                    Text = _questionArray[questionIndex]
                }
            };

            Console.WriteLine(_questionArray[questionIndex]);
            var result = _assistant.Message(_assistantId, _sessionId, messageRequest);
            Console.WriteLine(result.Output.Generic[0].Text);
            _questionIndex++;

            if (questionIndex < _questionArray.Length - 1)
            {
                CallAssistant(_questionIndex);
            }
            else
            {
                _assistant.DeleteSession(_assistantId, _sessionId);
                Console.WriteLine("Session deleted!");
            }
        }
    }
}