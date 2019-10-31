/**
* (C) Copyright IBM Corp. 2018, 2019.
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

using NSubstitute;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Cloud.SDK.Core.Authentication.NoAuth;
using IBM.Watson.LanguageTranslator.v3.Model;
using IBM.Cloud.SDK.Core.Model;

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
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY", "apikey");
            LanguageTranslatorService service = Substitute.For<LanguageTranslatorService>("versionDate");
            Assert.IsNotNull(service);
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
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY", "apikey");
            var url = System.Environment.GetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL");
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL", null);
            LanguageTranslatorService service = Substitute.For<LanguageTranslatorService>("versionDate");
            Assert.IsTrue(service.ServiceUrl == "https://gateway.watsonplatform.net/language-translator/api");
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_URL", url);
            System.Environment.SetEnvironmentVariable("LANGUAGE_TRANSLATOR_APIKEY", apikey);
        }
        #endregion

        [TestMethod]
        public void Translate_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var text = new List<string>();
            var modelId = "modelId";
            var source = "source";
            var target = "target";

            var result = service.Translate(text: text, modelId: modelId, source: source, target: target);

            JObject bodyObject = new JObject();
            if (text != null && text.Count > 0)
            {
                bodyObject["text"] = JToken.FromObject(text);
            }
            if (!string.IsNullOrEmpty(modelId))
            {
                bodyObject["model_id"] = JToken.FromObject(modelId);
            }
            if (!string.IsNullOrEmpty(source))
            {
                bodyObject["source"] = JToken.FromObject(source);
            }
            if (!string.IsNullOrEmpty(target))
            {
                bodyObject["target"] = JToken.FromObject(target);
            }
            var json = JsonConvert.SerializeObject(bodyObject);
            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(json)));
        }
        [TestMethod]
        public void ListIdentifiableLanguages_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;


            var result = service.ListIdentifiableLanguages();

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void Identify_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var text = "text";

            var result = service.Identify(text: text);

            request.Received().WithArgument("version", versionDate);
            request.Received().WithBodyContent(Arg.Is<StringContent>(x => x.ReadAsStringAsync().Result.Equals(text)));
        }
        [TestMethod]
        public void ListModels_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var source = "source";
            var target = "target";
            var _default = false;

            var result = service.ListModels(source: source, target: target, _default: _default);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void CreateModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var baseModelId = "baseModelId";
            var forcedGlossary = new MemoryStream();
            var parallelCorpus = new MemoryStream();
            var name = "name";

            var result = service.CreateModel(baseModelId: baseModelId, forcedGlossary: forcedGlossary, parallelCorpus: parallelCorpus, name: name);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void DeleteModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var modelId = "modelId";

            var result = service.DeleteModel(modelId: modelId);

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v3/models/{modelId}");
        }
        [TestMethod]
        public void GetModel_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var modelId = "modelId";

            var result = service.GetModel(modelId: modelId);

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v3/models/{modelId}");
        }
        [TestMethod]
        public void ListDocuments_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;


            var result = service.ListDocuments();

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void TranslateDocument_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.PostAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var file = new MemoryStream();
            var filename = "filename";
            var fileContentType = "fileContentType";
            var modelId = "modelId";
            var source = "source";
            var target = "target";
            var documentId = "documentId";

            var result = service.TranslateDocument(file: file, filename: filename, fileContentType: fileContentType, modelId: modelId, source: source, target: target, documentId: documentId);

            request.Received().WithArgument("version", versionDate);
        }
        [TestMethod]
        public void GetDocumentStatus_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var documentId = "documentId";

            var result = service.GetDocumentStatus(documentId: documentId);

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v3/documents/{documentId}");
        }
        [TestMethod]
        public void DeleteDocument_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.DeleteAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var documentId = "documentId";

            var result = service.DeleteDocument(documentId: documentId);

            request.Received().WithArgument("version", versionDate);
            client.Received().DeleteAsync($"{service.ServiceUrl}/v3/documents/{documentId}");
        }
        [TestMethod]
        public void GetTranslatedDocument_Success()
        {
            IClient client = Substitute.For<IClient>();
            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                .Returns(request);

            LanguageTranslatorService service = new LanguageTranslatorService(client);
            var versionDate = "versionDate";
            service.VersionDate = versionDate;

            var documentId = "documentId";
            var accept = "accept";

            var result = service.GetTranslatedDocument(documentId: documentId, accept: accept);

            request.Received().WithArgument("version", versionDate);
            client.Received().GetAsync($"{service.ServiceUrl}/v3/documents/{documentId}/translated_document");
        }
    }
}
