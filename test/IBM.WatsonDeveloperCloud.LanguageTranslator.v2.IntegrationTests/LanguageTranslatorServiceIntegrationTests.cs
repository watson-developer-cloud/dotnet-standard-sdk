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

using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;
using IBM.WatsonDeveloperCloud.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.v2.IntegrationTests
{
    [TestClass]
    public class LanguageTranslatorServiceIntegrationTests
    {
        private static string _userName;
        private static string _password;
        private static string _endpoint;
        private LanguageTranslatorService languageTranslator;
        private static string credentials = string.Empty;

        private static string _glossaryPath = "glossary.tmx";
        private static string _baseModel = "en-fr";
        private static string _customModelName = "dotnetExampleModel";
        private static string _customModelID = "en-fr";
        private static string _text = "I'm sorry, Dave. I'm afraid I can't do that.";

        [TestInitialize]
        public void Setup()
        {
            if (string.IsNullOrEmpty(credentials))
            {
                try
                {
                    credentials = Utility.SimpleGet(
                        Environment.GetEnvironmentVariable("VCAP_URL"),
                        Environment.GetEnvironmentVariable("VCAP_USERNAME"),
                        Environment.GetEnvironmentVariable("VCAP_PASSWORD")).Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Failed to get credentials: {0}", e.Message));
                }

                Task.WaitAll();

                var vcapServices = JObject.Parse(credentials);

                _endpoint = vcapServices["language_translator"]["url"].Value<string>();
                _userName = vcapServices["language_translator"]["username"].Value<string>();
                _password = vcapServices["language_translator"]["password"].Value<string>();
            }
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Sucess()
        {
            languageTranslator =
                new LanguageTranslatorService(_userName, _password);
            languageTranslator.Endpoint = _endpoint;

            var results = languageTranslator.ListIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess()
        {
            languageTranslator =
                new LanguageTranslatorService(_userName, _password);
            languageTranslator.Endpoint = _endpoint;

            var results = languageTranslator.Identify(_text);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess()
        {
            languageTranslator =
                new LanguageTranslatorService(_userName, _password);
            languageTranslator.Endpoint = _endpoint;

            var translateRequest = new TranslateRequest()
            {
                Text = new List<string>()
                {
                    _text
                },
                ModelId = _baseModel
            };

            var results = languageTranslator.Translate(translateRequest);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Translations.Count > 0);
        }

        [TestMethod]
        public void LisListModels_Sucess()
        {
            languageTranslator =
                new LanguageTranslatorService(_userName, _password);
            languageTranslator.Endpoint = _endpoint;

            var results = languageTranslator.ListModels();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Models.Count > 0);
        }

        [TestMethod]
        public void GetModelDetails_Success()
        {
            languageTranslator =
                new LanguageTranslatorService(_userName, _password);
            languageTranslator.Endpoint = _endpoint;

            var results = languageTranslator.GetModel(_baseModel);

            Assert.IsNotNull(results);
            Assert.IsFalse(string.IsNullOrEmpty(results.ModelId));
        }

        [TestMethod]
        public void CreateModel_Success()
        {
            languageTranslator =
                new LanguageTranslatorService(_userName, _password);
            languageTranslator.Endpoint = _endpoint;

            TranslationModel result;

            using (FileStream fs = File.OpenRead(_glossaryPath))
            {
                result = languageTranslator.CreateModel(_baseModel, _customModelName, forcedGlossary: fs);

                if (result != null)
                {
                    _customModelID = result.ModelId;
                }
                else
                {
                    Console.WriteLine("result is null.");
                }
            }

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.ModelId));
        }

        [TestMethod]
        public void DeleteModel_Success()
        {
            languageTranslator =
                new LanguageTranslatorService(_userName, _password);
            languageTranslator.Endpoint = _endpoint;

            var result = languageTranslator.DeleteModel(_customModelID);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == "OK");
        }

        #region Generated
        #region Translate
        private TranslationResult Translate(TranslateRequest request)
        {
            Console.WriteLine("\nAttempting to Translate()");
            var result = service.Translate(request: request);

            if (result != null)
            {
                Console.WriteLine("Translate() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Translate()");
            }

            return result;
        }
        #endregion

        #region Identify
        private IdentifiedLanguages Identify(string text)
        {
            Console.WriteLine("\nAttempting to Identify()");
            var result = service.Identify(text: text);

            if (result != null)
            {
                Console.WriteLine("Identify() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Identify()");
            }

            return result;
        }
        #endregion

        #region IdentifyPlain
        private IdentifiedLanguages IdentifyPlain(string text)
        {
            Console.WriteLine("\nAttempting to IdentifyPlain()");
            var result = service.IdentifyPlain(text: text);

            if (result != null)
            {
                Console.WriteLine("IdentifyPlain() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to IdentifyPlain()");
            }

            return result;
        }
        #endregion

        #region ListIdentifiableLanguages
        private IdentifiableLanguages ListIdentifiableLanguages()
        {
            Console.WriteLine("\nAttempting to ListIdentifiableLanguages()");
            var result = service.ListIdentifiableLanguages();

            if (result != null)
            {
                Console.WriteLine("ListIdentifiableLanguages() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListIdentifiableLanguages()");
            }

            return result;
        }
        #endregion

        #region CreateModel
        private TranslationModel CreateModel(string baseModelId, string name = null, System.IO.Stream forcedGlossary = null, System.IO.Stream parallelCorpus = null, System.IO.Stream monolingualCorpus = null)
        {
            Console.WriteLine("\nAttempting to CreateModel()");
            var result = service.CreateModel(baseModelId: baseModelId, name: name, forcedGlossary: forcedGlossary, parallelCorpus: parallelCorpus, monolingualCorpus: monolingualCorpus);

            if (result != null)
            {
                Console.WriteLine("CreateModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to CreateModel()");
            }

            return result;
        }
        #endregion

        #region DeleteModel
        private DeleteModelResult DeleteModel(string modelId)
        {
            Console.WriteLine("\nAttempting to DeleteModel()");
            var result = service.DeleteModel(modelId: modelId);

            if (result != null)
            {
                Console.WriteLine("DeleteModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to DeleteModel()");
            }

            return result;
        }
        #endregion

        #region GetModel
        private TranslationModel GetModel(string modelId)
        {
            Console.WriteLine("\nAttempting to GetModel()");
            var result = service.GetModel(modelId: modelId);

            if (result != null)
            {
                Console.WriteLine("GetModel() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to GetModel()");
            }

            return result;
        }
        #endregion

        #region ListModels
        private TranslationModels ListModels(string source = null, string target = null, bool? defaultModels = null)
        {
            Console.WriteLine("\nAttempting to ListModels()");
            var result = service.ListModels(source: source, target: target, defaultModels: defaultModels);

            if (result != null)
            {
                Console.WriteLine("ListModels() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ListModels()");
            }

            return result;
        }
        #endregion
        #endregion
    }
}