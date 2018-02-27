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

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.IntegrationTests
{
    [TestClass]
    public class NaturalLanguageUnderstandingIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private NaturalLanguageUnderstandingService naturalLanguageUnderstanding;
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

            naturalLanguageUnderstanding = new NaturalLanguageUnderstandingService(_username, _password, NaturalLanguageUnderstandingService.NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27);
            naturalLanguageUnderstanding.Endpoint = _endpoint;
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

            var result = naturalLanguageUnderstanding.Analyze(parameters);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ListModels_Success()
        {
            var result = naturalLanguageUnderstanding.ListModels();

            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void DeleteModel_Success()
        //{

        //}
    }
}
