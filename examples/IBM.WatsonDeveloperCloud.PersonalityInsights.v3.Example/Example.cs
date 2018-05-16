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
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Example
{
    public class Example
    {
        public static void Main(string[] args)
        {
            #region Get Credentials
            string credentials = string.Empty;
            string _endpoint = string.Empty;
            string _username = string.Empty;
            string _password = string.Empty;

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

                    VcapCredentials vcapCredentials = JsonConvert.DeserializeObject<VcapCredentials>(credentials);
                    var vcapServices = JObject.Parse(credentials);

                    Credential credential = vcapCredentials.GetCredentialByname("personality-insights-sdk")[0].Credentials;
                    _endpoint = credential.Url;
                    _username = credential.Username;
                    _password = credential.Password;
                }
                else
                {
                    Console.WriteLine("Credentials file does not exist. Please define credentials.");
                    _username = "";
                    _password = "";
                    _endpoint = "";
                }
            }
            #endregion

            PersonalityInsightsService _service = new PersonalityInsightsService(_username, _password, "2016-10-20");
            _service.Endpoint = _endpoint;
            string contentToProfile = "The IBM Watson™ Personality Insights service provides a Representational State Transfer (REST) Application Programming Interface (API) that enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts. The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can report consumption preferences based on the results of its analysis, and for JSON content that is timestamped, it can report temporal behavior.";

            //  Test Profile
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

            var result = _service.Profile(content, "text/plain", acceptLanguage:"application/json", rawScores: true, consumptionPreferences:true, csvHeaders:true);

            Console.WriteLine("Profile() succeeded:\n{0}", JsonConvert.SerializeObject(result, Formatting.Indented));

            Console.ReadKey();
        }
    }
}
