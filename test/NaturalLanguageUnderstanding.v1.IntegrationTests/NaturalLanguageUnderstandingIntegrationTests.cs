/**
* (C) Copyright IBM Corp. 2017, 2020.
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using IBM.Cloud.SDK.Core.Util;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.IntegrationTests
{
    [TestClass]
    public class NaturalLanguageUnderstandingIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private NaturalLanguageUnderstandingService service;
        private static string credentials = string.Empty;
        private string versionDate = "2017-02-27";
        private string nluText = "Analyze various features of text content at scale. Provide text, raw HTML, or a public URL, and IBM Watson Natural Language Understanding will give you results for the features you request. The service cleans HTML content before analysis by default, so the results can ignore most advertisements and other unwanted content.";

        [TestInitialize]
        public void Setup()
        {
            service = new NaturalLanguageUnderstandingService(versionDate);
        }

        [TestMethod]
        public void Analyze_Success()
        {
            var text = nluText;
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

            service.WithHeader("X-Watson-Test", "1");
            var result = service.Analyze(
                features: features,
                text: text,
                clean: true,
                fallbackToRaw: true,
                returnAnalyzedText: true,
                language: "en"
                );

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result.Categories.Count > 0);
            Assert.IsTrue(result.Result.Keywords.Count > 0);
        }

        [TestMethod]
        public void ListModels_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.ListModels();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void AnalyzeWithCategories()
        {
            service.WithHeader("X-Watson-Test", "1");
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

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Categories);
            Assert.IsTrue(result.Result.Categories.Count == 3);
            Assert.IsTrue(result.Result.RetrievedUrl.Contains("www.ibm.com"));
        }

        [TestMethod]
        public void AnalyzeWithConcepts()
        {
            service.WithHeader("X-Watson-Test", "1");
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

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Concepts);
            Assert.IsTrue(result.Result.Concepts.Count == 3);
            Assert.IsTrue(result.Result.RetrievedUrl.Contains("www.ibm.com"));
        }

        [TestMethod]
        public void AnalyzeWithEmotion()
        {
            service.WithHeader("X-Watson-Test", "1");
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

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Emotion);
            Assert.IsNotNull(result.Result.Emotion.Targets);
            Assert.IsTrue(result.Result.Emotion.Targets.Count == 2);
            Assert.IsTrue(result.Result.Emotion.Targets[0].Text == "apples");
            Assert.IsTrue(result.Result.Emotion.Targets[1].Text == "oranges");
            Assert.IsNotNull(result.Result.Emotion.Document);
            Assert.IsNotNull(result.Result.Emotion.Document.Emotion);
        }

        [TestMethod]
        public void AnalyzeWithEntities()
        {
            service.WithHeader("X-Watson-Test", "1");
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

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Entities);
            Assert.IsTrue(result.Result.Entities.Count == 1);
            Assert.IsTrue(result.Result.RetrievedUrl.Contains("www.cnn.com"));
        }

        [TestMethod]
        public void AnalyzeWithKeywords()
        {
            service.WithHeader("X-Watson-Test", "1");
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

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Keywords);
            Assert.IsTrue(result.Result.Keywords.Count == 2);
            Assert.IsNotNull(result.Result.Keywords[0].Sentiment);
            Assert.IsNotNull(result.Result.Keywords[0].Emotion);
            Assert.IsNotNull(result.Result.Keywords[1].Sentiment);
            Assert.IsNotNull(result.Result.Keywords[1].Emotion);
            Assert.IsTrue(result.Result.RetrievedUrl.Contains("www.ibm.com"));
        }

        [TestMethod]
        public void AnalyzeWithMetadata()
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.Analyze(
                url: "www.ibm.com",
                features: new Features()
                {
                    Metadata = new FeaturesResultsMetadata()
                }
                );

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Metadata);
            Assert.IsNotNull(result.Result.Metadata.Title);
            Assert.IsNotNull(result.Result.Metadata.PublicationDate);
            Assert.IsTrue(result.Result.RetrievedUrl.Contains("www.ibm.com"));
        }

        [TestMethod]
        public void AnalyzeWithRelations()
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.Analyze(
                text: "Leonardo DiCaprio won Best Actor in a Leading Role for his performance.",
                features: new Features()
                {
                    Relations = new RelationsOptions()
                }
                );

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Relations);
            Assert.IsTrue(result.Result.Relations.Count == 1);
            Assert.IsNotNull(result.Result.Relations[0].Sentence);
        }

        [TestMethod]
        public void AnalyzeWithSemanticRoles()
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.Analyze(
                text: "IBM has one of the largest workforces in the world",
                features: new Features()
                {
                    SemanticRoles = new SemanticRolesOptions()
                }
                );

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.SemanticRoles);
            Assert.IsTrue(result.Result.SemanticRoles.Count > 0);
            Assert.IsTrue(result.Result.SemanticRoles[0].Subject.Text == "IBM");
            Assert.IsTrue(result.Result.SemanticRoles[0].Sentence == "IBM has one of the largest workforces in the world");
        }

        [TestMethod]
        public void AnalyzeWithSentiment()
        {
            service.WithHeader("X-Watson-Test", "1");
            string text =
                "In 2009, Elliot Turner launched AlchemyAPI to process the written word,"
                  + " with all of its quirks and nuances, and got immediate traction.";
            var result = service.Analyze(
                text: text,
                features: new Features()
                {
                    Sentiment = new SentimentOptions()
                    {
                        Targets = new List<string>() { "Elliot Turner" }
                    }
                }
                );

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Sentiment);
            Assert.IsNotNull(result.Result.Sentiment.Targets);
            Assert.IsTrue(result.Result.Sentiment.Targets.Count == 1);
            Assert.IsTrue(result.Result.Sentiment.Targets[0].Text == "Elliot Turner");
            Assert.IsNotNull(result.Result.Sentiment.Document);
        }

        [TestMethod]
        public void AnalyzeWithSyntax()
        {
            service.WithHeader("X-Watson-Test", "1");
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

            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Syntax);
            Assert.IsNotNull(result.Result.Syntax.Tokens);
            Assert.IsTrue(result.Result.Syntax.Tokens.Count > 0);
            Assert.IsNotNull(result.Result.Syntax.Tokens[0].Lemma);
            Assert.IsNotNull(result.Result.Syntax.Sentences);
            Assert.IsTrue(result.Result.Syntax.Sentences.Count > 0);
            Assert.IsTrue(result.Result.Syntax.Sentences[0].Text == "With great power comes great responsibility");
        }
    }
}
