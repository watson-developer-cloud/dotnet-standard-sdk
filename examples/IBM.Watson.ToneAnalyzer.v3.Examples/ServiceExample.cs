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
using IBM.Watson.ToneAnalyzer.v3.Model;
using System;
using System.Collections.Generic;

namespace IBM.Watson.ToneAnalyzer.v3.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        private string versionDate = "{versionDate}";

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.Tone();
            example.ToneChat();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Analyze Tone
        public void Tone()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            ToneAnalyzerService service = new ToneAnalyzerService(versionDate, config);
            service.SetEndpoint(url);

            ToneInput toneInput = new ToneInput()
            {
                Text = "Hello! Welcome to IBM Watson! How can I help you?"
            };

            var result = service.Tone(
                toneInput: toneInput,
                contentType: "text/html",
                sentences: true,
                contentLanguage: "en-US",
                acceptLanguage: "en-US"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Analyze Customer Engagment Tone
        public void ToneChat()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            ToneAnalyzerService service = new ToneAnalyzerService(versionDate, config);
            service.SetEndpoint(url);

            var utterances = new List<Utterance>()
            {
                new Utterance()
                {
                    Text = "Hello! Welcome to IBM Watson! How can I help you?",
                    User = "testChatUser"
                }
            };

            var result = service.ToneChat(
                utterances: utterances,
                contentLanguage: "en-US",
                acceptLanguage: "en-US"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
