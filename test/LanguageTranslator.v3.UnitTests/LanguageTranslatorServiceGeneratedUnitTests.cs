/**
* (C) Copyright IBM Corp. 2018, 2020.
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
using IBM.Watson.LanguageTranslator.v3.Model;
using NSubstitute;
using System;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using System.Threading.Tasks;

namespace IBM.Watson.LanguageTranslator.v3.UnitTests
{
    [TestClass]
    public class LanguageTranslatorServiceUnitTests
    {
        #region Constructor
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_HttpClient_Null()
        {
            LanguageTranslatorService service = new LanguageTranslatorService(httpClient: null);
        }

        [TestMethod]
        public void ConstructorHttpClient()
        {
            LanguageTranslatorService service = new LanguageTranslatorService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorExternalConfig()
        {
            var apikey = System.Environment.GetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL");
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL", "http://www.url.com");
            LanguageTranslatorService service = Substitute.For<LanguageTranslatorService>("versionDate");
            Assert.IsNotNull(service);
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL", url);
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY", apikey);
        }

        [TestMethod]
        public void Constructor()
        {
            LanguageTranslatorService service = new LanguageTranslatorService(new IBMHttpClient());
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void ConstructorAuthenticator()
        {
            LanguageTranslatorService service = new LanguageTranslatorService("versionDate", new NoAuthAuthenticator());
            Assert.IsNotNull(service);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNoVersion()
        {
            LanguageTranslatorService service = new LanguageTranslatorService(null, new NoAuthAuthenticator());
        }

        [TestMethod]
        public void ConstructorNoUrl()
        {
            var apikey = System.Environment.GetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY");
            var url = System.Environment.GetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL");
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY", "apikey");
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL", null);
            LanguageTranslatorService service = Substitute.For<LanguageTranslatorService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/language-translator/api");
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL", url);
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void TestTestTranslateAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'word_count': 9, 'character_count': 14, 'translations': [{'translation': '_Translation'}]}";
            var response = new DetailedResponse<TranslationResult>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TranslationResult>(responseJson),
                StatusCode = 200
            };


            request.As<TranslationResult>().Returns(Task.FromResult(response));

            var result = service.Translate(text: new List<string> { "testString" }, modelId: "testString", source: "testString", target: "testString");

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/translate";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListIdentifiableLanguagesAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'languages': [{'language': 'Language', 'name': 'Name'}]}";
            var response = new DetailedResponse<IdentifiableLanguages>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<IdentifiableLanguages>(responseJson),
                StatusCode = 200
            };


            request.As<IdentifiableLanguages>().Returns(Task.FromResult(response));

            var result = service.ListIdentifiableLanguages();

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/identifiable_languages";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestIdentifyAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'languages': [{'language': 'Language', 'confidence': 10}]}";
            var response = new DetailedResponse<IdentifiedLanguages>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<IdentifiedLanguages>(responseJson),
                StatusCode = 200
            };

            string text = "testString";

            request.As<IdentifiedLanguages>().Returns(Task.FromResult(response));

            var result = service.Identify(text: text);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/identify";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListModelsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'models': [{'model_id': 'ModelId', 'name': 'Name', 'source': 'Source', 'target': 'Target', 'base_model_id': 'BaseModelId', 'domain': 'Domain', 'customizable': true, 'default_model': true, 'owner': 'Owner', 'status': 'uploading'}]}";
            var response = new DetailedResponse<TranslationModels>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TranslationModels>(responseJson),
                StatusCode = 200
            };

            string source = "testString";
            string target = "testString";
            bool? _default = true;

            request.As<TranslationModels>().Returns(Task.FromResult(response));

            var result = service.ListModels(source: source, target: target, _default: _default);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/models";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestCreateModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'model_id': 'ModelId', 'name': 'Name', 'source': 'Source', 'target': 'Target', 'base_model_id': 'BaseModelId', 'domain': 'Domain', 'customizable': true, 'default_model': true, 'owner': 'Owner', 'status': 'uploading'}";
            var response = new DetailedResponse<TranslationModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TranslationModel>(responseJson),
                StatusCode = 200
            };

            string baseModelId = "testString";
            System.IO.MemoryStream forcedGlossary = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            System.IO.MemoryStream parallelCorpus = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string name = "testString";

            request.As<TranslationModel>().Returns(Task.FromResult(response));

            var result = service.CreateModel(baseModelId: baseModelId, forcedGlossary: forcedGlossary, parallelCorpus: parallelCorpus, name: name);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/models";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'status': 'Status'}";
            var response = new DetailedResponse<DeleteModelResult>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DeleteModelResult>(responseJson),
                StatusCode = 200
            };

            string modelId = "testString";

            request.As<DeleteModelResult>().Returns(Task.FromResult(response));

            var result = service.DeleteModel(modelId: modelId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/models/{modelId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetModelAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'model_id': 'ModelId', 'name': 'Name', 'source': 'Source', 'target': 'Target', 'base_model_id': 'BaseModelId', 'domain': 'Domain', 'customizable': true, 'default_model': true, 'owner': 'Owner', 'status': 'uploading'}";
            var response = new DetailedResponse<TranslationModel>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<TranslationModel>(responseJson),
                StatusCode = 200
            };

            string modelId = "testString";

            request.As<TranslationModel>().Returns(Task.FromResult(response));

            var result = service.GetModel(modelId: modelId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/models/{modelId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestListDocumentsAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'documents': [{'document_id': 'DocumentId', 'filename': 'Filename', 'status': 'processing', 'model_id': 'ModelId', 'base_model_id': 'BaseModelId', 'source': 'Source', 'target': 'Target', 'word_count': 9, 'character_count': 14}]}";
            var response = new DetailedResponse<DocumentList>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DocumentList>(responseJson),
                StatusCode = 200
            };


            request.As<DocumentList>().Returns(Task.FromResult(response));

            var result = service.ListDocuments();

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/documents";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestTranslateDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'filename': 'Filename', 'status': 'processing', 'model_id': 'ModelId', 'base_model_id': 'BaseModelId', 'source': 'Source', 'target': 'Target', 'word_count': 9, 'character_count': 14}";
            var response = new DetailedResponse<DocumentStatus>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DocumentStatus>(responseJson),
                StatusCode = 202
            };

            System.IO.MemoryStream file = new System.IO.MemoryStream(Encoding.UTF8.GetBytes("This is a mock file."));
            string filename = "testString";
            string fileContentType = "application/powerpoint";
            string modelId = "testString";
            string source = "testString";
            string target = "testString";
            string documentId = "testString";

            request.As<DocumentStatus>().Returns(Task.FromResult(response));

            var result = service.TranslateDocument(file: file, filename: filename, fileContentType: fileContentType, modelId: modelId, source: source, target: target, documentId: documentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/documents";
            client.Received().PostAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetDocumentStatusAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{'document_id': 'DocumentId', 'filename': 'Filename', 'status': 'processing', 'model_id': 'ModelId', 'base_model_id': 'BaseModelId', 'source': 'Source', 'target': 'Target', 'word_count': 9, 'character_count': 14}";
            var response = new DetailedResponse<DocumentStatus>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<DocumentStatus>(responseJson),
                StatusCode = 200
            };

            string documentId = "testString";

            request.As<DocumentStatus>().Returns(Task.FromResult(response));

            var result = service.GetDocumentStatus(documentId: documentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/documents/{documentId}";
            client.Received().GetAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestDeleteDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var responseJson = "{}";
            var response = new DetailedResponse<object>()
            {
                Response = responseJson,
                Result = JsonConvert.DeserializeObject<object>(responseJson),
                StatusCode = 204
            };

            string documentId = "testString";

            request.As<object>().Returns(Task.FromResult(response));

            var result = service.DeleteDocument(documentId: documentId);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/documents/{documentId}";
            client.Received().DeleteAsync(messageUrl);
        }

        [TestMethod]
        public void TestTestGetTranslatedDocumentAllParams()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var response = new DetailedResponse<byte[]>()
            {
                Result = new byte[4],
                StatusCode = 200
            };
            string documentId = "testString";
            string accept = "application/powerpoint";

            request.As<byte[]>().Returns(Task.FromResult(response));

            var result = service.GetTranslatedDocument(documentId: documentId, accept: accept);

            request.Received().WithArgument("version", versionDate);

            string messageUrl = $"{service.ServiceUrl}/v3/documents/{documentId}/translated_document";
            client.Received().GetAsync(messageUrl);
        }

    }
}
