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

using IBM.Watson.Assistant.v2.Model;
using IBM.Cloud.SDK.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using IBM.Cloud.SDK.Core.Authentication.Iam;

namespace IBM.Watson.Assistant.v2.IntegrationTests
{
    [TestClass]
    public class AssistantServiceIntegrationConfigTests
    {
        private static string apikey;
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
            Utility.LoadExternalCredentials();
            var apikey = Environment.GetEnvironmentVariable("ASSISTANT_APIKEY");
            var url = Environment.GetEnvironmentVariable("ASSISTANT_URL");
            IamConfig iamConfig = new IamConfig(
                apikey: apikey
                );
            service = new AssistantService(versionDate, iamConfig);
            service.SetEndpoint(url);
            assistantId = Environment.GetEnvironmentVariable("ASSISTANT_ASSISTANT_ID");
        }

        #region Sessions
        [TestMethod]
        public void CreateDeleteSession_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createSessionResult = service.CreateSession(
                assistantId: assistantId
                );
            sessionId = createSessionResult.Result.SessionId;

            service.WithHeader("X-Watson-Test", "1");
            var deleteSessionResult = service.DeleteSession(
                assistantId: assistantId,
                sessionId: sessionId
                );

            sessionId = string.Empty;

            Assert.IsNotNull(createSessionResult);
            Assert.IsNotNull(deleteSessionResult);
            Assert.IsTrue(!string.IsNullOrEmpty(createSessionResult.Result.SessionId));
        }
        #endregion

        #region Message
        [TestMethod]
        public void Message_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createSessionResult = service.CreateSession(
                assistantId: assistantId
                );
            sessionId = createSessionResult.Result.SessionId;

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

            service.WithHeader("X-Watson-Test", "1");
            var messageResult = service.Message(
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteSessionResult = service.DeleteSession(
                assistantId: assistantId,
                sessionId: sessionId
                );
            sessionId = string.Empty;

            Assert.IsNotNull(createSessionResult);
            Assert.IsNotNull(messageResult);
            Assert.IsNotNull(deleteSessionResult);
            Assert.IsTrue(!string.IsNullOrEmpty(createSessionResult.Result.SessionId));
        }
        #endregion
    }
}
