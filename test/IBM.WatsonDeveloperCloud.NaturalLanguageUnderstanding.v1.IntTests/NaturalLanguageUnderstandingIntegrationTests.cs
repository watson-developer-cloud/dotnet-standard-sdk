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
                _endpoint = vcapServices["natural_language_understanding"]["url"].Value<string>();
                _username = vcapServices["natural_language_understanding"]["username"].Value<string>();
                _password = vcapServices["natural_language_understanding"]["password"].Value<string>();
            }

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
        private AnalysisResults Analyze(Parameters parameters)
        {
            Console.WriteLine("\nAttempting to Analyze()");
            var result = _service.Analyze(parameters: parameters);

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
        private InlineResponse200 DeleteModel(string modelId)
        {
            Console.WriteLine("\nAttempting to DeleteModel()");
            var result = _service.DeleteModel(modelId: modelId);

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
        private ListModelsResults ListModels()
        {
            Console.WriteLine("\nAttempting to ListModels()");
            var result = _service.ListModels();

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
