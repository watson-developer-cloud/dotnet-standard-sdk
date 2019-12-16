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


namespace IBM.Watson.ToneAnalyzer.v3.UnitTests
{
    [TestClass]
    public class ToneAnalyzerServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            ToneAnalyzerService service = new ToneAnalyzerService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            ToneAnalyzerService service = new ToneAnalyzerService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            ToneAnalyzerService service = Substitute.For<ToneAnalyzerService>("versionDate");
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            ToneAnalyzerService service = new ToneAnalyzerService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            ToneAnalyzerService service = new ToneAnalyzerService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            ToneAnalyzerService service = new ToneAnalyzerService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var url = System.Environment.GetEnvironmentVariable("TONE_ANALYZER_SERVICE_URL");
            System.Environment.SetEnvironmentVariable("TONE_ANALYZER_SERVICE_URL", null);
            ToneAnalyzerService service = Substitute.For<ToneAnalyzerService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/tone-analyzer/api");
            System.Environment.SetEnvironmentVariable("TONE_ANALYZER_SERVICE_URL", url);
        }
        #endregion

        [TestMethod]
        public void Tone_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var toneInput = new ToneInput();
            var contentType = "contentType";
            var sentences = false;
            var tones = new List<string>() { "tones0", "tones1" };
            var contentLanguage = "contentLanguage";
            var acceptLanguage = "acceptLanguage";

            var result = service.;

            JObject bodyObject = new JObject();
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
        [TestMethod]
        public void ToneChat_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var utterances = new List<Utterance>();
            var contentLanguage = "contentLanguage";
            var acceptLanguage = "acceptLanguage";

            var result = service.;

            JObject bodyObject = new JObject();
            if (utterances != null && utterances.Count > 0)
            {
                bodyObject["utterances"] = JToken.FromObject(utterances);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
    }
}
