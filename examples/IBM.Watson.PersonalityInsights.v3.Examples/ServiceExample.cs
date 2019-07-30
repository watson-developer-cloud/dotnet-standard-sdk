/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.PersonalityInsights.v3.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.PersonalityInsights.v3.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        string versionDate = "{versionDate}";

        string contentToProfile = "The IBM Watson™ Personality Insights service provides a Representational State Transfer (REST) Application Programming Interface (API) that enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts. The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can report consumption preferences based on the results of its analysis, and for JSON content that is timestamped, it can report temporal behavior.";

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.Profile();
            example.ProfileAsCsv();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Profile
        public void Profile()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            PersonalityInsightsService service = new PersonalityInsightsService("2017-10-13", config);
            service.SetEndpoint("{url}");

            Content content = null;
            content = JsonConvert.DeserializeObject<Content>(File.ReadAllText("profile.json"));

            var result = service.Profile(
                content: content,
                contentType: "application/json",
                rawScores: true,
                consumptionPreferences: true
                );

            Console.WriteLine(result.Response);
        }

        public void ProfileAsCsv()
        {
            IamConfig config = new IamConfig(
                apikey: "{apikey}"
                );

            PersonalityInsightsService service = new PersonalityInsightsService("2017-10-13", config);
            service.SetEndpoint("{url}");

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

            Console.WriteLine(text);
        }
        #endregion
    }
}
