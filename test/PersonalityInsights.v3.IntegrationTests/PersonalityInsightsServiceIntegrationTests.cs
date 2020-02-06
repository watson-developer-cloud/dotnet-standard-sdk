/**
* (C) Copyright IBM Corp. 2017, 2020.
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
using IBM.Watson.PersonalityInsights.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json.Linq;
using IBM.Cloud.SDK.Core.Util;
using Newtonsoft.Json;

namespace IBM.Watson.PersonalityInsights.v3.IntegrationTests
{
    [TestClass]
    public class PersonalityInsightsServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private PersonalityInsightsService service;
        private static string credentials = string.Empty;
        private string versionDate = "2016-10-20";

        [TestInitialize]
        public void Setup()
        {
            service = new PersonalityInsightsService(versionDate);
        }

        [TestMethod]
        public void Profile_Success()
        {
            string contentToProfile = "The IBM Watson™ Personality Insights service provides a Representational State Transfer (REST) Application Programming Interface (API) that enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts. The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can report consumption preferences based on the results of its analysis, and for JSON content that is timestamped, it can report temporal behavior.";

            Content content = new Content()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnumValue.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnumValue.EN,
                        Content = contentToProfile
                    }
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var result = service.Profile(
                content: content,
                contentType: "text/plain",
                contentLanguage: "en",
                acceptLanguage: "en",
                rawScores: true,
                consumptionPreferences: true,
                csvHeaders: true
                );

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Personality);
        }

        [TestMethod]
        public void ProfileAsCsv_Success()
        {
            string contentToProfile = "The IBM Watson™ Personality Insights service provides a Representational State Transfer (REST) Application Programming Interface (API) that enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts. The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can report consumption preferences based on the results of its analysis, and for JSON content that is timestamped, it can report temporal behavior.";

            Content content = new Content()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnumValue.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnumValue.EN,
                        Content = contentToProfile
                    }
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var result = service.ProfileAsCsv(
                content: content,
                contentLanguage: "en",
                acceptLanguage: "en",
                rawScores: true,
                csvHeaders: true,
                consumptionPreferences: true,
                contentType: "text/plain"
                );

            StreamReader reader = new StreamReader(result.Result);
            var text = reader.ReadToEnd();

            Assert.IsFalse(text.StartsWith("{"));
        }
    }
}
