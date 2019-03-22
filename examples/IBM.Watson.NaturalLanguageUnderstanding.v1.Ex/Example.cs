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
using IBM.Watson.Util;
using Newtonsoft.Json;
using System.IO;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.Example
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

                    Credential credential = vcapCredentials.GetCredentialByname("natural-language-understanding-sdk")[0].Credentials;
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

            NaturalLanguageUnderstandingExample _naturalLanguageUnderstandingExample = new NaturalLanguageUnderstandingExample(_endpoint, _username, _password);
            Console.ReadKey();
        }
    }
}
