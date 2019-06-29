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
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(config);
            service.SetEndpoint(url);

            var result = service.Classify(
                    classifierId: classifierId,
                    text: "Will it be hot today?"
                    );

            Console.WriteLine(result.Response);
        }

        public void ClassifyCollection()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(config);
            service.SetEndpoint(url);

            var collection = new List<ClassifyInput>()
            {
                new ClassifyInput()
                {
                    Text = "Will it be hot today?"
                },
                new ClassifyInput()
                {
                    Text = "Is it raining?"
                }
            };

            var result = service.ClassifyCollection(
                    classifierId: classifierId,
                    collection: collection
                    );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Manage Classifiers
        public void ListClassifiers()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(config);
            service.SetEndpoint(url);

            var result = service.ListClassifiers();

            Console.WriteLine(result.Response);

            if(result.Result.Classifiers != null && result.Result.Classifiers.Count > 0)
            {
                classifierId = result.Result.Classifiers[0].ClassifierId;
            }
        }

        public void CreateClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(config);
            service.SetEndpoint(url);

            DetailedResponse<Classifier> result = null;
            using (FileStream trainingDataFile = File.OpenRead(classifierDataFilePath), metadataFile = File.OpenRead(metadataDataFilePath))
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
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(config);
            service.SetEndpoint(url);

            var result = service.GetClassifier(
                classifierId: classifierId
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteClassifier()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageClassifierService service = new NaturalLanguageClassifierService(config);
            service.SetEndpoint(url);

            var result = service.DeleteClassifier(
                    classifierId: classifierId
                    );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
