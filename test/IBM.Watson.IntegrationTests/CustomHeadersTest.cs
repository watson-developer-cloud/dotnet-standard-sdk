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

using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;

namespace IBM.Watson.IntegrationTests
{
    [TestClass]
    public class CustomHeadersTest
    {
        [TestMethod]
        public void TestAddHeader()
        {
            IClient client = CreateClient();

            IBMService service = Substitute.For<IBMService>("serviceName", client);
            service.WithHeader("header0", "value0");

            var headers = service.GetCustomRequestHeaders();
            Assert.IsTrue(headers.ContainsKey("header0"));
            Assert.IsTrue(headers.ContainsValue("value0"));
            Assert.IsTrue(headers["header0"] == "value0");
        }

        [TestMethod]
        public void TestOverwriteHeader()
        {
            IClient client = CreateClient();

            IBMService service = Substitute.For<IBMService>("serviceName", client);
            service.WithHeader("header0", "value0");

            var headers = service.GetCustomRequestHeaders();
            Assert.IsTrue(headers.ContainsKey("header0"));
            Assert.IsTrue(headers.ContainsValue("value0"));
            Assert.IsTrue(headers["header0"] == "value0");

            service.WithHeader("header0", "newValue0");
            headers = service.GetCustomRequestHeaders();
            Assert.IsTrue(headers.ContainsKey("header0"));
            Assert.IsTrue(headers.ContainsValue("newValue0"));
            Assert.IsTrue(headers["header0"] == "newValue0");
        }

        [TestMethod]
        public void TestAddHeaders()
        {
            IClient client = CreateClient();

            IBMService service = Substitute.For<IBMService>("serviceName", client);
            Dictionary<string, string> customRequestHeaders = new Dictionary<string, string>();
            customRequestHeaders.Add("header0", "value0");
            customRequestHeaders.Add("header1", "value1");
            service.WithHeaders(customRequestHeaders);

            var headers = service.GetCustomRequestHeaders();
            Assert.IsTrue(headers.ContainsKey("header0"));
            Assert.IsTrue(headers.ContainsValue("value0"));
            Assert.IsTrue(headers["header0"] == "value0");
            Assert.IsTrue(headers.ContainsKey("header1"));
            Assert.IsTrue(headers.ContainsValue("value1"));
            Assert.IsTrue(headers["header1"] == "value1");
        }

        [TestMethod]
        public void TestOverwriteHeaders()
        {
            IClient client = CreateClient();

            IBMService service = Substitute.For<IBMService>("serviceName", client);
            service.WithHeader("header0", "value0");

            var headers = service.GetCustomRequestHeaders();
            Assert.IsTrue(headers.ContainsKey("header0"));
            Assert.IsTrue(headers.ContainsValue("value0"));
            Assert.IsTrue(headers["header0"] == "value0");

            Dictionary<string, string> customRequestHeaders = new Dictionary<string, string>();
            customRequestHeaders.Add("header0", "newValue0");
            customRequestHeaders.Add("header1", "newValue1");
            service.WithHeaders(customRequestHeaders);

            headers = service.GetCustomRequestHeaders();

            Assert.IsTrue(headers.ContainsKey("header0"));
            Assert.IsTrue(headers.ContainsValue("newValue0"));
            Assert.IsTrue(headers["header0"] == "newValue0");
            Assert.IsTrue(headers.ContainsKey("header1"));
            Assert.IsTrue(headers.ContainsValue("newValue1"));
            Assert.IsTrue(headers["header1"] == "newValue1");
        }

        #region Create Client
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                .Returns(client);

            return client;
        }
        #endregion
    }
}
