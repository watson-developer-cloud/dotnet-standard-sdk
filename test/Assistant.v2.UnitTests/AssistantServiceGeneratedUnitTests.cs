/**
* (C) Copyright IBM Corp. 2018, 2020.
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

using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.Assistant.v2.Model;
using NSubstitute;
using System;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

namespace IBM.Watson.Assistant.v2.UnitTests
{
    [TestClass]
    public class AssistantServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            AssistantService service = new AssistantService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            AssistantService service = new AssistantService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("ASSISTANT_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("ASSISTANT_URL");
            System.Environment.SetEnvironmentVariable("ASSISTANT_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("ASSISTANT_URL", "http://www.url.com");
            AssistantService service = Substitute.For<AssistantService>("versionDate");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("ASSISTANT_URL", url);
            System.Environment.SetEnvironmentVariable("ASSISTANT_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            AssistantService service = new AssistantService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            AssistantService service = new AssistantService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            AssistantService service = new AssistantService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("ASSISTANT_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("ASSISTANT_URL");
            System.Environment.SetEnvironmentVariable("ASSISTANT_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("ASSISTANT_URL", null);
            AssistantService service = Substitute.For<AssistantService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/assistant/api");
            System.Environment.SetEnvironmentVariable("ASSISTANT_URL", url);
            System.Environment.SetEnvironmentVariable("ASSISTANT_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestCaptureGroupModel()
        {

            var CaptureGroupLocation = new List<long?> { 38 };
            CaptureGroup testRequestModel = new CaptureGroup()
            {
                Group = "testString",
                Location = CaptureGroupLocation
            };

            Assert.IsTrue(testRequestModel.Group == "testString");
            Assert.IsTrue(testRequestModel.Location == CaptureGroupLocation);
        }

        [TestMethod]
        public void TestMessageContextModel()
        {
            MessageContextSkills MessageContextSkillsModel = new MessageContextSkills()
            {
            };

            MessageContextGlobalSystem MessageContextGlobalSystemModel = new MessageContextGlobalSystem()
            {
                Timezone = "testString",
                UserId = "testString",
                TurnCount = 38
            };

            MessageContextGlobal MessageContextGlobalModel = new MessageContextGlobal()
            {
                System = MessageContextGlobalSystemModel
            };

            MessageContext testRequestModel = new MessageContext()
            {
                Global = MessageContextGlobalModel,
                Skills = MessageContextSkillsModel
            };

            Assert.IsTrue(testRequestModel.Global == MessageContextGlobalModel);
            Assert.IsTrue(testRequestModel.Skills == MessageContextSkillsModel);
        }

        [TestMethod]
        public void TestMessageContextGlobalModel()
        {
            MessageContextGlobalSystem MessageContextGlobalSystemModel = new MessageContextGlobalSystem()
            {
                Timezone = "testString",
                UserId = "testString",
                TurnCount = 38
            };

            MessageContextGlobal testRequestModel = new MessageContextGlobal()
            {
                System = MessageContextGlobalSystemModel
            };

            Assert.IsTrue(testRequestModel.System == MessageContextGlobalSystemModel);
        }

        [TestMethod]
        public void TestMessageContextGlobalSystemModel()
        {

            MessageContextGlobalSystem testRequestModel = new MessageContextGlobalSystem()
            {
                Timezone = "testString",
                UserId = "testString",
                TurnCount = 38
            };

            Assert.IsTrue(testRequestModel.Timezone == "testString");
            Assert.IsTrue(testRequestModel.UserId == "testString");
            Assert.IsTrue(testRequestModel.TurnCount == 38);
        }

        [TestMethod]
        public void TestMessageContextSkillModel()
        {

            var MessageContextSkillUserDefined = new Dictionary<string, object>();
            var MessageContextSkillSystem = new Dictionary<string, object>();
            MessageContextSkill testRequestModel = new MessageContextSkill()
            {
                UserDefined = MessageContextSkillUserDefined,
                System = MessageContextSkillSystem
            };

            Assert.IsTrue(testRequestModel.UserDefined == MessageContextSkillUserDefined);
            Assert.IsTrue(testRequestModel.System == MessageContextSkillSystem);
        }

        [TestMethod]
        public void TestMessageInputModel()
        {
            var CaptureGroupLocation = new List<long?> { 38 };
            CaptureGroup CaptureGroupModel = new CaptureGroup()
            {
                Group = "testString",
                Location = CaptureGroupLocation
            };

            var RuntimeEntityLocation = new List<long?> { 38 };
            var RuntimeEntityMetadata = new Dictionary<string, object>();
            var RuntimeEntityGroups = new List<CaptureGroup> { CaptureGroupModel };
            RuntimeEntity RuntimeEntityModel = new RuntimeEntity()
            {
                Entity = "testString",
                Location = RuntimeEntityLocation,
                Value = "testString",
                Confidence = 72.5f,
                Metadata = RuntimeEntityMetadata,
                Groups = RuntimeEntityGroups
            };

            RuntimeIntent RuntimeIntentModel = new RuntimeIntent()
            {
                Intent = "testString",
                Confidence = 72.5f
            };

            MessageInputOptions MessageInputOptionsModel = new MessageInputOptions()
            {
                Debug = true,
                Restart = true,
                AlternateIntents = true,
                ReturnContext = true
            };

            var MessageInputIntents = new List<RuntimeIntent> { RuntimeIntentModel };
            var MessageInputEntities = new List<RuntimeEntity> { RuntimeEntityModel };
            MessageInput testRequestModel = new MessageInput()
            {
                MessageType = "text",
                Text = "testString",
                Options = MessageInputOptionsModel,
                Intents = MessageInputIntents,
                Entities = MessageInputEntities,
                SuggestionId = "testString"
            };

            Assert.IsTrue(testRequestModel.MessageType == "text");
            Assert.IsTrue(testRequestModel.Text == "testString");
            Assert.IsTrue(testRequestModel.Options == MessageInputOptionsModel);
            Assert.IsTrue(testRequestModel.Intents == MessageInputIntents);
            Assert.IsTrue(testRequestModel.Entities == MessageInputEntities);
            Assert.IsTrue(testRequestModel.SuggestionId == "testString");
        }

        [TestMethod]
        public void TestMessageInputOptionsModel()
        {

            MessageInputOptions testRequestModel = new MessageInputOptions()
            {
                Debug = true,
                Restart = true,
                AlternateIntents = true,
                ReturnContext = true
            };

            Assert.IsTrue(testRequestModel.Debug == true);
            Assert.IsTrue(testRequestModel.Restart == true);
            Assert.IsTrue(testRequestModel.AlternateIntents == true);
            Assert.IsTrue(testRequestModel.ReturnContext == true);
        }

        [TestMethod]
        public void TestRuntimeEntityModel()
        {
            var CaptureGroupLocation = new List<long?> { 38 };
            CaptureGroup CaptureGroupModel = new CaptureGroup()
            {
                Group = "testString",
                Location = CaptureGroupLocation
            };

            var RuntimeEntityLocation = new List<long?> { 38 };
            var RuntimeEntityMetadata = new Dictionary<string, object>();
            var RuntimeEntityGroups = new List<CaptureGroup> { CaptureGroupModel };
            RuntimeEntity testRequestModel = new RuntimeEntity()
            {
                Entity = "testString",
                Location = RuntimeEntityLocation,
                Value = "testString",
                Confidence = 72.5f,
                Metadata = RuntimeEntityMetadata,
                Groups = RuntimeEntityGroups
            };

            Assert.IsTrue(testRequestModel.Entity == "testString");
            Assert.IsTrue(testRequestModel.Location == RuntimeEntityLocation);
            Assert.IsTrue(testRequestModel.Value == "testString");
            Assert.IsTrue(testRequestModel.Confidence == 72.5f);
            Assert.IsTrue(testRequestModel.Metadata == RuntimeEntityMetadata);
            Assert.IsTrue(testRequestModel.Groups == RuntimeEntityGroups);
        }

        [TestMethod]
        public void TestRuntimeIntentModel()
        {

            RuntimeIntent testRequestModel = new RuntimeIntent()
            {
                Intent = "testString",
                Confidence = 72.5f
            };

            Assert.IsTrue(testRequestModel.Intent == "testString");
            Assert.IsTrue(testRequestModel.Confidence == 72.5f);
        }

        [TestMethod]
        public void TestMessageContextSkillsModel()
        {

            MessageContextSkills testRequestModel = new MessageContextSkills()
            {
            };

        }

        [TestMethod]
        public void TestTestCreateSessionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            AssistantService service = new AssistantService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'session_id': 'SessionId'}";
            var response = new DetailedResponse<SessionResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<SessionResponse>(responseJson),
                StatusCode = 201
            };

            string assistantId = "testString";

            request.As<SessionResponse>().Returns(Task.FromResult(response));

            var result = service.CreateSession(assistantId: assistantId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v2/assistants/{assistantId}/sessions";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteSessionAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            AssistantService service = new AssistantService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string assistantId = "testString";
            string sessionId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteSession(assistantId: assistantId, sessionId: sessionId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v2/assistants/{assistantId}/sessions/{sessionId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestMessageAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            AssistantService service = new AssistantService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'output': {'generic': [{'response_type': 'text', 'text': 'Text', 'time': 4, 'typing': true, 'source': 'Source', 'title': 'Title', 'description': 'Description', 'preference': 'dropdown', 'options': [{'label': 'Label', 'value': {'input': {'message_type': 'text', 'text': 'Text', 'options': {'debug': false, 'restart': false, 'alternate_intents': true, 'return_context': false}, 'intents': [{'intent': 'Intent', 'confidence': 10}], 'entities': [{'entity': 'Entity', 'location': [8], 'value': 'Value', 'confidence': 10, 'metadata': {}, 'groups': [{'group': 'Group', 'location': [8]}]}], 'suggestion_id': 'SuggestionId'}}}], 'message_to_human_agent': 'MessageToHumanAgent', 'topic': 'Topic', 'suggestions': [{'label': 'Label', 'value': {'input': {'message_type': 'text', 'text': 'Text', 'options': {'debug': false, 'restart': false, 'alternate_intents': true, 'return_context': false}, 'intents': [{'intent': 'Intent', 'confidence': 10}], 'entities': [{'entity': 'Entity', 'location': [8], 'value': 'Value', 'confidence': 10, 'metadata': {}, 'groups': [{'group': 'Group', 'location': [8]}]}], 'suggestion_id': 'SuggestionId'}}, 'output': {}}], 'header': 'Header', 'results': [{'id': 'Id', 'result_metadata': {'confidence': 10, 'score': 5}, 'body': 'Body', 'title': 'Title', 'url': 'Url', 'highlight': {'body': ['Body'], 'title': ['Title'], 'url': ['Url']}}]}], 'intents': [{'intent': 'Intent', 'confidence': 10}], 'entities': [{'entity': 'Entity', 'location': [8], 'value': 'Value', 'confidence': 10, 'metadata': {}, 'groups': [{'group': 'Group', 'location': [8]}]}], 'actions': [{'name': 'Name', 'type': 'client', 'parameters': {}, 'result_variable': 'ResultVariable', 'credentials': 'Credentials'}], 'debug': {'nodes_visited': [{'dialog_node': 'DialogNode', 'title': 'Title', 'conditions': 'Conditions'}], 'log_messages': [{'level': 'info', 'message': 'Message'}], 'branch_exited': true, 'branch_exited_reason': 'completed'}, 'user_defined': {}}, 'context': {'global': {'system': {'timezone': 'Timezone', 'user_id': 'UserId', 'turn_count': 9}}, 'skills': {}}}";
            var response = new DetailedResponse<MessageResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<MessageResponse>(responseJson),
                StatusCode = 200
            };

            MessageContextSkills MessageContextSkillsModel = new MessageContextSkills()
            {
            };

            MessageContextGlobalSystem MessageContextGlobalSystemModel = new MessageContextGlobalSystem()
            {
                Timezone = "testString",
                UserId = "testString",
                TurnCount = 38
            };

            MessageContextGlobal MessageContextGlobalModel = new MessageContextGlobal()
            {
                System = MessageContextGlobalSystemModel
            };

            MessageContext MessageContextModel = new MessageContext()
            {
                Global = MessageContextGlobalModel,
                Skills = MessageContextSkillsModel
            };

            var CaptureGroupLocation = new List<long?> { 38 };
            CaptureGroup CaptureGroupModel = new CaptureGroup()
            {
                Group = "testString",
                Location = CaptureGroupLocation
            };

            var RuntimeEntityLocation = new List<long?> { 38 };
            var RuntimeEntityMetadata = new Dictionary<string, object>();
            var RuntimeEntityGroups = new List<CaptureGroup> { CaptureGroupModel };
            RuntimeEntity RuntimeEntityModel = new RuntimeEntity()
            {
                Entity = "testString",
                Location = RuntimeEntityLocation,
                Value = "testString",
                Confidence = 72.5f,
                Metadata = RuntimeEntityMetadata,
                Groups = RuntimeEntityGroups
            };

            RuntimeIntent RuntimeIntentModel = new RuntimeIntent()
            {
                Intent = "testString",
                Confidence = 72.5f
            };

            MessageInputOptions MessageInputOptionsModel = new MessageInputOptions()
            {
                Debug = true,
                Restart = true,
                AlternateIntents = true,
                ReturnContext = true
            };

            var MessageInputIntents = new List<RuntimeIntent> { RuntimeIntentModel };
            var MessageInputEntities = new List<RuntimeEntity> { RuntimeEntityModel };
            MessageInput MessageInputModel = new MessageInput()
            {
                MessageType = "text",
                Text = "testString",
                Options = MessageInputOptionsModel,
                Intents = MessageInputIntents,
                Entities = MessageInputEntities,
                SuggestionId = "testString"
            };
            string assistantId = "testString";
            string sessionId = "testString";

            request.As<MessageResponse>().Returns(Task.FromResult(response));

            var result = service.Message(assistantId: assistantId, sessionId: sessionId, input: MessageInputModel, context: MessageContextModel);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v2/assistants/{assistantId}/sessions/{sessionId}/message";
            client.Received().PostAsync(messageUrl);
        }

    }
}
