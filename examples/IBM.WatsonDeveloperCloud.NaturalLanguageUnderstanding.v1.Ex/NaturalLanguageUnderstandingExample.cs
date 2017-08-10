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
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;

namespace IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Example
{
    public class NaturalLanguageUnderstandingExample
    {
        private NaturalLanguageUnderstandingService _naturalLanguageUnderstandingService;

        private string _nluText = "Analyze various features of text content at scale. Provide text, raw HTML, or a public URL, and IBM Watson Natural Language Understanding will give you results for the features you request. The service cleans HTML content before analysis by default, so the results can ignore most advertisements and other unwanted content.";
        #region Constructor
        public NaturalLanguageUnderstandingExample(string username, string password)
        {
            _naturalLanguageUnderstandingService = new NaturalLanguageUnderstandingService(username, password, NaturalLanguageUnderstandingService.NATURAL_LANGUAGE_UNDERSTANDING_VERSION_DATE_2017_02_27);
            //_naturalLanguageUnderstandingService.Endpoint = "http://localhost:1234";

            Analyze();
            ListModels();
            DeleteModel();
            Console.WriteLine("\n~ Natural Language Understanding examples complete.");
        }
        #endregion

        #region Analyze
        public void Analyze()
        {
            Parameters parameters = new Parameters()
            {
                Text = _nluText,
                Features = new Features()
                {
                    Keywords = new KeywordsOptions()
                    {
                        Limit = 8,
                        Sentiment = true,
                        Emotion = true
                    }
                }
            };

            Console.WriteLine(string.Format("\nCalling Analyze()..."));
            var result = _naturalLanguageUnderstandingService.Analyze(parameters);

            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.Language))
                    Console.WriteLine(string.Format("Language: {0}", result.Language));
                if (!string.IsNullOrEmpty(result.AnalyzedText))
                    Console.WriteLine(string.Format("AnalyzedText: {0}", result.AnalyzedText));
                if (!string.IsNullOrEmpty(result.RetrievedUrl))
                    Console.WriteLine(string.Format("RetrievedUrl: {0}", result.RetrievedUrl));

                if (result.Usage != null)
                {
                    if (result.Usage.Features != null)
                        Console.WriteLine(string.Format("Usage features: {0}", result.Usage.Features));
                }

                if (result.Concepts != null && result.Concepts.Count > 0)
                {
                    foreach (ConceptsResult conceptResult in result.Concepts)
                    {
                        Console.WriteLine(string.Format("ConceptResult Text: {0}, Relevance {1}, DbpediaResource {2}", conceptResult.Text, conceptResult.Relevance, conceptResult.DbpediaResource));
                    }
                }

                if (result.Entities != null && result.Entities.Count > 0)
                {
                    foreach(EntitiesResult entityResult in result.Entities)
                    {
                        Console.WriteLine(string.Format("entityResult type: {0} | relevance: {1} | count: {2} | text: {3}", entityResult.Type, entityResult.Relevance, entityResult.Count, entityResult.Text));

                        if (entityResult.Emotion != null)
                        {
                            EmotionScores emotionScores = entityResult.Emotion;
                            Console.WriteLine(string.Format("anger: {0} | disgust: {1} | fear: {2} | joy: {3} | sadness: {4}", emotionScores.Anger, emotionScores.Disgust, emotionScores.Fear, emotionScores.Joy, emotionScores.Sadness));
                        }

                        if(entityResult.Sentiment != null)
                        {
                            if (entityResult.Sentiment.Score != null)
                                Console.WriteLine("Sentiment score: " + entityResult.Sentiment.Score);
                        }

                        if(entityResult.Disambiguation != null)
                        {
                            DisambiguationResult disambiguationResult = entityResult.Disambiguation;
                            Console.Write(string.Format("Disambiguation result name: {0} | dbpediaResource: {1}", disambiguationResult.Name, disambiguationResult.DbpediaResource));

                            foreach(string type in disambiguationResult.Subtype)
                            {
                                Console.WriteLine("subtype: " + type);
                            }
                        }
                    }
                }

                if (result.Keywords != null && result.Keywords.Count > 0)
                {
                    foreach(KeywordsResult keywordResult in result.Keywords)
                    {
                        Console.WriteLine("keywordResult relevance: {0}, text: {1}", keywordResult.Relevance, keywordResult.Text);

                        if (keywordResult.Emotion != null)
                        {
                            EmotionScores emotionScores = keywordResult.Emotion;
                            Console.WriteLine(string.Format("anger: {0} | disgust: {1} | fear: {2} | joy: {3} | sadness: {4}", emotionScores.Anger, emotionScores.Disgust, emotionScores.Fear, emotionScores.Joy, emotionScores.Sadness));
                        }

                        if(keywordResult.Sentiment != null)
                        {
                            Console.WriteLine("sentiment score: " + keywordResult.Sentiment.Score);
                        }
                    }
                }

                if (result.Categories != null && result.Categories.Count > 0)
                {
                    foreach(CategoriesResult categoryResult in result.Categories)
                    {
                        Console.WriteLine(string.Format("categoryResult label: {0} | score: {1}", categoryResult.Label, categoryResult.Score));
                    }
                }

                if (result.Emotion != null)
                {
                    if(result.Emotion.Document != null)
                    {
                        if(result.Emotion.Document.Emotion != null)
                        {
                            EmotionScores emotionScores = result.Emotion.Document.Emotion;
                            Console.WriteLine(string.Format("anger: {0} | disgust: {1} | fear: {2} | joy: {3} | sadness: {4}", emotionScores.Anger, emotionScores.Disgust, emotionScores.Fear, emotionScores.Joy, emotionScores.Sadness));
                        }
                    }

                    if (result.Emotion.Targets != null && result.Emotion.Targets.Count > 0)
                    {
                        foreach (TargetedEmotionResults targetedEmotionResult in result.Emotion.Targets)
                        {
                            Console.WriteLine(string.Format("emotionResult text: {0}", targetedEmotionResult.Text));
                            if(targetedEmotionResult.Emotion != null)
                            {
                                EmotionScores emotionScores = targetedEmotionResult.Emotion;
                                Console.WriteLine(string.Format("anger: {0} | disgust: {1} | fear: {2} | joy: {3} | sadness: {4}", emotionScores.Anger, emotionScores.Disgust, emotionScores.Fear, emotionScores.Joy, emotionScores.Sadness));
                            }
                        }

                    }
                }

                if (result.Metadata != null)
                {
                    MetadataResult metadata = result.Metadata;
                    if(metadata.Authors != null && metadata.Authors.Count > 0)
                    {
                        foreach(Author author in metadata.Authors)
                        {
                            Console.WriteLine("Author: " + author.Name);
                        }
                    }
                }

                if (result.Relations != null && result.Relations.Count > 0)
                {
                    foreach(RelationsResult relationResult in result.Relations)
                    {
                        Console.WriteLine(string.Format("relationResult score: {0} | sentence: {1} | type: {2}", relationResult.Score, relationResult.Sentence, relationResult.Type));

                        if(relationResult.Arguments != null && relationResult.Arguments.Count > 0)
                        {
                            foreach(RelationArgument arg in relationResult.Arguments)
                            {
                                Console.WriteLine("text: " + arg.Text);

                                if(arg.Entities != null && arg.Entities.Count > 0)
                                {
                                    foreach(RelationEntity entity in arg.Entities)
                                    {
                                        Console.WriteLine(string.Format("relationEntity text: {0} | type: {1}", entity.Text, entity.Type));
                                    }
                                }
                            }
                        }
                    }
                }

                if (result.SemanticRoles != null && result.SemanticRoles.Count > 0)
                {
                    foreach(SemanticRolesResult semanticRoleResult in result.SemanticRoles)
                    {
                        Console.WriteLine(string.Format("semanticRoleResult sentance: {0}", semanticRoleResult.Sentence));

                        if(semanticRoleResult.Subject != null)
                        {
                            Console.WriteLine(string.Format("semanticRoleResult subject text: {0}", semanticRoleResult.Subject.Text));

                            if(semanticRoleResult.Subject.Entities != null && semanticRoleResult.Subject.Entities.Count > 0)
                            {
                                foreach(SemanticRolesEntity entity in semanticRoleResult.Subject.Entities)
                                {
                                    Console.WriteLine(string.Format("entity type: {0} | text: {1}", entity.Type, entity.Text));
                                }
                            }

                            if(semanticRoleResult.Subject.Keywords != null && semanticRoleResult.Subject.Keywords.Count > 0)
                            {
                                foreach (SemanticRolesKeyword keyword in semanticRoleResult.Subject.Keywords)
                                {
                                    Console.WriteLine(string.Format("keyword text: {0}", keyword.Text));
                                }
                            }
                        }

                        if (semanticRoleResult.Action != null)
                        {
                            Console.WriteLine(string.Format("action text: {0} | normalized: {1}", semanticRoleResult.Action.Text, semanticRoleResult.Action.Normalized));

                            if(semanticRoleResult.Action.Verb != null)
                            {
                                Console.WriteLine(string.Format("Verb text: {0} | tense: {1}", semanticRoleResult.Action.Verb.Text, semanticRoleResult.Action.Verb.Tense));
                            }
                        }

                        if (semanticRoleResult._Object != null)
                        {
                            Console.WriteLine(string.Format("object text: {0}", semanticRoleResult._Object.Text));

                            if(semanticRoleResult._Object.Keywords != null && semanticRoleResult._Object.Keywords.Count > 0)
                            {
                                foreach(SemanticRolesKeyword keyword in semanticRoleResult._Object.Keywords)
                                {
                                    Console.WriteLine("keyword: " + keyword.Text);
                                }
                            }
                        }
                    }
                }

                if(result.Sentiment != null)
                {
                    if(result.Sentiment.Document != null)
                    {
                        Console.WriteLine("Sentiment document score: " + result.Sentiment.Document.Score);

                        if(result.Sentiment.Targets != null && result.Sentiment.Targets.Count > 0)
                        {
                            foreach(TargetedSentimentResults targetedSentimentResult in result.Sentiment.Targets)
                            {
                                Console.WriteLine(string.Format("targetedSentimentResult text: {0} | score: {1}", targetedSentimentResult.Text, targetedSentimentResult.Score));
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region List Models
        private void ListModels()
        {
            Console.WriteLine(string.Format("\nCalling ListModels()..."));
            var result = _naturalLanguageUnderstandingService.ListModels();

            if(result != null)
            {
                if(result.Models != null && result.Models.Count > 0)
                {
                    foreach(ModelModel model in result.Models)
                    {
                        Console.WriteLine(string.Format("Model id: {0} | status: {1} | language: {3} | description: {4}", model.ModelId, model.Status, model.Language, model.Description));
                    }
                }
                else
                {
                    Console.WriteLine("There are no models.");
                }
            }
            else
            {
                Console.WriteLine("Result is null.");
            }
        }
        #endregion

        #region Delete Model
        private void DeleteModel()
        {

        }
        #endregion
    }
}
