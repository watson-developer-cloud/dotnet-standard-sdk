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
        private readonly string inputString0 = "Hello";
        private readonly string inputString1 = "Are you open on christmas?";
        private readonly string inputString2 = "I'd like to make an appointment.";
        private readonly string inputString3 = "Tomorrow at 3pm";
        private readonly string inputString4 = "Make that thursday at 2pm";
        private readonly string inputString5 = "Who did watson beat in jeopardy?";
        private readonly string versionDate = "2019-02-28";

        [TestInitialize]
        public void Setup()
        {
            service = new AssistantService(versionDate);
            var creds = CredentialUtils.GetServiceProperties("assistant");
            creds.TryGetValue("ASSISTANT_ID", out assistantId);
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
                Text = inputString0,
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

        #region MultiMessage
        [TestMethod]
        public void MultiMessage_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createSessionResult = service.CreateSession(
                assistantId: assistantId
                );
            sessionId = createSessionResult.Result.SessionId;

            MessageInput input = new MessageInput()
            {
                MessageType = MessageInput.MessageTypeEnumValue.TEXT,
                Text = inputString0,
                Options = new MessageInputOptions()
                {
                    ReturnContext = true,
                    AlternateIntents = true
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var messageResult0 = service.Message(
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
                );



            input.Text = inputString1;

            service.WithHeader("X-Watson-Test", "1");
            var messageResult1 = service.Message(
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
                );

            input.Text = inputString2;

            service.WithHeader("X-Watson-Test", "1");
            var messageResult2 = service.Message(
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
                );

            input.Text = inputString3;

            service.WithHeader("X-Watson-Test", "1");
            var messageResult3 = service.Message(
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
                );

            input.Text = inputString4;

            service.WithHeader("X-Watson-Test", "1");
            var messageResult4 = service.Message(
                assistantId: assistantId,
                sessionId: sessionId,
                input: input
                );

            input.Text = inputString5;

            service.WithHeader("X-Watson-Test", "1");
            var messageResult5 = service.Message(
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

            Assert.IsNotNull(createSessionResult.Result.SessionId);
            Assert.IsNotNull(messageResult0.Result.Output);
            Assert.IsNotNull(messageResult1.Result.Output);
            Assert.IsNotNull(messageResult2.Result.Output);
            Assert.IsNotNull(messageResult3.Result.Output);
            Assert.IsNotNull(messageResult4.Result.Output);
            Assert.IsNotNull(messageResult5.Result.Output);
            Assert.IsTrue(messageResult5.Result.Output.Generic[0].ResponseType == "search");
            Assert.IsTrue(deleteSessionResult.StatusCode == 200);
            Assert.IsTrue(!string.IsNullOrEmpty(createSessionResult.Result.SessionId));
        }
        #endregion
    }
}