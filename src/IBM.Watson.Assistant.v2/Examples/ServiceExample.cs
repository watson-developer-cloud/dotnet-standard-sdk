/**
* (C) Copyright IBM Corp. 2019, 2023.
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
using System.Collections.Generic;
using System;

namespace IBM.Watson.Assistant.v2.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
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
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2020-04-01", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.CreateSession(
                assistantId: "{assistantId}"
                );

            Console.WriteLine(result.Response);

            sessionId = result.Result.SessionId;
        }

        public void DeleteSession()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2020-04-01", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteSession(
                assistantId: "{assistantId}",
                sessionId: "{sessionId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Message
        public void Message()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2020-04-01", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Message(
                assistantId: "{assistantId}",
                sessionId: "{sessionId}",
                input: new MessageInput()
                {
                    Text = "Hello"
                }
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Message with context
        public void MessageWithContext()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2020-04-01", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            MessageContextSkills skills = new MessageContextSkills();
            Dictionary<string, object> userDefinedDictionary = new Dictionary<string, object>();

            userDefinedDictionary.Add("account_number", "123456");

            var result = service.Message(
                assistantId: "{assistantId}",
                sessionId: "{sessionId}",
                input: new MessageInput()
                {
                    Text = "Hello"
                },
                context: new MessageContext()
                {
                    Global = new MessageContextGlobal()
                    {
                        System = new MessageContextGlobalSystem()
                        {
                            UserId = "my_user_id"
                        }
                    },
                    Skills = skills
                }
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Message Stateless
        public void MessageStateless()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            AssistantService service = new AssistantService("2020-04-01", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.MessageStateless(
                assistantId: "{assistantId}",
                input: new MessageInputStateless()
                {
                    Text = "Hello"
                }
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
