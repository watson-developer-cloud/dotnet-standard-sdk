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

using IBM.WatsonDeveloperCloud.LanguageTranslator.v3.Model;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v3.IntegrationTests
{
    [TestClass]
    public class LTServiceIntTestBasicAuthIamApikey
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private static LanguageTranslatorService _service;
        private static string credentials = string.Empty;
        
        private static string _baseModel = "en-fr";
        private static string _text = "I'm sorry, Dave. I'm afraid I can't do that.";
        private string _versionDate = "2018-05-01";

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

                Credential credential = vcapCredentials.GetCredentialByname("language-translator-v3-sdk-rc-wdc")[0].Credentials;
                _endpoint = credential.Url;
                _username = "apikey";
                _password = credential.IamApikey;
            }
            #endregion

            _service = new LanguageTranslatorService(_username, _password, _versionDate);
            _service.SetEndpoint(_endpoint);
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Sucess_IamAsBasicAuth()
        {
            var results = _service.ListIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess_IamAsBasicAuth()
        {
            var results = _service.Identify(_text);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess_IamAsBasicAuth()
        {
            var translateRequest = new TranslateRequest()
            {
                Text = new List<string>()
                {
                    _text
                },
                ModelId = _baseModel
            };

            var results = _service.Translate(translateRequest);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Translations.Count > 0);
        }

        [TestMethod]
        public void ListModels_Sucess_IamAsBasicAuth()
        {
            var results = _service.ListModels();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Models.Count > 0);
        }
    }
}
