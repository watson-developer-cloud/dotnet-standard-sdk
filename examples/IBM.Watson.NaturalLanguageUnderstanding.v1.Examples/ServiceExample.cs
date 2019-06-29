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
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using System;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        string versionDate = "{versionDate}";
        private string text = "Analyze various features of text content at scale. Provide text, raw HTML, or a public URL, and IBM Watson Natural Language Understanding will give you results for the features you request. The service cleans HTML content before analysis by default, so the results can ignore most advertisements and other unwanted content.";
        private string modelId;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.Analyze();
            example.ListModels();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Analyze
        public void Analyze()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(versionDate, config);
            service.SetEndpoint(url);

            var features = new Features()
            {
                Keywords = new KeywordsOptions()
                {
                    Limit = 8,
                    Sentiment = true,
                    Emotion = true
                },
                Categories = new CategoriesOptions()
                {
                    Limit = 10
                }
            };

            var result = service.Analyze(
                features: features,
                text: text,
                clean: true,
                fallbackToRaw: true,
                returnAnalyzedText: true,
                language: "en"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Mangage Models
        public void ListModels()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListModels();

            Console.WriteLine(result.Response);
        }

        public void DeleteModel()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteModel(
                modelId: modelId
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
