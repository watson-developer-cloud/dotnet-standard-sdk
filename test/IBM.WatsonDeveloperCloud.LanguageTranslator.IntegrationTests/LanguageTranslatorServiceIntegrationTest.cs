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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.IntegrationTests
{
    [TestClass]
    public class LanguageTranslatorServiceIntegrationTest
    {
        [TestMethod]
        public void GetIdentifiableLanguages_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.GetIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.Identify("Hello! How are you?");

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.Translate("en", "pt", "Hello! How are you?");

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Translations.Count > 0);
        }

        [TestMethod]
        public void LisListModels_Sucess()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.ListModels(true, "en", string.Empty);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Models.Count > 0);
        }

        [TestMethod]
        public void GetModelDetails()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService();

            service.Endpoint = "https://watson-api-explorer.mybluemix.net/language-translation/api";
            var results = service.GetModelDetails("en-pt");

            Assert.IsNotNull(results);
            Assert.IsFalse(string.IsNullOrEmpty(results.ModelId));
        }
    }
}