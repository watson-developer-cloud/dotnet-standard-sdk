/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using IBM.Watson.TextToSpeech.v1.Model;

namespace IBM.Watson.TextToSpeech.v1
{
    public partial interface ITextToSpeechService
    {
        DetailedResponse<Voice> GetVoice(string voice, string customizationId = null);
        DetailedResponse<Voices> ListVoices();
        DetailedResponse<System.IO.MemoryStream> Synthesize(string text, string accept = null, string voice = null, string customizationId = null);
        DetailedResponse<Pronunciation> GetPronunciation(string text, string voice = null, string format = null, string customizationId = null);
        DetailedResponse<VoiceModel> CreateVoiceModel(string name, string language = null, string description = null);
        DetailedResponse<object> DeleteVoiceModel(string customizationId);
        DetailedResponse<VoiceModel> GetVoiceModel(string customizationId);
        DetailedResponse<VoiceModels> ListVoiceModels(string language = null);
        DetailedResponse<object> UpdateVoiceModel(string customizationId, string name = null, string description = null, List<Word> words = null);
        DetailedResponse<object> AddWord(string customizationId, string word, string translation, string partOfSpeech = null);
        DetailedResponse<object> AddWords(string customizationId, List<Word> words);
        DetailedResponse<object> DeleteWord(string customizationId, string word);
        DetailedResponse<Translation> GetWord(string customizationId, string word);
        DetailedResponse<Words> ListWords(string customizationId);
        DetailedResponse<object> DeleteUserData(string customerId);
    }
}
