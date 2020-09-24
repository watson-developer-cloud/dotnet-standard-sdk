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
using System.IO;
using System.Text;

namespace IBM.Watson.ToneAnalyzer.v3.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
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
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            ToneAnalyzerService service = new ToneAnalyzerService("2017-09-21", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            byte[] byteArray = Encoding.ASCII.GetBytes("Team, I know that times are tough! Product sales have been disappointing for the past three quarters. We have a competitive product, but we need to do a better job of selling it!");
            MemoryStream toneInput = new MemoryStream(byteArray);

            var result = service.Tone(
                toneInput: toneInput
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Analyze Customer Engagment Tone
        public void ToneChat()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            ToneAnalyzerService service = new ToneAnalyzerService("2017-09-21", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var utterances = new List<Utterance>()
            {
                new Utterance()
                {
                    Text = "Hello, I'm having a problem with your product.",
                    User = "customer"
                },
                new Utterance()
                {
                    Text = "OK, let me know what's going on, please.",
                    User = "agent"
                },
                new Utterance()
                {
                    Text = "Well, nothing is working :(",
                    User = "customer"
                },
                new Utterance()
                {
                    Text = "Sorry to hear that.",
                    User = "agent"
                }
            };

            var result = service.ToneChat(
                utterances: utterances
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
