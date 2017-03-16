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

using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace IBM.WatsonDeveloperCloud.VisualRecognition.Example
{
    public class Example
    {
        static void Main(string[] args)
        {
            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
            File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            var apikey =
            vcapServices["visual_recognition"][0]["credentials"]["apikey"];

            VisualRecognitionServiceExample _visualRecognitionExample = new VisualRecognitionServiceExample(apikey.ToString());
            Console.ReadKey();
        }
    }
}
