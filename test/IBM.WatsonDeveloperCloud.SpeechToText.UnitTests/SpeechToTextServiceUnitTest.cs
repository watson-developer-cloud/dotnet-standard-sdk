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
using System.Collections.Generic;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.IO;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Util;

namespace IBM.WatsonDeveloperCloud.SpeechToText.UnitTest
{
    [TestClass]
    public class SpeechToTextServiceUnitTest
    {
        [TestMethod]
        public void GetMediaTypeFromFile_Sucess()
        {
            FileStream audio = null;

            audio = new FileStream("GetMediaTypeFromFile_Sucess.wav", FileMode.Create);
            Assert.AreEqual(audio.GetMediaTypeFromFile(), HttpMediaType.AUDIO_WAV);

            audio = new FileStream("GetMediaTypeFromFile_Sucess.ogg", FileMode.Create);
            Assert.AreEqual(audio.GetMediaTypeFromFile(), HttpMediaType.AUDIO_OGG);

            audio = new FileStream("GetMediaTypeFromFile_Sucess.oga", FileMode.Create);
            Assert.AreEqual(audio.GetMediaTypeFromFile(), HttpMediaType.AUDIO_OGG);

            audio = new FileStream("GetMediaTypeFromFile_Sucess.flac", FileMode.Create);
            Assert.AreEqual(audio.GetMediaTypeFromFile(), HttpMediaType.AUDIO_FLAC);

            audio = new FileStream("GetMediaTypeFromFile_Sucess.raw", FileMode.Create);
            Assert.AreEqual(audio.GetMediaTypeFromFile(), HttpMediaType.AUDIO_RAW);
        }

        [TestMethod]
        public void Constructor_Without_Arguments()
        {
            SpeechToTextService service = new SpeechToTextService();
        }

        [TestMethod]
        public void Constructor_With_Credentials()
        {
            SpeechToTextService service = new SpeechToTextService("user_name", "password");
        }

        [TestMethod]
        public void GetModels_Sucess()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechModelSet response = new SpeechModelSet()
            {
                Models = new List<SpeechModel>()
                {
                    new SpeechModel()
                    {
                        Description = "TEST",
                        Language = "pt-br",
                        Name = "UNIT_TEST",
                        Rate = 1,
                        Sessions = Guid.NewGuid().ToString(),
                        Url = "http://",
                        SupportedFeatures = new SupportedFeatures()
                        {
                            CustomLanguageModel = false,
                            SpeakerLabels = false
                        }
                    }
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechModelSet>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModels();

            Assert.IsNotNull(models);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(models.Models.Count > 0);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetModels_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechModelSet response = new SpeechModelSet()
            {
                Models = new List<SpeechModel>()
                {
                    new SpeechModel()
                    {
                        Description = "TEST",
                        Language = "pt-br",
                        Name = "UNIT_TEST",
                        Rate = 1,
                        Sessions = Guid.NewGuid().ToString(),
                        Url = "http://"
                    }
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModels();
        }

        [TestMethod]
        public void GetModel_Sucess()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechModel response = new SpeechModel()
            {
                Description = "description",
                Language = "language",
                Name = "name",
                Rate = 1,
                Sessions = "session",
                SupportedFeatures = new SupportedFeatures()
                {
                    CustomLanguageModel = false,
                    SpeakerLabels = false
                },
                Url = "url"
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechModel>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModel("model_name");

            Assert.IsNotNull(models);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(models.Name);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetModel_Without_ModelName()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModel(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetModel_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.GetModel("model_name");
        }

        [TestMethod]
        public void CreateSession_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            Session response = new Session()
            {
                SessionId = "session_id",
                NewSessionUri = "new_session_uri",
                ObserveResult = "observe_result",
                Recognize = "recognize",
                RecognizeWS = "recognize_ws"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<Session>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.CreateSession("model_name");

            Assert.IsNotNull(models);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(models.SessionId));
        }

        [TestMethod]
        public void CreateSession_Default_Name_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            Session response = new Session()
            {
                SessionId = "session_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<Session>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.CreateSession(string.Empty);

            Assert.IsNotNull(models);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(models.SessionId));
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void CreateSession_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var models = service.CreateSession(string.Empty);
        }

        [TestMethod]
        public void GetSessionStatus_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized",
                    Model = "model",
                    ObserveResult = "observe_result",
                    Recognize = "recognize",
                    RecognizeWS = "recognize_ws"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<RecognizeStatus>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var sessionStatus = service.GetSessionStatus(new Session() { SessionId = "session_id" });

            Assert.IsNotNull(sessionStatus);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.AreEqual(sessionStatus.Session.State, "initialized");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetSessionStatus_SessionId_Empty()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<RecognizeStatus>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var sessionStatus = service.GetSessionStatus(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetSessionStatus_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x =>
                   {
                       throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                   });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var sessionStatus = service.GetSessionStatus("session_id");
        }

        [TestMethod]
        public void DeleteSession_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized",
                    Model = "model",
                    ObserveResult = "observe_result",
                    Recognize = "recognize",
                    RecognizeWS = "recognize_ws"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<RecognizeStatus>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteSession(new Session() { SessionId = "session_id" });

            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteSession_SessionId_Empty()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<RecognizeStatus>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteSession(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void DeleteSession_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            RecognizeStatus response = new RecognizeStatus()
            {
                Session = new SessionStatus()
                {
                    State = "initialized"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                   .Returns(x =>
                   {
                       throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                 Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                 string.Empty));
                   });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteSession("session_id");
        }

        [TestMethod]
        public void Recognize_WithBody_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,
                Results = new List<SpeechRecognitionResult>()
                {
                    new SpeechRecognitionResult()
                    {
                        Alternatives = new List<SpeechRecognitionAlternative>()
                        {
                            new SpeechRecognitionAlternative()
                            {
                                Confidence = 0.5,
                                Timestamps = new List<string>()
                                {
                                    "time_stamps"
                                },
                                Transcript = "transcript",
                                WordConfidence = new List<string>()
                                {
                                    "word_confidence"
                                }
                            }
                        },
                        Final = true,
                        KeywordResults = new KeywordResults()
                        {
                            Keyword = new List<KeywordResult>()
                            {
                                new KeywordResult()
                                {
                                    Confidence = 0.9,
                                    Endtime = 0.5,
                                    NormalizedText = "normalized_text",
                                    StartTime = 0
                                }
                            }
                        },
                        WordAlternativeResults = new List<WordAlternativeResults>()
                        {
                            new WordAlternativeResults()
                            {
                                Alternatives = new List<WordAlternativeResult>()
                                {
                                    new WordAlternativeResult()
                                    {
                                        Confidence = 0.5,
                                        Word = 1
                                    }
                                }
                            }
                        }
                    }
                },
                SpeakerLabels = new List<SpeakerLabelsResult>()
                {
                    new SpeakerLabelsResult()
                    {
                        Confidence = 1,
                        Final = true,
                        From = 1,
                        Speaker = 1,
                        To = 9
                    }
                },
                Warnings = new List<string>()
                {
                    "warnings"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.Recognize(contentType: HttpMediaType.AUDIO_WAV,
                                  transferEncoding: "transferEncoding",
                                  model: "model",
                                  customizationId: "customizationId",
                                  audio: new FileStream("audio_teste", FileMode.Create),
                                  continuous: true,
                                  timestamps: true,
                                  keywords: new string[1],
                                  keywordsThreshold: 0.1,
                                  wordAlternativesThreshold: 0.1,
                                  inactivityTimeout: 1,
                                  maxAlternatives: 2,
                                  profanityFilter: true,
                                  wordConfidence: true,
                                  smartFormatting: true,
                                  speakerLabels: true);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod]
        public void Recognize_WithBody_WithSession_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,
                Results = new List<SpeechRecognitionResult>()
                {
                    new SpeechRecognitionResult()
                    {
                        Alternatives = new List<SpeechRecognitionAlternative>()
                        {
                            new SpeechRecognitionAlternative()
                            {
                                Confidence = 0.5,
                                Timestamps = new List<string>()
                                {
                                    "time_stamps"
                                },
                                Transcript = "transcript",
                                WordConfidence = new List<string>()
                                {
                                    "word_confidence"
                                }
                            }
                        },
                        Final = true,
                        KeywordResults = new KeywordResults()
                        {
                            Keyword = new List<KeywordResult>()
                            {
                                new KeywordResult()
                                {
                                    Confidence = 0.9,
                                    Endtime = 0.5,
                                    NormalizedText = "normalized_text",
                                    StartTime = 0
                                }
                            }
                        },
                        WordAlternativeResults = new List<WordAlternativeResults>()
                        {
                            new WordAlternativeResults()
                            {
                                Alternatives = new List<WordAlternativeResult>()
                                {
                                    new WordAlternativeResult()
                                    {
                                        Confidence = 0.5,
                                        Word = 1
                                    }
                                }
                            }
                        }
                    }
                },
                SpeakerLabels = new List<SpeakerLabelsResult>()
                {
                    new SpeakerLabelsResult()
                    {
                        Confidence = 1,
                        Final = true,
                        From = 1,
                        Speaker = 1,
                        To = 9
                    }
                },
                Warnings = new List<string>()
                {
                    "warnings"
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.RecognizeWithSession(sessionId: "sessionId",
                                             contentType: HttpMediaType.AUDIO_WAV,
                                             transferEncoding: "transferEncoding",
                                             model: "model",
                                             customizationId: "customizationId",
                                             audio: new FileStream("Recognize_WithBody_WithSession_Success", FileMode.Create),
                                             continuous: true,
                                             timestamps: true,
                                             keywords: new string[1],
                                             keywordsThreshold: 0.1,
                                             wordAlternativesThreshold: 0.1,
                                             wordConfidence: true,
                                             smartFormatting: true,
                                             speakerLabels: true);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_WithBody_WithSession_With_SessionId_Empty()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.RecognizeWithSession(sessionId: string.Empty,
                                             contentType: HttpMediaType.AUDIO_WAV,
                                             transferEncoding: "transferEncoding",
                                             model: "model",
                                             customizationId: "customizationId",
                                             audio: new FileStream("Recognize_WithBody_WithSession_With_SessionId_Empty", FileMode.Create),
                                             continuous: true,
                                             timestamps: true,
                                             keywords: new string[1],
                                             keywordsThreshold: 0.1,
                                             wordAlternativesThreshold: 0.1,
                                             wordConfidence: true,
                                             smartFormatting: true,
                                             speakerLabels: true);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_WithBody_WithSession_With_SessionId_Null()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.RecognizeWithSession(sessionId: null,
                                             contentType: HttpMediaType.AUDIO_WAV,
                                             transferEncoding: "transferEncoding",
                                             model: "model",
                                             customizationId: "customizationId",
                                             audio: new FileStream("Recognize_WithBody_WithSession_With_SessionId_Null", FileMode.Create),
                                             continuous: true,
                                             timestamps: true,
                                             keywords: new string[1],
                                             keywordsThreshold: 0.1,
                                             wordAlternativesThreshold: 0.1,
                                             wordConfidence: true,
                                             smartFormatting: true,
                                             speakerLabels: true);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_WithBody_WithSession_With_Audio_Null()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.RecognizeWithSession(sessionId: "sessionId",
                                             contentType: HttpMediaType.AUDIO_WAV,
                                             transferEncoding: "transferEncoding",
                                             model: "model",
                                             customizationId: "customizationId",
                                             audio: null,
                                             continuous: true,
                                             timestamps: true,
                                             keywords: new string[1],
                                             keywordsThreshold: 0.1,
                                             wordAlternativesThreshold: 0.1,
                                             wordConfidence: true,
                                             smartFormatting: true,
                                             speakerLabels: true);

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod]
        public void Recognize_FormData_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,

            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
               service.Recognize(contentType: HttpMediaType.AUDIO_WAV,
                                 metaData: new Metadata()
                                 {
                                     Continuous = true,
                                     DataPartsCount = 1,
                                     InactivityTimeout = 0.1,
                                     Keywords = new string[1],
                                     KeywordsThreshold = 0.1,
                                     MaxAlternatives = 1,
                                     PartContentType = "content_type",
                                     ProfanityFilter = false,
                                     SequenceId = 1,
                                     SmartFormatting = true,
                                     SpeakerLabels = false,
                                     Timestamps = false,
                                     WordAlternativesThreshold = 0.1,
                                     WordConfidence = false
                                 },
                                 audio: new FileStream("audio_teste_form_data", FileMode.Create),
                                 transferEncoding: "transferEncoding",
                                 model: "model",
                                 customizationId: "customizationId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod]
        public void Recognize_FormData_WithSession_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,

            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
               service.RecognizeWithSession(sessionId: "sessionId",
                                            contentType: HttpMediaType.AUDIO_WAV,
                                            metaData: new Metadata()
                                            {
                                                Continuous = true,
                                                DataPartsCount = 1,
                                                InactivityTimeout = 0.1,
                                                Keywords = new string[1],
                                                KeywordsThreshold = 0.1,
                                                MaxAlternatives = 1,
                                                PartContentType = "content_type",
                                                ProfanityFilter = false,
                                                SequenceId = 1,
                                                SmartFormatting = true,
                                                SpeakerLabels = false,
                                                Timestamps = false,
                                                WordAlternativesThreshold = 0.1,
                                                WordConfidence = false
                                            },
                                            audio: new FileStream("Recognize_FormData_WithSession_Success", FileMode.Create),
                                            transferEncoding: "transferEncoding",
                                            model: "model",
                                            customizationId: "customizationId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_FormData_WithSession_With_SessionId_Empty()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
               service.RecognizeWithSession(sessionId: string.Empty,
                                            contentType: HttpMediaType.AUDIO_WAV,
                                            metaData: new Metadata()
                                            {
                                                Continuous = true,
                                                DataPartsCount = 1,
                                                InactivityTimeout = 0.1,
                                                Keywords = new string[1],
                                                KeywordsThreshold = 0.1,
                                                MaxAlternatives = 1,
                                                PartContentType = "content_type",
                                                ProfanityFilter = false,
                                                SequenceId = 1,
                                                SmartFormatting = true,
                                                SpeakerLabels = false,
                                                Timestamps = false,
                                                WordAlternativesThreshold = 0.1,
                                                WordConfidence = false
                                            },
                                            audio: new FileStream("Recognize_FormData_WithSession_With_SessionId_Empty", FileMode.Create),
                                            transferEncoding: "transferEncoding",
                                            model: "model",
                                            customizationId: "customizationId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_FormData_WithSession_With_SessionId_Null()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
               service.RecognizeWithSession(sessionId: null,
                                            contentType: HttpMediaType.AUDIO_WAV,
                                            metaData: new Metadata()
                                            {
                                                Continuous = true,
                                                DataPartsCount = 1,
                                                InactivityTimeout = 0.1,
                                                Keywords = new string[1],
                                                KeywordsThreshold = 0.1,
                                                MaxAlternatives = 1,
                                                PartContentType = "content_type",
                                                ProfanityFilter = false,
                                                SequenceId = 1,
                                                SmartFormatting = true,
                                                SpeakerLabels = false,
                                                Timestamps = false,
                                                WordAlternativesThreshold = 0.1,
                                                WordConfidence = false
                                            },
                                            audio: new FileStream("Recognize_FormData_WithSession_With_SessionId_Null", FileMode.Create),
                                            transferEncoding: "transferEncoding",
                                            model: "model",
                                            customizationId: "customizationId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_FormData_WithSession_With_Audio_Null()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
               service.RecognizeWithSession(sessionId: "sessionId",
                                            contentType: HttpMediaType.AUDIO_WAV,
                                            metaData: new Metadata()
                                            {
                                                Continuous = true,
                                                DataPartsCount = 1,
                                                InactivityTimeout = 0.1,
                                                Keywords = new string[1],
                                                KeywordsThreshold = 0.1,
                                                MaxAlternatives = 1,
                                                PartContentType = "content_type",
                                                ProfanityFilter = false,
                                                SequenceId = 1,
                                                SmartFormatting = true,
                                                SpeakerLabels = false,
                                                Timestamps = false,
                                                WordAlternativesThreshold = 0.1,
                                                WordConfidence = false
                                            },
                                            audio: null,
                                            transferEncoding: "transferEncoding",
                                            model: "model",
                                            customizationId: "customizationId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_FormData_WithSession_With_MetaData_Null()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
               service.RecognizeWithSession(sessionId: "sessionId",
                                            contentType: HttpMediaType.AUDIO_WAV,
                                            metaData: null,
                                            audio: new FileStream("Recognize_FormData_WithSession_With_MetaData_Null", FileMode.Create),
                                            transferEncoding: "transferEncoding",
                                            model: "model",
                                            customizationId: "customizationId");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(result.ResultIndex == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_With_Null_ContentType()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,

            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.Recognize(contentType: null,
                                  audio: new FileStream("Recognize_With_Null_ContentType", FileMode.Create));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_WithBody_With_Null_Audio()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,

            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.Recognize(contentType: "contentType",
                                  audio: null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_FormData_With_Null_ContentType()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,

            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.Recognize(contentType: null,
                                  audio: new FileStream("Recognize_FormData_With_Null_ContentType", FileMode.Create),
                                  metaData: new Metadata());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_FormData_With_Null_Audio()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,

            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.Recognize(contentType: "contentType",
                                  audio: null,
                                  metaData: new Metadata());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Recognize_FormData_With_Null_MetaData()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,

            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.Recognize(contentType: "contentType",
                                  audio: new FileStream("Recognize_FormData_With_Null_MetaData", FileMode.Create),
                                  metaData: null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void Recognize_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.Recognize(contentType: HttpMediaType.AUDIO_WAV,
                                  transferEncoding: "transferEncoding",
                                  model: "model",
                                  customizationId: "customizationId",
                                  audio: new FileStream("Recognize_Catch_Exception", FileMode.Create),
                                  continuous: true,
                                  timestamps: true,
                                  keywords: new string[1],
                                  keywordsThreshold: 0.1,
                                  wordAlternativesThreshold: 0.1,
                                  wordConfidence: true,
                                  smartFormatting: true,
                                  speakerLabels: true);
        }

        [TestMethod]
        public void ObserveResult_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent()
            {
                ResultIndex = 1,
                Results = new List<SpeechRecognitionResult>()
               {
                   new SpeechRecognitionResult()
                   {
                       Alternatives = new List<SpeechRecognitionAlternative>()
                       {
                           new SpeechRecognitionAlternative()
                           {
                               Confidence = 0.9,
                               Timestamps = new List<string>(),
                               Transcript = "transcript",
                               WordConfidence = new List<string>()
                           }
                       },
                       Final = true,
                       KeywordResults = new KeywordResults()
                       {
                           Keyword = new List<KeywordResult>()
                           {
                               new KeywordResult()
                               {
                                   Confidence = 0.9,
                                   Endtime = 0.1,
                                   NormalizedText = "normalized_text",
                                   StartTime = 0
                               }
                           }
                       },
                       WordAlternativeResults = new List<WordAlternativeResults>()
                       {
                           new WordAlternativeResults()
                           {
                               Alternatives = new List<WordAlternativeResult>()
                               {
                                   new WordAlternativeResult()
                                   {
                                       Confidence = 0.9,
                                       Word = 0.1
                                   }
                               },
                               Endtime = 1,
                               StartTime = 0.1
                           }
                       }
                   }
               }
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsList<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(new List<SpeechRecognitionEvent>()
                   {
                       response
                   }));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var recognitionEvent = service.ObserveResult("session_id", 1, true);

            Assert.IsNotNull(recognitionEvent);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(recognitionEvent.Count > 0);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ObserveResult_With_Empty_SessionId()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.As<SpeechRecognitionEvent>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var recognitionEvent = service.ObserveResult(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void ObserveResult_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            SpeechRecognitionEvent response = new SpeechRecognitionEvent();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var recognitionEvent = service.ObserveResult("session_id");
        }

        [TestMethod]
        public void CreateCustomModel_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            CustomizationID response = new CustomizationID()
            {
                CustomizationId = "customization_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<CustomizationID>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.CreateCustomModel(baseModelName: "base_model_name",
                                          description: "a_description",
                                          model: "name_of_model");

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(result.CustomizationId));
        }

        [TestMethod]
        public void CreateCustomModel_With_Options_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            CustomizationID response = new CustomizationID()
            {
                CustomizationId = "customization_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            request.As<CustomizationID>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.CreateCustomModel(new CustomModel()
                {
                    BaseModelName = "base_model_name",
                    Description = "a_description",
                    Name = "name_of_model"
                });

            Assert.IsNotNull(result);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(result.CustomizationId));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCustomModel_With_Empty_Name()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            CustomizationID response = new CustomizationID()
            {
                CustomizationId = "customization_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<CustomizationID>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.CreateCustomModel(new CustomModel()
                {
                    BaseModelName = "base_model_name",
                    Description = "a_description",
                    Name = ""
                });
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateCustomModel_With_Empty_BasModelName()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            CustomizationID response = new CustomizationID()
            {
                CustomizationId = "customization_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<CustomizationID>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.CreateCustomModel(new CustomModel()
                {
                    BaseModelName = "",
                    Description = "a_description",
                    Name = "name_of_model"
                });
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void CreateCustomModel_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            CustomizationID response = new CustomizationID()
            {
                CustomizationId = "customization_id"
            };

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.CreateCustomModel(new CustomModel()
                {
                    BaseModelName = "base_model_name",
                    Description = "a_description",
                    Name = "name_of_model"
                });
        }

        [TestMethod]
        public void ListCustomModels_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            Customizations response = new Customizations()
            {
                Customization = new List<Customization>()
                {
                    new Customization()
                    {
                        BaseModelName = "BaseModelName",
                        Created = "Created",
                        CustomizationId = "CustomizationId",
                        Description = "Description",
                        Language = "Language",
                        Name = "Name",
                        Owner = "Owner",
                        Progress = 100,
                        Status = "Status",
                        Warnings = "Warnings"
                    }
                }
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.As<Customizations>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomModels();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Customization);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomModels_With_Null_Language()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            Customizations response = new Customizations();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.As<Customizations>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomModels(null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void ListCustomModels_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomModels();
        }

        [TestMethod]
        public void GetCustomModels_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            Customization response = new Customization()
            {
                BaseModelName = "BaseModelName",
                Created = "Created",
                CustomizationId = "CustomizationId",
                Description = "Description",
                Language = "Language",
                Name = "Name",
                Owner = "Owner",
                Progress = 100,
                Status = "Status",
                Warnings = "Warnings"
            };

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.As<Customization>()
                   .Returns(Task.FromResult(response));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomModel("custom_model_id");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CustomizationId);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCustomModel_With_Null_CustomizationId()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomModel(null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetCustomModel_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomModel("custom_model_id");
        }

        [TestMethod]
        public void TrainCustomModel_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.TrainCustomModel("custom_model_id");

            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TrainCustomModell_With_Null_CustomizationId()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.TrainCustomModel(null);

            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void TrainCustomModel_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.TrainCustomModel("custom_model_id");
        }

        [TestMethod]
        public void ResetCustomModel_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.ResetCustomModel("custom_model_id");

            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ResetCustomModell_With_Null_CustomizationId()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.ResetCustomModel(null);

            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void ResetCustomModel_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.ResetCustomModel("custom_model_id");
        }

        [TestMethod]
        public void UpgradeCustomModel_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.UpgradeCustomModel("custom_model_id");

            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpgradeCustomModell_With_Null_CustomizationId()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.UpgradeCustomModel(null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void UpgradeCustomModel_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.UpgradeCustomModel("custom_model_id");
        }

        [TestMethod]
        public void DeleteCustomModel_Success()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomModel("custom_model_id");

            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCustomModell_With_Null_CustomizationId()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomModel(null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void DeleteCustomModel_Catch_Exception()
        {
            #region Mock IClient

            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            #endregion

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomModel("custom_model_id");
        }

        [TestMethod]
        public void AddCorpus_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>());

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCorpus("customization_id", "corpus_name", true, new FileStream("body", FileMode.Create));

            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCorpus_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>());

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCorpus(null, "corpus_name", true, new FileStream("body_customizationId_null", FileMode.Create));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCorpus_With_Null_CorpusName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>());

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCorpus("customization_id", null, true, new FileStream("body_corpusName_null", FileMode.Create));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddCorpus_With_UserString_CorpusName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>());

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCorpus("customization_id", "user", true, new FileStream("body_corpus_name_is_user", FileMode.Create));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddCorpus_With_WhiteSpaces_CorpusName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>());

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCorpus("customization_id", "corpus name with white spaces", true, new FileStream("body_corpus_name_whiteSpaces", FileMode.Create));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCorpus_With_Null_Body()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>());

            request.WithArgument(Arg.Any<string>(), Arg.Any<object>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCorpus("customization_id", "corpus_name", true, null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void AddCorpus_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                 .Returns(x =>
                 {
                     throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                 });

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCorpus("customization_id", "corpus_name", true, new FileStream("body_catch_exception", FileMode.Create));
        }

        [TestMethod]
        public void ListCorpora_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Corpora response =
                new Corpora()
                {
                    CorporaProperty = new List<Corpus>()
                    {
                        new Corpus()
                        {
                            Error = "error",
                            Name = "name",
                            OutOfVocabularyWords = 1,
                            Status = "status",
                            TotalWords = 1
                        }
                    }
                };

            request.As<Corpora>()
                   .Returns(Task.FromResult(response));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCorpora("customization_id");

            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CorporaProperty);
            Assert.IsTrue(result.CorporaProperty.Count == 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCorpora_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Corpora response =
                new Corpora()
                {
                    CorporaProperty = new List<Corpus>()
                    {
                        new Corpus()
                        {
                            Error = "error",
                            Name = "name",
                            OutOfVocabularyWords = 1,
                            Status = "status",
                            TotalWords = 1
                        }
                    }
                };

            request.As<Corpora>()
                   .Returns(Task.FromResult(response));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCorpora(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCorpora_With_Empty_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Corpora response =
                new Corpora()
                {
                    CorporaProperty = new List<Corpus>()
                    {
                        new Corpus()
                        {
                            Error = "error",
                            Name = "name",
                            OutOfVocabularyWords = 1,
                            Status = "status",
                            TotalWords = 1
                        }
                    }
                };

            request.As<Corpora>()
                   .Returns(Task.FromResult(response));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCorpora(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void ListCorpora_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCorpora("customization_id");
        }

        [TestMethod]
        public void GetCorpus_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Corpus response =
                new Corpus()
                {
                    Error = "error",
                    Name = "name",
                    OutOfVocabularyWords = 1,
                    Status = "status",
                    TotalWords = 1
                };

            request.As<Corpus>()
                   .Returns(Task.FromResult(response));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.GetCorpus("customization_id", "corpus_name");

            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCorpus_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Corpus response =
                new Corpus()
                {
                    Error = "error",
                    Name = "name",
                    OutOfVocabularyWords = 1,
                    Status = "status",
                    TotalWords = 1
                };

            request.As<Corpus>()
                   .Returns(Task.FromResult(response));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.GetCorpus(null, "corpus_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCorpusa_With_Empty_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Corpus response =
                new Corpus()
                {
                    Error = "error",
                    Name = "name",
                    OutOfVocabularyWords = 1,
                    Status = "status",
                    TotalWords = 1
                };

            request.As<Corpus>()
                   .Returns(Task.FromResult(response));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.GetCorpus(string.Empty, "corpus_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCorpus_With_Null_CorpusName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Corpus response =
                new Corpus()
                {
                    Error = "error",
                    Name = "name",
                    OutOfVocabularyWords = 1,
                    Status = "status",
                    TotalWords = 1
                };

            request.As<Corpus>()
                   .Returns(Task.FromResult(response));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.GetCorpus("customization_id", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetCorpus_With_Empty_CorpusName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            Corpus response =
                new Corpus()
                {
                    Error = "error",
                    Name = "name",
                    OutOfVocabularyWords = 1,
                    Status = "status",
                    TotalWords = 1
                };

            request.As<Corpus>()
                   .Returns(Task.FromResult(response));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.GetCorpus("customization_id", string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetCorpus_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.GetCorpus("customization_id", "corpus_name");
        }

        [TestMethod]
        public void DeleteCorpus_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCorpus("customization_id", "corpus_name");

            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCorpus_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.AsString()
                    .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCorpus(null, "corpus_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCorpus_With_Empty_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.AsString()
                    .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCorpus(string.Empty, "corpus_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCorpus_With_Null_CorpusName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.AsString()
                    .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCorpus("customization_id", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCorpus_With_Empty_CorpusName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.AsString()
                    .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCorpus("customization_id", string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void DeleteCorpus_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCorpus("customization_id", "corpus_name");
        }

        [TestMethod]
        public void AddCustomWords_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<Words>(Arg.Any<Words>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWords("customization_id",
                                  new Words()
                                  {
                                      WordsProperty = new List<Word>()
                                      {
                                          new Word()
                                          {
                                             DisplayAs = "Watson",
                                             SoundsLike = new List<string>()
                                             {
                                                 "wat son"
                                             },
                                             WordProperty = "watson"
                                          },
                                          new Word()
                                          {
                                             DisplayAs = "C#",
                                             SoundsLike = new List<string>()
                                             {
                                                 "si sharp"
                                             },
                                             WordProperty = "csharp"
                                          },
                                           new Word()
                                          {
                                             DisplayAs = "SDK",
                                             SoundsLike = new List<string>()
                                             {
                                                 "S.D.K."
                                             },
                                             WordProperty = "sdk"
                                          }
                                      }
                                  });

            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCustomWords_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<Words>(Arg.Any<Words>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWords(null,
                                  new Words()
                                  {
                                      WordsProperty = new List<Word>()
                                      {
                                          new Word()
                                          {
                                             DisplayAs = "Watson",
                                             SoundsLike = new List<string>()
                                             {
                                                 "wat son"
                                             },
                                             WordProperty = "watson"
                                          },
                                          new Word()
                                          {
                                             DisplayAs = "C#",
                                             SoundsLike = new List<string>()
                                             {
                                                 "si sharp"
                                             },
                                             WordProperty = "csharp"
                                          },
                                           new Word()
                                          {
                                             DisplayAs = "SDK",
                                             SoundsLike = new List<string>()
                                             {
                                                 "S.D.K."
                                             },
                                             WordProperty = "sdk"
                                          }
                                      }
                                  });

            client.Received().PostAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCustomWords_With_Empty_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<Words>(Arg.Any<Words>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWords(string.Empty,
                                  new Words()
                                  {
                                      WordsProperty = new List<Word>()
                                      {
                                          new Word()
                                          {
                                             DisplayAs = "Watson",
                                             SoundsLike = new List<string>()
                                             {
                                                 "wat son"
                                             },
                                             WordProperty = "watson"
                                          },
                                          new Word()
                                          {
                                             DisplayAs = "C#",
                                             SoundsLike = new List<string>()
                                             {
                                                 "si sharp"
                                             },
                                             WordProperty = "csharp"
                                          },
                                           new Word()
                                          {
                                             DisplayAs = "SDK",
                                             SoundsLike = new List<string>()
                                             {
                                                 "S.D.K."
                                             },
                                             WordProperty = "sdk"
                                          }
                                      }
                                  });
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCustomWords_With_Null_Words()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<Words>(Arg.Any<Words>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWords("customization_id", null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void AddCustomWords_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWords("customization_id",
                                  new Words()
                                  {
                                      WordsProperty = new List<Word>()
                                      {
                                          new Word()
                                          {
                                             DisplayAs = "Watson",
                                             SoundsLike = new List<string>()
                                             {
                                                 "wat son"
                                             },
                                             WordProperty = "watson"
                                          },
                                          new Word()
                                          {
                                             DisplayAs = "C#",
                                             SoundsLike = new List<string>()
                                             {
                                                 "si sharp"
                                             },
                                             WordProperty = "csharp"
                                          },
                                           new Word()
                                          {
                                             DisplayAs = "SDK",
                                             SoundsLike = new List<string>()
                                             {
                                                 "S.D.K."
                                             },
                                             WordProperty = "sdk"
                                          }
                                      }
                                  });
        }

        [TestMethod]
        public void AddCustomWord_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<WordDefinition>(Arg.Any<WordDefinition>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWord("customization_id",
                                  "word_name",
                                  new WordDefinition()
                                  {
                                      DisplayAs = "Social",
                                      SoundsLike = new List<string>()
                                             {
                                                 "so cial"
                                             }
                                  });

            client.Received().PutAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCustomWord_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<WordDefinition>(Arg.Any<WordDefinition>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWord(null,
                                  "word_name",
                                  new WordDefinition()
                                  {
                                      DisplayAs = "Social",
                                      SoundsLike = new List<string>()
                                             {
                                                 "so cial"
                                             }
                                  });
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCustomWord_With_Empty_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<WordDefinition>(Arg.Any<WordDefinition>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWord(string.Empty,
                                  "word_name",
                                  new WordDefinition()
                                  {
                                      DisplayAs = "Social",
                                      SoundsLike = new List<string>()
                                             {
                                                 "so cial"
                                             }
                                  });
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCustomWord_With_Null_WordName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<WordDefinition>(Arg.Any<WordDefinition>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWord("customization_id",
                                  null,
                                  new WordDefinition()
                                  {
                                      DisplayAs = "Social",
                                      SoundsLike = new List<string>()
                                             {
                                                 "so cial"
                                             }
                                  });
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCustomWord_With_Empty_WordName()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<WordDefinition>(Arg.Any<WordDefinition>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWord("customization_id",
                                  string.Empty,
                                  new WordDefinition()
                                  {
                                      DisplayAs = "Social",
                                      SoundsLike = new List<string>()
                                             {
                                                 "so cial"
                                             }
                                  });
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddCustomWord_With_Null_WordDefinition()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<Words>(Arg.Any<Words>())
                   .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWord("customization_id",
                                 "word_name",
                                 null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void AddCustomWord_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.PutAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            SpeechToTextService service = new SpeechToTextService(client);

            service.AddCustomWord("customization_id",
                                  "word_name",
                                  new WordDefinition()
                                  {
                                      DisplayAs = "Social",
                                      SoundsLike = new List<string>()
                                             {
                                                 "so cial"
                                             }
                                  });
        }

        [TestMethod]
        public void ListCustomWords_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WordsList>()
                   .Returns(Task.FromResult(new WordsList()
                   {
                       Words = new List<WordData>()
                       {
                           new WordData()
                           {
                               Count = 1,
                               DisplayAs = "display_as",
                               Error = new List<WordError>()
                               {
                                   new WordError()
                                   {
                                       Element = "element"
                                   }
                               },
                               SoundsLike = new List<string>()
                               {
                                   "sounds",
                                   "like"
                               },
                               Source = new List<string>()
                               {
                                   "source"
                               },
                               Word = "word"
                           }
                       }
                   }));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords("customization_id", null, null);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Words);
            Assert.IsTrue(result.Words.Count > 0);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void ListCustomWords_With_WordType()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WordsList>()
                   .Returns(Task.FromResult(new WordsList()
                   {
                       Words = new List<WordData>()
                       {
                           new WordData()
                           {
                               Count = 1,
                               DisplayAs = "display_as",
                               Error = new List<WordError>()
                               {
                                   new WordError()
                                   {
                                       Element = "element"
                                   }
                               },
                               SoundsLike = new List<string>()
                               {
                                   "sounds",
                                   "like"
                               },
                               Source = new List<string>()
                               {
                                   "source"
                               },
                               Word = "word"
                           }
                       }
                   }));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords("customization_id", WordType.All, null);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Words);
            Assert.IsTrue(result.Words.Count > 0);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void ListCustomWords_With_Sort_AscendingAlphabetical()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WordsList>()
                   .Returns(Task.FromResult(new WordsList()
                   {
                       Words = new List<WordData>()
                       {
                           new WordData()
                           {
                               Count = 1,
                               DisplayAs = "display_as",
                               Error = new List<WordError>()
                               {
                                   new WordError()
                                   {
                                       Element = "element"
                                   }
                               },
                               SoundsLike = new List<string>()
                               {
                                   "sounds",
                                   "like"
                               },
                               Source = new List<string>()
                               {
                                   "source"
                               },
                               Word = "word"
                           }
                       }
                   }));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords("customization_id", null, Sort.AscendingAlphabetical);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Words);
            Assert.IsTrue(result.Words.Count > 0);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void ListCustomWords_With_Sort_AscendingCount()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WordsList>()
                   .Returns(Task.FromResult(new WordsList()
                   {
                       Words = new List<WordData>()
                       {
                           new WordData()
                           {
                               Count = 1,
                               DisplayAs = "display_as",
                               Error = new List<WordError>()
                               {
                                   new WordError()
                                   {
                                       Element = "element"
                                   }
                               },
                               SoundsLike = new List<string>()
                               {
                                   "sounds",
                                   "like"
                               },
                               Source = new List<string>()
                               {
                                   "source"
                               },
                               Word = "word"
                           }
                       }
                   }));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords("customization_id", null, Sort.AscendingCount);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Words);
            Assert.IsTrue(result.Words.Count > 0);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void ListCustomWords_With_Sort_DescendingAlphabetical()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WordsList>()
                   .Returns(Task.FromResult(new WordsList()
                   {
                       Words = new List<WordData>()
                       {
                           new WordData()
                           {
                               Count = 1,
                               DisplayAs = "display_as",
                               Error = new List<WordError>()
                               {
                                   new WordError()
                                   {
                                       Element = "element"
                                   }
                               },
                               SoundsLike = new List<string>()
                               {
                                   "sounds",
                                   "like"
                               },
                               Source = new List<string>()
                               {
                                   "source"
                               },
                               Word = "word"
                           }
                       }
                   }));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords("customization_id", null, Sort.DescendingAlphabetical);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Words);
            Assert.IsTrue(result.Words.Count > 0);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod]
        public void ListCustomWords_With_Sort_DescendingCount()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<WordsList>()
                   .Returns(Task.FromResult(new WordsList()
                   {
                       Words = new List<WordData>()
                       {
                           new WordData()
                           {
                               Count = 1,
                               DisplayAs = "display_as",
                               Error = new List<WordError>()
                               {
                                   new WordError()
                                   {
                                       Element = "element"
                                   }
                               },
                               SoundsLike = new List<string>()
                               {
                                   "sounds",
                                   "like"
                               },
                               Source = new List<string>()
                               {
                                   "source"
                               },
                               Word = "word"
                           }
                       }
                   }));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords("customization_id", null, Sort.DescendingCount);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Words);
            Assert.IsTrue(result.Words.Count > 0);
            client.Received().GetAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomWords_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords(null, null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomWords_With_Empty_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords(string.Empty, null, null);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void ListCustomWords_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWords("customization_id", null, null);
        }

        [TestMethod]
        public void ListCustomWord_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);
            
            request.As<WordData>()
                   .Returns(Task.FromResult(new WordData()
                   {
                       Count = 1,
                       DisplayAs = "display_as",
                       Error = new List<WordError>()
                       {
                           new WordError()
                           {
                               Element = "elemente"
                           }
                       },
                       SoundsLike = new List<string>()
                       {
                           "sounds",
                           "like"
                       },
                       Source = new List<string>()
                       {
                           "source"
                       },
                       Word = "word"
                   }));

            SpeechToTextService service = new SpeechToTextService(client);

            var result =
                service.ListCustomWord("customization_id", "word_name");

            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsNotNull(result);
            Assert.AreEqual(result.DisplayAs, "display_as");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomWord_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            service.ListCustomWord(null, "word_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomWord_With_Empty_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            service.ListCustomWord(string.Empty, "word_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomWord_With_Null_WordName()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            service.ListCustomWord("customization_id", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomWord_With_Empty_WordName()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            service.ListCustomWord("customization_id", string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void ListCustomWord_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            SpeechToTextService service = new SpeechToTextService(client);

            service.ListCustomWord("customization_id", "word_name");
        }

        [TestMethod]
        public void DeleteCustomWord_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.AsString()
                   .Returns(Task.FromResult("{}"));

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomWord("customization_id", "word_name");

            client.Received().DeleteAsync(Arg.Any<string>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCustomWord_With_Null_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomWord(null, "word_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCustomWord_With_Empty_CustomizationId()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomWord(string.Empty, "word_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCustomWord_With_Null_WordName()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomWord("customization_id", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteCustomWord_With_Empty_WordName()
        {
            IClient client = Substitute.For<IClient>();

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomWord("customization_id", string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void DeleteCustomWord_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                  });

            SpeechToTextService service = new SpeechToTextService(client);

            service.DeleteCustomWord("customization_id", "corpus_name");
        }
    }
}