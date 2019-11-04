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
using IBM.Watson.TextToSpeech.v1.Model;
using IBM.Cloud.SDK.Core.Model;

namespace IBM.Watson.TextToSpeech.v1.UnitTests
{
    [TestClass]
    public class TextToSpeechServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            TextToSpeechService service = new TextToSpeechService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            TextToSpeechService service = new TextToSpeechService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEXT_TO_SPEECH_APIKEY");
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_APIKEY", "apikey");
            TextToSpeechService service = Substitute.For<TextToSpeechService>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            TextToSpeechService service = new TextToSpeechService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            TextToSpeechService service = new TextToSpeechService(new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEXT_TO_SPEECH_APIKEY");
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_APIKEY", "apikey");
            var url = System.Environment.GetEnvironmentVariable("TEXT_TO_SPEECH_URL");
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_URL", null);
            TextToSpeechService service = Substitute.For<TextToSpeechService>();
            Assert.IsTrue(service.ServiceUrl == "https://stream.watsonplatform.net/text-to-speech/api");
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_URL", url);
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void ListVoices_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);


            var result = service.ListVoices();

        }

        [TestMethod]
        public void GetVoice_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var voice = "voice";
            var customizationId = "customizationId";

            var result = service.GetVoice(voice: voice, customizationId: customizationId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/voices/{voice}");
        }

        [TestMethod]
        public void Synthesize_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var accept = "accept";
            var voice = "voice";
            var customizationId = "customizationId";

            var result = service.Synthesize(text: text, accept: accept, voice: voice, customizationId: customizationId);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(text))
            {
                bodyObject["text"] = JToken.FromObject(text);
            }
            var json = JsonConvert.SerializeObject(bodyObject);

            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }

        [TestMethod]
        public void GetPronunciation_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var text = "text";
            var voice = "voice";
            var format = "format";
            var customizationId = "customizationId";

            var result = service.GetPronunciation(text: text, voice: voice, format: format, customizationId: customizationId);

        }

        [TestMethod]
        public void CreateVoiceModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);


            var result = service.CreateVoiceModel(name: name, language: language, description: description);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(name))
            {
                bodyObject["name"] = JToken.FromObject(name);
            }
            if (!string.IsNullOrEmpty(language))
            {
                bodyObject["language"] = JToken.FromObject(language);
            }
            if (!string.IsNullOrEmpty(description))
            {
                bodyObject["description"] = JToken.FromObject(description);
            }
            var json = JsonConvert.SerializeObject(bodyObject);

            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }

        [TestMethod]
        public void ListVoiceModels_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var language = "language";

            var result = service.ListVoiceModels(language: language);

        }

        [TestMethod]
        public void UpdateVoiceModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customizationId = "customizationId";

            var result = service.UpdateVoiceModel(customizationId: customizationId, name: name, description: description, words: words);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(name))
            {
                bodyObject["name"] = JToken.FromObject(name);
            }
            if (!string.IsNullOrEmpty(description))
            {
                bodyObject["description"] = JToken.FromObject(description);
            }
            if (words != null && words.Count > 0)
            {
                bodyObject["words"] = JToken.FromObject(words);
            }
            var json = JsonConvert.SerializeObject(bodyObject);

            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}");
        }

        [TestMethod]
        public void GetVoiceModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customizationId = "customizationId";

            var result = service.GetVoiceModel(customizationId: customizationId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}");
        }

        [TestMethod]
        public void DeleteVoiceModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customizationId = "customizationId";

            var result = service.DeleteVoiceModel(customizationId: customizationId);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}");
        }

        [TestMethod]
        public void AddWords_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customizationId = "customizationId";

            var result = service.AddWords(customizationId: customizationId, words: words);

            JObject bodyObject = new JObject();
            if (words != null && words.Count > 0)
            {
                bodyObject["words"] = JToken.FromObject(words);
            }
            var json = JsonConvert.SerializeObject(bodyObject);

            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PostAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words");
        }

        [TestMethod]
        public void ListWords_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customizationId = "customizationId";

            var result = service.ListWords(customizationId: customizationId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words");
        }

        [TestMethod]
        public void AddWord_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customizationId = "customizationId";
            var word = "word";

            var result = service.AddWord(customizationId: customizationId, word: word, translation: translation, partOfSpeech: partOfSpeech);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(translation))
            {
                bodyObject["translation"] = JToken.FromObject(translation);
            }
            if (!string.IsNullOrEmpty(partOfSpeech))
            {
                bodyObject["part_of_speech"] = JToken.FromObject(partOfSpeech);
            }
            var json = JsonConvert.SerializeObject(bodyObject);

            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PutAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{word}");
        }

        [TestMethod]
        public void GetWord_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customizationId = "customizationId";
            var word = "word";

            var result = service.GetWord(customizationId: customizationId, word: word);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{word}");
        }

        [TestMethod]
        public void DeleteWord_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customizationId = "customizationId";
            var word = "word";

            var result = service.DeleteWord(customizationId: customizationId, word: word);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{word}");
        }

        [TestMethod]
        public void DeleteUserData_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);

            var customerId = "customerId";

            var result = service.DeleteUserData(customerId: customerId);

        }

    }
}
