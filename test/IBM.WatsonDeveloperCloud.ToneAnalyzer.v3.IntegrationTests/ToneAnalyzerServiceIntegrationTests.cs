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
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.IntegrationTests
{

    [TestClass]
    public class ToneAnalyzerServiceIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private string inputText = "Hello! Welcome to IBM Watson! How can I help you?";
        private string chatUser = "testChatUser";
        private string versionDate = "2016-05-19";
        private static string credentials = string.Empty;
        private ToneAnalyzerService _service;

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.FullName;
                string credentialsFilepath = parentDirectory + Path.DirectorySeparatorChar + "sdk-credentials" + Path.DirectorySeparatorChar + "credentials.json";
                if (File.Exists(credentialsFilepath))
                {
                    try
                    {
                        credentials = File.ReadAllText(credentialsFilepath);
                        credentials = Utility.AddTopLevelObjectToJson(credentials, "VCAP_SERVICES");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Failed to load credentials: {0}", e.Message));
                    }
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist.");
                }

                VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                var vcapServices = JObject.Parse(credentials);

                Credential credential = vcapCredentials.GetCredentialByname("tone-analyzer-sdk")[0].Credentials;
                _endpoint = credential.Url;
                _username = credential.Username;
                _password = credential.Password;
            }
            #endregion
        }

        [TestMethod]
        public void PostTone_Success()
        {
            _service = new ToneAnalyzerService(_username, _password, versionDate);

            ToneInput toneInput = new ToneInput()
            {
                Text = inputText
            };

            var result = _service.Tone(toneInput, "text/html", null);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.DocumentTone.ToneCategories.Count >= 1);
            Assert.IsTrue(result.DocumentTone.ToneCategories[0].Tones.Count >= 1);
        }

        [TestMethod]
        public void ToneChat_Success()
        {
            _service = new ToneAnalyzerService(_username, _password, versionDate);

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
            var result = _service.ToneChat(toneChatInput);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.UtterancesTone.Count > 0);
        }

        #region Generated
        #region Tone
        private ToneAnalysis Tone(ToneInput toneInput, string contentType, bool? sentences = null, List<string> tones = null, string contentLanguage = null, string acceptLanguage = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Tone()");
            var result = _service.Tone(toneInput: toneInput, contentType: contentType, sentences: sentences, tones: tones, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage, customData: customData);

            if (result != null)
            {
                Console.WriteLine("Tone() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Tone()");
            }

            return result;
        }
        #endregion

        #region ToneChat
        private UtteranceAnalyses ToneChat(ToneChatInput utterances, string contentLanguage = null, string acceptLanguage = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ToneChat()");
            var result = _service.ToneChat(utterances: utterances, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage, customData: customData);

            if (result != null)
            {
                Console.WriteLine("ToneChat() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ToneChat()");
            }

            return result;
        }
        #endregion

        #endregion
    }
}
