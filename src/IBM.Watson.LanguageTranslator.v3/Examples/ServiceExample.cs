/**
* (C) Copyright IBM Corp. 2019, 2020.
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

using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.LanguageTranslator.v3.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.LanguageTranslator.v3.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{serviceUrl}";
        string versionDate = "{versionDate}";
        private static string glossaryPath = "LanguageTranslatorTestData/glossary.tmx";
        private static string documentToTranslatePath = "LanguageTranslatorTestData/document-to-translate.txt";
        private string modelId;
        private string documentId;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.Translate();
            example.ListIdentifiableLanguages();
            example.IdentifyLanguage();
            example.ListModels();
            example.CreateModel();
            example.GetModel();
            example.DeleteModel();
            example.ListDocuments();
            example.TranslateDocument();
            example.GetDocumentStatus();
            example.GetTranslatedDocument();
            example.DeleteDocument();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Translation
        public void Translate()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);
            service.SetServiceUrl(url);

            var result = service.Translate(
                text: new List<string>() { "Hello" },
                modelId: "en-es"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Identification
        public void ListIdentifiableLanguages()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.ListIdentifiableLanguages();

            Console.WriteLine(result.Response);
        }

        public void IdentifyLanguage()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.Identify(
                text: "Language translator translates text from one language to another"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Models
        public void ListModels()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.ListModels();

            Console.WriteLine(result.Response);
        }

        public void CreateModel()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            DetailedResponse<TranslationModel> result;

            using (FileStream fs = File.OpenRead("glossary.tmx"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.CreateModel(
                        baseModelId: "en-es",
                        forcedGlossary: ms,
                        name: "custom-en-es"
                        );
                }
            }

            Console.WriteLine(result.Response);
            modelId = result.Result.ModelId;
        }

        public void GetModel()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.GetModel(
                modelId: "9f8d9c6f-2123-462f-9793-f17fdcb77cd6"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteModel()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.DeleteModel(
                modelId: "9f8d9c6f-2123-462f-9793-f17fdcb77cd6"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Documents
        public void ListDocuments()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.ListDocuments();

            Console.WriteLine(result.Response);
        }

        public void TranslateDocument()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            DetailedResponse<DocumentStatus> result;

            using (FileStream fs = File.OpenRead("{filepath}"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.TranslateDocument(
                        file: ms,
                        filename: "{filename}",
                        fileContentType: "{fileContentType}",
                        modelId: "en-fr"
                        );
                }
            }

            Console.WriteLine(result.Response);
            documentId = result.Result.DocumentId;
        }

        public void GetDocumentStatus()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.GetDocumentStatus(
                documentId: "{documentId}"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteDocument()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.DeleteDocument(
                documentId: "{documentId}"
                );

            Console.WriteLine(result.StatusCode);
        }

        public void GetTranslatedDocument()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "{apikey}");

            LanguageTranslatorService service = new LanguageTranslatorService("2018-05-01", authenticator);

            var result = service.GetTranslatedDocument(
                documentId: "{documentId}"
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
