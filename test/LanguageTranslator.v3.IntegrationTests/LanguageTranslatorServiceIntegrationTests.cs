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
    public class LanguageTranslatorServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private static LanguageTranslatorService service;
        private static string credentials = string.Empty;

        private static string glossaryPath = "glossary.tmx";
        private static string baseModel = "en-fr";
        private static string customModelName = "dotnetExampleModel";
        private static string customModelID = "en-fr";
        private static string text = "I'm sorry, Dave. I'm afraid I can't do that.";
        private string versionDate = "2018-05-01";

        [TestInitialize]
        public void Setup()
        {
            #region Get Credentials
            if (string.IsNullOrEmpty(credentials))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.Parent.FullName;
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
                apikey = credential.IamApikey;
            }
            #endregion

            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = endpoint
            };
            service = new LanguageTranslatorService(tokenOptions, versionDate);
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Sucess()
        {
            var results = ListIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess()
        {
            var results = Identify(text);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess()
        {
            var translateRequest = new TranslateRequest()
            {
                Text = new List<string>()
                {
                    text
                },
                ModelId = baseModel
            };

            var results = Translate(translateRequest);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Translations.Count > 0);
        }

        [TestMethod]
        public void ListModels_Sucess()
        {
            var results = ListModels();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Models.Count > 0);
        }

        [TestMethod]
        public void GetModelDetails_Success()
        {
            var results = GetModel(baseModel);

            Assert.IsNotNull(results);
            Assert.IsFalse(string.IsNullOrEmpty(results.ModelId));
        }

        //[TestMethod]
        public void Model_Success()
        {
            TranslationModel createModelResult;

            using (FileStream fs = File.OpenRead(glossaryPath))
            {
                createModelResult = CreateModel(baseModel, customModelName, forcedGlossary: fs);

                if (createModelResult != null)
                {
                    customModelID = createModelResult.ModelId;
                }
                else
                {
                    Console.WriteLine("result is null.");
                }
            }

            var result = DeleteModel(customModelID);

            Assert.IsNotNull(createModelResult);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Status == "OK");
        }

        #region Generated
        #region Translate
        private TranslationResult Translate(TranslateRequest request, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Translate()");
            var result = service.Translate(request: request, customData: customData);

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
        private IdentifiedLanguages Identify(string text, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Identify()");
            var result = service.Identify(text: text, customData: customData);

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

        #region ListIdentifiableLanguages
        private IdentifiableLanguages ListIdentifiableLanguages(Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListIdentifiableLanguages()");
            var result = service.ListIdentifiableLanguages(customData: customData);

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
        private TranslationModel CreateModel(string baseModelId, string name = null, System.IO.FileStream forcedGlossary = null, System.IO.FileStream parallelCorpus = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to CreateModel()");
            var result = service.CreateModel(baseModelId: baseModelId, name: name, forcedGlossary: forcedGlossary, parallelCorpus: parallelCorpus, customData: customData);

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
        private DeleteModelResult DeleteModel(string modelId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteModel()");
            var result = service.DeleteModel(modelId: modelId, customData: customData);

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
        private TranslationModel GetModel(string modelId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to GetModel()");
            var result = service.GetModel(modelId: modelId, customData: customData);

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
        private TranslationModels ListModels(string source = null, string target = null, bool? defaultModels = null, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListModels()");
            var result = service.ListModels(source: source, target: target, defaultModels: defaultModels, customData: customData);

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
