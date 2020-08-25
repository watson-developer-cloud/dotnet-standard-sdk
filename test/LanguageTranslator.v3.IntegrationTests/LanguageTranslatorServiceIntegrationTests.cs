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

using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.LanguageTranslator.v3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IBM.Watson.LanguageTranslator.v3.IntegrationTests
{
    [TestClass]
    public class LanguageTranslatorServiceIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private static LanguageTranslatorService service;
        private static string credentials = string.Empty;

        private static string glossaryPath = "LanguageTranslatorTestData/glossary.tmx";
        private static string documentToTranslatePath = "LanguageTranslatorTestData/document-to-translate.txt";
        private static string pptToTranslatePath = "LanguageTranslatorTestData/language-translation-ppt.pptx";
        private static string baseModel = "en-fr";
        private static string customModelName = "dotnetExampleModel";
        private static string customModelID = "en-fr";
        private static string text = "I'm sorry, Dave. I'm afraid I can't do that.";
        private string versionDate = "2019-06-03";

        AutoResetEvent autoEvent = new AutoResetEvent(false);

        [TestInitialize]
        public void Setup()
        {
            service = new LanguageTranslatorService(versionDate);
        }

        [TestMethod]
        public void GetIdentifiableLanguages_Sucess()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.ListIdentifiableLanguages();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Languages.Count > 0);
        }

        [TestMethod]
        public void Identify_Sucess()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.Identify(
                text: text
                );

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Languages.Count > 0);
        }

        [TestMethod]
        public void Translate_Sucess()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.Translate(
                text: new List<string>() { text },
                modelId: baseModel
                );

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Translations.Count > 0);
        }

        [TestMethod]
        public void TranslateAutodetect_Sucess()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.Translate(
                text: new List<string>() { text },
                target: "es"
                );

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Translations.Count > 0);
            Assert.IsTrue(results.Result.DetectedLanguage == "en");
            Assert.IsTrue(results.Result.DetectedLanguageConfidence > 0);
        }

        [TestMethod]
        public void ListModels_Sucess()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.ListModels();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Result.Models.Count > 0);
        }

        [TestMethod]
        public void GetModelDetails_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var results = service.GetModel(
                modelId: baseModel
                );

            Assert.IsNotNull(results);
            Assert.IsFalse(string.IsNullOrEmpty(results.Result.ModelId));
        }

        [TestMethod]
        public void Model_Success()
        {
            DetailedResponse<TranslationModel> createModelResult;

            using (FileStream fs = File.OpenRead(glossaryPath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    createModelResult = service.CreateModel(
                        baseModelId: baseModel,
                        forcedGlossary: ms,
                        name: customModelName
                        );

                    if (createModelResult != null)
                    {
                        customModelID = createModelResult.Result.ModelId;
                    }
                    else
                    {
                        Console.WriteLine("result is null.");
                    }
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var result = service.DeleteModel(
                modelId: customModelID
                );

            Assert.IsNotNull(createModelResult);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result.Status == "OK");
        }

        [TestMethod]
        public void Documents_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listDocumentsResult = service.ListDocuments();

            DetailedResponse<DocumentStatus> translateDocumentResult;
            string documentId;
            using (FileStream fs = File.OpenRead(documentToTranslatePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    translateDocumentResult = service.TranslateDocument(
                        file: ms,
                        filename: Path.GetFileName(documentToTranslatePath),
                        fileContentType: "text/plain",
                        modelId: "en-es"
                        );

                    documentId = translateDocumentResult.Result.DocumentId;
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var getDocumentStatusResult = service.GetDocumentStatus(
                documentId: documentId
                );

            try
            {
                IsDocumentReady(
                    documentId: documentId
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get document...{0}", e.Message);
            }
            autoEvent.WaitOne();

            service.WithHeader("X-Watson-Test", "1");
            var getTranslatedDocumentResult = service.GetTranslatedDocument(
                documentId: documentId
                );

            using (FileStream fs = File.Create("translate.txt"))
            {
                getTranslatedDocumentResult.Result.WriteTo(fs);
                fs.Close();
                getTranslatedDocumentResult.Result.Close();
            }

            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(
                documentId: documentId
                );

            Assert.IsTrue(deleteDocumentResult.StatusCode == 204);
            Assert.IsNotNull(getTranslatedDocumentResult);
            Assert.IsNotNull(getDocumentStatusResult.Result);
            Assert.IsTrue(getDocumentStatusResult.Result.DocumentId == documentId);
            Assert.IsNotNull(translateDocumentResult.Result);
            Assert.IsNotNull(translateDocumentResult.Result.DocumentId);
            Assert.IsNotNull(listDocumentsResult.Result);
        }

        [TestMethod]
        public void DocumentsAutodetect_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listDocumentsResult = service.ListDocuments();

            DetailedResponse<DocumentStatus> translateDocumentResult;
            string documentId;
            using (FileStream fs = File.OpenRead(documentToTranslatePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    service.WithHeader("X-Watson-Test", "1");
                    translateDocumentResult = service.TranslateDocument(
                        file: ms,
                        filename: Path.GetFileName(documentToTranslatePath),
                        fileContentType: "text/plain",
                        target:"es"
                        );

                    documentId = translateDocumentResult.Result.DocumentId;
                }
            }

            service.WithHeader("X-Watson-Test", "1");
            var getDocumentStatusResult = service.GetDocumentStatus(
                documentId: documentId
                );

            try
            {
                IsDocumentReady(
                    documentId: documentId
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get document...{0}", e.Message);
            }
            autoEvent.WaitOne();

            service.WithHeader("X-Watson-Test", "1");
            var getTranslatedDocumentResult = service.GetTranslatedDocument(
                documentId: documentId
                );

            service.WithHeader("X-Watson-Test", "1");
            var deleteDocumentResult = service.DeleteDocument(
                documentId: documentId
                );

            Assert.IsTrue(deleteDocumentResult.StatusCode == 204);
            Assert.IsNotNull(translateDocumentResult.Result);
            Assert.IsNotNull(getDocumentStatusResult.Result);
            Assert.IsTrue(getDocumentStatusResult.Result.DocumentId == documentId);
            Assert.IsTrue(getDocumentStatusResult.Result.DetectedLanguageConfidence > 0);
            Assert.IsTrue(getDocumentStatusResult.Result.Source == "en");
            Assert.IsNotNull(translateDocumentResult.Result.DocumentId);
            Assert.IsNotNull(listDocumentsResult.Result);
        }

        public void IsDocumentReady(string documentId)
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.GetDocumentStatus(
                documentId: documentId
                );

            Console.WriteLine(string.Format("Document status is {0}", result.Result.Status));

            if (result.Result.Status == DocumentStatus.StatusEnumValue.AVAILABLE || result.Result.Status == DocumentStatus.StatusEnumValue.FAILED)
            {
                autoEvent.Set();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(10000);
                    try
                    {
                        IsDocumentReady(documentId);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                });
            }
        }

        // [TestMethod]
        public void ListLanguages_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var listLanguagesResult = service.ListLanguages();

            Assert.IsNotNull(listLanguagesResult.Response);
            Assert.IsTrue(listLanguagesResult.Result._Languages.Count > 0);
        }
    }
}
