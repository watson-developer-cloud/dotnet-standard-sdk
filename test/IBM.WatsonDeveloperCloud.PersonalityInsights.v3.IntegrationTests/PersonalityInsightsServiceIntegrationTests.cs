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

using System;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.IntegrationTests
{
    [TestClass]
    public class PersonalityInsightsServiceIntegrationTests
    {
        private static string _username;
        private static string _password;
        private static string _endpoint;
        private PersonalityInsightsService _service;
        private static string credentials = string.Empty;

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
                _endpoint = vcapServices["personality_insights"]["url"].Value<string>();
                _username = vcapServices["personality_insights"]["username"].Value<string>();
                _password = vcapServices["personality_insights"]["password"].Value<string>();
            }
        }

        [TestMethod]
        public void Profile_Success()
        {
            _service = new PersonalityInsightsService(_username.ToString(), _password.ToString(), "2016-10-20");
            string contentToProfile = "The IBM Watson™ Personality Insights service provides a Representational State Transfer (REST) Application Programming Interface (API) that enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts. The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can report consumption preferences based on the results of its analysis, and for JSON content that is timestamped, it can report temporal behavior.";

            Content content = new Content()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = contentToProfile
                    }
                }
            };

            var result = _service.Profile(content, "text/plain", "application/json", rawScores: true, consumptionPreferences: true, csvHeaders: true);

            Assert.IsNotNull(result);
        }

        #region Generated
        #region Profile
        private Profile Profile(Content content, string contentType, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null)
        {
            Console.WriteLine("\nAttempting to Profile()");
            var result = _service.Profile(content: content, contentType: contentType, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage, rawScores: rawScores, csvHeaders: csvHeaders, consumptionPreferences: consumptionPreferences);

            if (result != null)
            {
                Console.WriteLine("Profile() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to Profile()");
            }

            return result;
        }
        #endregion

        #region ProfileCsv
        private Profile ProfileCsv(Content content, string contentType, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null)
        {
            Console.WriteLine("\nAttempting to ProfileCsv()");
            var result = _service.ProfileCsv(content: content, contentType: contentType, contentLanguage: contentLanguage, acceptLanguage: acceptLanguage, rawScores: rawScores, csvHeaders: csvHeaders, consumptionPreferences: consumptionPreferences);

            if (result != null)
            {
                Console.WriteLine("ProfileCsv() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Failed to ProfileCsv()");
            }

            return result;
        }
        #endregion
        #endregion
    }
}
