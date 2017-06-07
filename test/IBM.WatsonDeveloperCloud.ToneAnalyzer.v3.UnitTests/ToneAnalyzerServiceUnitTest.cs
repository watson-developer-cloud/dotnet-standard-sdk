﻿/**
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

using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace IBM.WatsonDeveloperCloud.ToneAnalyzer.UnitTests
{
    [TestClass]
    public class ToneAnalyzerServiceUnitTest
    {
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            return client;
        }

        [TestMethod]
        public void AnalyzeTone_Success_With_Text()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response
            ToneAnalysis response = new ToneAnalysis()
            {
                SentencesTone = new List<SentenceAnalysis>()
                {
                    new SentenceAnalysis()
                    {
                        SentenceId = 0,
                        InputFrom = 0,
                        InputTo = 0,
                        Text = "string",
                        ToneCategories = new List<ToneCategory>()
                        {
                            new ToneCategory()
                            {
                                CategoryName = "string",
                                CategoryId = "string",
                                Tones = new List<ToneScore>()
                                {
                                    new ToneScore()
                                    {
                                        ToneName = "string",
                                        ToneId = "string",
                                        Score = 0
                                    }
                                }
                            }
                        }
                    }
                },
                DocumentTone = new DocumentTone()
                {
                    ToneCategories = new List<ToneCategory>()
                    {
                        new ToneCategory()
                        {
                            CategoryName = "string",
                            CategoryId = "string",
                            Tones = new List<ToneScore>()
                            {
                                new ToneScore()
                                {
                                    ToneName = "string",
                                    ToneId = "string",
                                    Score = 0
                                }
                            }
                        }
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ToneAnalysis>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);

            var analyzeTone = service.AnalyzeTone("A word is dead when it is said, some say. Emily Dickinson");

            Assert.IsNotNull(analyzeTone);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(analyzeTone.DocumentTone.ToneCategories.Count >= 1);

        }

        [TestMethod]
        public void AnalyzeTone_Success_With_Text_Tones()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response
            ToneAnalysis response = new ToneAnalysis()
            {
                SentencesTone = new List<SentenceAnalysis>()
                {
                    new SentenceAnalysis()
                    {
                        SentenceId = 0,
                        InputFrom = 0,
                        InputTo = 0,
                        Text = "string",
                        ToneCategories = new List<ToneCategory>()
                        {
                            new ToneCategory()
                            {
                                CategoryName = "string",
                                CategoryId = "string",
                                Tones = new List<ToneScore>()
                                {
                                    new ToneScore()
                                    {
                                        ToneName = "string",
                                        ToneId = "string",
                                        Score = 0
                                    }
                                }
                            }
                        }
                    }
                },
                DocumentTone = new DocumentTone()
                {
                    ToneCategories = new List<ToneCategory>()
                    {
                        new ToneCategory()
                        {
                            CategoryName = "string",
                            CategoryId = "string",
                            Tones = new List<ToneScore>()
                            {
                                new ToneScore()
                                {
                                    ToneName = "string",
                                    ToneId = "string",
                                    Score = 0
                                }
                            }
                        }
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ToneAnalysis>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);

            var analyzeTone = service.AnalyzeTone("A word is dead when it is said, some say. Emily Dickinson", new List<Tone>() { Tone.EMOTION, Tone.LANGUAGE, Tone.SOCIAL });

            Assert.IsNotNull(analyzeTone);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(analyzeTone.DocumentTone.ToneCategories.Count >= 1);

        }

        [TestMethod]
        public void AnalyzeTone_Success_With_Text_Tones_Sentences()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response
            ToneAnalysis response = new ToneAnalysis()
            {
                SentencesTone = new List<SentenceAnalysis>()
                {
                    new SentenceAnalysis()
                    {
                        SentenceId = 0,
                        InputFrom = 0,
                        InputTo = 0,
                        Text = "string",
                        ToneCategories = new List<ToneCategory>()
                        {
                            new ToneCategory()
                            {
                                CategoryName = "string",
                                CategoryId = "string",
                                Tones = new List<ToneScore>()
                                {
                                    new ToneScore()
                                    {
                                        ToneName = "string",
                                        ToneId = "string",
                                        Score = 0
                                    }
                                }
                            }
                        }
                    }
                },
                DocumentTone = new DocumentTone()
                {
                    ToneCategories = new List<ToneCategory>()
                    {
                        new ToneCategory()
                        {
                            CategoryName = "string",
                            CategoryId = "string",
                            Tones = new List<ToneScore>()
                            {
                                new ToneScore()
                                {
                                    ToneName = "string",
                                    ToneId = "string",
                                    Score = 0
                                }
                            }
                        }
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ToneAnalysis>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);

            var analyzeTone = service.AnalyzeTone("A word is dead when it is said, some say. Emily Dickinson", new List<Tone>() { Tone.EMOTION, Tone.LANGUAGE, Tone.SOCIAL }, false);

            Assert.IsNotNull(analyzeTone);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(analyzeTone.DocumentTone.ToneCategories.Count >= 1);

        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AnalyzeTone_Text_Null()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response
            ToneAnalysis response = new ToneAnalysis()
            {
                SentencesTone = new List<SentenceAnalysis>()
                {
                    new SentenceAnalysis()
                    {
                        SentenceId = 0,
                        InputFrom = 0,
                        InputTo = 0,
                        Text = "string",
                        ToneCategories = new List<ToneCategory>()
                        {
                            new ToneCategory()
                            {
                                CategoryName = "string",
                                CategoryId = "string",
                                Tones = new List<ToneScore>()
                                {
                                    new ToneScore()
                                    {
                                        ToneName = "string",
                                        ToneId = "string",
                                        Score = 0
                                    }
                                }
                            }
                        }
                    }
                },
                DocumentTone = new DocumentTone()
                {
                    ToneCategories = new List<ToneCategory>()
                    {
                        new ToneCategory()
                        {
                            CategoryName = "string",
                            CategoryId = "string",
                            Tones = new List<ToneScore>()
                            {
                                new ToneScore()
                                {
                                    ToneName = "string",
                                    ToneId = "string",
                                    Score = 0
                                }
                            }
                        }
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<ToneAnalysis>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);

            service.AnalyzeTone(null, new List<Tone>() { Tone.EMOTION, Tone.LANGUAGE, Tone.SOCIAL }, false);

        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void AnalyzeTone_Cath_Exception()
        {
            IClient client = this.CreateClient();
            IRequest request = Substitute.For<IRequest>();

            #region response
            ToneAnalysis response = new ToneAnalysis()
            {
                SentencesTone = new List<SentenceAnalysis>()
                {
                    new SentenceAnalysis()
                    {
                        SentenceId = 0,
                        InputFrom = 0,
                        InputTo = 0,
                        Text = "string",
                        ToneCategories = new List<ToneCategory>()
                        {
                            new ToneCategory()
                            {
                                CategoryName = "string",
                                CategoryId = "string",
                                Tones = new List<ToneScore>()
                                {
                                    new ToneScore()
                                    {
                                        ToneName = "string",
                                        ToneId = "string",
                                        Score = 0
                                    }
                                }
                            }
                        }
                    }
                },
                DocumentTone = new DocumentTone()
                {
                    ToneCategories = new List<ToneCategory>()
                    {
                        new ToneCategory()
                        {
                            CategoryName = "string",
                            CategoryId = "string",
                            Tones = new List<ToneScore>()
                            {
                                new ToneScore()
                                {
                                    ToneName = "string",
                                    ToneId = "string",
                                    Score = 0
                                }
                            }
                        }
                    }
                }
            };

            #endregion

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            request.As<ToneAnalysis>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);

            service.AnalyzeTone("A word is dead when it is said, some say. Emily Dickinson", new List<Tone>() { Tone.EMOTION, Tone.LANGUAGE, Tone.SOCIAL }, false);

        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService(null, "pass");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_PassWord_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService("username", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService("username", "password");
        }
    }
}
