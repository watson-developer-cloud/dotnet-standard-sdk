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
        string contentString = "The IBM Watson™ Personality Insights service provides a Representational State Transfer (REST) Application Programming Interface (API) that enables applications to derive insights from social media, enterprise data, or other digital communications. The service uses linguistic analytics to infer individuals' intrinsic personality characteristics, including Big Five, Needs, and Values, from digital communications such as email, text messages, tweets, and forum posts. The service can automatically infer, from potentially noisy social media, portraits of individuals that reflect their personality characteristics. The service can report consumption preferences based on the results of its analysis, and for JSON content that is timestamped, it can report temporal behavior.";

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

            client.WithAuthentication(Arg.Any<string>()).Returns(client);

            return client;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Profile_No_Content()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", "password", "versionDate");
            
            service.Profile(null, "contentType");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Profile_No_Version()
        {
            PersonalityInsightsService service =
                new PersonalityInsightsService("username", "password", "versionDate");
            service.VersionDate = null;

            //  Test Profile
            Content content = new Content()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = contentString
                    }
                }
            };

            service.Profile(content, "contentType");
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
            Content content = new Content()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = contentString
                    }
                }
            };

            PersonalityInsightsService service = new PersonalityInsightsService(client);
            service.VersionDate = "versionDate";

            service.Profile(content, "contentType", "application/json");
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
                    ProcessedLanguage = Profile.ProcessedLanguageEnum.EN,
                    WordCount = 10,
                    WordCountMessage = "wordCountMessage",
                    Personality = new List<Trait>()
                    {
                        new Trait()
                        {
                            TraitId = "traitID",
                            Name = "name",
                            Category = Trait.CategoryEnum.NEEDS,
                            Percentile = 50.0,
                            RawScore = 25.0,
                            Children = new List<Trait>()
                            {
                                new Trait()
                                {
                                TraitId = "traitID",
                                Name = "name",
                                Category = Trait.CategoryEnum.NEEDS,
                                Percentile = 50.0,
                                RawScore = 25.0
                                }
                            }
                        }
                    },
                    Values = new List<Trait>()
                    {
                        new Trait()
                        {
                            TraitId = "traitID",
                            Name = "name",
                            Category = Trait.CategoryEnum.PERSONALITY,
                            Percentile = 50.0,
                            RawScore = 25.0,
                            Children = new List<Trait>()
                            {
                                new Trait()
                                {
                                TraitId = "traitID",
                                Name = "name",
                                Category = Trait.CategoryEnum.PERSONALITY,
                                Percentile = 50.0,
                                RawScore = 25.0
                                }
                            }
                        }
                    },
                    Needs = new List<Trait>()
                    {
                        new Trait()
                        {
                            TraitId = "traitID",
                            Name = "name",
                            Category = Trait.CategoryEnum.NEEDS,
                            Percentile = 50.0,
                            RawScore = 25.0,
                            Children = new List<Trait>()
                            {
                                new Trait()
                                {
                                TraitId = "traitID",
                                Name = "name",
                                Category = Trait.CategoryEnum.NEEDS,
                                Percentile = 50.0,
                                RawScore = 25.0
                                }
                            }
                        }
                    },
                    Behavior = new List<Behavior>()
                    {
                        new Behavior()
                        {
                            TraitId = "traitID",
                            Name = "name",
                            Category = "category",
                            Percentage = 50.0
                        }
                    },
                    ConsumptionPreferences = new List<ConsumptionPreferencesCategory>()
                    {
                        new ConsumptionPreferencesCategory()
                        {
                            ConsumptionPreferenceCategoryId = "consumptionPreferenceCategoryId",
                            Name = "name",
                            ConsumptionPreferences = new List<ConsumptionPreferences>()
                            {
                                new ConsumptionPreferences()
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
                            WarningId = Warning.WarningIdEnum.WORD_COUNT_MESSAGE,
                            Message = "message"
                        }
                    }
                };
            #endregion

            //  Test Profile
            Content content = new Content()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = contentString
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
            request.WithBody(Arg.Any<Content>())
                .Returns(request);

            request.As<Profile>()
                   .Returns(Task.FromResult(response));

            PersonalityInsightsService service = new PersonalityInsightsService(client);
            service.VersionDate = "versionDate";

            var result =
                service.Profile(content, "contentType");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Behavior.Count > 0);
            Assert.IsTrue(result.ConsumptionPreferences.Count > 0);
            Assert.IsTrue(result.Needs.Count > 0);
            Assert.IsTrue(result.Personality.Count > 0);
            Assert.IsTrue(result.ProcessedLanguage == Profile.ProcessedLanguageEnum.EN);
            Assert.IsTrue(result.WordCount == 10);
            Assert.IsTrue(result.Values.Count > 0);
            Assert.IsTrue(result.Warnings.Count > 0);
            Assert.IsTrue(result.WordCountMessage == "wordCountMessage");
        }
    }
}
