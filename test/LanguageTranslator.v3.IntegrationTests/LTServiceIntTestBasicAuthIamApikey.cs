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

using IBM.Watson.LanguageTranslator.v3.Model;
using IBM.Watson.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.LanguageTranslator.v3.IntegrationTests
{
    [TestClass]
    public class LTServiceIntTestBasicAuthIamApikey
    {
        private static string username;
        private static string password;
        private static string endpoint;
        private static LanguageTranslatorService service;
        private static string credentials = string.Empty;
        
        private static string baseModel = "en-fr";
        private static string text = "I'm sorry, Dave. I'm afraid I can't do that.";
        private string versionDate = "2018-05-01";

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

                Credential credential = vcapCredentials.GetCredentialByname("language-translator-sdk")[0].Credentials;
                endpoint = credential.Url;
                username = "apikey";
                password = credential.IamApikey;
            }
            #endregion

            service = new LanguageTranslatorService(username, password, versionDate);
            service.SetEndpoint(endpoint);
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Sucess_IamAsBasicAuth()
        {
            var results = service.ListIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess_IamAsBasicAuth()
        {
            var results = service.Identify(text);

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
                    text
                },
                ModelId = baseModel
            };

            var results = service.Translate(translateRequest);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Translations.Count > 0);
        }

        [TestMethod]
        public void ListModels_Sucess_IamAsBasicAuth()
        {
            var results = service.ListModels();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Models.Count > 0);
        }
    }
}
