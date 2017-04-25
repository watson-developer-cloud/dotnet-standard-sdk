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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v2.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NSubstitute;

namespace IBM.WatsonDeveloperCloud.LanguageTranslator.UnitTests
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
                new LanguageTranslatorService(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_UserName_Null()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService(null, "pass");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_PassWord_Null()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService("username", null);
        }

        [TestMethod]
        public void Constructor_With_UserName_Password()
        {
            LanguageTranslatorService service =
                new LanguageTranslatorService("username", "password");
        }

        [TestMethod]
        public void ListModels_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            TranslationModels response = new TranslationModels()
            {
                Models = new List<ModelPayload>()
                {
                    new ModelPayload()
                    {
                        ModelId = "ar-en",
                        Source = "ar",
                        Target = "en",
                        BaseModelId = "",
                        Domain = "news",
                        Customizable = true,
                        DefaultModel = true,
                        Owner = "",
                        Status = "available",
                        Name = ""
                    }
                }
            };

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.As<TranslationModels>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            var translationModels = service.ListModels(false, "ar", "en");

            Assert.IsNotNull(translationModels);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(translationModels.Models.Count > 0);
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ListModels_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            var translationModels = service.ListModels(false, "ar", "en");
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

            request.As<CustomModels>()
                   .Returns(Task.FromResult(new CustomModels()
                   {
                       ModelId = "new_id"
                   }));

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);
            
            var customModel =
                service.CreateModel(CreateModelOptions.CreateOptions()
                                                      .WithName("base_id")
                                                      .WithBaseModelId("model_unit_test")
                                                      .SetForcedGlossary(Substitute.For<FileStream>("any_file", FileMode.Create)));

            Assert.IsNotNull(customModel);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(customModel.ModelId));
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

            var customModel =
                service.CreateModel(CreateModelOptions.CreateOptions()
                                                      .WithName("model_unit_test")
                                                      .WithBaseModelId(null)
                                                      .SetForcedGlossary(Substitute.For<FileStream>("any_file_create_model", FileMode.Create)));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CreateModel_ModelName_WithSpaces()
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

            var customModel =
                service.CreateModel(CreateModelOptions.CreateOptions()
                                                      .WithName("model name")
                                                      .WithBaseModelId("base_id")
                                                      .SetForcedGlossary(Substitute.For<FileStream>("any_file_model_name_with_spaces", FileMode.Create)));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreateModel_File_Null()
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

            var customModel =
                service.CreateModel(CreateModelOptions.CreateOptions()
                                                      .WithName("base_id")
                                                      .WithBaseModelId("model_unit_test")
                                                      .SetForcedGlossary(null));
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
                   .Returns(x => { throw new AggregateException(new Exception()); });

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);

            var customModel =
                service.CreateModel(CreateModelOptions.CreateOptions()
                                                      .WithName("base_id")
                                                      .WithBaseModelId("model_unit_test")
                                                      .SetForcedGlossary(Substitute.For<FileStream>("any_file_catch_exception", FileMode.Create)));
        }

        [TestMethod]
        public void DeleteModel_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<DeleteModels>()
                   .Returns(Task.FromResult(new DeleteModels()
                   {
                       Deleted = "success"
                   }));

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);

            var deletedModel = service.DeleteModel("model_id");

            Assert.IsNotNull(deletedModel);
            client.Received().DeleteAsync(Arg.Any<string>());
            Assert.IsTrue(deletedModel.Deleted.Equals("success"));
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

            var deletedModel = service.DeleteModel("model_id");
        }

        [TestMethod]
        public void GetModelDetails_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<ModelPayload>()
                   .Returns(Task.FromResult(new ModelPayload()
                   {
                       ModelId = "model_id"
                   }));

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);

            var modelDetails = service.GetModelDetails("model_id");

            Assert.IsNotNull(modelDetails);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsFalse(string.IsNullOrEmpty(modelDetails.ModelId));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetModelDetails_ModelId_Null()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            request.As<ModelPayload>()
                   .Returns(Task.FromResult(new ModelPayload()
                   {
                       ModelId = "model_id"
                   }));

            LanguageTranslatorService service =
               new LanguageTranslatorService(client);

            service.GetModelDetails(null);
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

            service.GetModelDetails("model_id");
        }

        [TestMethod]
        public void Translate_Success_WithModel()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            TranslateResponse response = new TranslateResponse()
            {
                Translations = new List<Translations>()
                {
                   new Translations()
                   {
                       Translation = "text translated"
                   }
                },
                WordCount = 1,
                CharacterCount = 1
            };

            request.As<TranslateResponse>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            var translation = service.Translate("model_id", "text in any language");

            Assert.IsNotNull(translation);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(translation.Translations.Count == 1);
        }

        [TestMethod]
        public void Translate_Success_With_Source_Target()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            TranslateResponse response = new TranslateResponse()
            {
                Translations = new List<Translations>()
                {
                   new Translations()
                   {
                       Translation = "text translated"
                   }
                }
            };

            request.As<TranslateResponse>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            var translation = service.Translate("source", "target", "text in any language");

            Assert.IsNotNull(translation);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(translation.Translations.Count == 1);
        }

        [TestMethod]
        public void Translate_Success_WithModel_List()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            TranslateResponse response = new TranslateResponse()
            {
                Translations = new List<Translations>()
                {
                   new Translations()
                   {
                       Translation = "text translated"
                   }
                }
            };

            request.As<TranslateResponse>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            var translation = service.Translate("model_id", new List<string>() { "text in any language" });

            Assert.IsNotNull(translation);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(translation.Translations.Count == 1);
        }

        [TestMethod]
        public void Translate_Success_With_Source_Target_List()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(request);

            TranslateResponse response = new TranslateResponse()
            {
                Translations = new List<Translations>()
                {
                   new Translations()
                   {
                       Translation = "text translated"
                   }
                }
            };

            request.As<TranslateResponse>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            var translation = service.Translate("source", "target", new List<string>() { "text in any language" });

            Assert.IsNotNull(translation);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(translation.Translations.Count == 1);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Translate_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithBody<JObject>(Arg.Any<JObject>(), Arg.Any<MediaTypeHeaderValue>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            service.Translate("source", "target", new List<string>() { "text in any language" });
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            IdentifiableLanguages response = new IdentifiableLanguages()
            {
               Languages = new List<IdentifiableLanguage>()
               {
                   new IdentifiableLanguage()
                   {
                       Language = "language",
                       Name = "name"
                   }
               } 
            };

            request.As<IdentifiableLanguages>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            var identifiableLanguages = service.GetIdentifiableLanguages();

            Assert.IsNotNull(identifiableLanguages);
            client.Received().GetAsync(Arg.Any<string>());
            Assert.IsTrue(identifiableLanguages.Languages.Count == 1);
            Assert.IsTrue(identifiableLanguages.Languages.First().Name.Equals("name"));
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

            service.GetIdentifiableLanguages();
        }

        [TestMethod]
        public void Identify_Success()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), HttpMediaType.APPLICATION_JSON)
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(request);

            IdentifiedLanguages response = new IdentifiedLanguages()
            {
                Languages = new List<IdentifiedLanguage>()
                {
                    new IdentifiedLanguage()
                    {
                        Confidence = 1,
                        Language = "language"
                    }
                }
            };

            request.As<IdentifiedLanguages>()
                   .Returns(Task.FromResult(response));

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            IdentifiedLanguages identifiedLanguages = service.Identify("any text");

            Assert.IsNotNull(identifiedLanguages);
            client.Received().PostAsync(Arg.Any<string>());
            Assert.IsTrue(identifiedLanguages.Languages.Count == 1);
            Assert.IsTrue(identifiedLanguages.Languages.First().Confidence == 1);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void Identify_Catch_Exception()
        {
            IClient client = this.CreateClient();

            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                  .Returns(request);

            request.WithHeader(Arg.Any<string>(), HttpMediaType.APPLICATION_JSON)
                   .Returns(request);

            request.WithBodyContent(Arg.Any<HttpContent>())
                   .Returns(x => { throw new AggregateException(new Exception()); });

            LanguageTranslatorService service =
                new LanguageTranslatorService(client);

            service.Identify("any text");
        }
    }
}