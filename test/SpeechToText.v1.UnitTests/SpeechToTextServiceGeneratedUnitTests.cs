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
using IBM.Watson.SpeechToText.v1.Model;
using IBM.Cloud.SDK.Core.Model;

namespace IBM.Watson.SpeechToText.v1.UnitTests
{
    [TestClass]
    public class SpeechToTextServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            SpeechToTextService service = new SpeechToTextService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            SpeechToTextService service = new SpeechToTextService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("SPEECH_TO_TEXT_APIKEY");
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_APIKEY", "apikey");
            SpeechToTextService service = Substitute.For<SpeechToTextService>();
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            SpeechToTextService service = new SpeechToTextService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            SpeechToTextService service = new SpeechToTextService(new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("SPEECH_TO_TEXT_APIKEY");
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_APIKEY", "apikey");
            var url = System.Environment.GetEnvironmentVariable("SPEECH_TO_TEXT_URL");
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_URL", null);
            SpeechToTextService service = Substitute.For<SpeechToTextService>();
            Assert.IsTrue(service.ServiceUrl == "https://stream.watsonplatform.net/speech-to-text/api");
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_URL", url);
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void ListModels_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);


            var result = service.ListModels();

        }
        [TestMethod]
        public void GetModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var modelId = "modelId";

            var result = service.GetModel(modelId: modelId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/models/{modelId}");
        }
        [TestMethod]
        public void Recognize_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var audio = new byte[4];
            var contentType = "contentType";
            var model = "model";
            var languageCustomizationId = "languageCustomizationId";
            var acousticCustomizationId = "acousticCustomizationId";
            var baseModelVersion = "baseModelVersion";
            double? customizationWeight = 0.5f;
            long? inactivityTimeout = 1;
            var keywords = new List<string>() { "keywords0", "keywords1" };
            float? keywordsThreshold = 0.5f;
            long? maxAlternatives = 1;
            float? wordAlternativesThreshold = 0.5f;
            var wordConfidence = false;
            var timestamps = false;
            var profanityFilter = false;
            var smartFormatting = false;
            var speakerLabels = false;
            var customizationId = "customizationId";
            var grammarName = "grammarName";
            var redaction = false;
            var audioMetrics = false;

            var result = service.Recognize(audio: audio, contentType: contentType, model: model, languageCustomizationId: languageCustomizationId, acousticCustomizationId: acousticCustomizationId, baseModelVersion: baseModelVersion, customizationWeight: customizationWeight, inactivityTimeout: inactivityTimeout, keywords: keywords, keywordsThreshold: keywordsThreshold, maxAlternatives: maxAlternatives, wordAlternativesThreshold: wordAlternativesThreshold, wordConfidence: wordConfidence, timestamps: timestamps, profanityFilter: profanityFilter, smartFormatting: smartFormatting, speakerLabels: speakerLabels, customizationId: customizationId, grammarName: grammarName, redaction: redaction, audioMetrics: audioMetrics);

            var audioString = System.Text.Encoding.Default.GetString(audio);
            request.Received().WithBodyContent(Arg.Is<ByteArrayContent>(x => x.ReadAsStringAsync().Result.Equals(audioString)));
        }
        [TestMethod]
        public void RegisterCallback_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var callbackUrl = "callbackUrl";
            var userSecret = "userSecret";

            var result = service.RegisterCallback(callbackUrl: callbackUrl, userSecret: userSecret);

        }
        [TestMethod]
        public void UnregisterCallback_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var callbackUrl = "callbackUrl";

            var result = service.UnregisterCallback(callbackUrl: callbackUrl);

        }
        [TestMethod]
        public void CreateJob_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var audio = new byte[4];
            var contentType = "contentType";
            var model = "model";
            var callbackUrl = "callbackUrl";
            var events = "events";
            var userToken = "userToken";
            long? resultsTtl = 1;
            var languageCustomizationId = "languageCustomizationId";
            var acousticCustomizationId = "acousticCustomizationId";
            var baseModelVersion = "baseModelVersion";
            double? customizationWeight = 0.5f;
            long? inactivityTimeout = 1;
            var keywords = new List<string>() { "keywords0", "keywords1" };
            float? keywordsThreshold = 0.5f;
            long? maxAlternatives = 1;
            float? wordAlternativesThreshold = 0.5f;
            var wordConfidence = false;
            var timestamps = false;
            var profanityFilter = false;
            var smartFormatting = false;
            var speakerLabels = false;
            var customizationId = "customizationId";
            var grammarName = "grammarName";
            var redaction = false;
            var processingMetrics = false;
            float? processingMetricsInterval = 0.5f;
            var audioMetrics = false;

            var result = service.CreateJob(audio: audio, contentType: contentType, model: model, callbackUrl: callbackUrl, events: events, userToken: userToken, resultsTtl: resultsTtl, languageCustomizationId: languageCustomizationId, acousticCustomizationId: acousticCustomizationId, baseModelVersion: baseModelVersion, customizationWeight: customizationWeight, inactivityTimeout: inactivityTimeout, keywords: keywords, keywordsThreshold: keywordsThreshold, maxAlternatives: maxAlternatives, wordAlternativesThreshold: wordAlternativesThreshold, wordConfidence: wordConfidence, timestamps: timestamps, profanityFilter: profanityFilter, smartFormatting: smartFormatting, speakerLabels: speakerLabels, customizationId: customizationId, grammarName: grammarName, redaction: redaction, processingMetrics: processingMetrics, processingMetricsInterval: processingMetricsInterval, audioMetrics: audioMetrics);

            var audioString = System.Text.Encoding.Default.GetString(audio);
            request.Received().WithBodyContent(Arg.Is<ByteArrayContent>(x => x.ReadAsStringAsync().Result.Equals(audioString)));
        }
        [TestMethod]
        public void CheckJobs_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);


            var result = service.CheckJobs();

        }
        [TestMethod]
        public void CheckJob_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var id = "id";

            var result = service.CheckJob(id: id);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/recognitions/{id}");
        }
        [TestMethod]
        public void DeleteJob_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var id = "id";

            var result = service.DeleteJob(id: id);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/recognitions/{id}");
        }
        [TestMethod]
        public void CreateLanguageModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var name = "name";
            var baseModelName = "baseModelName";
            var dialect = "dialect";
            var description = "description";

            var result = service.CreateLanguageModel(name: name, baseModelName: baseModelName, dialect: dialect, description: description);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(name))
            {
                bodyObject["name"] = JToken.FromObject(name);
            }
            if (!string.IsNullOrEmpty(baseModelName))
            {
                bodyObject["base_model_name"] = JToken.FromObject(baseModelName);
            }
            if (!string.IsNullOrEmpty(dialect))
            {
                bodyObject["dialect"] = JToken.FromObject(dialect);
            }
            if (!string.IsNullOrEmpty(description))
            {
                bodyObject["description"] = JToken.FromObject(description);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
        [TestMethod]
        public void ListLanguageModels_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var language = "language";

            var result = service.ListLanguageModels(language: language);

        }
        [TestMethod]
        public void GetLanguageModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.GetLanguageModel(customizationId: customizationId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}");
        }
        [TestMethod]
        public void DeleteLanguageModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.DeleteLanguageModel(customizationId: customizationId);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}");
        }
        [TestMethod]
        public void TrainLanguageModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var wordTypeToAdd = "wordTypeToAdd";
            double? customizationWeight = 0.5f;

            var result = service.TrainLanguageModel(customizationId: customizationId, wordTypeToAdd: wordTypeToAdd, customizationWeight: customizationWeight);

            client.Received().PostAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/train");
        }
        [TestMethod]
        public void ResetLanguageModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.ResetLanguageModel(customizationId: customizationId);

            client.Received().PostAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/reset");
        }
        [TestMethod]
        public void UpgradeLanguageModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.UpgradeLanguageModel(customizationId: customizationId);

            client.Received().PostAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/upgrade_model");
        }
        [TestMethod]
        public void ListCorpora_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.ListCorpora(customizationId: customizationId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/corpora");
        }
        [TestMethod]
        public void AddCorpus_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var corpusName = "corpusName";
            var corpusFile = new MemoryStream();
            var allowOverwrite = false;

            var result = service.AddCorpus(customizationId: customizationId, corpusName: corpusName, corpusFile: corpusFile, allowOverwrite: allowOverwrite);

            client.Received().PostAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/corpora/{corpusName}");
        }
        [TestMethod]
        public void GetCorpus_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var corpusName = "corpusName";

            var result = service.GetCorpus(customizationId: customizationId, corpusName: corpusName);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/corpora/{corpusName}");
        }
        [TestMethod]
        public void DeleteCorpus_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var corpusName = "corpusName";

            var result = service.DeleteCorpus(customizationId: customizationId, corpusName: corpusName);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/corpora/{corpusName}");
        }
        [TestMethod]
        public void ListWords_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var wordType = "wordType";
            var sort = "sort";

            var result = service.ListWords(customizationId: customizationId, wordType: wordType, sort: sort);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words");
        }
        [TestMethod]
        public void AddWords_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var words = new List<CustomWord>();

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
        public void AddWord_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var wordName = "wordName";
            var word = "word";
            var soundsLike = new List<string>();
            var displayAs = "displayAs";

            var result = service.AddWord(customizationId: customizationId, wordName: wordName, word: word, soundsLike: soundsLike, displayAs: displayAs);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(word))
            {
                bodyObject["word"] = JToken.FromObject(word);
            }
            if (soundsLike != null && soundsLike.Count > 0)
            {
                bodyObject["sounds_like"] = JToken.FromObject(soundsLike);
            }
            if (!string.IsNullOrEmpty(displayAs))
            {
                bodyObject["display_as"] = JToken.FromObject(displayAs);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
            client.Received().PutAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{wordName}");
        }
        [TestMethod]
        public void GetWord_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var wordName = "wordName";

            var result = service.GetWord(customizationId: customizationId, wordName: wordName);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{wordName}");
        }
        [TestMethod]
        public void DeleteWord_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var wordName = "wordName";

            var result = service.DeleteWord(customizationId: customizationId, wordName: wordName);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{wordName}");
        }
        [TestMethod]
        public void ListGrammars_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.ListGrammars(customizationId: customizationId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/grammars");
        }
        [TestMethod]
        public void AddGrammar_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var grammarName = "grammarName";
            var grammarFile = "grammarFile";
            var contentType = "contentType";
            var allowOverwrite = false;

            var result = service.AddGrammar(customizationId: customizationId, grammarName: grammarName, grammarFile: grammarFile, contentType: contentType, allowOverwrite: allowOverwrite);

            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(grammarFile)));
            client.Received().PostAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/grammars/{grammarName}");
        }
        [TestMethod]
        public void GetGrammar_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var grammarName = "grammarName";

            var result = service.GetGrammar(customizationId: customizationId, grammarName: grammarName);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/grammars/{grammarName}");
        }
        [TestMethod]
        public void DeleteGrammar_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var grammarName = "grammarName";

            var result = service.DeleteGrammar(customizationId: customizationId, grammarName: grammarName);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/customizations/{customizationId}/grammars/{grammarName}");
        }
        [TestMethod]
        public void CreateAcousticModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var name = "name";
            var baseModelName = "baseModelName";
            var description = "description";

            var result = service.CreateAcousticModel(name: name, baseModelName: baseModelName, description: description);

            JObject bodyObject = new JObject();
            if (!string.IsNullOrEmpty(name))
            {
                bodyObject["name"] = JToken.FromObject(name);
            }
            if (!string.IsNullOrEmpty(baseModelName))
            {
                bodyObject["base_model_name"] = JToken.FromObject(baseModelName);
            }
            if (!string.IsNullOrEmpty(description))
            {
                bodyObject["description"] = JToken.FromObject(description);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
        [TestMethod]
        public void ListAcousticModels_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var language = "language";

            var result = service.ListAcousticModels(language: language);

        }
        [TestMethod]
        public void GetAcousticModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.GetAcousticModel(customizationId: customizationId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}");
        }
        [TestMethod]
        public void DeleteAcousticModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.DeleteAcousticModel(customizationId: customizationId);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}");
        }
        [TestMethod]
        public void TrainAcousticModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var customLanguageModelId = "customLanguageModelId";

            var result = service.TrainAcousticModel(customizationId: customizationId, customLanguageModelId: customLanguageModelId);

            client.Received().PostAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/train");
        }
        [TestMethod]
        public void ResetAcousticModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.ResetAcousticModel(customizationId: customizationId);

            client.Received().PostAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/reset");
        }
        [TestMethod]
        public void UpgradeAcousticModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var customLanguageModelId = "customLanguageModelId";
            var force = false;

            var result = service.UpgradeAcousticModel(customizationId: customizationId, customLanguageModelId: customLanguageModelId, force: force);

            client.Received().PostAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/upgrade_model");
        }
        [TestMethod]
        public void ListAudio_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";

            var result = service.ListAudio(customizationId: customizationId);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/audio");
        }
        [TestMethod]
        public void AddAudio_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var audioName = "audioName";
            var audioResource = new byte[4];
            var contentType = "contentType";
            var containedContentType = "containedContentType";
            var allowOverwrite = false;

            var result = service.AddAudio(customizationId: customizationId, audioName: audioName, audioResource: audioResource, contentType: contentType, containedContentType: containedContentType, allowOverwrite: allowOverwrite);

            var audioResourceString = System.Text.Encoding.Default.GetString(audioResource);
            request.Received().WithBodyContent(Arg.Is<ByteArrayContent>(x => x.ReadAsStringAsync().Result.Equals(audioResourceString)));
            client.Received().PostAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
        }
        [TestMethod]
        public void GetAudio_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var audioName = "audioName";

            var result = service.GetAudio(customizationId: customizationId, audioName: audioName);

            client.Received().GetAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
        }
        [TestMethod]
        public void DeleteAudio_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customizationId = "customizationId";
            var audioName = "audioName";

            var result = service.DeleteAudio(customizationId: customizationId, audioName: audioName);

            client.Received().DeleteAsync($"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/audio/{audioName}");
        }
        [TestMethod]
        public void DeleteUserData_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);

            var customerId = "customerId";

            var result = service.DeleteUserData(customerId: customerId);

        }
    }
}
