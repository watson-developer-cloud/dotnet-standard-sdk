/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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


using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v2.Model;
using System;

namespace IBM.Watson.Assistant.v2.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        string versionDate = "{versionDate}";
        string assistantId = "{assistantId}";
        string sessionId;
        string inputString = "hello";

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.CreateSession();
            example.Message();
            example.DeleteSession();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Sessions
        public void CreateSession()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateSession(
                assistantId: assistantId
                );
            sessionId = result.Result.SessionId;

            Console.WriteLine(result.Response);
        }

        public void DeleteSession()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteSession(
                assistantId: assistantId,
                sessionId: sessionId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Message
        public void Message()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            AssistantService service = new AssistantService(versionDate, config);
            service.SetEndpoint(url);

            MessageInput input = new MessageInput()
            {
                MessageType = MessageInput.MessageTypeEnumValue.TEXT,
                Text = inputString,
                Options = new MessageInputOptions()
                {
                    ReturnContext = true,
                    AlternateIntents = true
                }
            };

            var result = service.Message(
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
