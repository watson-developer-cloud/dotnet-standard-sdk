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
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.NaturalLanguageClassifier.v1;
using IBM.Watson.NaturalLanguageClassifier.v1.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.NaturalLangaugeClassifier.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        string classifierId;
        private string classifierDataFilePath = @"NaturalLanguageClassifierTestData/weather-data.csv";
        private string metadataDataFilePath = @"NaturalLanguageClassifierTestData/metadata.json";

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.ListClassifiers();
            example.Classify();
            example.ClassifyCollection();
            example.CreateClassifier();
            example.GetClassifier();
            example.DeleteClassifier();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Classify Text
        public void Classify()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(authenticator);
            service.SetEndpoint("{url}");

            var result = service.Classify(
                    classifierId: "10D41B-nlc-1",
                    text: "How hot will it be today?"
                    );

            Console.WriteLine(result.Response);
        }

        public void ClassifyCollection()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(authenticator);
            service.SetEndpoint("{url}");

            var collection = new List<ClassifyInput>()
            {
                new ClassifyInput()
                {
                    Text = "How hot will it be today?"
                },
                new ClassifyInput()
                {
                    Text = "Is it hot outside?"
                }
            };

            var result = service.ClassifyCollection(
                    classifierId: "10D41B-nlc-1",
                    collection: collection
                    );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Manage Classifiers
        public void ListClassifiers()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(authenticator);
            service.SetEndpoint("{url}");

            var result = service.ListClassifiers();

            Console.WriteLine(result.Response);

            if(result.Result.Classifiers != null && result.Result.Classifiers.Count > 0)
            {
                classifierId = result.Result.Classifiers[0].ClassifierId;
            }
        }

        public void CreateClassifier()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(authenticator);
            service.SetEndpoint("{url}");

            DetailedResponse<Classifier> result = null;
            using (FileStream trainingDataFile = File.OpenRead("./train.csv"), metadataFile = File.OpenRead("./metadata.json"))
            {
                using (MemoryStream trainingData = new MemoryStream(), metadata = new MemoryStream())
                {
                    trainingDataFile.CopyTo(trainingData);
                    metadataFile.CopyTo(metadata);
                    result = service.CreateClassifier(
                        metadata: metadata,
                        trainingData: trainingData
                        );
                }
            }

            Console.WriteLine(result.Response);

            classifierId = result.Result.ClassifierId;
        }

        public void GetClassifier()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(authenticator);
            service.SetEndpoint("{url}");

            var result = service.GetClassifier(
                classifierId: "10D41B-nlc-1"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteClassifier()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(authenticator);
            service.SetEndpoint("{url}");

            var result = service.DeleteClassifier(
                    classifierId: "10D41B-nlc-1"
                    );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
