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
using IBM.WatsonDeveloperCloud.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;
using System.Collections.Generic;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.PersonalityInsights.v3.UnitTests
{
    [TestClass]
    public class PersonalityInsightsServiceUnitTests
    {
        string content = "The IBM Watson™ Personality Insights service provides a Representational State Transfer (REST) Application Programming Interface (API) that enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts. The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can report consumption preferences based on the results of its analysis, and for JSON content that is timestamped, it can report temporal behavior.";

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService(null);
        }

        [TestMethod]
        public void Constructor_HttpClient()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService(null, "password", "2016-10-20");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Password_Null()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", null, "2016-10-20");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Version_Null()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", "password", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", "password", "versionDate");

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService();

            Assert.IsNotNull(service);
        }

        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                    .Returns(client);

            return client;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Profile_No_Content_Type()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", "password", "versionDate");


            //  Test Profile
            ContentListContainer contentListContainer = new ContentListContainer()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = content
                    }
                }
            };

            service.Profile(null, Arg.Any<string>(), contentListContainer);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Profile_No_Accept()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", "password", "versionDate");

            //  Test Profile
            ContentListContainer contentListContainer = new ContentListContainer()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = content
                    }
                }
            };

            service.Profile("test", null, contentListContainer);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Profile_No_Content()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", "password", "versionDate");
            
            service.Profile("Test", "me", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Profile_No_Version()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", "password", "versionDate");
            service.VersionDate = null;

            //  Test Profile
            ContentListContainer contentListContainer = new ContentListContainer()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = content
                    }
                }
            };

            service.Profile("contentType", "accept", contentListContainer);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Profle_Catch_Exception()
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

            //  Test Profile
            ContentListContainer contentListContainer = new ContentListContainer()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = content
                    }
                }
            };

            PersonalityInsightsService service = new PersonalityInsightsService(client);
            service.VersionDate = "versionDate";

            service.Profile("contentType", "application/json", contentListContainer);
        }

        [TestMethod]
        public void Profile_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            #region Response
            Profile response =
                new Profile()
                {
                    ProcessedLanguage = "en",
                    WordCount = 10,
                    WordCountMessage = "wordCountMessage",
                    Personality = new List<TraitTreeNode>()
                    {
                        new TraitTreeNode()
                        {
                            TraitId = "traitID",
                            Name = "name",
                            Category = "category",
                            Percentile = 50.0,
                            RawScore = 25.0,
                            Children = new List<TraitTreeNode>()
                            {
                                new TraitTreeNode()
                                {
                                TraitId = "traitID",
                                Name = "name",
                                Category = "category",
                                Percentile = 50.0,
                                RawScore = 25.0
                                }
                            }
                        }
                    },
                    Values = new List<TraitTreeNode>()
                    {
                        new TraitTreeNode()
                        {
                            TraitId = "traitID",
                            Name = "name",
                            Category = "category",
                            Percentile = 50.0,
                            RawScore = 25.0,
                            Children = new List<TraitTreeNode>()
                            {
                                new TraitTreeNode()
                                {
                                TraitId = "traitID",
                                Name = "name",
                                Category = "category",
                                Percentile = 50.0,
                                RawScore = 25.0
                                }
                            }
                        }
                    },
                    Needs = new List<TraitTreeNode>()
                    {
                        new TraitTreeNode()
                        {
                            TraitId = "traitID",
                            Name = "name",
                            Category = "category",
                            Percentile = 50.0,
                            RawScore = 25.0,
                            Children = new List<TraitTreeNode>()
                            {
                                new TraitTreeNode()
                                {
                                TraitId = "traitID",
                                Name = "name",
                                Category = "category",
                                Percentile = 50.0,
                                RawScore = 25.0
                                }
                            }
                        }
                    },
                    Behavior = new List<BehaviorNode>()
                    {
                        new BehaviorNode()
                        {
                            TraitId = "traitID",
                            Name = "name",
                            Category = "category",
                            Percentage = 50.0
                        }
                    },
                    ConsumptionPreferences = new List<ConsumptionPreferencesCategoryNode>()
                    {
                        new ConsumptionPreferencesCategoryNode()
                        {
                            ConsumptionPreferenceCategoryId = "consumptionPreferenceCategoryId",
                            Name = "name",
                            ConsumptionPreferences = new List<ConsumptionPreferencesNode>()
                            {
                                new ConsumptionPreferencesNode()
                                {
                                    ConsumptionPreferenceId = "consumptionPreferenceId",
                                    Name = "name",
                                    Score = 50.0
                                }
                            }
                        }
                    },
                    Warnings = new List<Warning>()
                    {
                        new Warning()
                        {
                            WarningId = "warningId",
                            Message = "message"
                        }
                    }
                };
            #endregion

            //  Test Profile
            ContentListContainer contentListContainer = new ContentListContainer()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = content
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                .Returns(request);
            request.WithBody(Arg.Any<ContentListContainer>())
                .Returns(request);

            request.As<Profile>()
                   .Returns(Task.FromResult(response));

            PersonalityInsightsService service = new PersonalityInsightsService(client);
            service.VersionDate = "versionDate";

            var result =
                service.Profile("contentType", "application/json", contentListContainer);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Behavior.Count > 0);
            Assert.IsTrue(result.ConsumptionPreferences.Count > 0);
            Assert.IsTrue(result.Needs.Count > 0);
            Assert.IsTrue(result.Personality.Count > 0);
            Assert.IsTrue(result.ProcessedLanguage == "en");
            Assert.IsTrue(result.WordCount == 10);
            Assert.IsTrue(result.Values.Count > 0);
            Assert.IsTrue(result.Warnings.Count > 0);
            Assert.IsTrue(result.WordCountMessage == "wordCountMessage");
        }
    }
}
