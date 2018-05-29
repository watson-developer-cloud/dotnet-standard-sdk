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
using System.IO;

namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Example
{
    public class Example
    {
        public static void Main(string[] args)
        {
            string credentials = string.Empty;

            #region Get Credentials
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

                    Credential credential = vcapCredentials.GetCredentialByname("tone-analyzer-sdk")[0].Credentials;
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

            var versionDate = "2016-05-19";
            ToneAnalyzerService _toneAnalyzer = new ToneAnalyzerService(_username, _password, versionDate);
            _toneAnalyzer.SetEndpoint(_endpoint);

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
