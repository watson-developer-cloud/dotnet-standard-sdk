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

using IBM.Cloud.SDK.Core.Util;
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
            service = new LanguageTranslatorService(versionDate);
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Sucess_IamAsBasicAuth()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.ListIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess_IamAsBasicAuth()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.Identify(
                text: text
                );

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess_IamAsBasicAuth()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.Translate(
                text: new List<string>() { text },
                modelId: baseModel
                );

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Translations.Count > 0);
        }

        [TestMethod]
        public void ListModels_Sucess_IamAsBasicAuth()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.ListModels();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Models.Count > 0);
        }
    }
}
