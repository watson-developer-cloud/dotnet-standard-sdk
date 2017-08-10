/**
* Copyright 2017 IBM Corp. All Rights Reserved.
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

using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.IntegrationTests
{

    [TestClass]
    public class ToneAnalyzerServiceIntegrationTests
    {
        private string _userName;
        private string _password;
        private string _endpoint;
        private string inputText = "Hello! Welcome to IBM Watson! How can I help you?";
        private string chatUser = "testChatUser";
        private string versionDate = "2016-05-19";

        [TestInitialize]
        public void Setup()
        {
            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
            File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            _endpoint = vcapServices["tone_analyzer"][0]["credentials"]["url"].Value<string>();
            _userName = vcapServices["tone_analyzer"][0]["credentials"]["username"].Value<string>();
            _password = vcapServices["tone_analyzer"][0]["credentials"]["password"].Value<string>();
        }

        [TestMethod]
        public void PostTone_Success()
        {
            ToneAnalyzerService service = new ToneAnalyzerService(_userName, _password, versionDate);

            ToneInput toneInput = new ToneInput()
            {
                Text = inputText
            };

            var result = service.Tone(toneInput, "text/html", null);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.DocumentTone.ToneCategories.Count >= 1);
            Assert.IsTrue(result.DocumentTone.ToneCategories[0].Tones.Count >= 1);
        }

        [TestMethod]
        public void ToneChat_Success()
        {
            ToneAnalyzerService service = new ToneAnalyzerService(_userName, _password, versionDate);

            ToneChatInput toneChatInput = new ToneChatInput()
            {
                Utterances = new List<Utterance>()
                {
                    new Utterance()
                    {
                        Text = inputText,
                        User = chatUser
                    }
                }
            };
            var result = service.ToneChat(toneChatInput);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.UtterancesTone.Count > 0);
        }
    }
}
