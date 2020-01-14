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

using System.Collections.Generic;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.ToneAnalyzer.v3.Model;

namespace IBM.Watson.ToneAnalyzer.v3
{
    public partial interface IToneAnalyzerService
    {
        DetailedResponse<ToneAnalysis> Tone(ToneInput toneInput, string contentType = null, bool? sentences = null, List<string> tones = null, string contentLanguage = null, string acceptLanguage = null);
        DetailedResponse<UtteranceAnalyses> ToneChat(List<Utterance> utterances, string contentLanguage = null, string acceptLanguage = null);
    }
}
