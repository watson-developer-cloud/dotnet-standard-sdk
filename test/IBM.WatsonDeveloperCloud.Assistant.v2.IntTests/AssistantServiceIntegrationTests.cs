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

using IBM.WatsonDeveloperCloud.Assistant.v2.Model;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace IBM.WatsonDeveloperCloud.Assistant.v2.IntTests
{
    [TestClass]
    public class AssistantServiceIntegrationTests
    {
        private static string username;
        private static string password;
        private static string endpoint;
        private AssistantService service;
        private static string credentials = string.Empty;

        private static string assistantId;
        private static string sessionId;
        private readonly string inputString = "Hello";
        private readonly string versionDate = "2018-09-17";

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";
                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                var vcapServices = JObject.Parse(credentials);

                Credential credential = vcapCredentials.GetCredentialByname("assistant-sdk")[0].Credentials;
                endpoint = credential.Url;
                username = credential.Username;
                password = credential.Password;
                assistantId = credential.AssistantId;
            }
            #endregion

            service = new AssistantService(username, password, versionDate);
            service.SetEndpoint(endpoint);
        }

        #region Sessions
        [TestMethod]
        public void CreateDeleteSession_Success()
        {
            var createSessionResult = service.CreateSession(assistantId);
            sessionId = createSessionResult.SessionId;

            var deleteSessionResult = service.DeleteSession(assistantId, sessionId);
            sessionId = string.Empty;

            Assert.IsNotNull(createSessionResult);
            Assert.IsNotNull(deleteSessionResult);
            Assert.IsTrue(!string.IsNullOrEmpty(createSessionResult.SessionId));
        }
        #endregion

        #region Message
        [TestMethod]
        public void Message_Success()
        {
            var createSessionResult = service.CreateSession(assistantId);
            sessionId = createSessionResult.SessionId;

            MessageRequest request = new MessageRequest()
            {
                Input = new MessageInput()
                {
                    MessageType = MessageInput.MessageTypeEnum.TEXT,
                    Text = inputString,
                    Options = new MessageInputOptions()
                    {
                        ReturnContext = true,
                        AlternateIntents = true
                    }
                }
            };
            var messageResult = service.Message(assistantId, sessionId, request);

            var deleteSessionResult = service.DeleteSession(assistantId, sessionId);
            sessionId = string.Empty;

            Assert.IsNotNull(createSessionResult);
            Assert.IsNotNull(messageResult);
            Assert.IsNotNull(deleteSessionResult);
            Assert.IsTrue(!string.IsNullOrEmpty(createSessionResult.SessionId));
        }
        #endregion
    }
}
