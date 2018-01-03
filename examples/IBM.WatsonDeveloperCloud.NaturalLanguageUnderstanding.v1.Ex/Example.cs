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
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Util;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Example
{
    public class Example
    {
        public static void Main(string[] args)
        {
            string credentials = string.Empty;

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
            var _url = vcapServices["natural_language_understanding"]["url"];
            var _username = vcapServices["natural_language_understanding"]["username"];
            var _password = vcapServices["natural_language_understanding"]["password"];

            NaturalLanguageUnderstandingExample _naturalLanguageUnderstandingExample = new NaturalLanguageUnderstandingExample(_url.ToString(), _username.ToString(), _password.ToString());
            Console.ReadKey();
        }
    }
}
