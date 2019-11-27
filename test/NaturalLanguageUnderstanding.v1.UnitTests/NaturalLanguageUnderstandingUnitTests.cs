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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Net.Http;
using System.Net;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.UnitTests
{
    [TestClass]
    public class NaturalLanguageUnderstandingUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService(httpClient: null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            NaturalLanguageUnderstandingService service =
                new NaturalLanguageUnderstandingService(new IBMHttpClient());

            Assert.IsNotNull(service);
        }
        #endregion

        #region Create Client
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                .Returns(client);

            return client;
        }
        #endregion

        #region Analyze
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Analyze_No_VersionDate()
        {

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("versionDate", new NoAuthAuthenticator());
            service.VersionDate = null;

            service.Analyze(new Features());
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Analyze_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "2017-02-27";

            service.Analyze(new Features());
        }

        [TestMethod]
        public void Analyze_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = new DetailedResponse<AnalysisResults>()
            {
                Result = new AnalysisResults()
                {
                    Language = "en",
                    AnalyzedText = "testText",
                    RetrievedUrl = "retrivedUrl",
                    Usage = new AnalysisResultsUsage()
                    {
                        Features = 1
                    },
                    Concepts = new List<ConceptsResult>()
                    {
                        new ConceptsResult()
                        {
                            Text = "text",
                            Relevance = 1.0f,
                            DbpediaResource = "dbpediaResouce"
                        }
                    },
                    Entities = new List<EntitiesResult>()
                    {
                        new EntitiesResult()
                        {
                            Type = "type",
                            Relevance = 1.0f,
                            Count = 1,
                            Text = "text",
                            Emotion = new EmotionScores() { Anger = 1.0f, Disgust = 1.0f, Fear = 1.0f, Joy = 1.0f, Sadness = 1.0f },
                            Sentiment = new FeatureSentimentResults() { Score = 1.0f },
                            Disambiguation = new DisambiguationResult()
                            {
                                Name = "name",
                                DbpediaResource = "dbpediaResource",
                                Subtype = new List<string>()
                                {
                                    "subtype"
                                }
                            }
                        }
                    },
                    Keywords = new List<KeywordsResult>()
                    {
                        new KeywordsResult()
                        {
                            Relevance = 1.0f,
                            Text = "text",
                            Emotion = new EmotionScores() { Anger = 1.0f, Disgust = 1.0f, Fear = 1.0f, Joy = 1.0f, Sadness = 1.0f },
                            Sentiment = new FeatureSentimentResults() { Score = 1.0f }
                        }
                    },
                    Categories = new List<CategoriesResult>()
                    {
                        new CategoriesResult()
                        {
                            Label = "label",
                            Score = 1.0f
                        }
                    },
                    Emotion = new EmotionResult()
                    {
                        Document = new DocumentEmotionResults()
                        {
                            Emotion = new EmotionScores() { Anger = 1.0f, Disgust = 1.0f, Fear = 1.0f, Joy = 1.0f, Sadness = 1.0f },
                        },
                        Targets = new List<TargetedEmotionResults>()
                        {
                            new TargetedEmotionResults()
                            {
                                Emotion = new EmotionScores() { Anger = 1.0f, Disgust = 1.0f, Fear = 1.0f, Joy = 1.0f, Sadness = 1.0f },
                                Text = "text"
                            }
                        }
                    },
                    Metadata = new AnalysisResultsMetadata()
                    {
                        Authors = new List<Author>()
                        {
                            new Author()
                            {
                                Name = "name"
                            }
                        },
                        PublicationDate = "publicationDate",
                        Title = "title"
                    },
                    Relations = new List<RelationsResult>()
                    {
                        new RelationsResult()
                        {
                            Score = 1.0f,
                            Sentence = "sentence",
                            Type = "type",
                            Arguments = new List<RelationArgument>()
                            {
                                new RelationArgument()
                                {
                                    Entities = new List<RelationEntity>()
                                    {
                                        new RelationEntity()
                                        {
                                            Text = "text",
                                            Type = "type"
                                        }
                                    },
                                    Text = "text"
                                }
                            }
                        }
                    },
                    SemanticRoles = new List<SemanticRolesResult>()
                    {
                        new SemanticRolesResult()
                        {
                            Sentence = "sentence",
                            Subject = new SemanticRolesResultSubject()
                            {
                                Text = "text",
                                Entities = new List<SemanticRolesEntity>()
                                {
                                    new SemanticRolesEntity()
                                    {
                                        Text = "text",
                                        Type = "type"
                                    }
                                },
                                Keywords = new List<SemanticRolesKeyword>()
                                {
                                    new SemanticRolesKeyword()
                                    {
                                        Text = "text"
                                    }
                                }
                            },
                            Action = new SemanticRolesResultAction()
                            {
                                Normalized = "normalized",
                                Text = "text",
                                Verb = new SemanticRolesVerb()
                                {
                                    Text = "text",
                                    Tense = "tense"
                                }
                            },
                            _Object = new SemanticRolesResultObject()
                            {
                                Text = "text",
                                Keywords = new List<SemanticRolesKeyword>()
                                {
                                    new SemanticRolesKeyword()
                                    {
                                        Text = "text"
                                    }
                                }
                            }
                        }
                    },
                    Sentiment = new SentimentResult()
                    {
                        Document = new DocumentSentimentResults()
                        {
                            Score = 1.0f
                        },
                        Targets = new List<TargetedSentimentResults>()
                    {
                        new TargetedSentimentResults()
                        {
                            Score = 1.0f,
                            Text = "text"
                        }
                    }
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithBodyContent(new StringContent("features"))
                .Returns(request);
            request.As<AnalysisResults>()
                .Returns(Task.FromResult(response));

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "versionDate";

            var result = service.Analyze(new Features(), "testText", language: "en");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Language == "en");
            Assert.IsTrue(result.Result.AnalyzedText == "testText");
        }
        #endregion

        #region Delete Model
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteModel_No_Model_Id()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("versionDate", new NoAuthAuthenticator());
            service.DeleteModel(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteModel_No_VersionDate()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("versionDate", new NoAuthAuthenticator());
            service.VersionDate = null;

            service.DeleteModel("modelId");
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteModel_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "2017-02-27";

            service.DeleteModel("modelID");
        }

        [TestMethod]
        public void DeleteModel_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = new DetailedResponse<DeleteModelResults>()
            {
                Result = new DeleteModelResults()
                {
                    Deleted = "true"
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<DeleteModelResults>()
                .Returns(Task.FromResult(response));

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "versionDate";

            var result = service.DeleteModel("modelId");

            Assert.IsNotNull(result.Result);
            client.Received().DeleteAsync(Arg.Any<string>());
            Assert.IsNotNull(result.Result.Deleted);
        }
        #endregion

        #region List Models
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListModels_No_VersionDate()
        {
            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService("versionDate", new NoAuthAuthenticator());
            service.VersionDate = null;

            service.ListModels();
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListModels_Catch_Exception()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "2017-02-27";

            service.ListModels();
        }

        [TestMethod]
        public void ListModels_Success()
        {
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            #region Response
            var response = new DetailedResponse<ListModelsResults>()
            {
                Result = new ListModelsResults()
                {
                    Models = new List<Model.Model>()
                    {
                        new Model.Model()
                        {
                            Status = "status",
                            ModelId = "modelId",
                            Language = "language",
                            Description = "description"
                        }
                    }
                }
            };
            #endregion

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.As<ListModelsResults>()
                .Returns(Task.FromResult(response));

            NaturalLanguageUnderstandingService service = new NaturalLanguageUnderstandingService(client);
            service.VersionDate = "versionDate";

            var result = service.ListModels();

            Assert.IsNotNull(result);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.Models[0].Status == "status");
            Assert.IsTrue(result.Result.Models[0].ModelId == "modelId");
            Assert.IsTrue(result.Result.Models[0].Language == "language");
            Assert.IsTrue(result.Result.Models[0].Description == "description");
        }
        #endregion
    }
}
