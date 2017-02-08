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

namespace IBM.WatsonDeveloperCloud.Conversation.Example
{
    public class Example
    {
        static void Main(string[] args)
        {
            string _username = "97a37439-9be0-4b01-8145-531c983dd34f";
            string _password = "SOuS08uQ8Z6v";
            string _workspaceID = "b42ee794-c019-4a0d-acd2-9e4d1d016767";

            ConversationServiceExample _conversationExample = new ConversationServiceExample(_username, _password, _workspaceID);
            Console.ReadKey();
        }
    }
}
