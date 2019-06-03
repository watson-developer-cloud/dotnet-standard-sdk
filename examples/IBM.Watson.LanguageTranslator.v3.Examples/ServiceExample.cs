/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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
using IBM.Cloud.SDK.Core.Util;
using IBM.Watson.LanguageTranslator.v3.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.LanguageTranslator.v3.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
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
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.Translate(
                text: new List<string>() { "I'm sorry, Dave. I'm afraid I can't do that." },
                modelId: "en-fr"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Identification
        public void ListIdentifiableLanguages()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.ListIdentifiableLanguages();

            Console.WriteLine(result.Response);
        }

        public void IdentifyLanguage()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.Identify(
                text: "I'm sorry, Dave. I'm afraid I can't do that."
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Models
        public void ListModels()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.ListModels();

            Console.WriteLine(result.Response);
        }

        public void CreateModel()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            DetailedResponse<TranslationModel> result;

            using (FileStream fs = File.OpenRead(glossaryPath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.CreateModel(
                        baseModelId: "en-fr",
                        forcedGlossary: ms,
                        name: "dotnetExampleModel"
                        );
                }
            }

            Console.WriteLine(result.Response);
            modelId = result.Result.ModelId;
        }

        public void GetModel()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.GetModel(
                modelId: modelId
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteModel()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.DeleteModel(
                modelId: modelId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Documents
        public void ListDocuments()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.ListDocuments();

            Console.WriteLine(result.Response);
        }

        public void TranslateDocument()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            DetailedResponse<DocumentStatus> result;

            using (FileStream fs = File.OpenRead(documentToTranslatePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.TranslateDocument(
                        file: ms,
                        fileContentType: "text/plain",
                        modelId: "en-es"
                        );
                }
            }

            Console.WriteLine(result.Response);
            documentId = result.Result.DocumentId;
        }

        public void GetDocumentStatus()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.GetDocumentStatus(
                documentId: documentId
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteDocument()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.DeleteDocument(
                documentId: documentId
                );

            Console.WriteLine(result.StatusCode);
        }

        public void GetTranslatedDocument()
        {
            TokenOptions tokenOptions = new TokenOptions()
            {
                IamApiKey = apikey,
                ServiceUrl = url
            };

            LanguageTranslatorService service = new LanguageTranslatorService(tokenOptions, versionDate);

            var result = service.GetTranslatedDocument(
                documentId: documentId
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
