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

using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.LanguageTranslator.v3.Model;
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
            service.WithHeader("X-Watson-Test", "1");
            var results = service.ListIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.Identify(
                text: text
                );

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess()
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
        public void ListModels_Sucess()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.ListModels();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Models.Count > 0);
        }

        [TestMethod]
        public void GetModelDetails_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.GetModel(
                modelId: baseModel
                );

            Assert.IsNotNull(results);
            Assert.IsFalse(string.IsNullOrEmpty(results.Result.ModelId));
        }

        [TestMethod]
        public void Model_Success()
        {
            DetailedResponse<TranslationModel> createModelResult;

            using (FileStream fs = File.OpenRead(glossaryPath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    createModelResult = service.CreateModel(
                        baseModelId: baseModel,
                        forcedGlossary: ms,
                        name: customModelName
                        );

                    if (createModelResult != null)
                    {
                        customModelID = createModelResult.Result.ModelId;
                    }
                    else
                    {
                        Console.WriteLine("result is null.");
                    }
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var result = service.DeleteModel(
                modelId: customModelID
                );

            Assert.IsNotNull(createModelResult);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result.Status == "OK");
        }
    }
}
