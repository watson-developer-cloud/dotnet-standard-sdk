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

using IBM.Watson.TextToSpeech.v1.Model;
using IBM.Cloud.SDK.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using IBM.Cloud.SDK.Core;
using System.Threading.Tasks;
using IBM.Cloud.SDK.Core.Sockets;
using System.Diagnostics;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.TextToSpeech.v1.websocket;

namespace IBM.Watson.TextToSpeech.v1.IntegrationTests
{
    [TestClass]
    public class TextToSpeechServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private TextToSpeechService service;
        private static string credentials = string.Empty;
        private const string allisonVoice = "en-US_AllisonVoice";
        private string synthesizeText = "Hello, welcome to the Watson dotnet SDK!";
        private string voiceModelName = "dotnet-sdk-voice-model";
        private string voiceModelUpdatedName = "dotnet-sdk-voice-model-updated";
        private string voiceModelDescription = "Custom voice model for .NET SDK integration tests.";
        private string voiceModelUpdatedDescription = "Custom voice model for .NET SDK integration tests. Updated.";

        [TestInitialize]
        public void Setup()
        {
            service = new TextToSpeechService();
        }

        #region Voices
        [TestMethod]
        public void Voices_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listVoicesResult = service.ListVoices();

            service.WithHeader("X-Watson-Test", "1");
            var getVoiceResult = service.GetVoice(
                voice: allisonVoice
                );

            Assert.IsNotNull(getVoiceResult);
            Assert.IsTrue(!string.IsNullOrEmpty(getVoiceResult.Result.Name));
            Assert.IsNotNull(listVoicesResult);
            Assert.IsNotNull(listVoicesResult.Result._Voices);
        }
        #endregion

        #region Synthesize
        [TestMethod]
        public void Synthesize_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var synthesizeResult = service.Synthesize(
                text: synthesizeText,
                accept: "audio/wav",
                voice: allisonVoice
                );

            //  Save file
            using (FileStream fs = File.Create("synthesize.wav"))
            {
                synthesizeResult.Result.WriteTo(fs);
                fs.Close();
                synthesizeResult.Result.Close();
            }

            Assert.IsNotNull(synthesizeResult.Result);
        }

        [TestMethod]
        public void Synthesize_sockets_SuccessAsync()
        {
            service.WithHeader("X-Watson-Test", "1");
            string url = ("{url}/v1/synthesize").Replace("https://", "wss://");
            //wss://api.{location}.text-to-speech.watson.cloud.ibm.com/instances/{instance_id}/v1/synthesize
            WebSocketClientTest callback = new WebSocketClientTest(url);

            var bearerToken = "";
            callback.WithHeader("Authorization", "Bearer " + bearerToken);

            callback.OnOpen = () =>
            {
                System.Diagnostics.Debug.WriteLine("WEBSOCKET IS OPEN");
            };
            callback.OnClose = () =>
            {
                System.Diagnostics.Debug.WriteLine("WEBSOCKET IS CLOSED");
            };
            callback.OnMessage = (message) =>
            {
                System.Diagnostics.Debug.WriteLine("MESSAGE");
                System.Diagnostics.Debug.WriteLine(message);

            };
            var synthesizeResult = service.SynthesizeUsingWebSocket(
                voice: allisonVoice,
                socketClient: callback
                );
            synthesizeResult.Send("hello test", "test");
        }
        #endregion

        #region Pronunciation
        [TestMethod]
        public void Pronunciation_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var getPronunciationResult = service.GetPronunciation(
                text: "IBM",
                voice: allisonVoice,
                format: "ipa"
                );

            Assert.IsNotNull(getPronunciationResult.Result);
            Assert.IsNotNull(getPronunciationResult.Result._Pronunciation);
        }
        #endregion

        #region Custom Voice Models
        [TestMethod]
        public void CustomVoiceModels_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listVoiceModelsResult = service.ListCustomModels();

            service.WithHeader("X-Watson-Test", "1");
            var createVoiceModelResult = service.CreateCustomModel(
                name: voiceModelName,
                language: "en-US",
                description: voiceModelDescription);
            var customizationId = createVoiceModelResult.Result.CustomizationId;

            service.WithHeader("X-Watson-Test", "1");
            var getVoiceModelResult = service.GetCustomModel(
                customizationId: customizationId
                );

            var words = new List<Word>()
            {
                new Word()
                {
                    _Word = "hello",
                    Translation = "hullo"
                },
                new Word()
                {
                    _Word = "goodbye",
                    Translation = "gbye"
                },
                new Word()
                {
                    _Word = "hi",
                    Translation = "ohioooo"
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var updateVoiceModelResult = service.UpdateCustomModel(
                customizationId: customizationId,
                name: voiceModelUpdatedName,
                description: voiceModelUpdatedDescription,
                words: words
                );

            service.WithHeader("X-Watson-Test", "1");
            var getVoiceModelResult2 = service.GetCustomModel(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteVoiceModelResult = service.DeleteCustomModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(deleteVoiceModelResult.StatusCode == 204);
            Assert.IsNotNull(getVoiceModelResult2.Result);
            Assert.IsTrue(getVoiceModelResult2.Result.Name == voiceModelUpdatedName);
            Assert.IsTrue(getVoiceModelResult2.Result.Description == voiceModelUpdatedDescription);
            Assert.IsTrue(getVoiceModelResult2.Result.Words.Count == 3);
            Assert.IsNotNull(getVoiceModelResult.Result);
            Assert.IsTrue(getVoiceModelResult.Result.Name == voiceModelName);
            Assert.IsTrue(getVoiceModelResult.Result.Description == voiceModelDescription);
            Assert.IsNotNull(createVoiceModelResult.Result);
            Assert.IsNotNull(listVoiceModelsResult.Result);
            Assert.IsNotNull(listVoiceModelsResult.Result.Customizations);
        }
        #endregion

        #region Words
        [TestMethod]
        public void Words_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var createVoiceModelResult = service.CreateCustomModel(
                name: voiceModelName,
                language: "en-US",
                description: voiceModelDescription
                );
            var customizationId = createVoiceModelResult.Result.CustomizationId;

            var words = new List<Word>()
            {
                new Word()
                {
                    _Word = "hello",
                    Translation = "hullo"
                },
                new Word()
                {
                    _Word = "goodbye",
                    Translation = "gbye"
                },
                new Word()
                {
                    _Word = "hi",
                    Translation = "ohioooo"
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var addWordsResult = service.AddWords(
                customizationId: customizationId,
                words: words
                );

            service.WithHeader("X-Watson-Test", "1");
            var listWordsResult = service.ListWords(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var getWordResult = service.GetWord(
                customizationId: customizationId,
                word: "hello"
                );

            service.WithHeader("X-Watson-Test", "1");
            var addWordResult = service.AddWord(
                customizationId: customizationId,
                word: "IBM",
                translation: "eye bee m",
                partOfSpeech: "noun"
                );

            service.WithHeader("X-Watson-Test", "1");
            var checkAddWordResult = service.ListWords(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteWordResult = service.DeleteWord(
                customizationId: customizationId,
                word: "hi"
                );

            service.WithHeader("X-Watson-Test", "1");
            var checkDeleteWordResult = service.ListWords(
                customizationId: customizationId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteVoiceModelResult = service.DeleteCustomModel(
                customizationId: customizationId
                );

            Assert.IsNotNull(checkDeleteWordResult.Result);
            Assert.IsNotNull(checkDeleteWordResult.Result._Words);
            Assert.IsTrue(checkDeleteWordResult.Result._Words.Count == 3);
            Assert.IsNotNull(checkAddWordResult.Result);
            Assert.IsNotNull(checkAddWordResult.Result._Words);
            Assert.IsTrue(checkAddWordResult.Result._Words.Count == 4);
            Assert.IsNotNull(getWordResult.Result);
            Assert.IsTrue(getWordResult.Result._Translation == "hullo");
            Assert.IsNotNull(listWordsResult.Result);
            Assert.IsNotNull(listWordsResult.Result._Words);
            Assert.IsTrue(listWordsResult.Result._Words.Count == 3);
            Assert.IsNotNull(addWordsResult.Result);
        }
        #endregion
    }
}
