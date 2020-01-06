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
using System.Collections.Generic;
using IBM.Watson.LanguageTranslator.v3.Model;

namespace IBM.Watson.LanguageTranslator.v3
{
    public partial interface ILanguageTranslatorService
    {
        DetailedResponse<TranslationResult> Translate(List<string> text, string modelId = null, string source = null, string target = null);
        DetailedResponse<IdentifiableLanguages> ListIdentifiableLanguages();
        DetailedResponse<IdentifiedLanguages> Identify(string text);
        DetailedResponse<TranslationModels> ListModels(string source = null, string target = null, bool? _default = null);
        DetailedResponse<TranslationModel> CreateModel(string baseModelId, System.IO.MemoryStream forcedGlossary = null, System.IO.MemoryStream parallelCorpus = null, string name = null);
        DetailedResponse<DeleteModelResult> DeleteModel(string modelId);
        DetailedResponse<TranslationModel> GetModel(string modelId);
        DetailedResponse<DocumentList> ListDocuments();
        DetailedResponse<DocumentStatus> TranslateDocument(System.IO.MemoryStream file, string filename, string fileContentType = null, string modelId = null, string source = null, string target = null, string documentId = null);
        DetailedResponse<DocumentStatus> GetDocumentStatus(string documentId);
        DetailedResponse<object> DeleteDocument(string documentId);
        DetailedResponse<System.IO.MemoryStream> GetTranslatedDocument(string documentId, string accept = null);
    }
}
