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

using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environmentVariable = Environment.GetEnvironmentVariable("VCAP_SERVICES");
            var fileContent = File.ReadAllText(environmentVariable);
            var vcapServices = JObject.Parse(fileContent);
            var _username = vcapServices["personality_insights"][0]["credentials"]["username"];
            var _password = vcapServices["personality_insights"][0]["credentials"]["password"];

            PersonalityInsightsService _personalityInsights = new PersonalityInsightsService(_username.ToString(), _password.ToString(), "2016-10-20");
            string content = "The IBM Watson™ Personality Insights service provides a Representational State Transfer (REST) Application Programming Interface (API) that enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts. The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can report consumption preferences based on the results of its analysis, and for JSON content that is timestamped, it can report temporal behavior.";

            //  Test Profile
            ContentListContainer contentListContainer = new ContentListContainer()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = content
                    }
                }
            };

            var result = _personalityInsights.Profile("text/plain", "application/json", contentListContainer, rawScores: true, consumptionPreferences:true, csvHeaders:true);

            Console.WriteLine(string.Format("Profile result: {0}", result));

            Console.ReadKey();
        }
    }
}
