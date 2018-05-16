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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using IBM.WatsonDeveloperCloud.Util;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.IntegrationTests
{
    [TestClass]
    public class NaturalLanguageUnderstandingIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private NaturalLanguageUnderstandingService _service;
        private static string credentials = string.Empty;
        private string _nluText = "Analyze various features of text content at scale. Provide text, raw HTML, or a public URL, and IBM Watson Natural Language Understanding will give you results for the features you request. The service cleans HTML content before analysis by default, so the results can ignore most advertisements and other unwanted content.";

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

                Credential credential = vcapCredentials.GetCredentialByname("natural-language-understanding-sdk")[0].Credentials;
                _endpoint = credential.Url;
                _username = credential.Username;
                _password = credential.Password;
            }
            #endregion

            _service = new NaturalLanguageUnderstandingService(_username, _password, "2017-02-27");
            _service.Endpoint = _endpoint;
        }

        [TestMethod]
        public void Analyze_Success()
        {
            Parameters parameters = new Parameters()
            {
                Text = _nluText,
                Features = new Features()
                {
                    Keywords = new KeywordsOptions()
                    {
                        Limit = 8,
                        Sentiment = true,
                        Emotion = true
                    }
                }
            };

            var result = _service.Analyze(parameters);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ListModels_Success()
        {
            var result = _service.ListModels();

            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void DeleteModel_Success()
        //{

        //}

        #region Generated
        #region Analyze
        private AnalysisResults Analyze(Parameters parameters, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to Analyze()");
            var result = _service.Analyze(parameters: parameters, customData: customData);

            if (result != null)
            {
                Console.WriteLine("Analyze() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Analyze()");
            }

            return result;
        }
        #endregion

        #region DeleteModel
        private InlineResponse200 DeleteModel(string modelId, Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to DeleteModel()");
            var result = _service.DeleteModel(modelId: modelId, customData: customData);

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

        #region ListModels
        private ListModelsResults ListModels(Dictionary<string, object> customData = null)
        {
            Console.WriteLine("\nAttempting to ListModels()");
            var result = _service.ListModels(customData: customData);

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
