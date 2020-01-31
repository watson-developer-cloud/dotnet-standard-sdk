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
using IBM.Watson.SpeechToText.v1.Model;
using NSubstitute;
using System;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

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
            var url = System.Environment.GetEnvironmentVariable("SPEECH_TO_TEXT_URL");
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_URL", "http://www.url.com");
            SpeechToTextService service = Substitute.For<SpeechToTextService>();
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_URL", url);
            System.Environment.SetEnvironmentVariable("SPEECH_TO_TEXT_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            SpeechToTextService service = new SpeechToTextService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("TEST_SERVICE_APIKEY");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", "apikey");
            SpeechToTextService service = Substitute.For<SpeechToTextService>("test_service");
            Assert.IsTrue(service.ServiceUrl == "https://stream.watsonplatform.net/speech-to-text/api");
            System.Environment.SetEnvironmentVariable("TEST_SERVICE_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestCustomWordModel()
        {

            var CustomWordSoundsLike = new List<string> { "testString" };
            CustomWord testRequestModel = new CustomWord()
            {
                Word = "testString",
                SoundsLike = CustomWordSoundsLike,
                DisplayAs = "testString"
            };

            Assert.IsTrue(testRequestModel.Word == "testString");
            Assert.IsTrue(testRequestModel.SoundsLike == CustomWordSoundsLike);
            Assert.IsTrue(testRequestModel.DisplayAs == "testString");
        }

        [TestMethod]
        public void TestTestListModelsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'models': [{'name': 'Name', 'language': 'Language', 'rate': 4, 'url': 'Url', 'supported_features': {'custom_language_model': false, 'speaker_labels': false}, 'description': 'Description'}]}";
            var response = new DetailedResponse<SpeechModels>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<SpeechModels>(responseJson),
                StatusCode = 200
            };


            request.As<SpeechModels>().Returns(Task.FromResult(response));

            var result = service.ListModels();


            string messageUrl = $"{service.ServiceUrl}/v1/models";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'name': 'Name', 'language': 'Language', 'rate': 4, 'url': 'Url', 'supported_features': {'custom_language_model': false, 'speaker_labels': false}, 'description': 'Description'}";
            var response = new DetailedResponse<SpeechModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<SpeechModel>(responseJson),
                StatusCode = 200
            };

            string modelId = "ar-AR_BroadbandModel";

            request.As<SpeechModel>().Returns(Task.FromResult(response));

            var result = service.GetModel(modelId: modelId);


            string messageUrl = $"{service.ServiceUrl}/v1/models/{modelId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestRecognizeAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'results': [{'final': false, 'alternatives': [{'transcript': 'Transcript', 'confidence': 10, 'timestamps': [['Timestamps']], 'word_confidence': [['WordConfidence']]}], 'keywords_result': {}, 'word_alternatives': [{'start_time': 9, 'end_time': 7, 'alternatives': [{'confidence': 10, 'word': 'Word'}]}], 'end_of_utterance': 'end_of_data'}], 'result_index': 11, 'speaker_labels': [{'from': 4, 'to': 2, 'speaker': 7, 'confidence': 10, 'final': false}], 'processing_metrics': {'processed_audio': {'received': 8, 'seen_by_engine': 12, 'transcription': 13, 'speaker_labels': 13}, 'wall_clock_since_first_byte_received': 31, 'periodic': true}, 'audio_metrics': {'sampling_interval': 16, 'accumulated': {'final': false, 'end_time': 7, 'signal_to_noise_ratio': 18, 'speech_ratio': 11, 'high_frequency_loss': 17, 'direct_current_offset': [{'begin': 5, 'end': 3, 'count': 5}], 'clipping_rate': [{'begin': 5, 'end': 3, 'count': 5}], 'speech_level': [{'begin': 5, 'end': 3, 'count': 5}], 'non_speech_level': [{'begin': 5, 'end': 3, 'count': 5}]}}, 'warnings': ['Warnings']}";
            var response = new DetailedResponse<SpeechRecognitionResults>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<SpeechRecognitionResults>(responseJson),
                StatusCode = 200
            };

            System.IO.MemoryStream audio = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string contentType = "application/octet-stream";
            string model = "ar-AR_BroadbandModel";
            string languageCustomizationId = "testString";
            string acousticCustomizationId = "testString";
            string baseModelVersion = "testString";
            double? customizationWeight = 72.5f;
            long? inactivityTimeout = 38;
            List<string> keywords = new List<string> { "testString" };
            float? keywordsThreshold = 36.0f;
            long? maxAlternatives = 38;
            float? wordAlternativesThreshold = 36.0f;
            bool? wordConfidence = true;
            bool? timestamps = true;
            bool? profanityFilter = true;
            bool? smartFormatting = true;
            bool? speakerLabels = true;
            string customizationId = "testString";
            string grammarName = "testString";
            bool? redaction = true;
            bool? audioMetrics = true;
            double? endOfPhraseSilenceTime = 72.5f;
            bool? splitTranscriptAtPhraseEnd = true;

            request.As<SpeechRecognitionResults>().Returns(Task.FromResult(response));

            var result = service.Recognize(audio: audio, contentType: contentType, model: model, languageCustomizationId: languageCustomizationId, acousticCustomizationId: acousticCustomizationId, baseModelVersion: baseModelVersion, customizationWeight: customizationWeight, inactivityTimeout: inactivityTimeout, keywords: keywords, keywordsThreshold: keywordsThreshold, maxAlternatives: maxAlternatives, wordAlternativesThreshold: wordAlternativesThreshold, wordConfidence: wordConfidence, timestamps: timestamps, profanityFilter: profanityFilter, smartFormatting: smartFormatting, speakerLabels: speakerLabels, customizationId: customizationId, grammarName: grammarName, redaction: redaction, audioMetrics: audioMetrics, endOfPhraseSilenceTime: endOfPhraseSilenceTime, splitTranscriptAtPhraseEnd: splitTranscriptAtPhraseEnd);


            string messageUrl = $"{service.ServiceUrl}/v1/recognize";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestRegisterCallbackAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'status': 'created', 'url': 'Url'}";
            var response = new DetailedResponse<RegisterStatus>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<RegisterStatus>(responseJson),
                StatusCode = 200
            };

            string callbackUrl = "testString";
            string userSecret = "testString";

            request.As<RegisterStatus>().Returns(Task.FromResult(response));

            var result = service.RegisterCallback(callbackUrl: callbackUrl, userSecret: userSecret);


            string messageUrl = $"{service.ServiceUrl}/v1/register_callback";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUnregisterCallbackAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 200
            };

            string callbackUrl = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.UnregisterCallback(callbackUrl: callbackUrl);


            string messageUrl = $"{service.ServiceUrl}/v1/unregister_callback";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateJobAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'id': 'Id', 'status': 'waiting', 'created': 'Created', 'updated': 'Updated', 'url': 'Url', 'user_token': 'UserToken', 'results': [{'results': [{'final': false, 'alternatives': [{'transcript': 'Transcript', 'confidence': 10, 'timestamps': [['Timestamps']], 'word_confidence': [['WordConfidence']]}], 'keywords_result': {}, 'word_alternatives': [{'start_time': 9, 'end_time': 7, 'alternatives': [{'confidence': 10, 'word': 'Word'}]}], 'end_of_utterance': 'end_of_data'}], 'result_index': 11, 'speaker_labels': [{'from': 4, 'to': 2, 'speaker': 7, 'confidence': 10, 'final': false}], 'processing_metrics': {'processed_audio': {'received': 8, 'seen_by_engine': 12, 'transcription': 13, 'speaker_labels': 13}, 'wall_clock_since_first_byte_received': 31, 'periodic': true}, 'audio_metrics': {'sampling_interval': 16, 'accumulated': {'final': false, 'end_time': 7, 'signal_to_noise_ratio': 18, 'speech_ratio': 11, 'high_frequency_loss': 17, 'direct_current_offset': [{'begin': 5, 'end': 3, 'count': 5}], 'clipping_rate': [{'begin': 5, 'end': 3, 'count': 5}], 'speech_level': [{'begin': 5, 'end': 3, 'count': 5}], 'non_speech_level': [{'begin': 5, 'end': 3, 'count': 5}]}}, 'warnings': ['Warnings']}], 'warnings': ['Warnings']}";
            var response = new DetailedResponse<RecognitionJob>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<RecognitionJob>(responseJson),
                StatusCode = 201
            };

            System.IO.MemoryStream audio = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string contentType = "application/octet-stream";
            string model = "ar-AR_BroadbandModel";
            string callbackUrl = "testString";
            string events = "recognitions.started";
            string userToken = "testString";
            long? resultsTtl = 38;
            string languageCustomizationId = "testString";
            string acousticCustomizationId = "testString";
            string baseModelVersion = "testString";
            double? customizationWeight = 72.5f;
            long? inactivityTimeout = 38;
            List<string> keywords = new List<string> { "testString" };
            float? keywordsThreshold = 36.0f;
            long? maxAlternatives = 38;
            float? wordAlternativesThreshold = 36.0f;
            bool? wordConfidence = true;
            bool? timestamps = true;
            bool? profanityFilter = true;
            bool? smartFormatting = true;
            bool? speakerLabels = true;
            string customizationId = "testString";
            string grammarName = "testString";
            bool? redaction = true;
            bool? processingMetrics = true;
            float? processingMetricsInterval = 36.0f;
            bool? audioMetrics = true;
            double? endOfPhraseSilenceTime = 72.5f;
            bool? splitTranscriptAtPhraseEnd = true;

            request.As<RecognitionJob>().Returns(Task.FromResult(response));

            var result = service.CreateJob(audio: audio, contentType: contentType, model: model, callbackUrl: callbackUrl, events: events, userToken: userToken, resultsTtl: resultsTtl, languageCustomizationId: languageCustomizationId, acousticCustomizationId: acousticCustomizationId, baseModelVersion: baseModelVersion, customizationWeight: customizationWeight, inactivityTimeout: inactivityTimeout, keywords: keywords, keywordsThreshold: keywordsThreshold, maxAlternatives: maxAlternatives, wordAlternativesThreshold: wordAlternativesThreshold, wordConfidence: wordConfidence, timestamps: timestamps, profanityFilter: profanityFilter, smartFormatting: smartFormatting, speakerLabels: speakerLabels, customizationId: customizationId, grammarName: grammarName, redaction: redaction, processingMetrics: processingMetrics, processingMetricsInterval: processingMetricsInterval, audioMetrics: audioMetrics, endOfPhraseSilenceTime: endOfPhraseSilenceTime, splitTranscriptAtPhraseEnd: splitTranscriptAtPhraseEnd);


            string messageUrl = $"{service.ServiceUrl}/v1/recognitions";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCheckJobsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'recognitions': [{'id': 'Id', 'status': 'waiting', 'created': 'Created', 'updated': 'Updated', 'url': 'Url', 'user_token': 'UserToken', 'results': [{'results': [{'final': false, 'alternatives': [{'transcript': 'Transcript', 'confidence': 10, 'timestamps': [['Timestamps']], 'word_confidence': [['WordConfidence']]}], 'keywords_result': {}, 'word_alternatives': [{'start_time': 9, 'end_time': 7, 'alternatives': [{'confidence': 10, 'word': 'Word'}]}], 'end_of_utterance': 'end_of_data'}], 'result_index': 11, 'speaker_labels': [{'from': 4, 'to': 2, 'speaker': 7, 'confidence': 10, 'final': false}], 'processing_metrics': {'processed_audio': {'received': 8, 'seen_by_engine': 12, 'transcription': 13, 'speaker_labels': 13}, 'wall_clock_since_first_byte_received': 31, 'periodic': true}, 'audio_metrics': {'sampling_interval': 16, 'accumulated': {'final': false, 'end_time': 7, 'signal_to_noise_ratio': 18, 'speech_ratio': 11, 'high_frequency_loss': 17, 'direct_current_offset': [{'begin': 5, 'end': 3, 'count': 5}], 'clipping_rate': [{'begin': 5, 'end': 3, 'count': 5}], 'speech_level': [{'begin': 5, 'end': 3, 'count': 5}], 'non_speech_level': [{'begin': 5, 'end': 3, 'count': 5}]}}, 'warnings': ['Warnings']}], 'warnings': ['Warnings']}]}";
            var response = new DetailedResponse<RecognitionJobs>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<RecognitionJobs>(responseJson),
                StatusCode = 200
            };


            request.As<RecognitionJobs>().Returns(Task.FromResult(response));

            var result = service.CheckJobs();


            string messageUrl = $"{service.ServiceUrl}/v1/recognitions";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCheckJobAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'id': 'Id', 'status': 'waiting', 'created': 'Created', 'updated': 'Updated', 'url': 'Url', 'user_token': 'UserToken', 'results': [{'results': [{'final': false, 'alternatives': [{'transcript': 'Transcript', 'confidence': 10, 'timestamps': [['Timestamps']], 'word_confidence': [['WordConfidence']]}], 'keywords_result': {}, 'word_alternatives': [{'start_time': 9, 'end_time': 7, 'alternatives': [{'confidence': 10, 'word': 'Word'}]}], 'end_of_utterance': 'end_of_data'}], 'result_index': 11, 'speaker_labels': [{'from': 4, 'to': 2, 'speaker': 7, 'confidence': 10, 'final': false}], 'processing_metrics': {'processed_audio': {'received': 8, 'seen_by_engine': 12, 'transcription': 13, 'speaker_labels': 13}, 'wall_clock_since_first_byte_received': 31, 'periodic': true}, 'audio_metrics': {'sampling_interval': 16, 'accumulated': {'final': false, 'end_time': 7, 'signal_to_noise_ratio': 18, 'speech_ratio': 11, 'high_frequency_loss': 17, 'direct_current_offset': [{'begin': 5, 'end': 3, 'count': 5}], 'clipping_rate': [{'begin': 5, 'end': 3, 'count': 5}], 'speech_level': [{'begin': 5, 'end': 3, 'count': 5}], 'non_speech_level': [{'begin': 5, 'end': 3, 'count': 5}]}}, 'warnings': ['Warnings']}], 'warnings': ['Warnings']}";
            var response = new DetailedResponse<RecognitionJob>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<RecognitionJob>(responseJson),
                StatusCode = 200
            };

            string id = "testString";

            request.As<RecognitionJob>().Returns(Task.FromResult(response));

            var result = service.CheckJob(id: id);


            string messageUrl = $"{service.ServiceUrl}/v1/recognitions/{id}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteJobAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string id = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteJob(id: id);


            string messageUrl = $"{service.ServiceUrl}/v1/recognitions/{id}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateLanguageModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'customization_id': 'CustomizationId', 'created': 'Created', 'updated': 'Updated', 'language': 'Language', 'dialect': 'Dialect', 'versions': ['Versions'], 'owner': 'Owner', 'name': 'Name', 'description': 'Description', 'base_model_name': 'BaseModelName', 'status': 'pending', 'progress': 8, 'error': 'Error', 'warnings': 'Warnings'}";
            var response = new DetailedResponse<LanguageModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<LanguageModel>(responseJson),
                StatusCode = 201
            };

            string name = "testString";
            string baseModelName = "de-DE_BroadbandModel";
            string dialect = "testString";
            string description = "testString";

            request.As<LanguageModel>().Returns(Task.FromResult(response));

            var result = service.CreateLanguageModel(name: name, baseModelName: baseModelName, dialect: dialect, description: description);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListLanguageModelsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'customizations': [{'customization_id': 'CustomizationId', 'created': 'Created', 'updated': 'Updated', 'language': 'Language', 'dialect': 'Dialect', 'versions': ['Versions'], 'owner': 'Owner', 'name': 'Name', 'description': 'Description', 'base_model_name': 'BaseModelName', 'status': 'pending', 'progress': 8, 'error': 'Error', 'warnings': 'Warnings'}]}";
            var response = new DetailedResponse<LanguageModels>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<LanguageModels>(responseJson),
                StatusCode = 200
            };

            string language = "testString";

            request.As<LanguageModels>().Returns(Task.FromResult(response));

            var result = service.ListLanguageModels(language: language);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetLanguageModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'customization_id': 'CustomizationId', 'created': 'Created', 'updated': 'Updated', 'language': 'Language', 'dialect': 'Dialect', 'versions': ['Versions'], 'owner': 'Owner', 'name': 'Name', 'description': 'Description', 'base_model_name': 'BaseModelName', 'status': 'pending', 'progress': 8, 'error': 'Error', 'warnings': 'Warnings'}";
            var response = new DetailedResponse<LanguageModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<LanguageModel>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<LanguageModel>().Returns(Task.FromResult(response));

            var result = service.GetLanguageModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteLanguageModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteLanguageModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestTrainLanguageModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'warnings': [{'code': 'invalid_audio_files', 'message': 'Message'}]}";
            var response = new DetailedResponse<TrainingResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingResponse>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string wordTypeToAdd = "all";
            double? customizationWeight = 72.5f;

            request.As<TrainingResponse>().Returns(Task.FromResult(response));

            var result = service.TrainLanguageModel(customizationId: customizationId, wordTypeToAdd: wordTypeToAdd, customizationWeight: customizationWeight);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/train";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestResetLanguageModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.ResetLanguageModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/reset";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpgradeLanguageModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.UpgradeLanguageModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/upgrade_model";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListCorporaAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'corpora': [{'name': 'Name', 'total_words': 10, 'out_of_vocabulary_words': 20, 'status': 'analyzed', 'error': 'Error'}]}";
            var response = new DetailedResponse<Corpora>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Corpora>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<Corpora>().Returns(Task.FromResult(response));

            var result = service.ListCorpora(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/corpora";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddCorpusAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 201
            };

            string customizationId = "testString";
            string corpusName = "testString";
            System.IO.MemoryStream corpusFile = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            bool? allowOverwrite = true;

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.AddCorpus(customizationId: customizationId, corpusName: corpusName, corpusFile: corpusFile, allowOverwrite: allowOverwrite);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/corpora/{corpusName}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetCorpusAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'name': 'Name', 'total_words': 10, 'out_of_vocabulary_words': 20, 'status': 'analyzed', 'error': 'Error'}";
            var response = new DetailedResponse<Corpus>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Corpus>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string corpusName = "testString";

            request.As<Corpus>().Returns(Task.FromResult(response));

            var result = service.GetCorpus(customizationId: customizationId, corpusName: corpusName);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/corpora/{corpusName}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteCorpusAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";
            string corpusName = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteCorpus(customizationId: customizationId, corpusName: corpusName);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/corpora/{corpusName}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListWordsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'words': [{'word': '_Word', 'sounds_like': ['SoundsLike'], 'display_as': 'DisplayAs', 'count': 5, 'source': ['Source'], 'error': [{'element': 'Element'}]}]}";
            var response = new DetailedResponse<Words>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Words>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string wordType = "all";
            string sort = "alphabetical";

            request.As<Words>().Returns(Task.FromResult(response));

            var result = service.ListWords(customizationId: customizationId, wordType: wordType, sort: sort);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddWordsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 201
            };

            var CustomWordSoundsLike = new List<string> { "testString" };
            CustomWord CustomWordModel = new CustomWord()
            {
                Word = "testString",
                SoundsLike = CustomWordSoundsLike,
                DisplayAs = "testString"
            };
            string customizationId = "testString";
            List<CustomWord> words = new List<CustomWord> { CustomWordModel };

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.AddWords(customizationId: customizationId, words: words);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddWordAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 201
            };

            string customizationId = "testString";
            string wordName = "testString";
            string word = "testString";
            List<string> soundsLike = new List<string> { "testString" };
            string displayAs = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.AddWord(customizationId: customizationId, wordName: wordName, word: word, soundsLike: soundsLike, displayAs: displayAs);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{wordName}";
            client.Received().PutAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetWordAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'word': '_Word', 'sounds_like': ['SoundsLike'], 'display_as': 'DisplayAs', 'count': 5, 'source': ['Source'], 'error': [{'element': 'Element'}]}";
            var response = new DetailedResponse<Word>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Word>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string wordName = "testString";

            request.As<Word>().Returns(Task.FromResult(response));

            var result = service.GetWord(customizationId: customizationId, wordName: wordName);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{wordName}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteWordAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";
            string wordName = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteWord(customizationId: customizationId, wordName: wordName);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/words/{wordName}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListGrammarsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'grammars': [{'name': 'Name', 'out_of_vocabulary_words': 20, 'status': 'analyzed', 'error': 'Error'}]}";
            var response = new DetailedResponse<Grammars>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Grammars>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<Grammars>().Returns(Task.FromResult(response));

            var result = service.ListGrammars(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/grammars";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddGrammarAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 201
            };

            string customizationId = "testString";
            string grammarName = "testString";
            string grammarFile = "testString";
            string contentType = "application/srgs";
            bool? allowOverwrite = true;

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.AddGrammar(customizationId: customizationId, grammarName: grammarName, grammarFile: grammarFile, contentType: contentType, allowOverwrite: allowOverwrite);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/grammars/{grammarName}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetGrammarAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'name': 'Name', 'out_of_vocabulary_words': 20, 'status': 'analyzed', 'error': 'Error'}";
            var response = new DetailedResponse<Grammar>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<Grammar>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string grammarName = "testString";

            request.As<Grammar>().Returns(Task.FromResult(response));

            var result = service.GetGrammar(customizationId: customizationId, grammarName: grammarName);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/grammars/{grammarName}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteGrammarAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";
            string grammarName = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteGrammar(customizationId: customizationId, grammarName: grammarName);


            string messageUrl = $"{service.ServiceUrl}/v1/customizations/{customizationId}/grammars/{grammarName}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateAcousticModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'customization_id': 'CustomizationId', 'created': 'Created', 'updated': 'Updated', 'language': 'Language', 'versions': ['Versions'], 'owner': 'Owner', 'name': 'Name', 'description': 'Description', 'base_model_name': 'BaseModelName', 'status': 'pending', 'progress': 8, 'warnings': 'Warnings'}";
            var response = new DetailedResponse<AcousticModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<AcousticModel>(responseJson),
                StatusCode = 201
            };

            string name = "testString";
            string baseModelName = "ar-AR_BroadbandModel";
            string description = "testString";

            request.As<AcousticModel>().Returns(Task.FromResult(response));

            var result = service.CreateAcousticModel(name: name, baseModelName: baseModelName, description: description);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListAcousticModelsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'customizations': [{'customization_id': 'CustomizationId', 'created': 'Created', 'updated': 'Updated', 'language': 'Language', 'versions': ['Versions'], 'owner': 'Owner', 'name': 'Name', 'description': 'Description', 'base_model_name': 'BaseModelName', 'status': 'pending', 'progress': 8, 'warnings': 'Warnings'}]}";
            var response = new DetailedResponse<AcousticModels>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<AcousticModels>(responseJson),
                StatusCode = 200
            };

            string language = "testString";

            request.As<AcousticModels>().Returns(Task.FromResult(response));

            var result = service.ListAcousticModels(language: language);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetAcousticModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'customization_id': 'CustomizationId', 'created': 'Created', 'updated': 'Updated', 'language': 'Language', 'versions': ['Versions'], 'owner': 'Owner', 'name': 'Name', 'description': 'Description', 'base_model_name': 'BaseModelName', 'status': 'pending', 'progress': 8, 'warnings': 'Warnings'}";
            var response = new DetailedResponse<AcousticModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<AcousticModel>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<AcousticModel>().Returns(Task.FromResult(response));

            var result = service.GetAcousticModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteAcousticModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteAcousticModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestTrainAcousticModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'warnings': [{'code': 'invalid_audio_files', 'message': 'Message'}]}";
            var response = new DetailedResponse<TrainingResponse>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TrainingResponse>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string customLanguageModelId = "testString";

            request.As<TrainingResponse>().Returns(Task.FromResult(response));

            var result = service.TrainAcousticModel(customizationId: customizationId, customLanguageModelId: customLanguageModelId);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/train";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestResetAcousticModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.ResetAcousticModel(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/reset";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestUpgradeAcousticModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";
            string customLanguageModelId = "testString";
            bool? force = true;

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.UpgradeAcousticModel(customizationId: customizationId, customLanguageModelId: customLanguageModelId, force: force);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/upgrade_model";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListAudioAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'total_minutes_of_audio': 19, 'audio': [{'duration': 8, 'name': 'Name', 'details': {'type': 'audio', 'codec': 'Codec', 'frequency': 9, 'compression': 'zip'}, 'status': 'ok'}]}";
            var response = new DetailedResponse<AudioResources>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<AudioResources>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";

            request.As<AudioResources>().Returns(Task.FromResult(response));

            var result = service.ListAudio(customizationId: customizationId);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/audio";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestAddAudioAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 201
            };

            string customizationId = "testString";
            string audioName = "testString";
            System.IO.MemoryStream audioResource = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string contentType = "application/zip";
            string containedContentType = "audio/alaw";
            bool? allowOverwrite = true;

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.AddAudio(customizationId: customizationId, audioName: audioName, audioResource: audioResource, contentType: contentType, containedContentType: containedContentType, allowOverwrite: allowOverwrite);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/audio/{audioName}";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetAudioAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var responseJson = "{'duration': 8, 'name': 'Name', 'details': {'type': 'audio', 'codec': 'Codec', 'frequency': 9, 'compression': 'zip'}, 'status': 'ok', 'container': {'duration': 8, 'name': 'Name', 'details': {'type': 'audio', 'codec': 'Codec', 'frequency': 9, 'compression': 'zip'}, 'status': 'ok'}, 'audio': [{'duration': 8, 'name': 'Name', 'details': {'type': 'audio', 'codec': 'Codec', 'frequency': 9, 'compression': 'zip'}, 'status': 'ok'}]}";
            var response = new DetailedResponse<AudioListing>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<AudioListing>(responseJson),
                StatusCode = 200
            };

            string customizationId = "testString";
            string audioName = "testString";

            request.As<AudioListing>().Returns(Task.FromResult(response));

            var result = service.GetAudio(customizationId: customizationId, audioName: audioName);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/audio/{audioName}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteAudioAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
            var response = new DetailedResponse<object>()
            {
                Result = new object(),
                StatusCode = 200
            };

            string customizationId = "testString";
            string audioName = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteAudio(customizationId: customizationId, audioName: audioName);


            string messageUrl = $"{service.ServiceUrl}/v1/acoustic_customizations/{customizationId}/audio/{audioName}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteUserDataAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            SpeechToTextService service = new SpeechToTextService(client);
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
