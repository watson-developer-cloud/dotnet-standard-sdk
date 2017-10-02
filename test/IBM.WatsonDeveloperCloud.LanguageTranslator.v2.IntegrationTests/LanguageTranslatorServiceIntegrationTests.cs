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

using IBM.WatsonDeveloperCloud.LanguageTranslator.v2;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.IntegrationTests
{
    [TestClass]
    public class LanguageTranslatorServiceIntegrationTests
    {
        private string _userName;
        private string _password;
        private string _endpoint;

        [TestInitialize]
        public void Setup()
        {
            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
            File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            _endpoint = vcapServices["language_translator"][0]["credentials"]["url"].Value<string>();
            _userName = vcapServices["language_translator"][0]["credentials"]["username"].Value<string>();
            _password = vcapServices["language_translator"][0]["credentials"]["password"].Value<string>();
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService(_userName, _password);
            
            var results = service.ListIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService(_userName, _password);

            var results = service.Identify("Hello! How are you?");

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService(_userName, _password);

            var translateRequest = new TranslateRequest()
            {
                Text = new List<string>()
                {
                    "Hello! How are you?"
                },
                Source = "en",
                Target = "pt"
            };

            var results = service.Translate(translateRequest);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Translations.Count > 0);
        }

        [TestMethod]
        public void LisListModels_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService(_userName, _password);
            
            var results = service.ListModels();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Models.Count > 0);
        }

        [TestMethod]
        public void GetModelDetails()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService(_userName, _password);
            
            var results = service.GetModel("en-pt");

            Assert.IsNotNull(results);
            Assert.IsFalse(string.IsNullOrEmpty(results.ModelId));
        }
    }
}