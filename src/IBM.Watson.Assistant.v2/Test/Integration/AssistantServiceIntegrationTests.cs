/**
* (C) Copyright IBM Corp. 2017, 2022.
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
using System.Collections.Generic;

namespace IBM.Watson.Assistant.v2.IntegrationTests
{
    [TestClass]
    public class AssistantServiceIntegrationTests
    {
        private static string endpoint;
        private AssistantService service;
        private static string credentials = string.Empty;

        private static string assistantId;
        private static string sessionId;
        private readonly string inputString = "Hello";
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

        #region Stateless Message
        [TestMethod]
        public void StatelessMessage_Success()
        {
            MessageInputStateless input = new MessageInputStateless()
            {
                MessageType = MessageInput.MessageTypeEnumValue.TEXT,
                Text = inputString,
                Options = new MessageInputOptionsStateless()
                {
                    AlternateIntents = true
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var messageResult = service.MessageStateless(
                assistantId: assistantId,
                input: input
                );

            Assert.IsNotNull(messageResult.Response);
            Assert.IsTrue(messageResult.Result.Output.Generic[0].ResponseType == "text");
            Assert.IsTrue(messageResult.Result.Output.Generic[0].Text.Contains("Hello"));
            Assert.IsTrue(messageResult.Result.Output.Intents[0].Intent == "General_Greetings");
            Assert.IsNotNull(messageResult.Result.Context.Global.SessionId);
        }
        #endregion

        #region List Logs
        //[TestMethod]
        public void ListLogs_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listLogsResults = service.ListLogs(
                assistantId: assistantId,
                sort: "request_timestamp",
                filter: "request.input.text::\"Hello\"",
                pageLimit: 5
                );

            Assert.IsNotNull(listLogsResults.Response);
            Assert.IsTrue(listLogsResults.Result.Logs[0].Request.Input.Text.Contains("Hello"));
            Assert.IsTrue(listLogsResults.Result.Logs[0].Language == "en");
        }
        #endregion

        #region Delete User Data
        public void DeleteUserData_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var deleteUserDataResults = service.DeleteUserData(
                customerId: "{customerId}"
                );

            Assert.IsNotNull(deleteUserDataResults.Response);
        }
        #endregion

        #region Bulk Classify
        //[TestMethod]
        public void TestBulk_Classify()
        {
            service.WithHeader("X-Watson-Test", "1");
            List<BulkClassifyUtterance> bulkClassifyUtterances = new List<BulkClassifyUtterance>();
            BulkClassifyUtterance bulkClassifyUtterance = new BulkClassifyUtterance();
            bulkClassifyUtterance.Text = "text text";
            bulkClassifyUtterances.Add(bulkClassifyUtterance);
            var bulkClassifyResponse = service.BulkClassify("{skillId}", bulkClassifyUtterances);

            Assert.IsNotNull(bulkClassifyResponse);
        }
        #endregion

        #region Miscellaneous
        [TestMethod]
        public void TestRuntimeResponseGenericRuntimeResponseTypeChannelTransfer()
        {
            service.WithHeader("X-Watson-Test", "1");

            MessageInputStateless input = new MessageInputStateless();
            input.Text = "test sdk";
            input.MessageType = MessageInput.MessageTypeEnumValue.TEXT;

            var response = service.MessageStateless(
                assistantId: assistantId,
                input: input
                );
            Assert.IsNotNull(response);

            RuntimeResponseGenericRuntimeResponseTypeChannelTransfer
                runtimeResponseGenericRuntimeResponseTypeChannelTransfer =
                    (RuntimeResponseGenericRuntimeResponseTypeChannelTransfer)response.Result.Output.Generic[0];
            ChannelTransferInfo channelTransferInfo =
                runtimeResponseGenericRuntimeResponseTypeChannelTransfer.TransferInfo;
            Assert.IsNotNull(channelTransferInfo);
        }

        [TestMethod]
        public void TestRuntimeResponseGeneric()
        {
            service.WithHeader("X-Watson-Test", "1");

            string[] inputStrings = { "audio", "iframe", "video" };

            foreach (string inputMessage in inputStrings)
            {
                MessageInputStateless input = new MessageInputStateless();
                input.Text = inputMessage;

                var response = service.MessageStateless(
                assistantId: assistantId,
                input: input
                );

                Assert.IsNotNull(response);
                Assert.IsTrue(response.Result.Output.Generic[0].ResponseType.Contains(inputMessage));
            }
        }
        #endregion
    }
}
