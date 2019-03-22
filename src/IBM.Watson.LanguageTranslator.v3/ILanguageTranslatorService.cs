/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using System.Collections.Generic;
using IBM.Watson.LanguageTranslator.v3.Model;

namespace IBM.Watson.LanguageTranslator.v3
{
    public partial interface ILanguageTranslatorService
    {
        TranslationResult Translate(TranslateRequest request, Dictionary<string, object> customData = null);
        IdentifiedLanguages Identify(string text, Dictionary<string, object> customData = null);
        IdentifiableLanguages ListIdentifiableLanguages(Dictionary<string, object> customData = null);
        TranslationModel CreateModel(string baseModelId, string name = null, System.IO.FileStream forcedGlossary = null, System.IO.FileStream parallelCorpus = null, Dictionary<string, object> customData = null);
        DeleteModelResult DeleteModel(string modelId, Dictionary<string, object> customData = null);
        TranslationModel GetModel(string modelId, Dictionary<string, object> customData = null);
        TranslationModels ListModels(string source = null, string target = null, bool? defaultModels = null, Dictionary<string, object> customData = null);
    }
}
