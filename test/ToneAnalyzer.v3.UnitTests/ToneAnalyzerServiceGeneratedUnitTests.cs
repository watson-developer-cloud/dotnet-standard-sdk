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
using NSubstitute;
using System;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Watson.ToneAnalyzer.v3.Model;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

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
            var apikey = System.Environment.GetEnvironmentVariable("TONE_ANALYZER_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("TONE_ANALYZER_URL");
            System.Environment.SetEnvironmentVariable("TONE_ANALYZER_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("TONE_ANALYZER_URL", "http://www.url.com");
            ToneAnalyzerService service = Substitute.For<ToneAnalyzerService>("testString");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("TONE_ANALYZER_URL", url);
            System.Environment.SetEnvironmentVariable("TONE_ANALYZER_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            ToneAnalyzerService service = new ToneAnalyzerService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            ToneAnalyzerService service = Substitute.For<ToneAnalyzerService>("testString", "test_service");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/tone-analyzer/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestToneInputModel()
        {

            ToneInput testRequestModel = new ToneInput()
            {
                Text = "testString"
            };

            Assert.IsTrue(testRequestModel.Text == "testString");
        }

        [TestMethod]
        public void TestUtteranceModel()
        {

            Utterance testRequestModel = new Utterance()
            {
                Text = "testString",
                User = "testString"
            };

            Assert.IsTrue(testRequestModel.Text == "testString");
            Assert.IsTrue(testRequestModel.User == "testString");
        }

        [TestMethod]
        public void TestTestToneAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            ToneAnalyzerService service = new ToneAnalyzerService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'document_tone': {'tones': [{'score': 5, 'tone_id': 'ToneId', 'tone_name': 'ToneName'}], 'tone_categories': [{'tones': [{'score': 5, 'tone_id': 'ToneId', 'tone_name': 'ToneName'}], 'category_id': 'CategoryId', 'category_name': 'CategoryName'}], 'warning': 'Warning'}, 'sentences_tone': [{'sentence_id': 10, 'text': 'Text', 'tones': [{'score': 5, 'tone_id': 'ToneId', 'tone_name': 'ToneName'}], 'tone_categories': [{'tones': [{'score': 5, 'tone_id': 'ToneId', 'tone_name': 'ToneName'}], 'category_id': 'CategoryId', 'category_name': 'CategoryName'}], 'input_from': 9, 'input_to': 7}]}";
            var response = new DetailedResponse<ToneAnalysis>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<ToneAnalysis>(responseJson),
                StatusCode = 200
            };

            ToneInput ToneInputModel = new ToneInput()
            {
                Text = "testString"
            };
            ToneInput toneInput = ToneInputModel;
            string contentType = "application/json";
            bool? sentences = true;
            List<string> tones = new List<string> { "emotion" };
            string contentLanguage = "en";
            string acceptLanguage = "ar";

            request.As<ToneAnalysis>().Returns(Task.FromResult(response));

            var result = service.Tone(toneInput: toneInput, contentType: contentType, sentences: sentences, tones: tones, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/tone";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestToneChatAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            ToneAnalyzerService service = new ToneAnalyzerService(client);

            var version = "testString";
            service.Version = version;

            var responseJson = "{'utterances_tone': [{'utterance_id': 11, 'utterance_text': 'UtteranceText', 'tones': [{'score': 5, 'tone_id': 'excited', 'tone_name': 'ToneName'}], 'error': 'Error'}], 'warning': 'Warning'}";
            var response = new DetailedResponse<UtteranceAnalyses>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<UtteranceAnalyses>(responseJson),
                StatusCode = 200
            };

            Utterance UtteranceModel = new Utterance()
            {
                Text = "testString",
                User = "testString"
            };
            List<Utterance> utterances = new List<Utterance> { UtteranceModel };
            string contentLanguage = "en";
            string acceptLanguage = "ar";

            request.As<UtteranceAnalyses>().Returns(Task.FromResult(response));

            var result = service.ToneChat(utterances: utterances, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage);

            request.Received().WithArgument("version", "testString");

            string messageUrl = $"{service.ServiceUrl}/v3/tone_chat";
            client.Received().PostAsync(messageUrl);
        }

    }
}
