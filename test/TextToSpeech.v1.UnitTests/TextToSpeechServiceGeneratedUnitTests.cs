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
using IBM.Watson.TextToSpeech.v1.Model;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

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
            var url = System.Environment.GetEnvironmentVariable("TEXT_TO_SPEECH_URL");
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_URL", "http://www.url.com");
            TextToSpeechService service = Substitute.For<TextToSpeechService>();
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_URL", url);
            System.Environment.SetEnvironmentVariable("TEXT_TO_SPEECH_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            TextToSpeechService service = new TextToSpeechService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            TextToSpeechService service = Substitute.For<TextToSpeechService>("test_service");
            Assert.IsTrue(service.ServiceUrl == "https://stream.watsonplatform.net/text-to-speech/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestTranslationModel()
        {

            Translation testRequestModel = new Translation()
            {
                _Translation = "testString",
                PartOfSpeech = "Dosi"
            };

            Assert.IsTrue(testRequestModel._Translation == "testString");
            Assert.IsTrue(testRequestModel.PartOfSpeech == "Dosi");
        }

        [TestMethod]
        public void TestWordModel()
        {

            Word testRequestModel = new Word()
            {
                _Word = "testString",
                Translation = "testString",
                PartOfSpeech = "Dosi"
            };

            Assert.IsTrue(testRequestModel._Word == "testString");
            Assert.IsTrue(testRequestModel.Translation == "testString");
            Assert.IsTrue(testRequestModel.PartOfSpeech == "Dosi");
        }

        [TestMethod]
        public void TestWordsModel()
        {
            Word WordModel = new Word()
            {
                _Word = "testString",
                Translation = "testString",
                PartOfSpeech = "Dosi"
            };

            var Words_Words = new List<Word> { WordModel };
            Words testRequestModel = new Words()
            {
                _Words = Words_Words
            };

            Assert.IsTrue(testRequestModel._Words == Words_Words);
        }

        [TestMethod]
        public void TestTestListVoicesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{'voices': [{'url': 'Url', 'gender': 'Gender', 'name': 'Name', 'language': 'Language', 'description': 'Description', 'customizable': true, 'supported_features': {'custom_pronunciation': false, 'voice_transformation': false}, 'customization': {'customization_id': 'CustomizationId', 'name': 'Name', 'language': 'Language', 'owner': 'Owner', 'created': 'Created', 'last_modified': 'LastModified', 'description': 'Description', 'words': [{'word': '_Word', 'translation': 'Translation', 'part_of_speech': 'Dosi'}]}}]}";
            var response = new DetailedResponse<Voices>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Voices>(responseJson),
                StatusCode = 200
            };


            request.As<Voices>().Returns(Task.FromResult(response));

            var result = service.ListVoices();


            string messageUrl = $"{service.ServiceUrl}/v1/voices";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetVoiceAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{'url': 'Url', 'gender': 'Gender', 'name': 'Name', 'language': 'Language', 'description': 'Description', 'customizable': true, 'supported_features': {'custom_pronunciation': false, 'voice_transformation': false}, 'customization': {'customization_id': 'CustomizationId', 'name': 'Name', 'language': 'Language', 'owner': 'Owner', 'created': 'Created', 'last_modified': 'LastModified', 'description': 'Description', 'words': [{'word': '_Word', 'translation': 'Translation', 'part_of_speech': 'Dosi'}]}}";
            var response = new DetailedResponse<Voice>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Voice>(responseJson),
                StatusCode = 200
            };

            string voice = "de-DE_BirgitVoice";
            string customizationId = "testString";

            request.As<Voice>().Returns(Task.FromResult(response));

            var result = service.GetVoice(voice: voice, customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/voices/{voice}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestSynthesizeAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var response = new DetailedResponse<byte[]>()
            {
                Result = new byte[4],
                StatusCode = 200
            };
            string text = "testString";
            string accept = "audio/basic";
            string voice = "ar-AR_OmarVoice";
            string customizationId = "testString";

            request.As<byte[]>().Returns(Task.FromResult(response));

            var result = service.Synthesize(text: text, accept: accept, voice: voice, customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/synthesize";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetPronunciationAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{'pronunciation': '_Pronunciation'}";
            var response = new DetailedResponse<Pronunciation>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Pronunciation>(responseJson),
                StatusCode = 200
            };

            string text = "testString";
            string voice = "de-DE_BirgitVoice";
            string format = "ibm";
            string customizationId = "testString";

            request.As<Pronunciation>().Returns(Task.FromResult(response));

            var result = service.GetPronunciation(text: text, voice: voice, format: format, customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/pronunciation";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateVoiceModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{'customization_id': 'CustomizationId', 'name': 'Name', 'language': 'Language', 'owner': 'Owner', 'created': 'Created', 'last_modified': 'LastModified', 'description': 'Description', 'words': [{'word': '_Word', 'translation': 'Translation', 'part_of_speech': 'Dosi'}]}";
            var response = new DetailedResponse<VoiceModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<VoiceModel>(responseJson),
                StatusCode = 201
            };

            string name = "testString";
            string language = "de-DE";
            string description = "testString";

            request.As<VoiceModel>().Returns(Task.FromResult(response));

            var result = service.CreateVoiceModel(name: name, language: language, description: description);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListVoiceModelsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{'customizations': [{'customization_id': 'CustomizationId', 'name': 'Name', 'language': 'Language', 'owner': 'Owner', 'created': 'Created', 'last_modified': 'LastModified', 'description': 'Description', 'words': [{'word': '_Word', 'translation': 'Translation', 'part_of_speech': 'Dosi'}]}]}";
            var response = new DetailedResponse<VoiceModels>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<VoiceModels>(responseJson),
                StatusCode = 200
            };

            string language = "de-DE";

            request.As<VoiceModels>().Returns(Task.FromResult(response));

            var result = service.ListVoiceModels(language: language);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpdateVoiceModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            Word WordModel = new Word()
            {
                _Word = "testString",
                Translation = "testString",
                PartOfSpeech = "Dosi"
            };
            string customizationId = "testString";
            string name = "testString";
            string description = "testString";
            List<Word> words = new List<Word> { WordModel };

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.UpdateVoiceModel(customizationId: customizationId, name: name, description: description, words: words);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetVoiceModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{'customization_id': 'CustomizationId', 'name': 'Name', 'language': 'Language', 'owner': 'Owner', 'created': 'Created', 'last_modified': 'LastModified', 'description': 'Description', 'words': [{'word': '_Word', 'translation': 'Translation', 'part_of_speech': 'Dosi'}]}";
            var response = new DetailedResponse<VoiceModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<VoiceModel>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<VoiceModel>().Returns(Task.FromResult(response));

            var result = service.GetVoiceModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteVoiceModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string customizationId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteVoiceModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddWordsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            Word WordModel = new Word()
            {
                _Word = "testString",
                Translation = "testString",
                PartOfSpeech = "Dosi"
            };
            string customizationId = "testString";
            List<Word> words = new List<Word> { WordModel };

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.AddWords(customizationId: customizationId, words: words);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListWordsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{'words': [{'word': '_Word', 'translation': 'Translation', 'part_of_speech': 'Dosi'}]}";
            var response = new DetailedResponse<Words>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Words>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<Words>().Returns(Task.FromResult(response));

            var result = service.ListWords(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddWordAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string word = "testString";
            string translation = "testString";
            string partOfSpeech = "Dosi";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.AddWord(customizationId: customizationId, word: word, translation: translation, partOfSpeech: partOfSpeech);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{word}";
            client.Received().PutAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetWordAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{'translation': '_Translation', 'part_of_speech': 'Dosi'}";
            var response = new DetailedResponse<Translation>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Translation>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string word = "testString";

            request.As<Translation>().Returns(Task.FromResult(response));

            var result = service.GetWord(customizationId: customizationId, word: word);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{word}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteWordAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string customizationId = "testString";
            string word = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteWord(customizationId: customizationId, word: word);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{word}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteUserDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            TextToSpeechService service = new TextToSpeechService(client);
            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 200
            };

            string customerId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteUserData(customerId: customerId);


            string messageUrl = $"{service.ServiceUrl}/v1/user_data";
            client.Received().DeleteAsync(messageUrl);
        }

    }
}
