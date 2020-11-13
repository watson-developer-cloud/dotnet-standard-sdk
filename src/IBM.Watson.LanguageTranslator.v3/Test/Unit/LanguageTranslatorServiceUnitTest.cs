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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Net;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.LanguageTranslator.v3.Model;
using IBM.Cloud.SDK.Core.Http.Exceptions;

namespace IBM.Watson.LanguageTranslator.v3.UnitTests
{
    [TestClass]
    public class LanguageTranslatorServiceUnitTest
    {
        /// <summary>
        /// Create a IClient Mock
        /// </summary>
        /// <returns></returns>
        private IClient CreateClient()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                .Returns(client);

            return client;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService(httpClient: null);
        }

        [TestMethod]
        public void ListModels_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            var response = new DetailedResponse<TranslationModels>()
            {
                Result = new TranslationModels()
                {
                    Models = new List<TranslationModel>()
                    {
                        new TranslationModel()
                        {
                            ModelId = "ar-en",
                            Source = "ar",
                            Target = "en",
                            BaseModelId = "",
                            Domain = "news",
                            Customizable = true,
                            DefaultModel = true,
                            Owner = "",
                            Status = TranslationModel.StatusEnumValue.AVAILABLE,
                            Name = ""
                        }
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<TranslationModels>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var translationModels = service.ListModels("ar", "en");

            Assert.IsNotNull(translationModels);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(translationModels.Result.Models.Count > 0);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void ListModels_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x =>
                   {
                       throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                   Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                   string.Empty));
                   });

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var translationModels = service.ListModels("ar", "en");
        }

        [TestMethod]
        public void CreateModel_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            request.As<TranslationModel>()
                   .Returns(Task.FromResult(new DetailedResponse<TranslationModel>()
                   {
                       Result = new TranslationModel()
                       {
                           ModelId = "new_id"
                       }
                   }));

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var customModel = service.CreateModel("model_unit_test");

            Assert.IsNotNull(customModel);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(customModel.Result.ModelId));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateModel_BaseModelId_Null()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);

            var customModel = service.CreateModel(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void CreateModel_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(x =>
                   {
                       throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                   Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                   string.Empty));
                   });

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var customModel = service.CreateModel("model_unit_test");
        }

        [TestMethod]
        public void DeleteModel_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<DeleteModelResult>()
                   .Returns(Task.FromResult(new DetailedResponse<DeleteModelResult>()
                   {
                       Result = new DeleteModelResult()
                       {
                           Status = "success"
                       }
                   }));

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var deletedModel = service.DeleteModel("model_id");

            Assert.IsNotNull(deletedModel);
            client.Received().DeleteAsync(Arg.Any<string>());
            Assert.IsTrue(deletedModel.Result.Status.Equals("success"));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void DeleteModel_ModelId_Null()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);

            var deletedModel = service.DeleteModel(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void DeleteModel_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(x => { throw new AggregateException(new Exception()); });

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var deletedModel = service.DeleteModel("model_id");
        }

        [TestMethod]
        public void GetModelDetails_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<TranslationModel>()
                   .Returns(Task.FromResult(new DetailedResponse<TranslationModel>()
                   {
                       Result = new TranslationModel()
                       {
                           ModelId = "model_id"
                       }
                   }));

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var modelDetails = service.GetModel("model_id");

            Assert.IsNotNull(modelDetails);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(modelDetails.Result.ModelId));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetModelDetails_ModelId_Null()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<TranslationModel>()
                   .Returns(Task.FromResult(new DetailedResponse<TranslationModel>()
                   {
                       Result = new TranslationModel()
                       {
                           ModelId = "model_id"
                       }
                   }));

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);

            service.GetModel(null);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetModelDetails_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x => { throw new AggregateException(new Exception()); });

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);
            service.Version = "versionDate";

            service.GetModel("model_id");
        }

        [TestMethod]
        public void Translate_Success_WithModel()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBodyContent(new StringContent("translation"))
                   .Returns(request);

            var response = new DetailedResponse<TranslationResult>()
            {
                Result = new TranslationResult()
                {
                    Translations = new List<Translation>()
                    {
                       new Translation()
                       {
                           _Translation = "text translated"
                       }
                    },
                    WordCount = 1,
                    CharacterCount = 1
                }
            };

            request.As<TranslationResult>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var translation = service.Translate(new List<string>() { "text in any language" });

            Assert.IsNotNull(translation);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(translation.Result.Translations.Count == 1);
        }

        [TestMethod]
        public void Translate_Success_With_Source_Target()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBodyContent(new StringContent("translation"))
                   .Returns(request);

            var response = new DetailedResponse<TranslationResult>()
            {
                Result = new TranslationResult()
                {
                    Translations = new List<Translation>()
                    {
                       new Translation()
                       {
                           _Translation = "text translated"
                       }
                    }
                }
            };

            request.As<TranslationResult>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var translation = service.Translate(new List<string>() { "text in any language" }, source: "source", target: "target");

            Assert.IsNotNull(translation);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(translation.Result.Translations.Count == 1);
        }

        [TestMethod]
        public void Translate_Success_With_Source_Target_List()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBodyContent(new StringContent("translation"))
                   .Returns(request);

            var response = new DetailedResponse<TranslationResult>()
            {
                Result = new TranslationResult()
                {
                    Translations = new List<Translation>()
                    {
                       new Translation()
                       {
                           _Translation = "text translated"
                       }
                    }
                }
            };

            request.As<TranslationResult>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var translation = service.Translate(new List<string>() { "text in any language" }, "modelId", "source", "target");

            Assert.IsNotNull(translation);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(translation.Result.Translations.Count == 1);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Translate_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBodyContent(Arg.Any<StringContent>())
                   .Returns(x =>
                   {
                       throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                   Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                   string.Empty));
                   });

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            service.Translate(new List<string>() { "text in any language" });
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            var response = new DetailedResponse<IdentifiableLanguages>()
            {
                Result = new IdentifiableLanguages()
                {
                    Languages = new List<IdentifiableLanguage>()
                    {
                        new IdentifiableLanguage()
                        {
                            Language = "language",
                            Name = "name"
                        }
                    }
                }
            };

            request.As<IdentifiableLanguages>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            var identifiableLanguages = service.ListIdentifiableLanguages();

            Assert.IsNotNull(identifiableLanguages);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(identifiableLanguages.Result.Languages.Count == 1);
            Assert.IsTrue(identifiableLanguages.Result.Languages.First().Name.Equals("name"));
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void GetIdentifiableLanguages_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x => { throw new AggregateException(new Exception()); });

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            service.ListIdentifiableLanguages();
        }

        [TestMethod]
        public void Identify_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            var response = new DetailedResponse<IdentifiedLanguages>()
            {
                Result = new IdentifiedLanguages()
                {
                    Languages = new List<IdentifiedLanguage>()
                    {
                        new IdentifiedLanguage()
                        {
                            Confidence = 1,
                            Language = "language"
                        }
                    }
                }
            };

            request.WithBodyContent(Arg.Any<StringContent>())
                   .Returns(request);

            request.As<IdentifiedLanguages>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);
            service.Version = "versionDate";

            DetailedResponse<IdentifiedLanguages> identifiedLanguages = service.Identify("any text");

            Assert.IsNotNull(identifiedLanguages);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(identifiedLanguages.Result.Languages.Count == 1);
            Assert.IsTrue(identifiedLanguages.Result.Languages.First().Confidence == 1);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Identify_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                   Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                   string.Empty));
                  });

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            service.Version = "versionDate";

            service.Identify("any text");
        }
    }
}
