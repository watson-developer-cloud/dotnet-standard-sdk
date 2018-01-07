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

using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;
using IBM.WatsonDeveloperCloud.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Example
{
    public class Program
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
            var _url = vcapServices["tone_analyzer"]["url"].Value<string>();
            var _username = vcapServices["tone_analyzer"]["username"].Value<string>();
            var _password = vcapServices["tone_analyzer"]["password"].Value<string>();
            var versionDate = "2016-05-19";

            ToneAnalyzerService _toneAnalyzer = new ToneAnalyzerService(_username, _password, versionDate);
            _toneAnalyzer.Endpoint = _url;

            //  Test PostTone
            ToneInput toneInput = new ToneInput()
            {
                Text = "How are you doing? My name is Taj!"
            };

            var postToneResult = _toneAnalyzer.Tone(toneInput, "application/json", null);
            Console.WriteLine(string.Format("post tone result: {0}", JsonConvert.SerializeObject(postToneResult, Formatting.Indented)));

            //  Test ToneChat
            ToneChatInput toneChatInput = new ToneChatInput()
            {
                Utterances = new List<Utterance>()
                {
                    new Utterance()
                    {
                        Text = "Hello how are you?"
                    }
                }
            };

            var toneChatResult = _toneAnalyzer.ToneChat(toneChatInput);
            Console.WriteLine(string.Format("tone chat result: {0}", JsonConvert.SerializeObject(toneChatResult, Formatting.Indented)));


            Console.ReadKey();
            
        }
    }
}
