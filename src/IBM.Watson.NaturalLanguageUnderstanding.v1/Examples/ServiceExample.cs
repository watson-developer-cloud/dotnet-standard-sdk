/**
* (C) Copyright IBM Corp. 2019, 2022.
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
using System.Collections.Generic;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
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
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var features = new Features()
            {
                Keywords = new KeywordsOptions()
                {
                    Limit = 2,
                    Sentiment = true,
                    Emotion = true
                },
                Entities = new EntitiesOptions()
                {
                    Sentiment = true,
                    Limit = 2
                }
            };

            var result = service.Analyze(
                features: features,
                text: "IBM is an American multinational technology company headquartered in Armonk, New York, United States, with operations in over 170 countries."
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Mangage Models
        public void ListModels()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.ListModels();

            Console.WriteLine(result.Response);
        }

        public void DeleteModel()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}"
                );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.DeleteModel(
                modelId: "model_id"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Features
        public void AnalyzeWithCategories()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                url: "www.ibm.com",
                features: new Features()
                {
                    Categories = new CategoriesOptions()
                    {
                        Limit = 3
                    }
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithConcepts()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                url: "www.ibm.com",
                features: new Features()
                {
                    Concepts = new ConceptsOptions()
                    {
                        Limit = 3
                    }
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithEmotion()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                html: "<html><head><title>Fruits</title></head><body><h1>Apples and Oranges</h1><p>I love apples! I don't like oranges.</p></body></html>",
                features: new Features()
                {
                    Emotion = new EmotionOptions()
                    {
                        Targets = new List<string> { "apples", "oranges" }
                    }
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithEntities()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                url: "www.cnn.com",
                features: new Features()
                {
                    Entities = new EntitiesOptions()
                    {
                        Sentiment = true,
                        Limit = 1
                    }
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithKeywords()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                url: "www.ibm.com",
                features: new Features()
                {
                    Keywords = new KeywordsOptions()
                    {
                        Sentiment = true,
                        Emotion = true,
                        Limit = 2
                    }
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithMetadata()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                url: "www.ibm.com",
                features: new Features()
                {
                    Metadata = new Dictionary<string, object>()
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithRelations()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                text: "Leonardo DiCaprio won Best Actor in a Leading Role for his performance.",
                features: new Features()
                {
                    Relations = new RelationsOptions()
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithSemanticRoles()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                text: "IBM has one of the largest workforces in the world",
                features: new Features()
                {
                    SemanticRoles = new SemanticRolesOptions()
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithSentiment()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                url: "www.wsj.com/news/markets",
                features: new Features()
                {
                    Sentiment = new SentimentOptions()
                    {
                        Targets = new List<string>() { "stocks" }
                    }
                }
                );

            Console.WriteLine(result.Response);
        }

        public void AnalyzeWithSyntax()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                   apikey: "{apikey}"
                   );

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("2019-07-12", authenticator);
            service.SetServiceUrl("{serviceUrl}");

            var result = service.Analyze(
                text: "With great power comes great responsibility",
                features: new Features()
                {
                    Syntax = new SyntaxOptions()
                    {
                        Sentences = true,
                        Tokens = new SyntaxOptionsTokens()
                        {
                            Lemma = true,
                            PartOfSpeech = true
                        }
                    }
                }
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
