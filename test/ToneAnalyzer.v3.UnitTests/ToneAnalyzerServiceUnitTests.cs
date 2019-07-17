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

using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Watson.ToneAnalyzer.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace IBM.Watson.ToneAnalyzer.v3.UnitTests
{
    //[TestClass]
    public class ToneAnalyzerServiceUnitTests
    {
        private string versionDate = "2016-05-19";

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
                new ToneAnalyzerService(null, "password", versionDate);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Password_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService("username", null, versionDate);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Version_Null()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService("username", "password", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService("username", "password", versionDate);

            Assert.IsNotNull(service);
        }

        //[TestMethod]
        public void Constructor_HttpClient()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService(CreateClient());

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Constructor()
        {
            ToneAnalyzerService service =
                new ToneAnalyzerService(new IBMHttpClient());

            Assert.IsNotNull(service);
        }

        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();
            
            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                .Returns(client);

            return client;
        }

        //[TestMethod]
        public void Tone_Success_With_ToneInput()
        {
            IClient client = CreateClient();

            #region response
            var response = new DetailedResponse<ToneAnalysis>()
            {
                Result = new ToneAnalysis()
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
                    DocumentTone = new DocumentAnalysis()
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
                }
            };
            #endregion

            IRequest request = Substitute.For<IRequest>();

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<bool>())
                   .Returns(request);
            request.WithArgument(Arg.Any<string>(), Arg.Any<List<string>>())
                   .Returns(request);
            request.WithBody<ToneInput>(Arg.Any<ToneInput>())
                   .Returns(request);
            request.As<ToneAnalysis>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            service.VersionDate = versionDate;
            service.UserName = "username";
            service.Password = "password";

            ToneInput toneInput = new ToneInput()
            {
                Text = "tone text"
            };

            var analyzeTone = service.Tone(new ToneInput(), "text/html");

            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsNotNull(analyzeTone);
            Assert.IsNotNull(analyzeTone.Result.DocumentTone);
            Assert.IsNotNull(analyzeTone.Result.DocumentTone.ToneCategories);
            Assert.IsTrue(analyzeTone.Result.DocumentTone.ToneCategories.Count >= 1);
            Assert.IsNotNull(analyzeTone.Result.SentencesTone);
            Assert.IsTrue(analyzeTone.Result.SentencesTone.Count >= 1);
            Assert.IsNotNull(analyzeTone.Result.SentencesTone[0].ToneCategories);
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].SentenceId == 0);
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].Text == "string");
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].ToneCategories.Count >= 1);
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].ToneCategories[0].CategoryId == "string");
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].ToneCategories[0].CategoryName == "string");
            Assert.IsNotNull(analyzeTone.Result.SentencesTone[0].ToneCategories[0].Tones);
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].ToneCategories[0].Tones.Count >= 1);
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].ToneCategories[0].Tones[0].ToneName == "string");
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].ToneCategories[0].Tones[0].ToneId == "string");
            Assert.IsTrue(analyzeTone.Result.SentencesTone[0].ToneCategories[0].Tones[0].Score == 0);
        }

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        public void Tone_Catch_Exception()
        {
            #region Mock IClient
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });
            #endregion

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            service.VersionDate = versionDate;

            ToneInput toneInput = new ToneInput()
            {
                Text = "tone text"
            };

            service.Tone(toneInput, "application/json");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Tone_ToneInputEmpty()
        {
            #region response
            var response = new DetailedResponse<ToneAnalysis>()
            {
                Result = new ToneAnalysis()
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
                    DocumentTone = new DocumentAnalysis()
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
                }
            };
            #endregion

            ToneAnalyzerService service = new ToneAnalyzerService("username", "password", versionDate);
            var analyzeTone = service.Tone(null, "application/json");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Tone_Empty_Version()
        {
            ToneAnalyzerService service = new ToneAnalyzerService("username", "password", versionDate);
            service.VersionDate = null;

            ToneInput toneInput = new ToneInput()
            {
                Text = Arg.Any<string>()
            };

            var analyzeTone = service.Tone(toneInput, "application/json");
        }

        //[TestMethod]
        public void ToneChat_Success_With_ToneChatInput()
        {
            IClient client = CreateClient();

            #region response
            var response = new DetailedResponse<UtteranceAnalyses>()
            {
                Result = new UtteranceAnalyses()
                {
                    UtterancesTone = new List<UtteranceAnalysis>()
                    {
                        new UtteranceAnalysis()
                        {
                            UtteranceId = 100,
                            UtteranceText = "utteranceText",
                            Tones = new List<ToneChatScore>()
                            {
                                new ToneChatScore()
                                {
                                    ToneName = "string",
                                    ToneId = ToneChatScore.ToneIdEnumValue.SAD,
                                    Score = 0
                                }
                            }
                        }
                    }
                }
            };
            #endregion

            var utterances = new List<Utterance>()
            {
                new Utterance()
                {
                    Text = "utteranceText",
                    User = "utteranceUser"
                }
            };

            IRequest request = Substitute.For<IRequest>();

            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);
            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);
            request.WithBodyContent(Arg.Any<StringContent>())
                   .Returns(request);
            request.As<UtteranceAnalyses>()
                   .Returns(Task.FromResult(response));

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            service.VersionDate = versionDate;

            var result = service.ToneChat(utterances);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.Result.UtterancesTone.Count >= 1);
            Assert.IsTrue(result.Result.UtterancesTone[0].Tones.Count >= 1);
            Assert.IsTrue(result.Result.UtterancesTone[0].Tones[0].ToneName == "string");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ToneChat_ToneChatInputEmpty()
        {
            ToneAnalyzerService service = new ToneAnalyzerService("username", "password", versionDate);
            var result = service.ToneChat(null);
        }

        //[TestMethod, ExpectedException(typeof(AggregateException))]
        public void ToneChat_Catch_Exception()
        {
            #region Mock IClient
            IClient client = CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            ToneAnalyzerService service = new ToneAnalyzerService(client);
            service.VersionDate = versionDate;

            var utterances = new List<Utterance>()
            {
                new Utterance()
                {
                    Text = "utteranceText",
                    User = "utteranceUser"
                }
            };

            service.ToneChat(utterances);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ToneChat_Empty_Version()
        {
            ToneAnalyzerService service = new ToneAnalyzerService("username", "password", versionDate);
            service.VersionDate = null;

            var utterances = new List<Utterance>()
            {
                new Utterance()
                {
                    Text = "utteranceText",
                    User = "utteranceUser"
                }
            };

            service.ToneChat(utterances);
        }
    }
}
