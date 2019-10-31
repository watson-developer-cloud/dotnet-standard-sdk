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
using IBM.Watson.PersonalityInsights.v3.Model;
using IBM.Cloud.SDK.Core.Model;

namespace IBM.Watson.PersonalityInsights.v3.UnitTests
{
    [TestClass]
    public class PersonalityInsightsServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            PersonalityInsightsService service = new PersonalityInsightsService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            PersonalityInsightsService service = new PersonalityInsightsService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY", "apikey");
            PersonalityInsightsService service = Substitute.For<PersonalityInsightsService>("versionDate");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            PersonalityInsightsService service = new PersonalityInsightsService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            PersonalityInsightsService service = new PersonalityInsightsService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            PersonalityInsightsService service = new PersonalityInsightsService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY", "apikey");
            var url = System.Environment.GetEnvironmentVariable("PERSONALITY_INSIGHTS_URL");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_URL", null);
            PersonalityInsightsService service = Substitute.For<PersonalityInsightsService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/personality-insights/api");
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_URL", url);
            System.Environment.SetEnvironmentVariable("PERSONALITY_INSIGHTS_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void Profile_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            PersonalityInsightsService service = new PersonalityInsightsService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var content = new Content();
            var contentType = "contentType";
            var contentLanguage = "contentLanguage";
            var acceptLanguage = "acceptLanguage";
            var rawScores = false;
            var csvHeaders = false;
            var consumptionPreferences = false;

            var result = service.Profile(content: content, contentType: contentType, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage, rawScores: rawScores, csvHeaders: csvHeaders, consumptionPreferences: consumptionPreferences);

            JObject bodyObject = new JObject();
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
        [TestMethod]
        public void ProfileAsCsv_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            PersonalityInsightsService service = new PersonalityInsightsService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var content = new Content();
            var contentType = "contentType";
            var contentLanguage = "contentLanguage";
            var acceptLanguage = "acceptLanguage";
            var rawScores = false;
            var csvHeaders = false;
            var consumptionPreferences = false;

            var result = service.ProfileAsCsv(content: content, contentType: contentType, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage, rawScores: rawScores, csvHeaders: csvHeaders, consumptionPreferences: consumptionPreferences);

            JObject bodyObject = new JObject();
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
    }
}
