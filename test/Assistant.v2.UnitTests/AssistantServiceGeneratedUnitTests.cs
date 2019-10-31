/**
* (C) Copyright IBM Corp. 2018, 2019.
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

using NSubstitute;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using IBM.Watson.Assistant.v2.Model;
using IBM.Cloud.SDK.Core.Model;

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
            System.Environment.SetEnvironmentVariable("ASSISTANT_APIKEY", "apikey");
            AssistantService service = Substitute.For<AssistantService>("versionDate");
            Assert.IsNotNull(service);
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
            var url = System.Environment.GetEnvironmentVariable("ASSISTANT_URL");
            var apikey = System.Environment.GetEnvironmentVariable("ASSISTANT_APIKEY");
            System.Environment.SetEnvironmentVariable("ASSISTANT_URL", null);
            System.Environment.SetEnvironmentVariable("ASSISTANT_APIKEY", "apikey");
            AssistantService service = Substitute.For<AssistantService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/assistant/api");
            System.Environment.SetEnvironmentVariable("ASSISTANT_URL", url);
            System.Environment.SetEnvironmentVariable("ASSISTANT_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void CreateSession_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            AssistantService service = new AssistantService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var assistantId = "assistantId";

            var result = service.CreateSession(assistantId: assistantId);

            request.Received().WithArgument("version", versionDate);
            client.Received().PostAsync($"{service.ServiceUrl}/v2/assistants/{assistantId}/sessions");
        }
        [TestMethod]
        public void DeleteSession_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            AssistantService service = new AssistantService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var assistantId = "assistantId";
            var sessionId = "sessionId";

            var result = service.DeleteSession(assistantId: assistantId, sessionId: sessionId);

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v2/assistants/{assistantId}/sessions/{sessionId}");
        }
        [TestMethod]
        public void Message_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            AssistantService service = new AssistantService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var assistantId = "assistantId";
            var sessionId = "sessionId";

            var result = service.Message(assistantId: assistantId, sessionId: sessionId, input: input, context: context);

            JObject bodyObject = new JObject();
            if (input != null)
            {
                bodyObject["input"] = JToken.FromObject(input);
            }
            if (context != null)
            {
                bodyObject["context"] = JToken.FromObject(context);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v2/assistants/{assistantId}/sessions/{sessionId}/message");
        }
    }
}
