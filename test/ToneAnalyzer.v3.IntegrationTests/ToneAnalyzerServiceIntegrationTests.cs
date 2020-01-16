/**
* (C) Copyright IBM Corp. 2017, 2019.
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

using IBM.Watson.ToneAnalyzer.v3.Model;
using IBM.Cloud.SDK.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IBM.Watson.ToneAnalyzer.v3.IntegrationTests
{

    [TestClass]
    public class ToneAnalyzerServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private string inputText = "Hello! Welcome to IBM Watson! How can I help you?";
        private string chatUser = "testChatUser";
        private string versionDate = "2016-05-19";
        private static string credentials = string.Empty;
        private ToneAnalyzerService service;

        [TestInitialize]
        public void Setup()
        {
            service = new ToneAnalyzerService(versionDate);
        }

        [TestMethod]
        public void PostTone_Success()
        {
            ToneInput toneInput = new ToneInput()
            {
                Text = inputText
            };

            service.WithHeader("X-Watson-Test", "1");
            var result = service.Tone(
                toneInput: toneInput,
                contentType: "text/html",
                sentences: true,
                contentLanguage: "en-US",
                acceptLanguage: "en-US"
                );

            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.DocumentTone.ToneCategories.Count >= 1);
            Assert.IsTrue(result.Result.DocumentTone.ToneCategories[0].Tones.Count >= 1);
        }

        [TestMethod]
        public void ToneChat_Success()
        {
            var utterances = new List<Utterance>()
            {
                new Utterance()
                {
                    Text = inputText,
                    User = chatUser
                }
            };
            service.WithHeader("X-Watson-Test", "1");
            var result = service.ToneChat(
                utterances: utterances,
                contentLanguage: "en-US",
                acceptLanguage: "en-US"
                );

            Assert.IsNotNull(result.Result);
            Assert.IsTrue(result.Result.UtterancesTone.Count > 0);
        }
    }
}
