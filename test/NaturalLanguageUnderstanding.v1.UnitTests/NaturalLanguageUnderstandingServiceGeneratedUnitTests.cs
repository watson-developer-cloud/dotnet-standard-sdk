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


namespace IBM.Watson.NaturalLanguageUnderstanding.v1.UnitTests
{
    [TestClass]
    public class NaturalLanguageUnderstandingServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            NaturalLanguageUnderstandingService service = Substitute.For<NaturalLanguageUnderstandingService>("versionDate");
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var url = System.Environment.GetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_SERVICE_URL");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_SERVICE_URL", null);
            NaturalLanguageUnderstandingService service = Substitute.For<NaturalLanguageUnderstandingService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/natural-language-understanding/api");
            System.Environment.SetEnvironmentVariable("NATURAL_LANGUAGE_UNDERSTANDING_SERVICE_URL", url);
        }
        #endregion

        [TestMethod]
        public void Analyze_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var features = new Features();
            var text = "text";
            var html = "html";
            var url = "url";
            var clean = false;
            var xpath = "xpath";
            var fallbackToRaw = false;
            var returnAnalyzedText = false;
            var language = "language";
            var limitTextCharacters = 1;

            var result = service.;

            JObject bodyObject = new JObject();
            if (features != null)
            {
                bodyObject["features"] = JToken.FromObject(features);
            }
            if (!string.IsNullOrEmpty(text))
            {
                bodyObject["text"] = JToken.FromObject(text);
            }
            if (!string.IsNullOrEmpty(html))
            {
                bodyObject["html"] = JToken.FromObject(html);
            }
            if (!string.IsNullOrEmpty(url))
            {
                bodyObject["url"] = JToken.FromObject(url);
            }
            bodyObject["clean"] = JToken.FromObject(clean);
            if (!string.IsNullOrEmpty(xpath))
            {
                bodyObject["xpath"] = JToken.FromObject(xpath);
            }
            bodyObject["fallback_to_raw"] = JToken.FromObject(fallbackToRaw);
            bodyObject["return_analyzed_text"] = JToken.FromObject(returnAnalyzedText);
            if (!string.IsNullOrEmpty(language))
            {
                bodyObject["language"] = JToken.FromObject(language);
            }
            if (limitTextCharacters != null)
            {
                bodyObject["limit_text_characters"] = JToken.FromObject(limitTextCharacters);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
        [TestMethod]
        public void ListModels_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;


            var result = service.;

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void DeleteModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var modelId = "modelId";

            var result = service.;

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/models/{modelId}");
        }
    }
}
