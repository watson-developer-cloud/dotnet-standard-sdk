/**
* (C) Copyright IBM Corp. 2020.
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
            AssistantService service = Substitute.For<AssistantService>("testString");
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
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            AssistantService service = Substitute.For<AssistantService>("testString", "test_service");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/assistant/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
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
                TurnCount = 38,
                Locale = "en-us",
                ReferenceTime = "testString"
            };

            Assert.IsTrue(testRequestModel.Timezone == "testString");
            Assert.IsTrue(testRequestModel.UserId == "testString");
            Assert.IsTrue(testRequestModel.TurnCount == 38);
            Assert.IsTrue(testRequestModel.Locale == "en-us");
            Assert.IsTrue(testRequestModel.ReferenceTime == "testString");
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

            var RuntimeEntityLocation = new List<long?> { 38 };
            var RuntimeEntityMetadata = new Dictionary<string, object>();
            var RuntimeEntityGroups = new List<CaptureGroup> { CaptureGroupModel };
            var RuntimeEntityAlternatives = new List<RuntimeEntityAlternative> { RuntimeEntityAlternativeModel };
            RuntimeEntity testRequestModel = new RuntimeEntity()
            {
                Entity = "testString",
                Location = RuntimeEntityLocation,
                Value = "testString",
                Confidence = 72.5f,
                Metadata = RuntimeEntityMetadata,
                Groups = RuntimeEntityGroups,
                Interpretation = RuntimeEntityInterpretationModel,
                Alternatives = RuntimeEntityAlternatives,
                Role = RuntimeEntityRoleModel
            };

            Assert.IsTrue(testRequestModel.Entity == "testString");
            Assert.IsTrue(testRequestModel.Location == RuntimeEntityLocation);
            Assert.IsTrue(testRequestModel.Value == "testString");
            Assert.IsTrue(testRequestModel.Confidence == 72.5f);
            Assert.IsTrue(testRequestModel.Metadata == RuntimeEntityMetadata);
            Assert.IsTrue(testRequestModel.Groups == RuntimeEntityGroups);
            Assert.IsTrue(testRequestModel.Interpretation == RuntimeEntityInterpretationModel);
            Assert.IsTrue(testRequestModel.Alternatives == RuntimeEntityAlternatives);
            Assert.IsTrue(testRequestModel.Role == RuntimeEntityRoleModel);
        }

        [TestMethod]
        public void TestRuntimeEntityAlternativeModel()
        {

            RuntimeEntityAlternative testRequestModel = new RuntimeEntityAlternative()
            {
                Value = "testString",
                Confidence = 72.5f
            };

            Assert.IsTrue(testRequestModel.Value == "testString");
            Assert.IsTrue(testRequestModel.Confidence == 72.5f);
        }

        [TestMethod]
        public void TestRuntimeEntityInterpretationModel()
        {

            RuntimeEntityInterpretation testRequestModel = new RuntimeEntityInterpretation()
            {
                CalendarType = "testString",
                DatetimeLink = "testString",
                Festival = "testString",
                Granularity = "day",
                RangeLink = "testString",
                RangeModifier = "testString",
                RelativeDay = 72.5f,
                RelativeMonth = 72.5f,
                RelativeWeek = 72.5f,
                RelativeWeekend = 72.5f,
                RelativeYear = 72.5f,
                SpecificDay = 72.5f,
                SpecificDayOfWeek = "testString",
                SpecificMonth = 72.5f,
                SpecificQuarter = 72.5f,
                SpecificYear = 72.5f,
                NumericValue = 72.5f,
                Subtype = "testString",
                PartOfDay = "testString",
                RelativeHour = 72.5f,
                RelativeMinute = 72.5f,
                RelativeSecond = 72.5f,
                SpecificHour = 72.5f,
                SpecificMinute = 72.5f,
                SpecificSecond = 72.5f,
                Timezone = "testString"
            };

            Assert.IsTrue(testRequestModel.CalendarType == "testString");
            Assert.IsTrue(testRequestModel.DatetimeLink == "testString");
            Assert.IsTrue(testRequestModel.Festival == "testString");
            Assert.IsTrue(testRequestModel.Granularity == "day");
            Assert.IsTrue(testRequestModel.RangeLink == "testString");
            Assert.IsTrue(testRequestModel.RangeModifier == "testString");
            Assert.IsTrue(testRequestModel.RelativeDay == 72.5f);
            Assert.IsTrue(testRequestModel.RelativeMonth == 72.5f);
            Assert.IsTrue(testRequestModel.RelativeWeek == 72.5f);
            Assert.IsTrue(testRequestModel.RelativeWeekend == 72.5f);
            Assert.IsTrue(testRequestModel.RelativeYear == 72.5f);
            Assert.IsTrue(testRequestModel.SpecificDay == 72.5f);
            Assert.IsTrue(testRequestModel.SpecificDayOfWeek == "testString");
            Assert.IsTrue(testRequestModel.SpecificMonth == 72.5f);
            Assert.IsTrue(testRequestModel.SpecificQuarter == 72.5f);
            Assert.IsTrue(testRequestModel.SpecificYear == 72.5f);
            Assert.IsTrue(testRequestModel.NumericValue == 72.5f);
            Assert.IsTrue(testRequestModel.Subtype == "testString");
            Assert.IsTrue(testRequestModel.PartOfDay == "testString");
            Assert.IsTrue(testRequestModel.RelativeHour == 72.5f);
            Assert.IsTrue(testRequestModel.RelativeMinute == 72.5f);
            Assert.IsTrue(testRequestModel.RelativeSecond == 72.5f);
            Assert.IsTrue(testRequestModel.SpecificHour == 72.5f);
            Assert.IsTrue(testRequestModel.SpecificMinute == 72.5f);
            Assert.IsTrue(testRequestModel.SpecificSecond == 72.5f);
            Assert.IsTrue(testRequestModel.Timezone == "testString");
        }

        [TestMethod]
        public void TestRuntimeEntityRoleModel()
        {

            RuntimeEntityRole testRequestModel = new RuntimeEntityRole()
            {
                Type = "date_from"
            };

            Assert.IsTrue(testRequestModel.Type == "date_from");
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

            var version = "testString";
            service.Version = version;

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

            request.Received().WithArgument("version", "testString");

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

            var version = "testString";
            service.Version = version;

            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string assistantId = "testString";
            string sessionId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteSession(assistantId: assistantId, sessionId: sessionId);

            request.Received().WithArgument("version", "testString");

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

            var version = "testString";
            service.Version = version;

            var responseJson = "{'output': {'generic': [{'response_type': 'text', 'text': 'Text', 'time': 4, 'typing': true, 'source': 'Source', 'title': 'Title', 'description': 'Description', 'preference': 'dropdown', 'options': [{'label': 'Label', 'value': {'input': {'message_type': 'text', 'text': 'Text', 'options': {'debug': false, 'restart': false, 'alternate_intents': true, 'return_context': false}, 'intents': [{'intent': 'Intent', 'confidence': 10}], 'entities': [{'entity': 'Entity', 'location': [8], 'value': 'Value', 'confidence': 10, 'metadata': {'mapKey': 'unknown property type: Inner'}, 'groups': [{'group': 'Group', 'location': [8]}], 'interpretation': {'calendar_type': 'CalendarType', 'datetime_link': 'DatetimeLink', 'festival': 'Festival', 'granularity': 'day', 'range_link': 'RangeLink', 'range_modifier': 'RangeModifier', 'relative_day': 11, 'relative_month': 13, 'relative_week': 12, 'relative_weekend': 15, 'relative_year': 12, 'specific_day': 11, 'specific_day_of_week': 'SpecificDayOfWeek', 'specific_month': 13, 'specific_quarter': 15, 'specific_year': 12, 'numeric_value': 12, 'subtype': 'Subtype', 'part_of_day': 'PartOfDay', 'relative_hour': 12, 'relative_minute': 14, 'relative_second': 14, 'specific_hour': 12, 'specific_minute': 14, 'specific_second': 14, 'timezone': 'Timezone'}, 'alternatives': [{'value': 'Value', 'confidence': 10}], 'role': {'type': 'date_from'}}], 'suggestion_id': 'SuggestionId'}}}], 'message_to_human_agent': 'MessageToHumanAgent', 'topic': 'Topic', 'suggestions': [{'label': 'Label', 'value': {'input': {'message_type': 'text', 'text': 'Text', 'options': {'debug': false, 'restart': false, 'alternate_intents': true, 'return_context': false}, 'intents': [{'intent': 'Intent', 'confidence': 10}], 'entities': [{'entity': 'Entity', 'location': [8], 'value': 'Value', 'confidence': 10, 'metadata': {'mapKey': 'unknown property type: Inner'}, 'groups': [{'group': 'Group', 'location': [8]}], 'interpretation': {'calendar_type': 'CalendarType', 'datetime_link': 'DatetimeLink', 'festival': 'Festival', 'granularity': 'day', 'range_link': 'RangeLink', 'range_modifier': 'RangeModifier', 'relative_day': 11, 'relative_month': 13, 'relative_week': 12, 'relative_weekend': 15, 'relative_year': 12, 'specific_day': 11, 'specific_day_of_week': 'SpecificDayOfWeek', 'specific_month': 13, 'specific_quarter': 15, 'specific_year': 12, 'numeric_value': 12, 'subtype': 'Subtype', 'part_of_day': 'PartOfDay', 'relative_hour': 12, 'relative_minute': 14, 'relative_second': 14, 'specific_hour': 12, 'specific_minute': 14, 'specific_second': 14, 'timezone': 'Timezone'}, 'alternatives': [{'value': 'Value', 'confidence': 10}], 'role': {'type': 'date_from'}}], 'suggestion_id': 'SuggestionId'}}, 'output': {'mapKey': 'unknown property type: Inner'}}], 'header': 'Header', 'results': [{'id': 'Id', 'result_metadata': {'confidence': 10, 'score': 5}, 'body': 'Body', 'title': 'Title', 'url': 'Url', 'highlight': {'body': ['Body'], 'title': ['Title'], 'url': ['Url']}}]}], 'intents': [{'intent': 'Intent', 'confidence': 10}], 'entities': [{'entity': 'Entity', 'location': [8], 'value': 'Value', 'confidence': 10, 'metadata': {'mapKey': 'unknown property type: Inner'}, 'groups': [{'group': 'Group', 'location': [8]}], 'interpretation': {'calendar_type': 'CalendarType', 'datetime_link': 'DatetimeLink', 'festival': 'Festival', 'granularity': 'day', 'range_link': 'RangeLink', 'range_modifier': 'RangeModifier', 'relative_day': 11, 'relative_month': 13, 'relative_week': 12, 'relative_weekend': 15, 'relative_year': 12, 'specific_day': 11, 'specific_day_of_week': 'SpecificDayOfWeek', 'specific_month': 13, 'specific_quarter': 15, 'specific_year': 12, 'numeric_value': 12, 'subtype': 'Subtype', 'part_of_day': 'PartOfDay', 'relative_hour': 12, 'relative_minute': 14, 'relative_second': 14, 'specific_hour': 12, 'specific_minute': 14, 'specific_second': 14, 'timezone': 'Timezone'}, 'alternatives': [{'value': 'Value', 'confidence': 10}], 'role': {'type': 'date_from'}}], 'actions': [{'name': 'Name', 'type': 'client', 'parameters': {'mapKey': 'unknown property type: Inner'}, 'result_variable': 'ResultVariable', 'credentials': 'Credentials'}], 'debug': {'nodes_visited': [{'dialog_node': 'DialogNode', 'title': 'Title', 'conditions': 'Conditions'}], 'log_messages': [{'level': 'info', 'message': 'Message'}], 'branch_exited': true, 'branch_exited_reason': 'completed'}, 'user_defined': {'mapKey': 'unknown property type: Inner'}}, 'context': {'global': {'system': {'timezone': 'Timezone', 'user_id': 'UserId', 'turn_count': 9, 'locale': 'en-us', 'reference_time': 'ReferenceTime'}}, 'skills': {}}}";
            var response = new DetailedResponse<MessageResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<MessageResponse>(responseJson),
                StatusCode = 200
            };

            var CaptureGroupLocation = new List<long?> { 38 };
            CaptureGroup CaptureGroupModel = new CaptureGroup()
            {
                Group = "testString",
                Location = CaptureGroupLocation
            };

            RuntimeEntityAlternative RuntimeEntityAlternativeModel = new RuntimeEntityAlternative()
            {
                Value = "testString",
                Confidence = 72.5f
            };

            RuntimeEntityInterpretation RuntimeEntityInterpretationModel = new RuntimeEntityInterpretation()
            {
                CalendarType = "testString",
                DatetimeLink = "testString",
                Festival = "testString",
                Granularity = "day",
                RangeLink = "testString",
                RangeModifier = "testString",
                RelativeDay = 72.5f,
                RelativeMonth = 72.5f,
                RelativeWeek = 72.5f,
                RelativeWeekend = 72.5f,
                RelativeYear = 72.5f,
                SpecificDay = 72.5f,
                SpecificDayOfWeek = "testString",
                SpecificMonth = 72.5f,
                SpecificQuarter = 72.5f,
                SpecificYear = 72.5f,
                NumericValue = 72.5f,
                Subtype = "testString",
                PartOfDay = "testString",
                RelativeHour = 72.5f,
                RelativeMinute = 72.5f,
                RelativeSecond = 72.5f,
                SpecificHour = 72.5f,
                SpecificMinute = 72.5f,
                SpecificSecond = 72.5f,
                Timezone = "testString"
            };

            RuntimeEntityRole RuntimeEntityRoleModel = new RuntimeEntityRole()
            {
                Type = "date_from"
            };

            MessageInputOptions MessageInputOptionsModel = new MessageInputOptions()
            {
                Debug = true,
                Restart = true,
                AlternateIntents = true,
                ReturnContext = true
            };

            var RuntimeEntityLocation = new List<long?> { 38 };
            var RuntimeEntityMetadata = new Dictionary<string, object>();
            var RuntimeEntityGroups = new List<CaptureGroup> { CaptureGroupModel };
            var RuntimeEntityAlternatives = new List<RuntimeEntityAlternative> { RuntimeEntityAlternativeModel };
            RuntimeEntity RuntimeEntityModel = new RuntimeEntity()
            {
                Entity = "testString",
                Location = RuntimeEntityLocation,
                Value = "testString",
                Confidence = 72.5f,
                Metadata = RuntimeEntityMetadata,
                Groups = RuntimeEntityGroups,
                Interpretation = RuntimeEntityInterpretationModel,
                Alternatives = RuntimeEntityAlternatives,
                Role = RuntimeEntityRoleModel
            };

            RuntimeIntent RuntimeIntentModel = new RuntimeIntent()
            {
                Intent = "testString",
                Confidence = 72.5f
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

            MessageContextGlobalSystem MessageContextGlobalSystemModel = new MessageContextGlobalSystem()
            {
                Timezone = "testString",
                UserId = "testString",
                TurnCount = 38,
                Locale = "en-us",
                ReferenceTime = "testString"
            };

            var MessageContextSkillUserDefined = new Dictionary<string, object>();
            var MessageContextSkillSystem = new Dictionary<string, object>();
            MessageContextSkill MessageContextSkillModel = new MessageContextSkill()
            {
                UserDefined = MessageContextSkillUserDefined,
                System = MessageContextSkillSystem
            };

            MessageContextGlobal MessageContextGlobalModel = new MessageContextGlobal()
            {
                System = MessageContextGlobalSystemModel
            };

            MessageContextSkills MessageContextSkillsModel = new MessageContextSkills()
            {
            };

            MessageContext MessageContextModel = new MessageContext()
            {
                Global = MessageContextGlobalModel,
                Skills = MessageContextSkillsModel
            };
            string assistantId = "testString";
            string sessionId = "testString";
            MessageInput input = MessageInputModel;
            MessageContext context = MessageContextModel;

            request.As<MessageResponse>().Returns(Task.FromResult(response));

            var result = service.Message(assistantId: assistantId, sessionId: sessionId, input: input, context: context);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v2/assistants/{assistantId}/sessions/{sessionId}/message";
            client.Received().PostAsync(messageUrl);
        }

    }
}
